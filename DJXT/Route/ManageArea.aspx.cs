using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BLL;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using SAC.DB2;
using System.Collections.Generic;


namespace DJXT.Device
{
    public partial class ManageArea : System.Web.UI.Page
    {
        object obj = null;

        string sql = "";
        string errMsg = "";
        string resultMenu = "";
        public string strInfo = "";

        DataTable dt = null;

        BLLManDevice bmd = new BLLManDevice();
        BLLManArea bmr = new BLLManArea();
        StringBuilder sb = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            string param = Request["param"];

            if (param == "query")
            { QueryData(); }
            else if (param == "SaveArea")
            {
                string areaName = HttpUtility.UrlDecode(Request.Form["name"]);
                string areaCd = Request.Form["cd"];

                SaveArea(areaName, areaCd);

            }
            else if (param == "EditArea")
            {
                string idkey = Request.Form["IDKEY"];
                string areaName = HttpUtility.UrlDecode(Request.Form["name"]);
                string areaCd = Request.Form["cd"];

                EditArea(idkey, areaName, areaCd);
            }
            else if (param == "DelArea")
            {
                string idkey = Request.Form["idKey"];

                DeleteArea(idkey);
            }
        }

        private void SaveArea(string areaName, string areaCd)
        {
            int count;
            string info = "";
            bool flag = false;

            //判断射频卡ID 是否重复
            sql = "select count(*) from T_BASE_AREA where T_AREACD='" + areaCd + "'";

            object obj = DBdb2.RunSingle(sql, out errMsg);

            sql = "select count(*) from T_BASE_AREA where T_AREANAME='" + areaName + "'";

            object obj1 = DBdb2.RunSingle(sql, out errMsg);

            if (obj.ToString() == "0" && obj1.ToString() == "0")
            {
                sql = "select T_AREAID from T_BASE_AREA order by ID_KEY desc FETCH FIRST 1 ROWS ONLY";

                obj = DBdb2.RunSingle(sql, out errMsg);

                if (obj != null && obj.ToString() != "")
                    count = int.Parse(obj.ToString()) + 1;
                else
                    count = 1;

                sql = @"insert into T_BASE_AREA (T_AREAID,T_AREANAME,T_AREACD) values('"
                    + count + "','"
                    + areaName + "','"
                    + areaCd + "')";

                flag = DBdb2.RunNonQuery(sql, out errMsg);

                if (errMsg == "")
                    info = "新增区域成功!";
                else
                    info = "新增区域失败!";
            }
            else
            { info = "射频卡编码和区域名称已存在"; }

            obj = new
            {
                msg = info
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }

        private void EditArea(string idkey, string areaName, string areaCd)
        {
            int count = 0;
            string info = "";

            sql = @"update T_BASE_AREA set T_AREACD='" + areaCd + "',T_AREANAME = '" + areaName + "' where ID_KEY =  " + idkey;

            bool falg = DBdb2.RunNonQuery(sql, out errMsg);

            if (errMsg == "")
                info = "区域修改成功!";
            else
                info = "区域修改失败!";

            obj = new
            {
                count = count,
                info = info
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }

        private void DeleteArea(string idkey)
        {
            string id = "";
            string info = "";
            string[] str = null;

            if (idkey.Contains(','))
            {
                str = idkey.Split(',');
            }
            else
            {
                str = new string[1];
                str[0] = idkey;
            }

            for (int i = 0; i < str.Length; i++)
            {
                id = str[i].Replace("'", "");

                sql = @"delete from T_BASE_AREA where ID_KEY =  " + id;

                bool falg = DBdb2.RunNonQuery(sql, out errMsg);

                if (errMsg == "")
                    info = "删除成功!";
                else
                    info = "删除失败!";
            }

            obj = new
            {
                msg = info
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }

        private void QueryData()
        {
            int page = Request.Form["page"] != "" ? Convert.ToInt32(Request.Form["page"]) : 0;
            int size = Request.Form["rows"] != "" ? Convert.ToInt32(Request.Form["rows"]) : 0;

            DataTable dt = bmr.RetTabAreas((page - 1) * size + 1, page * size);

            int count = bmr.RetTabAreasCounts();

            IList<Hashtable> list = new List<Hashtable>();

            foreach (DataRow row in dt.Rows)
            {
                Hashtable ht = new Hashtable();
                ht.Add("id_key", row["id_key"].ToString());
                ht.Add("t_areaid", row["t_areaid"].ToString());
                ht.Add("t_areaname", row["t_areaname"].ToString());
                ht.Add("t_routeid", row["t_routeid"].ToString());
                ht.Add("t_areacd", row["t_areacd"].ToString());
                ht.Add("rowid", row["rowid"].ToString());

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
