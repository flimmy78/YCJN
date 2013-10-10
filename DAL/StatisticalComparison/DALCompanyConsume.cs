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
    /// <summary>
    /// 集团耗差指标分析页面。
    /// </summary>
    public class DALCompanyConsume
    {
        string rlDBType = "";
        string rtDBType = "";

        string pGl1 = "WHSIS.U1APSH.U1A04013";
        string pGl2 = "WHSIS.U2APSH.U2A04013";

        /// <summary>
        /// 获取ParaId及对应的数据表名。（已过滤）
        /// </summary>
        /// <returns></returns>
        public List<ParaTableInfo> GetInfo(out string errMsg)
        {
            this.init();
            errMsg = "";
            List<ParaTableInfo> infoList = new List<ParaTableInfo>();
            string sql = "select distinct(b.T_PARAID),c.T_DESC,c.T_OUTTABLE from T_BASE_CONSUMEPARA as b left join T_BASE_CALCPARA as c on b.T_PARAID=c.T_PARAID and b.T_UNITID=c.T_UNITID where c.I_CONSUMETYPE  is not null  and c.I_TARGETTYPE IS NOT NULL";

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

            if (dt!=null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!String.IsNullOrEmpty(dt.Rows[i]["T_OUTTABLE"].ToString()))
                    {
                        ParaTableInfo info = new ParaTableInfo();
                        info.ParaId = String.IsNullOrEmpty(dt.Rows[i]["T_PARAID"].ToString()) ? String.Empty : dt.Rows[i]["T_PARAID"].ToString();
                        info.ParaDesc = String.IsNullOrEmpty(dt.Rows[i]["T_DESC"].ToString()) ? String.Empty : dt.Rows[i]["T_DESC"].ToString();
                        info.OutTableName =dt.Rows[i]["T_OUTTABLE"].ToString();

                        infoList.Add(info);
                    }
                }

            }
            //去掉重复。
            //return infoList.Distinct(new EqualCompare<ParaTableInfo>((x, y) => (x != null && y != null) &&(x.OutTableName == y.OutTableName))).ToList();
            //不能过滤，因为会把ParaId过滤掉。以ParaId来取值的。
            return infoList;
        }


        /// <summary>
        /// 循环遍历ParaId,获取该ParaId的平均值（得到所有ParaId的值）
        /// </summary>
        /// <param name="AllParaId">所有ParaId</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<ConsumeInfo> GetInfos( string beginTime, string endTime, out string errMsg)
        {
            this.init();
            errMsg = "";
            //counts = 0;
            List<ConsumeInfo> cInfoList = new List<ConsumeInfo>();
            //获取outtable表
            List<ParaTableInfo> infoList = GetInfo(out errMsg);
            //获取所有ParaId
            //string ParaId = GetParaId(out counts,out errMsg);

            if (infoList.Count > 0)
            {
                foreach (var info in infoList)
                {
                    if (!string.IsNullOrEmpty(info.OutTableName))
                    {
                        ConsumeInfo pa = new ConsumeInfo();
                        string sql = "select avg(D_VALUE) as value from " + info.OutTableName + "  where T_PARAID='" + info.ParaId + "' ";

                        if (!String.IsNullOrEmpty(beginTime) && !String.IsNullOrEmpty(endTime))
                        {
                            sql += " and T_DATETIME between '" + beginTime + "' and '" + endTime + "'";

                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(beginTime))
                            {
                                sql += " and T_DATETIME>'" + beginTime + "'";
                            }
                            if (!String.IsNullOrEmpty(endTime))
                            {
                                sql += " and T_DATETIME<'" + endTime + "'";
                            }
                        }
                        DataTable dt = new DataTable();

                        if (rlDBType == "SQL")
                        {
                            // sql = "select ID_KEY,班次名,起始时间,结束时间 from 班次时间表 order by ID_KEY";
                            //dt = DBsql.RunDataTable(sql, out errMsg);
                        }
                        else
                        {
                            dt = DBdb2.RunDataTable(sql, out errMsg);
                        }
                        if (dt!=null)
                        {
                            pa.Name = info.ParaDesc;
                            pa.Count = string.IsNullOrEmpty(dt.Rows[0][0].ToString()) ? 0 : Convert.ToDouble(dt.Rows[0][0].ToString());
                            cInfoList.Add(pa);
                        }
                    }
                }
            }
            return cInfoList;
        }

        /// <summary>
        /// 获取所有的ParaId
        /// </summary>
        /// <param name="counts">返回ParaId总数，计算平均值使用</param>
        /// <returns></returns>
        public string GetParaId(out int counts,out string errMsg)
        {
            this.init();
            errMsg = "";
            counts = 0;
            string sql = "select T_PARAID from T_BASE_ALLCONSUMEPARA ";

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
            StringBuilder sb = new StringBuilder();

            counts = dt.Rows.Count;
            if (counts > 0)
            {
                for (int i = 0; i < counts; i++)
                {
                    sb.Append(dt.Rows[i]["T_PARAID"].ToString());
                    sb.Append(",");
                }
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();

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
