<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlanDown_MZ.aspx.cs" Inherits="DJXT.ParentMember.PlanDown_MZ" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>点检任务下载</title>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../css/zTreeStyle/djxt.css" rel="stylesheet" type="text/css" />
    <link href="../css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />

    <script src="../jQueryEasyUI/jquery-1.6.2.js" type="text/javascript"></script>

    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>

    <script src="../js/jquery.ztree.core-3.5.js" type="text/javascript"></script>

    <script src="../js/jquery.ztree.excheck-3.5.js" type="text/javascript"></script>

    <style type="text/css">
        .button2
        {
            width: 70px; /*图片宽带*/
            background: url(../img/button2.jpg) no-repeat left top; /*图片路径*/
            border: none; /*去掉边框*/
            height: 24px; /*图片高度*/
            color: White;
            vertical-align: middle;
            text-align: center;
        }
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
</head>
<body style="font-size: 12px;">
    <form id="form1" runat="server">
    <object id="XSTCommOcx" classid="clsid:C5C95668-167F-4064-8596-382C2966CB7E" style="display: none;">
    </object>

    <script language="javascript" type="text/javascript">
        <!--
		var id='';
		var setting = {
			view: {
				selectedMulti: false
			},
			check: {
				enable: true
			},
			data: {
				simpleData: {
					enable: true
				}
			},
			callback: {
				beforeCheck: beforeCheck,
				onCheck: onCheck,
				onClick: onClick
			}
		};
		
		function RouteGrid(id){
            $('#gridRoute').datagrid({
			    title:'线路信息',
			    iconCls:'icon-search',
			    nowrap: true,
			    autoRowHeight: false,
			    striped: true,
			    height:520,
			    align:'center',
			    collapsible:true,
			    url:'PlanDown_MZ.aspx',
			    sortName: 'ID',
			    sortOrder: 'asc',
			    remoteSort: false,
			    queryParams:{param:'route',id:id},
			    idField:'ID',
			    columns:[[				    
				    {field:'RouteID',title:'线路编码',width:150,align:'center'},		
				    {field:'RouteName',title:'线路名称',width:120,align:'center'},
				    {field:'Rtype',title:'线路类型',width:150,align:'center'}
			    ]],
			    pagination:true,
			    rownumbers:true			
		    });
		}
		
		function RouteList(id){
            var query={param:'route',id:id}; //把查询条件拼接成JSON
            $("#gridRoute").datagrid('options').queryParams=query; //把查询条件赋值给datagrid内部变量
            $("#gridRoute").datagrid('reload'); //重新加载
		}
		
		function AreaGrid(id){
            $('#gridArea').datagrid({
			    title:'区域信息',
			    iconCls:'icon-search',
			    nowrap: true,
			    autoRowHeight: false,
			    striped: true,
			    align:'center',
			    collapsible:true,
			    url:'PlanDown_MZ.aspx',
			    sortName: 'ID',
			    sortOrder: 'asc',
			    remoteSort: false,
			    queryParams:{param:'area',id:id},
			    idField:'ID',
			    columns:[[	
			        {field:'AreaCD',title:'射频卡编码',width:150,align:'center'},			    
				    {field:'AreaID',title:'区域编码',width:150,align:'center'},		
				    {field:'AreaName',title:'区域名称',width:120,align:'center'}				    
			    ]],
			    pagination:true,
			    rownumbers:true			
		    });
		}
		
		function AreaList(id){
            var query={param:'area',id:id}; //把查询条件拼接成JSON
            $("#gridArea").datagrid('options').queryParams=query; //把查询条件赋值给datagrid内部变量
            $("#gridArea").datagrid('reload'); //重新加载
		}
		
		function DeviceGrid(id){
            $('#gridDevice').datagrid({
			    title:'设备信息',
			    iconCls:'icon-search',
			    nowrap: true,
			    autoRowHeight: false,
			    striped: true,
			    align:'center',
			    collapsible:true,
			    url:'PlanDown_MZ.aspx',
			    sortName: 'ID',
			    sortOrder: 'asc',
			    remoteSort: false,
			    queryParams:{param:'device',id:id},
			    idField:'ID',
			    columns:[[			    
				    {field:'DeviceID',title:'区域编码',width:150,align:'center'},		
				    {field:'DeviceName',title:'设备名称',width:120,align:'center'},
				    {field:'T_PARENTID',title:'父级代码',width:150,align:'center'},
				    {field:'T_SWERK',title:'维护工厂',width:150,align:'center'},
				    {field:'T_IWERK',title:'计划工厂',width:150,align:'center'},
				    {field:'T_KOSTL',title:'成本中心',width:150,align:'center'}		    
			    ]],
			    pagination:true,
			    rownumbers:true			
		    });
		}
		
		function DeviceList(id){
            var query={param:'device',id:id}; //把查询条件拼接成JSON
            $("#gridDevice").datagrid('options').queryParams=query; //把查询条件赋值给datagrid内部变量
            $("#gridDevice").datagrid('reload'); //重新加载
		}
		
		function ItemGrid(id){
            $('#gridItem').datagrid({
			    title:'点检项信息',
			    iconCls:'icon-search',
			    nowrap: true,
			    autoRowHeight: false,
			    striped: true,
			    align:'center',
			    collapsible:true,
			    url:'PlanDown_MZ.aspx',
			    sortName: 'ID',
			    sortOrder: 'asc',
			    remoteSort: false,
			    queryParams:{param:'item',id:id},
			    idField:'ID',
			    columns:[[			    
				    {field:'T_ITEMID',title:'点检项编码',width:150,align:'center'},		
				    {field:'T_ITEMPOSITION',title:'点检项部位',width:120,align:'center'},
				    {field:'T_ITEMDESC',title:'点检项描述',width:150,align:'center'},
				    {field:'T_CONTENT',title:'点检项检查内容',width:150,align:'center'},
				    {field:'T_TYPE',title:'点检类型',width:150,align:'center'},
				    {field:'T_OBSERVE',title:'观察名称',width:150,align:'center'},
				    {field:'T_UNIT',title:'单位',width:150,align:'center'},
				    {field:'F_LOWER',title:'测量下限',width:150,align:'center'},
				    {field:'F_UPPER',title:'测量上限',width:150,align:'center'},
				    {field:'T_STARTTIME',title:'开始时间',width:150,align:'center'},
				    {field:'T_PERIODVALUE',title:'周期数值',width:150,align:'center'},
				    {field:'T_STATUS',title:'检测状态',width:150,align:'center'},
				    {field:'T_PERIODTYPE',title:'周期类型',width:150,align:'center'}	    
			    ]],
			    pagination:true,
			    rownumbers:true			
		    });
		}
		
		function ItemList(id){
            var query={param:'item',id:id}; //把查询条件拼接成JSON
            $("#gridItem").datagrid('options').queryParams=query; //把查询条件赋值给datagrid内部变量
            $("#gridItem").datagrid('reload'); //重新加载
		}
		
		function beforeCheck(treeId, treeNode) {
			return (treeNode.doCheck !== false);
		}

		function onClick(event, treeId, treeNode, clickFlag) {
		    $('#txtID').val(treeNode.id);
		    $('#txtName').val(treeNode.name);
		    $('#txtParent').val(treeNode.pId);
//		    Grid(id)(treeNode.id);
            $.post("PlanDown_MZ.aspx", { param: 'judge',id:treeNode.id}, function (data) {
                if(data.num=='1'){
                    RouteList('0');
                }else if(data.num=='2'){
                    $.post("PlanDown_MZ.aspx", { param: 'judgeRouteOrArea',id:treeNode.id}, function (data) {
                        if(data.info=='route'){
                            RouteList(treeNode.id);
                            $('#dv_route').show();
                            $('#dv_area').hide();
                            $('#dv_device').hide();
                            $('#dv_itme').hide();
                        }else{
                            AreaList(treeNode.id);
                            $('#dv_route').hide();
                            $('#dv_area').show();
                            $('#dv_device').hide();
                            $('#dv_itme').hide();
                        }
                    });
                }else if(data.num=='3'){
                    $.post("PlanDown_MZ.aspx", { param: 'judgeAreaOrDevice',id:treeNode.id}, function (data) {
                        if(data.info=='area'){
                            AreaList(treeNode.id);
                            $('#dv_route').hide();
                            $('#dv_area').show();
                            $('#dv_device').hide();
                            $('#dv_itme').hide();
                        }else{
                            DeviceList(treeNode.id);
                            $('#dv_route').hide();
                            $('#dv_area').hide();
                            $('#dv_device').show();
                            $('#dv_itme').hide();
                        }
                    });
                }else if(data.num=='4'){
                    $.post("PlanDown_MZ.aspx", { param: 'judgeDeviceOrItem',id:treeNode.id}, function (data) {
                        if(data.info=='device'){
                            DeviceList(treeNode.id);
                            $('#dv_route').hide();
                            $('#dv_area').hide();
                            $('#dv_device').show();
                            $('#dv_itme').hide();
                        }else{
                            ItemList(treeNode.id);
                            $('#dv_route').hide();
                            $('#dv_area').hide();
                            $('#dv_device').hide();
                            $('#dv_itme').show();
                        }
                    });

                }
            },'json');
		};	
		
		function onCheck(e, treeId, treeNode) {
		    if(treeNode.checked==true){
		        id+=treeNode.nodeID+ ',';
		    }else{
		        id=id.replace(treeNode.nodeID+',','');
		    }
		}		
		function checkNode(e) {
			var zTree = $.fn.zTree.getZTreeObj("tree"),
			type = e.data.type,
			nodes = zTree.getSelectedNodes();
			if (type.indexOf("All")<0 && nodes.length == 0) {
				alert("请先选择一个节点");
			}

			if (type == "checkAllTrue") {
				zTree.checkAllNodes(true);
			} else if (type == "checkAllFalse") {
				zTree.checkAllNodes(false);
			}
		}
		  var user='';
        $(function(){
            $.post("PlanDown_MZ.aspx", { param: ''}, function (data) {
                var  zNodes = eval(data.menu);
    		    $('#txtID').val(data.id);
                $('#txtName').val(data.name);
                $.fn.zTree.init($("#tree"), setting, zNodes);
            },'json');
            
            $("#menu").css("height",pageHeight()-100);
            $("#menu").css("width",pageWidth()-100);
            $("#dv_tree").css("height",pageHeight()-118);
            $("#tree").css("height",pageHeight()-200);
            
            $("#grid").css("width",pageWidth()-380);
            $("#grid").css("height",pageHeight()-140);
            
            $("#checkAllTrue").hide();
            $("#checkAllFalse").hide();
            
			$("#checkAllTrue").bind("click", {type:"checkAllTrue"}, checkNode);
			$("#checkAllFalse").bind("click", {type:"checkAllFalse"}, checkNode);
            
            $("#btnOpen").click(function() { 
                id = id.substring(0,id.length-1); 
                startOcx("http://172.18.25.178/DJ/",user,id);
            }); 
            RouteGrid('0');
            AreaGrid('0');
            DeviceGrid('0');
            ItemGrid('0');
            $('#dv_route').show();
            $('#dv_area').hide();
            $('#dv_device').hide();
            $('#dv_itme').hide();
            getUser();
        });
        
      
        function getUser(){
             $.post("PlanDown_MZ.aspx", { param: 'user'}, function (data) {
                user = data.user;
             },'json');
        }
        
        function startOcx(url,userID,routeID) {
            try 
            {
//                    url="http://10.3.30.18/DJ/";
                    var ocx = document.getElementById('XSTCommOcx');
                    ocx.SetData("Key_ProjectNo", "华电");
                    ocx.SetData("Key_WebServiceBaseDir",url);
                    ocx.SetData("Key_UserAcount",userID);
                    ocx.SetData("Key_LineIDStr", routeID);
                    ocx.SetData("Key_InnerControl", "InnerControl_Communication");
                    ocx.SetData("Key_StartControl", "");
                }
                catch (ex) 
                {
                    alert(ex.description);
                alert("通信组件没有安装,需要下载安装!");
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
        
    -->
    </script>

    <div id="menu">
        <input id="txtHide" type="text" name="name" style="display: none;" />
        <table id="__01" width="100%" height="95%" border="0" cellpadding="0" cellspacing="0"
            bgcolor="#2a88bb">
            <tr>
                <td colspan="3" background="../img/table-head.jpg" height="30px" valign="middle"
                    class="style6">
                    &nbsp;任务下载
                </td>
            </tr>
            <tr>
                <td background="../img/table-head-2.jpg" height="25px" valign="middle" class="style5">
                    &nbsp;线路
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
                        <ul>
                            <li>
                                <div style="margin-top: 10px; top: 10px; text-align: center;">
                                    <%--  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a id="checkAllTrue" href="#" class="easyui-linkbutton"
                                        data-options="iconCls:'icon-ok'"> 全选</a> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a id="checkAllFalse"
                                            href="#" class="easyui-linkbutton" data-options="iconCls:'icon-reload'">取消</a>--%>
                                    <a id="btnOpen" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok'">
                                        链接</a>
                                </div>
                            </li>
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
                                            &nbsp;&nbsp;&nbsp; 节点编码&nbsp;
                                            <input id="txtID" class="easyui-validatebox" type="text" name="name" style="border: solid 1px #E0ECF9;
                                                text-align: center;" disabled="disabled" />
                                            &nbsp;&nbsp;&nbsp;节点名称&nbsp;
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
                                <div id="dv_route">
                                    <table id="gridRoute">
                                    </table>
                                </div>
                                <div id="dv_area">
                                    <table id="gridArea">
                                    </table>
                                </div>
                                <div id="dv_device">
                                    <table id="gridDevice">
                                    </table>
                                </div>
                                <div id="dv_itme">
                                    <table id="gridItem">
                                    </table>
                                </div>
                            </li>
                        </ul>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
