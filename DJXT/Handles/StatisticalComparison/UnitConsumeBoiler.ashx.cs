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
    /// UnitConsumeBoiler 机组耗差指标 锅炉数据提取。
    /// </summary>
    public class UnitConsumeBoiler : IHttpHandler
    {

        BLLEnergyLossIndicator bl = new BLLEnergyLossIndicator();
        string errMsg = string.Empty;
        DateHelper dh = new DateHelper();

        public void ProcessRequest(HttpContext context)
        {
            string unit = String.IsNullOrEmpty(context.Request["unit"].ToString()) ? string.Empty : context.Request["unit"].ToString();
            //string time = String.IsNullOrEmpty(context.Request["time"].ToString())?  string.Empty:context.Request["time"].ToString() ;
            string beginTime = String.IsNullOrEmpty(context.Request["beginTime"].ToString()) ? string.Empty : context.Request["beginTime"].ToString();
            string endTime = String.IsNullOrEmpty(context.Request["endTime"].ToString()) ? string.Empty : context.Request["endTime"].ToString();
            string timeType = String.IsNullOrEmpty(context.Request["timeType"].ToString()) ? string.Empty : context.Request["timeType"].ToString();
            string quarterType = String.IsNullOrEmpty(context.Request["quarterType"].ToString()) ? string.Empty : context.Request["quarterType"].ToString();

            //根据选择的时间段，设置开始时间和结束时间
            switch (quarterType)
            {
                case "0": //指定时间段

                    break;
                case "1"://月度平均值
                    DateTime dt1 = new DateTime();
                    dt1 = Convert.ToDateTime(beginTime.Substring(0, 7) + "-01");
                    beginTime = dh.GetFirstDayOfMonth(dt1).ToString();
                    endTime = dh.GetLastDayOfMonth(dt1).ToString();
                    break;
                case "2"://季度平均值
                    switch (quarterType)
                    {
                        case "0"://一季度
                            string ti = beginTime.Substring(0, 4) + "-01-01";
                            beginTime = ti;
                            endTime = beginTime.Substring(0, 4) + "-03-31";
                            break;
                        case "1"://二季度
                            string ti1 = beginTime.Substring(0, 4) + "-04-01";
                            beginTime = ti1;
                            endTime = beginTime.Substring(0, 4) + "-06-30";
                            break;
                        case "2"://三季度
                            string ti2 = beginTime.Substring(0, 4) + "-07-01";
                            beginTime = ti2;
                            endTime = beginTime.Substring(0, 4) + "-09-30";
                            break;
                        case "3"://四季度
                            string ti3 = beginTime.Substring(0, 4) + "-10-01";
                            beginTime = ti3;
                            endTime = beginTime.Substring(0, 4) + "-12-31";
                            break;
                    }
                    break;
                case "3"://年度平均值
                    string tim = beginTime.Substring(0, 4);
                    beginTime = tim + "01-01";
                    endTime = tim + "-12-31";
                    break;
            }

            List<IndicatorInfo> infoList = new List<IndicatorInfo>();

            //获取锅炉的所有耗差类型。
            infoList = bl.GetInfo(beginTime, endTime, unit, -1, 0, out errMsg);
            //foreach (var info in infoList)
            //{
            for (int i = 0; i < 10; i++)
            {
                IndicatorInfo infos = new IndicatorInfo();
                infos.Name = "主汽温度（°C）";
                infos.StandardValue = 333.22;
                infos.RealValue = 54.32;
                infos.ConsumeValue = 87.09;
                //上线启用
                //infos.Name = info.Name;
                //infos.StandardValue = info.StandardValue;
                //infos.RealValue = info.RealValue;
                //infos.ConsumeValue = info.ConsumeValue;
                infoList.Add(infos);
            }
            //}

            string content = infoList.ToJsonItem();
            int count = 0;
            object obj = new
            {
                total = count,
                rows = infoList
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