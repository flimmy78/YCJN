<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_Main" Codebehind="Main.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>主页面</title>
    <style type="text/css">
        @import url(../css/help3.css);
    </style>
    <script src="../js/myJSFrame.js" type="text/javascript"></script>
    <script language="javascript">
<!--
        window.onerror = function () { return true; }
// -->
    </script>
    <script type="text/javascript">


function initHeight(){
	var h = document.documentElement.clientHeight;
	var h2 = h-57;
	//$("lefterIframe").style.height = $("lefter").style.height = $("splitLineV").style.height = $("main").style.height = (h2-8) + "px";
	$("lefterIframe").style.height = (h2-26)+"px";
	$("lefter").style.height = (h2-26)+"px";
	$("splitLineV").style.height = (h2-26)+"px";
	$("main").style.height = (h2-26) + "px";
	$("content").style.height = (h2 - 26) + "px";

}
function beginDrag(me,evt){
	evt = evt?evt:window.event;		
    var d = document.createElement("div");
    d.style.position = "absolute";	
	d.style.width = "0px";
	d.style.background = "url(images/multidashed.gif) repeat";
	d.style.zIndex = 9999;
	d.style.cursor = "e-resize";
	var detlaX = (evt.clientX || evt.pageX)-me.offsetLeft;
	
    d.style.top = "63px";
	d.style.left = me.offsetLeft+"px";
	//d.style.left = "185px";
	d.style.height = $("lefter").style.height;
	
    document.body.appendChild(d);
	
	if(document.all){
	    d.attachEvent("onmousemove",move);
		d.attachEvent("onmouseup",up);
		d.setCapture();
	}else{
		document.addEventListener("mousemove",move,true);
		document.addEventListener("mouseup",up,true);
		evt.stopPropagation();
		evt.preventDefault();
	}
			
	function move(evt){
	if(!document.all){evt.stopPropagation();}
		var k = (evt.clientX || evt.pageX)-detlaX;
		if(k<190 || k>400){return;}
		//d.style.left = k + "px";
		d.style.left = "182px";
		detlaX=k=null;
		
	}
	
	function up(evt){
		d.style.width = "0px";
		d.style.height = "0px";
		var leftBarWidth = parseInt(d.style.left)-4;
		if(document.all){
			d.detachEvent("onmousemove",move);
			d.detachEvent("onmouseup",up);
			d.releaseCapture();
		}else{
			document.removeEventListener("mousemove",move,true);
			document.removeEventListener("mouseup",up,true);
			evt.stopPropagation();
		}	
		d.parentNode.removeChild(d);		
		d=null;
		if(leftBarWidth>-1){
			$("lefterIframe").width = $("lefter").style.width = (leftBarWidth+4) + "px";					
		}
		$("splitLineV").style.left = (leftBarWidth+4)+"px";		
		$("mainPaddingLeft").style.paddingLeft = (leftBarWidth+10)+"px";		

	}
}
function toggleHandle(obj){
	if(!obj.getAttribute("leftbar") || obj.getAttribute("leftbar")=="on"){
		obj.setAttribute("leftbar","off");
		obj.setAttribute("startLeft",parseInt($("splitLineV").style.left));
		$("lefterIframe").width = $("lefter").style.width = "1px";
		$("splitLineV").style.left = "0px";						
		$("mainPaddingLeft").style.paddingLeft = "6px";
		$(obj).style.backgroundPositionX = "-21px";		
	}else{
		obj.setAttribute("leftbar","on");
		var startLeft = obj.getAttribute("startLeft");
		$("lefterIframe").width = $("lefter").style.width = (startLeft-0)+"px";
		$("splitLineV").style.left = startLeft+"px";
		$("mainPaddingLeft").style.paddingLeft = (parseInt(startLeft) + 6) + "px";	
		$(obj).style.backgroundPositionX = "-11px";			
	}
}
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="box" id="box">
        <div class="header">
            <iframe src="Head.aspx" height="50" width="100%" frameborder="0" scrolling="no">
            </iframe>
        </div>
        <div class="menu_1">
            <iframe src="RootMenu.aspx" height="32" width="100%" frameborder="0" scrolling="no">
            </iframe>
        </div>
        <div class="mainBox">
            <div class="lefter" id="lefter">
                <iframe src="LeftMenuTree.aspx" id="lefterIframe" name="lefterIframe" height="100%" width="182px"
                    frameborder="0"></iframe>
            </div>
                <div class="mainPaddingLeft" id="mainPaddingLeft" >
                <div class="splitLineV" id="splitLineV" onmousedown="beginDrag(this,event)"  onclick="toggleHandle(this)"  leftbar>
                </div>
                <div class="main" id="main">
                    <iframe src="ContentPage.aspx" width="100%" height="100%" frameborder="0" scrolling="auto"
                        name="content" id="content"></iframe>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
$("box").onresize = initHeight;
$.DOMReady(initHeight);
//$("lefterIframe").src = "LeftMenuTree.aspx";
    </script>

    </form>
</body>
</html>
