using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Text;
using System.Collections;
using System.Data;
using Entity.Statistic;
using BLL.StatisticalComparison;
using SAC.Helper;
using SAC.Json;

namespace DJXT.StatisticalComparison
{
    public partial class CompanyConsume : System.Web.UI.Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    //先根据paraId，取出所有paraId的outtable,循环outtable,取平均值。


        //}
        BLLCompanyConsume bl = new BLLCompanyConsume();
        string errMsg = string.Empty;
        DateHelper dh = new DateHelper();



        protected void Page_Load(object sender, EventArgs e)
        {
            string param = Request["param"];

            string time = Request["beginTime"];

            //是否截图
            string type = Request["type"];

            if (param == "query")
            {
                this.QueryData();
            }

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty("1"))
                {
                    //this.QueryData("2013-09", endTime, "1", quarterType);

                    txtMonth.Value = time;
                   //根据不同的type来截取不同的图片,滚动到不同的位置

                    switch (type)
                    { 
                        case "1":

                            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "updateScript", "Scroll('" + 100 + "');", true);
                            
                            break;

                        case "2":
                            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "updateScript", "Scroll('" +450 + "');", true);

                            break;
                        case "3":
                            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "updateScript", "Scroll('" + 1100 + "');", true);

                            break;
                    }
                }
                    //this.qsrq.Value = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd H:00:00");
                //this.jsrq.Value = DateTime.Now.AddHours(-1).ToString("yyyy-MM-dd H:00:00");
            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            //Bind();
        }

        protected void Export_Click(object sender, EventArgs e)
        {
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {

        }

        private void QueryData()
        {

            string beginTime = Request["beginTime"] == null ? string.Empty : Request["beginTime"].ToString();
            string endTime = string.Empty;
            string timeType = Request["timeType"] == null ? string.Empty : Request["timeType"].ToString();
            string quarterType = Request["quarterType"] == null ? string.Empty : Request["quarterType"].ToString();

            //根据选择的时间段，设置quarterType开始时间和结束时间
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

            //先根据paraId，取出所有paraId的outtable,循环outtable,取平均值。
            AllInfo allInfo = new AllInfo();
            ArrayList list = new ArrayList();
            IList<Hashtable> iList = new List<Hashtable>();
            Hashtable ht = null;
            List<ConsumeInfo> infoList;
            infoList = bl.get(beginTime, endTime, out errMsg);

            //记录七月份数据，供分项比例用
            List<ConsumeInfo> tmpList = new List<ConsumeInfo>();
            List<ConsumeInfo> beforInfoList;
            infos tmpZhuTu = new infos();
            double tmpCount = 0.00;
            string beforBeginTime = DateTime.Parse(beginTime).AddMonths(-1).ToString("yyyy-MM-dd HH:mm:ss");
            string beforEndTime = DateTime.Parse(beforBeginTime).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
            beforInfoList = bl.get(beforBeginTime, beforEndTime, out errMsg);

            for (int j = 0; j < 4; j++)
            {
                ht = new Hashtable();
                //总耗差
                double allCount = 0;
                //infoList = new List<ConsumeInfo>();
                switch (j)
                {
                    case 0:
                        ht.Add("记录时间", DateTime.Parse(beginTime).Month + "月份耗差平均");
                        tmpList = infoList;
                        allCount = infoList.Select(info => info.Count).Sum();
                        ht.Add("总耗差", Math.Round(allCount,4));
                        //平均值柱状图
                        ArrayList name = new ArrayList();
                        ArrayList value = new ArrayList();

                        foreach (ConsumeInfo tmp in infoList)
                        {
                            ht.Add(tmp.Name, Math.Round(tmp.Count, 3));
                            name.Add(tmp.Name);
                            value.Add(Math.Round(tmp.Count, 3));
                        }
                        tmpZhuTu.name = name;
                        tmpZhuTu.value = value;
                        tmpCount = allCount;
                        //ht.Add("总耗差", allCount);
                        iList.Add(ht);

                        break;
                    case 1:
                        ht.Add("记录时间",  DateTime.Parse(beginTime).Month +"月份分项比列");
                        ht.Add("总耗差", 100.00);
                        if (tmpCount > 0)
                        {
                            foreach (ConsumeInfo tmp in tmpList)
                            {
                                ht.Add(tmp.Name, Math.Round((tmp.Count / tmpCount) * 100, 3));
                            }
                        }
                        else
                        {
                            foreach (ConsumeInfo tmp in tmpList)
                            {
                                ht.Add(tmp.Name, 0);
                            }
                        }
                        iList.Add(ht);

                        break;
                    case 2:
                       
                        ht.Add("记录时间", DateTime.Parse(beforBeginTime).Month + "月份耗差平均");

                        allCount = beforInfoList.Select(info => info.Count).Sum();
                        ht.Add("总耗差", allCount);

                        foreach (ConsumeInfo tmp in beforInfoList)
                        {
                            ht.Add(tmp.Name, Math.Round(tmp.Count, 3));
                            // allCount += tmp.Count;

                        }
                        //ht.Add("总耗差", allCount);

                        iList.Add(ht);

                        break;
                    case 3:
                        ht.Add("记录时间", DateTime.Parse(beginTime).Month + "月份环比" + DateTime.Parse(beforBeginTime).Month + "月份增长");
                        foreach (ConsumeInfo infos in tmpList)
                        {
                            ConsumeInfo c = beforInfoList.Where(t => t.Name == infos.Name).FirstOrDefault();
                           
                            //SsHb.name.Add(c.Name);
                            if (infos.Count > 0)
                            {
                                //SsHb.value.Add(Math.Round(((infos.Count - c.Count) / infos.Count) * 100, 2));
                                ht.Add(c.Name, Math.Round(((infos.Count - c.Count) / infos.Count) * 100, 2));
                            }
                            else
                            {
                                ht.Add(c.Name,0);
                            }
                        }
                        iList.Add(ht);

                        break;
                }
            }

            string str = this.CreateDataGridColumnModel(iList);

            object obj = new
            {
                rows = iList,
                columns = str
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }

        /// <summary>  
        /// 从dataTable创建 jquery easyui datagrid格式的columns参数  
        /// </summary>  
        /// <param name="dt"></param>  
        /// <returns></returns>  
        private string CreateDataGridColumnModel(IList<Hashtable> iList)
        {
            int width = 0;
            StringBuilder columns = new StringBuilder("[[");

            if (iList.Count > 0)
            {
                Hashtable ht = iList[0];
                ArrayList list = new ArrayList(ht.Keys);
                //list.Sort();
                foreach (string skey in list)
                {
                    if (skey.ToString() == "ID_KEY") { width = 100; }
                    else
                    {
                        if (skey.Length == 0)
                        {
                            width = 50;
                        }
                        else
                        {
                            width = skey.Length * 15;
                        }
                    }
                    columns.AppendFormat("{{field:'{0}',title:'{1}',align:'center',width:{2}}},", skey, skey,150);
                }
                //foreach (System.Collections.DictionaryEntry item in ht)
                //{              
                //    if (item.Key.ToString() == "ID_KEY")
                //    {
                //        width = 100;
                //    }
                //    else
                //    {
                //        if(item.Key.ToString().Length>0)
                //            width = item.Key.ToString().Length *20;
                //        else
                //            width =100 * 20;
                //    }

                //    columns.AppendFormat("{{field:'{0}',title:'{1}',align:'center',width:{2}}},", item.Key.ToString(), item.Key.ToString(), width);
                //}
            }
            if (iList.Count > 0)
            {
                columns.Remove(columns.Length - 1, 1);//去除多余的','号  
            }
            columns.Append("]]");
            return columns.ToString();
        }

        /// <summary>  
        /// 从dataTable创建 jquery easyui datagrid格式的columns参数  
        /// </summary>  
        /// <param name="dt"></param>  
        /// <returns></returns>  
        private string CreateDataGridColumnModel(DataTable dt)
        {
            StringBuilder columns = new StringBuilder("[[");
            int width = 0;
            foreach (DataColumn col in dt.Columns)
            {
                //控制列的宽度 第一列日期宽度为139,其余列为列名的汉字长度*20px  
                if (col.ColumnName == "ID_KEY")
                {
                    width = 100;
                }
                else
                {
                    width = col.ColumnName.Length * 20;
                }
                columns.AppendFormat("{{field:'{0}',title:'{1}',align:'center',width:{2}}},", col.ColumnName, col.ColumnName, width);
            }
            if (dt.Columns.Count > 0)
            {
                columns.Remove(columns.Length - 1, 1);//去除多余的','号  
            }
            columns.Append("]]");
            return columns.ToString();
        }
    }
    public class AllInfo
    {
        //表格
        public string table { set; get; }
        public bg bgs { set; get; }
        //耗差 饼状图
        public ArrayList ConsumeList { set; get; }

        //耗差 柱状图
        public infos ZhuTu { set; get; }

        //环比柱状图
        public List<infos> Hb { set; get; }
    }

    public class bg
    {
        public IList<Hashtable> h { set; get; }
        public string c { set; get; }
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