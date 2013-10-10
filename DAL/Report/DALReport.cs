using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.Helper;
using System.Data;
using SAC.DB2;
using Entity.Statistic;
using Entity.Report;

namespace DAL.Report
{
    /// <summary>
    /// 报表管理。
    /// </summary>
    public class DALReport
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
            string sql = "select b.T_PARAID,c.T_DESC,c.T_OUTTABLE,b.T_UNITID,p.T_PLANTDESC ,u.T_UNITDESC from T_BASE_CONSUMEPARA as b left join T_BASE_CALCPARA as c on b.T_PARAID=c.T_PARAID and b.T_UNITID=c.T_UNITID left join T_BASE_UNIT as u on b.T_UNITID=u.T_UNITID left join T_BASE_PLANT as p on  u.T_PLANTID=p.T_PLANTID where c.I_CONSUMETYPE  is not null  and c.I_TARGETTYPE IS NOT NULL";

           
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

            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!String.IsNullOrEmpty(dt.Rows[i]["T_OUTTABLE"].ToString()))
                    {
                        ParaTableInfo info = new ParaTableInfo();
                        info.ParaId = String.IsNullOrEmpty(dt.Rows[i]["T_PARAID"].ToString()) ? String.Empty : dt.Rows[i]["T_PARAID"].ToString();
                        info.ParaDesc = String.IsNullOrEmpty(dt.Rows[i]["T_DESC"].ToString()) ? String.Empty : dt.Rows[i]["T_DESC"].ToString();
                        info.OutTableName = dt.Rows[i]["T_OUTTABLE"].ToString();
                        info.UnitId = dt.Rows[i]["T_UNITID"].ToString();
                        info.UnitName = dt.Rows[i]["T_PLANTDESC"].ToString() + dt.Rows[i]["T_UNITDESC"].ToString();
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
        /// 获取集团ParaId及对应的数据表名。（已过滤）
        /// </summary>
        /// <returns></returns>
        public List<ParaTableInfo> GetCompanyInfo(out string errMsg)
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

            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!String.IsNullOrEmpty(dt.Rows[i]["T_OUTTABLE"].ToString()))
                    {
                        ParaTableInfo info = new ParaTableInfo();
                        info.ParaId = String.IsNullOrEmpty(dt.Rows[i]["T_PARAID"].ToString()) ? String.Empty : dt.Rows[i]["T_PARAID"].ToString();
                        info.ParaDesc = String.IsNullOrEmpty(dt.Rows[i]["T_DESC"].ToString()) ? String.Empty : dt.Rows[i]["T_DESC"].ToString();
                        info.OutTableName = dt.Rows[i]["T_OUTTABLE"].ToString();
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
        /// 循环遍历ParaId,获取该机组的ParaId的平均值（得到机组的ParaId的平均值）
        /// </summary>
        /// <param name="AllParaId">所有ParaId</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<ReportInfo> GetInfos(string beginTime, string endTime, out string errMsg)
        {
            this.init();
            errMsg = "";
            //counts = 0;
            List<ReportInfo> cInfoList = new List<ReportInfo>();
            //获取outtable表
            List<ParaTableInfo> infoList = GetInfo(out errMsg);
            //获取所有ParaId
            //string ParaId = GetParaId(out counts,out errMsg);

            if (infoList.Count > 0)
            {
                for (int i = 0; i < infoList.Count; i++)
                {
                    //foreach (var info in infoList)
                    //{
                    if (!string.IsNullOrEmpty(infoList[i].OutTableName))
                    {
                        ReportInfo pa = new ReportInfo();
                        string sql = "select T_UNITID, avg(D_VALUE) as value from " + infoList[i].OutTableName + "  where T_PARAID='" + infoList[i].ParaId + "' and T_UNITID='" + infoList[i].UnitId+ "' ";

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
                        sql += " group by T_UNITID";
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
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            pa.UnitName = infoList[i].UnitName;
                            pa.UnitId = infoList[i].UnitId;
                            pa.ParaDesc=infoList[i].ParaDesc;
                            pa.ParaValue = string.IsNullOrEmpty(dt.Rows[0]["value"].ToString()) ? 0 : Convert.ToDouble(dt.Rows[0]["value"].ToString());
                            cInfoList.Add(pa);
                        }
                    }
                    //}
                    ////删除已经计算过的机组(为什么:因为获取的outtable各个机组的都有 有重复的)
                    //List<ParaTableInfo> tmp = infoList.Where(infos => infos == infoList[i].ParaId).ToList();
                    //foreach (var tmpInfo in tmp)
                    //{
                    //    infoList.Remove(tmpInfo);
                    //}
                }
            }
            return cInfoList;
        }

        /// <summary>
        /// 循环遍历ParaId,获取集团的ParaId的平均值（得到集团的ParaId的平均值）
        /// </summary>
        /// <param name="AllParaId">所有ParaId</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<ReportInfo> GetCompanyInfos(string beginTime, string endTime, out string errMsg)
        {
            this.init();
            errMsg = "";
            //counts = 0;
            List<ReportInfo> cInfoList = new List<ReportInfo>();
            //获取outtable表
            List<ParaTableInfo> infoList = GetCompanyInfo(out errMsg);
            //获取所有ParaId
            //string ParaId = GetParaId(out counts,out errMsg);

            if (infoList.Count > 0)
            {
                for (int i = 0; i < infoList.Count; i++)
                {
                    //foreach (var info in infoList)
                    //{
                    if (!string.IsNullOrEmpty(infoList[i].OutTableName))
                    {
                        ReportInfo pa = new ReportInfo();
                        string sql = "select  avg(D_VALUE) as value from " + infoList[i].OutTableName + "  where T_PARAID='" + infoList[i].ParaId + "'  ";

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
                        //sql += " group by T_PARAID";
                        DataTable dt = new DataTable();

                        if (rlDBType == "SQL")
                        {
                            
                        }
                        else
                        {
                            dt = DBdb2.RunDataTable(sql, out errMsg);
                        }
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            pa.ParaDesc = infoList[i].ParaDesc;
                            pa.ParaValue = string.IsNullOrEmpty(dt.Rows[0]["value"].ToString()) ? 0 : Convert.ToDouble(dt.Rows[0]["value"].ToString());
                            cInfoList.Add(pa);
                        }
                    }
                    //}
                    //删除已经计算过的参数(为什么:因为获取的outtable各个机组的都有 有重复的)
                    //List<ParaTableInfo> tmp = infoList.Where(infos => infos.ParaId == infoList[i].ParaId).ToList();
                    //foreach (var tmpInfo in tmp)
                    //{
                    //    infoList.Remove(tmpInfo);
                    //}
                }
            }
            return cInfoList;
        }

        /// <summary>
        /// 循环遍历ParaId,获取机组的各个ParaId的平均值（得到集团的ParaId的平均值）
        /// </summary>
        /// <param name="AllParaId">所有ParaId</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<ReportInfo> GetUnitParaInfo(string beginTime, string endTime, out string errMsg)
        {
            this.init();
            errMsg = "";
            //counts = 0;
            List<ReportInfo> cInfoList = new List<ReportInfo>();
            //获取outtable表
            List<ParaTableInfo> infoList = GetInfo(out errMsg);
            //获取所有ParaId
            //string ParaId = GetParaId(out counts,out errMsg);

            if (infoList.Count > 0)
            {
                for (int i = 0; i < infoList.Count; i++)
                {
                    //foreach (var info in infoList)
                    //{
                    if (!string.IsNullOrEmpty(infoList[i].OutTableName))
                    {
                        ReportInfo pa = new ReportInfo();
                        string sql = "select T_PARAID,T_UNITID, avg(D_VALUE) as value from " + infoList[i].OutTableName + "  where T_PARAID='" + infoList[i].ParaId + "' and T_UNITID='"+infoList[i].UnitId+"'  ";

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
                        sql += " group by T_UNITID,T_PARAID";
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
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            pa.UnitName = infoList[i].UnitName;
                            pa.ParaDesc = infoList[i].ParaDesc;
                            pa.ParaValue = string.IsNullOrEmpty(dt.Rows[0]["value"].ToString()) ? 0 : Convert.ToDouble(dt.Rows[0]["value"].ToString());
                            cInfoList.Add(pa);
                        }
                    }
                }
            }
            return cInfoList;
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
