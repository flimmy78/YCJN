using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SAC.Helper;
using SAC.DB2;

namespace DAL
{
    public class DALBase
    {
        string rlDBType = "";
        string rtDBType = "";

        string pGl1 = "WHSIS.U1APSH.U1A04013";
        string pGl2 = "WHSIS.U2APSH.U2A04013";


        /// <summary>
        /// 获取公司信息。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetCompany(out string errMsg)
        {
            this.init();
            errMsg = "";

            //string sql1 = "select min(ID_KEY) as ID_KEY,UGROUP from WebUser group by UGROUP order by ID_KEY";
            string sql = "select * from T_BASE_COMPANY";

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
        /// 根据公司Id获取电厂信息。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetPlant(string companyId,out string errMsg)
        {
            this.init();
            errMsg = "";

            //string sql1 = "select min(ID_KEY) as ID_KEY,UGROUP from WebUser group by UGROUP order by ID_KEY";
            string sql = "select * from T_BASE_PLANT where T_COMPANYID='"+companyId+"'";

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
        /// 获取电厂Id获取机组信息。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetUnit(string plantId,out string errMsg)
        {
            this.init();
            errMsg = "";

            //string sql1 = "select min(ID_KEY) as ID_KEY,UGROUP from WebUser group by UGROUP order by ID_KEY";
            string sql = "select * from T_BASE_UNIT where T_PLANTID='"+plantId+"'";

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
        /// 获取锅炉信息。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetBoiler(out string errMsg)
        {
            this.init();
            errMsg = "";

            //string sql1 = "select min(ID_KEY) as ID_KEY,UGROUP from WebUser group by UGROUP order by ID_KEY";
            string sql = "select * from T_BASE_BOILER";

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
        /// 获取汽机信息。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetSteam(out string errMsg)
        {
            this.init();
            errMsg = "";

            //string sql1 = "select min(ID_KEY) as ID_KEY,UGROUP from WebUser group by UGROUP order by ID_KEY";
            string sql = "select * from T_BASE_STEAM";

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
