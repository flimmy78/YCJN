<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RealQuery.aspx.cs" Inherits="DJXT.Trend.RealQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/css/djxt.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../js/highcharts.js" type="text/javascript"></script>
    <script src="../js/exporting.js" type="text/javascript"></script>
    <link href="../js/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
    <script src="../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
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
    <script type="text/javascript">
        var data_sb = null; var chart1;
        $(function () {
            chart1 = new Highcharts.Chart({
                //$('#container').highcharts
                chart: {
                    renderTo: "container",
                    type: 'spline'
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
                exporting: {
                    enabled: false //用来设置是否显示‘打印’,'导出'等功能按钮，不设置时默认为显示 
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
        });
        function show() {
            if (document.getElementById("div_hide").style.display == "block") {
                document.getElementById("div_hide").style.display = "none"; //block
            }
            else {
                document.getElementById("div_hide").style.display = "block"; //block
            }
        }

        //控制时间段是否显示或者由动态更新
        function Time_Show(i) {
            if (i == 1) {

                $("#time_show td[id='designation_time']").hide();
            }
            else {
                $("#time_show td[id='designation_time']").show();
            }
        }
        function fuzzy_query(k) {
            if (k != "") {
                var arr = new Array();
                $("#lb_point_name option").each(function () {
                    if ($(this).text().indexOf(k) >= 0) {
                        arr.push($(this).val() + "," + $(this).text());
                        $(this).remove();
                    }
                });
                for (var i = 0; i < arr.length; i++) {
                    $('#lb_point_name').prepend(
        $('<option></option>').attr("text", arr[i].split(',')[0]).html(arr[i].split(',')[1]).attr("value", arr[i].split(',')[0]).html(arr[i].split(',')[1])
    );
                }
            }
        }
        function choose_point_name() {
            if ($("#lb_point_name  option:selected") && ($("#lb_point_name  option:selected").val() != "") && ($("#lb_point_name  option:selected").val() != null)) {
                $("#lb_sec_point").append("<option value=" + $("#lb_point_name  option:selected").val() + ">" + $("#lb_point_name  option:selected").text() + "</option>");
                $("#lb_point_name  option:selected").remove();
            }
        }
        function delete_sec_point() {
            if ($("#lb_sec_point  option:selected") && ($("#lb_sec_point  option:selected").val() != "") && ($("#lb_sec_point  option:selected").val() != null)) {
                $("#lb_point_name").append("<option value=" + $("#lb_sec_point  option:selected").val() + ">" + $("#lb_sec_point  option:selected").text() + "</option>");
                $("#lb_sec_point  option:selected").remove();

            }
        }

        function query() {
            if (($("#lb_sec_point  option").size() > 0) && ($("#lb_sec_point  option").size() <= 6)) {
                // var arr = new Array();


                if (($("#rd_designation").is(':checked') == true) && ($("#stime").val() != "") && ($("#etime").val() != "")) {
                    var string_append = null;
                    var size = $("#lb_sec_point option").length;

                    for (var i = 0; i < size; i++) {
                        if (i == 0) {
                            string_append = $("#lb_sec_point  option")[i].value + "," + $("#lb_sec_point  option")[i].text + ";";
                        }
                        else {
                            string_append += $("#lb_sec_point  option")[i].value + "," + $("#lb_sec_point  option")[i].text + ";";
                        }

                    }
                    //if ($('input[name=rd_dynamic][checked]').length >0) 
                    //arr.push($("#stime").val() + "," + $("#etime").val());
                    string_append += $("#stime").val() + "," + $("#etime").val();
                    $.post("RealQuery.aspx", { real_data: string_append }, function (data) {
                        getLine(data);
                        datagrid_ui();
                        $("#sec_choose_curve").empty();
                        for (var i = 0; i < $("#lb_sec_point  option").length; i++) {
                            $("#sec_choose_curve").append("<option value=" + $("#lb_sec_point  option")[i].value + ">" + $("#lb_sec_point  option")[i].text + "</option>");
                        }
                        $("#hf_value").attr("value", data.min_data + "|" + data.max_data);

                        $("#txt_min_value").attr("value", data.min_data.split(',')[0]);
                        $("#txt_max_value").attr("value", data.max_data.split(',')[0]);
                    }, 'json');
                }
                else if (($("#rd_dynamic").is(':checked') == false)) {
                    alert("请选择有效时间！");
                }
                else {
                    Set_Data();
                    //set_Interval();
                    setInterval("set_Interval()", 10000);
                }


            }
            else if ($("#lb_sec_point  option").size() > 6) {
                alert("要呈现的曲线最多可达六条！");
            }
            else {
                alert("请选择要呈现的曲线！");
            }
        }
        var string_append = null;
        function set_Interval() {

        }

        function Set_Data() {
            var size = $("#lb_sec_point option").length;

            for (var i = 0; i < size; i++) {
                if (i == 0) {
                    string_append = $("#lb_sec_point  option")[i].value + "," + $("#lb_sec_point  option")[i].text + ";";
                }
                else {
                    string_append += $("#lb_sec_point  option")[i].value + "," + $("#lb_sec_point  option")[i].text + ";";
                }

            }
            var currentTime = "";
            var myDate = new Date();
            var year = myDate.getFullYear();
            var month = parseInt(myDate.getMonth().toString()) + 1; //month是从0开始计数的，因此要 + 1
            if (month < 10) {
                month = "0" + month.toString();
            }
            var date = myDate.getDate();
            if (date < 10) {
                date = "0" + date.toString();
            }
            var hour = myDate.getHours();
            if (hour < 10) {
                hour = "0" + hour.toString();
            }
            var hous = myDate.getHours();
            if (hous < 3) {
                hous = "0" + hous.toString(); ;
            }
            else if ((hous > 3) && (hous < 10)) {
                hous = "0" + (myDate.getHours() - 2).toString();
            }
            else {
                hous = (myDate.getHours() - 2).toString();
            }
            var minute = myDate.getMinutes();
            if (minute < 10) {
                minute = "0" + minute.toString();
            }
            var second = myDate.getSeconds();
            if (second < 10) {
                second = "0" + second.toString();
            }
            currentTime = year.toString() + "-" + month.toString() + "-" + date.toString() + " " + hour.toString() + ":" + minute.toString() + ":" + second.toString(); //以时间格式返回
            string_append += currentTime + "," + currentTime;
            $.post("RealQuery.aspx", { real_data: string_append }, function (data) {
                getLine(data);
                datagrid_ui();
                $("#sec_choose_curve").empty();
                for (var i = 0; i < $("#lb_sec_point  option").length; i++) {
                    $("#sec_choose_curve").append("<option value=" + $("#lb_sec_point  option")[i].value + ">" + $("#lb_sec_point  option")[i].text + "</option>");
                }
                $("#hf_value").attr("value", data.min_data + "|" + data.max_data);

                $("#txt_min_value").attr("value", data.min_data.split(',')[0]);
                $("#txt_max_value").attr("value", data.max_data.split(',')[0]);
            }, 'json');
        }

        function getLine(list) {
            var highchartsOptions = Highcharts.setOptions(Highcharts.theme);
            if (list != null) {
                chart = new Highcharts.Chart({
                    chart: {
                        renderTo: 'container',
                        type: 'spline',
                        zoomType: 'x',
                        events: {
                            load: function () {
                                setInterval(function () {
                                    $.post(
                    	    "../datafile/GetRealQueryData.aspx",
                    	    { real_data_real: string_append
                    	    },
                        function (data) {
                            for (var i = 0; i < string_append.split(';').length - 1; i++) {
                                var series = chart.series[i];
                                series.addPoint([parseInt(data.split(';')[i].split(',')[0]), parseFloat(data.split(';')[i].split(',')[1])], true, true);
                            }
                        },

                        "html");
                                }, 10000);
                            }
                        }
                    }, exporting: {
                        enabled: false //用来设置是否显示‘打印’,'导出'等功能按钮，不设置时默认为显示 
                    },
                    title: {
                        text: list.title
                    },
                    xAxis: {
                        type: 'datetime',
                        labels: { formatter: function () {
                            //var vDate = new Date();

                            return Highcharts.dateFormat('%H:%M:%S', this.value);

                            //                            return vDate.getYear()+"-"+vDate.getMonth()+"-"+vDate.getDay()+" "+vDate.getHours() + ":" + vDate.getMinutes() + ":" + vDate.getSeconds();

                        }
                        }
                    },
                    colors: ['#058DC7', '#50B432', '#ED561B', '#DDDF00', '#24CBE5', '#64E572', '#FF9655', '#FFF263', '#6AF9C4'],
                    yAxis: list.y_data
                    ,
                    tooltip: {
                        xDateFormat: '<b>' + '%Y-%m-%d %H:%M:%S' + '</b>',
                        crosshairs: {

                            width: 2,
                            color: 'red'
                        },
                        shared: true
                    },
                    plotOptions: {
                        spline: {
                            lineWidth: 0.4,
                            states: {
                                hover: {
                                    lineWidth: 0.5
                                }
                            },
                            marker: {
                                enabled: false
                            }
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

        function formatDate(now) {
            var year = now.getYear();
            var month = now.getMonth() + 1;
            var date = now.getDate();
            var hour = now.getHours();
            var minute = now.getMinutes();
            var second = now.getSeconds();
            return year + "-" + month + "-" + date + "&nbsp;" + hour + ":" + minute + ":" + second;
        }

        function change_company() {
            var par = $("#sec_company").find("option:selected").val();
            $.post(
        "../datafile/Get_Chart_Data.aspx",
        {
            sec_type_real: par
        },
    function (data) {
        var array = new Array();
        array = data.split('|');
        $("#sec_electric").empty();
        $("#lb_sec_point").empty();

        $("#sec_crew").empty();
        if (data == "") {
            $("#sec_electric").append("<option value=请选择>-请选择-</option>");
            $("#sec_crew").append("<option value=请选择>-请选择-</option>");
        }
        else {
            $("#sec_electric").append(array[0]);
            $("#sec_crew").append(array[1]);
            $("#lb_point_name").empty();
            $("#lb_point_name").append(array[2]);


        }
    },

    "html");
        }
        function change_electric() {
            var par = $("#sec_electric").find("option:selected").val();
            $("#lb_sec_point").empty();
            $.post(
                    "../DataFile/Get_Chart_Data.aspx",
                    {
                        electric_id_real: par
                    },
                function (data) {
                    var array = new Array();
                    array = data.split('|');
                    $("#sec_crew").empty();
                    if (data == "") {
                        $("#sec_crew").append("<option value=请选择>-请选择-</option>");
                    }
                    else {
                        $("#sec_crew").append(array[0]);
                        $("#lb_point_name").empty();
                        $("#lb_point_name").append(array[1]);

                    }
                },

                "html");
        }

        function change_crew() {
            $("#lb_sec_point").empty();
            var par = $("#sec_crew").find("option:selected").val();
            $.post(
                    "../DataFile/Get_Chart_Data.aspx",
                    {
                        crew_id_real: par
                    },
                function (data) {
                    var array = new Array();
                    if (data == "") {
                        $("#lb_point_name").html('');
                    }
                    else {
                        $("#lb_point_name").empty();
                        $("#lb_point_name").append(data);
                    }
                },

                "html");
        }
        function change_curve() {
            if ($("#hf_value").val() != "") {
                $("#txt_min_value").attr("value", $("#hf_value").val().split('|')[0].split(',')[$("#sec_choose_curve :selected:selected")[0].index]);
                $("#txt_max_value").attr("value", $("#hf_value").val().split('|')[1].split(',')[$("#sec_choose_curve :selected:selected")[0].index]);
            }
        }


        function datagrid_ui() {
            $.post("RealQuery.aspx", { param: 'search' }, showGrid, "json");
            //            $('#grid').datagrid({
            //                nowrap: true,
            //                autoRowHeight: false,
            //                fitColumns: true,
            //                striped: true,
            //                align: 'center',
            //                loadMsg: "正在努力为您加载数据", //加载数据时向用户展示的语句
            //                collapsible: true,
            //                url: 'RealQuery.aspx',
            //                remoteSort: false,
            //                queryParams: { param: 'search' }
            //            });
        }
        function showGrid(data) {
            if (data.rows.length == 0) {
                $.messager.alert("结果", "没有数据!", "info", null);
            }
            var options = {
                width: "auto",
                rownumbers: true,
                fitColumns: true,
                striped: true,
                align: 'center',
                loadMsg: "正在努力为您加载数据", //加载数据时向用户展示的语句
                collapsible: true,
                remoteSort: false
            };
            options.columns = eval(data.columns); //把返回的数组字符串转为对象，并赋于datagrid的column属性  
            var dataGrid = $("#grid");
            dataGrid.datagrid(options); //根据配置选项，生成datagrid
            //dataGrid.innerHeight = 520;
            //dataGrid.height = 200;
            dataGrid.datagrid("loadData", data.rows); //载入本地json格式的数据  
        }  

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="menu">
        <div id="div_hide" style="left: 0px; display: block">
            <table width="100%" height="92%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
                <tr>
                    <td background="../img/table-head.jpg" height="30px" valign="middle" class="style6">
                        &nbsp;&nbsp;趋势查询-实时数据
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="style5">
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
                                <td colspan="2">
                                    <select id="sec_crew" runat="server" onchange="change_crew()">
                                    </select>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="7">
                        <table id="time_show" style="height: 50px" class="style5">
                            <tr>
                                <td>
                                    时间设置&nbsp;
                                </td>
                                <td>
                                    <input type="radio" id="rd_dynamic" name="check" value="radiobutton" checked="checked"
                                        onclick="Time_Show(1)" />动态更新&nbsp;
                                </td>
                                <td rowspan="2">
                                    <table width="500px">
                                        <tr>
                                            <td>
                                                <input type="radio" id="rd_designation" name="check" value="radiobutton" onclick="Time_Show(2)" />指定时间段
                                            </td>
                                            <td id="designation_time" style="display: none">
                                                <input id="stime" class="Wdate" style="text-align: center;" runat="server" readonly="readonly"
                                                    type="text" onclick="WdatePicker({maxDate:'#F{$dp.$D(\'etime\')||\'2020-10-01\'}',skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})" />&nbsp;
                                                <input id="etime" class="Wdate" style="text-align: center;" runat="server" readonly="readonly"
                                                    type="text" onclick="WdatePicker({maxDate:'#F{$dp.$D(\'etime\')||\'2020-10-01\'}',skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="border: 1 solid #cccccc">
                        <table>
                            <tr>
                                <td valign="top" class="style5">
                                    <table>
                                        <tr>
                                            <td>
                                                选择数据查询范围
                                            </td>
                                            <td>
                                                输入要查询的测点名称
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                被选的测点
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td>
                                                <select id="sec_data_point">
                                                    <option>全部数据点</option>
                                                </select>
                                            </td>
                                            <td>
                                                <input id="txt_point_name" onkeyup="fuzzy_query(this.value)" />
                                                <br />
                                                <asp:ListBox ID="lb_point_name" runat="server"></asp:ListBox>
                                            </td>
                                            <td>
                                                <a id="btn_sure" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok'"
                                                    onclick="choose_point_name()">确 定</a>
                                                <br />
                                                <a id="btn_delete" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-no'"
                                                    onclick="delete_sec_point()">删 除</a>
                                            </td>
                                            <td>
                                                <asp:ListBox ID="lb_sec_point" runat="server"></asp:ListBox>
                                            </td>
                                            <td>
                                                <a id="btn_query" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'"
                                                    onclick="query()">结果查询</a><br />
                                                <a id="btn_reset" href="RealQuery.aspx" class="easyui-linkbutton" data-options="iconCls:'icon-reload'">
                                                    重 置</a><br />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-left: 30%">
            <img src="../img/jiantou.png" onclick="show(1)" />
        </div>
        <div id="container" style="min-width: 400px; height: 400px; margin: 400px 0px 0px 0px auto">
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    曲线设置：选择曲线&nbsp;
                                </td>
                                <td>
                                    <select id="sec_choose_curve" onchange="change_curve()">
                                        <option>-请选择-</option>
                                    </select>&nbsp;
                                </td>
                                <td>
                                    最小值：&nbsp;
                                </td>
                                <td>
                                    <input id="txt_min_value" type="text" readonly="readonly" />
                                </td>
                                <td>
                                    最大值：&nbsp;
                                </td>
                                <td>
                                    <input id="txt_max_value" type="text" readonly="readonly" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="middle" colspan="6">
                        <table id="grid">
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hf_value" runat="server" />
    </div>
    </form>
</body>
</html>
