<%@ Page Language="C#" AutoEventWireup="true" Inherits="Login" Codebehind="Login.aspx.cs" %>



<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>运营监管平台系统</title>
    <link href="../css/SIS.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../js/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../Js/Excel.js"></script>
    <style type="text/css">

    .button
    {
    width:55px;  /*图片宽带*/
    background-image:url(../img/middle-button.jpg);
    border:none;  /*去掉边框*/
    height:55px; /*图片高度*/
    color:White;
    vertical-align: middle;
    text-align:center
    }
    
    .body
    {
    	background-color:#014b92;
    }

    </style>
</head>
<body class="body" text="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
    <form id="form1" runat="server">
    <div>
    <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
  <tr align="center" valign="middle">
    <td height="465" align="center" valign="middle">
	<table width="100" border="0" bordercolorlight="#000000" bordercolordark="#ffffff" cellspacing="0" cellpadding="0">
	<tr>
	<td>
	<table width="644" height="465" border="0" cellpadding="0" cellspacing="0" style="background-image:url(../img/middle.jpg); background-repeat:no-repeat;" >
	<tr> 
       <td height="345" colspan="3" valign="top"></td>
    </tr>
	<tr>
	<td width="289" height="78" valign="top"></td>
    <td width="303" align="center" valign="top">
	<table width="100%" border="0" cellpadding="0" cellspacing="0">
	<tr>
                  <td width="219" height="39" align="center" valign="bottom" style="FONT-SIZE: 9pt">用户名：<asp:TextBox ID="UserName" runat="server" Width="120px" Height="22px" TabIndex="1"></asp:TextBox></td>
                  <td width="84" rowspan="2" align="center" valign="middle"><asp:Button ID="Submit" OnClick="btnSure_Click" runat="server" CssClass="button"></asp:Button></td>
                </tr>
                <tr>
                  <td height="39" align="center" valign="top" style="FONT-SIZE: 9pt">密&nbsp;&nbsp;码：<asp:TextBox ID="PassWord" runat="server" Width="120px" TextMode="Password" Height="22px" TabIndex="2"></asp:TextBox></td>
                  </tr>
	</table>
	</td>
	<td width="52" valign="top" ></td>
	</tr>
	<tr>
       <td height="42" colspan="3" valign="top"></td>
    </tr>
	</table>
	</td>
	</tr>
	</table>
	</td>
  </tr>
</table>
    </div>
    </form>
</body>
</html>
