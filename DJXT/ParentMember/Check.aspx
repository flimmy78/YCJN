<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Check.aspx.cs" Inherits="DJXT.ParentMember.Check" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="../css/djxt.css" rel="stylesheet" type="text/css" />
    <link href="../css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />

    <script src="../jQueryEasyUI/jquery-1.6.2.js" type="text/javascript"></script>

    <script src="../js/jquery.ztree.core-3.5.js" type="text/javascript"></script>

    <script src="../js/jquery.ztree.excheck-3.5.js" type="text/javascript"></script>

    <%--    <!--
	<script type="text/javascript" src="../../../js/jquery.ztree.exedit-3.5.js"></script>
	-->
--%>

    <script type="text/javascript">       
		<!--
		var id='';
		var setting = {
			view: {
				selectedMulti: false
			},
			check: {
				enable: true
			},
			data: {
				simpleData: {
					enable: true
				}
			},
			callback: {
				beforeCheck: beforeCheck,
				onCheck: onCheck
			}
		};
		
		function beforeCheck(treeId, treeNode) {
			return (treeNode.doCheck !== false);
		}
		function onCheck(e, treeId, treeNode) {
		    if(treeNode.checked==true){
		        id+=treeNode.ide+ ',';
		    }else{
		        id=id.replace(treeNode.ide+',','');
		    }
		}		

        var code, log;
		function checkNode(e) {
			var zTree = $.fn.zTree.getZTreeObj("treeDemo"),
			type = e.data.type,
			nodes = zTree.getSelectedNodes();
			if (type.indexOf("All")<0 && nodes.length == 0) {
				alert("请先选择一个节点");
			}

			if (type == "checkAllTrue") {
				zTree.checkAllNodes(true);
			} else if (type == "checkAllFalse") {
				zTree.checkAllNodes(false);
			}
		}

		$(document).ready(function(){

            $.post("Check.aspx", { param: ''}, function (data) {
                var  zNodes = eval(data.menu);
                $.fn.zTree.init($("#treeDemo"), setting, zNodes);
            },'json');
			$("#checkAllTrue").bind("click", {type:"checkAllTrue"}, checkNode);
			$("#checkAllFalse").bind("click", {type:"checkAllFalse"}, checkNode);
            
            $("#btnShow").click(function() {    
//                alert(id);
startOcx();
            }); 
		});
		
		function startOcx() {
            try 
            {
                    var ocx = document.getElementById('XSTCommOcx');
//                    ocx.SetData("Key_WebServiceBaseDir", "http://localhost/wsxx/WSXSTCommFor2TH.asmx");
//                    ocx.SetData("Key_InnerControl", "InnerControl_Communication");
                    ocx.SetData("Key_StartControl", "");
                }
                catch (ex) 
                {
                    alert(ex.description);
                alert("通信组件没有安装,需要下载安装!");
            }
        }

		//-->
    </script>

</head>
<body>
    &nbsp;&nbsp;&nbsp;&nbsp;全部节点--[ <a id="checkAllTrue" href="#" title="不管你有多NB，统统都要听我的！！"
        onclick="return false;">勾选</a> ] &nbsp;&nbsp;&nbsp;&nbsp;[ <a id="checkAllFalse"
            href="#" title="不管你有多NB，统统都要听我的！！" onclick="return false;">取消勾选</a> ]
    <div class="content_wrap">
        <div class="zTreeDemoBackground left">
            <ul id="treeDemo" class="ztree">
            </ul>
        </div>
    </div>
    <label id="btnShow" title="数据">
        获取数据</label>
    <object id=" XSTCommOcx " classid="CLSID:C5C95668-167F-4064-8596-382C2966CB7E" style="width: 100%;
        height: 100%" viewastext>
    </object>
    <%--    <script type="text/javascript">
//          function startOcx() {
//            try {
//                    var ocx = document.getElementById('XSTCommOcx');
//                    ocx.SetData("Key_ProjectNo", "华电");
//                    ocx.SetData("Key_WebServiceBaseDir", "http://10.3.30.18/DJ/");
//                    ocx.SetData("Key_UserAcount", "administrator");
//                    ocx.SetData("Key_LineIDStr", "1");//"1,2,3"
//                    ocx.SetData("Key_InnerControl", "InnerControl_Communication");
//                    ocx.SetData("Key_StartControl", "");
//                }
//            catch (ex) {
//                alert("通信组件没有安装,需要下载安装!");
//            }
//        }
        function startOcx() {
            try 
            {
                    var ocx = document.getElementById('XSTCommOcx');
//                    ocx.SetData("Key_WebServiceBaseDir", "http://localhost/wsxx/WSXSTCommFor2TH.asmx");
//                    ocx.SetData("Key_InnerControl", "InnerControl_Communication");
                    ocx.SetData("Key_StartControl", "");
                }
                catch (ex) 
                {
                    alert(ex.description);
                alert("通信组件没有安装,需要下载安装!");
            }
        }
        startOcx();
    </script>--%>
</body>
</html>
