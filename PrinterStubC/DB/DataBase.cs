using System;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Data.SQLite;

namespace BYHXPrinterManager
{
    public class DataBase
    {
        private SQLiteConnection sqlConn = new SQLiteConnection();
        public bool OpenDB()
        {
            bool ret = false;
            string DBPath = Path.Combine(Application.StartupPath, "History", "HistoryPrintData.db");
            if (!File.Exists(DBPath))
            {
                //todo 创建空的数据库
            }
            sqlConn.ConnectionString = "Data Source=" + DBPath + ";Version=3;Password=BYHXPMDB;";
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

        public bool ExecSQL(string strSql)
        {
            bool ret = false;

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

            return ret;
        }

        public DataTable SelectData(string strSql)
        {
            DataTable dt = new DataTable();

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
                MessageBox.Show(ex.Message);
                return null;
            }

            return dt;
        }

        public void CloseDB()
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
