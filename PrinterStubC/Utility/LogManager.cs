using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BYHXLog;
using System.Windows;
using System.IO;

namespace PrinterStubC.Utility
{
    public class LogManager
    {
        private static ConcurrentDictionary<string, BYHXLogger> loggerDic = new ConcurrentDictionary<string, BYHXLogger>();

        public static BYHXLogger GetLogger(string logFileName)
        {
            return loggerDic.GetOrAdd(logFileName, (f) => new BYHXLogger(Path.Combine(Environment.CurrentDirectory, "LOG", f)));
        }

        public static void DisposeLogger()
        {
            foreach (BYHXLogger item in loggerDic.Values)
            {
                item.Dispose();
            }
        }
    }
}
