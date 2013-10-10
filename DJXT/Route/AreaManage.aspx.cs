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
using SAC.DB2;

namespace DJXT.Manage
{
    public partial class AreaManage : System.Web.UI.Page
    {
        int seqnum;
        string errMsg = "";

        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                //设置公司名下拉列表的值
                //this.tjdrp_corp.DataSource = GetDataRecord.getCorpDropList(olecon);
                //this.tjdrp_corp.DataTextField = "CORPNAME";
                ////this.drp_corp.DataTextField = "CORPID";
                //this.tjdrp_corp.DataValueField = "CORPID";
                //this.tjdrp_corp.DataBind();
                //this.tjdrp_corp.Items.Insert(0, new ListItem("全部", "0"));//添加的“=请选择=”行

                ////设置电厂名下拉列表的值
                //this.tjdrp_plant.DataSource = GetDataRecord.getDamPlantDropList(olecon, this.tjdrp_corp.SelectedValue);
                //this.tjdrp_plant.DataTextField = "NICKNAME";
                ////this.drp_changming.DataTextField = "PLANTID";
                //this.tjdrp_plant.DataValueField = "PLANTID";
                //this.tjdrp_plant.DataBind();
                //this.tjdrp_plant.Items.Insert(0, new ListItem("全部", "0"));//添加的“=请选择=”行

                //生成gridview
                GridView1_Bind();
            }


        }       

        protected void GridView1_Bind()
        {
            //GridView1.PageIndex = 0;

            //查询数据
            getDS();
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.GridView1.DataSource = ds;
                this.GridView1.DataBind();

                //设置格式
                GridView1.Attributes.Add("BorderColor", "lightBlue");
            }
            else
            {
                this.GridView1.DataSource = ds;
                this.GridView1.DataBind();
            }
        }

        //分页
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            GridView1.PageIndex = e.NewPageIndex;
            //num = GridView1.PageSize * GridView1.PageIndex;
            //查询数据
            getDS();
            //  绑定结果
            this.GridView1.DataSource = ds;
            this.GridView1.DataBind();
            //设置格式
            GridView1.Attributes.Add("BorderColor", "lightBlue");

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //string dsql;
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    //num = num + 1;
            //    //e.Row.Cells[0].Text = num.ToString();

            //}
            ////加入鼠标滑过的高亮效果
            //if (e.Row.RowType == DataControlRowType.DataRow)//判定当前的行是否属于datarow类型的行
            //{
            //    //当鼠标放上去的时候 先保存当前行的背景颜色 并给附一颜色
            //    e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E4F6FF',this.style.fontWeight='';");
            //    //当鼠标离开的时候 将背景颜色还原的以前的颜色
            //    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            //}

            ////为电厂名DropDownList绑定值
            //if (((DropDownList)e.Row.FindControl("drp_plant")) != null)
            //{
            //    //电厂名下拉列表
            //    dsql = @"select distinct * from T_SYS_PLANT ";
            //    DataSet ds1 = GetDataRecord.getDataSet(sqlcon, dsql);
            //    DropDownList ddlplant = (DropDownList)e.Row.FindControl("drp_plant");
            //    ddlplant.DataSource = ds1.Tables[0].DefaultView;
            //    ddlplant.DataTextField = "PLANTNAME";
            //    ddlplant.DataValueField = "PLANTNAME";
            //    ddlplant.DataBind();
            //    ddlplant.SelectedValue = ds.Tables[0].Rows[e.Row.DataItemIndex]["电厂名"].ToString();
            //}

            ////为发送频率DropDownList绑定值
            //if (((DropDownList)e.Row.FindControl("drp_fspl")) != null)
            //{
            //    DropDownList ddlFspl = (DropDownList)e.Row.FindControl("drp_fspl");
            //    ddlFspl.Items.Insert(0, new ListItem("日数据", "1"));
            //    ddlFspl.Items.Insert(1, new ListItem("实时数据", "0"));
            //    ddlFspl.SelectedValue = ds.Tables[0].Rows[e.Row.DataItemIndex]["发送频率"].ToString();
            //}
            //else if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if (ds.Tables[0].Rows[e.Row.DataItemIndex]["发送频率"].ToString() == "1")
            //    {
            //        e.Row.Cells[4].Text = "日数据";
            //    }
            //    else
            //    {
            //        e.Row.Cells[4].Text = "实时数据";
            //    }
            //}
        }

        //删除数据
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (GridView2.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "onekey", "alert('正在进行添加，禁止删除！')", true);
            }
            else if (GridView1.EditIndex != -1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "onekey", "alert('正在进行编辑，禁止删除！')", true);
            }
            else
            {
                string sql = "";
                sql = @"delete from T_BASE_AREA where ID_KEY = " + GridView1.DataKeys[e.RowIndex].Values[0];

                bool falg = DBdb2.RunNonQuery(sql, out errMsg);
                //num = GridView1.PageSize * GridView1.PageIndex;
                GridView1_Bind();
            }
        }

        /// <summary>
        /// 编辑当前行
        /// </summary>
        /// <param ></param>
        /// <param ></param>
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (GridView2.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "onekey", "alert('正在进行添加，禁止编辑！')", true);
            }
            else
            {
                GridView1.EditIndex = e.NewEditIndex;
                //当前编辑行背景色高亮
                this.GridView1.EditRowStyle.BackColor = System.Drawing.Color.FromName("#F7CE90");
                //num = GridView1.PageSize * GridView1.PageIndex;
                GridView1_Bind();
            }
            // (GridView1.Rows[GridView1.EditIndex].Cells[0].Controls[0] as TextBox).ReadOnly = true; 
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string sql = "";
            try
            {               
                sql = @"update T_BASE_AREA set T_AREAID = '" + ((TextBox)this.GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text
                + "',T_AREANAME = '" + ((TextBox)this.GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text              
                + "' where ID_KEY =  " + GridView1.DataKeys[e.RowIndex].Values[0];

                bool falg=DBdb2.RunNonQuery(sql, out errMsg);
                GridView1.EditIndex = -1;
                //num = GridView1.PageSize * GridView1.PageIndex;
                GridView1_Bind();
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "onekey", "alert('更新失败!')", true);
            }

        }
        /// <summary>
        /// 取消编辑状态
        /// </summary>
        /// <param ></param>
        /// <param ></param>
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            //num = GridView1.PageSize * GridView1.PageIndex;
            GridView1_Bind();
        }

        protected void LBtnAdd_Click(object sender, EventArgs e)
        {
            if (GridView2.Rows.Count > 0)
            {
            }
            else if (GridView1.EditIndex != -1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "onekey", "alert('正在进行编辑，禁止添加！')", true);
            }
            else
            {
                if (this.GridView1.Rows.Count < 1)
                    this.GridView2.ShowHeader = true;
                else
                    this.GridView2.ShowHeader = false;
                //DataTable dt = this.GetDataFromGrid();
                DataTable dt = new DataTable("Table1");
                DataRow newRow = dt.NewRow();

                dt.Rows.Add(newRow);
                this.GridView2.DataSource = dt;
                this.GridView2.DataBind();
            }
        }

        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {          
            try
            {
                string sql = "";
                sql = @"insert into T_BASE_AREA (T_AREAID,T_AREANAME) values('"
                    + ((TextBox)this.GridView2.Rows[0].FindControl("cdm")).Text + "','"
                    + ((TextBox)this.GridView2.Rows[0].FindControl("jzm")).Text + "')";
                
                bool falg=DBdb2.RunNonQuery(sql,out errMsg);

                GridView2.EditIndex = -1;
                //num = GridView1.PageSize * GridView1.PageIndex;
                GridView1_Bind();
                this.GridView2.DataBind();
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "onekey", "alert('插入失败!')", true);
            }
        }

        /// <summary>
        /// 取消编辑状态
        /// </summary>
        /// <param ></param>
        /// <param ></param>
        protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView2.EditIndex = -1;
            this.GridView2.DataBind();
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //string dsql;

            ////为电厂名DropDownList绑定值
            //if (((DropDownList)e.Row.FindControl("drp_editplant")) != null)
            //{
            //    //电厂名下拉列表
            //    dsql = @"select distinct * from T_SYS_PLANT ";
            //    DataSet ds1 = GetDataRecord.getDataSet(sqlcon, dsql);
            //    DropDownList ddlplant = (DropDownList)e.Row.FindControl("drp_editplant");
            //    ddlplant.DataSource = ds1.Tables[0].DefaultView;
            //    ddlplant.DataTextField = "PLANTNAME";
            //    ddlplant.DataValueField = "PLANTNAME";
            //    ddlplant.DataBind();
            //}

            ////为发送频率DropDownList绑定值
            //if (((DropDownList)e.Row.FindControl("drp_editfspl")) != null)
            //{
            //    DropDownList ddlFspl = (DropDownList)e.Row.FindControl("drp_editfspl");
            //    ddlFspl.Items.Insert(0, new ListItem("日数据", "1"));
            //    ddlFspl.Items.Insert(1, new ListItem("实时数据", "0"));
            //}

        }

        //查询数据
        public void getDS()
        {
            string sql = "";

            //  监视设备,记录表,启动限值,停止限值,机组容量,环保机组,数字点
            sql = @"select ROW_NUMBER() OVER(order by ID_KEY) as seqnum,ID_KEY,T_AREAID,T_AREANAME "
                + " from T_BASE_AREA ";

            ds = DBdb2.RunDataSet(sql, out errMsg);
            GridView1.DataKeyNames = new string[] { "ID_KEY" };
        }

        protected void LBtnSel_Click(object sender, EventArgs e)
        {
            //生成gridview
            GridView1_Bind();
        }
    }
}
