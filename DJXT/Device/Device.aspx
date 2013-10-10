<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Device.aspx.cs" Inherits="DJXT.Device.Device" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>设备维护</title>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/css/djxt.css" rel="stylesheet" type="text/css" />
    <style>
        .formbutton { 
	    color: #135294; 
	    border:1px 
	    solid #666; 
	    Width:67px;
	    text-align:center;
	    font-weight:normal;
	    /*line-height:100%; */
	    background:url("../img/button_bg.gif");
	    cursor:hand;
	    }
        .black_overlay
        {
            display: none;
            position: absolute;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 100%;
            background-color: black;
            z-index: 1001;
            -moz-opacity: 0.8;
            opacity: .80;
            filter: alpha(opacity=80);
        }
        .white_content
        {
            display: none;
            position: absolute;
            top: 20%;
            left: 20%;
            width: 60%;
            height: 60%;
            border: 16px solid lightblue;
            background-color: white;
            z-index: 1002;
            overflow: auto;
        }
        .white_content_small
        {
            display: none;
            position: absolute;
            top: 20%;
            left: 50%;
            width: 30%;
            height: 50%;
            border: 16px solid lightblue;
            background-color: white;
            z-index: 1002;
            overflow: auto;
        }
        
        .formbutton1 { 
	    color: #135294; 
	    border:1px 
	    solid #666; 
	    Width:67px;
	    text-align:center;
	    font-weight:normal;
	    /*line-height:100%; */
	    background:url("../img/button_bg.gif");
	    cursor:hand;
	    }
        .black_overlay1
        {
            display: none;
            position: absolute;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 100%;
            background-color: black;
            z-index: 1001;
            -moz-opacity: 0.8;
            opacity: .80;
            filter: alpha(opacity=80);
        }
        .white_content1
        {
            display: none;
            position: absolute;
            top: 20%;
            left: 20%;
            width: 60%;
            height: 20%;
            border: 16px solid lightblue;
            background-color: white;
            z-index: 1002;
            overflow: auto;
        }
        .white_content_small1
        {
            display: none;
            position: absolute;
            top: 20%;
            left: 50%;
            width: 30%;
            height: 50%;
            border: 16px solid lightblue;
            background-color: white;
            z-index: 1002;
            overflow: auto;
        }
    </style>    
    <script src="../jQueryEasyUI/jquery-1.6.2.js" type="text/javascript"></script>

    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>

    <script src="../js/jquery.ztree.core-3.5.js" type="text/javascript"></script>

    <script type="text/javascript" src="../js/jquery.ztree.excheck-3.5.js"></script>

    <script type="text/javascript" src="../js/My97DatePicker/WdatePicker.js"></script>
    
    <script type="text/javascript" src="../jQueryEasyUI/plugins/jquery.combobox.js"></script>
    <script type="text/javascript" src="../jQueryEasyUI/plugins/jquery.combo.js"></script>
    <script type="text/javascript" src="../jQueryEasyUI/plugins/jquery.combogrid.js"></script>
    
    <script>
        var users = {total:6,rows:[
            {no:1,name:'用户1',addr:'山东济南',email:'user1@163.com',birthday:'1/1/1980'},
            {no:2,name:'用户2',addr:'山东济南',email:'user2@163.com',birthday:'1/1/1980'},
            {no:3,name:'用户3',addr:'山东济南',email:'user3@163.com',birthday:'1/1/1980'},
            {no:4,name:'用户4',addr:'山东济南',email:'user4@163.com',birthday:'1/1/1980'},
            {no:5,name:'用户5',addr:'山东济南',email:'user5@163.com',birthday:'1/1/1980'},
            {no:6,name:'用户6',addr:'山东济南',email:'user6@163.com',birthday:'1/1/1980'}
        ]};
        $(function(){
          
        });
        var editcount = 0;
        function editrow(index){
            $('#dgDev').datagrid('beginEdit', index);
        }
        function deleterow(index){
            $.messager.confirm('确认','是否真的删除?',function(r){
                if (r){
                    $('#dgDev').datagrid('deleteRow', index);
                }
            });
        }
        function saverow(index){
            $('#dgDev').datagrid('endEdit', index);
            
            alert($('#JSON_t_statusdesc').val())
            
            $.post("Device.aspx", { param: 'AddStatus',id:treeNode.id}, function (data) {
                
            });
        }
        function cancelrow(index){
            $('#dgDev').datagrid('cancelEdit', index);
        }
        function addrow(){
//            if (editcount > 0){
//                $.messager.alert('警告','当前还有'+editcount+'记录正在编辑，不能增加记录。');
//                return;
//            }
//            $('#dgDev').datagrid('appendRow',{
//                JSON_t_deviceid:{ type: 'text' },
//                name:{ type: 'text' },
//                addr:{ type: 'text' },
//                email:{ type: 'text' },
//                birthday:{ type: 'text' }
//            });
        
//                if (endEditing()){  
//                $('#dgDev').datagrid('appendRow',{status:'P'});  
//                editIndex = $('#dgDev').datagrid('getRows').length-1;  
//                $('#dgDev').datagrid('selectRow', editIndex)  
//                        .datagrid('beginEdit', editIndex);  
                
                ShowDiv('MyItem', 'fade');
              

        }
        
         function addrowItem(){
//            if (editcount > 0){
//                $.messager.alert('警告','当前还有'+editcount+'记录正在编辑，不能增加记录。');
//                return;
//            }
//            $('#dgDev').datagrid('appendRow',{
//                JSON_t_deviceid:{ type: 'text' },
//                name:{ type: 'text' },
//                addr:{ type: 'text' },
//                email:{ type: 'text' },
//                birthday:{ type: 'text' }
//            });
        
//                if (endEditing()){  
//                $('#dgDev').datagrid('appendRow',{status:'P'});  
//                editIndex = $('#dgDev').datagrid('getRows').length-1;  
//                $('#dgDev').datagrid('selectRow', editIndex)  
//                        .datagrid('beginEdit', editIndex);  
                 
                ShowDiv('MyItems', 'fade');
              

        }
        function saveall(){            
            $('#ItemIDKEY').val('')
            var row = $('#dgDev').datagrid('getSelected');  
            if (row){ 
                
                $('#ItemIDKEY').val(row.JSON_id_key1);
                $('#ItemDateEdit').val(row.JSON_t_time);
                $('#ItemSelEdit').val(row.JSON_i_statusid);
                
                ShowDiv('MyItemEdit', 'fade'); 
            }else{
                alert("请选择要操作的行!");
            }           
        }
         function saveallItem(){
            $('#idkeyItems').val('')
            var row = $('#dgItem').datagrid('getSelected');  
            if (row){ 
                
                $('#idkeyItems').val(row.JSON_id_key);
                $('#DJXBWEdit').val(row.JSON_t_itemposition);
                $('#DJXDESCEdit').val(row.JSON_t_itemdesc);
                $('#DJXCONTENTEdit').val(row.JSON_t_content);
                $('#DJXObserveEdit').val(row.JSON_t_observe);
                $('#DJXUnitEdit').val(row.JSON_t_unit);
                $('#DJXTypeEdit').val(row.JSON_t_type);
                $('#DJXSelectStatusEdit').val(row.JSON_i_status);
                $('#DJXT_SATAUSEdit').val(row.JSON_t_status);
                $('#DJXUpperEdit').val(row.JSON_f_upper);
                $('#DJXLowerEdit').val(row.JSON_f_lower);
                $('#DJXSpectrumEdit').val(row.JSON_i_spectrum);
                $('#DJXSTARTTIMEEdit').val(row.JSON_t_starttime);
                $('#DJXPERIODVALUEEdit').val(row.JSON_t_periodvalue);
                $('#Select1Edit').val(row.JSON_t_periodtype);               
                
                ShowDiv('divEditItem', 'fade'); 
            }else{
                alert("请选择要操作的行!");
            } 
        }
        function cancelall(){
            $('#dgDev').datagrid('rejectChanges');
        }
    </script>
    
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
	    var setPerson={
	        data: {
				key: {
					title:"t",checked: "isChecked"
				},
				simpleData: {
					enable: true
				}
			},
			callback: {
		        onCheck: zTreeOnCheck
	        },
			check: {enable: true,chkStyle: "radio",radioType: "all"
		    }
	    };
	    var setDevice={
	        data: {
				key: {
					title:"t"
				},
				simpleData: {
					enable: true
				}
			},
			callback: {
		        onCheck: zTreeOnCheck
	        },
			check: {enable: true,chkStyle: "checkbox",chkboxType:{ "Y":"", "N":"" }
		    }
	    };
	   function zTreeOnCheck(event, treeId, treeNode) {
          
           $('#treePersonNodeID').val(treeNode.id);
           $('#txtLineGw').val(treeNode.name);
           $('#txtLineGwEdit').val(treeNode.name);          
       }; 
       
		function onClick(event, treeId, treeNode, clickFlag) {
		   $.post("Device.aspx", { param: 'clickTree',id:treeNode.id}, function (data) {
                
                $("#tt").hide();
                $("#tt1").show();
            
                $('#add').linkbutton({disabled:true});
		        $('#save').linkbutton({disabled:false});
		        $('#delete').linkbutton({disabled:true});
		        
                if($("#dv_Parent_Info").is(":visible")==true)
                {
                    $('#add').linkbutton({disabled:true});
		            $('#save').linkbutton({disabled:true});
		            $('#delete').linkbutton({disabled:true});
                }
                 
                if($("#dv_Member_Info").is(":visible")==true)
                {
                     $('#add').linkbutton({disabled:false});
		             $('#save').linkbutton({disabled:false});
		             $('#delete').linkbutton({disabled:true});
                }
                 
                if($("#dv_Device").is(":visible")==true)
                {
                     $('#add').linkbutton({disabled:false});
		             $('#save').linkbutton({disabled:false});
		             $('#delete').linkbutton({disabled:true});
                }
                
                if(Number(data.count)==1){
			        
			        document.getElementById("lblInfoName").innerText=data.sbName;
                    document.getElementById("lblInfoFile").innerText=data.file;
                    document.getElementById("lblInfoStatus").innerText=data.status;
                    
		            $('#txtSBName').val(data.sbName);
	                $('#txtDeviceId').val(treeNode.id);
	                $('#txtDeviceName').val(escape(data.sbName));
           
                    if($("#dv_Member_Info").is(":visible")==true)
                    {                        
                    }
	                	                
	                //对select
	                var list=data.list;
	                
                    if(list!=null){}
                }else{ 
		        }           
               
                $('#NodeCheckedID').val(treeNode.id);
              
            }, 'json');
		}		

		$(document).ready(function(){	    

            var products = [
            {id:'0',name:'在用'},
		    {id:'1',name:'停用'},
		    {id:'2',name:'退役'},
		    {id:'3',name:'报废'},
		    {id:'4',name:'删除'}		   
		    ];
		    
		    $('#add').linkbutton({text:'新建'});
		    $('#save').linkbutton({text:'编辑'});
		    $('#delete').linkbutton({text:'删除'});
		    		
		    $('#add').linkbutton({disabled:true});
		    $('#save').linkbutton({disabled:true});
		    $('#delete').linkbutton({disabled:true});
		   
		    $("#txtParentId").hide();
            $("#dv_Parent_Info").hide();
            $("#dv_Parent_Info_father").hide();            
            $("#dv_Edit_Parent").hide();
            $("#txtID_Old_Edit").hide();
            $("#dv_Edit_Remove").hide();
            $("#dv_Members_Parents").hide();
            
            $("#txt_Meber_Id").hide();            
            $("#txt_Members_ID").hide();
            
            $("#tabID").val('1');
            
            $("#tt").hide();
            $("#tt1").show();
                      
            $('#tt').tabs({
                onSelect: function (title) {
                    if(title=='附件管理')
                    {}
                    else if(title=='状态维护')
                    {   
                                            
                         $('#dgDev').datagrid({
                            iconCls:'icon-edit',
                            width:670,
                            height:378,
                            url: location.href,
                            sortName: 'JSON_id_key', //排序字段
                            idField: 'JSON_id_key', //标识字段,主键
                            singleSelect:true,
                            columns:[[                                
                                {field:'JSON_t_deviceid',title:'设备编码',width:100},
                                {field:'JSON_t_devicedesc',title:'设备名称',width:120},
                                {field:'JSON_t_time',title:'时间',width:120,
                                    editor:{ type: 'text' }
                                },
                                {field:'JSON_i_statusid',title:'设备状态ID',width:120,hidden:true,
                                    editor:{ type: 'text' }
                                },
                                {field:'JSON_id_key1',title:'',width:120,hidden:true,
                                    editor:{ type: 'text' }
                                },
                                {field:'JSON_t_statusdesc',title:'设备状态',width:140,editor:{
                                     type:'combobox',  
                                        options:{                                          
                                        valueField:'id',
								        textField:'name',   //valueField:'JSON_i_statusid',//'JSON_i_statusid',  
                                        //textField:'JSON_t_statusdesc',//'JSON_t_statusdesc',
                                        data:products,//'../combobox_data1.json',//'../Device/Device.aspx?param=status',//
                                        required:true,
                                        editable:false  
                                        }
                                }}
                            ]],
                            toolbar:[{
                                text:'增加',
                                iconCls:'icon-add',
                                handler:addrow
                            },{
                                text:'编辑',
                                iconCls:'icon-remove',
                                handler:saveall
                            }],
                            onBeforeEdit:function(index,row){
                                row.editing = true;
                                $('#dgDev').datagrid('refreshRow', index);
                                editcount++;
                            },
                            onAfterEdit:function(index,row){
                                row.editing = false;
                                $('#dgDev').datagrid('refreshRow', index);
                                editcount--;
                            },
                            onCancelEdit:function(index,row){
                                row.editing = false;
                                $('#dgDev').datagrid('refreshRow', index);
                                editcount--;
                            },
                            queryParams: { "param": "query","SBID":$('#NodeCheckedID').val()},
                            pagination: true, //是否开启分页
                            pageNumber: 1, //默认索引页
                            pageSize: 10, //默认一页数据条数
                            rownumbers: true //行号
                        }).datagrid('loadData',users).datagrid('acceptChanges');                           
                           
                    }
                    else if(title=='点检项维护')
                    {  
                         $('#dgItem').datagrid({
                            iconCls:'icon-edit',
                            width:670,
                            height:378,
                            url: location.href,
                            sortName: 'JSON_id_key', //排序字段
                            idField: 'JSON_id_key', //标识字段,主键
                            singleSelect:true,
                             frozenColumns: [[//冻结的列，不会随横向滚动轴移动
    	                    { title: '点检项描述', field: 'JSON_t_itemdesc', width: 100, sortable: true },
                            { title: '点检项部位', field: 'JSON_t_itemposition', width: 100, sortable: true }                            
			            ]],
                        columns: [[
                            { title: '观察名称', field: 'JSON_t_observe', width: 100 },   
                            { title: '单位', field: 'JSON_t_unit', width: 100 }, 
                            { title: '测量上限', field: 'JSON_f_upper', width: 80 },
                            { title: '测量下限', field: 'JSON_f_lower', width: 80 },
                            { field:'JSON_t_status',title:'',width:120,hidden:true},
                            { field:'JSON_i_status',title:'',width:120,hidden:true},
                            { field:'JSON_i_spectrum',title:'',width:120,hidden:true},
                            { field:'JSON_t_starttime',title:'',width:120,hidden:true},
                            { field:'JSON_t_periodvalue',title:'',width:120,hidden:true},
                            { field:'JSON_t_periodtype',title:'',width:120,hidden:true},
//                            { field:'JSON_id_key',title:'',width:120,hidden:true},
//                            { field:'JSON_id_key',title:'',width:120,hidden:true},
                            { title: '类型', field: 'JSON_t_type',formatter:function(value,rec,index){return value==0?'点检':'巡检'},width: 80 },                                
                            { title: '操作', field: 'JSON_id_key', width: 80, formatter: function (value, rec) {
                                return '<a style="color:red" href="../Device/msgInfo.aspx?id='+value+'" target="_blank">查看记录</a>';                               
                            }
                            }
                            ]],                          
                            toolbar:[{
                                text:'增加',
                                iconCls:'icon-add',
                                handler:addrowItem
                            },{
                                text:'编辑',
                                iconCls:'icon-remove',
                                handler:saveallItem
                            }],
                            onBeforeEdit:function(index,row){
                                row.editing = true;
                                $('#dgItem').datagrid('refreshRow', index);
                                editcount++;
                            },
                            onAfterEdit:function(index,row){
                                row.editing = false;
                                $('#dgItem').datagrid('refreshRow', index);
                                editcount--;
                            },
                            onCancelEdit:function(index,row){
                                row.editing = false;
                                $('#dgItem').datagrid('refreshRow', index);
                                editcount--;
                            },
                            queryParams: { "param": "queryItem","SBID":$('#NodeCheckedID').val()},
                            pagination: true, //是否开启分页
                            pageNumber: 1, //默认索引页
                            pageSize: 10, //默认一页数据条数
                            rownumbers: true //行号
                        }).datagrid('loadData',users).datagrid('acceptChanges');                      
                    } 
                }
            });
             
            $.post("../Device/Device.aspx", { param: ''}, function (data) {
                if(Number(data.count)==1){
                    var  nodeEval = eval(data.menu);
                    $.fn.zTree.init($("#treeDemo"), setting, nodeEval); 
                        $.post("../Device/Device.aspx", { param: 'load',SBID:''}, function (data) {
                            if(Number(data.count)==0){
                                 document.getElementById("lblInfoName").innerText=data.sbName;
                                 document.getElementById("lblInfoFile").innerText=data.file;
                                 document.getElementById("lblInfoStatus").innerText=data.status;
                            }                
                        }, 'json');                                      
                }                
            }, 'json');
            
            $("#add").click(function() {          
               
                $("#dv_Parent_Info").show();
                $("#dv_Parent_Info_father").hide();   
                $("#dv_Edit_Parent").hide();
                $("#dv_Edit_Remove").hide();
                
//                if($("#dv_Member").is(":visible")==true)
//                {
//                    $.post("DeviceNew.aspx", { param: 'clickTree',id:$('#NodeCheckedID').val()}, function (data) {                
//                        if(Number(data.count)==1){
//                           ShowDiv('MyItem', 'fade');
//                        }                   
//                    }, 'json');
//                }else 
//                if($("#dv_Device").is(":visible")==true)
//                {
//                    //弹出新建点检项按钮                   
//                    ShowDiv('MyItems', 'fade');
//                }                
                
            }); 
            
            $("#save").click(function() {    
                if($('#NodeCheckedID').val()!=''){
                     $("#tt").show();
                     $("#tt1").hide();
                     $('#save').linkbutton({disabled:true});
                     $("#dv_Parent_Info").show();
                     $('#tt').tabs('select', "附件管理")
                }
            }); 
            
            $("#delete").click(function() {   
               
            });  
            
            //上传附件
            $("#saveFile").click(function() {  
                //alert($("#flUp").val()); //
                if($("#NodeCheckedID").val()=="" || $("#txtSBName").val()==null || $("#NodeCheckedID").val()==null ||escape($("#txtSBName").val())==null||escape($("#txtSBName").val())=="" || escape($("#NodeCheckedID").val())==""  ||escape($("#flUp").val())==null||escape($("#flUp").val())==""){
                    $.messager.alert('上传附件','请选择需要上传的设备和附件!','error');
                }else{
                    $.post("Device.aspx", { param: 'AddFile',SBID:$("#NodeCheckedID").val(),SBName:escape($("#txtSBName").val()),flPath:escape($("#flUp").val())}, function (data) {
                        //新增记录后,重新绑定zTree
                        $.post("Device.aspx", { param: ''}, function (data) {
                                var nodeEval = eval(data.menu);
                                $.fn.zTree.init($("#treeDemo"), setting, nodeEval);
                                $("#dv_Parent_Info").show();
                            
                        }, 'json');                    
                      
                        $.messager.alert('附件上传',data.msg,'info');  
                        
                        if(Number(data.count)==1)
                        {                          
                            $("#flUp").val('');
                        }                     
                    }, 'json');                   
                }    
                //$('#txtLineType').show();              
            });  
           
            //下载附件
            $("#flXiaZai").click(function() {      
                if($("#NodeCheckedID").val()=="" || $("#txtSBName").val()==null || $("#NodeCheckedID").val()==null ||escape($("#txtSBName").val())==null||escape($("#txtSBName").val())=="" || escape($("#NodeCheckedID").val())=="" ){
                    $.messager.alert('下载附件','请选在设备,然后再下载!','error');
                }else{            
                    $.post("Device.aspx", { param: 'DownFile',SBID:$("#NodeCheckedID").val(),SBName:escape($("#txtSBName").val())}, function (data) {
                           
                        $.messager.alert('附件下载',data.msg,'info');
                        
                    }, 'json');  
                }
            });
            
            //重置上传附件部分
            $("#btnQuxiao").click(function() {           
                $("#txtLineID").val('');
                $("#txtLineName").val('');
                $("#hidGW").val('');
                $('#txtLineGw').val('');
            });  
            
            //添加点检项目
            $("#SaveItem2").click(function() {  
               
                if($("#NodeCheckedID").val()=="" || $("#NodeCheckedID").val()==null ){
                    $.messager.alert('新增点检项目','请选择需要新增的设备!','error');
                }else if(escape($("#DJXDESC").val())==null||escape($("#DJXDESC").val())==""){
                    $.messager.alert('新增点检项目','请输入点检项描述信息！','error');
                }else if(escape($("#DJXBW").val())==null||escape($("#DJXBW").val())==""){
                    $.messager.alert('新增点检项目','请输入点检项部位信息！','error');
                }else if(escape($("#DJXCONTENT").val())==null||escape($("#DJXBW").val())==""){
                    $.messager.alert('新增点检项目','请输入点检项内容！','error');
                }else if(escape($("#DJXUnit").val())==null||escape($("#DJXUnit").val())==""){
                    $.messager.alert('新增点检项目','请输入点检项单位！','error');
                }else if(escape($("#DJXSTARTTIME").val())==null||escape($("#DJXSTARTTIME").val())==""){
                    $.messager.alert('新增点检项目','请输入点检项开始时间！','error');
                }else if(escape($("#DJXPERIODVALUE").val())==null||escape($("#DJXPERIODVALUE").val())==""){                 
                    $.messager.alert('新增点检项目','请输入点检项周期数值！','error');
                }else{
                    
                    $.post("Device.aspx", { param: 'SaveItem',SBnodeKey:$('#NodeCheckedID').val(),itemBw:escape($("#DJXBW").val()),itemDesc:escape($("#DJXDESC").val())
                    ,itemContent:escape($("#DJXCONTENT").val()) ,itemObserve:escape($("#DJXObserve").val()),itemUnit:escape($("#DJXUnit").val()),itemType:$("#DJXType").val()
                    ,itemStatus:$("#DJXSelectStatus").val() ,itemStatusQJ:$("#DJXT_SATAUS").val() ,itemUpper:$("#DJXUpper").val() ,itemLower:$("#DJXLower").val(),itemSpectrum:$("#DJXSpectrum").val()
                    ,itemStartTime:escape($("#DJXSTARTTIME").val()) ,itemPerValue:$("#DJXPERIODVALUE").val(),itemPerType:$("#Select1").val()
                    }, function (data) {
                        
//                        $.post("DeviceNew.aspx", { param: ''}, function (data) {
//                            if(Number(data.count)==1){
//                                var  nodeEval = eval(data.menu);
//                                $.fn.zTree.init($("#treeDemo"), setting, nodeEval);
//                                $("#dv_Parent_Info").show();
//                            }else
//                            {
//                                $("#dv_Parent_Info_father").show();
//                            }
//                        }, 'json');
                        $.messager.alert('添加点检项',data.msg,'info');
                                         
                        $('#dgItem').datagrid('reload'); 
                    }, 'json');
                    
                }
            }); 
            
             //添加点检项目
            $("#EditItems").click(function() {  
              
                if($("#idkeyItems").val()=="" || $("#idkeyItems").val()==null ){
                    $.messager.alert('新增点检项目','请选择需要新增的设备!','error');
                }else if(escape($("#DJXDESCEdit").val())==null||escape($("#DJXDESCEdit").val())==""){
                    $.messager.alert('新增点检项目','请输入点检项描述信息！','error');
                }else if(escape($("#DJXBWEdit").val())==null||escape($("#DJXBWEdit").val())==""){
                    $.messager.alert('新增点检项目','请输入点检项部位信息！','error');
                }else if(escape($("#DJXCONTENTEdit").val())==null||escape($("#DJXCONTENTEdit").val())==""){
                    $.messager.alert('新增点检项目','请输入点检项内容！','error');
                }else if(escape($("#DJXUnitEdit").val())==null||escape($("#DJXUnitEdit").val())==""){
                    $.messager.alert('新增点检项目','请输入点检项单位！','error');
                }else if(escape($("#DJXSTARTTIMEEdit").val())==null||escape($("#DJXSTARTTIMEEdit").val())==""){
                    $.messager.alert('新增点检项目','请输入点检项开始时间！','error');
                }else if(escape($("#DJXPERIODVALUEEdit").val())==null||escape($("#DJXPERIODVALUEEdit").val())==""){                 
                    $.messager.alert('新增点检项目','请输入点检项周期数值！','error');
                }else{
                    
                    $.post("Device.aspx", { param: 'EditItem',idkey:$("#idkeyItems").val(),itemBw:escape($("#DJXBWEdit").val()),itemDesc:escape($("#DJXDESCEdit").val())
                    ,itemContent:escape($("#DJXCONTENTEdit").val()) ,itemObserve:escape($("#DJXObserveEdit").val()),itemUnit:escape($("#DJXUnitEdit").val()),itemType:$("#DJXTypeEdit").val()
                    ,itemStatus:$("#DJXSelectStatusEdit").val() ,itemStatusQJ:$("#DJXT_SATAUSEdit").val() ,itemUpper:$("#DJXUpperEdit").val() ,itemLower:$("#DJXLowerEdit").val(),itemSpectrum:$("#DJXSpectrumEdit").val()
                    ,itemStartTime:escape($("#DJXSTARTTIMEEdit").val()) ,itemPerValue:$("#DJXPERIODVALUEEdit").val(),itemPerType:$("#Select1Edit").val()
                    }, function (data) {
                        
                        $.messager.alert('添加点检项',data.msg,'info');
                        CloseDiv('divEditItem', 'fade');                     
                        $('#dgItem').datagrid('reload'); 
                    }, 'json');
                    
                }
              
            }); 
                    
            //重置线路
            $("#btnReload").click(function() {           
                $("#txtLineID").val('');
                $("#txtLineName").val('');
                $("#hidGW").val('');
                $('#txtLineGw').val('');
            });
            
            //添加设备状态
            $("#SaveItem1").click(function() {  
               
                if($("#NodeCheckedID").val()=="" || $("#NodeCheckedID").val()==null ){
                    $.messager.alert('设备状态编辑','请选择需要新增的设备!','error');
                }else if(escape($("#DJXSTARTTIME1").val())==null||escape($("#DJXSTARTTIME1").val())==""){
                    $.messager.alert('设备状态编辑','请选择开始时间！','error');
                }else{                    
                    $.post("Device.aspx", { param: 'SaveStatus',SBnodeKey:$('#NodeCheckedID').val(),StartTime:escape($("#DJXSTARTTIME1").val()),type:$("#DJXPERIODTYPE").val()}, function (data1) {
                        
                        $.messager.alert('新增设备状态',data1.msg,'info');  
                        CloseDiv('MyItem', 'fade');                      
                        $('#dgDev').datagrid('reload');  
                    }, 'json');   
                    
                             
                }
            });  
            
            //编辑设备状态
            $("#EditItme").click(function() {  
               
                if($("#ItemIDKEY").val()=="" || $("#ItemIDKEY").val()==null ){
                    $.messager.alert('设备状态编辑','请选择需要新增的设备!','error');
                }else if(escape($("#ItemDateEdit").val())==null||escape($("#ItemDateEdit").val())==""){
                    $.messager.alert('设备状态编辑','请选择开始时间！','error');
                }else{                    
                    $.post("Device.aspx", { param: 'EditStatus',IDKEY:$('#ItemIDKEY').val(),StartTime:escape($("#ItemDateEdit").val()),type:$("#ItemSelEdit").val()}, function (data1) {
                        if(Number(data1.count)==1){
                           $.messager.alert('修改设备状态',data1.info,'info'); 
                           CloseDiv('MyItemEdit', 'fade');                      
                           $('#dgDev').datagrid('reload');  
                        }else
                        { $.messager.alert('修改设备状态',data1.info,'info');}
                    }, 'json');  
                }
            });  
          
		});
		//弹出隐藏层        
        function ShowDiv(show_div, bg_div) {
        
            document.getElementById(show_div).style.display = 'block';
            document.getElementById(bg_div).style.display = 'block';
            var bgdiv = document.getElementById(bg_div);
            bgdiv.style.width = document.body.scrollWidth;
            // bgdiv.style.height = $(document).height();
            $("#" + bg_div).height($(document).height());
            $("#txtLineType").hide();    
            $("#txtLineTypeEdit").hide();    
            
            
            //判断是否是设备Device
            if(show_div=="MyDevice"){            
               $("#sbID").val();
            }    
        };
        
        //关闭弹出层
        function CloseDiv(show_div, bg_div) {
            document.getElementById(show_div).style.display = 'none';
            document.getElementById(bg_div).style.display = 'none';
            $("#txtLineType").show();
            $("#txtLineTypeEdit").show();  
            
            //判断是否是设备Device
            if(show_div=="MyDevice"){
            
                var treeObj = $.fn.zTree.getZTreeObj("treeDevice");
                var nodes = treeObj.getCheckedNodes(true);
                var deviceID='';
                
                for(var i=0;i<nodes.length;i++)
                {
                    deviceID+=nodes[i].id+',';                    
                }
                //alert(deviceID);
                $("#sbID").val(deviceID);
            }
        };
         
        //弹出点检项隐藏层        
        function ShowDivItem(show_div, bg_div) {
        
            document.getElementById(show_div).style.display = 'block';
            document.getElementById(bg_div).style.display = 'block';
            var bgdiv = document.getElementById(bg_div);
            bgdiv.style.width = document.body.scrollWidth;           
        };
        
        //关闭点检项弹出层
        function CloseDivItem(show_div, bg_div) {
            document.getElementById(show_div).style.display = 'none';
            document.getElementById(bg_div).style.display = 'none';
        };
                
		//-->
    </script>

</head>
<body>
    <form id="formMember" runat="server">
    <!-- 点击TreeNode赋值 -->
    <input type="hidden" id="NodeCheckedID" />
    <input type="hidden" id="treePersonNodeID" />
    <input type="hidden" id="treePersonNodeIDEdit" />
    <input type="hidden" id="AreaCheckedID" />
    <input type="hidden" id="tabID" />  <!-- 控制Tab页面编码-->
    <!-- 已经选中的区域ID集合 -->
    <div style="width: 948px; border-left: solid 1px #AED0EA; border-top: solid 1px #AED0EA;
        border-right: solid 1px #AED0EA;">
        <div id="toolbar" style="height: 26px; padding: 5px;">
            <a id="add" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'">Add</a>
            <a id="save" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-save'">Save</a>
            <a id="delete" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove'">Remove</a>                
        </div>
    </div>
    <div style="width: 950px; height: auto;">
        <div style="float: left; width: 250px; height: 410px; border-left: solid 1px #AED0EA;
            border-top: solid 1px #AED0EA; border-bottom: solid 1px #AED0EA;">
            <div class="zTreeDemoBackground left">
                <ul id="treeDemo" class="ztree">
                </ul>
            </div>
            <div class="right">
            </div>
        </div>
        <div id="tt" class="easyui-tabs" data-options="tools:'#tab-tools'" style="float: right;
            width: 699px; height: 412px;">
            <!--附件管理 -->
            <div id="dvLine" title="附件管理" data-options="tools:'#p-tools'" style="padding: 2px;">
                <div id="dv_Parent_Info">
                    <table class="admintable" width="100%">
                        <tr>
                            <td class="admincls1" align="center">
                                设备名称
                            </td>
                            <td class="admincls1">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtSBName" type="text" readonly="true"
                                    runat="server" /><input id="HidSBID" type="hidden" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="admincls0" align="center">
                                附件下载
                            </td>
                            <td class="admincls0">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="Text2" type="text" readonly="true"
                                    runat="server" /><input id="txtDeviceId" type="hidden" runat="server" />
                                    <input id="txtDeviceName" type="hidden" runat="server" />                           
                                <asp:Button ID="btnXia"  Text="下载" CssClass="formbutton" runat="server" onclick="btnXia_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td class="admincls1" align="center">
                                附件上传
                            </td>
                            <td class="admincls1">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="flUp" type="file" style="width: 200px;" /><br />
                               
                            </td>
                        </tr>
                        <tr>
                            <td class="admincls0" align="center" colspan="2">
                                <a id="saveFile" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok'">
                                    保存</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a id="btnQuxiao" href="#" class="easyui-linkbutton"
                                        data-options="iconCls:'icon-reload'">取消</a>
                            </td>
                        </tr>
                    </table>
                </div>
           
            </div>
            <!-- 状态维护 -->
            <div id="dv_Member" title="状态维护" data-options="tools:'#p-tools'" style="padding: 2px;">
                <div id="dv_Member_Info"> 
                    <table id="dgDev" ></table>                              
                </div> 
            </div>
            <!-- 点检项维护 -->
            <div id="dv_Device" title="点检项维护" data-options="tools:'#p-tools'" style="padding: 2px;">
                 <table id="dgItem" ></table>             
            </div>         
        </div>
        <div id="tt1" class="easyui-tabs" data-options="tools:'#tab-tools'" style="float: right;
            width: 699px; height: 412px;">
            <div id="divInfo" title="设备信息">
                    <table class="admintable" width="100%">
                        <tr>
                            <td class="admincls1" align="center" style="width:20%;">
                                设备名称
                            </td>
                            <td class="admincls1" style="width:80%;">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblInfoName" runat="server" ></asp:Label> 
                            </td>
                        </tr>
                        <tr>
                            <td class="admincls0" align="center">
                                设备状态
                            </td>
                            <td class="admincls0">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<label id="lblInfoStatus" runat="server"></label>
                            </td>
                        </tr>
                        <tr>
                            <td class="admincls1" align="center">
                                是否存在附件
                            </td>
                            <td class="admincls1">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblInfoFile" runat="server" ></asp:Label>
                            </td>
                        </tr>                        
                    </table>
                </div>
        </div>
    </div>
    <div id="fade" class="black_overlay">
    </div>
    <div id="MyItem" class="white_content1">
        <table width="90%" border="1">           
            <tr>
                <td>
                    &nbsp;&nbsp;时间:
                </td>
                <td>
                    <input type="text" id="DJXSTARTTIME1" style="text-align: center;" runat="server" readonly="readonly"
                        onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                </td>
                <td>
                    &nbsp;&nbsp;设备状态:
                </td>
                <td>
                    <select id="DJXPERIODTYPE" style="width: 60px; " >            <%--//onchange="d()"    --%>   
                        <option value="0">在用</option>
                        <option value="1">停用</option>
                        <option value="2">退役</option>
                        <option value="3">报废</option>
                        <option value="4">删除</option>
                    </select>
                </td>
                  <td>
                    &nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <div id="s" style="height: 10px;">
        </div>
        <div style="text-align: center; cursor: default; height: 15px;">
            <a id="SaveItem1" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-reload'">
                保存</a> &nbsp;&nbsp;&nbsp;<a id="A11" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-reload'"
                    onclick="CloseDivItem('MyItem','fade')">取消</a>
        </div>
    </div>
    <div id="MyItemEdit" class="white_content1">
        <table width="90%" border="1">           
            <tr>
                <td>
                    &nbsp;&nbsp;时间:<input type="hidden" id="ItemIDKEY" />
                </td>
                <td>
                    <input type="text" id="ItemDateEdit" style="text-align: center;" runat="server" readonly="readonly"
                        onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                </td>
                <td>
                    &nbsp;&nbsp;设备状态:
                </td>
                <td>
                    <select id="ItemSelEdit" style="width: 60px; " >            <%--//onchange="d()"    --%>   
                        <option value="0">在用</option>
                        <option value="1">停用</option>
                        <option value="2">退役</option>
                        <option value="3">报废</option>
                        <option value="4">删除</option>
                    </select>
                </td>
                  <td>
                    &nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <div id="Div2" style="height: 10px;">
        </div>
        <div style="text-align: center; cursor: default; height: 15px;">
            <a id="EditItme" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-reload'">
                保存</a> &nbsp;&nbsp;&nbsp;<a id="A2" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-reload'"
                    onclick="CloseDivItem('MyItemEdit','fade')">取消</a>
        </div>
    </div>
    <div id="MyItems" class="white_content">
        <table width="90%" border="1">
            <tr>              
                <td>
                    &nbsp;&nbsp;点检项部位:
                </td>
                <td>
                    <input type="text" id="DJXBW" />
                </td>
                <td>
                    &nbsp;&nbsp;点检项描述:
                </td>
                <td>
                    <input type="text" id="DJXDESC" />
                </td>
                  <td>
                    &nbsp;&nbsp;<%--点检项编码:--%>
                </td>
                <td>
                   &nbsp;&nbsp;<%-- <input type="text" id="DJXID" />--%>
                </td>
            </tr>
            <tr>
                <td colspan="1">
                    &nbsp;&nbsp;检查内容:
                </td>
                <td colspan="5">
                    <input style="width: 400px; height: 40px;" type="text" id="DJXCONTENT" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;数据名称:
                </td>
                <td>
                    <input type="text" id="DJXObserve" />
                </td>
                <td>
                    &nbsp;&nbsp;单位:
                </td>
                <td>
                    <input type="text" id="DJXUnit" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;点检类型:
                </td>
                <td>
                    <select id="DJXType" style="width: 60px;">
                        <option id="Option3" value="0">点检</option>
                        <option id="Option4" value="1">巡检</option>
                    </select>
                </td>
                <td>
                    &nbsp;&nbsp;设备状态:
                </td>
                <td>
                    <select id="DJXSelectStatus" style="width: 60px;">
                        <option id="Option5" value="0">在用</option>
                        <option id="Option6" value="1">停用</option>
                        <option id="Option7" value="2">退役</option>
                        <option id="Option8" value="3">报废</option>
                        <option id="Option9" value="4">删除</option>
                    </select>
                </td>
                <td>
                    &nbsp;&nbsp;点检类型:
                </td>
                <td>
                    <select id="DJXT_SATAUS" style="width: 60px;">
                        <option id="Option18" value="0">启机</option>
                        <option id="Option19" value="1">停机</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;测量上限:
                </td>
                <td>
                    <input type="text" id="DJXUpper" />
                </td>
                <td>
                    &nbsp;&nbsp;测量下限:
                </td>
                <td>
                    <input type="text" id="DJXLower" />
                </td>
                <td>
                    &nbsp;&nbsp;是否频谱:
                </td>
                <td>
                    <select id="DJXSpectrum" style="width: 60px;">
                        <option id="Option10" value="0">否</option>
                        <option id="Option11" value="1">是</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;开始时间:
                </td>
                <td>
                    <input type="text" id="DJXSTARTTIME" style="text-align: center;" runat="server" readonly="readonly"
                        onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                </td>
                <td>
                    &nbsp;&nbsp;周期数值:
                </td>
                <td>
                    <input type="text" id="DJXPERIODVALUE" />
                </td>
                <td>
                    &nbsp;&nbsp;周期类型:
                </td>
                <td>
                    <select id="Select1" style="width: 60px;">
                        <option id="Option14" value="1">日</option>
                        <option id="Option15" value="2">周</option>
                        <option id="Option16" value="3">月</option>
                        <option id="Option17" value="4">年</option>
                    </select>
                </td>
            </tr>
        </table>
        <div id="Div3" style="height: 10px;">
        </div>
        <div style="text-align: center; cursor: default; height: 15px;">
            <a id="SaveItem2" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-reload'">
                保存</a> &nbsp;&nbsp;&nbsp;<a id="A4" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-reload'"
                    onclick="CloseDivItem('MyItems','fade')">取消</a>
        </div>
    </div>    
    <div id="divEditItem" class="white_content">
        <table width="90%" border="1">
            <tr>              
                <td>
                    &nbsp;&nbsp;点检项部位:<input type="hidden" id="idkeyItems" />
                </td>
                <td>
                    <input type="text" id="DJXBWEdit" />
                </td>
                <td>
                    &nbsp;&nbsp;点检项描述:
                </td>
                <td>
                    <input type="text" id="DJXDESCEdit" />
                </td>
                  <td>
                    &nbsp;&nbsp;<%--点检项编码:--%>
                </td>
                <td>
                   &nbsp;&nbsp;<%-- <input type="text" id="DJXID" />--%>
                </td>
            </tr>
            <tr>
                <td colspan="1">
                    &nbsp;&nbsp;检查内容:
                </td>
                <td colspan="5">
                    <input style="width: 400px; height: 40px;" type="text" id="DJXCONTENTEdit" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;数据名称:
                </td>
                <td>
                    <input type="text" id="DJXObserveEdit" />
                </td>
                <td>
                    &nbsp;&nbsp;单位:
                </td>
                <td>
                    <input type="text" id="DJXUnitEdit" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;点检类型:
                </td>
                <td>
                    <select id="DJXTypeEdit" style="width: 60px;">
                        <option id="Option1" value="0">点检</option>
                        <option id="Option2" value="1">巡检</option>
                    </select>
                </td>
                <td>
                    &nbsp;&nbsp;设备状态:
                </td>
                <td>
                    <select id="DJXSelectStatusEdit" style="width: 60px;">
                        <option id="Option12" value="0">在用</option>
                        <option id="Option13" value="1">停用</option>
                        <option id="Option20" value="2">退役</option>
                        <option id="Option21" value="3">报废</option>
                        <option id="Option22" value="4">删除</option>
                    </select>
                </td>
                <td>
                    &nbsp;&nbsp;点检类型:
                </td>
                <td>
                    <select id="DJXT_SATAUSEdit" style="width: 60px;">
                        <option id="Option23" value="0">启机</option>
                        <option id="Option24" value="1">停机</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;测量上限:
                </td>
                <td>
                    <input type="text" id="DJXUpperEdit" />
                </td>
                <td>
                    &nbsp;&nbsp;测量下限:
                </td>
                <td>
                    <input type="text" id="DJXLowerEdit" />
                </td>
                <td>
                    &nbsp;&nbsp;是否频谱:
                </td>
                <td>
                    <select id="DJXSpectrumEdit" style="width: 60px;">
                        <option id="Option25" value="0">否</option>
                        <option id="Option26" value="1">是</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;开始时间:
                </td>
                <td>
                    <input type="text" id="DJXSTARTTIMEEdit" style="text-align: center;" runat="server" readonly="readonly"
                        onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                </td>
                <td>
                    &nbsp;&nbsp;周期数值:
                </td>
                <td>
                    <input type="text" id="DJXPERIODVALUEEdit" />
                </td>
                <td>
                    &nbsp;&nbsp;周期类型:
                </td>
                <td>
                    <select id="Select1Edit" style="width: 60px;">
                        <option id="Option27" value="1">日</option>
                        <option id="Option28" value="2">周</option>
                        <option id="Option29" value="3">月</option>
                        <option id="Option30" value="4">年</option>
                    </select>
                </td>
            </tr>
        </table>
        <div id="Div4" style="height: 10px;">
        </div>
        <div style="text-align: center; cursor: default; height: 15px;">
            <a id="EditItems" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-reload'">
                保存</a> &nbsp;&nbsp;&nbsp;<a id="A3" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-reload'"
                    onclick="CloseDivItem('divEditItem','fade')">取消</a>
        </div>
    </div>
    </form>
</body>
</html>
