using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.Helper;
using System.Data;
using SAC.DB2;
using Entity.Statistic;

namespace DAL.StatisticalComparison
{
    public class DALVerticalContrastSearch
    {
        string rlDBType = "";
        string rtDBType = "";

        string pGl1 = "WHSIS.U1APSH.U1A04013";
        string pGl2 = "WHSIS.U2APSH.U2A04013";

        /// <summary>
        /// 获取机组的参数列表
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetParaName(string unitId, out string errMsg)
        {
            this.init();
            errMsg = "";
            string sql = "select * from T_BASE_CRICPARA where 1=1";

            DataTable dt = null;

            if (!string.IsNullOrEmpty(unitId))
            {
                sql += " AND  T_UNITID='" + unitId + "'";
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
        ///根据unitId 从T_BASE_CALCPARA表（趋势分析表）中 获取不同(unitId和paraId)唯一的表名。数据存储在了不同的表中，耗差类型为0或1。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetTableName(string unitId,string paraId, out string errMsg)
        {
            this.init();
            errMsg = "";
            string sql = "select c1.T_OUTTABLE,t.T_UNITID,t.T_PARAID,t.T_UNIT,t.I_TARGETTYPE,t.I_CONSUMETYPE,t.I_ORDER  from T_BASE_CONSUMEPARA as t left  join T_BASE_CALCPARA as c1 on t.T_UNITID=c1.T_UNITID and t.T_PARAID=c1.T_PARAID where 1=1";

            DataTable dt = null;

            if (!string.IsNullOrEmpty(unitId))
            {
                sql += " AND  t.T_UNITID='" + unitId + "'";
            }
            if (!string.IsNullOrEmpty(paraId))
            {
                sql += " AND t.T_PARAID='" + paraId + "'";
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
        ///根据时间段取得 参数描述（指标名称），平均基准值。取得的paraId留着使用。注：paraId+_el_B为实际值，paraId+_B为耗差值
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetValueByPara(string tableName, string beginTime, string endTime, string unitId, string paraId, out string errMsg)
        {
            this.init();
            errMsg = "";
            string sql = "select c.T_PARAID, b.T_DESC,b.I_TARGETTYPE,b.I_CONSUMETYPE,avg(c.D_VALUE) AS counts  from " + tableName + "  as c  left   join  T_BASE_CONSUMEPARA  as b on  c.T_UNITID=b.T_UNITID  and  c.T_PARAID=b.T_PARAID WHERE 1=1";

            DataTable dt = null;

            if (!string.IsNullOrEmpty(beginTime) && !string.IsNullOrEmpty(endTime))
            {
                sql += " AND  c.T_DATETIME between " + beginTime + "' and " + endTime + "";
            }

            if (!string.IsNullOrEmpty(unitId) && unitId != "0")
            {
                sql += " AND  c.T_UNITID='" + unitId + "'";
            }

            if (!string.IsNullOrEmpty(paraId))
            {
                sql += " AND  c.T_PARAID='" + paraId + "'";
            }

            sql += " group by c.T_PARAID,b.T_DESC,b.I_TARGETTYPE,b.I_CONSUMETYPE ";
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
