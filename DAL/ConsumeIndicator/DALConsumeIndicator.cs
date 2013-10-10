using System;
using System.Collections.Generic;
using Entity.ConsumeIndicator;
using System.Collections;
using SAC.DB2;
using SAC.Helper;
using System.Data;

namespace DAL.ConsumeIndicator
{
    public class DALConsumeIndicator
    {
        string rlDBType = "";
        string rtDBType = "";

        string pGl1 = "WHSIS.U1APSH.U1A04013";
        string pGl2 = "WHSIS.U2APSH.U2A04013";



        /// <summary>
        /// 将数据对标信息从EXCEL中插入到数据库中。
        /// </summary>
        /// <param name="statisticList"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool InsertData(List<DataInfo> dataList, out string errMsg)
        {
            errMsg = string.Empty;
            ArrayList sqlList = new ArrayList();
            foreach (var info in dataList)
            {
                string sql = "insert into T_INFO_DATASTANDARD(T_COMPANY,T_DATATYPE,T_900_SL,T_900_KL,T_600_HJ,T_600_CCL,T_600_CLS,T_600_CLK,T_600_YLS,T_600_YLK,T_600_J,T_350_HJ,T_350_CNHJ,T_350_CLJ,T_350_YLJ,T_350_GRHJ,T_350_RCLJ,T_350_RYLJ,T_300_HJ,T_300_CGSL,T_300_CGKL,T_300_LHC,T_300_SL,T_300_KL,T_200_HJ,T_200_SL,T_200_KL,T_200_LHC,T_200_J,T_120_HJ,T_120_J,T_120_LHC,T_120_KL,T_120_JC,T_135_LHC,T_100_HJ,T_100_CG,T_100_LHC,T_100_J,T_90_HJ,T_90_NJ,T_90_LHC,T_90_RJ,T_TIME) values( '" + info.T_COMPANY + "','" + info.T_DATATYPE + "','" + info.T_900_SL + "','" + info.T_900_KL + "','" + info.T_600_HJ + "','" + info.T_600_CCL + "','" + info.T_600_CLS + "','" + info.T_600_CLK + "','" + info.T_600_YLS + "','" + info.T_600_YLK + "','" + info.T_600_J + "','" + info.T_350_HJ + "','" + info.T_350_CNHJ + "','" + info.T_350_CLJ + "','" + info.T_350_YLJ + "','" + info.T_350_GRHJ + "','" + info.T_350_RCLJ + "','" + info.T_350_RYLJ + "','" + info.T_300_HJ + "','" + info.T_300_CGSL + "','" + info.T_300_CGKL + "','" + info.T_300_LHC + "','" + info.T_300_SL + "','" + info.T_300_KL + "','" + info.T_200_HJ + "','" + info.T_200_SL + "','" + info.T_200_KL + "','" + info.T_200_LHC + "','" + info.T_200_J + "','" + info.T_120_HJ + "','" + info.T_120_J + "','" + info.T_120_LHC + "','" + info.T_120_KL + "','" + info.T_120_JC + "','" + info.T_135_LHC + "','" + info.T_100_HJ + "','" + info.T_100_CG + "','" + info.T_100_LHC + "','" + info.T_100_J + "','" + info.T_90_HJ + "','" + info.T_90_NJ + "','" + info.T_90_LHC + "','" + info.T_90_RJ + "','"+info.T_TIME+"')";
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
        /// 获取数据对标信息
        /// </summary>
        /// <param name="errMsg"></param>
        /// <param name="time">查询时间</param>
        /// <returns></returns>
        public List<DataInfo> GetInfo(string time, out string errMsg)
        {
            this.init();
            errMsg = "";
            List<DataInfo> infoList = new List<DataInfo>();
            string sql = "select * FROM T_INFO_DATASTANDARD";
            if (!string.IsNullOrEmpty(time))
            {
                sql += " WHERE T_TIME='" + time + "'";
            }
            DataTable dt = null;

            if (rlDBType == "SQL")
            {
                // sql = "select ID_KEY,班次名,起始时间,结束时间 from 班次时间表 order by ID_KEY";
                //dt = DBsql.RunDataTable(sql, out errMsg);
            }
            else
            {
                dt = DBdb2.RunDataTable(sql, out errMsg);
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataInfo info = new DataInfo();
                    //info.ParaId = String.IsNullOrEmpty(dt.Rows[i]["T_PARAID"].ToString()) ? String.Empty : dt.Rows[i]["T_PARAID"].ToString();
                    info.T_COMPANY = dt.Rows[i]["T_COMPANY"].ToString();
                    info.T_DATATYPE = dt.Rows[i]["T_DATATYPE"].ToString();
                    info.T_900_SL = dt.Rows[i]["T_900_SL"].ToString();
                    info.T_900_KL = dt.Rows[i]["T_900_KL"].ToString();
                    info.T_600_HJ = dt.Rows[i]["T_600_HJ"].ToString();
                    info.T_600_CCL = dt.Rows[i]["T_600_CCL"].ToString();
                    info.T_600_CLS = dt.Rows[i]["T_600_CLS"].ToString();
                    info.T_600_CLK = dt.Rows[i]["T_600_CLK"].ToString();
                    info.T_600_YLS = dt.Rows[i]["T_600_YLS"].ToString();
                    info.T_600_YLK = dt.Rows[i]["T_600_YLK"].ToString();
                    info.T_600_J = dt.Rows[i]["T_600_J"].ToString();
                    info.T_350_HJ = dt.Rows[i]["T_350_HJ"].ToString();
                    info.T_350_CNHJ = dt.Rows[i]["T_350_CNHJ"].ToString();
                    info.T_350_CLJ = dt.Rows[i]["T_350_CLJ"].ToString();
                    info.T_350_YLJ = dt.Rows[i]["T_350_YLJ"].ToString();
                    info.T_350_GRHJ = dt.Rows[i]["T_350_GRHJ"].ToString();
                    info.T_350_RCLJ = dt.Rows[i]["T_350_RCLJ"].ToString();
                    info.T_350_RYLJ = dt.Rows[i]["T_350_RYLJ"].ToString();
                    info.T_300_HJ = dt.Rows[i]["T_300_HJ"].ToString();
                    info.T_300_CGSL = dt.Rows[i]["T_300_CGSL"].ToString();
                    info.T_300_CGKL = dt.Rows[i]["T_300_CGKL"].ToString();
                    info.T_300_LHC = dt.Rows[i]["T_300_LHC"].ToString();
                    info.T_300_SL = dt.Rows[i]["T_300_SL"].ToString();
                    info.T_300_KL = dt.Rows[i]["T_300_KL"].ToString();
                    info.T_200_HJ = dt.Rows[i]["T_200_HJ"].ToString();
                    info.T_200_SL = dt.Rows[i]["T_200_SL"].ToString();
                    info.T_200_KL = dt.Rows[i]["T_200_KL"].ToString();
                    info.T_200_LHC = dt.Rows[i]["T_200_LHC"].ToString();
                    info.T_200_J = dt.Rows[i]["T_200_J"].ToString();
                    info.T_120_HJ = dt.Rows[i]["T_120_HJ"].ToString();
                    info.T_120_J = dt.Rows[i]["T_120_J"].ToString();
                    info.T_120_LHC = dt.Rows[i]["T_120_LHC"].ToString();
                    info.T_120_KL = dt.Rows[i]["T_120_KL"].ToString();
                    info.T_120_JC = dt.Rows[i]["T_120_JC"].ToString();
                    info.T_135_LHC = dt.Rows[i]["T_135_LHC"].ToString();
                    info.T_100_HJ = dt.Rows[i]["T_100_HJ"].ToString();
                    info.T_100_CG = dt.Rows[i]["T_100_CG"].ToString();
                    info.T_100_LHC = dt.Rows[i]["T_100_LHC"].ToString();
                    info.T_100_J = dt.Rows[i]["T_100_J"].ToString();
                    info.T_90_HJ = dt.Rows[i]["T_90_HJ"].ToString();
                    info.T_90_NJ = dt.Rows[i]["T_90_NJ"].ToString();
                    info.T_90_LHC = dt.Rows[i]["T_90_LHC"].ToString();
                    info.T_90_RJ = dt.Rows[i]["T_90_RJ"].ToString();
                    infoList.Add(info);
                }

            }
            //去掉重复。
            //return infoList.Distinct(new EqualCompare<ParaTableInfo>((x, y) => (x != null && y != null) &&(x.OutTableName == y.OutTableName))).ToList();
            //不能过滤，因为会把ParaId过滤掉。以ParaId来取值的。
            return infoList;
        }

        /// <summary>
        /// 将各个类型机组能耗信息从EXCEL中插入到数据库中。
        /// </summary>
        /// <param name="statisticList"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool InsertChartDetailData(List<UnitConsumeInfo> dataList, out string errMsg)
        {
            errMsg = string.Empty;
            ArrayList sqlList = new ArrayList();
            foreach (var info in dataList)
            {
                string sql = "insert into T_INFO_UNITCONSUME(T_DWNAME,T_UNITCODE,T_TIME,T_COUNT,T_USEHOUR,T_OF,T_RDB,T_CYDL,T_GDMH,T_DBMH,T_GDL,T_JTPJB) VALUES('"+info.T_DWNAME+"','"+info.T_UNITCODE+"','"+info.T_TIME.ToString().Replace("/","-")+"','"+info.T_COUNT+"','"+info.T_USEHOUR+"','"+info.T_OF+"','"+info.T_RDB+"','"+info.T_CYDL+"','"+info.T_GDMH+"','"+info.T_DBMH+"','"+info.T_GDL+"','"+info.T_JTPJB+"')";
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
        /// 根据时间获取各个类型机组能耗信息
        /// </summary>
        /// <returns></returns>
        public List<UnitConsumeInfo> GetUnitConsumeList(string time,out string errMsg)
        {
            this.init();
            errMsg = "";
            List<UnitConsumeInfo> infoList = new List<UnitConsumeInfo>();
            string sql = "SELECT * FROM ADMINISTRATOR.T_INFO_UNITCONSUME  AS U LEFT JOIN T_INFO_UNIT AS  T ON  U.T_UNITCODE=T.T_CODE  LEFT JOIN  T_BASE_UNIT  AS S ON   S.T_UNITID=U.T_UNITCODE  LEFT JOIN  T_BASE_PLANT AS B ON   B.T_PLANTID=S.T_PLANTID LEFT JOIN  T_BASE_COMPANY AS C ON C.T_COMPANYID=B.T_COMPANYID WHERE 1=1";

            if (!string.IsNullOrEmpty(time))
            {
                sql += " AND U.T_TIME = '"+time+"'";
            }
            DataTable dt = null;

            if (rlDBType == "SQL")
            {
                // sql = "select ID_KEY,班次名,起始时间,结束时间 from 班次时间表 order by ID_KEY";
                //dt = DBsql.RunDataTable(sql, out errMsg);
            }
            else
            {
                dt = DBdb2.RunDataTable(sql, out errMsg);
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UnitConsumeInfo info = new UnitConsumeInfo();
                    //info.ParaId = String.IsNullOrEmpty(dt.Rows[i]["T_PARAID"].ToString()) ? String.Empty : dt.Rows[i]["T_PARAID"].ToString();
                    ////
                    info.T_DWNAME = String.IsNullOrEmpty(dt.Rows[i]["T_DWNAME"].ToString()) ? String.Empty : dt.Rows[i]["T_DWNAME"].ToString();
                    info.T_PLANTNAME = String.IsNullOrEmpty(dt.Rows[i]["T_PLANTDESC"].ToString()) ? String.Empty : dt.Rows[i]["T_PLANTDESC"].ToString();
                    info.T_UNITCODE = String.IsNullOrEmpty(dt.Rows[i]["T_UNITCODE"].ToString()) ? String.Empty : dt.Rows[i]["T_UNITCODE"].ToString();
                    info.T_COUNT = String.IsNullOrEmpty(dt.Rows[i]["T_COUNT"].ToString()) ? String.Empty : dt.Rows[i]["T_COUNT"].ToString();
                    info.T_USEHOUR = String.IsNullOrEmpty(dt.Rows[i]["T_USEHOUR"].ToString()) ? String.Empty : dt.Rows[i]["T_USEHOUR"].ToString();
                    info.T_OF = String.IsNullOrEmpty(dt.Rows[i]["T_OF"].ToString()) ? String.Empty : dt.Rows[i]["T_OF"].ToString();
                    info.T_RDB = String.IsNullOrEmpty(dt.Rows[i]["T_RDB"].ToString()) ? String.Empty : dt.Rows[i]["T_RDB"].ToString();
                    info.T_CYDL = String.IsNullOrEmpty(dt.Rows[i]["T_CYDL"].ToString()) ? String.Empty : dt.Rows[i]["T_CYDL"].ToString();
                    info.T_GDMH = String.IsNullOrEmpty(dt.Rows[i]["T_GDMH"].ToString()) ? String.Empty : dt.Rows[i]["T_GDMH"].ToString();
                    info.T_DBMH = String.IsNullOrEmpty(dt.Rows[i]["T_DBMH"].ToString()) ? String.Empty : dt.Rows[i]["T_DBMH"].ToString();
                    info.T_GDL = String.IsNullOrEmpty(dt.Rows[i]["T_GDL"].ToString()) ? String.Empty : dt.Rows[i]["T_GDL"].ToString();
                    info.T_JTPJB = String.IsNullOrEmpty(dt.Rows[i]["T_JTPJB"].ToString()) ? String.Empty : dt.Rows[i]["T_JTPJB"].ToString();
                    info.T_TYPE = String.IsNullOrEmpty(dt.Rows[i]["T_CAPABILITYLEVEL"].ToString()) ? String.Empty : dt.Rows[i]["T_CAPABILITYLEVEL"].ToString();
                  
                    ///
                    infoList.Add(info);
                }

            }
            //去掉重复。
            //return infoList.Distinct(new EqualCompare<ParaTableInfo>((x, y) => (x != null && y != null) &&(x.OutTableName == y.OutTableName))).ToList();
            //不能过滤，因为会把ParaId过滤掉。以ParaId来取值的。
            return infoList;
        }

        /// <summary>
        /// 获取所有供电能耗月线信息。
        /// </summary>
        /// <returns></returns>
        public DataTable GetMonthConsume(out string errMsg)
        {
            this.init();
            errMsg = "";
            string sql = "select * from T_INFO_MONTHCONSUME";

            DataTable dt = null;
         
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
        /// 获取指定时间供电能耗月线信息。
        /// </summary>
        /// <returns></returns>
        public DataTable GetMonthConsumeByTime(string year,string month,out string errMsg)
        {
            this.init();
            errMsg = "";
            string sql = "select * from T_INFO_MONTHCONSUME where T_YEAR='"+year+"' and T_MONTH='"+month+"'";

            DataTable dt = null;

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
        /// 更新指定时间供电能耗月线信息。
        /// </summary>
        /// <returns></returns>
        public bool UpdateMonthConsumeByTime(MonthConsumeInfo info, out string errMsg)
        {
            this.init();
            errMsg = "";
            bool flag = false;
            string sql = "update T_INFO_MONTHCONSUME SET T_VALUE=" + info.values + " where T_YEAR='" + info.year + "' and T_MONTH='" + info.month + "'";

            if (rlDBType == "SQL")
            {
                // sql = "select ID_KEY,班次名,起始时间,结束时间 from 班次时间表 order by ID_KEY";
                //dt = DBsql.RunDataTable(sql, out errMsg);
            }
            else
            {
                flag = DBdb2.RunNonQuery(sql, out errMsg);
            }
            return flag;
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
