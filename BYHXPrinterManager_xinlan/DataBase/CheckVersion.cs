using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Data;
using System.Windows.Forms;
using PrinterStubC.Common;
using PrinterStubC.Utility;

namespace BYHXPrinterManager
{
    public class CheckVersion
    {
        private const int INTERNET_CONNECTION_MODEM = 1;
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

        public static void DownNewDataBase()
        {
            try
            {
                if (LocalConnectionStatus())
                {
                    string url = @"http://192.168.1.2:9999/UploadAndDownload.aspx"; //服务器上errorinfo.fdb的下载地址
                    string startPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                    string newDBFile = Path.Combine(startPath, "newerrorinfo.fdb");
                    string newDBFile_Temp = Path.Combine(startPath, "newerrorinfo.tmp");
                    string DBFile = Path.Combine(startPath, "errorinfo.fdb");

                    if (url == "") return;

                    if (File.Exists(newDBFile_Temp))
                    {
                        File.Delete(newDBFile_Temp);
                    }

                    System.Net.WebClient myWebClient = new System.Net.WebClient();
                    myWebClient.DownloadFile(url, newDBFile_Temp);
                    myWebClient.Dispose();

                    if (File.Exists(newDBFile_Temp))
                    {
                        File.Copy(newDBFile_Temp, newDBFile, true);
                    }
                }
            }
            catch { }
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
        public static void DownHeadVersionInfoDataBase_SQLite()
        {
            try
            {
                if (LocalConnectionStatus())
                {
                    //第一个参数是 下载地址，
                    //第二个参数是 下载后的临时文件地址，
                    //第三个参数是 下载完成后，将临时文件保存到的指定地址
                    string filename = Path.Combine(Application.StartupPath, @"DownloadFile.exe");
                    string url = "http://47.93.225.113:5002/KeyValueStores/ExportData";
                    string dbtemp = Path.Combine(Application.StartupPath, @"HeadVersionInfoTemp.db");
                    string db = Path.Combine(Application.StartupPath, @"HeadVersionInfo.db");
                    string dbtime = "";
                    string sql = "select DBVersion.PublishTime from DBVersion";
                    DataTable data = SqliteHelp.SelectData(sql);
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
                                Arguments = url + " " + dbtemp + " " + db + " \"DBName=f8b11d42-3446-4126-906a-64d43fea1728&PublishTime=" + DateTime.Parse(dbtime).ToString("yyyy-MM-dd") + "\" UniqueName=f8b11d42-3446-4126-906a-64d43fea1728"
                            }
                        };
                        process.Start();
                    }
                }
            }
            catch { }
        }
        public static void ReplaceDataBase()
        {
            try
            {
                string startPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                string newDBFile = Path.Combine(startPath, "newerrorinfo.fdb");
                string DBFile = Path.Combine(startPath, "errorinfo.fdb");

                if (File.Exists(newDBFile))
                {
                    if (File.Exists(DBFile))
                    {
                        string dir = Path.Combine(startPath, "DBFilesBK");
                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);
                        }
                        File.Move(DBFile, Path.Combine(dir, string.Format("errorinfo_{0}", DateTime.Now.ToString("yyyyMMddhhmmss"))));
                    }
                    File.Copy(newDBFile, DBFile, true);
                    File.Delete(newDBFile);
                }
            }
            catch { }
        }

//        public static void CheckHBVersion()
//        {
//            string currHBVersion = "";
//            int HBTypeNum = -1;
//            SBoardInfo sBoardInfo = new SBoardInfo();
//            try
//            {
//                if (CoreInterface.GetBoardInfo(0, ref sBoardInfo) != 0)
//                {
//                    ////FW Version Get
//                    //string fwversion = string.Empty;
//                    //int nMaxNum = (sBoardInfo.m_nNull & 0xFF00) >> 8;
//                    //for (int nIndex = 1; nIndex <= nMaxNum; nIndex++)
//                    //{
//                    //    if (nIndex == 1)
//                    //    {
//                    //        byte[] bytefwversion = new byte[16];
//                    //        if (EpsonLCD.GetFWVersionInfo(ref bytefwversion))
//                    //        {
//                    //            int name_term = 4;
//                    //            while (bytefwversion[name_term] != 0) name_term++;
//                    //            fwversion = "FW version:" + (uint)BitConverter.ToInt32(bytefwversion, 0) + " " +
//                    //                        System.Text.Encoding.ASCII.GetString(bytefwversion, 4, name_term - 4) + " " + "\n";
//                    //        }
//                    //    }
//                    //}
//                    //string mbversion = "MB version:" + VersionToString(sBoardInfo.m_nBoradVersion) + " " + sBoardInfo.sProduceDateTime + " " + sBoardInfo.m_nBoardManufatureID.ToString("X4") + sBoardInfo.m_nBoardProductID.ToString("X4") + "\n";
//                    //string mtversion = "MT version:" + VersionToString(sBoardInfo.m_nMTBoradVersion) + " " + sBoardInfo.sMTProduceDateTime + " " + "\n";
//                    //string hbversion = "HB version:" + VersionToString(sBoardInfo.m_nHBBoardVersion) + " " + sBoardInfo.sReserveProduceDateTime + " " + "\n";
//                    //string idversion = "ID :" + sBoardInfo.m_nBoardSerialNum.ToString() + " " + "\n";
//                    //return fwversion + mbversion + mtversion + hbversion + idversion;

//                    currHBVersion = VersionToString(sBoardInfo.m_nHBBoardVersion);

//                    HEAD_BOARD_TYPE hbType = (HEAD_BOARD_TYPE)CoreInterface.get_HeadBoardType(true);
//                    HBTypeNum = (int)hbType;

//#if DEBUG
//                    currHBVersion = "4.5.21.0";
//                    HBTypeNum = 21;
//#endif

//                    string sql = String.Format("select i.*,r.display_reason from  VERSION_INFO i,VERSION_REPLACE r where i.VID = r.VID and i.VID = (select VID from VERSION_REPLACE where REPLACE_VID = (select VID from VERSION_INFO where VERSION_NUM = '{0}' and HBTYPE = {1}))", currHBVersion, HBTypeNum);

//                    string startPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
//                    string DBFile = Path.Combine(startPath, "errorinfo.fdb");

//                    FirebirdHelper firebird = new FirebirdHelper(DBFile);
//                    DataTable dt = firebird.ExecuteDataTable(sql);

//                    if (dt != null && dt.Rows.Count > 0)
//                    {
//                        VersionReplace vr = new VersionReplace();
//                        vr.versionInfo = dt.Rows[0]["VERSION_NUM"].ToString();
//                        vr.replaceReason = dt.Rows[0]["DISPLAY_REASON"].ToString();
//                        vr.versionLocation = dt.Rows[0]["LOCATION"].ToString();
//                        vr.ShowDialog();
//                    }

//                }
//            }
//            catch{ }
//        }

        private static string VersionToString(uint nVersion)
        {

            SFWVersion fwv = new SFWVersion(nVersion);
            string sVersion = //fwv.m_nHWVersion.ToString()+"."+
                    fwv.m_nMainVersion.ToString()
                    + "." + fwv.m_nSubVersion.ToString()
                    + "." + fwv.m_nBuildVersion.ToString()
                    + "." + fwv.m_nHWVersion.ToString();
            return sVersion;
        }

        //下面为新添加的版本信息更新的方法
        public static void CheckHBVersion_New()
        {
            //bool bPowerOn = CoreInterface.GetBoardStatus() != JetStatusEnum.PowerOff;
            HEAD_BOARD_TYPE headBoardType = (HEAD_BOARD_TYPE)CoreInterface.get_HeadBoardType(true);
            SBoardInfo sBoardInfo = new SBoardInfo();
            if (CoreInterface.GetBoardInfo(0, ref sBoardInfo) != 0)
            {
                SFWVersion fwv = new SFWVersion(sBoardInfo.m_nHBBoardVersion);
                string sVersion = fwv.m_nMainVersion.ToString()
                        + "." + fwv.m_nSubVersion.ToString()
                        + "." + fwv.m_nBuildVersion.ToString()
                        + "." + fwv.m_nHWVersion.ToString();
                string sql = "select VERSION_REPLACE.DISPLAY_REASON,VERSION_REPLACE.SUBMIT_TIME from VERSION_REPLACE left " +
                             "join VERSION_INFO on VERSION_REPLACE.REPLACE_VID=VERSION_INFO.VID where VERSION_INFO.VERSION_NUM='" + sVersion + "' " +
                             "and VERSION_INFO.HBTYPE='" + headBoardType.ToString() + "' ORDER BY VERSION_REPLACE.SUBMIT_TIME DESC";
                DataTable data = SqliteHelp.SelectData(sql);
                if (data != null && data.Rows.Count > 0)
                {
                    string msg = data.Rows[0][0].ToString();
                    if (!string.IsNullOrEmpty(msg.Trim()))
                    {
                        MessageBox.Show(msg.Trim(), @"头板版本更新提示");
                    }
                }
            }
        }
    }
}
