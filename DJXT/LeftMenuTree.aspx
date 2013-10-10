<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_LeftMenuTree" Codebehind="LeftMenuTree.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>左侧树形菜单</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"/>
    <link href="../css/update8.css" type="text/css" rel="stylesheet"/>
    <style type="text/css">
        html
        {
        	overflow-y: auto;
            overflow-x:hidden;
        }
        body
        {
            margin-top: 0px;
            margin-left: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
        }
         <!
        -- .STYLE1
        {
            font-size: 12px;
            color: #FFFFFF;
            font-weight: bold;
        }
        .STYLE2
        {
            font-size: 12px;
            color: #003c75;
        }
        -- >
     </style>
</head>
<body bgcolor="#FFFFFF" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
    <form id="form1" runat="server">
    <div class="pnav-cnt">
        <div class="pnav-box" id="show" runat="server">
        <table border="0" cellpadding="0" cellspacing="0"><tr><td height="1" style="background-color:#2a88bb"></td></tr></table>
        </div>
        
        <script src="../Js/update8.js" type="text/javascript"></script>

        <script type="text/javascript">
 function gotoFile(fileName)
{
   window.parent.content.location.href = fileName;
}        
(function(){

NTES("span.photo-search input[type=text]").addEvent("focus", function(){ this.value == this.defaultValue && (this.value = ""); }).addEvent("blur", function(){ this.value == "" && (this.value = this.defaultValue); });
	NTES("div.pnav-box div.box-title a.btn-fold").addEvent("click", function(e){
		e.preventDefault();
		var eleTitle = NTES(this.parentNode);
		NTES(this).addCss("hidden");
		eleTitle.$("a.btn-unfold").removeCss("hidden");
		NTES(eleTitle.parentNode).$("ul.box-list").removeCss("hidden");
	});
	NTES("div.pnav-box div.box-title a.btn-unfold").addEvent("click", function(e){
		e.preventDefault();
		var eleTitle = NTES(this.parentNode);
		NTES(this).addCss("hidden");
		eleTitle.$("a.btn-fold").removeCss("hidden");
		NTES(eleTitle.parentNode).$("ul.box-list").addCss("hidden");
	});
	NTES("div.pnav-box ul.box-list a.btn-fold").addEvent("click", function(e){
		e.preventDefault();
		var eleTitle = NTES(this.parentNode);
		NTES(this).addCss("hidden");
		eleTitle.$("a.btn-unfold").removeCss("hidden");
		eleTitle.$("h2").removeCss("hidden");
	});
	NTES("div.pnav-box ul.box-list a.btn-unfold").addEvent("click", function(e){
		e.preventDefault();
		var eleTitle = NTES(this.parentNode);
		NTES(this).addCss("hidden");
		eleTitle.$("a.btn-fold").removeCss("hidden");
		eleTitle.$("h2").addCss("hidden");
	});
	new NTES.ui.Slide(NTES("ul.photo-snav li"), NTES("div.photo-scnt"), "active", "mouseover", 6000);
})();
        </script>

    </div>
    </form>
</body>
</html>
