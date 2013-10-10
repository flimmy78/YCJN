<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DevNew.aspx.cs" Inherits="DJXT.Device.DevNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>设备维护</title>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../css/zTreeStyle/djxt.css" rel="stylesheet" type="text/css" />
    <link href="../css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />

    <script src="../jQueryEasyUI/jquery-1.6.2.js" type="text/javascript"></script>

    <script src="../js/jquery.ztree.core-3.5.js" type="text/javascript"></script>

    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="../js/My97DatePicker/WdatePicker.js"></script>

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
		
		function onClick(event, treeId, treeNode, clickFlag) {
		      $("#flUp").val('');
		      $('#tt').tabs('select', "设备信息")
		      $('#NodeCheckedID').val(treeNode.id);
		      		      
		      $.post("DevNew.aspx", { param: 'clickTree',id:treeNode.id}, function (data) {
		            if(Number(data.count)==1){
		                $('#txtSBName').val(data.sbName);
	                    $('#txtDeviceId').val(treeNode.id);
	                    $('#txtDeviceName').val(escape(data.sbName));
	                }
		      }, 'json');		      
		     
		      $('#MyItem').hide();
		      $('#MyItems').hide();		      
		      $('#itemHis').hide();
		      
		      ReadInfo(treeNode.id);
		};	
		
        $(document).ready(function(){
            $("#menu").css("height",pageHeight()-100);
            $("#menu").css("width",pageWidth()-100);
            $("#dv_tree").css("height",pageHeight()-118);
            $("#tree").css("height",pageHeight()-120);
            
            $("#gridStatus").css("width",pageWidth()-395);
            $("#gridStatus").css("height",pageHeight()-140);
            
            $('#MyItem').hide();
            $('#MyItems').hide();
            $('#itemHis').hide();
            
            $('#tt').tabs('select', "设备信息")
            ReadInfo("");
            Tree(); //生成TREE
            
              $('#tt').tabs({
                onSelect: function (title) {                    
                    if(title=='状态维护')
                    {     
                        GridSta($("#NodeCheckedID").val());
                    }
                    else if(title=='点检项维护')
                    {      
                        GridIt($("#NodeCheckedID").val());        
                    } 
                }
            });
            
            //上传附件
            $("#saveFile").click(function() {
                if($("#NodeCheckedID").val()=="" || $("#txtSBName").val()==null || $("#NodeCheckedID").val()==null ||escape($("#txtSBName").val())==null||escape($("#txtSBName").val())=="" || escape($("#NodeCheckedID").val())==""  ||escape($("#flUp").val())==null||escape($("#flUp").val())==""){
                    $.messager.alert('上传附件','请选择需要上传的设备和附件!','error');
                }else{
                    $.post("DevNew.aspx", { param: 'AddFile',SBID:$("#NodeCheckedID").val(),SBName:escape($("#txtSBName").val()),flPath:escape($("#flUp").val())}, function (data) {
                        
                        $.messager.alert('附件上传',data.msg,'info');  
                        
                        if(Number(data.count)==1)
                        {                          
                            $("#flUp").val('');
                        }                     
                    }, 'json');                   
                }             
            });  
            
            //重置上传附件部分
            $("#btnQuxiao").click(function() {           
                $("#flUp").val('');                
            });  
		});
        
        function GridSta(sbID){
            $('#gridStatus').datagrid({			   
			    nowrap: true,
			    autoRowHeight: false,
			    striped: true,
			    align:'center',
			    collapsible:true,
			    url:'DevNew.aspx',
			    sortName: 'id_key1',
			    sortOrder: 'asc',
			    remoteSort: false,
			    queryParams:{param:'query',SBID:sbID},
			    idField:'id_key1',			    
			    columns:[[				    
				    {field:'t_deviceid',title:'设备编码',width:100,align:'center'},
                    {field:'t_devicedesc',title:'设备名称',width:120,align:'center'},
                    {field:'t_time',title:'时间',width:120,align:'center'},
                    {field:'t_statusdesc',title:'设备状态',width:140,align:'center'},
                    {field:'i_status',title:'状态编码',width:120,hidden:true},                    
                    {field:'id_key1',title:'状态编码',width:120,hidden:true}
                ]],
			    pagination:true,
			    rownumbers:true,
			    toolbar:[{
			        id:'btnadd', 
			        text:'添加',
			        iconCls:'icon-add',			       
			        handler:function(){
			            $('#MyItem').show();
                        AddShow();                
			        }
		        },
		        {
			        id:'btnadd', 
			        text:'编辑',
			        iconCls:'icon-edit',
			        handler:function(){
                        $('#MyItem').show();
                        EditShow(); 
			        }
		        }]			
		    });
		}
		
		//设备状态新增
		function AddShow(){		    
		    $("#MyItem").attr('title','新增设备状态');
		    $('#DJXSTARTTIME1').val('');
		    $('#DJXPERIODTYPE').val('');
		    $('#MyItem').show();			    	    
			$('#MyItem').dialog({
				buttons:[{
					text:'保存',
					iconCls:'icon-ok',
					handler:function(){
					    Add($('#DJXSTARTTIME1').val(),$('#DJXPERIODTYPE').val(),$('#NodeCheckedID').val());
					}
				},{
					text:'取消',
					iconCls:'icon-reload',
					handler:function(){
						$('#MyItem').dialog('close');
					}
				}]
			});
		}
		
		//编辑状态新增
		function EditShow(){		    
		    $("#MyItem").attr('title','修改设备状态');
		    $('#HidStaidKey').val('');
		    
		    var row = $('#gridStatus').datagrid('getSelected');  
            if (row){
                $('#HidStaidKey').val(row.id_key1);
                $('#DJXPERIODTYPE').val(row.i_status);
                $('#DJXSTARTTIME1').val(row.t_time);
                
                $('#MyItem').show();	        
                $('#MyItem').dialog({
				buttons:[{
					text:'保存',
					iconCls:'icon-ok',
					handler:function(){
					    Edit($('#DJXSTARTTIME1').val(),$('#DJXPERIODTYPE').val(),$('#HidStaidKey').val());
					}
				},{
					text:'取消',
					iconCls:'icon-reload',
					handler:function(){
						$('#MyItem').dialog('close');
					}
				}]
			});
                
            }else{
                alert("请选择要操作的数据!");
            }
		}
		
		//新增设备状态
		function Add(time,type,nodeID){
            $.post("DevNew.aspx", { param: 'SaveStatus',SBnodeKey:nodeID,StartTime:time,type:type}, function (data) {   
                $('#gridStatus').datagrid('reload');                              
                $.messager.alert('新增设备状态',data.msg,'info');                
            }, 'json');       
		}
		
		//修改设备状态
		function Edit(time,type,idKey){
            $.post("DevNew.aspx", { param: 'EditStatus',IDKEY:idKey,StartTime:time,type:type}, function (data) {   
                $('#gridStatus').datagrid('reload');                              
                $.messager.alert('修改设备状态',data.info,'info');                
            }, 'json');       
		}
		
		function GridIt(sbID){
            $('#gridItem').datagrid({			   
			    nowrap: true,
			    autoRowHeight:false,
			    striped: true,
			    align:'center',
			    collapsible:true,
			    url:'DevNew.aspx',
			    sortName: 'id_key',
			    sortOrder: 'asc',
			    remoteSort: false,
			    queryParams:{param:'queryItem',SBID:sbID},
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
			        text:'添加',
			        iconCls:'icon-add',			       
			        handler:function(){			           
                        AddItemShow();                
			        }
		        },
		        {
			        id:'btnadd', 
			        text:'编辑',
			        iconCls:'icon-edit',
			        handler:function(){                        
                        EditItemShow(); 
			        }
		        }]			
		    });
		}
		
		//查询点检项历史
		function ShowHisItems(val)
		{
		    $("#itemHis").attr('title','历史查询');		    
		    $('#itemHis').show();
		    GridItHis(val)		    
		    $('#itemHis').dialog({
				buttons:[{
					text:'关闭',
					iconCls:'icon-reload',
					handler:function(){
						$('#itemHis').dialog('close');
					}
				}]
			});	
		}	
		
		//点检项新增
		function AddItemShow(){		    
		    $("#MyItems").attr('title','新增点检项');
		    $("#DJXBW").val('');
		    $("#DJXDESC").val('');
		    $("#DJXCONTENT").val('');
		    $("#DJXUnit").val('');
		    $("#DJXUpper").val('');
		    $("#DJXLower").val('');
		    $("#DJXSTARTTIME").val('');
		    
		    $('#MyItems').show();			    	    
			$('#MyItems').dialog({
				buttons:[{
					text:'保存',
					iconCls:'icon-ok',
					handler:function(){
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
                            $.post("DevNew.aspx", { param: 'SaveItem',SBnodeKey:$('#NodeCheckedID').val(),itemBw:escape($("#DJXBW").val()),itemDesc:escape($("#DJXDESC").val())
                            ,itemContent:escape($("#DJXCONTENT").val()) ,itemObserve:escape($("#DJXObserve").val()),itemUnit:escape($("#DJXUnit").val()),itemType:$("#DJXType").val()
                            ,itemStatus:$("#DJXSelectStatus").val() ,itemStatusQJ:$("#DJXT_SATAUS").val() ,itemUpper:$("#DJXUpper").val() ,itemLower:$("#DJXLower").val(),itemSpectrum:$("#DJXSpectrum").val()
                            ,itemStartTime:escape($("#DJXSTARTTIME").val()) ,itemPerValue:$("#DJXPERIODVALUE").val(),itemPerType:$("#Select1").val()
                            }, function (data) { 
                                $.messager.alert('添加点检项',data.msg,'info');                                                 
                                $('#gridItem').datagrid('reload'); 
                            }, 'json');
                        }
					}
				},{
					text:'取消',
					iconCls:'icon-reload',
					handler:function(){
						$('#MyItems').dialog('close');
					}
				}]
			});
		}
		
		//编辑点检项
		function EditItemShow(){		    
		    $("#MyItems").attr('title','修改点检项');
		   	$('#idkeyItems').val('');	    
		    var row = $('#gridItem').datagrid('getSelected');  
            if (row){
                $('#idkeyItems').val(row.id_key);
                $('#DJXBW').val(row.t_itemposition);
                $('#DJXDESC').val(row.t_itemdesc);
                $('#DJXCONTENT').val(row.t_content);
                $('#DJXObserve').val(row.t_observe);
                $('#DJXUnit').val(row.t_unit);
                $('#DJXType').val(row.t_type);
                $('#DJXSelectStatus').val(row.i_status);
                $('#DJXT_SATAUS').val(row.t_status);
                $('#DJXUpper').val(row.f_upper);
                $('#DJXLower').val(row.f_lower);
                $('#DJXSpectrum').val(row.i_spectrum);
                $('#DJXSTARTTIME').val(row.t_starttime);
                $('#DJXPERIODVALUE').val(row.t_periodvalue);
                $('#Select1').val(row.t_periodtype);  
                
                $('#MyItems').show();	        
                $('#MyItems').dialog({
				    buttons:[{
					    text:'保存',
					    iconCls:'icon-ok',
					    handler:function(){
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
                                
                                $.post("DevNew.aspx", { param: 'EditItem',idkey:$("#idkeyItems").val(),itemBw:escape($("#DJXBW").val()),itemDesc:escape($("#DJXDESC").val())
                                ,itemContent:escape($("#DJXCONTENT").val()) ,itemObserve:escape($("#DJXObserve").val()),itemUnit:escape($("#DJXUnit").val()),itemType:$("#DJXType").val()
                                ,itemStatus:$("#DJXSelectStatus").val() ,itemStatusQJ:$("#DJXT_SATAUS").val() ,itemUpper:$("#DJXUpper").val() ,itemLower:$("#DJXLower").val(),itemSpectrum:$("#DJXSpectrum").val()
                                ,itemStartTime:escape($("#DJXSTARTTIME").val()) ,itemPerValue:$("#DJXPERIODVALUE").val(),itemPerType:$("#Select1").val()
                                }, function (data) {                                    
                                    $.messager.alert('修改点检项',data.msg,'info');                                                       
                                    $('#gridItem').datagrid('reload'); 
                                }, 'json');
                                
                            }
					    }
				    },{
					    text:'取消',
					    iconCls:'icon-reload',
					    handler:function(){
						    $('#MyItems').dialog('close');
					    }
				    }]
			    });                
            }else{
                alert("请选择要操作的数据!");
            }
		}
		
		function GridItHis(idKey){
            $('#gridItemHis').datagrid({			   
			    nowrap: true,
			    autoRowHeight: false,
			    striped: true,
			    align:'center',
			    collapsible:true,
			    url:'DevNew.aspx',
			    sortName: 'id_key',
			    sortOrder: 'asc',
			    remoteSort: false,
			    queryParams:{param:'queryHis',idKey:idKey},
			    idField:'id_key',
                columns: [[
                    { title: '点检项描述', field: 't_itemdesc', width: 80, sortable: true },
                    { title: '点检项部位', field: 't_itemposition', width: 80, sortable: true },
                    { title: '检查内容', field: 't_content',width: 80 },   
                    { title: '类型', field: 't_type',formatter:function(value,rec,index){return value==0?'点检':'巡检'},width: 50 },   
                    { title: '单位', field: 't_unit', width: 60 }, 
                    { title: '测量上限', field: 'f_upper', width: 65 },
                    { title: '测量下限', field: 'f_lower', width: 65 },                   
                    { field:'i_spectrum',title:'是否频谱',formatter:function(value,rec,index){return value==0?'否':'是'},width:70},
                    { field:'t_periodvalue',title:'周期数值',width:60},
                    { field:'t_periodtype',title:'周期类型',width:60},
                    { field:'t_starttime',title:'开始时间',width:80}                    
                ]],     
			    pagination:true,
			    rownumbers:true		   		
		    });
		}
		
        //生成Tree
        function Tree(){
            $.post("DevNew.aspx", { param: ''}, function (data) {
                if(Number(data.count)==1){
                    var  nodeEval = eval(data.menu);
                    $.fn.zTree.init($("#tree"), setting, nodeEval);                                                            
                }               
            }, 'json'); 
		}
		
		//读取设备信息
		function ReadInfo(id){
            $.post("DevNew.aspx", { param: 'load',SBID:id}, function (data) {
                if(Number(data.count)==0){
                     document.getElementById("lblInfoName").innerText=data.sbName;
                     document.getElementById("lblInfoFile").innerText=data.file;
                     if(id!="")
                        document.getElementById("lblInfoStatus").innerText=id; //设备编码
                     else
                        document.getElementById("lblInfoStatus").innerText="1000"; //设备编码
                        
                     document.getElementById("lblDJZT").innerText=data.fjbm;//父级编码                     
                     document.getElementById("lblWHGC").innerText=data.whgc;
                     document.getElementById("lblJHGC").innerText=data.jhgc;
                     document.getElementById("lblCBZX").innerText=data.cbzx;
                     //document.getElementById("lblSCBZ").innerText=data.scbs;
                     //document.getElementById("lblSBYY").innerText=data.sbyy;
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
        <table id="__01" width="100%" height="95%" border="0" cellpadding="0" cellspacing="0"
            bgcolor="#2a88bb">
            <tr>
                <td colspan="3" background="../img/table-head.jpg" height="30px" valign="middle"
                    class="style6">
                    &nbsp;设备台账
                </td>
            </tr>
            <tr>
                <td background="../img/table-head-2.jpg" height="25px" valign="middle" class="style5">
                    &nbsp;设备列表
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
                            <div id="tt" class="easyui-tabs" data-options="tools:'#tab-tools'" style="float: left;
                                width: 875px; height: 550px;">
                                <div id="divInfo" title="设备信息" data-options="tools:'#p-tools'" style="padding: 2px;">
                                    <%--  <tr>
                                            <td class="admincls1" align="center">
                                                是否存在附件
                                            </td>
                                            <td class="admincls1">
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblInfoFile" runat="server"></asp:Label>
                                            </td>
                                        </tr> --%>
                                    <table width="100%" class="admintable" border="1">
                                        <tr>
                                            <th class="adminth" colspan="5" align="center">
                                                &nbsp;<asp:Label ID="lblInfoName" runat="server"></asp:Label>
                                                &nbsp;
                                            </th>
                                        </tr>
                                        <tr>
                                            <td class="admincls1" style="width: 20%" align="center">
                                                &nbsp;&nbsp;设备编码:
                                            </td>
                                            <td class="admincls1" style="width: 30%" align="center">
                                                &nbsp;<asp:Label ID="lblInfoStatus" runat="server"></asp:Label>
                                            </td>
                                            <td class="admincls1" style="width: 20%" align="center">
                                                &nbsp;&nbsp;父级编码:
                                            </td>
                                            <td class="admincls1" style="width: 30%" align="center">
                                                &nbsp;<label id="lblDJZT" runat="server">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="admincls0" align="center">
                                                &nbsp;&nbsp;维护工厂:
                                            </td>
                                            <td class="admincls0" align="center">
                                                &nbsp;<label id="lblWHGC" runat="server">
                                                </label>
                                            </td>
                                            <td class="admincls0" align="center">
                                                &nbsp;&nbsp;计划工厂:
                                            </td>
                                            <td class="admincls0" align="center">
                                                &nbsp;<label id="lblJHGC" runat="server">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="admincls1" align="center">
                                                &nbsp;&nbsp;成本中心:
                                            </td>
                                            <td class="admincls1" align="center">
                                                &nbsp;<label id="lblCBZX" runat="server">
                                                </label>
                                            </td>
                                            <td class="admincls1" align="center">
                                                &nbsp;&nbsp;是否附件:
                                            </td>
                                            <td class="admincls1" align="center">
                                                &nbsp;<label id="lblInfoFile" runat="server">
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
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
                                                    <asp:Button ID="btnXia" Text="下载" CssClass="formbutton" runat="server" OnClick="btnXia_Click" />
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
                                        <table id="gridStatus">
                                        </table>
                                    </div>
                                </div>
                                <!-- 点检项维护 -->
                                <div id="dv_Device" title="点检项维护" data-options="tools:'#p-tools'" style="padding: 2px;">
                                    <div id="Div1">
                                        <table id="gridItem">
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </ul>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="MyItem" data-options="iconCls:'icon-save'" style="padding: 0px; width: 280px;
        height: 159px;">
        <table class="admintable" width="100%" border="1">
            <tr>
                <td class="admincls0">
                    &nbsp;&nbsp;时间:
                </td>
                <input type="hidden" id="HidStaidKey" />
                <td class="admincls0">
                    <input type="text" id="DJXSTARTTIME1" style="text-align: center;" runat="server"
                        readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                </td>
            </tr>
            <tr>
                <td class="admincls1">
                    &nbsp;&nbsp;设备状态:
                </td>
                <td class="admincls1">
                    <select id="DJXPERIODTYPE" style="width: 60px;">
                        <option value="1">在用</option>
                        <option value="2">停用</option>
                        <option value="3">退役</option>
                        <option value="4">报废</option>
                        <option value="5">删除</option>
                    </select>
                </td>
            </tr>
        </table>
    </div>
    <div id="MyItems" data-options="iconCls:'icon-save'" style="padding: 0px; width: 750px;
        height: 350px;">
        <table class="admintable"  width="100%" border="1">
            <tr>
                <td class="admincls1">
                    &nbsp;&nbsp;点检项部位:
                </td>
                <td class="admincls1">
                    <input type="text" id="DJXBW" />
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;点检项描述:<input type="hidden" id="idkeyItems" />
                </td>
                <td class="admincls1">
                    <input type="text" id="DJXDESC" />
                </td>
                <td colspan="2" class="admincls1">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="1" class="admincls0">
                    &nbsp;&nbsp;检查内容:
                </td>
                <td colspan="5"  class="admincls0">
                    <input style="width: 400px; height: 40px;" type="text" id="DJXCONTENT" />
                </td>
            </tr>
            <tr>
                <td  class="admincls1">
                    &nbsp;&nbsp;数据名称:
                </td>
                <td class="admincls1">
                    <input type="text" id="DJXObserve" />
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;单位:
                </td>
                <td class="admincls1">
                    <input type="text" id="DJXUnit" />
                </td>
                <td colspan="2" class="admincls1">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="admincls0">
                    &nbsp;&nbsp;点检类型:
                </td>
                <td class="admincls0">
                    <select id="DJXType" style="width: 60px;">
                        <option id="Option3" value="0">点检</option>
                        <option id="Option4" value="1">巡检</option>
                    </select>
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;设备状态:
                </td>
                <td class="admincls0">
                    <select id="DJXSelectStatus" style="width: 60px;">
                        <option id="Option5" value="0">在用</option>
                        <option id="Option6" value="1">停用</option>
                        <option id="Option7" value="2">退役</option>
                        <option id="Option8" value="3">报废</option>
                        <option id="Option9" value="4">删除</option>
                    </select>
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;点检类型:
                </td>
                <td class="admincls0">
                    <select id="DJXT_SATAUS" style="width: 60px;">
                        <option id="Option18" value="0">启机</option>
                        <option id="Option19" value="1">停机</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="admincls1">
                    &nbsp;&nbsp;测量上限:
                </td>
                <td class="admincls1">
                    <input type="text" id="DJXUpper" />
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;测量下限:
                </td>
                <td class="admincls1">
                    <input type="text" id="DJXLower" />
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;是否频谱:
                </td>
                <td class="admincls1">
                    <select id="DJXSpectrum" style="width: 60px;">
                        <option id="Option10" value="0">否</option>
                        <option id="Option11" value="1">是</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="admincls0">
                    &nbsp;&nbsp;开始时间:
                </td>
                <td class="admincls0">
                    <input type="text" id="DJXSTARTTIME" style="text-align: center;" runat="server" readonly="readonly"
                        onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;周期数值:
                </td>
                <td class="admincls0">
                    <input type="text" id="DJXPERIODVALUE" />
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;周期类型:
                </td>
                <td class="admincls0">
                    <select id="Select1" style="width: 60px;">
                        <option id="Option14" value="1">日</option>
                        <option id="Option15" value="2">周</option>
                        <option id="Option16" value="3">月</option>
                        <option id="Option17" value="4">年</option>
                    </select>
                </td>
            </tr>
        </table>
    </div>
    <div id="itemHis" data-options="iconCls:'icon-save'" style="padding: 0px; width: 780px;
        height: 500px;">
        <table id="gridItemHis">
        </table>
    </div>
    </form>
</body>
</html>
