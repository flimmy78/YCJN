using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using SAC.Helper;
using System.IO;
using System.Text;
using System.Collections;
using Newtonsoft.Json;
using SAC.JScript;
using SAC.Entity;

namespace YJJX.EquipmentReliable
{
    public partial class ProcessParameter : System.Web.UI.Page
    {
        string errMsg = string.Empty;
        public int _rowIndex = 0;
        BLLEquipmentReliable bl = new BLLEquipmentReliable();
        BLLBase bb = new BLLBase();


        protected void Page_Load(object sender, EventArgs e)
        {
            string param = Request["param"];
            if (param != "")
            {
                if (param == "seachList")
                {
                    GetBCList();

                }
            }
            if (!IsPostBack)
            {
                BindCompany();
                //Bind();
            }
        }

        public void GetBCList()
        {
            string companyId = Request["company"].ToString();
            string plantId = Request["plant"].ToString();
            string unitId = Request["unit"].ToString();
            string beginTime = Request["beginTime"].ToString();
            string endTime = Request["endTime"].ToString();
            int page = Convert.ToInt32(Request["page"].ToString());
            int rows = Convert.ToInt32(Request["rows"].ToString());
            int sCount = (page - 1) * rows + 1;
            int eCount = page * rows;
            //总的行数。
            int count = 0;
            DataTable dt = bl.GetInitByCondition(companyId, plantId, unitId, beginTime, endTime,sCount,eCount,out count , out errMsg);
            //int count =dt.Rows.Count;
            IList<Hashtable> list = new List<Hashtable>();

            foreach(DataRow item in dt.Rows)
            {
                Hashtable ht = new Hashtable();
                ht.Add("T_PLANTDESC", item["T_PLANTDESC"]);
                ht.Add("T_UNITDESC", item["T_UNITDESC"].ToString());
                ht.Add("D_CAPABILITY", item["D_CAPABILITY"].ToString());
                ht.Add("I_GAAG", item["I_GAAG"].ToString());
                ht.Add("I_PH", item["I_PH"].ToString());
                ht.Add("I_AH", item["I_AH"].ToString());
                ht.Add("I_SH", item["I_SH"].ToString());
                ht.Add("I_UOH", item["I_UOH"].ToString());
                ht.Add("I_FOH", item["I_FOH"].ToString());
                ht.Add("I_EUNDH", item["I_EUNDH"].ToString());
                list.Add(ht);
            }
            object obj = new
            {
                total = count,
                rows = list
            };

            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
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

        protected void ddlCompany_SelectedChanged(object sender, EventArgs e)
        {
            BindPlant();
        }

        protected void ddlPlant_SelectedChanged(object sender, EventArgs e)
        {
            BindUnit();
        }

        //protected void Bind()
        //{
        //    DataTable dt = new DataTable();
        //    dt = bl.GetInitByCondition(ddlCompany.SelectedValue.Trim(), ddlPlant.SelectedValue.Trim(), ddlUnit.SelectedValue.Trim(), txtTimeBegin.Value.ToString(), txtTimeEnd.Value.ToString(), out errMsg);

        //    if (dt.Rows.Count == 0)
        //    {
        //        dt.Rows.Add(dt.NewRow());
        //        gvm.DataSource = dt;
        //        gvm.DataBind();
        //        int columnCount = gvm.Rows[0].Cells.Count;
        //        gvm.Rows[0].Cells.Clear();
        //        gvm.Rows[0].Cells.Add(new TableCell());
        //        gvm.Rows[0].Cells[0].ColumnSpan = columnCount;
        //        gvm.Rows[0].Cells[0].BorderColor = System.Drawing.Color.Black;
        //        gvm.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Left;
        //        gvm.Rows[0].Cells[0].Text = "暂时没有信息";
        //        gvm.Rows[0].Cells[0].Style.Add("text-align", "center");
        //    }
        //    else
        //    {
        //        gvm.DataSource = dt;
        //        gvm.DataBind();

        //        this.ddlCurrentPage.Items.Clear();
        //        if (gvm.PageCount > 0)
        //        {
        //            for (int i = 1; i <= this.gvm.PageCount; i++)
        //            {
        //                this.ddlCurrentPage.Items.Add(i.ToString());
        //            }
        //            this.ddlCurrentPage.SelectedIndex = this.gvm.PageIndex;
        //        }
        //    }
        //}

        //protected void gvadm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    this.gvm.PageIndex = e.NewPageIndex;
        //}
        //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.gvm.PageIndex = this.ddlCurrentPage.SelectedIndex;
        //    Bind();
        //}
        //protected void lnkbtnFrist_Click(object sender, EventArgs e)
        //{
        //    this.gvm.PageIndex = 0;
        //    Bind();
        //}
        //protected void lnkbtnPre_Click(object sender, EventArgs e)
        //{
        //    if (this.gvm.PageIndex > 0)
        //    {
        //        this.gvm.PageIndex = this.gvm.PageIndex - 1;
        //        Bind();
        //    }
        //}
        //protected void lnkbtnNext_Click(object sender, EventArgs e)
        //{
        //    if (this.gvm.PageIndex < this.gvm.PageCount)
        //    {
        //        this.gvm.PageIndex = this.gvm.PageIndex + 1;
        //        Bind();
        //    }
        //}
        //protected void lnkbtnLast_Click(object sender, EventArgs e)
        //{
        //    this.gvm.PageIndex = this.gvm.PageCount;
        //    Bind();
        //}

        //protected void gvadm_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    this.lblCurrentPage.Text = string.Format("当前第{0}页/总共{1}页", this.gvm.PageIndex + 1, this.gvm.PageCount);

        //    //遍历所有行设置边框样式  
        //    foreach (TableCell tc in e.Row.Cells)
        //    {
        //        tc.Attributes["style"] = "border-color:#D9ECFB";
        //    }

        //    if (e.Row.RowIndex != -1)
        //    {
        //        int id = e.Row.RowIndex + 1;
        //        e.Row.Cells[0].Text = id.ToString();
        //    }

        //    //执行循环，保证每条数据都可以更新
        //    for (int i = 0; i < gvm.Rows.Count + 1; i++)
        //    {
        //        //首先判断是否是数据行
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            //当鼠标停留时更改背景色
        //            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.color='red';this.style.backgroundColor='#D9ECFB'");
        //            //当鼠标移开时还原背景色
        //            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;this.style.color='black';this.style.backgroundColor=c");
        //            e.Row.Attributes.Add("id", "row_" + i.ToString());
        //            e.Row.Attributes.Add("onclick", "Item_Click(this)");
        //            e.Row.Attributes["style"] = "Cursor:pointer";
        //        }
        //    }
        //}

        protected void Search_Click(object sender, EventArgs e)
        {
            //Bind();
        }

        protected void Export_Click(object sender, EventArgs e)
        {
            //gvm.AllowPaging = false;
            //Bind();
            //GridViewExportUtil.Export("设备可靠性过程参数.xls", gvm);
             DataTable dt = new DataTable();
             int count = 0;
             //int page = Convert.ToInt32(Request.QueryString["page"].ToString());
             //int rows = Convert.ToInt32(Request.QueryString["rows"].ToString());
             //int sCount = (page - 1) * rows + 1;
             //int eCount = page * rows;
            //导出所有符合条件的数据。
             dt = bl.GetInitByCondition(ddlCompany.SelectedValue.Trim(), ddlPlant.SelectedValue.Trim(), ddlUnit.SelectedValue.Trim(), txtTimeBegin.Value.ToString(), txtTimeEnd.Value.ToString(),0,0,out count, out errMsg);

            //GridViewExportUtil.ExportByWeb(dt, "设备可靠性过程参数", "设备可靠性过程参数.xls");
            //GridViewExportUtil.SaveToFile(dt, "设备可靠性过程参数");
            //GridViewExportUtil.ExportByWeb(dt, "设备可靠性过程参数", "设备可靠性过程参数.xls");
            //GridViewExportUtil.RenderToExcel(dt, Server.MapPath(("upfiles\\") + "设备可靠性.xls"));
            GridViewExportUtil.RenderToExcel(dt, HttpContext.Current, "设备可靠性.xls");
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
            DataSet ds = GridViewExportUtil.ExcelSqlConnection(savePath, filename, "sheet1");

            //定义一个DataRow数组
            DataRow[] dr = ds.Tables[0].Select();
            int rowsnum = ds.Tables[0].Rows.Count;
            if (rowsnum == 0)
            {
                JScript.Alert("Excel表为空表,无数据!");
            }
            else
            {
                List<UnitInfo> infoList = new List<UnitInfo>();
                for (int i = 0; i < dr.Length-1; i++)
                {
                    //前面除了你需要在建立一个“upfiles”的文件夹外，其他的都不用管了，你只需要通过下面的方式获取Excel的值，然后再将这些值用你的方式去插入到数据库里面
                    //string unitName = dr[i]["机组编号"].ToString();
                    UnitInfo info = new UnitInfo();
                    info.T_CODE = dr[i]["机组编号"].ToString();
                    info.T_TIME = dr[i]["时间"].ToString().Substring(0, 4).ToString() + "-" + dr[i]["时间"].ToString().Substring(5, 2).ToString() + "-01";
                    //info.T_BEGINTIME= DateTime.Parse(dr[i]["机组编号"].ToString());
                    //info.T_ENDTIME=  DateTime.Parse(dr[i]["机组编号"].ToString());
                    info.I_PH = Convert.ToDouble(dr[i]["统计期间小时PH"].ToString());

                    //info.T_FCATEGORYID = dr[i]["机组编号"].ToString();
                    //info.T_FPROPERTYID = dr[i]["机组编号"].ToString();
                    //info.T_FPROFEESIOID = dr[i]["机组编号"].ToString();
                    //info.T_FREASONID = dr[i]["机组编号"].ToString();
                    //info.T_EVENTDESC = dr[i]["机组编号"].ToString();
                    //info.T_REASONANALYSE = dr[i]["机组编号"].ToString();
                    //info.T_DEALCONDITION = dr[i]["机组编号"].ToString();
                    info.I_GAAG = Convert.ToDouble(dr[i]["发电量GAAG"].ToString());
                    info.I_PUMPPOWER = Convert.ToDouble(dr[i]["抽水电量"].ToString());
                    info.I_UTH = Convert.ToDouble(dr[i]["利用小时UTH"].ToString());
                    info.I_SH = Convert.ToDouble(dr[i]["运行小时SH"].ToString());
                    info.I_RH = Convert.ToDouble(dr[i]["备用小时RH"].ToString());
                    info.I_AH = Convert.ToDouble(dr[i]["可用小时AH"].ToString());
                    info.I_POH = Convert.ToDouble(dr[i]["计划停运小时POH"].ToString());
                    info.I_BPOH = Convert.ToDouble(dr[i]["大修停运小时POH1"].ToString());
                    info.I_SPOH = Convert.ToDouble(dr[i]["小修停运小时POH2"].ToString());
                    info.I_HPPOH = Convert.ToDouble(dr[i]["节日检修和公用系统计划检修停运小时POH3"].ToString());
                    info.I_IACTH = Convert.ToDouble(dr[i]["停机停运小时IACTH"].ToString());
                    info.I_FOH = Convert.ToDouble(dr[i]["强迫停运小时FOH"].ToString());
                    info.I_UOH = Convert.ToDouble(dr[i]["非计划停运小时UOH"].ToString());
                    info.I_ERFDH = Convert.ToDouble(dr[i]["备用降低出力等效小时ERFDH"].ToString());
                    info.I_EFDH = Convert.ToDouble(dr[i]["前三类降低出力等效停运小时EFDH"].ToString());
                    info.I_EUNDH = Convert.ToDouble(dr[i]["降低出力等效停运小时EUNDH"].ToString());
                    info.I_RT = Convert.ToDouble(dr[i]["备用次数RT"].ToString());
                    info.I_SST = Convert.ToDouble(dr[i]["启动成功次数SST"].ToString());
                    info.I_UST = Convert.ToDouble(dr[i]["启动失败次数UST"].ToString());
                    info.I_POT = Convert.ToDouble(dr[i]["计划停运次数POT"].ToString());
                    info.I_BPOT = Convert.ToDouble(dr[i]["计划大修停运次数POT1"].ToString());
                    info.I_SPOT = Convert.ToDouble(dr[i]["计划小修停运次数POT2"].ToString());
                    info.I_HPOT = Convert.ToDouble(dr[i]["节日检修停运次数POT3"].ToString());
                    info.I_IACTT = Convert.ToDouble(dr[i]["停机停运次数IACTT"].ToString());
                    info.I_FOT = Convert.ToDouble(dr[i]["强迫停运次数FOT"].ToString());
                    info.I_UOT = Convert.ToDouble(dr[i]["非计划停运次数UOT"].ToString());
                    info.I_FUOT = Convert.ToDouble(dr[i]["第5类非计划停运次数UOT5"].ToString());
                    info.I_LOSEPOWER = Convert.ToDouble(dr[i]["损失电量"].ToString());
                    info.D_SF = Convert.ToDouble(dr[i]["运行系数（%）SF"].ToString());
                    info.D_AF = Convert.ToDouble(dr[i]["可用系数（%）AF"].ToString());
                    info.D_UF = Convert.ToDouble(dr[i]["不可用系数（%）UF"].ToString());
                    info.D_EAF = Convert.ToDouble(dr[i]["等效可用系数（%）EAF"].ToString());
                    info.D_POF = Convert.ToDouble(dr[i]["计划停运系数（%）POF"].ToString());
                    info.D_UOF = Convert.ToDouble(dr[i]["非计划停运系数（%）UOF"].ToString());
                    info.D_FOF = Convert.ToDouble(dr[i]["强迫停运系数（%）FOF"].ToString());
                    info.D_UDF = Convert.ToDouble(dr[i]["降低出力系数（%）UDF"].ToString());
                    info.D_OF = Convert.ToDouble(dr[i]["出力系数（%）OF"].ToString());
                    info.D_GCF = Convert.ToDouble(dr[i]["毛容量系数（%）GCF"].ToString());
                    info.D_UTF = Convert.ToDouble(dr[i]["利用系数（%）UTF"].ToString());
                    info.D_FOR = Convert.ToDouble(dr[i]["强迫停运率（%）FOR"].ToString());
                    info.D_EFOR = Convert.ToDouble(dr[i]["等效强迫停运率（%）EFOR"].ToString());
                    info.D_UOR = Convert.ToDouble(dr[i]["非计划停运率（%）UOR"].ToString());
                    info.D_SR = Convert.ToDouble(dr[i]["启动可靠度（%）SR"].ToString());
                    info.D_EXR = Convert.ToDouble(dr[i]["运行暴露率（%）EXR"].ToString());
                    info.D_FOOR = Convert.ToDouble(dr[i]["强迫停运发生率（%）FOOR"].ToString());
                    info.S_MTBS = dr[i]["平均启动间隔MTBS"].ToString();
                    info.S_CAH = dr[i]["平均连续可用小时CAH"].ToString();
                    info.S_MTBF = dr[i]["平均无故障可用小时MTBF"].ToString();
                    info.S_MTTPO = dr[i]["平均计划停运间隔小时MTTPO"].ToString();
                    info.S_MTTUO = dr[i]["平均非计划停运间隔小时MTTUO"].ToString();
                    info.D_MPOD = Convert.ToDouble(dr[i]["平均计划停运小时MPOD"].ToString());
                    info.D_MUOD = Convert.ToDouble(dr[i]["平均非计划停运小时MUOD"].ToString());
                    info.S_BMTTPO = dr[i]["大修平均停运间隔小时MTTPO1"].ToString();
                    info.S_SMTTPO = dr[i]["小修平均停运间隔小时MTTPO2"].ToString();
                    info.D_BMPOD = Convert.ToDouble(dr[i]["大修平均延续小时MPOD1"].ToString());
                    info.D_SMPOD = Convert.ToDouble(dr[i]["小修平均延续小时MPOD2"].ToString());
                    info.D_KWZJ = Convert.ToDouble(dr[i]["千瓦装机发电量"].ToString());
                    info.I_GEH = Convert.ToDouble(dr[i]["发电小时GEH"].ToString());
                    info.I_PWH = Convert.ToDouble(dr[i]["抽水小时PWH"].ToString());
                    info.I_GMH = Convert.ToDouble(dr[i]["发电调相小时GMH"].ToString());
                    info.I_PMH = Convert.ToDouble(dr[i]["抽水调相小时PMH"].ToString());
                    info.I_GET = Convert.ToDouble(dr[i]["发电次数GET"].ToString());
                    info.I_PWT = Convert.ToDouble(dr[i]["抽水次数PWT"].ToString());
                    info.I_GMT = Convert.ToDouble(dr[i]["发电调相次数GMT"].ToString());
                    info.I_PMT = Convert.ToDouble(dr[i]["抽水调相次数PMT"].ToString());
                    info.I_MOE = Convert.ToDouble(dr[i]["调度停运备用小时"].ToString());
                    info.I_TOE = Convert.ToDouble(dr[i]["受累停运备用小时"].ToString());
                    info.I_FMOE = Convert.ToDouble(dr[i]["场内调度停运备用小时"].ToString());
                    info.I_CMOE = Convert.ToDouble(dr[i]["场外调度停运备用小时"].ToString());
                    info.I_MOET = Convert.ToDouble(dr[i]["调度停运备用次数"].ToString());
                    info.I_TOET = Convert.ToDouble(dr[i]["受累停运备用次数"].ToString());
                    info.I_FMOET = Convert.ToDouble(dr[i]["场内调度停运备用次数"].ToString());
                    info.I_CMOET = Convert.ToDouble(dr[i]["场外调度停运备用次数"].ToString());

                    infoList.Add(info);
                }
                try
                {
                    if (bl.InsertUnit(infoList, out errMsg))
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