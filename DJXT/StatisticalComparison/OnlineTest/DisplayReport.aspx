<%@ Page Language="C#" AutoEventWireup="true" Inherits="DisplayReport"
    EnableEventValidation="false" Codebehind="DisplayReport.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>报告数据展示</title>
    <link rel="stylesheet" type="text/css" href="../../css/master.css" />
    <style type="text/css">
        .style1
        {
            width: 123px;
        }
        .style2
        {
            width: 135px;
        }
        .style3
        {
            width: 170px;
        }
        .style4
        {
            width: 218px;
        }
        .style5
        {
            width: 271px;
        }
    </style>
    <meta http-equiv="Content-Type " content="text/html;   charset=gb2312 ">
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%">
        <img src="../../img/displayreport.gif" style="height: 38px; width: 100%" />
        <div id="result" runat="server">
        </div>
    </div>
    <div style="float: right; margin-right: 5px; margin-top:10px">
        <asp:Button ID="btnOutPutExcel" runat="server" Text="导出Excel文件" OnClick="btnOutPutExcel_Click" /></div>
    </form>
</body>
</html>
