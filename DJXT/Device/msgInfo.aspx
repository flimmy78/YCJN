<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="msgInfo.aspx.cs" Inherits="DJXT.Device.msgInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>详细信息查看</title>
     <link href="../css/SIS.css" rel="stylesheet" type="text/css" />
</head>
<body>
   <form id="form1" runat="server">
    <div id="Div1">
        <img id="imgTop" height="38px;" src="../img/xxxx.bmp" width="100%" />
    </div>
    <div>
         <table width="90%" border="1" align="center" class="bg" valign="top" bordercolorlight="#999999" bordercolordark="#FFFFFF" cellspacing="0" cellpadding="1" id="CX1">           
           <tr bgcolor="#CFE6FC"><td align="center" style="width: 8%;">点检项部位</td>
           <td align="center"  style="width: 8%;">点检项描述</td>
           <td align="center"  style="width: 8%;">点检项检查内容</td>
           <td align="center"  style="width: 8%;">点检类型</td>
           <td align="center"  style="width: 8%;">设备状态</td>
           <td align="center"  style="width: 8%;">单位</td>
           <td align="center"  style="width: 8%;">测量上限</td>
           <td align="center"  style="width: 8%;">测量下限</td>
           <td align="center"  style="width: 8%;">是否频谱</td> 
            <td align="center"  style="width: 8%;">周期数值</td>        
           <td align="center"  style="width: 8%;">周期类型</td>
          
           <td align="center"  style="width: 8%;">开始时间</td>
           </tr>
           <div id="show" runat="server"></div>
         </table>
    </div>
    </form>
</body>
</html>
