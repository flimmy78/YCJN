<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyConsume.aspx.cs"
    Inherits="DJXT.StatisticalComparison.CompanyConsume" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>集团公司耗差指标分析</title>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../css/My97DatePicker/WdatePicker.js"></script>
    <%--<script type="text/javascript" src="../jQueryEasyUI/jquery-1.6.2.js"></script>--%>
    <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/highcharts.js"></script>
    <script type="text/javascript" src="../js/exporting.js" charset="utf-8"></script>
    <script type="text/javascript">

        //时间设置 改变事件
        function change(ddl) {
            if ($(ddl).val() == 1) {
                $("#txtTimeBegin").attr("style", "display:block");
                $("#ddlQuarter").attr("style", "display:none");
                $("#txtTimeBegin").attr("value", "");
                $("#txtTimeBegin").attr("onclick", "WdatePicker({dateFmt:'yyyy-MM'})");
            }
            else if ($(ddl).val() == 2) {
                $("#txtTimeBegin").attr("style", "display:block");
                $("#ddlQuarter").attr("style", "display:block");
                $("#txtTimeBegin").attr("value", "");
                $("#txtTimeBegin").attr("onclick", "WdatePicker({dateFmt:'yyyy'})");
            }
            else if ($(ddl).val() == 3) {
                $("#txtTimeBegin").attr("style", "display:block");
                $("#ddlQuarter").attr("style", "display:none");
                $("#txtTimeBegin").attr("value", "");
                $("#txtTimeBegin").attr("onclick", "WdatePicker({dateFmt:'yyyy'})");
            }
        };

      
    </script>
    <style type="text/css">
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
        
        .grid-head
        {
            font-size: 12px;
            font-weight: bold;
            color: White;
            background-image: url(../img/footer.jpg);
            text-align: center;
            vertical-align: middle;
        }
    </style>
    <script type="text/javascript">

        var ihight;
        $(document).ready(function () {
            //ihight = pageHeight();
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
//        $.fn.extend({
//            /**
//            * 修改DataGrid对象的默认大小，以适应页面宽度。
//            *  
//            * @param heightMargin
//            *            高度对页内边距的距离。
//            * @param widthMargin
//            *            宽度对页内边距的距离。
//            * @param minHeight
//            *            最小高度。
//            * @param minWidth
//            *            最小宽度。
//            *  
//            */
//            resizeDataGrid: function (heightMargin, widthMargin, minHeight, minWidth) {
//                var height = $(document.body).height() - heightMargin;
//                var width = $(document.body).width() - widthMargin;

//                height = height < minHeight ? minHeight : height;
//                width = width < minWidth ? minWidth : width;

//                $(this).datagrid('resize', {
//                    height: height,
//                    width: width
//                });
//            }
//        });

//        $(function () {
//            //datagrid数据表格ID
//            var datagridId = 'GridItems';

//            // 第一次加载时自动变化大小  
//            $('#' + datagridId).resizeDataGrid(20, 60, 600, 800);

//            // 当窗口大小发生变化时，调整DataGrid的大小  
//            $(window).resize(function () {
//                $('#' + datagridId).resizeDataGrid(20, 60, 600, 800);
//            });
//        });
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

            var type = $("#ddlType").val();
            var quertertype = $("#ddlQuarter").val();
            var timeMonth = $("#txtMonth").val();
            var timeYear = $("#txtYear").val();

            var begintime;
            var endtime;
            var quertType;

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
                url: "../Handles/StatisticalComparison/CompanyChartConsume.ashx?beginTime=" + begintime + "&timeType=" + type + "&quarterType=" + quertType,
                contentType: "application/json; charset=utf-8",
                type: "GET",
                success: function (data) {
                    //alert(data.ZhuTu);
                    //alert(data.ZhuTu.name);
                    //alert(data.ZhuTu.value);
                    //                    alert(data.Hb[0].time);
                    //                    alert(data.Hb[1].value);
                    //                    alert(data.Hb[2].value);

                    (function ($) {
                        $(function () {
                            $('#bt').highcharts({
                                chart: {
                                    type: 'column'
                                },
                                title: {
                                    text: '华电集团耗差平均值'
                                },
                                xAxis: {
                                    categories: data.ZhuTu.name,
                                    labels: {
                                        rotation: -45
                                    },
                                    style: {
                                        fontSize: '13px',
                                        fontFamily: 'Verdana, sans-serif'
                                    }
                                },
                                yAxis: {
                                    title: {
                                        text: '影响煤耗(g/(KW/h))'
                                    }
                                },
                                credits: {
                                    enabled: false
                                },
                                series: [{ name: data.Hb[0].time, data: data.ZhuTu.value}]
                            });
                        })
                    })(jQuery);

                    //耗差平均值饼图
                    (function ($) {
                        $(function () {
                            $('#divConsume').highcharts({
                                chart: {
                                    plotBackgroundColor: null,
                                    plotBorderWidth: null,
                                    plotShadow: false
                                },
                                title: {
                                    text: '耗差平均值饼图'
                                },
                                tooltip: {
                                    pointFormat: '{series.name}: <b>{point.y}</b>'
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
                                    name: '耗差分析饼图',
                                    data: data.ConsumeList

                                }]
                            });
                        })
                    })(jQuery);

                    //耗差环比增长棒状图
                    (function ($) {
                        $(function () {
                            $('#ConsumeBt').highcharts({
                                chart: {
                                    type: 'column'
                                },
                                title: {
                                    text: '耗差分析棒状图'
                                },
                                xAxis: {
                                    categories: data.Hb[0].name,
                                    labels: {
                                        rotation: -45
                                    },
                                    style: {
                                        fontSize: '13px',
                                        fontFamily: 'Verdana, sans-serif'
                                    }
                                },
                                yAxis: {
                                    title: {
                                        text: '耗差量(g/(KW/h))'
                                    }
                                },
                                credits: {
                                    enabled: false
                                },
                                series: [{ name: data.Hb[0].time, data: data.Hb[0].value }, { name: data.Hb[1].time, data: data.Hb[1].value }, { name: data.Hb[2].time, data: data.Hb[2].value }


                                ],
                                dataLabels: {
                                    enabled: true,
                                    color: 'blue',
                                    align: 'center',
                                    x: 4,
                                    y: 1,
                                    style: {
                                        fontSize: '15px',
                                        fontFamily: 'Verdana, sans-serif'
                                    }
                                }
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
        function query() {
            var type = $("#ddlType").val();
            var quertertype = $("#ddlQuarter").val();
            var timeMonth = $("#txtMonth").val();
            var timeYear = $("#txtYear").val();

            var begintime;
            var quertType;

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

            var params = { param: 'query', beginTime: begintime, timeType: type, quarterType: quertType };
            var url = "CompanyConsume.aspx";
            $.post(url, params, showGrid, "json");
        }

        function showGrid(data) {
            if (data.rows.length == 0) {
                $.messager.alert("结果", "没有数据!", "info", null);
            }
            var options = {
                width: "auto",
                rownumbers: false,
                 fitColumns :false,
             collapsible : true,
             nowrap :true,
             autoRowHeight :false,
             striped :true
            };
            options.columns = eval(data.columns); //把返回的数组字符串转为对象，并赋于datagrid的column属性  
            var dataGrid = $("#GridItems");
            dataGrid.datagrid(options); //根据配置选项，生成datagrid
            //dataGrid._outerWidth(100);
           
            //dataGrid.height = 200;
            dataGrid.datagrid("loadData", data.rows); //载入本地json格式的数据  
        }

//        function pageHeight() {
//            if ($.browser.msie) {
//                return document.compatMode == "CSS1Compat" ? document.documentElement.clientHeight :
//            document.body.clientHeight;
//            } else {
//                return self.innerHeight;
//            }
//        };

//        function pageWidth() {
//            if ($.browser.msie) {
//                return document.compatMode == "CSS1Compat" ? document.documentElement.clientWidth :
//            document.body.clientWidth;
//            } else {
//                return self.innerWidth;
//            }
//        };
        function messages() {
            query();

            initChart();
        };
        //设置滚动条位置
        function Scroll(y) {
            initChart();
            window.scrollTo(0, y);
        }
    </script>
</head>
<body style="background-color: #E3EDF8; font-size: 12px; font-family: 宋体">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 1000px; margin: 0 auto;">
                <table height="25" border="0" cellpadding="0" cellspacing="0" width="100%" bgcolor="#e5f1f4"
                    style="border-bottom: 2px solid #48BADB;">
                    <tr>
                        <td>
                            <div align="left" class="title">
                                集团公司耗差指标分析</div>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="4">
                    <tr>
                        <td>
                            时间设置
                            <asp:DropDownList ID="ddlType" runat="server" onchange="change(this)">
                                <asp:ListItem Value="1">月度平均值</asp:ListItem>
                                <asp:ListItem Value="2">季度平均值</asp:ListItem>
                                <asp:ListItem Value="3">年度平均值</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <div style="float: left; margin-left: 5px;">
                                           
                                            <input class="Wdate" id="txtMonth" runat="server" type="text" readonly="readonly"
                                                onclick="WdatePicker({dateFmt:'yyyy-MM'})" />
                                            <input class="Wdate" id="txtYear" runat="server" type="text" readonly="readonly"
                                                onclick="WdatePicker({dateFmt:'yyyy'})" style="display: none" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlQuarter" runat="server" Style="display: none">
                                            <asp:ListItem Value="0">一季度</asp:ListItem>
                                            <asp:ListItem Value="1">二季度</asp:ListItem>
                                            <asp:ListItem Value="2">三季度</asp:ListItem>
                                            <asp:ListItem Value="3">四季度</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                       <%-- <asp:Button ID="btnSearch" runat="server" OnClick="Search_Click" Text="查询" />--%>
                                        <a id="CX" href="#" class="easyui-linkbutton" onclick="messages();">查&nbsp;&nbsp;询</a><%--  onclick="return CX_onclick()--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="2">
                            <asp:Button ID="Button2" runat="server" Text="导出当前查询" OnClick="Export_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <table id="GridItems" width="1500px">
                </table>
                </br>
                <div id="bt" style="width: 100%; height: 350px; margin: 0 auto">
                </div>
                <br />
                <div id="divConsume" style="width: 100%; height: 500px; margin: 0 auto">
                </div>
                <br />
                <div id="ConsumeBt" style="width: 100%; height: 450px; margin: 0 auto">
                </div>
            </div>
            <asp:HiddenField ID="hColumn" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Button2" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
