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
using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Reflection;
using System.Xml;
using MainWindow;

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
					//等待3秒钟
					//Proc.WaitForExit(3000);

				}
			}

			const string MUTEX = CoreConst.c_MUTEX_App;
			bool createdNew  = false;
			Mutex mutex = new Mutex(true,MUTEX,out createdNew);
            if (!createdNew)
            {
#if EpsonLcd
                Process process = RuningInstance();
                MyData data = ProcessMessaging.GetShareMem();//您new一个也行
                data.InfoCode = showUI?1:0;//消息码
                data.ProcessID = process.Id;//接收进程ID
                ProcessMessaging.SetShareMem(data);
                Thread.Sleep(100);
                return;
#else
                MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError), UIError.OnlyOneProgram));
                mutex.Close();
                return;
#endif
            }

#if EpsonLcd
			int lcid  = PubFunc.GetLanguage();
			if(lcid == -1)
			{
				// 未连接usb线,无法获取软件语言,启动终止.
				MessageBox.Show(ResString.GetResString("NoFoundUSBReturn"),ResString.GetProductName(),MessageBoxButtons.OK,MessageBoxIcon.Warning);
				return;
			}
#else
				AllParam cur = new AllParam();
				int lcid = cur.GetLanguage();			
#endif

			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(lcid);

		    bool splashEnable = PubFunc.GetEnableSplash();
            if (splashEnable)
                Splasher.ShowSplash(1000);

            string skinName = cur.GetSkinName();			

#if EpsonLcd
			MainForm mainWin = new MainForm(showUI);
#else
            MainForm mainWin = new MainForm(true, skinName);
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

	}
}
