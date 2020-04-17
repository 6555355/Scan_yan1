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
		[DllImport ( "user32.dll",  CharSet = CharSet.Auto )]
		public static extern int PostMessage( IntPtr hwnd, uint wMsg, int wParam, int lParam );
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);

        /// <summary>
        /// ���������� �����㣩��ʾ�ɹ������򷵻��㡣������GetLastError
        /// </summary>
        /// <param name="iFrequency">����Ƶ�ʣ���37Hz��32767Hz������windows95�к���</param>
        /// <param name="iDuration">�����ĳ���ʱ�䣬�Ժ���Ϊ��λ����Ϊ-1����ʾһֱ����������ֱ���ٴε��øú���Ϊֹ��</param>
        [DllImport("Kernel32.dll")]
        public static extern bool Beep(int frequency, int duration);

        //Structure for rectangle
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }


        [DllImport("User32", SetLastError = true)]
        public static extern int GetClientRect(
            //handler
                IntPtr hWnd,
                ref RECT lpRect);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE flags);

		[Flags]
		enum EXECUTION_STATE : uint
		{
			ES_SYSTEM_REQUIRED = 0x00000001,
			ES_DISPLAY_REQUIRED = 0x00000002,
			// ES_USER_PRESENT = 0x00000004,
			ES_CONTINUOUS = 0x80000000
		}

		public static void PreventSystemPowerdown()
		{
			//SetThreadExecutionState( EXECUTION_STATE.ES_DISPLAY_REQUIRED | 	EXECUTION_STATE.ES_CONTINUOUS);
			SetThreadExecutionState(EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
		}

		public static void AllowSystemPowerdown()
		{
			SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
		}

    }
}
