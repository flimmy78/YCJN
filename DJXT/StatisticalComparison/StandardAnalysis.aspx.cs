using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;

namespace DJXT.StatisticalComparison
{
    //耗差对标
    public partial class StandardAnalysis : System.Web.UI.Page
    {
        BLLBase bl = new BLLBase();
        string errMsg = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBoiler();
                BindSteam();
                txtTimeBegin.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            //Bind();
        }

        /// <summary>
        /// 绑定锅炉厂家
        /// </summary>
        public void BindBoiler()
        {
            DataTable dt = bl.GetBoiler(out errMsg);
            this.ddlBoiler.DataSource = dt;
            this.ddlBoiler.DataTextField = "T_BOILERDESC";
            this.ddlBoiler.DataValueField = "T_BOILERID";
            this.ddlBoiler.DataBind();
            ddlBoiler.Items.Add(new ListItem { Text = "--请选择锅炉厂家--", Value = "0", Selected = true });

        }

        /// <summary>
        /// 绑定汽机厂家
        /// </summary>
        public void BindSteam()
        {
            DataTable dt = bl.GetSteam(out errMsg);
            this.ddlSteam.DataSource = dt;
            this.ddlSteam.DataTextField = "T_STEAMDESC";
            this.ddlSteam.DataValueField = "T_STEAMID";
            this.ddlSteam.DataBind();
            ddlSteam.Items.Add(new ListItem { Text = "--请选择汽机厂家--", Value = "0", Selected = true });
        }

    }
}