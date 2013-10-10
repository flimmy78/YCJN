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
    public class DALIndicatorSearch
    {
        string rlDBType = "";
        string rtDBType = "";

        string pGl1 = "WHSIS.U1APSH.U1A04013";
        string pGl2 = "WHSIS.U2APSH.U2A04013";  


        /// <summary>
        ///根据unitId 从T_BASE_CALCPARA表（趋势分析表）中 获取不同(unitId和paraId)唯一的表名。数据存储在了不同的表中，耗差类型为0或1。(TargetType:指标类型:0锅炉指标,1汽机指标。ConsumeType:耗差类型:0可控耗差，1不可控耗差）
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetTableName(string companyId, string plantId, string unitId, int TargetType, int ConsumeType, out string errMsg)
        {
            this.init();
            errMsg = "";
            string sql = "select c1.T_OUTTABLE,t.T_UNITID,t.T_PARAID,c1.T_UNIT,t.I_TARGETTYPE,t.I_CONSUMETYPE,t.I_ORDER  from T_BASE_CONSUMEPARA as t left  join T_BASE_CALCPARA as c1 on t.T_UNITID=c1.T_UNITID and t.T_PARAID=c1.T_PARAID  left  join  T_BASE_UNIT as u on t.T_UNITID=u.T_UNITID LEFT JOIN  T_BASE_PLANT AS P ON U.T_PLANTID= P.T_PLANTID  LEFT JOIN  T_BASE_COMPANY AS M  ON P.T_COMPANYID=M.T_COMPANYID where c1.I_CONSUMETYPE  is null  and c1.I_TARGETTYPE IS NOT NULL";

            DataTable dt = null;

            if (!string.IsNullOrEmpty(companyId)&&companyId!="0")
            {
                sql += " AND  M.T_COMPANYID='" + companyId + "'";
            }
            if (!string.IsNullOrEmpty(plantId) && plantId != "0")
            {
                sql += " AND  P.T_PLANTID='" + plantId + "'";
            }
            if (!string.IsNullOrEmpty(unitId) && unitId != "0")
            {
                sql += " AND  t.T_UNITID='" + unitId + "'";
            }
            if (TargetType != -1)
            {
                sql += "  AND c1.I_TARGETTYPE = "+TargetType+"";
            }
            if (ConsumeType != -1)
            {
                sql += "  AND c1.I_CONSUMETYPE = " + ConsumeType + "";
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
