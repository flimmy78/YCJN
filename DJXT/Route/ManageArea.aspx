<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageArea.aspx.cs" Inherits="DJXT.Device.ManageArea" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>区域管理</title>
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
        $(document).ready(function(){
            $("#menu").css("height",pageHeight()-100);
            $("#menu").css("width",pageWidth()-100);           
          
            $('#MyItem').hide();
            $('#MyItems').hide();
            $('#itemHis').hide();
            
            GridAre();            
		});
        
        function GridAre(){
            $('#GridArea').datagrid({			   
			    nowrap: true,
			    autoRowHeight: false,
			    striped: true,
			    height:520,
			    align:'center',
			    collapsible:true,
			    url:'ManageArea.aspx',
			    sortName: 'id_key',
			    sortOrder: 'asc',
			    remoteSort: false,
			    queryParams:{param:'query'},
			    idField:'id_key',			    
			    columns:[[				    
				    {field:'t_areaid',title:'区域编码',width:100,align:'center'},
                    {field:'t_areacd',title:'射频卡编码',width:100,align:'center'},
                    {field:'t_areaname',title:'区域名称',width:100,align:'center'},
                    {field:'t_routeid',title:'',width:100,hidden:true},                    
                    {field:'id_key',title:'',width:100,hidden:true}
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
		        },
		        {
			        id:'btnadd', 
			        text:'删除',
			        iconCls:'icon-remove',
			        handler:function(){                       
	                    var rows = $('#GridArea').datagrid('getSelections');
                        var id="";
                        var name="";
                        $.each(rows,function(i,n){
	                        id +="'"+ n.id_key+"',";	                       
	                    });	                    
	                    id = id.substring(0,id.length-1);
	                    
                        $.messager.confirm('删除区域', '你确定要删除吗?', function(ok){
                        
                            if (ok){
	                            $.post("ManageArea.aspx", { param: 'DelArea',idKey:id}, function (data) {   
                                    $('#GridArea').datagrid('reload');                              
                                    $.messager.alert('删除区域',data.msg,'info');                                               
                                }, 'json');
	                        }else{
	                            $.messager.alert('删除区域','删除已取消!','info');
	                        } 
                        })                     
			        }
		        }]		        			
		    });
		}
		
		//设备状态新增
		function AddShow(){		    
		    $("#MyItem").attr('title','新增区域');
		    $('#areaName').val('');
		    $('#areaCd').val('');
		    $('#MyItem').show();			    	    
			$('#MyItem').dialog({
				buttons:[{
					text:'保存',
					iconCls:'icon-ok',
					handler:function(){
					    Add(escape($('#areaName').val()),$('#areaCd').val());
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
		    $("#MyItem").attr('title','编辑区域');
		    $('#HididKey').val('');
		    
		    var row = $('#GridArea').datagrid('getSelected');  
            if (row){
                $('#HididKey').val(row.id_key);
                $('#areaName').val(row.t_areaname);
                $('#areaCd').val(row.t_areacd);
                
                $('#MyItem').show();	        
                $('#MyItem').dialog({
				    buttons:[{
					    text:'保存',
					    iconCls:'icon-ok',
					    handler:function(){
					        Edit(escape($('#areaName').val()),$('#areaCd').val(),$('#HididKey').val());
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
		function Add(name,cd){
            $.post("ManageArea.aspx", { param: 'SaveArea',name:name,cd:cd}, function (data) {   
                $('#GridArea').datagrid('reload');                              
                $.messager.alert('新增区域',data.msg,'info'); 
                $('#MyItem').dialog('close');                 
            }, 'json');       
		}
		
		//修改设备状态
		function Edit(name,cd,idKey){
            $.post("ManageArea.aspx", { param: 'EditArea',IDKEY:idKey,name:name,cd:cd}, function (data) {   
                $('#GridArea').datagrid('reload');                              
                $.messager.alert('修改区域',data.info,'info');  
                $('#MyItem').dialog('close');              
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
        <table id="__01" width="100%" height="95%" border="0" cellpadding="0" cellspacing="0"
            bgcolor="#2a88bb">
            <tr>
                <td colspan="3" background="../img/table-head.jpg" height="30px" valign="middle"
                    class="style6">
                    &nbsp;区域信息
                </td>
            </tr>
            <tr>
                <td valign="top" style="background: #f2f5f7">
                    <div id="Area" data-options="iconCls:'icon-save'" style="padding: 0px; width: 100%;
                        height: 500px;">
                        <table id="GridArea" >
                        </table>
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
                    &nbsp;&nbsp;射频卡编码:<input type="hidden" id="HididKey" />
                </td>
                <td class="admincls0">
                    <input type="text" id="areaCd" style="text-align: center;" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="admincls1">
                    &nbsp;&nbsp;区域名称:
                </td>
                <td class="admincls1">
                    <input type="text" id="areaName" style="text-align: center;" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
