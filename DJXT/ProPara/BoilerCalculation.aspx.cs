using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace DJXT.ProPara
{
    public partial class BoilerCalculation : System.Web.UI.Page
    {
        string errMsg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Sec_data_bind();
            }
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
        }
    }
}