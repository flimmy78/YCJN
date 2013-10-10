<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AreaSelect.aspx.cs" Inherits="DJXT.Manage.AreaManageSelcet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>区域管理</title>
    <link href="../css/info.css" type="text/css" rel="stylesheet" />
</head>
<body style="margin-left: 0; margin-top: 0px; width: 100%;">
    <form id="form1" runat="server">
    <div id="top"  style="background-image: url(../img/quyu.bmp);  height: 38px;
        width: 100%;">
    </div>   
  
    <div style="text-align: center; float: left; width: 95%;" class="td_title">
        <asp:GridView ID="GridView1" CssClass="GridView" runat="server" AllowPaging="True"
            AutoGenerateColumns="False" Height="61px" PageSize="2" Width="95%" CellPadding="4"
            ShowFooter="False"  OnPageIndexChanging="GridView1_PageIndexChanging" BorderStyle="Inset">
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
                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                    <FooterStyle VerticalAlign="Middle" BorderColor="LightBlue" BorderStyle="Inset" />
                    <ControlStyle Width="94%" />
                </asp:BoundField>
                <asp:BoundField DataField="T_AREANAME" HeaderText="区域名称">
                    <HeaderStyle BackColor="#CFE6EA" Font-Bold="True" HorizontalAlign="Center" BorderColor="LightBlue"
                        BorderStyle="Inset" />
                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                    <FooterStyle VerticalAlign="Middle" BorderColor="LightBlue" BorderStyle="Inset" />
                    <ControlStyle Width="94%" />
                </asp:BoundField>
               
            </Columns>
        </asp:GridView>
    </div>  
    </form>
</body>
</html>
