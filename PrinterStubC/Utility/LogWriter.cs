using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using BYHXLog;
using BYHXPrinterManager;

namespace PrinterStubC.Utility
{
    public class LogWriter
    {
        private static BYHXLogger logger;
        private static BYHXLogger recentJobLogger;
        private static BYHXLogger tempLogger;
        private static BYHXLogger printedAreaLogger;

        static LogWriter()
        {
            logger = LogManager.GetLogger("log.txt");
            recentJobLogger = LogManager.GetLogger("RecentJob.txt");
            tempLogger = LogManager.GetLogger("TempLog.txt");
            printedAreaLogger = LogManager.GetLogger("PrintedArea.Log");
        }

        public static void DisposeLogger()
        {
            LogManager.DisposeLogger();
        }

        #region Constants and Fields

        public const string DefaultLogName = "log.txt";

        public const string DefaultRecentJobName = "RecentJob.txt";

        public const string logName = "WAFOnePassLog";

        public const string logSource = "WAFOnePass";

        public static bool BInited;

        public static List<DateTime> dtAllDateTimes = new List<DateTime>();

        public EventLog Elog = new EventLog();
        public static object lockobj = new object();
        public const string TemperatureLogName = "TempLog.txt";

        public const string PrintedAreaLogName = "PrintedArea.Log";

        #endregion

        #region Enums

        public enum LogTypeEnum
        {
            Information = 1,
            Warning = 2,
            Error = 3
        }

        #endregion

        #region Public Methods

        public static void SaveOptionLog(string msg)
        {
            WriteLog(msg, true);
        }

        public static void ClearOldLog()
        {
            string logFileName = Path.Combine(Application.StartupPath, DefaultLogName);
            if (!File.Exists(logFileName))
            {
                return;
            }
            var start = new DateTime();
            var end = new DateTime();
            var alldate = new Dictionary<DateTime, int>();
            var lines = new List<string>();
            using (var fs = new FileStream(logFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
            {
                var sr = new StreamReader(fs);
                while (true)
                {
                    DateTime dttemp;
                    string temp = sr.ReadLine();
                    if (string.IsNullOrEmpty(temp))
                    {
                        break;
                    }
                    lines.Add(temp);
                    if (DateTime.TryParse(temp, out dttemp))
                    {
                        end = dttemp;
                        if (!alldate.ContainsKey(dttemp))
                        {
                            alldate.Add(dttemp, lines.Count - 1);
                        }
                    }
                }
            }
            if (alldate.Count <= 30)
            {
                return;
            }
            for (int i = 0; i < alldate.Count - 30; i++)
            {
                start = end.Subtract(new TimeSpan(30, 0, 0, 0));
                if (alldate.ContainsKey(start))
                {
                    break;
                }
            }
            int startindex = alldate[start];
            lines.RemoveRange(0, startindex);
            var sw = new StreamWriter(logFileName, false);
            foreach (string str in lines)
            {
                sw.WriteLine(str);
            }
            sw.Flush();
            sw.Close();
        }

        /// <summary>
        /// 读入最近的作业
        /// </summary>
        public static List<string> ReadRecentJob()
        {
            const int recentJobMaxCount = 10;
            string path = Application.StartupPath;
            string logFileName = Path.Combine(path, DefaultRecentJobName);

            List<string> alllines = new List<string>();
            if (!File.Exists(logFileName))
            {
                return alllines;
            }
            using (var fs = new FileStream(logFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
            {
                var sr = new StreamReader(fs);
                while (!sr.EndOfStream)
                {
                    alllines.Add(sr.ReadLine());
                }
                sr.Close();
            }
            var sw = new StreamWriter(logFileName, false);
            List<string> ret = new List<string>();
            foreach (var path1 in alllines)
            {
                if (ret.Count > 0 && ret[ret.Count - 1].Equals(path1))
                {
                    continue;
                }
                ret.Add(path1);
            }
            if (ret.Count > recentJobMaxCount)
            {
                ret.RemoveRange(0, ret.Count - recentJobMaxCount);
            }
            foreach (string str in ret)
            {
                sw.WriteLine(str);
            }
            sw.Flush();
            sw.Close();
            ret.Reverse();
            return ret;
        }

        /// <summary>
        /// Write Into the file log
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="isAppend">isAppend</param>
        public static void WriteLog(string message, bool isAppend)
        {
            //string path = Application.StartupPath;
            //string logFileName = Path.Combine(path, DefaultLogName);
            //if (!BInited)
            //{
            //    if (File.Exists(logFileName))
            //        File.Delete(logFileName);
            //    Init();
            //}
            ////else
            //{
            //    lock (lockobj)
            //    {
            //        StreamWriter sw = new StreamWriter(logFileName, isAppend);

            //        if (!dtAllDateTimes.Contains(DateTime.Today))
            //        {
            //            sw.WriteLine(DateTime.Today.ToShortDateString());
            //            dtAllDateTimes.Add(DateTime.Today);
            //        }
            //        DateTime now = DateTime.Now;
            //        sw.WriteLine("          " + string.Format("{0}-{1}-{2}-{3}-{4}", now.ToShortDateString(), now.Hour, now.Minute, now.Second, now.Millisecond) + " : " + message);
            //        sw.Flush();
            //        sw.Close();
            //    }
            //}
            logger.LogInfo(message);
        }
        /// <summary>
        /// Write Into the file log
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="isAppend">isAppend</param>
        public static void WriteLog(string[] message, bool isAppend)
        {
            for (int i = 0; i < message.Length; i++)
            {
                WriteLog(message[i], isAppend);
            }
        }
        /// <summary>
        /// 写入最近的作业
        /// </summary>
        /// <param name="jobpath"></param>
        public static void WriteRecentJob(string jobpath)
        {
            //string path = Application.StartupPath;
            //string logFileName = Path.Combine(path, DefaultRecentJobName);

            //var sw = new StreamWriter(logFileName, true);
            //sw.WriteLine(jobpath);
            //sw.Flush();
            //sw.Close();
            recentJobLogger.LogInfo(jobpath);
        }

        /// <summary>
        /// Write Into the event log
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="logType">EventLogEntryType</param>
        public void WriteEntry(string message, int logType)
        {
            var objEleType = new EventLogEntryType();

            switch (logType)
            {
                case 0:
                    //objEleType = EventLogEntryType.Information;
                    logger.LogInfo(message);
                    break;
                case 1:
                    //objEleType = EventLogEntryType.Warning;
                    logger.LogWarn(message);
                    break;
                case 2:
                    //objEleType = EventLogEntryType.Error;
                    logger.LogError(message);
                    break;
            }

            //if (!EventLog.SourceExists(logSource))
            //{
            //    EventLog.CreateEventSource(logSource, logName);
            //}

            //this.Elog.Source = logSource;
            //this.Elog.WriteEntry(message, objEleType);
        }

        #endregion

        #region Methods

        private static void Init()
        {
            //dtAllDateTimes = ReadOptionLog();
            BInited = true;
        }

        private static List<DateTime> ReadOptionLog()
        {
            string logFileName = Path.Combine(Application.StartupPath, DefaultLogName);
            var alldate = new List<DateTime>();
            if (!File.Exists(logFileName))
            {
                return new List<DateTime>();
            }
            using (var fs = new FileStream(logFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
            {
                var sr = new StreamReader(fs);
                while (true)
                {
                    DateTime dttemp;
                    string temp = sr.ReadLine();
                    if (string.IsNullOrEmpty(temp))
                    {
                        break;
                    }
                    if (DateTime.TryParse(temp, out dttemp))
                    {
                        if (!alldate.Contains(dttemp))
                        {
                            alldate.Add(dttemp);
                        }
                    }
                }
                sr.Close();
            }
            return alldate;
        }

        public static List<float> ReadConfig()
        {
            string logFileName = Path.Combine(Application.StartupPath, "config.ini");
            var alldate = new List<float>();
            if (!File.Exists(logFileName))
            {
                return new List<float>();
            }
            using (var fs = new FileStream(logFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
            {
                var sr = new StreamReader(fs);
                while (true)
                {
                    float dttemp;
                    string temp = sr.ReadLine();
                    if (string.IsNullOrEmpty(temp))
                    {
                        break;
                    }
                    if (float.TryParse(temp, out dttemp))
                    {
                        alldate.Add(dttemp);
                    }
                }
                sr.Close();
            }
            return alldate;
        }
        #endregion
        /// <summary>
        /// Write Into the file log
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="isAppend">isAppend</param>
        public static void WriteTemperatureLog(string[] message, bool isAppend)
        {

            //string path = Application.StartupPath;
            //string logFileName = Path.Combine(path, TemperatureLogName);

            ////            if (String.Empty.Equals(logFileName) && File.Exists(logFileName))
            ////            {
            ////                Directory.CreateDirectory(logFileName);
            ////            }

            //StreamWriter sw = new StreamWriter(logFileName, isAppend);
            foreach (string msg in message)
            {
                tempLogger.LogInfo(msg);
                //sw.WriteLine(DateTime.Now.ToLongTimeString() + " : " + msg);
            }
            //sw.Flush();
            //sw.Close();
        }

        public static void WritePrintedAreaLog(string[] message, bool isAppend)
        {

            //string path = Application.StartupPath;
            //string logFileName = Path.Combine(path, PrintedAreaLogName);

            ////            if (String.Empty.Equals(logFileName) && File.Exists(logFileName))
            ////            {
            ////                Directory.CreateDirectory(logFileName);
            ////            }

            //StreamWriter sw = new StreamWriter(logFileName, isAppend);
            foreach (string msg in message)
            {
                printedAreaLogger.LogInfo(msg);
                //sw.WriteLine(DateTime.Now.ToLongTimeString() + " : " + msg);
            }
            //sw.Flush();
            //sw.Close();
        }

        public static void LogSetPrinterSetting(SPrinterSetting ss, bool bprinting, string source)
        {
            try
            {

                int passnum = 0;
                int step = 0;
                int passIndex = 0;
                if (bprinting)
                {
                    SPrtFileInfo jobInfo = new SPrtFileInfo();
                    if (CoreInterface.Printer_GetJobInfo(ref jobInfo) > 0)
                    {
                        passnum = jobInfo.sFreSetting.nPass;
                        if (ss.nKillBiDirBanding != 0)
                            passIndex = (passnum + 1)/2 - 1;
                        else
                            passIndex = passnum - 1;
                    }
                    else
                    {
                        LogWriter.WriteLog(new string[] {"LogSetPrinterSetting Printer_GetJobInfo fail!!"}, true);
                    }
                }
                else
                {
                    passnum = ss.sFrequencySetting.nPass;
                    if (ss.nKillBiDirBanding != 0)
                        passIndex = (ss.sFrequencySetting.nPass + 1)/2 - 1;
                    else
                        passIndex = ss.sFrequencySetting.nPass - 1;
                }
                if (passIndex >= 0)
                {
                    step = ss.sCalibrationSetting.nPassStepArray[passIndex];
                    LogWriter.WriteLog(
                        new string[]
                        {
                            string.Format(
                                "SetPrinterSetting[source={0}];setted value[passnum={1},stepPerhead={2},fixstep={3}]",
                                source, passnum, ss.sCalibrationSetting.nStepPerHead, step)
                        }, true);
                }
                else
                {
                    LogWriter.WriteLog(
                        new string[] {string.Format("LogSetPrinterSetting error!!! passIndex={0}", passIndex)}, true);
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(new string[] {"LogSetPrinterSetting Exception:" + ex.Message}, true);
            }
        }

    }
}