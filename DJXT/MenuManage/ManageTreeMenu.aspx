<%@ Page Language="C#" AutoEventWireup="true" Inherits="MenuManage_ManageTreeMenu" Codebehind="ManageTreeMenu.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head id="Head1" runat="server">
    <title>菜单管理</title>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/css/djxt.css" rel="stylesheet" type="text/css" />
    <link href="../js/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" type="text/css" />

    <script src="../jQueryEasyUI/jquery-1.6.2.js" type="text/javascript"></script>

    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>

    <script src="../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../js/My97DatePicker/WdatePicker.js"></script>
 
    <style type ="text/css" >
        #scrollDiv
        {
           border:black 0px solid;
           background-color:inherit;
           width: 100%;
           height: 275px;
            
           overflow:hidden;

        }
        #menu
        {
        	border:1px solid #2a88bb;
        }
        
        .style5
        {
            font-size:13px;     
            color:Black;
        }
        .style6
        {
        	font-size:12px;
        	color:#0a4869;
        	font-weight:bold;
        }
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


        .style7
        {
            font-size: 13px;
        }
        
        #Table1
        {
            height: 96%;
        }


    </style>
    
    <script type="text/javascript">
        var vis = '1';
        $(document).ready(function () {
            $('#dv_add').hide();
            $('#dv_edit').hide();
            $('#txtDepth').hide();
        });
        function Check(obj) {
            if (obj.checked == true) { vis = '1'; }
            else { vis = '0'; }
        }
        function AddShow() {
            //alert('1');
            $('#dv_add').show();
            $("#dv_add").attr('title', '添加菜单');
            //		    $('#txtID').val('');
            //		    $('#txtName').val('');
            //		    $('#txtSrc').val(''); 
            //		    var dep = $('#txtDepth').val();
            //		    //alert(dep);
            //		    var vis='';   

            //		    $("input[type=checkbox][checked]").each(function(){ //由于复选框一般选中的是多个,所以可以循环输出 
            //                        vis+=$(this).val()+',';
            //                    });

            //            if(document.getElementsByName("txtVis").checked==true){
            //                var vis = '1';
            //            } 

            var dlg = $('#dv_add').dialog({
            //				buttons:[{
            //					text:'添加',
            //					iconCls:'icon-ok',
            //					handler:function(){
            //					    Add($('#txtID').val(),$('#txtName').val(),$('#txtSrc').val(),vis,dep);
            //					}
            //				},{
            //					text:'取消',
            //					iconCls:'icon-no',
            //					handler:function(){
            //						$('#dv_add').dialog('close');
            //					}
            //				}]
        });
        dlg.parent().appendTo(jQuery("form:first"));
    }
    function EditShow() {
        $('#dv_edit').show();
        $("#dv_edit").attr('title', '编辑菜单');
        $('#txtID2').val($('#selectedvalue').val());
        $('#txtName2').val($('#nName').val());
        $('#txtSrc2').val($('#selecteUrl').val());
        $('#txtVis2').val($('#nVis').val());
        var dlg = $('#dv_edit').dialog({
    });
    dlg.parent().appendTo(jQuery("form:first"));
}


function Add(id, name, src, vis, dep) {
    $('#dv_add').dialog('close');
    $.post("ManageMenu.aspx", { param: 'Add', id: id, name: name, src: src, vis: vis, dep: dep }, function (data) {
    }, 'json');
}
function Cancel() {
    $('#dv_add').dialog('close');
}

function pageHeight() {
    if ($.browser.msie) {
        return document.compatMode == "CSS1Compat" ? document.documentElement.clientHeight :
            document.body.clientHeight;
    } else {
        return self.innerHeight;
    }
};

function pageWidth() {
    if ($.browser.msie) {
        return document.compatMode == "CSS1Compat" ? document.documentElement.clientWidth :
            document.body.clientWidth;
    } else {
        return self.innerWidth;
    }
};
//        $(document).ready(function(){	  
//            $("#__01").css("height",pageHeight()-10);
//            $("#menu").css("width",pageWidth()-20);
//            });
function txtID2_onclick() {

}

function txtName2_onclick() {

}

    </script>
</head>
<body topmargin="10" >
    
    <form id="form1" runat="server" >
    <div id="menu" >
    <table id="__01" width="100%" height="100%" border="0" cellpadding="0" cellspacing="0"  bgcolor="#f2f5f7" >
	<tr>
		<td background="../img/table-head.jpg" height="30px" valign="middle" class="style6">&nbsp;组织机构树管理</td>
	</tr>
	<tr>
		<td colspan="2" background="../img/table-head-2.jpg" height="25px" valign="middle" class="style5">&nbsp;导入组织机构树</td>
	</tr>
	<tr> 
	<td colspan="2" style="background-color:#f2f5f7" align="left" valign="middle" class="style7">
		组织机构树名称:<input id="treeName" type="text" runat="server" />&nbsp;&nbsp;&nbsp; 选择组织机构树：<asp:FileUpload ID="FileUpload1" runat="server"/>
				<asp:Button ID="BtnPutin" runat="server" Text="上传" OnClick="BtnPutin_Click" CssClass="button"/>
		    
	</td>
	</tr>
    </table>    
   </div> 
   
</form>

</body>
</html>
<script type="text/jscript">
    function TreeViewCheckBox_Click(e) {
        if (window.event == null)
            o = e.target;
        else
            o = window.event.srcElement;
        if (o.tagName == "INPUT" && o.type == "checkbox") {
            __doPostBack("", "");
        }
    }   
</script>
