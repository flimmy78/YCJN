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
using BLL;
using System.Text;

namespace DJXT.Device
{
    public partial class msgInfo : System.Web.UI.Page
    {
        DataTable dt = null;
        BLLManDevice bmd = new BLLManDevice();
        System.Text.StringBuilder sb = new StringBuilder(); 

        protected void Page_Load(object sender, EventArgs e)
        {
            string s = Request.QueryString["id"];
            
            dt=bmd.RetTabItemsByIDKEY(s);

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("<tr>");

                    if (dt.Rows[i]["T_ITEMPOSITION"] != null && dt.Rows[i]["T_ITEMPOSITION"].ToString() != "")
                        sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", dt.Rows[i]["T_ITEMPOSITION"].ToString());
                    else
                        sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", "&nbsp;");

                    if (dt.Rows[i]["T_ITEMDESC"] != null && dt.Rows[i]["T_ITEMDESC"].ToString() != "")
                        sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", dt.Rows[i]["T_ITEMDESC"].ToString());
                    else
                        sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", "&nbsp;");

                    if (dt.Rows[i]["T_CONTENT"] != null && dt.Rows[i]["T_CONTENT"].ToString() != "")
                        sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", dt.Rows[i]["T_CONTENT"].ToString());
                    else
                        sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", "&nbsp;");

                    if (dt.Rows[i]["T_TYPE"] != null && dt.Rows[i]["T_TYPE"].ToString() != "")
                    {
                        if(dt.Rows[i]["T_TYPE"].ToString() =="0")
                            sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", "点检");
                        else
                            sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", "巡检");
                    }else
                        sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", "&nbsp;");
                    
                    if (dt.Rows[i]["I_STATUS"] != null && dt.Rows[i]["I_STATUS"].ToString() != "")
                    {
                        DataRow dr = bmd.RetDrByStatusID(dt.Rows[i]["I_STATUS"].ToString());

                        if (dr != null) 
                            sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", dr["T_STATUSDESC"].ToString());
                            else
                              sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", "&nbsp;");

                    }else
                        sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", "&nbsp;");

                    if (dt.Rows[i]["T_UNIT"] != null && dt.Rows[i]["T_UNIT"].ToString() != "")
                        sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", dt.Rows[i]["T_UNIT"].ToString());
                    else
                        sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", "&nbsp;");

                    if (dt.Rows[i]["F_UPPER"] != null && dt.Rows[i]["F_UPPER"].ToString() != "")
                        sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", dt.Rows[i]["F_UPPER"].ToString());
                    else
                        sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", "&nbsp;");

                    if (dt.Rows[i]["F_LOWER"] != null && dt.Rows[i]["F_LOWER"].ToString() != "")
                        sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", dt.Rows[i]["F_LOWER"].ToString());
                    else
                        sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", "&nbsp;");

                     if (dt.Rows[i]["I_SPECTRUM"] != null && dt.Rows[i]["I_SPECTRUM"].ToString() != "")
                     {
                         if(dt.Rows[i]["I_SPECTRUM"].ToString()=="0")
                        sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", "否");
                         else
                             sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>","是");
                     }
                    else
                        sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", "&nbsp;");
                    
                    if (dt.Rows[i]["T_PERIODVALUE"] != null && dt.Rows[i]["T_PERIODVALUE"].ToString() != "")
                        sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", dt.Rows[i]["T_PERIODVALUE"].ToString());
                    else
                        sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", "&nbsp;");
                    
                    //周期类型
                    if (dt.Rows[i]["T_PERIODTYPE"] != null && dt.Rows[i]["T_PERIODTYPE"].ToString() != "")

                        if(dt.Rows[i]["T_PERIODTYPE"].ToString()=="1")
                            sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", "日");
                        else if(dt.Rows[i]["T_PERIODTYPE"].ToString()=="2")
                            sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", "周");
                        else if (dt.Rows[i]["T_PERIODTYPE"].ToString() == "3")
                            sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", "月");
                        else if (dt.Rows[i]["T_PERIODTYPE"].ToString() == "4")
                            sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", "年");
                    else
                        sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", "&nbsp;");

                     if (dt.Rows[i]["T_STARTTIME"] != null && dt.Rows[i]["T_STARTTIME"].ToString() != "")
                        sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>",DateTime.Parse( dt.Rows[i]["T_STARTTIME"].ToString()).ToShortDateString());
                    else
                        sb.AppendFormat("<td align=\"center\" style=\"width: 8%;\">{0}</td>", "&nbsp;");

           //          <td align="center"  style="width: 8%;">点检项描述</td>
           //<td align="center"  style="width: 8%;">点检项检查内容</td>
           //<td align="center"  style="width: 8%;">点检类型</td>
           //<td align="center"  style="width: 8%;">设备状态</td>
           //<td align="center"  style="width: 8%;">单位</td>
           //<td align="center"  style="width: 8%;">测量上限</td>
           //<td align="center"  style="width: 8%;">测量下限</td>
           //<td align="center"  style="width: 8%;">是否频谱</td> 
           //<td align="center"  style="width: 8%;">周期数值</td>
           //<td align="center"  style="width: 8%;">周期类型</td>
        
           //<td align="center"  style="width: 8%;">开始时间</td>

                    sb.Append("</tr>");
                }

                
            }
            else
            { sb.Append("没有查到相关记录！"); }

            this.show.InnerHtml = sb.ToString();
        }   
    }
}
