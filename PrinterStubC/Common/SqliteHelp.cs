using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using System.Data;

namespace PrinterStubC.Common
{
    public static class SqliteHelp
    {
        private static SQLiteConnection sqlConn = new SQLiteConnection();
        public static bool OpenDB()
        {
            bool ret = false;
            string DBPath = Path.Combine(Application.StartupPath, "HeadVersionInfo.db");
            sqlConn.ConnectionString = "Data Source=" + DBPath + ";Version=3;";
            try
            {
                sqlConn.Open();
                ret = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return ret;

        }

        public static bool ExecSQL(string strSql)
        {
            bool ret = false;
            sqlConn = new SQLiteConnection();
            if (sqlConn.State != System.Data.ConnectionState.Open)
            {
                if (!OpenDB())
                {
                    return false;
                }
            }
            try
            {
                SQLiteCommand sqlCmd = new SQLiteCommand(strSql, sqlConn);
                sqlCmd.ExecuteNonQuery();
                ret = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseDB();
            }
            return ret;
        }

        public static DataTable SelectData(string strSql)
        {
            DataTable dt = new DataTable();
            sqlConn = new SQLiteConnection();
            if (sqlConn.State != System.Data.ConnectionState.Open)
            {
                if (!OpenDB())
                {
                    return null;
                }
            }
            try
            {
                SQLiteDataAdapter da = new SQLiteDataAdapter(strSql, sqlConn);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                CloseDB();
            }
            return dt;
        }

        public static void CloseDB()
        {
            try
            {
                if (sqlConn != null && sqlConn.State == System.Data.ConnectionState.Open)
                {
                    sqlConn.Close();
                    sqlConn.Dispose();
                }
            }
            catch { }
        }
    }
}
