<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnergyLossIndicator.aspx.cs"
    Inherits="DJXT.StatisticalComparison.EnergyLossIndicator" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>机组耗差指标分析</title>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../css/My97DatePicker/WdatePicker.js"></script>
    <%--<script type="text/javascript" src="../jQueryEasyUI/jquery-1.6.2.js"></script>--%>
    <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../js/jquery-1.9.1.min.js"></script>
    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>



    <script type="text/javascript">
        //jQuery.noConflict();

        //时间设置 改变事件
        //        function change(ddl) {
        //            if ($(ddl).val() == 0) {
        //                $("#zhi").attr("style", "display:block");
        //                $("#txtTimeBegin").attr("style", "display:block");
        //                $("#txtTimeEnd").attr("style", "display:block");
        //                $("#ddlQuarter").attr("style", "display:none");

        //                //设置时间样式
        //                $("#txtTimeBegin").attr("value", "");
        //                $("#txtTimeEnd").attr("value", "");
        //                $("#txtTimeBegin").attr("onclick", "WdatePicker({dateFmt:'yyyy-MM-dd HH:mm ',maxDate:'%y-%M-%d'})");
        //                $("#txtTimeEnd").attr("onclick", "WdatePicker({dateFmt:'yyyy-MM-dd HH:mm ',maxDate:'%y-%M-%d'})");

        //            }
        //            else if ($(ddl).val() == 1) {
        //                $("#zhi").attr("style", "display:none");
        //                $("#txtTimeBegin").attr("style", "display:block");
        //                $("#txtTimeEnd").attr("style", "display:none");
        //                $("#ddlQuarter").attr("style", "display:none");

        //                $("#txtTimeBegin").attr("value", "");
        //                $("#txtTimeBegin").attr("onclick", "WdatePicker({dateFmt:'yyyy-MM'})");

        //            }
        //            else if ($(ddl).val() == 2) {
        //                $("#zhi").attr("style", "display:none");
        //                $("#txtTimeBegin").attr("style", "display:block");
        //                $("#txtTimeEnd").attr("style", "display:none");
        //                $("#ddlQuarter").attr("style", "display:block");

        //                $("#txtTimeBegin").attr("value", "");
        //                $("#txtTimeBegin").attr("onclick", "WdatePicker({dateFmt:'yyyy'})");

        //            }
        //            else if ($(ddl).val() == 3) {
        //                $("#zhi").attr("style", "display:none");
        //                $("#txtTimeBegin").attr("style", "display:block");
        //                $("#txtTimeEnd").attr("style", "display:none");
        //                $("#ddlQuarter").attr("style", "display:none");

        //                $("#txtTimeBegin").attr("value", "");
        //                $("#txtTimeBegin").attr("onclick", "WdatePicker({dateFmt:'yyyy'})");
        //            }
        //        };
    </script>
    <%--<script type="text/javascript">

        var ihight;
        $(document).ready(function () {
            ihight = pageHeight();
            //$('#dv_add').hide();
            Grid();
        });
        function Grid() {
            $('#steam').datagrid({
                title: '机组耗差指标分析',
                iconCls: 'icon-search',
                nowrap: true,
                autoRowHeight: false,
                striped: true,
                width: 600,
                height: ihight - 130,
                align: 'center',
                loadMsg: '数据加载中请稍后……',
                collapsible: true,
                url: 'EnergyLossIndicator.aspx',
                sortName: 'ID_KEY',
                sortOrder: 'asc',
                remoteSort: false,
                //queryParams: {param:'searchList', unit: $("#ddlUnit").val(), beginTime: $("#txtTimeBegin").val(), endTime: $("#txtTimeEnd").val(), timeType: $("#ddlType").val(),quarterType:$("#ddlQuarter").val() },
                //idField: 'T_CODE',
                queryParams: { param: 'seachList', company: $("#ddlCompany").val(), plant: $("#ddlPlant").val(), unit: $("#ddlUnit").val(), beginTime: $("#txtTimeBegin").val(), endTime: $("#txtTimeEnd").val() },
                frozenColumns: [[
                { field: 'ck', checkbox: true }

			    ]],
                columns: [[
				    { field: 'Name', title: '指标名称', width: 150, align: 'center' },
				    { field: 'StandardValue', title: '基准值', width: 200, align: 'center' },
				    { field: 'RealValue', title: '实际值', width: 150, align: 'center' },
				    { field: 'ConsumeValue', title: '耗差值', width: 150, align: 'center' }
			    ]],
                pagination: true,
                rownumbers: true
            })
        };
        //锅炉

        function seachParment() {
            var query = { param: 'seachList' }; //把查询条件拼接成JSON
            $("#steam").datagrid('reload'); //重新加载
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

    </script>--%>
    <script type="text/javascript">

        var ihight;
        $(document).ready(function () {
            ihight = pageHeight();
            //$('#dv_add').hide();
            Grid();
        });

        function Grid() {
            $('#steam').datagrid({
                title: '可靠性过程参数列表',
                iconCls: 'icon-search',
                nowrap: true,
                autoRowHeight: false,
                striped: true,
                height: ihight - 130,
                align: 'center',
                collapsible: true,
                url: 'EnergyLossIndicator.aspx',
                sortName: 'ID_KEY',
                sortOrder: 'asc',
                remoteSort: false,
                queryParams: {param:'searchList', unit: $("#ddlUnit").val(), beginTime: $("#txtTimeBegin").val(), endTime: $("#txtTimeEnd").val(), timeType: $("#ddlType").val(),quarterType:$("#ddlQuarter").val() },

                idField: 'T_CODE',
                frozenColumns: [[
                { field: 'ck', checkbox: true },
                { field: 'T_CODE', title: '机组编号', width: 50, align: 'center' },
				{ field: 'T_UNITDESC', title: '机组名称', width: 100, align: 'center' },
				{ field: 'D_CAPABILITY', title: '机组容量（MW）', width: 100, align: 'center' }
			    ]],
                columns: [[
				    { field: 'I_GAAG', title: '机组发电量（MWh）', width: 150, align: 'center' },
				    { field: 'I_PH', title: '统计期间小时（h）', width: 200, align: 'center' },
				    { field: 'I_AH', title: '可用小时（h）', width: 150, align: 'center' },
				    { field: 'I_SH', title: '运行小时（h）', width: 150, align: 'center' },
				    { field: 'I_UOH', title: '机组非停计划停运小时（h）', width: 250, align: 'center' },
				    { field: 'I_FOH', title: '机组强迫停运小时（h）', width: 250, align: 'center' },
				    { field: 'I_EUNDH', title: '降低出力等效停运小时（h）', width: 250, align: 'center' }
			    ]],
                pagination: true,
                rownumbers: true,
                onDblClickRow: function (rowIndex, rowData) {
                    $('#test').datagrid('clearSelections');
                    //$('#txtID').val(rowData.T_GRPID);
                    //$('#txtOID').val(rowData.T_GRPID);
                    //$('#txtName').val(rowData.T_GRPDESC);
                    $("#txtCapability").val(rowData.D_CAPABILITY);
                    $("#txtAH").val(rowData.I_AH);
                    $("#txtFOH").val(rowData.I_FOH);
                    $("#txtGAAG").val(rowData.I_GAAG);
                    $("#txtSH").val(rowData.I_SH);
                    $("#txtEUNDH").val(rowData.I_EUNDH);
                    $("#txtPH").val(rowData.I_PH);
                    $("#txtUOH").val(rowData.I_UOH);
                    //EditShow();
                }
            });
        }

        function seachParment() {
            var query = { param: 'seachList' }; //把查询条件拼接成JSON
            $("#test").datagrid('reload'); //重新加载
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
<body style="background-color: #E3EDF8; font-size: 12px; font-family: @宋体">
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
                            <div align="left" class="title" style="background-color: #e5f1f4; font-size: 16px;">
                                机组耗差指标分析</div>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="4">
                    <tr>
                        <td>
                            省公司<asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td>
                            电厂<asp:DropDownList ID="ddlPlant" runat="server" OnSelectedIndexChanged="ddlPlant_SelectedChanged"
                                AutoPostBack="true">
                                <asp:ListItem Selected="True" Value="0">--请选择电厂--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            机组<asp:DropDownList ID="ddlUnit" runat="server">
                                <asp:ListItem Selected="True" Value="0">--请选择机组--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="4">
                    <tr>
                        <td>
                            时间设置
                            <asp:DropDownList ID="ddlType" runat="server" onchange="change(this)">
                                <asp:ListItem Value="0">指定时间段平均值</asp:ListItem>
                                <asp:ListItem Value="1">月度平均值</asp:ListItem>
                                <asp:ListItem Value="2">季度平均值</asp:ListItem>
                                <asp:ListItem Value="3">年度平均值</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="2">
                            <input class="Wdate" id="txtTimeBegin" runat="server" type="text" readonly="readonly"
                                onclick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm ',maxDate:'%y-%M-%d'})" />
                            <span id="zhi" style="display: none">至</span>
                            <input class="Wdate" id="txtTimeEnd" runat="server" type="text" readonly="readonly"
                                onclick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm ',maxDate:'%y-%M-%d'})" style="display: none" />
                            <asp:DropDownList ID="ddlQuarter" runat="server" Style="display: none">
                                <asp:ListItem Value="0">一季度</asp:ListItem>
                                <asp:ListItem Value="1">二季度</asp:ListItem>
                                <asp:ListItem Value="2">三季度</asp:ListItem>
                                <asp:ListItem Value="3">四季度</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="btnSearch" runat="server" OnClick="Search_Click" Text="查询" />
                            <asp:Button ID="Button2" runat="server" Text="导出数据" OnClick="Export_Click" />
                        </td>
                    </tr>
                </table>
                <%--  <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
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
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <table id="pie">
                                        </table>
                                    </td>
                                    <td>
                                        <table id="zh">
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table id="bt">
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>--%>
                <table id="steam">
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="Button2" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Grid);
</script>
