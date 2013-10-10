using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DJXT
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtTimeBegin.Value = DateTime.Now.Year + "-" + DateTime.Now.Month;
            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            //Bind();
        }
    }
}