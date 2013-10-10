<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Monitor.aspx.cs" Inherits="YJJX.EquipmentReliable.Monitor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设备可靠性监视</title>
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
            $('#dv_add').hide();
            Grid();
        });

        function ShowPicDiv(Id, type) {
            var div_msg = document.getElementById('picDiv');
            if (div_msg.style.display == 'block') {
                div_msg.style.display = 'none';
            }
            else {
                div_msg.style.display = 'block';
                if (type == 1) {
                    document.getElementById("showpic").src = "UnplannedOutageEdit.aspx?Id=" + Id;
                }
                else if (type == 2) {
                    document.getElementById("showpic").src = "ReliabilityDescription.aspx?Id=" + Id;
                }
            }
        }

        function Grid() {
            $('#test').datagrid({
                title: '设备可靠性监视列表',
                iconCls: 'icon-search',
                nowrap: true,
                autoRowHeight: false,
                striped: true,
                height: ihight - 130,
                align: 'center',
                collapsible: true,
                url: 'Monitor.aspx',
                sortName: 'ID_KEY',
                sortOrder: 'asc',
                remoteSort: false,
                queryParams: { param: 'seachList', beginTime: $("#txtTimeBegin").val(), endTime: $("#txtTimeEnd").val() },
                idField: 'id',
                frozenColumns: [[
                { field: 'ck', checkbox: true },
                { field: 'T_PLANTDESC', title: '单位', width: 250, align: 'center' },
				{ field: 'T_UNITDESC', title: '机组名称', width: 100, align: 'center' },
				{ field: 'D_CAPABILITY', title: '机组容量（MW）', width: 100, align: 'center' }
			    ]],
                columns: [[
				    { field: 'T_BEGINTIME', title: '开始时间', width: 150, align: 'center' },
				    { field: 'T_ENDTIME', title: '结束时间', width: 200, align: 'center' },
				    { field: 'I_PH', title: '停运累计时间（小时）', width: 250, align: 'center' },
				    { field: 'I_SH', title: '影响电量（万千瓦时）', width: 250, align: 'center' },
				    { field: 'T_PROFESSIONALDESC', title: '故障专业分类', width: 250, align: 'center' },
				    { field: 'T_REASONDESC', title: '故障原因分类', width: 250, align: 'center' }
			    ]],
                pagination: true,
                rownumbers: true,
                toolbar: [{
                    id: 'btnadd',
                    text: '编辑',
                    iconCls: 'icon-edit',
                    handler: function () {
                        var rows = $('#test').datagrid('getSelections');
                        var id = "";
                        $.each(rows, function (i, n) {
                            id += "" + n.ID_KEY + ",";
                        });
                        id = id.substring(0, id.length - 1);
                        EditShow(id, 1);
                    }

                },
		        {
		            id: 'btnadd',
		            text: '查看',
		            iconCls: 'icon-edit',
		            handler: function () {
		                var rows = $('#test').datagrid('getSelections');
		                var id = "";
		                $.each(rows, function (i, n) {
		                    id += "" + n.ID_KEY + ",";
		                });
		                id = id.substring(0, id.length - 1);
		                EditShow(id, 2);
		            }
		        }]

            }
            )
        };


        function EditShow(id, ids) {
            ShowPicDiv(id, ids);
        }


        function seachParment() {
            var query = { param: 'seachList' }; //把查询条件拼接成JSON
            $("#test").datagrid('reload'); //重新加载
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
    <script type="text/javascript">
       
    </script>
</head>
<body style="background-color: #E3EDF8; font-size: 12px; font-family: 宋体">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table height="25" border="0" cellpadding="0" cellspacing="0" width="100%" bgcolor="#e5f1f4"
                style="border-bottom: 2px solid #48BADB;">
                <tr>
                    <td>
                        <div align="left" class="title">
                            设备可靠性监视</div>
                    </td>
                </tr>
            </table>
            <table style="width: 1000px;">
                <tr>
                    <td>
                        查询时间段
                        <input class="Wdate" id="txtTimeBegin" runat="server" type="text" onclick="WdatePicker()" />
                        至
                        <input class="Wdate" id="txtTimeEnd" runat="server" type="text" onclick="WdatePicker()" />
                        <asp:Button ID="btnSearch" runat="server" OnClick="Search_Click" Text="查询" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:FileUpload ID="fileUp" runat="server" />
                        <asp:Button ID="Button1" runat="server" Text="导入记录" OnClick="btnImport_Click" />
                        <asp:Button ID="Button2" runat="server" Text="导出当前查询" OnClick="Export_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <%--<asp:GridView ID="gvm" runat="server" AutoGenerateColumns="False" Width="1000px"
                        GridLines="Both" OnRowDataBound="gvadm_RowDataBound" PagerSettings-Visible="false"
                        OnPageIndexChanging="gvadm_PageIndexChanging" PageSize="10" AllowPaging="True"
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                        CellPadding="4" ForeColor="Black">
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <HeaderStyle Font-Bold="False" Width="50px" />
                                <ItemTemplate>
                                    <%=this._rowIndex += 1 %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="T_PLANTDESC" HeaderText="单位">
                                <HeaderStyle Font-Bold="False" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <FooterStyle BorderColor="#D6EFF8" BorderStyle="Solid" />
                            </asp:BoundField>
                            <asp:BoundField DataField="T_UNITDESC" HeaderText="机组">
                                <HeaderStyle Font-Bold="False" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <FooterStyle BorderColor="#D6EFF8" BorderStyle="Solid" />
                            </asp:BoundField>
                            <asp:BoundField DataField="D_CAPABILITY" HeaderText="容量（MW）">
                                <HeaderStyle Font-Bold="False" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <FooterStyle BorderColor="#D6EFF8" BorderStyle="Solid" />
                            </asp:BoundField>
                            <asp:BoundField DataField="T_BEGINTIME" HeaderText="开始时间">
                                <HeaderStyle Font-Bold="False" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <FooterStyle BorderColor="#D6EFF8" BorderStyle="Solid" />
                            </asp:BoundField>
                            <asp:BoundField DataField="T_ENDTIME" HeaderText="结束时间">
                                <HeaderStyle Font-Bold="False" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <FooterStyle BorderColor="#D6EFF8" BorderStyle="Solid" />
                            </asp:BoundField>
                            <asp:BoundField DataField="I_PH" HeaderText="停运累计时间（小时）">
                                <HeaderStyle Font-Bold="False" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <FooterStyle BorderColor="#D6EFF8" BorderStyle="Solid" />
                            </asp:BoundField>
                            <asp:BoundField DataField="I_PH" HeaderText="影响电量（万千瓦时）">
                                <HeaderStyle Font-Bold="False" Width="200px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <FooterStyle BorderColor="#D6EFF8" BorderStyle="Solid" />
                            </asp:BoundField>
                            <asp:BoundField DataField="T_PROFESSIONALDESC" HeaderText="故障专业分类">
                                <HeaderStyle Font-Bold="False" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <FooterStyle BorderColor="#D6EFF8" BorderStyle="Solid" />
                            </asp:BoundField>
                            <asp:BoundField DataField="T_REASONDESC" HeaderText="故障原因分类">
                                <HeaderStyle Font-Bold="False" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <FooterStyle BorderColor="#D6EFF8" BorderStyle="Solid" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="编辑">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle Font-Bold="False" Width="50px" />
                                <ItemTemplate>
                                    &nbsp; <a onclick="ShowPicDiv(<%#Eval("ID_KEY") %>,1)">编辑</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="详细说明">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle Font-Bold="False" Width="50px" />
                                <ItemTemplate>
                                    &nbsp; <a onclick="ShowPicDiv(<%#Eval("ID_KEY") %>,2)">查看</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <EmptyDataTemplate>
                            暂时没有信息！
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:LinkButton ID="lnkbtnFrist" runat="server" OnClick="lnkbtnFrist_Click">首页</asp:LinkButton>
                    <asp:LinkButton ID="lnkbtnPre" runat="server" OnClick="lnkbtnPre_Click">上一页</asp:LinkButton>
                    <asp:Label ID="lblCurrentPage" runat="server"></asp:Label>
                    <asp:LinkButton ID="lnkbtnNext" runat="server" OnClick="lnkbtnNext_Click">下一页</asp:LinkButton>
                    <asp:LinkButton ID="lnkbtnLast" runat="server" OnClick="lnkbtnLast_Click">尾页</asp:LinkButton>
                    <asp:DropDownList ID="ddlCurrentPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                    </asp:DropDownList>--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                    </td>
                </tr>
            </table>
            <table id="test">
            </table>
            <div id="picDiv" class="white_content">
                <div>
                    <span onclick="ShowPicDiv()" style="float: right; cursor: pointer">
                        <img src="../img/Close.gif" /></span>
                </div>
                <iframe id="showpic" marginwidth="0" marginheight="0" frameborder="0" scrolling="no"
                    width="100%" height="99%"></iframe>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Button1" />
            <asp:PostBackTrigger ControlID="Button2" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Grid);
</script>
