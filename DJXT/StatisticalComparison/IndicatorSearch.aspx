<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndicatorSearch.aspx.cs"
    Inherits="DJXT.StatisticalComparison.IndicatorSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>指标查询</title>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../css/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../jQueryEasyUI/jquery-1.6.2.js"></script>
    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <style type="text/css">
        .white_content
        {
            display: none;
            position: absolute;
            top: 10%;
            left: 20%;
            width: 800px;
            height: 500px;
            border: 10px solid lightblue;
            background-color: #E3EDF8;
        }
    </style>
    <style type="text/css">
        .button
        {
            width: 56px; /*图片宽带*/
            background: url(../img/button.jpg) no-repeat left top; /*图片路径*/
            border: none; /*去掉边框*/
            height: 24px; /*图片高度*/
            color: White;
            vertical-align: middle;
            text-align: center;
        }
        
        .grid-head
        {
            font-size: 12px;
            font-weight: bold;
            color: White;
            background-image: url(../img/footer.jpg);
            text-align: center;
            vertical-align: middle;
        }
    </style>
    <script type="text/javascript">

        var ihight;
        $(document).ready(function () {
            ihight = pageHeight();
          
        });

        function GridSteam() {
            $('#steam').datagrid({
                title: '汽机指标列表',
                iconCls: 'icon-search',
                nowrap: true,
                autoRowHeight: false,
                striped: true,
                height: ihight - 130,
                align: 'center',
                collapsible: true,
                loadMsg: '查询中,请稍等...',
                url: 'IndicatorSearch.aspx',
                //sortName: 'ID_KEY',
                sortOrder: 'asc',
                remoteSort: false,
                queryParams: { param: 'seachList', companyId: $("#ddlCompany").val(), plantId: $("#ddlPlant").val(), unit: $("#ddlUnit").val(), BeginTime: $("#txtBeginTime").val(), EndTime: $("#txtEndTime").val() },
                //idField: 'id',
                frozenColumns: [[
                { field: 'ck', checkbox: true }
               
			    ]],
                columns: [[
				    { field: 'Name', title: '指标名称', width: 150, align: 'center' },
				    { field: 'RealValue', title: '数值', width: 200, align: 'center' },
				    { field: 'Unit', title: '单位', width: 150, align: 'center' }
			    ]],
                pagination: false,
                rownumbers: true

            }
            )
        };

        function GridBoiler() {
            $('#boiler').datagrid({
                title: '锅炉指标列表',
                iconCls: 'icon-search',
                nowrap: true,
                autoRowHeight: false,
                striped: true,
                height: ihight - 130,
                align: 'center',
                collapsible: true,
                url: '../Handles/StatisticalComparison/IndicatorSearchBoiler.ashx',
                //sortName: 'ID_KEY',
                sortOrder: 'asc',
                loadMsg: '查询中,请稍等...',
                remoteSort: false,
                queryParams: { param: 'seachList', companyId: $("#ddlCompany").val(), plantId: $("#ddlPlant").val(), unit: $("#ddlUnit").val(), beginTime: $("#txtBeginTime").val(), endTime: $("#txtEndTime").val() },
                //idField: 'id',
                frozenColumns: [[
                { field: 'ck', checkbox: true }

			    ]],
                columns: [[
				    { field: 'Name', title: '指标名称', width: 150, align: 'center' },
				    { field: 'RealValue', title: '数值', width: 200, align: 'center' },
				    { field: 'Unit', title: '单位', width: 150, align: 'center' }
			    ]],
                pagination: false,
                rownumbers: true

            }
            )
        };
        function messages() {
            if ($("#ddlCompany").val() == "0") {
                alert("请选择公司！");
                return false;
            }
            if ($("#ddlPlant").val() == "0") {
                alert("请选择电厂！");
                return false;
            }
            if ($("#ddlUnit").val() == "0") {
                alert("请选择机组！");
                return false;
            }
            GridSteam();
            GridBoiler();
        }
        function pageHeight() {
            if ($.browser.msie) {
                return document.compatMode == "CSS1Compat" ? document.documentElement.clientHeight :
            document.body.clientHeight;
            } else {
                return self.innerHeight;
            }
        };

        function pageWidth() {
            if ($.browser.msie) {
                return document.compatMode == "CSS1Compat" ? document.documentElement.clientWidth :
            document.body.clientWidth;
            } else {
                return self.innerWidth;
            }
        }; 

    </script>
</head>
<body style="background-color: #E3EDF8; font-size: 12px; font-family: 宋体; width: 100%;
    height: 100%;">
    <form id="form1" runat="server">
    <table height="25" border="0" cellpadding="0" cellspacing="0" width="100%" bgcolor="#e5f1f4"
        style="border-bottom: 2px solid #48BADB;">
        <tr>
            <td>
                <div align="left" class="title">
                    经济指标和设备性能指标查询</div>
            </td>
        </tr>
    </table>
    <div style="width: 100%; height: 100%;">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" runat="server">
        </asp:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%" border="0" cellspacing="0" cellpadding="4">
                    <tr>
                        <td>
                            &nbsp; 省公司<asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedChanged"
                                AutoPostBack="true">
                                <%-- <asp:ListItem Selected="True"  Value="0">--请选择省公司--</asp:ListItem>--%>
                            </asp:DropDownList>
                            &nbsp; &nbsp; &nbsp; &nbsp; 电厂<asp:DropDownList ID="ddlPlant" runat="server" OnSelectedIndexChanged="ddlPlant_SelectedChanged"
                                AutoPostBack="true">
                                <asp:ListItem Selected="True" Value="0">--请选择电厂--</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp; &nbsp; &nbsp; &nbsp; 机组<asp:DropDownList ID="ddlUnit" runat="server">
                                <asp:ListItem Selected="True" Value="0">--请选择机组--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
        <table>
            <tr>
                <td>
                    时间段：<input class="Wdate" id="txtBeginTime" runat="server" type="text"   onclick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss ',maxDate:'%y-%M-%d'})"/>至
                    <input class="Wdate" id="txtEndTime" runat="server" type="text" onclick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss ',maxDate:'%y-%M-%d'})" />
                </td>
                <td>
                   <%-- <asp:Button ID="btnSearch" runat="server" OnClick="Search_Click" Text="查询" />--%>
                    <a id="CX" href="#" class="easyui-linkbutton" onclick="messages();">查&nbsp;&nbsp;询</a>
                </td>
            </tr>
        </table>
        <table style="width:98%"><tr>
        
        <td style="width:40%">
          <hr  style="background-color:Silver"/>
        汽机侧 
        <hr  style="background-color:Silver"/>

        <table id="steam">
        </table>
        </td>
            <td style="width: 50%;">
                <div style="width: 100%;">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <hr style="background-color: Silver" />
                                锅炉侧
                                <hr style="background-color: Silver" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table id="boiler">
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        </table>
    </div>
    <br />
    <br />
    </form>
</body>
</html>
