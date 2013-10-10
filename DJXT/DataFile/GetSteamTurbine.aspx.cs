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
    public partial class GetSteamTurbine : System.Web.UI.Page
    {

        public string errMsg = "", Steam = "", Steam_insert = "";
        StringBuilder sb = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            Steam = Request["Steam"];
            Steam_insert = Request["Steam_insert"];
            if ((Steam != "") && (Steam != null))
            {

                BLL.BLLSteamTurbine BB = new BLL.BLLSteamTurbine();

                DataSet DS = BB.GetSteamTurbineData(Steam);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[0].Columns.Count; i++)
                    {
                        sb.Append(DS.Tables[0].Rows[0][i].ToString() + ",");
                    }
                    sb.ToString().TrimEnd(',');
                }
            }
            else if ((Steam_insert != "") && (Steam_insert != null))
            {
                BLL.BLLSteamTurbine BB = new BLL.BLLSteamTurbine();
                int num = 0;
                num = BB.InsertSteamTurbineData(Steam_insert + ",'" + DateTime.Now + "'");
                sb.Append(num);
            }
            Response.Clear();
            Response.Write(sb.ToString());
            Response.End();  
        }
    }
}