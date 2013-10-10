<%@ Page Language="C#" AutoEventWireup="true" Inherits="ManageRolePower" Codebehind="ManageRolePower.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>权限管理</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="Div2">
        <img id="imgTop" height="38px;" src="../img/ManagePower.jpg" width="100%" alt="角色管理" />
    </div>
    <div>
        <iframe id="ifrRole" name ="ifrRole" src="RoleList.aspx" style="width: 40%"></iframe>
        <iframe id="ifrPower" name="ifrPower" src="ManagePower.aspx" style="width: 59%" scrolling="auto"></iframe>
    </div>
    </form>
</body>
</html>

<script type="text/jscript">
    var s = document.documentElement.clientHeight + "px";
    var ifrRole = document.getElementById("ifrRole");
    var ifrPower = document.getElementById("ifrPower");
    ifrRole.style.height=s;
    ifrPower.style.height=s;
</script>
