<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_ManageBC" Codebehind="ManageBC.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.01 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>班次管理</title>
        <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/css/djxt.css" rel="stylesheet" type="text/css" />
    <link href="../js/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" type="text/css" />

    <script src="../jQueryEasyUI/jquery-1.6.2.js" type="text/javascript"></script>

    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>

    <script src="../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../js/My97DatePicker/WdatePicker.js"></script>
    
    <style type="text/css">
        
        #menu
        {
        	border:1px solid #2a88bb;
        }

    .button
    {
    width:56px;  /*图片宽带*/
    background:url(../img/button.jpg) no-repeat left top;  /*图片路径*/
    border:none;  /*去掉边框*/
    height:24px; /*图片高度*/
    color:White;
    vertical-align: middle;
    text-align:center
    }

    .grid-head 
    { 
        font-size: 12px; 
        font-weight:bold; 
        color:White;
        background-image:url(../img/footer.jpg); 
        text-align:center; 
        vertical-align:middle; 
    }

    
    .style4
    {
    	font-size:12px;
        width:500px;
        color:#599ce0;
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

    </style>
<script type="text/javascript">
$(document).ready(function(){	
            $('#dv_add').hide();
            Grid();
		});
		
		function Grid(){
            $('#test').datagrid({
			    title:'班次列表',
			    iconCls:'icon-search',
			    nowrap: true,
			    autoRowHeight: false,
			    striped: true,
			    height:520,
			    align:'center',
			    collapsible:true,
			    url:'ManageBC.aspx',
			    sortName: 'ID_KEY',
			    sortOrder: 'asc',
			    remoteSort: false,
			    queryParams:{param:'seachList'},
			    idField:'id',
			    frozenColumns:[[
                {field:'ck',checkbox:true}
			    ]],
			    columns:[[				    
				    {field:'ID_KEY',title:'班次编号',width:120,align:'center'},
				    {field:'T_SHIFT',title:'班次描述',width:150,align:'center'},
				    {field:'T_STARTTIME',title:'起始时间',width:150,align:'center'},
				    {field:'T_ENDTIME',title:'结束时间',width:150,align:'center'}				
			    ]],
			    pagination:true,
			    rownumbers:true,
			    toolbar:[{
			        id:'btnadd', 
			        text:'添加班次',
			        iconCls:'icon-add',
			        handler:function(){
			            $('#dv_edit').show();
                        AddShow();                
			        }
		        },
		        {
			        id:'btnadd', 
			        text:'编辑班次',
			        iconCls:'icon-edit',
			        handler:function(){
	              var rows = $('#test').datagrid('getSelections');
	              var id="";
                        var shift="";
                        var St="";
                        var Et="";
                        var Oshift="";
                        $.each(rows,function(i,n){
	                        id +=""+ n.ID_KEY+",";
	                        shift+=n.T_SHIFT+',';
	                        Oshift+=n.T_SHIFT+',';
	                        St+=n.T_STARTTIME+',';
	                        Et+=n.T_ENDTIME+',';
	                    });
	                    shift = shift.substring(0,shift.length-1);
	                    id = id.substring(0,id.length-1);
	                    Oshift = Oshift.substring(0,Oshift.length-1);
	                    St = St.substring(0,St.length-1);
	                    Et = Et.substring(0,Et.length-1);
	                    $('#test').datagrid('clearSelections'); 
		            $('#txtID').val(id);
		            $('#txtShift').val(shift);
		            $('#txtOShift').val(Oshift);
		            $('#txtStime').val(St);
                    $('#txtEtime').val(Et);                   
                    EditShow();
			        }
		        },
		        {
			        id:'btnadd', 
			        text:'删除班次',
			        iconCls:'icon-remove',
			       handler:function(){
                        var rows = $('#test').datagrid('getSelections');
                        var id="";
                        var name="";
                        $.each(rows,function(i,n){
	                        id +=""+ n.ID_KEY+",";
	                        name+=n.T_SHIFT+',';
	                    });
	                    name = name.substring(0,name.length-1);
	                    id = id.substring(0,id.length-1);
                        $.messager.confirm('删除班次信息', '你确定要删除 '+name+'  吗?', function(ok){
		                    if (ok){
		                        Remove(id);
		                    }else{
		                        $.messager.alert('删除班次信息','删除已取消!','info');
		                    }
	                    });   
			        }
		        }],
		        onDblClickRow: function(rowIndex, rowData) {
		            $('#test').datagrid('clearSelections'); 
		            $('#txtID').val(rowData.ID_KEY);
		            $('#txtShift').val(rowData.T_SHIFT);
		            $('#txtOShift').val(rowData.T_SHIFT);
		            $('#txtStime').val(rowData.T_STARTTIME);
                    $('#txtEtime').val(rowData.T_ENDTIME);                   
                    EditShow();
                }				
		    });
		}
		
		function seachParment(){
            var query={param:'seachList'}; //把查询条件拼接成JSON
            $("#test").datagrid('reload'); //重新加载
		}
		
		function AddShow(){		    
		    $("#dv_add").attr('title','添加班次信息');
		    $('#txtShift').val('');
		    $('#txtStime').val('');
		    $('#txtEtime').val('');
		    $('#dv_add').show();			    	    
			$('#dv_add').dialog({
				buttons:[{
					text:'添加',
					iconCls:'icon-ok',
					handler:function(){
					    Add($('#txtShift').val(),$('#txtStime').val(),$('#txtEtime').val());
					}
				},{
					text:'重置',
					iconCls:'icon-no',
					handler:function(){
						$('#dv_add').dialog('close');
					}
				}]
			});
		}
		
		function Add(shift,St,Et){
		
            $.post("ManageBC.aspx", { param: 'Add',shift:shift,St:St,Et:Et}, function (data) {
                seachParment();
                $('#dv_add').dialog('close');
                $.messager.alert('添加班次信息',data.message,'info'); 
            }, 'json');       
		}
		function Remove(id){
            $.post("ManageBC.aspx", { param: 'Remove',id:id}, function (data) {
                seachParment();
                $.messager.alert('删除组织信息',data.message,'info');
            }, 'json');
		}
		
		function EditShow(){
		    $("#dv_add").attr('title','编辑班次信息');
		    $('#dv_add').show();
			$('#dv_add').dialog({
				buttons:[{
					text:'保存',
					iconCls:'icon-ok',
					handler:function(){
						Edit($('#txtID').val(),$('#txtShift').val(),$('#txtStime').val(),$('#txtEtime').val(),$('#txtOShift').val());
					}
				},{
					text:'取 消',
					iconCls:'icon-no',
					handler:function(){
						$('#dv_add').dialog('close');
					}
				}]
			});
		}
		
		function Edit(id,shift,St,Et,Oshift){
            $.post("ManageBC.aspx", { param: 'Edit',id:id,shift:shift,St:St,Et:Et,Oshift:Oshift}, function (data) {
                seachParment();
                $('#dv_add').dialog('close');
                $.messager.alert('编辑组织信息',data.message,'info'); 
            }, 'json');       
		}
		
</script>
    
</head>
<body>
    <form id="form1" runat="server">
     <div id="menu" >
    <table id="__01" width="100%" height="92%" border="0" cellpadding="0" cellspacing="0"  bgcolor="#FFFFFF" >       
    <tr>
		<td background="../img/table-head.jpg" height="30px" valign="middle" class="style6">&nbsp;班次管理</td>
	</tr>
	<tr>
		<td background="../img/table-head-2.jpg" height="25px" valign="middle" class="style5">&nbsp;注意事项</td>
	</tr>
	<tr>
	<td align="left" valign="middle" height="30px">
    <div class="style4">
    &nbsp;&nbsp;起始时间和结束时间的格式如：00:00:00，冒号使用半角符号</div>
    </td>
    </tr>
	<tr>
	<td>
	<div>
    <table id="test">
    </table>
    </div>
    </td>
    </tr>
    
    
    <div id="dv_add" data-options="iconCls:'icon-save'" style="padding: 5px; width: 400px;
        height: 200px;">
        <p>
            &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 班次描述&nbsp;
            <input id="txtShift" class="easyui-validatebox" type="text" name="name" style="border: solid 1px #E0ECF9;
                text-align: center;" /><input type="text" id="txtOShift" style="visibility:hidden" /></p>
        <p>
            &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 起始时间&nbsp;
            <input id="txtStime" class="easyui-validatebox" type="text" name="name" style="border: solid 1px #E0ECF9;
                text-align: center;" /></p>
        <p>
            &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 结束时间&nbsp;
            <input id="txtEtime" class="easyui-validatebox" type="text" name="name" style="border: solid 1px #E0ECF9;
                text-align: center;" /><br /><input type="text" id="txtID" style="visibility:hidden" /></p>
                
    </div>
    </table>
    </div>
    </form>
</body>
</html>
