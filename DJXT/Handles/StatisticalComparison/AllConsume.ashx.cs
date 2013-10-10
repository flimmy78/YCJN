using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.StatisticalComparison;
using Entity.Statistic;
using SAC.Helper;
using SAC.Json;
using Newtonsoft.Json;
using System.Collections;

namespace DJXT.Handles.StatisticalComparison
{
    /// <summary>
    /// AllConsume 的摘要说明 机组耗差分析页面总耗差表数据来源。
    /// </summary>
    public class AllConsume : IHttpHandler
    {
        BLLEnergyLossIndicator bl = new BLLEnergyLossIndicator();
        string errMsg = string.Empty;
        DateHelper dh = new DateHelper();

        public void ProcessRequest(HttpContext context)
        {
            string unit = context.Request["unit"] == null ? string.Empty : context.Request["unit"].ToString();

            //string unit = String.IsNullOrEmpty(context.Request["unit"].ToString()) ? string.Empty : context.Request["unit"].ToString();
            //string time = String.IsNullOrEmpty(context.Request["time"].ToString())?  string.Empty:context.Request["time"].ToString() ;
            string beginTime = context.Request["beginTime"] == null ? string.Empty : context.Request["beginTime"].ToString();
            string endTime = context.Request["endTime"] == null ? string.Empty : context.Request["endTime"].ToString();
            string timeType = context.Request["timeType"] == null ? string.Empty : context.Request["timeType"].ToString();
            string quarterType = context.Request["quarterType"] == null ? string.Empty : context.Request["quarterType"].ToString();

            //根据选择的时间段，设置开始时间和结束时间
            switch (timeType)
            {
                case "0": //指定时间段

                    break;
                case "1"://月度平均值
                    DateTime dt1 = new DateTime();
                    dt1 = Convert.ToDateTime(beginTime.Substring(0, 7) + "-01");
                    beginTime = dh.GetFirstDayOfMonth(dt1).ToString().Replace("/", "-");
                    endTime = dh.GetLastDayOfMonth(dt1).ToString().Replace("/", "-");
                    break;
                case "2"://季度平均值
                    switch (quarterType)
                    {
                        case "0"://一季度
                            string ti = beginTime.Substring(0, 4) + "-01-01 00:00:00";
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

            //获取所有耗差类型。
            //infoList = bl.GetAllInfo(beginTime, endTime, unit, out errMsg);
            //foreach (var info in infoList)
            //{
            //    ConsumeInfo infos = new ConsumeInfo();
            //    infos.Name = info.Name;
            //    infos.Count = info.Count;
            //    infoList.Add(infos);
            //}

            //ConsumeInfo infos = new ConsumeInfo();
            //infos.Name = "主汽温度";
            //infos.Count = 45;

            //infoList.Add(infos);

            //infos = new ConsumeInfo();
            //infos.Name = "主汽压力（Mpa）";
            //infos.Count = 43;
            //infoList.Add(infos);

            //获取锅炉和汽机的所有耗差类型。
            infoList = bl.GetInfo(beginTime, endTime, unit, -1, -1, out errMsg);
            List<BindInfo> bindList = new List<BindInfo>();
            //总耗差值初始化
            double allValue = 0;
            ArrayList reason = new ArrayList();
            //锅炉可控
            List<IndicatorInfo> tmpList = infoList.Where(info => info.ConsumeType == "0" && info.TargetType == "0").ToList();
            BindInfo tmp = new BindInfo();
            double value = 0;
            tmp.name="锅炉可控";
            foreach (var info in tmpList)
            {
                value += info.ConsumeValue;
            }
            //value = 32.23;
            allValue += value;
            tmp.value=Math.Round(value,2).ToString();
            bindList.Add(tmp);


            //锅炉不可控
            tmpList = infoList.Where(info => info.ConsumeType == "1" && info.TargetType == "0").ToList();
            tmp = new BindInfo();
            value = 0;
            tmp.name = "锅炉不可控";
            foreach (var info in tmpList)
            {
                value += info.ConsumeValue;
            }
            allValue += value;
            tmp.value = Math.Round(value, 2).ToString();
            bindList.Add(tmp);

            //汽机可控
            tmpList = infoList.Where(info => info.ConsumeType == "0" && info.TargetType == "1").ToList();
            tmp = new BindInfo();
            value = 0;
            tmp.name = "汽机可控";
            foreach (var info in tmpList)
            {
                value += info.ConsumeValue;
            }
            allValue += value;
            tmp.value = Math.Round(value, 2).ToString();
            bindList.Add(tmp);


            //汽机不可控
            tmpList = infoList.Where(info => info.ConsumeType == "1" && info.TargetType == "1").ToList();
            tmp = new BindInfo();
            value = 0;
            tmp.name = "汽机不可控";
            foreach (var info in tmpList)
            {
                value += info.ConsumeValue;
            }
            allValue += value;
            tmp.value = Math.Round(value, 2).ToString();
            bindList.Add(tmp);

            
            tmpList = infoList.Where(info => info.ConsumeType != "1" && info.TargetType != "1" && info.ConsumeType != "0" && info.TargetType != "0").ToList();
            tmp = new BindInfo();
            value = 0;
            tmp.name = "不可知因素能耗";
            foreach (var info in tmpList)
            {
                value += info.ConsumeValue;
            }
            allValue += value;
            tmp.value = Math.Round(value, 2).ToString();
            bindList.Add(tmp);

            //总耗差
            tmp = new BindInfo();
            tmp.name = "总耗差";
            //tmp.value = allValue.ToString();
            tmp.value = Math.Round(allValue, 2).ToString();
            bindList.Add(tmp);

            //string content = bindList.ToJsonItem();
            int count = 0;
            object obj = new
            {
                total = count,
                rows = bindList
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

        //前台绑定总耗差列表的类
        public class BindInfo
        {
            public string name { set; get; }
            public string value { set; get; }
        }
    }
}