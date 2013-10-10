<%@ Page Language="C#" AutoEventWireup="true" Inherits="ManagePower" Codebehind="ManagePower.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <script src="../jQueryEasyUI/jquery-1.6.2.js" type="text/javascript"></script>
        <style type="text/css">
            .button
        {
        width:76px;  /*图片宽带*/
        background:url(../img/button.jpg) no-repeat left top;  /*图片路径*/
        border:none;  /*去掉边框*/
        height:24px; /*图片高度*/
        color:Black;
        vertical-align: middle;
        text-align:center
        }

            .button2
            {
            width:70px;  /*图片宽带*/
            background:url(../img/button2.jpg) no-repeat left top;  /*图片路径*/
            border:none;  /*去掉边框*/
            height:24px; /*图片高度*/
            color:White;
            vertical-align: middle;
            text-align:center
            }

            .style3
        {
            font-size:13px;
            width:296px;
        }

        .style4
        {
            font-size:13px;
            width:125px;
        }
        .style5
        {
            font-size:13px;     
            color:Black;
        }
        .style6
        {
        	font-size:13px;
        	color:#0a4869;
        	font-weight:bold;
        }
        body{height:100%; margin:0px; padding:0px; }
        </style>
</head>
<body bgcolor="#FFFFFF" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" >
    <form id="form1" runat="server">
    <table id="__02"  width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
	<tr>
		<td background="../img/table-head-2.jpg" height="25px" valign="middle" class="style5" width="100%">&nbsp;组织名称</td>
	</tr>
	<tr height="40px">
		<td style="background-color:#f2f5f7" align="center">
			<asp:Label ID="lblRoleName" runat="server" Text="尚未选择任何组织" CssClass="style3"></asp:Label>
		    <asp:Label ID="lblRoleId" runat="server" Text="" Visible="False"></asp:Label>
		</td>
		</tr>
		<tr>
		<td background="../img/table-head-2.jpg" height="25px" valign="middle" class="style5">&nbsp;操作选项</td>
	</tr>
	<tr height="40px">
		<td style="background-color:#f2f5f7" align="center">
		        <asp:Button ID="btnSure" runat="server" Text="确定分配"  CssClass="button" onclick="btnSure_Click"></asp:Button>&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="取消"  CssClass="button" onclick="btnCancel_Click"></asp:Button>&nbsp;
		</td>
	</tr>
	<tr>
		<td background="../img/table-head-2.jpg" height="25px" valign="middle" class="style5">&nbsp;菜单列表</td>
	</tr>
	<tr>
		<td style="background-color:White" align="left" valign="top" height="100%">
		<div id="divgrid" style="overflow:auto; height:100%; ">
		    <asp:TreeView ID="treeMenuPower"  runat="server" ShowCheckBoxes="All" 
                    onclick="TreeViewCheckBox_Click(event)" 
                    ontreenodecheckchanged="PermTreeView_TreeNodeCheckChanged" Font-Size="10pt" 
                     ForeColor="Black" ShowLines="True">
                    <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" ForeColor="#5555DD" />
                    <DataBindings>
                        <asp:TreeNodeBinding DataMember="ROOT" TextField="caption" TargetField="owner" ImageToolTipField="visible" />
                        <asp:TreeNodeBinding DataMember="node" TextField="caption" TargetField="owner" ImageToolTipField="visible" />
                    </DataBindings>
            </asp:TreeView>
            </div>
		</td>
	</tr>
</table>
</form>
</body>
</html>
<script type="text/jscript">
    function TreeViewCheckBox_Click(e) 
    {   
         if (window.event == null)   
             o = e.target;   
         else  
             o = window.event.srcElement;   
         if (o.tagName == "INPUT" && o.type == "checkbox") 
         {   
             __doPostBack("", "");   
         }   
     }   
</script>