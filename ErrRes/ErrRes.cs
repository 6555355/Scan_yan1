using ErrRes.Manager;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading;
using ErrRes.SQLiteDB;

namespace BYHXPrinterManager
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class ErrRes
	{
		public ErrRes()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		static public string GetResString(string name)
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ErrRes));
			return resources.GetString(name);
		}

        //firebird数据库
        public static string GetResString(int errCode)
        {
            string startPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

            if (!ErrorResourceManager.Instance.IsFDBFileInitialized)
            {
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
                ErrorResourceManager.Instance.SetFDBFile(DBFile);
            }

            if (!File.Exists(Path.Combine(startPath, "errorinfo.fdb")))
            {
                return "";//"[404]Firebird Database Connection Failed!errorinfo.fdb NOT FOUND!";
            }
            return ErrorResourceManager.Instance.GetInfoByErrorCode(errCode);
        }

        //sqlite数据库
        public static string GetResString_SQLite(int errCode, int vid)
        {
            int languageid = Thread.CurrentThread.CurrentUICulture.LCID;
            string sql = @"select ErrorInfos.VenderId,GlobalErrorInfos.LocalContent from GlobalErrorInfos,ErrorInfos where 
                GlobalErrorInfos.ErrorInfoId=ErrorInfos.Id and GlobalErrorInfos.LanguageId = "
                + languageid +
                " and ErrorInfos.ErrorCode = "
                + errCode.ToString("D") +
                " and ErrorInfos.PID = 0";
            DataTable dt = SQLiteHelper.SelectData(sql);
            if (dt != null && dt.Rows.Count > 0 && dt.Columns.Count > 0)
            {
                DataRow[] drs = dt.Select("VenderId = " + vid);
                if (drs.Length > 0)
                {
                    return drs[0][1].ToString();
                }
                drs = dt.Select("VenderId = 0");
                if (drs.Length > 0)
                {
                    return drs[0][1].ToString();
                }
            }
            return "";
        }


	    /// <summary>
	    /// 判断此错误号是否启用
	    /// </summary>
	    /// <param name="code">错误号</param>
	    /// <param name="vid">厂商ID</param>
	    /// <returns>是否启用</returns>
	    public static bool IsEnable(int code, int vid)
        {
            string sql = @"select ErrorInfos.VenderId,ErrorInfos.Enable from ErrorInfos where " +
                "ErrorInfos.ErrorCode = " + code.ToString("D") +
                " and ErrorInfos.PID = 0";
            DataTable dt = SQLiteHelper.SelectData(sql);
            if (dt != null && dt.Rows.Count > 0 && dt.Columns.Count > 0)
            {
                DataRow[] drs = dt.Select("VenderId = " + vid);
                if (drs.Length > 0)
                {
                    return drs[0][1].ToString() == "1";
                }
                drs = dt.Select("VenderId = 0");
                if (drs.Length > 0)
                {
                    return drs[0][1].ToString() == "1";
                }
            }
            return true;
        }
    }
}
