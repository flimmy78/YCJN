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
using BLL;

public partial class ManagePower : System.Web.UI.Page
{
    string xmlpath = "";
    string webpath = "~/MenuManage/ManagePower.aspx";
    string errMsg = "";
    string FileName = "";
    XmlDocument xmldoc = new XmlDocument();

    BLLRole bl = new BLLRole();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            try
            {
                xmlpath = Server.MapPath("EditMenu.xml");
                DownLoadXml(sender, e);//下载XML文件至指定路径
                treeMenuPower.Nodes.Clear();
                //新建个DataSource指向要绑定的文件
                XmlDataSource xds = new XmlDataSource();
                xds.DataFile = xmlpath;
                XmlDocument xmlDocument = xds.GetXmlDocument();
                //把根节点和treeView实例根节点群放进去递归
                BindXmlToTreeView(xmlDocument.DocumentElement, treeMenuPower.Nodes);
                if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
                {
                    string id = Request.QueryString["id"].ToString();
                    string rolename = bl.GetRoleNameById(id,out errMsg).ToString();
                    this.lblRoleName.Text = rolename;

                    ShowTreeCheck(id, treeMenuPower.Nodes);
                }
                treeMenuPower.ExpandAll();
            }
            catch
            {

            }
        }
    }

    //确定分配
    protected void btnSure_Click(object sender, EventArgs e)
    {
        if (this.lblRoleName.Text != "尚未选择任何角色")
        {
            xmlpath = Server.MapPath("EditMenu.xml");
            xmldoc.Load(xmlpath);
            //XmlNode xnode = xmldoc.DocumentElement.FirstChild;
            //XmlNode xnode = xmldoc.DocumentElement;
            //TreeNode tNode = new TreeNode();
            string roleId = Request.QueryString["id"].ToString();
            if (roleId != "")
            {
                //SaveTreeCheck(rolename, xnode, treeMenuPower.Nodes);
                SaveTreeCheck1(roleId);
            }

            BtnUp_Click(sender,e);
            ShowTreeCheck(roleId, treeMenuPower.Nodes);
        }
    }

    //取消
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearTreeCheck(treeMenuPower.Nodes);
        this.lblRoleId.Text = "";
        this.lblRoleName.Text = "尚未选择任何角色";
    }

    //加载XML文件
    protected void LoadXml(string xmlPathS)
    {
        treeMenuPower.Nodes.Clear();
        //xmlpath = FileUpload1.PostedFile.FileName.ToString();
        xmlpath = xmlPathS;
        Session.Contents["xmlpath"] = xmlpath;
        XmlDataSource xds = new XmlDataSource();
        xds.DataFile = xmlpath;
        XmlDocument xmlDocument = xds.GetXmlDocument();
        //把根节点的东东和treeView实例根节点群丢进去递归
        BindXmlToTreeView(xmlDocument.DocumentElement, treeMenuPower.Nodes);
        if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
        {
            string id = Request.QueryString["id"].ToString();
            string rolename = id;
            this.lblRoleName.Text = rolename;

            ShowTreeCheck(rolename, treeMenuPower.Nodes);
        }
        treeMenuPower.ExpandAll();
    }

    //通过递归将XML节点生成treeview
    private void BindXmlToTreeView(XmlNode node, TreeNodeCollection tnc)
    {
        string visible = "";
        if (node.ChildNodes.Count == 0)
        {
            visible = "~/img/tree_file.jpg";
        }
        else
        {
            visible = "~/img/tree_folder.jpg";
        }
        string strText = node.Attributes["caption"].Value;
        string owner = node.Attributes["owner"].Value;
        TreeNode Tnode = new TreeNode();//创建新节点
        Tnode.Text = strText;//设置节点的属性
        Tnode.ImageUrl = visible;
        Tnode.Target = owner;
        //tnc.Add(new TreeNode(strText, owner));
        tnc.Add(Tnode);
        foreach (XmlNode n in node.ChildNodes)
        {
            //指向子节点和父节点的子节点群
            BindXmlToTreeView(n, tnc[tnc.Count - 1].ChildNodes);
        }

    }

    private void ShowTreeCheck(string roleId, TreeNodeCollection treeNodes)
    {
        foreach (TreeNode node in treeNodes)
        {
            string owner = node.Target.ToString();
            string[] val = owner.Split(',');
            for (int i = 0; i < val.Length; i++)
            {
                if (roleId == val[i])
                {
                    node.Checked = true;

                    break;
                }
            }
            ShowTreeCheck(roleId, node.ChildNodes);
        }
    }

    private void ClearTreeCheck(TreeNodeCollection treeNodes)
    {
        foreach (TreeNode node in treeNodes)
        {
            if (node.Checked)
            {
                node.Checked = false;
            }
            ClearTreeCheck(node.ChildNodes);
        }
    }

    private void SaveTreeCheck1(string roleId)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int l = 0;
        xmlpath = Server.MapPath("EditMenu.xml");
        xmldoc.Load(xmlpath);
        TreeNodeCollection tNode = treeMenuPower.Nodes;
        //根目录
        Save(roleId, xmldoc.ChildNodes[1], tNode[0]);

        //第一层
        for (i = 0; i < tNode[0].ChildNodes.Count; i++)
        {
            Save(roleId, xmldoc.ChildNodes[1].ChildNodes[i], tNode[0].ChildNodes[i]);
        }

        //第二层
        for (i = 0; i < tNode[0].ChildNodes.Count; i++)
        {
            for (j = 0; j < tNode[0].ChildNodes[i].ChildNodes.Count; j++)
            {
                Save(roleId, xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes[j], tNode[0].ChildNodes[i].ChildNodes[j]);
            }
        }

        //第三层
        for (i = 0; i < tNode[0].ChildNodes.Count; i++)
        {
            for (j = 0; j < tNode[0].ChildNodes[i].ChildNodes.Count; j++)
            {
                for (k = 0; k < tNode[0].ChildNodes[i].ChildNodes[j].ChildNodes.Count; k++)
                {
                    Save(roleId, xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes[j].ChildNodes[k], tNode[0].ChildNodes[i].ChildNodes[j].ChildNodes[k]);
                }
            }
        }

        //第四层
        for (i = 0; i < tNode[0].ChildNodes.Count; i++)
        {
            for (j = 0; j < tNode[0].ChildNodes[i].ChildNodes.Count; j++)
            {
                for (k = 0; k < tNode[0].ChildNodes[i].ChildNodes[j].ChildNodes.Count; k++)
                {
                    for (l = 0; l < tNode[0].ChildNodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Count; l++)
                    {
                        Save(roleId, xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l], tNode[0].ChildNodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l]);
                    }
                }
            }
        }

    }

    //进行逻辑判断并修改xml中的owner属性
    private void Save(string roleId, XmlNode xnode, TreeNode TN)
    {
        xmlpath = Server.MapPath("EditMenu.xml");
        bool flag = true;
        bool flag2 = false;
        string owner = xnode.Attributes["owner"].Value.ToString();
        string[] val = owner.Split(',');
        if (TN.Checked == true)
        {
            for (int i = 0; i < val.Length; i++)
            {
                if (roleId == val[i])
                {
                    flag = false;
                    break;
                }
            }
            if (flag == true)
            {
                string nowner = owner + roleId + ",";
                xnode.Attributes["owner"].Value = nowner;
                xmldoc.Save(xmlpath);
            }
        }
        else
        {
            for (int i = 0; i < val.Length; i++)
            {
                if (roleId == val[i])
                {
                    flag2 = true;
                    break;
                }
            }
            if (flag2 == true)
            {
                string del = roleId + ",";
                string nowner = Convert.ToString(owner.Replace(del, ""));
                xnode.Attributes["owner"].Value = nowner;
                xmldoc.Save(xmlpath);
            }
        }

    }

    protected void PermTreeView_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        TreeNodeCollection node = e.Node.ChildNodes;
        foreach (TreeNode item in node)
        {
            item.Checked = e.Node.Checked;
            CheckNode(item.ChildNodes, e.Node.Checked);
        }
        e.Node.ExpandAll();

    }

    public void CheckNode(TreeNodeCollection node, bool selected)
    {
        foreach (TreeNode item in node)
        {
            item.Checked = selected;
            CheckNode(item.ChildNodes, selected);
        }
    }

    //将XML上传至数据库
    protected void BtnUp_Click(object sender, EventArgs e)
    {
        bool isEmptyXml = bl.IsEmptyXmlMenu("Webmenu", out errMsg);
        bool ret = true;
        string message = "";
        //string DBtype = dr.GetDBtype();
        xmlpath = Server.MapPath("EditMenu.xml");
        string filename = xmlpath.Substring(xmlpath.LastIndexOf("\\") + 1);
        FileStream fs = new FileStream(xmlpath, FileMode.OpenOrCreate, FileAccess.Read);
        byte[] bytes = new byte[fs.Length];
        fs.Read(bytes, 0, bytes.Length);
        fs.Close();

        try
        {
            string connstr = bl.GetConnstr(out errMsg).ToString();
            //DB2Connection db2conn = new DB2Connection(connstr);
            //string sqlstr = "update XMLMENU set O_XML=@xmlfile where MENU='Webmenu'";
            //DB2Command db2cmd = new DB2Command(sqlstr, db2conn);
            //DB2Parameter db2para = new DB2Parameter("@xmlfile", DB2Type.Blob, bytes.Length, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, bytes);
            //db2cmd.Parameters.Add(db2para);
            //db2conn.Open();
            //db2cmd.ExecuteNonQuery();
            //db2conn.Close();
            OleDbConnection db2conn = new OleDbConnection(connstr);
            string sqlstr = "update T_SYS_MENU set B_XML=? where T_XMLID='Webmenu'";
            OleDbCommand db2cmd = new OleDbCommand(sqlstr, db2conn);
            OleDbParameter db2para = new OleDbParameter("?", OleDbType.Binary, bytes.Length);
            db2para.Value = bytes;
            db2cmd.Parameters.Add(db2para);
            db2conn.Open();
            db2cmd.ExecuteNonQuery();
            db2conn.Close();
        }
        catch (Exception ce)
        {
            errMsg = ce.Message;
            ret = false;
        }


        if (ret == true)
        {
            message = "上传成功！";
        }
        else
        {
            message = "上传失败！请检查数据库设置！";
        }
        Response.Write("<script>alert('" + message + "')</script>");
    }

    protected void DownLoadXml(object sender, EventArgs e)//下载XML
    {
        xmlpath = Server.MapPath("EditMenu.xml");
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
                string sqlstr = "select * from T_SYS_MENU where T_XMLID='Webmenu'";
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
}