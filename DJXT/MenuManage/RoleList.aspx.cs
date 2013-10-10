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

public partial class RoleList : System.Web.UI.Page
{
    ParmentBLL bll = new ParmentBLL();
    MemberBLL member = new MemberBLL();
    DataTable dt = new DataTable();
    StringBuilder st = new StringBuilder();
    object obj = null;
    string resultMenu = "";

    int judge = 0;
    string id = "";
    string name = "";
    string parentID = "";
    string pwd = "";
    string path = "";
    IList<Hashtable> list = null;

    bool res = false;
    string resultInfo = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string para = Request.Form["param"];
        if (para != "")
        {

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
        list = bll.GetClass();

        //获取所有人员信息
        IList<Hashtable> listMembers = member.GetMembers();

        //获取人员岗位关系
        DataTable dtMemberParent = new DataTable();
        dtMemberParent = member.GetMembersAndParent();
        if (dt != null && dt.Rows.Count > 0)
        {
            st.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["T_PARENTID"].ToString() == "0")
                {
                    ifJuage = judgeMemberByClassID(dt.Rows[i]["T_ORGID"].ToString());
                    st.Append("{id:'" + dt.Rows[i]["T_ORGID"] + "',pId:'" + dt.Rows[i]["T_PARENTID"] + "',name:'" + dt.Rows[i]["T_ORGDESC"] + "',t:'" + ifJuage + "', open:true},");//#1对应页面里的#1
                }
                else
                {
                    ifJuage = judgeMemberByClassID(dt.Rows[i]["T_ORGID"].ToString());
                    st.Append("{id:'" + dt.Rows[i]["T_ORGID"] + "',pId:'" + dt.Rows[i]["T_PARENTID"] + "',name:'" + dt.Rows[i]["T_ORGDESC"] + "',t:'" + ifJuage + "'},");
                }
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
                list = list,
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

}
