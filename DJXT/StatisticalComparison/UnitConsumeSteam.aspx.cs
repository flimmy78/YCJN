using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAC.JScript;
using Newtonsoft.Json;
using System.Data;
using System.Collections;
using BLL;

namespace DJXT.StatisticalComparison
{
    public partial class UnitConsumeSteam : System.Web.UI.Page
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
                txtTimeBegin.Value = DateTime.Now.ToString();
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
            //总的行数。
            int count = 0;
            DataTable dt = bl.GetInitByCondition(companyId, plantId, unitId, beginTime, endTime, sCount, eCount, out count, out errMsg);
            //int count =dt.Rows.Count;
            IList<Hashtable> list = new List<Hashtable>();

            foreach (DataRow item in dt.Rows)
            {
                Hashtable ht = new Hashtable();
                ht.Add("T_CODE", item["T_CODE"]);
                ht.Add("T_UNITDESC", item["T_UNITDESC"].ToString());
                ht.Add("D_CAPABILITY", item["D_CAPABILITY"].ToString());
                ht.Add("I_GAAG", item["I_GAAG"].ToString());
                ht.Add("I_PH", item["I_PH"].ToString());
                ht.Add("I_AH", item["I_AH"].ToString());
                ht.Add("I_SH", item["I_SH"].ToString());
                ht.Add("I_UOH", item["I_UOH"].ToString());
                ht.Add("I_FOH", item["I_FOH"].ToString());
                ht.Add("I_EUNDH", item["I_EUNDH"].ToString());
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
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
           
        }
    }
}