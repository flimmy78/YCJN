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
using System.Xml;
using System.Xml.XPath;
using System.Text;

public partial class Admin_LeftMenuTree : System.Web.UI.Page
{
    string xmlpath = "";
    BLL.BLLRole bl = new BLL.BLLRole();
    XmlDocument xmldoc = new XmlDocument();
    StringBuilder sb = new StringBuilder();
    string errMsg = "";
    string strApp = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["rootMenuItem"] != null)
        {
            string temp = Request.QueryString["rootMenuItem"];
            string rootMenuItem = Server.UrlDecode(temp).ToString();
            strApp = CreatMenuTree(rootMenuItem);
            this.show.InnerHtml = strApp;
        }
    }

    //自动生成菜单树
    protected string CreatMenuTree(string rootMenuItem)
    {
        xmlpath = Server.MapPath("Xml/WebMenu.xml");
        string idkey;
        string UserName;
        string[] role;
        string[] owner;
        string visible;
        if (Request.Cookies["ID_KEY"] != null)
        {
            idkey = Request.Cookies["ID_KEY"].Value.ToString();
            UserName = bl.GetUserNameById(idkey, out errMsg);
            ArrayList list = bl.GetRolesByUserName(UserName, out errMsg);//得到角色名称
            role = (string[])list[0];
        }
        else
        {
            role = null;
        }

        xmldoc.Load(xmlpath);
        for (int x = 0; x < xmldoc.ChildNodes[1].ChildNodes.Count; x++)//共有x个一级目录
        {
            if (Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].Attributes.GetNamedItem("caption").Value) == rootMenuItem)//找到根菜单中与从RootMenu页面传过来的菜单项相同的菜单项
            {
                if (xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes.Count != 0)//如果一级目录存在子节点
                {
                    for (int y = 0; y < xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes.Count; y++)//共有y个第二级目录
                    {
                        visible = xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].Attributes["visible"].Value.ToString();//获取第y个二级目录的可见属性
                        owner = xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].Attributes["owner"].Value.ToString().TrimStart(',').TrimEnd(',').Split(',');//获取第y个二级目录的角色所有者

                        if (IfRepeat(role, owner) == true)
                        {
                            if (visible == "1")
                            {
                                sb.Append("<div class=\"pnav-box\" id=\"letter-a\">");
                                sb.Append("<div class=\"box-title\">");
                                if (xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes.Count != 0)//如果第二级目录存在子节点
                                {
                                    sb.Append("<a class=\"btn-fold\" href=\"\"></a><a class=\"btn-unfold hidden\" href=\"\"></a>");
                                    sb.AppendFormat("<a class=\"pnav-letter\">{0}</a>", Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].Attributes.GetNamedItem("caption").Value));
                                    sb.Append("</div>");
                                    sb.Append("<ul class=\"box-list hidden\">");
                                    for (int z = 0; z < xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes.Count; z++)//共有z个第三级目录
                                    {
                                        visible = xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].Attributes["visible"].Value.ToString();//获取第z个三级目录的可见属性
                                        owner = xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].Attributes["owner"].Value.ToString().TrimStart(',').TrimEnd(',').Split(',');//获取第z个三级目录的角色所有者

                                        if (IfRepeat(role, owner) == true)
                                        {
                                            if (visible == "1")
                                            {
                                                sb.Append("<ul>");
                                                if (xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes.Count != 0)//如果三级目录存在子节点
                                                {
                                                    sb.Append("<a class=\"btn-fold\" href=\"\"></a><a class=\"btn-unfold hidden\" href=\"\"></a>");
                                                    sb.AppendFormat("<b><a>{0}</a></b>", Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].Attributes.GetNamedItem("caption").Value));
                                                    for (int u = 0; u < xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes.Count; u++)//共有u个四级目录，四级目录不存在子节点，点击直接跳至该目录FileName属性所存储的文件链接
                                                    {
                                                        visible = xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes[u].Attributes["visible"].Value.ToString();//获取第u个四级目录的可见属性
                                                        owner = xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes[u].Attributes["owner"].Value.ToString().TrimStart(',').TrimEnd(',').Split(',');//获取第u个四级目录的角色所有者

                                                        if (IfRepeat(role, owner) == true)
                                                        {
                                                            if (visible == "1")
                                                            {

                                                                sb.Append("<li>");
                                                                if (xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes[u].ChildNodes.Count != 0)//如果四级目录存在子节点
                                                                {
                                                                    sb.Append("<a class=\"btn-fold\" href=\"\"></a><a class=\"btn-unfold hidden\" href=\"\"></a>");
                                                                    sb.AppendFormat("<b><a href=\"javascript:gotoFile('{1}')\">{0}</a></b>", Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes[u].Attributes.GetNamedItem("caption").Value), Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes[u].Attributes.GetNamedItem("FileName").Value));
                                                                    for (int p = 0; p < xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes[u].ChildNodes.Count; p++)//共有p个四级目录，五级目录不存在子节点，点击直接跳至该目录FileName属性所存储的文件链接
                                                                    {
                                                                        visible = xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes[u].ChildNodes[p].Attributes["visible"].Value.ToString();//获取第p个五级目录的可见属性
                                                                        owner = xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes[u].ChildNodes[p].Attributes["owner"].Value.ToString().TrimStart(',').TrimEnd(',').Split(',');//获取第p个五级目录的角色所有者

                                                                        if (IfRepeat(role, owner) == true)
                                                                        {
                                                                            if (visible == "1")
                                                                            {
                                                                                sb.AppendFormat("<h2 class=\"hidden\"><a href=\"javascript:gotoFile('{0}')\">{1}</a></h2>", Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes[u].ChildNodes[p].Attributes.GetNamedItem("FileName").Value), Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes[u].ChildNodes[p].Attributes.GetNamedItem("caption").Value));
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                else//如果不存在自节点，点击直接跳转至该目录FileName属性所存储的文件链接
                                                                {
                                                                    sb.AppendFormat("<b><a href=\"javascript:gotoFile('{0}')\">{1}</a></b>", Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes[u].Attributes.GetNamedItem("FileName").Value), Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes[u].Attributes.GetNamedItem("caption").Value));
                                                                }
                                                                sb.Append("</li>");
                                                                //sb.AppendFormat("<h2 class=\"hidden\"><a href=\"javascript:gotoFile('{0}')\">{1}</a></h2>", Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes[u].Attributes.GetNamedItem("FileName").Value), Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].ChildNodes[u].Attributes.GetNamedItem("caption").Value));
                                                            }
                                                        }
                                                    }
                                                }
                                                else//如果不存在自节点，点击直接跳转至该目录FileName属性所存储的文件链接
                                                {
                                                    sb.AppendFormat("<b><a href=\"javascript:gotoFile('{0}')\">{1}</a></b>", Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].Attributes.GetNamedItem("FileName").Value), Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].ChildNodes[z].Attributes.GetNamedItem("caption").Value));
                                                }
                                                sb.Append("</ul>");
                                            }
                                        }
                                    }
                                    sb.Append("</ul>");
                                }
                                else//如果不存在自节点，点击直接跳转至该目录FileName属性所存储的文件链接
                                {
                                    sb.AppendFormat("<a class=\"pnav-letter\" href=\"javascript:gotoFile('{0}')\">{1}</a>", Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].Attributes.GetNamedItem("FileName").Value), Convert.ToString(xmldoc.ChildNodes[1].ChildNodes[x].ChildNodes[y].Attributes.GetNamedItem("caption").Value));
                                    sb.Append("</div>");
                                }
                                sb.Append("</div>");
                            }
                        }
                    }
                }
                else
                {
                    sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td height=\"1\" style=\"background-color:#2a88bb\"></td></tr></table>");
                }
            }
        }

        return sb.ToString();
    }

    //判断两个数组中是否有重复的元素，如果有则返回true
    protected bool IfRepeat(string[] A, string[] B)
    {
        bool repeat = false;
        int na = A.Count();
        int nb = B.Count();
        int mix = 0;
        for (int i = 0; i < nb; i++)
        {
            for (int j = 0; j < na; j++)
            {
                if (A[j] == B[i])
                {
                    mix = mix + 1;
                }
            }
        }
        if (mix > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
