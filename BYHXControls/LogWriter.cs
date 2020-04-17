using System;
using System.Text;
using System.IO;
using System.Configuration;
using System.Windows.Forms;
using System.Diagnostics;
using BYHXPrinterManager;

namespace BYHXPrinterManager
{
    public class LogWriter
    {
        public static bool BInited;
        public enum LogTypeEnum : int
        {
            Information = 1, Warning = 2, Error = 3
        }

        public const string logName = "BarCodePrintManagerLog";

        public const string logSource = "BarCodePrintManagerLog";

        public const string DefaultLogName = "log.txt";

		public const string TemperatureLogName = "TempLog.txt";

		public const string PrintedAreaLogName = "PrintedArea.Log";

//        private RulerConstantstruct RulerConstant = new RulerConstantstruct(null);
        private IPrinterChange m_iPrinterChange;

		public static object lockobj   =new object(); 
		
		private LogWriter(IPrinterChange ic)
		{
            this.m_iPrinterChange = ic;
//            this.RulerConstant = m_iPrinterChange.GetRulerConstants();
		}

        public EventLog elog = new EventLog();

        /// <summary>
        /// Write Into the event log
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="logType">EventLogEntryType</param>
        public void WriteEntry(string message, int logType)
        {
            EventLogEntryType objEleType = new EventLogEntryType();

            switch (logType)
            {
                case 0:
                    objEleType = EventLogEntryType.Information;
                    break;
                case 1:
                    objEleType = EventLogEntryType.Warning;
                    break;
                case 2:
                    objEleType = EventLogEntryType.Error;
                    break;
            }

            if (!EventLog.SourceExists(logSource))
                EventLog.CreateEventSource(logSource, logName);

            elog.Source = logSource;
            elog.WriteEntry(message, objEleType);

        }

        /// <summary>
        /// Write Into the file log
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="isAppend">isAppend</param>
		public static void  WriteLog(string[] message, bool isAppend)
		{
            try
            {
			string path = Application.StartupPath;
			string logFileName = Path.Combine(path, DefaultLogName);
			if (!BInited)
			{
				if (File.Exists(logFileName))
				{
#if !GZ_CARTON
                    File.Delete(logFileName);
#else
				    string datetime = string.Format(string.Format("{0}{1}{2}_{3}{4}{5}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second));
                    string dstFileName = Path.Combine(path, string.Format("log_{0}.txt", datetime));
                    File.Move(logFileName, dstFileName);
#endif
				}
				Init();
			}

                //            if (String.Empty.Equals(logFileName) && File.Exists(logFileName))
                //            {
                //                Directory.CreateDirectory(logFileName);
                //            }
			lock (lockobj)
			{
                StreamWriter sw = new StreamWriter(logFileName, isAppend);
                foreach (string msg in message)
                {
                    sw.WriteLine(DateTime.Now.ToLongTimeString() + " : " + msg);
                }
                sw.Flush();
                sw.Close();
            }
		}
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

		private static void Init()
		{
			//dtAllDateTimes = ReadOptionLog();
			BInited = true;
		}

		/// <summary>
		/// Write Into the file log
		/// </summary>
		/// <param name="message">message</param>
		/// <param name="isAppend">isAppend</param>
		public static void  WriteTemperatureLog(string[] message, bool isAppend)
		{

			string path = Application.StartupPath;
			string logFileName = Path.Combine(path, TemperatureLogName);

			//            if (String.Empty.Equals(logFileName) && File.Exists(logFileName))
			//            {
			//                Directory.CreateDirectory(logFileName);
			//            }

			StreamWriter sw = new StreamWriter(logFileName, isAppend);
			foreach(string msg in message)
			{
				sw.WriteLine(DateTime.Now.ToLongTimeString() + " : "+ msg);
			}
			sw.Flush();
			sw.Close();
		}

		public static void  WritePrintedAreaLog(string[] message, bool isAppend)
		{

			string path = Application.StartupPath;
			string logFileName = Path.Combine(path, PrintedAreaLogName);

			//            if (String.Empty.Equals(logFileName) && File.Exists(logFileName))
			//            {
			//                Directory.CreateDirectory(logFileName);
			//            }

			StreamWriter sw = new StreamWriter(logFileName, isAppend);
			foreach(string msg in message)
			{
				sw.WriteLine(DateTime.Now.ToLongTimeString() + " : "+ msg);
			}
			sw.Flush();
			sw.Close();
		}

		public static void LogSetPrinterSetting(SPrinterSetting ss,bool bprinting,string source)
		{
			int passnum = 0;
			int step = 0;
			if(bprinting)
			{
				SPrtFileInfo jobInfo = new SPrtFileInfo();
				if(CoreInterface.Printer_GetJobInfo(ref jobInfo) > 0)
				{
					passnum = jobInfo.sFreSetting.nPass;
					if(ss.nKillBiDirBanding != 0)
						step = ss.sCalibrationSetting.nPassStepArray[(passnum+1)/2 -1];	
					else
						step = ss.sCalibrationSetting.nPassStepArray[passnum -1];	
				}
			}
			else
			{
				passnum = ss.sFrequencySetting.nPass;
				if(ss.nKillBiDirBanding != 0)
					step = ss.sCalibrationSetting.nPassStepArray[(ss.sFrequencySetting.nPass+1)/2 -1];	
				else
					step = ss.sCalibrationSetting.nPassStepArray[ss.sFrequencySetting.nPass -1];	
			}
			LogWriter.WriteLog(new string[]{string.Format("SetPrinterSetting[source={0}];setted value[passnum={1},stepPerhead={2},fixstep={3}]",source,passnum,ss.sCalibrationSetting.nStepPerHead,step)},true);
		}


	}
}
