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
    /// IndicatorSearchBoiler 锅炉的返回信息
    /// </summary>
    public class IndicatorSearchBoiler : IHttpHandler
    {
        BLLIndicatorSearch bi = new BLLIndicatorSearch();
        string errMsg = string.Empty;
        DateHelper dh = new DateHelper();
        public void ProcessRequest(HttpContext context)
        {
            string companyId = String.IsNullOrEmpty(context.Request["companyId"]) ? string.Empty : context.Request["companyId"].ToString();
            string plantId = String.IsNullOrEmpty(context.Request["plantId"]) ? string.Empty : context.Request["plantId"].ToString();
            string unit = String.IsNullOrEmpty(context.Request["unit"]) ? string.Empty : context.Request["unit"].ToString();
            //string time = String.IsNullOrEmpty(context.Request["time"].ToString())?  string.Empty:context.Request["time"].ToString() ;
            string beginTime = String.IsNullOrEmpty(context.Request["beginTime"]) ? string.Empty : context.Request["beginTime"].ToString();
            string endTime = String.IsNullOrEmpty(context.Request["endTime"]) ? string.Empty : context.Request["endTime"].ToString();

            List<IndicatorInfo> infoList = new List<IndicatorInfo>();
            List<IndicatorInfo> saveList = new List<IndicatorInfo>();

            //获取锅炉的信息。
            infoList = bi.GetInfo(beginTime, endTime, companyId, plantId, unit, 0, -1, out errMsg);
            //上线启用
            //foreach (var info in infoList)
            for (int i = 0; i < infoList.Count; i++)
            {
                IndicatorInfo infos = new IndicatorInfo();
                if (!string.IsNullOrEmpty(infoList[i].Name))
                {
                    infos.Name = infoList[i].Name;
                    infos.StandardValue = Math.Round(infoList[i].StandardValue, 2);
                    infos.RealValue = Math.Round(infoList[i].RealValue, 2);
                    infos.ConsumeValue = Math.Round(infoList[i].ConsumeValue, 2);
                    infos.Unit = infoList[i].Unit;
                    saveList.Add(infos);
                }
            }

            int count = saveList.Count;
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