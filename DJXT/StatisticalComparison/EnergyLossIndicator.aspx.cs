using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Collections;
using System.Data;
using BLL;
using SAC.Helper;
using BLL.StatisticalComparison;
using Entity.Statistic;

namespace DJXT.StatisticalComparison
{
    public partial class EnergyLossIndicator : System.Web.UI.Page
    {
        string errMsg = string.Empty;
        public int _rowIndex = 0;
        BLLEquipmentReliable bl = new BLLEquipmentReliable();
        BLLBase bb = new BLLBase();

        BLLEnergyLossIndicator bll = new BLLEnergyLossIndicator();
        DateHelper dh = new DateHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            string param = Request["param"];
            if (param != null)
            {
                if (param == "seachList")
                {
                    GetBCList();
                }
            }
            if (!IsPostBack)
            {
                BindCompany();
                //Bind();
            }
        }

        public void GetBCList()
        {
            //string companyId = Request["company"].ToString();
            //string plantId = Request["plant"].ToString();
            //string unitId = Request["unit"].ToString();
            //string beginTime = Request["beginTime"].ToString();
            //string endTime = Request["endTime"].ToString();
            //int page = Convert.ToInt32(Request["page"].ToString());
            //int rows = Convert.ToInt32(Request["rows"].ToString());
            //int sCount = (page - 1) * rows + 1;
            //int eCount = page * rows;
            ////总的行数。
            //int count = 0;
            //DataTable dt = bl.GetInitByCondition(companyId, plantId, unitId, beginTime, endTime, sCount, eCount, out count, out errMsg);
            ////int count =dt.Rows.Count;
            //IList<Hashtable> list = new List<Hashtable>();

            //foreach (DataRow item in dt.Rows)
            //{
            //    Hashtable ht = new Hashtable();
            //    ht.Add("T_CODE", item["T_CODE"]);
            //    ht.Add("T_UNITDESC", item["T_UNITDESC"].ToString());
            //    ht.Add("D_CAPABILITY", item["D_CAPABILITY"].ToString());
            //    ht.Add("I_GAAG", item["I_GAAG"].ToString());
            //    ht.Add("I_PH", item["I_PH"].ToString());
            //    ht.Add("I_AH", item["I_AH"].ToString());
            //    ht.Add("I_SH", item["I_SH"].ToString());
            //    ht.Add("I_UOH", item["I_UOH"].ToString());
            //    ht.Add("I_FOH", item["I_FOH"].ToString());
            //    ht.Add("I_EUNDH", item["I_EUNDH"].ToString());
            //    list.Add(ht);
            //}
            //object obj = new
            //{
            //    total = count,
            //    rows = list
            //};

            //string result = JsonConvert.SerializeObject(obj);
            //Response.Write(result);
            //Response.End();
            string unit = String.IsNullOrEmpty(Request["unit"].ToString()) ? string.Empty : Request["unit"].ToString();
            string time = String.IsNullOrEmpty(Request["time"].ToString()) ? string.Empty : Request["time"].ToString();
            string beginTime = String.IsNullOrEmpty(Request["beginTime"].ToString()) ? string.Empty : Request["beginTime"].ToString();
            string endTime = String.IsNullOrEmpty(Request["endTime"].ToString()) ? string.Empty : Request["endTime"].ToString();
            string timeType = String.IsNullOrEmpty(Request["timeType"].ToString()) ? string.Empty : Request["timeType"].ToString();
            string quarterType = String.IsNullOrEmpty(Request["quarterType"].ToString()) ? string.Empty : Request["quarterType"].ToString();

            //根据选择的时间段，设置开始时间和结束时间
            switch (quarterType)
            {
                case "0": //指定时间段

                    break;
                case "1"://月度平均值
                    DateTime dt1 = new DateTime();
                    dt1 = Convert.ToDateTime(beginTime.Substring(0, 7) + "-01");
                    beginTime = dh.GetFirstDayOfMonth(dt1).ToString();
                    endTime = dh.GetLastDayOfMonth(dt1).ToString();
                    break;
                case "2"://季度平均值
                    switch (quarterType)
                    {
                        case "0"://一季度
                            string ti = beginTime.Substring(0, 4) + "-01-01";
                            beginTime = ti;
                            endTime = beginTime.Substring(0, 4) + "-03-31";
                            break;
                        case "1"://二季度
                            string ti1 = beginTime.Substring(0, 4) + "-04-01";
                            beginTime = ti1;
                            endTime = beginTime.Substring(0, 4) + "-06-30";
                            break;
                        case "2"://三季度
                            string ti2 = beginTime.Substring(0, 4) + "-07-01";
                            beginTime = ti2;
                            endTime = beginTime.Substring(0, 4) + "-09-30";
                            break;
                        case "3"://四季度
                            string ti3 = beginTime.Substring(0, 4) + "-10-01";
                            beginTime = ti3;
                            endTime = beginTime.Substring(0, 4) + "-12-31";
                            break;
                    }
                    break;
                case "3"://年度平均值
                    string tim = beginTime.Substring(0, 4);
                    beginTime = tim + "01-01";
                    endTime = tim + "-12-31";
                    break;
            }

            List<IndicatorInfo> infoList = new List<IndicatorInfo>();

            //获取汽机的所有耗差类型。
            //infoList = bl.GetInfo(beginTime, endTime, unit, -1, 1, out errMsg);
            for (int i = 0; i < 10; i++)
            {
                IndicatorInfo info = new IndicatorInfo();
                info.Name = "主汽温度（°C）";
                info.StandardValue = 333.22;
                info.RealValue = 54.32;
                info.ConsumeValue = 87.09;
            }
            //string content = infoList.ToJsonItem();
            //int count = 0;
            //object obj = new
            //{
            //    total = count,
            //    rows = infoList
            //};

            string result = JsonConvert.SerializeObject(infoList);
            //Response.ContentType = "text/json;charset=gb2312;";
            Response.Write(result);
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

        protected void Search_Click(object sender, EventArgs e)
        {
        }

        protected void Export_Click(object sender, EventArgs e)
        {
        
            DataTable dt = new DataTable();
            int count = 0;
            //int page = Convert.ToInt32(Request.QueryString["page"].ToString());
            //int rows = Convert.ToInt32(Request.QueryString["rows"].ToString());
            //int sCount = (page - 1) * rows + 1;
            //int eCount = page * rows;
            //导出所有符合条件的数据。
            dt = bl.GetInitByCondition(ddlCompany.SelectedValue.Trim(), ddlPlant.SelectedValue.Trim(), ddlUnit.SelectedValue.Trim(), txtTimeBegin.Value.ToString(), txtTimeEnd.Value.ToString(), 0, 0, out count, out errMsg);

            GridViewExportUtil.RenderToExcel(dt, HttpContext.Current, "机组耗差指标分析.xls");
        }

    }



}