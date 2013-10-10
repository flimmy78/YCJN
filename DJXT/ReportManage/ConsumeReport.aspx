<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsumeReport.aspx.cs" Inherits="DJXT.ReportManage.ConsumeReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../css/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../js/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../jQueryEasyUI/jquery-1.6.2.js"></script>
    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script  type="text/javascript" >
        var ihight;
        $(document).ready(function () {
            ihight = pageHeight();
        });
        function mssage() {
            var txt = document.getElementById("txtMonth");
            if (txt.val() == "") {
                alert("请选择月份!");
                return false;
            }
            return true;
        };
        function pageHeight() {
            if ($.browser.msie) {
                return document.compatMode == "CSS1Compat" ? document.documentElement.clientHeight :
            document.body.clientHeight;
            } else {
                return self.innerHeight;
            }
        };

        function Grid() {
            $('#test').datagrid({
                title: '报表列表',
                iconCls: 'icon-search',
                nowrap: true,
                autoRowHeight: false,
                striped: true,
                height: ihight - 130,
                align: 'center',
                collapsible: true,
                url: 'ConsumeReport.aspx',
                sortName: 'id',
                sortOrder: 'asc',
                remoteSort: false,
                queryParams: { param: 'seachList',  time: $("#txtMonth").val() },
                idField: 'id',
                frozenColumns: [[
                { field: 'id', title: '序号', width: 150, align: 'center' }
			    ]],
                columns: [[
				    { field: 'name', title: '报表名称', width: 150, align: 'center' },
				    { field: 'path', title: '下载', width: 150, align: 'center',
				        //添加超级链 
				        formatter: function (value, rowData, rowIndex) {
				            //function里面的三个参数代表当前字段值，当前行数据对象，行号（行号从0开始）
				            //alert(rowData.username);  
				            return "<a href='"+value+"' >下载</a>";
                        }  
                     }
				  ]],
                pagination: true,
                rownumbers: true
            }
            )
        };
        function messages() {
            Grid();
        };
        function message() {
            if ($("#txtMonth").val() == "") {
                alert("请选择月份！");
                return false;
            } else {
                return true;
            }
        };
    </script>
</head>
<body style="background-color: #E3EDF8; font-size: 12px; font-family: 宋体; width: 100%;
    height: 100%;">
    <form id="form1" runat="server">
    <div>
        <input class="Wdate" id="txtMonth" runat="server" type="text" readonly="readonly"
            onclick="WdatePicker({dateFmt:'yyyy-MM'})" />
        <a id="CX" href="#" class="easyui-linkbutton" onclick="messages();">查&nbsp;&nbsp;询</a><%--  onclick="return CX_onclick()--%>
        <asp:Button ID="btnReport" runat="server"  Text="导出报表"   OnClick="btnReport_Click"/>
        <br />
        <br />
        <br />

         <table id="test">
            </table>
    </div>
    </form>
</body>
</html>
