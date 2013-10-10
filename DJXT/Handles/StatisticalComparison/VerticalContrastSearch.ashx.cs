using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.StatisticalComparison;
using Entity.Statistic;
using SAC.Helper;
using SAC.Json;
using Newtonsoft.Json;
using System.Data;
using System.Collections;

namespace DJXT.Handles.StatisticalComparison
{
    /// <summary>
    /// VerticalContrastSearch 的摘要说明
    /// </summary>
    public class VerticalContrastSearch : IHttpHandler
    {
        BLLVerticalContrastSearch bv = new BLLVerticalContrastSearch();
        string errMsg = string.Empty;

        public void ProcessRequest(HttpContext context)
        {
            //处理时间  两个数组 分别对应 时间和值 看两个日期之间有多少个月
            string beginTime = context.Request["beginTime"].ToString() != "undefined" ? context.Request["beginTime"].ToString() : string.Empty;
            string endTime= context.Request["endTime"].ToString() != "undefined" ? context.Request["endTime"].ToString() : string.Empty;
            string unitId= context.Request["unitId"].ToString() != "undefined" ? context.Request["unitId"].ToString() : string.Empty;
            string paraId= context.Request["paraId"].ToString() != "undefined" ? context.Request["paraId"].ToString() : string.Empty;

            DateTime dt1=DateTime.Parse(beginTime+"-01");
            DateTime dt2=DateTime.Parse(endTime+"-01");
            int monthCount = dt2.Year * 12 + dt2.Month - dt1.Year * 12 - dt1.Month + 1;

            ReturnInfo returninfo = new ReturnInfo();
            returninfo.date = new ArrayList();
            returninfo.value = new ArrayList();
            List<IndicatorInfo> infoList;

            for (int i = 0; i < monthCount; i++)
            {
                //获取每个月的数据
                beginTime = dt1.AddMonths(i).ToString("yyyy-MM-01 00:00:00");
                endTime = DateTime.Parse(beginTime).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 24:00:00");
                infoList = new List<IndicatorInfo>();
                infoList = bv.GetInfo(beginTime, endTime, unitId, paraId, out errMsg);
                for (int j = 0; j < infoList.Count; j++)
                {
                    returninfo.date.Add(beginTime.Substring(0,7));
                    returninfo.value.Add(Math.Round(infoList[j].RealValue,2));
                    returninfo.name = infoList[j].Name;
                    returninfo.unit = infoList[j].Unit;
                }
            }

            string content = JsonConvert.SerializeObject(returninfo);
            context.Response.ContentType = "text/json;charset=gb2312;";
            context.Response.Write(content);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
    public class ReturnInfo
    {
        //日期
       public ArrayList date { set; get; }
        //每月对应的值
       public ArrayList value { set; get; }
        //指标名称
       public string name { set; get; }
        //指标单位
       public string unit { set; get; }
    }
}