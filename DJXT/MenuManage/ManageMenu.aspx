<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_ManageMenu" Codebehind="ManageMenu.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">

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
        
        .style8
        {
            font-size: 13px;
            width:5%;
        }

        #Table1
        {
            height: 96%;
        }


    </style>
    
    <script type="text/javascript">
    var vis='1';
    $(document).ready(function(){	
            $('#dv_add').hide();
            $('#dv_edit').hide();
            $('#txtDepth').hide();
		});
		function Check(obj){
		    if(obj.checked==true) { vis='1';} 
		    else{ vis='0';} 
		}
		function AddShow(){	
		//alert('1');
		    $('#dv_add').show();
		    $("#dv_add").attr('title','添加菜单');
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
            
		    var dlg =$('#dv_add').dialog({
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
		function EditShow(){
		    $('#dv_edit').show();
		    $("#dv_edit").attr('title','编辑菜单');
		    $('#txtID2').val($('#selectedvalue').val());
		    $('#txtName2').val($('#nName').val());
		    $('#txtSrc2').val($('#selecteUrl').val());
		    $('#txtVis2').val($('#nVis').val());
		    var dlg =$('#dv_edit').dialog({
		    });
			dlg.parent().appendTo(jQuery("form:first"));
		}
		
		
		function Add(id,name,src,vis,dep){
		$('#dv_add').dialog('close');
            $.post("ManageMenu.aspx", { param: 'Add',id:id,name:name,src:src,vis:vis,dep:dep}, function (data) {
            }, 'json');       
		}
		function Cancel(){
		    $('#dv_add').dialog('close');
		}
		
		function pageHeight(){ 
            if($.browser.msie){ 
            return document.compatMode == "CSS1Compat"? document.documentElement.clientHeight : 
            document.body.clientHeight; 
            }else{ 
            return self.innerHeight; 
            } 
        }; 

        function pageWidth(){ 
            if($.browser.msie){ 
            return document.compatMode == "CSS1Compat"? document.documentElement.clientWidth : 
            document.body.clientWidth; 
            }else{ 
            return self.innerWidth; 
            } 
        };
//        $(document).ready(function(){	  
//            $("#__01").css("height",pageHeight()-10);
//            $("#menu").css("width",pageWidth()-20);
//            });
    </script>
</head>
<body topmargin="10" >
    
    <form id="form1" runat="server" >
    <div id="menu" >
    <table id="__01" width="100%" height="100%" border="0" cellpadding="0" cellspacing="0"  bgcolor="#f2f5f7" >
	<tr>
		<td background="../img/table-head.jpg" height="30px" valign="middle" class="style6">&nbsp;菜单管理</td>
		<td background="../img/table-head.jpg" height="30px" valign="middle" class="style6"></td>
		<td background="../img/table-head.jpg" height="30px" valign="middle" class="style6"></td>
		<td background="../img/table-head.jpg" height="30px" valign="middle" class="style6"></td>
	</tr>
	<tr>
		<td background="../img/table-head-2.jpg" height="25px" valign="middle" class="style5" width="30%">&nbsp;菜单列表</td>
		<td background="../img/table-head-3.jpg" width="1px" valign="top"><img src="../img/table-head-3.jpg" /></td>
		<td colspan="2" background="../img/table-head-2.jpg" height="25px" valign="middle" class="style5">&nbsp;导入菜单</td>
	</tr>
	<tr> 
		<td rowspan="9" style="background-color:White" align="left" valign="top">
		<div id="divgrid" style="overflow:auto;height:100%; ">
			<asp:TreeView ID="TreeView1"  runat="server"
            onselectednodechanged="TreeView1_SelectedNodeChanged1" Font-Bold="False" 
                ForeColor="Black" Font-Size="10pt" ShowLines="True" LineImagesFolder>
            <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" 
                    VerticalPadding="0px" ForeColor="#000000" BackColor="#FFCC99"/>
            <DataBindings>
                <asp:TreeNodeBinding DataMember="ROOT" />
                <asp:TreeNodeBinding DataMember="node" />
            </DataBindings>
            </asp:TreeView>
           </div> 
		</td>
		<td background="../img/table-head-3.jpg" width="1px" height="40px"></td><td class="style8"></td>
	<td colspan="2" style="background-color:#f2f5f7" align="left" valign="middle" class="style7">
		导入菜单文件:<asp:FileUpload ID="FileUpload1" runat="server"/>
				<asp:Button ID="BtnPutin" runat="server" Text="上传" OnClick="BtnPutin_Click" CssClass="button"/>
		    
	</td>
	</tr>
	<tr>
	<td background="../img/table-head-3.jpg" width="1px"></td>
	<td colspan="2" background="../img/table-head-2.jpg" height="25px" valign="middle" class="style5">&nbsp;操作选项</td>
	</tr>
	</tr>
	<tr height="40px">
	<td background="../img/table-head-3.jpg" width="1px"></td><td></td>
		<td colspan="2" style=" background:#f2f5f7" align="left" class="style7">&nbsp;&nbsp;
            <input type="button" ID="BtnAdd" value="添加" onclick="AddShow()" class="button">
            <input type="button" ID="BtnEdit" value="修改" onclick="EditShow()" class="button">
	        <asp:Button ID="BtnDel" runat="server" Text="删除" OnClick="BtnDel_Click" CssClass="button"/>
        </td>
	</tr>
	<tr height="30px">
	<td background="../img/table-head-3.jpg" width="1px"></td><td></td>
		<td style="background-color:#f2f5f7" align="left" valign="middle" 
            class="style7">
		菜单序号:<input id="selectedvalue" type="text" runat="server" size="35" disabled/><input type="text" runat="server" id="txtDepth" size="2"/>
		</td>
	</tr>
	<tr height="30px">
	<td background="../img/table-head-3.jpg" width="1px"></td></td><td>
		<td style="background-color:#f2f5f7" align="left" valign="middle" 
            class="style7">
		菜单名称:<input id="nName" type="text" runat="server" size="35" disabled/>
		</td>
	</tr>
	<tr height="30px">
	<td background="../img/table-head-3.jpg" width="1px"></td></td><td>
		<td style="background-color:#f2f5f7" align="left" valign="middle" 
            class="style7">
		菜单链接:<input id="selecteUrl" type="text" runat="server" size="35" disabled/>
		</td>
	</tr>
	<tr height="30px">
	<td background="../img/table-head-3.jpg" width="1px"></td></td><td>
		<td style="background-color:#f2f5f7" align="left" valign="middle" 
            class="style7">
		显示菜单:<asp:CheckBox runat="server" ID="nVis" runat="server" disabled/>
		</td>
	</tr>
	<tr height="30px">
	<td background="../img/table-head-3.jpg" width="1px"></td>
		<td colspan="2" style="background-color:#f2f5f7" align="center" valign="middle" >
			<asp:Label  CssClass="style5" ID="Lab1" align="center" valign="bottom" runat="server" ForeColor="Red" Width="300px" Height="26px"></asp:Label>
		</td>
	</tr>
	<tr>
	<td background="../img/table-head-3.jpg" width="1px"></td>
	<td colspan="2" style="background-color:#f2f5f7" align="center" valign="middle" >
	</td>
	</tr>
    </table>    
   </div> 
   
   <div id="dv_add" data-options="iconCls:'icon-save'" style="padding: 5px; width: 400px;
        height: 200px;">

            &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 菜单序号:&nbsp;
            <input id="txtID" class="easyui-validatebox" type="text" name="name" runat="server" style="border: solid 1px #E0ECF9;
                text-align: center;" size="35" /><br>

            &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 菜单名称:&nbsp;
            <input id="txtName" class="easyui-validatebox" type="text" name="name" runat="server" style="border: solid 1px #E0ECF9;
                text-align: center;" size="35" /><br>

            &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 菜单链接:&nbsp;
            <input id="txtSrc" class="easyui-validatebox" type="text" name="name" runat="server" style="border: solid 1px #E0ECF9;
                text-align: center;" size="35"/><br>

            &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 显示菜单:&nbsp;
            <input type="checkbox" id="txtVis" name="txtVis" value="txtVis" checked="checked" runat="server" onclick="Check(this)"/><br><br>
            
              &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
              <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="BtnAdd_Click" CssClass="button"/> 
              <input type="button" runat="server" value="取消" onclick="Cancel()" class="button"/>
    </div>
    
    <div id="dv_edit" data-options="iconCls:'icon-save'" style="padding: 5px; width: 400px;
        height: 200px;">

            &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 菜单序号:&nbsp;
            <input id="txtID2" class="easyui-validatebox" type="text" name="name" runat="server" style="border: solid 1px #E0ECF9;
                text-align: center;" size="35" /><br>

            &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 菜单名称:&nbsp;
            <input id="txtName2" class="easyui-validatebox" type="text" name="name" runat="server" style="border: solid 1px #E0ECF9;
                text-align: center;" size="35" /><br>

            &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 菜单链接:&nbsp;
            <input id="txtSrc2" class="easyui-validatebox" type="text" name="name" runat="server" style="border: solid 1px #E0ECF9;
                text-align: center;" size="35"/><br>

            &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 显示菜单:&nbsp;
            <input type="checkbox" id="txtVis2" name="txtVis" value="txtVis" checked="checked" runat="server" onclick="Check(this)"/><br><br>
            
              &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
              <asp:Button ID="Button1" runat="server" Text="保存" OnClick="BtnUpd_Click" CssClass="button"/> 
              <input id="Button2" type="button" runat="server" value="取消" onclick="Cancel()" class="button"/>
    </div>
   
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

