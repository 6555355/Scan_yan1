// -----------------------------------------------------------------------
// <copyright file="RemoteClient.cs" company="">
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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using PrinterStubC.Utility;
using WAF_OnePass.Domain.Utility;
using TcpIpBase.Byhx;

namespace TcpIpBase
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RemoteControlServer:IDisposable
    {
        private TcpClient client;
        Dictionary<IPEndPoint, NetworkStream> clients = new Dictionary<IPEndPoint, NetworkStream>();
        //private NetworkStream streamToClient;
        private const int BufferSize = 512;
        //private byte[] buffer;
        Dictionary<NetworkStream, byte[]> buffers = new Dictionary<NetworkStream, byte[]>();

        private ProtocolHandler _handler;
        private TcpListener listener;

        private IPAddress ipAddress;
        private int port;

        private MyBackgroundWorker worker;

        //public event EventHandler TcpClientClosed;
        //public event CustomCommandEventHandler ReceiveNewCommond;
        private bool isDirty = false;
        //private SystemSetting systemSetting;

        //public PrinterStatus Status { get; set; }

        public RemoteControlServer(IPAddress ip, int port, ProtocolHandler handler)
        {
            try
            {
                this.ipAddress = ip;
                this.port = port;
                listener = new TcpListener(ip, port);
                Debug.WriteLine("Server is running ... ");
                listener.Start(); // 开启对控制端口的侦听
                Debug.WriteLine("Start Listening ...");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //buffer = new byte[BufferSize];
            _handler = handler;
        }

        public MyBackgroundWorker Worker
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
        public IPAddress IpAddress
        {
            get
            {
                return this.ipAddress;
            }
            set
            {
                this.ipAddress = value;
            }
        }

        public int Port
        {
            get
            {
                return this.port;
            }
            set
            {
                this.port = value;
            }
        }

        public bool IsDirty
        {
            get
            {
                return this.isDirty;
            }
            set
            {
                this.isDirty = value;
            }
        }

        /// <summary>
        /// 是否已经连接
        /// </summary>
        public bool HasConnected
        {
            get
            {
                NetworkStream stream = null;
                foreach (var networkStream in clients.Values)
                {
                    stream = networkStream;
                    break;
                }
                return stream != null;
            }
        }

        private void OnTcpClientClosed(EventArgs e)
        {
            this.Run();
        }

        //private void OnReceiveNewCommond(TcpIpCmdEnum command)
        //{
        //    CustomCommandEventHandler handler = this.ReceiveNewCommond;
        //    if (handler != null)
        //    {
        //        handler(new CustomCommandEventArgs(command));
        //    }
        //}


        public void Run()
        {
            while (true)
            {
                // 获取一个连接，同步方法，在此处中断
                 client = listener.AcceptTcpClient();
                // 打印连接到的客户端信息
                string msg = string.Format("Client Connected！{0} <-- {1}", client.Client.LocalEndPoint, client.Client.RemoteEndPoint);
                Debug.WriteLine(msg);
                if(worker != null)
                {
                    Protocol status = _handler.Helper.MakeInfoProtocol(msg);
                    worker.ReportProgress(0, status);
                }

                // 获得流
                NetworkStream streamToClient = client.GetStream();
                this.Reset();
                clients.Add((IPEndPoint)client.Client.RemoteEndPoint, streamToClient);
                buffers.Add(streamToClient, new byte[BufferSize]);
                this.BeginRead(streamToClient, buffers[streamToClient]);
                //break;
            }
        }
        // 开始进行读取
        public void BeginRead(NetworkStream stream,byte[] buffer)
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
                    Debug.WriteLine(string.Format("Reading data, {0} bytes ...",  bytesRead));
                }

                if (bytesRead != 0)
                {
                    string hex = string.Empty;
                    for (int i = 0; i < bytesRead; i++)
                    {
                        hex += string.Format("{0},", buffers[stream][i]);
                    }
                    LogWriter.SaveOptionLog(string.Format("OnReadComplete  hexstring={0}", hex));
                    string msg = _handler.Helper.ProtocolEncoding.GetString(buffers[stream], 0, bytesRead);
                    LogWriter.SaveOptionLog(string.Format("OnReadComplete  Encodingstring={0}", msg));
                    Array.Clear(buffers[stream], 0, buffers[stream].Length);		// 清空缓存，避免脏读

                    // 获取protocol数组
                    List<Protocol> protocolArray = _handler.GetProtocol(msg);

                    foreach (Protocol pro in protocolArray)
                    {
#if false
                        // 这里异步调用，不然这里可能会比较耗时
                        ParameterizedThreadStart start = new ParameterizedThreadStart(this.HandleProtocol);
                        start.BeginInvoke(pro, null, null);
#else
                        HandleProtocol(pro);
#endif
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
                    stream.BeginRead(buffers[stream], 0, BufferSize, callBack, stream);
                }
            }
            catch (Exception ex)
            {
                string protocolstr = ex.Message;
                LogWriter.SaveOptionLog("OnReadComplete.Excepton:" + protocolstr);
                //if (streamToClient != null)
                //    streamToClient.Dispose();
                client.Close();
                if (worker != null)
                {
                    Protocol status = _handler.Helper.MakeInfoProtocol(ex.Message);
                    worker.ReportProgress(0, status);
                }
                Debug.WriteLine(ex.Message);		// 捕获异常时退出程序
                //OnTcpClientClosed(new EventArgs());
                //MessageBox.Show(ex.Message);
                this.Reset();
            }
        }

        // 处理protocol
        private void HandleProtocol(Protocol protocol)
        {
            //Protocol protocol = (Protocol)obj;
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
                string protocolstr = ex.Message;
                LogWriter.SaveOptionLog("HandleProtocol.Excepton:" + protocolstr);
                protocol.Parameter = ex.Message;
                protocol.CmdReturnValue = CmdReturnValueEnum.Error;
                 this.SendCmdReturnValue(protocol);
            }
        }


        public void SendCmdReturnValue(Protocol protocol)
        {
            try
            {
                string protocolstr = string.Format("protocol.Send.Cmd={0},protocol.SubCmd={1},protocol.Parameter={2}", protocol.Cmd, ((ProtocolByhx)protocol).SubCmd, ((ProtocolByhx)protocol).Parameter);
                LogWriter.SaveOptionLog(protocolstr);

                NetworkStream stream = null;
#if false
                for (int i = 0; i < this.clients.Count; i++)
                {
                    IPEndPoint ipEndPoint = new IPEndPoint(protocol.Ip, protocol.Port);
                    if (clients.ContainsKey(ipEndPoint))
                    {
                        stream = clients[ipEndPoint];
                    }
                }
#else
                foreach (var networkStream in clients.Values)
                {
                    stream = networkStream;
                    break;
                }
#endif
                if (stream != null)
                {
                    //protocol.RepeatPos = repeatPos;
                    byte[] data = _handler.Helper.ToBytes(protocol);
                    stream.Write(data, 0, data.Length);
                }
                else
                {
                    //MessageBox.Show("连接丢失!!");
                    protocolstr = "protocol.Send.TcpClientLost";
                    LogWriter.SaveOptionLog(protocolstr);
                }
            }
            catch (Exception ex)
            {
                string protocolstr = ex.Message;
                LogWriter.SaveOptionLog("protocol.Send.Excepton:" + protocolstr);
                if (worker != null)
                {
                    Protocol status = _handler.Helper.MakeInfoProtocol(ex.Message);
                    worker.ReportProgress(0, status);
                }
            }
        }

        private void Reset()
        {
            this.clients.Clear();
            this.buffers.Clear();
        }

        public void Dispose()
        {
            if(client!= null)
                client.Close();
            this.listener.Stop();
            this.listener.Server.Dispose();
            this.listener = null;
            this.worker.CancelAsync();
        }
    }

}
