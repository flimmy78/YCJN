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
using System.Xml;
using System.Xml.XPath;
using System.Data.SqlClient;
using System.Data.OleDb;

public partial class MenuManage_ManageRoleUser : System.Web.UI.Page
{
    BLL.BLLRole bl = new BLL.BLLRole();
    ParmentBLL bll = new ParmentBLL();
    MemberBLL member = new MemberBLL();
    DataTable dt = new DataTable();
    StringBuilder st = new StringBuilder();
    int count = 0;
    object obj = null;
    string resultMenu = "";
    private static string treeID = "";
    private static string treeName = "";
    string errMsg = "";
    int judge = 0;
    string roleId = "";
    string name = "";
    string parentID = "";
    string pwd = "";
    string path = "";
    IList<Hashtable> list = null;
    DataTable dtb = new DataTable();
    bool res = false;
    string resultInfo = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string para = Request.Form["param"];
        if (para != "")
        {
            if (para == "seachList")//选择树中的节点时运行
            {
                roleId = Request.Form["id"];//选定的节点的ID
                //orgName = Request.Form["name"];//选定的节点的文字描述
                //this.LabTreeName.Value = treeName.ToString();
                //this.LabOrgName.Value = orgName.ToString();
                int page = Convert.ToInt32(Request["page"].ToString());
                int rows = Convert.ToInt32(Request["rows"].ToString());
                GetUserByRole(roleId, page, rows);
            }

        }
        else
        {
            getListMenu();
        }
    }

    #region 初始化tree  绑定值别
    /// <summary>
    /// 初始化组织结构
    /// </summary>
    private void getListMenu()
    {
        string ifJuage = "";
        dt = bll.GetMenu();
        DataTable dtClass = new DataTable();

        //获取值别信息
        //list = bll.GetClass();

        //获取所有人员信息
        IList<Hashtable> listMembers = member.GetMembers();

        //获取人员岗位关系
        //DataTable dtMemberParent = new DataTable();
        //dtMemberParent = member.GetMembersAndParent();
        if (dt != null && dt.Rows.Count > 0)
        {
            st.Append("[");
            st.Append("{id:'qianwanbunengdengyu1',pId:'0',name:'全部角色',t:'不存在人员', open:true},");//#1对应页面里的#1
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //if (i == 0)
                //{
                //    ifJuage = "不存在人员";
                //    st.Append("{id:'0',pId:'0',name:'角色列表',t:'" + ifJuage + "', open:true},");//#1对应页面里的#1
                //}
                //else
                //{
                //    ifJuage = judgeMemberByClassID(dt.Rows[i]["T_GRPID"].ToString());
                //    st.Append("{id:'" + dt.Rows[i]["T_GRPID"] + "',pId:'1',name:'" + dt.Rows[i]["T_GRPDESC"] + "',t:'" + ifJuage + "'},");
                //}
                ifJuage = judgeMemberByClassID(dt.Rows[i]["T_GRPID"].ToString());
                st.Append("{id:'" + dt.Rows[i]["T_GRPID"] + "',pId:'qianwanbunengdengyu1',name:'" + dt.Rows[i]["T_GRPDESC"] + "',t:'" + ifJuage + "'},");
            }
            //for (int i = 0; i < dtMemberParent.Rows.Count; i++)
            //{
            //    st.Append("{id:'" + dtMemberParent.Rows[i]["T_USERID"] + "',pId:'" + dtMemberParent.Rows[i]["T_ORGID"] + "',name:'" + dtMemberParent.Rows[i]["T_USERNAME"] + "',t:'" + dtMemberParent.Rows[i]["T_USERNAME"] + "'},");
            //}
            resultMenu = st.ToString().Substring(0, st.ToString().Length - 1) + "]";
            obj = new
            {
                count = 1,
                menu = resultMenu,
                list = "",
                listMember = listMembers
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        else
        {
            obj = new
            {
                count = 0
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
    }
    #endregion

    #region 判断某个岗位下面是否存在人员
    /// <summary>
    /// 判断某个岗位下面是否存在人员
    /// </summary>
    /// <param name="id">人员编码</param>
    private string judgeMemberByClassID(string id)
    {
        string ifJudge = "";
        res = member.JudgMemberByClassId(id);
        if (res)
            ifJudge = "存在人员";
        else
            ifJudge = "不存在人员";
        return ifJudge;
    }
    #endregion

    #region 根据角色找到所有用户
    private void GetUserByRole(string id, int page, int rows)
    {
        dtb = bl.GetUserMenuByRole(id, (page - 1) * rows + 1, page * rows);
        count = bl.GetUserCountByRole(id);
        IList<Hashtable> list = new List<Hashtable>();
        foreach (DataRow row in dtb.Rows)
        {
            Hashtable ht = new Hashtable();
            ht.Add("key", row["ID_KEY"].ToString());
            ht.Add("id", row["T_USERID"].ToString());
            ht.Add("name", row["T_USERNAME"].ToString());
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

    //将组织机构列表绑定到下拉列表
    protected void BindTree(object sender, EventArgs e)
    {
        DataTable dt = bl.GetTreeMenu(out errMsg);
        this.DropDownList1.DataSource = dt;
        this.DropDownList1.DataTextField = "T_XMLNAME";
        this.DropDownList1.DataValueField = "T_XMLID";
        this.DropDownList1.DataBind();
    }

    //选择其他组织机构树时重新读取组织机构树ID
    protected void DropDownListChange(object sender, EventArgs e)
    {
        treeID = DropDownList1.SelectedValue.ToString();//整个组织机构的ID
        treeName = DropDownList1.SelectedItem.Text;//整个组织机构的文字描述
    }
}