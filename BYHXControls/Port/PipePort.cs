/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/

using System;
using System.Collections.Concurrent;
using System.Threading; 
using System.Runtime.InteropServices;
using System.Diagnostics;

using BYHXPrinterManager;
using BYHXPrinterManager.CInterface;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
using PrinterStubC.Utility;

namespace BYHXPrinterManager.Port
{
	/// <summary>
	/// Summary description for PipePort.
	/// </summary>
	public class PipePort
	{
		public static string MUTEX_PORT_OPEN  = "MUTEX_NAME_BYHX_PORTOPEN";
        public static string PipeName = "\\\\.\\pipe\\pipenamePIPENAME_BYHX";
		public const int PIPE_TIMEOUT = 1000; //1000ms
		public int  m_hPipeHandle;
		IPrinterChange m_IPrinterChange;

        bool isRipPrintData = false;
	    public CachePrinter cachePrinter;
		public PipePort(IPrinterChange ipc)
		{
			//
			// TODO: Add constructor logic here
			//
			m_hPipeHandle = 0;
			m_IPrinterChange = ipc;
		    cachePrinter = new CachePrinter(ipc);
            LogWriter.WriteLog(new string[] { "[Test]PipePort init" }, true);
		}
		public bool OpenPort()
		{
            LogWriter.WriteLog(new string[] { "[Test]OpenPort" }, true);
			m_hPipeHandle = CNamedPipe.CreateNamedPipe( 
				PipeName,             // pipe name 
				CNamedPipe.PIPE_ACCESS_INBOUND,       // read/write access 
				CNamedPipe.PIPE_TYPE_BYTE|       // message type pipe 
				CNamedPipe.PIPE_READMODE_BYTE |   // message-read mode 
				CNamedPipe.PIPE_WAIT,                // blocking mode 
				1, // max. instances  
				CachePrinter.BUFSIZE,                  // output buffer size 
				CachePrinter.BUFSIZE,                  // input buffer size 
				PIPE_TIMEOUT,             // client time-out 
				IntPtr.Zero);                    // no security attribute 

			if (m_hPipeHandle == CNamedPipe.INVALID_HANDLE_VALUE) 
			{

				m_hPipeHandle = 0;
				int errorcode = CNamedPipe.GetLastError();
                LogWriter.WriteLog(new string[] { "[Test]OpenPort false" }, true);
				return false;
			}

            LogWriter.WriteLog(new string[] { "[Test]OpenPort true" }, true);
			return true;
			
		}
		public void ClosePort()
		{
			if(m_hPipeHandle != 0)
			{
				CNamedPipe.CloseHandle(m_hPipeHandle);
				m_hPipeHandle = 0;
			}
		}

		public void WaitConnectThread()
		{
			while(true)
			{	
				bool bConnetct = WaitConnect();
                //LogWriter.WriteLog(new string[] { "[Test]WaitConnectThread:Connect" + bConnetct.ToString() }, true);
				if(bConnetct)
				{
					SPrinterSetting ss= m_IPrinterChange.GetAllParam().PrinterSetting;
                    m_IPrinterChange.GetAllParam().PrinterProperty.SynchronousCalibrationSettings(ref ss);
					LogWriter.WriteLog(new string[]{string.Format("SCalibrationSetting.nStepPerHead={0}", ss.sCalibrationSetting.nStepPerHead)},true);

					ThreadStart myThreadStart = new ThreadStart(ReceiveDataThread);
					Thread pRecvThread = new Thread(myThreadStart);
					pRecvThread.IsBackground = true;
					//pRecvThread.Join();
					pRecvThread.Start();
				}
                Thread.Sleep(200);
			}
		}
        bool fConnected = false;
		public bool WaitConnect()
		{
		    if (fConnected)
		        return false;
            LogWriter.WriteLog(new string[] { "[Test]WaitConnect1" }, true);
            if (m_hPipeHandle != 0)
			{
				if(CNamedPipe.ConnectNamedPipe(m_hPipeHandle, IntPtr.Zero))
				{
                    LogWriter.WriteLog(new string[] { "[Test]WaitConnect2" }, true);
					fConnected = true;
				}
                else if ((CNamedPipe.GetLastError() == CNamedPipe.ERROR_PIPE_CONNECTED))
                {
                    LogWriter.WriteLog(new string[] { "[Test]WaitConnect3" }, true);
                    fConnected = false;
                }
                else
                {
                    LogWriter.WriteLog(new string[] { "[Test]WaitConnect4" }, true);
                    fConnected = false;
                }
			}
			return fConnected;
		}



		public void ReceiveDataThread() 
		{
            LogWriter.WriteLog(new string[] { "[Test]ReceiveData begin" }, true);

            string strRipFileName = "RipPrintData.prt";

            //SystemCall.isPmPrint = false;

            byte[] chRequest = new byte[CachePrinter.BUFSIZE]; 
			int cbBytesRead = 0; 
			bool fSuccess = false; 
            bool isSetWhiteFormPrt = false;
            bool isUseJobModeSetting = false;
            SPrinterSetting oldPrinterSetting = m_IPrinterChange.GetAllParam().PrinterSetting;

            //UVData oldUVData = new UVData();
            //oldUVData.Load();
            //启动发送打印数据线程
		    cachePrinter.Run();

			Mutex mutex = new Mutex(true,MUTEX_PORT_OPEN);
			int sendBytes = 0;

            if (isRipPrintData)
            {
                File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strRipFileName));
            }

			// The thread's parameter is a handle to a pipe instance. 
			while (true) 
			{
                // Read client requests from the pipe. 
				if(m_hPipeHandle != 0)
				{
				    cbBytesRead = 0;
					fSuccess = CNamedPipe.ReadFile( 
						m_hPipeHandle,        // handle to pipe 
						chRequest,    // buffer to receive data 
						CachePrinter.BUFSIZE,      // size of buffer 
						ref cbBytesRead, // number of bytes read 
						0);        // not overlapped I/O 
				}
				if(fSuccess)
				{
                    MyStruct data = new MyStruct();
				    data.buflen = cbBytesRead;
                    data.buf = new byte[CachePrinter.BUFSIZE];
                    Buffer.BlockCopy(chRequest, 0, data.buf, 0, cbBytesRead);
				    cachePrinter.AddPrintData(data);
				}
				if (! fSuccess || cbBytesRead == 0)
				{
                    cachePrinter.ReceiveDataFinishedFlag(true);
					break;
				}
			}
		    cachePrinter.WaitSendPrintDataFinish();

            if (isSetWhiteFormPrt || isUseJobModeSetting)
            {
                m_IPrinterChange.GetAllParam().PrinterSetting = oldPrinterSetting;
                CoreInterface.SetPrinterSetting(ref m_IPrinterChange.GetAllParam().PrinterSetting);
                LogWriter.WriteLog(new string[] { "[RIP]SetOldPrinterSetting" }, true);
            }

            if (isUseJobModeSetting)
            {
                //oldUVData.Save();
            }

            
            LogWriter.WriteLog(new string[] { "[RIP]Printer close" }, true);
			mutex.Close();


			// Flush the pipe to allow the client to read the pipe's contents 
			// before disconnecting. Then disconnect the pipe, and close the 
			// handle to this pipe instance. 
			//FlushFileBuffers(hPipe); 
			CNamedPipe.DisconnectNamedPipe(m_hPipeHandle); 
			///CloseHandle(hPipe);
		    fConnected = false;
		}

	}
}
