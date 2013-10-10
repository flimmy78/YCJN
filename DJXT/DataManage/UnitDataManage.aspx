<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitDataManage.aspx.cs"
    Inherits="DJXT.DataManage.UnitDataManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/css/djxt.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../js/jquery.blockUI.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            font-family: 宋体, Helvetica, sans-serif;
            font-size: 12px;
            background: #dfe8f6;
        }
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
        .style5
        {
            font-size: 12px;
            color: Black;
        }
        .style6
        {
            font-size: 13px;
            color: #0a4869;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function change_company() {
            var par = $("#sec_company").find("option:selected").val();
            if (par != "-请选择-") {
                $.post(
        "../datafile/GETWarningThresholdData.aspx",
        {
            sec_type_real: par
        },
    function (data) {
        var array = new Array();
        array = data.split('|');
        $("#sec_electric").empty();
        $("#sec_crew").empty();
        if (data == "") {
            $("#sec_electric").append("<option value=请选择>-请选择-</option>");
            $("#sec_crew").append("<option value=请选择>-请选择-</option>");
        }
        else {
            $("#sec_electric").append(array[0]);
            $("#sec_crew").append(array[1]);


        }
    },

    "html");
            }
            else {
                $("#sec_electric").empty();
                $("#sec_crew").empty();
                $("#sec_electric").append("<option value=请选择>-请选择-</option>");
                $("#sec_crew").append("<option value=请选择>-请选择-</option>");
            }
        }
        function change_electric() {
            var par = $("#sec_electric").find("option:selected").val();
            if (par != "-请选择-") {
                $.post(
                    "../DataFile/GETWarningThresholdData.aspx",
                    {
                        electric_id_real: par
                    },
                function (data) {
                    var array = new Array();
                    array = data.split('|');
                    $("#sec_crew").empty();
                    if (data == "") {
                        $("#sec_crew").append("<option value=请选择>-请选择-</option>");
                    }
                    else {
                        $("#sec_crew").append(array[0]);
                    }
                },

                "html");
            }
            else {
                $("#sec_crew").empty();
                $("#sec_crew").append("<option value=请选择>-请选择-</option>");
            }
        }
        function query() {
            if ($("#sec_crew  option:selected").val() == "-请选择-") {
                alert("请选择机组！");
            }
            else {
                var rating = $("#sec_crew  option:selected").val() + "," + $("#sec_type  option:selected").val() + "," + $("#txt_file_name").val();
                //alert(rating);
                GridSta(rating);
            }
        }

        function QX(a) {
            if (a == 1) {
                $('#dv_rename').dialog('close');
            }
            else {
                $('#div_update').dialog('close');
            }
        }

        function cancel() {
            $('#div_update').dialog('close');
        }
        function cancel_down() {
            $('#div_down').dialog('close');
        }
        function GridSta(list) {
            $('#gridItem').datagrid({
                nowrap: true,
                autoRowHeight: false,
                fitColumns: true,
                striped: true,
                align: 'center',
                loadMsg: "正在努力为您加载数据", //加载数据时向用户展示的语句
                collapsible: true,
                url: 'UnitDataManage.aspx',
                sortName: 'T_TIME',
                sortOrder: 'desc',
                remoteSort: false,
                queryParams: { param: list },
                idField: 'T_TIME',
                singleSelect:true ,
                columns: [[
                    { field: '多选', checkbox: true },
                    { field: 'ID', title: '序号', width: $(window).width() * 0.1 * 0.98, align: 'center' },
                    { field: 'ID_KEY', title: 'ID_KEY', width: 120, hidden: true },
                    { field: 'FILE_DESC', title: '文件名', width: $(window).width() * 0.2 * 0.98, align: 'center' },
                    { field: 'PARADESC', title: '文件分类', width: $(window).width() * 0.2 * 0.98, align: 'center' },
                    { field: 'T_TIME', title: '上传时间', width: $(window).width() * 0.2 * 0.98, align: 'center' },
                    { field: '下载', title: '下载', width: $(window).width() * 0.1 * 0.98, align: 'center',
                        formatter: function (value, row, index) {
                            var s = '<a href="#" onclick="download(' + row.ID_KEY + ')">下载</a> ';
                            return s;
                        }
                    }
                ]],
                pagination: true,
                rownumbers: true,
                toolbar: [{
                    id: 'rename',
                    text: '重命名',
                    iconCls: 'icon-edit',
                    handler: function () {
                        if ($("#gridItem").datagrid("getSelected") != null) {
                            $("#hf_value").attr("value", $("#gridItem").datagrid("getSelected").ID_KEY);
                            $('#dv_rename').show();
                            $("#dv_rename").attr('title', '重命名');
                            var dlg = $('#dv_rename').dialog({
                        });
                        dlg.parent().appendTo(jQuery("form:first"));
                    } else
                    { alert("请选择要重命名的文件！"); }
                }

            },
		        {
		            id: 'del',
		            text: '删除',
		            iconCls: 'icon-remove',
		            handler: function () {
		                deleterow()
		            }
		        },
		        {
		            id: 'del',
		            text: '上传',
		            iconCls: 'icon-save',
		            handler: function () {
		                $('#div_update').show();
		                $("#div_update").attr('title', '上传文件');
		                var dlg = $('#div_update').dialog({
		            });
		            dlg.parent().appendTo(jQuery("form:first"));
		        }
		    }],
            onSelect: function (index, row) {
                var rows = $("#gridItem").datagrid("getRows");
                for (var i = 0; i < rows.length; i++) {
                    if (rows[i]["ID"] - 1 != index) {
                        $(".datagrid-row[datagrid-row-index=" + i + "] input[type='checkbox']")[0].checked = false;
                    }
                }
            }

        });
    }

    function download(index) {
        //document.execCommand("SaveAs");
        $('#div_down').show();
        $("#div_down").attr('title', '下载文件');
        var dlg = $('#div_down').dialog({
    });
    dlg.parent().appendTo(jQuery("form:first"));
//        alert(index);
//        $.post("UnitDataManage.aspx", { para_down: index }, function (data) {
//        }, 'json');
    }
        function deleterow() {
            $.messager.confirm('Confirm', 'Are you sure?', function (r) {
                if (r) {
                    if ($("#gridItem").datagrid("getSelected") != null) {
                       
                        var rows = $("#gridItem").datagrid("getSelected"); // 这段代码是获取当前页的所有行。
                        var rating = rows.ID_KEY;
                        
                        $.post("UnitDataManage.aspx", { para_delete: rating }, function (data) {

                        }, 'json');
                        $('#gridItem').datagrid('deleteRow', $("#gridItem").datagrid("getSelected").ID - 1);
                    }
                    else
                    { alert("请选择要删除的文件！"); }
                }
            });
        }
        function sure() {
            if ($("#txt_name").val() != "") {
                var rating = $("#hf_value").val() + "," + $("#txt_name").val();
                $.post("UnitDataManage.aspx", { para_rename: rating }, function (data) {
                    //alert(data);
                }, 'json');
                 $('#dv_rename').dialog('close');
                 $('#gridItem').datagrid('reload'); 
            }
            else
            { alert("请输入要修改的名字！"); }
        }

        function update() {
            var rating = $("#fp_value").val() + "|" + $("#sec_crew  option:selected").val() + "|"  + $("#sec_data_type  option:selected").val();
            //alert($("#fp_value").val());
            $.post("UnitDataManage.aspx", { para_udate: rating }, function (data) {
            }, 'json');

            $('#div_update').dialog('close');
            setTimeout("aa()", 1000);
        }

        function aa() {
            $('#gridItem').datagrid('reload');
        }

        function browseFolder() {
            try {
                var Message = "\u8bf7\u9009\u62e9\u6587\u4ef6\u5939"; //选择框提示信息
                var Shell = new ActiveXObject("Shell.Application");
                var Folder = Shell.BrowseForFolder(0, Message, 64, 17); //起始目录为：我的电脑
                //var Folder = Shell.BrowseForFolder(0,Message,0); //起始目录为：桌面

                if (Folder != null) {
                    Folder = Folder.items(); // 返回 FolderItems 对象

                    Folder = Folder.item(); // 返回 Folderitem 对象
                    Folder = Folder.Path; // 返回路径

                    if (Folder.charAt(Folder.length - 1) != "\\") {
                        Folder = Folder + "\\";
                    }
                    $("#txt_liulan").attr("value", Folder);

                    var rows = $("#gridItem").datagrid("getSelected"); // 这段代码是获取当前页的所有行。


                    $.post("UnitDataManage.aspx", { para_down: rows.ID_KEY +"|"+ $("#txt_liulan").val() }, function (data) {
                    }, 'json');
                    
                    $('#div_down').dialog('close');
                    alert("下载成功！");
                }
            }
            catch (e) {
                alert(e.message);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data" >
    <div id="menu">
        <table width="100%" height="92%" cellpadding="4" cellspacing="0" class="style5" border="0">
            <tr>
                <td background="../img/table-head.jpg" height="30px" valign="middle" class="style6"
                    colspan="2">
                    &nbsp;&nbsp;机组资料管理
                </td>
            </tr>
            <tr>
                <td class="style5">
                    <table>
                        <tr>
                            <td>
                                省&nbsp; 公&nbsp; 司&nbsp;
                            </td>
                            <td>
                                <span style="z-index: -9999">
                                    <select id="sec_company" runat="server" onchange="change_company()">
                                    </select>&nbsp; </span>
                            </td>
                            <td>
                                电厂&nbsp;
                            </td>
                            <td>
                                <span style="z-index: -9999">
                                    <select id="sec_electric" runat="server" onchange="change_electric()">
                                        <option>-请选择-</option>
                                    </select>&nbsp; </span>
                            </td>
                            <td>
                                机组&nbsp;
                            </td>
                            <td>
                                <span style="z-index: -9999">
                                    <select id="sec_crew" runat="server">
                                        <option>-请选择-</option>
                                    </select>
                                </span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style5">
                    <table>
                        <tr>
                            <td>
                                请选择资料类别
                            </td>
                            <td>
                                <select id="sec_type" runat="server">
                                </select>
                            </td>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;<input id="txt_file_name" type="text" />
                            </td>
                            <td>
                                <a id="preserve" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'"
                                    onclick="query()">查询</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table id="gridItem">
        </table>
    </div>
    <div id="dv_rename" data-options="iconCls:'icon-save'" style="padding: 5px; width: 300px;
        height: 150px; display:none">
                    <table style="font-size: 12px; display: block;width:250px">
                        <tr><td style="width:20%">新名称</td><td align="left"><input type="text"  id="txt_name" /></td></tr>
                        
                        <tr>
                            <td colspan="2" style="margin-left: 20px";valign="bottom" class="style1" align="center">
                                <input id="btnsure" type="button" value="确定" onclick="sure()" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <input type="button" id="QX" value="取消" onclick="QX(1)"/>
                            </td>
                        </tr>
                    </table>
            </div>
            <div id="div_update" data-options="iconCls:'icon-save'" style="padding: 5px; width: 300px;
        height: 150px; display:none">
                    <table style="font-size: 12px; display: block;width:300px">
                    <tr id="tr_display"><td>文件类型</td><td align="left"><select id="sec_data_type" runat="server">
                                </select></td></tr>
                        <tr><td style="width:20%">文件名</td><td align="left"><input type="file" id="fp_value" runat="server" /></td></tr>
                        
                        <tr>
                            <td colspan="2" style="margin-left: 20px";valign="bottom" class="style1" align="center">
                                <input id="btn_update" type="button" value="确定" onclick="update()" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <input type="button" id="btn_cancel" value="取消" onclick="cancel()"/>
                            </td>
                        </tr>
                    </table>
            </div>
            <div id="div_down" data-options="iconCls:'icon-save'" style="padding: 5px; width: 300px;
        height: 150px; display:none">
                    <table style="font-size: 12px; display: block;width:300px">
                   

                        <tr><td ><input type="text" id="txt_liulan" runat="server" /></td><td><input id="btn_sure_down" type="button" value="浏览..." onclick="browseFolder()" /></td></tr>
                        
                    </table>
            </div>
    <asp:HiddenField ID="hf_value" runat="server" />
    </form>
</body>
</html>
