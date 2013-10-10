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
using Newtonsoft.Json;
using BLL;
using System.Collections.Generic;

namespace DJXT.Task
{
    public partial class XjTaskNew : System.Web.UI.Page
    {
        ParmentBLL bll = new ParmentBLL();

        string sTime = "";
        string eTime = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string param = Request["param"];
            if (param != "")
            {
                if (param == "query")
                {
                    int page = Convert.ToInt32(Request["page"].ToString());
                    int rows = Convert.ToInt32(Request["rows"].ToString());
                    sTime = Request.Form["sTime"];
                    eTime = Request.Form["eTime"];
                    GetInfo(sTime, eTime, page, rows);
                }
                else if (param == "load")
                {
                    int page = Convert.ToInt32(Request["page"].ToString());
                    int rows = Convert.ToInt32(Request["rows"].ToString());
                    sTime = Request.Form["sTime"];
                    eTime = Request.Form["eTime"];
                    GetInfo(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"), page, rows);
                }

            }
            else
            {
                GetInfo(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"), 1, 10);
            }
        }

        #region
        private void GetInfo(string sTime, string eTime, int page, int rows)
        {
            IList<Hashtable> list = bll.GetParmentCheckGrid(Convert.ToDateTime(sTime), Convert.ToDateTime(eTime), (page - 1) * rows + 1, page * rows);
            DataTable dt = bll.GetOrganizeExistPerson("");
            object obj = new
            {
                total = dt.Rows.Count,
                rows = list
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion
    }
}