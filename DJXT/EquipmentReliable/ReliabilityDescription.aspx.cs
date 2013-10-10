using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using System.Text;

namespace DJXT.EquipmentReliable
{
    public partial class ReliabilityDescription : System.Web.UI.Page
    {
        BLLEquipmentReliable bl = new BLLEquipmentReliable();
        string errMsg = string.Empty;
        string unitId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitialControls();
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
                StringBuilder sb=new StringBuilder ();
                sb.Append(" 事件描述：");
                sb.Append(dt.Rows[0]["T_EVENTDESC"].ToString());
                sb.Append( "\n原因分析：");
                sb.Append(dt.Rows[0]["T_REASONANALYSE"].ToString());
                sb.Append("\n处理情况：");
                sb.Append(dt.Rows[0]["T_DEALCONDITION"].ToString());
                txtDesc.Text = sb.ToString();
            }
        }
    }
}