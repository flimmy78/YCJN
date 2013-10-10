<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndicatorQuery.aspx.cs"
    Inherits="YJJX.EquipmentReliable.IndicatorQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>可靠性指标查询</title>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../css/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../jQueryEasyUI/jquery-1.6.2.js"></script>
    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        var ihight;
        $(document).ready(function () {
            ihight = pageHeight();
            $('#dv_add').hide();
            Grid();
        });

        function Grid() {
            $('#test').datagrid({
                title: '可靠性指标列表',
                iconCls: 'icon-search',
                nowrap: true,
                autoRowHeight: false,
                striped: true,
                height: ihight - 130,
                align: 'center',
                collapsible: true,
                url: 'IndicatorQuery.aspx',
                sortName: 'ID_KEY',
                sortOrder: 'asc',
                remoteSort: false,
                queryParams: { param: 'seachList', company: $("#ddlCompany").val(), plant: $("#ddlPlant").val(), unit: $("#ddlUnit").val(), beginTime: $("#txtTimeBegin").val(), endTime: $("#txtTimeEnd").val() },
                idField: 'id',
                frozenColumns: [[
                { field: 'ck', checkbox: true },
                { field: 'T_PLANTDESC', title: '电厂名称', width: 150, align: 'center' },
				{ field: 'T_UNITDESC', title: '机组名称', width: 100, align: 'center' },
				{ field: 'D_CAPABILITY', title: '机组容量（MW）', width: 100, align: 'center' }
			    ]],
                columns: [[
				    { field: 'I_UTH', title: '利用小时（h）', width: 150, align: 'center' },
				    { field: 'D_EAF', title: '等效可用系数（%）', width: 200, align: 'center' },
				    { field: 'D_FOF', title: '强迫停运系数（%）', width: 150, align: 'center' },
				    { field: 'D_FOR', title: '强迫停运率（%）', width: 150, align: 'center' },
				    { field: 'D_UOF', title: '非计划停运系数（%）', width: 250, align: 'center' },
				    { field: 'D_UOR', title: '非计划停运率（%）', width: 250, align: 'center' }
			    ]],
                pagination: true,
                rownumbers: true

            }
            )
        };

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
<body style="background-color: #E3EDF8; font-size: 12px; font-family: 宋体">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table height="25" border="0" cellpadding="0" cellspacing="0" width="100%" bgcolor="#e5f1f4"
                style="border-bottom: 2px solid #48BADB;">
                <tr>
                    <td>
                        <div align="left" class="title">
                            可靠性指标查询</div>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="4">
                <tr>
                    <td>
                        省公司<asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedChanged"
                            AutoPostBack="true">
                            <%-- <asp:ListItem Selected="True"  Value="0">--请选择省公司--</asp:ListItem>--%>
                        </asp:DropDownList>
                        &nbsp;&nbsp; 电厂<asp:DropDownList ID="ddlPlant" runat="server" OnSelectedIndexChanged="ddlPlant_SelectedChanged"
                            AutoPostBack="true">
                            <asp:ListItem Selected="True" Value="0">--请选择电厂--</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp; 机组<asp:DropDownList ID="ddlUnit" runat="server">
                            <asp:ListItem Selected="True" Value="0">--请选择机组--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="4">
                <tr>
                    <td>
                        查询时间段
                        <input class="Wdate" id="txtTimeBegin" runat="server" type="text" onclick="WdatePicker({dateFmt:'yyyy-MM'})" />
                        至
                        <input class="Wdate" id="txtTimeEnd" runat="server" type="text" onclick="WdatePicker({dateFmt:'yyyy-MM'})" />
                        <asp:Button ID="btnSearch" runat="server" OnClick="Search_Click" Text="查询" />
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="4">
                <tr>
                    <%-- <td>
                            <asp:Button ID="Button1" runat="server" Text="导入记录" />
                        </td>--%>
                    <td colspan="2">
                        <asp:Button ID="Button2" runat="server" Text="导出当前查询" OnClick="Export_Click" />
                    </td>
                </tr>
            </table>
            <%-- <hr style="border: solid 2px lightblue" />
            <span style="width: 100%;">可靠性指标</span>
            <hr style="border: solid 2px lightblue" />--%>
            <br />
            <table id="test">
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Button2" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Grid);
</script>
