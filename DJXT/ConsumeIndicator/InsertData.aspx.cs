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
    //导入数据对标
    public partial class InsertData : System.Web.UI.Page
    {
        BLLConsumeIndicator bc = new BLLConsumeIndicator();
        string errMsg = string.Empty;

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
            if ((IsXls != ".xls")&&(IsXls!=".xlsx"))
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
            DataSet ds = GridViewExportUtil.ExcelSqlConnection(savePath, filename, "五大集团");

            //定义一个DataRow数组
            DataRow[] dr = ds.Tables[0].Select();
            int rowsnum = ds.Tables[0].Rows.Count;
            if (rowsnum == 0)
            {
                JScript.Alert("Excel表为空表,无数据!");
            }
            else
            {
                List<DataInfo> infoList = new List<DataInfo>();
                //时间
                //string time = dr[1][1].ToString();
                //for (int i = 2; i < dr.Length - 1; )
                //{
                //    int j;
                //    //合并五行
                //    for (j = 0; j < 5; j++)
                //    {
                //        DataInfo info = new DataInfo();
                //        if (String.IsNullOrEmpty(dr[i][0].ToString()))
                //        {
                //            break;
                //        }
                //        //防止超出excel行数
                //        if (i + j > dr.Length-1)
                //        {
                //            break;
                //        }
                //        info.T_COMPANY = dr[i][0].ToString();
                //        info.T_DATATYPE = dr[i + j][1].ToString();

                //        info.T_900_SL = dr[i + j][2].ToString();
                //        info.T_900_KL = dr[i + j][3].ToString();
                //        info.T_600_HJ = dr[i + j][4].ToString();
                //        info.T_600_CCL = dr[i + j][5].ToString();
                //        info.T_600_CLS = dr[i + j][6].ToString();
                //        info.T_600_CLK = dr[i + j][7].ToString();
                //        info.T_600_YLS = dr[i + j][8].ToString();
                //        info.T_600_YLK = dr[i + j][9].ToString();
                //        info.T_600_J = dr[i + j][10].ToString();
                //        info.T_350_HJ = dr[i + j][11].ToString();
                //        info.T_350_CNHJ = dr[i + j][12].ToString();
                //        info.T_350_CLJ = dr[i + j][13].ToString();
                //        info.T_350_YLJ = dr[i + j][14].ToString();
                //        info.T_350_GRHJ = dr[i + j][15].ToString();
                //        info.T_350_RCLJ = dr[i + j][16].ToString();
                //        info.T_350_RYLJ = dr[i + j][17].ToString();
                //        info.T_300_HJ = dr[i + j][18].ToString();
                //        info.T_300_CGSL = dr[i + j][19].ToString();
                //        info.T_300_CGKL = dr[i + j][20].ToString();
                //        info.T_300_LHC = dr[i + j][21].ToString();
                //        info.T_300_SL = dr[i + j][22].ToString();
                //        info.T_300_KL = dr[i + j][23].ToString();
                //        info.T_200_HJ = dr[i + j][24].ToString();
                //        info.T_200_SL = dr[i + j][25].ToString();
                //        info.T_200_KL = dr[i + j][26].ToString();
                //        info.T_200_LHC = dr[i + j][27].ToString();
                //        info.T_200_J = dr[i + j][28].ToString();
                //        info.T_120_HJ = dr[i + j][29].ToString();
                //        info.T_120_J = dr[i + j][30].ToString();
                //        info.T_120_LHC = dr[i + j][31].ToString();
                //        info.T_120_KL = dr[i + j][32].ToString();
                //        info.T_120_JC = dr[i + j][33].ToString();
                //        info.T_135_LHC = dr[i + j][34].ToString();
                //        info.T_100_HJ = dr[i + j][35].ToString();
                //        info.T_100_CG = dr[i + j][36].ToString();
                //        info.T_100_LHC = dr[i + j][37].ToString();
                //        info.T_100_J = dr[i + j][38].ToString();
                //        info.T_90_HJ = dr[i + j][39].ToString();
                //        info.T_90_NJ = dr[i + j][40].ToString();
                //        info.T_90_LHC = dr[i + j][41].ToString();
                //        info.T_90_RJ = dr[i + j][42].ToString();

                //        infoList.Add(info);
                //    }
                //    i = i + j;
                //}
                string time = DateTime.Now.Year.ToString();
                int lee=dr[0][0].ToString().Length;
                string month = dr[0][0].ToString().TrimStart().Substring(3,1);
                int resul = 0;
                if (int.TryParse(month, out resul))
                {
                    month = dr[0][0].ToString().Substring(2, 2);
                }
                else
                {
                    month ="0"+dr[0][0].ToString().Substring(2, 1);
                }
                time += "-" + month;
                //dateTime = DateTime.Parse(time);

                for (int i = 3; i < dr.Length - 1; )
                {
                    if (String.IsNullOrEmpty(dr[i][0].ToString()))
                    {
                        break;
                    }
                    int j;
                    string  company = dr[i][0].ToString();
                    //合并八行
                    for (j = 0; j < 8; j++)
                    {
                        DataInfo info = new DataInfo();

                        //防止超出excel行数
                        if (i + j > dr.Length - 1)
                        {
                            break;
                        }
                        if ((dr[i + j][1].ToString().Trim() != "供电煤耗") && (dr[i + j][1].ToString().Trim() != "供电煤耗最优值") && (dr[i + j][1].ToString().Trim() != "供电煤耗最优机组"))
                        {
                            continue;
                        }

                        switch (dr[i + j][1].ToString().Trim())
                        {
                            case "供电煤耗": info.T_DATATYPE = "平均值";
                                break;
                            case "供电煤耗最优值": info.T_DATATYPE = "最优值";
                                break;
                            case "供电煤耗最优机组": info.T_DATATYPE = "最优机组";
                                break;
                        }
                        info.T_COMPANY = company;
                        info.T_TIME = time;

                        info.T_900_SL = dr[i + j][2].ToString();
                        info.T_900_KL = dr[i + j][3].ToString();
                        info.T_600_HJ = dr[i + j][4].ToString();
                        info.T_600_CCL = dr[i + j][5].ToString();
                        info.T_600_CLS = dr[i + j][6].ToString();
                        info.T_600_CLK = dr[i + j][7].ToString();
                        info.T_600_YLS = dr[i + j][8].ToString();
                        info.T_600_YLK = dr[i + j][9].ToString();
                        info.T_600_J = dr[i + j][10].ToString();
                        info.T_350_HJ = dr[i + j][11].ToString();
                        info.T_350_CNHJ = dr[i + j][12].ToString();
                        info.T_350_CLJ = dr[i + j][13].ToString();
                        info.T_350_YLJ = dr[i + j][14].ToString();
                        info.T_350_GRHJ = dr[i + j][15].ToString();
                        info.T_350_RCLJ = dr[i + j][16].ToString();
                        info.T_350_RYLJ = dr[i + j][17].ToString();
                        info.T_300_HJ = dr[i + j][18].ToString();
                        info.T_300_CGSL = dr[i + j][19].ToString();
                        info.T_300_CGKL = dr[i + j][20].ToString();
                        info.T_300_LHC = dr[i + j][21].ToString();
                        info.T_300_SL = dr[i + j][22].ToString();
                        info.T_300_KL = dr[i + j][23].ToString();
                        info.T_200_HJ = dr[i + j][24].ToString();
                        info.T_200_SL = dr[i + j][25].ToString();
                        info.T_200_KL = dr[i + j][26].ToString();
                        info.T_200_LHC = dr[i + j][27].ToString();
                        info.T_200_J = dr[i + j][28].ToString();
                        info.T_120_HJ = dr[i + j][29].ToString();
                        info.T_120_J = dr[i + j][30].ToString();
                        info.T_120_LHC = dr[i + j][31].ToString();
                        info.T_120_KL = dr[i + j][32].ToString();
                        info.T_120_JC = dr[i + j][33].ToString();
                        info.T_135_LHC = dr[i + j][34].ToString();
                        info.T_100_HJ = dr[i + j][35].ToString();
                        info.T_100_CG = dr[i + j][36].ToString();
                        info.T_100_LHC = dr[i + j][37].ToString();
                        info.T_100_J = dr[i + j][38].ToString();
                        info.T_90_HJ = dr[i + j][39].ToString();
                        info.T_90_NJ = dr[i + j][40].ToString();
                        info.T_90_LHC = dr[i + j][41].ToString();
                        info.T_90_RJ = dr[i + j][42].ToString();

                        infoList.Add(info);
                    }
                    i=i+8;

                    //再增加最差机组和最差值
                    DataInfo infos = new DataInfo();
                    infos.T_COMPANY = company;
                    infos.T_TIME = time;
                    infos.T_DATATYPE = "最差值";
                    infoList.Add(infos);
                    DataInfo infoss = new DataInfo();
                    infoss.T_COMPANY = company;
                    infoss.T_TIME = time;
                    infoss.T_DATATYPE = "最差机组";
                    infoList.Add(infoss);
                }
                try
                {
                    if (bc.InsertData(infoList, out errMsg))
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