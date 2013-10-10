<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Analyse.aspx.cs" Inherits="DJXT.EquipmentReliable.Analyse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<head runat="server">
    
<%--<link rel="shortcut icon" href="/favicon.ico"/>--%>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript"> window.onerror = function () { return true; };</script>
    <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../js/grid.js"></script>
    <script language="javascript" type="text/javascript" src="../css/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../js/highcharts.js"></script>
    <script type="text/javascript" src="../js/exporting.js" charset="utf-8"></script>
    <script type="text/javascript">
        jQuery.noConflict();
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            //initChart();
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

        var initChart = function () {
            var dataJson;
            var ss;
            var timess = $("#txtTime").val();
            var unitCode = $("#ddlUnit").val();
            $.ajax({
                url: "../Handles/ReliableAnalysis.ashx?time=" + timess+"&unitCode="+unitCode,
                contentType: "application/json; charset=utf-8",
                type: "GET",

                success: function (data) {
                    //alert(data.infoList[0].sTime);
                    // alert(data.plotList.toLocaleString());
                    //alert(data);
                    //alert(data.plotList.length);
                    //alert(data.professionList);
                    //alert(data.reasonList);
                    //alert(data.infoList[2].cTime);
                    (function ($) {
                        $(function () {
                            $('#container').highcharts({
                                chart: {
                                    type: 'column',
                                    margin: [50, 50, 100, 80]
                                },
                                title: {
                                    text: '强迫停运次数'
                                },
                                xAxis: {
                                    categories: [data.infoList[0].sTime, data.infoList[1].sTime, data.infoList[2].sTime, data.infoList[3].sTime, data.infoList[4].sTime],
                                    labels: {
                                        rotation: -45,
                                        align: 'right',
                                        style: {
                                            fontSize: '13px',
                                            fontFamily: 'Verdana, sans-serif'
                                        }
                                    }
                                },
                                yAxis: {
                                    min: 0,
                                    title: {
                                        text: '强迫停运次数 (次)'
                                    }
                                },
                                legend: {
                                    enabled: false
                                },
                                tooltip: {
                                    formatter: function () {
                                        return '<b>' + this.x + '</b><br/>' +
                        '强迫停运次数: ' + Highcharts.numberFormat(this.y, 1) +
                        ' 次';
                                    }
                                },
                                series: [{
                                    name: 'Population',
                                    data: [data.infoList[0].cTime, data.infoList[1].cTime, data.infoList[2].cTime, data.infoList[3].cTime, data.infoList[4].cTime],
                                    dataLabels: {
                                        enabled: true,
                                        rotation: -90,
                                        color: '#FFFFFF',
                                        align: 'right',
                                        x: 4,
                                        y: 10,
                                        style: {
                                            fontSize: '12px',
                                            fontFamily: 'Verdana, sans-serif'
                                        }
                                    }
                                }]
                            });
                        })
                    })(jQuery);

                    (function ($) {
                        $(function () {
                            $('#divFoh').highcharts({
                                chart: {
                                    type: 'column',
                                    margin: [50, 50, 100, 80]
                                },
                                title: {
                                    text: '强迫停运时间'
                                },
                                xAxis: {
                                    categories: [data.infoList[0].sTime, data.infoList[1].sTime, data.infoList[2].sTime, data.infoList[3].sTime, data.infoList[4].sTime],
                                    labels: {
                                        rotation: -45,
                                        align: 'right',
                                        style: {
                                            fontSize: '13px',
                                            fontFamily: 'Verdana, sans-serif'
                                        }
                                    }
                                },
                                yAxis: {
                                    min: 0,
                                    title: {
                                        text: '强迫停运时间 (小时)'
                                    }
                                },
                                legend: {
                                    enabled: false
                                },
                                tooltip: {
                                    formatter: function () {
                                        return '<b>' + this.x + '</b><br/>' +
                        '强迫停运时间: ' + Highcharts.numberFormat(this.y, 1) +
                        ' 小时';
                                    }
                                },
                                series: [{
                                    name: 'Population',
                                    data: [data.infoList[0].sj, data.infoList[1].sj, data.infoList[2].sj, data.infoList[3].sj, data.infoList[4].sj],
                                    dataLabels: {
                                        enabled: true,
                                        rotation: -90,
                                        color: '#FFFFFF',
                                        align: 'right',
                                        x: 4,
                                        y: 10,
                                        style: {
                                            fontSize: '12px',
                                            fontFamily: 'Verdana, sans-serif'
                                        }
                                    }
                                }]
                            });
                        })
                    })(jQuery);
                    (function ($) {
                        $(function () {
                            $('#divRlty').highcharts({
                                chart: {
                                    plotBackgroundColor: null,
                                    plotBorderWidth: null,
                                    plotShadow: false
                                },
                                title: {
                                    text: '强迫停运次数分析（按容量分类）'
                                },
                                tooltip: {
                                    pointFormat: '{series.name}: <b>{point.y}次</b>'
                                },
                                plotOptions: {
                                    pie: {
                                        allowPointSelect: true,
                                        cursor: 'pointer',
                                        dataLabels: {
                                            enabled: true,
                                            color: '#000000',
                                            connectorColor: '#000000',
                                            format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                                        }
                                    }
                                },
                                series: [{
                                    type: 'pie',
                                    name: '强迫停运次数',
                                    data:
                                    data.plotList

                                }]
                            });
                        })
                    })(jQuery);
                    (function ($) {
                        $(function () {
                            $('#divZyty').highcharts({
                                chart: {
                                    plotBackgroundColor: null,
                                    plotBorderWidth: null,
                                    plotShadow: false
                                },
                                title: {
                                    text: '强迫停运次数分析（按专业分类）'
                                },
                                tooltip: {
                                    pointFormat: '{series.name}: <b>{point.y}次</b>'
                                },
                                plotOptions: {
                                    pie: {
                                        allowPointSelect: true,
                                        cursor: 'pointer',
                                        dataLabels: {
                                            enabled: true,
                                            color: '#000000',
                                            connectorColor: '#000000',
                                            format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                                        }
                                    }
                                },
                                series: [{
                                    type: 'pie',
                                    name: '强迫停运次数',
                                    data:
                                    data.professionList
                                }]
                            });
                        })
                    })(jQuery);

                    (function ($) {
                        $(function () {
                            $('#divGzty').highcharts({
                                chart: {
                                    plotBackgroundColor: null,
                                    plotBorderWidth: null,
                                    plotShadow: false
                                },
                                title: {
                                    text: '强迫停运次数分析（按故障原因分类）'
                                },
                                tooltip: {
                                    pointFormat: '{series.name}: <b>{point.y}次</b>'
                                },
                                plotOptions: {
                                    pie: {
                                        allowPointSelect: true,
                                        cursor: 'pointer',
                                        dataLabels: {
                                            enabled: true,
                                            color: '#000000',
                                            connectorColor: '#000000',
                                            format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                                        }
                                    }
                                },
                                series: [{
                                    type: 'pie',
                                    name: '强迫停运次数',
                                    data: data.reasonList
                                }]
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
        };
         
    </script>
</head>
<body style="background-color: #E3EDF8; font-size: 12px; font-family: 宋体; width: 100%;
    height: 100%;">
    <form id="form1" runat="server">
    
    <table height="25" border="0" cellpadding="0" cellspacing="0" width="100%" bgcolor="#e5f1f4"
        style="border-bottom: 2px solid #48BADB;">
        <tr>
            <td>
                <div align="left" class="title">
                    可靠性分析</div>
            </td>
        </tr>
    </table>
    <div style="width: 100%; height: 100%;">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="4">
                <tr>
                    <td>
                        &nbsp; 省公司<asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedChanged"
                            AutoPostBack="true">
                            <%-- <asp:ListItem Selected="True"  Value="0">--请选择省公司--</asp:ListItem>--%>
                        </asp:DropDownList>
                    &nbsp; &nbsp; &nbsp; &nbsp;
                        电厂<asp:DropDownList ID="ddlPlant" runat="server" OnSelectedIndexChanged="ddlPlant_SelectedChanged"
                            AutoPostBack="true">
                            <asp:ListItem Selected="True" Value="0">--请选择电厂--</asp:ListItem>
                        </asp:DropDownList>
                    &nbsp; &nbsp; &nbsp; &nbsp;
                        机组<asp:DropDownList ID="ddlUnit" runat="server">
                            <asp:ListItem Selected="True" Value="0">--请选择机组--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="ddlCompany" />--%>
        </Triggers>
    </asp:UpdatePanel>
        <table>
            <tr>
                <td>
                    时间段：<input class="Wdate" id="txtTime" runat="server" type="text" onclick="WdatePicker({dateFmt:'yyyy-MM'})" />
                </td>
                <td>
                    <%--<asp:Button ID="btnSearch" runat="server" OnClick="Search_Click" Text="查询" />--%>
        <a id="CX" href="#" class="easyui-linkbutton" onclick="initChart();">查&nbsp;&nbsp;询</a> 

                </td>
            </tr>
        </table>
        <br />
        <br />
        <table style="width: 100%; height: 100%;">
            <tr>
                <td>
                    <div id="container" style="width: 500px; height: 300px; margin: 0 auto">
                    </div>
                </td>
                <td>
                    <div id="divFoh" style="width: 500px; height: 300px; margin: 0 auto">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divRlty" style="width: 500px; height: 300px; margin: 0 auto">
                    </div>
                </td>
                <td>
                    <div id="divZyty" style="width: 500px; height: 300px; margin: 0 auto">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divGzty" style="width: 500px; height: 300px; margin: 0 auto">
                    </div>
                </td>
            </tr>
        </table>
    </div>
     
    </form>
</body>
</html>
