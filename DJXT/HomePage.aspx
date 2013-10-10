<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="DJXT.HomePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript" src="css/My97DatePicker/WdatePicker.js"></script>
     <script type="text/javascript" src="js/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
     <script type="text/javascript" src="js/highcharts.js"></script>
    <script type="text/javascript" src="js/exporting.js" charset="utf-8"></script>
    <script type="text/javascript">
        jQuery.noConflict();
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            initChart();
            // Apply the theme
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
            var timess = $("#txtTimeBegin").val();
            $.ajax({
                url: "Handles/HomePage/HomePage.ashx?time=" + timess,
                contentType: "application/json; charset=utf-8",
                type: "GET",

                success: function (data) {
                    //alert(data[0].T_INDICATORNAME);

                    //容量
                    (function ($) {
                        $(function () {
                            $('#container').highcharts({
                                chart: {
                                    type: 'column',
                                    margin: [50, 50, 100, 80]
                                },
                                title: {
                                    text: '发电设备容量'
                                },
                                xAxis: {
                                    categories: ['华电', '华能', '大唐', '国电', '中电投'],
                                    labels: {
                                        align: 'center',
                                        style: {
                                            fontSize: '13px',
                                            fontFamily: 'Verdana, sans-serif'
                                        }
                                    }
                                },
                                yAxis: {
                                    min: 5000,
                                    title: {
                                        text: '万千瓦'
                                    }
                                },
                                legend: {
                                    enabled: false
                                },
                                tooltip: {
                                    formatter: function () {
                                        return '<b>' + this.x + '</b><br/>' +
                        '发电设备容量: ' + Highcharts.numberFormat(this.y, 1) +
                        ' 万千瓦';
                                    }
                                },
                                series: [{
                                    name: '华电：',
                                    data: [data[0].D_HDALL, data[0].D_HNALL, data[0].D_DTALL, data[0].D_GDALL, data[0].D_ZDTALL],
                                    dataLabels: {
                                        enabled: true,
                                        color: 'blue',
                                        align: 'center',
                                        x: 4,
                                        y: 1,
                                        style: {
                                            fontSize: '13px',
                                            fontFamily: 'Verdana, sans-serif'
                                        }
                                    }
                                }]
                            });
                        })
                    })(jQuery);

                    //利用小时
                    (function ($) {
                        $(function () {
                            $('#useTime').highcharts({
                                chart: {
                                    type: 'column',
                                    margin: [50, 50, 100, 80]
                                },
                                title: {
                                    text: '发电设备利用小时'
                                },
                                xAxis: {
                                    categories: ['华电', '华能', '大唐', '国电', '中电投'],
                                    labels: {
                                        align: 'center',
                                        style: {
                                            fontSize: '13px',
                                            fontFamily: 'Verdana, sans-serif'
                                        }
                                    }
                                },
                                yAxis: {
                                    min: 400,
                                    title: {
                                        text: '小时'
                                    }
                                },
                                legend: {
                                    enabled: false
                                },
                                tooltip: {
                                    formatter: function () {
                                        return '<b>' + this.x + '</b><br/>' +
                        '发电设备利用小时: ' + Highcharts.numberFormat(this.y, 1) +
                        ' 小时';
                                    }
                                },
                                series: [{
                                    name: '小时',
                                    data: [data[1].D_HDALL, data[1].D_HNALL, data[1].D_DTALL, data[1].D_GDALL, data[1].D_ZDTALL],
                                    dataLabels: {
                                        enabled: true,
                                        color: 'blue',
                                        align: 'center',
                                        x: 4,
                                        y: 1,
                                        style: {
                                            fontSize: '13px',
                                            fontFamily: 'Verdana, sans-serif'
                                        }
                                    }
                                }]
                            });
                        })
                    })(jQuery);

                    //供电煤耗
                    (function ($) {
                        $(function () {
                            $('#consume').highcharts({
                                chart: {
                                    type: 'column',
                                    margin: [50, 50, 100, 80]
                                },
                                title: {
                                    text: '供电煤耗'
                                },
                                xAxis: {
                                    categories: ['华电', '华能', '大唐', '国电', '中电投'],
                                    labels: {
                                        align: 'center',
                                        style: {
                                            fontSize: '13px',
                                            fontFamily: 'Verdana, sans-serif'
                                        }
                                    }
                                },
                                yAxis: {
                                    min: 300,
                                    title: {
                                        text: '克/千瓦时'
                                    }
                                },
                                legend: {
                                    enabled: false
                                },
                                tooltip: {
                                    formatter: function () {
                                        return '<b>' + this.x + '</b><br/>' +
                        '供电煤耗: ' + Highcharts.numberFormat(this.y, 1) +
                        ' 克/千瓦时';
                                    }
                                },
                                series: [{
                                    name: '克/千瓦时',
                                    data: [data[2].D_HDALL, data[2].D_HNALL, data[2].D_DTALL, data[2].D_GDALL, data[2].D_ZDTALL],
                                    dataLabels: {
                                        enabled: true,
                                        color: 'blue',
                                        align: 'center',
                                        x: 4,
                                        y: 1,
                                        style: {
                                            fontSize: '13px',
                                            fontFamily: 'Verdana, sans-serif'
                                        }
                                    }
                                }]
                            });
                        })
                    })(jQuery);

                    //厂用电率
                    (function ($) {
                        $(function () {
                            $('#changyong').highcharts({
                                chart: {
                                    type: 'column',
                                    margin: [50, 50, 100, 80]
                                },
                                title: {
                                    text: '厂用电率'
                                },
                                xAxis: {
                                    categories: ['华电', '华能', '大唐', '国电', '中电投'],
                                    labels: {
                                        align: 'center',
                                        style: {
                                            fontSize: '13px',
                                            fontFamily: 'Verdana, sans-serif'
                                        }
                                    }
                                },
                                yAxis: {
                                    min: 0,
                                    title: {
                                        text: '%'
                                    }
                                },
                                legend: {
                                    enabled: false
                                },
                                tooltip: {
                                    formatter: function () {
                                        return '<b>' + this.x + '</b><br/>' +
                        '厂用电率: ' + Highcharts.numberFormat(this.y, 1) +
                        '%';
                                    }
                                },
                                series: [{
                                    name: '%',
                                    data: [data[3].D_HDALL, data[3].D_HNALL, data[3].D_DTALL, data[3].D_GDALL, data[3].D_ZDTALL],
                                    dataLabels: {
                                        enabled: true,
                                        color: 'blue',
                                        align: 'center',
                                        x: 4,
                                        y: 1,
                                        style: {
                                            fontSize: '13px',
                                            fontFamily: 'Verdana, sans-serif'
                                        }
                                    }
                                }]
                            });
                        })
                    })(jQuery);
                },
//                error: function (x, e) {
//                    alert(x.responseText);
//                },
                complete: function () {
                    //Handle the complete event
                }
            });
        };
         
    </script>
</head>
<body style="background-color: #E3EDF8; font-size: 12px; font-family: 宋体">
    <form id="form1" runat="server">
    <span style="font-size: 30px; margin-left: 30%; font-weight: bolder; color: Blue;">火力发电企业生产统计信息</span>
    <hr style="color: Blue" />
    <div style="margin: 0 1em">
      
        <div style="float: left; margin-left: 5px;">
            时间设置
            <asp:DropDownList ID="ddlType" runat="server">
                <asp:ListItem Value="1">月度累计平均值</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div style="float: left; margin-left: 5px;">
            <input class="Wdate" id="txtTimeBegin" runat="server" type="text" readonly="readonly"
                onclick="WdatePicker({dateFmt:'yyyy-MM'})"  /></div>
        <div style="float: left; margin-left: 5px;">
            <asp:Button ID="btnSearch" runat="server" OnClientClick="initChart()" Text="查询" /></div>
        <br />
        <br />
        <table>
            <tr>
                <td>
                    <div id="container" style="min-width: 500px; height: 400px; margin: 0 auto">
                    </div>
                </td>
                <td>
                    <div id="useTime" style="min-width: 500px; height: 400px; margin: 0 auto; margin-left: 100px;">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <div id="consume" style="min-width: 500px; height: 400px; margin: 0 auto;">
                    </div>
                </td>
                <td>
                    <div id="changyong" style="min-width: 500px; height: 400px; margin: 0 auto; margin-left: 100px;">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
