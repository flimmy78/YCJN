<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="EditThermalTestCfg" Codebehind="EditThermalTestCfg.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>维护全局配置</title>
    <link rel="stylesheet" type="text/css" href="../../css/master.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <img src="../../img/ThermalTestCfg.gif" style="width: 99%; height: 38px" />
    <div style="width: 99%;">
        <div class="tablediv2">
            <div class="rowdiv3">
                <table width="100%" border="0">
                    <tr>
                        <td height="5">
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="groupBox">
            <table width="100%" class="groupBoxCaption">
                <tr>
                    <td nowrap="noWrap">
                        维护全局配置
                    </td>
                </tr>
            </table>
            <table width="100%" class="groupBoxContainer" >
                <tr>
                    <td width="34%" nowrap="noWrap">
                        <span class="groupBoxLabel5">默认取点间隔</span>
                        <asp:TextBox ID="txtDefaultTime" runat="server" Style="width: 160px;margin-left: 5px;"></asp:TextBox>秒
                    </td>
                    <td style="width: 308px" nowrap="noWrap">
                        <span class="groupBoxLabel5">最小取点间隔</span>
                        <input type="text" id="txtMinTime" runat="server" size="12" style="overflow: hidden;
                            margin-left: 0px; width: 160px" />秒
                    </td>
                </tr>
                <tr></tr>
                <tr>
                    <td width="34%" nowrap="noWrap">
                        <span class="groupBoxLabel5">最多工况数量</span>
                        <input type="text" id="txtMaxMount" runat="server" size="12" style="overflow: hidden;
                            margin-left: 5px; width: 160px" />
                    </td>
                    <td>
                        <span class="groupBoxLabel5">默认试验时间</span><input type="text" id="txtDefaultSYTime"
                            runat="server" style="width: 160px; overflow: hidden; margin-left: 8px;" />分
                    </td>
                </tr>
                <tr></tr>
                <tr>
                    <td>
                        <span class="groupBoxLabel5">最大试验时间</span><input type="text" id="txtMaxSYTime" runat="server"
                            size="12" style="margin-left: 13px; overflow: hidden; width: 160px" />分
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:Button ID="Button1" runat="server" Style="float: right; margin-right: 150px;
        margin-top: 10px" Text="保  存" OnClick="Button1_Click" />
    </form>
</body>
</html>
