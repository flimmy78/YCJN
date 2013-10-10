<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Demo.aspx.cs" Inherits="DJXT.ParentMember.Demo" %>

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

    <script type="text/javascript">
		$(function(){
		
            getStartDate();
            getEndDate();
            load();

            $("#Routes").change(function() {  
                $.post("Demo.aspx", { param: 'Route',routeID:$("#Routes").val()}, function (data) {
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
                $.post("Demo.aspx", { param: 'Area',areaID:$("#Areas").val()}, function (data) {
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
                $('#test').datagrid({
				    title:'任务查询',
				    iconCls:'icon-search',
				    width:1000,
				    height:520,
				    nowrap: true,
				    autoRowHeight: false,
				    striped: true,
				    align:'center',
				    collapsible:true,
				    url:'Demo.aspx',
				    sortName: 'ID',
				    sortOrder: 'asc',
				    remoteSort: false,
				    queryParams:{param:'searchList',routeID:$('#Routes').val(),areaID:$('#Areas').val(),deviceID:$('#Devices').val(),sTime:$('#txtStime').val(),eTime:$('#txtEtime').val()},
				    idField:'ID',
				    frozenColumns:[[
	                    {field:'ck',checkbox:true}
				    ]],
				    columns:[[				    
					    {field:'routeName',title:'线路',width:120,align:'center'},
					    {field:'areaName',title:'区域',width:150,align:'center'},
					    {field:'deviceName',title:'设备',width:150,align:'center'},
					    {field:'T_ITEMPOSITION',title:'检测部位',width:150,align:'center'},
					    {field:'T_ITEMDESC',title:'描述',width:150,align:'center'},
					    {field:'type',title:'检测类型',width:150,align:'center'},
					    {field:'sTime',title:'开始时间',width:150,align:'center'},
					    {field:'eTime',title:'结束时间',width:150,align:'center'},
					    {field:'cTime',title:'检测时间',width:150,align:'center'},
					    {field:'uTime',title:'上传时间',width:150,align:'center'},
					    {field:'value',title:'检测结果',width:150,align:'center'},
					    {field:'state',title:'任务状态',width:150,align:'center'},
					    {field:'status',title:'上报状态',width:150,align:'center'},
					    {field: 'optDel', title: '上报', width: 150,align:'center',  
						    formatter:function(value,rec,index){  
						        var s=''; 
							    s='<a href="javascript:void(0);" mce_href="javascript:void(0);" class="easyui-linkbutton" data-options="iconCls:icon-search" onclick="UpValues(\''+ rec.ID + '\')" data-options="iconCls:icon-search">上  报</a>';				
						        return s;  						    
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
		                    id += n.id+',';
		                    alert(id);
	                    });
	                    
					    }
				    }]				
			    });
            }); 
			
		});
		function UpValues(value){
		    $('#test').datagrid('clearSelections');
		    alert(value);
		}
		function load(){
            $.post("Demo.aspx", { param: ''}, function (data) {
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
				width:1000,
				height:520,
				nowrap: true,
				autoRowHeight: false,
				striped: true,
				align:'center',
				collapsible:true,
				url:'Demo.aspx',
				sortName: 'ID',
				sortOrder: 'asc',
				remoteSort: false,
				queryParams:{param:'queryList'},
				idField:'ID',
				frozenColumns:[[
	                {field:'ck',checkbox:true}
				]],
				columns:[[
					{field:'routeName',title:'线路',width:120,align:'center'},
					{field:'areaName',title:'区域',width:150,align:'center'},
					{field:'deviceName',title:'设备',width:150,align:'center'},
					{field:'T_ITEMPOSITION',title:'检测部位',width:150,align:'center'},
					{field:'T_ITEMDESC',title:'描述',width:150,align:'center'},
					{field:'sTime',title:'开始时间',width:150,align:'center'},
					{field:'eTime',title:'结束时间',width:150,align:'center'},
					{field:'state',title:'任务状态',width:150,align:'center'},
					{field:'I_STATUSS',title:'上报状态',width:150,align:'center'},
					{field: 'optDel', title: '上报', width: 150,align:'center',  
						formatter:function(value,rec,index){  
						    var s=''; 
							s='<a href="javascript:void(0);" mce_href="javascript:void(0);" class="easyui-linkbutton" data-options="iconCls:icon-search" onclick="UpValues(\''+ rec.ID + '\')" data-options="iconCls:icon-search">上  报</a>';				
						    return s;  						    
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

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 90%; left: 200px;">
        <tr>
            <td style="height: 30px; background-image: url(../img/plan.jpg);">
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle">
                &nbsp; &nbsp
                <label id="lblRoute" style="height: 20px;" runat="server" class="td_title">
                    线 &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;路:</label>
                &nbsp; &nbsp;
                <select id="Routes" style="width: 130px;">
                </select>
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                <label id="lblAreas" style="height: 30px;" runat="server" class="td_title">
                    区&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;域:</label>
                &nbsp; &nbsp;
                <select id="Areas" style="width: 130px;">
                </select>
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                <label id="lblDevice" style="height: 30px;" runat="server" class="td_title">
                    设&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;备:</label>
                &nbsp; &nbsp;
                <select id="Devices" style="width: 130px;">
                </select>
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle">
                &nbsp; &nbsp
                <label id="lalStime" style="height: 20px;" runat="server" class="td_title">
                    开始时间:</label>
                &nbsp; &nbsp;
                <input type="text" id="txtStime" style="text-align: center;" runat="server" readonly="readonly"
                    onfocus="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd'})" class="Wdate" />
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                <label id="Label1" style="height: 30px;" runat="server" class="td_title">
                    结束时间:</label>
                &nbsp; &nbsp;
                <input type="text" id="txtEtime" style="text-align: center;" runat="server" readonly="readonly"
                    onfocus="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd'})" class="Wdate" />
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;
                &nbsp; &nbsp; <a id="Check" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'">
                    查 询</a>
            </td>
        </tr>
    </table>
    <table id="test">
    </table>
    </form>
</body>
</html>
