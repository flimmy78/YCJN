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
using Newtonsoft.Json;
using System.Collections.Generic;

public partial class MenuManage_ManageRole : System.Web.UI.Page
{
    BLL.BLLRole bz = new BLLRole();
    IList<Hashtable> list = null;
    object obj = null;
    int count = 0;
    string errMsg = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string param = Request["param"];
        if (param != "")
        {
            if (param == "seachList")
            {
                GetBCList();
            }
            else if (param == "Add")
            {
                string id = Request.Form["id"];
                string name = Request.Form["name"];
                btnSure_Click(id, name, "");
            }
            else if (param == "Edit")
            {
                string id = Request["id"].ToString();
                string name = Request["name"].ToString();
                string oid = Request["oid"].ToString();
                btnSure_Click(id, name, oid);
            }
            else if (param == "Remove")
            {
                string id = Request["id"].ToString();
                gridBC_RowDeleting(id);
            }
        }
        if (!IsPostBack)
        {
            //GridBC.DataSource = bz.RetAllBC(out errMsg);
            //GridBC.DataBind();
            //GetBCList();
        }
    }
    protected void btnSure_Click(string roleId, string roleName, string OroleId)
    {
        string message = "";
        try
        {
            if (OroleId == "")
            {
                if (bz.SaveRole(roleId, roleName, out errMsg))
                {
                    //GridBZ.DataSource = bz.RetAllBZ(out errMsg);
                    //GridBZ.DataBind();
                    message = "添加角色成功！";
                }
                else
                {
                    message = "添加角色失败！请检查是否存在相同的职别编号！";
                }
            }
            else
            {
                if (bz.UpDateRole(OroleId, roleId, roleName, out errMsg))
                {
                    //GridBZ.DataSource = bz.RetAllBZ(out errMsg);
                    //GridBZ.DataBind();
                    message = "修改角色成功";
                }
                else
                {
                    message = "修改角色失败！请检查是否存在相同的角色编号！";
                }
            }
        }
        catch (Exception ex)
        {
            message = "添加角色失败！失败信息：" + ex.Message.ToString();
        }

        obj = new
        {
            message = message
        };
        string result = JsonConvert.SerializeObject(obj);
        Response.Write(result);
        Response.End();

    }

    protected void gridBC_RowDeleting(string rId)
    {
        //string BcId = GridBC.DataKeys[e.RowIndex].Value.ToString();

        string message = "";
        if (bz.DeleteRole(rId, out errMsg))
        {
            //GridBC.DataSource = bz.RetAllBC(out errMsg);
            //GridBC.DataBind();
            message = "删除角色成功";
        }
        else
        {
            message = "修改角色失败！请检查是否存在相同的班次名！";
        }
        obj = new
        {
            message = message
        };
        string result = JsonConvert.SerializeObject(obj);
        Response.Write(result);
        Response.End();
    }

    #region 所有班次信息
    public void GetBCList()
    {
        int page = Convert.ToInt32(Request["page"].ToString());
        int rows = Convert.ToInt32(Request["rows"].ToString());
        DataTable dt = bz.GetAllRole((page - 1) * rows + 1, page * rows);
        count = bz.GetRoleCount();
        IList<Hashtable> list = new List<Hashtable>();

        foreach (DataRow item in dt.Rows)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ID_KEY", item["ID_KEY"]);
            ht.Add("T_GRPID", item["T_GRPID"].ToString());
            ht.Add("T_GRPDESC", item["T_GRPDESC"].ToString());
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
}
