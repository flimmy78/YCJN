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
    public partial class WarningThreshold : System.Web.UI.Page
    {
        string errMsg = "", rating = "", para = "", para_save = "", para_delete="";
        protected void Page_Load(object sender, EventArgs e)
        {
            rating = Request["rating"]; para = Request["para"];
            para_save = Request["para_save"]; para_delete = Request["para_delete"];
            if ((rating != "") && (rating != null))
            {
                get_data(rating);
            }

            if ((para != "") && (para != null))
            {
                Insert_data(para);
            }
            if ((para_save != "") && (para_save != null))
            {
                Save_data(para_save);
            }
            if ((para_delete != "") && (para_delete != null))
            {
                Delete_data(para_delete);
            }
            
            if (!IsPostBack)
            {
                Sec_data_bind();
            }
        }

        private void Delete_data(string msg)
        {

            BLL.PerformanceAlarm.BLLWarningThreshold BUP = new BLL.PerformanceAlarm.BLLWarningThreshold();
            bool flag = BUP.Delete_data(msg);

            Response.Clear();
            Response.Write(flag);
            Response.End();
        }
        private void Insert_data(string msg) {
            BLL.PerformanceAlarm.BLLWarningThreshold BUP = new BLL.PerformanceAlarm.BLLWarningThreshold();
            BUP.Insert_data(msg);
        }
        private void Save_data(string msg)
        {
            BLL.PerformanceAlarm.BLLWarningThreshold BUP = new BLL.PerformanceAlarm.BLLWarningThreshold();
           bool flag=  BUP.Save_data(msg);

           Response.Clear();
           Response.Write(flag);
           Response.End();
        }


        private void Sec_data_bind()
        {
            BLL.BLLRealQuery BLQ = new BLL.BLLRealQuery();
            DataSet DS = BLQ.Get_Company_Info(out errMsg);
            this.sec_company.DataSource = DS.Tables[0].DefaultView;
            this.sec_company.DataTextField = "T_COMPANYDESC";
            this.sec_company.DataValueField = "T_COMPANYID";
            this.sec_company.DataBind();
            this.sec_company.Items.Insert(0, "-请选择-");
        }

        private void get_data(string unit_id)
        {
            BLL.PerformanceAlarm.BLLWarningThreshold BUP = new BLL.PerformanceAlarm.BLLWarningThreshold();
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
                ht.Add("ID", row["ID_KEY"].ToString());
                ht.Add("考核点描述", row["考核点描述"].ToString());
                ht.Add("考核下限", row["考核下限"].ToString());
                ht.Add("考核上限", row["考核上限"].ToString());
                ht.Add("提示信息", row["提示信息"].ToString());
                ht.Add("机组", row["机组"].ToString());
                ht.Add("故障类型ID", row["故障类型ID"].ToString());
                
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