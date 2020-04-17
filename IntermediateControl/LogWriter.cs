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
        public enum LogTypeEnum : int
        {
            Information = 1, Warning = 2, Error = 3
        }

        public const string logName = "BarCodePrintManagerLog";

        public const string logSource = "BarCodePrintManagerLog";

        public const string DefaultLogName = "setvoltempLog.xml";

        //private RulerConstantstruct RulerConstant = new RulerConstantstruct(null);
        //private IPrinterChange m_iPrinterChange;

        //private LogWriter(IPrinterChange ic)
        //{
        //    this.m_iPrinterChange = ic;
        //    this.RulerConstant = m_iPrinterChange.GetRulerConstants();
        //}
        public LogWriter()
        {
            //string logFileName = Path.Combine(Application.StartupPath, DefaultLogName);
            //if (!string.IsNullOrEmpty(logFileName) && File.Exists(logFileName))
            //{
            //    File.Delete(logFileName);
            //    //File.Create(logFileName);
            //}

        }

        public static void Init()
        {
            string logFileName = Path.Combine(Application.StartupPath, DefaultLogName);
            if (!string.IsNullOrEmpty(logFileName) && File.Exists(logFileName))
            {
                File.Delete(logFileName);
                //File.Create(logFileName);
            }
        }

        public EventLog elog = new EventLog();

        #region Write Into the event log
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
        #endregion

        #region Write Into the file log
        /// <summary>
        /// Write Into the file log
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="isAppend">isAppend</param>
        public static void WriteLog(string[] message, bool isAppend)
        {
            try
            {
                string path = Application.StartupPath;
                string logFileName = Path.Combine(path, DefaultLogName);

                //if (!string.IsNullOrEmpty(logFileName) && !File.Exists(logFileName))
                //{
                //    File.Create(logFileName);
                //}

                StreamWriter sw = new StreamWriter(logFileName, isAppend);
                foreach (string msg in message)
                {
                    sw.WriteLine(msg);
                }
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}
