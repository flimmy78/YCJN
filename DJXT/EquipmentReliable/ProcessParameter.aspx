<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessParameter.aspx.cs"
    Inherits="YJJX.EquipmentReliable.ProcessParameter" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>可靠性过程参数</title>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../css/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../jQueryEasyUI/jquery-1.6.2.js"></script>
    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Item_Click(item) {
            $("#txtCapability").attr("value", item.cells[2].innerText);
            $("#txtAH").attr("value", item.cells[5].innerText);
            $("#txtFOH").attr("value", item.cells[8].innerText);
            $("#txtGAAG").attr("value", item.cells[3].innerText);
            $("#txtSH").attr("value", item.cells[6].innerText);
            $("#txtEUNDH").attr("value", item.cells[9].innerText);
            $("#txtPH").attr("value", item.cells[4].innerText);
            $("#txtUOH").attr("value", item.cells[7].innerText);
        }
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
            //$('#dv_add').hide();
            Grid();
        });

        function Grid() {
            $('#test').datagrid({
                title: '可靠性过程参数列表',
                iconCls: 'icon-search',
                nowrap: true,
                autoRowHeight: false,
                striped: true,
                height: ihight - 130,
                align: 'center',
                collapsible: true,
                url: 'ProcessParameter.aspx',
                sortName: 'ID_KEY',
                sortOrder: 'asc',
                remoteSort: false,
                queryParams: { param: 'seachList', company: $("#ddlCompany").val(), plant: $("#ddlPlant").val(), unit: $("#ddlUnit").val(), beginTime: $("#txtTimeBegin").val(), endTime: $("#txtTimeEnd").val() },
                idField: 'T_CODE',
                frozenColumns: [[
                { field: 'ck', checkbox: true },
                { field: 'T_PLANTDESC', title: '电厂名称', width: 150, align: 'center' },
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
                onClickRow: function (rowIndex, rowData) {
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
                            可靠性过程参数</div>
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
                    &nbsp; &nbsp; &nbsp; &nbsp;
                        电厂<asp:DropDownList ID="ddlPlant" runat="server" OnSelectedIndexChanged="ddlPlant_SelectedChanged"
                            AutoPostBack="true">
                            <asp:ListItem Selected="True" Value="0">--请选择电厂--</asp:ListItem>
                        </asp:DropDownList>
                    &nbsp; &nbsp; &nbsp; &nbsp;
                        机组<asp:DropDownList ID="ddlUnit" runat="server">
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
                    <td>
                        <asp:FileUpload ID="fileUp" runat="server" ViewStateMode="Enabled" />
                   &nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:Button ID="Button1" runat="server" Text="导入记录" OnClick="btnImport_Click" />
                   &nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:Button ID="Button2" runat="server" Text="导出当前查询" OnClick="Export_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <table width="100%" border="0" cellspacing="0" cellpadding="4">
                <tr>
                    <td>
                        机组铭牌容量<asp:TextBox ID="txtCapability" runat="server"></asp:TextBox>MW
                     &nbsp;&nbsp;
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;可用小时<asp:TextBox ID="txtAH" runat="server"></asp:TextBox>h
                     &nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;机组强迫停运小时<asp:TextBox ID="txtFOH" runat="server"></asp:TextBox>h
                    </td>
                </tr>
                <tr>
                    <td>
                         &nbsp;&nbsp;机组发电量<asp:TextBox ID="txtGAAG" runat="server"></asp:TextBox>MWh
                   &nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;运行小时<asp:TextBox ID="txtSH" runat="server"></asp:TextBox>h
                   &nbsp;&nbsp;
                        降低出力等效停运小时<asp:TextBox ID="txtEUNDH" runat="server"></asp:TextBox>h
                    </td>
                </tr>
                <tr>
                    <td>
                        统计期间小时<asp:TextBox ID="txtPH" runat="server"></asp:TextBox>h
                    &nbsp;&nbsp;
                        机组非计划停运小时<asp:TextBox ID="txtUOH" runat="server"></asp:TextBox>h
                    </td>
                </tr>
            </table>
            <%-- <table width="100%" border="0" cellspacing="0" cellpadding="4">
                    <tr>
                        <td>
                            <asp:GridView ID="gvm" runat="server" AutoGenerateColumns="False" Width="1000px"
                                GridLines="Both" OnRowDataBound="gvadm_RowDataBound" PagerSettings-Visible="false"
                                OnPageIndexChanging="gvadm_PageIndexChanging" PageSize="10" AllowPaging="True"
                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                CellPadding="4" ForeColor="Black">
                                <Columns>
                                    <asp:TemplateField HeaderText="序号">
                                        <HeaderStyle Font-Bold="False" Width="50px" />
                                        <ItemTemplate>
                                            <%=this._rowIndex += 1 %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="T_UNITDESC" HeaderText="机组名称">
                                        <HeaderStyle Font-Bold="False" Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <FooterStyle BorderColor="#D6EFF8" BorderStyle="Solid" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="D_CAPABILITY" HeaderText="机组容量（MW）">
                                        <HeaderStyle Font-Bold="False" Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <FooterStyle BorderColor="#D6EFF8" BorderStyle="Solid" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="I_GAAG" HeaderText="机组发电量（MWh）">
                                        <HeaderStyle Font-Bold="False" Width="100px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <FooterStyle BorderColor="#D6EFF8" BorderStyle="Solid" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="I_PH" HeaderText="统计期间小时（h）">
                                        <HeaderStyle Font-Bold="False" Width="200px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <FooterStyle BorderColor="#D6EFF8" BorderStyle="Solid" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="I_AH" HeaderText="可用小时（h）">
                                        <HeaderStyle Font-Bold="False" Width="100px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <FooterStyle BorderColor="#D6EFF8" BorderStyle="Solid" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="I_SH" HeaderText="运行小时（h）">
                                        <HeaderStyle Font-Bold="False" Width="100px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <FooterStyle BorderColor="#D6EFF8" BorderStyle="Solid" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="I_UOH" HeaderText="机组非停计划停运小时（h）">
                                        <HeaderStyle Font-Bold="False" Width="100px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <FooterStyle BorderColor="#D6EFF8" BorderStyle="Solid" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="I_FOH" HeaderText="机组强迫停运小时（h）">
                                        <HeaderStyle Font-Bold="False" Width="100px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <FooterStyle BorderColor="#D6EFF8" BorderStyle="Solid" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="I_EUNDH" HeaderText="降低出力等效停运小时（h）">
                                        <HeaderStyle Font-Bold="False" Width="100px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <FooterStyle BorderColor="#D6EFF8" BorderStyle="Solid" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <EmptyDataTemplate>
                                    暂时没有信息！
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <asp:LinkButton ID="lnkbtnFrist" runat="server" OnClick="lnkbtnFrist_Click">首页</asp:LinkButton>
                            <asp:LinkButton ID="lnkbtnPre" runat="server" OnClick="lnkbtnPre_Click">上一页</asp:LinkButton>
                            <asp:Label ID="lblCurrentPage" runat="server"></asp:Label>
                            <asp:LinkButton ID="lnkbtnNext" runat="server" OnClick="lnkbtnNext_Click">下一页</asp:LinkButton>
                            <asp:LinkButton ID="lnkbtnLast" runat="server" OnClick="lnkbtnLast_Click">尾页</asp:LinkButton>
                            <asp:DropDownList ID="ddlCurrentPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>--%>
            <table id="test">
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Button1" />
            <asp:PostBackTrigger ControlID="Button2" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Grid);
</script>
