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
    public class DALSteamTurbine
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


        public DataSet GetSteamTurbineData(string unit)
        {
            this.init();
            string errMsg = "";

            DataSet DS = new DataSet();
            string sql = "select  D_L1, D_L2, D_Delta_dtr, D_Rdtr, D_L_well, D_W_well, D_Eta_p, D_Delta_fdj, D_Delta_gcb, D_Delta_qbb, D_DK_e, D_DB_e, D_DM_e, D_DL_e, D_P_e, D_D_gbmfgs,"+
                "D_D_gbmfhs,D_Dphp_e ,D_DN_e,D_Djc_e,D_A,I_N_pipe,I_N_flow,D_Din,D_Dout,I_N_ball_i,D_V_xb1I,D_V_xb2I,D_Z_xb1I,D_Z_xb2I,D_W_lqt_I,D_Txhs_in_d,D_Wd ,D_Din_xb1O,D_Din_xb2O,T_Type,I_N_ball_o,D_Z_xb1O,D_Z_xb2O,D_Eta_gr_xb1,D_Eta_gr_xb2 "+
                "from T_INFO_PROCPARA_TURB  where T_INFO_PROCPARA_TURB.T_UNITID ='" + unit + "'   order by  T_INFO_PROCPARA_TURB.T_TIME desc  fetch first 1 rows only";

            if (rlDBType == "SQL")
            {

            }
            else
            {
                DS = DBdb2.RunDataSet(sql, out errMsg);
            }
            return DS;
        }


        public int InsertSteamTurbineData(string para)
        {
            this.init();
            string errMsg = "";
            int num = 0;
            string sql = "insert into T_INFO_PROCPARA_TURB  (T_UNITID,D_L1, D_L2, D_Delta_dtr, D_Rdtr, D_L_well, D_W_well, D_Eta_p, D_Delta_fdj, D_Delta_gcb, D_Delta_qbb, D_DK_e, D_DB_e, D_DM_e, D_DL_e, D_P_e, D_D_gbmfgs," +
                "D_D_gbmfhs,D_Dphp_e ,D_DN_e,D_Djc_e,D_A,I_N_pipe,I_N_flow,D_Din,D_Dout,I_N_ball_i,D_V_xb1I,D_V_xb2I,D_Z_xb1I,D_Z_xb2I,D_W_lqt_I,D_Txhs_in_d,D_Wd ,D_Din_xb1O,D_Din_xb2O,T_Type,I_N_ball_o,D_Z_xb1O,D_Z_xb2O,D_Eta_gr_xb1,D_Eta_gr_xb2,T_TIME)  values (" + para + ")";
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
