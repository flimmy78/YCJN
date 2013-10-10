<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChartDetail.aspx.cs" Inherits="DJXT.ConsumeIndicator.ChartDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript" src="../css/My97DatePicker/WdatePicker.js"></script>
    <link href="../jQueryEasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../jQueryEasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../jQueryEasyUI/jquery-1.6.2.js"></script>
    <script src="../jQueryEasyUI/jquery.easyui.min.js" type="text/javascript"></script>
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
            //1000MW等级
              $('#Unit100').datagrid({
                  title: '1000MW煤机供电煤耗完成情况',
                  iconCls: 'icon-search',
                  nowrap: true,
                  autoRowHeight: false,
                  striped: true,
                  height: ihight - 130,
                  align: 'center',
                  loadMsg: '查询中,请稍等...',
                  collapsible: true,
                  url: 'ChartDetail.aspx?type=1',
                  sortName: 'T_COMPANYID',
                  sortOrder: 'asc',
                  remoteSort: false,
                  queryParams: { param: 'seachList' },
                  frozenColumns: [
                [{ field: 'T_DWNAME', title: '单位名称', width: 200, align: 'center'}]
    ],
                  columns: [[{ title: '各单位1000MW煤机供电煤耗完成情况', colspan: 10, align: 'center'}],
                   [{ field: 'T_COUNT', title: '台数', width: 120, align: 'center' },
                   { field: 'T_USEHOUR', title: '利用小时', width: 120, align: 'center' },
                   { field: 'T_OF', title: '出力系数%', width: 120, align: 'center' },
                   { field: 'T_RDB', title: '热电比%', width: 120, align: 'center' },
                   { field: 'T_CYDL', title: '厂用电率（%）', width: 120, align: 'center' },
                   { field: 'T_GDMH', title: '供电煤耗', width: 120, align: 'center' },
                   { field: 'T_DBMH', title: '对标煤耗', width: 120, align: 'center' },
                   { field: 'T_GDL', title: '供电量', width: 120, align: 'center' },
                   { field: 'T_JTPJB', title: '与集团平均比', width: 120, align: 'center' }
]]
              });


              //600MW等级
              $('#Unit60').datagrid({
                  title: '600MW煤机供电煤耗完成情况',
                  iconCls: 'icon-search',
                  nowrap: true,
                  autoRowHeight: false,
                  striped: true,
                  height: ihight - 130,
                  align: 'center',
                  collapsible: true,
                  loadMsg: '查询中,请稍等...',
                  url: 'ChartDetail.aspx?type=2',
                  sortName: 'T_COMPANYID',
                  sortOrder: 'asc',
                  remoteSort: false,
                  queryParams: { param: 'seachList' },
                  frozenColumns: [
                [{ field: 'T_DWNAME', title: '单位名称', width: 200, align: 'center'}]
    ],
                  columns: [[{ title: '各单位600MW煤机供电煤耗完成情况', colspan: 10, align: 'center'}],
                 [{ field: 'T_COUNT', title: '台数', width: 120, align: 'center' },
                   { field: 'T_USEHOUR', title: '利用小时', width: 120, align: 'center' },
                   { field: 'T_OF', title: '出力系数%', width: 120, align: 'center' },
                   { field: 'T_RDB', title: '热电比%', width: 120, align: 'center' },
                   { field: 'T_CYDL', title: '厂用电率（%）', width: 120, align: 'center' },
                   { field: 'T_GDMH', title: '供电煤耗', width: 120, align: 'center' },
                   { field: 'T_DBMH', title: '对标煤耗', width: 120, align: 'center' },
                   { field: 'T_GDL', title: '供电量', width: 120, align: 'center' },
                   { field: 'T_JTPJB', title: '与集团平均比', width: 120, align: 'center' }
]]
              });


              //300MW等级
              $('#Unit30').datagrid({
                  title: '300MW煤机供电煤耗完成情况',
                  iconCls: 'icon-search',
                  nowrap: true,
                  autoRowHeight: false,
                  striped: true,
                  height: ihight - 130,
                  align: 'center',
                  collapsible: true,
                  loadMsg: '查询中,请稍等...',
                  url: 'ChartDetail.aspx?type=3',
                  sortName: 'T_COMPANYID',
                  sortOrder: 'asc',
                  remoteSort: false,
                  queryParams: { param: 'seachList' },

                  frozenColumns: [
                [{ field: 'T_DWNAME', title: '单位名称', width: 200, align: 'center'}]
    ],
                  columns: [[{ title: '各单位300MW煤机供电煤耗完成情况', colspan: 10, align: 'center'}],
                    [{ field: 'T_COUNT', title: '台数', width: 120, align: 'center' },
                   { field: 'T_USEHOUR', title: '利用小时', width: 120, align: 'center' },
                   { field: 'T_OF', title: '出力系数%', width: 120, align: 'center' },
                   { field: 'T_RDB', title: '热电比%', width: 120, align: 'center' },
                   { field: 'T_CYDL', title: '厂用电率（%）', width: 120, align: 'center' },
                   { field: 'T_GDMH', title: '供电煤耗', width: 120, align: 'center' },
                   { field: 'T_DBMH', title: '对标煤耗', width: 120, align: 'center' },
                   { field: 'T_GDL', title: '供电量', width: 120, align: 'center' },
                   { field: 'T_JTPJB', title: '与集团平均比', width: 120, align: 'center' }
]]
              });


              //200MW等级
              $('#Unit20').datagrid({
                  title: '200MW煤机供电煤耗完成情况',
                  iconCls: 'icon-search',
                  nowrap: true,
                  autoRowHeight: false,
                  striped: true,
                  height: ihight - 130,
                  align: 'center',
                  collapsible: true,
                  loadMsg: '查询中,请稍等...',
                  url: 'ChartDetail.aspx?type=4',
                  sortName: 'T_COMPANYID',
                  sortOrder: 'asc',
                  remoteSort: false,
                  queryParams: { param: 'seachList' },
                  frozenColumns: [
                [{ field: 'T_DWNAME', title: '单位名称', width: 200, align: 'center'}]
    ],
                  columns: [[{ title: '各单位200MW煤机供电煤耗完成情况', colspan: 10, align: 'center'}],
                    [{ field: 'T_COUNT', title: '台数', width: 120, align: 'center' },
                   { field: 'T_USEHOUR', title: '利用小时', width: 120, align: 'center' },
                   { field: 'T_OF', title: '出力系数%', width: 120, align: 'center' },
                   { field: 'T_RDB', title: '热电比%', width: 120, align: 'center' },
                   { field: 'T_CYDL', title: '厂用电率（%）', width: 120, align: 'center' },
                   { field: 'T_GDMH', title: '供电煤耗', width: 120, align: 'center' },
                   { field: 'T_DBMH', title: '对标煤耗', width: 120, align: 'center' },
                   { field: 'T_GDL', title: '供电量', width: 120, align: 'center' },
                   { field: 'T_JTPJB', title: '与集团平均比', width: 120, align: 'center' }
]]
              });


              //135MW等级
              $('#Unit13').datagrid({
                  title: '135MW煤机供电煤耗完成情况',
                  iconCls: 'icon-search',
                  nowrap: true,
                  autoRowHeight: false,
                  striped: true,
                  height: ihight - 130,
                  align: 'center',
                  collapsible: true,
                  loadMsg: '查询中,请稍等...',
                  url: 'ChartDetail.aspx?type=5',
                  sortName: 'T_COMPANYID',
                  sortOrder: 'asc',
                  remoteSort: false,
                  queryParams: { param: 'seachList' },
                  frozenColumns: [
                [{ field: 'T_DWNAME', title: '单位名称', width: 200, align: 'center'}]
    ],
                  columns: [[{ title: '各单位135MW煤机供电煤耗完成情况', colspan: 10, align: 'center'}],
                      [{ field: 'T_COUNT', title: '台数', width: 120, align: 'center' },
                   { field: 'T_USEHOUR', title: '利用小时', width: 120, align: 'center' },
                   { field: 'T_OF', title: '出力系数%', width: 120, align: 'center' },
                   { field: 'T_RDB', title: '热电比%', width: 120, align: 'center' },
                   { field: 'T_CYDL', title: '厂用电率（%）', width: 120, align: 'center' },
                   { field: 'T_GDMH', title: '供电煤耗', width: 120, align: 'center' },
                   { field: 'T_DBMH', title: '对标煤耗', width: 120, align: 'center' },
                   { field: 'T_GDL', title: '供电量', width: 120, align: 'center' },
                   { field: 'T_JTPJB', title: '与集团平均比', width: 120, align: 'center' }
]]
              });
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
<body style="background-color: #E3EDF8; font-size: 12px; font-family: 宋体">
    <form id="form1" runat="server">
    <span style="font-size: 30px; margin-left: 30%; font-weight: bolder; color: Blue;">各类型机组能耗指标情况</span>
    <hr style="color: Blue" />
    <div>
     排序顺序
        <asp:DropDownList ID="ddlType" runat="server"   OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="true" >
            <asp:ListItem Value="1">分公司</asp:ListItem>
            <asp:ListItem Value="2">电厂</asp:ListItem>
            <asp:ListItem Value="3">机组</asp:ListItem>

        </asp:DropDownList>
    </div>
    <br />
    <br />
    <div>
        <table id="Unit100" style="width: 1300px;" runat="server" visible="false">
        </table>
        <table id="Unit60" style="width: 1300px;" runat="server" visible="false">
        </table>
        <table id="Unit30" style="width: 1300px;" runat="server" visible="false">
        </table>
        <table id="Unit20" style="width: 1300px;" runat="server" visible="false">
        </table>
        <table id="Unit13" style="width: 1300px;" runat="server" visible="false">
        </table>
    </div>
    <asp:HiddenField ID="hidTime" runat="server" />
    </form>
</body>
</html>
