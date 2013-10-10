using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAC.Entity;
using SAC.Json;
using System.Data;
using BLL;
using System.Collections;

namespace DJXT.Handle
{
    /// <summary>
    /// ReliableAnalysis 的摘要说明
    /// </summary>
    public class ReliableAnalysis : IHttpHandler
    {
        BLLEquipmentReliable bl = new BLLEquipmentReliable();
        string errMsg = string.Empty;

        public void ProcessRequest(HttpContext context)
        {
            string time=context.Request["time"].ToString()!="undefined"?context.Request["time"].ToString():string.Empty;

            string unitCode = context.Request["unitCode"].ToString() != "undefined" ? context.Request["unitCode"].ToString() : string.Empty;

            //       // string name = context.Request["name"].ToString();
            //        string time = "2013-01";

            //string time=context.Request["time"].ToString()!="undefined"?context.Request["time"].ToString():string.Empty;
            // string name = context.Request["name"].ToString();
            //string time = "2014-01";


            //List<UnitInfo> infoList = new List<UnitInfo>();
            //UnitInfo info = new UnitInfo();
            //info.I_FOT = 3;
            //info.T_BEGINTIME = DateTime.Now;
            //infoList.Add(info);


            List<infos> infoList = new List<infos>();

            string times = string.Empty;
           
                //当前月份
                times += String.IsNullOrEmpty(time) ? string.Empty : time + "-01";
            
                //上年年份的当前月份
                string ln = String.IsNullOrEmpty(times) ? string.Empty : DateTime.Parse(times).AddYears(1).ToString("yyyy-MM-dd");

                //当前月份的上个月份
                string dt = String.IsNullOrEmpty(times) ? string.Empty : DateTime.Parse(times).AddMonths(1).ToString("yyyy-MM-dd");

                //当年一月
                string bn = String.IsNullOrEmpty(times) ? string.Empty : DateTime.Parse(times).Year.ToString() + "-01-01";

                //上年一月
                string lbn = String.IsNullOrEmpty(times) ? string.Empty : DateTime.Parse(bn).AddYears(-1).ToString("yyyy-MM-dd");
            





            //起始月份
            DataTable dts = new DataTable();
            dts = bl.GetInitByCondition(times, out errMsg);

            //临近年份
            DataTable dtln = new DataTable();
            dtln = bl.GetInitByCondition(ln, out errMsg);

            //临近月份
            DataTable dtl = new DataTable();
            dtl = bl.GetInitByCondition(dt, out errMsg);

            //从一月开始
            DataTable oneMonth = new DataTable();
            oneMonth = bl.GetInitByCondition(bn, times, out errMsg);

            //上年从一月开始
            DataTable lastOneMonth = new DataTable();
            lastOneMonth = bl.GetInitByCondition(lbn, ln, out errMsg);



            infos tmp = new infos();

            //起始月份
            //int i = 0;
            infos qs = new infos();
            qs.cTime = 0;
            if (dts != null)
            {
                qs.cTime = String.IsNullOrEmpty(dts.Rows[0]["fot"].ToString()) ? 0 : Convert.ToInt32(dts.Rows[0]["fot"].ToString());
                qs.sj = String.IsNullOrEmpty(dts.Rows[0]["foh"].ToString()) ? 0 : Convert.ToDouble(dts.Rows[0]["foh"].ToString());

            }
            qs.sTime = String.IsNullOrEmpty(times) ? string.Empty : times.Remove(7) + "月";

            infoList.Add(qs);


            //临近年度
            infos lj = new infos();
            lj.cTime = 0;
            if (dtln != null)
            {
                lj.cTime = String.IsNullOrEmpty(dtln.Rows[0]["fot"].ToString()) ? 0 : Convert.ToInt32(dtln.Rows[0]["fot"].ToString());
                lj.sj = String.IsNullOrEmpty(dtln.Rows[0]["foh"].ToString()) ? 0 : Convert.ToDouble(dtln.Rows[0]["foh"].ToString());

            }
            lj.sTime = String.IsNullOrEmpty(ln) ? string.Empty : ln.Remove(7) + "月";
            infoList.Add(lj);


            //临近月份
            infos ly = new infos();
            ly.cTime = 0;

            if (dtl != null)
            {
                ly.cTime = String.IsNullOrEmpty(dtl.Rows[0]["fot"].ToString()) ? 0 : Convert.ToInt32(dtl.Rows[0]["fot"].ToString());
                ly.sj = String.IsNullOrEmpty(dtl.Rows[0]["foh"].ToString()) ? 0 : Convert.ToDouble(dtl.Rows[0]["foh"].ToString());

            }
            ly.sTime = String.IsNullOrEmpty(dt) ? string.Empty : dt.Remove(7) + "月";
            infoList.Add(ly);

            //从一月开始
            infos yy = new infos();
            yy.cTime = 0;
            if (oneMonth != null)
            {
                yy.cTime = String.IsNullOrEmpty(oneMonth.Rows[0]["fot"].ToString()) ? 0 : Convert.ToInt32(oneMonth.Rows[0]["fot"].ToString());
                yy.sj = String.IsNullOrEmpty(oneMonth.Rows[0]["foh"].ToString()) ? 0 : Convert.ToDouble(oneMonth.Rows[0]["foh"].ToString());

            }
            yy.sTime = (String.IsNullOrEmpty(bn) ? string.Empty : bn.Remove(7)) + "月-" + (String.IsNullOrEmpty(times) ? string.Empty : times.Remove(0, 7)) + "月";
            infoList.Add(yy);

            //上年从一月开始
            infos syy = new infos();
            syy.cTime = 0;
            if (lastOneMonth != null)
            {
                syy.cTime = String.IsNullOrEmpty(lastOneMonth.Rows[0]["fot"].ToString()) ? 0 : Convert.ToInt32(lastOneMonth.Rows[0]["fot"].ToString());
                syy.sj = String.IsNullOrEmpty(lastOneMonth.Rows[0]["foh"].ToString()) ? 0 : Convert.ToDouble(lastOneMonth.Rows[0]["foh"].ToString());

            }
            syy.sTime = (String.IsNullOrEmpty(lbn) ? string.Empty : lbn.Remove(7)) + "月-" + (String.IsNullOrEmpty(ln) ? string.Empty : ln.Remove(0, 7)) + "月";
            infoList.Add(syy);

            //饼状图（按容量分类）
            ArrayList capability = new ArrayList();
            DataTable dtCapability = bl.GetInitByCapality(times, out errMsg);


            if (dtCapability != null)
            {

                foreach (DataRow dr in dtCapability.Rows)
                {
                    ArrayList tmsp = new ArrayList();
                    tmsp.Add((String.IsNullOrEmpty(dr["D_CAPABILITY"].ToString()) ? "0" : dr["D_CAPABILITY"].ToString()) + "MV");
                    tmsp.Add(String.IsNullOrEmpty(dr["FOH"].ToString()) ? 0 : Convert.ToInt32(dr["FOH"].ToString()));
                    //tmsp.Add(10);

                    capability.Add(tmsp);
                }
            }
            //for (int i = 0; i < 10; i++)
            //{
            //    ArrayList tmsp = new ArrayList();

            //    tmsp.Add("容量分类");
            //    tmsp.Add(10);

            //    capability.Add(tmsp);
            //}

            //饼状图（按专业分类）
            ArrayList profession = new ArrayList();
            DataTable dtProfession = bl.GetInitByProfession(times, out errMsg);
            if (dtProfession != null)
            {
                foreach (DataRow dr in dtProfession.Rows)
                {
                    ArrayList tmsp = new ArrayList();
                    tmsp.Add(String.IsNullOrEmpty(dr["T_PROFESSIONALDESC"].ToString()) ? "Empty" : dr["T_PROFESSIONALDESC"].ToString());
                    tmsp.Add(String.IsNullOrEmpty(dr["FOH"].ToString()) ? 0 : Convert.ToInt32(dr["FOH"].ToString()));
                    //tmsp.Add(10);

                    profession.Add(tmsp);
                }

            }
            //for (int i = 0; i < 10; i++)
            //{
            //    ArrayList tmsp = new ArrayList();

            //    tmsp.Add("专业分类");
            //    tmsp.Add(10);

            //    profession.Add(tmsp);
            //}

            //饼状图（按故障原因分类）
            ArrayList reason = new ArrayList();
            DataTable dtReason = bl.GetInitByReason(times, out errMsg);
            if (dtProfession != null)
            {
                foreach (DataRow dr in dtReason.Rows)
                {
                    ArrayList tmsp = new ArrayList();
                    tmsp.Add(String.IsNullOrEmpty(dr["T_REASONDESC"].ToString()) ? "Empty" : dr["T_REASONDESC"].ToString());
                    tmsp.Add(String.IsNullOrEmpty(dr["FOH"].ToString()) ? 0 : Convert.ToInt32(dr["FOH"].ToString()));

                    reason.Add(tmsp);
                }
            }
            //for (int i = 0; i < 10; i++)
            //{
            //    ArrayList tmsp = new ArrayList();

            //    tmsp.Add("故障原因");
            //    tmsp.Add(10);

            //    reason.Add(tmsp);
            //}

            AllInfo allInfo = new AllInfo();
            allInfo.infoList = infoList;
            allInfo.plotList = capability;
            allInfo.professionList = profession;
            allInfo.reasonList = reason;

            string content = allInfo.ToJsonItem();

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
    public class infos
    {
        /// <summary>
        /// 强迫停运次数。
        /// </summary>
        public int cTime { set; get; }
        /// <summary>
        /// 强迫停运时间。
        /// </summary>
        public double sj { set; get; }
        /// <summary>
        /// 时间轴。
        /// </summary>
        public string sTime { set; get; }

    }

    /// <summary>
    /// 饼状图类
    /// </summary>
    public class plot
    {
        /// <summary>
        /// 次数
        /// </summary>
        public int count { set; get; }

        /// <summary>
        /// 分类描述
        /// </summary>
        public string desc { set; get; }
    }

    public class AllInfo
    {
        //柱状图
        public List<infos> infoList { set; get; }

        //按容量分析 饼状图
        public ArrayList plotList { set; get; }

        //按专业分类 饼状图
        public ArrayList professionList { set; get; }

        //按故障原因分类 饼状图
        public ArrayList reasonList { set; get; }
    }
}