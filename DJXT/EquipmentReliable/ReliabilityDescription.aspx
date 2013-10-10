<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReliabilityDescription.aspx.cs"
    Inherits="DJXT.EquipmentReliable.ReliabilityDescription" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .desc
        {
            width: 550px;
            height: 400px;
        }
    </style>
</head>
<body style="background-color: #E3EDF8; font-size: 12px; font-family: 宋体">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td valign="top">
                    事故详细信息
                </td>
                <td>
                    <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" CssClass="desc">
                    </asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
