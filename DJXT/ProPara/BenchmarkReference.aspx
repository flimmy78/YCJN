<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BenchmarkReference.aspx.cs"
    Inherits="DJXT.ProPara.BenchmarkReference" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                    &nbsp;&nbsp;对标基准值
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
                <td colspan="2">
                    <hr style="color: #4897DC" />
                </td>
            </tr>
            <tr>
                <td style="font-size: 16px; color: #48317C; font-weight: bold;" align="center" colspan="2">
                    基准值及耗差因子
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr style="color: #4897DC;" />
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table>
                        <tr style="font-size: 15px; background: #328CD3; color: White; font-weight: bold;
                            width: 30%">
                            <td style="width: 25%" align="center">
                                参数名称
                            </td>
                            <td style="width: 20%" align="center">
                                对标基准值
                            </td>
                            <td style="width: 10%" align="center">
                                单位
                            </td>
                            <td style="width: 20%" align="center">
                                参数变化
                            </td>
                            <td align="center">
                                对发电煤耗的影响g/(kW•h)
                            </td>
                        </tr>
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold;" align="lift">
                            <td>
                                &nbsp;&nbsp;主汽温度
                            </td>
                            <td>
                                <input type="text" id="txt_Car" />
                            </td>
                            <td>
                                ℃
                            </td>
                            <td>
                                降低1℃
                            </td>
                            <td>
                                <input type="text" id="Text1" />
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;"
                            align="lift">
                            <td>
                                &nbsp;&nbsp;主汽压力
                            </td>
                            <td>
                                <input type="text" id="Text2" />
                            </td>
                            <td>
                                MPa
                            </td>
                            <td>
                                降低0.1MPa&nbsp;
                            </td>
                            <td>
                                <input type="text" id="Text3" />
                            </td>
                        </tr>
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold;" align="lift">
                            <td>
                                &nbsp;&nbsp;再热温度
                            </td>
                            <td>
                                <input type="text" id="Text4" />
                            </td>
                            <td>
                                ℃
                            </td>
                            <td>
                                降低1℃
                            </td>
                            <td>
                                <input type="text" id="Text5" />
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;"
                            align="lift">
                            <td>
                                &nbsp;&nbsp;再热压损
                            </td>
                            <td>
                                <input type="text" id="Text6" />
                            </td>
                            <td>
                                %
                            </td>
                            <td>
                                升高1%</td>
                            <td>
                                <input type="text" id="Text7" />
                            </td>
                        </tr>
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold;" align="lift">
                            <td>
                                &nbsp;&nbsp;排气压力
                            </td>
                            <td>
                                <input type="text" id="Text8" />
                            </td>
                            <td>
                                kPa
                            </td>
                            <td>
                                升高1kPa
                            </td>
                            <td>
                                <input type="text" id="Text9" />
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;"
                            align="lift">
                            <td>
                                &nbsp;&nbsp;过减水流量
                            </td>
                            <td>
                                <input type="text" id="Text10" />
                            </td>
                            <td>
                                t/h
                            </td>
                            <td>
                                增加1t/h&nbsp;
                            </td>
                            <td>
                                <input type="text" id="Text11" />
                            </td>
                        </tr>
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold;" align="lift">
                            <td>
                                &nbsp;&nbsp;再减水流量
                            </td>
                            <td>
                                <input type="text" id="Text12" />
                            </td>
                            <td>
                                t/h
                            </td>
                            <td>
                                增加1t/h
                            </td>
                            <td>
                                <input type="text" id="Text13" />
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;"
                            align="lift">
                            <td>
                                &nbsp;&nbsp;补水率
                            </td>
                            <td>
                                <input type="text" id="Text14" />
                            </td>
                            <td>
                                %
                            </td>
                            <td>
                                增加1%
                            </td>
                            <td>
                                <input type="text" id="Text15" />
                            </td>
                        </tr>
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold;" align="lift">
                            <td>
                                &nbsp;&nbsp;锅炉排污流量
                            </td>
                            <td>
                                <input type="text" id="Text16" />
                            </td>
                            <td>
                                t/h
                            </td>
                            <td>
                                增加1t/h
                            </td>
                            <td>
                                <input type="text" id="Text17" />
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;"
                            align="lift">
                            <td>
                                &nbsp;&nbsp;凝汽器过冷度
                            </td>
                            <td>
                                <input type="text" id="Text18" />
                            </td>
                            <td>
                                ℃
                            </td>
                            <td>
                                增加1℃
                            </td>
                            <td>
                                <input type="text" id="Text19" />
                            </td>
                        </tr>
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold;" align="lift">
                            <td>
                                &nbsp;&nbsp;凝汽器端差
                            </td>
                            <td>
                                <input type="text" id="Text20" />
                            </td>
                            <td>
                                ℃
                            </td>
                            <td>
                                增加1℃
                            </td>
                            <td>
                                <input type="text" id="Text21" />
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;"
                            align="lift">
                            <td>
                                &nbsp;&nbsp;小汽机用汽量
                            </td>
                            <td>
                                <input type="text" id="Text22" />
                            </td>
                            <td>
                                t/h
                            </td>
                            <td>
                                增加1t/h
                            </td>
                            <td>
                                <input type="text" id="Text23" />
                            </td>
                        </tr>
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold;" align="lift">
                            <td>
                                &nbsp;&nbsp;排烟温度
                            </td>
                            <td>
                                <input type="text" id="Text24" />
                            </td>
                            <td>
                                ℃
                            </td>
                            <td>
                                增加1℃
                            </td>
                            <td>
                                <input type="text" id="Text25" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top">
                    <table>
                        <tr style="font-size: 15px; background: #328CD3; color: White; font-weight: bold;
                            width: 30%">
                            <td style="width: 25%" align="center">
                                参数名称
                            </td>
                            <td style="width: 20%" align="center">
                                对标基准值
                            </td>
                            <td style="width: 10%" align="center">
                                单位
                            </td>
                            <td style="width: 20%" align="center">
                                参数变化
                            </td>
                            <td align="center">
                                对发电煤耗的影响g/(kW•h)
                            </td>
                        </tr>
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold;" align="lift">
                            <td>
                                &nbsp;&nbsp;1号高加上端差
                            </td>
                            <td>
                                <input type="text" id="Text26" />
                            </td>
                            <td>
                                ℃
                            </td>
                            <td>
                                增加1℃
                            </td>
                            <td>
                                <input type="text" id="Text27" />
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;"
                            align="lift">
                            <td>
                                &nbsp;&nbsp;2号高加上端差
                            </td>
                            <td>
                                <input type="text" id="Text28" />
                            </td>
                            <td>
                                ℃
                            </td>
                            <td>
                                增加1℃
                            </td>
                            <td>
                                <input type="text" id="Text29" />
                            </td>
                        </tr>
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold;" align="lift">
                            <td>
                                &nbsp;&nbsp;3号高加上端差
                            </td>
                            <td>
                                <input type="text" id="Text30" />
                            </td>
                            <td>
                                ℃
                            </td>
                            <td>
                                增加1℃
                            </td>
                            <td>
                                <input type="text" id="Text31" />
                            </td>
                        </tr>
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold;" align="lift">
                            <td>
                                &nbsp;&nbsp;5号高加上端差
                            </td>
                            <td>
                                <input type="text" id="Text34" />
                            </td>
                            <td>
                                ℃
                            </td>
                            <td>
                                增加1℃
                            </td>
                            <td>
                                <input type="text" id="Text35" />
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;"
                            align="lift">
                            <td>
                                &nbsp;&nbsp;6号高加上端差
                            </td>
                            <td>
                                <input type="text" id="Text36" />
                            </td>
                            <td>
                                ℃
                            </td>
                            <td>
                                增加1℃
                            </td>
                            <td>
                                <input type="text" id="Text37" />
                            </td>
                        </tr>
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold;" align="lift">
                            <td>
                                &nbsp;&nbsp;7号高加上端差
                            </td>
                            <td>
                                <input type="text" id="Text38" />
                            </td>
                            <td>
                                ℃
                            </td>
                            <td>
                                增加1℃
                            </td>
                            <td>
                                <input type="text" id="Text39" />
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;"
                            align="lift">
                            <td>
                                &nbsp;&nbsp;8号高加上端差
                            </td>
                            <td>
                                <input type="text" id="Text40" />
                            </td>
                            <td>
                                ℃
                            </td>
                            <td>
                                增加1℃
                            </td>
                            <td>
                                <input type="text" id="Text41" />
                            </td>
                        </tr>
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold;" align="lift">
                            <td>
                                &nbsp;&nbsp;高压缸效率
                            </td>
                            <td>
                                <input type="text" id="Text42" />
                            </td>
                            <td>
                                %</td>
                            <td>
                                降低1%</td>
                            <td>
                                <input type="text" id="Text43" />
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;"
                            align="lift">
                            <td>
                                &nbsp;&nbsp;中压缸效率
                            </td>
                            <td>
                                <input type="text" id="Text44" />
                            </td>
                            <td>
                                %</td>
                            <td>
                                降低1%</td>
                            <td>
                                <input type="text" id="Text45" />
                            </td>
                        </tr>
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold;" align="lift">
                            <td>
                                &nbsp;&nbsp;锅炉效率
                            </td>
                            <td>
                                <input type="text" id="Text46" />
                            </td>
                            <td>
                                %</td>
                            <td>
                                降低1%</td>
                            <td>
                                <input type="text" id="Text47" />
                            </td>
                        </tr>
                        <tr style="font-size: 14px; background-color: #F8FBFC; color: #48317C; font-weight: bold;"
                            align="lift">
                            <td>
                                &nbsp;&nbsp;炉渣含碳量
                            </td>
                            <td>
                                <input type="text" id="Text48" />
                            </td>
                            <td>
                                %</td>
                            <td>
                                变化1%</td>
                            <td>
                                <input type="text" id="Text49" />
                            </td>
                        </tr>
                        <tr style="font-size: 14px; color: #48317C; font-weight: bold;" align="lift">
                            <td>
                                &nbsp;&nbsp;飞灰含碳量
                            </td>
                            <td>
                                <input type="text" id="Text50" />
                            </td>
                            <td>
                                %
                            </td>
                            <td>
                                增加1%</td>
                            <td>
                                <input type="text" id="Text51" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
