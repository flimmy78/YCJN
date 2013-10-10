<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chart.aspx.cs" Inherits="DJXT.ConsumeIndicator.Chart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../css/My97DatePicker/WdatePicker.js"></script>
    <%--<script type="text/javascript" src="../jQueryEasyUI/jquery-1.6.2.js"></script>--%>
    <script type="text/javascript" src="../js/jquery-1.8.0.min.js"></script>
    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/highcharts.js"></script>
    <script type="text/javascript" src="../js/exporting.js" charset="utf-8"></script>
    <style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
            background-color: #e3edf8;
            background-image: url(../img/bg_main.png);
            background-repeat: no-repeat;
            background-position: right 100px;
        }
        .wrapper
        {
            width: 100%;
        }
        .wrap
        {
            width: 100%;
            min-width: 1000px;
            _width: expression((document.documentElement.clientWidth||document.body.clientWidth)<1000? "1000px" : "" );
        }
        
        .sbox
        {
            width: 300px;
            height: 160px;
            float: left;
            margin-right: 1px;
            overflow: hidden;
            width: 50%;
        }
        .sbox a
        {
            text-decoration: none;
        }
        .sbox1
        {
            background-image: url("../img/rb_logo.png");
            float: left;
            height: 300px;
            margin-left: 8%;
            margin-right: 4%;
            margin-top: 4%;
            overflow: hidden;
            position: relative;
            text-align: center;
            width: 300px;
        }
        .sbtn
        {
            position: absolute;
            width: 38px;
            height: 45px;
            cursor: pointer;
        }
        .sbox_img
        {
            width: 100%;
            height: 100%;
            border: 0;
            margin: 0;
            padding: 0;
            cursor: pointer;
        }
        .win
        {
            position: absolute;
            left: 30%;
            top: 180px;
            width: 500px;
            border: #FAF3AB 4px solid;
            display: none;
            background-color: #fcfcfc;
        }
        .white_content
        {
            display: none;
            position: absolute;
            top: 10%;
            left: 20%;
            width: 700px;
            height: 400px;
            border: 10px solid lightblue;
            background-color: white;
        }
        
        .style1
        {
            width: 100%;
        }
        
        .STYLE2
        {
            color: #ff6e47;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
        function opens(types) {
            window.open('ChartDetail.aspx?type=' + types);
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            initChart();
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
            //var timess = $("#txtTime").val();
            //var unit = $("#ddlUnit").val();
            var beginTime = $("#txtTimeBegin").val();
            //var timeType = $("#ddlType").val();
            //var quarterType = $("#ddlQuarter").val();
            $.ajax({
                url: "../Handles/ConsumeIndicator/UnitConsume.ashx?beginTime=" + beginTime,
                contentType: "application/json; charset=UTF-8",
                type: "get",

                success: function (data) {
////                    alert(data.SInfo[0].T_120_HJ);
////                    alert(data.SInfo);
//                    alert(data.Minfo[0]);
//                    alert(data.Minfo[1]);
//                    alert(data.Minfo[2]);

                    (function ($) {
                        $(function () {
                            $('#unitConsume').highcharts({
                                chart: {
                                    type: 'column'
                                },
                                title: {
                                    text: '五大集团不同容量最优机组供电煤耗'
                                },
                                xAxis: {
                                    categories: ['华电集团', '华能集团', '大唐集团', '国电集团', '中电投集团']
                                },
                                yAxis: {
                                    min: 200,
                                    title: {
                                        text: '克/千瓦时'
                                    }
                                },
                                credits: {
                                    enabled: false
                                },
                                series: [
                                                    { name: '135MW', data: [data.SInfo[0].T_120_HJ, data.SInfo[1].T_120_HJ, data.SInfo[2].T_120_HJ, data.SInfo[3].T_120_HJ, data.SInfo[4].T_120_HJ] },
                                                    { name: '200MW', data: [data.SInfo[0].T_200_HJ, data.SInfo[1].T_200_HJ, data.SInfo[2].T_200_HJ, data.SInfo[3].T_200_HJ, data.SInfo[4].T_200_HJ] },
                                                    { name: '300MW', data: [data.SInfo[0].T_300_HJ, data.SInfo[1].T_300_HJ, data.SInfo[2].T_300_HJ, data.SInfo[3].T_300_HJ, data.SInfo[4].T_300_HJ] },
                                                    { name: '600MW', data: [data.SInfo[0].T_600_HJ, data.SInfo[1].T_600_HJ, data.SInfo[2].T_600_HJ, data.SInfo[3].T_600_HJ, data.SInfo[4].T_600_HJ] },
                                                    { name: '1000MW', data: [data.SInfo[0].T_900_SL, data.SInfo[1].T_900_SL, data.SInfo[2].T_900_SL, data.SInfo[3].T_900_SL, data.SInfo[4].T_900_SL] }
                                                    ]

                            });
                        })
                    })(jQuery);


                    (function ($) {
                        $(function () {
                            $('#monthConsume').highcharts({
                                chart: {
                                    zoomType: 'xy'
                                },
                                title: {
                                    text: '供电煤耗'
                                },
                                subtitle: {
                                    text: '月线'
                                },
                                xAxis: [{
                                    categories: ['一', '二', '三', '四', '五', '六', '七', '八', '九', '十', '十一', '十二']
                                }],
                                yAxis: [{
                                    labels: {
                                        format: '{value}',
                                        style: {
                                            color: '#89A54E'
                                        }
                                    },

                                    title: {
                                        text: '克/千瓦时',
                                        style: {
                                            color: '#89A54E'
                                        }
                                    }
                                }, {
                                    title: {
                                        text: '2013',
                                        style: {
                                            color: '#4572A7'
                                        },
                                        min: 300
                                    },
                                    labels: {
                                        format: '{value} ',
                                        style: {
                                            color: '#4572A7'
                                        }
                                    },
                                    min: 300,
                                    opposite: true
                                }],
                                tooltip: {
                                    shared: true
                                },
                                legend: {
                                    layout: 'vertical',
                                    align: 'left',
                                    x: 120,
                                    verticalAlign: 'top',
                                    y: 100,
                                    floating: true,
                                    backgroundColor: '#FFFFFF'
                                },
                                series: [{
                                    name: '2013',
                                    color: '#4572A7',
                                    type: 'column',
                                    yAxis: 1,
                                    data: data.Minfo[2],
                                    tooltip: {
                                        valueSuffix: '克/千瓦时'
                                    }

                                }, {
                                    name: '2012',
                                    color: '#89A54E',

                                    type: 'spline',
                                    data: data.Minfo[1],
                                    tooltip: {
                                        valueSuffix: '克/千瓦时'
                                    }
                                }, {
                                    name: '2011',
                                    color: 'blue',
                                    type: 'spline',
                                    data: data.Minfo[0],
                                    tooltip: {
                                        valueSuffix: '克/千瓦时'
                                    }
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

        function pageHeight() {
            if ($.browser.msie) {
                return document.compatMode == "CSS1Compat" ? document.documentElement.clientHeight :
            document.body.clientHeight;
            } else {
                return self.innerHeight;
            }
        };

        function pageWidth() {
            if ($.browser.msie) {
                return document.compatMode == "CSS1Compat" ? document.documentElement.clientWidth :
            document.body.clientWidth;
            } else {
                return self.innerWidth;
            }
        }; 
    </script>
</head>
<body style="background-color: #E3EDF8; font-size: 12px; font-family: 宋体; padding-left: 10px;">
    <form id="form1" runat="server">
    <span style="font-size: 30px; margin-left: 30%; font-weight: bolder; color: Blue;">五大集团能耗指标对比情况</span>
    <hr style="color: Blue" />
    <div style="float: left; margin-left: 5px;">
        时间设置
        <asp:DropDownList ID="ddlType" runat="server">
            <asp:ListItem Value="1">月度累计平均值</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div style="float: left; margin-left: 5px;">
        <input class="Wdate" id="txtTimeBegin" runat="server" type="text" readonly="readonly"
            onclick="WdatePicker({dateFmt:'yyyy-MM'})" /></div>
    <div style="float: left; margin-left: 5px;">
        <%--<asp:Button ID="btnSearch" runat="server" OnClick="Search_Click" Text="查询" />--%>
        <a id="CX" href="#" class="easyui-linkbutton" onclick="initChart();">查&nbsp;&nbsp;询</a><%--  onclick="return CX_onclick()--%>
        
        </div>
    <br />
    <br />
    <br />
    <div>
        <div id="unitConsume" style="width: 50%; float: left;" runat="server">
        </div>
        <div class="sbox1" id="ctrl">
            <div class="sbtn" style="left: 30px; top: 59px;" id="sbtn1" onmouseover="OnBtnOver(1)"
                onmouseout="OnBtnOut(1)" onclick="opens(1)">
            </div>
            <div class="sbtn" style="left: 30px; top: 158px;" id="sbtn2" onmouseover="OnBtnOver(2)"
                onmouseout="OnBtnOut(2)" onclick="opens(2)">
            </div>
            <div class="sbtn" style="left: 116px; top: 206px;" id="sbtn3" onmouseover="OnBtnOver(3)"
                onmouseout="OnBtnOut(3)" onclick="opens(3)">
            </div>
            <div class="sbtn" style="left: 200px; top: 158px;" id="sbtn4" onmouseover="OnBtnOver(4)"
                onmouseout="OnBtnOut(4)" onclick="opens(4)">
            </div>
            <div class="sbtn" style="left: 200px; top: 56px;" id="sbtn5" onmouseover="OnBtnOver(5)"
                onmouseout="OnBtnOut(5)" onclick="opens(5)">
            </div>
            <div class="sbtn" style="left: 116px; top: 5px;" id="sbtn6" onmouseover="OnBtnOver(6)"
                onmouseout="OnBtnOut(6)" onclick="opens(6)">
            </div>
            <div class="sbtn" style="left: 116px; top: 110px;" id="sbtn7" onmouseover="OnBtnOver(7)"
                onmouseout="OnBtnOut(7)">
            </div>
        </div>
    </div>
    <div>
        <div id="monthConsume" style="width: 90%; float: left;" runat="server">
        </div>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">

    function OnBtnOver(no) {
        var ctrl = document.getElementById("ctrl");
        var s = no * 300 + 2;
        ctrl.style.backgroundPosition = "0px -" + s.toString() + "px";
    }
    function OnBtnOut(no) {
        var ctrl = document.getElementById("ctrl");
        var s = no * 300;
        ctrl.style.backgroundPosition = "0 0";
    }
</script>
