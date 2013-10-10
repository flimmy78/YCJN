using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BLL;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;
using FineUI;
using Newtonsoft.Json.Linq;

namespace DJXT.Route
{
    public partial class RouteManageNew : System.Web.UI.Page
    {
        object obj = null;

        string errMsg = "";
        string resultMenu = "";

        public string title1 = "信息";

        DataTable dt = null;
        IList<Hashtable> list = null;

        ParmentBLL bll = new ParmentBLL();
        MemberBLL member = new MemberBLL();

        BLLManRoute bmr = new BLLManRoute();
        StringBuilder sb = new StringBuilder();
        BLL.BLLManDevice bmd = new BLLManDevice();

        protected void Page_Load(object sender, EventArgs e)
        {

            string param = Request["param"];

            if (param == "")
            { GetListTree(); }
            else
            {
                if (param == "DevicTree")
                {
                    string nodeKey = Request.Form["areaNodeKey"];
                    GetListDeviceTree(nodeKey);
                }
                else if (param == "AddLineInfo")
                {
                    string lineName = HttpUtility.UrlDecode(Request.Form["lineName"]);
                    string lineType = Request.Form["lineType"];
                    string lineGw = Request.Form["lineGw"];             //岗位ID
                    string lineParent = Request.Form["LineParentID"];   //总线路ID

                    SetLineInfo(lineName, lineType, lineGw, lineParent, out errMsg);
                }
                else if (param == "EditLineInfo")
                {
                    string lineID = Request.Form["lineID"];
                    string lineName = HttpUtility.UrlDecode(Request.Form["lineName"]);
                    string lineType = Request.Form["lineType"];
                    string lineGw = Request.Form["lineGw"];             //岗位ID
                    string lineParent = Request.Form["LineParentID"];

                    EditLineInfo(lineID, lineName, lineType, lineGw, lineParent, out errMsg);
                }
                else if (param == "AddAreaRelation")
                {
                    string nodeKey = Request.Form["nodeKey"];
                    string areaID = Request.Form["aID"];    //选择后的AREAID
                    string chkID = Request.Form["chkID"];   //已选择的AREAID 

                    AddAreaInfo(nodeKey, areaID, chkID);
                }
                else if (param == "DelArea")
                {
                    //删除区域功能
                    string areaID = Request.Form["AreaID"];
                    string nodeKey = Request.Form["nodeKey"];

                    DelArea(areaID, nodeKey);
                }
                else if (param == "AddDeviceRelation")
                {
                    //添加区域和设备关系
                    string sbID = Request.Form["sbID"];
                    string nodeKey = Request.Form["nodeKey"];

                    AddDeviceRelation(sbID, nodeKey);
                }
                else if (param == "SaveItem")
                {
                    //SBnodeKey:$('#NodeCheckedID').val,itemID:$("#DJXID").val(),itemBw:escape($("#DJXBW").val()),itemDesc:escape($("#DJXDESC").val())
                    //,itemContent:escape($("#DJXCONTENT").val()) ,itemObserve:escape($("#DJXObserve").val()),itemUnit:escape($("#DJXUnit").val()),itemType:$("#DJXType").val()
                    //,itemStatus:$("#DJXSelectStatus").val() ,itemStatusQJ:$("#DJXT_SATAUS").val() ,itemUpper:$("#DJXUpper").val() ,itemLower:$("#DJXLower").val(),itemSpectrum:$("#DJXSpectrum").val()
                    //,itemStartTime:$("#DJXSTARTTIME").val() ,itemPerValue:$("#DJXPERIODVALUE").val(),itemPerType:$("#DJXPERIODTYPE").val()

                    //string itemID = Request.Form["itemID"];     //点检向标识 
                    string nodeKey = Request.Form["SBnodeKey"]; //设备标识
                    string itemBw = HttpUtility.UrlDecode(Request.Form["itemBw"]);      //点检项目部位 
                    string itemDesc = HttpUtility.UrlDecode(Request.Form["itemDesc"]);  //点检项目描述 
                    string itemContent = HttpUtility.UrlDecode(Request.Form["itemContent"]); //点检项内容
                    string itemObserve = HttpUtility.UrlDecode(Request.Form["itemObserve"]); //观察名称
                    string itemUnit = HttpUtility.UrlDecode(Request.Form["itemUnit"]);       //点检项单位
                    string itemType = Request.Form["itemType"];         //点检项类型
                    string itemStatus = Request.Form["itemStatus"];     //设备状态
                    string itemStatusQJ = Request.Form["itemStatusQJ"];     //启动 停止
                    string itemUpper = Request.Form["itemUpper"];     //上限
                    string itemLower = Request.Form["itemLower"];     //下限
                    string itemSpectrum = Request.Form["itemSpectrum"];     //是否频谱
                    string itemStartTime = HttpUtility.UrlDecode(Request.Form["itemStartTime"]);   //开始时间
                    string itemPerValue = Request.Form["itemPerValue"];     //周期数值
                    string itemPerType = Request.Form["itemPerType"];       //周期类型

                    AddItem(nodeKey, itemBw, itemDesc, itemContent, itemObserve, itemUnit, itemType, itemStatus, itemStatusQJ, itemUpper, itemLower, itemSpectrum, itemStartTime, itemPerValue, itemPerType);


                }
                else if (param == "clickTree")
                {
                    string id = Request.Form["id"];
                    GetButton(id);
                }
                else if (param == "queryItem")
                {
                    string sbID = Request.Form["SBID"];
                    //string sTime = Request.Form["StartTime"];
                    //string sType = Request.Form["type"];

                    QueryDataByItem(sbID);
                }
                else if (param == "SaveRelation")
                {
                    string sbID = Request.Form["id"];
                    string id_key_new = Request.Form["id_key_new"];
                    string id_key_old = Request.Form["id_key_old"];
                    SaveRelation(sbID, id_key_new, id_key_old);
                }
            }
        }

        /// <summary>
        /// 初始化Tree结构
        /// </summary>
        private void GetListTree()
        {
            string[] res = bmr.GetTotalLineNameAndID().Split(',');

            if (res != null)
            {
                string zxlParentID = res[0];
                string zxlNodeID = res[1];
                string zxlName = res[2];

                sb.Append("[");
                sb.Append("{id:'0',pId:'" + zxlParentID + "',nodeID:'" + zxlNodeID + "',name:'" + zxlName + "',t:'" + zxlName + "', open:true},");

                dt = bmr.GetTableRoute(out errMsg);

                //线路和关系表拼接
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("{id:'" + dt.Rows[i]["T_NODEKEY"] + "',pId:'" + dt.Rows[i]["T_PARAENTID"] + "',nodeID:'" + dt.Rows[i]["T_NODEID"] + "',name:'" + dt.Rows[i]["T_ROUTENAME"] + "',t:'" + dt.Rows[i]["T_ROUTENAME"] + "'},");
                    }

                    DataTable dtArea = bmr.GetTableArea(out errMsg);

                    //区域和关系表拼接
                    if (dtArea != null && dtArea.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtArea.Rows.Count; j++)
                        {
                            sb.Append("{id:'" + dtArea.Rows[j]["T_NODEKEY"] + "',pId:'" + dtArea.Rows[j]["T_PARAENTID"] + "',nodeID:'" + dtArea.Rows[j]["T_NODEID"] + "',name:'" + dtArea.Rows[j]["T_AREANAME"] + "',t:'" + dtArea.Rows[j]["T_AREANAME"] + "'},");
                        }
                    }

                    //设备和区域关系拼接
                    DataTable dtDevice = bmr.GetTableAreaAndDevInfo(out errMsg);

                    if (dtDevice != null && dtDevice.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtDevice.Rows.Count; j++)
                        {
                            sb.Append("{id:'" + dtDevice.Rows[j]["T_NODEKEY"] + "',pId:'" + dtDevice.Rows[j]["T_PARAENTID"] + "',nodeID:'" + dtDevice.Rows[j]["T_NODEID"] + "',name:'" + dtDevice.Rows[j]["T_DEVICEDESC"] + "',t:'" + dtDevice.Rows[j]["T_DEVICEDESC"] + "'},");
                        }
                    }

                    //点检项和设备关系拼接
                    DataTable dtItem = bmr.GetTableDevAndItemInfo(out errMsg);

                    if (dtItem != null && dtItem.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtItem.Rows.Count; j++)
                        {
                            sb.Append("{id:'" + dtItem.Rows[j]["T_NODEKEY"] + "',pId:'" + dtItem.Rows[j]["T_PARAENTID"] + "',nodeID:'" + dtItem.Rows[j]["T_NODEID"] + "',name:'" + dtItem.Rows[j]["T_ITEMDESC"] + "',t:'" + dtItem.Rows[j]["T_ITEMDESC"] + "'},");
                        }
                    }

                    resultMenu = sb.ToString().TrimEnd(',') + "]";
                }
                else
                { resultMenu = sb.ToString().TrimEnd(',') + "]"; }

                obj = new
                {
                    count = 1,
                    menu = resultMenu
                };
                string result = JsonConvert.SerializeObject(obj);
                Response.Write(result);
                Response.End();
            }
        }

        /// <summary>
        /// 初始化设备树 
        /// </summary>
        private void GetListDeviceTree(string nodeKey)
        {
            int count = 1;
            DataTable dtDev = null;

            //获取此区域下所有设备信息
            if (nodeKey != "")
                dtDev = bmr.GetTableDevByNodeKey(nodeKey);

            //获取设备基础表中所有设别
            dt = bmr.RetTableDevice();

            //设备树结构拼接
            if (dt != null && dt.Rows.Count > 0)
            {
                sb.Append("[");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if ((bmr.RetGetNodeBy(dt.Rows[i]["T_DEVICEID"].ToString()) == true))
                    {
                        sb.Append("{id:'" + dt.Rows[i]["T_DEVICEID"] + "',pId:'" + dt.Rows[i]["T_PARENTID"] + "',name:'" + dt.Rows[i]["T_DEVICEDESC"] + "',t:'" + dt.Rows[i]["T_DEVICEDESC"] + "', open:true,nocheck:true},");//,nocheck:true
                    }
                    else
                    {
                        if (dt.Rows[i]["T_PARENTID"].ToString() == "0")
                            sb.Append("{id:'" + dt.Rows[i]["T_DEVICEID"] + "',pId:'" + dt.Rows[i]["T_PARENTID"] + "',name:'" + dt.Rows[i]["T_DEVICEDESC"] + "',t:'" + dt.Rows[i]["T_DEVICEDESC"] + "', open:true,nocheck:true},");
                        else
                        {
                            if (dtDev != null && dtDev.Rows.Count > 0)
                            {
                                int g = 0;
                                for (int j = 0; j < dtDev.Rows.Count; j++)
                                {

                                    if (dt.Rows[i]["T_DEVICEID"].ToString() == dtDev.Rows[j]["T_NODEID"].ToString())
                                    {
                                        sb.Append("{id:'" + dt.Rows[i]["T_DEVICEID"] + "',pId:'" + dt.Rows[i]["T_PARENTID"] + "',name:'" + dt.Rows[i]["T_DEVICEDESC"] + "',t:'" + dt.Rows[i]["T_DEVICEDESC"] + "',checked:true},");
                                        g = 1;
                                        break;
                                    }
                                }
                                if (g == 0)
                                    sb.Append("{id:'" + dt.Rows[i]["T_DEVICEID"] + "',pId:'" + dt.Rows[i]["T_PARENTID"] + "',name:'" + dt.Rows[i]["T_DEVICEDESC"] + "',t:'" + dt.Rows[i]["T_DEVICEDESC"] + "'},");
                            }
                            else
                            { sb.Append("{id:'" + dt.Rows[i]["T_DEVICEID"] + "',pId:'" + dt.Rows[i]["T_PARENTID"] + "',name:'" + dt.Rows[i]["T_DEVICEDESC"] + "',t:'" + dt.Rows[i]["T_DEVICEDESC"] + "'},"); }
                        }
                    }
                }
                resultMenu = sb.ToString().TrimEnd(',') + "]";
            }
            else
            { count = 0; }

            obj = new
            {
                count = count,
                menu = resultMenu
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();

        }

        /// <summary>
        /// 判断是否存在子节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns>1:总线路,2:线路</returns>
        private void GetButton(string treeId)
        {
            int i = -1;

            //线路
            string lID = "";
            string lName = "";
            string lOrgID = "";
            string lOrgName = "";
            string lType = "";
            //区域
            string areaID = "";
            string areaName = "";
            string areaParaentID = "";
            string devs = "";

            string json = "";
            string id_key = "";
            string itemDesc = "";
            string devName = "";


            string itemPosition = "";
            string itemContent = "";
            string itemType = "";
            string itemObserve = "";
            string itemI_Status = "";
            string itemUint = "";
            string itemLower = "";
            string itemUpper = "";
            string itemSpectrum = "";
            string itemStartTime = "";
            string itemPerodType = "";
            string itemPerodValue = "";
            string itemT_Status = "";


            DataRow drLine = null;
            DataRow drArea = null;
            DataRow drItem = null;
            IList<Hashtable> listArea = null;
            IList<Hashtable> listDev = null;
            IList<Hashtable> list = null;

            i = bmr.RetCount(treeId);



            //获取线路信息
            if (i == 2)
            {
                drLine = bmr.RetDataRowByNodeID(treeId);

                if (drLine != null)
                {
                    lID = drLine["T_ROUTEID"].ToString();
                    lName = drLine["T_ROUTENAME"].ToString();
                    lOrgID = drLine["T_ORGID"].ToString();
                    lType = drLine["I_TYPE"].ToString();
                    lOrgName = drLine["T_ORGDESC"].ToString();
                }

                list = bmr.RetListLineAndAreaRealtion(treeId);

                listArea = bmr.RetListAraeInfo();

            }

            //获取区域相关信息
            if (i == 3)
            {
                DataRow drLine1 = bmr.RetDataRowLineByAreaNodeKey(treeId);

                if (drLine1 != null)
                {
                    lName = drLine1["T_ROUTENAME"].ToString();
                }

                drArea = bmr.RetDataRowAreaByNodeID(treeId);

                if (drArea != null)
                {
                    areaID = drArea["T_AREAID"].ToString();
                    areaName = drArea["T_AREANAME"].ToString();
                    areaParaentID = drArea["T_PARAENTID"].ToString();
                }

                DataTable dtDev = bmr.RetDataTableDeviceByNodeID(treeId);

                if (dtDev != null && dtDev.Rows.Count > 0)
                {
                    for (int j = 0; j < dtDev.Rows.Count; j++)
                    {
                        devs += dtDev.Rows[j]["T_Devicedesc"].ToString() + ',';
                    }
                    devs = devs.TrimEnd(',');
                }
            }

            //获取设备相关信息
            if (i == 4)
            {
                //json=BLL.JsonHelper.CreateJsonParameters(bmd.RetTabItemsBySbID(treeId, out errMsg));
                //BindGrid(bmd.RetTabItemsBySbID(treeId, out errMsg));
                //list = bmd.RetTabItemsBySbID(treeId);
                id_key = bmd.RetStrItemsBySbID(treeId, out errMsg);

                string[] s = id_key.Split(';');

                if (s != null)
                {
                    id_key = s[0].ToString().TrimEnd(',');
                    itemDesc = s[1].ToString().TrimEnd(',');
                }

                devName = bmd.RetSBNameBySbNodeKey(treeId);

            }

            //点检项
            if (i == 5)
            {
                drItem = bmd.RetItemInfoByNodeKey(treeId);

                if (drItem != null)
                {
                    itemDesc = drItem["T_ITEMDESC"].ToString();
                    itemPosition = drItem["T_ITEMPOSITION"].ToString();
                    itemContent = drItem["T_Content"].ToString();

                    if (drItem["T_type"].ToString() == "0")
                    { itemType = "点检"; }
                    else
                    { itemType = "巡检"; }

                    itemObserve = drItem["T_OBSERVE"].ToString();
                    itemI_Status = bmr.RetStatusBySID(drItem["I_Status"].ToString());
                    itemUint = drItem["T_UNIT"].ToString();
                    itemLower = drItem["F_Lower"].ToString();
                    itemUpper = drItem["F_UPPER"].ToString();

                    if (drItem["I_spectrum"].ToString() == "1")
                        itemSpectrum = "是";
                    else
                        itemSpectrum = "否";

                    itemStartTime = drItem["T_STARTTIME"].ToString();

                    if (drItem["T_PERIODTYPE"].ToString() == "1")
                        itemPerodType = "日";
                    else if (drItem["T_PERIODTYPE"].ToString() == "2")
                        itemPerodType = "周";
                    else if (drItem["T_PERIODTYPE"].ToString() == "3")
                        itemPerodType = "月";
                    else if (drItem["T_PERIODTYPE"].ToString() == "4")
                        itemPerodType = "年";

                    itemPerodValue = drItem["T_PERIODValue"].ToString();

                    if (drItem["T_STATUS"].ToString() == "0")
                        itemT_Status = "停机";
                    else
                        itemT_Status = "启机";
                }
            }

            obj = new
            {
                count = i,
                lID = lID,
                lName = lName,
                lOrgID = lOrgID,
                lOrgName = lOrgName,
                lType = lType,
                list = list,          //线路和区域关系
                listArea = listArea,   //区域
                listDev = listDev,
                areaID = areaID,
                areaName = areaName,
                areaParaentID = areaParaentID,
                devs = devs,
                json = json,
                id_key = id_key,
                itemDesc = itemDesc,
                devName = devName,
                itemPosition = itemPosition,
                itemContent = itemContent,
                itemType = itemType,
                itemObserve = itemObserve,
                itemI_Status = itemI_Status,
                itemUint = itemUint,
                itemLower = itemLower,
                itemUpper = itemUpper,
                itemSpectrum = itemSpectrum,
                itemStartTime = itemStartTime,
                itemPerodType = itemPerodType,
                itemPerodValue = itemPerodValue,
                itemT_Status = itemT_Status
            };

            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }

        /// <summary>
        /// 新建线路
        /// </summary>
        /// <param name="lID">线路ID</param>
        /// <param name="lName">线路名称</param>
        /// <param name="lType">线路类型</param>
        /// <param name="lGw">线路岗位</param>
        /// <param name="lPID">线路父ID</param>
        private void SetLineInfo(string lName, string lType, string lGw, string lPID, out string errMsg)
        {
            string info = "";

            bool flag = bmr.AddLineInfo(lName, lType, lGw, lPID, out errMsg);

            if (errMsg == "")
            {
                if (flag == true)
                    info = "新建线路成功！";
                else
                    info = "新建线路失败！";
            }
            else { info = errMsg; }

            obj = new
            {
                msg = info
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }

        /// <summary>
        /// 编辑线路信息
        /// </summary>
        /// <param name="lID"></param>
        /// <param name="lName"></param>
        /// <param name="lType"></param>
        /// <param name="lGw"></param>
        /// <param name="lPID"></param>
        /// <param name="errMsg"></param>
        private void EditLineInfo(string lID, string lName, string lType, string lGw, string lPID, out string errMsg)
        {
            string info = "";

            bool flag = bmr.EditLineInfo(lID, lName, lType, lGw, lPID, out errMsg);

            if (errMsg == "")
            {
                if (flag == true)
                    info = "编辑线路成功！";
                else
                    info = "编辑线路失败！";
            }
            else { info = errMsg; }

            obj = new
            {
                msg = info
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }

        /// <summary>
        /// 线路关联区域
        /// </summary>
        /// <param name="nodeKey">线路nodeKey</param>
        /// <param name="areaID">选择后的AREAID</param>
        /// <param name="chkID">已选择的AREAID </param>
        private void AddAreaInfo(string nodeKey, string areaID, string chkID)
        {
            string res = "";
            string oRes = "";
            string iRes = "";
            string info = "";

            bool flag = false;
            string[] aID = null;    //选择后的AREAID数组
            string[] cID = null;    //已选择的AREAID数组
            string[] iArea = null;  //添加数组
            string[] oArea = null;  //删除数组

            if (areaID == "" && chkID == "")
            { info = "请选择在点保存!"; }
            else
            {
                if (areaID.Contains(','))
                {
                    res = areaID.TrimEnd(',');

                    if (res.Contains(','))
                        aID = res.Split(',');
                    else
                    {
                        aID = new string[1];
                        aID[0] = res;
                    }
                }

                if (chkID.Contains(','))
                {
                    res = chkID.TrimEnd(',');

                    if (res.Contains(','))
                        cID = res.Split(',');
                    else
                    {
                        cID = new string[1];
                        cID[0] = res;
                    }
                }

                if (aID == null)
                { oArea = cID; }      //选择后AREAID为null,要删除所有已选择区域
                else if (cID == null)
                { iArea = aID; }      //选择前AREAID为null,要添加所有已选择区域
                else
                {
                    for (int i = 0; i < cID.Length; i++)
                    {
                        int con = 0;
                        for (int j = 0; j < aID.Length; j++)
                        {
                            if (cID[i] == aID[j])
                            {
                                con = 1;
                                break;
                            }
                        }
                        if (con == 0)
                            oRes += cID[i] + ',';    //需要删除的记录
                    }

                    for (int j = 0; j < aID.Length; j++)
                    {
                        int con = 0;
                        for (int i = 0; i < cID.Length; i++)
                        {
                            if (aID[j] == cID[i])
                            {
                                con = 1;
                                break;
                            }
                        }
                        if (con == 0)
                            iRes += aID[j] + ',';    //需要添加的记录
                    }

                    if (oRes != "")
                    {
                        oRes = oRes.TrimEnd(',');

                        if (oRes.Contains(','))
                        { oArea = oRes.Split(','); }
                        else
                        { oArea = new string[1]; oArea[0] = oRes; }
                    }

                    if (iRes != "")
                    {
                        iRes = iRes.TrimEnd(',');

                        if (iRes.Contains(','))
                        { iArea = iRes.Split(','); }
                        else
                        { iArea = new string[1]; iArea[0] = iRes; }
                    }
                }

                flag = bmr.AddLineAndAreaRealtion(nodeKey, oArea, iArea,"QY", out errMsg);

                if (errMsg == "")
                {
                    if (flag == true)
                    { info = "添加成功!"; }
                    else
                    { info = "添加失败!"; }
                }
                else
                { info = errMsg; }

                obj = new
                {
                    msg = info
                };
                string result = JsonConvert.SerializeObject(obj);
                Response.Write(result);
                Response.End();
            }
        }

        /// <summary>
        /// 删除区域
        /// </summary>
        private void DelArea(string areaID, string nodeKey)
        {
            bool flag = false;

            string info = "";

            flag = bmr.DelArea(areaID, nodeKey, out errMsg);

            if (errMsg == "")
            {
                if (flag == true)
                { info = "删除成功!"; }
                else
                { info = "删除失败!"; }
            }
            else
            { info = errMsg; }

            obj = new
            {
                msg = info
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }

        /// <summary>
        /// 添加设备和区域关系
        /// </summary>
        /// <param name="sbID"></param>
        /// <param name="nodeKey"></param>
        private void AddDeviceRelation(string sbID, string nodeKey)
        {
            string res = "";
            string oRes = "";
            string iRes = "";
            string info = "";

            bool flag = false;
            string[] aID = null;    //选择后的AREAID数组
            string[] cID = null;    //已选择的AREAID数组
            string[] iArea = null;  //添加数组
            string[] oArea = null;  //删除数组

            //根据NodeKey找到关系表中所有设备的节点
            DataTable dtInfo = null;

            dtInfo = bmr.GetTableDevByNodeKey(nodeKey);

            if (dtInfo != null && dtInfo.Rows.Count > 0)
            {
                cID = new string[dtInfo.Rows.Count];

                for (int k = 0; k < dtInfo.Rows.Count; k++)
                {
                    cID[k] = dtInfo.Rows[k]["T_NODEID"].ToString();
                }
            }

            if (sbID != "")
            {
                if (sbID.Contains(','))
                {
                    res = sbID.TrimEnd(',');

                    if (res.Contains(','))
                        aID = res.Split(',');
                    else
                    {
                        aID = new string[1];
                        aID[0] = res;
                    }
                }
            }

            if (cID != null)
            {
                for (int i = 0; i < cID.Length; i++)
                {
                    if (aID != null)
                    {
                        int con = 0;
                        for (int j = 0; j < aID.Length; j++)
                        {
                            if (cID[i] == aID[j])
                            {
                                con = 1;
                                break;
                            }
                        }
                        if (con == 0)
                            oRes += cID[i] + ',';    //需要删除的记录
                    }
                    else
                    { oRes += cID[i] + ','; }
                }
            }

            if (aID != null)
            {
                for (int j = 0; j < aID.Length; j++)
                {
                    if (cID != null)
                    {
                        int con = 0;
                        for (int i = 0; i < cID.Length; i++)
                        {
                            if (aID[j] == cID[i])
                            {
                                con = 1;
                                break;
                            }
                        }
                        if (con == 0)
                            iRes += aID[j] + ',';    //需要添加的记录
                    }
                    else
                    { iRes += aID[j] + ','; }
                }
            }

            if (oRes != "")
            {
                oRes = oRes.TrimEnd(',');

                if (oRes.Contains(','))
                { oArea = oRes.Split(','); }
                else
                { oArea = new string[1]; oArea[0] = oRes; }
            }

            if (iRes != "")
            {
                iRes = iRes.TrimEnd(',');

                if (iRes.Contains(','))
                { iArea = iRes.Split(','); }
                else
                { iArea = new string[1]; iArea[0] = iRes; }
            }

            flag = bmr.AddLineAndAreaRealtion(nodeKey, oArea, iArea, "SB",out errMsg);

            if (oRes != "" || iRes != "")
            {
                if (errMsg == "")
                {
                    if (flag == true)
                    { info = "添加成功!"; }
                    else
                    { info = "添加失败!"; }
                }
                else
                { info = errMsg; }
            }
            else { info = "请先选择设备,然后在操作!"; }

            obj = new
            {
                msg = info
            };
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }

        /// <summary>
        /// 新增点检项
        /// </summary>
        /// <param name="?"></param>
        private void AddItem(string nodeKey, string itemBw, string itemDesc, string itemContent, string itemObserve, string itemUnit, string itemType, string itemStatus, string itemStatusQJ, string itemUpper, string itemLower, string itemSpectrum, string itemStartTime, string itemPerValue, string itemPerType)
        {
            bool flag = false;

            string info = "";

            //string nodeKey = Request.Form["SBnodeKey"]; //设备标识
            //string itemID = Request.Form["itemID"];     //点检向标识 
            //string itemBw = HttpUtility.UrlDecode(Request.Form["itemBw"]);      //点检项目部位 
            //string itemDesc = HttpUtility.UrlDecode(Request.Form["itemDesc"]);  //点检项目描述 
            //string itemContent = HttpUtility.UrlDecode(Request.Form["itemContent"]); //点检项内容
            //string itemObserve = HttpUtility.UrlDecode(Request.Form["itemObserve"]); //观察名称
            //string itemUnit = HttpUtility.UrlDecode(Request.Form["itemUnit"]);       //点检项单位
            //string itemType = Request.Form["itemType"];         //点检项类型
            //string itemStatus = Request.Form["itemStatus"];     //设备状态
            //string itemStatusQJ = Request.Form["itemStatusQJ"];     //启动 停止
            //string itemUpper = Request.Form["itemUpper"];     //上限
            //string itemLower = Request.Form["itemLower"];     //下限
            //string itemSpectrum = Request.Form["itemSpectrum"];     //是否频谱
            //string itemStartTime = HttpUtility.UrlDecode(Request.Form["itemStartTime"]);   //开始时间
            //string itemPerValue = Request.Form["itemPerValue"];     //周期数值
            //string itemPerType = Request.Form["itemPerType"];       //周期类型

            #region  判断数字
            if (itemUpper != "")
            {
                try { int.Parse(itemUpper); }
                catch { info = "数据上限请输入数字."; }
            }

            if (itemLower != "")
            {
                try { int.Parse(itemLower); }
                catch { info = "数据下限请输入数字."; }
            }

            if (itemPerValue != "")
            {
                try { int.Parse(itemPerValue); }
                catch { info = "周期数值请输入数字."; }
            }
            #endregion

            if (info == "")
            {
                flag = bmr.AddItem(nodeKey, itemBw, itemDesc, itemContent, itemObserve, itemUnit, itemType, itemStatus, itemStatusQJ, itemUpper, itemLower, itemSpectrum, itemStartTime, itemPerValue, itemPerType, out errMsg);

                if (errMsg == "")
                {
                    if (flag == true)
                        info = "新增点检项成功!";
                    else
                        info = "新增点检项失败!";
                }
                else { info = errMsg; }
            }

            obj = new
            {
                msg = info
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }

        //#region LoadData

        //private void BindGrid(DataTable dt)
        //{

        //    //Grid1.DataSource = dt;
        //    //Grid1.DataBind();
        //}

        //#endregion

        //#region Events

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    SyncSelectedRowIndexArrayToHiddenField();

        //    labResult.Text = "选中行的ID列表为：" + hfSelectedIDS.Text.Trim();
        //}

        //protected void Grid1_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        //{
        //    SyncSelectedRowIndexArrayToHiddenField();

        //    Grid1.PageIndex = e.NewPageIndex;

        //    UpdateSelectedRowIndexArray();

        //}

        //private List<string> GetSelectedRowIndexArrayFromHiddenField()
        //{
        //    JArray idsArray = new JArray();

        //    string currentIDS = hfSelectedIDS.Text.Trim();
        //    if (!String.IsNullOrEmpty(currentIDS))
        //    {
        //        idsArray = JArray.Parse(currentIDS);
        //    }
        //    else
        //    {
        //        idsArray = new JArray();
        //    }

        //    return new List<string>(idsArray.ToObject<string[]>());
        //}

        //private void SyncSelectedRowIndexArrayToHiddenField()
        //{
        //    List<string> ids = GetSelectedRowIndexArrayFromHiddenField();

        //    List<int> selectedRows = new List<int>();
        //    if (Grid1.SelectedRowIndexArray != null && Grid1.SelectedRowIndexArray.Length > 0)
        //    {
        //        selectedRows = new List<int>(Grid1.SelectedRowIndexArray);
        //    }

        //    int startPageIndex = Grid1.PageIndex * Grid1.PageSize;
        //    for (int i = startPageIndex, count = Math.Min(startPageIndex + Grid1.PageSize, Grid1.RecordCount); i < count; i++)
        //    {
        //        string id = Grid1.DataKeys[i][0].ToString();
        //        if (selectedRows.Contains(i - startPageIndex))
        //        {
        //            if (!ids.Contains(id))
        //            {
        //                ids.Add(id);
        //            }
        //        }
        //        else
        //        {
        //            if (ids.Contains(id))
        //            {
        //                ids.Remove(id);
        //            }
        //        }

        //    }

        //    hfSelectedIDS.Text = new JArray(ids).ToString(Formatting.None);
        //}


        //private void UpdateSelectedRowIndexArray()
        //{
        //    List<string> ids = GetSelectedRowIndexArrayFromHiddenField();

        //    List<int> nextSelectedRowIndexArray = new List<int>();
        //    int nextStartPageIndex = Grid1.PageIndex * Grid1.PageSize;
        //    for (int i = nextStartPageIndex, count = Math.Min(nextStartPageIndex + Grid1.PageSize, Grid1.RecordCount); i < count; i++)
        //    {
        //        string id = Grid1.DataKeys[i][0].ToString();
        //        if (ids.Contains(id))
        //        {
        //            nextSelectedRowIndexArray.Add(i - nextStartPageIndex);
        //        }
        //    }
        //    Grid1.SelectedRowIndexArray = nextSelectedRowIndexArray.ToArray();
        //}

        //#endregion

        /// <summary>
        /// 查询点检项信息
        /// </summary>
        private void QueryDataByItem(string sbID)
        {
            DataTable dt = null;

            int page = Request.Form["page"] != "" ? Convert.ToInt32(Request.Form["page"]) : 0;
            int size = Request.Form["rows"] != "" ? Convert.ToInt32(Request.Form["rows"]) : 0;
            string sort = Request.Form["sort"] != "" ? Request.Form["sort"] : "";
            string order = Request.Form["order"] != "" ? Request.Form["order"] : "";
            if (page < 1) return;
            string orderField = sort.Replace("item_", "");
            string strWhere = "";// GetWhere();

            dt = bmd.RetTabItemsBySbID(sbID, out errMsg); //bmd.RetTabDevByDevID(sbID, out errMsg);// DBdb2.RunDataSet("select * from T_INFO_DEVICE", out errMsg);// DataHandler.GetList("TUser", "*", "ID", size, page, false, false, strWhere);
            int count = dt.Rows.Count; //bll.GetList(strWhere).Tables[0].Rows.Count;//获取总数
            string strJSON = JsonHelper.CreateJsonParameters(dt, true, count);

            Response.Write(strJSON);
            Response.End();
        }

        /// <summary>
        /// 保存点检项和设备关系 
        /// </summary>
        /// <param name="sbNodeKey"></param>
        /// <param name="item_ID_KEY"></param>
        private void SaveRelation(string sbNodeKey, string id_key_new, string id_key_old)
        {
            string res = "";
            string oRes = "";
            string iRes = "";
            string info = "";

            bool flag = false;
            string[] aID = null;    //选择后的AREAID数组
            string[] cID = null;    //已选择的AREAID数组
            string[] iArea = null;  //添加数组
            string[] oArea = null;  //删除数组

            if (id_key_new != "")
            {
                if (id_key_new.Contains(','))
                {
                    res = id_key_new.TrimEnd(',');

                    if (res.Contains(','))
                        aID = res.Split(',');
                    else
                    {
                        aID = new string[1];
                        aID[0] = res;
                    }
                }
            }

            if (id_key_old != "")
            {
                if (id_key_old.Contains(','))
                {
                    res = id_key_old.TrimEnd(',');

                    if (res.Contains(','))
                        cID = res.Split(',');
                    else
                    {
                        cID = new string[1];
                        cID[0] = res;
                    }
                }
            }

            if (cID != null)
            {
                for (int i = 0; i < cID.Length; i++)
                {
                    if (aID != null)
                    {
                        int con = 0;
                        for (int j = 0; j < aID.Length; j++)
                        {
                            if (cID[i] == aID[j])
                            {
                                con = 1;
                                break;
                            }
                        }
                        if (con == 0)
                            oRes += cID[i] + ',';    //需要删除的记录
                    }
                    else
                    { oRes += cID[i] + ','; }
                }
            }

            if (aID != null)
            {
                for (int j = 0; j < aID.Length; j++)
                {
                    if (cID != null)
                    {
                        int con = 0;
                        for (int i = 0; i < cID.Length; i++)
                        {
                            if (aID[j] == cID[i])
                            {
                                con = 1;
                                break;
                            }
                        }
                        if (con == 0)
                            iRes += aID[j] + ',';    //需要添加的记录
                    }
                    else
                    { iRes += aID[j] + ','; }
                }
            }

            if (oRes != "")
            {
                oRes = oRes.TrimEnd(',');

                if (oRes.Contains(','))
                { oArea = oRes.Split(','); }
                else
                { oArea = new string[1]; oArea[0] = oRes; }
            }

            if (iRes != "")
            {
                iRes = iRes.TrimEnd(',');

                if (iRes.Contains(','))
                { iArea = iRes.Split(','); }
                else
                { iArea = new string[1]; iArea[0] = iRes; }
            }

            flag = bmr.AddDeviceAndItemRealtion(sbNodeKey, oArea, iArea, out errMsg);

            if (oRes != "" || iRes != "")
            {
                if (errMsg == "")
                {
                    if (flag == true)
                    { info = "添加成功!"; }
                    else
                    { info = "添加失败!"; }
                }
                else
                { info = errMsg; }
            }
            else { info = "请先选择设备,然后在操作!"; }

            obj = new
            {
                msg = info
            };
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();

        }
    }
}
