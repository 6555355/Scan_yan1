#define OPEN_TCPIP
/* 
	��Ȩ���� 2006��2007��������Դ��о�Ƽ����޹�˾����������Ȩ����
	ֻ�б�������Դ��о�Ƽ����޹�˾��Ȩ�ĵ�λ���ܸ��ĳ�д�ʹ�����
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using System.Threading; 

using BYHXPrinterManager.CInterface;

namespace BYHXPrinterManager.Port
{
	/// <summary>
	/// Summary description for PortManager.
	/// </summary>
	public class PortManager
	{
		SynchronousSocketListener m_TcpPort;
		PipePort m_PipePort;
		bool m_bPipePortOpen;
		IPrinterChange m_IPrinterChange;
		public PortManager(IPrinterChange ipc)
		{
			//
			// TODO: Add constructor logic here
			//
			m_IPrinterChange = ipc;
			TcpIpPortParam param = new TcpIpPortParam();
			IAbort abort = new IAbort();
			m_PipePort = new PipePort(m_IPrinterChange);
#if OPEN_TCPIP
			m_TcpPort = new SynchronousSocketListener(param,abort,m_IPrinterChange);
#endif
			m_bPipePortOpen = false;
		}
		public void TaskStart()
		{
#if OPEN_TCPIP
			ThreadStart tcpThreadStart = new ThreadStart(m_TcpPort.StartListening);
			Thread tcpRecvThread = new Thread(tcpThreadStart);
			tcpRecvThread.IsBackground = true;
			//pRecvThread.Join();
			tcpRecvThread.Start();	
#endif
			if(m_bPipePortOpen)
			{
				ThreadStart pipeThreadStart = new ThreadStart(m_PipePort.WaitConnectThread);
				Thread pipeRecvThread = new Thread(pipeThreadStart);
				pipeRecvThread.IsBackground = true;
				//pRecvThread.Join();
				pipeRecvThread.Start();	
			}

		}
		public void OpenPort()
		{
			m_bPipePortOpen = m_PipePort.OpenPort();
		}
		public void ClosePort()
		{
			//if(m_bPipePortOpen)
			//	m_PipePort.ClosePort();
		}
	}
}
