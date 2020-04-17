/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using System.Runtime.InteropServices;

namespace BYHXControls
{
	/// <summary>
	/// Summary description for SerialNoTextBox.
	/// </summary>
	public class SerialNoTextBox:System.Windows.Forms.TextBox
	{
		const int WM_LBUTTONDOWN = 0x0201;
		const int WM_PASTE	= 0x0302;
		public System.IntPtr m_ParentHandle;
		[DllImport("User32.DLL")]
		public extern static Int32 SendMessage(System.IntPtr handle,Int32 msg,Int32 wParam,Int32 lParam);

		
		
		public SerialNoTextBox()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		protected override void WndProc(ref System.Windows.Forms.Message msg)
		{
			if(msg.Msg == WM_PASTE)
			{
				SendMessage(m_ParentHandle, WM_PASTE, 0, 0);
				return;
			}
			base.WndProc(ref msg);
		}

		protected override void OnClick(System.EventArgs e)
		{
			SendMessage(m_ParentHandle, WM_LBUTTONDOWN, 0, 0);
		}
	}
}
