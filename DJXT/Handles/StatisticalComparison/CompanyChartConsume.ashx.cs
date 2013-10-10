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
    /// CompanyChartConsume 集团图表耗差  CompanyConsume.aspx
    /// </summary>
    public class CompanyChartConsume : IHttpHandler
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

            AllInfo allinfo = new AllInfo();

            List<ConsumeInfo> infoList;

            infoList = bl.get(beginTime, endTime, out errMsg);

            //平均值柱状图
            infos tmp = new infos();
            tmp.name = new ArrayList();
            tmp.value = new ArrayList();
            foreach (ConsumeInfo tmps in infoList)
            {

                tmp.name.Add(tmps.Name);
                tmp.value.Add(Math.Round(tmps.Count, 2));
            }
            allinfo.ZhuTu = tmp;


            //饼状图
            ArrayList ConsumeList = new ArrayList();

            ArrayList tmsp;
            foreach (ConsumeInfo tmps in infoList)
            {
                tmsp = new ArrayList();
                tmsp.Add(tmps.Name);
                tmsp.Add(Math.Round(tmps.Count, 2));
                ConsumeList.Add(tmsp);
            }
            allinfo.ConsumeList = ConsumeList;


            //环比柱状图
            List<infos> info = new List<infos>();
            //七月份
            infos ht = new infos();
            ht.time = DateTime.Parse(beginTime).Month+"月份";
            ht.name = new ArrayList();
            ht.value = new ArrayList();

            foreach (ConsumeInfo tmps in infoList)
            {

                ht.name.Add(tmps.Name);
                ht.value.Add(Math.Round(tmps.Count, 2));
            }
            info.Add(ht);


            //六月份
            infos sixMonth = new infos();
            sixMonth.name = new ArrayList();
            sixMonth.value = new ArrayList();
            string beforBeginTime = DateTime.Parse(beginTime).AddMonths(-1).ToString("yyyy-MM-dd HH:mm:ss");
            string beforEndTime = DateTime.Parse(beforBeginTime).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
            sixMonth.time = DateTime.Parse(beforBeginTime).Month + "月份";
            List<ConsumeInfo> beforInfoList = bl.get(beforBeginTime, beforEndTime, out errMsg);

            foreach (ConsumeInfo tmps in beforInfoList)
            {

                sixMonth.name.Add(tmps.Name);
                sixMonth.value.Add(Math.Round(tmps.Count, 2));
            }
            info.Add(sixMonth);

            //环比
            infos SsHb = new infos();
            SsHb.time = "环比增长";
            SsHb.name = new ArrayList();
            SsHb.value = new ArrayList();

            foreach (ConsumeInfo infos in infoList)
            {
                ConsumeInfo c = beforInfoList.Where(t => t.Name == infos.Name).FirstOrDefault();
                SsHb.name.Add(c.Name);
                if (infos.Count > 0)
                {
                    SsHb.value.Add(Math.Round(((infos.Count - c.Count) / infos.Count) * 100, 2));
                }
                else
                {
                    SsHb.value.Add(0);
                }
            }
            info.Add(SsHb);

            allinfo.Hb = info;

            
            string content = allinfo.ToJsonItem();
            context.Response.ContentType = "text/json;charset=gb2312;";
            context.Response.Write(content);
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
    public class AllInfo
    {
        //耗差 饼状图
        public ArrayList ConsumeList { set; get; }

        //耗差 柱状图
        public infos ZhuTu { set; get; }

        public List<infos> Hb { set; get; }
    }

    /// <summary>
    /// 柱状图类
    /// </summary>
    public class infos
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