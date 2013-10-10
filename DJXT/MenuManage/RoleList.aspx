<%@ Page Language="C#" AutoEventWireup="true" Inherits="RoleList" Codebehind="RoleList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>组织岗位列表</title>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/css/djxt.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #menu
        {
        	border:1px solid #2a88bb;
        }
        .grid-head
        {
            font-size: 12px;
            font-weight: bold;
            color: White;
            background-image: url(../img/footer.jpg);
            text-align: center;
            vertical-align: middle;
        }
        .style4
        {
            height: 42px;
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

    </style>

    <script src="../jQueryEasyUI/jquery-1.6.2.js" type="text/javascript"></script>

    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>

    <script src="../js/jquery.ztree.core-3.5.js" type="text/javascript"></script>

    <script type="text/javascript">
    
		<!--
		var btn_num_add=1;
		var btn_num_remove=1;
		var btn_num_save=1;
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
		 $("#txtRoleName").val(treeNode.name);//右侧表格显示组织名称
		 $("#txtRoleId").val(treeNode.id);
		 $("#lblRoleId").val(treeNode.id);
		 $("#txtID").val(treeNode.id);
		 if(treeNode.t =="存在人员")
		 {
		    window.ifrPower.location.href = "ManagePower.aspx?id=" + treeNode.id;
		 }
		 else
		 {
		    alert("此组织下不存在人员，无法分配权限，请选择其他组织或下一层组织！"); 
		 }
		 //var ifrPower = document.getElementById("");
		 //window.parent.ifrPower.location.href = "ManagePower.aspx";
		 //document.getElementById('ifrPower').src = "ManagePower.aspx";
//            $.post("RoleList.aspx", { param: 'btnTree',id:treeNode.id}, function (data) {
//                if(Number(data.count)>0){
//			        $('#add').linkbutton({disabled:false});
//		            $('#save').linkbutton({disabled:false});
//		            $('#delete').linkbutton({disabled:true});
//		            btn_num_remove=0;}
//                else{
//			        $('#add').linkbutton({disabled:false});
//		            $('#save').linkbutton({disabled:false});
//		            $('#delete').linkbutton({disabled:false});
//		            btn_num_remove=1;}
//                
//                
//                //#1对应CS里的#1
//                $("#txtTreeID").val(treeNode.id);
//                $("#txtTreeName").val(treeNode.name);
//                $("#txtTreeFatherID").val(treeNode.pId);
//                $("#txtRoleName").val(treeNode.name);//右侧表格显示组织名称
//                

//                //组织关系
//                $("#txtParentName").val(treeNode.name);               
//                $("#txtId_Edit").val(treeNode.id);
//                $("#txtID_Old_Edit").val(treeNode.id);
//                $("#txtName_Edit").val(treeNode.name);                
//                $("#txtID_Remove").val(treeNode.id);
//                $("#txtName_Remove").val(treeNode.name);
//                
//                //人员信息
//                $("#txt_Meber_Id").val(treeNode.id);
//                $("#txt_Meber_Parent").val(treeNode.name);
//                
//                //人员分配
//                $("#txt_Members_ID").val(treeNode.id);
//                $("#txt_Members_Name").val(treeNode.name);
//                if(Number(data.num)==0){           
//                    $("#tt").tabs("select", "部门信息");
//                    $("#dv_Parent_Info").show();
//                    $("#dv_Parent_Info_father").hide();
//                    $("#dv_Edit_Parent").hide();
//                    $("#dv_Edit_Remove").hide();
//                    
//                    $("#dv_Member_Info").show();
//                    $("#dv_Member_Info_Edit").hide();
//                    $("#dv_Member_Info_Remove").hide();
//                    
//                    //加载人员岗位---关系(人员分岗部分)
//                    var list=data.list;
//                    var listMembers = data.listMembers;
//                    var checkboxs='';
//                    if(list!=null){
//                        $("#dv_Members").html('');
//                        var count=0;
//                        for(var i=0;i<list.length;i++){
//                            count++;
//                            if(count%4==0){
//                                checkboxs+="<input type='checkbox' name='checkbox' value='"+list[i].T_USERID+"' checked='checked'>&nbsp;&nbsp;&nbsp;"+list[i].T_USERNAME+'&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>';
//                            }else{
//                                checkboxs+="<input type='checkbox' name='checkbox' value='"+list[i].T_USERID+"' checked='checked'>&nbsp;&nbsp;&nbsp;"+list[i].T_USERNAME+'&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;'; 
//                            }
//                        }                    
//                        for(var i=0;i<listMembers.length;i++){      
//                            count++;
//                            var num=0;
//                            for(k=0;k<list.length;k++){  
//                                if(listMembers[i].T_USERID!=list[k].T_USERID){
//                                    num++;
//                                }
//                            }
//                            if(num== list.length){         
//                                if(count%4==0){
//                                    checkboxs+="<input type='checkbox' name='checkbox' value='"+listMembers[i].T_USERID+"'>&nbsp;&nbsp;&nbsp;"+listMembers[i].T_USERNAME+'&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>';
//                                }else{
//                                   checkboxs+="<input type='checkbox' name='checkbox' value='"+listMembers[i].T_USERID+"'>&nbsp;&nbsp;&nbsp;"+listMembers[i].T_USERNAME+'&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;'; 
//                                }
//                            }
//                        }
//                    }else{
//                        for(var i=0;i<listMembers.length;i++){       
//                            if(i%4==0&&i!=0){
//                                checkboxs+="<input type='checkbox' name='checkbox' value='"+listMembers[i].T_USERID+"'>&nbsp;&nbsp;&nbsp;"+listMembers[i].T_USERNAME+'&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>';
//                            }else{
//                                checkboxs+="<input type='checkbox' name='checkbox' value='"+listMembers[i].T_USERID+"'>&nbsp;&nbsp;&nbsp;"+listMembers[i].T_USERNAME+'&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;'; 
//                            }                        
//                        } 
//                    }
//                    $("#dv_Members").html(checkboxs);
//                    $("#dv_Members_Parents").show();
//                    btn_num_add=1;
//                }else{
//                    $('#add').linkbutton({disabled:true});
//                    btn_num_add=0;
//                    $("#tt").tabs("select", "人员信息");
//                    $("#dv_Member_Info").hide();
//                    $("#dv_Member_Info_Edit").show();
//                    $("#dv_Member_Info_Remove").hide();
//                    $("#dv_Members_Parents").hide();
//                }
//            }, 'json');
		}		

		$(document).ready(function(){	  
		  
            $("#txtTreeID").hide();
            $("#txtTreeName").hide();
            $("#txtTreeFatherID").hide();
            
		    $('#add').linkbutton({text:'新建'});
		    $('#save').linkbutton({text:'编辑'});
		    $('#delete').linkbutton({text:'删除'});
		    		
		    $('#add').linkbutton({disabled:true});
		    $('#save').linkbutton({disabled:true});
		    $('#delete').linkbutton({disabled:true});
		   
            $("#dv_Parent_Info").hide();
            $("#dv_Parent_Info_father").hide();            
            $("#dv_Edit_Parent").hide();
            $("#txtID_Old_Edit").hide();
            $("#dv_Edit_Remove").hide();          

            $("#txt_Meber_Id").hide();            
            $("#txt_Members_ID").hide();
            $("#dv_Member_Info_Edit").hide();
            $("#txt_Meber_Id_Edit").hide();

            $("#Par").empty();
            $("#Par").append("<option value='0'>主岗</option>");
            $("#Par").append("<option value='1'>副岗</option>");
                    
            $.post("RoleList.aspx", { param: ''}, function (data) {
//                if(Number(data.count)==1){
                    var  nodeEval = eval(data.menu);
                    $.fn.zTree.init($("#treeDemo"), setting, nodeEval);
                    $("#dv_Parent_Info").show();
                    //绑定值别信息
//                    var list = data.list;  
//                    $("#slclass").empty();
//                    for(var i=0;i<list.length;i++){
//                        $("#slclass").append("<option value='"+list[i].T_CLASSID+"'>"+list[i].T_CLASSNAME+"</option>");
//                    }
//                    $("#slclass").append("<option value='0000'>长白班</option>");
//                    //绑定人员信息listMember
//                    var listMember=data.listMember;

//                    if(listMember==null){
//                        $("#dv_Members_Parents").hide();
//                    }else{
//                        var checkboxs='';
//                        for(var i=0;i<listMember.length;i++){
//                            if(i%4==0&&i!=0){
//                                checkboxs+="<input type='checkbox' name='checkbox' value='"+listMember[i].T_USERID+"'>&nbsp;&nbsp;&nbsp;"+listMember[i].T_USERNAME+'&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>';
//                            }else{
//                               checkboxs+="<input type='checkbox' name='checkbox' value='"+listMember[i].T_USERID+"'>&nbsp;&nbsp;&nbsp;"+listMember[i].T_USERNAME+'&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;'; 
//                            }
//                        }
//                        $("#dv_Members").html(checkboxs);
//                        $("#dv_Members_Parents").show();
//                    }
//                }else
//                {
//                    $("#dv_Parent_Info_father").show();
//                }
//                $("#dv_Member_Info").show();
                
            }, 'json');
            
            $("#BtnLoad").click(function() {          
                $.post("RoleList.aspx", { param: 'Load',id:$("#txtID").val(),img:escape($("#flImg_Edit").val())}, function (data) {

                }, 'json');
            }); 
            
            $("#save").click(function() {              
                $.post("RoleList.aspx", { param: 'Edit',id:$("#txtID_Old_Edit").val()}, function (data) {
                    var list = data.list;
                    var listClass = data.listClass; 
                    if(list!=null){
                        $("#tt").tabs("select", "人员信息");
                        $("#dv_Member_Info").hide();
                        $("#dv_Member_Info_Edit").show();
                        $("#dv_Member_Info_Remove").hide();
                        if(data.img.length>0){    
                            $("#img_").show();                
                            $("#img_").attr("src",data.img);
                        }else{
                            $("#img_").hide();
                        }
                        $("#txt_Member_UserPassWord_Edit").val(list[0].T_PASSWD);
                        $("#txt_Member_UserName_Edit").val(list[0].T_USERID);                        
                        $("#txt_Meber_Name_Edit").val(list[0].T_USERNAME);                       
                        
                        $("#slclass_Edit").empty();
                        var count=0;
                        for(var i=0;i<listClass.length;i++){
                            if(list[0].T_CLASSID==listClass[i].T_CLASSID){
                                count=i;
                                $("#slclass_Edit").append("<option value='"+listClass[i].T_CLASSID+"' selected='selected'>"+listClass[i].T_CLASSNAME+"</option>");
                            }else{
                                $("#slclass_Edit").append("<option value='"+listClass[i].T_CLASSID+"'>"+listClass[i].T_CLASSNAME+"</option>");
                            }
                        }                        
                        if(count==listClass.length){
                            $("#slclass_Edit").append("<option value='0000' selected='selected'>长白班</option>");
                        }else{
                            $("#slclass_Edit").append("<option value='0000'>长白班</option>");
                        }
                    }else{
                        $("#tt").tabs("select", "部门信息");
                        $("#dv_Parent_Info").hide();
                        $("#dv_Parent_Info_father").hide();
                        $("#dv_Edit_Parent").show();
                        $("#dv_Edit_Remove").hide();
                    }
                }, 'json');
            }); 
            
            $("#delete").click(function() {
                if(btn_num_remove!=0){ 
                    $.post("RoleList.aspx", { param: 'Edit',id:$("#txtTreeID").val()}, function (data) {
                        var list = data.list;
                        var listClass = data.listClass; 
                        if(list!=null){
                            $("#tt").tabs("select", "人员信息");
                            $("#dv_Member_Info").hide();
                            $("#dv_Member_Info_Edit").hide();
                            $("#dv_Member_Info_Remove").show();
      
                            if(data.img.length>0){    
                                $("#img_Remove").show();                
                                $("#img_Remove").attr("src",data.img);
                            }else{
                                $("#img_Remove").hide();
                            }
                            $("#txtPwd_Remove").val(list[0].T_PASSWD);
                            $("#txtNameMember_Remove").val(list[0].T_USERID);                        
                            $("#txtTrueName_Remove").val(list[0].T_USERNAME);                       
                            
                            $("#slclass_Edit").empty();
                            var count='';
                            for(var i=0;i<listClass.length;i++){
                                if(list[0].T_CLASSID==listClass[i].T_CLASSID){
                                    $("#slclass_Remove").val(listClass[i].T_CLASSNAME);
                                    break;
                                }else{
                                     $("#slclass_Remove").val('长白班');  
                                }
                            }                            
                        }else{
                            $("#tt").tabs("select", "部门信息");
                            $("#dv_Parent_Info").hide();
                            $("#dv_Parent_Info_father").hide();
                            $("#dv_Edit_Parent").hide();
                            $("#dv_Edit_Remove").show();
                        }
                    }, 'json');
                }
            });  
            
            //添加组织关系
            $("#btnOK").click(function() {      
                if($("#txtID").val()=="" || $("#txtID").val()==null ||escape($("#txtName").val())==null||escape($("#txtName").val())==""){
                    $.messager.alert('添加组织关系','组织编码和组名称都不能为空!','error');
                }else{
                    $.post("RoleList.aspx", { param: 'AddOrganize',parentID:$("#txtTreeID").val(),id:$("#txtID").val(),name:escape($("#txtName").val())}, function (data) {
                        $.post("RoleList.aspx", { param: ''}, function (data) {
                            if(Number(data.count)==1){
                                var  nodeEval = eval(data.menu);
                                $.fn.zTree.init($("#treeDemo"), setting, nodeEval);
                                $("#dv_Parent_Info").show();
                            }else
                            {
                                $("#dv_Parent_Info_father").show();
                            }
                        }, 'json');
                        $("#txtID").val('');
                        $("#txtName").val('');
                        if(Number(data.judge)==1){
                            $.messager.alert('添加组织关系',data.info,'warning');
                            $("#txtID").val('');
                            $("#txtName").val('');
                        }else{
                            $.messager.alert('添加组织关系',data.info,'info');
                        }

                    }, 'json');
                }
            });  
            //重置组织关系
            $("#btnReload").click(function() {           
                $("#txtID").val('');
                $("#txtName").val('');
            });  
            //添加组织关系
            $("#btnOK_F").click(function() {      
                if($("#txtID_F").val()=="" || $("#txtID_F").val()==null ||escape($("#txtName_F").val())==null||escape($("#txtName_F").val())==""){
                    $.messager.alert('添加组织关系','组织编码和组名称都不能为空!','error');
                }else{
                    $.post("RoleList.aspx", { param: 'AddOrganize',parentID:'0',id:$("#txtID_F").val(),name:escape($("#txtName_F").val())}, function (data) {
                        $.post("RoleList.aspx", { param: ''}, function (data) {
                            if(Number(data.count)==1){
                                var  nodeEval = eval(data.menu);
                                $.fn.zTree.init($("#treeDemo"), setting, nodeEval);
                                $("#dv_Parent_Info").show();
                            }else
                            {
                                $("#dv_Parent_Info_father").show();
                            }
                        }, 'json');
                        $.messager.alert('添加组织关系',data.info,'info');
                    }, 'json');
                }
            });  
             //重置组织关系
            $("#btnReload_F").click(function() {           
                $("#txtID_F").val('');
                $("#txtName_F").val('');
            });  
            //编辑组织关系
            $("#btnParentEdit").click(function() {      
                if($("#txtId_Edit").val()=="" || $("#txtId_Edit").val()==null ||escape($("#txtName_Edit").val())==null||escape($("#txtName_Edit").val())==""){
                    $.messager.alert('编辑组织关系','编辑组织关系时组织编码和组名称都不能为空!','error');
                }else{
                    $.post("RoleList.aspx", { param: 'EditOrganize',oid:$("#txtID_Old_Edit").val(),id:$("#txtId_Edit").val(),name:escape($("#txtName_Edit").val())}, function (data) {
                        $.post("RoleList.aspx", { param: ''}, function (data) {
                            var  nodeEval = eval(data.menu);
                            $.fn.zTree.init($("#treeDemo"), setting, nodeEval);
                        }, 'json');
                        $.messager.alert('编辑组织关系',data.info,'info');
                    }, 'json');
                }
            });  
            //取消编辑组织关系
            $("#btnParentReload").click(function() {           
                $("#txtId_Edit").val('');
                $("#txtName_Edit").val('');
            });
            
            //删除组织关系
            $("#dv_Edit_Remove").click(function() {     
	            $.messager.confirm('删除组织关系', '你确定要删除 '+$("#txtName_Remove").val()+'  吗?', function(ok){
		            if (ok){
                        $.post("RoleList.aspx", { param: 'RemoveOrganize',id:$("#txtID_Remove").val()}, function (data) {
                            $.post("RoleList.aspx", { param: ''}, function (data) {
                                var  nodeEval = eval(data.menu);
                                $.fn.zTree.init($("#treeDemo"), setting, nodeEval);
                            }, 'json');
                            $.messager.alert('删除组织关系',data.info,'info');
                        }, 'json');
		            }else{
		                $.messager.alert('删除组织关系','删除已取消!','info');
		            }
	            });            
            });    
                       
            //添加人员信息
            $("#btn_Member_OK").click(function() {      
                if($("#txt_Meber_Id").val()=="" || $("#txt_Meber_Id").val()==null ){
                    $.messager.alert('添加人员信','所添加的人员必须有隶属的组织关系!','info');
                }else{
                    if($("#txt_Member_UserName").val()=="" || $("#txt_Member_UserName").val()==null ||escape($("#txt_Member_UserPassWord").val())==null||escape($("#txt_Member_UserPassWord").val())==""||escape($("#txt_Meber_Name").val())==null||escape($("#txt_Meber_Name").val())==""){
                        $.messager.alert('添加人员信','用户名 密码 真实姓名都不能为空!','info');
                    }else{
                        $.post("RoleList.aspx", { param: 'JudgeMember',userName:escape($("#txt_Member_UserName").val()),name:escape($("#txt_Meber_Name").val())}, function (data) {
                            if(Number(data.judge)==1&&Number(data.num)==1){
                                $.messager.alert('添加人员信','已经存在用户名为：'+$("#txt_Member_UserName").val()+'  并且真实姓名为：'+$("#txt_Meber_Name").val()+'  的人员,不能重复添加!','error');
                            }else if(Number(data.judge)==1){
                                $.messager.alert('添加人员信','已经存在用户名为：'+$("#txt_Member_UserName").val()+'  的人员,不能重复添加!','error');
                            }else if(Number(data.num)==1){
                                $.messager.confirm('添加人员信', '你确定要重复添加真实姓名为：'+$("#txt_Meber_Name").val()+'  的人员吗?', function(ok){
                                    if (ok){
                                        $.post("RoleList.aspx", { param: 'AddMember',classs:$("#slclass").val(),userName:escape($("#txt_Member_UserName").val()),pwd:escape($("#txt_Member_UserPassWord").val()),name:escape($("#txt_Meber_Name").val()),
                                        img:escape($("#flImg").val()),parentId:$("#txt_Meber_Id").val(),par:$("#Par").val()}, function (data) {
                                            $.messager.alert('添加人员信',data.info,'info');
                                        }, 'json'); 
                                    }
                                });            
                            }else{
                                $.post("RoleList.aspx", { param: 'AddMember',classs:$("#slclass").val(),userName:escape($("#txt_Member_UserName").val()),pwd:escape($("#txt_Member_UserPassWord").val()),name:escape($("#txt_Meber_Name").val()),
                                img:escape($("#flImg").val()),parentId:$("#txt_Meber_Id").val(),par:$("#Par").val()}, function (data) {
                                    $.messager.alert('添加人员信',data.info,'info');
                                }, 'json'); 
                            }
                        }, 'json'); 
                    }
                }
            }); 
            
             //人员分岗
            $("#btn_Members_Ok").click(function() {   
                if($("#txt_Members_ID").val()==null ||$("#txt_Members_ID").val()==''){
                    $.messager.alert('人员分岗','人员分岗时,岗位信息不能为空!','info');
                }else{
                    var membersId='';   
                    $("input[type=checkbox][checked]").each(function(){ //由于复选框一般选中的是多个,所以可以循环输出 
                        membersId+=$(this).val()+',';
                    }); 
                    $.post("RoleList.aspx", { param: 'AddMembersParment',id:$("#txt_Members_ID").val(),membersId:membersId}, function (data) {
                        $.post("RoleList.aspx", { param: ''}, function (data) {
                            if(Number(data.count)==1){
                                var  nodeEval = eval(data.menu);
                                $.fn.zTree.init($("#treeDemo"), setting, nodeEval);
                                $("#dv_Parent_Info").show();
                            }else
                            {
                                $("#dv_Parent_Info_father").show();
                            }
                        }, 'json');
                        $.messager.alert('人员分岗',data.info,'info');
                    }, 'json'); 
                 }
            });
            //编辑人员信息
            $("#btn_Member_OK_Edit").click(function() {  
                $("#txtTreeID").val($("#txt_Member_UserName_Edit").val());
                if($("#txt_Meber_Name_Edit").val()==null ||$("#txt_Meber_Name_Edit").val()==''||$("#txt_Member_UserName_Edit").val()==null ||$("#txt_Member_UserName_Edit").val()==''||$("#txt_Member_UserPassWord_Edit").val()==null ||$("#txt_Member_UserPassWord_Edit").val()==''){
                    $.messager.alert('编辑人员信息','编辑人员信息时：用户名、密码和真实姓名都不能为空!','info');
                }else{
                    if($("#txtTreeID").val()==$("#txt_Member_UserName_Edit").val()){
                        $.post("RoleList.aspx", { param: 'EditMemberInfo',oldId:$("#txtTreeID").val(),id:$("#txt_Member_UserName_Edit").val(),pwd:escape($("#txt_Member_UserPassWord_Edit").val()),
                        trueName:escape($("#txt_Meber_Name_Edit").val()),img:escape($("#flImg_Edit").val()),classId:$("#slclass_Edit").val()}, function (data) {
                            $.messager.alert('人员分岗',data.info,'info');
                        }, 'json');     
                    }else{
                        $.post("RoleList.aspx", { param: 'JudgeMemberInfo',id:$("#txt_Member_UserName_Edit").val()}, function (data) {
                            if(Number(data.judge)==1){
                                 $.messager.alert('编辑人员信息','已经存在用户名为：'+$("#txtTreeName").val()+'  的人员,用户名不能重复!','error');
                            }else{
                                $.post("RoleList.aspx", { param: 'EditMemberInfo',oldId:$("#txtTreeID").val(),id:$("#txt_Member_UserName_Edit").val(),pwd:escape($("#txt_Member_UserPassWord_Edit").val()),
                                trueName:escape($("#txt_Meber_Name_Edit").val()),img:escape($("#flImg_Edit").val()),classId:$("#slclass_Edit").val()}, function (data) {
                                    $.messager.alert('人员分岗',data.info,'info');
                                }, 'json');   
                            }
                        }, 'json');   
                    }
                }
            }); 
            //删除人员信息
            $("#btnMemberRemove").click(function() { 
                $.messager.confirm('删除人员信息', '你确定要删除 '+$("#txtTreeName").val()+'  的信息吗?如果删除,将会删除所有岗位下的该人员,并清空该人员信息!', function(ok){
                    if (ok){
                        $.post("RoleList.aspx", { param: 'RemoveMemberInfo',id:$("#txtTreeID").val()}, function (data) {
                            $.post("RoleList.aspx", { param: ''}, function (data) {
                                var  nodeEval = eval(data.menu);
                                $.fn.zTree.init($("#treeDemo"), setting, nodeEval);
                            }, 'json');
                            $.messager.alert('删除人员信息',data.info,'info');
                        }, 'json'); 
                    }else{
                        $.messager.alert('删除人员信息','删除已取消!','info');
                    }
                });  
            });
		});
		//-->
    </script>

</head>
<body topmargin="10" >
<body >
    <form id="form1" runat="server">
    <div id="menu" >
    <table width="100%" height="90%" border="0" cellpadding="0" cellspacing="0"  bgcolor="#2a88bb" >
    <tr>
		<td colspan="3" background="../img/table-head.jpg" height="30px" valign="middle" class="style6">&nbsp;权限管理</td>
	</tr>
    <tr>
    <td valign="top" style="background-color: White" height="100%">
    <div>
    <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0"  bgcolor="#2a88bb" >
    <tr>
		<td background="../img/table-head-2.jpg" height="25px" valign="middle" class="style5">&nbsp;菜单列表</td>
		</tr>
		<tr>
            <td style="background-color: White" align="left" valign="top"><ul id="treeDemo" class="ztree"></ul></td> 
            </tr>
	</table>
	</div>
	</td>
	<td background="../img/table-head-3.jpg" width="1px"></td>
		<td valign="top">
    <iframe id="ifrPower" name="ifrPower" src="ManagePower.aspx" height="100%" width="100%" frameborder="0" scrolling="0"></iframe>
    </td>                                
            </tr>
            
    
    </table>
    </div>
    </form>
</body>
</html>
