using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class ViewExpriment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int reportId = Convert.ToInt32(Request.QueryString["reportid"]);
        if (!IsPostBack)
        {
            Bussiness.ThermalTestReport report = new Bussiness.ThermalTestReport();
            DataTable dt = report.GetTest(reportId);
            this.grvFeeInfo.DataSource = dt;
            this.grvFeeInfo.DataBind();
        }

    }
}
