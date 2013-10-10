<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageParent.aspx.cs" Inherits="DJXT.ParentMember.ManageParent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.01 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>组织机构管理</title>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../css/zTreeStyle/djxt.css" rel="stylesheet" type="text/css" />
    <link href="../css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />

    <script src="../jQueryEasyUI/jquery-1.6.2.js" type="text/javascript"></script>

    <script src="../js/jquery.ztree.core-3.5.js" type="text/javascript"></script>

    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>

    <style type="text/css">
        #menu
        {
            border: 1px solid #2a88bb;
        }
        .style5
        {
            font-size: 13px;
            color: Black;
        }
        .style6
        {
            font-size: 13px;
            color: #0a4869;
            font-weight: bold;
        }
    </style>

    <script type="text/javascript">
		<!--
		var setting = {
			data: {
				key: {
					title:"t"
				},
				simpleData: {
					enable: true
				}
			},
			callback: {
				onClick: onClick
			}
		};
		function onClick(event, treeId, treeNode, clickFlag) {
            $('#txtID').val(treeNode.id);
            $('#txtName').val(treeNode.name);
            $('#txtParent').val(treeNode.pId);
            $('#txtParent_edit').val(treeNode.id);
            seachParment(treeNode.id);
		};		

		$(document).ready(function(){	
            $('#dv_edit').hide();
            $("#menu").css("height",pageHeight()-100);
            $("#menu").css("width",pageWidth()-100);
            $("#dv_tree").css("height",pageHeight()-118);
            $("#tree").css("height",pageHeight()-120);
            
            $("#grid").css("width",pageWidth()-380);
            $("#grid").css("height",pageHeight()-140);
                   
            Tree("");
            Grid();
		});
		
		function Grid(){
            $('#grid').datagrid({
			    title:'组织机构',
			    iconCls:'icon-search',
			    nowrap: true,
			    autoRowHeight: false,
			    striped: true,
			    align:'center',
			    collapsible:true,
			    url:'ManageParent.aspx',
			    sortName: 'key',
			    sortOrder: 'asc',
			    remoteSort: false,
			    queryParams:{param:'seachList',id:'DFFDC'},
			    idField:'id',
			    frozenColumns:[[
                {field:'ck',checkbox:true}
			    ]],
			    columns:[[				    
				    {field:'id',title:'机构编码',width:120,align:'center'},
				    {field:'name',title:'机构名称',width:150,align:'center'},
				    {field:'parmentID',title:'父类编码',width:150,align:'center'}				
			    ]],
			    pagination:true,
			    rownumbers:true,
			    toolbar:[{
			        id:'btnadd', 
			        text:'添加部门',
			        iconCls:'icon-add',
			        handler:function(){
			            $('#dv_edit').show();
                        AddShow();                
			        }
		        },
		        {
			        id:'btnadd', 
			        text:'删除部门',
			        iconCls:'icon-remove',
			        handler:function(){
                        var rows = $('#grid').datagrid('getSelections');
                        var id="";
                        var name="";
                        $.each(rows,function(i,n){
	                        id +="'"+ n.id+"',";
	                        name+=n.name+',';
	                    });
	                    name = name.substring(0,name.length-1);
	                    id = id.substring(0,id.length-1);
                        $.messager.confirm('删除组织信息', '你确定要删除 '+id+'  吗?', function(ok){
		                    if (ok){
		                        Remove(id);
		                    }else{
		                        $.messager.alert('删除组织信息','删除已取消!','info');
		                    }
	                    });   
			        }
		        }],
		        onDblClickRow: function(rowIndex, rowData) {
		            $('#grid').datagrid('clearSelections'); 
		            $('#txtID_edit').val(rowData.id);
		            $('#txtName_edit').val(rowData.name);
		            $('#txtParent_edit').val(rowData.parmentID);
                    $('#txtHide').val(rowData.id);
                    EditShow();
                }				
		    });
		}
		
		function Tree(id){
           $.post("ManageParent.aspx", { param: '',id:id}, function (data) {
                    var  nodeEval = eval(data.menu);
                    $('#txtID').val(data.id);
                    $('#txtName').val(data.name);
                    $('#txtParent_edit').val(data.id);
                    $.fn.zTree.init($("#tree"), setting, nodeEval);                 
            }, 'json'); 
		}
		
		function seachParment(id){
            var query={param:'seachList',id:id}; //把查询条件拼接成JSON
            $("#grid").datagrid('options').queryParams=query; //把查询条件赋值给datagrid内部变量
            $("#grid").datagrid('reload'); //重新加载
		}
		
		function Remove(id){
            $.post("ManageParent.aspx", { param: 'Remove',id:id}, function (data) {
                seachParment($('#txtParent_edit').val());
                if($('#txtParent').val()==null||$('#txtParent').val()=='')
                    Tree("");
                else
                    Tree($('#txtParent').val());
                $.messager.alert('删除组织信息',data.info,'info');
            }, 'json');
		}
		
		function AddShow(){		    
		    $("#dv_edit").attr('title','添加组织信息');
		    $('#txtID_edit').val('');
		    $('#txtName_edit').val('');
		    $('#dv_edit').show();			    	    
			$('#dv_edit').dialog({
				buttons:[{
					text:'添加',
					iconCls:'icon-ok',
					handler:function(){
					    Add($('#txtID_edit').val(),escape($('#txtName_edit').val()),$('#txtParent_edit').val());
					}
				},{
					text:'重置',
					handler:function(){
						$('#dv_edit').dialog('close');
					}
				}]
			});
		}
		
		function Add(id,name,parentID){
            $.post("ManageParent.aspx", { param: 'Add',id:id,name:name,pID:parentID}, function (data) {
                seachParment($('#txtParent_edit').val());
                if($('#txtParent_edit').val()==null||$('#txtParent_edit').val()=='')
                    Tree("");
                else
                    Tree($('#txtParent_edit').val());
                $.messager.alert('编辑组织信息',data.info,'info'); 
            }, 'json');       
		}
		
		function EditShow(){
		    $("#dv_edit").attr('title','编辑组织信息');
		    $('#dv_edit').show();
			$('#dv_edit').dialog({
				buttons:[{
					text:'保存',
					iconCls:'icon-ok',
					handler:function(){
						Edit($('#txtID_edit').val(),escape($('#txtName_edit').val()),$('#txtHide').val());
					}
				},{
					text:'取 消',
					handler:function(){
						$('#dv_edit').dialog('close');
					}
				}]
			});
		}
		
		function Edit(id,name,oldID){
            $.post("ManageParent.aspx", { param: 'Edit',id:id,name:name,oldID:oldID}, function (data) {
                seachParment($('#txtParent_edit').val());
                Tree($('#txtParent_edit').val());
                $.messager.alert('编辑组织信息',data.info,'info'); 
            }, 'json');       
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

		//-->
	</script>

</head>
<body style="font-size: 12px;">
    <div id="menu">
        <input id="txtHide" type="text" name="name" style="display: none;" />
        <table id="__01" width="100%" height="95%" border="0" cellpadding="0" cellspacing="0"
            bgcolor="#2a88bb">
            <tr>
                <td colspan="3" background="../img/table-head.jpg" height="30px" valign="middle"
                    class="style6">
                    &nbsp;组织机构关系
                </td>
            </tr>
            <tr>
                <td background="../img/table-head-2.jpg" height="25px" valign="middle" class="style5">
                    &nbsp;机构列表
                </td>
                <td background="../img/table-head-3.jpg" width="1px">
                </td>
                <td background="../img/table-head-2.jpg" height="25px" valign="middle" class="style5">
                    &nbsp;操作选项
                </td>
            </tr>
            <tr>
                <td style="background-color: White" width="240px" align="left" valign="top">
                    <div id="dv_tree" class="zTreeDemoBackground left">
                        <ul id="tree" class="ztree">
                        </ul>
                    </div>
                </td>
                <td background="../img/table-head-3.jpg" width="1px">
                </td>
                <td valign="top" style="background: #f2f5f7">
                    <div class="right">
                        <ul>
                            <li>
                                <table>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp; 机构编码&nbsp;
                                            <input id="txtID" class="easyui-validatebox" type="text" name="name" style="border: solid 1px #E0ECF9;
                                                text-align: center;" disabled="disabled" />
                                            &nbsp;&nbsp;&nbsp;机构名称&nbsp;
                                            <input id="txtName" class="easyui-validatebox" type="text" name="name" style="border: solid 1px #E0ECF9;
                                                text-align: center;" disabled="disabled" />
                                            &nbsp;&nbsp;&nbsp;父类编码&nbsp;
                                            <input id="txtParent" class="easyui-validatebox" type="text" name="name" style="border: solid 1px #E0ECF9;
                                                text-align: center;" disabled="disabled" />
                                        </td>
                                    </tr>
                                </table>
                            </li>
                            <li>
                                <table id="grid">
                                </table>
                            </li>
                        </ul>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="dv_edit" data-options="iconCls:'icon-save'" style="padding: 5px; width: 400px;
        height: 200px;">
        <p>
            &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 父类编码&nbsp;
            <input id="txtParent_edit" class="easyui-validatebox" type="text" name="name" style="border: solid 1px #E0ECF9;
                text-align: center;" disabled="disabled" /></p>
        <p>
            &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 机构编码&nbsp;
            <input id="txtID_edit" class="easyui-validatebox" type="text" name="name" style="border: solid 1px #E0ECF9;
                text-align: center;" /></p>
        <p>
            &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 机构名称&nbsp;
            <input id="txtName_edit" class="easyui-validatebox" type="text" name="name" style="border: solid 1px #E0ECF9;
                text-align: center;" /></p>
    </div>
</body>
</html>
