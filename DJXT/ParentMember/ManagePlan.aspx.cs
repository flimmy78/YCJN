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
using Newtonsoft.Json;
using BLL;
using System.Collections.Generic;

namespace DJXT.ParentMember
{
    public partial class managePlan : System.Web.UI.Page
    {
        IList<Hashtable> list = null;
        object obj = null;
        PlanBLL plan = new PlanBLL();
        BLLManRoute route = new BLLManRoute();
        BLLManArea area = new BLLManArea();
        BLL.BLLRole bl = new BLL.BLLRole();
        int count = 0;
        string errMsg = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string param = Request["param"];
            if (param != "")
            {
                if (param == "Route")
                {
                    string routeID = Request["routeID"].ToString();
                    GetAreaAndDevice(routeID);
                }
                else if (param == "Area")
                {
                    string areaID = Request["areaID"].ToString();
                    GetDevices(areaID);
                }
                else if (param == "queryList")
                {
                    GetString();
                }
                else if (param == "searchList")
                {
                    string routeID = Request["routeID"].ToString();
                    string areaID = Request["areaID"].ToString();
                    string deviceID = Request["deviceID"].ToString();
                    DateTime sTime = Convert.ToDateTime(Request["sTime"].ToString());
                    DateTime eTime = Convert.ToDateTime(Request["eTime"].ToString());
                    GetPlanList(sTime, eTime, routeID, areaID, deviceID);
                }
                else if (param == "Edit")
                {
                    //routeID:routeID,areaID:areaID,deviceID:deviceID,itemID:itemID,time:cTime,state:state,value:value
                    string routeId = Request["routeID"];
                    string areaId = Request["areaID"];
                    string deviceId = Request["deviceID"];
                    string itemId = Request["itemID"];
                    string cTime = HttpUtility.UrlDecode(Request["time"]);
                    string state = Request["state"];
                    string value = Request["value"];
                    EditResult(routeId, areaId, deviceId, itemId, cTime, Convert.ToInt32(state), value);
                }
            }
            else
            {
                GetRoutesAndAreasAndDevices();
            }
        }

        #region 修改点检结果
        private void EditResult(string routeId, string areaId, string deviceId, string itemId, string cTime, int state, string value)
        {
            if (plan.EditPlanResult(routeId, areaId, deviceId, itemId, state, value, cTime))
                errMsg = "编辑成功!";
            else
                errMsg = "编辑失败!";
            object obj = new
            {
                info = errMsg
            };

            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 获取  路线  区域  设备
        public void GetRoutesAndAreasAndDevices()
        {
            string idkey = Request.Cookies["ID_KEY"].Value.ToString();
            string userID = bl.GetUserNameById(idkey, out errMsg);
            list = route.GetRouteTree(userID);
            IList<Hashtable> AreaList = null;
            IList<Hashtable> DevicesList = null;
            if (list != null)
            {
                Hashtable hs = new Hashtable();
                hs = list[0];
                string routeID = hs["T_ROUTEID"].ToString();
                AreaList = area.GetAreas(routeID);
                if (AreaList != null)
                {
                    hs = AreaList[0];
                    string areaID = hs["T_AREAID"].ToString();
                    DevicesList = area.GetDevices(areaID);
                }
            }
            obj = new
            {
                RouteList = list,
                AreaList = AreaList,
                DeviceList = DevicesList
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 获取 区域  设备
        public void GetAreaAndDevice(string routeID)
        {
            if (routeID == "0")
            {
                string idkey = Request.Cookies["ID_KEY"].Value.ToString();
                string userID = bl.GetUserNameById(idkey, out errMsg);
                list = route.GetRouteTree(userID);

                Hashtable hs = new Hashtable();
                hs = list[0];
                routeID = hs["T_ROUTEID"].ToString();
            }

            IList<Hashtable> AreaList = area.GetAreas(routeID);
            IList<Hashtable> DevicesList = null;
            if (AreaList != null)
            {
                Hashtable hs = AreaList[0];
                string areaID = hs["T_AREAID"].ToString();
                DevicesList = area.GetDevices(areaID);
            }
            obj = new
            {
                AreaList = AreaList,
                DeviceList = DevicesList
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 获取  设备
        public void GetDevices(string areaID)
        {
            IList<Hashtable> DevicesList = area.GetDevices(areaID);

            obj = new
            {
                DeviceList = DevicesList
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 初始化点检任务信息
        public void GetString()
        {
            string idkey = Request.Cookies["ID_KEY"].Value.ToString();
            string userID = bl.GetUserNameById(idkey, out errMsg);
            PlanBLL plan = new PlanBLL();
            int page = Convert.ToInt32(Request["page"].ToString());
            int rows = Convert.ToInt32(Request["rows"].ToString());
            DataTable dt = plan.GetPlan(userID, Convert.ToDateTime(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd 00:00:00")), Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:59")), (page - 1) * rows + 1, page * rows);

            count = plan.GetPlanCount(userID, "0", "0", "0", Convert.ToDateTime(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd 00:00:00")), Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:59")));
            IList<Hashtable> list = new List<Hashtable>();

            foreach (DataRow item in dt.Rows)
            {
                Hashtable ht = new Hashtable();
                string startTime = item["sTime"].ToString();
                string endTime = item["eTIme"].ToString();
                string cTime = item["cTime"].ToString();
                string uTime = item["uTime"].ToString();
                ht.Add("ID", item["ID"]);
                ht.Add("routeName", item["routeName"].ToString());
                ht.Add("areaName", item["areaName"].ToString());
                ht.Add("deviceName", item["deviceName"].ToString());
                ht.Add("T_ITEMPOSITION", item["T_ITEMPOSITION"].ToString());
                ht.Add("T_ITEMDESC", item["T_ITEMDESC"].ToString());
                ht.Add("type", item["type"].ToString());
                ht.Add("sTime", startTime);
                ht.Add("eTime", endTime);
                ht.Add("cTime", cTime);
                ht.Add("uTime", uTime);
                ht.Add("value", item["value"].ToString());

                ht.Add("RouteID", item["ROUTEID"].ToString());
                ht.Add("AreaID", item["AREAID"].ToString());
                ht.Add("DeviceID", item["DEVICEID"].ToString());
                ht.Add("ItemID", item["ITEMID"].ToString());

                if (item["status"].ToString() == "新任务")
                {
                    if (DateTime.Now > Convert.ToDateTime(endTime))
                        ht.Add("status", "缺陷");
                }
                else
                    ht.Add("status", item["status"].ToString());

                ht.Add("state", item["state"].ToString());

                list.Add(ht);
            }
            object obj = new
            {
                total = count,
                rows = list
            };

            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 查询任务信息
        public void GetPlanList(DateTime sTime, DateTime eTime, string routeID, string areaID, string deviceID)
        {
            string idkey = Request.Cookies["ID_KEY"].Value.ToString();
            string userID = bl.GetUserNameById(idkey, out errMsg);
            PlanBLL plan = new PlanBLL();
            int page = Convert.ToInt32(Request["page"].ToString());
            int rows = Convert.ToInt32(Request["rows"].ToString());
            DataTable dt = plan.GetPlan(userID, routeID, areaID, deviceID, sTime, eTime, (page - 1) * rows + 1, page * rows);
            count = plan.GetPlanCount(userID, routeID, areaID, deviceID, sTime, eTime);
            IList<Hashtable> list = new List<Hashtable>();

            foreach (DataRow item in dt.Rows)
            {
                Hashtable ht = new Hashtable();
                string startTime = item["sTime"].ToString();
                string endTime = item["eTIme"].ToString();
                string cTime = item["cTime"].ToString();
                string uTime = item["uTime"].ToString();
                ht.Add("ID", item["ID"]);
                ht.Add("routeName", item["routeName"].ToString());
                ht.Add("areaName", item["areaName"].ToString());
                ht.Add("deviceName", item["deviceName"].ToString());
                ht.Add("T_ITEMPOSITION", item["T_ITEMPOSITION"].ToString());
                ht.Add("T_ITEMDESC", item["T_ITEMDESC"].ToString());
                ht.Add("type", item["type"].ToString());
                ht.Add("sTime", startTime);
                ht.Add("eTime", endTime);
                ht.Add("cTime", cTime);
                ht.Add("uTime", uTime);
                ht.Add("value", item["value"].ToString());
                ht.Add("state", item["state"].ToString());
                ht.Add("status", item["status"].ToString());
                list.Add(ht);
            }
            object obj = new
            {
                total = count,
                rows = list
            };

            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion
    }
}