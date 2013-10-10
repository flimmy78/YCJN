using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SAC.JScript;
using SAC.Helper;
using Entity.ConsumeIndicator;
using Entity.ProPara;
using System.Collections;
using Newtonsoft.Json;

namespace DJXT.PerformanceAlarm
{
    public partial class Unit_Performance_Evaluation : System.Web.UI.Page
    {
        string errMsg = "", rating = "", para_edit="";
        protected void Page_Load(object sender, EventArgs e)
        {
            rating = Request["rating"]; para_edit = Request["para_edit"];
            if ((rating != "") && (rating != null))
            {
                get_data(rating);
            }
            if ((para_edit != "") && (para_edit != null))
            {
                Edit_data(para_edit);
            }
            if (!IsPostBack)
            {
                Sec_data_bind();
            }
        }

        private void Edit_data(string msg)
        {
            BLL.PerformanceAlarm.BLLUnitPerformance BUP = new BLL.PerformanceAlarm.BLLUnitPerformance();
           bool flag =  BUP.Edit_data(msg);
            
        }


        private void Sec_data_bind()
        {
            BLL.BLLRealQuery BLQ = new BLL.BLLRealQuery();
            DataSet DS = BLQ.Get_Company_Info(out errMsg);
            DataSet DSS = BLQ.Get_Electric_Info(DS.Tables[0].Rows[0]["T_COMPANYID"].ToString(), out errMsg);
            DataSet DDS = BLQ.Get_Unit_Info(DSS.Tables[0].Rows[0]["T_PLANTID"].ToString(), out errMsg);
            this.sec_company.DataSource = DS.Tables[0].DefaultView;
            this.sec_company.DataTextField = "T_COMPANYDESC";
            this.sec_company.DataValueField = "T_COMPANYID";
            this.sec_company.DataBind();
            this.sec_company.Items.Insert(0, "-请选择-");

            BLL.PerformanceAlarm.BLLUnitPerformance BUP = new BLL.PerformanceAlarm.BLLUnitPerformance();
            DataSet DS_FC= BUP.GetFAULTCATEGORY();
            DataSet DS_FP = BUP.GetFAULTPROPERTY();
            DataSet DS_FT = BUP.GetFAULTPROFESSIONAL();
            DataSet DS_FA = BUP.GetFAULTREASON();
            this.cbl_yjlb.DataSource = DS_FC.Tables[0].DefaultView;
            this.cbl_yjlb.DataTextField = "T_CATEGORYDESC";
            this.cbl_yjlb.DataValueField = "T_CATEGORYID";
            this.cbl_yjlb.DataBind();
            this.cbl_yjlb.Items[0].Selected = true;

            this.cbl_yjxz.DataSource = DS_FP.Tables[0].DefaultView;
            this.cbl_yjxz.DataTextField = "T_PROPERTYDESC";
            this.cbl_yjxz.DataValueField = "T_PROPERTYID";
            this.cbl_yjxz.DataBind();
            this.cbl_yjxz.Items[0].Selected = true;

            this.cbl_yjzyfl.DataSource = DS_FT.Tables[0].DefaultView;
            this.cbl_yjzyfl.DataTextField = "T_PROFESSIONALDESC";
            this.cbl_yjzyfl.DataValueField = "T_PROFESSIONALID";
            this.cbl_yjzyfl.DataBind();
            this.cbl_yjzyfl.Items[0].Selected = true;

            this.cbl_yjyyfl.DataSource = DS_FA.Tables[0].DefaultView;
            this.cbl_yjyyfl.DataTextField = "T_REASONDESC";
            this.cbl_yjyyfl.DataValueField = "T_REASONID";
            this.cbl_yjyyfl.DataBind();
            this.cbl_yjyyfl.Items[0].Selected = true;
        }

        private void get_data(string unit_id)
        {
            BLL.PerformanceAlarm.BLLUnitPerformance BUP = new BLL.PerformanceAlarm.BLLUnitPerformance();
            DataSet DS = BUP.Get_GRID_DATA(unit_id);

            //this.dl_data.DataBind();
            int page = Request.Form["page"] != "" ? Convert.ToInt32(Request.Form["page"]) : 0;
            int size = Request.Form["rows"] != "" ? Convert.ToInt32(Request.Form["rows"]) : 0;


            int count = 0;
            if (DS.Tables[0].Rows.Count > 0)
            {
                count = DS.Tables[0].Rows.Count;
            }


            IList<Hashtable> list = new List<Hashtable>();

            foreach (DataRow row in DS.Tables[0].Rows)
            {
                Hashtable ht = new Hashtable();

                ht.Add("ID_KTEY", row["ID_KTEY"].ToString());
                ht.Add("T_COMPANYDESC", row["T_COMPANYDESC"].ToString());
                ht.Add("T_PLANTDESC", row["T_PLANTDESC"].ToString());
                ht.Add("T_UNITDESC", row["T_UNITDESC"].ToString());
                ht.Add("T_DESC", row["T_DESC"].ToString());
                ht.Add("开始时间", row["开始时间"].ToString());
                ht.Add("结束时间", row["结束时间"].ToString());
                ht.Add("T_CATEGORYDESC", row["T_CATEGORYDESC"].ToString());
                ht.Add("T_PROPERTYDESC", row["T_PROPERTYDESC"].ToString());
                ht.Add("T_PROFESSIONALDESC", row["T_PROFESSIONALDESC"].ToString());
                ht.Add("T_REASONDESC", row["T_REASONDESC"].ToString());
                ht.Add("影响电量", row["影响电量"].ToString());
                ht.Add("T_CAPABILITYLEVEL", row["T_CAPABILITYLEVEL"].ToString());
                ht.Add("事件描述", row["事件描述"].ToString());
                ht.Add("原因分析", row["原因分析"].ToString());
                ht.Add("处理建议", row["处理建议"].ToString());
                
                
                list.Add(ht);
            }

            object obj = new
            {
                total = count,
                rows = list
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
    }
}