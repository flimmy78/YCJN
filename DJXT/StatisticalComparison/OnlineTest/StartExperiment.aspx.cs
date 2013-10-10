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
using BLL;
using SAC.JScript;
using BLL.StatisticalComparison;

public partial class StartExperiment : System.Web.UI.Page
{
    string errMsg = string.Empty;
    public int _rowIndex = 0;
    BLLEquipmentReliable bl = new BLLEquipmentReliable();
    BLLBase bb = new BLLBase();
    Bussiness.ThermalTestReport bt = new ThermalTestReport();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCompany();

            this.Timer1.Enabled = false;
            //BindReportName();
            this.Session["bl"] = 0;

            Bussiness.Config config = new Config();
            DataTable dt = config.ReadConfig();
            if (dt != null && dt.Rows.Count > 0)
            {
                this.hddMinSplitTime.Value = dt.Rows[0][2].ToString();
                this.txtSplitTime.Value = dt.Rows[0][2].ToString();
                this.hddMaxTime.Value = dt.Rows[0][5].ToString();
                this.txtSYTime.Value = dt.Rows[0][4].ToString();
                this.calSYStartTime.Text = DateTime.Now.ToString();
            }

            Bussiness.ThermalTestReport report = new ThermalTestReport();
            if (report.IsDoingExpriment())
                this.btnStartEnd.Enabled = false;

            this.btnView.Enabled = false;
            this.btnView.Attributes.Add("onclick", "linkExpriment()");
        }       
    }
    /// <summary>
    /// 绑定试验名
    /// </summary>
    public void BindReportName()
    {
        ThermalTestTemplate template = new ThermalTestTemplate();
        //template.UnitID = "0";
        template.UnitID = this.ddlUnit.SelectedValue;
        DataTable dt = template.GetReportName();
        this.ddlSYName.DataSource = dt;
        this.ddlSYName.DataTextField = "TemplateName";
        this.ddlSYName.DataValueField = "TEMPLATEID";
        this.ddlSYName.DataBind();
        ddlSYName.Items.Add(new ListItem { Text = "--请选择试验名称--", Value = "0", Selected = true });

    }
    /// <summary>
    /// 开始试验
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnStartEnd_Click(object sender, EventArgs e)
    {
        ThermalTestReport report = new ThermalTestReport();

        if (ddlCompany.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "msg1", "alert('请选择分公司...');", true);
            return;
        }
        if (ddlPlant.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "msg1", "alert('请选择电厂...');", true);
            return;
        }
        if (ddlUnit.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "msg1", "alert('请选择机组...');", true);
            return;
        }
        if (ddlSYName.SelectedValue=="0")
        {
            JScript.Alert("请选择试验名称！");
            return;
        }
        if (txtSYR.Value == "")
        {
            JScript.Alert("请填写试验人！");
            return;
        }
        if (btnStartEnd.Text == "开始试验")
        {
            if (report.IsDoingExpriment())
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "msg1", "alert('已有试验正在运行，请稍后运行试验...');", true);
                return;
            }
            this.btnView.Enabled = false;

            ThermalTestTemplate template = new ThermalTestTemplate();
            if (template.GetTemplateID(this.ddlSYName.SelectedItem.Text.Trim(), this.ddlUnit.SelectedValue).ToString() == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "msg1", "alert('该机组尚未配置...');", true);
                return;
            }
            report.TESTTemplateID = template.GetTemplateID(this.ddlSYName.SelectedItem.Text.Trim(), this.ddlUnit.SelectedValue);
            report.TESTCondition = GetCondition();
            report.ReportName = this.txtReportName.Value;
            report.Tester = this.txtSYR.Value;
            report.UNITID = ddlUnit.SelectedValue.Trim();
            report.TestBegin = Convert.ToDateTime(this.calSYStartTime.Text);
            report.TestCalBegin = Convert.ToDateTime(DateTime.Now.ToLongTimeString());
            report.TestDuration = Convert.ToInt32(this.txtSYTime.Value);
            report.SampleInterval = Convert.ToInt32(this.txtSplitTime.Value);
            Bussiness.ThermalTestReport.UserState userState = ThermalTestReport.UserState.New;
            
            
            report.InsertTest(userState,report);

            this.Timer1.Interval = 1000;            
            this.Timer1.Enabled = true;
            btnStartEnd.Text = "终止试验";
            
            this.Label1.Text = this.txtReportName.Value + "试验正在进行已完成0%";
            
        }
        else
        {
            this.Timer1.Enabled = false;
            int curReportId = report.GetMaxReportId();            
            report.EndTest(curReportId);
            btnStartEnd.Text = "开始试验";
            report.EndTest(curReportId);
            this.Timer1.Enabled = true;            
            btnStartEnd.Enabled = false;
            this.Label1.Text = "试验已中止";            
        }
    }

    /// <summary>
    /// 改变试验状态（点击确认后）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnHelp_Click(object sender, EventArgs e)
    {
        ThermalTestReport report = new ThermalTestReport();
        int curReportId = report.GetMaxReportId();
        if (hidvalue.Value == "0")
        {
            report.BDEndTest(curReportId, 12);
            //this.Timer1.Enabled = false;
        }
        else if (hidvalue.Value == "1")
        {
            report.BDEndTest(curReportId, 5);
            this.Timer1.Enabled = false;
            
            btnStartEnd.Text = "开始试验";
            btnStartEnd.Enabled = true;
            this.Label1.Text = "试验已中止";  
        }

    }
    /// <summary>
    /// 实时更新试验进度
    /// </summary>
    public void UpdateState()
    {
        ThermalTestReport report = new ThermalTestReport();
        report.TestBegin = Convert.ToDateTime(this.calSYStartTime.Text);
        int curReportId = report.GetMaxReportId();
        string state = report.GetTest(curReportId).Rows[0]["TESTSTATE"].ToString();
        double roral = report.GetDiffTime(curReportId) / Convert.ToInt32(this.txtSYTime.Value) * 100;
        if (state != "4" && state != "8" && state != "9" && state != "10" && state != "11")
        {
            this.Label1.Text = this.txtReportName.Value + "试验正在进行已完成" + roral.ToString("0") + "%";
        }
        else if (state == "8")
        {
            this.Timer1.Enabled = false;
            //JScript.Alert("不明泄漏率计算失败，试验终止！");
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "updateScript", "alert('不明泄漏率计算失败，试验终止！');", true);
            report.BDEndTest(curReportId,5);
            this.Timer1.Enabled = false;
            btnStartEnd.Text = "开始试验";
            btnStartEnd.Enabled = true;
            this.Label1.Text = "试验已中止";  
            //this.btnView.Enabled = true;
            //this.hddMaxReportID.Value = curReportId.ToString();
        }
        else if (state == "10")
        {
            //if (hidvalue.Value == "1")
            //{
            //    report.BDEndTest(curReportId, 12);
            //    this.Timer1.Enabled = false;
            //}
            //else
            //{
            //    report.BDEndTest(curReportId, 5);
            //}
            string x = report.GetX(curReportId);
            string y = report.GetY(ddlUnit.SelectedValue.Trim(), ddlSYName.SelectedValue.Trim(),ddlSYGK.SelectedItem.Text.Trim());
            //ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "updateScript", "confir('"+x+"','"+y+"')", true);
            //this.ClientScript.RegisterStartupScript(this.GetType(), "msgbox", "return confirm('" + x + "?');", true);
            hidX.Value = x;
            hidY.Value = y;
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "updateScript", "document.getElementById('btnHelp').click();", true);

            //return confirm('不明泄漏率为x，超出限值y，是否继续试验？')
           
        }
        else if (state == "11")
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "updateScript", "alert('数据处理失败，试验终止！');", true);
            report.BDEndTest(curReportId, 5);
            this.Timer1.Enabled = false;
            btnStartEnd.Text = "开始试验";
            btnStartEnd.Enabled = true;
            this.Label1.Text = "试验已中止";  
        }
        else if (state == "9")
        {
            
            string x = report.GetX(curReportId);
            string y = report.GetY(ddlUnit.SelectedValue.Trim(), ddlSYName.SelectedValue.Trim(), ddlSYGK.SelectedItem.Text.Trim());
            hidX.Value = x;
            hidY.Value = y;
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "updateScript", "document.getElementById('btnHelps').click();", true);
            //report.BDEndTest(curReportId, 12);
        }
        else
        {
            this.Label1.Text = this.txtReportName.Value + "试验已完成100%";
            btnStartEnd.Text = "开始试验";
            this.btnView.Enabled = true;
            this.hddMaxReportID.Value = curReportId.ToString();
            this.Timer1.Enabled = false;

        }

    }
    /// <summary>
    /// 时间触发事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        UpdateState();
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

    protected void BindSYGK()
    {
        ddlSYGK.DataSource = bt.GetThermalTestRequirement(ddlUnit.SelectedValue.Trim(), ddlSYName.SelectedValue.Trim());
        ddlSYGK.DataTextField = "TESTCONDITION";
        //ddlSYGK.DataValueField = "TEMPLATEID";
        ddlSYGK.DataBind();
        ddlSYGK.Items.Add(new ListItem { Value = "0", Text = "--请选择试验工况--", Selected = true });
    }


    protected void ddlCompany_SelectedChanged(object sender, EventArgs e)
    {
        BindPlant();
    }

    protected void ddlPlant_SelectedChanged(object sender, EventArgs e)
    {
        BindUnit();
    }
    protected void ddlUnit_SelectedChanged(object sender, EventArgs e)
    {
        BindReportName();
    }
    protected void ddlSYName_SelectedChanged(object sender, EventArgs e)
    {
        if (ddlSYName.SelectedValue == "128" || ddlSYName.SelectedValue == "32" || ddlSYName.SelectedValue == "64")
        {
            sgk.Text = "设定加热器水位";
            hm.Visible = true;
            txtHeater.Visible = true;
            ddlSYGK.Visible = false;
            txtYJ.Visible = false;
            divContent.Visible = false;
        }
        else if (ddlSYName.SelectedValue == "256"||ddlSYName.SelectedValue == "512")
        {
            sgk.Text = "运行方式";
            txtYJ.Visible = true;
            txtHeater.Visible = false;
            ddlSYGK.Visible = false;
            hm.Visible = false;
            divContent.Visible = true;
        }
        else
        {
            sgk.Text = "试验工况";
            ddlSYGK.Visible = true;
            hm.Visible = false;
            txtHeater.Visible = false;
            txtYJ.Visible = false;
            divContent.Visible = false;

            BindSYGK();
        }
    }

    public string GetCondition()
    {
        if (ddlSYGK.Visible)
        {
            if (ddlSYGK.SelectedValue != "0")
            {
                return ddlSYGK.SelectedItem.Text.Trim();
            }
        }
        else if (txtHeater.Visible)
        {
            if (!string.IsNullOrEmpty(txtHeater.Text.Trim()))
            {
                return "加热器水位：" + txtHeater.Text.Trim()+"mm";
            }
        }
        else if (divContent.Visible)
        {
            if (ddlSelect.SelectedValue != "0" && !string.IsNullOrEmpty(txtKD.Text.Trim()))
            {
                return ddlSelect.SelectedItem.Text.Trim() + "开度：" + txtKD.Text.Trim()+"%";
            }
        }
        return string.Empty;
    }
}
