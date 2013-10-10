<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Analysis.aspx.cs" Inherits="YJJX.EquipmentReliable.ReliableAnalysis" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 
    <title>可靠性分析</title>
</head>
<style type="text/css">
    body
    {
        margin: 0;
        padding: 0;
        background-color: #e3edf8;
        background-image: url(http://10.180.66.99:8081/images/bg_main.png);
        background-repeat: no-repeat;
        background-position: right 100px;
    }
    .wrapper
    {
        width: 100%;
    }
	.wrap
	{
		width: 100%;
		min-width: 1000px;
		_width: expression((document.documentElement.clientWidth||document.body.clientWidth)<1000? "1000px" : "" );
	}
    .fix
    {
        zoom: 1;
    }
    .fix:after
    {
        content: ".";
        display: block;
        clear: both;
        height: 0;
        font-size: 0;
        line-height: 0;
        visibility: hidden;
    }
    .title_main
    {
        height: 40px;
        margin-top: 5px;
        background-image: url(http://10.180.66.99:8081/images/title_main.png);
        background-position: center;
        background-repeat: no-repeat;
    }
    .nav_main
    {
        width: 440px;
        height: 24px;
        float: right;
        margin-top: -28px;
    }
    .nav_main a
    {
        font-size: 14px;
        line-height: 24px;
        color: #888;
        margin-right: 5px;
    }
    .nav_main select
    {
        width: 90px;
        margin-left: 10px;
        border: #279ffb 1px solid;
    }
    .boxs
    {
        width: 90%;
        margin: 0 auto;
        padding-top: 20px;
    }
    .fl
    {
        float: left;
    }
    .fr
    {
        float: right;
    }
    .box_main
    {
        width: 49%;
        height: 160px;
        margin-top: 10px;
        border: #279ffb 1px solid;
        background-image: url(http://10.180.66.99:8081/images/bg_box.png);
    }
    .box_main_first
    {
        width: 49%;
        height: 110px;
        border: #279ffb 1px solid;
        background-image: url(http://10.180.66.99:8081/images/bg_box.png);
    }
    .box_main_second
    {
        width: 49%;
        height: 210px;
        margin-top: 10px;
        border: #279ffb 1px solid;
        background-image: url(http://10.180.66.99:8081/images/bg_box.png);
    }
    .box_main_third
    {
        width: 49%;
        height: 180px;
        margin-top: 10px;
        border: #279ffb 1px solid;
        background-image: url(http://10.180.66.99:8081/images/bg_box.png);
    }
    .box_main_fourth
    {
        width: 49%;
        height: 180px;
        border: #279ffb 1px solid;
        background-image: url(http://10.180.66.99:8081/images/bg_box.png);
    }
    .table_main
    {
        font-family: 'lucida grande' , tahoma, verdana, arial, sans-serif;
        font-size: 12px;
        border-collapse: collapse;
        border: 0px solid #CCB;
        width: 100%;
    }
    .table_main th
    {
        color: #0a2267;
        height: 24px;
    }
    .table_main td
    {
        color: #444;
        height: 20px;
        font-size: 12px;
        text-align: center;
    }
    .sbox
    {
        width: 300px;
        height: 160px;
        float: left;
        margin-right: 1px;
        overflow: hidden;
        width: 50%;
    }
    .sbox a
    {
        text-decoration: none;
    }
    .sbox1
    {
        width: 150px;
        height: 150px;
        float: right;
        margin-top: 5px;
        margin-right: 1px;
        text-align: right;
        overflow: hidden;
        position: relative;
        background-image: url(http://10.180.66.99:8081/images/rb_logo.png);
    }
    .sbtn
    {
        position: absolute;
        width: 38px;
        height: 45px;
        cursor: pointer;
    }
    .sbox_img
    {
        width: 100%;
        height: 100%;
        border: 0;
        margin: 0;
        padding: 0;
        cursor: pointer;
    }
    .win
    {
        position: absolute;
        left: 30%;
        top: 180px;
        width: 500px;
        border: #FAF3AB 4px solid;
        display: none;
        background-color: #fcfcfc;
    }
    .white_content
    {
        display: none;
        position: absolute;
        top: 10%;
        left: 20%;
        width: 700px;
        height: 400px;
        border: 10px solid lightblue;
        background-color: white;
    }
    
    .style1
    {
        width: 100%;
    }
    
.STYLE2 {
	color: #ff6e47;
	font-weight: bold;
}
    
</style>

<body>



<form name="form1" method="post" action="GCEconomicIndicators.aspx" id="form1">

<div class="wrap">
    <div class="wrapper title_main fix">
    </div>
    <div class="boxs">
        <div class="nav_main ">
            <select name="ddlSelect" onchange="javascript:setTimeout('__doPostBack(\'ddlSelect\',\'\')', 0)" id="ddlSelect" style="width:60px;">
	<option selected="selected" value="æœˆä»½">月份</option>
	<option value="å­£åº¦">季度</option>

</select>
            <select name="ddlYear" onchange="javascript:setTimeout('__doPostBack(\'ddlYear\',\'\')', 0)" id="ddlYear" style="width:84px;">
	<option value="2012">2012</option>
	<option selected="selected" value="2013">2013</option>

</select>
            <select name="ddlMonthQuarter" onchange="javascript:setTimeout('__doPostBack(\'ddlMonthQuarter\',\'\')', 0)" id="ddlMonthQuarter" style="width:80px;">
	<option value="1">1æœˆ</option>
	<option value="2">2æœˆ</option>
	<option value="3">3æœˆ</option>
	<option value="4">4æœˆ</option>
	<option value="5">5æœˆ</option>
	<option selected="selected" value="6">6æœˆ</option>

</select>
            <input type="submit" name="btn_query" value="æŸ¥è¯¢" id="btn_query" class="'btn_back';" />
        </div>
        <div class="box_main_first fl" id="box1" onmouseover="OnOver(1)" onmouseout="OnOut(1)">
            <table id="tb1000" class="table_main">
	<tr>
		<th style="text-align: right;">
                            ä¾›ç”µç…¤è€—
                        </th>
		<th style="text-align: left;">
                            (g/kwh)
                        </th>
		<th>
                            å¹³å‡å€¼
                        </th>
		<th>
                            æœ€ä¼˜æœºç»„
                        </th>
		<th>
                            æœ€ä¼˜å€¼
                        </th>
		<th>
                            æœ€å·®æœºç»„
                        </th>
		<th>
                            æœ€å·®å€¼
                        </th>
	</tr>
	<tr>
		<td>
                        </td>
		<td style="text-align: left; height:25px">
                            åˆ è®¡
                        </td>
		<td>
                        </td>
		<td style="cursor: pointer;">
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
	</tr>
	<tr>
		<td style="text-align: center;">
                            1000MW
                        </td>
		<td style="text-align: left; height :25px">
                           æ¹¿ å†·
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
	</tr>
	<tr>
		<td>
                        </td>
		<td style="text-align: left; height :25px">
                            ç©º å†·
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
	</tr>
</table>

        </div>
        <div class="box_main_fourth fr" id="box5" onmouseover="OnOver(5)" onmouseout="OnOut(5)">
            <table id="tb135" class="table_main">
	<tr>
		<th style="text-align: right;">
                            ä¾›ç”µç…¤è€—
                        </th>
		<th style="text-align: left;">
                            (g/kwh)
                        </th>
		<th>
                            å¹³å‡å€¼
                        </th>
		<th>
                            æœ€ä¼˜æœºç»„
                        </th>
		<th>
                            æœ€ä¼˜å€¼
                        </th>
		<th>
                            æœ€å·®æœºç»„
                        </th>
		<th>
                            æœ€å·®å€¼
                        </th>
	</tr>
	<tr>
		<td>
                        </td>
		<td style="text-align: left;">
                            åˆ è®¡
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
	</tr>
	<tr>
		<td>
                        </td>
		<td style="text-align: left; height: 25px;">
                            å¸¸è§„çº¯å‡
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
	</tr>
	<tr>
		<td>
                            135MW
                        </td>
		<td style="text-align: left;  height: 25px;">æµåŒ–åºŠçº¯å‡
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
	</tr>
	<tr>
		<td>
                        </td>
		<td style="text-align: left;  height: 25px;">
                            ç©ºå†·çº¯å‡
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
	</tr>
	<tr>
		<td>
                        </td>
		<td style="text-align: left;  height: 25px;">
                            å¸¸è§„ä¾›çƒ­
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
	</tr>
	<tr>
		<td>
                        </td>
		<td style="text-align: left;  height: 25px;">
                            æµåŒ–åºŠä¾›çƒ­
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
	</tr>
</table>

        </div>
        <div class="box_main_second fl" id="box2" onmouseover="OnOver(2)" onmouseout="OnOut(2)">
            <table id="tb600" class="table_main">
	<tr>
		<th style="text-align: right;">
                            ä¾›ç”µç…¤è€—
                        </th>
		<th style="text-align: left;">
                            (g/kwh)
                        </th>
		<th>
                            å¹³å‡å€¼
                        </th>
		<th>
                            æœ€ä¼˜æœºç»„
                        </th>
		<th>
                            æœ€ä¼˜å€¼
                        </th>
		<th>
                            æœ€å·®æœºç»„
                        </th>
		<th>
                            æœ€å·®å€¼
                        </th>
	</tr>
	<tr>
		<td>
                        </td>
		<td style="text-align: left; height :28px;">
                            åˆ è®¡
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
	</tr>
	<tr>
		<td>
                        </td>
		<td style="text-align: left; height :28px">
                            è¶…è¶…ä¸´ç•Œ
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
	</tr>
	<tr>
		<td>
                            600MW
                        </td>
		<td style="text-align: left; height :28px">
                            è¶…ä¸´ç•Œæ¹¿å†·
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
	</tr>
	<tr>
		<td>
                        </td>
		<td style="text-align: left; height :28px">
                            è¶…ä¸´ç•Œç©ºå†·
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
	</tr>
	<tr>
		<td>
                        </td>
		<td style="text-align: left; height :28px">
                            äºšä¸´ç•Œç©ºå†·
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
	</tr>
	<tr>
		<td>
                        </td>
		<td style="text-align: left; height :28px">
                            äºšä¸´ç•Œæ¹¿å†·
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
	</tr>
	<tr style="display:none">
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
	</tr>
</table>

        </div>
        <div class="box_main fr" id="box4" onmouseover="OnOver(4)" onmouseout="OnOut(4)">
            <table id="tb200" class="table_main">
	<tr>
		<th style="text-align: right;">
                            ä¾›ç”µç…¤è€—
                        </th>
		<th style="text-align: left;">
                            (g/kwh)
                        </th>
		<th>
                            å¹³å‡å€¼
                        </th>
		<th>
                            æœ€ä¼˜æœºç»„
                        </th>
		<th>
                            æœ€ä¼˜å€¼
                        </th>
		<th>
                            æœ€å·®æœºç»„
                        </th>
		<th>
                            æœ€å·®å€¼
                        </th>
	</tr>
	<tr>
		<td>
                        </td>
		<td style="text-align: left; height :25px">
                            åˆ è®¡
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
	</tr>
	<tr>
		<td>
                        </td>
		<td style="text-align: left; height :25px">
                            å¸¸è§„çº¯å‡
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
	</tr>
	<tr>
		<td>
                            200MW
                        </td>
		<td style="text-align: left; height :25px">
                            ç©ºå†·çº¯å‡
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
	</tr>
	<tr>
		<td>
                        </td>
		<td style="text-align: left; height :25px">
                            æµ åŒ– åºŠ
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
	</tr>
	<tr>
		<td>
                        </td>
		<td style="text-align: left; height :25px">
                            ä¾› çƒ­
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
	</tr>
</table>

        </div>
        <div class="box_main_third fl" id="box3" onmouseover="OnOver(3)" onmouseout="OnOut(3)">
            <table id="tb300" class="table_main">
	<tr>
		<th style="text-align: right;">
                            ä¾›ç”µç…¤è€—
                        </th>
		<th style="text-align: left;">
                            (g/kwh)
                        </th>
		<th>
                            å¹³å‡å€¼
                        </th>
		<th>
                            æœ€ä¼˜æœºç»„                        
                            </th>
		<th>
                            æœ€ä¼˜å€¼
                        </th>
		<th style="display:none"></th>
		<th>
                            æœ€å·®æœºç»„
                        </th>
		<th>
                            æœ€å·®å€¼
                        </th>
		<th style="display:none"></th>
		<th style="display:none"></th>
		<th style="display:none"></th>
	</tr>
	<tr>
		<td>
                        </td>
		<td style="text-align: left; height: 25px">
                            åˆ è®¡
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
	</tr>
	<tr>
		<td>
                        </td>
		<td style="text-align: left; height: 25px">
                            æ¹¿å†·ä¾›çƒ­
                        </td>
		<td>379.280</td>
		<td id="normalgu" onclick="initPic('trgood300')">æ‰¬å·ž7       </td>
		<td id="normalgv" onclick="ShowDiv('tdgood300')">368.977</td>
		<td id="tdbest1" style="display:none">368.97664</td>
		<td id="badunit" onclick="initPic('trbad300')">æœ›äº­11      </td>
		<td id="normalbv" onclick="ShowDiv('tdbad300')">381.774</td>
		<td id="tdbadest1" style="display:none">381.773719</td>
		<td id="trgood300" style="display:none">64</td>
		<td id="trbad300" style="display:none">1</td>
	</tr>
	<tr>
		<td>
                            300MW
                        </td>
		<td style="text-align: left; height: 25px">
                            æµ åŒ– åºŠ
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
	</tr>
	<tr>
		<td>
                        </td>
		<td style="text-align: left; height: 25px">
                            å¸¸è§„çº¯å‡
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
	</tr>
	<tr>
		<td>
                        </td>
		<td style="text-align: left; height: 25px">
                            ç©ºå†·ä¾›çƒ­
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
		<td>
                        </td>
	</tr>
</table>

        </div>
        <div class="box_main fr" style="border: none;">
            <div class="sbox1" id="ctrl">
                <div class="sbtn" style="left: 15px; top: 30px;" id="sbtn1" onmouseover="OnBtnOver(1)"
                    onmouseout="OnBtnOut(1)" onclick="ShowListDiv('1000')">
                </div>
                <div class="sbtn" style="left: 15px; top: 79px;" id="sbtn2" onmouseover="OnBtnOver(2)"
                    onmouseout="OnBtnOut(2)" onclick="ShowListDiv('600')">
                </div>
                <div class="sbtn" style="left: 58px; top: 103px;" id="sbtn3" onmouseover="OnBtnOver(3)"
                    onmouseout="OnBtnOut(3)" onclick="ShowListDiv('300')">
                </div>
                <div class="sbtn" style="left: 100px; top: 78px;" id="sbtn4" onmouseover="OnBtnOver(4)"
                    onmouseout="OnBtnOut(4)" onclick="ShowListDiv('200')">
                </div>
                <div class="sbtn" style="left: 100px; top: 28px;" id="sbtn5" onmouseover="OnBtnOver(5)"
                    onmouseout="OnBtnOut(5)" onclick="ShowListDiv('135')">
                </div>
                <div class="sbtn" style="left: 58px; top: 3px;" id="sbtn6" onmouseover="OnBtnOver(6)"
                    onmouseout="OnBtnOut(6)">
                </div>
                <div class="sbtn" style="left: 58px; top: 53px;" id="sbtn7" onmouseover="OnBtnOver(7)"
                    onmouseout="OnBtnOut(7)">
                </div>
            </div>
            <div class="sbox"  style="width:300px">
                <table class="style1">
                    <tr>
                        <td><div align="center" class="STYLE2">ä¾›ç”µç…¤è€—æœˆçº¿</div></td>
                  </tr>
                    <tr>
                        <td >
                            <img src="http://10.180.66.99:8081/images/curve.png" onclick = "ShowPicDiv()"/>
                       </td>
                    </tr>
                </table>
          </div>
        </div>
    </div>
    <div style="display:none">
    <span id="lb_id">Label</span>
    <input type="submit" name="btn_initId" value="btn_initId" id="btn_initId" />
    </div>
    <div class="win" id="win1">
        
    </div>
    <div class="win" id="win2">
        
    </div>
        <!--å¼¹å‡ºå±‚-->
    <div id="MyDiv" class="white_content" >
        <div>
            <span onclick = "Close('MyDiv')" style="float: right; cursor: pointer"><img src="http://10.180.66.99:8081/images/Close.gif"/></span>
        </div>
        <iframe id = "showiframe"  marginwidth="0" marginheight="0" frameborder="0" scrolling="no"
        width="100%" height="95%"></iframe>
    </div>
    
    <div id="picDiv" class="white_content" >
        <div>
            <span onclick="ShowPicDiv()" style="float: right; cursor: pointer"><img src="http://10.180.66.99:8081/images/Close.gif"/></span>
        </div>
        <iframe id = "showpic"  marginwidth="0" marginheight="0" frameborder="0" scrolling="no"
        width="100%" height="95%"></iframe>
    </div>
    <div id="targetlistdiv" class="white_content">
        <div>
            <span onclick="closeListdiv()" style="float: right; cursor: pointer"><img src="http://10.180.66.99:8081/images/Close.gif"/></span>
        </div>
        <iframe id="listiframe" marginwidth="0" marginheight="0" frameborder="0" scrolling="auto" width="100%" height="95%"></iframe>
    </div>
    </div>
    </form>
</body>
</html>

<script type="text/javascript">
    function ShowWin(no) {
        var win = document.getElementById("win" + no.toString());
        if (win.style.display == "block") {
            HideWin(no);
        }
        else {
            win.style.display = "block";
        }
    }
    function HideWin(no) {
        var win = document.getElementById("win" + no.toString());
        win.style.display = "none";
    }

    function OnOver(no) {
        var box = document.getElementById("box" + no.toString());
        box.style.backgroundColor = "#e9dcd4";
        var ctrl = document.getElementById("ctrl");
        var s = no * 150;
        ctrl.style.backgroundPosition = "0px -" + s.toString() + "px";
    }
    function OnOut(no) {
        var box = document.getElementById("box" + no.toString());
        //box.style.borderColor = "#279ffb";
        box.style.backgroundImage = "url(../../images/bg_box.png)";
        box.style.backgroundColor = "#e3edf8";
        var ctrl = document.getElementById("ctrl");
        var s = no * 150;
        ctrl.style.backgroundPosition = "0 0";
    }
    function OnBtnOver(no) {
        if (no >= 1 && no <= 5) {
            var box = document.getElementById("box" + no.toString());
            //box.style.borderColor = "#F5FC78";
            //box.style.backgroundImage = "url(../../iamges/bg_box2.png)";
            box.style.backgroundColor = "#e9dcd4";

        }

        var ctrl = document.getElementById("ctrl");
        var s = no * 150;
        ctrl.style.backgroundPosition = "0px -" + s.toString() + "px";
    }
    function OnBtnOut(no) {
        if (no >= 1 && no <= 5) {
            var box = document.getElementById("box" + no.toString());
            //box.style.borderColor = "#279ffb";
            //box.style.backgroundImage = "url(../../images/bg_box.png)";
            box.style.backgroundColor = "#e3edf8";
        }

        var ctrl = document.getElementById("ctrl");
        var s = no * 150;
        ctrl.style.backgroundPosition = "0 0";
    }



    //å¼¹å‡º/å…³é—­ éšè—å±‚
    function ShowDiv(unitname) {

        var div_msg = document.getElementById('MyDiv');
        if (div_msg.style.display == 'block') {
            div_msg.style.display = 'none';

        }
        else {
            div_msg.style.display = 'block';
            if (unitname == "tdgood300") {
                var div_msg2 = document.getElementById("trgood300");
                var unitId = div_msg2.innerHTML;

                document.getElementById("showiframe").src = "UnitMainIndex.aspx?unitId=" + unitId + "&indexload=300";
            }
            else if (unitname == "tdbad300") {
                var div_msg2 = document.getElementById("trbad300");
                var unitId = div_msg2.innerHTML;

                document.getElementById("showiframe").src = "UnitMainIndex.aspx?unitId=" + unitId + "&indexload=300";
            }
        }
    }
    function Close() {
        var div_msg = document.getElementById('MyDiv');
        div_msg.style.display = 'none';
    }


    //å¼¹å‡º/å…³é—­ éšè—å±‚
    function ShowPicDiv() {
        var div_msg = document.getElementById('picDiv');
        if (div_msg.style.display == 'block') {
            div_msg.style.display = 'none';

        }
        else {
            var div_msg2 = document.getElementById('lb_id');
            var unitId = div_msg2.innerHTML;
            div_msg.style.display = 'block';
            var month = document.getElementById('ddlMonthQuarter');
            var type = document.getElementById('ddlSelect');
            var year = document.getElementById('ddlYear');
            document.getElementById("showpic").src = "unitload_month.aspx?unitId=" + unitId + "&month=" + month.value + "&type=" + type.value + "&year=" + year.value;
        }
    }


    //æ›´æ–°å°çª—
    function updatePic(str) {
        if (str == "tdgood300") {
            var div_msg2 = document.getElementById("trgood300");
            var unitId = div_msg2.innerHTML;
            document.getElementById("scanPicDiv").src = "unitload_month.aspx?unitId=" + unitId;

        }
    }

    //å¼¹å‡º/å…³é—­ éšè—å±‚
    function ShowListDiv(unittype) {
        var div_msg = document.getElementById('targetlistdiv');
        var stryear = document.getElementById('ddlYear');

        var index = stryear.selectedIndex;

        var Value = stryear.options[index].value;
        var text = stryear.options[index].text;

        var strmonth = document.getElementById('ddlMonthQuarter');
        var index1 = strmonth.selectedIndex;

        var Value1 = strmonth.options[index1].value;
        var text1 = strmonth.options[index1].text;

        div_msg.style.display = 'block';
        var url = encodeURI("&year=" + text + "&month=" + text1);
        document.getElementById("listiframe").src = "Target_List.aspx?unitType=" + unittype + url;
    }
    function setYearIndex(index) {
        var stryear = document.getElementById('ddlYear');
        stryear.selectedIndex = index;
    }
    function setMonthIndex(index) {
        var strmonth = document.getElementById('ddlMonthQuarter');
        strmonth.selectedIndex = index;
    }

    function closeListdiv() {
        var div_msg = document.getElementById('targetlistdiv');
        div_msg.style.display = 'none';
    }
    window.onload = function () {
        parent.disableNav();

    }

    function initPic(td_id) {
        var td = document.getElementById(td_id);
        var lb = document.getElementById('lb_id');
        lb.innerHTML = td.innerHTML;
    }
</script>

