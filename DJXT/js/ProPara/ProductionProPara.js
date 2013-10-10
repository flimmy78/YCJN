$(document).ready(function () {

    $("#txt_Mt").attr("disabled", "disabled").css({ "cursor": "default", "background-color": "#CCCCCC" });
    $("#txt_Mad").attr("disabled", "disabled").css({ "cursor": "default", "background-color": "#CCCCCC" });
    $("#txt_Vdaf").attr("disabled", "disabled").css({ "cursor": "default", "background-color": "#CCCCCC" });
    $("#txt_St_ad").attr("disabled", "disabled").css({ "cursor": "default", "background-color": "#CCCCCC" });
    $("#txt_St_ar").attr("disabled", "disabled").css({ "cursor": "default", "background-color": "#CCCCCC" });
    $('#preserve').live("click", function () {
        var par = $("#sec_crew").find("option:selected").val();
        if (par == "-请选择-") {
            alert("请选择机组！");
        }
        else {
            $("#show").html("");
            $('#pre').show();
            //            $("#pre").attr('title', '煤质查询');
            var dlg = $('#pre').dialog({
        });
        dlg.parent().appendTo(jQuery("form:first"));
    }

});
})



function change_company() {
    var par = $("#sec_company").find("option:selected").val();
    if (par != "-请选择-") {
        $.post(
        "../datafile/GetProductionProPara.aspx",
        {
            sec_type_real: par
        },
    function (data) {
        var array = new Array();
        array = data.split('|');
        $("#sec_electric").empty();
        $("#sec_crew").empty();
        if (data == "") {
            $("#sec_electric").append("<option value=请选择>-请选择-</option>");
            $("#sec_crew").append("<option value=请选择>-请选择-</option>");
        }
        else {
            $("#sec_electric").append(array[0]);
            $("#sec_crew").append(array[1]);


        }
    },

    "html");
    }
    else {
        $("#sec_electric").empty();
        $("#sec_crew").empty();
        $("#sec_electric").append("<option value=请选择>-请选择-</option>");
        $("#sec_crew").append("<option value=请选择>-请选择-</option>");
    }
}
function change_electric() {
    var par = $("#sec_electric").find("option:selected").val();
    if (par != "-请选择-") {
        $.post(
                    "../DataFile/GetProductionProPara.aspx",
                    {
                        electric_id_real: par
                    },
                function (data) {
                    var array = new Array();
                    array = data.split('|');
                    $("#sec_crew").empty();
                    if (data == "") {
                        $("#sec_crew").append("<option value=请选择>-请选择-</option>");
                    }
                    else {
                        $("#sec_crew").append(array[0]);
                    }
                },

                "html");
    }
    else {
        $("#sec_crew").empty();
        $("#sec_crew").append("<option value=请选择>-请选择-</option>");
    }
}
function Load_Rating() {
    if ($('input:radio[id="rd_one"]').is(':checked')) {
        $("#txt_Mt").attr("disabled", "disabled").css({ "cursor": "default", "background-color": "#CCCCCC" });
        $("#txt_Mad").attr("disabled", "disabled").css({ "cursor": "default", "background-color": "#CCCCCC" });
        $("#txt_Vdaf").attr("disabled", "disabled").css({ "cursor": "default", "background-color": "#CCCCCC" });
        $("#txt_St_ad").attr("disabled", "disabled").css({ "cursor": "default", "background-color": "#CCCCCC" });
        $("#txt_St_ar").attr("disabled", "disabled").css({ "cursor": "default", "background-color": "#CCCCCC" });

        $("#txt_Car").removeAttr("disabled").css({ "cursor": "pointer", "background-color": "#FFFFFF" });
        $("#txt_Har").removeAttr("disabled").css({ "cursor": "pointer", "background-color": "#FFFFFF" });
        $("#txt_Oar").removeAttr("disabled").css({ "cursor": "pointer", "background-color": "#FFFFFF" });
        $("#txt_Nar").removeAttr("disabled").css({ "cursor": "pointer", "background-color": "#FFFFFF" });
        $("#txt_Sar").removeAttr("disabled").css({ "cursor": "pointer", "background-color": "#FFFFFF" });

        $("#tab_hide td[id='td_disable']").hide();

    }
    else {
        $("#txt_Car").attr("disabled", "disabled").css({ "cursor": "default", "background-color": "#CCCCCC" });
        $("#txt_Har").attr("disabled", "disabled").css({ "cursor": "default", "background-color": "#CCCCCC" });
        $("#txt_Oar").attr("disabled", "disabled").css({ "cursor": "default", "background-color": "#CCCCCC" });
        $("#txt_Nar").attr("disabled", "disabled").css({ "cursor": "default", "background-color": "#CCCCCC" });
        $("#txt_Sar").attr("disabled", "disabled").css({ "cursor": "default", "background-color": "#CCCCCC" });

        $("#txt_Mt").removeAttr("disabled").css({ "cursor": "pointer", "background-color": "#FFFFFF" });
        $("#txt_Mad").removeAttr("disabled").css({ "cursor": "pointer", "background-color": "#FFFFFF" });
        $("#txt_Vdaf").removeAttr("disabled").css({ "cursor": "pointer", "background-color": "#FFFFFF" });
        $("#txt_St_ad").removeAttr("disabled").css({ "cursor": "pointer", "background-color": "#FFFFFF" });
        $("#txt_St_ar").removeAttr("disabled").css({ "cursor": "pointer", "background-color": "#FFFFFF" });

        $("#tab_hide td[id='td_disable']").show();
    }
}

function query() {
    var par = $("#sec_crew").find("option:selected").val();
    if (par == "-请选择-") {
        alert("请选择机组！");
    }
    else {
        $("#txt_Car").attr("value", "");
        $("#txt_Har").attr("value", "");
        $("#txt_Oar").attr("value", "");
        $("#txt_Nar").attr("value", "");
        $("#txt_Sar").attr("value", "");
        $("#txt_afh").attr("value", "");
        $("#txt_alz").attr("value", "");
        $("#txt_Mt").attr("value", "");
        $("#txt_Mad").attr("value", "");
        $("#txt_Vdaf").attr("value", "");
        $("#txt_Qnet_ar").attr("value", "");
        $("#txt_St_ad").attr("value", "");
        $("#txt_St_ar").attr("value", "");
        $("#txt_Mar").attr("value", "");
        $("#txt_Aar").attr("value", "");

        if ($('input:radio[id="rd_one"]').is(':checked')) {
            par = "1|" + par;
        }
        else {
            par = "2|" + par;
        }

        $.post(
                    "../DataFile/GetProductionProPara.aspx",
                    {
                        PrpPara: par
                    },
                function (data) {
                    var array = new Array();
                    if (data != "") {
                        array = data.split(',');
                        if (par.split('|')[0] == "1") {
                            $("#txt_Car").attr("value", array[0]);
                            $("#txt_Har").attr("value", array[1]);
                            $("#txt_Oar").attr("value", array[2]);
                            $("#txt_Nar").attr("value", array[3]);
                            $("#txt_Sar").attr("value", array[4]);
                            $("#txt_afh").attr("value", array[5]);
                            $("#txt_alz").attr("value", array[6]);
                            $("#txt_Qnet_ar").attr("value", array[7]);
                            $("#txt_Mar").attr("value", array[8]);
                            $("#txt_Aar").attr("value", array[9]);
                        }
                        else {
                            $("#txt_afh").attr("value", array[0]);
                            $("#txt_alz").attr("value", array[1]);
                            $("#txt_Mt").attr("value", array[2]);
                            $("#txt_Mad").attr("value", array[3]);
                            $("#txt_Vdaf").attr("value", array[4]);
                            $("#txt_Qnet_ar").attr("value", array[5]);
                            $("#txt_St_ad").attr("value", array[6]);
                            $("#txt_St_ar").attr("value", array[7]);
                            $("#txt_Mar").attr("value", array[8]);
                            $("#txt_Aar").attr("value", array[9]);
                        }
                    }
                },

                "html");
    }
}

function save() {
    if ($("#sec_crew").find("option:selected").val() == "-请选择-") {
        alert("请选择机组！");
    }
    else if ($('input:radio[id="rd_one"]').is(':checked')) {
        if ((($("#txt_Car").val() == "") || ($("#txt_Har").val() == "") || ($("#txt_Oar").val() == "") || ($("#txt_Nar").val() == "") || ($("#txt_Sar").val() == "") || ($("#txt_afh").val() == "") || ($("#txt_alz").val() == "") || ($("#txt_Qnet_ar").val() == "") || ($("#txt_Mar").val() == "") || ($("#txt_Aar").val() == "")))
        { alert("值不能为空！"); }
        else {
            var par = "";
            if ($('input:radio[id="rd_one"]').is(':checked')) {
                par = "1|'" + $("#sec_crew").find("option:selected").val() + "'," + $("#txt_Car").val() + "," + $("#txt_Har").val() + "," + $("#txt_Oar").val() + ","
             + $("#txt_Nar").val() + "," + $("#txt_Sar").val() + "," + $("#txt_afh").val() + "," + $("#txt_alz").val() + ","
              + $("#txt_Qnet_ar").val() + "," + $("#txt_Mar").val() + "," + $("#txt_Aar").val();
            }
            else {
                par = "2|'" + $("#sec_crew").find("option:selected").val() + "'," + $("#txt_afh").val() + "," + $("#txt_alz").val() + "," + $("#txt_Mt").val() + "," +
            $("#txt_Mad").val() + "," + $("#txt_Vdaf").val() + "," + $("#txt_Qnet_ar").val() + "," + $("#txt_St_ad").val() + "," + $("#txt_St_ar").val() + "," +
            $("#txt_Mar").val() + "," + $("#txt_Aar").val();
            }
            $.post(
                    "../DataFile/GetProductionProPara.aspx",
                    {
                        PrpPara_insert: par
                    },
                function (data) {
                    if (data == -1) {
                        alert("数据保存成功！");
                    }
                },

                "html");
        }
    }
    else if ($('input:radio[id="rd_second"]').is(':checked')) {
        if (($("#txt_afh").val() == "") || ($("#txt_alz").val() == "") || ($("#txt_Mt").val() == "") || ($("#txt_Mad").val() == "") || ($("#txt_Vdaf").val() == "") || ($("#txt_Qnet_ar").val() == "") || ($("#txt_St_ad").val() == "") || ($("#txt_St_ar").val() == "") || ($("#txt_Mar").val() == "") || ($("#txt_Aar").val() == ""))
        { alert("值不能为空！"); }
        else {
            var par = "";
            if ($('input:radio[id="rd_one"]').is(':checked')) {
                par = "1|'" + $("#sec_crew").find("option:selected").val() + "'," + $("#txt_Car").val() + "," + $("#txt_Har").val() + "," + $("#txt_Oar").val() + ","
             + $("#txt_Nar").val() + "," + $("#txt_Sar").val() + "," + $("#txt_afh").val() + "," + $("#txt_alz").val() + ","
              + $("#txt_Qnet_ar").val() + "," + $("#txt_Mar").val() + "," + $("#txt_Aar").val();
            }
            else {
                par = "2|'" + $("#sec_crew").find("option:selected").val() + "'," + $("#txt_afh").val() + "," + $("#txt_alz").val() + "," + $("#txt_Mt").val() + "," +
            $("#txt_Mad").val() + "," + $("#txt_Vdaf").val() + "," + $("#txt_Qnet_ar").val() + "," + $("#txt_St_ad").val() + "," + $("#txt_St_ar").val() + "," +
            $("#txt_Mar").val() + "," + $("#txt_Aar").val();
            }
            $.post(
                    "../DataFile/GetProductionProPara.aspx",
                    {
                        PrpPara_insert: par
                    },
                function (data) {
                    if (data == -1) {
                        alert("数据保存成功！");
                    }
                },

                "html");
        }
    }
    else {



    }

}



function GridSta(list) {
    $('#gridItem').datagrid({
        nowrap: true,
        autoRowHeight: false,
        fitColumns: true,
        striped: true,
        align: 'center',
        loadMsg: "正在努力为您加载数据", //加载数据时向用户展示的语句
        collapsible: true,
        url: 'CompareAnalyse.aspx',
        sortName: 'T_DATETIME',
        sortOrder: 'asc',
        remoteSort: false,
        queryParams: { param: list },
        idField: 'T_DATETIME',
        columns: [[
                    { field: 'Pel', title: '机组负荷', width: $(window).width() * 0.2 * 0.98, align: 'center' },
                     { field: 'q_fd', title: '热耗率', width: $(window).width() * 0.2 * 0.98, align: 'center' },
                     { field: 'b_g', title: '供电煤耗', width: $(window).width() * 0.2 * 0.98, align: 'center' },
                    { field: 'Eta_b', title: '锅炉效率', width: $(window).width() * 0.2 * 0.98, align: 'center' },
                    { field: 'Eta_H', title: '高压缸效率', width: $(window).width() * 0.2 * 0.98, align: 'center' },
                    { field: 'Eta_M', title: '中压缸效率', width: $(window).width() * 0.2 * 0.98, align: 'center' },
                    { field: 'Rho', title: '厂用电率', width: $(window).width() * 0.2 * 0.98, align: 'center' },
                    { field: 'T_DATETIME', title: '时间', width: $(window).width() * 0.2 * 0.98, align: 'center' }
                ]],
        pagination: true,
        rownumbers: true
    });
}


function chkform() {
    if (document.getElementById("fileUp").value == "") {
        document.getElementById("fileUp").focus();
        alert('请选择您要导入的文件');
        return false;
    }
}

function pre_query() {
    var rating = "";
    if (($("#stime").val() != "") && ($("#etime").val() != "")) {
        var sTime = new Date($("#stime").val().replace(/-/g, "/")); //开始时间
        var eTime = new Date($("#etime").val().replace(/-/g, "/")); //结束时间
        if (parseInt((eTime.getTime() - sTime.getTime()) / parseInt(1000 * 3600)) > 1) {
            rating += $("#stime").val() + "," + $("#etime").val() + ";";
            rating += $("#sec_crew").find("option:selected").val();
            $.post(
                    "../DataFile/GetProductionProPara.aspx",
                    {
                        pre_query: rating
                    },
                function (data) {
                    if (data != "") {
                        $("#show").html(data);
                    }
                    else {
                        alert("没有查询到该机组相关的信息！");
                    }
                },

                "html");
        }
        else {
            alert("时间间隔过小！");
        }
    }
    else
    { alert("时间不能为空！"); }
}