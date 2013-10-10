using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.Helper;
using SAC.DB2;
using SAC;
using System.Collections;
using DAO;
using  Entity.Home;
using System.Data;

namespace DAL.Task
{
    public class DALTask
    {
        string rlDBType = "";
        string rtDBType = "";

        string pGl1 = "WHSIS.U1APSH.U1A04013";
        string pGl2 = "WHSIS.U2APSH.U2A04013";



        /// <summary>
        /// 将生产统计信息从EXCEL中插入到数据库中。
        /// </summary>
        /// <param name="statisticList"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool InsertHomeData(List<StatisticInfo> statisticList, out string errMsg)
        {
            errMsg = string.Empty;
            ArrayList sqlList = new ArrayList();
            foreach (var info in statisticList)
            {
                string sql = "insert into T_INFO_STATISTIC(T_INDICATORNAME,T_UNITNAME,T_TIME,D_HNALL,D_HNADD,D_DTALL,D_DTADD,D_HDALL,D_HDADD,D_GDALL,D_GDADD,D_ZDTALL,D_ZDTADD) values('" + info.T_INDICATORNAME + "','" + info.T_UNITNAME + "','" + info.T_TIME + "'," + info.D_HNALL + "," + info.D_HNADD + "," + info.D_DTALL + "," + info.D_DTADD + "," + info.D_HDALL + "," + info.D_HDADD + "," + info.D_GDALL + "," + info.D_GDADD + "," + info.D_ZDTALL + "," + info.D_ZDTADD + ")";

                sqlList.Add(sql);
            }
            try
            {
                DBdb2.ExecuteSqlTran(sqlList);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 根据时间获取五大集团不同容量最优机组信息。
        /// </summary>
        /// <param name="times"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetInfoByTime(string times, out string errMsg)
        {
            this.init();
            errMsg = "";
            string sql = "select * from T_INFO_DATASTANDARD where T_DATATYPE='平均值'";

            DataTable dt = null;

            if (!string.IsNullOrEmpty(times))
            {
                sql += " AND  T_TIME='" + times + "'";
            }

            if (rlDBType == "SQL")
            {
                // sql = "select ID_KEY,班次名,起始时间,结束时间 from 班次时间表 order by ID_KEY";
                //dt = DBsql.RunDataTable(sql, out errMsg);
            }
            else
            {
                dt = DBdb2.RunDataTable(sql, out errMsg);
            }

            return dt;
        }

        /// <summary>
        /// 根据时间获取四类信息（首页使用）。
        /// </summary>
        /// <param name="times"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetHomeByTime(string times, out string errMsg)
        {
            this.init();
            errMsg = "";
            string sql = "select * from T_INFO_STATISTIC where 1=1";

            DataTable dt = null;

            if (!string.IsNullOrEmpty(times))
            {
                sql += " AND  T_TIME='" + times + "'";
            }

            if (rlDBType == "SQL")
            {
                // sql = "select ID_KEY,班次名,起始时间,结束时间 from 班次时间表 order by ID_KEY";
                //dt = DBsql.RunDataTable(sql, out errMsg);
            }
            else
            {
                dt = DBdb2.RunDataTable(sql, out errMsg);
            }

            return dt;
        }

        /// <summary>
        /// 初始化数据库。
        /// </summary>
        /// <returns></returns>
        private string init()
        {
            rlDBType = IniHelper.ReadIniData("RelationDBbase", "DBType", null);
            rtDBType = IniHelper.ReadIniData("RTDB", "DBType", null);
            pGl1 = IniHelper.ReadIniData("Report", "FH1", null);
            pGl2 = IniHelper.ReadIniData("Report", "FH2", null);

            return rlDBType;
        }

        public string GetDBtype()
        {
            this.init();
            string DBtype = rlDBType;
            return DBtype;
        }

}
}
