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
public partial class BrowserThemalTemplate : System.Web.UI.Page
{
    string errMsg = string.Empty;
    public int _rowIndex = 0;
    BLLEquipmentReliable bl = new BLLEquipmentReliable();
    BLLBase bb = new BLLBase();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCompany();
           // BindReport(this.calStartTime.Text, this.calEndTime.Text, "0", ddlUnit.SelectedValue.Trim());
            //BindExprimentName();
        //    Bussiness.ThermalTestReport report = new Bussiness.ThermalTestReport();
        //    DataTable dt = report.GetFinishExpriment();
        //    this.grvFeeInfo.DataSource = dt;
        //    this.grvFeeInfo.DataBind();

        //    if (this.ddlExperiment.SelectedValue == "0")
        //    {
        //        for (int i = 0; i < grvFeeInfo.Rows.Count; i++)
        //        {
        //            GridViewRow row = grvFeeInfo.Rows[i];
        //            CheckBox isChecked = ((CheckBox)row.FindControl("ChkSelected"));
        //            isChecked.Enabled = false;
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < grvFeeInfo.Rows.Count; i++)
        //        {
        //            GridViewRow row = grvFeeInfo.Rows[i];
        //            CheckBox isChecked = ((CheckBox)row.FindControl("ChkSelected"));
        //            isChecked.Enabled = true;
        //        }
        //    }
        }
    }
    protected void grvFeeInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grvFeeInfo.PageIndex = e.NewPageIndex;
        btnSearch_Click(null, null);
        //ddlExperiment_SelectedIndexChanged(null, null);
    }
    protected void btnDisplay_Click(object sender, EventArgs e)
    {
        if (this.ddlExperiment.SelectedValue == "0")
        {
            Response.Write("<script language='javascript'> alert('不同类型实验无法进行对比，不能选择全部实验'); </script>");
            //BindReport(this.calStartTime.Text, this.calEndTime.Text, "0", ddlUnit.SelectedValue.Trim());
            return;
        }

        string colReportId = "";
        string colReportName = "";
        for (int i = 0; i < grvFeeInfo.Rows.Count; i++)
        {
            GridViewRow row = grvFeeInfo.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("ChkSelected")).Checked;
            if (isChecked)
            {
                colReportId += this.grvFeeInfo.DataKeys[i].Value.ToString() + ',';
                colReportName += this.grvFeeInfo.Rows[i].Cells[2].Text + ',';
            }
        }

        if (colReportId != "")
        {
            colReportId = colReportId.Substring(0, colReportId.Length - 1);
            colReportName = colReportName.Substring(0, colReportName.Length - 1);
            //Response.Write(string.Format("<script language='javascript' > window.open('DisplayReport.aspx?ReportID={0}&ReportName={1}') </script>", colReportId,colReportName));
            Response.Write(string.Format("<script language='javascript' > window.open('DisplayReport.aspx?ReportID={0}') </script>", colReportId));
        }


    }
    private void BindExprimentName()
    {
        Bussiness.ThermalTestTemplate template = new Bussiness.ThermalTestTemplate();
        template.UnitID = this.ddlUnit.SelectedValue;
        DataTable dt = template.GetReportName();
        this.ddlExperiment.DataSource = dt;
        this.ddlExperiment.DataTextField = "TemplateName";
        //this.ddlExperiment.DataValueField = "TemplateID";
        this.ddlExperiment.DataBind();
        ddlExperiment.Items.Add(new ListItem { Text = "--全部--", Value = "0", Selected = true });
    }
    //protected void ddlCompute_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindExprimentName();
    //    //ddlExperiment_SelectedIndexChanged(null, null);
    //}
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlUnit.SelectedValue == "0")
        {
            JScript.Alert("请选择机组！");
            BindReport(this.calStartTime.Text, this.calEndTime.Text, "0", ddlUnit.SelectedValue.Trim());

            return;
        }
        Bussiness.ThermalTestReport report = new Bussiness.ThermalTestReport();
        //DataTable dt = report.GetFinishExpriment(this.calStartTime.Text, this.calEndTime.Text, Convert.ToInt32(this.ddlExperiment.SelectedValue));
        //DataTable dt = report.GetExpriment(this.calStartTime.Text, this.calEndTime.Text, Convert.ToInt32(this.ddlExperiment.SelectedValue));
        //this.grvFeeInfo.DataSource = dt;
        //this.grvFeeInfo.DataBind();
        Bussiness.ThermalTestTemplate TestTemplate = new Bussiness.ThermalTestTemplate();
        //int templateId = TestTemplate.GetTemplateID(this.ddlExperiment.SelectedValue, this.ddlCompute.SelectedValue);

        DataTable dt = null;
        if (this.ddlExperiment.SelectedValue != "0")
        {
            dt = TestTemplate.GetTemplateIDs(this.ddlExperiment.SelectedValue, this.ddlUnit.SelectedValue);
            string templateId = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    templateId += dr[0].ToString() + ",";
                }
                templateId = templateId.Substring(0, templateId.Length - 1);
            }

            BindReport(this.calStartTime.Text, this.calEndTime.Text, templateId,ddlUnit.SelectedValue.Trim());
        }
        else
            BindReport(this.calStartTime.Text, this.calEndTime.Text, "0", ddlUnit.SelectedValue.Trim());


        //BindReport(this.calStartTime.Text, this.calEndTime.Text, templateId);   
       
    }
    private void BindReport(string startTime,string endTime,string id_key,string unitId)
    {
        Bussiness.ThermalTestReport report = new Bussiness.ThermalTestReport();
        
        DataTable dt = report.GetExpriment(startTime, endTime, id_key,unitId);

        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
            grvFeeInfo.DataSource = dt;
            grvFeeInfo.DataBind();
            int columnCount = grvFeeInfo.Rows[0].Cells.Count;
            grvFeeInfo.Rows[0].Cells.Clear();
            grvFeeInfo.Rows[0].Cells.Add(new TableCell());
            grvFeeInfo.Rows[0].Cells[0].ColumnSpan = columnCount;
            grvFeeInfo.Rows[0].Cells[0].BorderColor = System.Drawing.Color.Black;
            grvFeeInfo.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Left;
            grvFeeInfo.Rows[0].Cells[0].Text = "暂时没有信息";
            grvFeeInfo.Rows[0].Cells[0].Style.Add("text-align", "center");
        }
        else
        {
            this.grvFeeInfo.DataSource = dt;
            this.grvFeeInfo.DataBind();
        }
      
    }
    //protected void ddlExperiment_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    Bussiness.ThermalTestTemplate TestTemplate = new Bussiness.ThermalTestTemplate();
    //    DataTable dt = null;
    //    if (this.ddlExperiment.SelectedValue != "0")
    //    {
    //        dt = TestTemplate.GetTemplateIDs(this.ddlExperiment.SelectedValue, this.ddlUnit.SelectedValue);
    //        string templateId = "";
    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            foreach (DataRow dr in dt.Rows)
    //            {
    //                templateId += dr[0].ToString() + ",";
    //            }
    //            templateId = templateId.Substring(0, templateId.Length - 1);
    //        }

    //        BindReport(this.calStartTime.Text, this.calEndTime.Text, templateId);
    //    }
    //    else
    //        BindReport(this.calStartTime.Text, this.calEndTime.Text, "0");

    //    if (this.ddlExperiment.SelectedValue == "0")
    //    {
    //        for (int i = 0; i < grvFeeInfo.Rows.Count; i++)
    //        {
    //            GridViewRow row = grvFeeInfo.Rows[i];
    //            CheckBox isChecked = ((CheckBox)row.FindControl("ChkSelected"));
    //            isChecked.Enabled = false;
    //        }
    //    }
    //    else
    //    {
    //        for (int i = 0; i < grvFeeInfo.Rows.Count; i++)
    //        {
    //            GridViewRow row = grvFeeInfo.Rows[i];
    //            CheckBox isChecked = ((CheckBox)row.FindControl("ChkSelected"));
    //            isChecked.Enabled = true;
    //        }
    //    }


    //}

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
        BindUnit();
        BindReport(this.calStartTime.Text, this.calEndTime.Text, "0", ddlUnit.SelectedValue.Trim());
    }

    protected void ddlPlant_SelectedChanged(object sender, EventArgs e)
    {
        BindUnit();
        BindReport(this.calStartTime.Text, this.calEndTime.Text, "0", ddlUnit.SelectedValue.Trim());
    }

    protected void ddlUnit_SelectedChanged(object sender, EventArgs e)
    {
        BindExprimentName();
        BindReport(this.calStartTime.Text, this.calEndTime.Text, "0", ddlUnit.SelectedValue.Trim());
    }
}
