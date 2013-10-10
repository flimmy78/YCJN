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
    public partial class AreaManageSelcet : System.Web.UI.Page
    {
        int seqnum;
        string errMsg = "";

        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {                
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
