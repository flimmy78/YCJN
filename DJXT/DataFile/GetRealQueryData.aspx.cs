using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Collections;
using System.Web.UI.WebControls;

///创建者：刘海杰
///创建日期：2013-07-03
namespace DJXT.Datafile
{
    public partial class GetRealQueryData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string real_data_real = Request["real_data_real"];
            string[] str_data = new string[real_data_real.Split(';').Length - 1];
            string stime = null;
            BLL.BLLRealQuery BRQ = new BLL.BLLRealQuery();
            for (int i = 0; i < real_data_real.Split(';').Length; i++)
            {
                if (i != real_data_real.Split(';').Length - 1)
                {
                    str_data[i] += real_data_real.Split(';')[i].Split(',')[0];
                }
                else
                {
                    stime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            string list_real = BRQ.GetChartRealData(str_data, stime);

            Response.Clear();
            Response.Write(list_real);
            Response.End();
        }
    }
}