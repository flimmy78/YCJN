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
///日期：2013-08-30
namespace DAL.StatisticalComparison
{
    public class DALHorizontalComparison
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
        /// 获取所有机组容量等级 T_BASE_UNIT
        /// </summary>
        /// <returns></returns>
        public DataSet GetCAPABILITYLEVEL()
        {
            this.init();
            string errMsg = "";
            DataSet DS = new DataSet();
            string sql = "select T_CAPABILITYLEVEL from T_BASE_UNIT group by  T_CAPABILITYLEVEL";
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
        /// 获取所有机组类型 T_BASE_UNIT
        /// </summary>
        /// <returns></returns>
        public DataSet GetPLANTTYPE()
        {
            this.init();
            string errMsg = "";
            DataSet DS = new DataSet();
            string sql = "select T_PLANTTYPE from T_BASE_UNIT group by  T_PLANTTYPE";
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
        /// 获取锅炉厂家 T_BASE_BOILER
        /// </summary>
        /// <returns></returns>
        public DataSet GetBOILERDESC()
        {
            this.init();
            string errMsg = "";
            DataSet DS = new DataSet();
            string sql = "select T_BOILERID,T_BOILERDESC from T_BASE_BOILER";
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
        /// 获取汽轮机厂家 T_BASE_STEAM
        /// </summary>
        /// <returns></returns>
        public DataSet GetSTEAMDESC()
        {
            this.init();
            string errMsg = "";
            DataSet DS = new DataSet();
            string sql = "select T_STEAMID,T_STEAMDESC from T_BASE_STEAM";
            if (rlDBType == "SQL")
            {

            }
            else
            {
                DS = DBdb2.RunDataSet(sql, out errMsg);
            }
            return DS;
        }


        public IList<Hashtable> GetChartData(string para_id)
        {
            this.init();
            string errMsg = "";
            DataSet DS = new DataSet();
            DataSet DDS = new DataSet();
            IList<Hashtable> listdata = new List<Hashtable>();
            string[] para = new string[0]; ;
            string sql = "select T_UNITID,T_UNITDESC  from T_BASE_UNIT where T_CAPABILITYLEVEL ='" + para_id.Split(',')[0] + "' and T_PLANTTYPE='" + para_id.Split(',')[1] + "'  and T_BOILERID='" + para_id.Split(',')[2] + "' and T_STEAMID ='" + para_id.Split(',')[3] + "'";
            if (rlDBType == "SQL")
            {

            }
            else
            {
                DS = DBdb2.RunDataSet(sql, out errMsg);
                para = new string[DS.Tables[0].Rows.Count];
            }
            if (DS.Tables[0].Rows.Count > 0)
            {
                string[] str = new string[DS.Tables[0].Rows.Count];
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    para[i] = DS.Tables[0].Rows[i][1].ToString();
                    string sql_Required = "select T_PARAID,T_OUTTABLE from T_BASE_CALCPARA where T_UNITID ='" + DS.Tables[0].Rows[i][0].ToString() + "' and (T_PARAID ='Pel' or T_PARAID='P0_t_el_B' or T_PARAID='P0_t' or T_PARAID='T0_t' or T_PARAID='T0_t_el_B' or T_PARAID='"+para_id.Trim(',').Split(',')[6]+"')";
                    if (rlDBType == "SQL")
                    {

                    }
                    else
                    {
                        DDS = DBdb2.RunDataSet(sql_Required, out errMsg);
                    }

                    string str_pel = "", str_P0_t = "", str_T0_t = "", append_sql="";
                    if (DDS.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < DDS.Tables[0].Rows.Count; j++)
                        {
                            string out_table = "";
                            if (DDS.Tables[0].Rows[j]["T_OUTTABLE"].ToString() == "")
                            {
                                out_table = "T_INFO_CALCDATA";
                            }
                            else
                            {
                                out_table = DDS.Tables[0].Rows[j]["T_OUTTABLE"].ToString();
                            }
                            if (DDS.Tables[0].Rows[j]["T_PARAID"].ToString() == "Pel")
                            {
                                str_pel = "(select D_VALUE,a.D_CAPABILITY,T_DATETIME  from " + out_table + ", (select T_UNITID,\"D_CAPABILITY\" from T_BASE_UNIT where T_UNITID ='" + DS.Tables[0].Rows[i][0].ToString() + "') as a where  a.T_UNITID = T_INFO_CALCDATA.T_UNITID  and   T_INFO_CALCDATA.T_PARAID = 'Pel' ) as s ";

                            }
                            else if ((DDS.Tables[0].Rows[j]["T_PARAID"].ToString() == "P0_t") || (DDS.Tables[0].Rows[j]["T_PARAID"].ToString() == "P0_t_el_B"))
                            {
                                str_P0_t = ",(select " + out_table + ".D_VALUE,b.D_VALUE as b_D_VALUE, " + out_table + ".T_DATETIME  from  " + out_table + " inner join (select D_VALUE,T_DATETIME  from  " + out_table + " where  T_PARAID  ='P0_t_el_B') as b " +
                                    "on  " + out_table + ".T_DATETIME = b.T_DATETIME and " + out_table + ".T_PARAID ='P0_t' and T_UNITID ='" + DS.Tables[0].Rows[i][0].ToString() + "' ) as ss ";

                            }
                            else if ((DDS.Tables[0].Rows[j]["T_PARAID"].ToString() == "T0_t") || (DDS.Tables[0].Rows[j]["T_PARAID"].ToString() == "T0_t_el_B"))
                            {
                                str_T0_t = ",(select " + out_table + ".D_VALUE,c.D_VALUE as c_D_VALUE," + out_table + ".T_DATETIME  from  " + out_table + " inner join (select D_VALUE ,T_DATETIME from  " + out_table + " where  T_PARAID  ='T0_t_el_B') as c" +
                                   " on " + out_table + ".T_DATETIME = c.T_DATETIME and " + out_table + ".T_PARAID ='T0_t' and T_UNITID ='" + DS.Tables[0].Rows[i][0].ToString() + "' ) as sss ";


                            }
                            else
                            {

                                append_sql = ",(select " + out_table + ".D_VALUE," + out_table + ".T_DATETIME from " + out_table + "  where T_PARAID='" + DDS.Tables[0].Rows[j]["T_PARAID"].ToString() + "' and T_UNITID='" + DS.Tables[0].Rows[i][0].ToString() + "') as  ssss ";


                            }
                        }
                        string sql_num = "";
                        if ((Convert.ToDateTime(para_id.Trim(',').Split(',')[5].Split(';')[1]).Month == DateTime.Now.Month) && (Convert.ToDateTime(para_id.Trim(',').Split(',')[5].Split(';')[1]).Year == DateTime.Now.Year))
                        {
                            sql_num = DateTime.Now.Day.ToString();

                        }
                        else
                        {
                            sql_num = DateTime.DaysInMonth(Convert.ToDateTime(para_id.Trim(',').Split(',')[5].Split(';')[1]).Year, Convert.ToDateTime(para_id.Trim(',').Split(',')[5].Split(';')[1]).Month).ToString();
                        }
                        string str_sql = "select s.D_VALUE,s.D_CAPABILITY,ss.D_VALUE,ss.b_D_VALUE,sss.D_VALUE,sss.c_D_VALUE,ssss.D_VALUE,s.T_DATETIME from " + str_pel + str_P0_t + str_T0_t + append_sql + "where  s.T_DATETIME = ss.T_DATETIME and   s.T_DATETIME = sss.T_DATETIME  and  s.T_DATETIME = ssss.T_DATETIME   and  s.T_DATETIME between '" + para_id.Trim(',').Split(',')[5].Split(';')[0] + "-01 00:00:00' and '" + para_id.Trim(',').Split(',')[5].Split(';')[1] + "-" + sql_num + " 00:00:00'  order by s.T_DATETIME asc ";


                        Hashtable ht = new Hashtable();

                        if (rlDBType == "SQL")
                        {

                        }
                        else
                        {
                            ht = new Hashtable();
                            ht.Add("name", para[i]);

                            ArrayList ld = new ArrayList();
                            ArrayList lt = new ArrayList();

                            DataSet  DDDDS = DBdb2.RunDataSet(str_sql, out errMsg);
                            double data_sql = 0; int num = 0;
                            if (DDDDS.Tables[0].Rows.Count > 0)
                            {
                                for (int j = 0; j < DDDDS.Tables[0].Rows.Count; j++)
                                {//(Convert.ToInt32(para_id.Trim(',').Split(',')[4]) + 5) * 0.01 
                                    if ((Convert.ToDouble(DDDDS.Tables[0].Rows[j][0].ToString()) / Convert.ToDouble(DDDDS.Tables[0].Rows[j][1].ToString()) > ((Convert.ToInt32(para_id.Trim(',').Split(',')[4]) - 5) * 0.01)) && (Convert.ToDouble(DDDDS.Tables[0].Rows[j][0].ToString()) / Convert.ToDouble(DDDDS.Tables[0].Rows[j][1].ToString()) < ((Convert.ToInt32(para_id.Trim(',').Split(',')[4]) + 5) * 0.01)) && (Math.Abs(Convert.ToDouble(DDDDS.Tables[0].Rows[j][2].ToString()) - Convert.ToDouble(DDDDS.Tables[0].Rows[j][3].ToString())) / Convert.ToDouble(DDDDS.Tables[0].Rows[j][3].ToString()) < 0.05) && (Math.Abs(Convert.ToDouble(DDDDS.Tables[0].Rows[j][4].ToString()) - Convert.ToDouble(DDDDS.Tables[0].Rows[j][5].ToString())) / Convert.ToDouble(DDDDS.Tables[0].Rows[j][5].ToString()) < 0.05))
                                    {
                                        num++;
                                        data_sql += Convert.ToDouble(DDDDS.Tables[0].Rows[j][6]);
                                    }
                                }
                            }
                            ld = new ArrayList();
                            ld.Add(para[i]);
                            if (data_sql == 0)
                            {
                                ld.Add(data_sql);
                            }
                            else
                            { ld.Add(data_sql / num); }
                            
                            lt.Add(ld);
                            ht.Add("data", lt);
                            listdata.Add(ht);
                        }
                    }
                }
            }

            return listdata;
        }
    }
}
