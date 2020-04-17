using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using System.IO;

namespace ErrRes.SQLiteDB
{
    public static class SQLiteHelper
    {
        private static SQLiteConnection sqlConn;
        
        private static readonly string DbPath = "Data Source=" + Path.Combine(Application.StartupPath, "ErrorInfo.db") + ";Version=3;";
        public static DataTable SelectData(string strSql)
        {
            DataTable dt = new DataTable();
            sqlConn = new SQLiteConnection { ConnectionString = DbPath };
            try
            {
                sqlConn.Open();
                SQLiteDataAdapter da = new SQLiteDataAdapter(strSql, sqlConn);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConn.Close();
                //sqlConn.Dispose();
            }
            return dt;
        }
    }
}
