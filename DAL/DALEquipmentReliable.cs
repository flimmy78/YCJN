using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SAC.Helper;
using SAC.DB2;
using SAC;
using System.Collections;
using DAO;
using SAC.Entity;

namespace DAL
{
    public class DALEquipmentReliable
    {
        string rlDBType = "";
        string rtDBType = "";

        string pGl1 = "WHSIS.U1APSH.U1A04013";
        string pGl2 = "WHSIS.U2APSH.U2A04013";

        /// <summary>
        /// 根据条件获取数据,设备可靠性监视。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetInitByCondition(string beginTime, string endTime,int sCount,int eCount, out int count, out string errMsg)
        {
            this.init();
            errMsg = "";
            count = 0;
            //string sql1 = "select min(ID_KEY) as ID_KEY,UGROUP from WebUser group by UGROUP order by ID_KEY";
            string sql = "SELECT I.ID_KEY,T_PLANTDESC,T_UNITDESC,D_CAPABILITY,T_BEGINTIME,T_ENDTIME,I_PH,T_PROFESSIONALDESC,T_REASONDESC,  rownumber() over(order by  I.ID_KEY asc ) as rowid  FROM T_INFO_UNIT AS I LEFT JOIN T_BASE_UNIT AS B ON  I.T_CODE=B.T_UNITID LEFT JOIN T_BASE_PLANT AS P ON P.T_PLANTID=B.T_PLANTID LEFT JOIN T_BASE_COMPANY  AS C ON  P.T_COMPANYID= C.T_COMPANYID LEFT JOIN T_BASE_FAULTPROFESSIONAL AS BF ON I.T_FPROFEESIOID=BF.T_PROFESSIONALID  LEFT JOIN T_BASE_FAULTREASON  AS BR ON I.T_FREASONID=BR.T_REASONID   WHERE 1=1";

            DataTable dt = null;

            if (!string.IsNullOrEmpty(beginTime))
            {
                sql += " AND  I.T_BEGINTIME>'" + beginTime + " 00:00:00.000" + "'";
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                sql += " AND I.T_ENDTIME<'" + endTime + " 00:00:00.000" + "'";
            }

            
            string sqlStr = "select * from (" + sql + ") as a ";
            if (eCount != 0)
            {
                sqlStr += "where a.rowid between " + sCount + " and " + eCount + "";
            }
            if (rlDBType == "SQL")
            {
                // sql = "select ID_KEY,班次名,起始时间,结束时间 from 班次时间表 order by ID_KEY";
                //dt = DBsql.RunDataTable(sql, out errMsg);
            }
            else
            {
                dt = DBdb2.RunDataTable(sqlStr, out errMsg);
                count = DBdb2.RunDataTable(sql, out errMsg).Rows.Count;
            }
            return dt;
        }


        /// <summary>
        /// 根据条件获取数据，可靠性过程参数。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetInitByCondition(string companyId, string plantId, string unitId, string beginTime, string endTime,int sCount,int eCount,out int count, out string errMsg)
        {
            this.init();
            errMsg = "";
            count = 0;

            string sql = "SELECT P.T_PLANTDESC,T_CODE,T_UNITDESC,D_CAPABILITY,I_UTH,D_EAF,D_FOF,D_FOR,D_UOF,D_UOR,I_GAAG,I_PH,I_AH,I_SH,I_UOH,I_FOH,I_EUNDH,  rownumber() over(order by  I.ID_KEY asc ) as rowid  FROM T_INFO_UNIT AS I LEFT JOIN T_BASE_UNIT AS B ON  I.T_CODE=B.T_UNITID LEFT JOIN T_BASE_PLANT AS P ON P.T_PLANTID=B.T_PLANTID LEFT JOIN T_BASE_COMPANY  AS C ON  P.T_COMPANYID= C.T_COMPANYID WHERE 1=1";
            if ((!string.IsNullOrEmpty(companyId))&&companyId!="0")
            {
                sql += " AND C.T_COMPANYID='" + companyId + "'";
            }

            if ((!string.IsNullOrEmpty(plantId))&&plantId!="0")
            {
                sql += " AND P.T_PLANTID='" + plantId + "'";
            }

            if ((!string.IsNullOrEmpty(unitId))&&unitId!="0")
            {
                sql += " AND B.T_UNITID='" + unitId + "'";
            }

            if (!string.IsNullOrEmpty(beginTime))
            {
                sql += " AND  I.T_TIME>='" +  beginTime +"-01'";
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                sql += " AND I.T_TIME<='" + endTime + "-01'";
            }

            string sqlStr = "select * from (" + sql + ") as a ";
            if (eCount != 0)
            {
                sqlStr += "where a.rowid between " + sCount + " and " + eCount + "";
            }
            DataTable dt = null;

            if (rlDBType == "SQL")
            {
                // sql = "select ID_KEY,班次名,起始时间,结束时间 from 班次时间表 order by ID_KEY";
                //dt = DBsql.RunDataTable(sql, out errMsg);
            }
            else
            {
                dt = DBdb2.RunDataTable(sqlStr, out errMsg);
                count = DBdb2.RunDataTable(sql, out errMsg).Rows.Count;
            }
            return dt;
        }

        ///// <summary>
        ///// 根据条件获取数据。
        ///// </summary>
        ///// <param name="errMsg"></param>
        ///// <returns></returns>
        //public DataTable GetInitByCondition(string companyId, string plantId, string unitId, string beginTime, string endTime, int sCount, int eCount, out int count, out string errMsg)
        //{
        //    this.init();
        //    errMsg = "";
        //    count = 0;

        //    string sql = "SELECT T_CODE,T_UNITDESC,D_CAPABILITY,I_UTH,D_EAF,D_FOF,D_FOR,D_UOF,D_UOR,I_GAAG,I_PH,I_AH,I_SH,I_UOH,I_FOH,I_EUNDH,  rownumber() over(order by  I.ID_KEY asc ) as rowid  FROM T_INFO_UNIT AS I LEFT JOIN T_BASE_UNIT AS B ON  I.T_CODE=B.T_UNITID LEFT JOIN T_BASE_PLANT AS P ON P.T_PLANTID=B.T_PLANTID LEFT JOIN T_BASE_COMPANY  AS C ON  P.T_COMPANYID= C.T_COMPANYID WHERE 1=1";
        //    if ((!string.IsNullOrEmpty(companyId)) && companyId != "0")
        //    {
        //        sql += " AND C.T_COMPANYID='" + companyId + "'";
        //    }

        //    if ((!string.IsNullOrEmpty(plantId)) && plantId != "0")
        //    {
        //        sql += " AND P.T_PLANTID='" + plantId + "'";
        //    }

        //    if ((!string.IsNullOrEmpty(unitId)) && unitId != "0")
        //    {
        //        sql += " AND B.T_UNITID='" + unitId + "'";
        //    }

        //    if (!string.IsNullOrEmpty(beginTime))
        //    {
        //        sql += " AND  I.BEGINTIME>='" + beginTime + " 00:00:00.000" + "'";
        //    }
        //    if (!string.IsNullOrEmpty(endTime))
        //    {
        //        sql += " AND I.ENDTIME<='" + endTime + " 00:00:00.000" + "'";
        //    }

        //    string sqlStr = "select * from (" + sql + ") as a where a.rowid between " + sCount + " and " + eCount + "";
        //    DataTable dt = null;

        //    if (rlDBType == "SQL")
        //    {
        //        // sql = "select ID_KEY,班次名,起始时间,结束时间 from 班次时间表 order by ID_KEY";
        //        //dt = DBsql.RunDataTable(sql, out errMsg);
        //    }
        //    else
        //    {
        //        dt = DBdb2.RunDataTable(sqlStr, out errMsg);
        //        count = DBdb2.RunDataTable(sql, out errMsg).Rows.Count;
        //    }
        //    return dt;
        //}

        /// <summary>
        /// 根据条件获取数据，可靠性分析。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetInitByCondition(string time, out string errMsg)
        {
            this.init();
            errMsg = "";
            //string sql1 = "select min(ID_KEY) as ID_KEY,UGROUP from WebUser group by UGROUP order by ID_KEY";
            //string sql = "SELECT I_FOT,I.ID_KEY,T_PLANTDESC,T_UNITDESC,D_CAPABILITY,T_BEGINTIME,T_ENDTIME,I_PH,T_PROFESSIONALDESC,T_REASONDESC,  rownumber() over(order by  I.ID_KEY asc ) as rowid  FROM T_INFO_UNIT AS I LEFT JOIN T_BASE_UNIT AS B ON  I.T_CODE=B.T_UNITID LEFT JOIN T_BASE_PLANT AS P ON P.T_PLANTID=B.T_PLANTID LEFT JOIN T_BASE_COMPANY  AS C ON  P.T_COMPANYID= C.T_COMPANYID LEFT JOIN T_BASE_FAULTPROFESSIONAL AS BF ON I.T_FPROFEESIOID=BF.T_PROFESSIONALID  LEFT JOIN T_BASE_FAULTREASON  AS BR ON I.T_FREASONID=BR.T_REASONID   WHERE 1=1";
            string sql = "SELECT SUM(I_FOT) AS fot,SUM(I_FOH) AS foh  FROM T_INFO_UNIT WHERE 1=1";

            DataTable dt = null;

            if (!string.IsNullOrEmpty(time))
            {
                sql += " AND  T_TIME='" + time+ "'";
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
       /// 根据条件获取数据，可靠性分析一月开始。
       /// </summary>
       /// <param name="sTime">起始时间</param>
       /// <param name="eTime">结束时间</param>
       /// <param name="errMsg"></param>
       /// <returns></returns>
        public DataTable GetInitByCondition(string sTime, string eTime,out string errMsg)
        {
            this.init();
            errMsg = "";
            string sql = "SELECT SUM(I_FOT) AS fot ,SUM(I_FOH) AS foh FROM T_INFO_UNIT  WHERE 1=1";

            DataTable dt = null;

            if (!string.IsNullOrEmpty(sTime) && !string.IsNullOrEmpty(eTime))
            {
                sql += " AND  T_TIME bettwen '" + sTime + "' and '"+eTime+"'";
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
        /// 根据条件获取数据，可靠性分析(强迫停运次数分析（按容量分类）)。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetInitByCapality(string time, out string errMsg)
        {
            this.init();
            errMsg = "";
            string sql = "SELECT SUM(I_FOT) AS FOH ,B.D_CAPABILITY  FROM T_INFO_UNIT AS I LEFT JOIN T_BASE_UNIT AS  B ON B.T_UNITID=I.T_CODE ";

            DataTable dt = null;

            if (!string.IsNullOrEmpty(time))
            {
                sql += " WHERE  I.T_TIME='" + time + "'";
            }

            sql += "  GROUP BY B.D_CAPABILITY  ";
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
        /// 根据条件获取数据，可靠性分析(强迫停运次数分析（按专业分类）)。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetInitByProfession(string time, out string errMsg)
        {
            this.init();
            errMsg = "";
            string sql = "SELECT SUM(I_FOT) AS FOH ,P.T_PROFESSIONALDESC  FROM T_INFO_UNIT AS I LEFT JOIN T_BASE_FAULTPROFESSIONAL AS  P ON P.T_PROFESSIONALID=I.T_FPROFEESIOID ";

            DataTable dt = null;

            if (!string.IsNullOrEmpty(time))
            {
                sql += " WHERE  I.T_TIME='" + time + "'";
            }

            sql += "  GROUP BY P.T_PROFESSIONALDESC  ";
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
        /// 根据条件获取数据，可靠性分析(强迫停运次数分析（按故障原因分类）)。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetInitByReason(string time, out string errMsg)
        {
            this.init();
            errMsg = "";
            string sql = "SELECT SUM(I_FOT) AS FOH ,R.T_REASONDESC  FROM T_INFO_UNIT AS I LEFT JOIN T_BASE_FAULTREASON AS  R ON R.T_REASONID=I.T_FREASONID ";

            DataTable dt = null;

            if (!string.IsNullOrEmpty(time))
            {
                sql += " WHERE  I.T_TIME='" + time + "'";
            }

            sql += "  GROUP BY R.T_REASONDESC ";
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
        /// 获取故障类别信息。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetFaultCategory(out string errMsg)
        {
            this.init();
            errMsg = "";

            //string sql1 = "select min(ID_KEY) as ID_KEY,UGROUP from WebUser group by UGROUP order by ID_KEY";
            string sql = "select * from T_BASE_FAULTCATEGORY";

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
        /// 获取故障性质信息。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetFaultProperty(out string errMsg)
        {
            this.init();
            errMsg = "";

            //string sql1 = "select min(ID_KEY) as ID_KEY,UGROUP from WebUser group by UGROUP order by ID_KEY";
            string sql = "select * from T_BASE_FAULTPROPERTY";

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
        /// 获取故障专业分类信息。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetFaultProfessional(out string errMsg)
        {
            this.init();
            errMsg = "";

            //string sql1 = "select min(ID_KEY) as ID_KEY,UGROUP from WebUser group by UGROUP order by ID_KEY";
            string sql = "select * from T_BASE_FAULTPROFESSIONAL";

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
        /// 获取故障故障原因分类信息。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetFaultReason(out string errMsg)
        {
            this.init();
            errMsg = "";

            //string sql1 = "select min(ID_KEY) as ID_KEY,UGROUP from WebUser group by UGROUP order by ID_KEY";
            string sql = "select * from T_BASE_FAULTREASON";

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
        /// 根据唯一ID获取数据。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetUnitById(string Id, out string errMsg)
        {
            this.init();
            errMsg = "";

            //string sql1 = "select min(ID_KEY) as ID_KEY,UGROUP from WebUser group by UGROUP order by ID_KEY";
            string sql = "select T_PLANTDESC,T_UNITDESC,I_PH,T_FCATEGORYID,T_FPROPERTYID,T_FPROFEESIOID,T_FREASONID,T_EVENTDESC,T_REASONANALYSE,T_DEALCONDITION from T_INFO_UNIT as i left join T_BASE_UNIT as b on i.T_CODE=b.T_UNITID left join T_BASE_PLANT as p on p.T_PLANTID=b.T_PLANTID where i.ID_KEY=" + Id + " order by i.ID_KEY";

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
        /// 根据机组Id更新机组部分信息。
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="professionalId"></param>
        /// <param name="reasonId"></param>
        /// <param name="eventDesc"></param>
        /// <param name="reasonAnalyse"></param>
        /// <param name="dealCondition"></param>
        /// <returns></returns>
        public bool UpdateUnit(string unitId,string professionalId,string reasonId,string eventDesc,string reasonAnalyse,string dealCondition,out string errMsg)
        {
            this.init();
            errMsg = "";
            bool flag = false;
            //string sql1 = "select min(ID_KEY) as ID_KEY,UGROUP from WebUser group by UGROUP order by ID_KEY";
            string sql = "update T_INFO_UNIT set T_FPROFEESIOID='" + professionalId + "',T_FREASONID='" + reasonId + "',T_EVENTDESC='" + eventDesc + "',T_REASONANALYSE='" + reasonAnalyse + "',T_DEALCONDITION='"+dealCondition+"' where ID_KEY="+unitId+"";

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
        /// 将机组信息从EXCEL中插入到数据库中。
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="professionalId"></param>
        /// <param name="reasonId"></param>
        /// <param name="eventDesc"></param>
        /// <param name="reasonAnalyse"></param>
        /// <param name="dealCondition"></param>
        /// <returns></returns>
        public bool InsertUnit(List<UnitInfo> unitList, out string errMsg)
        {
            errMsg = string.Empty;
            ArrayList sqlList = new ArrayList();
            foreach (var info in unitList)
            {
                string sql = "insert into T_INFO_UNIT(T_CODE,T_TIME,I_PH,I_GAAG,I_PUMPPOWER,I_UTH,I_SH,I_RH,I_AH,I_POH,I_BPOH,I_SPOH,I_HPPOH,I_IACTH,I_FOH,I_UOH,I_ERFDH,I_EFDH,I_EUNDH,I_RT,I_SST,I_UST,I_POT,I_BPOT,I_SPOT,I_HPOT,I_IACTT,I_FOT,I_UOT,I_FUOT,I_LOSEPOWER,D_SF,D_AF,D_UF,D_EAF,D_POF,D_UOF,D_FOF,D_UDF,D_OF,D_GCF,D_UTF,D_FOR,D_EFOR,D_UOR,D_SR,D_EXR,D_FOOR,S_MTBS,S_CAH,S_MTBF,S_MTTPO,S_MTTUO,D_MPOD,D_MUOD,S_BMTTPO,S_SMTTPO,D_BMPOD,D_SMPOD,D_KWZJ,I_GEH,I_PWH,I_GMH,I_PMH,I_GET,I_PWT,I_GMT,I_PMT,I_MOE,I_TOE,I_FMOE,I_CMOE,I_MOET,I_TOET,I_FMOET,I_CMOET) values('" + info.T_CODE + "','" + info.T_TIME + "'," + info.I_PH + "," + info.I_GAAG + "," + info.I_PUMPPOWER + "," + info.I_UTH + "," + info.I_SH + "  ," + info.I_RH + "  ," + info.I_AH + " ," + info.I_POH + "  ," + info.I_BPOH + " ," + info.I_SPOH + "," + info.I_HPPOH + "," + info.I_IACTH + "," + info.I_FOH + "," + info.I_UOH + "," + info.I_ERFDH + "," + info.I_EFDH + "," + info.I_EUNDH + " ," + info.I_RT + "," + info.I_SST + "," + info.I_UST + "," + info.I_POT + "," + info.I_BPOT + "," + info.I_SPOT + "," + info.I_HPOT + "," + info.I_IACTT + "," + info.I_FOT + "," + info.I_UOT + "," + info.I_FUOT + "," + info.I_LOSEPOWER + "," + info.D_SF + "," + info.D_AF + "," + info.D_UF + "," + info.D_EAF + "," + info.D_POF + "," + info.D_UOF + "," + info.D_FOF + "," + info.D_UDF + "," + info.D_OF + "," + info.D_GCF + " ," + info.D_UTF + "," + info.D_FOR + "," + info.D_EFOR + "," + info.D_UOR + "," + info.D_SR + "," + info.D_EXR + "," + info.D_FOOR + ",'" + info.S_MTBS + "','" + info.S_CAH + "','" + info.S_MTBF + "','" + info.S_MTTPO + "','" + info.S_MTTUO + "'," + info.D_MPOD + "," + info.D_MUOD + ",'" + info.S_BMTTPO + "','" + info.S_SMTTPO + "'," + info.D_BMPOD + "," + info.D_SMPOD + "," + info.D_KWZJ + "," + info.I_GEH + "," + info.I_PWH + "," + info.I_GMH + "," + info.I_PMH + "," + info.I_GET + "," + info.I_PWT + "," + info.I_GMT + "," + info.I_PMT + "," + info.I_MOE + "," + info.I_TOE + "," + info.I_FMOE + "," + info.I_CMOE + "," + info.I_MOET + "," + info.I_TOET + "," + info.I_FMOET + "," + info.I_CMOET + ")";
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