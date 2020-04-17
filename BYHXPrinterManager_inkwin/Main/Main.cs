/* 
	��Ȩ���� 2006��2007��������Դ��о�Ƽ����޹�˾����������Ȩ����
	ֻ�б�������Դ��о�Ƽ����޹�˾��Ȩ�ĵ�λ���ܸ��ĳ�д�ʹ�����
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Reflection;
//using BYHXPrinterManager.Calibration;
namespace BYHXPrinterManager.Main
{
	/// <summary>
	/// Summary description for Main.
	/// </summary>
	public class MainEntry
	{
		[STAThread]
		static void Main(string[] args) 
		{
#if EpsonLcd
			bool showUI = true;
			try
			{
				if(args != null && args.Length > 0)
				{
					showUI = bool.Parse(args[0]);
				}
			}
			catch
			{
//				showUI = false;
			}
#endif
			// enable XP theme support
			Application.EnableVisualStyles();
			Application.DoEvents();

			//SetLanguage();
			//Chinese XP system
			if(((System.Globalization.CultureInfo.InstalledUICulture.LCID &0xff) == 04)
				&&System.Environment.OSVersion.Version.Major == 5 
				&& System.Environment.OSVersion.Version.Minor == 1)
			{
				string filename = System.Environment.SystemDirectory+ Path.DirectorySeparatorChar
					 + "conime.exe";
				if(File.Exists(filename))
				{
					System.Diagnostics.ProcessStartInfo  Info  =  new  System.Diagnostics.ProcessStartInfo();
					Info.FileName  =  filename;

					System.Diagnostics.Process  Proc  ;
					try
					{
						Proc  =  System.Diagnostics.Process.Start(Info);
					}
					catch//(System.ComponentModel.Win32Exception  e)
					{
						return;
					}
					//�ȴ�3����
					//Proc.WaitForExit(3000);

				}
			}

			const string MUTEX = CoreConst.c_MUTEX_App;
			bool createdNew  = false;
			Mutex mutex = new Mutex(true,MUTEX,out createdNew);
			if(!createdNew)
			{
#if EpsonLcd
				Process process = RuningInstance();
                MyData data = ProcessMessaging.GetShareMem();//��newһ��Ҳ��
                data.InfoCode = showUI?1:0;//��Ϣ��
                data.ProcessID = process.Id;//���ս���ID
                ProcessMessaging.SetShareMem(data);
				Thread.Sleep(100);
				return;
#else
				MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError),UIError.OnlyOneProgram));
				mutex.Close();
				return;
#endif
			}

#if EpsonLcd
			int lcid  = GetLanguage();
			if(lcid == -1)
			{
				// δ����usb��,�޷���ȡ�������,������ֹ.
				MessageBox.Show(ResString.GetResString("NoFoundUSBReturn"),ResString.GetProductName(),MessageBoxButtons.OK,MessageBoxIcon.Warning);
				return;
			}
#else
				AllParam cur = new AllParam();
				int lcid = cur.GetLanguage();			
#endif
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(lcid);

#if EpsonLcd
			MainForm mainWin = new MainForm(showUI);
#else
			MainForm mainWin = new MainForm(true);
#endif
			Application.AddMessageFilter(mainWin);
			if(mainWin.Start())
			{
				Application.Run(mainWin);
			}
			mutex.Close();

		}

		private static Process RuningInstance()
		{
			Process currentProcess = Process.GetCurrentProcess();
			Process[] Processes = Process.GetProcessesByName(currentProcess.ProcessName);
			foreach (Process process in Processes)
			{
				if (process.Id != currentProcess.Id)
				{
//					if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == currentProcess.MainModule.FileName)
//					if (process.MainModule.FileName == currentProcess.MainModule.FileName)
					{
						return process;
					}
				}
			}
			return null;
		}

		private static int GetLanguage()
		{
			uint len = 1;
			byte[] lang = new byte[len];
			int lcid = 0x0004;
			
			if(PreInterface.GetEpsonEP0Cmd(0x7F,lang,ref len,13,0) != 0)
			{
				//0: Sim chinese, 1: trad chinese, 3: english, 4: tailand 
				switch(lang[0])
				{
					case 0:
						lcid = 0x0004;// zh-CHS 0x0004 ���ģ����壩
						break;
					case 1:
						lcid = 0x7C04; // zh-CHT 0x7C04 ���ģ����壩 
						break;				
					case 2:
						lcid = 0x0409;// en-US 0x0409 Ӣ�� - ���� 
						break;				
					case 3:
						lcid = 0x001E;// th 0x001E ̩�� 
						break;	
					default:
						lcid = -1;
						break;
				}
			}
			else
			{
				lcid = -1;
			}
			return lcid;
		}

	}
}
