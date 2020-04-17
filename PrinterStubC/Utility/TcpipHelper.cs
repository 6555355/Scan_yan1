﻿// -----------------------------------------------------------------------
// <copyright file="TcpipHelper.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace WAF_OnePass.Domain.Utility
{
    using System.Collections;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TcpipHelper
    {
        public static IPAddress GetCurrentIpAddress()
        {
            IPAddress ip = new IPAddress(new byte[] { 127, 0, 0, 1 });
            foreach (IPAddress ipAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (!ipAddress.IsIPv6LinkLocal && ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    ip = ipAddress;
                    break;
                }
            }
            return ip;
        }

        /// <summary> 
        /// 获取第一个可用的端口号 
        /// </summary> 
        /// <returns></returns> 
        public static int GetFirstAvailablePort()
        {
            int MAX_PORT = 65535; //系统tcp/udp端口数最大是65535 
            int BEGIN_PORT = 5000;//从这个端口开始检测 

            for (int i = BEGIN_PORT; i < MAX_PORT; i++)
            {
                if (PortIsAvailable(i)) return i;
            }

            return -1;
        }

        /// <summary> 
        /// 获取操作系统已用的端口号 
        /// </summary> 
        /// <returns></returns> 
        private static IList PortIsUsed()
        {
            //获取本地计算机的网络连接和通信统计数据的信息 
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

            //返回本地计算机上的所有Tcp监听程序 
            IPEndPoint[] ipsTCP = ipGlobalProperties.GetActiveTcpListeners();

            //返回本地计算机上的所有UDP监听程序 
            IPEndPoint[] ipsUDP = ipGlobalProperties.GetActiveUdpListeners();

            //返回本地计算机上的Internet协议版本4(IPV4 传输控制协议(TCP)连接的信息。 
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            IList allPorts = new ArrayList();
            foreach (IPEndPoint ep in ipsTCP) allPorts.Add(ep.Port);
            foreach (IPEndPoint ep in ipsUDP) allPorts.Add(ep.Port);
            foreach (TcpConnectionInformation conn in tcpConnInfoArray) allPorts.Add(conn.LocalEndPoint.Port);

            return allPorts;
        }

        /// <summary> 
        /// 检查指定端口是否已用
        /// </summary> 
        /// <param name="port"></param> 
        /// <returns></returns> 
        private static bool PortIsAvailable(int port)
        {
            bool isAvailable = true;

            IList portUsed = PortIsUsed();

            foreach (int p in portUsed)
            {
                if (p == port)
                {
                    isAvailable = false; break;
                }
            }

            return isAvailable;
        }

    }
}
