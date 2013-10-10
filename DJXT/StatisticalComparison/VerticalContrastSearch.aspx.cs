using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Entity.Statistic;
using Newtonsoft.Json;
using BLL.StatisticalComparison;
using SAC.JScript;
using System.Text;
using System.Collections;
using System.Data;

namespace DJXT.StatisticalComparison
{
    public partial class VerticalContrastSearch : System.Web.UI.Page
    {
        BLLBase bb = new BLLBase();
        string errMsg = string.Empty;
        BLLVerticalContrastSearch bv = new BLLVerticalContrastSearch();

        protected void Page_Load(object sender, EventArgs e)
        {
            string param = Request["param"];
            if (param == "query")
            {
                string beginTime = "", endTime = "", unitId = "", paraId = "";
                if (Request["beginTime"] != null)
                    beginTime = Request["beginTime"];
                if (Request["endTime"] != null)
                    endTime = Request["endTime"];
                if (Request["unitId"] != null)
                    unitId = Request["unitId"];
               

                this.QueryData(beginTime, endTime, unitId);
            }
            if (!IsPostBack)
            {
                BindCompany();
                //Bind();
                InitialControl();
            }
        }

        private void QueryData(string beginTime, string endTime, string unitId)
        {
            DateTime dt1=DateTime.Parse(beginTime+"-01");
            DateTime dt2=DateTime.Parse(endTime+"-01");
            int monthCount = dt2.Year * 12 + dt2.Month - dt1.Year * 12 - dt1.Month + 1;

            //获取机组所属参名称和Id
            DataTable dt = bv.GetParaName(unitId, out errMsg);
            List<IndicatorInfo> infoList;
            IList<Hashtable> iList = new List<Hashtable>();

            Hashtable ht;
            for (int i = 0; i < monthCount; i++)
            {
                ht = new Hashtable();
                beginTime = dt1.AddMonths(i).ToString("yyyy-MM-01 00:00:00");
                endTime = DateTime.Parse(beginTime).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 24:00:00");

                infoList = new List<IndicatorInfo>();
                foreach (DataRow dr in dt.Rows)
                {
                    string tmpParaId = dr["T_PARAID"] == null ? string.Empty : dr["T_PARAID"].ToString();
                    infoList = bv.GetInfo(beginTime, endTime, unitId, tmpParaId, out errMsg);
                    for (int j = 0; j < infoList.Count; j++)
                    {
                        ht.Add(dr["T_PARADESC"], Math.Round(infoList[j].RealValue, 2));
                    }
                }
                if (infoList.Count > 0)
                {
                    ht.Add("月份", beginTime.Substring(0, 7));
                    iList.Add(ht);
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
                    else { width = skey.Length * 20; }
                    columns.AppendFormat("{{field:'{0}',title:'{1}',align:'center',width:{2}}},", skey, skey, 80);
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

        protected void BindCompany()
        {
            ddlCompany.DataSource = bb.GetCompany(out errMsg);
            ddlCompany.DataTextField = "T_COMPANYDESC";
            ddlCompany.DataValueField = "T_COMPANYID";
            ddlCompany.DataBind();
            ddlCompany.Items.Add(new ListItem { Value = "0", Text = "--请选择省公司--", Selected = true });
        }

        protected void BindPlant()
        {
            ddlPlant.DataSource = bb.GetPlant(ddlCompany.SelectedValue, out errMsg);
            ddlPlant.DataTextField = "T_PLANTDESC";
            ddlPlant.DataValueField = "T_PLANTID";
            ddlPlant.DataBind();
            ddlPlant.Items.Add(new ListItem { Value = "0", Text = "--请选择电厂--", Selected = true });
        }

        protected void BindUnit()
        {
            ddlUnit.DataSource = bb.GetUnit(ddlPlant.SelectedValue, out errMsg);
            ddlUnit.DataTextField = "T_UNITDESC";
            ddlUnit.DataValueField = "T_UNITID";
            ddlUnit.DataBind();
            ddlUnit.Items.Add(new ListItem { Value = "0", Text = "--请选择机组--", Selected = true });
        }

        protected void ddlCompany_SelectedChanged(object sender, EventArgs e)
        {
            BindPlant();
        }

        protected void ddlPlant_SelectedChanged(object sender, EventArgs e)
        {
            BindUnit();
        }

        public void ddlUnit_SelectedChanged(object sender, EventArgs e)
        {
            rBtn.DataSource = bv.GetParaName(ddlUnit.SelectedValue.Trim(), out errMsg);
            rBtn.DataTextField = "T_PARADESC";
            rBtn.DataValueField = "T_PARAID";
            rBtn.DataBind();
        }
        protected void Search_Click(object sender, EventArgs e)
        {
           
        }

        public void InitialControl()
        {
            txtBeginTime.Value = DateTime.Now.ToString("yyyy-MM");
            txtEndTime.Value = DateTime.Now.ToString("yyyy-MM");
        }
    }
    public class ReturnInfos
    {
        //日期
        public ArrayList date { set; get; }
        //每月对应的值
        public ArrayList value { set; get; }
        //指标名称
        public string name { set; get; }
        //指标单位
        public string unit { set; get; }
    }
}