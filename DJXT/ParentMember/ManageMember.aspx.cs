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
using Newtonsoft.Json;
using BLL;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace DJXT.ParentMember
{
    public partial class ManageMember : System.Web.UI.Page
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

                    GetMemberOrParent(id);
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
                    RemoveMember(id);
                }
                else if (param == "JudgeMember")
                {
                    id = HttpUtility.UrlDecode(Request.Form["userName"]);
                    name = HttpUtility.UrlDecode(Request["name"]);
                    judgeMember(id, name);
                }
                else if (param == "AddMember")
                {
                    string parentID = Request.Form["classs"];
                    id = HttpUtility.UrlDecode(Request.Form["userName"]);
                    string pwd = HttpUtility.UrlDecode(Request["pwd"]);
                    name = HttpUtility.UrlDecode(Request["name"]);
                    string path = HttpUtility.UrlDecode(Request["img"]);
                    string par = Request.Form["par"];
                    string parent = Request.Form["parentId"];
                    addMember(id, name, pwd, parentID, path, parent, par);
                }
                else if (param == "EditMemberInfo")
                {
                    string oldID = Request.Form["oldId"];
                    id = Request.Form["id"];
                    string trueName = HttpUtility.UrlDecode(Request.Form["trueName"]);
                    string pwd = HttpUtility.UrlDecode(Request.Form["pwd"]);
                    string parentID = Request.Form["classId"];
                    string path = HttpUtility.UrlDecode(Request["img"]);
                    EditMember(oldID, trueName, id, pwd, parentID, path);
                }
                else if (param == "Edit")
                {
                    id = Request.Form["id"];
                    GetMemberOrParent(id);
                }
                else if (param == "JudgeMemberInfo")
                {
                    id = Request.Form["id"];
                    judgeMember(id);
                }
            }
            else
            {
                getListMenu("");
            }
        }

        #region 删除人员信息
        /// <summary>
        /// 删除人员信息
        /// </summary>
        /// <param name="id">人员编码</param>
        private void RemoveMember(string id)
        {
            res = member.RemoveMember(id);
            if (res)
                resultInfo = "人员删除成功!";
            else
                resultInfo = "人员删除失败!";
            obj = new
            {
                info = resultInfo
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 获取人员信息
        public void GetMemberOrParent(string id)
        {
            IList<Hashtable> listMembers = member.GetmemberInfo(id, 0);
            IList<Hashtable> listClass = null;
            IList<Hashtable> list = null;
            string imgs = "";
            if (listMembers != null)
            {
                list = member.GetmemberInfo(id, 1);
                Hashtable htb = new Hashtable();
                htb = listMembers[0];

                if (htb["B_ATTACHMENT"] != null && htb["B_ATTACHMENT"].ToString() != "")
                {
                    byte[] imgBytes = (byte[])(htb["B_ATTACHMENT"]);

                    string filePath = "../Files/" + htb["T_USERID"] + ".jpg";
                    imgs = filePath;
                    filePath = Server.MapPath(filePath);
                    BinaryWriter bw = new BinaryWriter(File.Open(filePath, FileMode.OpenOrCreate));
                    bw.Write(imgBytes);
                    bw.Close();
                }

                //获取值别信息
                listClass = parment.GetClass();
            }
            obj = new
            {
                img = imgs,
                list = list,
                listClass = listClass
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 编辑人员信息
        private void EditMember(string oldId, string trueName, string id, string pwd, string classID, string path)
        {
            byte[] imgBytesIn = null;
            if (path != null && path != "")
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read); //将图片以文件流的形式进行保存
                BinaryReader br = new BinaryReader(fs);
                imgBytesIn = br.ReadBytes((int)fs.Length);  //将流读入到字节数组中
            }
            res = member.EditMemberInfo(oldId, trueName, id, pwd, classID, imgBytesIn);

            if (res)
                resultInfo = "人员编辑成功!";
            else
                resultInfo = "人员编辑失败!";
            obj = new
            {
                info = resultInfo
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 添加人员
        private void addMember(string id, string name, string pwd, string classID, string path, string parent, string par)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read); //将图片以文件流的形式进行保存
            BinaryReader br = new BinaryReader(fs);
            byte[] imgBytesIn = br.ReadBytes((int)fs.Length);  //将流读入到字节数组中

            if (member.AddMember(id, name, pwd, classID, imgBytesIn) && member.AddMember(parent, id, Convert.ToInt32(par)))
                resultInfo = "人员添加成功!";
            else
                resultInfo = "人员添加失败!";
            obj = new
            {
                info = resultInfo
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 判断是否存在该人员
        private void judgeMember(string id, string name)
        {
            if (member.JudgMember(id))
                count = 1;
            int num = 0;
            if (member.JudgMemberByName(name))
                num = 1;
            obj = new
            {
                judge = count,
                num = num
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        private void judgeMember(string id)
        {
            if (member.JudgMember(id))
                count = 1;
            obj = new
            {
                judge = count
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 查询子岗信息
        /// <summary>
        /// 查询子岗信息
        /// </summary>
        /// <param name="id">父类ID</param>
        /// <param name="page">第几页数</param>
        /// <param name="rows">每页数据量</param>
        private void GetMenuByID(string id, int page, int rows)
        {
            dt = member.GetMember(id, (page - 1) * rows + 1, page * rows);
            count = member.GetMemberCount(id);
            IList<Hashtable> list = new List<Hashtable>();
            foreach (DataRow row in dt.Rows)
            {
                Hashtable ht = new Hashtable();
                ht.Add("key", row["ID"].ToString());
                ht.Add("id", row["UserID"].ToString());
                ht.Add("name", row["UserName"].ToString());
                ht.Add("parmentID", row["parmentID"].ToString());
                ht.Add("plan", row["Plan"].ToString());
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
            //获取值别信息
            IList<Hashtable> list = parment.GetClass();

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
                    menu = resultMenu,
                    list = list
                };
                string result = JsonConvert.SerializeObject(obj);
                Response.Write(result);
                Response.End();
            }
        }
        #endregion
    }
}