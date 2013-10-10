using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.StatisticalComparison;
using Entity.Statistic;
using SAC.Helper;
using SAC.Json;
using Newtonsoft.Json;

namespace DJXT.Handles.StatisticalComparison
{
    /// <summary>
    /// UnitConsumeSteam 机组耗差指标 汽机数据提取。
    /// </summary>
    public class UnitConsumeSteam : IHttpHandler
    {
        BLLEnergyLossIndicator bl = new BLLEnergyLossIndicator();
        string errMsg = string.Empty;
        DateHelper dh = new DateHelper();

        public void ProcessRequest(HttpContext context)
        {
            string unit = context.Request["unit"]==null ? string.Empty : context.Request["unit"].ToString();
            //string time = String.IsNullOrEmpty(context.Request["time"].ToString())?  string.Empty:context.Request["time"].ToString() ;
            string beginTime = context.Request["beginTime"]==null ? string.Empty : context.Request["beginTime"].ToString();
            string endTime = context.Request["endTime"]==null? string.Empty : context.Request["endTime"].ToString();
            string timeType = context.Request["timeType"]==null ? string.Empty : context.Request["timeType"].ToString();
            string quarterType = context.Request["quarterType"]==null ? string.Empty : context.Request["quarterType"].ToString();

            //根据选择的时间段，设置开始时间和结束时间
            switch (timeType)
            {
                case "0": //指定时间段
                    
                    break;
                case "1"://月度平均值
                    DateTime dt1 = new DateTime();
                    dt1 = Convert.ToDateTime(beginTime.Substring(0,7) + "-01");
                    beginTime = dh.GetFirstDayOfMonth(dt1).ToString().Replace("/","-");
                    endTime = dh.GetLastDayOfMonth(dt1).ToString().Replace("/", "-");
                    break;
                case "2"://季度平均值
                    switch (quarterType)
                    { 
                        case "0"://一季度
                            string  ti = beginTime.Substring(0,4)+"-01-01 00:00:00";
                            beginTime = ti;
                            endTime = beginTime.Substring(0, 4) + "-03-31 23:59:59";
                            break;
                        case "1"://二季度
                            string ti1 = beginTime.Substring(0, 4) + "-04-01 00:00:00";
                            beginTime = ti1;
                            endTime = beginTime.Substring(0, 4) + "-06-30 23:59:59";
                            break;
                        case "2"://三季度
                            string ti2 = beginTime.Substring(0, 4) + "-07-01 00:00:00";
                            beginTime = ti2;
                            endTime = beginTime.Substring(0, 4) + "-09-30 23:59:59";
                            break;
                        case "3"://四季度
                            string ti3 = beginTime.Substring(0, 4) + "-10-01 00:00:00";
                            beginTime = ti3;
                            endTime = beginTime.Substring(0, 4) + "-12-31 23:59:59";
                            break;
                    }
                    break;
                case "3"://年度平均值
                    string tim = beginTime.Substring(0, 4);
                    beginTime = tim + "-01-01 00:00:00";
                    endTime = tim + "-12-31 23:59:59";
                    break;
            }

            List<IndicatorInfo> infoList = new List<IndicatorInfo>();
            List<IndicatorInfo> saveList = new List<IndicatorInfo>();

            //获取汽机和锅炉的所有耗差类型。
            infoList = bl.GetInfo(beginTime, endTime, unit, -1, -1, out errMsg);
           List<IndicatorInfo> tmpList=  infoList.Where(info=>info.ConsumeType=="0"||info.ConsumeType=="1").ToList();
            //上线启用
            //foreach (var info in infoList)
           for (int i = 0; i < tmpList.Count; i++)
            {
                IndicatorInfo infos = new IndicatorInfo();
                //info.Name = "主汽温度（°C）";
                //info.StandardValue = 333.22;
                //info.RealValue = 54.32;
                //info.ConsumeValue = 87.09;
                if (!string.IsNullOrEmpty(tmpList[i].Name))
                {
                    infos.Name = tmpList[i].Name;
                    infos.StandardValue = Math.Round(tmpList[i].StandardValue, 2);
                    infos.RealValue = Math.Round(tmpList[i].RealValue, 2);
                    infos.ConsumeValue = Math.Round(tmpList[i].ConsumeValue, 2);
                    saveList.Add(infos);
                }
            }
            //IndicatorInfo infos = new IndicatorInfo();

            //infos.Name = "主汽温度（°C）";
            //infos.StandardValue = 538.22;
            //infos.RealValue = 533.32;
            //infos.ConsumeValue = 0.64;
            //infoList.Add(infos);
            //infos = new IndicatorInfo();
            //infos.Name = "主汽压力（Mpa）";
            //infos.StandardValue = 16.70;
            //infos.RealValue = 16.43;
            //infos.ConsumeValue = 0.21;
            //infoList.Add(infos);

            int count = 0;
            object obj = new
            {
                total = count,
                rows = saveList
            };

            string result = JsonConvert.SerializeObject(obj);
            context.Response.ContentType = "text/json;charset=gb2312;";
            context.Response.Write(result);


        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}