<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SteamTurbine.aspx.cs" Inherits="DJXT.ProPara.SteamTurbine" %>

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
                $("#txt_D_L1").attr("value", "");
                $("#txt_D_L2").attr("value", "");
                $("#txt_D_Delta_dtr").attr("value", "");
                $("#txt_D_Rdtr").attr("value", "");
                $("#txt_D_L_well").attr("value", "");
                $("#txt_D_W_well").attr("value", "");
                $("#txt_D_Eta_p").attr("value", "");
                $("#txt_D_Delta_fdj").attr("value", "");
                $("#txt_D_Delta_gcb").attr("value", "");
                $("#txt_D_Delta_qbb").attr("value", "");
                $("#txt_D_DK_e").attr("value", "");
                $("#txt_D_DB_e").attr("value", "");
                $("#txt_D_DM_e").attr("value", "");
                $("#txt_D_DL_e").attr("value", "");
                $("#txt_D_P_e").attr("value", "");
                $("#txt_D_D_gbmfgs").attr("value", "");
                $("#txt_D_D_gbmfhs").attr("value", "");
                $("#txt_D_Dphp_e").attr("value", "");
                $("#txt_D_DN_e").attr("value", "");
                $("#txt_D_A").attr("value", "");
                $("#txt_I_N_pipe").attr("value", "");
                $("#txt_I_N_flow").attr("value", "");
                $("#txt_D_Din").attr("value", "");
                $("#txt_D_Dout").attr("value", "");
                $("#txt_I_N_ball_i").attr("value", "");
                $("#txt_D_Txhs_in_d").attr("value", "");
                $("#txt_D_Wd").attr("value", "");
                $("#txt_D_Din_xb1O").attr("value", "");
                $("#txt_D_Din_xb2O").attr("value", "");
                $("#sec_T_Type option")[5].selected = true
                $("#txt_I_N_ball_o").attr("value", "");
                $("#txt_D_Djc_e").attr("value", "");
                $("#txt_D_V_xb1I").attr("value", "");
                $("#txt_D_V_xb2I").attr("value", "");
                $("#txt_D_Z_xb1I").attr("value", "");
                $("#txt_D_Z_xb2I").attr("value", "");
                $("#txt_D_Z_xb1O").attr("value", "");
                $("#txt_D_Z_xb2O").attr("value", "");
                $("#txt_D_Eta_gr_xb1").attr("value", "");
                $("#txt_D_Eta_gr_xb2").attr("value", "");
                $("#txt_D_W_lqt_I").attr("value", "");
            }
            else {

                $("#txt_D_L1").attr("value", "");
                $("#txt_D_L2").attr("value", "");
                $("#txt_D_Delta_dtr").attr("value", "");
                $("#txt_D_Rdtr").attr("value", "");
                $("#txt_D_L_well").attr("value", "");
                $("#txt_D_W_well").attr("value", "");
                $("#txt_D_Eta_p").attr("value", "");
                $("#txt_D_Delta_fdj").attr("value", "");
                $("#txt_D_Delta_gcb").attr("value", "");
                $("#txt_D_Delta_qbb").attr("value", "");
                $("#txt_D_DK_e").attr("value", "");
                $("#txt_D_DB_e").attr("value", "");
                $("#txt_D_DM_e").attr("value", "");
                $("#txt_D_DL_e").attr("value", "");
                $("#txt_D_P_e").attr("value", "");
                $("#txt_D_D_gbmfgs").attr("value", "");
                $("#txt_D_D_gbmfhs").attr("value", "");
                $("#txt_D_Dphp_e").attr("value", "");
                $("#txt_D_DN_e").attr("value", "");
                $("#txt_D_A").attr("value", "");
                $("#txt_I_N_pipe").attr("value", "");
                $("#txt_I_N_flow").attr("value", "");
                $("#txt_D_Din").attr("value", "");
                $("#txt_D_Dout").attr("value", "");
                $("#txt_I_N_ball_i").attr("value", "");
                $("#txt_D_Txhs_in_d").attr("value", "");
                $("#txt_D_Wd").attr("value", "");
                $("#txt_D_Din_xb1O").attr("value", "");
                $("#txt_D_Din_xb2O").attr("value", "");
                $("#sec_T_Type option")[5].selected = true
                $("#txt_I_N_ball_o").attr("value", "");
                $("#txt_D_Djc_e").attr("value", "");
                $("#txt_D_V_xb1I").attr("value", "");
                $("#txt_D_V_xb2I").attr("value", "");
                $("#txt_D_Z_xb1I").attr("value", "");
                $("#txt_D_Z_xb2I").attr("value", "");
                $("#txt_D_Z_xb1O").attr("value", "");
                $("#txt_D_Z_xb2O").attr("value", "");
                $("#txt_D_Eta_gr_xb1").attr("value", "");
                $("#txt_D_Eta_gr_xb2").attr("value", "");
                $("#txt_D_W_lqt_I").attr("value", "");

                var par = $("#sec_crew").find("option:selected").val();
                $.post(
                    "../DataFile/GetSteamTurbine.aspx",
                    {
                        Steam: par
                    },
                function (data) {
                    var array = new Array();
                    if (data != "") {
                        array = data.split(',');

                        $("#txt_D_L1").attr("value", array[0]);
                        $("#txt_D_L2").attr("value", array[1]);
                        $("#txt_D_Delta_dtr").attr("value", array[2]);
                        $("#txt_D_Rdtr").attr("value", array[3]);
                        $("#txt_D_L_well").attr("value", array[4]);
                        $("#txt_D_W_well").attr("value", array[5]);
                        $("#txt_D_Eta_p").attr("value", array[6]);
                        $("#txt_D_Delta_fdj").attr("value", array[7]);
                        $("#txt_D_Delta_gcb").attr("value", array[8]);
                        $("#txt_D_Delta_qbb").attr("value", array[9]);
                        $("#txt_D_DK_e").attr("value", array[10]);
                        $("#txt_D_DB_e").attr("value", array[11]);
                        $("#txt_D_DM_e").attr("value", array[12]);
                        $("#txt_D_DL_e").attr("value", array[13]);
                        $("#txt_D_P_e").attr("value", array[14]);
                        $("#txt_D_D_gbmfgs").attr("value", array[15]);
                        $("#txt_D_D_gbmfhs").attr("value", array[16]);
                        $("#txt_D_Dphp_e").attr("value", array[17]);
                        $("#txt_D_DN_e").attr("value", array[18]);
                        $("#txt_D_Djc_e").attr("value", array[19]);
                        $("#txt_D_A").attr("value", array[20]);
                        $("#txt_I_N_pipe").attr("value", array[21]);
                        $("#txt_I_N_flow").attr("value", array[22]);
                        $("#txt_D_Din").attr("value", array[23]);
                        $("#txt_D_Dout").attr("value", array[24]);
                        $("#txt_I_N_ball_i").attr("value", array[25]);
                        $("#txt_D_V_xb1I").attr("value", array[26]);
                        $("#txt_D_V_xb2I").attr("value", array[27]);
                        $("#txt_D_Z_xb1I").attr("value", array[28]);
                        $("#txt_D_Z_xb2I").attr("value", array[29]);
                        $("#txt_D_W_lqt_I").attr("value", array[30]);
                        $("#txt_D_Txhs_in_d").attr("value", array[31]);
                        $("#txt_D_Wd").attr("value", array[32]);
                        $("#txt_D_Din_xb1O").attr("value", array[33]);
                        $("#txt_D_Din_xb2O").attr("value", array[34]);

                        $("#sec_T_Type option").each(function () {
                            //alert($(this).text());
                            if ($(this).text() == array[35]) {
                                $(this).attr("selected", "selected");
                            }
                        });
                        $("#txt_I_N_ball_o").attr("value", array[36]);

                        $("#txt_D_Z_xb1O").attr("value", array[37]);
                        $("#txt_D_Z_xb2O").attr("value", array[38]);
                        $("#txt_D_Eta_gr_xb1").attr("value", array[39]);
                        $("#txt_D_Eta_gr_xb2").attr("value", array[40]);
                    }
                },

                "html");
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
                    var par = par = "'" + $("#sec_crew").find("option:selected").val() + "'," + $("#txt_D_L1").val() + "," + $("#txt_D_L2").val() + "," + $("#txt_D_Delta_dtr").val() + ","
              + $("#txt_D_Rdtr").val() + "," + $("#txt_D_L_well").val() + "," + $("#txt_D_W_well").val() + "," + $("#txt_D_Eta_p").val() + ","
              + $("#txt_D_Delta_fdj").val() + "," + $("#txt_D_Delta_gcb").val() + "," + $("#txt_D_Delta_qbb").val() + ","
              + $("#txt_D_DK_e").val() + "," + $("#txt_D_DB_e").val() + "," + $("#txt_D_DM_e").val() + ","
              + $("#txt_D_DL_e").val() + "," + $("#txt_D_P_e").val() + "," + $("#txt_D_D_gbmfgs").val() + ","
              + $("#txt_D_D_gbmfhs").val() + "," + $("#txt_D_Dphp_e").val() + "," + $("#txt_D_DN_e").val() + "," + $("#txt_D_Djc_e").val() + ","
              + $("#txt_D_A").val() + "," + $("#txt_I_N_pipe").val() + "," + $("#txt_I_N_flow").val() + ","
              + $("#txt_D_Din").val() + "," + $("#txt_D_Dout").val() + "," + $("#txt_I_N_ball_i").val() + ","
              + $("#txt_D_V_xb1I").val() + "," + $("#txt_D_V_xb2I").val() + "," + $("#txt_D_Z_xb1I").val() + "," + $("#txt_D_Z_xb2I").val() + ","
              + $("#txt_D_W_lqt_I").val() + "," + $("#txt_D_Txhs_in_d").val() + "," + $("#txt_D_Wd").val() + "," + $("#txt_D_Din_xb1O").val() + ","
              + $("#txt_D_Din_xb2O").val() + ",'" + $("#sec_T_Type").find("option:selected").val() + "'," + $("#txt_I_N_ball_o").val() + ","
              + $("#txt_D_Z_xb1O").val() + "," + $("#txt_D_Z_xb2O").val() + "," + $("#txt_D_Eta_gr_xb1").val() + "," + $("#txt_D_Eta_gr_xb2").val();
                   // alert(par);
                    $.post(
                    "../DataFile/GetSteamTurbine.aspx",
                    {
                        Steam_insert: par
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
                    &nbsp;&nbsp;主辅机设计参数-汽机计算所需
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
                                    <option>-请选择-</option>
                                </select>&nbsp;
                            </td>
                            <td>
                                机组&nbsp;
                            </td>
                            <td>
                                <select id="sec_crew" runat="server">
                                    <option>-请选择-</option>
                                </select>
                            </td>
                            <td>
                                <a id="btn_query" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'"
                                    onclick="query()">查询</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr style="color: #4897DC" />
                </td>
            </tr>
            <tr>
                <td style="font-size: 16px; color: #48317C; font-weight: bold;" align="center" colspan="2">
                    汽机热力计算所需
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr style="color: #4897DC;" />
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold;">
                            <td style="width: 50%; font-size: 14px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;除氧器长度1
                            </td>
                            <td style="width: 30%">
                                <input type="text" id="txt_D_L1" />
                            </td>
                            <td style="width: 20%; font-size: 14px;">
                                m
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;">
                            <td style="width: 30%; font-size: 14px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;除氧器长度2
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_L2" />
                            </td>
                            <td style="width: 30%; font-size: 14px;">
                                m
                            </td>
                        </tr>
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold;">
                            <td style="width: 30%; font-size: 14px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;除氧器壁厚
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_Delta_dtr" />
                            </td>
                            <td style="width: 30%; font-size: 14px;">
                                m
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;">
                            <td style="width: 30%; font-size: 14px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;除氧器半径
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_Rdtr" />
                            </td>
                            <td style="width: 30%; font-size: 14px;">
                                m
                            </td>
                        </tr>
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold; width: 30%">
                            <td style="width: 30%; font-size: 14px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;热井长度
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_L_well" />
                            </td>
                            <td style="width: 30%; font-size: 14px;">
                                m
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;">
                            <td style="width: 30%; font-size: 14px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;热井宽度
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_W_well" />
                            </td>
                            <td style="width: 30%; font-size: 14px;">
                                m
                            </td>
                        </tr>
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold;">
                            <td style="width: 30%; font-size: 14px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;管道效率
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_Eta_p" />
                            </td>
                            <td style="width: 30%; font-size: 14px;">
                                %
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;">
                            <td style="width: 30%; font-size: 14px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;发电量倍率
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_Delta_fdj" />
                            </td>
                            <td style="width: 30%; font-size: 14px;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold;">
                            <td style="width: 30%; font-size: 14px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;高厂变倍率
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_Delta_gcb" />
                            </td>
                            <td style="width: 30%; font-size: 14px;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;">
                            <td style="width: 30%; font-size: 14px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;启备变倍率
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_Delta_qbb" />
                            </td>
                            <td style="width: 30%; font-size: 14px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top">
                    <table width="100%">
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold; width: 45%">
                            <td style="width: 50%; font-size: 14px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;额定负荷K中压门杆漏汽至3号高加流量
                            </td>
                            <td style="width: 30%">
                                <input type="text" id="txt_D_DK_e" />
                            </td>
                            <td style="width: 20%; font-size: 14px;">
                                t/h
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;">
                            <td style="width: 40%; font-size: 14px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;B高压门杆二档漏汽至输加流量
                            </td>
                            <td style="width: 30%">
                                <input type="text" id="txt_D_DB_e" />
                            </td>
                            <td style="width: 30%; font-size: 14px;">
                                t/h
                            </td>
                        </tr>
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold;">
                            <td style="width: 40%; font-size: 14px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;M高排轴端漏汽至均压箱流量
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_DM_e" />
                            </td>
                            <td style="width: 30%; font-size: 14px;">
                                t/h
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;">
                            <td style="width: 40%; font-size: 14px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;L高排轴端漏汽至中排流量
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_DL_e" />
                            </td>
                            <td style="width: 30%; font-size: 14px;">
                                t/h&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 14px; color: #48317C; font-weight: bold; width: 40%">
                                &nbsp;&nbsp;&nbsp;&nbsp;额定负荷发电机有机功率
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_P_e" />
                            </td>
                            <td style="font-size: 14px; color: #48317C; font-weight: bold; width: 40%">
                                MW
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;">
                            <td style="width: 40%; font-size: 14px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;给水泵密封水供水流量
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_D_gbmfgs" />
                            </td>
                            <td style="width: 40%; font-size: 14px;">
                                t/h
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 14px; color: #48317C; font-weight: bold; width: 40%">
                                &nbsp;&nbsp;&nbsp;&nbsp;给水泵密封水回水流量
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_D_gbmfhs" />
                            </td>
                            <td style="font-size: 14px; color: #48317C; font-weight: bold; width: 30%">
                                t/h
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;">
                            <td style="width: 40%; font-size: 14px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;平衡盘漏汽量流量
                            </td>
                            <td style="width: 30%">
                                <input type="text" id="txt_D_Dphp_e" />
                            </td>
                            <td style="width: 30%; font-size: 14px;">
                                t/h
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 14px; color: #48317C; font-weight: bold; width: 40%">
                                &nbsp;&nbsp;&nbsp;&nbsp;N高排轴端漏汽至轴加流量
                            </td>
                            <td style="width: 30%">
                                <input type="text" id="txt_D_DN_e" />
                            </td>
                            <td style="font-size: 14px; color: #48317C; font-weight: bold; width: 30%">
                                t/h
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;">
                            <td style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;
                                width: 40%">
                                &nbsp;&nbsp;&nbsp;&nbsp;夹层漏汽至高排流量
                            </td>
                            <td style="width: 30%">
                                <input type="text" id="txt_D_Djc_e" />
                            </td>
                            <td style="font-size: 14px; color: #48317C; font-weight: bold; width: 30%">
                                t/h
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr style="color: #4897DC" />
                </td>
            </tr>
            <tr>
                <td style="font-size: 16px; color: #48317C; font-weight: bold;" align="center" colspan="2">
                    汽机冷端计算所需
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr style="color: #4897DC" />
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td style="font-size: 14px; color: #48317C; font-weight: bold; width: 50%">
                                &nbsp;&nbsp;&nbsp;&nbsp;凝汽器传热面积
                            </td>
                            <td style="width: 30%">
                                <input type="text" id="txt_D_A" />
                            </td>
                            <td style="font-size: 14px; color: #48317C; font-weight: bold; width: 20%">
                                `
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;">
                            <td style="font-size: 14px; width: 30%">
                                &nbsp;&nbsp;&nbsp;&nbsp;凝汽器冷却管数
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_I_N_pipe" />
                            </td>
                            <td style="font-size: 14px; width: 30%">
                                /
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 14px; color: #48317C; font-weight: bold; width: 30%">
                                &nbsp;&nbsp;&nbsp;&nbsp;凝汽器流程数
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_I_N_flow" />
                            </td>
                            <td style="font-size: 14px; color: #48317C; font-weight: bold; width: 30%">
                                /
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;">
                            <td style="font-size: 14px; width: 30%">
                                &nbsp;&nbsp;&nbsp;&nbsp;凝汽器冷却管内径
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_Din" />
                            </td>
                            <td style="font-size: 14px; width: 30%">
                                m
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 14px; color: #48317C; font-weight: bold; width: 30%">
                                &nbsp;&nbsp;&nbsp;&nbsp;凝汽器冷却管外径
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_Dout" />
                            </td>
                            <td style="font-size: 14px; color: #48317C; font-weight: bold; width: 30%">
                                m
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;">
                            <td style="font-size: 14px; width: 30%">
                                &nbsp;&nbsp;&nbsp;&nbsp;凝汽器胶球投球数
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_I_N_ball_i" />
                            </td>
                            <td style="font-size: 14px; width: 30%">
                                /
                            </td>
                        </tr>
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold;">
                            <td style="font-size: 14px; width: 30%">
                                &nbsp;&nbsp;&nbsp;&nbsp;A循环水泵进口流速
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_V_xb1I" />
                            </td>
                            <td style="font-size: 14px; width: 30%">
                                m/s
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;">
                            <td style="font-size: 14px; width: 30%">
                                &nbsp;&nbsp;&nbsp;&nbsp;B循环水泵进口流速
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_V_xb2I" />
                            </td>
                            <td style="font-size: 14px; width: 30%">
                                m/s</td>
                        </tr>
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold;">
                            <td style="font-size: 14px; width: 30%">
                                &nbsp;&nbsp;&nbsp;&nbsp;循泵A进口集水池水位
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_Z_xb1I" />
                            </td>
                            <td style="font-size: 14px; width: 30%">
                                m
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;">
                            <td style="font-size: 14px; width: 30%">
                                &nbsp;&nbsp;&nbsp;&nbsp;循泵B进口集水池水位
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_Z_xb2I" />
                            </td>
                            <td style="font-size: 14px; width: 30%">
                                m
                            </td>
                        </tr>
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold;">
                            <td style="font-size: 14px; width: 30%">
                                &nbsp;&nbsp;&nbsp;&nbsp;冷却塔进水流量
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_W_lqt_I" />
                            </td>
                            <td style="font-size: 14px; width: 30%">
                                t/h
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td style="font-size: 14px; color: #48317C; font-weight: bold; width: 50%">
                                &nbsp;&nbsp;&nbsp;&nbsp;设计工况下的循环冷却水进口温度
                            </td>
                            <td style="width: 30%">
                                <input type="text" id="txt_D_Txhs_in_d" />
                            </td>
                            <td style="font-size: 14px; color: #48317C; font-weight: bold; width: 20%">
                                ℃
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;">
                            <td style="font-size: 14px; width: 40%">
                                &nbsp;&nbsp;&nbsp;&nbsp;设计工况下的冷却水流量
                            </td>
                            <td style="width: 30%">
                                <input type="text" id="txt_D_Wd" />
                            </td>
                            <td style="font-size: 14px; width: 30%">
                                t/h
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 14px; color: #48317C; font-weight: bold; width: 40%">
                                &nbsp;&nbsp;&nbsp;&nbsp;循环水泵A出口管道内径
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_Din_xb1O" />
                            </td>
                            <td style="font-size: 14px; color: #48317C; font-weight: bold; width: 30%">
                                m
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;">
                            <td style="font-size: 14px; width: 40%">
                                &nbsp;&nbsp;&nbsp;&nbsp;循环水泵B出口管道内径
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_Din_xb2O" />
                            </td>
                            <td style="font-size: 14px; width: 30%">
                                m
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 14px; color: #48317C; font-weight: bold; width: 40%">
                                &nbsp;&nbsp;&nbsp;&nbsp;凝汽器冷却水管材类型
                            </td>
                            <td style="width: 40%">
                                <select id="sec_T_Type">
                                    <option value="钛管TI">钛管TI</option>
                                    <option value="海军黄铜管HSN70-1">海军黄铜管HSN70-1</option>
                                    <option value="白铜管B30">白铜管B30</option>
                                    <option value="白铜管B10">白铜管B10</option>
                                    <option value="铝黄铜HA117-1">铝黄铜HA117-1</option>
                                    <option selected="selected" value="不锈钢管304">不锈钢管304</option>
                                </select>
                            </td>
                            <td style="font-size: 14px; color: #48317C; font-weight: bold; width: 40%">
                                /
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;">
                            <td style="font-size: 14px; width: 40%">
                                &nbsp;&nbsp;&nbsp;&nbsp;胶球回收数
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_I_N_ball_o" />
                            </td>
                            <td style="font-size: 14px; width: 40%">
                                /
                            </td>
                        </tr>
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold;">
                            <td style="font-size: 14px; width: 40%">
                                &nbsp;&nbsp;&nbsp;&nbsp;A循环水泵出口高程
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_Z_xb1O" />
                            </td>
                            <td style="font-size: 14px; width: 40%">
                                m</td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;">
                            <td style="font-size: 14px; width: 40%">
                                &nbsp;&nbsp;&nbsp;&nbsp;B循环水泵出口高程
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_Z_xb2O" />
                            </td>
                            <td style="font-size: 14px; width: 40%">
                                m</td>
                        </tr>
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold;">
                            <td style="font-size: 14px; width: 40%">
                                &nbsp;&nbsp;&nbsp;&nbsp;A循泵配套电动机效率
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_Eta_gr_xb1" />
                            </td>
                            <td style="font-size: 14px; width: 40%">
                                %
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;">
                            <td style="font-size: 14px; width: 40%">
                                &nbsp;&nbsp;&nbsp;&nbsp;B循泵配套电动机效率
                            </td>
                            <td style="width: 40%">
                                <input type="text" id="txt_D_Eta_gr_xb2" />
                            </td>
                            <td style="font-size: 14px; width: 40%">
                                %</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <a id="btn_save" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'"
                        onclick="save()">保存</a>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
