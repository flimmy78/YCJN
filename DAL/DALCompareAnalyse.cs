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
///日期：2013-07-05
namespace DAL
{
    public class DALCompareAnalyse
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

        public static int DateTimeToUTC(DateTime DT)
        {
            long a = new DateTime(1970, 1, 1, 0, 0, 0, 0).Ticks;
            int rtnInt = 0;
            rtnInt = (int)((DT.Ticks - 8 * 3600 * 1e7 - a) / 1e7);
            return rtnInt;
        }

        public IList<Hashtable> Get_Required_data(string unit_id, string[] para_id, string per, string stime, string etime, out string errMsg,out string max_data,out string min_data)
        {
            this.init();
            errMsg = ""; max_data = ""; min_data = "";
            ArrayList list = new ArrayList();
            string str_pel = "", str_P0_t = "", str_T0_t = "";// str_b_g = "", str_q_fd = "", str_Eta_H = "", str_Eta_M = "", str_Rho = "", str_Eta_b = ""
            DataSet DS = new DataSet();
            //SELECT T_PARAID,T_OUTTABLE FROM ADMINISTRATOR.T_BASE_CALCPARA where T_UNITID ='GZTZ-01' and (T_PARAID ='Pel' or T_PARAID='P0_t_el_B' or T_PARAID='P0_t' or T_PARAID='T0_t' or T_PARAID='T0_t_el_B')
            string sql_Required = "select T_PARAID,T_DESC,T_OUTTABLE from T_BASE_CALCPARA where T_UNITID ='" + unit_id + "' and (T_PARAID ='Pel' or T_PARAID='P0_t_el_B' or T_PARAID='P0_t' or T_PARAID='T0_t' or T_PARAID='T0_t_el_B'";
            for (int i = 0; i < para_id.Length; i++)
            {
                sql_Required += " or T_PARAID='" + para_id[i].ToString() + "' ";
                
            }
            sql_Required += ")";
            if (rlDBType == "SQL")
            {
                
            }
            else
            {
                DS = DBdb2.RunDataSet(sql_Required, out errMsg);
            }
            
            string[] append_sql = new string[DS.Tables[0].Rows.Count]; //呈现曲线sql语句
            
            for(int i=0;i<DS.Tables[0].Rows.Count;i++)
            {
                string out_table = "";
                if (DS.Tables[0].Rows[i]["T_OUTTABLE"].ToString() == "")
                {
                    out_table = "T_INFO_CALCDATA";
                }
                else
                {
                    out_table = DS.Tables[0].Rows[i]["T_OUTTABLE"].ToString();
                }

                if(DS.Tables[0].Rows[i]["T_PARAID"].ToString() =="Pel")
                {
                    str_pel = "(select D_VALUE,a.D_CAPABILITY,T_DATETIME  from " + out_table + ", (select T_UNITID,\"D_CAPABILITY\" from T_BASE_UNIT where T_UNITID ='" + unit_id + "') as a where  a.T_UNITID = T_INFO_CALCDATA.T_UNITID  and   T_INFO_CALCDATA.T_PARAID = 'Pel' ) as s ";
                }
                else if ((DS.Tables[0].Rows[i]["T_PARAID"].ToString() == "P0_t") || (DS.Tables[0].Rows[i]["T_PARAID"].ToString() == "P0_t_el_B"))
                {
                    str_P0_t = ",(select " + out_table + ".D_VALUE,b.D_VALUE as b_D_VALUE, " + out_table + ".T_DATETIME  from  " + out_table + " inner join (select D_VALUE,T_DATETIME  from  " + out_table + " where  T_PARAID  ='P0_t_el_B') as b " +
                                     "on  " + out_table + ".T_DATETIME = b.T_DATETIME and " + out_table + ".T_PARAID ='P0_t' and T_UNITID ='" + unit_id + "' ) as ss ";

                }
                else if ((DS.Tables[0].Rows[i]["T_PARAID"].ToString() == "T0_t") || (DS.Tables[0].Rows[i]["T_PARAID"].ToString() == "T0_t_el_B"))
                {
                    str_T0_t = ",(select " + out_table + ".D_VALUE,c.D_VALUE as c_D_VALUE," + out_table + ".T_DATETIME  from  " + out_table + " inner join (select D_VALUE ,T_DATETIME from  " + out_table + " where  T_PARAID  ='T0_t_el_B') as c" +
                                   " on " + out_table + ".T_DATETIME = c.T_DATETIME and " + out_table + ".T_PARAID ='T0_t' and T_UNITID ='" + unit_id + "' ) as sss ";

                   }
                else 
                {
                    for (int j = 0; j < para_id.Length;j++ )
                    {
                        if (para_id[j] == DS.Tables[0].Rows[i]["T_PARAID"].ToString())
                        {
                            //append_sql[j] = "select " + DS.Tables[0].Rows[i]["T_OUTTABLE"].ToString() + ".D_VALUE," + DS.Tables[0].Rows[i]["T_OUTTABLE"].ToString() + ".T_DATETIME  from " + DS.Tables[0].Rows[i]["T_OUTTABLE"].ToString() + " | where T_PARAID='" + DS.Tables[0].Rows[i]["T_PARAID"].ToString() + "' and T_UNITID='" + unit_id + "' |" + DS.Tables[0].Rows[i]["T_OUTTABLE"].ToString();
                            append_sql[j] = ",(select " + out_table + ".D_VALUE," + out_table + ".T_DATETIME from " + out_table + "  where T_PARAID='" + DS.Tables[0].Rows[i]["T_PARAID"].ToString() + "' and T_UNITID='" + unit_id + "') as  ssss |" + DS.Tables[0].Rows[i]["T_DESC"].ToString();

                            break;
                        }
                    }
                    
                }
            }
            //string sql = "select s.D_VALUE,s.T_DATETIME from "+str_pel+str_P0_t+str_T0_t;
            string str_sql = "";

             str_sql = "select s.D_VALUE,s.D_CAPABILITY,ss.D_VALUE,ss.b_D_VALUE,sss.D_VALUE,sss.c_D_VALUE,s.T_DATETIME from " + str_pel + str_P0_t + str_T0_t  + "where  s.T_DATETIME = ss.T_DATETIME and   s.T_DATETIME = sss.T_DATETIME   and  s.T_DATETIME between '" +stime + "' and '" +etime+ "'  order by s.T_DATETIME asc ";

            IList<Hashtable> listdata = new List<Hashtable>();

            Hashtable ht = new Hashtable();
            for (int i = 0; i < para_id.Length; i++)
            {
                if (para_id[i].ToString() == "Pel")
                {
                    ht = new Hashtable();
                    ht.Add("name", "机组负荷");
                    ht.Add("yAxis", i);
                    ArrayList ld = new ArrayList();
                    ArrayList lt = new ArrayList();
                    DS = DBdb2.RunDataSet(str_sql, out errMsg);
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                       double max_data1 = Convert.ToDouble(DS.Tables[0].Rows[0][0].ToString());
                       double min_data1 = Convert.ToDouble(DS.Tables[0].Rows[0][0].ToString());
                        for (int j = 0; j < DS.Tables[0].Rows.Count; j++)
                        {

                            if ((DS.Tables[0].Rows[j][0].ToString() != "- 9999") && (Convert.ToDouble(DS.Tables[0].Rows[j][0].ToString()) / Convert.ToDouble(DS.Tables[0].Rows[j][1].ToString()) > (Convert.ToDouble(per.Split('|')[0]))) && (Convert.ToDouble(DS.Tables[0].Rows[j][0].ToString()) / Convert.ToDouble(DS.Tables[0].Rows[j][1].ToString()) < (Convert.ToDouble(per.Split('|')[1]))) && (Math.Abs(Convert.ToDouble(DS.Tables[0].Rows[j][2].ToString()) - Convert.ToDouble(DS.Tables[0].Rows[j][3].ToString())) / Convert.ToDouble(DS.Tables[0].Rows[j][3].ToString()) < 0.05) && (Math.Abs(Convert.ToDouble(DS.Tables[0].Rows[j][4].ToString()) - Convert.ToDouble(DS.Tables[0].Rows[j][5].ToString())) / Convert.ToDouble(DS.Tables[0].Rows[j][5].ToString()) < 0.05))
                            {
                                ld = new ArrayList();

                                ld.Add(DateTimeToUTC(DateTime.Parse(DS.Tables[0].Rows[j][6].ToString())));

                                ld.Add(Math.Round(Convert.ToDouble(DS.Tables[0].Rows[j][0].ToString()),3));

                                lt.Add(ld);

                                if (max_data1 < Convert.ToDouble(DS.Tables[0].Rows[j][0].ToString()))
                                {
                                    max_data1 = Convert.ToDouble(DS.Tables[0].Rows[j][0].ToString());
                                }
                                if (min_data1 > Convert.ToDouble(DS.Tables[0].Rows[j][0].ToString()))
                                {
                                    min_data1 = Convert.ToDouble(DS.Tables[0].Rows[j][0].ToString());
                                }
                            }
                        }
                        max_data = max_data1.ToString();
                        min_data = min_data1.ToString();
                    }
                    ht.Add("data", lt);
                    listdata.Add(ht);
                }
            }



                for (int i = 0; i < append_sql.Length; i++)
                {
                    if (append_sql[i] != null)
                    {
                        str_sql = "select s.D_VALUE,s.D_CAPABILITY,ss.D_VALUE,ss.b_D_VALUE,sss.D_VALUE,sss.c_D_VALUE,ssss.D_VALUE,s.T_DATETIME from " + str_pel + str_P0_t + str_T0_t + append_sql[i].Split('|')[0] + "where  s.T_DATETIME = ss.T_DATETIME and   s.T_DATETIME = sss.T_DATETIME  and  s.T_DATETIME = ssss.T_DATETIME and  s.T_DATETIME between '" + stime + "' and '" + etime + "'  order by s.T_DATETIME asc ";

                        
                        if (rlDBType == "SQL")
                        {

                        }
                        else
                        {
                            ht = new Hashtable();
                            if (i<para_id.Length)
                            {
                                ht.Add("name", append_sql[i].Split('|')[1]);

                            //ht.Add("step", "left");
                            ht.Add("yAxis", i);
                            }
                            
                            ArrayList ld = new ArrayList();
                            ArrayList lt = new ArrayList();

                            DS = DBdb2.RunDataSet(str_sql, out errMsg);
                            
                            if (DS.Tables[0].Rows.Count > 0)
                            {
                                double max_data1 = Convert.ToDouble(DS.Tables[0].Rows[0][6].ToString());
                                double min_data1 = Convert.ToDouble(DS.Tables[0].Rows[0][6].ToString());
                                for (int j = 0; j < DS.Tables[0].Rows.Count; j++)
                                {
                                    if ((DS.Tables[0].Rows[j][6].ToString() != "- 9999") && (Convert.ToDouble(DS.Tables[0].Rows[j][0].ToString()) / Convert.ToDouble(DS.Tables[0].Rows[j][1].ToString()) > (Convert.ToDouble(per.Split('|')[0]))) && (Convert.ToDouble(DS.Tables[0].Rows[j][0].ToString()) / Convert.ToDouble(DS.Tables[0].Rows[j][1].ToString()) < (Convert.ToDouble(per.Split('|')[1]))) && (Math.Abs(Convert.ToDouble(DS.Tables[0].Rows[j][2].ToString()) - Convert.ToDouble(DS.Tables[0].Rows[j][3].ToString())) / Convert.ToDouble(DS.Tables[0].Rows[j][3].ToString()) < 0.05) && (Math.Abs(Convert.ToDouble(DS.Tables[0].Rows[j][4].ToString()) - Convert.ToDouble(DS.Tables[0].Rows[j][5].ToString())) / Convert.ToDouble(DS.Tables[0].Rows[j][5].ToString()) < 0.05))
                                    {
                                        ld = new ArrayList();

                                        ld.Add(DateTimeToUTC(DateTime.Parse(DS.Tables[0].Rows[j][7].ToString())));

                                        ld.Add(Convert.ToDouble(DS.Tables[0].Rows[j][6].ToString()));

                                        lt.Add(ld);

                                        if (max_data1 < Convert.ToDouble(DS.Tables[0].Rows[j][6].ToString()))
                                        {
                                            max_data1 = Convert.ToDouble(DS.Tables[0].Rows[j][6].ToString());
                                        }
                                        if (min_data1 > Convert.ToDouble(DS.Tables[0].Rows[j][6].ToString()))
                                        {
                                            min_data1 = Convert.ToDouble(DS.Tables[0].Rows[j][6].ToString());
                                        }
                                    }
                                }
                                max_data += max_data1.ToString() + ",";
                                min_data += min_data1.ToString() + ",";
                            }
                            ht.Add("data", lt);
                            listdata.Add(ht);

                        }
                    }
                }
                return listdata;
            //return str_append;
        }

        public IList<Hashtable> Get_All_data(string unit_id, string[] para_id, string per, string stime, string etime, out string errMsg)
        {
            this.init();
            ArrayList list = new ArrayList();
            string str_pel = "", str_P0_t = "", str_T0_t = "";
            DataSet DS = new DataSet();
            string sql_Required = "select T_PARAID,T_OUTTABLE from T_BASE_CALCPARA where T_UNITID ='" + unit_id + "' and (T_PARAID ='Pel' or T_PARAID='P0_t_el_B' or T_PARAID='P0_t' or T_PARAID='T0_t' or T_PARAID='T0_t_el_B'";
            for (int i = 0; i < para_id.Length; i++)
            {
                sql_Required += " or T_PARAID='" + para_id[i].ToString() + "' ";

            }
            sql_Required += ")";
            if (rlDBType == "SQL")
            {

            }
            else
            {
                DS = DBdb2.RunDataSet(sql_Required, out errMsg);
            }

            string[] append_sql = new string[DS.Tables[0].Rows.Count]; //呈现曲线sql语句
            int num_para = 0,num_add=0;
            string para_pin="";
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                string out_table = "";
                if (DS.Tables[0].Rows[i]["T_OUTTABLE"].ToString() == "")
                {
                    out_table = "T_INFO_CALCDATA";
                }
                else
                {
                    out_table = DS.Tables[0].Rows[i]["T_OUTTABLE"].ToString();
                }

                if (DS.Tables[0].Rows[i]["T_PARAID"].ToString() == "Pel")
                {
                    str_pel = "(select D_VALUE,a.D_CAPABILITY,T_DATETIME  from " + out_table + ", (select T_UNITID,\"D_CAPABILITY\" from T_BASE_UNIT where T_UNITID ='" + unit_id + "') as a where  a.T_UNITID = T_INFO_CALCDATA.T_UNITID  and   T_INFO_CALCDATA.T_PARAID = 'Pel' ) as s ";
                }
                else if ((DS.Tables[0].Rows[i]["T_PARAID"].ToString() == "P0_t") || (DS.Tables[0].Rows[i]["T_PARAID"].ToString() == "P0_t_el_B"))
                {
                    str_P0_t = ",(select " + out_table + ".D_VALUE,b.D_VALUE as b_D_VALUE, " + out_table + ".T_DATETIME  from  " + out_table + " inner join (select D_VALUE,T_DATETIME  from  " + out_table + " where  T_PARAID  ='P0_t_el_B') as b " +
                                     "on  " + out_table + ".T_DATETIME = b.T_DATETIME and " + out_table + ".T_PARAID ='P0_t' and T_UNITID ='" + unit_id + "' ) as ss ";

                }
                else if ((DS.Tables[0].Rows[i]["T_PARAID"].ToString() == "T0_t") || (DS.Tables[0].Rows[i]["T_PARAID"].ToString() == "T0_t_el_B"))
                {
                    str_T0_t = ",(select " + out_table + ".D_VALUE,c.D_VALUE as c_D_VALUE," + out_table + ".T_DATETIME  from  " + out_table + " inner join (select D_VALUE ,T_DATETIME from  " + out_table + " where  T_PARAID  ='T0_t_el_B') as c" +
                                   " on " + out_table + ".T_DATETIME = c.T_DATETIME and " + out_table + ".T_PARAID ='T0_t' and T_UNITID ='" + unit_id + "' ) as sss ";

                }
                else
                {
                    para_pin += DS.Tables[0].Rows[i]["T_PARAID"].ToString() + ".D_VALUE  as " + DS.Tables[0].Rows[i]["T_PARAID"].ToString() + ",";
                   
                        append_sql[num_add] = "select " + DS.Tables[0].Rows[i]["T_OUTTABLE"].ToString() + ".D_VALUE," + DS.Tables[0].Rows[i]["T_OUTTABLE"].ToString() + ".T_DATETIME  from " + out_table + "  where T_PARAID='" + DS.Tables[0].Rows[i]["T_PARAID"].ToString() + "' and T_UNITID='" + unit_id + "' |" + DS.Tables[0].Rows[i]["T_PARAID"].ToString();
                        num_add++;
                }
            }
            string sql =  str_pel + str_P0_t + str_T0_t;

            IList<Hashtable> listdata = new List<Hashtable>();
            int num_pin = 0;
            string sql_pin = "",str_sql = "",str_where="";

            Hashtable ht = new Hashtable();
            for (int i = 0; i < append_sql.Length; i++)
            {
                if (append_sql[i] != null)
                {
                    //if (num_pin==0)
                    //{
                    //    //sql_pin = "select " + append_sql[i].Split('|')[2].ToString() + ".D_VALUE as " + append_sql[i].Split('|')[3].ToString() + ",q.D_VALUE as Pel," + append_sql[i].Split('|')[2].ToString() + ".T_DATETIME as T_DATETIME from " + append_sql[i].Split('|')[2].ToString() + " inner join (" + sql + ") as q on " + append_sql[i].Split('|')[2].ToString() + ".T_DATETIME =q.T_DATETIME" + append_sql[i].Split('|')[1].ToString();
                    //    //num_pin++;
                    //}
                    //else
                    //{
                        str_sql += "(" + append_sql[i].Split('|')[0].ToString() + ") as " + append_sql[i].Split('|')[1].ToString() + ",";
                        if (i != append_sql.Length-1)
                        {
                            str_where += "s.T_DATETIME=" + append_sql[i].Split('|')[1].ToString() + ".T_DATETIME and ";
                        }
                        else
                        {
                            str_where += "s.T_DATETIME=" + append_sql[i].Split('|')[1].ToString() + ".T_DATETIME ";
                        }
                    //}
                }
            }
            if (str_where!="")
            {
                string sql_str = "select s.D_VALUE as Pel,s.D_CAPABILITY,ss.D_VALUE,ss.b_D_VALUE,sss.D_VALUE,sss.c_D_VALUE,Eta_b.D_VALUE as Eta_b, Eta_M.D_VALUE as Eta_M,q_fd.D_VALUE as q_fd,Eta_H.D_VALUE as Eta_H,b_g.D_VALUE as b_g, Rho.D_VALUE as Rho,s.T_DATETIME  as T_DATETIME from " + sql + " ," + str_sql.TrimEnd(',') + " where s.T_DATETIME = ss.T_DATETIME and   s.T_DATETIME = sss.T_DATETIME   and  " + str_where.Remove(str_where.Length - 5, 5) + " and s.T_DATETIME between '" + stime + "' and '" + etime + "'  order by s.T_DATETIME asc ";
                DS = DBdb2.RunDataSet(sql_str, out errMsg);
                foreach (DataRow row in DS.Tables[0].Rows)
                {
                    if ((row["Pel"].ToString() != "-9999") && (row["Eta_b"].ToString() != "-9999") && (row["Eta_M"].ToString() != "-9999") && (row["q_fd"].ToString() != "-9999") && (row["b_g"].ToString() != "-9999") && (row["Eta_b"].ToString() != "-9999") && (row["Rho"].ToString() != "-9999") && (Convert.ToDouble(row["Pel"].ToString()) / Convert.ToDouble(row[1].ToString()) > (Convert.ToDouble(per.Split('|')[0]))) && (Convert.ToDouble(row[0].ToString()) / Convert.ToDouble(row[1].ToString()) < (Convert.ToDouble(per.Split('|')[1]))) && (Math.Abs(Convert.ToDouble(row[2].ToString()) - Convert.ToDouble(row[3].ToString())) / Convert.ToDouble(row[3].ToString()) < 0.05) && (Math.Abs(Convert.ToDouble(row[4].ToString()) - Convert.ToDouble(row[5].ToString())) / Convert.ToDouble(row[5].ToString()) < 0.05))
                    {
                        ht = new Hashtable();
                        ht.Add("Pel", Math.Round(Convert.ToDouble(row["Pel"].ToString()),3));
                        ht.Add("Eta_b", Math.Round(Convert.ToDouble(row["Eta_b"].ToString()),3));
                        ht.Add("Eta_M",Math.Round(Convert.ToDouble(row["Eta_M"].ToString()),3));
                        ht.Add("q_fd",Math.Round(Convert.ToDouble(row["q_fd"].ToString()),3));
                        ht.Add("b_g",Math.Round(Convert.ToDouble(row["b_g"].ToString()),3));
                        ht.Add("Eta_H", Math.Round(Convert.ToDouble(row["Eta_H"].ToString()), 3));
                        ht.Add("Rho",Math.Round(Convert.ToDouble(row["Rho"].ToString()),3));
                        ht.Add("T_DATETIME", row["T_DATETIME"].ToString());
                        listdata.Add(ht);
                    }
                }
               
                
            }
            errMsg = "";
            return listdata;
        }
    }
}
