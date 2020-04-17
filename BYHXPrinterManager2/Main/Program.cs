using System;
using System.Collections.Generic;
//using System.Linq;
using System.Windows.Forms;
using System.IO;
using BYHXPrinterManager;
using System.Threading;
using System.Globalization;

namespace BYHXPrinterManager
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            double d0 = System.Environment.TickCount;
            LogWriter.WriteLog(new string[] { "BYHXPrinterManager Main() Start at:" + d0.ToString() }, true);

            // enable XP theme support
            Application.EnableVisualStyles();
            Application.DoEvents();
            //SetLanguage();
            //Chinese XP system
            if (((System.Globalization.CultureInfo.InstalledUICulture.LCID & 0xff) == 04)
                && System.Environment.OSVersion.Version.Major == 5
                && System.Environment.OSVersion.Version.Minor == 1)
            {
                string filename = System.Environment.SystemDirectory + Path.DirectorySeparatorChar
                     + "conime.exe";
                if (File.Exists(filename))
                {
                    System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();
                    Info.FileName = filename;

                    System.Diagnostics.Process Proc;
                    try
                    {
                        Proc = System.Diagnostics.Process.Start(Info);
                    }
                    catch //(System.ComponentModel.Win32Exception e)
                    {
                        return;
                    }
                }
            }
            try
            {
                //if (!PubFunc.IsFactoryUser())
                //    Splasher.ShowSplash(1000);

                AllParam cur = new AllParam();
                int lcid = cur.GetLanguage();

                const string MUTEX = CoreConst.c_MUTEX_App;
                bool createdNew = false;
                Mutex mutex = new Mutex(true, MUTEX, out createdNew);
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lcid);
                if (!createdNew)
                {
                    MessageBox.Show(ResString.GetEnumDisplayName(typeof(UIError), UIError.OnlyOneProgram));
                    mutex.Close();
                    return;
                }
                double d1 = System.Environment.TickCount;
                LogWriter.WriteLog(new string[]{"new MainForm()之前耗时:" + (d1 - d0).ToString() + "毫秒"},true);
                MainForm mainWin = new MainForm();
                double d2 = System.Environment.TickCount;
                LogWriter.WriteLog(new string[]{"new MainForm()耗时:" + (d2 - d1).ToString() + "毫秒"}, true);
                //Application.AddMessageFilter(mainWin);
                if (mainWin.Start())
                {
                    double d3 = System.Environment.TickCount;
                    LogWriter.WriteLog(new string[]{"mainWin.Start()耗时:" + (d3 - d2).ToString() + "毫秒"}, true);
                    LogWriter.WriteLog(new string[] { "Application.Run(mainWin) at:" + d0.ToString() }, true);
                    Application.Run(mainWin);
                }
                mutex.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
