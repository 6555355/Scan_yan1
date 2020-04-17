/*参考：http://www.cnblogs.com/xuhongfei/archive/2013/06/15/3137188.html
 *     http://www.cnblogs.com/top5/archive/2009/05/15/1457933.html
 *     http://blog.csdn.net/HiSpring/article/details/5310243
 *     http://www.firebirdsql.org/en/net-examples-of-use/
 */
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
namespace ErrRes.FirebirdDB
{
    public class FirebirdHelper
    {
        private string connStrTemplate = "Server={0};User={1};Password={2};Charser={3};Database={4};ServerType=1;";
        private string connStr;

        protected char ParameterToken
        {
            get { return '@'; }
        }

        //连接字符串
        public string ConnectString
        {
            set { connStr = value; }
        }

        //构造函数
        public FirebirdHelper()
        {

        }

        public FirebirdHelper(string database)
        {
            connStr = string.Format(connStrTemplate, "localhost", "sysdba", "byhx1109", "NONE", database);
        }

        //构造函数3
        public FirebirdHelper(string host, string user, string password, string charset, string database)
        {
            connStr = String.Format(connStrTemplate, host, user, password, charset, database);
        }

        /// <summary>
        /// Open FbConnection
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  FbConnection conn = OpenConnection();
        /// </remarks>
        /// <returns>an FbConnection</returns>
        public FbConnection OpenConnection()
        {
            FbConnection connection = null;
            try
            {
                try
                {
                    //FbConnectionStringBuilder connBuilder = new FbConnectionStringBuilder();
                    //connBuilder.UserID = "SYSDBA";//设置一个值，嵌入式版本并不验证用户名。  
                    //connBuilder.ServerType = FbServerType.Embedded;//设置数据库类型为 嵌入式；  
                    //connBuilder.Database = @"errorinfo.fdb";//数据库文件的目录；  

                    //connection = new FbConnection(connBuilder.ConnectionString);
                    connection = new FbConnection(connStr);
                    connection.Open();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            catch
            {
                if (connection != null)
                    connection.Close();
            }
            return connection;
        }

        /// <summary>
        /// Execute a FbCommand (that returns no resultset) using an existing SQL Transaction 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery( CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public int ExecuteNonQuery(CommandType cmdType, string cmdText, params FbParameter[] commandParameters)
        {
            FbCommand cmd = new FbCommand();
            using (FbConnection conn = OpenConnection())
            {

                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }


        /// <summary>
        /// Execute a FbCommand (that returns no resultset) using an existing SQL Transaction 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">an existing sql transaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public int ExecuteNonQuery(FbTransaction trans, CommandType cmdType, string cmdText, params FbParameter[] commandParameters)
        {
            FbCommand cmd = new FbCommand();
            using (FbConnection conn = OpenConnection())
            {

                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// Execute a FbCommand that returns a resultset against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader r = ExecuteReader( CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public FbDataReader ExecuteReader(CommandType cmdType, string cmdText, params FbParameter[] commandParameters)
        {
            FbCommand cmd = new FbCommand();
            FbConnection conn = OpenConnection();

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                FbDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
        #region 执行一个带参数的存储过程，返回 DataSet +DataSet RunProcedure(string commandText, SqlParameter [] commandParameters)
        /// <summary>
        /// 执行一个带参数的存储过程，返回 DataSet
        /// 格式 DataSet ds = ExecuteDataset("GetOrders", new SqlParameter("@prodid", 24));
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public DataSet RunProcedure(string commandText, FbParameter[] commandParameters)
        {
            //创建一个命令
            FbCommand sqlCommand = new FbCommand();

            sqlCommand.CommandText = commandText;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Connection = new FbConnection(connStr);
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddRange(commandParameters);

            //创建 DataAdapter 和 DataSet
            FbDataAdapter da = new FbDataAdapter(sqlCommand);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                sqlCommand.Parameters.Clear();
                //返回结果集
                return ds;
            }
            catch (Exception ex)
            {
                string parasList = "";
                foreach (FbParameter para in commandParameters)
                {
                    parasList += "<\r\n>@" + para.ParameterName + ":" + para.Value;
                }
                throw new Exception(ex + "<\r\n>" + commandText + parasList);
            }

        }
        #endregion

        #region 执行SQL语句。返回 DataSet +DataSet ExecuteDataSet(string sql)
        public DataSet ExecuteDataSet(string sql)
        {
            FbConnection conn = new FbConnection(connStr);
            FbCommand sqlCommand = new FbCommand(sql, conn);
            DataSet retrunDataSet = new DataSet();
            FbDataAdapter sqlDataAdapter = new FbDataAdapter(sqlCommand);
            try
            {
                sqlDataAdapter.Fill(retrunDataSet);
                return retrunDataSet;
            }
            catch (Exception ex)
            {
                throw new Exception(ex + "<\r\n>" + sql);
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion


        #region 执行SQL语句。返回 DataTable +DataTable ExecuteDataTable(string sql)
        public DataTable ExecuteDataTable(string sql)
        {
            FbConnection conn = new FbConnection(connStr);
            FbCommand sqlCommand = new FbCommand(sql, conn);
            DataTable retrunDataTable = new DataTable();

            try
            {
                using (FbDataAdapter sqlDataAdapter = new FbDataAdapter(sqlCommand))
                {
                    sqlDataAdapter.Fill(retrunDataTable);
                    return retrunDataTable;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex + "<\r\n>" + sql);
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region 执行带参数的sql + DataSet ExecuteDataSet(string sql, params SqlParameter[] sqlParames)
        /// <summary>
        /// 执行带参数的sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlParames"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string sql, params FbParameter[] sqlParames)
        {
            FbConnection conn = new FbConnection(connStr);
            FbCommand cmd = new FbCommand(sql, conn);
            if (sqlParames.Length > 0)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(sqlParames);
            }
            DataSet ds = new DataSet();
            FbDataAdapter sda = new FbDataAdapter(cmd);
            try
            {
                sda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex + "<\r\n>" + sql);
            }
        }

        #endregion

        #region 执行带参数的sql + DataTable ExecuteDataTable(string sql, params SqlParameter[] sqlParames)
        /// <summary>
        /// 执行带参数的sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlParames"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string sql, params FbParameter[] sqlParames)
        {
            FbConnection conn = new FbConnection(connStr);
            FbCommand cmd = new FbCommand(sql, conn);
            if (sqlParames.Length > 0)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(sqlParames);
            }
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            FbDataAdapter sda = new FbDataAdapter(cmd);
            try
            {
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex + "<\r\n>" + sql);
            }
        }

        #endregion
        /// <summary>
        /// Execute a FbCommand that returns the first column of the first record against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of FbParameter used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public object ExecuteScalar(CommandType cmdType, string cmdText, params FbParameter[] commandParameters)
        {
            FbCommand cmd = new FbCommand();
            using (FbConnection conn = OpenConnection())
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                //conn.Close();
                //conn.Dispose();
                return val;
            }
        }



        /// <summary>
        /// Prepare a command for execution
        /// </summary>
        /// <param name="cmd">FbCommand object</param>
        /// <param name="conn">FbConnection object</param>
        /// <param name="trans">FbTransaction object</param>
        /// <param name="cmdType">Cmd type e.g. stored procedure or text</param>
        /// <param name="cmdText">Command text, e.g. Select * from Products</param>
        /// <param name="cmdParms">FbParameter to use in the command</param>
        private void PrepareCommand(FbCommand cmd, FbConnection conn, FbTransaction trans, CommandType cmdType, string cmdText, FbParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (FbParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
    }
}
