/* 
	��Ȩ���� 2006��2007��������Դ��о�Ƽ����޹�˾����������Ȩ����
	ֻ�б�������Դ��о�Ƽ����޹�˾��Ȩ�ĵ�λ���ܸ��ĳ�д�ʹ�����
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
