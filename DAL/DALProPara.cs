using System;
using SAC.DB2;
using SAC.Plink;
using SAC.Elink;
using SAC.Helper;
using System.Text;
using System.Data;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


///创建者：刘海杰
///日期：2013-08-20
namespace DAL
{
    public class DALProPara
    {
        string rtDBType = "";   //实时数据库
        string rlDBType = "";   //关系数据库
        Elink ek = new Elink();
        Plink pk = new Plink();

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <returns></returns>
        private void init()
        {
            rtDBType = IniHelper.ReadIniData("RTDB", "DBType", null);
            rlDBType = IniHelper.ReadIniData("RelationDBbase", "DBType", null);
        }


        public DataSet GetProductionData(string unit,string num)
        {
            this.init();
            string errMsg = "";
            DataSet DS = new DataSet();
            string sql = "";
            if (num == "1")
            {
                sql = "SELECT  D_C_ar,D_H_ar,D_O_ar,D_N_ar,D_S_ar,D_Cfh_c_ulti,D_Clz_c_ulti,D_Qnet_ar_ulti,D_M_AR_ULTI,D_A_AR_ULTI   from T_INFO_PROCPARA_COAL_ULTI " +
                    "where T_INFO_PROCPARA_COAL_ULTI.T_UNITID ='" + unit + "' order by T_INFO_PROCPARA_COAL_ULTI.T_TIME   desc  fetch first 1 rows only";

            }
            else
            {
                sql = "SELECT  D_CFH_C_PROX,D_CLZ_C_PROX,D_M_AD,D_A_AD,D_V_DAF,D_QNET_AR_PROX,D_ST_AD,D_ST_AR,D_M_AR_PROX,D_A_AR_PROX  from T_INFO_PROCPARA_COAL_PROX " +
                    " where T_INFO_PROCPARA_COAL_PROX.T_UNITID ='" + unit + "'   order by  T_INFO_PROCPARA_COAL_PROX.T_TIME desc  fetch first 1 rows only";
            }
            
            if (rlDBType == "SQL")
            {

            }
            else
            {
                DS = DBdb2.RunDataSet(sql, out errMsg);
            }
            return DS;
        }

        /// <summary>
        /// 元素
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public int InsertProductionElementData(string para)
        {
            this.init();
            string errMsg = "";
            int num = 0;
            string sql = "insert into T_INFO_PROCPARA_COAL_ULTI  (T_UNITID,D_C_ar,D_H_ar,D_O_ar,D_N_ar,D_S_ar,D_Cfh_c_ulti,D_Clz_c_ulti,D_Qnet_ar_ulti,D_M_AR_ULTI,D_A_AR_ULTI,T_TIME)  values (" + para + ")";
            if (rlDBType == "SQL")
            {

            }
            else
            {
                num = DBdb2.RunRowCount(sql, out errMsg);
            }
            return num;
        }

        /// <summary>
        /// 工业
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public int InsertProductionIndustryData(string para)
        {
            this.init();
            string errMsg = "";
            int num = 0;
            string sql = "insert into T_INFO_PROCPARA_COAL_PROX  (T_UNITID,D_CFH_C_PROX,D_CLZ_C_PROX,D_M_AD,D_A_AD,D_V_DAF,D_QNET_AR_PROX,D_ST_AD,D_ST_AR,D_M_AR_PROX,D_A_AR_PROX,T_TIME)  values (" + para + ")";
            if (rlDBType == "SQL")
            {

            }
            else
            {
                num = DBdb2.RunRowCount(sql, out errMsg);
            }
            return num;
        }

        /// <summary>
        /// 煤质查询
        /// </summary>
        /// <param name="unit">机组编号</param>
        /// <returns></returns>
        public DataSet GetProductionPreData(string stime,string etime,string unit)
        {
            this.init();
            string errMsg = "";
            DataSet DS = new DataSet();
            string sql = "SELECT  T_UNITID,T_TIME,D_M_AR_PROX,D_M_ad,D_A_ad,D_V_DAF,D_Qnet_ar_PROX,D_St_ad FROM T_INFO_PROCPARA_COAL_PROX  where T_UNITID ='" + unit + "' and T_TIME between '" + stime + "' and '" + etime + "'  order by T_TIME asc";
            if (rlDBType == "SQL")
            {

            }
            else
            {
                DS = DBdb2.RunDataSet(sql, out errMsg);
            }
            return DS;
        }


        public bool InsertExcelData(List<Entity.ProPara.ProductionProPara> dataList, out string errMsg)
        {
            errMsg = string.Empty;
            ArrayList sqlList = new ArrayList();
            foreach (var info in dataList)
            {
                string sql = "";
                for (int i = 0; i < info.T_UNITID.Split('、').Length; i++)
                {

                    sql = "insert into T_INFO_PROCPARA_COAL_PROX(T_TIME,D_M_AR_PROX,D_M_AD,D_A_AD,D_V_DAF,D_A_AR_PROX,D_QNET_AR_PROX,D_ST_AD,D_ST_AR,D_CFH_C_PROX,D_CLZ_C_PROX,T_UNITID) VALUES('"+info.T_TIME+"',"+info.D_M_AR_PROX+","+info.D_M_AD+","+info.D_A_AD+","+info.D_V_DAF+","+info.D_A_AR_PROX+","+info.D_QNET_AR_PROX+","+info.D_ST_AD+","+info.D_ST_AR+","+info.D_CFH_C_PROX+","+info.D_CLZ_C_PROX+",'"+info.T_UNITID.Split('、')[i]+"')";
                    sqlList.Add(sql);
                }
                    
                
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
    }
}
