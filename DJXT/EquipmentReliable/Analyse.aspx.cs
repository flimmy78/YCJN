using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace DJXT.EquipmentReliable
{
    public partial class Analyse : System.Web.UI.Page
    {
        BLLBase bb = new BLLBase();
        string errMsg = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCompany();
                //Bind();
            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            //Bind();
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
    }
}