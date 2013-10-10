<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RealTime.aspx.cs" Inherits="DJXT.RealTimeMonitor.RealTime" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="width:100%; height:500px;">
    <form id="form1" runat="server">
    <%--<div>
    <asp:Literal ID="litName" runat="server" ></asp:Literal>
    <hr style="border-color:Blue" />
    </div>--%>
    <div style="width:100%; height:500px">
        <object classid="clsid:4F26B906-2854-11D1-9597-00A0C931BFC8" id="Pbd1" width="100%"
            height="100%" border="0" vspace="0" hspace="0" codebase="2.ActiveView_3_2_0_0_.exe#version=3,0,15,4">
            <param name="ServerIniURL" value="C:\Program Files\PIPC\DAT\pilogin.ini" />
            <param name="DIsplayURL" value="http://10.76.66.44/PIImages/<%= this.urlName %>"/>
        </object>
    </div>
    </form>
</body>
</html>
