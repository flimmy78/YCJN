<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerticalContrastSearch.aspx.cs"
    Inherits="DJXT.StatisticalComparison.VerticalContrastSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>纵向对比查询</title>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../css/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../jQueryEasyUI/jquery-1.6.2.js"></script>
    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/highcharts.js"></script>
    <script type="text/javascript" src="../js/exporting.js" charset="utf-8"></script>
    <style type="text/css">
        .white_content
        {
            display: none;
            position: absolute;
            top: 10%;
            left: 20%;
            width: 800px;
            height: 500px;
            border: 10px solid lightblue;
            background-color: #E3EDF8;
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
        $(document).ready(function () {
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
            var beginTime = $("#txtBeginTime").val();
            var endTime = $("#txtEndTime").val();
            var unitId = $("#ddlUnit").val();
            var paraId = $("input[name='rBtn']:checked").val();
            $.ajax({
                url: "../Handles/StatisticalComparison/VerticalContrastSearch.ashx?beginTime=" + beginTime + "&endTime=" + endTime + "&unitId=" + unitId + "&paraId=" + paraId,
                contentType: "application/json; charset=utf-8",
                type: "GET",

                success: function (data) {
                    //alert(data[0].T_INDICATORNAME);
//                    alert(data.date);
//                    alert(data.value);
//                    alert(data.name);
//                    alert(data.unit);
                    //容量
                    (function ($) {
                        $(function () {
                            $('#container').highcharts({
                                chart: {
                                    type: 'column',
                                    margin: [50, 50, 100, 80]
                                },
                                title: {
                                    text: data.name
                                },
                                xAxis: {
                                    categories: data.date,
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
                                        text: data.unit
                                    }
                                },
                                legend: {
                                    enabled: false
                                },
                                tooltip: {
                                    formatter: function () {
                                        return '<b>' + this.x + '</b><br/>' +
                        data.name + ":" + Highcharts.numberFormat(this.y, 1) +
                        data.unit;
                                    }
                                },
                                series: [{
                                    name: 'Population',
                                    data: data.value,
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

                },
                //                error: function (x, e) {
                //                    alert(x.responseText);
                //                },
                complete: function () {
                    //Handle the complete event
                }
            });
        };
        function messages() {
            if ($("#ddlCompany").val() == "0") {
                alert("请选择公司！");
                return false;
            }
            if ($("#ddlPlant").val() == "0") {
                alert("请选择电厂！");
                return false;
            }
            if ($("#ddlUnit").val() == "0") {
                alert("请选择机组！");
                return false;
            }
            initChart();
            query();
            //return false;

        }
        function RadioClick() {
            if ($("#ddlCompany").val() == "0") {
                alert("请选择公司！");
                return false;
            }
            if ($("#ddlPlant").val() == "0") {
                alert("请选择电厂！");
                return false;
            }
            if ($("#ddlUnit").val() == "0") {
                alert("请选择机组！");
                return false;
            }
            initChart();
            //return false;

        }

        function query() {
            var params = { param: 'query', beginTime: $("#txtBeginTime").val(), endTime:$("#txtEndTime").val(), unitId: $("#ddlUnit").val(), paraId:  $("input[name='rBtn']:checked").val() };
            var url = "VerticalContrastSearch.aspx";
            $.post(url, params, showGrid, "json");
        }

        function showGrid(data) {
            
            if (data.rows.length == 0) {
                $.messager.alert("结果", "没有数据!", "info", null);
            }
            var options = {
                width: "auto",
                rownumbers: true
            };
            options.columns = eval(data.columns); //把返回的数组字符串转为对象，并赋于datagrid的column属性  
            var dataGrid = $("#GridItems");
            dataGrid.datagrid(options); //根据配置选项，生成datagrid
            dataGrid.datagrid("loadData", data.rows); //载入本地json格式的数据  
        }  

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
                    经济指标和设备性能指标查询</div>
            </td>
        </tr>
    </table>
    <div style="width: 100%; height: 100%;">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" runat="server">
        </asp:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%" border="0" cellspacing="0" cellpadding="4">
                    <tr>
                        <td>
                            &nbsp; 省公司<asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedChanged"
                                AutoPostBack="true">
                                <%-- <asp:ListItem Selected="True"  Value="0">--请选择省公司--</asp:ListItem>--%>
                            </asp:DropDownList>
                            &nbsp; &nbsp; &nbsp; &nbsp; 电厂<asp:DropDownList ID="ddlPlant" runat="server" OnSelectedIndexChanged="ddlPlant_SelectedChanged"
                                AutoPostBack="true">
                                <asp:ListItem Selected="True" Value="0">--请选择电厂--</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp; &nbsp; &nbsp; &nbsp; 机组<asp:DropDownList ID="ddlUnit" runat="server" OnSelectedIndexChanged="ddlUnit_SelectedChanged"
                                AutoPostBack="true">
                                <asp:ListItem Selected="True" Value="0">--请选择机组--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            时间段：<input class="Wdate" id="txtBeginTime" runat="server" type="text" onclick="WdatePicker({dateFmt:'yyyy-MM'})" />至
                            <input class="Wdate" id="txtEndTime" runat="server" type="text" onclick="WdatePicker({dateFmt:'yyyy-MM'})" />月平均值比较
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            参数选择<asp:RadioButtonList ID="rBtn" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"  onclick="RadioClick();">
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
        <%--<asp:Button ID="btnSearch" runat="server" OnClick="Search_Click" Text="查询" OnClientClick="messages();" />--%>
        <a id="CX" href="#" class="easyui-linkbutton" onclick="messages();">查&nbsp;&nbsp;询</a>
        <br />
        <div id="container" runat="server" style="width:90%">
        </div>
        <br />
        <div  style="width:90%">
        <table id="GridItems" width="96%">
        </table>
        </div>
    </div>
    </form>
</body>
</html>
