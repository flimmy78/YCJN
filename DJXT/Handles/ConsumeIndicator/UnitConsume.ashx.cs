using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.ConsumeIndicator;
using Newtonsoft.Json;
using Entity.Home;
using BLL.Task;
using SAC.Json;
using System.Collections;
using Entity.ConsumeIndicator;

namespace DJXT.Handles.ConsumeIndicator
{
    /// <summary>
    /// 能耗指标-图表对标详细机组类型
    /// </summary>
    public class UnitConsume : IHttpHandler
    {
        string errMsg = string.Empty;
        BLLConsumeIndicator bc = new BLLConsumeIndicator();
        BLLTask bt = new BLLTask();

        public void ProcessRequest(HttpContext context)
        {
            string time = context.Request["beginTime"].ToString() != "undefined" ? context.Request["beginTime"].ToString() : string.Empty;

            string times = string.Empty;
            AllInfo aInfo = new AllInfo();
            //时间
            //times += String.IsNullOrEmpty(time) ? string.Empty : time + "-01 00:00:00.0";
            //times = "2013-05-01 00:00:00.0";


            //五大集团供电煤耗
            List<BestUnitConsumeInfo> info = new List<BestUnitConsumeInfo>();
            info = bt.GetInfoByTime(time, out errMsg);
            List<BestUnitConsumeInfo> saveInfo = new List<BestUnitConsumeInfo>();
            BestUnitConsumeInfo tmp = new BestUnitConsumeInfo();
            if (info.Count > 0)
            {
                tmp = info.Where(infos => infos.T_COMPANY.Trim() == "华电").FirstOrDefault();
                saveInfo.Add(tmp);
                tmp = info.Where(infos => infos.T_COMPANY.Trim() == "华能").FirstOrDefault();
                saveInfo.Add(tmp);
                tmp = info.Where(infos => infos.T_COMPANY.Trim() == "大唐").FirstOrDefault();
                saveInfo.Add(tmp);
                tmp = info.Where(infos => infos.T_COMPANY.Trim() == "国电").FirstOrDefault();
                saveInfo.Add(tmp);
                tmp = info.Where(infos => infos.T_COMPANY.Trim() == "中电投").FirstOrDefault();
                saveInfo.Add(tmp);
            }
            else
            {
                for (int i = 0; i < 5; i++)
                { saveInfo.Add(tmp); }
            }
            aInfo.SInfo = saveInfo;
            saveInfo = null;

            //供电煤耗月线
            List<MonthConsumeInfo> mTmp = bc.GetMonthConsume(out errMsg);
            List<MonthConsumeInfo> m = new List<MonthConsumeInfo>();
            aInfo.Minfo = new List<ArrayList>();

            //2011年
            ArrayList al = new ArrayList();
            m = mTmp.Where(tinfo => tinfo.year == 2011).OrderBy(infos => infos.month).ToList();
            foreach (MonthConsumeInfo ts in m)
            {
                al.Add(ts.values);
            }
            aInfo.Minfo.Add(al);
            //2012年
            al = new ArrayList();
            m = mTmp.Where(tinfo => tinfo.year == 2012).OrderBy(infos => infos.month).ToList();
            foreach (MonthConsumeInfo ts in m)
            {
                al.Add(ts.values);
            }
            aInfo.Minfo.Add(al);
            //2013年
            al = new ArrayList();
            m = mTmp.Where(tinfo => tinfo.year == 2013).OrderBy(infos => infos.month).ToList();
            foreach (MonthConsumeInfo ts in m)
            {
                al.Add(ts.values);
            }
            aInfo.Minfo.Add(al);

            string content = aInfo.ToJsonItem();
            context.Response.ContentType = "text/json;charset=GB2312;";
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
        /// <summary>
        /// 五大集团最优机组供电煤耗
        /// </summary>
        public List<BestUnitConsumeInfo> SInfo { set; get; }
        /// <summary>
        /// 供电煤耗月线
        /// </summary>
        public List<ArrayList> Minfo { set; get; }
    
    }
}