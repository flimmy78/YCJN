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
public partial class DisplayReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string colReportId = Request.QueryString["ReportID"];
            string[] reportIds = colReportId.Split(',');
            int isSplit = colReportId.IndexOf(',');
            int count = 1;
            for (int L = 0; L < colReportId.Length; L++)
            {
                string temp = colReportId.Substring(L, 1);
                if (temp == ",")
                    count = count + 1;
            }

            colReportId = "(" + colReportId + ")";
            Bussiness.ThermalTestReport Report = new Bussiness.ThermalTestReport();
            DataTable nameDT = Report.GetTest(colReportId);

            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("<table class=\"grid\" cellspacing=\"0\" rules=\"all\" border=\"1\" id=\"tab\" style=\"width: 100%;border-collapse: collapse; z-index: 100; left: 0px; top: 20px;\">");
            
            builder.Append("<tr><th scope='col'>序号</th><th scope='col'>参数描述</th>");
            if (nameDT != null && nameDT.Rows.Count > 0)
            {
                for (int n = 0; n < nameDT.Rows.Count; n++)
                {
                    builder.Append(string.Format("<th scope=\"col\">{0}</th>", nameDT.Rows[n]["REPORTNAME"].ToString()+ "的实验值"));
                }
            }
            builder.Append("</tr>");

            Bussiness.ThermalTestTemplate TestTemplate = new Bussiness.ThermalTestTemplate();
            Bussiness.TestReport Test = new Bussiness.TestReport();
            DataTable dt = Test.GetReportResult(colReportId);
            Bussiness.ThermaltestPara Para = new Bussiness.ThermaltestPara();
            string preReportId = "";
            int sortId = 0;
            int reportCount = 0;
            int copycount = 0;
            System.Collections.ArrayList lstHtml = new ArrayList();
            System.Collections.ArrayList lstDesc = new ArrayList();
            
            if (!isSplit.Equals(-1))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    int descCount = 0;
                    string firstValue = "";
                    foreach (string r in reportIds)
                    {
                        DataTable rDT = Test.GetReportResult(Convert.ToInt32(r));
                        DataTable nDT = Report.GetTest(Convert.ToInt32(r));
                        int Id = Convert.ToInt32(nDT.Rows[0]["TESTTEMPLATEID"]);
                        System.Collections.ArrayList lstValue = new ArrayList();
                        if (rDT != null && rDT.Rows.Count > 0)
                        {
                            for (int M = 0; M < rDT.Rows.Count; M++)
                            {

                                bool exist = TestTemplate.IsExitTemplatePara(Id, rDT.Rows[M][1].ToString());
                                if (exist)
                                {
                                    try
                                    {
                                        if (descCount == 0)
                                        {

                                            string descript = Para.GetParaDescUnit(rDT.Rows[M][1].ToString());
                                            lstDesc.Add(descript);
                                            lstValue.Add(rDT.Rows[M][2].ToString());
                                            reportCount += 1;

                                        }
                                        else
                                        {
                                            lstValue.Add(rDT.Rows[M][2].ToString());
                                            copycount += 1;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }

                                }

                            }
                        }

                        if (descCount == 0)
                        {
                            lstHtml.Add(lstDesc);
                        }
                        lstHtml.Add(lstValue);
                        descCount += 1;
                    }

                    for (int rC = 0; rC < reportCount; rC++)
                    {
                        

                        builder.Append("<tr>");
                        builder.Append(string.Format("<td>{0}</td>", rC + 1));
                        for (int N = 0; N < count + 1; N++)
                        {
                            builder.Append(string.Format("<td>{0}</td>", ((System.Collections.ArrayList)lstHtml[N])[rC].ToString()));
                        }

                        builder.Append("</tr>");

                    }


                    #region 旧方法
                    //int allCount = reportCount * (count + 1);
                    //string[] splitHTML = new string[allCount];
                    //splitHTML = colHtml.Split(';');
                    //for (int rC = 0; rC < reportCount; rC++)
                    //{
                    //    builder.Append("<tr>");
                    //    builder.Append(string.Format("<td>{0}</td>", rC + 1));
                    //    for (int N = 0; N < count + 1; N++)
                    //    {
                    //        builder.Append(string.Format("<td>{0}</td>", splitHTML[rC + N * reportCount].ToString()));
                    //    }

                    //    builder.Append("</tr>");
                    //    //builder.Append(string.Format("<td>{0}</td>", desc[0]));

                    ////    foreach (string html in splitHTML)
                    ////    {
                    ////        if (html.IndexOf(":") != -1)
                    ////        {
                    ////            string[] descvalue = html.Split(",");
                    ////            for (int d = 0; d < descvalue.Length; d++)
                    ////            {
                    ////                string[] desc = descvalue[d].Split(':');
                    ////                builder.Append(string.Format("<td>{0}</td>", desc[0]));
                    ////                builder.Append(string.Format("<td>{0}</td>", desc[1]));

                    ////            }
                    ////        }
                    ////    }
                    //}


                    //int rowCount = dt.Rows.Count / count;

                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{

                    //    int templateId = Convert.ToInt32(nameDT.Rows[0][1]);
                    //    bool falg = TestTemplate.IsExitTemplatePara(templateId, dt.Rows[i][1].ToString());

                    //    if (falg)
                    //    {
                    //        sortId += 1;
                    //        builder.Append("<tr>");
                    //        builder.Append(string.Format("<td>{0}</td>", sortId));
                    //        string desc = Para.GetParaDescUnit(dt.Rows[i][1].ToString());
                    //        builder.Append(string.Format("<td>{0}</td>", desc));
                    //        for (int j = 0; j < count; j++)
                    //        {

                    //            builder.Append(string.Format("<td>{0}</td>", dt.Rows[i + j][2].ToString()));
                    //        }
                    //        builder.Append("</tr>");
                    //        //builder.Append("</td>");
                    //    }

                    //    i = i + count - 1;
                    //}


                    #endregion

                }
            }
            else
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        if (preReportId != dt.Rows[k][0].ToString())
                        {
                            int templateId = Convert.ToInt32(nameDT.Rows[0]["TESTTEMPLATEID"]);
                            bool falg = TestTemplate.IsExitTemplatePara(templateId, dt.Rows[k][1].ToString());
                            if (falg)
                            {
                                sortId += 1;
                                builder.Append("<tr>");
                                builder.Append(string.Format("<td>{0}</td>", sortId));
                                string desc = Para.GetParaDescUnit(dt.Rows[k][1].ToString());
                                builder.Append(string.Format("<td>{0}</td>", desc));
                                builder.Append(string.Format("<td>{0}</td>", dt.Rows[k][2].ToString()));
                                builder.Append("</tr>");
                            }
                        }
                    }
                }
            }
            builder.Append("</table>");

            this.result.InnerHtml = builder.ToString();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }


    public void OutPutExcel()
    {

        Response.ClearContent();
        Response.Buffer = false;
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");


        Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("实验报告数据.xls"));
        Response.ContentType = "application/excel";
        //Page.EnableViewState = false;

        System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);

        //this.Page.RenderControl(oHtmlTextWriter);

        result.RenderControl(oHtmlTextWriter);

        string temp = oStringWriter.ToString();

        Response.Write(temp);
        Response.End();
    }
    protected void btnOutPutExcel_Click(object sender, EventArgs e)
    {
        OutPutExcel();
    }
}
