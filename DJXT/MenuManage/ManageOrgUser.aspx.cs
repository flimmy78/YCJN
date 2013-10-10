using System;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.XPath;
using System.Data.OleDb;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;

public partial class MenuManage_ManageOrgUser : System.Web.UI.Page
{
    string xmlpath = "";
    string webpath = "~/MenuManage/ManageMenu.aspx";
    string FileName = "";
    string errMsg = "";
    string message = "";
    string resultInfo = "";
    bool res = false;
    private static string treeID = "";
    private static string treeName = "";
    DataTable dtb = new DataTable();
    BLL.BLLRole bl = new BLL.BLLRole();
    XmlDocument xmldoc = new XmlDocument();

    object obj = null;
    int count = 0;
    private static string treeId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = HttpUtility.UrlDecode(Request.QueryString["TreeId"]);
        string orgName = "";
        id = "Tree";
        if (id != "" && id != null)
            treeId = "1";
        else
            treeId = "0";

        string param = Request["param"];
        if (param != "")
        {
            if (param == "seachList")//选择树中的节点时运行
            {
                id = Request.Form["id"];//选定的节点的ID
                //orgName = Request.Form["name"];//选定的节点的文字描述
                //this.LabTreeName.Value = treeName.ToString();
                //this.LabOrgName.Value = orgName.ToString();
                int page = Convert.ToInt32(Request["page"].ToString());
                int rows = Convert.ToInt32(Request["rows"].ToString());
                GetUserByTreeID(id, page, rows);
            }
            else if (param == "JudgeMember")
            {
                string userID = Request.Form["userID"];
                judgeMember(userID);
            }
            else if (param == "AddMember")
            {
                string userID = Request.Form["userID"];
                string userName = HttpUtility.UrlDecode(Request["userName"]);
                string pwd = HttpUtility.UrlDecode(Request["pwd"]);
                string path = HttpUtility.UrlDecode(Request["img"]);
                string treeNodeId = Request.Form["treeNodeId"];//当前组织机构的ID
                string treeAllId = Request.Form["treeAllId"];//当前组织机构树的ID
                AddMember(userID, userName, pwd, path, treeNodeId, treeAllId);
            }
            else if (param == "Edit")
            {
                id = Request.Form["id"];
                GetMemberOrParent(id);
            }
            else if (param == "EditMemberInfo")
            {
                string userIDO = Request.Form["oldId"];
                string userID = Request.Form["id"];
                string userName = HttpUtility.UrlDecode(Request.Form["name"]);
                string pwd = HttpUtility.UrlDecode(Request["pwd"]);
                string path = HttpUtility.UrlDecode(Request["img"]);
                string treeNodeId = Request.Form["treeNodeId"];//当前组织机构的ID
                string treeAllId = Request.Form["treeAllId"];//当前组织机构树的ID
                EditMember(userIDO, userID, userName, pwd, path, treeNodeId, treeAllId);
            }
            else if (param == "Remove")
            {
                id = Request["id"].ToString();
                RemoveMember(id);
            }
        }
        else
        {
            DownLoadXml(treeID);
            GetTreeListAll(treeID);//生成树
        }
        if (!Page.IsPostBack)//页面第一次加载时运行
        {
            BindTree(sender, e);
            treeID = DropDownList1.SelectedValue.ToString();//整个组织机构的ID
            treeName = DropDownList1.SelectedItem.Text;//整个组织机构的文字描述
            
        }
    }
    static DataTable dt = new DataTable();
    static string PTreeNodes = "";

    //根据组织机构树和组织机构找到所有用户
    private void GetUserByTreeID(string id, int page, int rows)
    {
        dtb = bl.GetUserMenu(id, treeID, (page - 1) * rows + 1, page * rows);
        count = bl.GetUserCount(id, treeID);
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

    //在页面展示时生成树的方法
    private void GetTreeList(string TreeID)
    {
        string tr = "";
        if (TreeID != "")
        {
            XmlTextReader reader = new XmlTextReader(
              Server.MapPath(TreeID + ".xml"));

            reader.WhitespaceHandling = WhitespaceHandling.None;
            XmlDocument xmlDoc = new XmlDocument();
            //将文件加载到XmlDocument对象中
            xmlDoc.Load(reader);
            //关闭连接
            reader.Close();

            XmlNode xnod = xmlDoc.DocumentElement;

            dt.Columns.Add("ID");
            dt.Columns.Add("NAME");
            dt.Columns.Add("PID");

            XmlToDataTable(xnod);

            DataSet ds = new DataSet();
            ds = bl.GetTreeNodeID("admin", "tree");
            //DataTable dt = ds.Tables[0];

            DataTable dts = dt.Copy();
            dt.Reset();
            //GetTreeNodesParentID(dts, ds.Tables[0].Rows[0][0].ToString());
            //GetTreeNodeSunList(dts, ds.Tables[0].Rows[0][0].ToString());

            GetTreeNodesParentID(dts, "8");
            GetTreeNodeSunList(dts, "8");

            tr = PTreeNodes;

            tr = tr.Substring(0, tr.Length - 1);
            tr = "[" + tr + "]";
        }
        object obj = new
        {
            menu = tr,
            treeId = treeId
        };
        PTreeNodes = "";
        string result = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        Response.Write(result);
        Response.End();

    }
    //生成树字符串
    public string GetTreeList(DataTable dt1)
    {
        string tree = "";
        tree += "[";
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            if (dt.Rows[i]["PID"].ToString() == "0")
                tree += "{id:'" + dt1.Rows[i]["ID"] + "',pId:'" + dt1.Rows[i]["PID"] + "',name:'" + dt1.Rows[i]["NAME"] + "',t:'" + dt1.Rows[i]["NAME"] + "',open:true},";
            else
                tree += "{id:'" + dt1.Rows[i]["ID"] + "',pId:'" + dt1.Rows[i]["PID"] + "',name:'" + dt1.Rows[i]["NAME"] + "',t:'" + dt1.Rows[i]["NAME"] + "'},";
        }
        tree = tree.Substring(0, tree.Length - 1);
        tree += "]";
        return tree;
    }
    //人员管理时生成所有节点的树的方法
    private void GetTreeListAll(string TreeID)
    {
        string tr = "";
        if (TreeID != "")
        {
            XmlTextReader reader = new XmlTextReader(
              Server.MapPath(TreeID + ".xml"));

            reader.WhitespaceHandling = WhitespaceHandling.None;
            XmlDocument xmlDoc = new XmlDocument();
            //将文件加载到XmlDocument对象中
            xmlDoc.Load(reader);
            //关闭连接
            reader.Close();

            XmlNode xnod = xmlDoc.DocumentElement;
            dt.Columns.Add("ID");
            dt.Columns.Add("NAME");
            dt.Columns.Add("PID");

            XmlToDataTable(xnod);

            DataSet ds = new DataSet();
            ds = bl.GetTreeNodeID("admin", "tree");
        }
        string Trees = GetTreeList(dt);
        dt.Reset();
        object obj = new
        {
            menu = Trees,
            treeId = treeId
        };
        PTreeNodes = "";
        string result = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        Response.Write(result);
        Response.End();

    }

    /// <summary>
    /// 获取父类节点Tree
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="TreeNodeId"></param>
    public void GetTreeNodesParentID(DataTable dt, string TreeNodeId)
    {
        DataRow[] irows = null;
        irows = dt.Select("ID='" + TreeNodeId + "'");
        if (irows.Length > 0)
        {
            PTreeNodes += "{id:'" + irows[0][0] + "',pId:'" + irows[0][2] + "',name:'" + irows[0][1] + "',t:'" + irows[0][2] + "',open:true},";
            GetTreeNodesParentID(dt, irows[0][2].ToString());
        }
    }

    /// <summary>
    /// 获取子类节点Tree
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="TreeNodeId"></param>
    public void GetTreeNodeSunList(DataTable dt, string TreeNodeId)
    {
        DataRow[] irows = null;
        irows = dt.Select("PID='" + TreeNodeId + "'");
        if (irows.Length > 0)
        {
            for (int k = 0; k < irows.Length; k++)
            {
                PTreeNodes += "{id:'" + irows[k][0] + "',pId:'" + irows[k][2] + "',name:'" + irows[k][1] + "',t:'" + irows[k][2] + "'},";
                GetTreeNodeSunList(dt, irows[k][0].ToString());
            }
        }
    }

    /// <summary>
    /// 将XML转换成DataTable
    /// </summary>
    /// <param name="xnod"></param>
    /// <param name="intLevel"></param>
    private void XmlToDataTable(XmlNode xnod)
    {
        DataRow dr = dt.NewRow();
        XmlNode xnodWorking;

        //如果是元素节点，获取它的属性
        if (xnod.NodeType == XmlNodeType.Element)
        {
            XmlNamedNodeMap mapAttributes = xnod.Attributes;
            if (mapAttributes.Count > 0)
            {
                dr[0] = mapAttributes.Item(0).Value;
                dr[1] = mapAttributes.Item(1).Value;
                dr[2] = mapAttributes.Item(2).Value;
                dt.Rows.Add(dr);
            }

            //如果还有子节点，就递归地调用这个程序
            if (xnod.HasChildNodes)
            {
                xnodWorking = xnod.FirstChild;
                while (xnodWorking != null)
                {
                    XmlToDataTable(xnodWorking);
                    xnodWorking = xnodWorking.NextSibling;
                }
            }
        }
    }

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

    //将数据库中的XML文件下载到服务器的指定路径
    protected void DownLoadXml(string id)//下载XML
    {
        xmlpath = Server.MapPath(id + ".xml");
        bool ret = true;
        string DBtype = bl.GetDBtype();
        if (DBtype == "SQL")//从SQLSERVER数据库下载菜单XML文件
        {
            //try
            //{
            //    SqlConnection sqlconn = SAC.sqlHelper.DBsql.GetConnection();
            //    string sqlstr = "select * from T_SYS_MENU where T_XMLID='Webmenu'";
            //    SqlCommand sqlcmd = new SqlCommand(sqlstr, sqlconn);
            //    SqlDataReader sqlreader = sqlcmd.ExecuteReader();
            //    FileName = xmlpath;
            //    if (!sqlreader.Read())
            //    {
            //        FileName = "";
            //    }
            //    else
            //    {
            //        byte[] bytes = (byte[])sqlreader["B_XML"];
            //        FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write);
            //        fs.Write(bytes, 0, bytes.Length);
            //        fs.Flush();
            //        fs.Close();
            //    }
            //    sqlreader.Close();
            //    sqlconn.Close();
            //}
            //catch (Exception ce)
            //{
            //    errMsg = ce.Message;
            //    ret = false;
            //}
        }
        else//从DB2数据库下载菜单XML文件
        {
            try
            {
                string connstr = bl.GetConnstr(out errMsg).ToString();
                OleDbConnection db2conn = new OleDbConnection(connstr);
                string sqlstr = "select * from T_SYS_MENU where T_XMLID='" + id + "'";
                OleDbCommand db2cmd = new OleDbCommand(sqlstr, db2conn);
                db2conn.Open();
                OleDbDataReader db2reader = db2cmd.ExecuteReader();
                FileName = xmlpath;
                if (!db2reader.Read())
                {
                    FileName = "";
                }
                else
                {
                    byte[] bytes = (byte[])db2reader["B_XML"];
                    FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Flush();
                    fs.Close();
                }
                db2reader.Close();
                db2conn.Close();
            }
            catch (Exception ce)
            {
                errMsg = ce.Message;
                ret = false;
            }
        }
    }

    #region 根据用户名判断是否存在该人员
    private void judgeMember(string userID)
    {
        if (bl.JudgMember(userID))
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

    #region 添加人员
    private void AddMember(string id, string name, string pwd, string path, string treeNodeId, string treeAllId)
    {
        if (treeNodeId != null && treeNodeId != "" && treeAllId != null && treeAllId != "")
        {
            if (path != null && path != "")
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read); //将图片以文件流的形式进行保存
                BinaryReader br = new BinaryReader(fs);
                byte[] imgBytesIn = br.ReadBytes((int)fs.Length);  //将流读入到字节数组中
                if (bl.AddMember(id, name, pwd, imgBytesIn, treeNodeId, treeAllId))
                    resultInfo = "人员添加成功!";
                else
                    resultInfo = "人员添加失败!";
            }
            else
            {
                if (bl.AddMember(id, name, pwd, null, treeNodeId, treeAllId))
                    resultInfo = "人员添加成功!";
                else
                    resultInfo = "人员添加失败!";
            }
        }
        else
        {
            resultInfo = "请选择一个组织机构!";
        }
        obj = new
        {
            info = resultInfo
        };
        string result = JsonConvert.SerializeObject(obj);
        Response.Write(result);
        Response.End();

    }
    #endregion

    #region 获取编辑的人员信息
    public void GetMemberOrParent(string id)
    {
        IList<Hashtable> listMembers = bl.GetmemberInfo(id, 0);
        IList<Hashtable> list = null;
        string imgs = "";
        if (listMembers != null)
        {
            list = bl.GetmemberInfo(id, 1);
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

        }
        obj = new
        {
            img = imgs,
            list = list,
        };
        string result = JsonConvert.SerializeObject(obj);
        Response.Write(result);
        Response.End();
    }
    #endregion

    #region 编辑人员信息
    private void EditMember(string userIDO, string userID, string userName, string pwd, string path, string treeNodeId, string treeAllId)
    {
        byte[] imgBytesIn = null;
        if (path != null && path != "")
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read); //将图片以文件流的形式进行保存
            BinaryReader br = new BinaryReader(fs);
            imgBytesIn = br.ReadBytes((int)fs.Length);  //将流读入到字节数组中
        }
        res = bl.EditMemberInfo(userIDO, userID, userName, pwd, imgBytesIn, treeNodeId, treeAllId);

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

    #region 删除人员信息
    /// <summary>
    /// 删除人员信息
    /// </summary>
    /// <param name="id">人员编码</param>
    private void RemoveMember(string id)
    {
        res = bl.RemoveMember(id);
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
}