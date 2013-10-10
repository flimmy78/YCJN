using System;
using System.Data;
using Microsoft.Office.Interop.Word;
using SAC.Helper;
using BLL.Report;
using Entity.Statistic;
using System.Collections.Generic;
using System.Linq;
using Entity.Report;
using System.Linq.Expressions;
using SAC.JScript;
using Newtonsoft.Json;
using System.Collections;
using System.IO;
using System.IO.Ports;
using System.Configuration;

namespace DJXT.ReportManage
{
    public partial class ConsumeReport : System.Web.UI.Page
    {
        public Report report = new Report();
        DateHelper dh = new DateHelper();
        string errMsg = string.Empty;
        BLLReport bl = new BLLReport();

        protected void Page_Load(object sender, EventArgs e)
        {
            string param = Request["param"];
            if (param != "")
            {
                if (param == "seachList")
                {
                    GetBCList();

                }
            }
        }

        public void GetBCList()
        {
            string time = Request["time"].ToString();
            //int page = Convert.ToInt32(Request["page"].ToString());
            //int rows = Convert.ToInt32(Request["rows"].ToString());
            //int sCount = (page - 1) * rows + 1;
            //int eCount = page * rows;
            //int count = 0;
            //DataTable dt = bl.GetInitByCondition( beginTime, endTime, sCount, eCount, out count, out errMsg);
            //int count = dt.Rows.Count;
            IList<Hashtable> list = new List<Hashtable>();

            string path = Server.MapPath("/ReportManage/doc/");
            //if (File.Exists(path))
            //{
            //string[] pathName;
            List<string> name=new List<string> ();
            //pathName = Directory.GetFiles(path, time + ".doc");


            name = Directory.GetFiles(path).Where(p => Path.GetExtension(p).ToLower() == ".doc").ToList();
            
            List<string> doc = new List<string>();
            //foreach (string p in name)
            //{
            //    //Stream ss= File.Open(p, FileMode.Open);
            //    FileInfo s = new FileInfo(p);
            //    name.Add(s.ToString());
            //}
            int id = 0;
            if (!String.IsNullOrEmpty(time))
            {
                name.Where(info => info.Contains(time)).FirstOrDefault();
            }
           
            foreach (string p in name)
            {
                string tmp = Path.GetFileName(p);
                id++;
                Hashtable ht = new Hashtable();
                ht.Add("id", id);
                ht.Add("name", tmp);
                ht.Add("path","doc/"+tmp);
                list.Add(ht);
            }
            int count = list.Count;
            
            object obj = new
            {
                total = count,
                rows = list
            };

            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMonth.Value))
            {
                JScript.Alert("请选择月份!");
                return;
            }
            //报表截图存放位置
            //string ReportImgPath = ConfigurationSettings.AppSettings["ReportImg"].ToString();
            string ReportImgPath = Server.MapPath("/ReportManage/img/").ToString();
            DateTime dt = DateTime.Parse(txtMonth.Value.Trim() + "-01");
            string RepPath = Server.MapPath("/ReportManage/doc/") + dt.ToString("yyyy-MM") + ".doc";

            string time1 = dt.GetDateTimeFormats('D')[0].ToString();

            string time2_1 = dh.GetFirstDayOfMonth(dt).GetDateTimeFormats('M')[0].ToString();
            string time2_2 = dh.GetLastDayOfMonth(dt).GetDateTimeFormats('M')[0].ToString();
            string beginTime = dh.GetFirstDayOfMonth(dt).ToString("yyyy-MM-dd 00:00:00");
            string endTime = dh.GetLastDayOfMonth(dt).ToString("yyyy-MM-dd 00:00:00");
            string beginBeforTime = dh.GetFirstDayOfMonth(dt.AddMonths(-1)).ToString("yyyy-MM-dd 00:00:00");
            string endBeforTime = dh.GetLastDayOfMonth(dt.AddMonths(-1)).ToString("yyyy-MM-dd 00:00:00");
            string time2 = time2_1 + "~" + time2_2;
            string time4 = dt.Month.ToString() + "月";
            string time5 = dt.AddMonths(-1).Month.ToString() + "月";

            // 1,首先需要载入模板
            Report report = new Report();
            report.CreateNewDocument(Server.MapPath("../ReportManage/Report.doc")); //模板路径

            //2,插入一个值
            report.InsertValue("time2", time2);//在书签“time1”处插入值
            report.InsertValue("time3", time4);//在书签“time1”处插入值
            report.InsertValue("time4", time4);//在书签“time1”处插入值
            report.InsertValue("time4_1", time4);//在书签“time1”处插入值
            report.InsertValue("time4_2", time4);//在书签“time1”处插入值
            report.InsertValue("time4_3", time4);//在书签“time1”处插入值
            report.InsertValue("time4_4", time4);//在书签“time1”处插入值
            report.InsertValue("time4_5", time4);//在书签“time1”处插入值
            report.InsertValue("time4_6", time4);//在书签“time1”处插入值
            report.InsertValue("time4_7", time4);//在书签“time1”处插入值
            report.InsertValue("time4_8", time4);//在书签“time1”处插入值
            report.InsertValue("time4_9", time4);//在书签“time1”处插入值
            report.InsertValue("time4_10", time4);//在书签“time1”处插入值
            report.InsertValue("time4_11", time4);//在书签“time1”处插入值
            report.InsertValue("time4_12", time4);//在书签“time1”处插入值
            report.InsertValue("time4_13", time4);//在书签“time1”处插入值
            report.InsertValue("time5", time5);//在书签“time1”处插入值
            report.InsertValue("time5_1", time5);//在书签“time1”处插入值

            //当月数据
            List<ConsumeInfo> infoList = new List<ConsumeInfo>();
            infoList = bl.get(beginTime, endTime, out errMsg);
            double allCount = Math.Round(infoList.Select(info => info.Count).Sum(), 3);
            //表一 集团数据
            double a = infoList.Where(info => info.Name == "主蒸汽温度(℃)").FirstOrDefault().Count;
            double a1 = infoList.Where(info => info.Name == "主蒸汽压力(MPa)").FirstOrDefault().Count;
            double a2 = infoList.Where(info => info.Name == "再热温度(℃)").FirstOrDefault().Count;
            double a4 = infoList.Where(info => info.Name == "再热压损(%)").FirstOrDefault().Count;
            double a5 = infoList.Where(info => info.Name == "凝汽器压力(kPa)").FirstOrDefault() == null ? 0 : infoList.Where(info => info.Name == "凝汽器压力(kPa)").FirstOrDefault().Count;
            double a6 = infoList.Where(info => info.Name == "过热减温水流量(t/h)").FirstOrDefault().Count;
            double a7 = infoList.Where(info => info.Name == "再热减温水流量(t/h)").FirstOrDefault().Count;
            double a8 = infoList.Where(info => info.Name == "补水率(%)").FirstOrDefault().Count;
            double b = infoList.Where(info => info.Name == "锅炉连续排污流量(t/h)").FirstOrDefault().Count;
            double b1 = infoList.Where(info => info.Name == "排烟温度(℃)").FirstOrDefault().Count;
            double b2 = infoList.Where(info => info.Name == "排烟氧量(%)").FirstOrDefault().Count;
            double b3 = infoList.Where(info => info.Name == "飞灰含碳量(%)").FirstOrDefault() == null ? 0 : infoList.Where(info => info.Name == "飞灰含碳量(%)").FirstOrDefault().Count;
            double b4 = infoList.Where(info => info.Name == "凝汽器过冷度(℃)").FirstOrDefault().Count;
            double b5 = infoList.Where(info => info.Name == "凝汽器压力(kPa)").FirstOrDefault().Count;
            double b6 = infoList.Where(info => info.Name == "小机用汽量(t/h)").FirstOrDefault().Count;
            double b7 = infoList.Where(info => info.Name == "#1高加上端差(℃)").FirstOrDefault().Count;
            double b8 = infoList.Where(info => info.Name == "#2高加上端差(℃)").FirstOrDefault().Count;
            double c = infoList.Where(info => info.Name == "#3高加上端差(℃)").FirstOrDefault().Count;
            double c1 = infoList.Where(info => info.Name == "#5低加上端差(℃)").FirstOrDefault().Count;
            double c2 = infoList.Where(info => info.Name == "#6低加上端差(℃)").FirstOrDefault().Count;
            double c3 = infoList.Where(info => info.Name == "#7低加上端差(℃)").FirstOrDefault().Count;
            double c4 = infoList.Where(info => info.Name == "#8低加上端差(℃)").FirstOrDefault().Count;
            double c5 = infoList.Where(info => info.Name == "高压缸效率(%)").FirstOrDefault().Count;
            double c6 = infoList.Where(info => info.Name == "中压缸效率(%)").FirstOrDefault().Count;
            double c7 = infoList.Where(info => info.Name == "锅炉效率(%)").FirstOrDefault() == null ? 0 : infoList.Where(info => info.Name == "锅炉效率(%)").FirstOrDefault().Count;

            report.InsertCell(1, 2, 2, Math.Round(allCount, 3).ToString());
            report.InsertCell(1, 2, 3, Math.Round(a, 3).ToString());
            report.InsertCell(1, 2, 4, Math.Round(a1, 3).ToString());
            report.InsertCell(1, 2, 5, Math.Round(a2, 3).ToString());
            report.InsertCell(1, 2, 6, Math.Round(a4, 3).ToString());
            report.InsertCell(1, 2, 7, Math.Round(a5, 3).ToString());
            report.InsertCell(1, 2, 8, Math.Round(a6, 3).ToString());
            report.InsertCell(1, 2, 9, Math.Round(a7, 3).ToString());
            report.InsertCell(1, 2, 10, Math.Round(a8, 3).ToString());
            //第五行                    
            report.InsertCell(1, 5, 2, Math.Round(b, 3).ToString());
            report.InsertCell(1, 5, 3, Math.Round(b1, 3).ToString());
            report.InsertCell(1, 5, 4, Math.Round(b2, 3).ToString());
            report.InsertCell(1, 5, 5, Math.Round(b3, 3).ToString());
            report.InsertCell(1, 5, 6, Math.Round(b4, 3).ToString());
            report.InsertCell(1, 5, 7, Math.Round(b5, 3).ToString());
            report.InsertCell(1, 5, 8, Math.Round(b6, 3).ToString());
            report.InsertCell(1, 5, 9, Math.Round(b7, 3).ToString());
            report.InsertCell(1, 5, 10, Math.Round(b8, 3).ToString());
            //第八行                    
            report.InsertCell(1, 8, 2, Math.Round(c, 3).ToString());
            report.InsertCell(1, 8, 3, Math.Round(c1, 3).ToString());
            report.InsertCell(1, 8, 4, Math.Round(c2, 3).ToString());
            report.InsertCell(1, 8, 5, Math.Round(c3, 3).ToString());
            report.InsertCell(1, 8, 6, Math.Round(c4, 3).ToString());
            report.InsertCell(1, 8, 7, Math.Round(c5, 3).ToString());
            report.InsertCell(1, 8, 8, Math.Round(c6, 3).ToString());
            report.InsertCell(1, 8, 9, Math.Round(c7, 3).ToString());

            //表一  分项比例
            report.InsertCell(1, 3, 2, "100");
            report.InsertCell(1, 3, 3, Math.Round(((a / allCount) * 100), 2).ToString());
            report.InsertCell(1, 3, 4, Math.Round(((a1 / allCount) * 100), 2).ToString());
            report.InsertCell(1, 3, 5, Math.Round(((a2 / allCount) * 100), 2).ToString());
            report.InsertCell(1, 3, 6, Math.Round(((a4 / allCount) * 100), 2).ToString());
            report.InsertCell(1, 3, 7, Math.Round(((a5 / allCount) * 100), 2).ToString());
            report.InsertCell(1, 3, 8, Math.Round(((a6 / allCount) * 100), 2).ToString());
            report.InsertCell(1, 3, 9, Math.Round(((a7 / allCount) * 100), 2).ToString());
            report.InsertCell(1, 3, 10, Math.Round(((a8 / allCount) * 100), 2).ToString());
            //第六行                    Math.Round(                        
            report.InsertCell(1, 6, 2, Math.Round(((b / allCount) * 100), 2).ToString());
            report.InsertCell(1, 6, 3, Math.Round(((b1 / allCount) * 100), 2).ToString());
            report.InsertCell(1, 6, 4, Math.Round(((b2 / allCount) * 100), 2).ToString());
            report.InsertCell(1, 6, 5, Math.Round(((b3 / allCount) * 100), 2).ToString());
            report.InsertCell(1, 6, 6, Math.Round(((b4 / allCount) * 100), 2).ToString());
            report.InsertCell(1, 6, 7, Math.Round(((b5 / allCount) * 100), 2).ToString());
            report.InsertCell(1, 6, 8, Math.Round(((b6 / allCount) * 100), 2).ToString());
            report.InsertCell(1, 6, 9, Math.Round(((b7 / allCount) * 100), 2).ToString());
            report.InsertCell(1, 6, 10, Math.Round(((b8 / allCount) * 100), 2).ToString());
            //第九行
            report.InsertCell(1, 9, 2, Math.Round(((c / allCount) * 100), 2).ToString());
            report.InsertCell(1, 9, 3, Math.Round(((c1 / allCount) * 100), 2).ToString());
            report.InsertCell(1, 9, 4, Math.Round(((c2 / allCount) * 100), 2).ToString());
            report.InsertCell(1, 9, 5, Math.Round(((c3 / allCount) * 100), 2).ToString());
            report.InsertCell(1, 9, 6, Math.Round(((c4 / allCount) * 100), 2).ToString());
            report.InsertCell(1, 9, 7, Math.Round(((c5 / allCount) * 100), 2).ToString());
            report.InsertCell(1, 9, 8, Math.Round(((c6 / allCount) * 100), 2).ToString());
            report.InsertCell(1, 9, 9, Math.Round(((c7 / allCount) * 100), 2).ToString());


            report.InsertValue("data", allCount.ToString());//在书签“time1”处插入值
            report.InsertValue("data1_1", allCount.ToString());//在书签“time1”处插入值
            double maxValue = -9999;
            string maxName = string.Empty;
            string maxUnit = string.Empty;
            foreach (var s in infoList)
            {
                if (s.Count > maxValue)
                {
                    maxValue = s.Count;
                    maxName = s.Name;
                }
            }
            report.InsertValue("data2", Math.Round(maxValue, 3).ToString());//在书签“time1”处插入值
            report.InsertValue("name1", maxName);//在书签“time1”处插入值
            report.InsertValue("data2_1", Math.Round((maxValue / allCount), 2).ToString());//在书签“time1”处插入值

            //第五个表（用第一个表的数据）
            //集团平均
            report.InsertCell(5, 3, 2, Math.Round(a, 3).ToString());
            report.InsertCell(5, 3, 3, Math.Round(a1, 3).ToString());
            report.InsertCell(5, 3, 4, Math.Round(a2, 3).ToString());
            report.InsertCell(5, 3, 5, Math.Round(a4, 3).ToString());
            report.InsertCell(5, 3, 6, Math.Round(a5, 3).ToString());
            report.InsertCell(5, 3, 7, Math.Round(a6, 3).ToString());
            report.InsertCell(5, 3, 8, Math.Round(a7, 3).ToString());
            report.InsertCell(5, 3, 9, Math.Round(a8, 3).ToString());
            report.InsertCell(5, 3, 10, Math.Round(b, 3).ToString());
            //第五行           5                     , 3)
            report.InsertCell(5, 8, 2, Math.Round(b1, 3).ToString());
            report.InsertCell(5, 8, 3, Math.Round(b2, 3).ToString());
            report.InsertCell(5, 8, 4, Math.Round(b3, 3).ToString());
            report.InsertCell(5, 8, 5, Math.Round(b4, 3).ToString());
            report.InsertCell(5, 8, 6, Math.Round(b5, 3).ToString());
            report.InsertCell(5, 8, 7, Math.Round(b6, 3).ToString());
            report.InsertCell(5, 8, 8, Math.Round(b7, 3).ToString());
            report.InsertCell(5, 8, 9, Math.Round(b8, 3).ToString());
            report.InsertCell(5, 8, 10, Math.Round(c, 3).ToString());

            //第八行                     
            report.InsertCell(5, 13, 2, Math.Round(c1, 3).ToString());
            report.InsertCell(5, 13, 3, Math.Round(c2, 3).ToString());
            report.InsertCell(5, 13, 4, Math.Round(c3, 3).ToString());
            report.InsertCell(5, 13, 5, Math.Round(c4, 3).ToString());
            report.InsertCell(5, 13, 6, Math.Round(c5, 3).ToString());
            report.InsertCell(5, 13, 7, Math.Round(c6, 3).ToString());
            report.InsertCell(5, 13, 8, Math.Round(c7, 3).ToString());
            //上月数据
            List<ConsumeInfo> infoLists = new List<ConsumeInfo>();
            infoLists = bl.get(beginBeforTime, endBeforTime, out errMsg);
            double allCounts = infoLists.Select(info => info.Count).Sum();

            //第二个表 当与数据
            report.InsertCell(2, 2, 1, time4);
            report.InsertCell(2, 3, 1, time5);
            report.InsertCell(2, 4, 1, time4 + "环比" + time5);
            report.InsertCell(2, 6, 1, time4);
            report.InsertCell(2, 7, 1, time5);
            report.InsertCell(2, 8, 1, time4 + "环比" + time5);
            report.InsertCell(2, 10, 1, time4);
            report.InsertCell(2, 11, 1, time5);
            report.InsertCell(2, 12, 1, time4 + "环比" + time5);

            report.InsertCell(2, 2, 2, Math.Round(allCount, 3).ToString());
            report.InsertCell(2, 2, 3, Math.Round(a, 3).ToString());
            report.InsertCell(2, 2, 4, Math.Round(a1, 3).ToString());
            report.InsertCell(2, 2, 5, Math.Round(a2, 3).ToString());
            report.InsertCell(2, 2, 6, Math.Round(a4, 3).ToString());
            report.InsertCell(2, 2, 7, Math.Round(a5, 3).ToString());
            report.InsertCell(2, 2, 8, Math.Round(a6, 3).ToString());
            report.InsertCell(2, 2, 9, Math.Round(a7, 3).ToString());
            report.InsertCell(2, 2, 10, Math.Round(a8, 3).ToString());
            //第六行                      
            report.InsertCell(2, 6, 2, Math.Round(b, 3).ToString());
            report.InsertCell(2, 6, 3, Math.Round(b1, 3).ToString());
            report.InsertCell(2, 6, 4, Math.Round(b2, 3).ToString());
            report.InsertCell(2, 6, 5, Math.Round(b3, 3).ToString());
            report.InsertCell(2, 6, 6, Math.Round(b4, 3).ToString());
            report.InsertCell(2, 6, 7, Math.Round(b5, 3).ToString());
            report.InsertCell(2, 6, 8, Math.Round(b6, 3).ToString());
            report.InsertCell(2, 6, 9, Math.Round(b7, 3).ToString());
            report.InsertCell(2, 6, 10, Math.Round(b8, 3).ToString());
            //第十行                      
            report.InsertCell(2, 10, 2, Math.Round(c, 3).ToString());
            report.InsertCell(2, 10, 3, Math.Round(c1, 3).ToString());
            report.InsertCell(2, 10, 4, Math.Round(c2, 3).ToString());
            report.InsertCell(2, 10, 5, Math.Round(c3, 3).ToString());
            report.InsertCell(2, 10, 6, Math.Round(c4, 3).ToString());
            report.InsertCell(2, 10, 7, Math.Round(c5, 3).ToString());
            report.InsertCell(2, 10, 8, Math.Round(c6, 3).ToString());
            report.InsertCell(2, 10, 9, Math.Round(c7, 3).ToString());

            //第二个表 上月数据
            double aa = infoLists.Where(info => info.Name == "主蒸汽温度(℃)").FirstOrDefault().Count;
            double aa1 = infoLists.Where(info => info.Name == "主蒸汽压力(MPa)").FirstOrDefault().Count;
            double aa2 = infoLists.Where(info => info.Name == "再热温度(℃)").FirstOrDefault().Count;
            double aa4 = infoLists.Where(info => info.Name == "再热压损(%)").FirstOrDefault().Count;
            double aa5 = infoLists.Where(info => info.Name == "凝汽器压力(kPa)").FirstOrDefault() == null ? 0 : infoLists.Where(info => info.Name == "凝汽器压力(kPa)").FirstOrDefault().Count;
            double aa6 = infoLists.Where(info => info.Name == "过热减温水流量(t/h)").FirstOrDefault().Count;
            double aa7 = infoLists.Where(info => info.Name == "再热减温水流量(t/h)").FirstOrDefault().Count;
            double aa8 = infoLists.Where(info => info.Name == "补水率(%)").FirstOrDefault().Count;
            double ab = infoLists.Where(info => info.Name == "锅炉连续排污流量(t/h)").FirstOrDefault().Count;
            double ab1 = infoLists.Where(info => info.Name == "排烟温度(℃)").FirstOrDefault().Count;
            double ab2 = infoLists.Where(info => info.Name == "排烟氧量(%)").FirstOrDefault().Count;
            double ab3 = infoLists.Where(info => info.Name == "飞灰含碳量(%)").FirstOrDefault() == null ? 0 : infoLists.Where(info => info.Name == "飞灰含碳量(%)").FirstOrDefault().Count;
            double ab4 = infoLists.Where(info => info.Name == "凝汽器过冷度(℃)").FirstOrDefault().Count;
            double ab5 = infoLists.Where(info => info.Name == "凝汽器压力(kPa)").FirstOrDefault().Count;
            double ab6 = infoLists.Where(info => info.Name == "小机用汽量(t/h)").FirstOrDefault().Count;
            double ab7 = infoLists.Where(info => info.Name == "#1高加上端差(℃)").FirstOrDefault().Count;
            double ab8 = infoLists.Where(info => info.Name == "#2高加上端差(℃)").FirstOrDefault().Count;
            double ac = infoLists.Where(info => info.Name == "#3高加上端差(℃)").FirstOrDefault().Count;
            double ac1 = infoLists.Where(info => info.Name == "#5低加上端差(℃)").FirstOrDefault().Count;
            double ac2 = infoLists.Where(info => info.Name == "#6低加上端差(℃)").FirstOrDefault().Count;
            double ac3 = infoLists.Where(info => info.Name == "#7低加上端差(℃)").FirstOrDefault().Count;
            double ac4 = infoLists.Where(info => info.Name == "#8低加上端差(℃)").FirstOrDefault().Count;
            double ac5 = infoLists.Where(info => info.Name == "高压缸效率(%)").FirstOrDefault().Count;
            double ac6 = infoLists.Where(info => info.Name == "中压缸效率(%)").FirstOrDefault().Count;
            double ac7 = infoLists.Where(info => info.Name == "锅炉效率(%)").FirstOrDefault() == null ? 0 : infoLists.Where(info => info.Name == "锅炉效率(%)").FirstOrDefault().Count;

            report.InsertCell(2, 3, 2, Math.Round(allCounts, 3).ToString());
            report.InsertCell(2, 3, 3, Math.Round(aa, 3).ToString());
            report.InsertCell(2, 3, 4, Math.Round(aa1, 3).ToString());
            report.InsertCell(2, 3, 5, Math.Round(aa2, 3).ToString());
            report.InsertCell(2, 3, 6, Math.Round(aa4, 3).ToString());
            report.InsertCell(2, 3, 7, Math.Round(aa5, 3).ToString());
            report.InsertCell(2, 3, 8, Math.Round(aa6, 3).ToString());
            report.InsertCell(2, 3, 9, Math.Round(aa7, 3).ToString());
            report.InsertCell(2, 3, 10, Math.Round(aa8, 3).ToString());
            //第七行                    Math.Round(
            report.InsertCell(2, 7, 2, Math.Round(ab, 3).ToString());
            report.InsertCell(2, 7, 3, Math.Round(ab1, 3).ToString());
            report.InsertCell(2, 7, 4, Math.Round(ab2, 3).ToString());
            report.InsertCell(2, 7, 5, Math.Round(ab3, 3).ToString());
            report.InsertCell(2, 7, 6, Math.Round(ab4, 3).ToString());
            report.InsertCell(2, 7, 7, Math.Round(ab5, 3).ToString());
            report.InsertCell(2, 7, 8, Math.Round(ab6, 3).ToString());
            report.InsertCell(2, 7, 9, Math.Round(ab7, 3).ToString());
            report.InsertCell(2, 7, 10, Math.Round(ab8, 3).ToString());
            //第十一行
            report.InsertCell(2, 11, 2, Math.Round(ac, 3).ToString());
            report.InsertCell(2, 11, 3, Math.Round(ac1, 3).ToString());
            report.InsertCell(2, 11, 4, Math.Round(ac2, 3).ToString());
            report.InsertCell(2, 11, 5, Math.Round(ac3, 3).ToString());
            report.InsertCell(2, 11, 6, Math.Round(ac4, 3).ToString());
            report.InsertCell(2, 11, 7, Math.Round(ac5, 3).ToString());
            report.InsertCell(2, 11, 8, Math.Round(ac6, 3).ToString());
            report.InsertCell(2, 11, 9, Math.Round(ac, 3).ToString());


            //第二个表 环比增长
            report.InsertCell(2, 4, 2, Math.Round((((allCount - allCounts) / allCounts) * 100), 2).ToString());
            report.InsertCell(2, 4, 3, Math.Round((((a - aa) / aa) * 100), 2).ToString());
            report.InsertCell(2, 4, 4, Math.Round((((a1 - aa1) / aa1) * 100), 2).ToString());
            report.InsertCell(2, 4, 5, Math.Round((((a2 - aa2) / aa2) * 100), 2).ToString());
            report.InsertCell(2, 4, 6, Math.Round((((a4 - aa4) / aa4) * 100), 2).ToString());
            report.InsertCell(2, 4, 7, Math.Round((((a5 - aa5) / aa5) * 100), 2).ToString());
            report.InsertCell(2, 4, 8, Math.Round((((a6 - aa6) / aa6) * 100), 2).ToString());
            report.InsertCell(2, 4, 9, Math.Round((((a7 - aa7) / aa7) * 100), 2).ToString());
            report.InsertCell(2, 4, 10, Math.Round((((a8 - aa8) / aa8) * 100), 2).ToString());
            //第八行                                  
            report.InsertCell(2, 8, 2, Math.Round((((b - ab) / ab) * 100), 2).ToString());
            report.InsertCell(2, 8, 3, Math.Round((((b1 - ab1) / ab1) * 100), 2).ToString());
            report.InsertCell(2, 8, 4, Math.Round((((b2 - ab2) / ab2) * 100), 2).ToString());
            report.InsertCell(2, 8, 5, Math.Round((((b3 - ab3) / ab3) * 100), 2).ToString());
            report.InsertCell(2, 8, 6, Math.Round((((b4 - ab4) / ab4) * 100), 2).ToString());
            report.InsertCell(2, 8, 7, Math.Round((((b5 - ab5) / ab5) * 100), 2).ToString());
            report.InsertCell(2, 8, 8, Math.Round((((b6 - ab6) / ab6) * 100), 2).ToString());
            report.InsertCell(2, 8, 9, Math.Round((((b7 - ab7) / ab7) * 100), 2).ToString());
            report.InsertCell(2, 8, 10, Math.Round((((b8 - ab8) / ab8) * 100), 2).ToString());
            //第十二行                                  
            report.InsertCell(2, 12, 2, Math.Round((((c - ac) / ac) * 100), 2).ToString());
            report.InsertCell(2, 12, 3, Math.Round((((c1 - ac1) / ac1) * 100), 2).ToString());
            report.InsertCell(2, 12, 4, Math.Round((((c2 - ac2) / ac2) * 100), 2).ToString());
            report.InsertCell(2, 12, 5, Math.Round((((c3 - ac3) / ac3) * 100), 2).ToString());
            report.InsertCell(2, 12, 6, Math.Round((((c4 - ac4) / ac4) * 100), 2).ToString());
            report.InsertCell(2, 12, 7, Math.Round((((c5 - ac5) / ac5) * 100), 2).ToString());
            report.InsertCell(2, 12, 8, Math.Round((((c6 - ac6) / ac6) * 100), 2).ToString());
            report.InsertCell(2, 12, 9, Math.Round((((c7 - ac7) / ac7) * 100), 2).ToString());

            string data3 = string.Empty;
            double tmpData3 = allCounts - allCount;
            if (tmpData3 > 0)
            {
                data3 = "上升" + Math.Round(tmpData3, 3).ToString();
            }
            else if (tmpData3 == 0)
            {
                data3 = "无改变";

            }
            else
            {
                data3 = "下降" + Math.Round((allCount - allCounts), 3).ToString();
            }
            report.InsertValue("data3", data3);//在书签“time1”处插入值



            string data4 = string.Empty;
            string dataDown = string.Empty;
            for (int i = 0; i < infoList.Count; i++)
            {
                ConsumeInfo info = new ConsumeInfo();
                info = infoLists.Where(j => j.Name == infoList[i].Name).FirstOrDefault();
                if (info.Count > infoList[i].Count)
                {
                    data4 += info.Name + "下降" + Math.Round((info.Count - infoList[i].Count), 3).ToString() + ",";
                }
                else if (info.Count < infoList[i].Count)
                {
                    dataDown += info.Name + "上升" + Math.Round((infoList[i].Count - info.Count), 3).ToString() + ",";
                }
            }
            if (data4.Length > 0)
            {
                data4.Remove(data4.Length - 1, 1);
            }
            if (dataDown.Length > 0)
            {
                dataDown.Remove(dataDown.Length - 1, 1);
            }
            report.InsertValue("dataDown", data4);//在书签“time1”处插入值
            report.InsertValue("data4", dataDown);//在书签“time1”处插入值


            //第三张表
            List<ReportInfo> reportInfo = new List<ReportInfo>();
            reportInfo = bl.GetCompanyInfo(beginTime, endTime, out errMsg);
            //集团平均值总耗差
            double companyValue = reportInfo.Select(info => info.ParaValue).Sum();
            double ta1 = reportInfo.Where(info => info.ParaDesc == "主蒸汽温度(℃)").FirstOrDefault().ParaValue;
            double ta2 = reportInfo.Where(info => info.ParaDesc == "主蒸汽压力(MPa)").FirstOrDefault().ParaValue;
            double ta3 = reportInfo.Where(info => info.ParaDesc == "再热温度(℃)").FirstOrDefault().ParaValue;
            double ta4 = reportInfo.Where(info => info.ParaDesc == "再热压损(%)").FirstOrDefault().ParaValue;
            double ta5 = reportInfo.Where(info => info.ParaDesc == "凝汽器压力(kPa)").FirstOrDefault() == null ? 0 : reportInfo.Where(info => info.ParaDesc == "凝汽器压力(kPa)").FirstOrDefault().ParaValue;
            double ta6 = reportInfo.Where(info => info.ParaDesc == "过热减温水流量(t/h)").FirstOrDefault().ParaValue;
            double ta7 = reportInfo.Where(info => info.ParaDesc == "再热减温水流量(t/h)").FirstOrDefault() == null ? 0 : reportInfo.Where(info => info.ParaDesc == "再热减温水流量(t/h)").FirstOrDefault().ParaValue;
            double ta8 = reportInfo.Where(info => info.ParaDesc == "补水率(%)").FirstOrDefault().ParaValue;
            double ta9 = reportInfo.Where(info => info.ParaDesc == "锅炉连续排污流量(t/h)").FirstOrDefault().ParaValue;
            double tb1 = reportInfo.Where(info => info.ParaDesc == "排烟温度(℃)").FirstOrDefault().ParaValue;
            double tb2 = reportInfo.Where(info => info.ParaDesc == "排烟氧量(%)").FirstOrDefault().ParaValue;
            double tb3 = reportInfo.Where(info => info.ParaDesc == "飞灰含碳量(%)").FirstOrDefault() == null ? 0 : reportInfo.Where(info => info.ParaDesc == "飞灰含碳量(%)").FirstOrDefault().ParaValue;
            double tb4 = reportInfo.Where(info => info.ParaDesc == "凝汽器过冷度(℃)").FirstOrDefault().ParaValue;
            double tb5 = reportInfo.Where(info => info.ParaDesc == "凝汽器压力(kPa)").FirstOrDefault().ParaValue;
            double tb6 = reportInfo.Where(info => info.ParaDesc == "小机用汽量(t/h)").FirstOrDefault().ParaValue;
            double tb7 = reportInfo.Where(info => info.ParaDesc == "#1高加上端差(℃)").FirstOrDefault().ParaValue;
            double tb8 = reportInfo.Where(info => info.ParaDesc == "#2高加上端差(℃)").FirstOrDefault().ParaValue;
            double tb9 = reportInfo.Where(info => info.ParaDesc == "#3高加上端差(℃)").FirstOrDefault() == null ? 0 : reportInfo.Where(info => info.ParaDesc == "#3高加上端差(℃)").FirstOrDefault().ParaValue;
            double tc1 = reportInfo.Where(info => info.ParaDesc == "#5低加上端差(℃)").FirstOrDefault().ParaValue;
            double tc2 = reportInfo.Where(info => info.ParaDesc == "#6低加上端差(℃)").FirstOrDefault().ParaValue;
            double tc3 = reportInfo.Where(info => info.ParaDesc == "#7低加上端差(℃)").FirstOrDefault().ParaValue;
            double tc4 = reportInfo.Where(info => info.ParaDesc == "#8低加上端差(℃)").FirstOrDefault().ParaValue;
            double tc5 = reportInfo.Where(info => info.ParaDesc == "高压缸效率(%)").FirstOrDefault().ParaValue;
            double tc6 = reportInfo.Where(info => info.ParaDesc == "中压缸效率(%)").FirstOrDefault().ParaValue;
            double tc7 = reportInfo.Where(info => info.ParaDesc == "锅炉效率(%)").FirstOrDefault() == null ? 0 : reportInfo.Where(info => info.ParaDesc == "锅炉效率(%)").FirstOrDefault().ParaValue;

            //第二行
            report.InsertCell(3, 2, 3, Math.Round(companyValue, 3).ToString());
            report.InsertCell(3, 2, 4, Math.Round(ta1, 3).ToString());
            report.InsertCell(3, 2, 5, Math.Round(ta2, 3).ToString());
            report.InsertCell(3, 2, 6, Math.Round(ta3, 3).ToString());
            report.InsertCell(3, 2, 7, Math.Round(ta4, 3).ToString());
            report.InsertCell(3, 2, 8, Math.Round(ta5, 3).ToString());
            report.InsertCell(3, 2, 9, Math.Round(ta6, 3).ToString());
            report.InsertCell(3, 2, 10, Math.Round(ta7, 3).ToString());
            report.InsertCell(3, 2, 11, Math.Round(ta8, 3).ToString());



            List<ReportInfo> reportUnitInfo = new List<ReportInfo>();
            List<ReportInfo> tmpUnitList = new List<ReportInfo>();
            reportUnitInfo = bl.GetByTime(beginTime, endTime, out errMsg);
            ////取大于2的机组列表
            ////先分组 再求平均
            //// tmpUnitList = reportUnitInfo.GroupBy(info => info.UnitId).SelectMany(info => info.Where(s => s.ParaValue < 2).Select(j=>new ReportInfo{  UnitId=j.UnitId})).ToList();
            //var tmptList = reportUnitInfo.GroupBy(info => info.UnitId).ToList();

            //取不重复的unitid
            List<ReportInfo> tmpR = reportUnitInfo.Distinct(new EqualCompare<ReportInfo>((x, y) => (x != null && y != null) && (x.UnitId == y.UnitId))).ToList();
            //获取小于2的机组
            List<ReportInfo> tmtList = new List<ReportInfo>();

            foreach (var s in tmpR)
            {
                var v = reportUnitInfo.Where(info => info.UnitId == s.UnitId).ToList().Sum(es => es.ParaValue);
                if (v < 6)
                {
                    tmpUnitList.Add(s);
                }
            }
            //tmpUnitList = reportUnitInfo.Average(info=>info.ParaValue).
            //var tmpss=from s in reportUnitInfo
            //         group s by s.UnitId into d
            //         select new ReportInfo{ ParaDesc=d.pa
            int tmpCount = tmpUnitList.Count();
            report.InsertValue("unitCount", tmpCount + "台");//在书签“time1”处插入值
            //表插入行
            report.AddRow(3, tmpCount, "table3_1"); //在书签“table3_1”处插入tmpCount行
            report.AddRow(3, tmpCount, "table3_2"); //在书签“table3_1”处插入tmpCount行
            report.AddRow(3, tmpCount, "table3_3"); //在书签“table3_1”处插入tmpCount行

            string unitName = string.Empty;
            foreach (var j in tmpUnitList)
            {
                unitName += j.UnitName + ","; ;
            }
            report.InsertValue("unitName", unitName);//在书签“time1”处插入值

            //第六行                    
            report.InsertCell(3, 4 + tmpCount, 3, Math.Round(ta9, 3).ToString());
            report.InsertCell(3, 4 + tmpCount, 4, Math.Round(tb1, 3).ToString());
            report.InsertCell(3, 4 + tmpCount, 5, Math.Round(tb2, 3).ToString());
            report.InsertCell(3, 4 + tmpCount, 6, Math.Round(tb3, 3).ToString());
            report.InsertCell(3, 4 + tmpCount, 7, Math.Round(tb4, 3).ToString());
            report.InsertCell(3, 4 + tmpCount, 8, Math.Round(tb5, 3).ToString());
            report.InsertCell(3, 4 + tmpCount, 9, Math.Round(tb6, 3).ToString());
            report.InsertCell(3, 4 + tmpCount, 10, Math.Round(tb7, 3).ToString());
            report.InsertCell(3, 4 + tmpCount, 11, Math.Round(tb8, 3).ToString());

            //第十行         
            report.InsertCell(3, 6 + tmpCount, 3, Math.Round(tb9, 3).ToString());
            report.InsertCell(3, 6 + tmpCount, 4, Math.Round(tc1, 3).ToString());
            report.InsertCell(3, 6 + tmpCount, 5, Math.Round(tc2, 3).ToString());
            report.InsertCell(3, 6 + tmpCount, 6, Math.Round(tc3, 3).ToString());
            report.InsertCell(3, 6 + tmpCount, 7, Math.Round(tc4, 3).ToString());
            report.InsertCell(3, 6 + tmpCount, 8, Math.Round(tc5, 3).ToString());
            report.InsertCell(3, 6 + tmpCount, 9, Math.Round(tc6, 3).ToString());
            report.InsertCell(3, 6 + tmpCount, 10, Math.Round(tc7, 3).ToString());

            int t = 0;
            foreach (var i in tmpUnitList)
            {
                t++;
                List<ReportInfo> tmp = new List<ReportInfo>();
                tmp = reportUnitInfo.Where(info => info.UnitId == i.UnitId).ToList();
                double tmpC = Math.Round(tmp.Select(info => info.ParaValue).Sum(), 3);
                //符合要求的每台机组的数据插入表中
                report.InsertCell(3, 2 + t, 1, (t + 1).ToString());
                report.InsertCell(3, 2 + t, 2, i.UnitName);

                report.InsertCell(3, 2 + t, 3, tmpC.ToString());
                report.InsertCell(3, 2 + t, 4, tmp.Where(info => info.ParaDesc == "主蒸汽温度(℃)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "主蒸汽温度(℃)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(3, 2 + t, 5, tmp.Where(info => info.ParaDesc == "主蒸汽压力(MPa)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "主蒸汽压力(MPa)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(3, 2 + t, 6, tmp.Where(info => info.ParaDesc == "再热温度(℃)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "再热温度(℃)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(3, 2 + t, 7, tmp.Where(info => info.ParaDesc == "再热压损(%)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "再热压损(%)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(3, 2 + t, 8, tmp.Where(info => info.ParaDesc == "凝汽器压力(kPa)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "凝汽器压力(kPa)").FirstOrDefault().ParaValue, 3).ToString());

                report.InsertCell(3, 2 + t, 9, tmp.Where(info => info.ParaDesc == "过热减温水流量(t/h)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "过热减温水流量(t/h)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(3, 2 + t, 10, tmp.Where(info => info.ParaDesc == "再热减温水流量(t/h)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "再热减温水流量(t/h)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(3, 2 + t, 11, tmp.Where(info => info.ParaDesc == "补水率(%)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "补水率(%)").FirstOrDefault().ParaValue, 3).ToString());
                //第八行
                report.InsertCell(3, 4 + tmpCount + t, 1, (t + 1).ToString());
                report.InsertCell(3, 4 + tmpCount + t, 2, i.UnitName);
                report.InsertCell(3, 4 + tmpCount + t, 3, tmp.Where(info => info.ParaDesc == "锅炉连续排污流量(t/h)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "锅炉连续排污流量(t/h)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(3, 4 + tmpCount + t, 4, tmp.Where(info => info.ParaDesc == "排烟温度(℃)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "排烟温度(℃)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(3, 4 + tmpCount + t, 5, tmp.Where(info => info.ParaDesc == "排烟氧量(%)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "排烟氧量(%)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(3, 4 + tmpCount + t, 6, tmp.Where(info => info.ParaDesc == "飞灰含碳量(%)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "飞灰含碳量(%)").FirstOrDefault().ParaValue, 3).ToString());

                report.InsertCell(3, 4 + tmpCount + t, 7, tmp.Where(info => info.ParaDesc == "凝汽器过冷度(℃)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "凝汽器过冷度(℃)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(3, 4 + tmpCount + t, 8, tmp.Where(info => info.ParaDesc == "凝汽器压力(kPa)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "凝汽器压力(kPa)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(3, 4 + tmpCount + t, 9, tmp.Where(info => info.ParaDesc == "小机用汽量(t/h)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "小机用汽量(t/h)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(3, 4 + tmpCount + t, 10, tmp.Where(info => info.ParaDesc == "#1高加上端差(℃)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "#1高加上端差(℃)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(3, 4 + tmpCount + t, 11, tmp.Where(info => info.ParaDesc == "#2高加上端差(℃)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "#2高加上端差(℃)").FirstOrDefault().ParaValue, 3).ToString());
                //第十二行
                report.InsertCell(3, 6 + 2 * tmpCount + t, 1, (t + 1).ToString());
                report.InsertCell(3, 6 + 2 * tmpCount + t, 2, i.UnitName);
                report.InsertCell(3, 6 + 2 * tmpCount + t, 3, tmp.Where(info => info.ParaDesc == "#3高加上端差(℃)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "#3高加上端差(℃)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(3, 6 + 2 * tmpCount + t, 4, tmp.Where(info => info.ParaDesc == "#5低加上端差(℃)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "#5低加上端差(℃)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(3, 6 + 2 * tmpCount + t, 5, tmp.Where(info => info.ParaDesc == "#6低加上端差(℃)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "#6低加上端差(℃)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(3, 6 + 2 * tmpCount + t, 6, tmp.Where(info => info.ParaDesc == "#7低加上端差(℃)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "#7低加上端差(℃)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(3, 6 + 2 * tmpCount + t, 7, tmp.Where(info => info.ParaDesc == "#8低加上端差(℃)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "#8低加上端差(℃)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(3, 6 + 2 * tmpCount + t, 8, tmp.Where(info => info.ParaDesc == "高压缸效率(%)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "高压缸效率(%)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(3, 6 + 2 * tmpCount + t, 9, tmp.Where(info => info.ParaDesc == "中压缸效率(%)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "中压缸效率(%)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(3, 6 + 2 * tmpCount + t, 10, tmp.Where(info => info.ParaDesc == "锅炉效率(%)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "锅炉效率(%)").FirstOrDefault().ParaValue, 3).ToString());

            }

            //第四张表
            report.InsertCell(4, 2, 3, Math.Round(companyValue, 3).ToString());
            report.InsertCell(4, 2, 4, Math.Round(ta1, 3).ToString());
            report.InsertCell(4, 2, 5, Math.Round(ta2, 3).ToString());
            report.InsertCell(4, 2, 6, Math.Round(ta3, 3).ToString());
            report.InsertCell(4, 2, 7, Math.Round(ta4, 3).ToString());
            report.InsertCell(4, 2, 8, Math.Round(ta5, 3).ToString());
            report.InsertCell(4, 2, 9, Math.Round(ta6, 3).ToString());
            report.InsertCell(4, 2, 10, Math.Round(ta7, 3).ToString());
            report.InsertCell(4, 2, 11, Math.Round(ta8, 3).ToString());

            List<ReportInfo> fourUnitList = new List<ReportInfo>();

            foreach (var s in tmpR)
            {
                var v = reportUnitInfo.Where(info => info.UnitId == s.UnitId).ToList().Sum(es => es.ParaValue);
                if (v > 10)
                {
                    fourUnitList.Add(s);
                }
            }
            //fourUnitList = reportUnitInfo.GroupBy(info => info.UnitId).SelectMany(infos => infos.Where(j => j.ParaValue > 10)).ToList();
            int tmpFourCount = fourUnitList.Count;
            report.InsertValue("unitCount2", tmpFourCount + "台");//在书签“time1”处插入值
            //表插入行
            report.AddRow(4, tmpFourCount, "table4_1"); //在书签“table3_1”处插入tmpCount行
            report.AddRow(4, tmpFourCount, "table4_2"); //在书签“table3_1”处插入tmpCount行
            report.AddRow(4, tmpFourCount, "table4_3"); //在书签“table3_1”处插入tmpCount行

            string unitFourName = string.Empty;
            foreach (var j in fourUnitList)
            {
                unitFourName += j.UnitName + ","; ;
            }
            report.InsertValue("unitName2", unitFourName);//在书签“time1”处插入值

            //第六行                    
            report.InsertCell(4, 4 + tmpFourCount, 3, Math.Round(ta9, 3).ToString());
            report.InsertCell(4, 4 + tmpFourCount, 4, Math.Round(tb1, 3).ToString());
            report.InsertCell(4, 4 + tmpFourCount, 5, Math.Round(tb2, 3).ToString());
            report.InsertCell(4, 4 + tmpFourCount, 6, Math.Round(tb3, 3).ToString());
            report.InsertCell(4, 4 + tmpFourCount, 7, Math.Round(tb4, 3).ToString());
            report.InsertCell(4, 4 + tmpFourCount, 8, Math.Round(tb5, 3).ToString());
            report.InsertCell(4, 4 + tmpFourCount, 9, Math.Round(tb6, 3).ToString());
            report.InsertCell(4, 4 + tmpFourCount, 10, Math.Round(tb7, 3).ToString());
            report.InsertCell(4, 4 + tmpFourCount, 11, Math.Round(tb8, 3).ToString());
            //第十行           4      
            report.InsertCell(4, 6 + tmpFourCount, 3, Math.Round(tb9, 3).ToString());
            report.InsertCell(4, 6 + tmpFourCount, 4, Math.Round(tc1, 3).ToString());
            report.InsertCell(4, 6 + tmpFourCount, 5, Math.Round(tc2, 3).ToString());
            report.InsertCell(4, 6 + tmpFourCount, 6, Math.Round(tc3, 3).ToString());
            report.InsertCell(4, 6 + tmpFourCount, 7, Math.Round(tc4, 3).ToString());
            report.InsertCell(4, 6 + tmpFourCount, 8, Math.Round(tc5, 3).ToString());
            report.InsertCell(4, 6 + tmpFourCount, 9, Math.Round(tc6, 3).ToString());
            report.InsertCell(4, 6 + tmpFourCount, 10, Math.Round(tc7, 3).ToString());

            int f = 0;
            foreach (var i in fourUnitList)
            {
                f++;
                List<ReportInfo> tmp = new List<ReportInfo>();
                tmp = reportUnitInfo.Where(info => info.UnitId == i.UnitId).ToList();
                double tmpC = Math.Round(tmp.Select(info => info.ParaValue).Sum(), 3);
                //符合要求的每台机组的数据插入表中
                report.InsertCell(4, 2 + f, 1, (f + 1).ToString());
                report.InsertCell(4, 2 + f, 2, i.UnitName);
                report.InsertCell(4, 2 + f, 3, tmpC.ToString());
                report.InsertCell(4, 2 + f, 4, Math.Round(tmp.Where(info => info.ParaDesc == "主蒸汽温度(℃)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(4, 2 + f, 5, Math.Round(tmp.Where(info => info.ParaDesc == "主蒸汽压力(MPa)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(4, 2 + f, 6, Math.Round(tmp.Where(info => info.ParaDesc == "再热温度(℃)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(4, 2 + f, 7, Math.Round(tmp.Where(info => info.ParaDesc == "再热压损(%)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(4, 2 + f, 8, tmp.Where(info => info.ParaDesc == "凝汽器压力(kPa)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "凝汽器压力(kPa)").FirstOrDefault().ParaValue, 3).ToString());

                report.InsertCell(4, 2 + f, 9, Math.Round(tmp.Where(info => info.ParaDesc == "过热减温水流量(t/h)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(4, 2 + f, 10, Math.Round(tmp.Where(info => info.ParaDesc == "再热减温水流量(t/h)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(4, 2 + f, 11, Math.Round(tmp.Where(info => info.ParaDesc == "补水率(%)").FirstOrDefault().ParaValue, 3).ToString());
                //第八行                   
                report.InsertCell(4, 4 + tmpFourCount + f, 1, (f + 1).ToString());
                report.InsertCell(4, 4 + tmpFourCount + f, 2, i.UnitName);
                report.InsertCell(4, 4 + tmpFourCount + f, 3, Math.Round(tmp.Where(info => info.ParaDesc == "锅炉连续排污流量(t/h)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(4, 4 + tmpFourCount + f, 4, Math.Round(tmp.Where(info => info.ParaDesc == "排烟温度(℃)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(4, 4 + tmpFourCount + f, 5, Math.Round(tmp.Where(info => info.ParaDesc == "排烟氧量(%)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(4, 4 + tmpFourCount + f, 6, tmp.Where(info => info.ParaDesc == "飞灰含碳量(%)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "飞灰含碳量(%)").FirstOrDefault().ParaValue, 3).ToString());

                report.InsertCell(4, 4 + tmpFourCount + f, 7, Math.Round(tmp.Where(info => info.ParaDesc == "凝汽器过冷度(℃)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(4, 4 + tmpFourCount + f, 8, Math.Round(tmp.Where(info => info.ParaDesc == "凝汽器压力(kPa)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(4, 4 + tmpFourCount + f, 9, tmp.Where(info => info.ParaDesc == "小机用汽量(t/h)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "小机用汽量(t/h)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(4, 4 + tmpFourCount + f, 10, Math.Round(tmp.Where(info => info.ParaDesc == "#1高加上端差(℃)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(4, 4 + tmpFourCount + f, 11, Math.Round(tmp.Where(info => info.ParaDesc == "#2高加上端差(℃)").FirstOrDefault().ParaValue, 3).ToString());
                //第十二行          
                report.InsertCell(4, 6 + 2 * tmpFourCount + f, 1, (f + 1).ToString());
                report.InsertCell(4, 6 + 2 * tmpFourCount + f, 2, i.UnitName);
                report.InsertCell(4, 6 + 2 * tmpFourCount + f, 3, Math.Round(tmp.Where(info => info.ParaDesc == "#3高加上端差(℃)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(4, 6 + 2 * tmpFourCount + f, 4, Math.Round(tmp.Where(info => info.ParaDesc == "#5低加上端差(℃)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(4, 6 + 2 * tmpFourCount + f, 5, Math.Round(tmp.Where(info => info.ParaDesc == "#6低加上端差(℃)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(4, 6 + 2 * tmpFourCount + f, 6, Math.Round(tmp.Where(info => info.ParaDesc == "#7低加上端差(℃)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(4, 6 + 2 * tmpFourCount + f, 7, Math.Round(tmp.Where(info => info.ParaDesc == "#8低加上端差(℃)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(4, 6 + 2 * tmpFourCount + f, 8, Math.Round(tmp.Where(info => info.ParaDesc == "高压缸效率(%)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(4, 6 + 2 * tmpFourCount + f, 9, Math.Round(tmp.Where(info => info.ParaDesc == "中压缸效率(%)").FirstOrDefault().ParaValue, 3).ToString());
                report.InsertCell(4, 6 + 2 * tmpFourCount + f, 10, tmp.Where(info => info.ParaDesc == "锅炉效率(%)").FirstOrDefault() == null ? string.Empty : Math.Round(tmp.Where(info => info.ParaDesc == "锅炉效率(%)").FirstOrDefault().ParaValue, 3).ToString());

            }


            //第五个表 
            //各个参数最大值的机组
            List<ReportInfo> unitParaList = new List<ReportInfo>();
            unitParaList = bl.GetUnitParaInfo(beginTime, endTime, out errMsg);
            ReportInfo info1 = unitParaList.Where(info => info.ParaDesc == "主蒸汽温度(℃)").OrderByDescending(j => j.ParaValue).FirstOrDefault();
            ReportInfo info2 = unitParaList.Where(info => info.ParaDesc == "主蒸汽压力(MPa)").OrderByDescending(j => j.ParaValue).FirstOrDefault();
            ReportInfo info3 = unitParaList.Where(info => info.ParaDesc == "再热温度(℃)").OrderByDescending(j => j.ParaValue).FirstOrDefault();
            ReportInfo info4 = unitParaList.Where(info => info.ParaDesc == "再热压损(%)").OrderByDescending(j => j.ParaValue).FirstOrDefault();
            ReportInfo info5 = unitParaList.Where(info => info.ParaDesc == "凝汽器压力(kPa)").OrderByDescending(j => j.ParaValue).FirstOrDefault() == null ? new ReportInfo() : unitParaList.Where(info => info.ParaDesc == "凝汽器压力(kPa)").OrderByDescending(j => j.ParaValue).FirstOrDefault();
            ReportInfo info6 = unitParaList.Where(info => info.ParaDesc == "过热减温水流量(t/h)").OrderByDescending(j => j.ParaValue).FirstOrDefault();
            ReportInfo info7 = unitParaList.Where(info => info.ParaDesc == "再热减温水流量(t/h)").OrderByDescending(j => j.ParaValue).FirstOrDefault();
            ReportInfo info8 = unitParaList.Where(info => info.ParaDesc == "补水率(%)").OrderByDescending(j => j.ParaValue).FirstOrDefault();
            ReportInfo info9 = unitParaList.Where(info => info.ParaDesc == "锅炉连续排污流量(t/h)").OrderByDescending(j => j.ParaValue).FirstOrDefault();
            ReportInfo info10 = unitParaList.Where(info => info.ParaDesc == "排烟温度(℃)").OrderByDescending(j => j.ParaValue).FirstOrDefault();
            ReportInfo info11 = unitParaList.Where(info => info.ParaDesc == "排烟氧量(%)").OrderByDescending(j => j.ParaValue).FirstOrDefault();
            ReportInfo info12 = unitParaList.Where(info => info.ParaDesc == "飞灰含碳量(%)").OrderByDescending(j => j.ParaValue).FirstOrDefault() == null ? new ReportInfo() : unitParaList.Where(info => info.ParaDesc == "飞灰含碳量(%)").OrderByDescending(j => j.ParaValue).FirstOrDefault();
            ReportInfo info13 = unitParaList.Where(info => info.ParaDesc == "凝汽器过冷度(℃)").OrderByDescending(j => j.ParaValue).FirstOrDefault();
            ReportInfo info14 = unitParaList.Where(info => info.ParaDesc == "凝汽器压力(kPa)").OrderByDescending(j => j.ParaValue).FirstOrDefault();
            ReportInfo info15 = unitParaList.Where(info => info.ParaDesc == "小机用汽量(t/h)").OrderByDescending(j => j.ParaValue).FirstOrDefault();
            ReportInfo info16 = unitParaList.Where(info => info.ParaDesc == "#1高加上端差(℃)").OrderByDescending(j => j.ParaValue).FirstOrDefault();
            ReportInfo info17 = unitParaList.Where(info => info.ParaDesc == "#2高加上端差(℃)").OrderByDescending(j => j.ParaValue).FirstOrDefault();
            ReportInfo info18 = unitParaList.Where(info => info.ParaDesc == "#3高加上端差(℃)").OrderByDescending(j => j.ParaValue).FirstOrDefault();
            ReportInfo info19 = unitParaList.Where(info => info.ParaDesc == "#5低加上端差(℃)").OrderByDescending(j => j.ParaValue).FirstOrDefault();
            ReportInfo info20 = unitParaList.Where(info => info.ParaDesc == "#6低加上端差(℃)").OrderByDescending(j => j.ParaValue).FirstOrDefault();
            ReportInfo info21 = unitParaList.Where(info => info.ParaDesc == "#7低加上端差(℃)").OrderByDescending(j => j.ParaValue).FirstOrDefault();
            ReportInfo info22 = unitParaList.Where(info => info.ParaDesc == "#8低加上端差(℃)").OrderByDescending(j => j.ParaValue).FirstOrDefault();
            ReportInfo info23 = unitParaList.Where(info => info.ParaDesc == "高压缸效率(%)").OrderByDescending(j => j.ParaValue).FirstOrDefault();
            ReportInfo info24 = unitParaList.Where(info => info.ParaDesc == "中压缸效率(%)").OrderByDescending(j => j.ParaValue).FirstOrDefault();
            ReportInfo info25 = unitParaList.Where(info => info.ParaDesc == "锅炉效率(%)").OrderByDescending(j => j.ParaValue).FirstOrDefault() == null ? new ReportInfo() : unitParaList.Where(info => info.ParaDesc == "锅炉效率(%)").OrderByDescending(j => j.ParaValue).FirstOrDefault();

            //单位及机组
            report.InsertCell(5, 2, 2, info1  == null ? string.Empty : info1.UnitName.ToString());
            report.InsertCell(5, 2, 3, info2  == null ? string.Empty : info2.UnitName.ToString());
            report.InsertCell(5, 2, 4, info3 == null ? string.Empty : info3.UnitName.ToString());
            report.InsertCell(5, 2, 5, info4 == null ? string.Empty : info4.UnitName.ToString());
            report.InsertCell(5, 2, 6, info5 == null ? string.Empty : info5.UnitName.ToString());
            report.InsertCell(5, 2, 7, info6 == null ? string.Empty : info6.UnitName.ToString());
            report.InsertCell(5, 2, 8, info7 == null ? string.Empty : info7.UnitName.ToString());
            report.InsertCell(5, 2, 9, info8 == null ? string.Empty : info8.UnitName.ToString());
            report.InsertCell(5, 2, 10, info9  == null ? string.Empty : info9.UnitName.ToString());
            //第五行           5          
            report.InsertCell(5, 7, 2, info10  == null ? string.Empty : info10.UnitName.ToString());
            report.InsertCell(5, 7, 3, info11  == null ? string.Empty : info11.UnitName.ToString());
            report.InsertCell(5, 7, 4, info12  == null ? string.Empty : info12.UnitName.ToString());
            report.InsertCell(5, 7, 5, info13  == null ? string.Empty : info13.UnitName.ToString());
            report.InsertCell(5, 7, 6, info14 == null ? string.Empty : info14.UnitName.ToString());
            report.InsertCell(5, 7, 7, info15 == null ? string.Empty : info15.UnitName.ToString());
            report.InsertCell(5, 7, 8, info16 == null ? string.Empty : info16.UnitName.ToString());
            report.InsertCell(5, 7, 9, info17 == null ? string.Empty : info17.UnitName.ToString());
            report.InsertCell(5, 7, 10, info18  == null ? string.Empty : info18.UnitName.ToString());
            //第八行                      
            report.InsertCell(5, 12, 2, info19  == null ? string.Empty : info19.UnitName.ToString());
            report.InsertCell(5, 12, 3, info20  == null ? string.Empty : info20.UnitName.ToString());
            report.InsertCell(5, 12, 4, info21  == null ? string.Empty : info21.UnitName.ToString());
            report.InsertCell(5, 12, 5, info22  == null ? string.Empty : info22.UnitName.ToString());
            report.InsertCell(5, 12, 6, info23  == null ? string.Empty : info23.UnitName.ToString());
            report.InsertCell(5, 12, 7, info24  == null ? string.Empty : info24.UnitName.ToString());
            report.InsertCell(5, 12, 8, info25  == null ? string.Empty : info25.UnitName == null ? string.Empty : info25.UnitName.ToString());
            //偏离最大值
            report.InsertCell(5, 4, 2, info1 == null ? string.Empty : Math.Round(info1.ParaValue, 3).ToString());
            report.InsertCell(5, 4, 3, info2 == null ? string.Empty : Math.Round(info2.ParaValue, 3).ToString());
            report.InsertCell(5, 4, 4, info3 == null ? string.Empty : Math.Round(info3.ParaValue, 3).ToString());
            report.InsertCell(5, 4, 5, info4 == null ? string.Empty : Math.Round(info4.ParaValue, 3).ToString());
            report.InsertCell(5, 4, 6, info5 == null ? string.Empty : Math.Round(info5.ParaValue, 3).ToString());
            report.InsertCell(5, 4, 7, info6 == null ? string.Empty : Math.Round(info6.ParaValue, 3).ToString());
            report.InsertCell(5, 4, 8, info7 == null ? string.Empty : Math.Round(info7.ParaValue, 3).ToString());
            report.InsertCell(5, 4, 9, info8 == null ? string.Empty : Math.Round(info8.ParaValue, 3).ToString());
            report.InsertCell(5, 4, 10, info9 == null ? string.Empty : Math.Round(info9.ParaValue, 3).ToString());
            //第五行           5          
            report.InsertCell(5, 9, 2, info10 == null ? string.Empty : Math.Round(info10.ParaValue, 3).ToString());
            report.InsertCell(5, 9, 3, info11 == null ? string.Empty : Math.Round(info11.ParaValue, 3).ToString());
            report.InsertCell(5, 9, 4, info12 == null ? string.Empty : Math.Round(info12.ParaValue, 3).ToString());
            report.InsertCell(5, 9, 5, info13 == null ? string.Empty : Math.Round(info13.ParaValue, 3).ToString());
            report.InsertCell(5, 9, 6, info14 == null ? string.Empty : Math.Round(info14.ParaValue, 3).ToString());
            report.InsertCell(5, 9, 7, info15 == null ? string.Empty : Math.Round(info15.ParaValue, 3).ToString());
            report.InsertCell(5, 9, 8, info16 == null ? string.Empty : Math.Round(info16.ParaValue, 3).ToString());
            report.InsertCell(5, 9, 9, info17 == null ? string.Empty : Math.Round(info17.ParaValue, 3).ToString());
            report.InsertCell(5, 9, 10, info18 == null ? string.Empty : Math.Round(info18.ParaValue, 3).ToString());

            //第八行                     
            report.InsertCell(5, 14, 2, info19 == null ? string.Empty : Math.Round(info19.ParaValue, 3).ToString());
            report.InsertCell(5, 14, 3, info20 == null ? string.Empty : Math.Round(info20.ParaValue, 3).ToString());
            report.InsertCell(5, 14, 4, info21 == null ? string.Empty : Math.Round(info21.ParaValue, 3).ToString());
            report.InsertCell(5, 14, 5, info22 == null ? string.Empty : Math.Round(info22.ParaValue, 3).ToString());
            report.InsertCell(5, 14, 6, info23 == null ? string.Empty : Math.Round(info23.ParaValue, 3).ToString());
            report.InsertCell(5, 14, 7, info24 == null ? string.Empty : Math.Round(info24.ParaValue, 3).ToString());
            report.InsertCell(5, 14, 8, info25 == null ? string.Empty : Math.Round(info25.ParaValue, 3).ToString());
            //偏差值
            report.InsertCell(5, 5, 2, Math.Round((info1.ParaValue - a), 3).ToString());
            report.InsertCell(5, 5, 3, Math.Round((info2.ParaValue - a1), 3).ToString());
            report.InsertCell(5, 5, 4, info3 == null ? Math.Round((-a2), 3).ToString() : Math.Round((info3.ParaValue - a2), 3).ToString());
            report.InsertCell(5, 5, 5, Math.Round((info4.ParaValue - a4), 3).ToString());
            report.InsertCell(5, 5, 6, info5 == null ? Math.Round((-a5), 3).ToString() : Math.Round((info5.ParaValue - a5), 3).ToString());
            report.InsertCell(5, 5, 7, Math.Round((info6.ParaValue - a6), 3).ToString());
            report.InsertCell(5, 5, 8, Math.Round((info7.ParaValue - a7), 3).ToString());
            report.InsertCell(5, 5, 9, Math.Round((info8.ParaValue - a8), 3).ToString());
            report.InsertCell(5, 5, 10, Math.Round((info9.ParaValue - b), 3).ToString());
            //第五行           5          
            report.InsertCell(5, 10, 2, Math.Round((info10.ParaValue - b1), 3).ToString());
            report.InsertCell(5, 10, 3, Math.Round((info11.ParaValue - b2), 3).ToString());
            report.InsertCell(5, 10, 4, info12 == null ? Math.Round((-b3), 3).ToString() : Math.Round((info12.ParaValue - b3), 3).ToString());
            report.InsertCell(5, 10, 5, Math.Round((info13.ParaValue - b4), 3).ToString());
            report.InsertCell(5, 10, 6, info3 == null ? Math.Round((-b5), 3).ToString() : Math.Round((info14.ParaValue - b5), 3).ToString());
            report.InsertCell(5, 10, 7, Math.Round((info15.ParaValue - b6), 3).ToString());
            report.InsertCell(5, 10, 8, Math.Round((info16.ParaValue - b7), 3).ToString());
            report.InsertCell(5, 10, 9, Math.Round((info17.ParaValue - b8), 3).ToString());
            report.InsertCell(5, 10, 10, Math.Round((info18.ParaValue - c), 3).ToString());

            //第八行                     
            report.InsertCell(5, 15, 2, Math.Round((info19.ParaValue - c1), 3).ToString());
            report.InsertCell(5, 15, 3, Math.Round((info20.ParaValue - c2), 3).ToString());
            report.InsertCell(5, 15, 4, Math.Round((info21.ParaValue - c3), 3).ToString());
            report.InsertCell(5, 15, 5, Math.Round((info22.ParaValue - c4), 3).ToString());
            report.InsertCell(5, 15, 6, Math.Round((info23.ParaValue - c5), 3).ToString());
            report.InsertCell(5, 15, 7, Math.Round((info24.ParaValue - c6), 3).ToString());
            report.InsertCell(5, 15, 8, info25 == null ? Math.Round((-c7), 3).ToString() : Math.Round((info25.ParaValue - c7), 3).ToString());
           
            
            //插入图片
            string year = dt.Year.ToString();
            string month = dt.Month.ToString();

            string path_1 = ReportImgPath + year + month + "1.jpg";
            string path_2 = ReportImgPath + year + month + "2.jpg";
            string path_3 = ReportImgPath + year + month + "3.jpg";
            string path_4 = ReportImgPath + year + month + "4.jpg";

            report.InsertPicture("img1", path_1, 200, 200);
            report.InsertPicture("img2", path_2, 200, 200);
            report.InsertPicture("img3", path_3, 200, 200);
            report.InsertPicture("img4", path_4, 200, 200);
            
            //List<ParaTableInfo> threeInfoList = new List<ParaTableInfo>();
            //List<ParaTableInfo> tmp = threeInfoList.Where(infos => infos.UnitId == infoList[i].UnitId).ToList();
            //foreach (var tmpInfo in tmp)
            //{
            //    threeInfoList.Remove(tmpInfo);
            //}
             
            //report.InsertValue("data5", "再热蒸汽减温水下降0.111 g/(kW.h)，再热蒸汽温度0.111g/(kW.h)、排烟氧量下降0.1115g/(kW.h)，主蒸汽温度降低0.04g/(kW.h)");//在书签“time1”处插入值


            ////string[] val;// { "1.1", "1.11", "1.111", "1.1111", "1.1111", "1.1111", "1.1111", "1.1111", "1.1111" };
            ////string[] val1;//= { "1.1%", "1.11%", "1.1115", "1.1111%", "1.1111%", "1.1111%", "1.1111%", "1.1111%", "1.1111%" };


            ////for (int i = 0; i < val.Length;i++)
            ////{
            ////    report.InsertCell(1, 2, 2+i, val[i]); 
            ////}
            ////for (int i = 0; i < val1.Length; i++)
            ////{
            ////    report.InsertCell(1, 3, 2 + i, val1[i]); 
            ////}

            //string img1 = Server.MapPath("../ReportManage/IMG_1405.JPG");
            //report.InsertPicture("img1", img1, 150, 150); //书签位置，图片路径，图片宽度，图片高度
            //report.InsertValue("name1", "再热温度(℃)(℃)");//在书签“time1”处插入值
            //report.InsertValue("unitCount", "1台");//在书签“time1”处插入值
            //report.InsertValue("unitCount2", "11台");//在书签“time1”处插入值
            //report.InsertValue("unitName", "望亭#14，望亭#14望亭#14");//在书签“time1”处插入值
            //report.InsertValue("unitName2", "望亭#14望亭#14");//在书签“time1”处插入值

            ////3,创建一个表格
            //Microsoft.Office.Interop.Word.Table table = report.InsertTable("BookMark_ConsumeTable", 4, 10, 0); //在书签“Bookmark_table”处插入2行3列行宽最大的表

            ////4，合并单元格
            ////report.MergeCell(table, 1, 1, 1, 3); //表名,开始行号,开始列号,结束行号,结束列号

            ////5,表格添加一行
            //report.AddRow(table); //表名

            ////6,在单元格中插入值
            //report.InsertCell(table, 2, 1, "测试");//表名,行号,列号,值

            ////7,设置表格中文字的对齐方式
            //report.SetParagraph_Table(table, -1, 0);//水平方向左对齐，垂直方向居中对齐

            ////8,设置表格字体
            //report.SetFont_Table(table, "宋体", 9);//宋体9磅

            ////9,给现有的表格添加一行
            ////report.AddRow(1); //给模板中第一个表格添加一行

            ////10,确定现有的表格是否使用边框
            //report.UseBorder(1, true); //模板中第一个表格使用实线边框

            ////11,给现有的表格添加多行
            ////report.AddRow(1, 2); //给模板中第一个表格插入2行

            ////12,给现有的表格插入一行数据
            //string[] values = { "英超", "意甲", "德甲", "西甲", "法甲" };
            //report.InsertCell(1, 2, 5, values); 

            ////12,插入图片
            //string picturePath = Server.MapPath("../ReportManage/IMG_1405.JPG");
            //report.InsertPicture("BookMark_Pic", picturePath, 150, 150); //书签位置，图片路径，图片宽度，图片高度

            ////13,插入一段文字
            //string text = "长期从事电脑操作者，应多吃一些新鲜的蔬菜和水果，同时增加维生素A、B1、C、E的摄入。为预防角膜干燥、眼干涩、视力下降、甚至出现夜盲等，电 脑操作者应多吃富含维生素A的食物，如豆制品、鱼、牛奶、核桃、青菜、大白菜、空心菜、西红柿及新鲜水果等。";
            ////report.InsertText("Bookmark_text", text);

            //14,最后保存文档
            report.SaveDocument(RepPath); //文档路径


            Response.Write("<script language='javascript'>alert('生成模板成功!')</script>");

        }


        public void tab(Application appWord, Document doc)
        {

            //Microsoft.Office.Interop.Word.Application appWord = null;//应用程序

            // doc = null;//文档


            appWord = new Microsoft.Office.Interop.Word.Application();



            appWord.Visible = false;



            object objTrue = true;



            object objFalse = false;


            object objTemplate = Server.MapPath("person.dot");//模板路径

            object objDocType = WdDocumentType.wdTypeDocument;


            doc = (Document)appWord.Documents.Add(ref objTemplate, ref objFalse, ref objDocType, ref objTrue);


            //第一步生成word文档
            //定义书签变量


            object obDD_Name = "bm_Name";//


            object obDD_Sex = "bm_Sex";//


            object obDD_Birthday = "bm_Birthday"; //

            object obpic = "pic";

            object obtable = "obtable";


            object Nothing = System.Reflection.Missing.Value;

            InlineShape shape = appWord.Selection.InlineShapes.AddPicture(Server.MapPath("../ReportManage/IMG_1405.JPG"), ref Nothing, ref Nothing, ref Nothing);



            //第二步读取数据，填充数据集

            System.Data.DataTable dt = new DataTable();



            dt.Columns.Add("p_Name");



            dt.Columns.Add("p_Sex");



            dt.Columns.Add("p_Birthday");



            DataRow dr = dt.NewRow();



            dr["p_Name"] = "张三";



            dr["p_Sex"] = "男";



            dr["p_Birthday"] = "1980-01-01";



            dt.Rows.Add(dr);


            //第三步
            //给书签赋值

            doc.Bookmarks.get_Item(ref obDD_Name).Range.Text = dt.Rows[0]["p_Name"].ToString(); //


            doc.Bookmarks.get_Item(ref obDD_Sex).Range.Text = dt.Rows[0]["p_Sex"].ToString();//


            doc.Bookmarks.get_Item(ref obDD_Birthday).Range.Text =

            dt.Rows[0]["p_Birthday"].ToString();//


            doc.Bookmarks.get_Item(ref obpic).Range.InlineShapes.AddPicture(Server.MapPath("../ReportManage/IMG_1405.JPG"), ref Nothing, ref Nothing, ref Nothing);

            //文档中插入表格

            doc.Bookmarks.get_Item(ref obtable).Range.Tables.Add(doc.Bookmarks.get_Item(ref obtable).Range, 12, 3, ref Nothing, ref Nothing);



            Microsoft.Office.Interop.Word.Table newTable = doc.Tables.Add(doc.Bookmarks.get_Item(ref obtable).Range, 12, 3, ref Nothing, ref Nothing);

            newTable.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;

            newTable.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;

            //给文档的最后一行再添加内容
            doc.Paragraphs.Last.Range.Text = "";


            //第四步生成word
            object filename = @"C:ss.doc";



            object miss = System.Reflection.Missing.Value;



            doc.SaveAs(ref filename, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);



            object missingValue = Type.Missing;



            object doNotSaveChanges = WdSaveOptions.wdDoNotSaveChanges;



            doc.Close(ref doNotSaveChanges, ref missingValue, ref missingValue);



            appWord.Application.Quit(ref miss, ref miss, ref miss);



            doc = null;



            appWord = null;


        }
    }


}