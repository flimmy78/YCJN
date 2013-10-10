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
    public partial class GetProductionProPara : System.Web.UI.Page
    {
        public string errMsg = "", sec_type_real = "", electric_id_real = "", PrpPara = "", PrpPara_insert = "", pre_query = "";
        StringBuilder sb = new StringBuilder();
        BLL.BLLRealQuery BCA = new BLL.BLLRealQuery();
        protected void Page_Load(object sender, EventArgs e)
        {
            sec_type_real = Request["sec_type_real"];
            electric_id_real = Request["electric_id_real"];
            PrpPara = Request["PrpPara"]; PrpPara_insert = Request["PrpPara_insert"];
            pre_query = Request["pre_query"];
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
            else if ((PrpPara != "") && (PrpPara != null))
            {
                BLL.BLLProPara BPP = new BLL.BLLProPara();
                DataSet DS = BPP.Get_Required_data(PrpPara.Split('|')[1], PrpPara.Split('|')[0]);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[0].Columns.Count; i++)
                    {
                        sb.Append(DS.Tables[0].Rows[0][i].ToString() + ",");

                    }
                    sb.ToString().TrimEnd(',');
                }
            }
            else if ((PrpPara_insert != "") && (PrpPara_insert != null))
            {
                BLL.BLLProPara BPP = new BLL.BLLProPara();
                int num = 0;
                if (PrpPara_insert.Split('|')[0] == "1")
                {
                    num = BPP.InsertProductionElementData(PrpPara_insert.Split('|')[1] + ",'" + DateTime.Now + "'");
                }
                else
                {
                    num = BPP.InsertProductionIndustryData(PrpPara_insert.Split('|')[1] + ",'" + DateTime.Now + "'");
                }
                sb.Append(num);
            }
            else if ((pre_query != "") && (pre_query != null))
            {
                BLL.BLLProPara BPP = new BLL.BLLProPara();
                DataSet DS = BPP.GetProductionPreData(pre_query.Split(';')[0].Split(',')[0], pre_query.Split(';')[0].Split(',')[1], pre_query.Split(';')[1]);
                return_table(DS);
            }
            Response.Clear();
            Response.Write(sb.ToString());
            Response.End();
        }

        private void Return_dataset(DataSet DS)
        {
            //DataSet DS = BCA(id, level_id, para_id);
            sb.Append("<option value=-请选择->-请选择-</option>");
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                sb.Append("<option value=" + DS.Tables[0].Rows[i][0].ToString() + ">" + DS.Tables[0].Rows[i][1].ToString() + "</option>");
            }
            sb.Append("|");
        }


        private void return_table(DataSet DS)
        {

            sb.Append("<table width=\"100%\" border=\"1\" align=\"center\" class=\"bg\" valign=\"top\" bordercolorlight=\"#999999\" bordercolordark=\"#FFFFFF\" cellspacing=\"0\" cellpadding=\"1\" id=\"CX\">");

            sb.Append(" <tr style='width: 100%;' bgcolor='#CFE6FC'>");

            sb.Append("<td > <div align='center'> 序号 </td>");

            sb.Append("<td > <div align='center'> 机组简称 </td>");

            sb.Append("<td ><div align='center'> 日期 </td>");

            sb.Append("<td > <div align='center'> 全水分Mt(%) </td>");

            sb.Append("<td > <div align='center'> 空干基水分Mad(%) </td>");

            sb.Append("<td > <div align='center'> 空干基灰分Aad(%) </td>");

            sb.Append("<td > <div align='center'> 空干机挥发分Vad（%） </td>");

            sb.Append("<td ><div align='center'> 空干基低位发热量Qgr,ar(MJ/kg) </td>");

            sb.Append("<td > <div align='center'> 空干基硫分Stad(%) </td>");

            sb.Append(" </tr>");
            //style='width: 10%;'
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                sb.Append("<tr>");

                sb.AppendFormat("<td align='center'>{0}</td>", i + 1);

                sb.AppendFormat("<td align='center'>{0}</td>", DS.Tables[0].Rows[0]["T_UNITID"].ToString());

                sb.AppendFormat("<td align='center'>{0}</td>", DS.Tables[0].Rows[0]["T_TIME"].ToString());

                sb.AppendFormat("<td align='center'>{0}</td>", DS.Tables[0].Rows[0]["D_M_AR_PROX"].ToString());

                sb.AppendFormat("<td align='center'>{0}</td>", DS.Tables[0].Rows[0]["D_M_ad"].ToString());

                sb.AppendFormat("<td align='center'>{0}</td>", DS.Tables[0].Rows[0]["D_A_ad"].ToString());

                sb.AppendFormat("<td align='center'>{0}</td>", DS.Tables[0].Rows[0]["D_V_DAF"].ToString());

                sb.AppendFormat("<td align='center'>{0}</td>", DS.Tables[0].Rows[0]["D_Qnet_ar_PROX"].ToString());

                sb.AppendFormat("<td align='center'>{0}</td>", DS.Tables[0].Rows[0]["D_St_ad"].ToString());

                sb.Append("</tr>");
            }
            sb.Append("</table>");
        }
    }
}
