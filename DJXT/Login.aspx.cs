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

public partial class Login : System.Web.UI.Page
{
    BLL.BLLRole bl = new BLL.BLLRole();
    string errMsg = "";

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //登录按钮
    protected void btnSure_Click(object sender, EventArgs e)
    {
        string UserName = this.UserName.Text;
        string PassWordinput = this.PassWord.Text;
        this.UserName.Text = "";
        this.PassWord.Text = "";
        if (UserName == "")
        {
            Response.Write("<script>alert('请输入用户名！')</script>");
        }
        else if (PassWordinput == "")
        {
            Response.Write("<script>alert('请输入密码！')</script>");
        }
        if (UserName != "" && PassWordinput != "")
        {
            string PassWordReal = bl.GetPwd(UserName, out errMsg);
            string UserNumber = bl.GetUnmN(UserName, out errMsg);
            if (UserNumber == "0")
            {
                Response.Write("<script>alert('用户名不存在，请检查后重新输入！')</script>");
            }
            else if (PassWordinput != PassWordReal)
            {
                Response.Write("<script>alert('密码不正确，请检查后重新输入！')</script>");
            }
            else if (PassWordinput == PassWordReal)
            {
                string idkey = bl.GetIdKeyByUN(UserName, out errMsg);
                //Response.Redirect("Main.aspx?idkey=" + idkey);
                //Response.Redirect("Main.aspx");
                //Session["ID_KEY"] = idkey;
                //Session["UserName"] = UserName;
                //Application["ID_KEY"] = idkey;
                HttpCookie cookieID = new HttpCookie("ID_KEY", idkey);
                HttpCookie cookieUN = new HttpCookie("T_USERID", UserName);
                Response.Cookies.Add(cookieID);
                Response.Cookies.Add(cookieUN);
                Server.Transfer("Main.aspx");
            }

        }

    }
}
