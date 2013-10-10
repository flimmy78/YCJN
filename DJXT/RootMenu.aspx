<%@ Page Language="C#" AutoEventWireup="true" Inherits="RootMenu" Codebehind="RootMenu.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>横排导航菜单</title>
    <script type="text/javascript" src="js/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
	<style>
body{height:100%; overflow:hidden; margin:0px; padding:0px;}.box {height:100%; background:#ff0000; position:absolute; width:100%;} 

.Text1 {
	font-size: 13px;
	font-weight: bold;
	color: #FFFFFF;
}

.Text2 {
	font-size: 12px;
	font-weight:normal;
	color: #FFFFFF;
}

.Text3 {
	font-size: 12px;
	font-weight:normal;
	color:Yellow;
}
</style>
<style TYPE="text/css"><!--A:link{text-decoration:none}A:visited{text-decoration:none}A:hover {color: #87aaca;text-decoration:underline} --></style>
</style>
<script type="text/javascript">
window.onload=function(){
            gotoHomePage();
    }

function gotoHomePage()
{
    var url = "<%=GetHomePageUrl()%>";
    window.parent.content.location.href = url;
}
function gotoTree(fileName,rootMenuItem)
{
   window.parent.lefterIframe.location.href = "LeftMenuTree.aspx?rootMenuItem=" + escape(rootMenuItem);
}
function gotoFile(fileName)
{
    window.parent.content.location.href = fileName;
    gotoTree("", "");
    //隐藏侧边栏
    $("#lefter").attr("display", "none");
}
function SetColor(a) {
    $(a).parent().find("a").each(function (i, j) {
        $(j).attr("style", "background-color:");
    });
    $(a).attr("style", "background-color:Red;");
}

function reloadMain()
{
    window.top.document.location.reload();
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%"  bgcolor="#013b72" height="32" border="0" cellpadding="0" cellspacing="0" background="../img/MENU_2_bg.gif">
    <tr>
    <div id="show" runat = "server">
    
    </div>
    <td align="right" valign="middle"><a class="Text3">欢迎您,</a><asp:Label ID="lblUserWelcome" runat="server" CssClass="Text3"></asp:Label>&nbsp;
    <asp:LinkButton ID="linkBtnLogout" runat="server" OnClick="linkBtnLogout_Click" BorderStyle="None" CssClass="Text2">注销</asp:LinkButton>&nbsp;&nbsp;</td></tr>
    </table>
    </div>
    </form>
</body>
</html>
