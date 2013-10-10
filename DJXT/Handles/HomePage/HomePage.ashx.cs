using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Task;
using Entity.Home;
using SAC.Json;
namespace DJXT.Handles.HomePage
{
    /// <summary>
    /// HomePage 的摘要说明
    /// </summary>
    public class HomePage : IHttpHandler
    {
        BLLTask bt = new BLLTask();
        string errMsg = string.Empty;


        public void ProcessRequest(HttpContext context)
        {
            string time = context.Request["time"].ToString() != "undefined" ? context.Request["time"].ToString() : string.Empty;

            string times = string.Empty;

            //时间
            times += String.IsNullOrEmpty(time) ? string.Empty : time + "-01 00:00:00.0";
            //times = "2013-05-01 00:00:00.0";
            List<StatisticInfo> info = new List<StatisticInfo>();
            info=bt.GetHomeByTime(times,out errMsg);

            List<StatisticInfo> saveInfo = new List<StatisticInfo>();

            StatisticInfo tmp = new StatisticInfo();
            if (info.Count>0)
            {
                //发电设备容量
                tmp = info.Where(infos => infos.T_INDICATORNAME == "设备容量").FirstOrDefault();
                saveInfo.Add(tmp);

                //发电设备利用小时
                tmp = info.Where(infos => infos.T_INDICATORNAME == "利用小时").FirstOrDefault();
                saveInfo.Add(tmp);
                //供电煤耗
                tmp = info.Where(infos => infos.T_INDICATORNAME == "供电煤耗").FirstOrDefault();
                saveInfo.Add(tmp);
                //厂用电率
                tmp = info.Where(infos => infos.T_INDICATORNAME == "厂用电率").FirstOrDefault();
                saveInfo.Add(tmp);
            }
            else
            {
                for(int i=0;i<4;i++)
                { saveInfo.Add(tmp); }
            }
            string content = saveInfo.ToJsonItem();

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
}