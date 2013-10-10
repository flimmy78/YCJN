using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Collections;
using Newtonsoft.Json;

namespace DJXT.StatisticalComparison
{
    public partial class HorizontalComparison : System.Web.UI.Page
    {
        string errMsg = "", rating = "";
        BLL.StatisticalComparison.BLLHorizontalComparison BHC = new BLL.StatisticalComparison.BLLHorizontalComparison();
        protected void Page_Load(object sender, EventArgs e)
        {
            rating = Request["rating"]; 
            if ((rating != "") && (rating != null))
            {
                get_data(rating);
            }

            if (!IsPostBack)
            {
                bingdata();

            }
        }


        private void get_data(string id)
        {
             IList<Hashtable> list = new List<Hashtable>();
            list =BHC.GetChartData(id);
            ArrayList _listdata = new ArrayList();
            foreach (Hashtable _ht in list)
            {
                _listdata.Add(_ht["name"].ToString());
            }

            object obj = new
            {
                title = "趋势呈现数据图",
                x_data = _listdata,
                list = list
            };

            Response.Clear();
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            //Response.Write(str_append.TrimEnd('|'));
            Response.End();
        }
        private void bingdata()
        {
            
            
            this.sec_Capacity.DataSource = BHC.GetCAPABILITYLEVEL().Tables[0].DefaultView;
            this.sec_Capacity.DataTextField = "T_CAPABILITYLEVEL";
            this.sec_Capacity.DataValueField = "T_CAPABILITYLEVEL";
            this.sec_Capacity.DataBind();
            this.sec_Capacity.Items.Insert(0, "--请选择--");

            this.sec_UnitType.DataSource = BHC.GetPLANTTYPE().Tables[0].DefaultView;
            this.sec_UnitType.DataTextField = "T_PLANTTYPE";
            this.sec_UnitType.DataValueField = "T_PLANTTYPE";
            this.sec_UnitType.DataBind();
            this.sec_UnitType.Items.Insert(0, "--请选择--");


            this.sec_Boiler.DataSource = BHC.GetBOILERDESC().Tables[0].DefaultView;
            this.sec_Boiler.DataTextField = "T_BOILERDESC";
            this.sec_Boiler.DataValueField = "T_BOILERID";
            this.sec_Boiler.DataBind();
            this.sec_Boiler.Items.Insert(0, "--请选择--");

            this.sec_Steam.DataSource = BHC.GetSTEAMDESC().Tables[0].DefaultView;
            this.sec_Steam.DataTextField = "T_STEAMDESC";
            this.sec_Steam.DataValueField = "T_STEAMID";
            this.sec_Steam.DataBind();
            this.sec_Steam.Items.Insert(0, "--请选择--");
                
        }
    }
}