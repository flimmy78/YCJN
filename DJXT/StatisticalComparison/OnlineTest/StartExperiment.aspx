<%@ Page Language="C#" AutoEventWireup="true" Inherits="StartExperiment" CodeBehind="StartExperiment.aspx.cs" %>

<%@ Register Assembly="Wisesoft.Web.Control" Namespace="Wisesoft.Web.Control" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>维护全局配置</title>
    <link rel="stylesheet" type="text/css" href="../../css/master.css" />
     <script type="text/javascript" src="../../js/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../../js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript">

     
        function GetReportName() {
            var report = document.getElementById('txtReportName');
            var SY = document.getElementById('ddlSYName');
            var compute = document.getElementById('ddlUnit');
            var plant = document.getElementById('ddlPlant');
            var sgk = document.getElementById('sgk');
            //var sygk = document.getElementById('ddlSYGK');

            
            var selectedComputeID = "";
            var selectedSYName = "";
            var reportName = "";
            for (var computeIndex = 0; computeIndex < compute.options.length; computeIndex++) {
                if (compute.options[computeIndex].selected)
                    selectedComputeID = compute.options[computeIndex].text;
            }
            for (var plantIndex = 0; plantIndex < plant.options.length; plantIndex++) {
                if (plant.options[plantIndex].selected)
                    selectedPlantID = plant.options[plantIndex].text;
            }
            for (var JDNameindex = 0; JDNameindex < SY.options.length; JDNameindex++) {
                if (SY.options[JDNameindex].selected)
                    selectedSYName = SY.options[JDNameindex].text;
            }

            var myDate = new Date();
            var month = myDate.getMonth() + 1;
            var day = "";
            if (month < 10)
                month = "0" + month;

            if (myDate.getDate() < 10)
                day = "0" + myDate.getDate();
            else
                day = myDate.getDate();
            var hours = myDate.getHours();
            var minute = myDate.getMinutes();
            var second = myDate.getSeconds();
            var time = hours + ":" + minute + ":" + second;
            if (selectedComputeID == "1")
                selectedComputeID = "5";
            else if (selectedComputeID == "2")
                selectedComputeID = "6";
            report.value = selectedPlantID + selectedComputeID + selectedSYName + "-" + myDate.getFullYear() + month + day + " " + time;


            //隐藏显示工况
//            var sygk = document.getElementById('ddlSYGK');
//            //alert(sygk.options[sygk.selectedIndex].value);
//            //alert("s");
//            if (SY.options[SY.selectedIndex].text == '锅炉本体试验' || SY.options[SY.selectedIndex].text == '汽轮机热耗试验' || SY.options[SY.selectedIndex].text == '给水泵性能试验' || SY.options[SY.selectedIndex].text == '凝汽器性能试验') {
//                sygk.setAttribute('style', 'display:block;margin-left: 2px;');
//                sgk.setAttribute('style', 'display:block');
//                if (SY.options[SY.selectedIndex].text == '给水泵性能试验') {
//                    //alert(sygk.options[5].Enabled);
//                    //sygk.options.remove(5);
//                    sygk.options[1].setAttribute('style', 'display:none');
//                    sygk.options[2].setAttribute('style', 'display:none');
//                    sygk.options[3].setAttribute('style', 'display:none');
//                    sygk.options[4].setAttribute('style', 'display:none');
//                    sygk.options[5].setAttribute('style', 'display:block');
//                    sygk.options[6].setAttribute('style', 'display:block');
//                    sygk.options[7].setAttribute('style', 'display:block');
//                    sygk.options[8].setAttribute('style', 'display:block');
//                    sygk.options[9].setAttribute('style', 'display:block');
//                }
//                if (SY.options[SY.selectedIndex].text == '汽轮机热耗试验') {
//                    sygk.options[1].setAttribute('style', 'display:block');
//                    sygk.options[2].setAttribute('style', 'display:block');
//                    sygk.options[3].setAttribute('style', 'display:block');
//                    sygk.options[4].setAttribute('style', 'display:block');
//                    sygk.options[5].setAttribute('style', 'display:block');
//                    sygk.options[6].setAttribute('style', 'display:block');
//                    sygk.options[7].setAttribute('style', 'display:block');
//                    sygk.options[8].setAttribute('style', 'display:block');
//                    sygk.options[9].setAttribute('style', 'display:block');
//                }
//                if (SY.options[SY.selectedIndex].text == '锅炉本体试验') {
//                    sygk.options[1].setAttribute('style', 'display:block');
//                    sygk.options[2].setAttribute('style', 'display:none');
//                    sygk.options[3].setAttribute('style', 'display:none');
//                    sygk.options[4].setAttribute('style', 'display:none');
//                    sygk.options[5].setAttribute('style', 'display:block');
//                    sygk.options[6].setAttribute('style', 'display:none');
//                    sygk.options[7].setAttribute('style', 'display:block');
//                    sygk.options[8].setAttribute('style', 'display:none');
//                    sygk.options[9].setAttribute('style', 'display:block');
//                }
//                if (SY.options[SY.selectedIndex].text == '凝汽器性能试验') {
//                    sygk.options[1].setAttribute('style', 'display:none');
//                    sygk.options[2].setAttribute('style', 'display:none');
//                    sygk.options[3].setAttribute('style', 'display:none');
//                    sygk.options[4].setAttribute('style', 'display:none');
//                    sygk.options[5].setAttribute('style', 'display:block');
//                    sygk.options[6].setAttribute('style', 'display:block');
//                    sygk.options[7].setAttribute('style', 'display:block');
//                    sygk.options[8].setAttribute('style', 'display:block');
//                    sygk.options[9].setAttribute('style', 'display:block');
//                }

//            }
//            else {
//                sygk.setAttribute('style', 'display:none;margin-left: 2px;');
//                sgk.setAttribute('style', 'display:none');
//            }
//            if (SY.options[SY.selectedIndex].text == '锅炉本体试验') { 
//            
//            
//            }

        }


        function check(v) {
            var maxTime = document.getElementById('hddMaxTime').value;
            if (v.id == 'txtSYTime' && parseInt(v.value) > maxTime) {
                alert('试验时长不能超过' + maxTime + '分钟');
                return;
            }
            var SplitTime = document.getElementById('hddMinSplitTime').value;
            if (v.id == 'txtSplitTime' && parseInt(v.value) < SplitTime) {
                alert('采样间隔时间不能小于' + SplitTime + '秒');
                return;
            }
        }

        function linkExpriment() {
            var reportid = document.getElementById('hddMaxReportID').value;
            window.open('DisplayReport.aspx?ReportID=' + reportid, "_blank");
        }
//        function confir(x,y) {
//            if (confirm("不明泄漏率为'"+x+"'，超出限值'"+y+"'，是否继续试验？")==true) {
//                document.getElementById('hidvalue').value = 1;
//            }
//            else {
//                return false;
//            }
        //        }
        function confir() {
            var x = $("#hidX").val();
            var y = $("#hidY").val();
            if (confirm("不明泄漏率为'" + x + "'，超出限值'" + y + "'，是否继续试验？")) {
                //document.getElementById('hidvalue').value = 0;
                $("#hidvalue").val(0);
                return true;
            }
            else {
                //document.getElementById('Timer1').attributes(); //setAttribute("Enabled", false);
               // var t = document.getElementById('Timer1');
                //t.setAttribute("Enabled", false);
                //var timer = $find("Timer1");
                //document.getElementById('hidvalue').value = 1;
                //stopTimer();
                $("#hidvalue").val(1);
                return true;
            }
        }
       function confirs(){
          var x = $("#hidX").val();
          var y = $("#hidY").val();
          alert("不明泄漏率'"+x+"'在限值'"+y+"'范围内，试验继续，继续试验!");
          $("#hidvalue").val(0);
          return true;
       }

        function setTimer() {
            var timer = $find("Timer1");
            Sys.Debug.trace(timer.get_interval());
            timer.set_interval(100);
            Sys.Debug.trace(timer.get_interval());
        }

        function startTimer() {
            var timer = $find("Timer1");
            timer._startTimer();
        }

        function stopTimer() {
            var timer = $find("Timer1");
            timer._stopTimer();
        }
    </script>
</head>
<body onload="GetReportName();" style="background-color: #E3EDF8; font-size: 12px;
    font-family: 宋体">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <img alt="" src="../../img/Experiment.gif" style="width: 99%; height: 38px" />
            <div style="width: 99%">
                <div class="tablediv2">
                    <div class="rowdiv3">
                        <table width="100%" border="0">
                            <tr>
                                <td height="5">
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="groupBox">
                    <table width="100%" class="groupBoxCaption">
                        <tr>
                            <td nowrap="noWrap">
                                启停热力试验
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="0" cellpadding="4">
                        <tr>
                            <td colspan="3">
                                省公司<asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedChanged"
                                    AutoPostBack="true">
                                    <%-- <asp:ListItem Selected="True"  Value="0">--请选择省公司--</asp:ListItem>--%>
                                </asp:DropDownList>
                                &nbsp; &nbsp; &nbsp; &nbsp; 电厂<asp:DropDownList ID="ddlPlant" runat="server" OnSelectedIndexChanged="ddlPlant_SelectedChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Selected="True" Value="0">--请选择电厂--</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp; &nbsp; &nbsp; &nbsp; 机组<asp:DropDownList ID="ddlUnit" runat="server" OnSelectedIndexChanged="ddlUnit_SelectedChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Selected="True" Value="0">--请选择机组--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td style="width: 308px" nowrap="noWrap">
                                <span class="groupBoxLabel4">试验名称</span>
                                <asp:DropDownList ID="ddlSYName" runat="server" Width="168px" Style="margin-left: 2px"  onchange="GetReportName();"   OnSelectedIndexChanged="ddlSYName_SelectedChanged" AutoPostBack="true">
                                    <asp:ListItem Selected="True" Value="0">--请选择试验名称--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td colspan="2" valign="top">
                                <div>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Literal ID="sgk" runat="server" ></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSYGK" runat="server" Style="margin-left: 2px;" visible="false">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtHeater" runat="server" Visible="false"></asp:TextBox>
                                                <span id="hm" runat="server" visible="false">mm</span>
                                                <asp:TextBox Enabled="false" runat="server" ID="txtYJ" Text="一机两泵" Visible="false"
                                                    Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                              <div runat="server" id="divContent" visible="false">
                                                <asp:Literal ID="litContent" runat="server" Text="凝汽器循环冷却水"></asp:Literal>
                                                <asp:DropDownList ID="ddlSelect" runat="server">
                                                    <asp:ListItem Value="0">--请选择--</asp:ListItem>
                                                    <asp:ListItem Value="1">进口</asp:ListItem>
                                                    <asp:ListItem Value="2">出口</asp:ListItem>
                                                </asp:DropDownList>
                                                蝶阀开度
                                                 <asp:TextBox  runat="server" ID="txtKD"  Width="100px"></asp:TextBox>%
                                                    </div>
                                            </td>

                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <span class="groupBoxLabel4">报告名称</span>
                                <input type="text" id="txtReportName" runat="server" size="12" style="overflow: hidden;
                                    width: 350px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="groupBoxLabel4">开始时间</span><cc1:CalenderBox Style="margin-left: 5px;
                                    width: 162px" ID="calSYStartTime" runat="server" BackColor="White"></cc1:CalenderBox>
                            </td>
                            <td>
                                <span class="groupBoxLabel4">试验时长</span><input type="text" id="txtSYTime" runat="server"
                                    onblur="check(this);" size="12" style="margin-left: 10px; overflow: hidden; width: 160px" />分
                            </td>
                            <%--<td>
                        <span class="groupBoxLabel4">结束时间</span><cc1:CalenderBox ID="calEndTime" Style="margin-left: 8px;
                            width: 250px" runat="server" BackColor="White"></cc1:CalenderBox>
                            
                    </td>--%>
                        </tr>
                        <tr>
                        </tr>
                        <tr>
                            <td>
                                <span class="groupBoxLabel4">采样间隔</span><input type="text" id="txtSplitTime" runat="server"
                                    onblur="check(this);" style="width: 160px; overflow: hidden; margin-left: 5px;" />秒
                            </td>
                            <td>
                                <span class="groupBoxLabel3">试验人</span><input type="text" id="txtSYR" runat="server"
                                    size="12" style="margin-left: 14px; overflow: hidden; width: 160px" />
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" >
                                        </asp:Timer>
                                        <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                        </tr>
                    </table>
                </div>
            </div>
            <div style="float: right; margin-right: 20px; margin-top: 10px">
                <input id="hddMaxReportID" type="hidden" runat="server" />
                <asp:Button ID="btnStartEnd" runat="server" Text="开始试验" OnClick="btnStartEnd_Click" />
                <asp:Button ID="btnView" runat="server" Text="查看当前试验" /></div>
                <asp:Button ID="btnHelp" runat="server" OnClick="btnHelp_Click" OnClientClick="return confir()"   Width="1" Height="1"/>
                <asp:Button ID="btnHelps" runat="server" OnClick="btnHelp_Click" OnClientClick="return confirs()"   Width="1" Height="1"/>

            <div>
                <input id="hddMaxTime" type="hidden" runat="server" />
                <input id="hddMinSplitTime" type="hidden" runat="server" />
                <input id="hidvalue" type="hidden" runat="server" />
                <input id="hidX" type="hidden" runat="server" />
                <input id="hidY" type="hidden" runat="server" />

            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnStartEnd" />
            <asp:PostBackTrigger ControlID="btnView" />
            <asp:PostBackTrigger ControlID="btnHelp" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
