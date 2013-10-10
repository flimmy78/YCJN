<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HorizontalComparison.aspx.cs"
    Inherits="DJXT.StatisticalComparison.HorizontalComparison" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../js/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../js/highcharts.js" type="text/javascript"></script>
    <script src="../js/exporting.js" type="text/javascript"></script>
    <script src="../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        var chart1;
        $(document).ready(function () {
            chart1 = new Highcharts.Chart({
                //$('#container').highcharts
                chart: {
                    renderTo: "container",
                    type: 'spline'
                },
                exporting: {
                    enabled: false //用来设置是否显示‘打印’,'导出'等功能按钮，不设置时默认为显示 
                },
                title: {
                    text: '数据趋势图'
                },
                subtitle: {//副标题
                    text: ''
                },
                xAxis: {
                    type: 'datetime', reversed: true, allowDecimals: false,
                    tickPixelInterval: 50,
                    //reversed: true(颠倒排列顺序), allowDecimals: false(显示为整数)，tickPixelInterval:50(刻度间隔)
                    labels: { formatter: function () {
                        var vDate = new Date(this.value);
                        return vDate.getHours() + ":" + vDate.getMinutes() + ":" + vDate.getSeconds();
                    }
                    }
                },
                yAxis: {
                    title: {
                        text: '百分比 (%)'
                    }
                },
                global: {
                    useUTC: false
                }
            });
            function Hc() {
                var chart;
                Highcharts.theme = {
                    colors: ['#058DC7', '#50B432', '#ED561B', '#DDDF00', '#24CBE5', '#64E572', '#FF9655', '#FFF263', '#6AF9C4'],
                    chart: {
                        backgroundColor: {
                            linearGradient: [0, 0, 500, 500],
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
                    global: { useUTC: false },
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
                    }
                };
            }
            $("input[id='txt_rating_x']").keyup(function () {  //keyup事件处理 
                $(this).val($(this).val().replace(/\D|^0/g, ''));
            }).bind("paste", function () {  //CTR+V事件处理 
                $(this).val($(this).val().replace(/\D|^0/g, ''));
            }).css("ime-mode", "disabled");  //CSS设置输入法不可用
        });

        function Load_Rating() {
            if ($('input:radio[id="rd_rating_x"]').is(':checked')) {
                document.getElementById("td_rating_x").style.display = "block"; //block
            }
            else {
                document.getElementById("td_rating_x").style.display = "none"; //block
            }
        }

        function query() {
            var flag = true;
            if ($("#sec_Capacity  option:selected").val() == "--请选择--") {
                alert("请选择机组容量！");
            }
            else if ($("#sec_UnitType  option:selected").val() == "--请选择--") {
                alert("请选择机组类型！");
            }
            else if ($("#sec_Boiler  option:selected").val() == "--请选择--") {
                alert("请选择锅炉厂家！");
            }
            else if ($("#sec_Steam  option:selected").val() == "--请选择--") {
                alert("请选择汽轮机厂家！");
            }
            else if (($('input:radio[id="rd_rating_x"]').is(':checked')) && (($("#txt_rating_x").val() == "") || (parseInt($("#txt_rating_x").val()) > 95) || (parseInt($("#txt_rating_x").val()) < 40))) {
                alert("请输入正确负荷！");
            }
            else if (($("#txt_stime").val() == "") || ($("#txt_etime").val() == "")) {
                alert("时间不能为空！");
            }
            else {
                var rating = "";
                rating += $("#sec_Capacity  option:selected").val() + "," + $("#sec_UnitType  option:selected").val() + "," + $("#sec_Boiler  option:selected").val() + "," +
                $("#sec_Steam  option:selected").val() + ",";
                if ($('input:radio[id="rd_rating6"]').is(':checked')) {
                    rating += $("input[id='rd_rating6']:checked").val() + ",";
                }
                else if ($('input:radio[id="rd_rating7"]').is(':checked')) {
                    rating += $("input[id='rd_rating7']:checked").val() + ",";
                }
                else if ($('input:radio[id="rd_rating8"]').is(':checked')) {
                    rating += $("input[id='rd_rating8']:checked").val() + ",";
                }
                else if ($('input:radio[id="rd_rating_x"]').is(':checked')) {
                    rating += $("#txt_rating_x").val() + ",";
                }
                rating += $("#txt_stime").val() + ";" + $("#txt_etime").val() + ",";
                if ($('input:radio[id="rd_one"]').is(':checked')) {
                    rating += $("input[id='rd_one']:checked").val() + ",";
                }
                else if ($('input:radio[id="rd_two"]').is(':checked')) {
                    rating += $("input[id='rd_two']:checked").val() + ",";
                }
                else if ($('input:radio[id="rd_three"]').is(':checked')) {
                    rating += $("input[id='rd_three']:checked").val() + ",";
                }
                else if ($('input:radio[id="rd_four"]').is(':checked')) {
                    rating += $("#rd_four").val() + ",";
                }
                else if ($('input:radio[id="rd_five"]').is(':checked')) {
                    rating += $("#rd_five").val() + ",";
                }
                else if ($('input:radio[id="rd_six"]').is(':checked')) {
                    rating += $("#rd_six").val() + ",";
                }
                else if ($('input:radio[id="rd_seven"]').is(':checked')) {
                    rating += $("#rd_seven").val() + ",";
                }

                $.post("HorizontalComparison.aspx", { rating: rating }, function (data) {
                    getLine(data);
                }, 'json');
            }
        }

        function getLine(list) {
            var highchartsOptions = Highcharts.setOptions(Highcharts.theme);
            if (list != null) {
                chart = new Highcharts.Chart({
                    chart: {
                        renderTo: 'container',
                        type: 'column'
                    },
                    title: {
                        text: list.title
                    },
                    xAxis: {
                        categories: list.x_data
                }, 
                exporting: {
                    enabled: false //用来设置是否显示‘打印’,'导出'等功能按钮，不设置时默认为显示 
                },
                yAxis: {
                
                },
                tooltip: {
                    formatter: function () {
                        return '<b>' + this.series.name + '</b><br/>数据:' + this.y;
                    }
                },
                plotOptions: {
                    column: {
                        pointPadding: 0.45,
                        borderWidth: 0
                    }

                },
                series: list.list
            });
        }
        else {
            chart = new Highcharts.Chart({
                chart: {
                    renderTo: 'container',
                    type: 'spline'
                },
                title: {
                    text: '趋势呈现数据图'
                },
                xAxis: {
                    type: 'datetime'
                },
                yAxis: {
                    title: {
                        text: ''
                    },
                    min: 0
                }
            });
        }
    }
    </script>
</head>
<body style="background-color: #E3EDF8; font-size: 12px; font-family: 宋体">
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellspacing="0" cellpadding="2">
            <tr>
                <td background="../img/table-head.jpg" height="30px" valign="middle" class="style6">
                    &nbsp;&nbsp;横向对比查询
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                机组容量&nbsp;
                            </td>
                            <td>
                                <select id="sec_Capacity" runat="server">
                                </select>
                            </td>
                            <td>
                                机组类型&nbsp;
                            </td>
                            <td>
                                <select id="sec_UnitType" runat="server">
                                </select>
                                &nbsp;
                            </td>
                            <td>
                                锅炉厂家&nbsp;
                            </td>
                            <td>
                                <select id="sec_Boiler" runat="server">
                                </select>&nbsp;
                            </td>
                            <td>
                                汽机厂家&nbsp;
                            </td>
                            <td>
                                <select id="sec_Steam" runat="server">
                                </select>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="load_rating" style="height: 50px" class="style5">
                        <tr>
                            <td>
                                负荷选择&nbsp;
                            </td>
                            <td>
                                <input type="radio" id="rd_rating6" name="check" value="60" checked="checked" onclick="Load_Rating()" />60%额定负荷&nbsp;
                            </td>
                            <td>
                                <input type="radio" id="rd_rating7" name="check" value="70" onclick="Load_Rating()" />70%额定负荷&nbsp;
                            </td>
                            <td>
                                <input type="radio" id="rd_rating8" name="check" value="80" onclick="Load_Rating()" />80%额定负荷&nbsp;
                            </td>
                            <td>
                                <input type="radio" id="rd_rating_x" name="check" onclick="Load_Rating()" />自定义负荷&nbsp;
                            </td>
                            <td style="display: none" id="td_rating_x">
                                <input id="txt_rating_x" type="text" maxlength="3" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                时间设置&nbsp;
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
                            <td>
                                月平均值比较
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                参数选择&nbsp;
                            </td>
                            <td>
                                <input type="radio" id="rd_one" name="gongshi" checked="checked" value="b_g" />供电煤耗
                            </td>
                            <td>
                                <input type="radio" id="rd_two" name="gongshi" value="b_fd" />发电煤耗
                            </td>
                            <td>
                                <input type="radio" id="rd_three" name="gongshi" value="Rho" />厂用电率
                            </td>
                            <td>
                                <input type="radio" id="rd_four" name="gongshi" value="q_fd" />热耗率
                            </td>
                            <td>
                                <input type="radio" id="rd_five" name="gongshi" value="Eta_b" />锅炉效率
                            </td>
                            <td>
                                <input type="radio" id="rd_six" name="gongshi" value="Eta_H" />高压缸效率
                            </td>
                            <td>
                                <input type="radio" id="rd_seven" name="gongshi" value="Eta_M" />中压缸效率
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <a id="btn_query" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'"
                        onclick="query()">结果查询</a><br />
                </td>
            </tr>
        </table>
    </div>
    <div id="container" style="min-width: 400px; height: 400px; margin: 400px 0px 0px 0px auto">
    </div>
    </form>
</body>
</html>
