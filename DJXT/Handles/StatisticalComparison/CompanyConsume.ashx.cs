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
    /// CompanyConsume 的摘要说明 集团公司耗差指标分析表格的数据来源。
    /// </summary>
    public class CompanyConsume : IHttpHandler
    {

        BLLCompanyConsume bl = new BLLCompanyConsume();
        string errMsg = string.Empty;
        DateHelper dh = new DateHelper();

        public void ProcessRequest(HttpContext context)
        {
            string beginTime = context.Request["beginTime"] == null ? string.Empty : context.Request["beginTime"].ToString();
            string endTime = string.Empty;
            string timeType = context.Request["timeType"] == null ? string.Empty : context.Request["timeType"].ToString();
            string quarterType = context.Request["quarterType"] == null ? string.Empty : context.Request["quarterType"].ToString();

            //根据选择的时间段，设置开始时间和结束时间
            switch (timeType)
            {
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

            TmpInfoE allInfo = new TmpInfoE();

            List<ConsumeInfo> infoList = new List<ConsumeInfo>();
            infoList = bl.get(beginTime, endTime, out errMsg);

            ConsumeInfo info = new ConsumeInfo();
            info.ConsumeValue = 23;
            info.Name = "七月份耗差平均";
            info.RealValue = 34;
            info.StandardValue = 12.4;
            infoList.Add(info);

            info = new ConsumeInfo();
            info.ConsumeValue = 23;
            info.Name = "七月份分项比例";
            info.RealValue = 34;
            info.StandardValue = 12.4;
            infoList.Add(info);
            //表格
            //string content = infoList.ToJsonItem();
            int count = 0;
            object obj = new
            {
                total = count,
                rows = infoList
            };

            //列名
            //List<column> col = new List<column>();

            //foreach (var info in infoList)
            //{
            //    column c = new column();
            //    c.field = info.Name;
            //    c.title = info.Name;
            //    c.width = "150px";
            //    c.align = "center";
            //}
            //string lm = JsonConvert.SerializeObject(col);
     
            string result = JsonConvert.SerializeObject(infoList);
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

    
    //列名的类
    public class column
    {
        public string field { set; get; }
        public string title { set; get; }
        public string width { set; get; }
        public string align { set; get; }
    }
}