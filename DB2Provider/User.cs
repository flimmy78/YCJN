using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Odbc;

namespace DB2Provider
{
    public class User
    {
        private string conString = "Dsn=CHD;uid=Administrator;pwd=1QAZxsw@;Provider=System.Data.Odbc";
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 通信地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Telphone { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public int DepartID { get; set; }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <returns></returns>
        public DataTable ReadData()
        {            
            OdbcConnection odbcCon = new OdbcConnection(conString);
            OdbcDataAdapter odbcAdpater = new OdbcDataAdapter("SELECT * FROM ADMINISTRATOR.USER", odbcCon);
            DataTable DT = new DataTable();
            odbcAdpater.Fill(DT);
            return DT;
        }
    }
}
