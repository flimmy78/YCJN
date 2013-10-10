<%@ Page Language="C#" AutoEventWireup="true" Inherits="MenuManage_ManageOrgUser" Codebehind="ManageOrgUser.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>组织人员管理</title>
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
        .style6
        {
        	font-size:13px;
        	color:#0a4869;
        	font-weight:bold;
        }

    </style>

    <script src="../jQueryEasyUI/jquery-1.6.2.js" type="text/javascript"></script>

    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>

    <script src="../js/jquery.ztree.core-3.5.js" type="text/javascript"></script>
    <script type="text/javascript">
		<!--
        var treeNodeId; //当前组织机构的ID
        var treeAllId; //当前组织机构树的ID
        var ihight;
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
            var tree_orgname = $("#DropDownList1").find("option:selected").text() + " — " + treeNode.name;
            $("#LabTree_OrgName").val(tree_orgname);
            treeNodeId = treeNode.id;
            treeAllId = $("#DropDownList1").val();
            seachParment(treeNode.id, treeNode.name);
            //alert(treename);
        };

        function seachParment(id, name) {
            var query = { param: 'seachList', id: id, name: name }; //把查询条件拼接成JSON
            $("#grid").datagrid('options').queryParams = query; //把查询条件赋值给datagrid内部变量
            $("#grid").datagrid('reload'); //重新加载
        }

        $(document).ready(function () {
            $("#lblTitle").text("数据查询");
            Tree("");
            $('#dv_add').hide();
            $('#dv_edit').hide();
            Grid();
        });

        function Grid() {
            $('#grid').datagrid({
                title: '人员列表',
                iconCls: 'icon-search',
                nowrap: true,
                border: false,
                autoRowHeight: false,
                striped: true,
                height: 520,
                align: 'center',
                collapsible: true,
                url: 'ManageOrgUser.aspx',
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
            if (treeNodeId == "" || treeNodeId == null) {
                $.messager.alert('添加人员信息', '请选择一个组织机构!', 'info');
            }
            else {
                $("#dv_add").attr('title', '添加人员');
                $('#txtUserID').val('');
                $('#txtUserName').val('');
                $('#txtPwd').val('');
                $('#txtPwd2').val('');
                $('#dv_add').show();
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
        function Add(id, name, pwd) {
            if ($("#txtUserID").val() == "" || $("#txtUserID").val() == null || escape($("#txtUserName").val()) == null || escape($("#txtUserName").val()) == "" || escape($("#txtPwd").val()) == null || escape($("#txtPwd").val()) == "" || escape($("#txtPwd2").val()) == null || escape($("#txtPwd2").val()) == "") {
                $("#Par").hide();
                $("#slclass").hide();
                $.messager.alert('添加人员信息', '用户ID、用户姓名、密码都不能为空!', 'info');
                $("#Par").show();
                $("#slclass").show();
            } else {
                $.post("ManageOrgUser.aspx", { param: 'JudgeMember', userID: escape($("#txtUserID").val()) }, function (data) {
                    if (Number(data.judge) == 1 && Number(data.num) == 1) {
                        $('#txtPwd').val('');
                        $('#txtPwd2').val('');
                        $.messager.alert('添加人员信息', '已经存在用户名为：' + $("#txtUserID").val() + '的人员,不能重复添加!', 'error');
                    }
                    else if ($("#txtPwd").val() != $("#txtPwd2").val()) {
                        $.messager.alert('添加人员信息', '密码输入不一致，请重新输入密码', 'error');
                    }
                    else {
                        $.post("ManageOrgUser.aspx", { param: 'AddMember', userID: $("#txtUserID").val(), userName: escape($("#txtUserName").val()), pwd: escape($("#txtPwd").val()),
                            img: escape($("#flImg").val()), par: $("#Par").val(), treeNodeId: treeNodeId, treeAllId: treeAllId
                        }, function (data) {
                            $.messager.alert('添加人员信息', data.info, 'info');
                            //Tree($('#txtID').val());
                            //$('#dv_add').hide();
                            $('#dv_add').dialog('close');
                            seachParment(treeNodeId, '');
                        }, 'json');
                    }
                }, 'json');
            }
        }
        function EditShow(id) {
            $.post("ManageOrgUser.aspx", { param: 'Edit', id: id }, function (data) {
                var list = data.list;
                if (data.img.length > 0) {
                    $("#img_").show();
                    $("#img_").attr("src", data.img);
                } else {
                    $("#img_").hide();
                }
                $("#txtUserIDEdit").val(list[0].T_USERID);
                $("#txtUserNameEdit").val(list[0].T_USERNAME);
                $("#txtPwdEdit").val(list[0].T_PASSWD);
                $("#txtPwd2Edit").val(list[0].T_PASSWD);
                var count = 0;
            }, 'json');

            $("#dv_edit").attr('title', '编辑人员信息');
            $('#dv_edit').show();
            $('#dv_edit').dialog({
                buttons: [{
                    text: '保存',
                    iconCls: 'icon-ok',
                    handler: function () {
                        Edit();
                    }
                }, {
                    text: '取 消',
                    handler: function () {
                        $('#dv_edit').dialog('close');
                    }
                }]
            });
        }
        function Edit() {
            if ($("#txtUserIDEdit").val() == null || $("#txtUserIDEdit").val() == '' || $("#txtUserNameEdit").val() == null || $("#txtUserNameEdit").val() == '' || $("#txtPwdEdit").val() == null || $("#txtPwdEdit").val() == '' || $("#txtPwd2Edit").val() == null || $("#txtPwd2Edit").val() == '') {
                $.messager.alert('编辑人员信息', '编辑人员信息时：用户ID、用户姓名和密码都不能为空!', 'info');
            } else {
                if ($("#txtHide").val() == $("#txtUserIDEdit").val()) {
                    if ($("#txtPwd").val() != $("#txtPwd2").val()) {
                        $.messager.alert('添加人员信息', '密码输入不一致，请重新输入密码', 'error');
                    }
                    else {
                        $.post("ManageOrgUser.aspx", { param: 'EditMemberInfo', oldId: $("#txtHide").val(), id: $("#txtUserIDEdit").val(), name: escape($("#txtUserNameEdit").val()),
                            pwd: escape($("#txtPwdEdit").val()), img: escape($("#flImgEdit").val()), treeNodeId: treeNodeId, treeAllId: treeAllId
                        }, function (data) {
                            seachParment(treeNodeId, '');
                        }, 'json');
                    }
                } else {
                    $.post("ManageOrgUser.aspx", { param: 'JudgeMember', userID: $("#txtUserIDEdit").val() }, function (data) {
                        if (Number(data.judge) == 1) {
                            $.messager.alert('编辑人员信息', '已经存在用户ID为：' + $("#txtUserIDEdit").val() + '  的人员,用户名不能重复!', 'error');
                        } else {
                            $.post("ManageOrgUser.aspx", { param: 'EditMemberInfo', oldId: $("#txtHide").val(), id: $("#txtUserIDEdit").val(), pwd: escape($("#txtPwdEdit").val()),
                                name: escape($("#txtUserNameEdit").val()), img: escape($("#flImgEdit").val()), treeNodeId: treeNodeId, treeAllId: treeAllId
                            }, function (data) {
                                $('#dv_edit').dialog('close');
                                seachParment(treeNodeId, '');
                            }, 'json');
                        }
                    }, 'json');
                }
            }
        }
        function Remove(id) {
            $.post("ManageOrgUser.aspx", { param: 'Remove', id: id }, function (data) {
                seachParment(treeNodeId, '');
                $.messager.alert('删除人员信息', data.info, 'info');
            }, 'json');
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
		//-->
	</script>
</head>
<body style="height:100%">
    <form id="form1" runat="server">
    <div id="menu">
    <table width="100%" height="90%" border="0" cellpadding="0" cellspacing="0"  bgcolor="#ffffff" >
        <tr>
		    <td colspan="3" background="../img/table-head.jpg" height="30px" valign="middle" class="style6">&nbsp;组织人员管理</td>
	    </tr>
    <tr>
    <td valign="top" style="background-color: White" height="100%" width="40%">
    <div>
    <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0"  bgcolor="#ffffff" >
    <tr>
		<td background="../img/table-head-2.jpg" height="25px" valign="middle" class="style5">&nbsp;选择组织机构树</td>
		</tr>
        <tr>
		<td style="background-color:#f2f5f7" align="center" valign="middle" class="style7" height="30px">
        请选择组织机构树：&nbsp;           
                <asp:DropDownList ID="DropDownList1" runat="server" Width="100" OnSelectedIndexChanged="DropDownListChange" AutoPostBack="true">
                </asp:DropDownList>
        </td>
		</tr>
        <tr>
		<td background="../img/table-head-2.jpg" height="25px" valign="middle" class="style5">&nbsp;组织机构树</td>
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
            </tr>
	</table>
	</div>
	</td>
	<td background="../img/table-head-3.jpg" width="1px"></td>
	<td valign="top">
    <div>
    <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0"  bgcolor="#ffffff" >
    <tr>
		<td background="../img/table-head-2.jpg" height="25px" valign="middle" class="style5">&nbsp;选定的组织机构</td>
		</tr>
        <tr>
		<td style="background-color:#f2f5f7" align="center" valign="middle" class="style7" height="30px">
        <input type="text" ID="LabTree_OrgName" runat="server" class="text1" />
        </td>
		</tr>
        <tr>
		<td background="../img/table-head-2.jpg" height="25px" valign="middle" class="style5">&nbsp;人员列表</td>
		</tr>
        <tr><td>
    <div width="100%" height="100%">
    <table id="grid" width="100%" height="100%">
    </table>
    </div>
    </td></tr>
    </table>
    </div>
    </td>                                
            </tr>
    </table>
    </div>
    </form>

    <div id="dv_add" data-options="iconCls:'icon-save'" style="padding: 5px; width: 700px;
        height: 280px;">
        <div id="dv_Member_info">
            <table class="admintable" width="100%">
                <tr>
                    <th class="adminth" colspan="4">
                        添加人员信息
                    </th>
                </tr>
                <tr>
                    <td class="admincls0" align="center">
                        用户ID
                    </td>
                    <td class="admincls0">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtUserID" type="text" />
                    </td>
                    <td class="admincls0" align="center">
                        用户姓名
                    </td>
                    <td class="admincls0">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtUserName" type="text" />
                    </td>
                </tr>
                <tr>
                    <td class="admincls1" align="center">
                        用户密码
                    </td>
                    <td class="admincls1">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtPwd" type="password" />
                    </td>
                    <td class="admincls1" align="center">
                        确认密码
                    </td>
                    <td class="admincls1">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtPwd2" type="password" />
                    </td>
                </tr>
                <tr>
                    <td class="admincls0" align="center">
                        照片
                    </td>
                    <td class="admincls0" colspan="3">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="flImg" type="file" style="width: 280px;" /></td>
                </tr>
            </table>
        </div>
    </div>
    <div id="dv_edit" style="padding: 5px; width: 700px; height: 360px;">
        <div id="dv_Member_Info_Edit">
            <table class="admintable" width="100%">
                <tr>
                    <th class="adminth" colspan="4">
                        编辑人员信息
                    </th>
                </tr>
                <tr>
                    <td class="admincls0" align="center">
                        用户ID
                    </td>
                    <td class="admincls0">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtUserIDEdit" type="text" />
                    </td>
                    <td class="admincls0" align="center">
                        用户姓名
                    </td>
                    <td class="admincls0">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtUserNameEdit" type="text" />
                    </td>
                </tr>
                <tr>
                    <td class="admincls1" align="center">
                        用户密码
                    </td>
                    <td class="admincls1">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtPwdEdit" type="password" />
                    </td>
                    <td class="admincls1" align="center">
                        确认密码
                    </td>
                    <td class="admincls1">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="txtPwd2Edit" type="password" />
                    </td>
                </tr>
                <tr>
                    <td class="admincls0" align="center">
                        照&nbsp;&nbsp;&nbsp;&nbsp;片
                    </td>
                    <td class="admincls0" colspan="3">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="flImgEdit" type="file" style="width: 280px;" /><br />
                    </td>
                </tr>
                <tr>
                    <td class="admincls1" align="center">
                        已传照片
                    </td>
                    <td class="admincls1" colspan="3" align="center">
                        <img id="img_" alt="" src="" style="width: 60px; height: 80px; text-align: center;" />
                        <input id="txtHide" name="name" style="display: none;" type="text" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</body>
</html>
