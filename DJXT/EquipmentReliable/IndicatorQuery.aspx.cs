using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using SAC.Helper;
using System.IO;
using System.Data;
using System.Collections;
using Newtonsoft.Json;

namespace YJJX.EquipmentReliable
{
    public partial class IndicatorQuery : System.Web.UI.Page
    {
        string errMsg = string.Empty;
        public int _rowIndex = 0;
        BLLEquipmentReliable bl = new BLLEquipmentReliable();
        BLLBase bb = new BLLBase();

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
            if (!IsPostBack)
            {
                BindCompany();
                //Bind();
            }
        }

        public void GetBCList()
        {
            string companyId = Request["company"].ToString();
            string plantId = Request["plant"].ToString();
            string unitId = Request["unit"].ToString();
            string beginTime = Request["beginTime"].ToString();
            string endTime = Request["endTime"].ToString();
            int page = Convert.ToInt32(Request["page"].ToString());
            int rows = Convert.ToInt32(Request["rows"].ToString());
            int sCount = (page - 1) * rows + 1;
            int eCount = page * rows;
            int count = 0;
            DataTable dt = bl.GetInitByCondition(companyId, plantId, unitId, beginTime, endTime, sCount, eCount,out count, out errMsg);
            //int count = dt.Rows.Count;
            IList<Hashtable> list = new List<Hashtable>();

            foreach (DataRow item in dt.Rows)
            {
                Hashtable ht = new Hashtable();
                ht.Add("T_PLANTDESC", item["T_PLANTDESC"]);
                ht.Add("T_UNITDESC", item["T_UNITDESC"].ToString());
                ht.Add("D_CAPABILITY", item["D_CAPABILITY"].ToString());
                ht.Add("I_UTH", item["I_UTH"].ToString());
                ht.Add("D_EAF", item["D_EAF"].ToString());
                ht.Add("D_FOF", item["D_FOF"].ToString());
                ht.Add("D_FOR", item["D_FOR"].ToString());
                ht.Add("D_UOF", item["D_UOF"].ToString());
                ht.Add("D_UOR", item["D_UOR"].ToString());
                list.Add(ht);
            }
            object obj = new
            {
                total = count,
                rows = list
            };

            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
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
            //Bind();
        }

        protected void Export_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            int count = 0;
            //int page = Convert.ToInt32(Request["page"].ToString());
            //int rows = Convert.ToInt32(Request["rows"].ToString());
            //int sCount = (page - 1) * rows + 1;
            //int eCount = page * rows;
            //导出当前符合条件的所有数据。
            dt = bl.GetInitByCondition(ddlCompany.SelectedValue.Trim(), ddlPlant.SelectedValue.Trim(), ddlUnit.SelectedValue.Trim(), txtTimeBegin.Value.ToString(), txtTimeEnd.Value.ToString(), 0, 0, out count, out errMsg);
            
            //GridViewExportUtil.ExportByWeb(dt, "设备可靠性过程参数", "设备可靠性过程参数.xls");
            //GridViewExportUtil.SaveToFile(dt, "设备可靠性过程参数");
            //GridViewExportUtil.ExportByWeb(dt, "设备可靠性过程参数", "设备可靠性过程参数.xls");
            //GridViewExportUtil.RenderToExcel(dt, Server.MapPath(("upfiles\\") + "设备可靠性.xls"));
            GridViewExportUtil.ExportByWeb(dt, "设备可靠性监视", "设备可靠性监视.xls");
        }
    }
}