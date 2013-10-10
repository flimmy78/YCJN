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
    public partial class DjTaskNew : System.Web.UI.Page
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
                    sTime = Request.Form["sTime"];
                    eTime = Request.Form["eTime"];
                }
            }
            else
            {
                GetInfo(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
            }
        }

        #region
        private void GetInfo(string sTime, string eTime)
        {
            IList<Hashtable> list = bll.GetCheckGrid(Convert.ToDateTime(sTime), Convert.ToDateTime(eTime));
            object obj = new
            {
                total = list.Count,
                rows = list
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion
    }
}