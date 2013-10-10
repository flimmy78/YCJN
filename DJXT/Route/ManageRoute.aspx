<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageRoute.aspx.cs" Inherits="DJXT.Route.ManageRoute" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>线路整理维护</title>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../css/zTreeStyle/djxt.css" rel="stylesheet" type="text/css" />
    <link href="../css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />

    <script src="../jQueryEasyUI/jquery-1.6.2.js" type="text/javascript"></script>

    <script src="../js/jquery.ztree.core-3.5.js" type="text/javascript"></script>

    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="../js/My97DatePicker/WdatePicker.js"></script>

    <script type="text/javascript" src="../js/jquery.ztree.excheck-3.5.js"></script>

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
         var setPer={
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
            if(treeId=='treeGW'){         
                $('#txtLineGw').val(treeNode.name);
                $('#HidGwName').val(treeNode.name);
                $('#HidGwID').val(treeNode.id);                
            }
        }; 
       
        function onClick(event, treeId, treeNode, clickFlag) {
            $("#HidNodeKey").val(treeNode.id);
               
		    $.post("ManageRoute.aspx", { param: 'clickTree',id:treeNode.id}, function (data) {               
                if(Number(data.count)==1){
			        //总线路			       
                  　$("#divZXL").show();
                  　$("#divXL").hide();
                  　$("#divQY").hide();
                  　$("#divSB").hide();                  　
                }else if(Number(data.count)==2){
                    GridQY(treeNode.id);
                    //GridAre()
                    $("#divZXL").hide();
			        $("#divXL").show();
			        $("#divQY").hide();
			        $("#divSB").hide();	
			        $("#txtLbm").val(data.lID);
                    $("#txtLName").val(data.lName);
                    if(Number(data.lType)==0){
                        $("#txtLtype").val('点检');
                    }else{
                        $("#txtLtype").val('巡检');
                    }                    
                    $("#txtLgw").val(data.lOrgName);  
                    
                    //获取此线路下所有区域ID 
                    $("#HidAreaID").val(data.lineAreaID);
                    $("#AreaID").val(data.areaID2);                                  	           
                }else if(Number(data.count)==3){
                      $("#divZXL").hide();
			          $("#divXL").hide();
			          $("#divQY").show();
			          $("#divSB").hide();
			          
			          $("#txtQYbm").val(data.areaID);
                      $("#txtQYcd").val(data.areacd);
                      $("#txtQYname").val(data.areaName);
			         
			          GridDev(treeNode.id)
                }else if(Number(data.count)==4){
                      $("#divZXL").hide();
			          $("#divXL").hide();
			          $("#divQY").hide();
			          $("#divSB").show();
			          
			          $("#txtSBbm").val(data.devid);
                      $("#txtSBname").val(data.devfid);
                      $("#txtSBfbm").val(data.devdes);
                      
                       //获取此线路下所有区域ID 
                      $("#hidsbidkey").val(data.sbIdKey);
                      $("#hidsbnodekey").val(data.sbNodekey);     
                      
                      GridItem(treeNode.id);
                      //alert(treeNode.id);
                }else if(Number(data.count)==5){
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
                      DJXInfoShow();
                }
                else{                
                   //alert(data.count);                        
		        }		       
		     }, 'json');    		      		     
		};	
        
        $(document).ready(function(){
            $("#menu").css("height",pageHeight()-100);
            $("#menu").css("width",pageWidth()-100);
            $("#dv_tree").css("height",pageHeight()-118);
            $("#tree").css("height",pageHeight()-120);                      
            
            $("#dvLineOne").hide();
            $("#divGW").hide();
            $("#divXL").hide();
            $("#divAllQY").hide();
            $("#divQY").hide();
            $("#divSBTree").hide();
            $("#divSB").hide();
            $("#divItem").hide();
            $("#dvItemInfo").hide();
            Tree();
            ReadInfo(""); 
            GridXL();     //加载总线路下所有线路         
            
            //选择岗位Tree
            $("#show_div").click(function() { 
                $("#txtLineType").hide();                  
                GWTreeShow();
            });   
		});
		
		function GridXL(){
            $('#gridXL').datagrid({	
                title:'下属线路',
			    nowrap: true,
			    autoRowHeight:false,
			    striped: true,
			    height:520,
			    align:'center',
			    collapsible:true,
			    url:'ManageRoute.aspx',
			    sortName: 'id_key',
			    sortOrder: 'asc',
			    remoteSort: false,
			    queryParams:{param:'queryXL',SBID:'0'},
			    idField:'id_key',			   
			    frozenColumns: [[
                    { title: '线路编码', field: 'routeid', width: 70, sortable: true ,align:'center'},
                    { title: '线路名称', field: 'routename', width: 110, sortable: true ,align:'center'}                            
                ]],
                columns: [[
                    { title: '线路类型 ', field: 'rtype',formatter:function(value,rec,index){return value==0?'点检':'巡检'}, width: 80,align:'center' }   
                    //{ title: '所属岗位', field: 'routegw', width: 80 ,align:'center'}                                
                ]],     
			    pagination:true,
			    rownumbers:true,
			    toolbar:[{
			        id:'btnadd', 
			        text:'添加',
			        iconCls:'icon-add',			       
			        handler:function(){			           
                        AddXLShow();                
			        }
		        },
		        {
			        id:'btnadd', 
			        text:'编辑',
			        iconCls:'icon-edit',
			        handler:function(){                        
                        EditXLShow(); 
			        }
		        }]			
		    });
		}
		
		//线路新增
		function AddXLShow(){		    
		    $("#dvLineOne").attr('title','新增线路');
		    $('#txtLineName').val('');
		    $('#txtLineGw').val('');
		    $('#HidGwID').val('');
		    $('#HidGwName').val('');
		    $('#dvLineOne').show();			    	    
			$('#dvLineOne').dialog({
				buttons:[{
					text:'保存',
					iconCls:'icon-ok',
					handler:function(){
					    if($("#txtLineName").val()==null || escape($("#txtLineName").val())=="" ){
                            $.messager.alert('新增线路','请输入线路名称!','error');
                        }else{
                            $.post("ManageRoute.aspx", { param: 'AddLineInfo',lineName:escape($("#txtLineName").val()),lineType:escape($("#txtLineType").val()),lineGw:escape($("#HidGwID").val())}, function (data) {
                                 if(Number(data.count)==1){
                                       Tree();
                                       $('#gridXL').datagrid('reload');                                                       
                                 }
                                 $.messager.alert('添加线路',data.msg,'info');
                                 $('#dvLineOne').dialog('close');                       
                            }, 'json');                   
                        }    
				    }
				 },{
					text:'取消',
					iconCls:'icon-reload',
					handler:function(){
						$('#dvLineOne').dialog('close');
						$('#txtLineGw').val('');
                        $('#HidGwName').val('');
                        $('#HidGwID').val('');
                        $('#txtLineName').val('');  
					}
				}]
			});
		}
                
		//线路修改
		function EditXLShow(){
		    $("#dvLineOne").attr('title','修改线路');		  		    
		    var row = $('#gridXL').datagrid('getSelected');  
            if (row){
                $('#txtLineType').val(row.rtype);
                $('#txtLineName').val(row.routename);
                $('#HidLineID').val(row.routeid);               
                $('#dvLineOne').show();	        
                $('#dvLineOne').dialog({
				buttons:[{
					text:'保存',
					    iconCls:'icon-ok',
					    handler:function(){					        
					        $.post("ManageRoute.aspx", { param: 'EditLineInfo',lineID:$("#HidLineID").val(),lineName:escape($("#txtLineName").val()),lineType:escape($("#txtLineType").val()),lineGw:escape($("#HidGwID").val())}, function (data) {
                                if(Number(data.count)==1){
                                       Tree();
                                       $('#gridXL').datagrid('reload');                                                       
                                 }
                                 $.messager.alert('编辑线路',data.msg,'info');
                                 $('#dvLineOne').dialog('close');
                            }, 'json');
					    }
				    },{
					text:'取消',
					    iconCls:'icon-reload',
					    handler:function(){
						    $('#dvLineOne').dialog('close');
						    $('#txtLineGw').val('');
                            $('#HidGwName').val('');
                            $('#HidGwID').val(''); 
                            $('#txtLineName').val('');
                            $('#HidLineID').val('');
					    }
				    }]
			    });
                
            }else{
                alert("请选择要操作的数据!");
            }
            
            $.post("ManageRoute.aspx", { param: 'EditStatus',IDKEY:idKey,StartTime:time,type:type}, function (data) {   
                $('#gridStatus').datagrid('reload');                              
                $.messager.alert('修改设备状态',data.info,'info');                
            }, 'json');       
		}
		
		//弹出岗位TREE
		function GWTreeShow(){		    
		    $("#divGW").attr('title','选择岗位');		   
		    $('#divGW').show();
		    $.post("../ParentMember/ManageMember.aspx", { param: ''}, function (data) {                    
                    if(Number(data.num)==1){
                        var  nodeEval = eval(data.res);                        
                        $.fn.zTree.init($("#treeGW"), setPer, nodeEval);                                                            
                    }else{
                        $.messager.alert('信息','目前尚无部门岗位信息！','info');
                    }                
                }, 'json');			    	    
			$('#divGW').dialog({
				buttons:[{
					text:'选择',
					iconCls:'icon-ok',
					handler:function(){
					     $('#divGW').dialog('close');
					     $("#txtLineType").show(); 
				    }
				 },{
					text:'取消',
					iconCls:'icon-reload',
					handler:function(){					   
						$('#divGW').dialog('close');
						$('#txtLineGw').val('');
                        $('#HidGwName').val('');
                        $('#HidGwID').val('');    
						$("#txtLineType").show(); 
					}
				}]
			});			
		}
		
		function GridQY(id){
            $('#gridQY').datagrid({
                title:'下属区域',		 			   
			    nowrap: true,
			    autoRowHeight:false,
			    striped: true,
			    align:'center',
			    collapsible:true,
			    url:'ManageRoute.aspx',
			    sortName: 'bk',
			    sortOrder: 'asc',
			    remoteSort: false,
			    queryParams:{param:'queryQY',SBID:id},
			    idField:'bk',			   
			    frozenColumns: [[
			        {field: 'chk', checkbox: true },
                    { title: '区域编码', field: 't_areaid', width: 70, sortable: true ,align:'center'},
                    { title: '射频卡编码 ', field: 't_areacd', width: 80,sortable: true ,align:'center' }
                        
                ]],
                columns: [[
                    { title: '区域名称', field: 't_areaname', width: 110, sortable: true ,align:'center'},                       
                    {field:'bk',title:'',width:120,hidden:true}                                        
                ]],     
			    pagination:true,
			    rownumbers:true,			    		            			
			    toolbar:[{
			        id:'btnadd', 
			        text:'添加',
			        iconCls:'icon-add',			       
			        handler:function(){	
			            GridAre();	 		           
                        AddQYShow();                
			        }
		        },
		        {
			        id:'btnadd', 
			        text:'删除',
			        iconCls:'icon-remove',
			        handler:function(){    
			                            
                        var rows = $('#gridQY').datagrid('getSelections');
                        
                        if (rows){
//                            var id="";
//                            var name="";
//                            $.each(rows,function(i,n){
//	                            id += n.t_areaid+",";	                      
//	                        });
//	               	        id = id.substring(0,id.length-1);
//    	                    //alert(id);
//                            $.messager.confirm('删除组织信息', '你确定要删除吗?', function(ok){
//		                        if (ok){
//		                            $.post("ManageRoute.aspx", { param: 'DelArea',AreaID:id,nodeKey:$("#HidNodeKey").val()}, function (data) {
//                                        $.post("ManageRoute.aspx", { param: ''}, function (data) {
//                                            if(Number(data.count)==1){
//                                                var  nodeEval = eval(data.menu);
//                                                $.fn.zTree.init($("#treeDemo"), setting, nodeEval);
//                                                $("#dv_Parent_Info").show();
//                                            }else
//                                            {
//                                                $("#dv_Parent_Info_father").show();
//                                            }
//                                        }, 'json');
//                                        $.messager.alert('删除区域',data.msg,'info');
//                                    }, 'json');
//		                        }else{
//		                            $.messager.alert('删除区域','删除已取消!','info');
//		                        }
//	                        });   
	                    }else { $.messager.alert('删除区域','请选择需要删除的数据!','info');}
			        }
		        }]			
		    });
		}
		
		//线路新增区域
		function AddQYShow(){		    
		    $("#divAllQY").attr('title','新增区域');		   	   
		   	$("#divAllQY").show();
			$('#divAllQY').dialog({
				buttons:[{
					text:'保存', //HidNodeKey
					iconCls:'icon-ok',
					handler:function(){
					    var rows = $('#gridArea').datagrid('getSelections');
					    if (rows){
                            var id="";
                            var name="";
                            $.each(rows,function(i,n){
                                id += n.t_areaid+',';
                                name+=n.t_areaname+',';
                            });
	                        name = name.substring(0,name.length-1);
	                        id = id.substring(0,id.length-1);
					        //alert(id+name);
					        if(id!=''){				       
                                $.post("ManageRoute.aspx", { param: 'AddAreaRelation',nodeKey:$("#HidNodeKey").val(),aID:id,chkID: $("#AreaID").val()}, function (data) {
                                     if(Number(data.count)==1){
                                           Tree();
                                           $('#gridQY').datagrid('reload');                                                       
                                     }
                                     $.messager.alert('添加区域',data.msg,'info');
                                     $('#divAllQY').dialog('close');                       
                                }, 'json'); 
                           }else{ $.messager.alert('新增区域','请选择需要新的区域!','info');}  
                        }else
                        { $.messager.alert('新增区域','请选择需要操作的数据!','info');}  
				    }
				 },{
					text:'取消',
					iconCls:'icon-reload',
					handler:function(){
						$('#divAllQY').dialog('close');						  
					}
				}]
			});
		}
			
		function GridAre(){
            $('#gridArea').datagrid({
                //title:'勾选区域',		 			   
			    nowrap: true,
			    autoRowHeight: false,
			    striped: true,
			    align:'center',
			    collapsible:true,
			    url:'ManageArea.aspx',
			    sortName: 'id_key',
			    sortOrder: 'asc',
			    remoteSort: false,
			    queryParams:{param:'query'},
			    idField:'id_key',			    
			    columns:[[	
			        {field: 'chk', checkbox: true },
			        {field:'t_areaid',title:'区域编码',width:60,align:'center'},
                    {field:'t_areacd',title:'射频卡编码',width:100,align:'center'},
                    {field:'t_areaname',title:'区域名称',width:100,align:'center'},
                    {field:'t_routeid',title:'',width:100,hidden:true},                    
                    {field:'id_key',title:'',width:100,hidden:true}
                ]],
			    pagination:true,
			    rownumbers:true,
			    onLoadSuccess:function(){  //当数据加载成功时触发
                  var userIds =$("#HidAreaID").val(); //隐藏字段获取值该字符串有，号分隔                 
                  var userIda =  userIds.split(',');
                  for(var i in userIda){
                      $('#gridArea').datagrid('selectRecord',userIda[i]);//根据id选中行
                  }                 
               }			            			
		    });
		}
		//读取信息
		function ReadInfo(id){
	        if(id==""){
                $("#txtXLid").val('DFZXL');
                $("#txtXLname").val('东风电厂总线路');
                $("#txtXLtype").val('');
                $("#txtXLgw").val('');
            }else{
                $.post("ManageRoute.aspx", { param: 'load',SBID:id}, function (data) {            
                    if(Number(data.count)==0){
                    
                    }  
                }, 'json');             
            } 
		}		
	    //区域下属设备
		function GridDev(id){
            $('#gridSB').datagrid({
                title:'下属设备',		 			   
			    nowrap: true,
			    autoRowHeight:false,
			    striped: true,
			    align:'center',
			    collapsible:true,
			    url:'ManageRoute.aspx',
			    sortName: 'dk',
			    sortOrder: 'asc',
			    remoteSort: false,
			    queryParams:{param:'querySB',SBID:id},
			    idField:'dk',			   
			    columns:[[	
			        {field:'t_deviceid',title:'设备编码',width:100,align:'center'},                    
                    {field:'t_parentid',title:'父级编码',width:100,align:'center'},
                    {field:'t_devicedesc',title:'设备名称',width:150,align:'center'},
                    {field:'rowid',title:'',width:100,hidden:true}, 
                    {field:'dk',title:'',width:100,hidden:true},
                    {field:'ik',title:'',width:100,hidden:true},                              
                    {field:'t_nodekey',title:'',width:100,hidden:true}
                ]],
			    pagination:true,
			    rownumbers:true,			    		            			
			    toolbar:[{
			        id:'btnadd', 
			        text:'新增',
			        iconCls:'icon-add',			       
			        handler:function(){			            			            	 		           
                        DeviceTree(id);                
			        }		        
		        }]			
		    });
		}
		
		//弹出岗位TREE
		function DeviceTree(devs){		    
		    $("#divSBTree").attr('title','选择设备');		   
		    $('#divSBTree').show();
		    
		    $.post("ManageRoute.aspx", { param: 'DevicTree',SBID:devs}, function (data) {
                if(Number(data.count)==1){
                    var  nodeEval = eval(data.menu);
                    $.fn.zTree.init($("#treeDev"), setDevice, nodeEval);                                                            
                }else{
                    $.messager.alert('信息','目前无法获取设备信息!','info');
                }               
            }, 'json'); 		    
		   		    	    
			$('#divSBTree').dialog({
				buttons:[{
					text:'确定',
					iconCls:'icon-ok',
					handler:function(){
					    var treeObj = $.fn.zTree.getZTreeObj("treeDev");
                        var nodes = treeObj.getCheckedNodes(true);
                        var deviceID='';
                        var devDesc='';
                        var res='';
                
                        for(var i=0;i<nodes.length;i++)
                        {
                            deviceID+=nodes[i].id+',';     
                            devDesc+=nodes[i].name+',';               
                        }	
                        $.post("ManageRoute.aspx", { param: 'AddDeviceRelation',newSBID:deviceID,nodeKey:devs}, function (data) {
                            if(Number(data.count)==1){
                                    Tree();
                                    $('#gridSB').datagrid('reload');                                                       
                             }
                             $.messager.alert('新增设备',data.msg,'info');
                             $('#divSBTree').dialog('close'); 
                        }, 'json');	    
				    }
				 },{
					text:'取消',
					iconCls:'icon-reload',
					handler:function(){					   
						$('#divSBTree').dialog('close');						
					}
				}]
			});			
		}
		
		//设备下面点检项
		function GridItem(id){
            $('#gridDJX').datagrid({
                title:'下属点检项',		 			   
			    nowrap: true,
			    autoRowHeight:false,
			    striped: true,
			    align:'center',
			    collapsible:true,
			    url:'ManageRoute.aspx',
			    sortName: 'id_key',
			    sortOrder: 'asc',
			    remoteSort: false,
			    queryParams:{param:'queryDJX',SBID:id},
			    idField:'id_key',			   
			    frozenColumns: [[
                    { title: '点检项描述', field: 't_itemdesc', width: 100, sortable: true },
                    { title: '点检项部位', field: 't_itemposition', width: 100, sortable: true }                            
                ]],
                columns: [[
                    { title: '观察名称', field: 't_observe', width: 100 },   
                    { title: '单位', field: 't_unit', width: 100 }, 
                    { title: '测量上限', field: 'f_upper', width: 80 },
                    { title: '测量下限', field: 'f_lower', width: 80 },
                    { field:'t_status',title:'',width:120,hidden:true},
                    { field:'i_status',title:'',width:120,hidden:true},
                    { field:'i_spectrum',title:'',width:120,hidden:true},
                    { field:'t_starttime',title:'',width:120,hidden:true},
                    { field:'t_periodvalue',title:'',width:120,hidden:true},
                    { field:'t_periodtype',title:'',width:120,hidden:true},
                    { title: '类型', field: 't_type',formatter:function(value,rec,index){return value==0?'点检':'巡检'},width: 80 },                                
                    { title: '操作', field: 'id_key', width: 80, formatter: function (value, rec) {
                        //return '<a style="color:red" href="#" onclick="ShowHisItems('+value+');" >查看历史</a>';
                        return '<a style="color:red" href="../Device/msgInfo.aspx?id='+value+'" target="_blank">查看记录</a>';                                                      
                    }
                    }
                ]],     
			    pagination:true,
			    rownumbers:true,			    		            			
			    toolbar:[{
			        id:'btnadd', 
			        text:'新增',
			        iconCls:'icon-add',			       
			        handler:function(){	
			            GridIte(id);		            			            	 		           
                        AddDJXShow(id);                
			        }		        
		        }]			
		    });
		}
		//勾选点检项
		function GridIte(id){
            $('#gridItem').datagrid({
                //title:'勾选区域',		 			   
			    nowrap: true,
			    autoRowHeight: false,
			    striped: true,
			    align:'center',
			    collapsible:true,
			    url:'ManageRoute.aspx',
			    sortName: 'id_key',
			    sortOrder: 'asc',
			    remoteSort: false,
			    queryParams:{param:'queryDJXall1',SBID:id},
			    idField:'id_key',			    
			    frozenColumns: [[//冻结的列，不会随横向滚动轴移动
    	                    {field: 'chk', checkbox: true },
                            { title: '点检项描述', field: 't_itemdesc', width: 100, sortable: true },
                            { title: '点检项部位', field: 't_itemposition', width: 100, sortable: true }                            
			            ]],
                        columns: [[
                            { title: '观察名称', field: 't_observe', width: 100 },   
                            { title: '单位', field: 't_observe', width: 100 }, 
                            { title: '测量上限', field: 'f_upper', width: 80 },
                            { title: '测量下限', field: 'f_lower', width: 80 },
                            { title: '类型', field: 't_type',formatter:function(value,rec,index){return value==0?'点检':'巡检'},width: 80 }                    
                        ]],
			    pagination:true,
			    rownumbers:true,
			    onLoadSuccess:function(){  //当数据加载成功时触发
			     
                  var userIds =$("#hidsbidkey").val(); //隐藏字段获取值该字符串有，号分隔                 
                  var userIda =  userIds.split(',');
                  for(var i in userIda){
                      $('#gridItem').datagrid('selectRecord',userIda[i]);//根据id选中行
                  }                 
               }			            			
		    });
		}
		
		//设备增加点检项
		function AddDJXShow(id1){		    
		    $("#divItem").attr('title','新增点检项');		   	   
		   	$("#divItem").show();
			$('#divItem').dialog({
				buttons:[{
					text:'保存', //HidNodeKey
					iconCls:'icon-ok',
					handler:function(){
					    var rows = $('#gridItem').datagrid('getSelections');
					    if (rows){
                            var id="";
                            var name="";
                            $.each(rows,function(i,n){
                                id += n.id_key+',';                               
                            });	                       
	                        id = id.substring(0,id.length-1);
					        //alert(id+name);
					        if(id!=''){				       
                                  $.post("ManageRoute.aspx", { param: 'SaveRelation',id:id1,id_key_new:id,id_key_old: $('#hidsbidkey').val()}, function (data) { 
                                       if(Number(data.count)==1){
                                        Tree();
                                        $('#gridItem').datagrid('reload');                                                       
                                     }
                                      $.messager.alert('消息','关系更新成功!','info');
                                      $('#divItem').dialog('close'); 
                                  }, 'json');
                           }else{ $.messager.alert('新增区域','请选择需要新的区域!','info');}  
                        }else
                        { $.messager.alert('新增区域','请选择需要操作的数据!','info');}  
				    }
				 },{
					text:'取消',
					iconCls:'icon-reload',
					handler:function(){
						$('#divItem').dialog('close');						  
					}
				}]
			});
		}
		
		//设备增加点检项
		function DJXInfoShow(){		    
		    $("#dvItemInfo").attr('title','点检项信息');		   	   
		   	$("#dvItemInfo").show();
			$('#dvItemInfo').dialog({
				buttons:[{
					text:'确定', //HidNodeKey
					iconCls:'icon-ok',
					handler:function(){

                         $('#dvItemInfo').dialog('close');	                                                           
                             
				    }
				 }]
			});
		}
		
		//线路TREE方法
        function Tree(){
            $.post("ManageRoute.aspx", { param: ''}, function (data) {
                if(Number(data.count)==1){
                    var  nodeEval = eval(data.menu);
                    $.fn.zTree.init($("#tree"), setting, nodeEval);                                                            
                }               
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
		
    </script>

</head>
<body>
    <form id="formMember" runat="server">
    <div id="menu">
        <input type="hidden" id="NodeCheckedID" />
        <input type="hidden" id="HidGwID" />
        <input type="hidden" id="HidGwName" />
        <input type="hidden" id="HidLineID" />
        <input type="hidden" id="HidNodeKey" />
        <input type="hidden" id="HidAreaID" />
        <input type="hidden" id="AreaID" />
        <input type="hidden" id="hidsbidkey" />
        <input type="hidden" id="hidsbnodekey" />
        <!-- 线路ID -->
        <table id="__01" width="100%" height="95%" border="0" cellpadding="0" cellspacing="0"
            bgcolor="#2a88bb">
            <tr>
                <td colspan="3" background="../img/table-head.jpg" height="30px" valign="middle"
                    class="style6">
                    &nbsp;线路管理
                </td>
            </tr>
            <tr>
                <td background="../img/table-head-2.jpg" height="25px" valign="middle" class="style5">
                    &nbsp;线路列表
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
                    <div id="divZXL" class="right">
                        <ul>
                            <li>
                                <div id="divXLInfo">
                                    <table>
                                        <tr>
                                            <td>
                                                <p style="font-size: 12px;">
                                                    &nbsp;&nbsp; 线路编码 &nbsp;
                                                    <input id="txtXLid" class="easyui-validatebox" type="text" name="name" style="border: solid 1px #E0ECF9;
                                                        font-size: 12px; width: 90px; text-align: center;" disabled="disabled" />
                                                    &nbsp; 线路名称 &nbsp;
                                                    <input id="txtXLname" class="easyui-validatebox" type="text" name="name" style="border: solid 1px #E0ECF9;
                                                        font-size: 12px; width: 120px; text-align: center;" disabled="disabled" />
                                                    &nbsp; 线路类型 &nbsp;
                                                    <input id="txtXLtype" class="easyui-validatebox" type="text" name="name" style="border: solid 1px #E0ECF9;
                                                        font-size: 12px; width: 90px; text-align: center;" disabled="disabled" />
                                                    &nbsp; 所属岗位 &nbsp;
                                                    <input id="txtXLgw" class="easyui-validatebox" type="text" name="name" style="border: solid 1px #E0ECF9;
                                                        font-size: 12px; width: 90px; text-align: center;" disabled="disabled" /></p>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </li>
                            <li>
                                <div id="divXL1">
                                    <table id="gridXL">
                                    </table>
                                </div>
                            </li>
                        </ul>
                    </div>
                    <div id="divXL" class="right">
                        <ul>
                            <li>
                                <div>
                                    <table>
                                        <tr>
                                            <td>
                                                <p style="font-size: 12px;">
                                                    &nbsp;&nbsp; 线路编码 &nbsp;
                                                    <input id="txtLbm" class="easyui-validatebox" type="text" name="name" style="border: solid 1px #E0ECF9;
                                                        font-size: 12px; width: 90px; text-align: center;" disabled="disabled" />
                                                    &nbsp; 线路名称 &nbsp;
                                                    <input id="txtLName" class="easyui-validatebox" type="text" name="name" style="border: solid 1px #E0ECF9;
                                                        font-size: 12px; width: 120px; text-align: center;" disabled="disabled" />
                                                    &nbsp; 线路类型 &nbsp;
                                                    <input id="txtLtype" class="easyui-validatebox" type="text" name="name" style="border: solid 1px #E0ECF9;
                                                        font-size: 12px; width: 90px; text-align: center;" disabled="disabled" />
                                                    &nbsp; 所属岗位 &nbsp;
                                                    <input id="txtLgw" class="easyui-validatebox" type="text" name="name" style="border: solid 1px #E0ECF9;
                                                        font-size: 12px; width: 90px; text-align: center;" disabled="disabled" /></p>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </li>
                            <li>
                                <div id="div3">
                                    <table id="gridQY">
                                    </table>
                                    <%--<div id="gh" style=" height:2px;"></div>
                                    <table id="gridArea">
                                    </table>--%>
                                </div>
                            </li>
                        </ul>
                    </div>
                    <div id="divQY" class="right">
                        <ul>
                            <li>
                                <div>
                                    <table>
                                        <tr>
                                            <td>
                                                <p style="font-size: 12px;">
                                                    &nbsp;&nbsp; 区域编码 &nbsp;
                                                    <input id="txtQYbm" class="easyui-validatebox" type="text" name="name" style="border: solid 1px #E0ECF9;
                                                        font-size: 12px; width: 90px; text-align: center;" disabled="disabled" />
                                                    &nbsp; 射频卡编码 &nbsp;
                                                    <input id="txtQYcd" class="easyui-validatebox" type="text" name="name" style="border: solid 1px #E0ECF9;
                                                        font-size: 12px; width: 120px; text-align: center;" disabled="disabled" />
                                                    &nbsp; 区域名称 &nbsp;
                                                    <input id="txtQYname" class="easyui-validatebox" type="text" name="name" style="border: solid 1px #E0ECF9;
                                                        font-size: 12px; width: 90px; text-align: center;" disabled="disabled" /></p>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </li>
                            <li>
                                <div id="div2">
                                    <table id="gridSB">
                                    </table>
                                </div>
                            </li>
                        </ul>
                    </div>
                    <div id="divSB" class="right">
                        <ul>
                            <li>
                                <div>
                                    <table>
                                        <tr>
                                            <td>
                                                <p style="font-size: 12px;">
                                                    &nbsp;&nbsp; 设备编码 &nbsp;
                                                    <input id="txtSBbm" class="easyui-validatebox" type="text" name="name" style="border: solid 1px #E0ECF9;
                                                        font-size: 12px; width: 90px; text-align: center;" disabled="disabled" />
                                                    &nbsp; 父级编码 &nbsp;
                                                    <input id="txtSBfbm" class="easyui-validatebox" type="text" name="name" style="border: solid 1px #E0ECF9;
                                                        font-size: 12px; width: 90px; text-align: center;" disabled="disabled" />
                                                    &nbsp; 设备名称 &nbsp;
                                                    <input id="txtSBname" class="easyui-validatebox" type="text" name="name" style="border: solid 1px #E0ECF9;
                                                        font-size: 12px; width: 160px; text-align: center;" disabled="disabled" /></p>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </li>
                            <li>
                                <div id="div4">
                                    <table id="gridDJX">
                                    </table>
                                </div>
                            </li>
                        </ul>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <!-- 新建线路 -->
    <div id="dvLineOne">
        <table class="admintable" width="320px;">
            <tr>
                <th class="adminth" colspan="2">
                    线路设置信息
                </th>
            </tr>
            <tr>
                <td class="admincls1" align="center" style="width: 100px;">
                    线路名称
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;<input id="txtLineName" style="width: 120px;" type="text" />
                </td>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    线路类型
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;
                    <select id="txtLineType" style="width: 80px;">
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
                    &nbsp;&nbsp;<input id="txtLineGw" type="text" style="width: 120px;" readonly="true" />
                    <span id="show_div"><a id="a1" href="#" class="easyui-linkbutton">选择</a></span>
                </td>
            </tr>
        </table>
    </div>
    <!-- 岗位DIV -->
    <div id="divGW" style="height: 400px; width: 300px;">
        <%--class="white_content"--%>
        <table class="admintable" style="height: 399px;">
            <tr>
                <td>
                    <div id="treeDiv1">
                        <ul id="treeGW" class="ztree">
                        </ul>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <!-- 区域选择 -->
    <div id="divAllQY" style="height: 450px; width: 450px;">
        <table id="gridArea" style="width: 430px;">
        </table>
    </div>
    <!-- 设备Tree -->
    <div id="divSBTree" style="height: 400px; width: 300px;">
        <%--class="white_content"--%>
        <table class="admintable" style="height: 399px;">
            <tr>
                <td>
                    <div id="treeDiv2">
                        <ul id="treeDev" class="ztree">
                        </ul>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <!-- 点检项选择 -->
    <div id="divItem" style="height: 450px; width: 600px;">
        <table id="gridItem" style="width: 570px;">
        </table>
    </div>
    <div id="dvItemInfo" style="height: 350px; width: 740px;" data-options="tools:'#p-tools'"
        style="padding: 1px;">
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
    </form>
</body>
</html>
