using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Newtonsoft.Json;
using System.Text;

namespace DJXT.Trend
{

    public partial class RealQuery : System.Web.UI.Page
    {
        private IList<Hashtable> list = new List<Hashtable>();
        static IList<Hashtable> _list = new List<Hashtable>();
        static IList<Hashtable> name_list = new List<Hashtable>();
        string errMsg = "", real_data = "", param = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            real_data = Request["real_data"]; param = Request["param"];
            if ((real_data != "") && (real_data != null))
            {
                list = new List<Hashtable>();
                _list = new List<Hashtable>();
                name_list = new List<Hashtable>();
                get_data();
            }
            if ((param != "") && (param != null))
            {
                if (param == "search")
                {
                    object obj = new
                    {
                        columns = CreateDataGridColumnModel(name_list),
                        rows = _list
                    };
                    Response.Clear();
                    string result = JsonConvert.SerializeObject(obj);
                    Response.Write(result);
                    Response.End();
                }
            }
            if (!IsPostBack)
            {
                Sec_data_bind();
            }
        }

        private void Get_Real()
        {
            string[] str_data = new string[real_data.Split(';').Length - 1];
            string stime = null;
            BLL.BLLRealQuery BRQ = new BLL.BLLRealQuery();
            for (int i = 0; i < real_data.Split(';').Length; i++)
            {
                if (i != real_data.Split(';').Length - 1)
                {
                    str_data[i] += real_data.Split(';')[i].Split(',')[0] + "," + real_data.Split(';')[i].Split(',')[1];
                }
                else
                {
                    stime = real_data.Split(';')[i].Split(',')[0];
                }
            }
            list = BRQ.GetChartData_Real(str_data, stime);
            object obj = new
            {
                list = list
            };
            Response.Clear();
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }


        private void get_data()
        {
            string[] str_data = new string[real_data.Split(';').Length - 1];
            string stime = null, etime = null;
            string max_data ="", min_data = "";
            BLL.BLLRealQuery BRQ = new BLL.BLLRealQuery();
            for (int i = 0; i < real_data.Split(';').Length; i++)
            {
                if (i != real_data.Split(';').Length - 1)
                {
                    str_data[i] += real_data.Split(';')[i].Split(',')[0] + "," + real_data.Split(';')[i].Split(',')[1];
                }
                else
                {
                    stime = real_data.Split(';')[i].Split(',')[0];
                    etime = real_data.Split(';')[i].Split(',')[1];
                }
            }
            list = BRQ.GetChartData(str_data, stime, etime, out max_data, out min_data);
            Hashtable data_dv = new Hashtable();
            //data_dv.Add("时间", "时间");
            ArrayList list1 = new ArrayList();
            string[] str = new string[9] { "#058DC7", "#50B432", "#ED561B", "#DDDF00", "#24CBE5", "#64E572", "#FF9655", "#FFF263", "#6AF9C4" };
            int num1 = 0;
            for (int i = 0; i < 600; i++)
            {
                 //{'field':'name','title':'Name','width':100,'align':'center'}, 
                int num = 0;
                Hashtable _dv = new Hashtable();
                foreach (Hashtable _ht in list)
                {
                    
                    ArrayList _data = (ArrayList)_ht["data"];
                    if (i > _data.Count-1)
                    {
                        break;
                    }
                    ArrayList _listdata = new ArrayList();
                    _listdata = (ArrayList)_data[i];
                    
                    string _name = _ht["name"].ToString();
                    if (i==0)
                    {
                        Hashtable _dv1 = new Hashtable();
                        _dv1.Add("lineColor", str[num1]);
                        _dv1.Add("title", "{text:'" + _name + "'}");
                        //_dv.Add("opposite", true);//Y轴右端显示
                        _dv1.Add("lineWidth", 1);
                        list1.Add(_dv1);
                        num1++;
                    }
                //string[] _val = (string[])_data["data"];
                ////int[] _time = (int[])_val[0];
                    if (i==0)
                    {
                        data_dv.Add(_name, _name);
                    }
                    //_dv.Add("name", _name);

                    _dv.Add(_name, Math.Round(Convert.ToDouble(_listdata[1]), 2).ToString());
                    if (num == 0)
                    {
                        _dv.Add("时间", Convert.ToDouble(_listdata[0]).ToString());
                       // ConvertIntDatetime(Convert.ToDouble(_listdata[0])).ToString()
                    }
                    num++;
                }

                _list.Add(_dv);
            }
            name_list.Add(data_dv);

            

            object obj = new
            {
                max_data = max_data.TrimEnd(','),
                min_data = min_data.TrimEnd(','),
                title = "趋势呈现数据图",
                y_data=list1,
                list = list
            };
            Response.Clear();
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }

        /// <summary>  
        /// 从dataTable创建 jquery easyui datagrid格式的columns参数  
        /// </summary>  
        /// <param name="dt"></param>  
        /// <returns></returns>  
        private string CreateDataGridColumnModel(IList<Hashtable> iList)
        {
            int width = 0;
            StringBuilder columns = new StringBuilder("[[");

            if (iList.Count > 0)
            {
                columns.Append("{field:'时间',title:'时间',align:'center',width:80},");
                Hashtable ht = iList[0];
                ArrayList list = new ArrayList(ht.Keys);
                //list.Sort();
                foreach (string skey in list)
                {
                    if (skey.ToString() == "ID_KEY") { width = 100; }
                    else { width = skey.Length * 20; }
                    columns.AppendFormat("{{field:'{0}',title:'{1}',align:'center',width:{2}}},", skey, skey, 80);
                }
            }
            if (iList.Count > 0)
            {
                columns.Remove(columns.Length - 1, 1);//去除多余的','号  
            }
            columns.Append("]]");
            return columns.ToString();
        }  



        public DateTime ConvertIntDatetime(double utc)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            startTime = startTime.AddSeconds(utc);
            //startTime = startTime.AddHours(8);//转化为北京时间(北京时间=UTC时间+8小时 )
            return startTime;
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
            this.sec_electric.DataSource = DSS.Tables[0].DefaultView;
            this.sec_electric.DataTextField = "T_PLANTDESC";
            this.sec_electric.DataValueField = "T_PLANTID";
            this.sec_electric.DataBind();
            this.sec_crew.DataSource = DDS.Tables[0].DefaultView;
            this.sec_crew.DataTextField = "T_UNITDESC";
            this.sec_crew.DataValueField = "T_UNITID";
            this.sec_crew.DataBind();
            this.lb_point_name.DataSource = BLQ.Get_Para_Info(DDS.Tables[0].Rows[0]["T_UNITID"].ToString(), out errMsg);
            this.lb_point_name.DataTextField = "T_PARADESC";
            this.lb_point_name.DataValueField = "T_PARAID";
            this.lb_point_name.DataBind();
        }
    }
}