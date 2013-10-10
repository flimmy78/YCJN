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

using Bussiness;

public partial class EditThermalTestCfg : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            ReadDefaultConfig();
    }
    public void ReadDefaultConfig()
    {
        Config config = new Config();
        DataTable dt = config.ReadConfig();
        if (dt != null && dt.Rows.Count > 0)
        {
            this.txtDefaultTime.Text = dt.Rows[0][1].ToString();
            this.txtMinTime.Value = dt.Rows[0][2].ToString();
            this.txtMaxMount.Value = dt.Rows[0][3].ToString();
            this.txtDefaultSYTime.Value = dt.Rows[0][4].ToString();
            this.txtMaxSYTime.Value = dt.Rows[0][5].ToString();
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            Config config = new Config();
            config.DefaultInterval = Convert.ToInt32(this.txtDefaultTime.Text);
            config.MinInterval = Convert.ToInt32(this.txtMinTime.Value);
            config.MaxSampleCondition = Convert.ToInt32(this.txtMaxMount.Value);
            config.DefaultTestDuration = Convert.ToInt32(this.txtDefaultSYTime.Value);
            config.MaxTestDuration = Convert.ToInt32(this.txtMaxSYTime.Value);
            DataTable dt = config.ReadConfig();
            if (dt != null && dt.Rows.Count > 0)
            {
                config.ID_KEY = Convert.ToInt32(dt.Rows[0][0].ToString());
                config.EditConfig(config);
            }
            else
            {
                config.InsertConfig();
            }
            Response.Write("<script language='javascript'> alert('保存成功！') </script>");
        }
        catch (Exception ex)
        {
            return;
        }
    }
}
