<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnplannedOutageEdit.aspx.cs"
    Inherits="YJJX.EquipmentReliable.UnplannedOutageEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>非计划停运事件信息编辑</title>
    <script type="text/javascript" src="../jQueryEasyUI/jquery-1.6.2.js"></script>
    <script type="text/javascript">
      function Save() {
            //取故障专业分类。
            $("input[name='Professional']").each(function (i, j) {
                if ($(j).attr("checked") != undefined) {
                    $("#hidProfessional").val($(j).attr("Id"));
                    //跳出循环。
                    return false;
                }
            })
            //取故障原因分类。
            $("input[name='Reason']").each(function (i, j) {
                if ($(j).attr("checked") != undefined) {
                    $("#hidReason").val($(j).attr("Id"));
                    //跳出循环。
                    return false;
                }
            })
        }
    
        //设置当前按钮为单选。
        function SetValue(rad) {
            $(rad).parent().find("input").each(function (i, j) {
                $(j).removeAttr("checked");
            });
            $(rad).attr("checked", "checked");
        }

        //设置刷新后的按钮选择状态。
        window.onload = function jz() {
            //设置故障专业分类。
            $("input[name='Professional']").each(function (i, j) {
                if ($(j).attr("Id") == $("#hidProfessional").val()) {
                    $(j).attr("checked", "checked");
                    //跳出循环。
                    return false;
                }
            })
            //设置故障原因分类。
            $("input[name='Reason']").each(function (i, j) {
                if ($(j).attr("Id") == $("#hidReason").val()) {
                    $(j).attr("checked", "checked");
                    //跳出循环。
                    return false;
                }
            })
        }
    </script>
</head>
<body style="background-color:#E3EDF8; font-size:12px; font-family:宋体">
    <form id="form1" runat="server">
    <div style="width:750px;margin:0 auto;"> 
        非计划停运事件信息编辑
        <table>
            <tr>
                <td>
                    单位<asp:TextBox ID="txtPlant" runat="server" Enabled="false"></asp:TextBox>
                </td>
                <td>
                    机组<asp:TextBox ID="txtUnit" runat="server" Enabled="false"></asp:TextBox>
                </td>
                <td>
                    容量<asp:TextBox ID="txtCapability" runat="server" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    非计划停运起止时间<asp:TextBox ID="txtBeginTime" runat="server" Enabled="false"></asp:TextBox>至<asp:TextBox
                        ID="txtEndTime" runat="server" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    停运累计时间<asp:TextBox ID="txtLjTime" runat="server" Enabled="false"></asp:TextBox>小时&nbsp;&nbsp;&nbsp;&nbsp;
                    影响电量<asp:TextBox ID="txtYxTime" runat="server" Enabled="false"></asp:TextBox>
                    万kW.h
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    故障类别：
                    <asp:Repeater ID="rptCategory" runat="server">
                        <ItemTemplate>
                            <%-- <asp:RadioButton ID="rad" runat="server"   GroupName="category" />--%>
                            <input type="radio" id="<%#Eval("T_CATEGORYID").ToString() %>" name="category" <%#select(1,Eval("T_CATEGORYID").ToString()) %>
                                disabled="disabled">
                            <%#Eval("T_CATEGORYDESC")%>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    故障性质：
                    <asp:Repeater ID="rptProperty" runat="server">
                        <ItemTemplate>
                            <%-- <asp:RadioButton ID="rad" runat="server"   GroupName="category" />--%>
                            <input type="radio" id="<%#Eval("T_PROPERTYID").ToString() %>" name="Property" <%#select(2,Eval("T_PROPERTYID").ToString()) %>
                                disabled="disabled" >
                            <%#Eval("T_PROPERTYDESC")%>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    故障专业分类：
                    <asp:Repeater ID="rptProfessional" runat="server">
                        <ItemTemplate>
                            <%-- <asp:RadioButton ID="rad" runat="server"   GroupName="category" />--%>
                            <input type="radio" id="<%#Eval("T_PROFESSIONALID").ToString() %>" name="Professional"
                                <%#select(3,Eval("T_PROFESSIONALID").ToString()) %> onclick="SetValue(this)">
                            <%#Eval("T_PROFESSIONALDESC")%>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    故障原因分类：
                    <asp:Repeater ID="rptReason" runat="server">
                        <ItemTemplate>
                            <%-- <asp:RadioButton ID="rad" runat="server"   GroupName="category" />--%>
                            <input type="radio" id="<%#Eval("T_REASONID").ToString() %>" name="Reason" <%#select(4,Eval("T_REASONID").ToString()) %> onclick="SetValue(this)">
                            <%#Eval("T_REASONDESC")%>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    事件描述：<asp:TextBox TextMode="MultiLine" ID="txtEventDesc" runat="server" Width="500px"
                        Height="60px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    原因分析：<asp:TextBox TextMode="MultiLine" ID="txtReason" runat="server" Width="500px"
                        Height="60px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    处理情况：<asp:TextBox TextMode="MultiLine" ID="txtDealCondition" runat="server" Width="500px"
                        Height="60px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnSave" runat="server" OnClientClick="Save()" OnClick="btnSave_Click"
                        Text="提交信息" />
                    <asp:HiddenField ID="hidProfessional" runat="server" />
                    <asp:HiddenField ID="hidReason" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
