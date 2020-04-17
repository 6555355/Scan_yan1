using ErrRes.FirebirdDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using FirebirdSql.Data.FirebirdClient;

namespace ErrRes.Manager
{
    public class ErrorResourceManager
    {
        private bool isDBInitialized = false;
        private FirebirdHelper firebird;
        public readonly static ErrorResourceManager Instance = new ErrorResourceManager();
        private ErrorResourceManager()
        {

        }

        /// <summary>
        /// 指示Firebird数据库文件是否已经指定。若为false则需要先调用SetFDBFile(string database)方法。
        /// </summary>
        public bool IsFDBFileInitialized
        {
            get
            {
                return isDBInitialized;
            }
        }
        public void SetFDBFile(string database)
        {
            firebird = new FirebirdHelper(database);
            isDBInitialized = true;
        }

        /// <summary>
        /// 数据库文件设置检查，若未进行数据库文件设置，则抛出非法操作异常。
        /// </summary>
        public void FDBFileCheck()
        {
            if (!isDBInitialized)
            {
                throw new InvalidOperationException("Please call method [SetFDBFIle(string database)] first before other method call!");
            }
        }

        public string GetInfoByErrorCode(int errorCode)
        {
            string info = "";
            try
            {
                FDBFileCheck();
#if true
                int langId = Thread.CurrentThread.CurrentUICulture.LCID;
                FbParameter[] fbParams = new FbParameter[4];
                fbParams[0] = new FbParameter("@ERR_CODE", FbDbType.Integer);
                fbParams[0].Value = errorCode;
                fbParams[1] = new FbParameter("@VENDER_ID", FbDbType.Integer);
                fbParams[1].Value = 0;
                fbParams[2] = new FbParameter("@PID", FbDbType.Integer);
                fbParams[2].Value = 0;
                fbParams[3] = new FbParameter("@LANG_ID", FbDbType.Integer);
                fbParams[3].Value = langId;

                var ret = firebird.ExecuteScalar(System.Data.CommandType.StoredProcedure, "GET_INFO_BY_CODE", fbParams);
                info = Convert.ToString(ret);
#else
            string sql = "SELECT * FROM ERROR_INFO WHERE 1=1";
            {
                //int errCode;
                //if (int.TryParse(errorCode, System.Globalization.NumberStyles.HexNumber, null, out errCode))
                {
                    sql += " AND ERROR_CODE=" + errorCode;
                }
            }
                DataTable tbErrorInfo = firebird.ExecuteDataTable(sql);
                info = tbErrorInfo.ToString();
#endif
            }
            catch (Exception ex)
            {
                info = "";
            }

            return info;
        }

    }
}
