<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertHomeData.aspx.cs" Inherits="DJXT.Task.InsertHomeData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
    </div>
    </form>
</body>
</html>
