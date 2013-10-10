using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DJXT.RealTimeMonitor
{
    public partial class RealTime : System.Web.UI.Page
    {
        public string urlName = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string name = Request.Params["name"];
                if (!String.IsNullOrEmpty(name))
                {
                    //litName.Text = name.Replace("/", " ").ToString();
                    urlName = name;
                    //switch (name)
                    //{
                    //    case "boiler":
                    //        urlName = "Boiler.PDI";
                    //        break;
                    //    case "steam":
                    //        urlName = "SteamTurbine.pdi";
                    //        break;
                    //    case "changyongdian":
                    //        urlName = "ChangYongDian.pdi";
                    //        break;
                    //    case "zhendong":
                    //        urlName = "ZhenDong.pdi";
                    //        break;
                    
                    
                    
                    //}
                
                }
            }
        }
    }
}