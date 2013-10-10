<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductionProPara.aspx.cs"
    Inherits="DJXT.ProPara.ProductionProPara" %>

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
</head>
<body>
    <form id="form1" runat="server">
    <div id="menu">
        <table width="100%" height="92%" cellpadding="4" cellspacing="0" class="style5" border="0">
            <tr>
                <td background="../img/table-head.jpg" height="30px" valign="middle" class="style6"
                    colspan="2">
                    &nbsp;&nbsp;生产过程参数
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
                            <span style="z-index:-9999"> 
                                <select id="sec_company" runat="server" onchange="change_company()">
                                </select>&nbsp;
                                </span>
                            </td>
                            <td>
                                电厂&nbsp;
                            </td>
                            <td>
                            <span style="z-index:-9999"> 
                                <select id="sec_electric" runat="server" onchange="change_electric()">
                                    <option>-请选择-</option>
                                </select>&nbsp;
                                </span>
                            </td>
                            <td>
                                机组&nbsp;
                            </td>
                            <td>
                            <span style="z-index:-9999"> 
                                <select id="sec_crew" runat="server">
                                    <option>-请选择-</option>
                                </select>
                                </span>
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
                <td align="right" style="width: 50%">
                    <input type="radio" id="rd_one" name="fangshi" onclick="Load_Rating()" checked="checked" />方式一
                </td>
                <td>
                    <input type="radio" id="rd_second" name="fangshi" onclick="Load_Rating()" />方式二
                </td>
            </tr>
            <tr>
                <td>
                    <hr style="color: #4897DC" />
                </td>
                <td>
                    <hr style="color: #4897DC" />
                </td>
            </tr>
            <tr>
                <td style="font-size: 16px; color: #48317C; font-weight: bold;" align="center">
                    元素分析试验煤质
                </td>
                <td style="font-size: 16px; color: #48317C; font-weight: bold;" align="center">
                    其他参数
                </td>
            </tr>
            <tr>
                <td>
                    <hr style="color: #4897DC" />
                </td>
                <td>
                    <hr style="color: #4897DC" />
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table width="100%">
                        <tr>
                            <td style="font-size: 14px; color: #48317C; font-weight: bold; width: 40%">
                                &nbsp;&nbsp;&nbsp;&nbsp;收到基碳Car
                            </td>
                            <td style="width: 30%">
                                <input type="text" id="txt_Car" />
                            </td>
                            <td style="font-size: 14px; color: #48317C; font-weight: bold; width: 30%">
                                %
                            </td>
                        </tr>
                        <tr style="background-color: #F8FBFC; color: #48317C; font-weight: bold; width: 30%">
                            <td style="width: 40%; font-size: 14px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;收到基氢Har
                            </td>
                            <td style="width: 30%">
                                <input type="text" id="txt_Har" />
                            </td>
                            <td style="width: 30%; font-size: 14px;">
                                %
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 14px; color: #48317C; font-weight: bold; width: 40%">
                                &nbsp;&nbsp;&nbsp;&nbsp;收到基氧Oar
                            </td>
                            <td style="width: 30%">
                                <input type="text" id="txt_Oar" />
                            </td>
                            <td style="font-size: 14px; color: #48317C; font-weight: bold; width: 30%">
                                %
                            </td>
                        </tr>
                        <tr style="background-color: #F8FBFC; color: #48317C; font-weight: bold;">
                            <td style="width: 40%; font-size: 14px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;收到基氮Nar
                            </td>
                            <td style="width: 30%">
                                <input type="text" id="txt_Nar" />
                            </td>
                            <td style="width: 30%; font-size: 14px;">
                                %
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 14px; color: #48317C; font-weight: bold; width: 40%">
                                &nbsp;&nbsp;&nbsp;&nbsp;收到基硫Sar
                            </td>
                            <td style="width: 30%">
                                <input type="text" id="txt_Sar" />
                            </td>
                            <td style="font-size: 14px; color: #48317C; font-weight: bold; width: 30%">
                                %
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top">
                    <table width="100%">
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold; width: 30%">
                            <td style="width: 40%; font-size: 14px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;飞灰可燃物含碳量afh
                            </td>
                            <td style="width: 30%">
                                <input type="text" id="txt_afh" />
                            </td>
                            <td style="width: 30%; font-size: 14px;">
                                %
                            </td>
                        </tr>
                        <tr style="font-size: 14px; color: #48317C; background-color: #F8FBFC; font-weight: bold;">
                            <td style="width: 40%; font-size: 14px;">
                                &nbsp;&nbsp;&nbsp;&nbsp;炉渣可燃物含碳量alz
                            </td>
                            <td style="width: 30%">
                                <input type="text" id="txt_alz" />
                            </td>
                            <td style="width: 30%; font-size: 14px;">
                                %
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
                <td colspan="2" align="center">
                    <table id="tab_hide">
                        <tr>
                            <td style="font-size: 16px; color: #48317C; font-weight: bold;" align="center" colspan="2">
                                工业分析试验煤质
                            </td>
                            <td id="td_disable">
                             

                                &nbsp;&nbsp;<asp:FileUpload ID="fileUp" runat="server" ViewStateMode="Enabled" style="background-color: #CCCCCC" />
                               &nbsp;&nbsp;<input name="button"
                            type="submit" class="Botton" id="button" value="导入Excel" onclick="return chkform();" onserverclick="button_ServerClick"
                             runat="server" />
                               &nbsp;&nbsp;&nbsp;&nbsp;<a id="preserve" href="#" class="easyui-linkbutton"
                                data-options="iconCls:'icon-search'">查询当前机组煤质</a>
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
                <td>
                    <table width="100%">
                        <tr>
                            <td style="font-size: 14px; color: #48317C; width: 40%">
                                &nbsp;&nbsp;&nbsp;&nbsp;空干基水分Md
                            </td>
                            <td style="width: 30%">
                                <input type="text" id="txt_Mt" />
                            </td>
                            <td style="font-size: 14px; color: #48317C; width: 30%">
                                %
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="font-size: 14px; color: #48317C; width: 40%">
                                &nbsp;&nbsp;&nbsp;&nbsp;空干基全硫St,ad
                            </td>
                            <td style="width: 30%">
                                <input type="text" id="txt_St_ad" />
                            </td>
                            <td style="font-size: 14px; color: #48317C; width: 30%">
                                %
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="background-color: #F8FBFC">
                <td>
                    <table width="100%">
                        <tr>
                            <td style="font-size: 14px; color: #48317C; width: 40%">
                                &nbsp;&nbsp;&nbsp;&nbsp;空干基灰分Aad
                            </td>
                            <td style="width: 30%">
                                <input type="text" id="txt_Mad" />
                            </td>
                            <td style="font-size: 14px; color: #48317C; width: 30%">
                                %
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="font-size: 14px; color: #48317C; width: 40%">
                                &nbsp;&nbsp;&nbsp;&nbsp;收到基全硫St,ar
                            </td>
                            <td style="width: 30%">
                                <input type="text" id="txt_St_ar" />
                            </td>
                            <td style="font-size: 14px; color: #48317C; width: 30%">
                                %
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="font-size: 14px; color: #48317C; width: 40%">
                                &nbsp;&nbsp;&nbsp;&nbsp;干燥无灰基挥发分Vdaf
                            </td>
                            <td style="width: 30%">
                                <input type="text" id="txt_Vdaf" />
                            </td>
                            <td style="font-size: 14px; color: #48317C; width: 30%">
                                %
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="font-size: 14px; color: #48317C; width: 40%">
                                &nbsp;&nbsp;&nbsp;&nbsp;收到基水分Mar
                            </td>
                            <td style="width: 30%">
                                <input type="text" id="txt_Mar" />
                            </td>
                            <td style="font-size: 14px; color: #48317C; width: 30%">
                                %
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="background-color: #F8FBFC">
                <td>
                    <table width="100%">
                        <tr>
                            <td style="font-size: 14px; color: #48317C; width: 40%">
                                &nbsp;&nbsp;&nbsp;&nbsp;收到基低位发热量Qnet.ar
                            </td>
                            <td style="width: 30%">
                                <input type="text" id="txt_Qnet_ar" />
                            </td>
                            <td style="font-size: 14px; color: #48317C; width: 30%">
                                KJ/kg
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="font-size: 14px; color: #48317C; width: 40%">
                                &nbsp;&nbsp;&nbsp;&nbsp;收到基灰分Aar
                            </td>
                            <td style="width: 30%">
                                <input type="text" id="txt_Aar" />
                            </td>
                            <td style="font-size: 14px; color: #48317C; width: 30%">
                                %
                            </td>
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
    <div id="pre" title="煤质查询" data-options="iconCls:'icon-search'" style="display: none;
        overflow-y: scroll; overflow-x: hidden; width: 800px; height: 550px; padding: 10px;position:absolute;z-index:10;">
        <iframe  style="position:absolute;z-index:-1;width:150%;height:150%;top:0;left:0;scrolling:no;" frameborder="0" src="about:blank"></iframe>
        <div style="margin-left: 10px; height: 250px;">
            <table style="font-size: 13px; display: block;" height="100%" width="700px" border="0">
                <tr valign="top">
                    <td>
                        查询时间段：
                    </td>
                    <td align="left">
                        <input id="stime" class="Wdate" style="text-align: center;" runat="server" readonly="readonly"
                            type="text" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'etime\')||\'2020-10-01\'}',skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})" />&nbsp;
                        至
                        <input id="etime" class="Wdate" style="text-align: center;" runat="server" readonly="readonly"
                            type="text" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'etime\')||\'2020-10-01\'}',skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})" />
                    </td>
                    <td>
                        <input type="button" value="查询" onclick="pre_query()"; /><input type="button" id="btnExportCSV" value="导出当前记录" class="nvinput" onclick="toExcel(CX)" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" valign="top" align="left">
                        <div id="show" runat="server">
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
