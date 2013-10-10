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

public partial class Admin_ManageMenu : System.Web.UI.Page
{
    string xmlpath = "";
    string webpath = "~/MenuManage/ManageMenu.aspx";
    string FileName = "";
    string errMsg = "";
    string message = "";

    BLL.BLLRole bl = new BLL.BLLRole();
    XmlDocument xmldoc = new XmlDocument();

    object obj = null;
    int count = 0;

    TreeNode tn = new TreeNode();
    TreeNode tnp1 = new TreeNode();
    TreeNode tnp2 = new TreeNode();
    TreeNode tnp3 = new TreeNode();

    protected void Page_Load(object sender, EventArgs e)
    {
        string param = Request["param"];
        if (param != "")
        {
            if (param == "Add")
            {
                string id = Request.Form["id"];
                string name = Request.Form["name"];
                string src = Request.Form["src"];
                string vis = Request.Form["vis"];
                string dep = Request.Form["dep"];
                BtnAdd_Click(sender,e);
            }
        }
        if (!IsPostBack)
        {

        }
        if (!Page.IsPostBack)
        {
            xmlpath = Server.MapPath("EditMenu.xml");
            DownLoadXml(sender, e);//下载XML文件至指定路径
            TreeView1.Nodes.Clear();
            //Session.Contents["xmlpath"] = xmlpath;
            XmlDataSource xds = new XmlDataSource();
            xds.DataFile = xmlpath;
            XmlDocument xmlDocument = xds.GetXmlDocument();
            BindXmlToTreeView(xmlDocument.DocumentElement, TreeView1.Nodes);
            TreeView1.ExpandAll();
        }
    }

    //从数据库中下载最新菜单
    protected void BtnDld_Click(object sender, EventArgs e)
    {
        xmlpath = Server.MapPath("EditMenu.xml");
        DownLoadXml(sender, e);//下载XML文件至指定路径
        Response.Redirect(webpath);
    }

    //上传用户导入XML
    protected void BtnPutin_Click(object sender, EventArgs e)
    {
        errMsg = "";
        int count = 0;
        string info = "";
        string flPath = FileUpload1.PostedFile.FileName.ToString();
        FileStream fs = new FileStream(flPath, FileMode.Open, FileAccess.Read); //将图片以文件流的形式进行保存
        BinaryReader br = new BinaryReader(fs);

        byte[] imgBytesIn = br.ReadBytes((int)fs.Length);  //将流读入到字节数组中

        bool flag = bl.RetBoolUpFile(imgBytesIn, out errMsg);

        if (errMsg == "")
        {
            if (flag == true)
            {
                info = "菜单文件导入成功!";
                count = 1;
            }
            else
            {
                info = "菜单文件导入失败!";
            }
        }
        else
            info = errMsg;
        DownLoadXml(sender, e);//下载XML文件至指定路径
        Response.Redirect(webpath);
        
    }

    //添加菜单
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        xmlpath = Server.MapPath("EditMenu.xml");
        string name = this.txtName.Value;
        try
        {
            //if (this.nName.Value != "")//判断菜单名称是否为空
            if (name != "")//判断菜单名称是否为空
            {
                //tn = TreeView1.SelectedNode;
                xmldoc.Load(xmlpath);
                //ViewState["GetText"] = tn.Text;
                ////判断被选中的节点是第几层节点
                tn = (TreeNode)Session["SelectNd"];
                int c = tn.Depth;
                //int c = int.Parse(dep);

                string inVis;
                //string owner = ",管理员,";//权限默认是管理员
                string owner = ",";//权限默认是空
                if (this.txtVis.Checked == true)//判断是否显示
                {
                    inVis = "1";
                }
                else
                {
                    inVis = "0";
                }

                XmlElement nodeE = xmldoc.CreateElement("node");
                //设置需要添加的节点的标示和各项属性
                nodeE.SetAttribute("caption", this.txtName.Value);

                //nodeE.SetAttribute("caption", name);
                nodeE.SetAttribute("visible", inVis);
                //如果序号没有填写，则添加一个默认的序号“0”
                if (this.txtID.Value == "")
                {
                    nodeE.SetAttribute("order", "0");
                }
                else
                {
                    nodeE.SetAttribute("order", this.txtID.Value);
                }
                //if (id == "")
                //{
                //    nodeE.SetAttribute("order", "0");
                //}
                //else
                //{
                //    nodeE.SetAttribute("order", id);
                //}
                nodeE.SetAttribute("owner", owner);
                nodeE.SetAttribute("FileName", this.txtSrc.Value);
                //nodeE.SetAttribute("FileName", src);
                int x = 0;
                int y = 0;
                int z = 0;
                int u = 0;
                bool w = true;

                message = "菜单添加成功!";
                this.Lab1.Text = "";

                //为ROOT添加子节点
                if (c == 0)
                {
                    if (xmldoc.ChildNodes[1].HasChildNodes)//判断ROOT下是否存在子节点
                    {
                        for (x = 0; x < xmldoc.ChildNodes[1].ChildNodes.Count; x++)
                        {
                            if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].Attributes.GetNamedItem("caption").Value) == name)//判断同一父节点下的同级子节点是否有相同名称的节点
                            {
                                w = false;
                                break;
                            }
                        }
                        if (w == true)
                        {
                            if (name != "")
                            {
                                xmldoc.ChildNodes[1].AppendChild(nodeE);//添加节点
                                xmldoc.Save(xmlpath);//保存xml文件
                                //Response.Redirect(webpath);
                                //LoadXml(xmlpath);
                                //EmptyText(sender, e);
                                
                            }
                        }
                    }
                    else
                    {
                        if (name != "")
                        {
                            xmldoc.ChildNodes[1].AppendChild(nodeE);//添加节点
                            xmldoc.Save(xmlpath);
                            //Response.Redirect(webpath);
                            //LoadXml(xmlpath);
                            //EmptyText(sender, e);
                        }
                    }
                }

                //为ROOT/node添加子节点
                else if (c == 1)
                {
                    for (x = 0; x < xmldoc.ChildNodes[1].ChildNodes.Count; x++)
                    {
                        if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetText"])
                        {
                            if (xmldoc.ChildNodes[1].ChildNodes[x].HasChildNodes)
                            {
                                for (y = 0; y < xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes.Count; y++)
                                {
                                    if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].Attributes.GetNamedItem("caption").Value) == name)
                                    {
                                        w = false;
                                        break;
                                    }
                                }
                                if (w == true)
                                {
                                    if (name != "")
                                    {
                                        xmldoc.ChildNodes[1].ChildNodes[x].AppendChild(nodeE);
                                        xmldoc.Save(xmlpath);
                                        //Response.Redirect(webpath);
                                        //LoadXml(xmlpath);
                                        //EmptyText(sender, e);
                                    }
                                }
                            }
                            else
                            {
                                if (name != "")
                                {
                                    xmldoc.ChildNodes[1].ChildNodes[x].AppendChild(nodeE);
                                    xmldoc.Save(xmlpath);
                                    //Response.Redirect(webpath);
                                    //LoadXml(xmlpath);
                                    //EmptyText(sender, e);
                                }
                            }
                        }
                    }
                }

                //为ROOT/node/node添加子节点
                else if (c == 2)
                {
                    tnp1 = TreeView1.SelectedNode.Parent;

                    ViewState["GetTextp1"] = tnp1.Text;

                    for (x = 0; x < xmldoc.ChildNodes[1].ChildNodes.Count; x++)
                    {
                        if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetTextp1"])
                        {
                            for (y = 0; y < xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes.Count; y++)
                            {
                                if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetText"])
                                {
                                    if (xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].HasChildNodes)
                                    {
                                        for (z = 0; z < xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes.Count; z++)
                                        {
                                            if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].Attributes.GetNamedItem("caption").Value) == name)
                                            {
                                                w = false;
                                                break;
                                            }
                                        }
                                        if (w == true)
                                        {
                                            if (name != "")
                                            {
                                                xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].AppendChild(nodeE);
                                                xmldoc.Save(xmlpath);
                                                //Response.Redirect(webpath);
                                                //LoadXml(xmlpath);
                                                //EmptyText(sender, e);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (name != "")
                                        {
                                            xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].AppendChild(nodeE);
                                            xmldoc.Save(xmlpath);
                                            //Response.Redirect(webpath);
                                            //LoadXml(xmlpath);
                                            //EmptyText(sender, e);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //为ROOT/node/node/node添加子节点
                else if (c == 3)
                {
                    tnp1 = TreeView1.SelectedNode.Parent;
                    tnp2 = TreeView1.SelectedNode.Parent.Parent;

                    ViewState["GetTextp1"] = tnp1.Text;
                    ViewState["GetTextp2"] = tnp2.Text;

                    for (x = 0; x < xmldoc.ChildNodes[1].ChildNodes.Count; x++)
                    {
                        if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetTextp2"])
                        {
                            for (y = 0; y < xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes.Count; y++)
                            {
                                if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetTextp1"])
                                {
                                    for (z = 0; z < xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes.Count; z++)
                                    {
                                        if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetText"])
                                        {
                                            if (xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].HasChildNodes)
                                            {
                                                for (u = 0; u < xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes.Count; u++)
                                                {
                                                    if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes[u].Attributes.GetNamedItem("caption").Value) == name)
                                                    {
                                                        w = false;
                                                        break;
                                                    }
                                                }
                                                if (w == true)
                                                {
                                                    if (name != "")
                                                    {
                                                        xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].AppendChild(nodeE);
                                                        xmldoc.Save(xmlpath);
                                                        //Response.Redirect(webpath);
                                                        //LoadXml(xmlpath);
                                                        //EmptyText(sender, e);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (name != "")
                                                {
                                                    xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].AppendChild(nodeE);
                                                    xmldoc.Save(xmlpath);
                                                    //Response.Redirect(webpath);
                                                    //LoadXml(xmlpath);
                                                    //EmptyText(sender, e);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                else
                {
                    this.Lab1.Text = "本系统不允许在本级菜单下添加子菜单";
                }

                if (w == false)
                {
                    this.Lab1.Text = message;
                }
            }
            else
            {
                message = "请输入菜单名称!";
            }
        }

        catch (Exception)
        {
            throw;
        }
        BtnUp_Click(sender, e);
        BtnSort_Click(sender, e);
        Response.Redirect(webpath);
    }
    //编辑菜单
    protected void BtnUpd_Click(object sender, EventArgs e)
    {
        xmlpath = Server.MapPath("EditMenu.xml");
        try
        {
            if (this.txtName2.Value != "")
            {
                //tn = TreeView1.SelectedNode;

                xmldoc.Load(xmlpath);
                tn = (TreeNode)Session["SelectNd"];
                ViewState["GetText"] = tn.Text;
                int c = tn.Depth;

                string inVis;
                string owner = ",";//默认权限为空
                string order = "0";
                if (this.txtVis2.Checked == true)
                {
                    inVis = "1";
                }
                else
                {
                    inVis = "0";
                }
                if (this.txtID2.Value == "")
                {
                    order = "0";
                }
                else
                {
                    order = this.txtID2.Value;
                }
                XmlElement nodeE = xmldoc.CreateElement("node");
                int x = 0;
                int y = 0;
                int z = 0;
                int u = 0;
                int v = 0;
                int w = 0;
                string AlertAdd = "同一父菜单下的同级子菜单不允许有相同的名称";
                this.Lab1.Text = "";

                if (c == 0)
                {
                    xmldoc.ChildNodes[1].Attributes["caption"].Value = this.txtName2.Value;
                    xmldoc.ChildNodes[1].Attributes["visible"].Value = inVis;
                    xmldoc.ChildNodes[1].Attributes["order"].Value = order;
                    //xmldoc.ChildNodes[1].Attributes["owner"].Value = owner;
                    xmldoc.ChildNodes[1].Attributes["FileName"].Value = this.txtSrc2.Value;
                    xmldoc.Save(xmlpath);
                    //Response.Redirect(webpath);
                    //LoadXml(xmlpath);
                    //EmptyText(sender, e);
                }

                else if (c == 1)
                {
                    for (x = 0; x < xmldoc.ChildNodes[1].ChildNodes.Count; x++)
                    {
                        if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetText"])
                        {
                            for (v = 0; v < xmldoc.ChildNodes[1].ChildNodes.Count; v++)
                            {
                                //判断同一父节点下的同级子节点中是否有与需要编辑的节点名称相同的，有则w+1，如果只有一个，那么就是编辑后的节点没有改变名称，如果大于一个，说明存在与需要编辑的节点名称相同的节点，则不允许修改
                                if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[v].Attributes.GetNamedItem("caption").Value) == this.txtName2.Value)
                                {
                                    w = w + 1;
                                }
                            }
                            if (w <= 1)
                            {
                                xmldoc.ChildNodes[1].ChildNodes[x].Attributes["caption"].Value = this.txtName2.Value;
                                xmldoc.ChildNodes[1].ChildNodes[x].Attributes["visible"].Value = inVis;
                                xmldoc.ChildNodes[1].ChildNodes[x].Attributes["order"].Value = order;
                                //xmldoc.ChildNodes[1].ChildNodes[x].Attributes["owner"].Value = owner;
                                xmldoc.ChildNodes[1].ChildNodes[x].Attributes["FileName"].Value = this.txtSrc2.Value;
                                xmldoc.Save(xmlpath);
                                //Response.Redirect(webpath);
                                //LoadXml(xmlpath);
                                //EmptyText(sender, e);
                            }
                        }
                    }
                }

                else if (c == 2)
                {
                    tnp1 = tn.Parent;

                    ViewState["GetTextp1"] = tnp1.Text;

                    for (x = 0; x < xmldoc.ChildNodes[1].ChildNodes.Count; x++)
                    {
                        if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetTextp1"])
                        {
                            for (y = 0; y < xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes.Count; y++)
                            {
                                if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetText"])
                                {
                                    for (v = 0; v < xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes.Count; v++)
                                    {
                                        if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[v].Attributes.GetNamedItem("caption").Value) == this.txtName2.Value)
                                        {
                                            w = w + 1;
                                        }
                                    }
                                    if (w <= 1)
                                    {
                                        xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].Attributes["caption"].Value = this.txtName2.Value;
                                        xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].Attributes["visible"].Value = inVis;
                                        xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].Attributes["order"].Value = order;
                                        //xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].Attributes["owner"].Value = owner;
                                        xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].Attributes["FileName"].Value = this.txtSrc2.Value;
                                        xmldoc.Save(xmlpath);
                                        //Response.Redirect(webpath);
                                        //LoadXml(xmlpath);
                                        //EmptyText(sender, e);
                                    }
                                }
                            }
                        }
                    }
                }

                else if (c == 3)
                {
                    tnp1 = tn.Parent;
                    tnp2 = tn.Parent.Parent;

                    ViewState["GetTextp1"] = tnp1.Text;
                    ViewState["GetTextp2"] = tnp2.Text;

                    for (x = 0; x < xmldoc.ChildNodes[1].ChildNodes.Count; x++)
                    {
                        if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetTextp2"])
                        {
                            for (y = 0; y < xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes.Count; y++)
                            {
                                if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetTextp1"])
                                {
                                    for (z = 0; z < xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes.Count; z++)
                                    {
                                        if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetText"])
                                        {
                                            for (v = 0; v < xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes.Count; v++)
                                            {
                                                if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[v].Attributes.GetNamedItem("caption").Value) == this.txtName2.Value)
                                                {
                                                    w = w + 1;
                                                }
                                            }
                                            if (w <= 1)
                                            {
                                                xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].Attributes["caption"].Value = this.txtName2.Value;
                                                xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].Attributes["visible"].Value = inVis;
                                                xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].Attributes["order"].Value = order;
                                                //xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].Attributes["owner"].Value = owner;
                                                xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].Attributes["FileName"].Value = this.txtSrc2.Value;
                                                xmldoc.Save(xmlpath);
                                                //Response.Redirect(webpath);
                                                //LoadXml(xmlpath);
                                                //EmptyText(sender, e);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


                else if (c == 4)
                {
                    tnp1 = tn.Parent;
                    tnp2 = tn.Parent.Parent;
                    tnp3 = tn.Parent.Parent.Parent;

                    ViewState["GetTextp1"] = tnp1.Text;
                    ViewState["GetTextp2"] = tnp2.Text;
                    ViewState["GetTextp3"] = tnp3.Text;

                    for (x = 0; x < xmldoc.ChildNodes[1].ChildNodes.Count; x++)
                    {
                        if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetTextp3"])
                        {
                            for (y = 0; y < xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes.Count; y++)
                            {
                                if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetTextp2"])
                                {
                                    for (z = 0; z < xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes.Count; z++)
                                    {
                                        if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetTextp1"])
                                        {
                                            for (u = 0; u < xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes.Count; u++)
                                            {
                                                if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes[u].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetText"])
                                                {
                                                    for (v = 0; v < xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes.Count; v++)
                                                    {
                                                        if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes[v].Attributes.GetNamedItem("caption").Value) == this.txtName2.Value)
                                                        {
                                                            w = w + 1;
                                                        }
                                                    }
                                                    if (w <= 1)
                                                    {
                                                        xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes[u].Attributes["caption"].Value = this.txtName2.Value;
                                                        xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes[u].Attributes["visible"].Value = inVis;
                                                        xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes[u].Attributes["order"].Value = order;
                                                        //xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes[u].Attributes["owner"].Value = owner;
                                                        xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes[u].Attributes["FileName"].Value = this.txtSrc2.Value;
                                                        xmldoc.Save(xmlpath);
                                                        //Response.Redirect(webpath);
                                                        //LoadXml(xmlpath);
                                                        //EmptyText(sender, e);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }

                if (w > 1)
                {
                    this.Lab1.Text = AlertAdd;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
        BtnUp_Click(sender, e);
        BtnSort_Click(sender, e);
        Response.Redirect(webpath);
    }
    //删除菜单
    protected void BtnDel_Click(object sender, EventArgs e)
    {
        xmlpath = Server.MapPath("EditMenu.xml");
        try
        {
            if (this.nName.Value != "")
            {
                //tn = TreeView1.SelectedNode;
                tn = (TreeNode)Session["SelectNd"];
                ViewState["GetText"] = tn.Text;

                xmldoc.Load(xmlpath);
                int c = tn.Depth;

                XmlElement nodeE = xmldoc.CreateElement("node");
                string AlertAdd2 = "此菜单存在子菜单，禁止删除";
                this.Lab1.Text = "";
                int x = 0;
                int y = 0;
                int z = 0;
                int u = 0;
                bool w = true;

                if (c == 0)
                {
                    this.Lab1.Text = "根节点不允许删除";
                }

                else if (c == 1)
                {
                    for (x = 0; x < xmldoc.ChildNodes[1].ChildNodes.Count; x++)
                    {
                        if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetText"])
                        {
                            if (xmldoc.ChildNodes[1].ChildNodes[x].HasChildNodes)//如果需要删除的节点存在子节点则不允许删除
                            {
                                w = false;
                                break;
                            }
                            else
                            {
                                xmldoc.ChildNodes[1].RemoveChild(xmldoc.ChildNodes[1].ChildNodes[x]);
                                xmldoc.Save(xmlpath);
                                //Response.Redirect(webpath);
                                //LoadXml(xmlpath);
                                //EmptyText(sender, e);
                            }
                        }
                    }
                }

                else if (c == 2)
                {
                    tnp1 = tn.Parent;

                    ViewState["GetTextp1"] = tnp1.Text;

                    for (x = 0; x < xmldoc.ChildNodes[1].ChildNodes.Count; x++)
                    {
                        if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetTextp1"])
                        {
                            for (y = 0; y < xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes.Count; y++)
                            {
                                if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetText"])
                                {
                                    if (xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].HasChildNodes)
                                    {
                                        w = false;
                                        break;
                                    }
                                    else
                                    {
                                        xmldoc.ChildNodes[1].ChildNodes[x].RemoveChild(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y]);
                                        xmldoc.Save(xmlpath);
                                        //Response.Redirect(webpath);
                                        //LoadXml(xmlpath);
                                        //EmptyText(sender, e);
                                    }
                                }
                            }
                        }
                    }
                }

                else if (c == 3)
                {
                    tnp1 = tn.Parent;
                    tnp2 = tn.Parent.Parent;

                    ViewState["GetTextp1"] = tnp1.Text;
                    ViewState["GetTextp2"] = tnp2.Text;

                    for (x = 0; x < xmldoc.ChildNodes[1].ChildNodes.Count; x++)
                    {
                        if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetTextp2"])
                        {
                            for (y = 0; y < xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes.Count; y++)
                            {
                                if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetTextp1"])
                                {
                                    for (z = 0; z < xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes.Count; z++)
                                    {
                                        if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetText"])
                                        {
                                            if (xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].HasChildNodes)
                                            {
                                                w = false;
                                                break;
                                            }
                                            else
                                            {
                                                xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].RemoveChild(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z]);
                                                xmldoc.Save(xmlpath);
                                                //Response.Redirect(webpath);
                                                //LoadXml(xmlpath);
                                                //EmptyText(sender, e);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                else if (c == 4)
                {
                    tnp1 = tn.Parent;
                    tnp2 = tn.Parent.Parent;
                    tnp3 = tn.Parent.Parent.Parent;

                    ViewState["GetTextp1"] = tnp1.Text;
                    ViewState["GetTextp2"] = tnp2.Text;
                    ViewState["GetTextp3"] = tnp3.Text;

                    for (x = 0; x < xmldoc.ChildNodes[1].ChildNodes.Count; x++)
                    {
                        if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetTextp3"])
                        {
                            for (y = 0; y < xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes.Count; y++)
                            {
                                if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetTextp2"])
                                {
                                    for (z = 0; z < xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes.Count; z++)
                                    {
                                        if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetTextp1"])
                                        {
                                            for (u = 0; u < xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes.Count; u++)
                                            {
                                                if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes[u].Attributes.GetNamedItem("caption").Value) == (string)ViewState["GetText"])
                                                {
                                                    if (xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes[u].HasChildNodes)
                                                    {
                                                        w = false;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].RemoveChild(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes[u]);
                                                        xmldoc.Save(xmlpath);
                                                        //Response.Redirect(webpath);
                                                        //LoadXml(xmlpath);
                                                        //EmptyText(sender, e);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (w == false)
                {
                    this.Lab1.Text = AlertAdd2;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
        BtnUp_Click(sender, e);
        BtnSort_Click(sender, e);
        Response.Redirect(webpath);
    }
    //菜单排序
    protected void BtnSort_Click(object sender, EventArgs e)
    {
        xmlpath = Server.MapPath("EditMenu.xml");
        try
        {
            xmldoc.Load(xmlpath);
            int i = 0;
            int j = 0;
            int k = 0;
            int l = 0;
            int m = 0;

            for (i = 0; i < xmldoc.ChildNodes[1].ChildNodes.Count; i++)
            {
                if (xmldoc.ChildNodes[1].ChildNodes[i].HasChildNodes)
                {
                    for (j = 0; j < xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes.Count; j++)
                    {
                        if (xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes[j].HasChildNodes)
                        {
                            for (k = 0; k < xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes[j].ChildNodes.Count; k++)
                            {
                                if (xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes[j].ChildNodes[k].HasChildNodes)
                                {
                                    for (l = 0; l < xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Count; l++)
                                    {
                                        for (m = 0; m < xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Count - 1; m++)
                                        {
                                            if (Convert.ToDouble(xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[m].Attributes.GetNamedItem("order").Value) > Convert.ToDouble(xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[m + 1].Attributes.GetNamedItem("order").Value))
                                            {
                                                xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes[j].ChildNodes[k].InsertBefore(xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[m + 1], xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[m]);
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }

            for (i = 0; i < xmldoc.ChildNodes[1].ChildNodes.Count; i++)
            {
                if (xmldoc.ChildNodes[1].ChildNodes[i].HasChildNodes)
                {
                    for (j = 0; j < xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes.Count; j++)
                    {
                        if (xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes[j].HasChildNodes)
                        {
                            for (k = 0; k < xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes[j].ChildNodes.Count; k++)
                            {
                                for (l = 0; l < xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes[j].ChildNodes.Count - 1; l++)
                                {
                                    if (Convert.ToDouble(xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes[j].ChildNodes[l].Attributes.GetNamedItem("order").Value) > Convert.ToDouble(xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes[j].ChildNodes[l + 1].Attributes.GetNamedItem("order").Value))
                                    {
                                        xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes[j].InsertBefore(xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes[j].ChildNodes[l + 1], xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes[j].ChildNodes[l]);
                                    }

                                }
                            }
                        }
                    }
                }
            }

            for (i = 0; i < xmldoc.ChildNodes[1].ChildNodes.Count; i++)
            {
                if (xmldoc.ChildNodes[1].ChildNodes[i].HasChildNodes)
                {
                    for (j = 0; j < xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes.Count; j++)
                    {
                        for (k = 0; k < xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes.Count - 1; k++)
                        {
                            if (Convert.ToDouble(xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes[k].Attributes.GetNamedItem("order").Value) > Convert.ToDouble(xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes[k + 1].Attributes.GetNamedItem("order").Value))
                            {
                                xmldoc.ChildNodes[1].ChildNodes[i].InsertBefore(xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes[k + 1], xmldoc.ChildNodes[1].ChildNodes[i].ChildNodes[k]);
                            }

                        }
                    }
                }
            }

            for (i = 0; i < xmldoc.ChildNodes[1].ChildNodes.Count; i++)
            {
                for (j = 0; j < xmldoc.ChildNodes[1].ChildNodes.Count - 1; j++)
                {
                    if (Convert.ToDouble(xmldoc.ChildNodes[1].ChildNodes[j].Attributes.GetNamedItem("order").Value) > Convert.ToDouble(xmldoc.ChildNodes[1].ChildNodes[j + 1].Attributes.GetNamedItem("order").Value))
                    {
                        xmldoc.ChildNodes[1].InsertBefore(xmldoc.ChildNodes[1].ChildNodes[j + 1], xmldoc.ChildNodes[1].ChildNodes[j]);
                    }
                }
            }

            xmldoc.Save(xmlpath);
            //Response.Redirect(webpath);
            //LoadXml(xmlpath);
            //EmptyText(sender, e);
        }
        catch (Exception)
        {
            throw;
        }
    }
    //显示选中内容
    protected void TreeView1_SelectedNodeChanged1(object sender, EventArgs e)
    {
        tn = TreeView1.SelectedNode;
        Session["SelectNd"] = tn;
        ViewState["GetText"] = tn.Text;
        ViewState["GetNavigateUrl"] = tn.Target.ToString();
        ViewState["GetVis"] = tn.ImageUrl.ToString();
        ViewState["GetOrder"] = tn.ToolTip.ToString();
        //ViewState["NodePath"] = tn.Depth;
        int d = tn.Depth;
        this.txtDepth.Value = d.ToString();
        this.nName.Value = (string)ViewState["GetText"];
        this.selecteUrl.Value = (string)ViewState["GetNavigateUrl"];
        this.selectedvalue.Value = (string)ViewState["GetOrder"];
        //this.selectedvalue.Value = (string)ViewState["NodePath"];
        string vis = (string)ViewState["GetVis"];
        //if (vis == "~/img/visible1.gif")
        //{
        //    this.nVis.Checked = true;
        //}
        //else if (vis == "~/img/visible0.gif")
        //{
        //    this.nVis.Checked = false;
        //}
        this.nVis.Checked = true;//菜单默认为显示

    }
    //点击加载按钮，先上传本地文件至至服务器，最后加载服务器中的文件
    protected void BtnLoad_Click(object sender, EventArgs e)
    {
        ////Server.MapPath返回与Web服务器上指定虚拟路径相对应的绝对路径,Path.GetFileName返回指定路径字符串的文件名和扩展名
        //string xmlpathS = Server.MapPath( Path.GetFileName(FileUpload1.PostedFile.FileName.ToString()));
        //string xmlName = Path.GetFileName(FileUpload1.PostedFile.FileName.ToString());//用于保存到数据库中的上传文件URL路径
        //int lN = xmlName.LastIndexOf("."); //取得文件扩展名
        //if (lN > 0)
        //{
        //    string newext = xmlName.Substring(lN).ToLower();//将文件扩展名转换为小写
        //    if (newext != ".xml")
        //    {
        //        Response.Write("对不起,文件类型不符,不能上传。上传文件扩展必须为(.xml) ");
        //        return;
        //    }
        //    else
        //    {
        //        FileUpload1.PostedFile.SaveAs(xmlpathS);
        //    }
        //}
        string xmlPathS = "";
        LoadXml(xmlPathS);
    }
    //将XML上传至数据库
    protected void BtnUp_Click(object sender, EventArgs e)
    {
        xmlpath = Server.MapPath("EditMenu.xml");
        bool isEmptyXml = bl.IsEmptyXmlMenu("Webmenu",out errMsg);
        bool ret = true;
        string message = "";
        string DBtype = bl.GetDBtype();
        //xmlpath = Session.Contents["xmlpath"].ToString();
        string filename = xmlpath.Substring(xmlpath.LastIndexOf("\\") + 1);
        FileStream fs = new FileStream(xmlpath, FileMode.OpenOrCreate, FileAccess.Read);
        byte[] bytes = new byte[fs.Length];
        fs.Read(bytes, 0, bytes.Length);
        fs.Close();
        if (DBtype == "SQL")//上传至SQLSERVER数据库
        {
            //try
            //{
            //    SqlConnection sqlconn = SAC.sqlHelper.DBsql.GetConnection();

            //    string sqlstr = "update T_SYS_MENU set B_XML=@xmlfile where T_XMLID='Webmenu'";
            //    SqlCommand sqlcmd = new SqlCommand(sqlstr, sqlconn);
            //    SqlParameter db2para = new SqlParameter("@xmlfile", SqlDbType.VarBinary, bytes.Length);
            //    db2para.Value = bytes;
            //    sqlcmd.Parameters.Add(db2para);
            //    sqlcmd.ExecuteNonQuery();
            //    SAC.sqlHelper.DBsql.CloseConnection(sqlconn);
            //}
            //catch (Exception ce)
            //{
            //    errMsg = ce.Message;
            //    ret = false;
            //}
        }

        else//上传至DB2数据库
        {
            try
            {
                //string connstr = bl.RetDB2conn().ToString();
                string connstr = bl.GetConnstr(out errMsg).ToString();

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
        }
    }
    //加载XML文件
    protected void LoadXml(string xmlPathS)
    {
        TreeView1.Nodes.Clear();
        //xmlpath = FileUpload1.PostedFile.FileName.ToString();
        xmlpath = xmlPathS;
        Session.Contents["xmlpath"] = xmlpath;
        XmlDataSource xds = new XmlDataSource();
        xds.DataFile = xmlpath;
        XmlDocument xmlDocument = xds.GetXmlDocument();
        BindXmlToTreeView(xmlDocument.DocumentElement, TreeView1.Nodes);
        TreeView1.ExpandAll();
    }
    //逐级生成树形菜单
    private void BindXmlToTreeView(XmlNode node, TreeNodeCollection tnc)
    {
        //获得节点字段值
        string strText = node.Attributes["caption"].Value;//名字
        string FileName = node.Attributes["FileName"].Value;//菜单链接
        string navurl = "";//这个只要为空，否则点击会弹出链接的窗口
        string visible = "";
        //if (node.Attributes["visible"].Value == "0")
        //{
        //    visible = "~/img/tree_folder.gif";
        //}
        //else if (node.Attributes["visible"].Value == "1")
        //{
        //    visible = "~/img/tree_folder.gif";
        //}
        if (node.ChildNodes.Count == 0)
        {
            visible = "~/img/tree_file.jpg";
        }
        else
        {
            visible = "~/img/tree_folder.jpg";
        }

        string order = node.Attributes["order"].Value;//序号
        //tnc.Add(new TreeNode(strText,FileName,visible,navurl,order));

        TreeNode Tnode = new TreeNode();//创建新节点        
        Tnode.Text = strText;//设置节点的属性 
        Tnode.ImageUrl = visible;
        Tnode.Target = FileName;
        Tnode.ToolTip = order;
        tnc.Add(Tnode);
        foreach (XmlNode n in node.ChildNodes)
        {
            //指向子节点和父节点的子节点群
            BindXmlToTreeView(n, tnc[tnc.Count - 1].ChildNodes);
        }

    }
    //将所有文本框清空
    protected void EmptyText(object sender, EventArgs e)
    {
        this.selectedvalue.Value = null;
        this.nName.Value = null;
        this.selecteUrl.Value = null;
        this.nVis.Checked = false;
    }
    //将数据库中的XML文件下载到服务器的指定路径
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
