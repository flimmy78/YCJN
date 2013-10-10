<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageMember.aspx.cs" Inherits="DJXT.ParentMember.ManageMember" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人员信息管理</title>
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
            $('#txtParment').val(treeNode.name);
            seachParment(treeNode.id);
		};		

		$(document).ready(function(){	
            $('#dv_edit').hide();
            $('#dv_Editmember').hide();
            
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
			    title:'下级数据',
			    iconCls:'icon-search',
			    nowrap: true,
			    autoRowHeight: false,
			    striped: true,
			    align:'center',
			    collapsible:true,
			    url:'ManageMember.aspx',
			    sortName: 'key',
			    sortOrder: 'asc',
			    remoteSort: false,
			    queryParams:{param:'seachList',id:'AS'},
			    idField:'id',
			    frozenColumns:[[
                {field:'ck',checkbox:true}
			    ]],
			    columns:[[				    
				    {field:'parmentID',title:'职位编码',width:150,align:'center'},		
				    {field:'id',title:'用户名',width:120,align:'center'},
				    {field:'name',title:'真实姓名',width:150,align:'center'},
				    {field:'plan',title:'值    别',width:150,align:'center'}	
			    ]],
			    pagination:true,
			    rownumbers:true,
			    toolbar:[{
			        id:'btnadd', 
			        text:'添加人员',
			        iconCls:'icon-add',
			        handler:function(){
			            $('#dv_edit').show();
                        AddShow();                
			        }
		        },
		        {
			        id:'btnadd', 
			        text:'删除人员',
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
                        $.messager.confirm('删除组织信息', '你确定要删除 '+name+'  吗?', function(ok){
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
                    $('#txtHide').val(rowData.id);
                    EditShow(rowData.id);
                }				
		    });
		}
		
		function Tree(id){
           $.post("ManageMember.aspx", { param: '',id:id}, function (data) {
                    var  nodeEval = eval(data.menu);
                    $('#txtID').val(data.id);
                    $('#txtName').val(data.name);
                    $('#txtParment').val(data.name);
                    $.fn.zTree.init($("#tree"), setting, nodeEval);
                    //岗位信息
                    $("#Par").empty();
                    $("#Par").append("<option value='0'>主岗</option>");
                    $("#Par").append("<option value='1'>副岗</option>"); 
                    //绑定值别信息
                    var list = data.list;  
                    $("#slclass").empty();
                    if(list!=null){
                        $("#slclass").append("<option value='0000'>长白班</option>"); 
                        for(var i=0;i<list.length;i++){
                            $("#slclass").append("<option value='"+list[i].T_CLASSID+"'>"+list[i].T_CLASSNAME+"</option>");
                        }                              
                    }         
            }, 'json'); 
		}
		
		function seachParment(id){
            var query={param:'seachList',id:id}; //把查询条件拼接成JSON
            $("#grid").datagrid('options').queryParams=query; //把查询条件赋值给datagrid内部变量
            $("#grid").datagrid('reload'); //重新加载
		}
		
		function Remove(id){
            $.post("ManageMember.aspx", { param: 'Remove',id:id}, function (data) {
                seachParment($('#txtID').val());
                $.messager.alert('删除人员信息',data.info,'info');
            }, 'json');
		}
		
		function AddShow(){		    
		    $("#dv_edit").attr('title','添加人员信息');
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
					iconCls:'icon-reload',
					handler:function(){
						$('#dv_edit').dialog('close');
					}
				}]
			});
		}
		
		function Add(id,name,parentID){
            if($("#txtTrueName").val()=="" || $("#txtTrueName").val()==null ||escape($("#txtUserName").val())==null||escape($("#txtUserName").val())==""||escape($("#txtPwd").val())==null||escape($("#txtPwd").val())==""){
                $("#Par").hide();
                $("#slclass").hide();
                $.messager.alert('添加人员信','用户名 密码 真实姓名都不能为空!','info');
                $("#Par").show();
                $("#slclass").show();
            }else{
                $.post("ManageMember.aspx", { param: 'JudgeMember',userName:escape($("#txtUserName").val()),name:escape($("#txtTrueName").val())}, function (data) {
                    if(Number(data.judge)==1&&Number(data.num)==1){
                        $.messager.alert('添加人员信','已经存在用户名为：'+$("#txtUserName").val()+'  并且真实姓名为：'+$("#txtTrueName").val()+'  的人员,不能重复添加!','error');
                    }else if(Number(data.judge)==1){
                        $.messager.alert('添加人员信','已经存在用户名为：'+$("#txtUserName").val()+'  的人员,不能重复添加!','error');
                    }else if(Number(data.num)==1){
                        $.messager.confirm('添加人员信', '你确定要重复添加真实姓名为：'+$("#txtTrueName").val()+'  的人员吗?', function(ok){
                            if (ok){
                                if($("#txtPwd").val().length>10){
                                    $.messager.alert('添加人员信',"密码长度不能大于10",'warning');
                                }else{                                    
                                    $.post("ManageMember.aspx", { param: 'AddMember',classs:$("#slclass").val(),userName:escape($("#txtUserName").val()),pwd:escape($("#txtPwd").val()),name:escape($("#txtTrueName").val()),
                                    img:escape($("#flImg").val()),parentId:$('#txtID').val(),par:$("#Par").val()}, function (data) {
                                        $.messager.alert('添加人员信',data.info,'info');
//                                            Tree($('#txtID').val());
                                        seachParment($('#txtID').val());
                                    }, 'json'); 
                                }
                            }
                        });            
                    }else{
                        $.post("ManageMember.aspx", { param: 'AddMember',classs:$("#slclass").val(),userName:escape($("#txtUserName").val()),pwd:escape($("#txtPwd").val()),name:escape($("#txtTrueName").val()),
                        img:escape($("#flImg").val()),parentId:$('#txtID').val(),par:$("#Par").val()}, function (data) {
                            $.messager.alert('添加人员信',data.info,'info');
//                                Tree($('#txtID').val());
                            seachParment($('#txtID').val());
                        }, 'json');
                    }
                }, 'json'); 
            }    
		}
		
		function EditShow(id){
            $.post("ManageMember.aspx", { param: 'Edit',id:id}, function (data) {
                var list = data.list;
                var listClass = data.listClass; 
                if(data.img.length>0){    
                    $("#img_").show();                
                    $("#img_").attr("src",data.img);
                }else{
                    $("#img_").hide();
                }
                $("#txtPwdEdit").val(list[0].T_PASSWD);
                $("#txtUserNameEdit").val(list[0].T_USERID);                        
                $("#txtTrueNameEdit").val(list[0].T_USERNAME);                       
                
                $("#slclassEdit").empty();
                var count=0;
                for(var i=0;i<listClass.length;i++){
                    if(list[0].T_CLASSID==listClass[i].T_CLASSID){
                        count=i;
                        $("#slclassEdit").append("<option value='"+listClass[i].T_CLASSID+"' selected='selected'>"+listClass[i].T_CLASSNAME+"</option>");
                    }else{
                        $("#slclassEdit").append("<option value='"+listClass[i].T_CLASSID+"'>"+listClass[i].T_CLASSNAME+"</option>");
                    }
                }                        
                if(count==listClass.length){
                    $("#slclassEdit").append("<option value='0000' selected='selected'>长白班</option>");
                }else{
                    $("#slclassEdit").append("<option value='0000'>长白班</option>");
                }
            }, 'json');
                
		    $("#dv_Editmember").attr('title','编辑人员信息');
		    $('#dv_Editmember').show();
			$('#dv_Editmember').dialog({
				buttons:[{
					text:'保存',
					iconCls:'icon-ok',
					handler:function(){
						Edit();
					}
				},{
					text:'取 消',
					handler:function(){
						$('#dv_Editmember').dialog('close');
					}
				}]
			});
		}
		
		function Edit(){
            if($("#txtUserNameEdit").val()==null ||$("#txtUserNameEdit").val()==''||$("#txtTrueNameEdit").val()==null ||$("#txtTrueNameEdit").val()==''||$("#txtPwdEdit").val()==null ||$("#txtPwdEdit").val()==''){
                $.messager.alert('编辑人员信息','编辑人员信息时：用户名、密码和真实姓名都不能为空!','info');
            }else{
                if($("#txtHide").val()==$("#txtUserNameEdit").val()){
                    $.post("ManageMember.aspx", { param: 'EditMemberInfo',oldId:$("#txtHide").val(),id:$("#txtUserNameEdit").val(),pwd:escape($("#txtPwdEdit").val()),
                    trueName:escape($("#txtTrueNameEdit").val()),img:escape($("#flImgEdit").val()),classId:$("#slclassEdit").val()}, function (data) {
                        seachParment($('#txtID').val());
                        $.messager.alert('人员分岗',data.info,'info');
                    }, 'json');     
                }else{
                    $.post("ManageMember.aspx", { param: 'JudgeMemberInfo',id:$("#txtUserNameEdit").val()}, function (data) {
                        if(Number(data.judge)==1){
                             $.messager.alert('编辑人员信息','已经存在用户名为：'+$("#txtUserNameEdit").val()+'  的人员,用户名不能重复!','error');
                        }else{
                            $.post("ParentMember.aspx", { param: 'EditMemberInfo',oldId:$("#txtHide").val(),id:$("#txtUserNameEdit").val(),pwd:escape($("#txtPwdEdit").val()),
                            trueName:escape($("#txtTrueNameEdit").val()),img:escape($("#flImgEdit").val()),classId:$("#slclassEdit").val()}, function (data) {
                                seachParment($('#txtID').val());
                                $.messager.alert('人员分岗',data.info,'info');
                            }, 'json');   
                        }
                    }, 'json');   
                }
            }  
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
                    &nbsp;人员管理
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
    <div id="dv_edit" data-options="iconCls:'icon-save'" style="padding: 5px; width: 700px;
        height: 320px;">
        <div id="dv_Member_info">
            <table class="admintable" width="100%">
                <tr>
                    <th class="adminth" colspan="4">
                        添加人员信息
                    </th>
                </tr>
                <tr>
                    <td class="admincls0" align="center">
                        上级组织
                    </td>
                    <td class="admincls0">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtParment" type="text" disabled="disabled" />
                    </td>
                    <td class="admincls0" align="center">
                        岗位类型
                    </td>
                    <td class="admincls0">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<select id="Par" style="width: 135px;">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="admincls1" align="center">
                        真实姓名
                    </td>
                    <td class="admincls1">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtTrueName" type="text" />
                    </td>
                    <td class="admincls1" align="center">
                        值&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;别
                    </td>
                    <td class="admincls1">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<select id="slclass" style="width: 135px;
                            z-index: 20;">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="admincls0" align="center">
                        用户名称
                    </td>
                    <td class="admincls0">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtUserName" type="text" />
                    </td>
                    <td class="admincls0" align="center">
                        用户密码
                    </td>
                    <td class="admincls0">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtPwd" type="text" />
                    </td>
                </tr>
                <tr>
                    <td class="admincls1" align="center">
                        照&nbsp;&nbsp;&nbsp;&nbsp;片
                    </td>
                    <td class="admincls1" colspan="3">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="flImg" type="file" style="width: 280px;" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="dv_Editmember" style="padding: 5px; width: 700px; height: 400px;">
        <div id="dv_Member_Info_Edit">
            <table class="admintable" width="100%">
                <tr>
                    <th class="adminth" colspan="4">
                        编辑人员信息
                    </th>
                </tr>
                <tr>
                    <td class="admincls0" align="center">
                        用户名称
                    </td>
                    <td class="admincls0">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtUserNameEdit" type="text" />
                    </td>
                    <td class="admincls0" align="center">
                        用户密码
                    </td>
                    <td class="admincls0">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtPwdEdit" type="text" />
                    </td>
                </tr>
                <tr>
                    <td class="admincls1" align="center">
                        真实姓名
                    </td>
                    <td class="admincls1">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtTrueNameEdit" type="text" />
                    </td>
                    <td class="admincls1" align="center">
                        值&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;别
                    </td>
                    <td class="admincls1">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<select id="slclassEdit" style="width: 135px;
                            z-index: 20;">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="admincls0" align="center">
                        照&nbsp;&nbsp;&nbsp;&nbsp;片
                    </td>
                    <td class="admincls0" colspan="3">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="flImgEdit" type="file" style="width: 280px;" /><br />
                    </td>
                </tr>
                <tr>
                    <td class="admincls1" align="center">
                        已传照片
                    </td>
                    <td class="admincls1" colspan="3" align="center">
                        <img id="img_" alt="" src="" style="width: 60px; height: 80px; text-align: center;" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</body>
</html>
