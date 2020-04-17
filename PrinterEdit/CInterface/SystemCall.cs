/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Xml;
using System.IO;


namespace BYHXPrinterManager
{
	/// <summary>
	/// Summary description for SystemCall.
	/// </summary>
	public class SystemCall
	{
		[DllImport("User32.dll",CharSet=CharSet.Auto)]
		public static extern uint RegisterWindowMessage(string message);
		[DllImport("Kernel32.dll",CharSet=CharSet.Unicode)]
		public static extern uint Beep(uint dwFreq,uint dwDuration);
	}
}
