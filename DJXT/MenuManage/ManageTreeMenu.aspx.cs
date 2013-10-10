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

public partial class MenuManage_ManageTreeMenu : System.Web.UI.Page
{
    string xmlpath = "";
    string webpath = "~/MenuManage/ManageTreeMenu.aspx";
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
    TreeNode tnp4 = new TreeNode();
    TreeNode tnp5 = new TreeNode();

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
            }
        }
        if (!IsPostBack)
        {

        }
        if (!Page.IsPostBack)
        {

        }
    }


    //上传用户导入XML
    protected void BtnPutin_Click(object sender, EventArgs e)
    {
        errMsg = "";
        int count = 0;
        string info = "";
        string flPath = FileUpload1.PostedFile.FileName.ToString();
        string treeID = Path.GetFileNameWithoutExtension(FileUpload1.PostedFile.FileName);
        FileStream fs = new FileStream(flPath, FileMode.Open, FileAccess.Read); //将图片以文件流的形式进行保存
        BinaryReader br = new BinaryReader(fs);

        byte[] imgBytesIn = br.ReadBytes((int)fs.Length);  //将流读入到字节数组中

        bool flag = bl.RetBoolUpFile(treeID, this.treeName.Value, imgBytesIn, out errMsg);

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
        Response.Redirect(webpath);

    }

    //将XML上传至数据库
    protected void BtnUp_Click(object sender, EventArgs e)
    {
        xmlpath = Server.MapPath("EditMenu.xml");
        bool isEmptyXml = bl.IsEmptyXmlMenu("Webmenu", out errMsg);
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
}
