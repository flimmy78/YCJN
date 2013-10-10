<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DjTaskNew.aspx.cs" Inherits="DJXT.Task.DjTaskNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>巡检任务完成情况查询</title>
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
        .button
        {
            width: 56px; /*图片宽带*/
            background: url(../img/button.jpg) no-repeat left top; /*图片路径*/
            border: none; /*去掉边框*/
            height: 24px; /*图片高度*/
            color: White;
            vertical-align: middle;
            text-align: center;
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
            font-size: 12px;
            width: 500px;
            color: #599ce0;
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
            
            $("#btnSearch").click(function() {
                var query={param: 'query', sTime: $("#txtStime").val(),eTime:$("#txtEtime").val()}; //把查询条件拼接成JSON
                $("#grid").datagrid('options').queryParams=query; //把查询条件赋值给datagrid内部变量
                $("#grid").datagrid('reload'); //重新加载
            }); 
		});
		
		function load(){
            $.post("DjTaskNew.aspx", { param: ''}, function (data) {
                $("#dv_show").html(data.info);
            },'json');
            $('#grid').datagrid({
			    title:'各班组巡检完成情况分析',
			    iconCls:'icon-search',
			    nowrap: true,
			    autoRowHeight: false,
			    striped: true,
			    height:520,
			    align:'center',
			    collapsible:true,
			    url:'DjTaskNew.aspx',
			    sortName: 'id',
			    sortOrder: 'asc',
			    remoteSort: false,
			    queryParams:{param:''},
			    idField:'id',
//			    frozenColumns:[[
//                {field:'ck',checkbox:true}
//			    ]],
			    columns:[[				    		
				    {field:'id',title:'班值名称',width:120,align:'center'},
				    {field:'task',title:'任务数量',width:150,align:'center'},
				    {field:'Maketask',title:'完成数量',width:150,align:'center'},
				    {field:'value',title:'漏检数量',width:150,align:'center'},	
				    {field:'values',title:'任务完成率',width:150,align:'center'}		
			    ]],
			    pagination:true,
			    rownumbers:true				
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
    <div id="menu">
        <table id="__01" width="100%" height="92%" border="0" cellpadding="0" cellspacing="0"
            bgcolor="#FFFFFF">
            <tr>
                <td background="../img/table-head.jpg" height="30px" valign="middle" class="style6">
                    &nbsp;&nbsp;巡检率查询
                </td>
            </tr>
            <tr>
                <td background="../img/table-head-2.jpg" height="25px" valign="middle" class="style5">
                    &nbsp;&nbsp;操作选项
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 90%; left: 200px;">
                        <tr>
                            <td align="left" valign="middle">
                                &nbsp; &nbsp 开始时间: &nbsp; &nbsp;
                                <input type="text" id="txtStime" style="text-align: center;" runat="server" readonly="readonly"
                                    onfocus="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd'})" class="Wdate" />
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                <label id="Label1" style="height: 30px;" runat="server" class="td_title">
                                    结束时间:</label>
                                &nbsp; &nbsp;
                                <input type="text" id="txtEtime" style="text-align: center;" runat="server" readonly="readonly"
                                    onfocus="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd'})" class="Wdate" />
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <a id="btnSearch" href="#" class="easyui-linkbutton"
                                    data-options="iconCls:'icon-reload'">查 询</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" valign="middle" height="30px">
                    <table id="grid">
                    </table>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
