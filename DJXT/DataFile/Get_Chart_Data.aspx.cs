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
    public partial class Get_Chart_Data : System.Web.UI.Page
    {
        public string sec_type = "", errMsg = "", electric_id = "", crew_id = "", sec_type_real = "", electric_id_real = "", crew_id_real="";
        StringBuilder sb = new StringBuilder();
        BLL.BLLRealQuery BCA = new BLL.BLLRealQuery();

        protected void Page_Load(object sender, EventArgs e)
        {
            sec_type = Request["sec_type"]; electric_id = Request["electric_id"]; crew_id = Request["crew_id"]; sec_type_real = Request["sec_type_real"]; electric_id_real = Request["electric_id_real"];
            crew_id_real = Request["crew_id_real"]; 
            
            //风电、太阳能选项改变时，绑定DropDownLIst数据
            if ((sec_type != "") && (sec_type != null))
            {
                DataSet DS = BCA.Get_Electric_Info(sec_type, out errMsg);
                DataSet DDS = BCA.Get_Unit_Info(DS.Tables[0].Rows[0]["T_PLANTID"].ToString(), out errMsg);
                Return_dataset(DS);
                Return_dataset(DDS);
                DataSet DDDS= BCA.Get_BASE_CRICPARA(DDS.Tables[0].Rows[0]["T_UNITID"].ToString(), out errMsg);
                int count = 0;
                if (DDDS.Tables[0].Rows.Count > 0)
                {
                    sb.Append("<table><tr>");
                    for (int i = 0; i < DDDS.Tables[0].Rows.Count; i++)
                    {
                        count++;
                        if (count % 3 == 0)
                        {
                            sb.Append("<td width=\"300px\"><input type='checkbox' name='" + DDDS.Tables[0].Rows[i][1].ToString() + "' value='" + DDDS.Tables[0].Rows[i][0].ToString() + "'>&nbsp;&nbsp;&nbsp;" + DDDS.Tables[0].Rows[i][1].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br></td></tr>");
                        }
                        else
                        {
                            sb.Append("<td width=\"300px\"><input type='checkbox' name='" + DDDS.Tables[0].Rows[i][1].ToString() + "' value='" + DDDS.Tables[0].Rows[i][0].ToString() + "'>&nbsp;&nbsp;&nbsp;" + DDDS.Tables[0].Rows[i][1].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>");
                        }
                        if ((count % 3 != 0) && (i == DDDS.Tables[0].Rows.Count - 1))
                        {
                            sb.Append("</tr>");
                        }
                        //sb.Append(DS.Tables[0].Rows[i][0].ToString() + "," + DS.Tables[0].Rows[i][1].ToString() + ";");
                    }
                    sb.Append("</table>");
                }
               
            }
            //分公司选项改变时，绑定DropDownLIst数据
            else if ((electric_id != "") && (electric_id != null))
            {
                Return_dataset(BCA.Get_Unit_Info(electric_id, out errMsg));

                DataSet DDDS= BCA.Get_BASE_CRICPARA(BCA.Get_Unit_Info(electric_id, out errMsg).Tables[0].Rows[0]["T_UNITID"].ToString(), out errMsg);
                int count = 0;
                if (DDDS.Tables[0].Rows.Count > 0)
                {
                    sb.Append("<table ><tr>");//border=\"1\"
                    for (int i = 0; i < DDDS.Tables[0].Rows.Count; i++)
                    {
                        count++;
                        if (count % 7 == 0)
                        {
                            sb.Append("<td><input type='checkbox' name='" + DDDS.Tables[0].Rows[i][1].ToString() + "' value='" + DDDS.Tables[0].Rows[i][0].ToString() + "'>" + DDDS.Tables[0].Rows[i][1].ToString() + "<br></td></tr>");
                        }
                        else
                        {
                            sb.Append("<td><input type='checkbox' name='" + DDDS.Tables[0].Rows[i][1].ToString() + "' value='" + DDDS.Tables[0].Rows[i][0].ToString() + "'>" + DDDS.Tables[0].Rows[i][1].ToString() + "</td>");
                        }
                        if ((count % 7 != 0) && (i == DDDS.Tables[0].Rows.Count - 1))
                        {
                            sb.Append("</tr>");
                        }
                        //sb.Append(DS.Tables[0].Rows[i][0].ToString() + "," + DS.Tables[0].Rows[i][1].ToString() + ";");
                    }
                    sb.Append("</table>");
                }
            }
            else if ((crew_id != "") && (crew_id != null))
            {

                DataSet DDDS = BCA.Get_BASE_CRICPARA(crew_id, out errMsg);
                int count = 0;
                if (DDDS.Tables[0].Rows.Count > 0)
                {
                    sb.Append("<table ><tr>");//border=\"1\"
                    for (int i = 0; i < DDDS.Tables[0].Rows.Count; i++)
                    {
                        count++;
                        if (count % 7 == 0)
                        {
                            sb.Append("<td><input type='checkbox' name='" + DDDS.Tables[0].Rows[i][1].ToString() + "' value='" + DDDS.Tables[0].Rows[i][0].ToString() + "'>" + DDDS.Tables[0].Rows[i][1].ToString() + "<br></td></tr>");
                        }
                        else
                        {
                            sb.Append("<td><input type='checkbox' name='" + DDDS.Tables[0].Rows[i][1].ToString() + "' value='" + DDDS.Tables[0].Rows[i][0].ToString() + "'>" + DDDS.Tables[0].Rows[i][1].ToString() + "</td>");
                        }
                        if ((count % 7 != 0) && (i == DDDS.Tables[0].Rows.Count - 1))
                        {
                            sb.Append("</tr>");
                        }
                        //sb.Append(DS.Tables[0].Rows[i][0].ToString() + "," + DS.Tables[0].Rows[i][1].ToString() + ";");
                    }
                    sb.Append("</table>");
                }
            }
            else if ((sec_type_real != "") && (sec_type_real != null))
            {

                DataSet DS = BCA.Get_Electric_Info(sec_type_real, out errMsg);
                DataSet DDS = BCA.Get_Unit_Info(DS.Tables[0].Rows[0]["T_PLANTID"].ToString(), out errMsg);
                DataSet DDDS = BCA.Get_Para_Info(DDS.Tables[0].Rows[0]["T_UNITID"].ToString(), out errMsg);
                Return_dataset(DS);
                Return_dataset(DDS);
                Return_dataset(DDDS);
            }
            else if ((electric_id_real != "") && (electric_id_real != null))
            {
                DataSet DS = BCA.Get_Unit_Info(electric_id_real, out errMsg);
                DataSet DDS = BCA.Get_Para_Info(DS.Tables[0].Rows[0]["T_UNITID"].ToString(), out errMsg);
                Return_dataset(DS);
                Return_dataset(DDS);
            }
            else if ((crew_id_real != "") && (crew_id_real != null))
            {
                DataSet DS = BCA.Get_Para_Info(crew_id_real, out errMsg);
                Return_dataset(DS);
            }
            
            Response.Clear();
            Response.Write(sb.ToString());
            Response.End();  
        }

        private void Return_dataset(DataSet DS)
        {
            //DataSet DS = BCA(id, level_id, para_id);
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                sb.Append("<option value=" + DS.Tables[0].Rows[i][0].ToString() + ">" + DS.Tables[0].Rows[i][1].ToString() + "</option>");
            }
            sb.Append("|");
        }
    }
}