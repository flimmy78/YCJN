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
///日期：2013-08-23

namespace DAL
{
    public class DALBoiler
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


        public DataSet GetBoilerData(string unit)
        {
            this.init();
            string errMsg = "";
            DataSet DS = new DataSet();
            string sql = "select D_Alpha_fh,D_Alpha_lz,D_D_e,D_CO,D_Tlz,D_Tlk_d,D_RH,D_Tfw_d,D_H2,D_CH4 from T_INFO_PROCPARA_BOILER where T_INFO_PROCPARA_BOILER.T_UNITID ='" + unit + "'   order by  T_INFO_PROCPARA_BOILER.T_TIME desc  fetch first 1 rows only";

            if (rlDBType == "SQL")
            {

            }
            else
            {
                DS = DBdb2.RunDataSet(sql, out errMsg);
            }
            return DS;
        }


        public int InsertBoilerData(string para)
        {
            this.init();
            string errMsg = "";
            int num = 0;
            string sql = "insert into T_INFO_PROCPARA_BOILER  (T_UNITID,D_Alpha_fh,D_Alpha_lz,D_D_e,D_CO,D_Tlz,D_Tlk_d,D_RH,D_Tfw_d,D_H2,D_CH4,T_TIME)  values (" + para + ")";
            if (rlDBType == "SQL")
            {

            }
            else
            {
                num = DBdb2.RunRowCount(sql, out errMsg);
            }
            return num;
        }
    }
}
