<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagePlan.aspx.cs" Inherits="DJXT.ParentMember.managePlan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>点检任务查询</title>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/css/djxt.css" rel="stylesheet" type="text/css" />
    <link href="../js/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" type="text/css" />

    <script src="../jQueryEasyUI/jquery-1.6.2.js" type="text/javascript"></script>

    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>

    <script src="../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>

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
		$(function(){
		
            getStartDate();
            getEndDate();
            load();
            
            $("#test").css("width",pageWidth());
            $("#test").css("height",pageHeight()-400);
            
            $("#Routes").change(function() {  
                $.post("ManagePlan.aspx", { param: 'Route',routeID:$("#Routes").val()}, function (data) {
                    var list = data.AreaList;  
                    $("#Areas").empty();
                    if(list!=null){
                        $("#Areas").append("<option value='0'>全    部</option>");
                        for(var i=0;i<list.length;i++){
                            $("#Areas").append("<option value='"+list[i].T_AREAID+"'>"+list[i].T_AREANAME+"</option>");
                        }
                    }else{
                        $("#Areas").append("<option value='0'>没有区域数据</option>");
                    }
                    list = data.DeviceList;  
                    $("#Devices").empty();
                    if(list!=null){
                        $("#Devices").append("<option value='0'>全    部</option>");
                        for(var i=0;i<list.length;i++){
                            $("#Devices").append("<option value='"+list[i].T_DEVICEID+"'>"+list[i].T_DEVICEDESC+"</option>");
                        } 
                    }else{
                        $("#Devices").append("<option value='0'>没有设备数据</option>");
                    }            
                },'json');    
            }); 
            
            $("#Areas").change(function() {  
                $.post("ManagePlan.aspx", { param: 'Area',areaID:$("#Areas").val()}, function (data) {
                    var list = data.DeviceList;  
                    $("#Devices").empty();
                    if(list!=null){
                        $("#Devices").append("<option value='0'>全    部</option>");
                        for(var i=0;i<list.length;i++){
                            $("#Devices").append("<option value='"+list[i].T_DEVICEID+"'>"+list[i].T_DEVICEDESC+"</option>");
                        }      
                    }else{
                        $("#Devices").append("<option value='0'>没有设备数据</option>");
                    }       
                },'json');    
            });            
            
            $("#Check").click(function() {    
                seachPlan();
            }); 
			$("#dv_Up").hide();
		});
		function UpValues(value){
		    $('#test').datagrid('clearSelections');
		    alert(value);
		}
		function load(){
		    $('#dv_Edit').hide();
            $.post("ManagePlan.aspx", { param: ''}, function (data) {
                var list = data.RouteList;  
                $("#Routes").empty();
                $("#Routes").append("<option value='0'>全    部</option>");
                if(list!=null)
                {
                    for(var i=0;i<list.length;i++){
                        $("#Routes").append("<option value='"+list[i].T_ROUTEID+"'>"+list[i].T_ROUTENAME+"</option>");
                    }
                }
                
                list = data.AreaList;
                $("#Areas").empty(); 
                if(list!=null){                    
                    $("#Areas").append("<option value='0'>全    部</option>");
                    for(var i=0;i<list.length;i++){
                        $("#Areas").append("<option value='"+list[i].T_AREAID+"'>"+list[i].T_AREANAME+"</option>");
                    }
                }else{
                     $("#Areas").append("<option value='0'>没有区域数据</option>");
                }
                
                list = data.DeviceList; 
                $("#Devices").empty(); 
                if(list!=null){                   
                    $("#Devices").append("<option value='0'>全    部</option>");
                    for(var i=0;i<list.length;i++){
                        $("#Devices").append("<option value='"+list[i].T_DEVICEID+"'>"+list[i].T_DEVICEDESC+"</option>");
                    }
                }else{
                    $("#Devices").append("<option value='0'>没有设备数据</option>");
                }
            },'json');
            
            $('#test').datagrid({
				title:'任务查询',
				iconCls:'icon-search',
//				width:1400,
				height:520,
				nowrap: true,
				autoRowHeight: false,
				striped: true,
				align:'center',
				collapsible:true,
				url:'ManagePlan.aspx',
				sortName: 'ID',
				sortOrder: 'asc',
				remoteSort: false,
				queryParams:{param:'queryList'},
				idField:'ID',
				frozenColumns:[[
	                {field:'ck',checkbox:true}
				]],
				columns:[[
				    {field:'status',title:'任务状态',width:150,align:'center'},
					{field:'routeName',title:'线路',width:120,align:'center'},
					{field:'areaName',title:'区域',width:150,align:'center'},
					{field:'deviceName',title:'设备',width:150,align:'center'},
					{field:'T_ITEMPOSITION',title:'检测部位',width:150,align:'center'},
					{field:'T_ITEMDESC',title:'描述',width:150,align:'center'},
					{field:'sTime',title:'开始时间',width:150,align:'center'},
					{field:'eTime',title:'结束时间',width:150,align:'center'},
					{field:'cTime',title:'检测时间',width:150,align:'center'},
					{field:'uTime',title:'上传时间',width:150,align:'center'},					
					{field:'value',title:'检测结果',width:150,align:'center'},
					{field:'RouteID',title:'线路编号',width:150,align:'center',hidden:true},
					{field:'AreaID',title:'区域编号',width:150,align:'center',hidden:true},
					{field:'DeviceID',title:'设备编号',width:150,align:'center',hidden:true},
					{field:'ItemID',title:'点检项编号',width:150,align:'center',hidden:true},
					{field: 'optDel', title: '上报', width: 150,align:'center',  
						formatter:function(value,rec,index){  
						    var up=''; 
//							up='<a href="javascript:void(0);" mce_href="javascript:void(0);" class="easyui-linkbutton" data-options="iconCls:icon-search" onclick="UpValues(\''+ rec.ID + '\')" data-options="iconCls:icon-search">上  报</a>';	
			                up='<a href="javascript:void(0);" mce_href="javascript:void(0);" class="easyui-linkbutton" data-options="iconCls:icon-search" onclick="UpResult()" data-options="iconCls:icon-search">上  报</a>';	
						    return up;  						    
						}
					}					
				]],
				pagination:true,
				rownumbers:true,
					toolbar:[{
					id:'btnadd', 
					text:'上报选中任务',
					iconCls:'icon-print',
					handler:function(){
					$('#btnsave').linkbutton('enable');
                    var rows = $('#test').datagrid('getSelections');
                    var id="";
                    $.each(rows,function(i,n){
		                    id += n.ID+',';
	                });
					}
				}],
		        onDblClickRow: function(rowIndex, rowData) {
                    $("#dv_Edit").attr('title','编辑点检信息');
		            $('#dv_Edit').show();
		            setEditShow(rowData.routeName,rowData.areaName,rowData.deviceName,rowData.T_ITEMPOSITION,rowData.sTime,rowData.eTime,rowData.cTime,rowData.uTime,rowData.status,rowData.value,rowData.RouteID,rowData.AreaID,rowData.DeviceID,rowData.ItemID);
			        $('#dv_Edit').dialog({
				        buttons:[{
					        text:'保存',
					        iconCls:'icon-ok',
					        handler:function(){					            
                                EditResutl(rowData.RouteID,rowData.AreaID,rowData.DeviceID,rowData.ItemID,rowData.cTime,rowData.status,$("#hide_val").val());
					        }
				        },{
					        text:'取 消',
					        handler:function(){
						        $('#dv_Edit').dialog('close');
					        }
				        }]
			        });
                }				
			});
		}
		
		function setEditShow(route,area,device,item,stime,etime,ctime,utime,state,value,routeId,areaId,deviceId,itemId){
            $("#p_route").text(route);
            $("#p_area").text(area);
            $("#p_device").text(device);
            $("#p_item").text(item);
            $("#p_sTime").text(stime);
            $("#p_eTime").text(etime);
            $("#p_cTime").text(ctime);
            $("#p_uTime").text(utime);
            $("#p_state").text(state);
            $("#p_result").text(value);
             
            $("#hide_deviceId").hide();
            $("#hide_areaId").hide();
            $("#hide_routeId").hide();
            $("#hide_itemId").hide();
            $("#hide_routeId").text(deviceId);
            $("#hide_areaId").text(areaId);
            $("#hide_deviceId").text(routeId);
            $("#hide_itemId").text(itemId);          
            
            if(state=="已上报"){
                $("#p_result").hide();
                $("#hide_val").show();
                $("#hide_val").val(value);
                $("#hide_stat").hide();
            }else if(state=="异常"){
                 $("#p_state").hide();
                 $("hide_stat").show();
                 
                $("#hide_stat").empty();
                $("#hide_stat").append("<option value='1'>异常</option>");
                $("#hide_stat").append("<option value='2'>已上报</option>");
            }else{
                $('#hide_val').hide();
                $('#hide_stat').hide();
            }            
		}
		
		function EditResutl(routeID,areaID,deviceID,itemID,cTime,state,value){
		
		    if(state!=null){		        
		        if($.trim(state)=="已上报"){		       
		        
		            state="1"; 
		        }else if($.trim(state)=="异常"){
		            state="2";
		        }
		        value = $("#hide_val").val();
		    }else{
		        if($.trim(state)=="已上报"){
		            state="1";
		        }else if($.trim(state)=="异常"){
		            state="2";
		        }else if($.trim(state)=="报缺陷"){
		            state="3";
		        }else if($.trim(state)=="已解决"){
		            state="4";
		        }
		        value = $("#hide_val").text();		        
		    }
		    $.post("ManagePlan.aspx",{param :"Edit",routeID:routeID,areaID:areaID,deviceID:deviceID,itemID:itemID,time:escape(cTime),state:state,value:value},function(data){
		        $.messager.alert('编辑点检信息',data.info,'info');
		        seachPlan();
		    });		   
		}
		
		function seachPlan(){
            var query={param:'searchList',routeID:$('#Routes').val(),areaID:$('#Areas').val(),deviceID:$('#Devices').val(),sTime:$('#txtStime').val(),eTime:$('#txtEtime').val()}; //把查询条件拼接成JSON
            $("#test").datagrid('options').queryParams=query; //把查询条件赋值给datagrid内部变量
            $("#test").datagrid('reload'); //重新加载
		}
		
		function UpResult(){
            $("#dv_Up").attr('title','缺陷上报');
            $('#dv_Up').show();
            $('#dv_Up').dialog({
                buttons:[{
                    text:'上报',
                    iconCls:'icon-ok',
                    handler:function(){					            
//                        EditResutl(rowData.RouteID,rowData.AreaID,rowData.DeviceID,rowData.ItemID,rowData.status,rowData.value);
                    }
                },{
                    text:'取 消',
                    handler:function(){
                        $('#dv_Up').dialog('close');
                    }
                }]
            });
		}
		
	   function getStartDate(){
            var myDate = new Date();
            var date;
            
            if(myDate.getMonth()==0 && myDate.getDate()==1){
                date =Number(myDate.getYear()-1)+'-12-31';     
            }else if(Number(myDate.getDate())==1){
                var month= myDate.getMonth();
                if(month==1||month==3||month==5||month==7||month==8||month==10){               
                    date = myDate.getYear()+'-'+ Number(myDate.getMonth())+'-24';   
                }else if(month==4||month==6||month==9||month==11){
                    date = myDate.getYear()+'-'+ Number(myDate.getMonth())+'-23';    
                }else{
                    if(myDate.getYear%4==0){
                        date = myDate.getYear()+'-'+ Number(myDate.getMonth())+'-22';   
                    }else{
                        date = myDate.getYear()+'-'+ Number(myDate.getMonth())+'-21';   
                    }
                }
            }else{
                if(myDate.getDate()<=7){
                    if(month==1||month==3||month==5||month==7||month==8||month==10){    
                        date = myDate.getYear()+'-'+Number(myDate.getMonth())+'-'+(31+Number(myDate.getDate()-7));
                    }else if(month==4||month==6||month==9||month==11){
                        date = myDate.getYear()+'-'+Number(myDate.getMonth())+'-'+(30+Number(myDate.getDate()-7));
                    }else{
                        if(myDate.getYear%4==0){
                            date = myDate.getYear()+'-'+Number(myDate.getMonth())+'-'+(29+Number(myDate.getDate()-7));
                        }else{
                            date = myDate.getYear()+'-'+Number(myDate.getMonth())+'-'+(28+Number(myDate.getDate()-7)); 
                        }
                    }    
                }else{
                    date = myDate.getYear()+'-'+Number(myDate.getMonth()+1)+'-'+Number(myDate.getDate()-7);     
                }  
            }
            
            $("#txtStime").val(date);
        }
        function getEndDate(){
            var myDate = new Date();
            var date;
            
            if(myDate.getMonth()==0 && myDate.getDate()==1){
                date = myDate.getYear()+'-'+Number(myDate.getMonth()+1)+'-'+Number(myDate.getDate()-1);     
            }else if(Number(myDate.getDate())==1){
                var month= myDate.getMonth();
                if(month==1||month==3||month==5||month==7||month==8||month==10){               
                    date = myDate.getYear()+'-'+myDate.getMonth()+'-31';   
                }else if(month==4||month==6||month==9||month==11){
                    date = myDate.getYear()+'-'+myDate.getMonth()+'-30';  
                }else{
                    if(myDate.getYear%4==0){
                        date = myDate.getYear()+'-'+myDate.getMonth()+'-29';  
                    }else{
                        date = myDate.getYear()+'-'+myDate.getMonth()+'-28';  
                    }
                }
            }else{
                date = myDate.getYear()+'-'+Number(myDate.getMonth()+1)+'-'+Number(myDate.getDate()-1);     
            }  
            $("#txtEtime").val(date);
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
<body style="font-size: 12px;">
    <form id="form1" runat="server">
    <div id="menu">
        <table id="__01" width="100%" height="100%" border="0" cellpadding="0" cellspacing="0"
            bgcolor="#FFFFFF">
            <tr>
                <td background="../img/table-head.jpg" height="30px" valign="middle" class="style6">
                    &nbsp;&nbsp;任务完成情况查询
                </td>
            </tr>
            <tr>
                <td background="../img/table-head-2.jpg" height="25px" valign="middle" class="style5">
                    &nbsp;&nbsp;操作选项
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%; left: 200px;">
                        <%--                    <tr>
                            <td style="height: 30px; background-image: url(../img/plan.jpg);">
                            </td>
                        </tr>--%>
                        <tr>
                            <td align="left" valign="middle">
                                &nbsp; &nbsp
                                <label id="lblRoute" style="height: 20px;" runat="server" class="td_title">
                                    线 &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;路:</label>
                                &nbsp; &nbsp;
                                <select id="Routes" style="width: 130px; text-align: center;">
                                </select>
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                <label id="lblAreas" style="height: 30px;" runat="server" class="td_title">
                                    区&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;域:</label>
                                &nbsp; &nbsp;
                                <select id="Areas" style="width: 130px; text-align: center;">
                                </select>
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                <label id="lblDevice" style="height: 30px;" runat="server" class="td_title">
                                    设&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;备:</label>
                                &nbsp; &nbsp;
                                <select id="Devices" style="width: 130px; text-align: center;">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle">
                                &nbsp; &nbsp
                                <label id="lalStime" style="height: 20px;" runat="server" class="td_title">
                                    开始时间:</label>
                                &nbsp; &nbsp;
                                <input type="text" id="txtStime" style="text-align: left;" runat="server" readonly="readonly"
                                    onfocus="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd'})" class="Wdate" />
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                <label id="Label1" style="height: 30px;" runat="server" class="td_title">
                                    结束时间:</label>
                                &nbsp; &nbsp;
                                <input type="text" id="txtEtime" style="text-align: left;" runat="server" readonly="readonly"
                                    onfocus="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd'})" class="Wdate" />
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;
                                &nbsp; &nbsp; <a id="Check" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'">
                                    查 询</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" valign="middle">
                    <table id="test">
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="dv_Edit" style="padding: 5px; width: 700px; height: 360px;">
        <table class="admintable" width="100%">
            <tr>
                <th class="adminth" colspan="4">
                    编辑点检信息
                </th>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    线路名称
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="p_route"></span>
                </td>
                <td class="admincls0" align="center">
                    区域名称
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="p_area"></span>
                </td>
            </tr>
            <tr>
                <td class="admincls1" align="center">
                    设备名称
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="p_device"></span>
                </td>
                <td class="admincls1" align="center">
                    点检部位
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="p_item"></span>
                </td>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    开始时间
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="p_sTime"></span>
                </td>
                <td class="admincls0" align="center">
                    结束时间
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="p_eTime"></span>
                </td>
            </tr>
            <tr>
                <td class="admincls1" align="center">
                    检测时间
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="p_cTime"></span>
                </td>
                <td class="admincls1" align="center">
                    上传时间
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="p_uTime"></span>
                </td>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    任务状态
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="p_state"></span>
                    <select id="hide_stat" style="width: 130px; text-align: center;">
                    </select>
                </td>
                <td class="admincls0" align="center">
                    检测结果
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="p_result"></span> <span id="hide_routeId">
                    </span><span id="hide_itemId"></span><span id="hide_areaId"></span><span id="hide_deviceId">
                    </span>
                    <input id="hide_val" type="text" />
                </td>
            </tr>
        </table>
    </div>
    <div id="dv_Up" style="padding: 5px; width: 720px; height: 450px;">
        <table class="admintable" width="100%">
            <tr>
                <th class="adminth" colspan="4">
                    缺陷上报
                </th>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    点检单号
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="" />
                </td>
                <td class="admincls0" align="center">
                    缺陷内容
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="Text1" />
                </td>
            </tr>
            <tr>
                <td class="admincls1" align="center">
                    缺陷性质
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="Text2" />
                </td>
                <td class="admincls1" align="center">
                    机组
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="Text3" />
                </td>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    KKS编码
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="Text4" />
                </td>
                <td class="admincls0" align="center">
                    设备号
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="Text5" />
                </td>
            </tr>
            <tr>
                <td class="admincls1" align="center">
                    缺陷起始日期
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="Text6" />
                </td>
                <td class="admincls1" align="center">
                    缺陷起始时间
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="Text7" />
                </td>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    问题损坏代码
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="Text8" />
                </td>
                <td class="admincls0" align="center">
                    代码组
                </td>
                <td class="admincls0">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="Text9" />
                </td>
            </tr>
            <tr>
                <td class="admincls1" align="center">
                    计划员组
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="Text10" />
                </td>
                <td class="admincls1" align="center">
                    班组
                </td>
                <td class="admincls1">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="Text11" />
                </td>
            </tr>
            <tr>
                <td class="admincls0" align="center">
                    工厂
                </td>
                <td class="admincls0" colspan="3">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="Text14" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
