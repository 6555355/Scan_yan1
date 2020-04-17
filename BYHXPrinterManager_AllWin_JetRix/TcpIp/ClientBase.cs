// -----------------------------------------------------------------------
// <copyright file="ClientBase.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BYHXPrinterManager.TcpIp
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ClientBase
    {
        private NetworkStream workStream;
        private IPAddress ipAddress;
        private int port;

        public ClientBase(NetworkStream workStream)
        {
            this.workStream = workStream;
        }

        //public ClientBase(IPAddress ip, int port)
        //{
        //        this.port = port;
        //        this.ipAddress = ip;
        //}

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
        // 发送消息到服务端
        public void SendDataBuffer(byte[] data)
        {
            try
            {
                lock (workStream)
                {
                    workStream.Write(data, 0, data.Length);	// 发往服务器
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("+++++");
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}
