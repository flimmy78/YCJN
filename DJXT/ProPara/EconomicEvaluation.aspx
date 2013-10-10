<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EconomicEvaluation.aspx.cs"
    Inherits="DJXT.ProPara.EconomicEvaluation" %>

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
    <style type="text/css">
        body
        {
            font-family: 宋体, Helvetica, sans-serif;
            font-size: 12px;
            background: #dfe8f6;
        }
        #menu
        {
            <%--border: 1px solid #2a88bb;--%>
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


        function query() {
            if ($("#sec_crew").find("option:selected").val() == "-请选择-") {
                alert("请选择机组！");
                $("#txt_D_Alpha_fh").attr("value", "");
                $("#txt_D_Alpha_lz").attr("value", "");
                $("#txt_D_D_e").attr("value", "");
                $("#txt_D_CO").attr("value", "");
                $("#txt_D_Tlz").attr("value", "");
                $("#txt_D_Tlk_d").attr("value", "");
                $("#txt_D_RH").attr("value", "");
                $("#txt_D_Tfw_d").attr("value", "");
                $("#txt_D_H2").attr("value", "");
                $("#txt_D_CH4").attr("value", "");
            }
            else {
                var flag = true;
                //                $("input[type='text']").each(function () {
                //                    if ($(this).val() == "") {
                //                        alert("不能为空！");
                //                        flag = false;
                //                        return false;
                //                    }
                //                })
                if (flag == true) {

                    $("#txt_D_Alpha_fh").attr("value", "");
                    $("#txt_D_Alpha_lz").attr("value", "");
                    $("#txt_D_D_e").attr("value", "");
                    $("#txt_D_CO").attr("value", "");
                    $("#txt_D_Tlz").attr("value", "");
                    $("#txt_D_Tlk_d").attr("value", "");
                    $("#txt_D_RH").attr("value", "");
                    $("#txt_D_Tfw_d").attr("value", "");
                    $("#txt_D_H2").attr("value", "");
                    $("#txt_D_CH4").attr("value", "");


                    var par = $("#sec_crew").find("option:selected").val();
                    $.post(
                    "../DataFile/GetBoilerCalculation.aspx",
                    {
                        Boiler: par
                    },
                function (data) {
                    var array = new Array();
                    if (data != "") {
                        array = data.split(',');
                        $("#txt_D_Alpha_fh").attr("value", array[0]);
                        $("#txt_D_Alpha_lz").attr("value", array[1]);
                        $("#txt_D_D_e").attr("value", array[2]);
                        $("#txt_D_CO").attr("value", array[3]);
                        $("#txt_D_Tlz").attr("value", array[4]);
                        $("#txt_D_Tlk_d").attr("value", array[5]);
                        $("#txt_D_RH").attr("value", array[6]);
                        $("#txt_D_Tfw_d").attr("value", array[7]);
                        $("#txt_D_H2").attr("value", array[8]);
                        $("#txt_D_CH4").attr("value", array[9]);
                    }
                },

                "html");
                }
            }
        }

        function save() {
            if ($("#sec_crew").find("option:selected").val() == "-请选择-") {
                alert("请选择机组！");
            }
            else {
                var flag = true;
                $("input[type='text']").each(function () {
                    if ($(this).val() == "") {
                        alert("值不能为空！");
                        flag = false;
                        return false;
                    }
                })
                if (flag == true) {
                    var par = par = "'" + $("#sec_crew").find("option:selected").val() + "'," + $("#txt_D_Alpha_fh").val() + "," + $("#txt_D_Alpha_lz").val() + "," + $("#txt_D_D_e").val() + ","
             + $("#txt_D_CO").val() + "," + $("#txt_D_Tlz").val() + "," + $("#txt_D_Tlk_d").val() + "," + $("#txt_D_RH").val() + ","
              + $("#txt_D_Tfw_d").val() + "," + $("#txt_D_H2").val() + "," + $("#txt_D_CH4").val();
                    $.post(
                    "../DataFile/GetBoilerCalculation.aspx",
                    {
                        Boiler_insert: par
                    },
                function (data) {
                    if (data == -1) {
                        alert("数据保存成功！");
                    }
                },

                "html");
                }
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
                    &nbsp;&nbsp;经济性评价标准
                </td>
            </tr>
            <tr>
                <td class="style5" colspan="2">
                    <table>
                        <tr>
                            <td>
                                省公司&nbsp;
                            </td>
                            <td>
                                <select id="sec_company" runat="server" onchange="change_company()">
                                </select>&nbsp;
                            </td>
                            <td>
                                电厂&nbsp;
                            </td>
                            <td>
                                <select id="sec_electric" runat="server" onchange="change_electric()">
                                </select>&nbsp;
                            </td>
                            <td>
                                机组&nbsp;
                            </td>
                            <td>
                                <select id="sec_crew" runat="server">
                                </select>
                            </td>
                            <td>
                                <a id="btn_query" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'">
                                    查询</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" height="92%" cellpadding="0" cellspacing="0" class="style5" style="border: 1px solid #2a88bb;">
                        <tr>
                            <td style="font-size: 16px; color: #48317C; font-weight: bold;" align="center">
                                热耗率性能
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="font-size: 16px">
                                公式：&nbsp;&nbsp;y&nbsp;=&nbsp;&nbsp;<input type="text" />&nbsp;&nbsp;X2&nbsp;+&nbsp;&nbsp;<input
                                    type="text" />&nbsp;X&nbsp;+&nbsp;&nbsp;<input type="text" />
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 16px">
                                经济性评价标准：&nbsp;&nbsp;优秀&nbsp;<&nbsp;<input type="text" />&nbsp;良好&nbsp;<&nbsp;<input
                                    type="text" />&nbsp;合格&nbsp;<&nbsp;<input type="text" />&nbsp;不合格
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height:24px"><td></td></tr>
            <tr>
                <td>
                    <table width="100%" height="92%" cellpadding="0" cellspacing="0" class="style5" style="border: 1px solid #2a88bb;">
                        <tr>
                            <td style="font-size: 16px; color: #48317C; font-weight: bold;" align="center">
                                锅炉效率性能
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="font-size: 16px">
                                公式：&nbsp;&nbsp;y&nbsp;=&nbsp;&nbsp;<input type="text" />&nbsp;&nbsp;X2&nbsp;+&nbsp;&nbsp;<input
                                    type="text" />&nbsp;X&nbsp;+&nbsp;&nbsp;<input type="text" />
                            </td>
                        </tr>
                        <tr align="center">
                            <td style="font-size: 16px">
                                经济性评价标准：&nbsp;&nbsp;优秀&nbsp;<&nbsp;<input type="text" />&nbsp;良好&nbsp;<&nbsp;<input
                                    type="text" />&nbsp;合格&nbsp;<&nbsp;<input type="text" />&nbsp;不合格
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <a id="btn_save" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'">
                        保存</a>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
