<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RouteManageNew.aspx.cs"
    Inherits="DJXT.Route.RouteManageNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>线路整理维护</title>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/css/djxt.css" rel="stylesheet" type="text/css" />

    <script src="../CustomersData.js" type="text/javascript"></script>

    <script src="../jQueryEasyUI/jquery-1.6.2.js" type="text/javascript"></script>

    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>

    <script src="../js/jquery.ztree.core-3.5.js" type="text/javascript"></script>

    <script type="text/javascript" src="../js/jquery.ztree.excheck-3.5.js"></script>

    <script type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>

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
           $('#linkBut').val();        
       }; 
	    
		function onClick(event, treeId, treeNode, clickFlag) {
		   $.post("RouteManageNew.aspx", { param: 'clickTree',id:treeNode.id}, function (data) {
                
                $('#add').linkbutton({text:'新建'});
                //$('#linkBut').val('');                
                
                if(Number(data.count)==1){
			        $('#add').linkbutton({disabled:false});
		            $('#save').linkbutton({disabled:true});
		            $('#delete').linkbutton({disabled:true});	
		            
		            $('#dvLineOne').hide();
                    $('#dvEditLineOne').hide();
                    $('#dvArea').hide();
                    $('#dvDevice').hide();
                    $('#dvItem').hide();
                    $('#dvZXL').show();
                    $('#dvInfoLineOne').hide();
                    $('#dvAreaInfo').hide();
                    
                    $('#linkBut').val(''); 	
                    $('#dvItemInfo').hide();
                }else if(Number(data.count)==2){
			        
			        $('#add').linkbutton({disabled:false});
		            $('#save').linkbutton({disabled:false});
		            $('#delete').linkbutton({disabled:true});
		           
		            $('#txtLineIDEdit').val(data.lID);
		            $('#txtLineNameEdit').val(data.lName);
		            $('#txtLineTypeEdit').val(Number(data.lType));
		            $('#txtLineGwEdit').val(data.lOrgName);
		            $('#treePersonNodeIDEdit').val(data.lOrgID);
		            
		            $('#LineAreaName').val(data.lName);
		            //$("#dvLine").attr({title:"赋值"});
		            
		            $('#txtLineIDEdit').val(data.lID);
		            $('#txtLineNameEdit').val(data.lName);
		            $('#txtLineTypeEdit').val(Number(data.lType));
		            $('#txtLineGwEdit').val(data.lOrgName);
		            $('#treePersonNodeIDEdit').val(data.lOrgID);
		            		     
		            $('#txtInfoBM').val(data.lID);   
		            $('#txtInfoName').val(data.lName);
		            $('#infoSel').val(Number(data.lType));   
		            $('#txtInfoGW').val(data.lOrgName);
		            
		            $('#dvInfoLineOne').show();
		            $('#dvLineOne').hide();
                    $('#dvEditLineOne').hide();
                    $('#dvArea').hide();
                    $('#dvDevice').hide();
                    $('#dvItem').hide();
                    $('#dvZXL').hide();
                    $('#dvAreaInfo').hide();
                    $('#dvItemInfo').hide();
		            $('#linkBut').val(''); 
		            //$("#tt").tabs('getTab',{title:'线路信息'}).hide();
		            //$("#tt").tabs('getTab','线路信息').hide();
		           
                }else if(Number(data.count)==3){
                    //alert("区域");
                    $('#add').linkbutton({disabled:true});
		            $('#save').linkbutton({disabled:false});
		            $('#delete').linkbutton({disabled:false});	
		            
		            //alert(data.areaID);
                    $('#qyID').val(data.areaID);
                    $('#txtQyName').val(data.areaName);
                    //alert($('#qyID').val());	
                    
                    $('#dvLineOne').hide();
                    $('#dvEditLineOne').hide();
                    $('#dvArea').hide();
                    $('#dvDevice').hide();
                    $('#dvItem').hide();
                    $('#dvZXL').hide();
                    $('#dvAreaInfo').show();
                    $('#dvInfoLineOne').hide();
                    $('#dvItemInfo').hide();
                    
                    var res=data.devs;
                    
                    $("#sb1").html('');
                    
                    if(res!=''){
                        var re=res.split(',');  
                        
                         res='</br></br>';
                        
                        for(j=0;j<re.length ;j++)
                        {
                           res+="&nbsp;&nbsp;&nbsp;"+re[j]+"&nbsp;&nbsp;&nbsp;";
                        }    
                    }   
                    $('#linkBut').val('');                    
                    $("#sb1").html(res);
                    $("#dv_AreaInfo").html(res);
                    
                    $('#LineAreaNameInfo').val(data.lName);		          
                }else if(Number(data.count)==4){
                    
                    $('#qyID').val('');
                    $('#txtQyName').val('');
                    
                    $('#add').linkbutton({disabled:true});
		            $('#save').linkbutton({disabled:false});
		            $('#delete').linkbutton({disabled:true});		           
	             
	                $('#dvLineOne').hide();
                    $('#dvEditLineOne').hide();
                    $('#dvArea').hide();
                    $('#dvDevice').hide();
                    $('#dvItem').show();	
                    $('#dvInfoLineOne').hide();
                    $('#dvZXL').hide();
                    $('#dvAreaInfo').hide();
                    $('#dvItemInfo').hide();
                    //$('#add').linkbutton({text:'保存'});
                     
                    
                    var res=data.itemDesc;
                    
                    $("#dvDevItem").html('');
                    
                    if(res!=''){
                        var re=res.split(',');  
                        
                         res='';
                        
                        for(j=0;j<re.length ;j++)
                        {
                           res+="&nbsp;&nbsp;&nbsp;"+re[j]+"&nbsp;&nbsp;&nbsp;";
                        }    
                    }  
                    
                     $("#dvDevItem").html(res);    
                     $("#txtInfoDevName").val(data.devName);
                              
                              
                }else if(Number(data.count)==5){
                  
                    $('#dvEditLineOne').hide();
                    $('#dvArea').hide();
                    $('#dvDevice').hide();
                    $('#dvItem').hide();
                    $('#dvLineOne').hide();
                    $('#dvEditLineOne').hide();
                    $('#dvArea').hide();
                    $('#dvDevice').hide();                    	
                    $('#dvInfoLineOne').hide();
                    $('#dvZXL').hide();
                    $('#dvAreaInfo').hide();
                 
                    $('#DJXBW').val(data.itemPosition);
                    $('#DJXDESC').val(data.itemDesc);
                    $('#DJXCONTENT').val(data.itemContent);
                    $('#DJXObserve').val(data.itemObserve);
                    $('#DJXUnit').val(data.itemUint);
                    $('#DJXType').val(data.itemType);
                    $('#DJXSelectStatus').val(data.itemI_Status);
                    $('#DJXT_SATAUS').val(data.itemT_Status);
                    $('#DJXUpper').val(data.itemUpper);
                    $('#DJXLower').val(data.itemLower);
                    $('#DJXSpectrum').val(data.itemSpectrum);
                    $('#DJXSTARTTIME').val(data.itemStartTime);
                    $('#DJXPERIODVALUE').val(data.itemPerodValue);
                    $('#Select1').val(data.itemPerodType);
                    //$('#DJXSTARTTIME').val(data.itemStartTime);
                    
                    $('#dvItemInfo').show();
                    
                    $('#add').linkbutton({disabled:true});
		            $('#save').linkbutton({disabled:true});
		            $('#delete').linkbutton({disabled:true}); 
                }
                else{                
                    $('#add').linkbutton({disabled:false});
		            $('#save').linkbutton({disabled:false});
		            $('#delete').linkbutton({disabled:false}); 
		            $('#linkBut').val();                         
		        }
                
                $('#NodeCheckedID').val(treeNode.id);
                $('#treePersonNodeID').val('');
                $('#txtLineGw').val('');
                
                if(Number(data.count)==2){
                    
                    var list=data.list;
                    var listArea = data.listArea;                    
                    var checkboxs='';
                    var chkRes='';
                    
                    if(list!=null){
                       
                        $("#dv_Area").html('');
                        var count=0;
                        for(var i=0;i<list.length;i++){
                            count++;
                            if(count%3==0){
                                checkboxs+="<input type='checkbox' name='checkbox' value='"+list[i].T_NODEID+"' checked='checked'>&nbsp;&nbsp;&nbsp;"+list[i].T_AREANAME+'&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>';
                            }else{
                                checkboxs+="<input type='checkbox' name='checkbox' value='"+list[i].T_NODEID+"' checked='checked'>&nbsp;&nbsp;&nbsp;"+list[i].T_AREANAME+'&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;'; 
                            }
                            chkRes+=list[i].T_NODEID+',';
                        }                    
                        for(var i=0;i<listArea.length;i++){      
                            count++;
                            var num=0;
                            for(k=0;k<list.length;k++){  
                                if(listArea[i].T_AREAID!=list[k].T_NODEID){
                                    num++;
                                }
                            }
                            if(num== list.length){         
                                if(count%4==0){
                                    checkboxs+="<input type='checkbox' name='checkbox' value='"+listArea[i].T_AREAID+"'>&nbsp;&nbsp;&nbsp;"+listArea[i].T_AREANAME+'&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>';
                                }else{
                                    checkboxs+="<input type='checkbox' name='checkbox' value='"+listArea[i].T_AREAID+"'>&nbsp;&nbsp;&nbsp;"+listArea[i].T_AREANAME+'&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;'; 
                                }
                            }
                        }
                    }else{
                        for(var i=0;i<listArea.length;i++){       
                            if(i%3==0 && i!=0){
                                checkboxs+="<input type='checkbox' name='checkbox' value='"+listArea[i].T_AREAID+"'>&nbsp;&nbsp;&nbsp;"+listArea[i].T_AREANAME+'&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>';
                            }else{
                                checkboxs+="<input type='checkbox' name='checkbox' value='"+listArea[i].T_AREAID+"'>&nbsp;&nbsp;&nbsp;"+listArea[i].T_AREANAME+'&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;'; 
                            }                        
                        }                          
                    }
                    
                    $("#dv_Area").html(checkboxs);   
                    $("#AreaCheckedID").val(chkRes);             
                }                
            }, 'json');
		}		

		$(document).ready(function(){	    
           
		    $('#add').linkbutton({text:'新建'});
		    $('#save').linkbutton({text:'编辑'});
		    $('#delete').linkbutton({text:'删除'});
		    		
		    $('#add').linkbutton({disabled:true});
		    $('#save').linkbutton({disabled:true});
		    $('#delete').linkbutton({disabled:true});
		    
		    $('#dvLineOne').hide();
            $('#dvEditLineOne').hide();
            $('#dvArea').hide();
            $('#dvDevice').hide();
            $('#dvItem').hide();
            $('#dvInfoLineOne').hide();
            $('#dvAreaInfo').hide();
            $('#dvItemInfo').hide();
            $('#linkBut').val(''); 
                 
            $.post("RouteManageNew.aspx", { param: ''}, function (data) {
                if(Number(data.count)==1){
                    var  nodeEval = eval(data.menu);
                    $.fn.zTree.init($("#treeDemo"), setting, nodeEval);
                    $('#dvZXL').show();
                }
            }, 'json');
            
            $("#add").click(function() { 
                           
                     $.post("RouteManageNew.aspx", { param: 'clickTree',id:$('#NodeCheckedID').val()}, function (data) {
                    if(Number(data.count)==0){
                       ShowDiv('MyItem', 'fade');
                    }else if(Number(data.count)==2){
                        $('#dvLineOne').hide();
                        $('#dvEditLineOne').hide();
                        $('#dvArea').show();
                        $('#dvDevice').hide();
                        $('#dvItem').hide();
                        $('#dvInfoLineOne').hide();
                    }else if(Number(data.count)==1){
                        $('#dvLineOne').show();
                        $('#dvEditLineOne').hide();
                        $('#dvArea').hide();
                        $('#dvDevice').hide();
                        $('#dvItem').hide();
                        $('#dvZXL').hide();
                    }else if(Number(data.count)==3){
                        $('#dvLineOne').hide();
                        $('#dvEditLineOne').hide();
                        $('#dvArea').hide();
                        $('#dvDevice').show();
                        $('#dvItem').hide();
                        $('#dvAreaInfo').hide();
                    }else if(Number(data.count)==4){
                        $('#dvLineOne').hide();
                        $('#dvEditLineOne').hide();
                        $('#dvArea').hide();
                        $('#dvDevice').hide();
                        $('#dvItem').show();
                        
                    }else if(Number(data.count)==5){
                        $('#dvLineOne').hide();
                        $('#dvEditLineOne').hide();
                        $('#dvArea').hide();
                        $('#dvDevice').hide();
                        $('#dvItem').hide();
                    }else
                    {
                        $('#dvLineOne').show();
                        $('#dvEditLineOne').hide();
                        $('#dvArea').hide();
                        $('#dvDevice').hide();
                        $('#dvItem').hide();
                       
                    }
                }, 'json');
                
                //}
            }); 
            
            $("#save").click(function() {
                $.post("RouteManageNew.aspx", { param: 'clickTree',id:$('#NodeCheckedID').val()}, function (data) {               
                    if(Number(data.count)==5){
                       ShowDiv('MyItem', 'fade');
                    }else if(Number(data.count)==2){                        
                        $('#dvLineOne').hide();
                        $('#dvEditLineOne').show();
                        $('#dvArea').hide();
                        $('#dvDevice').hide();
                        $('#dvItem').hide();
                        $('#dvInfoLineOne').hide();
                    }else if(Number(data.count)==3){
                        
                        $('#dvLineOne').hide();
                        $('#dvEditLineOne').hide();
                        $('#dvArea').hide();
                        $('#dvDevice').show();
                        $('#dvItem').hide();
                        $('#dvAreaInfo').hide();
                    }else if(Number(data.count)==4){
                         //初始化点检项表格
                     $('#dgItem').datagrid({
                    
                        title: '点检项信息', //表格标题
                        url: location.href,//'../ROUTE/ROUTEGRID.aspx',//location.href,//'../Device/Device.aspx',//location.href, //请求数据的页面
                        sortName: 'JSON_id_key', //排序字段
                        idField: 'JSON_id_key', //标识字段,主键
                        iconCls: '', //标题左边的图标
                        width: 720,//'80%', //宽度
                        height: 350,//$(parent.document).find("#mainPanle").height() - 10 > 0 ? $(parent.document).find("#mainPanle").height() - 10 : 500, //高度
                        nowrap: false, //是否换行，True 就会把数据显示在一行里
                        striped: true, //True 奇偶行使用不同背景色
                        collapsible: false, //可折叠
                        sortOrder: 'desc', //排序类型
                        remoteSort: true, //定义是否从服务器给数据排序
                        frozenColumns: [[//冻结的列，不会随横向滚动轴移动
    	                    {field: 'chk', checkbox: true },
                            { title: '点检项描述', field: 'JSON_t_itemdesc', width: 100, sortable: true },
                            { title: '点检项部位', field: 'JSON_t_itemposition', width: 100, sortable: true }                            
			            ]],
                        columns: [[
                            { title: '观察名称', field: 'JSON_t_observe', width: 100 },   
                            { title: '单位', field: 'JSON_t_observe', width: 100 }, 
                            { title: '测量上限', field: 'JSON_f_upper', width: 80 },
                            { title: '测量下限', field: 'JSON_f_lower', width: 80 },
                            { title: '类型', field: 'JSON_t_type',formatter:function(value,rec,index){return value==0?'点检':'巡检'},width: 80 }
        //                    { title: '是否超级管理员', field: 'item_isadmin',formatter:function(value,rec,index){return value==0?'否':'是'}, width: 100 },
        //                    { title: '邮箱地址', field: 'item_email', width: 150 }, 
//                                { title: '操作', field: 'JSON_id_key', width: 80, formatter: function (value, rec) {
//                                    return '<a style="color:red" href="../Device/msgInfo.aspx?id='+value+'" target="_blank">查看记录</a>';//onclick="EditData(' + value + ');$(this).parent().click();return false;"
//                                    //return '<a style="color:red" href="javascript:;" name="oop" onclick="javascript:window.open(Default.aspx,_blank);">查看历史</a>';//onclick="op()"
//                                }//onclick="window.open('../bwjc/Bwmsg.aspx?id=','','height=600,width=550,top=100,left=320,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no')"
//                                }
                        ]],
                        toolbar: "#tab_toolbar",
                        queryParams: { "param": "queryItem","SBID":$('#NodeCheckedID').val()},
                        pagination: true, //是否开启分页
                        //pageNumber: 1, //默认索引页
                        //pageSize: 10, //默认一页数据条数
                        rownumbers: true, //行号
                        onLoadSuccess:function(){  //当数据加载成功时触发
                          var userIds =data.id_key;  //隐藏字段获取值该字符串有，号分隔
                          var userIda =  userIds.split(',');
                          for(var i in userIda){
                              $('#dgItem').datagrid('selectRecord',userIda[i]);//根据id选中行
                          } 
                          $('#id_key_old').val(data.id_key);
                         
                        }
                    }); 
                    
                    var p = $('#dgItem').datagrid('getPager'); 
                    $(p).pagination({  
                        pageSize:10,  
                        pageList:[1,2,3],  
                        beforePageText: '第',  
                        afterPageText: '页    共 {pages} 页',  
                        displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条',  
                        onBeforeRefresh:function(){  
                            $("#test").datagrid("reload");  
                        }  
                    });  
                    
    		              ShowDiv('MyItem', 'fade');
                    }            
                }, 'json');                
            }); 
            
            $("#delete").click(function() {
                $.post("RouteManageNew.aspx", { param: 'clickTree',id:$('#NodeCheckedID').val()}, function (data) {               
                    if(Number(data.count)==3){
                       if($("#qyID").val()==null ||$("#qyID").val()==''){
                            $.messager.alert('删除区域','请选择所要删除的区域,然后再进行删除操作!','info');
                       }else{
                            $.post("RouteManageNew.aspx", { param: 'DelArea',AreaID:$("#qyID").val(),nodeKey:$("#NodeCheckedID").val()}, function (data) {
                                    $.post("RouteManageNew.aspx", { param: ''}, function (data) {
                                        if(Number(data.count)==1){
                                            var  nodeEval = eval(data.menu);
                                            $.fn.zTree.init($("#treeDemo"), setting, nodeEval);
                                            $("#dv_Parent_Info").show();
                                        }else
                                        {
                                            $("#dv_Parent_Info_father").show();
                                        }
                                    }, 'json');
                                    $.messager.alert('删除区域',data.msg,'info');
                                }, 'json');
                        }
                    }                  
                }, 'json'); 
            });  
            
            //添加线路
            $("#btnAddXLInfo").click(function() {  
                
                if($("#txtLineName").val()==null || $("#treePersonNodeID").val()==null || $("#NodeCheckedID").val()==null||escape($("#txtLineName").val())=="" || escape($("#treePersonNodeID").val())=="" || escape($("#NodeCheckedID").val())==""){
                    $.messager.alert('新增线路','请选择右侧线路,并且线路编码,线路名称,所属岗位不能为空','error');
                }else{
                    $.post("RouteManageNew.aspx", { param: 'AddLineInfo',lineName:escape($("#txtLineName").val()),lineType:escape($("#txtLineType").val()),lineGw:escape($("#treePersonNodeID").val()),LineParentID:escape($("#NodeCheckedID").val())}, function (data) {
                        //新增记录后,重新绑定zTree
                        $.post("RouteManageNew.aspx", { param: ''}, function (data) {
                                var nodeEval = eval(data.menu);
                                $.fn.zTree.init($("#treeDemo"), setting, nodeEval);
                               
                            
                        }, 'json');                    
                      
                        $.messager.alert('添加线路',data.msg,'info');                       
                    }, 'json');                   
                }    
                //$('#txtLineType').show();              
            });  
            
            //重置线路
            $("#btnReload").click(function() {           
                $("#txtLineID").val('');
                $("#txtLineName").val('');
                $("#hidGW").val('');
                $('#txtLineGw').val('');
            });             
             
            //编辑线路信息
            $("#btnParentEdit").click(function() {  
                
                if($("#txtLineIDEdit").val()=="" || $("#txtLineNameEdit").val()==null || $("#treePersonNodeIDEdit").val()==null || $("#NodeCheckedID").val()==null ||escape($("#txtLineIDEdit").val())==null||escape($("#txtLineNameEdit").val())=="" || escape($("#treePersonNodeIDEdit").val())=="" || escape($("#NodeCheckedID").val())==""){
                    $.messager.alert('线路编辑','线路编码,线路名称,所属岗位不能为空','error');
                }else{
                    $.post("RouteManageNew.aspx", { param: 'EditLineInfo',lineID:$("#txtLineIDEdit").val(),lineName:escape($("#txtLineNameEdit").val()),lineType:escape($("#txtLineTypeEdit").val()),lineGw:escape($("#treePersonNodeIDEdit").val()),LineParentID:escape($("#NodeCheckedID").val())}, function (data) {
                        //新增记录后,重新绑定zTree
                        $.post("RouteManageNew.aspx", { param: ''}, function (data) {
                            var  nodeEval = eval(data.menu);
                            $.fn.zTree.init($("#treeDemo"), setting, nodeEval);
                        }, 'json');
                        $.messager.alert('编辑线路',data.msg,'info');
                    }, 'json');
                }
            });  
            //取消编辑线路信息
            $("#btnParentReload").click(function() {           
                $("#txtLineIDEdit").val('');
                $("#txtLineNameEdit").val('');
                $("#hidGWEdit").val('');
                $('#txtLineGwEdit').val('');
            });
                
            //线路区域添加
            $("#btnAreaOK").click(function() {   
                if($("#NodeCheckedID").val()==null ||$("#NodeCheckedID").val()==''){
                    $.messager.alert('区域划分','请选择线路,然后在分区域!','info');
                }else{
                    var areaID='';   
                    $("input[type=checkbox][checked]").each(function(){ //由于复选框一般选中的是多个,所以可以循环输出 
                        areaID+=$(this).val()+',';
                    }); 
                  
                    $.post("RouteManageNew.aspx", { param: 'AddAreaRelation',nodeKey:$("#NodeCheckedID").val(),aID:areaID,chkID:$('#AreaCheckedID').val()}, function (data) {
                        //新增记录后,重新绑定zTree
                        $.post("RouteManageNew.aspx", { param: ''}, function (data) {
                            var  nodeEval = eval(data.menu);
                            $.fn.zTree.init($("#treeDemo"), setting, nodeEval);
                        }, 'json');
                        $.messager.alert('区域信息',data.msg,'info');
                    }, 'json'); 
                 }
            }); 
                 
            //区域设备添加
            $("#btn_AreaAndDevice_Ok").click(function() { 
                             
                if($("#NodeCheckedID").val()==null ||$("#NodeCheckedID").val()==''){
                    $.messager.alert('区域','请选择所要添加的区域!','info');
                }else{
                    $.post("RouteManageNew.aspx", { param: 'AddDeviceRelation',sbID:$("#sbID").val(),nodeKey:$("#NodeCheckedID").val()}, function (data) {
                            $.post("RouteManageNew.aspx", { param: ''}, function (data) {
                                if(Number(data.count)==1){
                                    var  nodeEval = eval(data.menu);
                                    $.fn.zTree.init($("#treeDemo"), setting, nodeEval);
                                    $("#dv_Parent_Info").show();
                                    
                                     $.post("RouteManageNew.aspx", { param: 'clickTree',id:treeNode.id}, function (data) {
                                         if(Number(data.count)==3){
                                      
                                            var res=data.devs;
                                            
                                            $("#sb1").html('');
                                            
                                            if(res!=''){
                                                var re=res.split(',');  
                                                
                                                 res='</br></br>';
                                                
                                                for(j=0;j<re.length ;j++)
                                                {
                                                   res+="&nbsp;&nbsp;&nbsp;"+re[j]+"&nbsp;&nbsp;&nbsp;";
                                                }    
                                            }   
                                                                 
                                            $("#sb1").html(res);   
		          
                                        }
                                     }, 'json');
                                }else
                                {
                                    $("#dv_Parent_Info_father").show();
                                }
                            }, 'json');
                            $.messager.alert('区域',data.msg,'info');
                        }, 'json');
                }
            });
            
            //区域删除  
            $("#spDelArea").click(function() { 
            //qyID 
                if($("#qyID").val()==null ||$("#qyID").val()==''){
                    $.messager.alert('删除区域','请选择所要删除的区域,然后再进行删除操作!','info');
                }else{
                    $.post("RouteManageNew.aspx", { param: 'DelArea',AreaID:$("#qyID").val(),nodeKey:$("#NodeCheckedID").val()}, function (data) {
                            $.post("RouteManageNew.aspx", { param: ''}, function (data) {
                                if(Number(data.count)==1){
                                    var  nodeEval = eval(data.menu);
                                    $.fn.zTree.init($("#treeDemo"), setting, nodeEval);                                   
                                }
                            }, 'json');
                            
                            
                            
                            $.messager.alert('删除区域',data.msg,'info');
                        }, 'json');
                }
            }); 
            
            //添加点检项和设备
            $("#SaveDevAndItem").click(function() { 
            //qyID 
                    var ids = [];
                    var rows = $('#dgItem').datagrid('getSelections');
                    for (var i = 0; rows && i < rows.length; i++) {
                        var row = rows[i];                    
                        ids.push(row.JSON_id_key);                     
                    }
                    //alert(ids.join(','));
                    if(ids.join(',')!="")
                    {
                        //保存设备和点检项关系
                         $.post("RouteManageNew.aspx", { param: 'SaveRelation',id:$('#NodeCheckedID').val(),id_key_new:ids.join(','),id_key_old: $('#id_key_old').val()}, function (data) { 
                            $.post("RouteManageNew.aspx", { param: ''}, function (data) {
                                        if(Number(data.count)==1){
                                            var  nodeEval = eval(data.menu);
                                            $.fn.zTree.init($("#treeDemo"), setting, nodeEval);
                                            $("#dvLineOne").show();
                                            $("#dvItem").hide();
                                            $.post("RouteManageNew.aspx", { param: 'clickTree',id:$('#NodeCheckedID').val()}, function (data) {
                                               if(Number(data.count)==4)
                                               {
                                                     $('#id_key_old').val(data.id_key);
                                                     //alert(data.id_key);
                                               }
                                            }, 'json');
                                        }else
                                        {
                                            $("#dvItem").hide();
                                        }
                                    }, 'json');
                                  $.messager.alert('消息','关系更新成功!','info');
                                  CloseDivItem('MyItem','fade');
                                  $("#txtLineType").show();
                         }, 'json');
                    }else{
                         $.messager.alert('消息','请选择点检项后再保存!','info');
                    }
            }); 
                  
            //新建弹出div Tree
            $("#show_div").click(function() {  
                $.post("../ParentMember/ManageMember.aspx", { param: ''}, function (data) {
                    
                    if(Number(data.num)==1){
                        var  nodeEval = eval(data.menu);
                        
                        $.fn.zTree.init($("#treePerson"), setPerson, nodeEval);
                    
                    }else{$.messager.alert('信息','目前尚无部门岗位信息！','info');}                
                }, 'json');
            
                ShowDiv('MyDiv', 'fade');
            }); 
            //编辑弹出div Tree
            $("#show_divEdit").click(function() {  
                $.post("../ParentMember/ParentMember.aspx", { param: ''}, function (data) {
                    
                    if(Number(data.count)==1){
                        var  nodeEval = eval(data.menu);
                        
                        $.fn.zTree.init($("#treePerson"), setPerson, nodeEval);
                    
                    }else{$.messager.alert('信息','目前尚无部门岗位信息！','info');}                
                }, 'json');
            
                ShowDiv('MyDiv', 'fade');
            }); 
            //区域关联设备DIV 
            $("#spMyDevice").click(function() {  
                $.post("RouteManageNew.aspx", { param: 'DevicTree',areaNodeKey:$('#NodeCheckedID').val()}, function (data) {
                   if(Number(data.count)==1){
                        var  nodeEval = eval(data.menu);
                        
                        $.fn.zTree.init($("#treeDevice"), setDevice, nodeEval);
                    
                    }else{$.messager.alert('信息','设备表中没有设备记录','info');}                
                }, 'json');
               
                ShowDiv('MyDevice', 'fade');
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
                var devDesc='';
                var res='';
                
                for(var i=0;i<nodes.length;i++)
                {
                    deviceID+=nodes[i].id+',';     
                    devDesc+=nodes[i].name+',';               
                }
                //alert(devDesc);
                $("#sbID").val(deviceID);
                
                $("#sb1").html('');
                    
                if(devDesc!=''){
                    //devDesc=devDesc.trim(',');
                    var re=devDesc.split(',');  
                    
                     res='</br></br>';
                    
                    for(j=0;j<re.length ;j++)
                    {
                       res+="&nbsp;&nbsp;&nbsp;"+re[j]+"&nbsp;&nbsp;&nbsp;";
                    }    
                }   
                                     
                $("#sb1").html(res); 
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
    <input type="hidden" id="linkBut" />
    <input type="hidden" id="id_key_old" />
    <!-- 已经选中的区域ID集合 -->
    <div style="width: 948px; border-left: solid 1px #AED0EA; border-top: solid 1px #AED0EA;
        border-right: solid 1px #AED0EA;">
        <div id="toolbar" style="height: 26px; padding: 5px;">
            <a id="add" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'">Add</a>
            <a id="save" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-save'">Save</a>
            <a id="delete" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove'">
                Remove</a>
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
            <!--线路信息 -->
            <div id="dvLine" title="信息" data-options="tools:'#p-tools'" style="padding: 2px;">
                <%--<%=title1 %>--%>
                <div id="dvLineOne">
                    <table class="admintable" width="100%">
                        <tr>
                            <th class="adminth" colspan="2">
                                线路设置信息
                            </th>
                        </tr>
                        <tr>
                            <td class="admincls1" align="center">
                                线路名称
                            </td>
                            <td class="admincls1">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtLineName" type="text" />
                            </td>
                        </tr>
                        <tr>
                            <td class="admincls0" align="center">
                                线路类型
                            </td>
                            <td class="admincls0">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <select id="txtLineType" style="width: 60px;">
                                    <option id="dj" value="0">点检</option>
                                    <option id="xj" value="1">巡检</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td class="admincls1" align="center">
                                所属岗位
                            </td>
                            <td class="admincls1">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtLineGw" type="text" readonly="true" />
                                <span id="show_div"><a id="a1" href="#" class="easyui-linkbutton">选择</a></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="admincls1" align="center" colspan="2">
                                <a id="btnAddXLInfo" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok'">
                                    保存</a> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a id="btnReload" href="#" class="easyui-linkbutton"
                                        data-options="iconCls:'icon-reload'">取消</a>
                            </td>
                        </tr>
                    </table>
                </div>
                <!-- 线路信息编辑 -->
                <div id="dvEditLineOne">
                    <table class="admintable" width="100%">
                        <tr>
                            <th class="adminth" colspan="2">
                                线路设置信息
                            </th>
                        </tr>
                        <tr>
                            <td class="admincls1" align="center">
                                线路编码
                            </td>
                            <td class="admincls1">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtLineIDEdit" type="text" readonly="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="admincls0" align="center">
                                线路名称
                            </td>
                            <td class="admincls0">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtLineNameEdit" type="text" />
                            </td>
                        </tr>
                        <tr>
                            <td class="admincls1" align="center">
                                线路类型
                            </td>
                            <td class="admincls1">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <select id="txtLineTypeEdit" style="width: 60px;">
                                    <option id="Option1" value="0">点检</option>
                                    <option id="Option2" value="1">巡检</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td class="admincls0" align="center">
                                所属岗位
                            </td>
                            <td class="admincls0">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtLineGwEdit" type="text" readonly="true" />
                                <span id="show_divEdit"><a id="a3" href="#" class="easyui-linkbutton">选择</a></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="admincls0" align="center" colspan="2">
                                <a id="btnParentEdit" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-save'">
                                    保存</a> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a id="btnParentReload" href="#" class="easyui-linkbutton"
                                        data-options="iconCls:'icon-reload'">取消</a>
                            </td>
                        </tr>
                    </table>
                </div>
                <!-- 线路信息Info -->
                <div id="dvInfoLineOne">
                    <table class="admintable" width="100%">
                        <tr>
                            <th class="adminth" colspan="2">
                                线路信息
                            </th>
                        </tr>
                        <tr>
                            <td class="admincls1" align="center">
                                线路编码
                            </td>
                            <td class="admincls1">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtInfoBM" type="text" readonly="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="admincls0" align="center">
                                线路名称
                            </td>
                            <td class="admincls0">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtInfoName" type="text" readonly="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="admincls1" align="center">
                                线路类型
                            </td>
                            <td class="admincls1">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <select id="infoSel" style="width: 60px;">
                                    <option id="Option12" value="0">点检</option>
                                    <option id="Option13" value="1">巡检</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td class="admincls0" align="center">
                                所属岗位
                            </td>
                            <td class="admincls0">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtInfoGW" type="text" readonly="true" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="dvZXL">
                    <table class="admintable" width="100%">
                        <tr>
                            <th class="adminth" colspan="2">
                                线路信息
                            </th>
                        </tr>
                        <tr>
                            <td class="admincls1" align="center">
                                线路名称
                            </td>
                            <td class="admincls1">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtZXL" type="text" readonly="true"
                                    value="东风电厂总线路" />
                            </td>
                        </tr>
                    </table>
                </div>
                <!-- 区域分配 -->
                <div id="dvArea">
                    <table class="admintable" width="100%">
                        <tr>
                            <th class="adminth" colspan="2">
                                区域信息
                            </th>
                        </tr>
                        <tr>
                            <td class="admincls0" align="center">
                                所属线路
                            </td>
                            <td class="admincls0">
                                <%--<input id="LineID" type="text" />--%>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="LineAreaName" disabled="disabled"
                                    type="text" />
                            </td>
                        </tr>
                        <tr>
                            <td class="admincls1" align="left" colspan="2">
                                <div id="dv_Area" style="padding-left: 50px;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="admincls0" align="center" colspan="2">
                                <a id="btnAreaOK" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok'">
                                    保存</a> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a id="btn_Members_Reload" href="#" class="easyui-linkbutton"
                                        data-options="iconCls:'icon-reload'">取消</a>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="dvAreaInfo">
                    <table class="admintable" width="100%">
                        <tr>
                            <th class="adminth" colspan="2">
                                区域信息
                            </th>
                        </tr>
                        <tr>
                            <td class="admincls0" align="center">
                                所属线路
                            </td>
                            <td class="admincls0">
                                <%--<input id="LineID" type="text" />--%>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="LineAreaNameInfo" disabled="disabled"
                                    type="text" />
                            </td>
                        </tr>
                        <tr>
                            <td class="admincls1" align="center">
                                下属设备
                            </td>
                            <td class="admincls1">
                                <div id="dv_AreaInfo">
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <!-- 设备分配 -->
                <div id="dvDevice" data-options="tools:'#p-tools'" style="padding: 1px;">
                    <table class="admintable" width="100%">
                        <tr>
                            <th class="adminth" colspan="2">
                                设备信息
                            </th>
                        </tr>
                        <tr>
                            <td class="admincls1" align="center">
                                所属区域
                            </td>
                            <td class="admincls1">
                                <input type="hidden" id="qyID" />
                                <input type="hidden" id="sbID" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtQyName" disabled="disabled"
                                    type="text" />&nbsp;&nbsp;<%--<span id="spDelArea"><a id="a5" href="#" class="easyui-linkbutton">删除区域</a></span>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="admincls0" align="center">
                                下属设备
                            </td>
                            <td class="admincls0">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span id="spMyDevice"><a id="a4" href="#"
                                    class="easyui-linkbutton">选择</a></span></br>
                                <div id="sb1">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="admincls0" align="center" colspan="2">
                                <a id="btn_AreaAndDevice_Ok" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok'">
                                    保存</a> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a id="btn_AreaAndDevice_Reload" href="#"
                                        class="easyui-linkbutton" data-options="iconCls:'icon-reload'">取消</a>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="dvItem" data-options="tools:'#p-tools'" style="padding: 2px;">
                    <table class="admintable" width="100%">
                        <tr>
                            <th class="adminth" colspan="2">
                                设备信息
                            </th>
                        </tr>
                        <tr>
                            <td class="admincls1" align="center">
                                设备名称
                            </td>
                            <td class="admincls1">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtInfoDevName" disabled="disabled"
                                    type="text" />&nbsp;&nbsp;<%--<span id="spDelArea"><a id="a5" href="#" class="easyui-linkbutton">删除区域</a></span>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="admincls0" align="center">
                                下属点检项
                            </td>
                            <td class="admincls0">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <%--<span id="Span1"><a id="a5" href="#"
                                class="easyui-linkbutton">选择</a></span></br>--%>
                                <div id="dvDevItem">
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="dvItemInfo" data-options="tools:'#p-tools'" style="padding: 2px;">
                    <table class="admintable" width="100%">
                        <tr>
                            <td class="admincls1">
                                &nbsp;&nbsp;点检项部位:
                            </td>
                            <td class="admincls1">
                                <input type="text" id="DJXBW" readonly="true" />
                            </td>
                            <td class="admincls1">
                                &nbsp;&nbsp;点检项描述:
                            </td>
                            <td class="admincls1">
                                <input type="text" id="DJXDESC" readonly="true" />
                            </td>
                            <td class="admincls1">
                                &nbsp;&nbsp;<%--点检项编码:--%>
                            </td>
                            <td class="admincls1">
                                &nbsp;&nbsp;<%-- <input type="text" id="DJXID" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="1" class="admincls0">
                                &nbsp;&nbsp;检查内容:
                            </td>
                            <td class="admincls0" colspan="5">
                                <input style="width: 400px; height: 40px;" type="text" id="DJXCONTENT" readonly="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="admincls1">
                                &nbsp;&nbsp;数据名称:
                            </td>
                            <td class="admincls1">
                                <input type="text" id="DJXObserve" readonly="true" />
                            </td>
                            <td class="admincls1">
                                &nbsp;&nbsp;单位:
                            </td>
                            <td class="admincls1">
                                <input type="text" id="DJXUnit" readonly="true" />
                            </td>
                            <td class="admincls1">
                                &nbsp;&nbsp;
                            </td>
                            <td class="admincls1">
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="admincls0">
                                &nbsp;&nbsp;点检类型:
                            </td>
                            <td class="admincls0">
                                <input type="text" id="DJXType" readonly="true" readonly="true" />
                            </td>
                            <td class="admincls0">
                                &nbsp;&nbsp;设备状态:
                            </td>
                            <td class="admincls0">
                                <input type="text" id="DJXSelectStatus" readonly="true" />
                            </td>
                            <td class="admincls0">
                                &nbsp;&nbsp;点检类型:
                            </td>
                            <td class="admincls0">
                                <input type="text" id="DJXT_SATAUS" readonly="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="admincls1">
                                &nbsp;&nbsp;测量上限:
                            </td>
                            <td class="admincls1">
                                <input type="text" id="DJXUpper" readonly="true" />
                            </td>
                            <td class="admincls1">
                                &nbsp;&nbsp;测量下限:
                            </td>
                            <td class="admincls1">
                                <input type="text" id="DJXLower" readonly="true" />
                            </td>
                            <td class="admincls1">
                                &nbsp;&nbsp;是否频谱:
                            </td>
                            <td class="admincls1">
                                <input type="text" id="DJXSpectrum" readonly="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="admincls0">
                                &nbsp;&nbsp;开始时间:
                            </td>
                            <td class="admincls0">
                                <input type="text" id="DJXSTARTTIME" readonly="ture" />
                            </td>
                            <td class="admincls0">
                                &nbsp;&nbsp;周期数值:
                            </td>
                            <td class="admincls0">
                                <input type="text" id="DJXPERIODVALUE" readonly="ture" />
                            </td>
                            <td class="admincls0">
                                &nbsp;&nbsp;周期类型:
                            </td>
                            <td class="admincls0">
                                <input type="text" id="Select1" readonly="ture" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div id="fade" class="black_overlay">
    </div>
    <!-- 岗位DIV -->
    <div id="MyDiv" class="white_content">
        <div id="dvPoint">
            <ul id="treePerson" class="ztree">
            </ul>
        </div>
        <div style="text-align: center; cursor: default; height: 15px;">
            <a id="A2" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-reload'"
                onclick="CloseDiv('MyDiv','fade')">选择</a>
        </div>
    </div>
    <!-- 设备DIV -->
    <div id="MyDevice" class="white_content">
        <div id="Div2">
            <ul id="treeDevice" class="ztree">
            </ul>
        </div>
        <div style="text-align: center; cursor: default; height: 15px;">
            <a id="A6" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-reload'"
                onclick="CloseDiv('MyDevice','fade')">选择</a>
        </div>
    </div>
    <div id="MyItem" class="white_content">
        <table id="dgItem" class="easyui-datagrid">
        </table>
        <div id="s" style="height: 10px;">
        </div>
        <div style="text-align: center; cursor: default; height: 15px;">
            <a id="SaveDevAndItem" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-reload'">
                保存</a> &nbsp;&nbsp;&nbsp;<a id="A11" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-reload'"
                    onclick="CloseDivItem('MyItem','fade')">关闭</a>
        </div>
    </div>
    </form>
</body>
</html>
