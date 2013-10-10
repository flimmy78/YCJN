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
    /// IndicatorSearch 汽机的返回信息
    /// </summary>
    public class IndicatorSearch : IHttpHandler
    {
        BLLIndicatorSearch bi = new BLLIndicatorSearch();
        string errMsg = string.Empty;
        DateHelper dh = new DateHelper();
        public void ProcessRequest(HttpContext context)
        {
            string param = context.Request["param"];

            if (param != "")
            {
                if (param == "seachList")
                {
                    string companId = String.IsNullOrEmpty(context.Request["companId"].ToString()) ? string.Empty : context.Request["companId"].ToString();
                    string plantId = String.IsNullOrEmpty(context.Request["plantId"].ToString()) ? string.Empty : context.Request["plantId"].ToString();
                    string unit = String.IsNullOrEmpty(context.Request["unit"].ToString()) ? string.Empty : context.Request["unit"].ToString();
                    //string time = String.IsNullOrEmpty(context.Request["time"].ToString())?  string.Empty:context.Request["time"].ToString() ;
                    string beginTime = String.IsNullOrEmpty(context.Request["beginTime"].ToString()) ? string.Empty : context.Request["beginTime"].ToString();
                    string endTime = String.IsNullOrEmpty(context.Request["endTime"].ToString()) ? string.Empty : context.Request["endTime"].ToString();

                    List<IndicatorInfo> infoList = new List<IndicatorInfo>();
                    List<IndicatorInfo> saveList = new List<IndicatorInfo>();

                    //获取汽机和锅炉的所有耗差类型。
                    infoList = bi.GetInfo(beginTime, endTime, companId, plantId, unit, -1, 1, out errMsg);
                    //上线启用
                    //foreach (var info in infoList)
                    for (int i = 0; i < infoList.Count; i++)
                    {
                        IndicatorInfo infos = new IndicatorInfo();
                        //info.Name = "主汽温度（°C）";
                        //info.StandardValue = 333.22;
                        //info.RealValue = 54.32;
                        //info.ConsumeValue = 87.09;
                        if (!string.IsNullOrEmpty(infoList[i].Name))
                        {
                            infos.Name = infoList[i].Name;
                            infos.StandardValue = Math.Round(infoList[i].StandardValue, 2);
                            infos.RealValue = Math.Round(infoList[i].RealValue, 2);
                            infos.ConsumeValue = Math.Round(infoList[i].ConsumeValue, 2);
                            saveList.Add(infos);
                        }
                    }

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
            }
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