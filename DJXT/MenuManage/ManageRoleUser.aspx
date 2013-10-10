<%@ Page Language="C#" AutoEventWireup="true" Inherits="MenuManage_ManageRoleUser" Codebehind="ManageRoleUser.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>角色人员管理</title>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/css/djxt.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #menu
        {
        	border:1px solid #2a88bb;
        	height:100%;
        }
        .text1
        {
            font-size: 12px;
            background:#f2f5f7;
            border:none;
            text-align:right;
            vertical-align: middle;
        }
        .text2
        {
            font-size: 12px;
            background:#f2f5f7;
            border:none;
            text-align:left;
            vertical-align: middle;
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

        .style5
        {
            font-size:13px;     
            color:Black;
        }
        
    </style>

    <script src="../jQueryEasyUI/jquery-1.6.2.js" type="text/javascript"></script>

    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>

    <script src="../js/jquery.ztree.core-3.5.js" type="text/javascript"></script>

    <script type="text/javascript">
    
		<!--
        var btn_num_add = 1;
        var btn_num_remove = 1;
        var btn_num_save = 1;
        var ihight;
        var roleId;
        var setting = {
            data: {
                key: {
                    title: "t"
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
            $("#LabTree_RoleName").val(treeNode.name); //右侧表格显示组织名称
            //alert(treeNode.id);
            roleId = treeNode.id;
            seachParment(treeNode.id);
        }

        function seachParment(id) {
            var query = { param: 'seachList', id: id }; //把查询条件拼接成JSON
            $("#grid").datagrid('options').queryParams = query; //把查询条件赋值给datagrid内部变量
            $("#grid").datagrid('reload'); //重新加载
        }

        $(document).ready(function () {
            $('#dv_add').hide();
            ihight = pageHeight();
            //alert(ihight);
            Grid();
            $.post("ManageRoleUser.aspx", { param: '' }, function (data) {
                var nodeEval = eval(data.menu);
                $.fn.zTree.init($("#treeDemo"), setting, nodeEval);
            }, 'json');
        });

        function Grid() {
            $('#grid').datagrid({
                title: '人员列表',
                iconCls: 'icon-search',
                nowrap: true,
                border: false,
                autoRowHeight: false,
                striped: true,
                height: ihight - 155,
                align: 'center',
                collapsible: true,
                url: 'ManageRoleUser.aspx',
                sortName: 'ID_KEY',
                sortOrder: 'asc',
                remoteSort: false,
                queryParams: { param: 'seachList', id: '', name: '' },
                idField: 'id',
                frozenColumns: [[
                { field: 'ck', checkbox: true }
			    ]],
                columns: [[
				    { field: 'id', title: '用户名', width: 120, align: 'center' },
				    { field: 'name', title: '真实姓名', width: 150, align: 'center' }
			    ]],
                pagination: true,
                rownumbers: true,
                toolbar: [{
                    id: 'btnadd',
                    text: '添加人员',
                    iconCls: 'icon-add',
                    handler: function () {
                        $('#dv_add').show();
                        AddShow();
                    }
                },
                {
                    id: 'btnedit',
                    text: '编辑人员',
                    iconCls: 'icon-edit',
                    handler: function () {
                        var rows = $('#grid').datagrid('getSelections');
                        var id = "";
                        $.each(rows, function (i, n) {
                            id += "" + n.id + ",";
                        });
                        id = id.substring(0, id.length - 1);
                        $('#txtHide').val(id);
                        EditShow(id);
                    }
                },
		        {
		            id: 'btnremove',
		            text: '删除人员',
		            iconCls: 'icon-remove',
		            handler: function () {
		                var rows = $('#grid').datagrid('getSelections');
		                var id = "";
		                var name = "";
		                $.each(rows, function (i, n) {
		                    id += "'" + n.id + "',";
		                    name += n.name + ',';
		                });
		                name = name.substring(0, name.length - 1);
		                id = id.substring(0, id.length - 1);
		                $.messager.confirm('删除组织信息', '你确定要删除 ' + id + '  吗?', function (ok) {
		                    if (ok) {
		                        Remove(id);
		                    } else {
		                        $.messager.alert('删除组织信息', '删除已取消!', 'info');
		                    }
		                });
		            }
		        }],
                onDblClickRow: function (rowIndex, rowData) {
                    $('#grid').datagrid('clearSelections');
                    $('#txtHide').val(rowData.id);
                    EditShow(rowData.id);
                }
            });
        }

        function AddShow() {
            if (roleId == "" || roleId == null || roleId == "qianwanbunengdengyu1") {
                $.messager.alert('添加人员信息', '请选择一个角色!', 'info');
            }
            else {
                $("#dv_add").attr('title', '添加人员');
                $('#dv_add').show();
                $.post("ManageRoleUser.aspx", { param: '' }, function (data) {
                    var nodeEval = eval(data.menu);
                    $.fn.zTree.init($("#treeDemo"), setting, nodeEval);
                }, 'json');
                $('#dv_add').dialog({
                    buttons: [{
                        text: '添加',
                        iconCls: 'icon-ok',
                        handler: function () {
                            Add($('#txtUserID').val(), $('#txtUserName').val(), $('#txtPwd').val(), $('#txtPwd2').val());
                        }
                    }, {
                        text: '重置',
                        iconCls: 'icon-no',
                        handler: function () {
                            $('#dv_add').dialog('close');
                        }
                    }]
                });
            }
        }

        function Tree(id) {
            $.post("ManageOrgUser.aspx", { param: '', id: id, name: '' }, function (data) {
                var nodeEval = eval(data.menu);
                var treeId = data.treeId;
                if (treeId != '1') {
                    $("#dv_right").css("width", "100%")
                    $("#dv_left").hide();
                } else {
                    $.fn.zTree.init($("#tree"), setting, nodeEval);
                    $("#dv_right").css("width", pageWidth() - 290);
                    $("#dv_left").show();
                }
            }, 'json');
        };

        function pageHeight() {
            if ($.browser.msie) {
                return document.compatMode == "CSS1Compat" ? document.documentElement.clientHeight :
            document.body.clientHeight;
            } else {
                return self.innerHeight;
            }
        };

        function pageWidth() {
            if ($.browser.msie) {
                return document.compatMode == "CSS1Compat" ? document.documentElement.clientWidth :
            document.body.clientWidth;
            } else {
                return self.innerWidth;
            }
        }; 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="menu">
        09</div>
    

    <div id="dv_add" data-options="iconCls:'icon-save'" style="padding: 5px; width: 700px;
        height: 400px;">
        <div id="dv_Member_info">
            <table class="admintable" width="100%">
           <tr>
		<td background="../img/table-head-2.jpg" height="25px" valign="middle" class="style5" width="30%">&nbsp;选择组织机构树</td>
        <td background="../img/table-head-3.jpg" width="1px"></td>
        <td background="../img/table-head-2.jpg" height="25px" valign="middle" class="style5">&nbsp;选择组织机构树</td>
		</tr>
                <tr>
        <td style="background-color:#f2f5f7" align="center" valign="middle" class="style7" height="30px">
            组织机构树：&nbsp;           
                <asp:DropDownList ID="DropDownList1" runat="server" Width="100" OnSelectedIndexChanged="DropDownListChange" AutoPostBack="true">
                </asp:DropDownList>
        </td>
        <td background="../img/table-head-3.jpg" width="1px"></td>
        <td style="background-color:#f2f5f7" align="center" valign="middle" class="style7" height="30px">
        <input type="text" ID="LabTree_OrgName" runat="server" class="text1" />
        </td>
        </tr>
                <tr>
		<td background="../img/table-head-2.jpg" height="25px" valign="middle" class="style5" width="30%">&nbsp;组织机构树</td>
        <td background="../img/table-head-3.jpg" width="1px"></td>
        <td background="../img/table-head-2.jpg" height="25px" valign="middle" class="style5">&nbsp;人员列表</td>
		</tr>
        <tr>
        <td style="background-color: White" align="left" valign="top">
            <div id="dv_left" style="float: left; height:400px;">
                <div id="dv_tree" class="zTreeDemoBackground left">
                    <ul id="tree" class="ztree">
                    </ul>
                </div>
                </div>
            </td>
            <td background="../img/table-head-3.jpg" width="1px"></td>
            <td valign="top" align="left">
                <div width="100%" height="100%">
                <table id="GridOrg" width="100%" height="100%">
                </table>
                </div>
            </td> 
            </tr>
            </table>
        </div>
    </div>
</form>
</body>
</html>
