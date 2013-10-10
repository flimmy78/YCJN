using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.ConsumeIndicator;
using SAC.JScript;
using SAC.Helper;
using System.Data;
using Entity.ConsumeIndicator;

namespace DJXT.ConsumeIndicator
{
    /// <summary>
    /// 导入各容量等级机组能耗数据
    /// </summary>
    public partial class InsertChartDetail : System.Web.UI.Page
    {
        BLLConsumeIndicator bc = new BLLConsumeIndicator();
        string errMsg = string.Empty;
        DateHelper dh = new DateHelper();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnImport_Click(object sender, EventArgs e)
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
            DataSet ds = GridViewExportUtil.ExcelSqlConnection(savePath, filename, "com.sac.platform.action.report");
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
                List<UnitConsumeInfo> infoList = new List<UnitConsumeInfo>();
               DateTime dt = DateTime.Parse((dr[0][2].ToString().Replace("年","-").Replace("月","-")+"01"));
                for (int i = 2; i < dr.Length - 1; i++)
                {
                    UnitConsumeInfo info = new UnitConsumeInfo();
                    if (String.IsNullOrEmpty(dr[i][0].ToString()))
                    {
                        continue;
                    }
                    info.T_TIME = dt;

                    info.T_DWNAME = dr[i][1].ToString().Trim();
                    info.T_COUNT = dr[i][2].ToString();
                    info.T_CYDL = dr[i][14].ToString();
                    //对标煤耗暂时不计算。
                    info.T_DBMH = dr[i][2].ToString();
                    info.T_GDL = dr[i][20].ToString();
                    info.T_GDMH = dr[i][23].ToString();
                    //与集团平均，暂时不计算。
                    info.T_JTPJB = dr[i][1].ToString();
                    info.T_OF = dr[i][61].ToString();
                    info.T_RDB = dr[i][46].ToString();
                    info.T_UNITCODE = dr[i][0].ToString().Trim();
                    info.T_USEHOUR = dr[i][8].ToString();

                    infoList.Add(info);
                }
                try
                {
                    if (bc.InsertChartDetailData(infoList, out errMsg))
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

        //修改或加入新数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string time = txtTimeBegin.Value.Trim();
            if (string.IsNullOrEmpty(time))
            {
                JScript.Alert("请选择月份！");
                return;
            }
            MonthConsumeInfo info = new MonthConsumeInfo();
            string year = time.Substring(0, 4);
            info.year =Convert.ToInt32(year);
            string month = string.Empty;
            
            if (time.Length>6)
            {
                month = time.Substring(5, 2);
            }
            else
            {
                month = time.Substring(5, 1);
            }
            info.month = Convert.ToInt32(month);

            double value;
            if (double.TryParse(txtValue.Text.Trim(), out value))
            {
                info.values = value;
                DataTable dt = bc.GetMonthConsumeByTime(year, month, out errMsg);
                if (dt.Rows.Count>0)
                {
                    //更新
                    if (bc.UpdateMonthConsumeByTime(info, out errMsg))
                    {
                        JScript.Alert("更新成功！");
                    }
                    else
                    {
                        JScript.Alert("更新失败！");
                    }
                }
                else
                {
                    //插入
                }
            }
            else
            {
                JScript.Alert("请输入数字！");
            }
            
           
        }
    }
}