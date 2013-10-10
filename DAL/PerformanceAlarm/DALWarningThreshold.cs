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
///日期：2013-09-06
namespace DAL.PerformanceAlarm
{
    public class DALWarningThreshold
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

        public bool Delete_data(string para)
        {
            this.init();
            bool flag = false;
            string errMsg = "";
            string sql = "delete from 超温考核故障映射表 where ID_KEY="+para;
            if (rlDBType == "SQL")
            {

            }
            else
            {
                flag = DBdb2.RunNonQuery(sql, out errMsg);
            }
            return flag;
        }
        public bool Save_data(string para) {
            this.init();
            bool flag=false;
            string errMsg = "";
            string sql = "update 超温考核故障类型表 set 提示信息 = '" + para.Split(',')[2] + "',考核上限 =" + para.Split(',')[0] + ",  考核下限=" + para.Split(',')[1]+ " where 故障类型ID='"+ para.Split(',')[3]+"'";
            if (rlDBType == "SQL")
            {

            }
            else
            {
                flag  = DBdb2.RunNonQuery(sql, out errMsg);
            }
            return flag;
        }


        public void Insert_data(string para)
        {
            this.init();
            string errMsg = "";
            DataSet DS = new DataSet();
            string sql = "select * from  超温考核故障类型表 where 考核上限 =" + para.Split(',')[0] + " and 考核下限=" + para.Split(',')[1];
            if (rlDBType == "SQL")
            {

            }
            else
            {
                DS = DBdb2.RunDataSet(sql, out errMsg);
            }

            if (DS.Tables[0].Rows.Count > 0)
            {
                string sql1 = "update 超温考核故障类型表 set 提示信息 = '" + para.Split(',')[2] + "' where 考核上限 =" + para.Split(',')[0] + " and  考核下限=" + para.Split(',')[1];
                if (rlDBType == "SQL")
                {

                }
                else
                {
                    bool falg = DBdb2.RunNonQuery(sql1, out errMsg);

                }
                string sql2 = "insert into 超温考核故障映射表(考核点ID,故障类型ID) values('" + para.Split(',')[3] + "','" + DS.Tables[0].Rows[0]["故障类型ID"].ToString() + "')";
                bool falg1 = DBdb2.RunNonQuery(sql2, out errMsg);
            }
            else
            {
                string sql3="select 故障类型ID from 超温考核故障类型表 order by 故障类型ID desc  fetch first 1 rows only";
                DataSet DDS =new DataSet();
                if (rlDBType == "SQL")
                {

                }
                else
                {
                     DDS = DBdb2.RunDataSet(sql3, out errMsg);
                    
                }
                string str_sql3="";
                if(DDS.Tables[0].Rows.Count>0)
                {
                    str_sql3 =( Convert.ToInt32(DDS.Tables[0].Rows[0][0].ToString())+1).ToString();
                }
                else
                {
                    str_sql3 ="1";
                }
                string sql4 = "insert into 超温考核故障类型表(故障类型ID,考核下限,考核上限,过滤公式,公式参数,提示信息) values('" + str_sql3 + "'," + para.Split(',')[1] + "," + para.Split(',')[0] + ",'0','0','" + para.Split(',')[2] + "')";
                string sql5 = "insert into 超温考核故障映射表(考核点ID,故障类型ID) values('" + para.Split(',')[3] + "','" + str_sql3 + "')";

                if (rlDBType == "SQL")
                {

                }
                else
                {
                    bool falg2 = DBdb2.RunNonQuery(sql4, out errMsg);
                    bool falg3 = DBdb2.RunNonQuery(sql5, out errMsg);

                }
                
            }
        }

        /// <summary>
        /// 获取考核点描述
        /// </summary>
        public DataSet GETKAOHRDIAN_DESC(string uint_id)
        {
            this.init();
            string errMsg = "";
            DataSet DS = new DataSet();

            string sql = "select 考核点ID,考核点描述 from 超温考核测点表  where  超温考核测点表.机组 = '" + uint_id+"'";
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
            
            this.init();
            string errMsg = "";
            DataSet DS = new DataSet();

            string sql = "select 超温考核故障映射表.ID_KEY,超温考核测点表.考核点描述,超温考核故障类型表.考核下限, 超温考核故障类型表.考核上限,超温考核故障类型表.提示信息,超温考核测点表.机组,超温考核故障类型表.故障类型ID  from 超温考核故障映射表 inner join 超温考核测点表 " +
"on 超温考核测点表.考核点ID = 超温考核故障映射表.考核点ID and 超温考核测点表.机组 = '" + unit_id + "'  inner join 超温考核故障类型表 on 超温考核故障映射表.故障类型ID =  超温考核故障类型表.故障类型ID";
            if (rlDBType == "SQL")
            {

            }
            else
            {
                DS = DBdb2.RunDataSet(sql, out errMsg);
            }
            return DS;
        }
    }
}
