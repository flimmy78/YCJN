using System;
using System.Text;
using System.Web;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json;

namespace DJXT.Tend
{
    public partial class CompareAnalyse : System.Web.UI.Page
    {
        private IList<Hashtable> list = new List<Hashtable>();
        string errMsg = "", rating = "", param = "";
        StringBuilder sb = new StringBuilder();
        BLL.BLLRealQuery BCA = new BLL.BLLRealQuery();
        protected void Page_Load(object sender, EventArgs e)
        {
            rating = Request["rating"]; param=Request["param"];
            if ((rating != "") && (rating != null))
            {
                get_data();
            }
            if ((param != "") && (param != null))
            {
                GET_CHARTID(param);
            }
            if (!IsPostBack)
            {
                Sec_data_bind();
            }
        }

        private void get_data()
        {

            string rating_data = rating;
            // string[] str_data = new string[rating_data.Split(';').Length - 1];
            string errMsg = "",max_data="",min_data="";
            //ArrayList list = new ArrayList();
            string stime = rating_data.Split(';')[1].Split(',')[0], etime = rating_data.Split(';')[1].Split(',')[1];
            string per = ((Convert.ToInt32(rating_data.Split(';')[0]) - 5) * 0.01).ToString() + "|" + ((Convert.ToInt32(rating_data.Split(';')[0]) + 5) * 0.01).ToString();
            string[] para_id = new string[rating_data.Split(';')[2].Split(',').Length - 1];
            for (int i = 0; i < rating_data.Split(';')[2].Split(',').Length - 1; i++)
            {
                para_id[i] = rating_data.Split(';')[2].Split(',')[i];
            }
            string unit_id = rating_data.Split(';')[3];
            BLL.BLLCompareAnalyse BCA = new BLL.BLLCompareAnalyse();
            list = BCA.Get_Required_data(unit_id, para_id, per, stime, etime, out errMsg,out max_data,out min_data);
            string[] str = new string[9]{"#058DC7", "#50B432", "#ED561B", "#DDDF00", "#24CBE5", "#64E572", "#FF9655", "#FFF263", "#6AF9C4"}; 
            ArrayList _list = new ArrayList();
            int num = 0;
            foreach (Hashtable _ht in list)
            {
                ArrayList _data = (ArrayList)_ht["data"];
                string _name = _ht["name"].ToString();
                Hashtable _dv = new Hashtable();
                _dv.Add("lineColor",str[num] );
                _dv.Add("title","{text:'"+_name+"'}");
                _dv.Add("lineWidth", 1);
                _list.Add(_dv);
                num++;
            }


            object obj = new
            {
                max_data=max_data.TrimEnd(','),
                min_data = min_data.TrimEnd(','),
                str_para_id = para_id,
                title = "趋势呈现数据图",
                y_data=_list,
                list = list
            };

            Response.Clear();
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }


        /// <summary>
        /// datagrid数据获取
        /// </summary>
        private void GET_CHARTID(string param)
        {
            string rating_data = param;
            string errMsg = "";
            //ArrayList list = new ArrayList();
            string stime = rating_data.Split(';')[1].Split(',')[0], etime = rating_data.Split(';')[1].Split(',')[1];
            string per = ((Convert.ToInt32(rating_data.Split(';')[0]) - 5) * 0.01).ToString() + "|" + ((Convert.ToInt32(rating_data.Split(';')[0]) +5) * 0.01).ToString();
            string[] para_id = new string[rating_data.Split(';')[2].Split(',').Length - 1];
            for (int i = 0; i < rating_data.Split(';')[2].Split(',').Length - 1; i++)
            {
                para_id[i] = rating_data.Split(';')[2].Split(',')[i];
            }
            string unit_id = rating_data.Split(';')[3];
            BLL.BLLCompareAnalyse BCA = new BLL.BLLCompareAnalyse();
            list = BCA.Get_All_data(unit_id, para_id, per, stime, etime, out errMsg);

            object obj = new
            {
                rows = list
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }


        private void Return_dataset(DataSet DS)
        {
            //DataSet DS = BCA(id, level_id, para_id);
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                sb.Append("<option value=" + DS.Tables[0].Rows[i][0].ToString() + ">" + DS.Tables[0].Rows[i][0].ToString() + "</option>");
            }
            sb.Append("|");
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

            DataSet DDDS = BLQ.Get_BASE_CRICPARA(DDS.Tables[0].Rows[0]["T_UNITID"].ToString(), out errMsg);

            //foreach (DataRow dr in .Tables[0].Rows)
            //{
            //    //ListItem li = new ListItem();
            //    //li.Value = dr["T_PARAID"].ToString();
            //    //li.Text = dr["T_PARADESC"].ToString();
            //    //li.Attributes.Add("alt", dr["T_PARAID"].ToString());
            //    //cbl_data_point.Items.Add(li);
            //}
            StringBuilder sb = new StringBuilder();
            int count = 0;
                if (DDDS.Tables[0].Rows.Count > 0)
                {
                    sb.Append("<table ><tr>");//border=\"1\"
                    for (int i = 0; i < DDDS.Tables[0].Rows.Count; i++)
                    {
                        count++;
                        if (count % 7 == 0)
                        {
                            sb.Append("<td><input type='checkbox' name='" + DDDS.Tables[0].Rows[i][1].ToString() + "' value='" + DDDS.Tables[0].Rows[i][0].ToString() + "'>" + DDDS.Tables[0].Rows[i][1].ToString() + "<br></td></tr>");
                        }
                        else
                        {
                            sb.Append("<td><input type='checkbox' name='" + DDDS.Tables[0].Rows[i][1].ToString() + "' value='" + DDDS.Tables[0].Rows[i][0].ToString() + "'>" + DDDS.Tables[0].Rows[i][1].ToString() + "</td>");
                        }
                        if ((count % 7 != 0) && (i == DDDS.Tables[0].Rows.Count - 1))
                        {
                            sb.Append("</tr>");
                        }
                        //sb.Append(DS.Tables[0].Rows[i][0].ToString() + "," + DS.Tables[0].Rows[i][1].ToString() + ";");
                    }
                    sb.Append("</table>");
                }
                this.div_point_name.InnerHtml=sb.ToString();
        }


    }
}