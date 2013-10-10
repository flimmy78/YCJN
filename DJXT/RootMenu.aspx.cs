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

public partial class RootMenu : System.Web.UI.Page
{
    string xmlpath = "";
    //string webpath = "~/Admin/ManageMenu.aspx";
    string FileName = "";
    string errMsg = "";
    string strApp = "";
    BLL.BLLRole bl = new BLL.BLLRole();
    XmlDocument xmldoc = new XmlDocument();
    StringBuilder sb = new StringBuilder();

    TreeNode tn = new TreeNode();
    TreeNode tnp1 = new TreeNode();
    TreeNode tnp2 = new TreeNode();
    TreeNode tnp3 = new TreeNode();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            /*正式上线注释去掉*/
            //DownLoadXml(sender, e);
            strApp = CreateMenu_1(sender, e);
            this.show.InnerHtml = strApp;
            //if (Session["ID_KEY"] != null)
            //{
            //    String idkey = Session["ID_KEY"].ToString();
            //}
            //GreateRootMenu(sender, e);
            string userId = Request.Cookies["ID_KEY"].Value.ToString();
            string realName = bl.GetUserRealNameById(userId,out errMsg);
            this.lblUserWelcome.Text = realName;
        }
    }

    //下载存储菜单的XML文件
    protected void DownLoadXml(object sender, EventArgs e)//下载XML
    {
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
                xmlpath = Server.MapPath("Xml/WebMenu.xml");
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

    //读取第一级菜单，生成按钮
    protected string CreateMenu_1(object sender, EventArgs e)//读取第一级菜单，生成按钮
    {
        xmlpath = Server.MapPath("Xml/WebMenu.xml");
        string idkey;
        string[] role;
        string UserName;
        //Application.Lock();
        //idkey = Application["ID_KEY"].ToString();
        //Application.UnLock();
        //if (Session["ID_KEY"] != null)
        if (Request.Cookies["ID_KEY"] != null)
        {
            idkey = Request.Cookies["ID_KEY"].Value.ToString();
            //UserName = Request.Cookies["UserName"].Value.ToString();
            UserName = bl.GetUserNameById(idkey, out errMsg);
            ArrayList list = bl.GetRolesByUserName(UserName, out errMsg);//得到角色名称
            role = (string[])list[0];
        }
        else
        {
            role = null;
        }

        //role = new  string[]{"1"};//测试
        xmldoc.Load(xmlpath);
        int c = xmldoc.ChildNodes[1].ChildNodes.Count;//获取首页下面第一级菜单的个数
        string[] caption = new string[c + 1];
        string[] visible = new string[c + 1];
        string[][] owner = new string[c + 1][];
        string[] FileName = new string[c + 1];
        caption[0] = xmldoc.ChildNodes[1].Attributes["caption"].Value.ToString();
        visible[0] = xmldoc.ChildNodes[1].Attributes["visible"].Value.ToString();
        owner[0] = xmldoc.ChildNodes[1].Attributes["owner"].Value.ToString().TrimStart(',').TrimEnd(',').Split(',');
        FileName[0] = xmldoc.ChildNodes[1].Attributes["FileName"].Value.ToString();
        sb.Append("<td align=\"left\" valign=\"middle\">&nbsp;&nbsp;");
        if (visible[0] == "1")
        {

            if (IfRepeat(role, owner[0]) == true)
            {
                sb.AppendFormat("<a class=\"Text1\" href=\"javascript:gotoFile('{0}');\" onclick=\"SetColor(this)\">{1}</a>&nbsp;", FileName[0], caption[0]);
            }

            for (int i = 1; i <= c; i++)
            {
                caption[i] = xmldoc.ChildNodes[1].ChildNodes[i - 1].Attributes["caption"].Value.ToString();
                visible[i] = xmldoc.ChildNodes[1].ChildNodes[i - 1].Attributes["visible"].Value.ToString();
                owner[i] = xmldoc.ChildNodes[1].ChildNodes[i - 1].Attributes["owner"].Value.ToString().TrimStart(',').TrimEnd(',').Split(',');
                FileName[i] = xmldoc.ChildNodes[1].ChildNodes[i - 1].Attributes["FileName"].Value.ToString();

                if (IfRepeat(role, owner[i]) == true)
                {
                    if (visible[i] == "1")
                    {
                        if (xmldoc.ChildNodes[1].ChildNodes[i - 1].ChildNodes.Count != 0)//如果该节点存在子节点，则生成菜单树
                        {
                            sb.AppendFormat("<a class=\"Text1\">|</a>&nbsp;<a runat=\"server\" runat=\"server\" Class=\"Text1\" onclick=\"SetColor(this)\"  href=\"javascript:gotoTree('{0}','{1}')\">{1}</a>&nbsp;", FileName[i], caption[i]);
                        }
                        else//如果该节点不在子节点，则点击直接跳转至该节点FileName属性所存储的文件链接
                        {
                            sb.AppendFormat("<a class=\"Text1\">|</a>&nbsp;<a runat=\"server\" runat=\"server\" Class=\"Text1\" onclick=\"SetColor(this)\" href=\"javascript:gotoFile('{0}');javascript:gotoTree('{0}','{1}')\">{1}</a>&nbsp;", FileName[i], caption[i]);
                        }
                    }
                }

            }
        }
        else if (visible[0] == "0")
        {
            sb.Append("<a class=\"Text1\">您缺少相关权限</a>");
        }
        sb.Append("</td>");

        return sb.ToString();
    }

    //注销账户页面
    protected void linkBtnLogout_Click(object sender, EventArgs e)
    {
        //Request.Cookies["ID_KEY"] = null;
        //Request.Cookies["UserName"] = null;
        Request.Cookies.Clear();
        Response.Write("<script type='text/javascript'>top.window.location.href='Login.aspx'; </script>");//整个主页面跳转到登录界面
        //Response.Write("<script type='text/javascript'>window.parent.lefterIframe.location.reload(true);</script>");//刷新框架中的lefterIframe页面
    }

    //得到登陆后默认页面的url
    public string GetHomePageUrl()
    {
        xmlpath = Server.MapPath("Xml/WebMenu.xml");
        string idkey;
        string[] role;
        string UserName;
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
        int c = xmldoc.ChildNodes[1].ChildNodes.Count;//获取首页下面第一级菜单的个数
        string[] caption = new string[c + 1];
        string[] visible = new string[c + 1];
        string[][] owner = new string[c + 1][];
        string[] FileName = new string[c + 1];
        caption[0] = xmldoc.ChildNodes[1].Attributes["caption"].Value.ToString();
        visible[0] = xmldoc.ChildNodes[1].Attributes["visible"].Value.ToString();
        owner[0] = xmldoc.ChildNodes[1].Attributes["owner"].Value.ToString().TrimStart(',').TrimEnd(',').Split(',');
        FileName[0] = xmldoc.ChildNodes[1].Attributes["FileName"].Value.ToString();
        return (FileName[0].ToString());
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
