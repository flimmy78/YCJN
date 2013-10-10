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
using BLL;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace DJXT.ParentMember
{
    public partial class Check : System.Web.UI.Page
    {
        string errMsg = "";
        QuestcompleteBLL bll = new QuestcompleteBLL();
        BLL.BLLManRoute route = new BLLManRoute();
        BLL.BLLRole bl = new BLLRole();
        StringBuilder st = new StringBuilder();
        bool res = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            string param = Request.Form["param"];

            if (param != "")
            {

            }
            else
            {
                ShowRouteTree();
                // judgeMemberByClassID();
            }
        }

        #region 加载线路Tree
        private void ShowRouteTree()
        {
            DataTable dt = route.GetRouteTree();

            st.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i]["PARMENTID"].ToString()) == 0)
                    st.Append("{id:'" + dt.Rows[i]["ID"] + "',ide:'" + dt.Rows[i]["IDE"] + "', pId:'" + dt.Rows[i]["PARMENTID"] + "',name:'" + dt.Rows[i]["NAME"] + "',t:'" + dt.Rows[i]["NAME"] + "', open:true},");
                else
                    st.Append("{id:'" + dt.Rows[i]["ID"] + "',ide:'" + dt.Rows[i]["IDE"] + "',pId:'" + dt.Rows[i]["PARMENTID"] + "',name:'" + dt.Rows[i]["NAME"] + "',t:'" + dt.Rows[i]["NAME"] + "',doCheck:false},");
            }

            string resultMenu = st.ToString().Substring(0, st.ToString().Length - 1) + "]";
            object obj = new
            {
                count = 1,
                menu = resultMenu
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion
    }
}
