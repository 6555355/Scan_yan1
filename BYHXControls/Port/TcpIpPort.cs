/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading; 
using System.Runtime.InteropServices;
using System.Diagnostics;

using BYHXPrinterManager;
using PrinterStubC.Utility;

namespace BYHXPrinterManager.Port
{
	/// <summary>
	/// Summary description for TcpIpPort.
	/// </summary>
	/// 


	//This Sever Should use asyn model ////
	//Blocking should in accept, send , receive and receive data
	public class SynchronousSocketListener 
	{
		private const int BUFSIZE = 8*1024;
		private Socket m_hAcceptSocket;
		private TcpIpPortParam m_TcpIpPortParam = new TcpIpPortParam();
		private IAbort m_IAbort;
		IPrinterChange m_IPrinterChange;
	    public CachePrinter cachePrinter;

		// Incoming data from the client.
		//public static string data = null;

		public SynchronousSocketListener(TcpIpPortParam tcpParam,IAbort abort,IPrinterChange ipc)
		{
            LogWriter.WriteLog(new string[] { string.Format("SynchronousSocketListener") }, true);
			m_TcpIpPortParam = tcpParam;
			m_IAbort = abort;
			m_IPrinterChange = ipc;
		    cachePrinter = new CachePrinter(ipc);
		}
		public void StartListening( ) 
		{

			// Establish the local endpoint for the  socket.
			//  Dns.GetHostName returns the name of the 
			// host running the application.
            LogWriter.WriteLog(new string[] { string.Format("StartListening") }, true);
			Socket hSocket;
			IPEndPoint localEndPoint = new IPEndPoint(m_TcpIpPortParam.m_address, m_TcpIpPortParam.m_port);

			// Create a TCP/IP socket.
			hSocket = new Socket(AddressFamily.InterNetwork,
				SocketType.Stream, ProtocolType.Tcp );

			// Bind the socket to the local endpoint and 
			// listen for incoming connections.
			try 
			{
				hSocket.Bind(localEndPoint);
				hSocket.Listen(10);

				// Start listening for connections.
				while (true) 
				{
                    LogWriter.WriteLog(new string[] { string.Format("Waiting for a connection...") }, true);
					Console.WriteLine("Waiting for a connection...");
					// Program is suspended while waiting for an incoming connection.
					///?????????????????????????????????????????????should this how to control it close
					m_hAcceptSocket = hSocket.Accept();

                    LogWriter.WriteLog(new string[] { string.Format("Socket Connect") }, true);

					SPrinterSetting ss= m_IPrinterChange.GetAllParam().PrinterSetting;
                    m_IPrinterChange.GetAllParam().PrinterProperty.SynchronousCalibrationSettings(ref ss);
					LogWriter.WriteLog(new string[]{string.Format("SCalibrationSetting.nStepPerHead={0}", ss.sCalibrationSetting.nStepPerHead)},true);
					// An incoming connection needs to be processed.
					//while (true) 
					ThreadStart myThreadStart = new ThreadStart(ReceiveDataThread);
					Thread pRecvThread = new Thread(myThreadStart);
					pRecvThread.IsBackground = true;
					//pRecvThread.Join();
					pRecvThread.Start();	
				}
      
			} 
			catch (Exception e) 
			{
				Console.WriteLine(e.ToString());
			}

			Console.WriteLine("\nHit enter to continue...");
			//Console.Read();
		}

		public void ReceiveDataThread() 
		{
            LogWriter.WriteLog(new string[] { string.Format("ReceiveDataThread begin") }, true);
			byte []chRequest = new byte[BUFSIZE];
		    //启动发送打印数据线程
		    cachePrinter.Run();

			try
			{
				bool bFileHeader = true;
				int sendBytes = 0;
				// The thread's parameter is a handle to a pipe instance. 
				while (true) 
				{ 
					// Read client requests from the pipe. 
					int bytesRec = m_hAcceptSocket.Receive(chRequest);
                    //LogWriter.WriteLog(new string[] { string.Format("Receive Size:{0}", bytesRec) }, true);
				    if (bytesRec > 0)
				    {
				        MyStruct data = new MyStruct();
                        data.buflen = bytesRec;
				        data.buf = new byte[CachePrinter.BUFSIZE];
                        Buffer.BlockCopy(chRequest, 0, data.buf, 0, bytesRec);
				        cachePrinter.AddPrintData(data);
				    }
				    else if(bytesRec == 0) 
					{
					    cachePrinter.ReceiveDataFinishedFlag(true);
						break;
					}
				} 
			    cachePrinter.WaitSendPrintDataFinish();
				// Flush the pipe to allow the client to read the pipe's contents 
				// before disconnecting. Then disconnect the pipe, and close the 
				// handle to this pipe instance. 
				//FlushFileBuffers(hPipe); 
				m_hAcceptSocket.Shutdown(SocketShutdown.Both);
				m_hAcceptSocket.Close();
			}
			catch(Exception)
			{
				m_hAcceptSocket.Shutdown(SocketShutdown.Both);
				m_hAcceptSocket.Close();
			}
		}

	}
	public class SynchronousSocketClient 
	{
		private Socket m_Socket;
		public SynchronousSocketClient()
		{
		}
		public bool Open(IPAddress ipAdress, Int16 port)
		{
			byte[] bytes = new byte[1024];
			try
			{
				IPEndPoint remoteEP = new IPEndPoint(ipAdress,port);
				// Create a TCP/IP  socket.
				Socket sender = new Socket(AddressFamily.InterNetwork, 
					SocketType.Stream, ProtocolType.Tcp );
				sender.Blocking = true; //???????

				sender.Connect(remoteEP);
				Console.WriteLine("Socket connected to {0}",sender.RemoteEndPoint.ToString());
				m_Socket = sender;
			}
			catch (Exception e) 
			{
				Console.WriteLine( e.ToString());
			}
			Close();
			return false;
		}
		public void Close()
		{
			try 
			{
				// Release the socket.
				if(m_Socket != null)
				{
					m_Socket.Shutdown(SocketShutdown.Both);
					m_Socket.Close();
				}
			} 
			catch (Exception e) 
			{
				Console.WriteLine( e.ToString());
			}
		}
		public int PutDataBuffer(byte [] buffer, int size)
		{
			
			// Send the data through the  socket.
			try 
			{
				int bytesSent = m_Socket.Send(buffer,0,size,SocketFlags.None);
				return bytesSent;
			}
			catch (Exception e) 
			{
				Console.WriteLine( e.ToString());
				return -1;
			}
		}
	}
	public class  TcpIpPortParam
	{
		public IPAddress m_address = IPAddress.Parse("127.0.0.1");
		public Int16 m_port = 9100;
	}
}
