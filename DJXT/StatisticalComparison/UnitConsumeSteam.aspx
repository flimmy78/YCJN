<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitConsumeSteam.aspx.cs"
    Inherits="DJXT.StatisticalComparison.UnitConsumeSteam" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../css/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../jQueryEasyUI/jquery-1.6.2.js"></script>
    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/highcharts.js" ></script>
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
            ihight = pageHeight();
          
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
        

        //汽机和锅炉 信息
        function GridSteam() {
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
            $('#steam').datagrid({
                title: '耗差分项列表',
                iconCls: 'icon-search',
                nowrap: true,
                autoRowHeight: false,
                striped: true,
                //height: ihight - 130,
                align: 'center',
                loadMsg: '查询中,请稍等...',
                collapsible: true,
                url: '../Handles/StatisticalComparison/UnitConsumeSteam.ashx',
                //sortName: 'ID_KEY',
                sortOrder: 'asc',
                remoteSort: false,
                queryParams: { param: 'searchList', unit: $("#ddlUnit").val(), beginTime: begintime, endTime: endtime, timeType: type, quarterType: quertType },
                //idField: 'T_CODE',
                frozenColumns: [[
                { field: 'ck', checkbox: true }
			    ]],
                columns: [[
				    { field: 'Name', title: '指标名称', width: 100, align: 'center' },
				    { field: 'StandardValue', title: '基准值', width: 75, align: 'center' },
				    { field: 'RealValue', title: '实际值', width: 75, align: 'center' },
				    { field: 'ConsumeValue', title: '耗差值', width: 75, align: 'center' }
			    ]],
                pagination: false,
                rownumbers: false
            });
        }

        var initChart = function () {
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
                url: "../Handles/StatisticalComparison/EnergyLossIndicator.ashx?unit=" + $("#ddlUnit").val() + "&beginTime=" + begintime + "&endTime=" + endtime + "&timeType=" + type + "&quarterType=" + quertType,
                contentType: "application/json; charset=utf-8",
                type: "GET",
                success: function (data) {
                    //                    alert(data.ConsumeList);
                    //                    alert(data.name);
                    //                    alert(data.value);
                    (function ($) {
                        $(function () {
                            $('#divConsume').highcharts({
                                chart: {
                                    plotBackgroundColor: null,
                                    plotBorderWidth: null,
                                    plotShadow: false
                                },
                                title: {
                                    text: '耗差分析饼图'
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
                                    categories: data.name,
                                    labels: {
                                        rotation: -45
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
        //总耗差表
        function AllConsume() {
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
            $('#consume').datagrid({
                title: '总耗差表',
                iconCls: 'icon-search',
                nowrap: true,
                autoRowHeight: false,
                striped: true,
                height: ihight - 130,
                align: 'center',
                collapsible: true,
                loadMsg: '查询中,请稍等...',
                url: '../Handles/StatisticalComparison/AllConsume.ashx',
                //sortName: 'ID_KEY',
                sortOrder: 'asc',
                remoteSort: false,
                queryParams: { param: 'searchList', unit: $("#ddlUnit").val(), beginTime: begintime, endTime: endtime, timeType: type, quarterType: quertType },
                //idField: 'T_CODE',
                frozenColumns: [[
                { field: 'ck', checkbox: true }
			    ]],
                columns: [[
				    { field: 'name', title: '指标名称', width: 100, align: 'center' },
				    { field: 'value', title: '耗差值', width: 75, align: 'center' }
			    ]],
                pagination: false,
                rownumbers: false
            });
        }

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
            GridSteam();
            AllConsume();
            initChart();

            //return false;

        }

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
<body style="background-color: #E3EDF8; font-size: 12px; font-family: 宋体">
    <form id="form1" runat="server">
  
            <table height="25" border="0" cellpadding="0" cellspacing="0" width="100%" bgcolor="#e5f1f4"
                style="border-bottom: 2px solid #48BADB;">
                <tr>
                    <td>
                        <div align="left" class="title">
                            机组耗差指标分析</div>
                    </td>
                </tr>
            </table>
              <asp:ToolkitScriptManager ID="ToolkitScriptManager" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
         
            <table width="100%" border="0" cellspacing="0" cellpadding="4">
                <tr>
                    <td>
                        &nbsp;省公司 <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                        &nbsp; &nbsp; &nbsp; &nbsp; 电厂 <asp:DropDownList ID="ddlPlant" runat="server" OnSelectedIndexChanged="ddlPlant_SelectedChanged"
                            AutoPostBack="true">
                            <asp:ListItem Selected="True" Value="0">--请选择电厂--</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp; &nbsp; &nbsp; &nbsp; 机组 <asp:DropDownList ID="ddlUnit" runat="server">
                            <asp:ListItem Selected="True" Value="0">--请选择机组--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
                 </ContentTemplate>
        <Triggers>
          
        </Triggers>
    </asp:UpdatePanel>
            <div style="float: left; margin-left: 5px;">
                时间设置
                <asp:DropDownList ID="ddlType" runat="server"  onchange="change(this)">
                    <asp:ListItem Value="0">指定时间段平均值</asp:ListItem>
                    <asp:ListItem Value="1">月度平均值</asp:ListItem>
                    <asp:ListItem Value="2">季度平均值</asp:ListItem>
                    <asp:ListItem Value="3">年度平均值</asp:ListItem>
                </asp:DropDownList>
            </div>
            &nbsp; &nbsp;
            <div style="float: left; margin-left: 5px;">
                <input class="Wdate" id="txtTimeBegin" runat="server" type="text" readonly="readonly"
                    onclick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss ',maxDate:'%y-%M-%d'})"  />
                     <input class="Wdate" id="txtMonth" runat="server" type="text" readonly="readonly"
                    onclick="WdatePicker({dateFmt:'yyyy-MM'})" style="display: none" />
                     <input class="Wdate" id="txtYear" runat="server" type="text" readonly="readonly"
                    onclick="WdatePicker({dateFmt:'yyyy'})" style="display: none" />
                <span id="zhi" >至</span></div>
            <div style="float: left; margin-left: 5px;">
                <input class="Wdate" id="txtTimeEnd" runat="server" type="text" readonly="readonly"
                    onclick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss ',maxDate:'%y-%M-%d'})"  />
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
               <%-- <asp:Button ID="btnSearch" runat="server" OnClick="Search_Click" Text="查询" OnClientClick="messages();" />--%>
                &nbsp; &nbsp;
                 <a id="CX" href="#" class="easyui-linkbutton" onclick="messages();">查&nbsp;&nbsp;询</a>
                <asp:Button ID="Button2" runat="server" Text="导出当前查询" OnClick="Export_Click" />
            </div>
            <br />
            <br />
            <br />
            <br />
            <table>
                <tr>
                    <td valign="top">
                        <table style="width: 400px; height: 400px; margin: 0 auto">
                            <tr>
                                <td>
                                    <table id="steam">
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="boiler">
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top">
                        <table style="width: 700px; height: 400px; margin: 0 auto">
                            <tr>
                                <td>
                                    <div id="divConsume" style="width: 500px; height: 450px; margin: 0 auto">
                                    </div>
                                </td>
                                <td>
                                    <table id="consume" style="width: 200px; height: 400px; margin: 0 auto">
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div id="bt" style="width: 700px; height: 450px; margin: 0 auto">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
   
    </form>
</body>
</html>
<%--<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridSteam);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(AllConsume);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(initChart);
</script>--%>
