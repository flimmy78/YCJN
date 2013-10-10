<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="data.aspx.cs" Inherits="DJXT.ConsumeIndicator.data" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../css/help3.css" rel="stylesheet" type="text/css" />
     <script language="javascript" type="text/javascript" src="../css/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../jQueryEasyUI/jquery-1.6.2.js"></script>
    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        //时间设置 改变事件
        function change(ddl) {
            if ($(ddl).val() == 1) {
                $("#txtTimeBegin").attr("style", "display:block");
                $("#ddlQuarter").attr("style", "display:none");
                $("#txtTimeBegin").attr("value", "");
                $("#txtTimeBegin").attr("onclick", "WdatePicker({dateFmt:'yyyy-MM'})");
            }
            else if ($(ddl).val() == 2) {
                $("#txtTimeBegin").attr("style", "display:block");
                $("#ddlQuarter").attr("style", "display:block");
                $("#txtTimeBegin").attr("value", "");
                $("#txtTimeBegin").attr("onclick", "WdatePicker({dateFmt:'yyyy'})");
            }
        };
    </script>
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
            //$('#dv_add').hide();
            Grid();
            // mergeGridColCells();
        });

        function Grid() {
                     $("#test").datagrid({
                         title: '能耗指标列表',
                         iconCls: 'icon-search',
                         nowrap: true,
                         autoRowHeight: false,
                         striped: true,
                         height: ihight - 130,
                         align: 'center',
                         collapsible: true,
                         url: 'data.aspx',
                         sortName: ['T_COMPANY', 'T_DATATYPE'],
                         sortOrder: 'asc',
                         striped: true,
                         loadMsg: '查询中,请稍等...',
                         remoteSort: false,
                         queryParams: { param: 'seachList', beginTime: $("#txtTimeBegin").val() },
                         idField: 'T_COMPANY',
                         onLoadSuccess: function (data) {
                             //alert(data.rows);
                             if (data.result == "error") {

                                 $.messager.alert('提示信息', data.errorMsg, 'error');

                             };
                             var rows = $('#test').datagrid("getRows"); //获取行的数据
                             for (var i = 0; i < rows.length; i++) {
                                 //根据情况判断相同的数据，我这里是根据T_COMPANY
                                 var id = rows[i].T_COMPANY;
                                 var rowspan = 0;
                                 for (var j = 0; j < rows.length; j++) {

                                     if (id == rows[j].T_COMPANY) {
                                         //计算合并多少行
                                         rowspan++;
                                     }
                                 }
                                 if (rowspan != 0) {
                                     //mergeCells这个方法是合并单元格，index表示标示号就是第几行开始，field表示要合并的字段，rowspan合并行数，colspan:合并列
                                     $("#test").datagrid('mergeCells', { index: i, field: 'T_COMPANY', rowspan: rowspan });
                                     i = i + rowspan - 1;
                                 }
                             }
                         },
                         frozenColumns: [[{ title: '供电能耗', colspan: 2, width: 130, align: 'center'}],
                             [{ field: 'T_COMPANY', title: '单位', width: 50, rowspan: 2, align: 'center' },
                             { field: 'T_DATATYPE', title: '值类型', width: 80, rowspan: 2, align: 'center'}]],
                         columns: [[{ title: '900-1000MW等级', width: 100, colspan: 2, align: 'center' },
                   { title: '600MW等级', width: 625, colspan: 7, align: 'center' },
                 { title: '350MW等级', width: 900, colspan: 7, align: 'center' },
                 { title: '300MW等级', width: 500, colspan: 6, align: 'center' },
                 { title: '200MW等级', width: 500, colspan: 5, align: 'center' },
                 { title: '120MW-165等级', width: 500, colspan: 6, align: 'center' },
                 { title: '100MW等级', width: 500, colspan: 4, align: 'center' },
                 { title: '100MW等级以下', width: 500, colspan: 4, align: 'center' }
                 ], [{ field: 'T_900_SL', title: '湿冷', width: 50, rowspan: 2, align: 'center' },
         { field: 'T_900_KL', title: '空冷', width: 50, rowspan: 2, align: 'center' },
         { field: 'T_600_HJ', title: '600MW合计', width: 80, rowspan: 2, align: 'center' },
        { title: '纯凝机组', colspan: 5, align: 'center' },
        { title: '供热机组', colspan: 1, width: 100, align: 'center' },
         { field: 'T_350_HJ', title: '350MW合计', width: 80, rowspan: 2, align: 'center' },
        { title: '纯凝机组', colspan: 3, align: 'center' },
        { title: '供热机组', colspan: 3, width: 100, align: 'center' },
         { field: 'T_300_HJ', title: '300-330MW合计', width: 80, rowspan: 2, align: 'center' },
        { title: '纯凝机组', colspan: 3, align: 'center' },
        { title: '供热机组', colspan: 2, width: 100, align: 'center' },
         { field: 'T_200_HJ', title: '200MW合计', width: 80, rowspan: 2, align: 'center' },
        { title: '纯凝机组', colspan: 3, align: 'center' },
        { title: '供热机组', colspan: 1, width: 100, align: 'center' },
         { field: 'T_120_HJ', title: '120-165MW级合计', width: 80, rowspan: 2, align: 'center' },
        { title: '纯凝机组', colspan: 3, align: 'center' },
        { title: '供热机组', colspan: 2, width: 100, align: 'center' },
         { field: 'T_100_HJ', title: '100MW级合计', width: 80, rowspan: 2, align: 'center' },
        { title: '纯凝机组', colspan: 2, align: 'center' },
        { title: '供热机组', colspan: 1, width: 100, align: 'center' },
         { field: 'T_90_HJ', title: '100MW级以下合计', width: 80, rowspan: 2, align: 'center' },
        { title: '纯凝机组', colspan: 2, align: 'center' },
        { title: '供热机组', colspan: 1, width: 100, align: 'center'}], [
        { field: 'T_600_CCL', title: '600MW超超临界（湿冷）', width: 100, align: 'center' },
        { field: 'T_600_CLS', title: '600MW超临界（湿冷）', width: 100, align: 'center' },
        { field: 'T_600_CLK', title: '600MW超临界(空冷)', width: 100, align: 'center' },
        { field: 'T_600_YLS', title: '600MW亚临界（湿冷）', width: 100, align: 'center' },
        { field: 'T_600_YLK', title: '600MW亚临界(空冷)', width: 100, align: 'center' },
        { field: 'T_600_J', title: '600MW级', width: 45, align: 'center' },
        { field: 'T_350_CNHJ', title: '纯凝合计', width: 95, align: 'center' },
        { field: 'T_350_CLJ', title: '超临界', width: 100, align: 'center' },
        { field: 'T_350_YLJ', title: '亚临界', width: 100, align: 'center' },
        { field: 'T_350_GRHJ', title: '供热合计', width: 100, align: 'center' },
        { field: 'T_350_RCLJ', title: '超临界', width: 100, align: 'center' },
        { field: 'T_350_RYLJ', title: '亚临界', width: 100, align: 'center' },
        { field: 'T_300_CGSL', title: '300MW级常规湿冷', width: 100, align: 'center' },
        { field: 'T_300_CGKL', title: '300MW级常规空冷', width: 100, align: 'center' },
        { field: 'T_300_LHC', title: '300MW级流化床', width: 100, align: 'center' },
        { field: 'T_300_SL', title: '300MW级湿冷', width: 100, align: 'center' },
        { field: 'T_300_KL', title: '300MW级空冷', width: 100, align: 'center' },
        { field: 'T_200_SL', title: '200MW级湿冷', width: 100, align: 'center' },
        { field: 'T_200_KL', title: '200MW级空冷', width: 100, align: 'center' },
        { field: 'T_200_LHC', title: '200MW级流化床', width: 100, align: 'center' },
        { field: 'T_200_J', title: '200-220MW级', width: 100, align: 'center' },
        { field: 'T_120_J', title: '120-165MW级', width: 100, align: 'center' },
        { field: 'T_120_LHC', title: '135MW级流化床', width: 100, align: 'center' },
        { field: 'T_120_KL', title: '空冷', width: 100, align: 'center' },
        { field: 'T_120_JC', title: '120-165MW级', width: 100, align: 'center' },
        { field: 'T_135_LHC', title: '135MW级流化床', width: 100, align: 'center' },
        { field: 'T_100_CG', title: '100MW级常规', width: 100, align: 'center' },
        { field: 'T_100_LHC', title: '100MW流化床', width: 100, align: 'center' },
        { field: 'T_100_J', title: '100MW级', width: 100, align: 'center' },
        { field: 'T_90_NJ', title: '100MW以下级', width: 100, align: 'center' },
        { field: 'T_90_LHC', title: '100MW以下流化床', width: 100, align: 'center' },
        { field: 'T_90_RJ', title: '100MW以下级', width: 100, align: 'center' }

    ]],
                         pagination: true,
                         rownumbers: true
                     })
                };
           
        function pageHeight() {
            if ($.browser.msie) {
                return document.compatMode == "CSS1Compat" ? document.documentElement.clientHeight :
            document.body.clientHeight;
            } else {
                return self.innerHeight;
            }
        };


    </script>
</head>
<body>
    <form id="form1" runat="server">
    <span style="font-size: 30px; margin-left: 30%; font-weight: bolder; color:Blue;">五大集团各类型机组能耗指标</span>
    <hr style="color: Blue" />
    <div style="margin: 0 1em">
       
        <div style="float: left; margin-left: 5px;">
            时间设置
            <asp:DropDownList ID="ddlType" runat="server" onchange="change(this)" CssClass="easyui-combobox">
                <asp:ListItem Value="1">月度累计平均值</asp:ListItem>
                <asp:ListItem Value="2">季度平均值</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div style="float: left; margin-left: 5px;">
            <input class="Wdate" id="txtTimeBegin" runat="server" type="text" readonly="readonly"
                onclick="WdatePicker({dateFmt:'yyyy-MM'})" /></div>
        <div style="float: left; margin-left: 5px;">
            <asp:DropDownList ID="ddlQuarter" runat="server" Style="display: none">
                <asp:ListItem Value="0">一季度</asp:ListItem>
                <asp:ListItem Value="1">二季度</asp:ListItem>
                <asp:ListItem Value="2">三季度</asp:ListItem>
                <asp:ListItem Value="3">四季度</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div style="float: left; margin-left: 5px;">
            <%--<asp:Button ID="btnSearch" runat="server" OnClick="Search_Click" Text="查询"  />--%>
        <a id="CX" href="#" class="easyui-linkbutton" onclick="Grid();">查&nbsp;&nbsp;询</a><%--  onclick="return CX_onclick()--%>
            
            </div>
        <br />
        <br />
        <table id="test" style="width:1100px;" runat="server">
        </table>
    </div>
    </form>
</body>
</html>
