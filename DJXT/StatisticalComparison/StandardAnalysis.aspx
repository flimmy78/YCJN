<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StandardAnalysis.aspx.cs" Inherits="DJXT.StatisticalComparison.StandardAnalysis" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../css/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../jQueryEasyUI/jquery-1.6.2.js"></script>
    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/highcharts.js"></script>
    <script type="text/javascript" src="../js/exporting.js" charset="utf-8"></script>
    <script type="text/javascript">
        //时间设置 改变事件
        function change(ddl) {
            if ($(ddl).val() == 0) {
                $("#zhi").attr("style", "display:block");
                $("#txtTimeBegin").attr("style", "display:block");
                $("#txtTimeEnd").attr("style", "display:block");
                $("#ddlQuarter").attr("style", "display:none");
                $("#txtMonth").attr("style", "display:none");
                $("#txtYear").attr("style", "display:none");
            }
            else if ($(ddl).val() == 1) {
                $("#txtMonth").attr("style", "display:block");

                $("#zhi").attr("style", "display:none");
                $("#txtTimeBegin").attr("style", "display:none");
                $("#txtTimeEnd").attr("style", "display:none");
                $("#ddlQuarter").attr("style", "display:none");
                $("#txtYear").attr("style", "display:none");



            }
            else if ($(ddl).val() == 2) {
                $("#txtYear").attr("style", "display:block");
                $("#ddlQuarter").attr("style", "display:block");

                $("#txtMonth").attr("style", "display:none");
                $("#zhi").attr("style", "display:none");
                $("#txtTimeBegin").attr("style", "display:none");
                $("#txtTimeEnd").attr("style", "display:none");
            }
            else if ($(ddl).val() == 3) {
                $("#txtYear").attr("style", "display:block");

                $("#ddlQuarter").attr("style", "display:none");
                $("#txtMonth").attr("style", "display:none");
                $("#zhi").attr("style", "display:none");
                $("#txtTimeBegin").attr("style", "display:none");
                $("#txtTimeEnd").attr("style", "display:none");

            }
        };
        var initChart = function () {
            //var timess = $("#txtTime").val();
            var capacityLevel = $("#ddlCapacity").find("option:selected").text();
            var unitType = $("#ddlUnitType").find("option:selected").text();
            var BoilerId = $("#ddlBoiler").val();
            var SteamId = $("#ddlSteam").val();
            var type = $("#ddlType").val();
            var quertertype = $("#ddlQuarter").val();
            var beginC = $("#txtTimeBegin").val();
            var endC = $("#txtTimeEnd").val();
            var timeMonth = $("#txtMonth").val();
            var timeYear = $("#txtYear").val();

            var begintime;
            var endtime;
            var quertType;
            if (type == "0") {
                begintime = beginC;
                endtime = endC;
            }
            if (type == "1") {
                begintime = timeMonth;
            }
            if (type == "2") {
                begintime = timeYear;
                quertType = quertertype;
            }
            if (type == "3") {
                begintime = timeYear;
            }
            $.ajax({
                url: "../Handles/StatisticalComparison/ConsumeDb.ashx?capacityLevel=" +escape(capacityLevel) + "&unitType=" +escape(unitType) + "&BoilerId=" + BoilerId + "&SteamId=" + SteamId + "&beginTime=" + begintime + "&endTime=" + endtime + "&timeType=" + type + "&quarterType=" + quertType,
                contentType: "application/json; charset=gb2312",
                type: "post",

                success: function (data) {

                    //                    alert(data.ConsumeList);
                    //                    alert(data.name);
                    //                    alert(data.value);
                    //alert(data.name);
                    //alert(data.value);
                    (function ($) {
                        $(function () {
                            $('#bt').highcharts({
                                chart: {
                                    type: 'column'
                                },
                                title: {
                                    text: '耗差分析棒状图'
                                },
                                xAxis: {
                                    categories: data.name
                                },
                                yAxis: {
                                    title: {
                                        text: '耗差值(g/(KW/h))'
                                    }
                                },
                                credits: {
                                    enabled: false
                                },
                                series: [{ name: '耗差分析', data: data.value}]
                            });
                        })
                    })(jQuery);

                },
                error: function (x, e) {
                    alert(x.responseText);
                },
                complete: function () {
                    //Handle the complete event
                }
            });
        }
    </script>
     <script type="text/javascript">
        $(document).ready(function () {
           
            var highchartsOptions = Highcharts.setOptions(Highcharts.theme);
        });
        Highcharts.theme = {
            colors: ['#058DC7', '#50B432', '#ED561B', '#DDDF00', '#24CBE5', '#64E572', '#FF9655', '#FFF263', '#6AF9C4'],
            chart: {
                backgroundColor: {
                    linearGradient: { x1: 0, y1: 0, x2: 1, y2: 1 },
                    stops: [
            [0, 'rgb(255, 255, 255)'],
            [1, 'rgb(240, 240, 255)']
         ]
                },
                borderWidth: 2,
                plotBackgroundColor: 'rgba(255, 255, 255, .9)',
                plotShadow: true,
                plotBorderWidth: 1
            },
            title: {
                style: {
                    color: '#000',
                    font: 'bold 16px "Trebuchet MS", Verdana, sans-serif'
                }
            },
            subtitle: {
                style: {
                    color: '#666666',
                    font: 'bold 12px "Trebuchet MS", Verdana, sans-serif'
                }
            },
            xAxis: {
                gridLineWidth: 1,
                lineColor: '#000',
                tickColor: '#000',
                labels: {
                    style: {
                        color: '#000',
                        font: '11px Trebuchet MS, Verdana, sans-serif'
                    }
                },
                title: {
                    style: {
                        color: '#333',
                        fontWeight: 'bold',
                        fontSize: '12px',
                        fontFamily: 'Trebuchet MS, Verdana, sans-serif'

                    }
                }
            },
            yAxis: {
                minorTickInterval: 'auto',
                lineColor: '#000',
                lineWidth: 1,
                tickWidth: 1,
                tickColor: '#000',
                labels: {
                    style: {
                        color: '#000',
                        font: '11px Trebuchet MS, Verdana, sans-serif'
                    }
                },
                title: {
                    style: {
                        color: '#333',
                        fontWeight: 'bold',
                        fontSize: '12px',
                        fontFamily: 'Trebuchet MS, Verdana, sans-serif'
                    }
                }
            },
            legend: {
                itemStyle: {
                    font: '9pt Trebuchet MS, Verdana, sans-serif',
                    color: 'black'

                },
                itemHoverStyle: {
                    color: '#039'
                },
                itemHiddenStyle: {
                    color: 'gray'
                }
            },
            labels: {
                style: {
                    color: '#99b'
                }
            },

            navigation: {
                buttonOptions: {
                    theme: {
                        stroke: '#CCCCCC'
                    }
                }
            }
        };

        function messages() {
//            if ($("#ddlCompany").val() == "0") {
//                alert("请选择公司！");
//                return false;
//            }
//            if ($("#ddlPlant").val() == "0") {
//                alert("请选择电厂！");
//                return false;
//            }
//            if ($("#ddlUnit").val() == "0") {
//                alert("请选择机组！");
//                return false;
//            }
            initChart();
            //return false;

        }
        </script>
</head>
<body style="background-color: #E3EDF8; font-size: 12px; font-family: 宋体">
    <form id="form1" runat="server">
    <div>
    <table width="100%" border="0" cellspacing="0" cellpadding="4">
        <tr>
            <td>
                &nbsp;机组容量
                <asp:DropDownList ID="ddlCapacity" runat="server">
                    <asp:ListItem Selected="True" Value="0">--请选择机组容量--</asp:ListItem>
                    <asp:ListItem Value="1">135MW</asp:ListItem>
                    <asp:ListItem Value="2">200MW</asp:ListItem>
                    <asp:ListItem Value="3">300MW</asp:ListItem>
                    <asp:ListItem Value="4">600MW</asp:ListItem>
                </asp:DropDownList>
                &nbsp; &nbsp; &nbsp; &nbsp; 机组类型
                <asp:DropDownList ID="ddlUnitType" runat="server">
                    <asp:ListItem Selected="True" Value="0">--请选择机组类型--</asp:ListItem>
                    <asp:ListItem Value="1">空冷</asp:ListItem>
                    <asp:ListItem Value="2">湿冷</asp:ListItem>
                    <asp:ListItem Value="3">流化床</asp:ListItem>
                    <asp:ListItem Value="4">供热机组</asp:ListItem>
                    <asp:ListItem Value="5">超超临界</asp:ListItem>
                    <asp:ListItem Value="6">空冷供热</asp:ListItem>
                    <asp:ListItem Value="7">湿冷供热</asp:ListItem>
                    <asp:ListItem Value="8">纯凝机组</asp:ListItem>
                    <asp:ListItem Value="9">常规供热</asp:ListItem>
                    <asp:ListItem Value="10">常规纯凝</asp:ListItem>
                    <asp:ListItem Value="11">空冷纯凝</asp:ListItem>
                    <asp:ListItem Value="12">流化床机组</asp:ListItem>
                    <asp:ListItem Value="13">超临界湿冷</asp:ListItem>
                    <asp:ListItem Value="14">亚临界空冷</asp:ListItem>
                    <asp:ListItem Value="15">流化床供热</asp:ListItem>
                    <asp:ListItem Value="16">亚临界湿冷</asp:ListItem>
                </asp:DropDownList>
                &nbsp; &nbsp; &nbsp; &nbsp; 锅炉厂家
                <asp:DropDownList ID="ddlBoiler" runat="server">
                    <asp:ListItem Selected="True" Value="0">--请选择锅炉厂家--</asp:ListItem>
                </asp:DropDownList>
                汽机厂家
                <asp:DropDownList ID="ddlSteam" runat="server">
                    <asp:ListItem Selected="True" Value="0">--请选择汽机厂家--</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table><tr><td>
        <div style="float: left; margin-left: 5px;">
            时间设置
                <asp:DropDownList ID="ddlType" runat="server" onchange="change(this)">
                    <asp:ListItem Value="0">指定时间段平均值</asp:ListItem>
                    <asp:ListItem Value="1">月度平均值</asp:ListItem>
                    <asp:ListItem Value="2">季度平均值</asp:ListItem>
                    <asp:ListItem Value="3">年度平均值</asp:ListItem>
                </asp:DropDownList>
        </div>
        &nbsp; &nbsp;
        <div style="float: left; margin-left: 5px;">
            <input class="Wdate" id="txtTimeBegin" runat="server" type="text" readonly="readonly"
                onclick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss ',maxDate:'%y-%M-%d'})" />
            <input class="Wdate" id="txtMonth" runat="server" type="text" readonly="readonly"
                onclick="WdatePicker({dateFmt:'yyyy-MM'})" style="display: none" />
            <input class="Wdate" id="txtYear" runat="server" type="text" readonly="readonly"
                onclick="WdatePicker({dateFmt:'yyyy'})" style="display: none" />
            <span id="zhi">至</span></div>
        <div style="float: left; margin-left: 5px;">
            <input class="Wdate" id="txtTimeEnd" runat="server" type="text" readonly="readonly"
                onclick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss ',maxDate:'%y-%M-%d'})" />
        </div>
            <div style="float: left; margin-left: 5px;">
                <asp:DropDownList ID="ddlQuarter" runat="server" Style="display: none">
                    <asp:ListItem Value="0">一季度</asp:ListItem>
                    <asp:ListItem Value="1">二季度</asp:ListItem>
                    <asp:ListItem Value="2">三季度</asp:ListItem>
                    <asp:ListItem Value="3">四季度</asp:ListItem>
                </asp:DropDownList>
            </div>
        <div style="float: left; margin-left: 5px;">
           <%-- <asp:Button ID="btnSearch" runat="server" OnClick="Search_Click" Text="查询" />--%>
                 <a id="CX" href="#" class="easyui-linkbutton" onclick="messages();">查&nbsp;&nbsp;询</a>
        </div>
    </td>
    </tr>
    <tr><td><br /></td></tr>
        <tr>
            <td>
                <div id="bt" style="width: 700px; height: 450px; margin: 0 auto; float: left;">
                </div>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
