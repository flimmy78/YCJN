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
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace DJXT.ParentMember
{
    public partial class ManageParent : System.Web.UI.Page
    {
        ParmentBLL parment = new ParmentBLL();
        MemberBLL member = new MemberBLL();
        DataTable dt = new DataTable();
        StringBuilder st = new StringBuilder();
        object obj = null;
        string resultMenu = "";

        string id = "";
        string name = "";
        string oldId = "";
        int count = 0;

        bool res = false;
        string resultInfo = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            string param = Request["param"];
            if (param != "")
            {
                if (param == "seachList")
                {
                    id = Request.Form["id"];
                    int page = Convert.ToInt32(Request["page"].ToString());
                    int rows = Convert.ToInt32(Request["rows"].ToString());
                    GetMenuByID(id, page, rows);
                }
                else if (param == "Edit")
                {
                    id = Request["id"].ToString();
                    name = HttpUtility.UrlDecode(Request["name"].ToString());
                    oldId = Request["oldID"].ToString();

                    EditOrgainze(oldId, id, name);
                }
                else if (param == "Add")
                {
                    id = Request["id"].ToString();
                    name = HttpUtility.UrlDecode(Request["name"].ToString());
                    string parentID = Request["pID"].ToString();
                    AddOrgainze(parentID, id, name);
                }
                else if (param == "Remove")
                {
                    id = Request["id"].ToString();
                    RemoveOrgainze(id);
                }
            }
            else
            {
                getListMenu("");
            }
        }



        #region 查询子岗信息
        /// <summary>
        /// 查询子岗信息
        /// </summary>
        /// <param name="id">父类ID</param>
        /// <param name="page">第几页数</param>
        /// <param name="rows">每页数据量</param>
        private void GetMenuByID(string id, int page, int rows)
        {
            dt = parment.Getmenu(id, (page - 1) * rows + 1, page * rows);
            count = parment.GetmenuCount(id);
            IList<Hashtable> list = new List<Hashtable>();
            foreach (DataRow row in dt.Rows)
            {
                Hashtable ht = new Hashtable();
                ht.Add("key", row["ID_KEY"].ToString());
                ht.Add("id", row["T_ORGID"].ToString());
                ht.Add("name", row["T_ORGDESC"].ToString());
                ht.Add("parmentID", row["T_PARENTID"].ToString());
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
        #endregion

        #region 删除组织关系
        /// <summary>
        /// 编辑组织关系
        /// </summary>
        /// <param name="id">组织编码</param>
        private void RemoveOrgainze(string id)
        {
            res = parment.RemoveOrganize(id);
            if (res)
                resultInfo = "组织删除成功!";
            else
                resultInfo = "组织删除失败!";
            obj = new
            {
                info = resultInfo
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 添加组织关系
        /// <summary>
        /// 添加组织关系
        /// </summary>
        /// <param name="parentId">父类ID</param>
        /// <param name="id">组织编码</param>
        /// <param name="name">组织名称</param>
        private void AddOrgainze(string parentId, string id, string name)
        {
            if (parment.JudgeExitParent(id))
            {
                resultInfo = "已经存在ID为  " + id + "  的组织!";
                count = 1;
            }
            else
            {
                res = parment.AddOrganize(parentId, id, name);
                if (res)
                    resultInfo = "组织添加成功!";
                else
                    resultInfo = "组织添加失败!";
            }
            obj = new
            {
                judge = count,
                info = resultInfo
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 编辑组织关系
        /// <summary>
        /// 编辑组织信息
        /// </summary>
        /// <param name="oid">组织原编码</param>
        /// <param name="id">组织编码</param>
        /// <param name="name">组织名称</param>
        private void EditOrgainze(string oid, string id, string name)
        {
            res = parment.EditOrganize(oid, id, name);
            if (res)
                resultInfo = "组织编辑成功!";
            else
                resultInfo = "组织编辑失败!";
            obj = new
            {
                info = resultInfo
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 初始化tree  绑定值别
        /// <summary>
        /// 初始化组织结构
        /// </summary>
        private void getListMenu(string id)
        {
            dt = parment.GetMenu();
            DataTable dtClass = new DataTable();

            if (dt != null && dt.Rows.Count > 0)
            {
                st.Append("[");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (id != "")
                        if (dt.Rows[i]["T_ORGID"].ToString() == id)
                            st.Append("{id:'" + dt.Rows[i]["T_ORGID"] + "',pId:'" + dt.Rows[i]["T_PARENTID"] + "',name:'" + dt.Rows[i]["T_ORGDESC"] + "',t:'" + dt.Rows[i]["T_ORGDESC"] + "', open:true},");
                        else
                            st.Append("{id:'" + dt.Rows[i]["T_ORGID"] + "',pId:'" + dt.Rows[i]["T_PARENTID"] + "',name:'" + dt.Rows[i]["T_ORGDESC"] + "',t:'" + dt.Rows[i]["T_ORGDESC"] + "'},");
                    else
                        if (dt.Rows[i]["T_PARENTID"].ToString() == "0")
                            st.Append("{id:'" + dt.Rows[i]["T_ORGID"] + "',pId:'" + dt.Rows[i]["T_PARENTID"] + "',name:'" + dt.Rows[i]["T_ORGDESC"] + "',t:'" + dt.Rows[i]["T_ORGDESC"] + "', open:true},");
                        else
                            st.Append("{id:'" + dt.Rows[i]["T_ORGID"] + "',pId:'" + dt.Rows[i]["T_PARENTID"] + "',name:'" + dt.Rows[i]["T_ORGDESC"] + "',t:'" + dt.Rows[i]["T_ORGDESC"] + "'},");
                }

                resultMenu = st.ToString().Substring(0, st.ToString().Length - 1) + "]";
                obj = new
                {
                    id = dt.Rows[0]["T_ORGID"],
                    name = dt.Rows[0]["T_ORGDESC"],
                    parentID = dt.Rows[0]["T_PARENTID"],
                    menu = resultMenu
                };
                string result = JsonConvert.SerializeObject(obj);
                Response.Write(result);
                Response.End();
            }
        }
        #endregion
    }
}