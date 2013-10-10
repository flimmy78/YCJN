using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using SAC.JScript;
namespace YJJX.EquipmentReliable
{
    public partial class UnplannedOutageEdit : System.Web.UI.Page
    {
        BLLEquipmentReliable bl = new BLLEquipmentReliable();
        string errMsg = string.Empty;
        //string unitId = string.Empty;
        public string categoryId = string.Empty;
        public string propertyId = string.Empty;
        public string professionalId = string.Empty;
        public string reasonId = string.Empty;

        public string unitId
        {
            set {

                ViewState["unitId"] = value;
            }
            get{
                return ViewState["unitId"].ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitialControls();
                BindCategory();
                BindProperty();
                BindProfessional();
                BindReason();
            }
        }

        public void BindCategory()
        {
            DataTable dt  = bl.GetFaultCategory(out errMsg);
            if (dt != null)
            {
                rptCategory.DataSource = dt;
                rptCategory.DataBind();
            }
        }

        public void BindProperty()
        {
            DataTable dt = bl.GetFaultProperty(out errMsg);
            if (dt != null)
            {
                rptProperty.DataSource = dt;
                rptProperty.DataBind();
            }
        }

        public void BindProfessional()
        {
            DataTable dt = bl.GetFaultProfessional(out errMsg);
            if (dt != null)
            {
                rptProfessional.DataSource = dt;
                rptProfessional.DataBind();
            }
        }

        public void BindReason()
        {
            DataTable dt = bl.GetFaultReason(out errMsg);
            if (dt != null)
            {
                rptReason.DataSource = dt;
                rptReason.DataBind();
            }
        }

        /// <summary>
        /// 初始化页面控件。
        /// </summary>
        public void InitialControls()
        {
            unitId = Request.QueryString["Id"].ToString();
            DataTable dt = bl.GetUnitById(unitId, out errMsg);
            if (dt != null)
            {
                txtPlant.Text = String.IsNullOrEmpty(dt.Rows[0]["T_PLANTDESC"].ToString()) ? string.Empty : dt.Rows[0]["T_PLANTDESC"].ToString();
                categoryId = String.IsNullOrEmpty(dt.Rows[0]["T_FCATEGORYID"].ToString()) ? string.Empty : dt.Rows[0]["T_FCATEGORYID"].ToString();
                propertyId = String.IsNullOrEmpty(dt.Rows[0]["T_FPROPERTYID"].ToString()) ? string.Empty : dt.Rows[0]["T_FPROPERTYID"].ToString();
                professionalId = String.IsNullOrEmpty(dt.Rows[0]["T_FPROFEESIOID"].ToString()) ? string.Empty : dt.Rows[0]["T_FPROFEESIOID"].ToString();
                reasonId = String.IsNullOrEmpty(dt.Rows[0]["T_FREASONID"].ToString()) ? string.Empty : dt.Rows[0]["T_FREASONID"].ToString();
            


            
            
            }
        }

        /// <summary>
        /// 提交保存。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string professional = hidProfessional.Value;
            string reason = hidReason.Value;
            string eventDesc = txtEventDesc.Text.Trim();
            string reasonAnalyse = txtReason.Text.Trim();
            string dealCondition = txtDealCondition.Text.Trim();

            if (bl.UpdateUnit(unitId, professional, reason, eventDesc, reasonAnalyse, dealCondition, out errMsg))
            {
                JScript.Alert("更新成功。");
            }
            else
            {
                JScript.Alert("更新失败。");
            }
        }

        /// <summary>
        /// 单选按钮是否被选中。
        /// </summary>
        /// <param name="i">代表哪种故障</param>
        /// <param name="Id">前台的故障Id</param>
        /// <returns></returns>
        public string select(int i,string nId)
        {
            switch (i)
            {
                case 1: if (nId == categoryId)
                    {
                    return "checked='checked'";
                    }; break;
                case 2: if (nId == propertyId)
                    {
                        return "checked='checked'";
                    }; break;
                case 3: if (nId == professionalId)
                    {
                        return "checked='checked'";
                    }; break;
                case 4: if (nId == reasonId)
                    {
                        return "checked='checked'";
                    }; break;
            }
            return string.Empty;
        }
    }
}