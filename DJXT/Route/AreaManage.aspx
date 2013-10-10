<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AreaManage.aspx.cs" Inherits="DJXT.Manage.AreaManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>区域管理</title>
    <link href="../css/info.css" type="text/css" rel="stylesheet" />
</head>
<body style="margin-left: 0; margin-top: 0px; width: 100%;">
    <form id="form1" runat="server">
    <div id="top" style="background-image: url(../img/quyu.bmp); height: 38px;
        width: 100%;">
    </div>   
    
    <div style="text-align: center; float: left; width: 100%;" class="td_title">
        <asp:GridView ID="GridView1" CssClass="GridView" runat="server" AllowPaging="True"
            AutoGenerateColumns="False" Height="61px" PageSize="10" Width="100%" CellPadding="4"
            ShowFooter="False" BorderStyle="Inset" OnPageIndexChanging="GridView1_PageIndexChanging"
            OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"
            OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit">
            <PagerSettings FirstPageText="首　页" LastPageText="尾　页" PageButtonCount="10" Mode="NumericFirstLast" />
            <FooterStyle BackColor="#F2FAFB" Font-Bold="True" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#F2FAFB" HorizontalAlign="Left" BorderColor="Red" BorderStyle="Double"
                BorderWidth="2px" ForeColor="#135294" />
            <EditRowStyle BackColor="#F2FAFB" />
            <SelectedRowStyle BackColor="#CFE6FC" Font-Bold="True" ForeColor="#F2FAFB" />
            <PagerStyle BackColor="#CFE6FC" ForeColor="Blue" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#CFE6EA" Font-Bold="True" ForeColor="#CFE6FC" HorizontalAlign="Center"
                BorderColor="LightBlue" BorderStyle="Inset" />
            <AlternatingRowStyle BackColor="White" HorizontalAlign="Left" BorderColor="Red" BorderStyle="Double"
                BorderWidth="2px" />
            <Columns>
                <asp:BoundField DataField="seqnum" HeaderText="序号" ReadOnly="true">
                    <HeaderStyle BackColor="#CFE6EA" Font-Bold="True" HorizontalAlign="Center" BorderColor="LightBlue"
                        BorderStyle="Inset" />
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <FooterStyle VerticalAlign="Middle" BorderColor="LightBlue" BorderStyle="Inset" />
                </asp:BoundField>
                <asp:BoundField DataField="T_AREAID" HeaderText="区域标识">
                    <HeaderStyle BackColor="#CFE6EA" Font-Bold="True" HorizontalAlign="Center" BorderColor="LightBlue"
                        BorderStyle="Inset" />
                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                    <FooterStyle VerticalAlign="Middle" BorderColor="LightBlue" BorderStyle="Inset" />
                    <ControlStyle Width="94%" />
                </asp:BoundField>
              
                <asp:BoundField DataField="T_AREANAME" HeaderText="区域名称">
                    <HeaderStyle BackColor="#CFE6EA" Font-Bold="True" HorizontalAlign="Center" BorderColor="LightBlue"
                        BorderStyle="Inset" />
                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                    <FooterStyle VerticalAlign="Middle" BorderColor="LightBlue" BorderStyle="Inset" />
                    <ControlStyle Width="94%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="操 作" ShowHeader="False">
                    <HeaderStyle BackColor="#CFE6EA" Font-Bold="True" HorizontalAlign="Center" BorderColor="LightBlue"
                        BorderStyle="Inset" />
                    <ItemStyle HorizontalAlign="Center" Width="18%" VerticalAlign="Middle" />
                    <FooterStyle VerticalAlign="Middle" BorderColor="LightBlue" BorderStyle="Inset" />
                    <HeaderTemplate>
                        操作
                        <asp:Button ID="LBtnAdd" runat="server" CssClass="formbutton" Text="添 加" CommandName="BtnAdd"
                             OnClick="LBtnAdd_Click"></asp:Button>
                    </HeaderTemplate>
                    <EditItemTemplate>
                        <asp:Button ID="lbgx" runat="server" CausesValidation="True" CommandName="Update"
                            Text="更 新" CssClass="formbutton" OnClientClick="return confirm('确认要保存吗？');">
                        </asp:Button>
                        <asp:Button ID="lbqx" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text="取 消" CssClass="formbutton"></asp:Button>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Button ID="lbbj" runat="server" CausesValidation="False" CommandName="Edit"
                            Text="编 辑" CssClass="formbutton"></asp:Button>
                        <asp:Button ID="lbsc" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="删 除" CssClass="formbutton" OnClientClick="return confirm('确认要删除吗？')"></asp:Button>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div style="text-align: center; float: left; height: 5%; width: 100%;" class="td_title">
    </div>
    <div style="text-align: center; float: left; width: 100%; height: 5%;" class="td_title">
        <asp:GridView ID="GridView2" CssClass="GridView" runat="server" AutoGenerateColumns="False"
            ShowHeader="False" Height="5%" PageSize="10" Width="100%" CellPadding="4" ShowFooter="False"
            BorderStyle="Inset" OnRowUpdating="GridView2_RowUpdating" OnRowCancelingEdit="GridView2_RowCancelingEdit"
            OnRowDataBound="GridView2_RowDataBound">
            <FooterStyle BackColor="#F2FAFB" Font-Bold="True" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#F2FAFB" HorizontalAlign="Left" BorderColor="Red" BorderStyle="Double"
                BorderWidth="2px" />
            <EditRowStyle BackColor="#F2FAFB" />
            <SelectedRowStyle BackColor="#CFE6FC" Font-Bold="True" ForeColor="#F2FAFB" />
            <PagerStyle BackColor="#CFE6FC" ForeColor="Blue" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#CFE6EA" Font-Bold="True" ForeColor="#CFE6FC" HorizontalAlign="Center"
                BorderColor="LightBlue" BorderStyle="Inset" />
            <AlternatingRowStyle BackColor="White" HorizontalAlign="Left" BorderColor="Red" BorderStyle="Double"
                BorderWidth="2px" />
            <Columns>
                <asp:TemplateField HeaderText="序号">
                    <HeaderStyle BackColor="#CFE6EA" Font-Bold="True" HorizontalAlign="Center" BorderColor="LightBlue"
                        BorderStyle="Inset" />
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <FooterStyle VerticalAlign="Middle" BorderColor="LightBlue" BorderStyle="Inset" />
                    <ItemTemplate>
                        <asp:Label ID="label1" runat="server" Text="请输入"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="区域标识" ControlStyle-Width="94%">
                    <HeaderStyle BackColor="#CFE6EA" Font-Bold="True" HorizontalAlign="Center" BorderColor="LightBlue"
                        BorderStyle="Inset" />
                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                    <FooterStyle VerticalAlign="Middle" BorderColor="LightBlue" BorderStyle="Inset" />
                    <ItemTemplate>
                        <asp:TextBox ID="cdm" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
           
                <asp:TemplateField HeaderText="区域名称" ControlStyle-Width="94%">
                    <HeaderStyle BackColor="#CFE6EA" Font-Bold="True" HorizontalAlign="Center" BorderColor="LightBlue"
                        BorderStyle="Inset" />
                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                    <FooterStyle VerticalAlign="Middle" BorderColor="LightBlue" BorderStyle="Inset" />
                    <ItemTemplate>
                        <asp:TextBox ID="jzm" runat="server"></asp:TextBox>
                      
                    </ItemTemplate>
                </asp:TemplateField>
            
                <asp:TemplateField HeaderText="操 作">
                    <HeaderStyle BackColor="#CFE6EA" Font-Bold="True" HorizontalAlign="Center" BorderColor="LightBlue"
                        BorderStyle="Inset" />
                    <ItemStyle HorizontalAlign="Center" Width="18%" />
                    <FooterStyle VerticalAlign="Middle" BorderColor="LightBlue" BorderStyle="Inset" />
                    <ItemTemplate>
                        <asp:Button ID="lbbc" runat="server" CausesValidation="True" CommandName="Update"
                            Text="保 存" CssClass="formbutton" OnClientClick="return confirm('确认要保存吗？')" ></asp:Button>
<%--                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False"></asp:ValidationSummary>--%>
                        <asp:Button ID="lbqx2" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text="取 消" CssClass="formbutton"></asp:Button>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
