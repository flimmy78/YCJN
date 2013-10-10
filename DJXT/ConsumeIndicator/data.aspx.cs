using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Newtonsoft.Json;
using Entity.ConsumeIndicator;
using BLL.ConsumeIndicator;
using SAC.Helper;

namespace DJXT.ConsumeIndicator
{
   
    public partial class data : System.Web.UI.Page
    {
        public static int bz = 0;
        string errMsg = string.Empty;
        BLLConsumeIndicator bc = new BLLConsumeIndicator();
        protected void Page_Load(object sender, EventArgs e)
        {
            string param = Request["param"];

            if (!IsPostBack)
            {
                txtTimeBegin.Value = DateTime.Now.Year + "-" + DateTime.Now.Month;
            }
            if (param != "")
            {
                if (param == "seachList")
                {
                    Get();
                }
            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            //Bind();
        }

        public void Get()
        {
            string beginTime = Request["beginTime"].ToString();
            List<DataInfo> list = new List<DataInfo>();
            list = bc.GetInfo(beginTime,out errMsg);
            list = list.Distinct(new EqualCompare<DataInfo>((x, y) => (x != null && y != null) && (x.T_COMPANY == y.T_COMPANY)&&(x.T_DATATYPE==y.T_DATATYPE))).ToList();

            List<DataInfo> tmp = new List<DataInfo>();
            
            tmp= list.Where(info => info.T_COMPANY.Trim()=="华电").ToList();
            tmp.AddRange(list.Where(info => info.T_COMPANY.Trim() == "中电投").ToList());
            tmp.AddRange(list.Where(info => info.T_COMPANY.Trim() == "大唐").ToList());
            tmp.AddRange(list.Where(info => info.T_COMPANY.Trim() == "华能").ToList());
            tmp.AddRange(list.Where(info => info.T_COMPANY.Trim() == "国电").ToList());

            object obj = new
            {
                total = 25,
                rows = tmp
            };

            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        
        }

    }
}