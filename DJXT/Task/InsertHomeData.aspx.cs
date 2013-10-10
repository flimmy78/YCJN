using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAC.JScript;
using SAC.Helper;
using System.Data;
using Entity.Home;
using BLL.Task;

namespace DJXT.Task
{
    //导入首页excel数据
    public partial class InsertHomeData : System.Web.UI.Page
    {
        BLLTask bt = new BLLTask();
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
            DataSet ds = GridViewExportUtil.ExcelSqlConnection(savePath, filename, "电力生产");

            //定义一个DataRow数组
            DataRow[] dr = ds.Tables[0].Select();
            int rowsnum = ds.Tables[0].Rows.Count;
            if (rowsnum == 0)
            {
                JScript.Alert("Excel表为空表,无数据!");
            }
            else
            {
                List<StatisticInfo> infoList = new List<StatisticInfo>();

                try
                {

                    //前面除了你需要在建立一个“upfiles”的文件夹外，其他的都不用管了，你只需要通过下面的方式获取Excel的值，然后再将这些值用你的方式去插入到数据库里面
                    StatisticInfo info = new StatisticInfo();
                    //设备容量
                    info.T_INDICATORNAME = "设备容量";
                    info.T_UNITNAME = dr[6][1].ToString();
                    info.T_TIME = DateTime.Parse((dr[1][4].ToString()+"1日")).ToString().Replace('/','-');
                    info.D_HNALL = Convert.ToDouble(dr[6][2].ToString());
                    info.D_HNADD = Convert.ToDouble(dr[6][3].ToString());
                    info.D_DTALL = Convert.ToDouble(dr[6][4].ToString());
                    info.D_DTADD = Convert.ToDouble(dr[6][5].ToString());
                    info.D_HDALL = Convert.ToDouble(dr[6][6].ToString());
                    info.D_HDADD = Convert.ToDouble(dr[6][7].ToString());
                    info.D_GDALL = Convert.ToDouble(dr[6][8].ToString());
                    info.D_GDADD = Convert.ToDouble(dr[6][9].ToString());
                    info.D_ZDTALL = Convert.ToDouble(dr[6][10].ToString());
                    info.D_ZDTADD = Convert.ToDouble(dr[6][11].ToString());
                    infoList.Add(info);

                    //利用小时
                    info = new StatisticInfo();
                    info.T_INDICATORNAME = "利用小时";
                    info.T_UNITNAME = dr[23][1].ToString();
                    info.T_TIME = DateTime.Parse((dr[1][4].ToString() + "1日")).ToString().Replace('/', '-');
                    info.D_HNALL = Convert.ToDouble(dr[23][2].ToString());
                    info.D_HNADD = Convert.ToDouble(dr[23][3].ToString());
                    info.D_DTALL = Convert.ToDouble(dr[23][4].ToString());
                    info.D_DTADD = Convert.ToDouble(dr[23][5].ToString());
                    info.D_HDALL = Convert.ToDouble(dr[23][6].ToString());
                    info.D_HDADD = Convert.ToDouble(dr[23][7].ToString());
                    info.D_GDALL = Convert.ToDouble(dr[23][8].ToString());
                    info.D_GDADD = Convert.ToDouble(dr[23][9].ToString());
                    info.D_ZDTALL = Convert.ToDouble(dr[23][10].ToString());
                    info.D_ZDTADD = Convert.ToDouble(dr[23][11].ToString());
                    infoList.Add(info);

                    //供电煤耗
                    info = new StatisticInfo();
                    info.T_INDICATORNAME = "供电煤耗";
                    info.T_UNITNAME = dr[32][1].ToString();
                    info.T_TIME = DateTime.Parse((dr[1][4].ToString() + "1日")).ToString().Replace('/', '-');
                    info.D_HNALL = Convert.ToDouble(dr[32][2].ToString());
                    info.D_HNADD = Convert.ToDouble(dr[32][3].ToString());
                    info.D_DTALL = Convert.ToDouble(dr[32][4].ToString());
                    info.D_DTADD = Convert.ToDouble(dr[32][5].ToString());
                    info.D_HDALL = Convert.ToDouble(dr[32][6].ToString());
                    info.D_HDADD = Convert.ToDouble(dr[32][7].ToString());
                    info.D_GDALL = Convert.ToDouble(dr[32][8].ToString());
                    info.D_GDADD = Convert.ToDouble(dr[32][9].ToString());
                    info.D_ZDTALL = Convert.ToDouble(dr[32][10].ToString());
                    info.D_ZDTADD = Convert.ToDouble(dr[32][11].ToString());
                    infoList.Add(info);

                    //厂用电率
                    info = new StatisticInfo();
                    info.T_INDICATORNAME = "厂用电率";
                    info.T_UNITNAME = dr[40][1].ToString();
                    info.T_TIME = DateTime.Parse((dr[1][4].ToString() + "1日")).ToString().Replace('/', '-');
                    info.D_HNALL = Convert.ToDouble(dr[40][2].ToString());
                    info.D_HNADD = Convert.ToDouble(dr[40][3].ToString());
                    info.D_DTALL = Convert.ToDouble(dr[40][4].ToString());
                    info.D_DTADD = Convert.ToDouble(dr[40][5].ToString());
                    info.D_HDALL = Convert.ToDouble(dr[40][6].ToString());
                    info.D_HDADD = Convert.ToDouble(dr[40][7].ToString());
                    info.D_GDALL = Convert.ToDouble(dr[40][8].ToString());
                    info.D_GDADD = Convert.ToDouble(dr[40][9].ToString());
                    info.D_ZDTALL = Convert.ToDouble(dr[40][10].ToString());
                    info.D_ZDTADD = Convert.ToDouble(dr[40][11].ToString());
                    infoList.Add(info);


                    //供电煤耗1000MW
                    info = new StatisticInfo();
                    info.T_INDICATORNAME = "1000MW";
                    info.T_UNITNAME = dr[34][1].ToString();
                    info.T_TIME = DateTime.Parse((dr[1][4].ToString() + "1日")).ToString().Replace('/', '-');
                    info.D_HNALL = Convert.ToDouble(dr[34][2].ToString());
                    info.D_HNADD = Convert.ToDouble(dr[34][3].ToString());
                    info.D_DTALL = Convert.ToDouble(dr[34][4].ToString());
                    info.D_DTADD = Convert.ToDouble(dr[34][5].ToString());
                    info.D_HDALL = Convert.ToDouble(dr[34][6].ToString());
                    info.D_HDADD = Convert.ToDouble(dr[34][7].ToString());
                    info.D_GDALL = Convert.ToDouble(dr[34][8].ToString());
                    info.D_GDADD = Convert.ToDouble(dr[34][9].ToString());
                    info.D_ZDTALL = Convert.ToDouble(dr[34][10].ToString());
                    info.D_ZDTADD = Convert.ToDouble(dr[34][11].ToString());
                    infoList.Add(info);


                    //供电煤耗600MW
                    info = new StatisticInfo();
                    info.T_INDICATORNAME = "600MW";
                    info.T_UNITNAME = dr[35][1].ToString();
                    info.T_TIME = DateTime.Parse((dr[1][4].ToString() + "1日")).ToString().Replace('/', '-');
                    info.D_HNALL = Convert.ToDouble(dr[35][2].ToString());
                    info.D_HNADD = Convert.ToDouble(dr[35][3].ToString());
                    info.D_DTALL = Convert.ToDouble(dr[35][4].ToString());
                    info.D_DTADD = Convert.ToDouble(dr[35][5].ToString());
                    info.D_HDALL = Convert.ToDouble(dr[35][6].ToString());
                    info.D_HDADD = Convert.ToDouble(dr[35][7].ToString());
                    info.D_GDALL = Convert.ToDouble(dr[35][8].ToString());
                    info.D_GDADD = Convert.ToDouble(dr[35][9].ToString());
                    info.D_ZDTALL = Convert.ToDouble(dr[35][10].ToString());
                    info.D_ZDTADD = Convert.ToDouble(dr[35][11].ToString());
                    infoList.Add(info);


                    //供电煤耗300MW
                    info = new StatisticInfo();
                    info.T_INDICATORNAME = "300MW";
                    info.T_UNITNAME = dr[36][1].ToString();
                    info.T_TIME = DateTime.Parse((dr[1][4].ToString() + "1日")).ToString().Replace('/', '-');
                    info.D_HNALL = Convert.ToDouble(dr[36][2].ToString());
                    info.D_HNADD = Convert.ToDouble(dr[36][3].ToString());
                    info.D_DTALL = Convert.ToDouble(dr[36][4].ToString());
                    info.D_DTADD = Convert.ToDouble(dr[36][5].ToString());
                    info.D_HDALL = Convert.ToDouble(dr[36][6].ToString());
                    info.D_HDADD = Convert.ToDouble(dr[36][7].ToString());
                    info.D_GDALL = Convert.ToDouble(dr[36][8].ToString());
                    info.D_GDADD = Convert.ToDouble(dr[36][9].ToString());
                    info.D_ZDTALL = Convert.ToDouble(dr[36][10].ToString());
                    info.D_ZDTADD = Convert.ToDouble(dr[36][11].ToString());
                    infoList.Add(info);


                    //供电煤耗200MW
                    info = new StatisticInfo();
                    info.T_INDICATORNAME = "200MW";
                    info.T_UNITNAME = dr[37][1].ToString();
                    info.T_TIME = DateTime.Parse((dr[1][4].ToString() + "1日")).ToString().Replace('/', '-');
                    info.D_HNALL = Convert.ToDouble(dr[37][2].ToString());
                    info.D_HNADD = Convert.ToDouble(dr[37][3].ToString());
                    info.D_DTALL = Convert.ToDouble(dr[37][4].ToString());
                    info.D_DTADD = Convert.ToDouble(dr[37][5].ToString());
                    info.D_HDALL = Convert.ToDouble(dr[37][6].ToString());
                    info.D_HDADD = Convert.ToDouble(dr[37][7].ToString());
                    info.D_GDALL = Convert.ToDouble(dr[37][8].ToString());
                    info.D_GDADD = Convert.ToDouble(dr[37][9].ToString());
                    info.D_ZDTALL = Convert.ToDouble(dr[37][10].ToString());
                    info.D_ZDTADD = Convert.ToDouble(dr[37][11].ToString());
                    infoList.Add(info);

                   

                }
                catch
                {
                    JScript.Alert("Excle表格数据有误");
                }
                try
                {
                    if (bt.InsertHomeData(infoList, out errMsg))
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