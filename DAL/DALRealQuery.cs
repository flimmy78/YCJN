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
///日期：2013-06-27
namespace DAL
{
    public class DALRealQuery
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
        /// 获取数据库中分公司
        /// </summary>
        /// <returns></returns>
        public DataSet Get_Company_Info(out string errMsg)
        {
            this.init();
            errMsg = "";
            DataSet DS = new DataSet();
            string sql = "select T_COMPANYID,T_COMPANYDESC from T_BASE_COMPANY";

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
        /// 获取数据库中电厂
        /// </summary>
        /// <param name="company_id">分公司ID</param>
        /// <returns></returns>
        public DataSet Get_Electric_Info(string company_id,out string errMsg)
        {
            this.init();
            errMsg = "";
            DataSet DS = new DataSet();
            string sql = "select T_PLANTID,T_PLANTDESC from T_BASE_PLANT where T_COMPANYID='" + company_id+"'";

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
        /// 获取数据库中电厂机组
        /// </summary>
        /// <param name="electric_id">电厂ID</param>
        /// <returns></returns>
        public DataSet Get_Unit_Info(string electric_id, out string errMsg)
        {
            this.init();
            errMsg = "";
            DataSet DS = new DataSet();
            string sql = "select T_UNITID,T_UNITDESC from T_BASE_UNIT where T_PLANTID='" + electric_id + "'";

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
        /// 获取数据库中机组测点
        /// </summary>
        /// <param name="unit_id">机组ID</param>
        /// <returns></returns>
        public DataSet Get_Para_Info(string unit_id, out string errMsg)
        {
            this.init();
            errMsg = "";
            DataSet DS = new DataSet();
            string sql = "select T_PARAID,T_PARADESC from T_BASE_PARAID where T_UNITID	='" + unit_id + "'";

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
        /// 获取数据库中机组参数ID
        /// </summary>
        /// <param name="unit_id">机组ID</param>
        /// <returns></returns>
        public DataSet Get_BASE_CRICPARA(string unit_id, out string errMsg)
        {
            this.init();
            errMsg = "";
            DataSet DS = new DataSet();
            string sql = "select T_PARAID,T_PARADESC from T_BASE_CRICPARA where T_UNITID	='" + unit_id + "'";

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
        /// 获取实时测点数据
        /// </summary>
        /// <param name="real_data">测点</param>
        /// <param name="stime">当前时间</param>
        /// <returns></returns>
        public string GetChartRealData(string[] real_data, string stime)
        {
            this.init();
            int ret = 0;
            double drvA = 0;
            string str = "";
            if (rtDBType == "EDNA")
            {

            }
            else
            {
                Plink.OpenPi();
            }
            

            for (int i = 0; i < real_data.Length; i++)
            {

                
                if (rtDBType == "EDNA")
                {
                    ek.GetHisValueByTime(real_data[i].Split(',')[0], stime, ref ret, ref drvA);
                }
                else
                {
                    pk.GetHisValue(real_data[i].Split(',')[0], stime, ref drvA);
                }
                str += DateTimeToUTC(Convert.ToDateTime(stime)) + "," + Math.Round(Convert.ToDouble(drvA.ToString()), 3) + ";";
            }
            return str.TrimEnd(';');
        }


        /// <summary>
        /// 获取实时测点数据-所有的
        /// </summary>
        /// <param name="real_data">测点id</param>
        /// <param name="stime">查询的起始时间</param>
        /// <param name="etime">结束时间</param>
        /// <returns>返回值数组</returns>
        public IList<Hashtable> GetChartData(string[] real_data, string stime, string etime, out string max_data_total, out string min_data_total)
        {
            this.init();
            int ret = 0, num = 0,num_pin=1;
            max_data_total = ""; min_data_total = "";
            double drvA = 0;
            double max_data = 0,min_data = 0;
            //dt = new DataTable();
            if (rtDBType == "EDNA")
            {
                
            }
            else
            {
                Plink.OpenPi();
            }
            IList<Hashtable> listdata = new List<Hashtable>();
            //dt.Columns.Add("序号", typeof(int));
            Hashtable ht = new Hashtable();
            
            for (int i = 0; i < real_data.Length; i++)
            {
                string[] dv;

                DateTime date;
                if (Convert.ToInt32((DateTime.Parse(etime) - DateTime.Parse(stime)).TotalMinutes) > 20)
                {
                    date = DateTime.Parse(stime);
                    dv = new string[600]; //值
                }
                else
                {
                    date = DateTime.Parse(stime).AddMinutes(-10);
                    dv = new string[Convert.ToInt32((DateTime.Parse(etime) - DateTime.Parse(stime).AddMinutes(-10)).TotalSeconds / 1)]; //值
                }

                ht = new Hashtable();
                ArrayList ld = new ArrayList();
                ArrayList lt = new ArrayList();
                ht.Add("name", real_data[i].Split(',')[1]);
                //dt.Columns.Add(real_data[i].Split(',')[1], typeof(double));
                //if (i==real_data.Length-1)
                //{
                //    dt.Columns.Add("时间", typeof(DateTime));
                //}
                
                while (date < DateTime.Parse(etime))
                {
                    if (rtDBType == "EDNA")
                    {
                        ek.GetHisValueByTime(real_data[i].Split(',')[0], date.ToString(), ref ret, ref drvA);
                    }
                    else
                    {
                        pk.GetHisValue(real_data[i].Split(',')[0], date.ToString(), ref drvA);
                    }
                    ld = new ArrayList();
                    ld.Add(DateTimeToUTC(date));
                    ld.Add(Math.Round(Convert.ToDouble(drvA.ToString()), 3));
                    lt.Add(ld);
                    if (num ==0)
                    {
                        max_data = Convert.ToDouble(drvA.ToString());
                        min_data = Convert.ToDouble(drvA.ToString());
                        num++;
                    }
                    else
                    {
                        if (max_data<Convert.ToDouble(drvA.ToString()))
                        {
                            max_data=Convert.ToDouble(drvA.ToString());
                        }
                        if (min_data>Convert.ToDouble(drvA.ToString()))
                        {
                            min_data=Convert.ToDouble(drvA.ToString());
                        }
                    }
                    if (Convert.ToInt32((DateTime.Parse(etime) - DateTime.Parse(stime)).TotalMinutes) > 20)
                    {
                        date = date.AddSeconds(Convert.ToInt32((DateTime.Parse(etime) - DateTime.Parse(stime)).TotalSeconds) /600);
                    }
                    else
                    {

                        date = date.AddSeconds(3.0);
                    }
                }

                max_data_total += Math.Round(max_data, 3) + ",";
                min_data_total += Math.Round(min_data, 3) + ",";
                max_data = 0; min_data = 0;
                ht.Add("data", lt);
                ht.Add("yAxis", i);
                listdata.Add(ht);
            }
            return listdata;
        }


        /// <summary>
        /// 获取实时测点数据
        /// </summary>
        /// <param name="real_data">测点id</param>
        /// <param name="stime">查询时间</param>
        /// <returns>返回值数组</returns>
        public IList<Hashtable> GetChartData_Real(string[] real_data, string stime)
        {
            this.init();
            int ret = 0;
            double drvA = 0;
            if (rtDBType == "EDNA")
            {

            }
            else
            {
                Plink.OpenPi();
            }
            IList<Hashtable> listdata = new List<Hashtable>();
            Hashtable ht = new Hashtable();

            for (int i = 0; i < real_data.Length; i++)
            {
                ht = new Hashtable();
                ArrayList ld = new ArrayList();
                ArrayList lt = new ArrayList();
                if (rtDBType == "EDNA")
                {
                    ek.GetHisValueByTime(real_data[i].Split(',')[0], stime, ref ret, ref drvA);
                }
                else
                {
                    pk.GetHisValue(real_data[i].Split(',')[0], stime, ref drvA);
                }
                ld = new ArrayList();
                ld.Add(DateTimeToUTC(Convert.ToDateTime(stime)));
                ld.Add(Math.Round(Convert.ToDouble(drvA.ToString()),3));
                lt.Add(ld);
                ht.Add("data", lt);
                listdata.Add(ht);
            }
            return listdata;
        }

        public static long DateTimeToUTC(DateTime DT)
        {
            DateTime _sTime = new DateTime(1970, 1, 1);
            long _time = Convert.ToInt64((DT - _sTime).TotalMilliseconds);
            //long a = new DateTime(1970, 1, 1, 0, 0, 0, 0).Ticks;
            //int rtnInt = 0;
            //rtnInt = (int)((DT.Ticks - 8 * 3600 * 1e7 - a) / 1e7);
            return _time;
        }

    }
}
