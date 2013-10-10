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
///日期：2013-09-26
namespace DAL
{
    public class DALBenchmarkReference
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
        /// <summary>
        /// 获取队标基准值公式
        /// </summary>
        /// <param name="unit">机组编号</param>
        /// <returns></returns>
        public DataSet GetBoilerData(string unit)
        {
            this.init();
            string errMsg = "";
            DataSet DS = new DataSet();
            
            //"T0_t_el主蒸汽温度(℃),"P0_t_el主蒸汽压力(MPa)",,'Trh_el再热温度(℃)','PLrh_el再热压损(%)','Pdp_el凝汽器压力(kPa)','Dgrjw_el过热减温水流量(t/h)','Dzrjw_el再热减温水流量(t/h)','Dpw_el锅炉连续排污流量(t/h)','DeltaT_gl_el凝汽器过冷度(℃)','Dtur_el小机用汽量(t/h)','O2_el排烟氧量(%)','Tpy_el排烟温度(℃)','Alpha_bs_el补水率(%)','Tfw_el给水温度(℃)','Eta_H_el高压缸效率(%)','Eta_M_el中压缸效率(%)','Theta_1_el#1高加上端差(℃)','Theta_2_el#2高加上端差(℃)','Theta_3_el#3高加上端差(℃)','Theta_5_el#5高加上端差(℃)','Theta_6_el#6高加上端差(℃)','Theta_7_el#7高加上端差(℃)','Theta_8_el#8高加上端差(℃)'

            string sql = "select T_PARAID,T_FORMULA from T_BASE_CALCPARA where T_UNITID = '" + unit + "' and ( T_PARAID= 'T0_t_el_B' or T_PARAID=  'P0_t_el_B' or T_PARAID=  'Trh_el_B' or T_PARAID=  'PLrh_el_B'" +
                "or T_PARAID=  'Pdp_el_B' or T_PARAID=  'Dgrjw_el_B' or T_PARAID=  'Dzrjw_el_B' or T_PARAID=  'Dpw_el_B' or T_PARAID=  'Del_BtaT_gl_el_B' or T_PARAID=  'Dtur_el_B'"+
                "or T_PARAID=  'O2_el_B' or T_PARAID=  'Tpy_el_B' or T_PARAID=  'Alpha_bs_el_B' or T_PARAID=  'Tfw_el_B' or T_PARAID=  'Eta_H_el_B' or T_PARAID=  'Eta_M_el_B' or "+
                "T_PARAID=  'Theta_1_el_B' or T_PARAID=  'Theta_2_el_B' or T_PARAID=  'Theta_3_el_B' or T_PARAID=  'Theta_5_el_B' or T_PARAID=  'Theta_6_el_B' or "+
                "T_PARAID=  'Theta_7_el_B' or T_PARAID=  'Theta_8_el_B') ";
            
            if (rlDBType == "SQL")
            {

            }
            else
            {
                DS = DBdb2.RunDataSet(sql, out errMsg);
            }
            return DS;
        }

        public bool updateBoiler(string unit)
        {
            this.init();
            string errMsg = "";
            bool flag = false;


             

            //"T0_t_el主蒸汽温度(℃),"P0_t_el主蒸汽压力(MPa)",,'Trh_el再热温度(℃)','PLrh_el再热压损(%)','Pdp_el凝汽器压力(kPa)','Dgrjw_el过热减温水流量(t/h)','Dzrjw_el再热减温水流量(t/h)','Dpw_el锅炉连续排污流量(t/h)','DeltaT_gl_el凝汽器过冷度(℃)','Dtur_el小机用汽量(t/h)','O2_el排烟氧量(%)','Tpy_el排烟温度(℃)','Alpha_bs_el补水率(%)','Tfw_el给水温度(℃)','Eta_H_el高压缸效率(%)','Eta_M_el中压缸效率(%)','Theta_1_el#1高加上端差(℃)','Theta_2_el#2高加上端差(℃)','Theta_3_el#3高加上端差(℃)','Theta_5_el#5高加上端差(℃)','Theta_6_el#6高加上端差(℃)','Theta_7_el#7高加上端差(℃)','Theta_8_el#8高加上端差(℃)'
            string[] str = new string[23]{"T0_t_el_B","P0_t_el_B","Trh_el_B","PLrh_el_B","Pdp_el_B","Dgrjw_el_B","Dzrjw_el_B","Dpw_el_B","Del_BtaT_gl_el_B","Dtur_el_B","O2_el_B","Tpy_el_B","Alpha_bs_el_B","Tfw_el_B","Eta_H_el_B","Eta_M_el_B","Theta_1_el_B","Theta_2_el_B","Theta_3_el_B","Theta_5_el_B","Theta_6_el_B","Theta_7_el_B","Theta_8_el_B"};
            for (int i = 0; i < unit.Split(',')[1].Split('|').Length; i++)
            {

                string sql = "update T_BASE_CALCPARA set T_FORMULA ='" + unit.Split(',')[1].Split('|')[i] + "' where T_UNITID = '" + unit.Split(',')[0] + "' and T_PARAID ='"+str[i]+"'";
                if (rlDBType == "SQL")
                {

                }
                else
                {
                    flag = DBdb2.RunNonQuery(sql, out errMsg);
                }
            }



            return flag;
        }
    }
}
