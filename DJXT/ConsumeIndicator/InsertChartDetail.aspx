<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertChartDetail.aspx.cs" Inherits="DJXT.ConsumeIndicator.InsertChartDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript" src="../css/My97DatePicker/WdatePicker.js"></script>
</head>
<body style="background-color: #E3EDF8; font-size: 12px; font-family: 宋体">
    <form id="form1" runat="server">
    <div>
        <br />
        <table width="100%" border="0" cellspacing="0" cellpadding="4">
            <tr>
                <td>
                    <asp:FileUpload ID="fileUp" runat="server" ViewStateMode="Enabled" />
                    &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="Button1" runat="server" Text="导入记录" OnClick="btnImport_Click" />
                </td>
            </tr>
        </table>
        <br />
        <hr />
        录入月度供电煤耗
        <table><tr><td>
        月份：<input class="Wdate" id="txtTimeBegin" runat="server" type="text" readonly="readonly"
                    onclick="WdatePicker({dateFmt:'yyyy-M'})" />
        </td>
        <td>供电煤耗：<asp:TextBox ID="txtValue" runat="server"></asp:TextBox></td>
        </tr>
        <tr><td><asp:Button ID="btnSave" Text="保存" runat="server"  OnClick="btnSave_Click" OnClientClick="return confirm('是否保存?')"/></td></tr>
        </table>
    </div>
    </form>
</body>
</html>
