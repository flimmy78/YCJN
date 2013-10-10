<%@ Page Language="C#" AutoEventWireup="true" Inherits="ViewExpriment" Codebehind="ViewExpriment.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看实验详情</title>
    <link rel="stylesheet" type="text/css" href="../../css/master.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <img src="../../img/ExprimentContent.gif" style="width: 100%; height: 38px" />
        <asp:GridView Style="z-index: 100; left: 0px; top: 20px;" ID="grvFeeInfo" runat="server"
            CssClass="grid" Width="100%" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField HtmlEncode="False" ReadOnly="false" DataField="REPORTNAME" HeaderText="实验名">
                    <HeaderStyle Width="20%" Font-Bold="False"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField HtmlEncode="False" ReadOnly="true" DataField="TESTER" HeaderText="实验人">
                    <HeaderStyle Width="10%" Font-Bold="true"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField HtmlEncode="False" ReadOnly="false" DataField="TESTCALBEGIN" HeaderText="开始实验时间">
                    <HeaderStyle Width="25%" Font-Bold="False"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField HtmlEncode="False" ReadOnly="false" DataField="TESTCALEND" HeaderText="结束实验时间">
                    <HeaderStyle Width="40%" Font-Bold="False"></HeaderStyle>
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <div style="text-align: center; width: 100%;">
                    暂无数据</div>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
