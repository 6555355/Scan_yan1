using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WAF_OnePass.Domain.CInterface;
using WAF_OnePass.Applications.Documents;
using WAF_OnePass.Domain.CoreEnums;
using WAF_OnePass.Domain.CoreStructs;
using WAF_OnePass.Domain.Utility;

namespace TcpIp
{
    public class ClassFileServer
    {
        const int SERVER_PORT = 32147;
        const int BUFF_SIZE = 0x100000;
        private volatile bool bListen = true;
        public event StartPrintJob myEvent;
        private TcpListener m_Server = new TcpListener(IPAddress.Any, SERVER_PORT);

        public void RecvFile()
        {
            int totalpage_num = 0;
            string strJobProperty = "";
            string strJobName = "";
            JobProperty jobproperty = new JobProperty();

            string strDirectory = Path.Combine(PubFunc.StartupPath, "img");
            string strPath = System.IO.Directory.GetCurrentDirectory();
            string strCompletedFile = strPath;
            if (!Directory.Exists(strDirectory))
                Directory.CreateDirectory(strDirectory);
            if (null != strPath)
            {
                strPath = Path.Combine(strDirectory, "printBackup_1.backup");
            }
            m_Server.Start();
            Debug.WriteLine("Data server listen at port " + SERVER_PORT);
            int nSeek = 0;
            while (bListen)
            {
                try
                {
                    TcpClient client = m_Server.AcceptTcpClient();
                    try
                    {
                        while (client.Connected)
                        {
                            Debug.WriteLine("New client " + client.Client.RemoteEndPoint + " connected.");
                            NetworkStream ns = client.GetStream();
                            byte[] buff = new byte[BUFF_SIZE];
                            int bytesRead = ns.Read(buff, 0, 1024);
                            if (bytesRead == 0)
                            {
                                Debug.WriteLine("Remote host has disconnected.");
                                break;
                            }
                            ASCIIEncoding encoder = new ASCIIEncoding();
                            string str = encoder.GetString(buff, 0, 4);
                            if (str != "BYHX")
                            {
                                Debug.WriteLine("Wrong header flag, " + str + " received.");
                                continue;
                            }
                            str = encoder.GetString(buff, 4, 4);
                            Debug.Write("Flag is " + str + ". ");
                            if (str == "jbgn")
                            {
                                Debug.Write("Job begin received.");
                                //取得文件名称开始位置
                                int name_ptr = BitConverter.ToInt32(buff, 16);
                                //取得页数值
                                totalpage_num = BitConverter.ToInt32(buff, 20);
                                //文件长度统计
                                int name_term = name_ptr;
                                while (buff[name_term] != 0) name_term++;
                                //文件属性解析
                                strJobProperty = Encoding.Default.GetString(buff, name_ptr, name_term - name_ptr);

                                //作业属性解析
                                if (!string.IsNullOrEmpty(strJobProperty))
                                {
                                    jobproperty = JobPropertyAnalysis(strJobProperty);
                                    strJobName = jobproperty.Name.Substring(0, jobproperty.Name.LastIndexOf('.')) + "." + jobproperty.Type;
                                }

                                //文件存储文件夹生成
                                if (!Directory.Exists(strDirectory + "\\" + jobproperty.GroupID))
                                    Directory.CreateDirectory(strDirectory + "\\" + jobproperty.GroupID);
                                strCompletedFile = Path.Combine(strDirectory + "\\" + jobproperty.GroupID, strJobName);

                                Debug.Write(" Name:" + jobproperty.Name + ",Index:" + jobproperty.Index +  ",GroupID:" + jobproperty.GroupID + ", TotalPage:" + totalpage_num + ".");
                                if (File.Exists(strPath))
                                {
                                    File.Delete(strPath);
                                }
                                File.Create(strPath).Close();
                                nSeek = 2048;
                            }
                            else if (str == "jend")
                            {
                                if (File.Exists(strPath))
                                {
                                    if (File.Exists(strCompletedFile))
                                    {
                                        //如果Rip过来的图片有同名文件，则在同名文件加_x
                                        string dirName = Path.GetDirectoryName(strCompletedFile);
                                        string fileName = Path.GetFileNameWithoutExtension(strCompletedFile);
                                        string extension = Path.GetExtension(strCompletedFile);
                                        for (int i = 0; i < int.MaxValue; i++)
                                        {
                                            string tempPath = Path.Combine(dirName, string.Format("{0}_{1}{2}", fileName, i, extension));
                                            if (!File.Exists(tempPath))
                                            {
                                                strCompletedFile = tempPath;
                                                break;
                                            }
                                        }
                                    }
                                    File.Copy(strPath, strCompletedFile, true);
                                }
                                if (File.Exists(strCompletedFile))
                                    myEvent.Invoke(this, new MyTcpFileEventArgs(strCompletedFile, jobproperty.Copies, jobproperty.Index, totalpage_num, jobproperty.GroupID));
                                Debug.Write("Job end received.");
                            }
                            else if (str == "band")
                            {
                                int dataSize = BitConverter.ToInt32(buff, 12);
                                Debug.WriteLine(" Data size is " + dataSize);
                                while ((dataSize > 0) && client.Connected && bytesRead != 0)
                                {
                                    int bytesToRead = dataSize;
                                    if (bytesToRead > BUFF_SIZE - 2048) bytesToRead = BUFF_SIZE - 2048;//job header & band header
                                    bytesRead = ns.Read(buff, nSeek, bytesToRead);
                                    dataSize -= bytesRead;
                                    FileStream f = File.Open(strPath, FileMode.Append, FileAccess.Write);
                                    BinaryWriter sw = new BinaryWriter(f);
                                    sw.Write(buff.Skip(nSeek).Take(bytesRead).ToArray());
                                    sw.Flush();
                                    Debug.Write("Band data received.");
                                    nSeek = 0;
                                    sw.Close();
                                    f.Close();
                                }
                            }
                            else if (str == "pbgn")
                            {
                                Debug.Write("Job name received.");
                                //取得文件名称开始位置
                                int name_ptr = BitConverter.ToInt32(buff, 16);
                                //取得页数值
                                int jobId = BitConverter.ToInt32(buff, 20);
                                //文件长度统计
                                int name_term = name_ptr;
                                while (buff[name_term] != 0) name_term++;
                                //文件属性解析
                                string strJobPath = Encoding.Default.GetString(buff, name_ptr, name_term - name_ptr);
                                Debug.Write("Job Path =" + strJobPath + "," + "JobID = " + jobId.ToString() + ".");

                                if (File.Exists(strJobPath))
                                {
                                    myEvent.Invoke(this, new MyTcpFileEventArgs(strJobPath, 1, jobId, 1));
                                }
                                else
                                {
                                    MessageBox.Show(string.Format("Can't Find the file, please ensure that the {0} exists!", strJobPath));                                    
                                }
                            }
                            else
                            {
                                Debug.Write("Unknown block header.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        client.Close();
                    }
                }
                catch (Exception ex)
                {
                    m_Server.Stop();
                    LogWriter.SaveOptionLog("The underlying Socket is closed.", LogLevel.Error);
                }
            }
        }

        public void StartListen()
        {
            ThreadStart myThreatStart = new ThreadStart(RecvFile);
            Thread myThread = new Thread(myThreatStart);
            myThread.Start();
        }

        public void StopListen()
        {
            this.m_Server.Stop();
            this.bListen = false;
        }

        public JobProperty JobPropertyAnalysis(string jobproperty)
        {
            JobProperty job = new JobProperty();
            string[] itemArray = jobproperty.Split('|');
            foreach (var item in itemArray) 
            {
                string[] subitem = item.Split('=');
                if (subitem[0] == "Index")
                    job.Index = Convert.ToInt32(subitem[1]);
                else if (subitem[0] == "Name")
                    job.Name = subitem[1];
                else if (subitem[0] == "GroupID")
                    job.GroupID = subitem[1];
                else if (subitem[0] == "Copies")
                    job.Copies = Convert.ToInt32(subitem[1]);
                else if (subitem[0] == "Type")
                    job.Type = subitem[1];
            }
            return job;
        }

        public struct JobProperty 
        {
            public int Index;
            public string Name;
            public string GroupID;
            public int Copies;
            public string Type;
        }
    }

    public class ClassStatusManager//up dir report
    {
        private volatile bool bListen = true;
        const int SERVER_PORT = 32149;
        private ArrayList msg = ArrayList.Synchronized(new ArrayList(10));
        private TcpListener server = new TcpListener(IPAddress.Any, SERVER_PORT);
        public void StopListen()
        {
            server.Stop();
            this.bListen = false;
        }

        public void AddMsg(string m)
        {
            try
            {
                if (null != m)
                {
                    this.msg.Add((object)m);
                }
            }
        	catch (Exception ex)
            {
                  MessageBox.Show(ex.Message);
            }
        }

        private void ListenStatuPort()
        {
            Debug.WriteLine("Control server listen at port " + SERVER_PORT);
            server.Start();
            while (bListen)
            {
                try
                {
                    Debug.WriteLine("Control server listen at port " + SERVER_PORT);
                    TcpClient client = server.AcceptTcpClient();
                    try
                    {
                        Debug.WriteLine("New client " + client.Client.RemoteEndPoint + " connected.");
                        NetworkStream ns = client.GetStream();
                        StreamWriter sw = new StreamWriter(ns);
                        sw.AutoFlush = true;
                        Debug.WriteLine("New client " + client.Client.RemoteEndPoint + " connected.");
                        while (client.Connected)
                        {
                            while (msg.Count > 0)
                            {
                                int nLast = msg.Count - 1;
                                sw.WriteLine((string)msg[nLast]);
                                msg.RemoveAt(nLast);
                                System.Threading.Thread.Sleep(100);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        client.Close();
                        LogWriter.SaveOptionLog("The underlying Socket is closed.", LogLevel.Error);
                    }
                }
                catch (Exception)
                {
                    server.Stop();
                    //throw;
                }
            }
        }

        public void StartListen()
        {
            ThreadStart myThreadStart = new ThreadStart(ListenStatuPort);
            Thread myThread = new Thread(myThreadStart);
            myThread.Start();
        }
    }
    public class ClassCtrlManager//up dir report
    {
        private volatile bool bListen = true;
        const int SERVER_PORT = 32148;
        private const string strHeader = "<<Status ";
        public EventHandler MyPausePrinter;
        public EventHandler MyCancelPrinter;
        private TcpListener m_Server = new TcpListener(IPAddress.Any, SERVER_PORT);
        public void StopListen()
        {
            m_Server.Stop();
            this.bListen = false;
        }
        public void StartListen() 
        {
            ThreadStart myThreadStart = new ThreadStart(ListentPort);
            Thread myThread = new Thread(myThreadStart);
            myThread.Start();
        }

        public string GetHexString(byte[] data)
        {
            string sss = "";
            for (int i = 0; i < data.Length; i++)
            {
                sss += string.Format("{0:X2}", data[i]);
            }
            return sss;
        }
        public void ListentPort()
        {
            m_Server.Start();
            CADG_PRINT_STATUS hbConfInfo = new CADG_PRINT_STATUS();
            while (bListen)
            {
                try
                {
                    Debug.WriteLine("Control server listen at port " + SERVER_PORT);
                    TcpClient client = m_Server.AcceptTcpClient();
                    try
                    {
                        Debug.WriteLine("New client " + client.Client.RemoteEndPoint + " connected.");
                        NetworkStream ns = client.GetStream();
                        StreamReader sr = new StreamReader(ns);
                        StreamWriter sw = new StreamWriter(ns);
                        while (client.Connected)
                        {
                            string strLine = sr.ReadLine();
                            if (strLine == null) break;
                            Debug.WriteLine(strLine);
                            if (string.Compare(strLine, 0, ">>QueryStatus ", 0, 14, true) != 0)
                                continue;
                            int cmd;
                            int.TryParse(strLine.Substring(14), out cmd);
                            switch (cmd)
                            {
                                case 5: //暂停打印
                                case 6: //恢复打印
                                    {
                                        this.MyPausePrinter(null, null);
                                        sw.WriteLine(strHeader + string.Format("{0:X8}", (int)1));
                                    }
                                    break;
                                case 7: //取消打印
                                    {
                                        this.MyCancelPrinter(null, null);
                                        sw.WriteLine(strHeader + string.Format("{0:X8}", (int)1));
                                    }
                                    break;
                                case 8: //查询错误码
                                    {
                                        int nErrorCode = -1;
                                        int arraynum = 64;
                                        int[] usbIdArray = new int[arraynum];
                                        int usbNum = 0;
                                        if (CoreInterface.EnumUsb(ref usbNum, usbIdArray, arraynum) != 1)
                                            nErrorCode = 0;
                                        else
                                            nErrorCode = CoreInterface.GetBoardError(1);
                                        sw.WriteLine(strHeader + string.Format("{0:X8}",nErrorCode));
                                    }
                                    break;
                                case 9: //查询状态
                                    {
                                        JetStatusEnum status = JetStatusEnum.Unknown;
                                        int arraynum = 64;
                                        int[] usbIdArray = new int[arraynum];
                                        int usbNum = 0;
                                        if (CoreInterface.EnumUsb(ref usbNum, usbIdArray, arraynum) != 1)
                                            status = JetStatusEnum.PowerOff;
                                        else
                                            status = CoreInterface.GetBoardStatus(1);
                                        sw.WriteLine(strHeader + string.Format("{0:X8}",(int)status));
                                    }
                                    break;
                                default:
                                    sw.WriteLine(strHeader + string.Format("{0:X8}", (int)-1));
                                    break;
                            }
                            sw.Flush();
                        }
                    }
                    catch (Exception e)
                    {
                        client.Close();
                        LogWriter.SaveOptionLog("The underlying Socket is closed.",LogLevel.Error);
                    }
                }
                catch (Exception)
                {
                    m_Server.Stop();
                }

            }
        }
    }
}
