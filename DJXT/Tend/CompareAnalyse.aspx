<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompareAnalyse.aspx.cs"
    Inherits="DJXT.Tend.CompareAnalyse" %>

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
    <script src="../jQueryEasyUI/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
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
    <script type="text/javascript" language="javascript">
        var chart1;
        $(function () {
            //function aa(){
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
                exporting: {
                    enabled: false //用来设置是否显示‘打印’,'导出'等功能按钮，不设置时默认为显示 
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
        function query() {
            var rating, rat_data;
            var flag = true;
            if ($('input:radio[id="rd_rating6"]').is(':checked')) {
                rating = $("input[id='rd_rating6']:checked").val() + ";";
                rat_data = $("input[id='rd_rating6']:checked").val() + ";";
            }
            else if ($('input:radio[id="rd_rating7"]').is(':checked')) {
                rating = $("input[id='rd_rating7']:checked").val() + ";";
                rat_data = $("input[id='rd_rating7']:checked").val() + ";";
            }
            else if ($('input:radio[id="rd_rating8"]').is(':checked')) {
                rating = $("input[id='rd_rating8']:checked").val() + ";";
                rat_data = $("input[id='rd_rating8']:checked").val() + ";";
            }
            else if ($('input:radio[id="rd_rating_x"]').is(':checked')) {
                if ($("#txt_rating_x").val() != "") {
                    if ((parseInt($("#txt_rating_x").val()) > 0) && (parseInt($("#txt_rating_x").val()) <= 100)) {
                        rating = $("#txt_rating_x").val() + ";";
                        rat_data = $("#txt_rating_x").val() + ";";
                    }
                    else {
                        alert("请输入小于100的整数！");
                        flag = false;
                    }
                }
                else {
                    alert("请输入自定义负荷！");
                    flag = false;
                }
            }
            if (($("#stime").val() != "") && ($("#etime").val() != "") && (flag == true)) {
                var sTime = new Date($("#stime").val().replace(/-/g, "/")); //开始时间
                var eTime = new Date($("#etime").val().replace(/-/g, "/")); //结束时间
                if (parseInt((eTime.getTime() - sTime.getTime()) / parseInt(1000 * 3600)) > 1) {
                    rating += $("#stime").val() + "," + $("#etime").val() + ";";
                    rat_data += $("#stime").val() + "," + $("#etime").val() + ";";
                }
                else {
                    alert("时间间隔过小，无法呈现曲线！");
                    flag = false;
                }
            }
            else if (flag == true) {

                alert("请选择确定时间！");
                flag = false;
            }
            var aa = ""
            if (($("#div_point_name :checkbox:checked").length > 0) && (flag == true)) {
                $("#sec_choose_curve").empty();
                for (var i = 0; i < $("#div_point_name :checkbox:checked").length; i++) {

                    rating += $("#div_point_name :checkbox:checked")[i].value + ",";
                    aa += "<option value=" + $("#div_point_name :checkbox:checked")[i].value + ">" + $("#div_point_name :checkbox:checked")[i].name + "</option>";
                }
                //aa += $(this).val() + "," + $(this).text();
                rating += ";";
                $("#sec_choose_curve").append(aa);
                $("#sec_choose_curve").attr("enabled", false);
                rat_data += "Pel,";
                for (var j = 0; j < $("#div_point_name :checkbox").length; j++) {

                    rat_data += $("#div_point_name :checkbox")[j].value + ",";
                }
                rat_data += ";";
            }
            else if (flag == true) {
                alert("请选择要呈现的曲线！");
                flag = false;
            }

            if (flag == true) {
                rating += $("#sec_crew  option:selected").val();
                rat_data += $("#sec_crew  option:selected").val();
                var arr = new Array();
                var str = new Array();
                $.post("CompareAnalyse.aspx", { rating: rating }, function (data) {
                    getLine(data);
                    $("#hf_value").attr("value", data.min_data + "|" + data.max_data);
                    $("#txt_min_value").attr("value", data.min_data.split(',')[0]);
                    $("#txt_max_value").attr("value", data.max_data.split(',')[0]);
                }, 'json');
                GridSta(rat_data);
            }
            else {
                getLine();
            }
        }

        function getLine(list) {
            var highchartsOptions = Highcharts.setOptions(Highcharts.theme);
            if (list != null) {
                chart = new Highcharts.Chart({
                    chart: {
                        renderTo: 'container',
                        type: 'spline',
                        zoomType: 'x'
                    },
                    title: {
                        text: list.title
                    },
                    xAxis: {
                        type: 'datetime',
                        labels: { formatter: function () {
                            //return Highcharts.dateFormat('%H:%M:%S', this.value); 
                            var vDate = new Date(this.value * 1000);
                            return vDate.getHours() + ":" + vDate.getMinutes() + ":" + vDate.getSeconds();

                        }
                        }
                    },
                    exporting: {
                        enabled: false //用来设置是否显示‘打印’,'导出'等功能按钮，不设置时默认为显示 
                    },
                    colors: ['#058DC7', '#50B432', '#ED561B', '#DDDF00', '#24CBE5', '#64E572', '#FF9655', '#FFF263', '#6AF9C4'],
                    yAxis: list.y_data,
                    tooltip: {
                        formatter: function () {
                            var vDate = new Date(this.x * 1000);
                            //vDate.getHours() + ":" + vDate.getMinutes() + ":" + vDate.getSeconds()
                            return '<b>' + this.series.name + '</b><br/>时间:' +
                                formatDate(vDate) + '</b><br/>数据:' + this.y;
                        }
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
        function show() {
            if (document.getElementById("div_hide").style.display == "block") {
                document.getElementById("div_hide").style.display = "none"; //block
            }
            else {
                document.getElementById("div_hide").style.display = "block"; //block
            }
        }

        function Load_Rating() {
            if ($('input:radio[id="rd_rating_x"]').is(':checked')) {
                document.getElementById("td_rating_x").style.display = "block"; //block
            }
            else {
                document.getElementById("td_rating_x").style.display = "none"; //block
            }
        }

        function change_company() {
            var par = $("#sec_company").find("option:selected").val();
            $.post(
        "../datafile/Get_Chart_Data.aspx",
        {
            sec_type: par
        },
    function (data) {
        var array = new Array();
        array = data.split('|');
        $("#sec_electric").empty();
        $("#sec_crew").empty();
        if (data == "") {
            $("#sec_electric").append("<option value=请选择>-请选择-</option>");
            $("#sec_crew").append("<option value=请选择>-请选择-</option>");
        }
        else {
            $("#sec_electric").append(array[0]);
            $("#sec_crew").append(array[1]);
        }
    },

    "html");
        }
        function change_electric() {
            var par = $("#sec_electric").find("option:selected").val();
            $.post(
                    "../DataFile/Get_Chart_Data.aspx",
                    {
                        electric_id: par
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
                        $("#div_point_name").html(array[1]);
                    }
                },

                "html");
        }

        function change_crew() {
            var par = $("#sec_crew").find("option:selected").val();
            $.post(
                    "../DataFile/Get_Chart_Data.aspx",
                    {
                        crew_id: par
                    },
                function (data) {
                    var array = new Array();
                    if (data == "") {
                        $("#div_point_name").html('');
                    }
                    else {
                        $("#div_point_name").html(data);
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

        function GridSta(list) {
            $('#gridItem').datagrid({
                nowrap: true,
                autoRowHeight: false,
                fitColumns: true,
                striped: true,
                align: 'center',
                loadMsg: "正在努力为您加载数据", //加载数据时向用户展示的语句
                collapsible: true,
                url: 'CompareAnalyse.aspx',
                sortName: 'T_DATETIME',
                sortOrder: 'asc',
                remoteSort: false,
                queryParams: { param: list },
                idField: 'T_DATETIME',
                columns: [[
                    { field: 'Pel', title: '机组负荷', width: $(window).width() * 0.2 * 0.98, align: 'center' },
                     { field: 'q_fd', title: '热耗率', width: $(window).width() * 0.2 * 0.98, align: 'center' },
                     { field: 'b_g', title: '供电煤耗', width: $(window).width() * 0.2 * 0.98, align: 'center' },
                    { field: 'Eta_b', title: '锅炉效率', width: $(window).width() * 0.2 * 0.98, align: 'center' },
                    { field: 'Eta_H', title: '高压缸效率', width: $(window).width() * 0.2 * 0.98, align: 'center' },
                    { field: 'Eta_M', title: '中压缸效率', width: $(window).width() * 0.2 * 0.98, align: 'center' },
                    { field: 'Rho', title: '厂用电率', width: $(window).width() * 0.2 * 0.98, align: 'center' },
                    { field: 'T_DATETIME', title: '时间', width: $(window).width() * 0.2 * 0.98, align: 'center' }
                ]],
                pagination: true,
                rownumbers: true
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="menu">
        <div id="div_hide" style="display: block">
            <table width="100%" height="92%" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
                class="style5">
                <tr>
                    <td background="../img/table-head.jpg" height="30px" valign="middle" class="style6">
                        &nbsp;&nbsp;趋势查询-关键数据
                    </td>
                </tr>
                <tr>
                    <td class="style5">
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
                                    <select id="sec_crew" runat="server" onchange="change_crew()">
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
                                    时间设置
                                </td>
                                <td id="designation_time">
                                    <input id="stime" class="Wdate" style="text-align: center;" runat="server" readonly="readonly"
                                        type="text" onclick="WdatePicker({maxDate:'#F{$dp.$D(\'etime\')||\'2020-10-01\'}',skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})" />&nbsp;
                                    <input id="etime" class="Wdate" style="text-align: center;" runat="server" readonly="readonly"
                                        type="text" onclick="WdatePicker({maxDate:'#F{$dp.$D(\'etime\')||\'2020-10-01\'}',skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})" />
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
                                    曲线选择
                                </td>
                                <td>
                                    <div id="div_point_name" runat="Server">
                                    </div>
                                    <%--<asp:CheckBoxList ID="cbl_data_point" runat="server" RepeatDirection="Horizontal">
                    </asp:CheckBoxList>--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <input type="button" id="btn_query" value="重新选择" onclick="query()" /><br />
                        <br />
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-left: 50%">
            <img src="../img/jiantou.png" onclick="show(1)" />
        </div>
        <div id="container" style="min-width: 400px; height: 400px; margin: 400px 0px 0px 0px auto">
        </div>
        <table class="style5">
            <tr>
                <td>
                    选择曲线&nbsp;
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
        <div>
            <table id="gridItem">
            </table>
        </div>
        <asp:HiddenField ID="hf_value" runat="server" />
    </div>
    </form>
</body>
</html>
