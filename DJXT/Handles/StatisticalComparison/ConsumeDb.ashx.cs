using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using Entity.Statistic;
using SAC.Helper;
using SAC.Json;
using Newtonsoft.Json;
using BLL.StatisticalComparison;

namespace DJXT.Handles.StatisticalComparison
{
    /// <summary>
    /// ConsumeDb 的摘要说明
    /// </summary>
    public class ConsumeDb : IHttpHandler
    {
        BLLStandardAnalysis bs = new BLLStandardAnalysis();
        string errMsg = string.Empty;
        DateHelper dh = new DateHelper();

        public void ProcessRequest(HttpContext context)
        {
            string capacityLevel = String.IsNullOrEmpty(context.Request.QueryString["capacityLevel"].ToString()) ? string.Empty : HttpContext.Current.Server.UrlDecode(context.Request.QueryString["capacityLevel"]).ToString();
            string unitType = String.IsNullOrEmpty(context.Request["unitType"].ToString()) ? string.Empty : HttpContext.Current.Server.UrlDecode(context.Request["unitType"].ToString());
            string BoilerId = String.IsNullOrEmpty(context.Request["BoilerId"].ToString()) ? string.Empty : context.Request["BoilerId"].ToString();
            string SteamId = String.IsNullOrEmpty(context.Request["SteamId"].ToString()) ? string.Empty : context.Request["SteamId"].ToString();

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

            List<ConsumeInfo> infoList;
            if (capacityLevel == "--请选择机组容量--")
            {
                capacityLevel = string.Empty;
            }
            if (unitType == "--请选择机组类型--")
            {
                unitType = string.Empty;
            }
            infoList = bs.Get(capacityLevel,unitType,BoilerId,SteamId,beginTime, endTime, out errMsg);

            //获取耗差平均值。（柱状图）
            ZhuTu tmp = new ZhuTu();
            tmp.name = new ArrayList();
            tmp.value = new ArrayList();

            double count = 0;
            int i = 0;
            foreach (var info in infoList)
            {
                tmp.name.Add(info.Name);
                tmp.value.Add(Math.Round(info.Count,4));
                ++i;
                count += info.Count;
            }
            tmp.name.Add("集团平均值");
            if (i != 0)
            {
                tmp.value.Add(Math.Round(count / i,4));
            }
            else
            {
                tmp.value.Add(0);
            }
          
            //for (int i = 0; i < 10; i++)
            //{
            //    tmp.name.Add("主汽温度（°C）");

            //    tmp.value.Add(3.22);

            //}
             

            string content = JsonConvert.SerializeObject(tmp); //allinfo.ToJsonItem();
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

    /// <summary>
    /// 柱状图类
    /// </summary>
    public class ZhuTu
    {
        /// <summary>
        /// 值。
        /// </summary>
        public ArrayList value { set; get; }
        /// <summary>
        /// 耗差名称。
        /// </summary>
        public ArrayList name { set; get; }
        /// <summary>
        /// 时间
        /// </summary>
        public string time { set; get; }
    }
}