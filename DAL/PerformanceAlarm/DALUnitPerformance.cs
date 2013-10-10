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
///日期：2013-09-04

namespace DAL.PerformanceAlarm
{
    public class DALUnitPerformance
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
        /// 获取预警类别 T_BASE_FAULTCATEGORY
        /// </summary>
        /// <returns></returns>
        public DataSet GetFAULTCATEGORY()
        {
            this.init();
            string errMsg = "";
            DataSet DS = new DataSet();
            string sql = "select T_CATEGORYID,T_CATEGORYDESC from T_BASE_FAULTCATEGORY";
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
        /// 获取预警性质 T_BASE_FAULTPROPERTY
        /// </summary>
        /// <returns></returns>
        public DataSet GetFAULTPROPERTY()
        {
            this.init();
            string errMsg = "";
            DataSet DS = new DataSet();
            string sql = "select T_PROPERTYID,T_PROPERTYDESC from T_BASE_FAULTPROPERTY";
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
        /// 获取预警专业分类 T_BASE_FAULTPROFESSIONAL
        /// </summary>
        /// <returns></returns>
        public DataSet GetFAULTPROFESSIONAL()
        {
            this.init();
            string errMsg = "";
            DataSet DS = new DataSet();
            string sql = "select T_PROFESSIONALID,T_PROFESSIONALDESC from T_BASE_FAULTPROFESSIONAL";
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
        /// 获取预警原因分类 T_BASE_FAULTREASON
        /// </summary>
        /// <returns></returns>
        public DataSet GetFAULTREASON()
        {
            this.init();
            string errMsg = "";
            DataSet DS = new DataSet();
            string sql = "select T_REASONID,T_REASONDESC from T_BASE_FAULTREASON";
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
        /// 获取datagridview的值
        /// </summary>
        /// <param name="unit_id"></param>
        /// <returns></returns>
        public DataSet Get_GRID_DATA(string unit_id)
        {
            string[] str = new string[unit_id.Split(',').Length];
            for (int i = 0; i < unit_id.Split(',').Length; i++)
            {
                str[i] = unit_id.Split(',')[i];
            }
            string sql_str = "";
            if (str[2] != "")
            {
                sql_str = "and 超温考核记录表.影响电量 = " + str[2] + "";
            }
            this.init();
            string errMsg = "";
            DataSet DS = new DataSet();
            string sql_num = "";
            if ((Convert.ToDateTime(str[0].Split(';')[1]).Month == DateTime.Now.Month) && (Convert.ToDateTime(str[0].Split(';')[1]).Year == DateTime.Now.Year))
            {
                sql_num = DateTime.Now.Day.ToString();

            }
            else
            {
                sql_num = DateTime.DaysInMonth(Convert.ToDateTime(str[0].Split(';')[1]).Year, Convert.ToDateTime(str[0].Split(';')[1]).Month).ToString();
            }
            string sql = "select  超温考核记录表.ID_KEY,T_COMPANYDESC,T_PLANTDESC,T_UNITDESC,T_DESC,开始时间, 结束时间,T_CATEGORYDESC,T_PROPERTYDESC,T_PROFESSIONALDESC,T_REASONDESC,影响电量,T_CAPABILITYLEVEL,超温考核记录表.事件描述,超温考核记录表.原因分析,超温考核记录表.处理建议 from " +
 " T_BASE_UNIT inner join T_BASE_PLANT   on T_BASE_UNIT.T_PLANTID = T_BASE_PLANT.T_PLANTID and  T_BASE_UNIT.T_UNITID= '" + str[1] + "' " +
 "inner join T_BASE_COMPANY  on T_BASE_PLANT.T_COMPANYID = T_BASE_COMPANY.T_COMPANYID " +
"inner join  超温考核记录表  on   超温考核记录表.机组 =T_BASE_UNIT.T_UNITID  and  超温考核记录表.开始时间  between '" + str[0].Split(';')[0] + "-01 00:00:00' and  '" + str[0].Split(';')[1] + "-" + sql_num + " 00:00:00' " +

"and 超温考核记录表.预警类别ID ='" + str[3] + "'" + sql_str + " and 超温考核记录表.预警性质ID ='" + str[4] + "' and 超温考核记录表.预警专业分类ID ='" + str[5] + "' and 超温考核记录表.预警原因分类ID ='" + str[6] + "' " +
" inner join T_BASE_CALCPARA on 超温考核记录表.考核点ID = T_BASE_CALCPARA.T_PARAID and " +
"超温考核记录表.机组 = T_BASE_CALCPARA.T_UNITID " +
"inner join T_BASE_FAULTCATEGORY on 超温考核记录表.预警类别ID  = T_BASE_FAULTCATEGORY.T_CATEGORYID " +
"inner join T_BASE_FAULTPROPERTY on T_BASE_FAULTPROPERTY.T_PROPERTYID  = 超温考核记录表.预警性质ID " +
"inner join T_BASE_FAULTPROFESSIONAL on T_BASE_FAULTPROFESSIONAL.T_PROFESSIONALID= 超温考核记录表.预警专业分类ID " +
"inner join T_BASE_FAULTREASON on T_BASE_FAULTREASON.T_REASONID = 超温考核记录表.预警原因分类ID";
            if (rlDBType == "SQL")
            {

            }
            else
            {
                DS = DBdb2.RunDataSet(sql, out errMsg);
            }
            return DS;
        }


        public DataSet Get_data(string para)
        {
            this.init();
            string errMsg = "";
            DataSet DS = new DataSet();
            string sql = "select 开始时间,事件描述,原因分析,处理建议 from 超温考核记录表 where ID_KEY=" + para;
            if (rlDBType == "SQL")
            {

            }
            else
            {
                DS = DBdb2.RunDataSet(sql, out errMsg);
            }
            return DS;
        }

        public bool Edit_data(string para)
        {
            this.init();
            bool flag = false;
            string errMsg = "";
            string sql = "update 超温考核记录表 set 事件描述='" + para.Split(',')[1] + "',原因分析='" + para.Split(',')[2] + "',处理建议='" + para.Split(',')[3] + "'  where ID_KEY=" + para.Split(',')[0];
            if (rlDBType == "SQL")
            {

            }
            else
            {
                flag = DBdb2.RunNonQuery(sql, out errMsg);
            }
            return flag;
        }
    }
}
