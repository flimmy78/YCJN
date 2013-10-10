<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="BrowserThemalTemplate" Codebehind="BrowserThemalTemplate.aspx.cs" %>
    <%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Wisesoft.Web.Control" Namespace="Wisesoft.Web.Control" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>试验报告显示页面</title>
    <link rel="stylesheet" type="text/css" href="../../css/master.css" />
</head>
<body style="background-color: #E3EDF8; font-size: 12px;
    font-family: 宋体">
    <form id="form1" runat="server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
    <img alt="" src="../../img/Experiment.gif" style="width: 100%; height: 38px" />
    <div style="margin-top: 10px; border-collapse: collapse; background-color: #EEEEEE;
        vertical-align: middle; border: solid 1px black;">
        <div style="margin-top: 2px; margin-bottom: 2px">
           <table width="100%" border="0" cellspacing="0" cellpadding="4">
                        <tr>
                            <td>
                                省公司<asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedChanged"
                                    AutoPostBack="true">
                                    <%-- <asp:ListItem Selected="True"  Value="0">--请选择省公司--</asp:ListItem>--%>
                                </asp:DropDownList>
                                &nbsp; &nbsp; &nbsp; &nbsp; 电厂<asp:DropDownList ID="ddlPlant" runat="server" OnSelectedIndexChanged="ddlPlant_SelectedChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Selected="True" Value="0">--请选择电厂--</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp; &nbsp; &nbsp; &nbsp; 机组<asp:DropDownList ID="ddlUnit" runat="server" OnSelectedIndexChanged="ddlUnit_SelectedChanged"  AutoPostBack="true">
                                    <asp:ListItem Selected="True" Value="0">--请选择机组--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
            <span>试验名称：</span><asp:DropDownList ID="ddlExperiment" runat="server" Width="200px" AutoPostBack="false">
                                    <asp:ListItem Selected="True" Value="0">--全部--</asp:ListItem>

            </asp:DropDownList>
            <span>启始时间：</span>
            <cc1:CalenderBox ID="calStartTime" runat="server" BackColor="White" ShowTime="false"
                MaxLength="10"></cc1:CalenderBox>
            <span>终止时间：</span>
            <cc1:CalenderBox ID="calEndTime" runat="server" BackColor="White" ShowTime="false"
                MaxLength="10"></cc1:CalenderBox>
            <asp:Button ID="btnSearch" runat="server" Text="查  询" OnClick="btnSearch_Click" Style="margin-left: 5px" />
        </div>
    </div>
    <asp:GridView Style="z-index: 100; left: 0px; top: 20px" ID="grvFeeInfo" runat="server"
        Width="100%" AutoGenerateColumns="False" CssClass="grid" AllowPaging="true" PageSize="15"
        DataKeyNames="ReportID" OnPageIndexChanging="grvFeeInfo_PageIndexChanging">
        <PagerSettings Mode="Numeric" />
        <Columns>
            <asp:TemplateField HeaderText="序号" ItemStyle-Width="200px">
                <ItemTemplate>
                    <asp:Label Text='<%# Container.DataItemIndex+1%> ' runat="server" ID='lblID'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HtmlEncode="False" DataField="ReportID" HeaderText="报告编号" Visible="false">
                <HeaderStyle Width="200px" Font-Bold="False"></HeaderStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="报告名称" ItemStyle-Width="600px">
                <ItemTemplate>
                    <asp:HyperLink ID="hlkReport" runat="server" NavigateUrl='<%# "DisplayReport.aspx?ReportID="+Eval("ReportID") %>'
                        Text='<%# Eval("ReportName") %> ' Target="_blank" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="选择">
                <ItemTemplate>
                    <asp:CheckBox ID="ChkSelected" runat="server" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                <ItemStyle HorizontalAlign="Center" Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="详情" ItemStyle-Width="200px">
                <ItemTemplate>
                    <asp:HyperLink ID="hlkViewReport" runat="server" NavigateUrl='<%# "ViewExpriment.aspx?reportid="+Eval("ReportID") %>'
                        Text='查看详情 ' Target="_blank" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <div style="text-align: center; width: 100%;">
                暂无数据</div>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:Button ID="btnDisplay" runat="server" Text="数据对比" OnClick="btnDisplay_Click"
        Style="margin-right: 80px; margin-top: 10px; float:right" />
         </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnDisplay" />
            <asp:PostBackTrigger ControlID="btnSearch" />
            
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
