using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using System.Data;
using System.Data.Odbc;

namespace DB2Provider
{
    
    public class DataLink
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private static string conString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
        /// <summary>
        /// 连接对象
        /// </summary>
        private OdbcConnection odbcCon = null;
        /// <summary>
        /// 操作命令
        /// </summary>
        private OdbcCommand odbcCmd = null;
        /// <summary>
        /// 数据适配器
        /// </summary>
        private OdbcDataAdapter odbcAdapter = null;
        /// <summary>
        /// 数据读取器
        /// </summary>
        private OdbcDataReader odbcReader = null;
        /// <summary>
        /// 创建连接
        /// </summary>
        private void CreateConnection()
        {
            if (odbcCon == null)
                odbcCon = new OdbcConnection(conString);
        }
        /// <summary>
        /// 创建命令
        /// </summary>
        private void CreateCommand()
        {
            if (odbcCmd == null)
            {
                odbcCmd = new OdbcCommand();
                odbcCmd.Connection = odbcCon;
            }
        }
        /// <summary>
        /// 打开连接
        /// </summary>
        private void Open()
        {
            if (odbcCon != null)
            {
                odbcCon.Close();
                odbcCon.Open();
            }
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            if (odbcCon != null)
            {
                odbcCon.Close();
            }
        }
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="sql"></param>
        public void Excute(string sql)
        {
            try
            {
                CreateConnection();
                CreateCommand();
                odbcCmd.CommandType = System.Data.CommandType.Text;
                odbcCmd.CommandText = sql;
                Open();
                odbcCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
                odbcCmd = null;
                odbcCon = null;
            }
        }
        /// <summary>
        /// 执行存储过程命令
        /// </summary>
        /// <param name="procedureName">存储过程名</param>
        public void ExcuteProduce(string procedureName)
        {
            try
            {
                CreateConnection();
                CreateCommand();
                odbcCmd.CommandType = System.Data.CommandType.StoredProcedure;
                odbcCmd.CommandText = procedureName;
                Open();
                odbcCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
                odbcCmd = null;
                odbcCon = null;
            }
        }
        /// <summary>
        /// 返回查询结果表
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <returns></returns>
        public DataTable ExcuteRetureTable(string sql)
        {
            try
            {
                DataTable dt = new DataTable();                
                CreateConnection();
                Open();
                if (odbcAdapter == null)
                    odbcAdapter = new OdbcDataAdapter(sql, odbcCon);
                odbcAdapter.Fill(dt);                

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
                odbcCmd = null;
                odbcCon = null;
            }
        }
        /// <summary>
        /// 返回第一行第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public object RetureFirstValue(string sql)
        {
            try
            {
                CreateConnection();
                CreateCommand();
                odbcCmd.CommandType = System.Data.CommandType.Text;
                odbcCmd.CommandText = sql;
                Open();
                return odbcCmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
                odbcCmd = null;
                odbcCon = null;
            }
        }
        /// <summary>
        /// 返回数据表第一行第一列值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public object GetFirstValue(string sql)
        {
            object firstValue = null;
            DataTable dt = ExcuteRetureTable(sql);
            if (dt != null && dt.Rows.Count>0)
            {
                firstValue= dt.Rows[0][0].ToString();
            }
            return firstValue;
        }
        /// <summary>
        /// 返回第一行指定列值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="indexColumn"></param>
        /// <returns></returns>
        public object GetValue(string sql,int indexColumn)
        {
            object objValue = null;
            DataTable dt = ExcuteRetureTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                objValue = dt.Rows[0][indexColumn];
            }
            return objValue;
        }

        private void GetValue()
        {
            

        }


    }
}
