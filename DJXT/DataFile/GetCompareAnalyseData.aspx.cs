using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Collections;
using System.Web.UI.WebControls;
using System.Data;
using Newtonsoft.Json;

/////创建者：刘海杰
/////创建日期 2013-07-05
namespace DJXT.Datafile
{
    public partial class GetCompareAnalyseData : System.Web.UI.Page
    {
        private IList<Hashtable> list = new List<Hashtable>();
        protected void Page_Load(object sender, EventArgs e)
        {
           // string rating_data = Request["rating_data"];
           //// string[] str_data = new string[rating_data.Split(';').Length - 1];
           // string errMsg = "";
           // //ArrayList list = new ArrayList();
           // string stime = rating_data.Split(';')[1].Split(',')[0], etime = rating_data.Split(';')[1].Split(',')[1];
           // string per = (Convert.ToInt32(rating_data.Split(';')[0]) * 0.01).ToString();
           // string[] para_id = new string[rating_data.Split(';')[2].Split(',').Length-1];
           // for (int i = 0; i < rating_data.Split(';')[2].Split(',').Length-1; i++)
           // {
           //     para_id[i] = rating_data.Split(';')[2].Split(',')[i];
           // }
           // string unit_id = rating_data.Split(';')[3];
           // BLL.BLLCompareAnalyse BCA = new BLL.BLLCompareAnalyse();
           // list= BCA.Get_Required_data(unit_id, para_id, per, stime, etime, out errMsg,out max_data,out min_data);

           // object obj = new
           // {
           //     title = "趋势呈现数据图",
           //     list = list
           // };
            
           // Response.Clear();
           // string result = JsonConvert.SerializeObject(obj);
           // Response.Write(result);
           // //Response.Write(str_append.TrimEnd('|'));
           // Response.End();
        }
    }
}