<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WarningThreshold.aspx.cs"
    Inherits="DJXT.PerformanceAlarm.WarningThreshold" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/css/djxt.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../js/ProPara/ProductionProPara.js" type="text/javascript"></script>
    <link href="../js/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <script src="../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../js/ProPara/Excel.js" type="text/javascript"></script>
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
        function change_sec_crew() {
            var par = $("#sec_crew").find("option:selected").val();
            if (par != "-请选择-") {
                $.post(
                    "../DataFile/GETWarningThresholdData.aspx",
                    {
                        sec_crew_real: par
                    },
                function (data) {
                    var array = new Array();
                    array = data.split('|');
                    $("#sec_yujing").empty();
                    if (data == "") {
                        $("#sec_yujing").append("<option value=请选择>-请选择-</option>");
                    }
                    else {
                        $("#sec_yujing").append(array[0]);
                    }
                },

                "html");
            }
            else {
                $("#sec_yujing").empty();
                $("#sec_yujing").append("<option value=请选择>-请选择-</option>");
            }
        }
        function query() {
            if ($("#sec_crew  option:selected").val() == "-请选择-") {
                alert("请选择机组！");
            }
            else {
                var rating = $("#sec_crew  option:selected").val();
                GridSta(rating);
            }
        }

        function GridSta(total_data) {
            $('#gridItem').datagrid({

                nowrap: true,
                autoRowHeight: false,
                fitColumns: true,
                striped: true,
                align: 'center',
                loadMsg: "正在努力为您加载数据", //加载数据时向用户展示的语句
                collapsible: true,
                url: 'WarningThreshold.aspx',
                sortName: '考核点描述',
                sortOrder: 'asc',
                remoteSort: false,
                queryParams: { rating: total_data },
                idField: '考核点描述',
                columns: [[
                    { field: 'ID', title: 'ID', width: 120, hidden: true },
                    { field: '故障类型ID', title: '故障类型ID', width: 120, hidden: true },
				    { field: '考核点描述', title: '考核点描述', width: $(window).width() * 0.1 * 0.98, align: 'center' },
                    { field: '考核上限', title: '考核上限', width: $(window).width() * 0.1 * 0.98, align: 'center', editor: { type: 'numberbox', options: { precision: 1}} },
                    { field: '考核下限', title: '考核下限', width: $(window).width() * 0.1 * 0.98, align: 'center', editor: { type: 'numberbox', options: { precision: 1}} },
                    { field: '提示信息', title: '提示信息', width: $(window).width() * 0.1 * 0.98, align: 'center', editor: 'text' },
                    { field: '机组', title: '机组', width: $(window).width() * 0.1 * 0.98, align: 'center' },
                    { field: '操作', title: '操作', width: $(window).width() * 0.1 * 0.98, align: 'center',
                        formatter: function (value, row, index) {
                            if (row.editing) {
                                var s = '<a href="#" onclick="saverow(' + index+ ')">保存</a> ';
                                var c = '<a href="#" onclick="cancelrow(' + index + ')">取消</a>';
                                return s + c;
                            } else {
                                var e = '<a href="#" onclick="editrow(' + index + ')">编辑</a> ';
                                var d = '<a href="#" onclick="deleterow(' + index + ')">删除</a>';
                                return e + d;
                            }
                        }
                    }
                ]],
                onBeforeEdit:function(index,row){
        row.editing = true;
        $('#gridItem').datagrid('refreshRow', index);
    },
    onAfterEdit:function(index,row){
        row.editing = false;
        $('#gridItem').datagrid('refreshRow', index);
    },
    onCancelEdit:function(index,row){
        row.editing = false;
        $('#gridItem').datagrid('refreshRow', index);
    },

                pagination: true,
                rownumbers: true
            });
        }
        function editrow(index) {
            $('#gridItem').datagrid('beginEdit', index);

        }
        function deleterow(index) {
            $.messager.confirm('Confirm', 'Are you sure?', function (r) {
                if (r) {
                    $('#gridItem').datagrid('deleteRow', index);
                    var rows = $("#gridItem").datagrid("getRows"); // 这段代码是获取当前页的所有行。
                    var rating = rows[index].ID;
                    $.post("WarningThreshold.aspx", { para_delete: rating }, function (data) {

                    }, 'json');
                }
            });
        }
        function saverow(index) {

            $('#gridItem').datagrid('endEdit', index);
            var rows = $("#gridItem").datagrid("getRows"); // 这段代码是获取当前页的所有行。
            var rating = rows[index].考核上限 + "," + rows[index].考核下限 + "," + rows[index].提示信息 + "," + rows[index].故障类型ID;
            $.post("WarningThreshold.aspx", { para_save: rating }, function (data) {
                
            }, 'json');
            
        }
        function cancelrow(index) {
            $('#gridItem').datagrid('cancelEdit', index);
        }


        function add() {

            if ($("#sec_crew  option:selected").val() == "-请选择-") {
                alert("请选择机组！");
            }
            else if ($("#sec_yujing  option:selected").val() == "-请选择-") {
                alert("请选择预警对象！");
            }
            else {
                var rating = $("#txt_shang").val() + "," + $("#txt_xia").val() + "," + $("#txt_message").val() + "," + $("#sec_yujing  option:selected").val();
                $.post("WarningThreshold.aspx", { para: rating }, function (data) {
                    alert("添加成功！");

                }, 'json');
                GridSta($("#sec_crew  option:selected").val());
            } 
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="menu">
        <table width="100%" height="92%" cellpadding="4" cellspacing="0" class="style5" border="0">
            <tr>
                <td background="../img/table-head.jpg" height="30px" valign="middle" class="style6"
                    colspan="2">
                    &nbsp;&nbsp;预警阀值设置
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
                                    <select id="sec_crew" runat="server" onchange="change_sec_crew()">
                                        <option>-请选择-</option>
                                    </select>
                                </span>
                            </td>
                            <td>
                                <a id="preserve" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'"
                                    onclick="query()">查询</a>
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
                                预警对象&nbsp;
                            </td>
                            <td>
                                <select id="sec_yujing" runat="server">
                                    <option>-请选择-</option>
                                </select>&nbsp;
                            </td>
                            <td>
                                预警上限
                            </td>
                            <td>
                                <input id="txt_shang" type="text" style="width: 50px" />&nbsp;
                            </td>
                            <td>
                                预警下限
                            </td>
                            <td>
                                <input id="txt_xia" type="text" style="width: 50px" />&nbsp;
                            </td>
                            <td>
                                提示信息
                            </td>
                            <td>
                                <input id="txt_message" type="text" />&nbsp;
                            </td>
                            <td>
                                <a id="add" href="#" class="easyui-linkbutton" onclick="add()">添加</a>
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
    </form>
</body>
</html>
