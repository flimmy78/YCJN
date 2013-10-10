using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Collections;

namespace DJXT.DataFile
{
    public partial class GetBoilerCalculation : System.Web.UI.Page
    {
        public string errMsg = "", Boiler = "", Boiler_insert = "", pre_query = "";
        StringBuilder sb = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            Boiler = Request["Boiler"];
            Boiler_insert = Request["Boiler_insert"];
            if ((Boiler != "") && (Boiler != null))
            {
                BLL.BLLBoiler BB = new BLL.BLLBoiler();

                DataSet DS = BB.GetBoilerData(Boiler);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[0].Columns.Count; i++)
                    {
                        sb.Append(DS.Tables[0].Rows[0][i].ToString() + ",");
                    }
                    sb.ToString().TrimEnd(',');
                }
            }
            else if ((Boiler_insert != "") && (Boiler_insert != null))
            {
                BLL.BLLBoiler BB = new BLL.BLLBoiler();
                int num = 0;
                num = BB.InsertBoilerData(Boiler_insert + ",'" + DateTime.Now + "'");
                sb.Append(num);
            }

            Response.Clear();
            Response.Write(sb.ToString());
            Response.End();  
        }
    }
}