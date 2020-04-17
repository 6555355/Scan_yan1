/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using System.Drawing;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Data;
using System.IO;
using System.Xml;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

//using BYHXPrinterManager.JobListView;
namespace BYHXPrinterManager
{
	/// <summary>
	/// Summary description for System.
	/// </summary>
	///
	public class SystemInit
	{
		
		private IPrinterChange m_IPrinterChange;
		private IntPtr		   m_MessageWindowHandle;
		private uint		m_KernelMessage;

		public SystemInit(IPrinterChange iPrinterChange,IntPtr		   messageWindowHandle,uint  Messagecode)
		{
			//
			// TODO: Add constructor logic here
			//
			m_IPrinterChange = iPrinterChange;
			m_MessageWindowHandle = messageWindowHandle;
			m_KernelMessage = Messagecode;
		}
		public bool SystemStart()
		{
			CoreInterface.SystemInit();
			//CoreInterface.SendJetCommand((int)JetCmdEnum.ResetBoard,0);
			if( CoreInterface.SetMessageWindow(m_MessageWindowHandle,m_KernelMessage)== 0)
			{
					return false;
			}

			//AllParam allParam = new AllParam();
			AllParam allParam = m_IPrinterChange.GetAllParam();
			allParam.LoadFromXml(null,true);
			m_IPrinterChange.OnPreferenceChange(allParam.Preference);
			m_IPrinterChange.OnPrinterPropertyChange(allParam.PrinterProperty);
			m_IPrinterChange.OnPrinterSettingChange(allParam.PrinterSetting);
	
			//Must after printer property because status depend on property sensor measurepaper
			JetStatusEnum status = CoreInterface.GetBoardStatus();
			m_IPrinterChange.OnPrinterStatusChanged(status);
			int errcode = CoreInterface.GetBoardError();
			if(errcode != 0)
				m_IPrinterChange.OnErrorCodeChanged(errcode);

            m_IPrinterChange.LoadJobList();
			StartWorking();
			return true;

		}
		public void SystemEnd()
		{
			//CoreInterface.SavePrinterSetting();
			EndWorking();
			m_IPrinterChange.SaveJobList();
			AllParam allParam = m_IPrinterChange.GetAllParam();
			if(allParam != null)
			{
				allParam.SaveToXml(null,true);
			}
			
			CoreInterface.SystemClose();
		}

		private void StartWorking()
		{
		}

		public void EndWorking()
		{
		}


	}
}
