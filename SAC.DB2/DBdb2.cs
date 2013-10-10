using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using IBM.Data.DB2;
using System.Collections;
using System.Data.OleDb;

namespace SAC.DB2
{
    /// <summary>
    /// DB2 数据库操作类
    /// </summary>
    public class DBdb2
    {
        public DBdb2()
        { }

        //static protected void CloseConn(DB2Connection db2con)
        //{
        //    db2con.Close();
        //}

        //static protected DB2Connection GetConn()
        //{
        //    string conn = System.Configuration.ConfigurationSettings.AppSettings["conDB2"].ToString();
        //    DB2Connection db2con = new DB2Connection(conn);

        //    return db2con;
        //}

        ///// <summary>
        ///// 获取单一对象的值
        ///// </summary>
        ///// <param name="conStr"></param>
        ///// <param name="sqlcmd"></param>
        ///// <param name="errMsg"></param>
        ///// <returns></returns>
        //static public object RunSingle(string sqlcmd, out string errMsg)
        //{
        //    errMsg = "";
        //    DB2Connection connect = GetConn();
        //    try
        //    {
        //        return DAO.DB2Helper.ExecuteScalar(connect, CommandType.Text, sqlcmd);
        //    }
        //    catch (Exception e)
        //    {
        //        errMsg = e.Message;
        //        return null;
        //    }
        //    finally
        //    {
        //        CloseConn(connect);
        //    }
        //}

        ///// <summary>
        ///// 返回 DataSet
        ///// </summary>
        ///// <param name="sqlCmd">sql语句</param>
        ///// <param name="errMsg"></param>
        ///// <returns></returns>
        //static public DataSet RunDataSet(string sqlCmd, out string errMsg)
        //{
        //    errMsg = "";
        //    DB2Connection db = GetConn();

        //    try
        //    {
        //        return DAO.DB2Helper.ExecuteDataset(db, CommandType.Text, sqlCmd);

        //    }
        //    catch (Exception ce)
        //    {
        //        errMsg = ce.Message;
        //        return null;
        //    }
        //    finally
        //    {
        //        CloseConn(db);
        //    }
        //}

        //static public DataTable RunDataTable(string sqlCmd, out string errMsg)
        //{
        //    DataSet ds = RunDataSet(sqlCmd, out errMsg);
        //    if (ds == null || ds.Tables == null || ds.Tables.Count == 0)
        //    {
        //        errMsg = string.Format("查询{0}返回数据集为空", sqlCmd);
        //        return null;
        //    }
        //    else
        //        return ds.Tables[0];
        //}

        //static public DataRow RunDataRow(string sqlCmd, out string errMsg)
        //{
        //    DataTable table = RunDataTable(sqlCmd, out errMsg);
        //    if (table == null || table.Rows == null || table.Rows.Count == 0)
        //    {
        //        errMsg = string.Format("查询{0}返回数据集为空", sqlCmd);
        //        return null;
        //    }
        //    else
        //        return table.Rows[0];
        //}

        private static string RetConString()
        {
            //string conn = "Provider=IBMDADB2;Data Source= " + IniHelper.ReadIniData("RelationDBbase", "DBName", null) + ";User ID=" + IniHelper.ReadIniData("RelationDBbase", "DBUser", null) + ";Password=" + IniHelper.ReadIniData("RelationDBbase", "DBPwd", null) + ";Default Collection =alfm01;Default Schema=Schema";
            //string conn = "Provider=IBMDADB2;Database=" + IniHelper.ReadIniData("RelationDBbase", "DBName", null) + ";Hostname=" + IniHelper.ReadIniData("RelationDBbase", "DBIP", null) + ";Protocol=TCPIP; Port=50000;Uid=" + IniHelper.ReadIniData("RelationDBbase", "DBUser", null) + ";Pwd=" + IniHelper.ReadIniData("RelationDBbase", "DBPwd", null) + ";";
            //string conn = "Provider=IBMDADB2;Database=ITST;Hostname=192.168.0.106;Protocol=TCPIP; Port=50000;Uid=Administrator;Pwd=eagle123*;";
            //string conn = "Provider=IBMDADB2;Database=ITST;Hostname=172.18.25.178;Protocol=TCPIP; Port=50000;Uid=Administrator;Pwd=sacsis;";//连财哥电脑
            //string conn = "Provider=IBMDADB2;Data Source=YCJX;User ID=administrator;Password=1qazXSW@;";//连自己电脑
             string conn=System.Configuration.ConfigurationSettings.AppSettings["conDB2"].ToString();
            //string conn = "Provider=IBMDADB2;Database=YCJX;Hostname=10.76.66.44;Protocol=TCPIP; Port=50000;Uid=Administrator;Pwd=1qazXSW@;";//服务器上VS

            return conn;
        }
        public static string SetConString()
        {
            //string conn = "Provider=IBMDADB2;Data Source= " + IniHelper.ReadIniData("RelationDBbase", "DBName", null) + ";User ID=" + IniHelper.ReadIniData("RelationDBbase", "DBUser", null) + ";Password=" + IniHelper.ReadIniData("RelationDBbase", "DBPwd", null) + ";Default Collection =alfm01;Default Schema=Schema";
            //string conn = "Provider=IBMDADB2;Database=ITST;Hostname=192.168.0.106;Protocol=TCPIP; Port=50000;Uid=Administrator;Pwd=eagle123*;";
            //string conn = "Provider=IBMDADB2;Database=YCJX;Hostname=10.76.66.44;Protocol=TCPIP; Port=50000;Uid=Administrator;Pwd=1qazXSW@;";//服务器上VS
            //string conn = "Provider=IBMDADB2;Data Source=YCJX;User ID=ADMINISTRATOR;Password=1qazXSW@;";//连自己电脑
            //string conn = "Provider=IBMDADB2;Database=" + IniHelper.ReadIniData("RelationDBbase", "DBName", null) + ";Hostname=" + IniHelper.ReadIniData("RelationDBbase", "DBIP", null) + ";Protocol=TCPIP; Port=50000;Uid=" + IniHelper.ReadIniData("RelationDBbase", "DBUser", null) + ";Pwd=" + IniHelper.ReadIniData("RelationDBbase", "DBPwd", null) + ";";
            string conn = System.Configuration.ConfigurationSettings.AppSettings["conDB2"].ToString();
           
            return conn;
        }

        static public int RunCommand(string sqlCmd, out string errMsg)
        {
            //errMsg = "";
            //OleDbConnection olecon = new OleDbConnection(conn);
            //try
            //{
            //    return DAO.DB2Helper.ExecuteNonQuery(connect, CommandType.Text, sqlCmd);
            //}
            //catch (Exception e)
            //{
            //    errMsg = e.Message;
            //    return 0;
            //}
            //finally
            //{
            //    CloseConn(connect);
            //}
            errMsg = "";
            string conn = RetConString();// System.Configuration.ConfigurationSettings.AppSettings["conStr"];

            using (OleDbConnection connection = new OleDbConnection(conn))
            {
                using (OleDbCommand cmd = new OleDbCommand(sqlCmd, connection))
                {
                    try
                    {
                        connection.Open();
                        int i = cmd.ExecuteNonQuery();

                        //if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        //{
                        //    return null;
                        //}
                        //else
                        //{
                        //    return obj;
                        //}
                        return i;
                    }
                    catch (System.Data.OleDb.OleDbException e)
                    {
                        errMsg = e.Message;
                        return 0;
                    }
                    finally { connection.Close(); }
                }
            }
        }

        static public int RunRowCount(string sqlCmd, out string errMsg)
        {
            object o = RunSingle(sqlCmd, out errMsg);
            if (o == null) return -1;
            return intParse(o.ToString());
        }

        static public int intParse(string value)
        {
            return (value.Length == 0) ? -1 : int.Parse(value);
        }

        static protected OleDbConnection GetConn()
        {
            //string conn = "Provider=IBMDADB2;Data Source= " + IniHelper.ReadIniData("RelationDBbase", "DBName", null) + ";User ID=" + IniHelper.ReadIniData("RelationDBbase", "DBUser", null) + ";Password=" + IniHelper.ReadIniData("RelationDBbase", "DBPwd", null) + ";Default Collection =alfm01;Default Schema=Schema";
            ////System.Configuration.ConfigurationSettings.AppSettings["conStr"];
            string conn = RetConString();
            OleDbConnection olecon = new OleDbConnection(conn);
            if (olecon != null)
            {
                olecon.Open();
            }

            return olecon;
        }

        static public bool RunNonQuery(string sqlCmd, out string errMsg)
        {
            errMsg = "";

            string conn = RetConString();

            using (OleDbConnection connection = new OleDbConnection(conn))
            {
                using (OleDbCommand cmd = new OleDbCommand(sqlCmd, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ce)
                    {
                        errMsg = ce.Message;
                        return false;
                    }
                    finally { connection.Close(); }
                }
            }
            return true;
        }

        /// <summary>
        /// 调用存储过程
        /// </summary>
        /// <param name="ID_KEY"></param>
        /// <param name="bName"></param>
        /// <param name="qsrq"></param>
        /// <param name="jsrq"></param>
        /// <param name="BZ"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        static public object RunSingle_SP(string sp_name, string ID_KEY, string qsrq, string jsrq, string bz, out string errMsg)
        {

            OleDbCommand comm = new OleDbCommand();

            AddInParamToSqlCommand(comm, "@ID_KEY", ID_KEY);
            AddInParamToSqlCommand(comm, "@qsrq", qsrq);
            AddInParamToSqlCommand(comm, "@jsrq", jsrq);
            AddInParamToSqlCommand(comm, "@bz", bz);

            OleDbParameter outstr = AddOutParamToSqlCommand(comm, "@value", OleDbType.VarChar, 50);

            RunSingle_SP(sp_name, comm, out errMsg); //RunDataTable_SP("getdata", comm, out err

            return outstr.Value.ToString();
        }

        /// <summary>
        /// 调用存储过程[多班值值报]
        /// </summary>
        /// <param name="ID_KEY"></param>
        /// <param name="bName"></param>
        /// <param name="qsrq"></param>
        /// <param name="jsrq"></param>
        /// <param name="BZ"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        static public object RunSingle_SP(string sp_name, string ID_KEY, string qsrq, string jsrq, string bz, string paras, string value, out string errMsg)
        {
            OleDbCommand comm = new OleDbCommand();

            AddInParamToSqlCommand(comm, "@ID_KEY", ID_KEY);
            AddInParamToSqlCommand(comm, "@qsrq", qsrq);
            AddInParamToSqlCommand(comm, "@jsrq", jsrq);
            AddInParamToSqlCommand(comm, "@bz", bz);

            if (paras != null && paras != "")
            {
                string str = "";
                string strRes = "";
                string[] arr = null;
                string[] val = null;

                if (paras.Contains(','))
                    arr = paras.Split(',');
                else
                {
                    arr = new string[1];
                    arr[0] = paras;
                }

                if (value.Contains(','))
                    val = value.Split(',');
                else
                {
                    val = new string[1];
                    val[0] = value;
                }

                for (int i = 0; i < arr.Length; i++)
                {
                    str = "@" + arr[i].ToString();
                    strRes = val[i].ToString();

                    AddInParamToSqlCommand(comm, str, strRes);
                }
            }

            OleDbParameter outstr = AddOutParamToSqlCommand(comm, "@value", OleDbType.VarChar, 50);

            RunSingle_SP(sp_name, comm, out errMsg); //RunDataTable_SP("getdata", comm, out err

            return outstr.Value.ToString();
        }

        static public object RunSingle_SP(string sp_name, OleDbCommand comm, out string errMsg)
        {
            errMsg = "";
            OleDbConnection connect = GetConn();
            try
            {
                comm.Connection = connect;
                comm.CommandText = sp_name;
                comm.CommandType = CommandType.StoredProcedure;
                return comm.ExecuteScalar();

            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            finally
            {
                connect.Close();
            }
        }

        /// VarChar和NVarChar等字符串类型需要Size参数
        static public OleDbParameter AddOutParamToSqlCommand(OleDbCommand comm, string pName, OleDbType pType, int size)
        {
            OleDbParameter param = new OleDbParameter(pName, pType, size);
            param.Direction = ParameterDirection.Output;
            comm.Parameters.Add(param);
            return param;
        }

        static public OleDbParameter AddInParamToSqlCommand(OleDbCommand comm, string pName, object pValue)
        {
            OleDbParameter param = new OleDbParameter(pName, pValue);
            comm.Parameters.Add(param);
            return param;
        }

        static public OleDbParameter AddOutParamToSqlCommand(OleDbCommand comm, string pName, OleDbType pType)
        {
            OleDbParameter param = new OleDbParameter(pName, pType);
            param.Direction = ParameterDirection.Output;
            comm.Parameters.Add(param);
            return param;
        }
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        /// <param name="conn"></param>
        static protected void CloseConn(OleDbConnection conn)
        {
            conn.Close();
        }

        /// <summary>
        /// 获取单一对象的值
        /// </summary>
        /// <param name="conStr"></param>
        /// <param name="sqlcmd"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        static public object RunSingle(string sqlcmd, out string errMsg)
        {
            errMsg = "";
            //OleDbConnection connect = GetConn();
            //OleDbCommand comm = new OleDbCommand();
            //try
            //{
            //    comm.Connection = connect;
            //    comm.CommandText = sqlcmd;
            //    comm.CommandType = CommandType.Text;

            //    return comm.ExecuteScalar();
            //}
            //catch (Exception e)
            //{
            //    errMsg = e.Message;
            //    return null;
            //}
            //finally
            //{
            //    CloseConn(connect);
            //}

            string conn = RetConString();

            using (OleDbConnection connection = new OleDbConnection(conn))
            {
                using (OleDbCommand cmd = new OleDbCommand(sqlcmd, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.OleDb.OleDbException e)
                    {
                        errMsg = e.Message;
                        return null;
                    }
                    finally { connection.Close(); }
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString)
        {
            //string conn = System.Configuration.ConfigurationSettings.AppSettings["conStr"];
            string conn = RetConString();
            using (OleDbConnection connection = new OleDbConnection(conn))
            {
                using (OleDbCommand cmd = new OleDbCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.OleDb.OleDbException e)
                    {
                        connection.Close();
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            //string conn = System.Configuration.ConfigurationSettings.AppSettings["conStr"];
            string conn = RetConString();
            using (OleDbConnection connection = new OleDbConnection(conn))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    OleDbDataAdapter command = new OleDbDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.OleDb.OleDbException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }

        /// <summary>
        /// 返回 DataSet
        /// </summary>
        /// <param name="sqlCmd">sql语句</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        static public DataSet RunDataSet(string sqlCmd, out string errMsg)
        {
            errMsg = "";
            OleDbConnection db = GetConn();

            DataSet ds = new DataSet();
            try
            {
                OleDbDataAdapter command = new OleDbDataAdapter(sqlCmd, db);
                command.Fill(ds, "ds");
            }
            catch (System.Data.OleDb.OleDbException ce)
            {
                errMsg = ce.Message;
                return null;
            }
            finally
            {
                CloseConn(db);
            }
            return ds;
        }

        static public DataTable RunDataTable(string sqlCmd, out string errMsg)
        {
            DataSet ds = RunDataSet(sqlCmd, out errMsg);
            if (ds == null || ds.Tables == null || ds.Tables.Count == 0)
            {
                errMsg = string.Format("查询{0}返回数据集为空", sqlCmd);
                return null;
            }
            else
                return ds.Tables[0];
        }

        static public DataRow RunDataRow(string sqlCmd, out string errMsg)
        {
            DataTable table = RunDataTable(sqlCmd, out errMsg);
            if (table == null || table.Rows == null || table.Rows.Count == 0)
            {
                errMsg = string.Format("查询{0}返回数据集为空", sqlCmd);
                return null;
            }
            else
                return table.Rows[0];
        }

        #region  执行多条SQL语句，实现数据库事务。
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">ArrayList</param>
        public static void ExecuteSqlTran(ArrayList sqlList)
        {
            bool mustCloseConnection = false;
            string ConString = System.Configuration.ConfigurationSettings.AppSettings["conInsertDB2"].ToString();
            using (DB2Connection conn = new DB2Connection(ConString))
            {
                conn.Open();
                using (DB2Transaction trans = conn.BeginTransaction())
                {
                    DB2Command cmd = new DB2Command();
                    try
                    {
                        for (int i = 0; i < sqlList.Count; i++)
                        {
                            string cmdText = sqlList[i].ToString();
                            PrepareCommand(cmd, conn, trans, CommandType.Text, cmdText, null, out mustCloseConnection);
                            int val = cmd.ExecuteNonQuery();

                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                        cmd.Dispose();
                    }
                }
            }
        }

       

        /// <summary>
        /// 预处理用户提供的命令,数据库连接/事务/命令类型/参数
        /// </summary>
        /// <param name="command">要处理的db2Command</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="transaction">一个有效的事务或者是null值</param>
        /// <param name="commandType">命令类型 (存储过程,命令文本, 其它.)</param>
        /// <param name="commandText">存储过程名或都T-SQL命令文本</param>
        /// <param name="commandParameters">和命令相关联的db2Parameter参数数组,如果没有参数为'null'</param>
        /// <param name="mustCloseConnection"><c>true</c> 如果连接是打开的,则为true,其它情况下为false.</param>
        private static void PrepareCommand(DB2Command command, DB2Connection connection, DB2Transaction transaction, CommandType commandType, string commandText, DB2Parameter[] commandParameters, out bool mustCloseConnection)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }

            // 给命令分配一个数据库连接.
            command.Connection = connection;

            // 设置命令文本(存储过程名或SQL语句)
            command.CommandText = commandText;

            // 分配事务
            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }

            // 设置命令类型.
            command.CommandType = commandType;

            // 分配命令参数
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return;

        }
        /// <summary>
        /// 将db2Parameter参数数组(参数值)分配给db2Command命令.
        /// 这个方法将给任何一个参数分配DBNull.Value;
        /// 该操作将阻止默认值的使用.
        /// </summary>
        /// <param name="command">命令名</param>
        /// <param name="commandParameters">DB2Parameters数组</param>

        private static void AttachParameters(DB2Command command, DB2Parameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (DB2Parameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // 检查未分配值的输出参数,将其分配以DBNull.Value.
                        if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }


        #endregion
    }
}
