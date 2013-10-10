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
    public partial class GETWarningThresholdData : System.Web.UI.Page
    {
        public string errMsg = "", sec_type_real = "", electric_id_real = "", sec_crew_real="";
        StringBuilder sb = new StringBuilder();
        BLL.BLLRealQuery BCA = new BLL.BLLRealQuery();
        protected void Page_Load(object sender, EventArgs e)
        {
            sec_type_real = Request["sec_type_real"];
            electric_id_real = Request["electric_id_real"];
            sec_crew_real = Request["sec_crew_real"];
            if ((sec_type_real != "") && (sec_type_real != null))
            {

                DataSet DS = BCA.Get_Electric_Info(sec_type_real, out errMsg);
                DataSet DDS = BCA.Get_Unit_Info(DS.Tables[0].Rows[0]["T_PLANTID"].ToString(), out errMsg);
                Return_dataset(DS);
                Return_dataset(DDS);
            }
            else if ((electric_id_real != "") && (electric_id_real != null))
            {
                DataSet DS = BCA.Get_Unit_Info(electric_id_real, out errMsg);
                Return_dataset(DS);
            }
            else if ((sec_crew_real != "") && (sec_crew_real != null))
            {
                BLL.PerformanceAlarm.BLLWarningThreshold BWT = new BLL.PerformanceAlarm.BLLWarningThreshold();
                DataSet DS = BWT.GETKAOHRDIAN_DESC(sec_crew_real);
                Return_dataset(DS);
            }
            
            Response.Clear();
            Response.Write(sb.ToString());
            Response.End();
        }

        private void Return_dataset(DataSet DS)
        {
            //DataSet DS = BCA(id, level_id, para_id);
            sb.Append("<option value=-请选择->-请选择-</option>");
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    sb.Append("<option value=" + DS.Tables[0].Rows[i][0].ToString() + ">" + DS.Tables[0].Rows[i][1].ToString() + "</option>");
                }
                sb.Append("|");
            }
        }
    }
}