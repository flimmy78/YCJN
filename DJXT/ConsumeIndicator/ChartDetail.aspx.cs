using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.ConsumeIndicator;
using BLL.ConsumeIndicator;
using SAC.Helper;
using Newtonsoft.Json;
using System.Collections;

namespace DJXT.ConsumeIndicator
{
    public partial class ChartDetail : System.Web.UI.Page
    {
        string errMsg = string.Empty;
        BLLConsumeIndicator bc = new BLLConsumeIndicator();
        //public List<UnitConsumeInfo> List
        //{
        //    set {
        //        List = value;
        //    }
        //    get {
        //        return (List < UnitConsumeInfo >)ViewState["consume"];
        //    }
        //}
        //public static string type = string.Empty;
        //public static string time = string.Empty;
        public string time
        {
            set { time = value; }
            get
            {
                return ViewState["time"].ToString();
                
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {

            string param = Request["param"];
            string type = Request.QueryString["type"];
            if (ViewState["time"] == null)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["time"]))
                {
                    string tmp = Request.QueryString["time"];
                    //移除0，从2013-08转换成2013-8
                    if (tmp.IndexOf("0", 4) != 0)
                    {
                        tmp.Remove(5, 1);
                    }
                    ViewState["time"] = tmp + "-1";
                }
                else
                {
                    ViewState["time"] = string.Empty;
                }
            }
            if (!IsPostBack)
            {
              
                if (!String.IsNullOrEmpty(type))
                {
                    switch (type)
                    {
                        case "1":
                            Unit100.Visible = true;
                            break;
                        case "2":
                            Unit60.Visible = true;

                            break;
                        case "3":
                            Unit30.Visible = true;

                            break;
                        case "4":
                            Unit20.Visible = true;

                            break;
                        case "5":
                            Unit13.Visible = true;

                            break;
                        case "6":
                            Unit100.Visible = true;
                            Unit60.Visible = true;
                            Unit30.Visible = true;
                            Unit20.Visible = true;
                            Unit13.Visible = true;

                            break;


                    }



                }
            }

            if (param != "")
            {
                if (param == "seachList")
                {
                    GetInfo(type, time);
                }
            }

        }

        //根据不同类型的机组获取信息
        public void GetInfo(string type, string time)
        {

            List<UnitConsumeInfo> List = new List<UnitConsumeInfo>();
            List = bc.GetUnitConsumeList(time, out errMsg);
            List<UnitConsumeInfo> tmp = new List<UnitConsumeInfo>();

            switch (type)
            {

                case "1":
                    tmp = List.Where(info => info.T_TYPE == "1000MW").ToList();
                    break;
                case "2":
                    tmp = List.Where(info => info.T_TYPE == "600MW").ToList();
                    break;
                case "3":
                    tmp = List.Where(info => info.T_TYPE == "300MW").ToList();
                    break;
                case "4":
                    tmp = List.Where(info => info.T_TYPE == "200MW").ToList();
                    break;
                case "5":
                    tmp = List.Where(info => info.T_TYPE == "135MW").ToList();
                    break;
                case "6":
                    tmp = List;
                    break;
            }

            IList<Hashtable> list = new List<Hashtable>();
            int count = tmp.Count;
            foreach (var item in tmp)
            {
                Hashtable ht = new Hashtable();
                ht.Add("T_COUNT", item.T_COUNT);
                ht.Add("T_CYDL", item.T_CYDL);
                ht.Add("T_DBMH", item.T_DBMH);
                ht.Add("T_DWNAME", item.T_DWNAME);
                ht.Add("T_GDL", item.T_GDL);
                ht.Add("T_GDMH", item.T_GDMH);
                ht.Add("T_JTPJB", item.T_JTPJB);
                ht.Add("T_OF", item.T_OF);
                ht.Add("T_PLANTNAME", item.T_PLANTNAME);
                ht.Add("T_RDB", item.T_RDB);
                ht.Add("T_TIME", item.T_TIME);
                ht.Add("T_TYPE", item.T_TYPE);
                ht.Add("T_UNITCODE", item.T_UNITCODE);
                ht.Add("T_USEHOUR", item.T_USEHOUR);

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

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
    }
}