// -----------------------------------------------------------------------
// <copyright file="ClientBase.cs" company="BYHX">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using PrinterStubC.Utility;
using WAF_OnePass.Domain.Utility;

namespace TcpIpBase
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RemoteControlClient //: Socket
    {
        private NetworkStream workStream;
        private string ipAddress;
        private int port;
        private IPEndPoint remoteEP;
        private TcpClient remoteTcp;
        private volatile bool bconnect = false;
        private volatile bool bAbort = false;
        private const int BufferSize = 8192;

        private byte[] m_MessageBuf;
        public Mutex m_MessageMutex = new Mutex();
        BackgroundWorker autoConnectWorker;
        BackgroundWorker worker;
        private ProtocolHandler _handler;

        public RemoteControlClient(ProtocolHandler handler)
        {
            _handler = handler;
            autoConnectWorker = new BackgroundWorker();
            autoConnectWorker.DoWork += new DoWorkEventHandler(worker_DoWork);
            autoConnectWorker.WorkerSupportsCancellation = true;
            m_MessageBuf = new byte[BufferSize];
        }
        public BackgroundWorker Worker
        {
            get
            {
                return this.worker;
            }
            set
            {
                this.worker = value;
            }
        }

        public string IpAddress
        {
            get { return this.ipAddress; }
            set { this.ipAddress = value; }
        }

        public int Port
        {
            get { return this.port; }
            set { this.port = value; }
        }

        public bool ConnectStatus
        {
            get { return bconnect; }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!bconnect && !bAbort)
            {
                try
                {
                    remoteEP = new IPEndPoint(IPAddress.Parse(ipAddress), port);
                    remoteTcp = new TcpClient();
                    remoteTcp.Connect(remoteEP);
                    LogWriter.SaveOptionLog(string.Format("TcpIpClient---Already Connect to {0}.", (EndPoint)remoteEP));
                    workStream = remoteTcp.GetStream();
                    this.BeginRead(workStream, m_MessageBuf);
                    bconnect = true;
                    while (bconnect)
                    {
                        if (bAbort)
                            break;
                        Thread.Sleep(100);
                    }
                }
                catch (Exception ex)
                {
                    LogWriter.SaveOptionLog(string.Format("TcpIpClient.worker_DoWork---{0}", ex.Message));
                    bconnect = false;
                }
            }
        }

        /// <summary>
        /// 打开Socket
        /// </summary>
        public void Open(string ip, int port)
        {
            this.port = port;
            this.ipAddress = ip;

            if (!autoConnectWorker.IsBusy)
                autoConnectWorker.RunWorkerAsync();
        }


        /// <summary>
        /// 收发消息
        /// </summary>
        public bool SendAndReciverDataBuffer(byte[] data, ref int datasize, byte[] retbuf)
        {
            try
            {
                m_MessageMutex.WaitOne();
                if (workStream.CanWrite)
                {
                    workStream.Write(data, 0, datasize);
                    Debug.WriteLine(string.Format(
                        "****SendAndReciverDataBuffer Send datasize ={0},data={1},datastr={2}", datasize,
                        string.Join(",", data), ASCIIEncoding.ASCII.GetString(data)));
                    datasize = workStream.Read(retbuf, 0, retbuf.Length);
                    Debug.WriteLine(
                        string.Format("****SendAndReciverDataBuffer Recive datasize ={0},data={1},datastr={2}", datasize,
                            string.Join(",", retbuf.Take(datasize).ToArray()),
                            ASCIIEncoding.ASCII.GetString(retbuf.Take(datasize).ToArray())));
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                bconnect = false;
                Debug.WriteLine(string.Format("TcpIpClient.SendAndReciverDataBuffer---{0}", ex.Message));
                LogWriter.SaveOptionLog(string.Format("TcpIpClient.SendAndReciverDataBuffer---{0}", ex.Message));
                return false;
            }
            finally
            {
                m_MessageMutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// 关闭Socket
        /// </summary>
        public void Close()
        {
            bAbort = true;
            //if (!bconnect)
            //    return;
            if (workStream != null)
                workStream.Close();
            if (remoteTcp != null)
                remoteTcp.Close();
            bconnect = false;
        }

        // 开始进行读取
        public void BeginRead(NetworkStream stream, byte[] buffer)
        {
            AsyncCallback callBack = new AsyncCallback(OnReadComplete);
            stream.BeginRead(buffer, 0, BufferSize, callBack, stream);
        }

        // 再读取完成时进行回调
        private void OnReadComplete(IAsyncResult ar)
        {
            int bytesRead = 0;
            try
            {
                NetworkStream stream = (NetworkStream)ar.AsyncState;
                lock (stream)
                {
                    bytesRead = stream.EndRead(ar);
                    Debug.WriteLine(string.Format("Reading data, {0} bytes ...", bytesRead));
                }

                if (bytesRead != 0)
                {
                    string msg = _handler.Helper.ProtocolEncoding.GetString(m_MessageBuf, 0, bytesRead);
                    Array.Clear(m_MessageBuf, 0, m_MessageBuf.Length);		// 清空缓存，避免脏读

                    // 获取protocol数组
                    List<Protocol> protocolArray = _handler.GetProtocol(msg);

                    foreach (Protocol pro in protocolArray)
                    {
                        // 这里异步调用，不然这里可能会比较耗时
                        ParameterizedThreadStart start = new ParameterizedThreadStart(this.HandleProtocol);
                        start.BeginInvoke(pro, null, null);
                    }
                }
                else
                {
                    return;
                    //throw new Exception("收到0字节");
                }
                // 再次调用BeginRead()，完成时调用自身，形成无限循环
                lock (stream)
                {
                    AsyncCallback callBack = new AsyncCallback(OnReadComplete);
                    stream.BeginRead(m_MessageBuf, 0, BufferSize, callBack, stream);
                }
            }
            catch (Exception ex)
            {
                //if (streamToClient != null)
                //    streamToClient.Dispose();
                remoteTcp.Close();
                if (worker != null)
                {
                    Protocol status = _handler.Helper.MakeInfoProtocol(ex.Message);// new Protocol((byte)TcpIpCmdEnum.StatusDirty, 0, ex.Message, CmdReturnValueEnum.Error, 0);
                    worker.ReportProgress(0, status);
                }
                Debug.WriteLine(ex.Message);		// 捕获异常时退出程序
                //OnTcpClientClosed(new EventArgs());
                //MessageBox.Show(ex.Message);
            }
        }

        // 处理protocol
        private void HandleProtocol(object obj)
        {
            Protocol protocol = (Protocol)obj;
            try
            {
                if (worker != null)
                {
                    worker.ReportProgress(0, protocol);
                }
                else
                {
                    MessageBox.Show("worker is null");
                }
                //MessageBox.Show(protocol.Parameter);
                //this.SendCmdReturnValue(protocol, CmdReturnValueEnum.Success);
            }
            catch (Exception ex)
            {
                protocol.Parameter = ex.Message;
                protocol.CmdReturnValue = CmdReturnValueEnum.Error;
                this.SendCmd(protocol);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="protocol"></param>
        /// <returns>是否发送成功</returns>
        public bool SendCmd(Protocol protocol)
        {
            try
            {
                if(workStream != null)
                {
                    //protocol.RepeatPos = repeatPos;
                    byte[] data = _handler.Helper.ToBytes(protocol);
                    string tt= string.Join(",", data);
                    string hex = string.Empty;
                    for (int i = 0; i < data.Length; i++)
                    {
                        hex += "0x"+data[i].ToString("X2");
                    }
                    workStream.Write(data, 0, data.Length);
                    return true;
                }
                //MessageBox.Show("连接丢失!!");
                return false;
            }
            catch (Exception ex)
            {
                if (worker != null)
                {
                    Protocol status = _handler.Helper.MakeInfoProtocol(ex.Message);//new Protocol((byte)TcpIpCmdEnum.StatusDirty, 0, ex.Message, CmdReturnValueEnum.Error, 0);
                    worker.ReportProgress(0, status);
                }
                return false;
            }
        }

    }
}
