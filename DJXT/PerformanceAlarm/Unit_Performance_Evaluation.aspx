<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Unit_Performance_Evaluation.aspx.cs"
    Inherits="DJXT.PerformanceAlarm.Unit_Performance_Evaluation" %>

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
        $(document).ready(function () {
            $("#QX").live("click", function () {
                $.unblockUI();
            });

            $('#addclose a').live("click", function () {

                $.unblockUI();
            });
        });

        function change_company() {
            var par = $("#sec_company").find("option:selected").val();
            if (par != "-请选择-") {
                $.post(
        "../datafile/GetProductionProPara.aspx",
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
                    "../DataFile/GetProductionProPara.aspx",
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
        function change_click(b, a) {
            var click_id;
            if (a == 1) {
                click_id = $("#cbl_yjlb :checkbox:checked");
            }
            if (click_id.length > 1) {
                for (var i = 0; i < click_id.length; i++) {
                }
            }
        }

        function change_shou() {
            if ($("#btn_shou").val() == "收起") {
                $("#btn_shou").attr("value", "更多选择");
                document.getElementById("div_shouqi").style.display = "none";
            }
            else {
                $("#btn_shou").attr("value", "收起");
                document.getElementById("div_shouqi").style.display = "block";
            }
        }

        function query() {
            if (($("#txt_stime").val() == "") || ($("#txt_etime").val() == "")) {
                alert("时间不能为空！");
            }
            else if ($("#sec_crew  option:selected").val() == "-请选择-") {
                alert("请选择机组！");
            }
            else {
                var rating = "";
                rating += $("#txt_stime").val() + ";" + $("#txt_etime").val() + "," + $("#sec_crew  option:selected").val() + "," + $("#txt_dianliang").val() + ",";

                rating += $('table[id="cbl_yjlb"] input:checked').val() + "," + $('table[id="cbl_yjxz"] input:checked').val() + "," + $('table[id="cbl_yjzyfl"] input:checked').val() + "," + $('table[id="cbl_yjyyfl"] input:checked').val() + ",";
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
                url: 'Unit_Performance_Evaluation.aspx',
                sortName: 'T_COMPANYDESC',
                sortOrder: 'asc',
                remoteSort: false,
                queryParams: { rating: total_data },
                idField: 'T_COMPANYDESC',
                columns: [[
                    { field: 'ID_KTEY', title: 'ID_KTEY', width: 120, hidden: true },
                    { field: '事件描述', title: '事件描述', width: 120, hidden: true },
                    { field: '原因分析', title: '原因分析', width: 120, hidden: true },
                    { field: '处理建议', title: '处理建议', width: 120, hidden: true },
				    { field: 'T_COMPANYDESC', title: '省公司', width: $(window).width() * 0.1 * 0.98, align: 'center' },
                    { field: 'T_PLANTDESC', title: '电厂名称', width: $(window).width() * 0.1 * 0.98, align: 'center' },
                    { field: 'T_UNITDESC', title: '机组号', width: $(window).width() * 0.1 * 0.98, align: 'center' },
                    { field: 'T_DESC', title: '预警点类型', width: $(window).width() * 0.1 * 0.98, align: 'center' },
                    { field: '开始时间', title: '开始时间', width: $(window).width() * 0.1 * 0.98, align: 'center' },
                    { field: '结束时间', title: '结束时间', width: $(window).width() * 0.1 * 0.98, align: 'center' },
                    { field: 'T_CAPABILITYLEVEL', title: '铭牌容量MW', width: $(window).width() * 0.1 * 0.98, align: 'center' },
                    { field: 'T_CATEGORYDESC', title: '预警类别', width: $(window).width() * 0.1 * 0.98, align: 'center' },
                    { field: 'T_PROPERTYDESC', title: '预警性质', width: $(window).width() * 0.1 * 0.98, align: 'center' },
                    { field: 'T_PROFESSIONALDESC', title: '预警专业分类', width: $(window).width() * 0.1 * 0.98, align: 'center' },
                    { field: 'T_REASONDESC', title: '预警原因分类', width: $(window).width() * 0.1 * 0.98, align: 'center' },
                    { field: '影响电量', title: '影响电量', width: $(window).width() * 0.1 * 0.98, align: 'center' },
                    { field: '编辑', title: '编辑', width: $(window).width() * 0.1 * 0.98, align: 'center',
                        formatter: function (value, row, index) {

                            var d = '<a href="#" onclick="editrow(' + index + ')">编辑</a>';
                            return d;
                        }
                    },
                    { field: '详细说明', title: '详细说明', width: $(window).width() * 0.1 * 0.98, align: 'center',
                        formatter: function (value, row, index) {
                            var c = '<a href="#" onclick="lookrow(' + index + ')">查看</a>';
                            return c;
                        }
                    }
                ]],
                pagination: true,
                rownumbers: true
            });
        }
        function lookrow(index) {
            $.blockUI({
                message: $("#look"),
                baseZ: "1000",
                css: { width: "500px", height: "350px", top: "10%", left: "30%" },
                target: "#form1"

            });
            var rows = $("#gridItem").datagrid("getRows");
            var rating = "事件描述：" + rows[index].事件描述 + "\n原因分析：" + rows[index].原因分析 + "\n处理建议：" + rows[index].处理建议
            $("#txt_xiangxi").attr("value", rating);
        }
        function editrow(index) {
            
            $.blockUI({
                message: $("#edit"),
                baseZ: "1000",
                css: { width: "500px", height: "350px", top: "10%", left: "30%" },
                target: "#form1"

            });
            var rows = $("#gridItem").datagrid("getRows");
            $("#txt_time").attr("value", rows[index].开始时间);
            $("#txt_shijian").attr("value", rows[index].事件描述);
            $("#txt_yuanyin").attr("value", rows[index].原因分析);
            $("#txt_chuli").attr("value", rows[index].处理建议);
            $("#hf_value").attr("value", rows[index].ID_KTEY);
            $("#hf_row_value").attr("value", index);
            
        }

        function sure() {
            var rating = $("#hf_value").val() + "," + $("#txt_shijian").val() + "," + $("#txt_yuanyin").val() + "," + $("#txt_chuli").val()  ;
            $.post("Unit_Performance_Evaluation.aspx", { para_edit: rating }, function (data) {
                alert(data);
            }, 'json');
            $('#gridItem').datagrid('reload'); 
            $.unblockUI();
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
                    &nbsp;&nbsp;机组性能评估
                </td>
            </tr>
            <tr>
                <td class="style5">
                    <table>
                        <tr>
                            <td>
                                查询时间段&nbsp;
                            </td>
                            <td>
                                <input class="Wdate" id="txt_stime" runat="server" type="text" readonly="readonly"
                                    onclick="WdatePicker({dateFmt:'yyyy-MM',maxDate:'%y-%M-%d'})" />
                            </td>
                            <td>
                                至
                            </td>
                            <td>
                                <input class="Wdate" id="txt_etime" runat="server" type="text" readonly="readonly"
                                    onclick="WdatePicker({dateFmt:'yyyy-MM',maxDate:'%y-%M-%d'})" />
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
                                预警影响电量大小
                            </td>
                            <td>
                                <input type="text" id="txt_dianliang" />
                            </td>
                            <td>
                                万kW.h
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
                                <input type="button" id="btn_query" value="查询" onclick="query()" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <input type="button" id="btn_reset" value="重置" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <input type="button" id="btn_shou" value="收起" onclick="change_shou()" />
                                <%--<a id="preserve" href="#" class="easyui-linkbutton"
                                data-options="iconCls:'icon-search'">查询</a>&nbsp;&nbsp;&nbsp;&nbsp;
                                <a id="A1" href="#" class="easyui-linkbutton"
                                data-options="iconCls:'icon-reload'">重置</a>&nbsp;&nbsp;&nbsp;&nbsp;
                                <a id="A2" href="#" class="easyui-linkbutton"
                                data-options="iconCls:'icon-search'">查询</a>--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style5" id="div_shouqi">
                    <table>
                        <tr>
                            <td>
                                预警类别：
                            </td>
                            <td>
                                <asp:RadioButtonList ID="cbl_yjlb" runat="server" RepeatDirection="Horizontal">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                预警性质：
                            </td>
                            <td>
                                <asp:RadioButtonList ID="cbl_yjxz" runat="server" RepeatDirection="Horizontal">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                预警专业分类：
                            </td>
                            <td>
                                <asp:RadioButtonList ID="cbl_yjzyfl" runat="server" RepeatDirection="Horizontal">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                预警原因分类：
                            </td>
                            <td>
                                <asp:RadioButtonList ID="cbl_yjyyfl" runat="server" RepeatDirection="Horizontal">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
                <div id="edit" style="display: none; height: 350px; width:550px">
                <div style="font-size: 12px; background: url('../img/content-bg.gif') repeat-x;
                    height: 31px;">
                    <span style="float: left; height: 31px; line-height: 31px; padding-left: 10px"><strong>
                        编辑</strong></span>
                    <div id="addclose" style="float: right; font-size: 12px; padding: 2px;">
                        <a href="#" title="关闭">关闭</a></div>
                </div>
                <div style="margin-left: 10px; height: 250px; margin-top:50px">
                    <table style="font-size: 12px; display: block;width:500px">
                        <tr><td style="width:20%">预警时间：</td><td align="left"><input type="text" readonly="readonly" id="txt_time" /></td></tr>
                        <tr><td style="width:10%">事件描述：</td><td style="width:90%" align="left">
                            <asp:TextBox ID="txt_shijian" runat="server" TextMode="MultiLine" Width="90%"></asp:TextBox></td></tr>
                        <tr><td style="width:10%">原因分析：</td><td style="width:90%" align="left">
                            <asp:TextBox ID="txt_yuanyin" runat="server" TextMode="MultiLine" Width="90%"></asp:TextBox></td></tr>
                        <tr><td style="width:10%">处理建议：</td><td style="width:90%" align="left">
                            <asp:TextBox ID="txt_chuli" runat="server" TextMode="MultiLine" Width="90%"></asp:TextBox></td></tr>
                        <tr>
                            <td colspan="2" style="margin-left: 20px";valign="bottom" class="style1" align="center">
                                <input id="btnsure" type="button" value="确定" onclick="sure()" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <input type="button" id="QX" value="取消" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div id="look" style="display: none; height: 350px; width:550px">
                <div style="font-size: 12px; background: url('../img/content-bg.gif') repeat-x;
                    height: 31px;">
                    <span style="float: left; height: 31px; line-height: 31px; padding-left: 10px"><strong>
                        详细情况</strong></span>
                    <div id="addclose" style="float: right; font-size: 12px; padding: 2px;">
                        <a href="#" title="关闭">关闭</a></div>
                </div>
                <div style="margin-left: 10px; height: 250px; margin-top:50px">
                    <table style="font-size: 12px; display: block;width:500px">
                        <tr><td align="left">详细情况：</td></tr>
                        <tr>
                        <td><asp:TextBox ID="txt_xiangxi" runat="server" TextMode="MultiLine" Width="100%" Height="300px"></asp:TextBox></td>
                            </tr>
                        
                        
                    </table>
                </div>
            </div>
    <div>
        <table id="gridItem">
        </table>
    </div>
    <asp:HiddenField ID="hf_value" runat="server" />
    <asp:HiddenField ID="hf_row_value" runat="server" />
    </form>
</body>
</html>
