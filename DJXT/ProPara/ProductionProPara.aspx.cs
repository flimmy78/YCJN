using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SAC.JScript;
using SAC.Helper;
using Entity.ConsumeIndicator;
using Entity.ProPara;

namespace DJXT.ProPara
{
    public partial class ProductionProPara : System.Web.UI.Page
    {
        string errMsg = "";
        BLL.BLLProPara BPP = new BLL.BLLProPara();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Sec_data_bind();
            }
        }

        private void Sec_data_bind()
        {
            BLL.BLLRealQuery BLQ = new BLL.BLLRealQuery();
            DataSet DS = BLQ.Get_Company_Info(out errMsg);
            DataSet DSS = BLQ.Get_Electric_Info(DS.Tables[0].Rows[0]["T_COMPANYID"].ToString(), out errMsg);
            DataSet DDS = BLQ.Get_Unit_Info(DSS.Tables[0].Rows[0]["T_PLANTID"].ToString(), out errMsg);
            this.sec_company.DataSource = DS.Tables[0].DefaultView;
            this.sec_company.DataTextField = "T_COMPANYDESC";
            this.sec_company.DataValueField = "T_COMPANYID";
            this.sec_company.DataBind();
            this.sec_company.Items.Insert(0,"-请选择-");
        }

        protected void button_ServerClick(object sender, EventArgs e)
        {
            if (fileUp.HasFile == false)//HasFile用来检查FileUpload是否有指定文件
            {
                JScript.Alert("请您选择Excel文件");
                return;
            }
            string IsXls = System.IO.Path.GetExtension(fileUp.FileName).ToString().ToLower();//System.IO.Path.GetExtension获得文件的扩展名
            if (IsXls != ".xls")
            {
                JScript.Alert("只可以选择Excel文件");
                return;
            }

            //获取Execle文件名  DateTime日期函数
            string filename = fileUp.FileName;


            //Server.MapPath 获得虚拟服务器相对路径
            string savePath = Server.MapPath(("upfiles\\") + filename);

            //SaveAs 将上传的文件内容保存在服务器上
            fileUp.SaveAs(savePath);

            //连接Excel  读取Excel数据   并返回DataSet数据集合
            DataSet ds = GridViewExportUtil.ExcelSqlConnection(savePath, filename, "COALDAYEXPENDall");
            //com.sac.platform.action.report
            //定义一个DataRow数组
            DataRow[] dr = ds.Tables[0].Select();
            int rowsnum = ds.Tables[0].Rows.Count;
            if (rowsnum == 0)
            {
                JScript.Alert("Excel表为空表,无数据!");
            }
            else
            {
                List<Entity.ProPara.ProductionProPara> infoList = new List<Entity.ProPara.ProductionProPara>();
                DateTime dt = DateTime.Parse(dr[0][19].ToString().Split('：')[1]);
                for (int i = 6; i < dr.Length - 1; i++)
                {
                    Entity.ProPara.ProductionProPara info = new Entity.ProPara.ProductionProPara();
                    if (String.IsNullOrEmpty(dr[i][21].ToString()))
                    {
                        continue;
                    }
                    info.T_TIME = dt;

                    info.D_M_AR_PROX = Convert.ToDouble(string.IsNullOrEmpty(dr[i][11].ToString().Trim()) ? "0" : dr[i][11].ToString().Trim());
                    info.D_M_AD = Convert.ToDouble(string.IsNullOrEmpty(dr[i][12].ToString().Trim()) ? "0" : dr[i][12].ToString().Trim());
                    info.D_A_AD = Convert.ToDouble(string.IsNullOrEmpty(dr[i][13].ToString().Trim()) ? "0" : dr[i][13].ToString().Trim());
                    info.D_V_DAF = Convert.ToDouble(string.IsNullOrEmpty(dr[i][14].ToString().Trim()) ? "0" : dr[i][14].ToString().Trim());
                    info.D_A_AR_PROX = Convert.ToDouble(string.IsNullOrEmpty(dr[i][15].ToString().Trim()) ? "0" : dr[i][15].ToString().Trim());
                    info.D_QNET_AR_PROX = Convert.ToDouble(string.IsNullOrEmpty(dr[i][16].ToString().Trim()) ? "0" : dr[i][16].ToString().Trim());
                    info.D_ST_AD = Convert.ToDouble(string.IsNullOrEmpty(dr[i][17].ToString().Trim()) ? "0" : dr[i][17].ToString().Trim());
                    info.D_ST_AR = Convert.ToDouble(string.IsNullOrEmpty(dr[i][18].ToString().Trim()) ? "0" : dr[i][18].ToString().Trim());
                    info.D_CFH_C_PROX = Convert.ToDouble(string.IsNullOrEmpty(dr[i][19].ToString().Trim()) ? "0" : dr[i][19].ToString().Trim());
                    info.D_CLZ_C_PROX = Convert.ToDouble(string.IsNullOrEmpty(dr[i][20].ToString().Trim()) ? "0" : dr[i][20].ToString().Trim());
                    info.T_UNITID = dr[i][21].ToString().Trim();
                    infoList.Add(info);
                }
                try
                {
                    if (BPP.InsertExcelData(infoList, out errMsg))
                    {
                        JScript.Alert("Excle表导入成功");
                    }
                    else
                    {
                        JScript.Alert("Excle表导入失败");
                    }
                }
                catch
                {
                    JScript.Alert("Excle表导入失败");

                }
            }
        }
    }
}