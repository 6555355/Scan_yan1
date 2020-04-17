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


//using BYHXPrinterManager.Calibration;
namespace BYHXPrinterManager.Main
{
	/// <summary>
	/// Summary description for Main.
	/// </summary>
	public class MainEntry
	{
		[STAThread]
		static void Main() 
		{
			// enable XP theme support
			Application.EnableVisualStyles();
			Application.DoEvents();

			//Application.Run(new AboutForm());
			//Application.Run(new CaliWizard());
			//Application.Run(new Updater());
			//Application.Run(new PasswordForm());
			//Application.Run(new CleanForm());

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
			AllParam cur = new AllParam();
			int lcid = cur.GetLanguage();
			//string name = "en-US";
			//name = "zh-chs";
			//name = "zh-cht";

			const string MUTEX = CoreConst.c_MUTEX_App;
			bool createdNew  = false;
			Mutex mutex = new Mutex(true,MUTEX,out createdNew);
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(lcid);
			if(!createdNew)
			{
				MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError),UIError.OnlyOneProgram));
				mutex.Close();
				return;
			}
			MainForm mainWin = new MainForm();
			Application.AddMessageFilter(mainWin);
			if(mainWin.Start())
				Application.Run(mainWin);

			mutex.Close();

		}

	}
}
