using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PrinterStubC.Utility
{
    public class SQLiteHelper
    {
        private static readonly string DataPath = Path.Combine(Application.StartupPath, "ErrorInfo.db");

        private static readonly string DataSource = "Data Source=" + DataPath + ";Version=3;";

        public static DataTable ExecuteDataTable(string cmd)
        {
            try
            {
                if (!File.Exists(DataPath))
                    return null;
                DataTable dt = new DataTable();
                using (SQLiteConnection conn = new SQLiteConnection(DataSource))
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd, conn))
                    {
                        da.Fill(dt);
                    }
                }
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }private const int INTERNET_CONNECTION_MODEM = 1;
        private const int INTERNET_CONNECTION_LAN = 2;
        [System.Runtime.InteropServices.DllImport("winInet.dll")]
        private static extern bool InternetGetConnectedState(ref int dwFlag, int dwReserved);

        public static bool LocalConnectionStatus()
        {
            System.Int32 dwFlag = new Int32();
            if (!InternetGetConnectedState(ref dwFlag, 0))
            {
                //Console.WriteLine("LocalConnectionStatus--未连网!");
                return false;
            }
            else
            {
                //if ((dwFlag & INTERNET_CONNECTION_MODEM) != 0)
                //{
                //    Console.WriteLine("LocalConnectionStatus--采用调制解调器上网。");
                //    return true;
                //}
                //else if ((dwFlag & INTERNET_CONNECTION_LAN) != 0)
                if ((dwFlag & INTERNET_CONNECTION_LAN) != 0)
                {
                    //Console.WriteLine("LocalConnectionStatus--采用网卡上网。");
                    return true;
                }
            }
            return false;
        }

        public static void DownNewDataBase_SQLite()
        {
            try
            {
                if (LocalConnectionStatus())
                {
                    //第一个参数是 下载地址，
                    //第二个参数是 下载后的临时文件地址，
                    //第三个参数是 下载完成后，将临时文件保存到的指定地址
                    string filename = Path.Combine(Application.StartupPath, @"DownloadFile.exe");
                    string url = "http://47.93.225.113:5002/ErrorInfoApi/GetErrorDb";
                    string dbtemp = Path.Combine(Application.StartupPath, @"ErrorInfoTemp.db");
                    string db = Path.Combine(Application.StartupPath, @"ErrorInfo.db");
                    string dbtime = "";
                    string sql = "select DBVersion.PublishTime from DBVersion";
                    DataTable data = SQLiteHelper.ExecuteDataTable(sql);
                    if (data != null && data.Rows.Count > 0)
                    {
                        string msg = data.Rows[0][0].ToString();
                        if (!string.IsNullOrEmpty(msg.Trim()))
                        {
                            dbtime = msg;
                        }
                    }
                    else
                    {
                        dbtime = DateTime.MinValue.ToString("G");
                    }
                    Process[] ps = Process.GetProcessesByName("ConsoleApp");
                    foreach (Process process in ps)
                    {
                        process.Kill();
                    }
                    if (File.Exists(filename))
                    {
                        Process process = new Process
                        {
                            StartInfo =
                            {
                                FileName = filename,
                                CreateNoWindow = true,
                                UseShellExecute = false,
                                Arguments = url + " " + dbtemp + " " + db + " \"DBName=ErrorInfo&PublishTime=" + DateTime.Parse(dbtime).ToString("yyyy-MM-dd") + "\""
                            }
                        };
                        process.Start();
                    }
                }
            }
            catch { }
        }

    }
}
