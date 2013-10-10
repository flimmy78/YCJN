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

namespace DJXT.ParentMember
{
    public partial class PlanDown_ZC : System.Web.UI.Page
    {
        ParmentBLL parment = new ParmentBLL();
        MemberBLL member = new MemberBLL();
        DataTable dt = new DataTable();
        StringBuilder st = new StringBuilder();
        object obj = null;
        string resultMenu = "";

        string id = "";
        string name = "";
        string oldId = "";
        int count = 0;

        bool res = false;
        string resultInfo = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            string param = Request["param"];
            if (param != "")
            {
                if (param == "route")
                {
                    id = Request.Form["id"];
                    int page = Convert.ToInt32(Request["page"].ToString());
                    int rows = Convert.ToInt32(Request["rows"].ToString());
                    GetLoadRoute(id, page, rows);
                }
                else if (param == "area")
                {
                    id = Request.Form["id"];
                    int page = Convert.ToInt32(Request["page"].ToString());
                    int rows = Convert.ToInt32(Request["rows"].ToString());
                    GetLoadArea(id, page, rows);
                }
                else if (param == "device")
                {
                    id = Request.Form["id"];
                    int page = Convert.ToInt32(Request["page"].ToString());
                    int rows = Convert.ToInt32(Request["rows"].ToString());
                    GetLoadDevice(id, page, rows);
                }
                else if (param == "item")
                {
                    id = Request.Form["id"];
                    int page = Convert.ToInt32(Request["page"].ToString());
                    int rows = Convert.ToInt32(Request["rows"].ToString());
                    GetLoadItem(id, page, rows);
                }
                else if (param == "judge")
                {
                    id = Request["id"].ToString();
                    GetJudge(id);
                }
                else if (param == "judgeRouteOrArea")
                {
                    id = Request["id"].ToString();
                    JudgeRouteOrArea(id);
                }
                else if (param == "judgeAreaOrDevice")
                {
                    id = Request["id"].ToString();
                    JudgeAreaOrDevice(id);
                }
                else if (param == "judgeDeviceOrItem")
                {
                    id = Request["id"].ToString();
                    JudgeDeviceOrItem(id);
                }
            }
            else
                GetListTree();
        }

        #region 判断选中的节点的数据类型
        public void GetJudge(string id)
        {
            BLLManRoute route = new BLLManRoute();
            count = route.RetCount(id);
            object obj = new
            {
                num = count
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 初始化线路信息
        /// <summary>
        /// 查询子岗信息
        /// </summary>
        /// <param name="id">父类ID</param>
        /// <param name="page">第几页数</param>
        /// <param name="rows">每页数据量</param>
        private void GetLoadRoute(string id, int page, int rows)
        {
            BLLManRoute route = new BLLManRoute();
            dt = route.GetRouteDt(id, (page - 1) * rows + 1, page * rows);
            count = route.GetRountCount(id);
            IList<Hashtable> list = new List<Hashtable>();
            foreach (DataRow row in dt.Rows)
            {
                Hashtable ht = new Hashtable();
                ht.Add("ID", row["ID"].ToString());
                ht.Add("RouteID", row["RouteID"].ToString());
                ht.Add("RouteName", row["RouteName"].ToString());
                ht.Add("Rtype", row["Rtype"].ToString());
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

        #region 初始化区域信息
        /// <summary>
        /// 初始化区域信息
        /// </summary>
        /// <param name="id">父类ID</param>
        /// <param name="page">第几页数</param>
        /// <param name="rows">每页数据量</param>
        private void GetLoadArea(string id, int page, int rows)
        {
            BLLManArea area = new BLLManArea();
            count = area.GetAreaCount(id);
            dt = area.GetAreaDt(id, (page - 1) * rows + 1, page * rows);

            IList<Hashtable> list = new List<Hashtable>();
            object obj = null;
            if (count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("ID", row["ID"].ToString());
                    ht.Add("AreaID", row["AreaID"].ToString());
                    ht.Add("AreaName", row["AreaName"].ToString());
                    ht.Add("AreaCD", row["AreaCD"].ToString());
                    list.Add(ht);
                }
                obj = new
                {
                    total = count,
                    rows = list
                };
            }
            else
            {
                obj = new
                {
                    total = 0,
                    rows = list
                };
            }

            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 初始化设备信息
        /// <summary>
        /// 初始化设备信息
        /// </summary>
        /// <param name="id">父类ID</param>
        /// <param name="page">第几页数</param>
        /// <param name="rows">每页数据量</param>
        private void GetLoadDevice(string id, int page, int rows)
        {
            BLLManDevice device = new BLLManDevice();
            count = device.GetDeviceCount(id);
            dt = device.GetDeviceDt(id, (page - 1) * rows + 1, page * rows);

            IList<Hashtable> list = new List<Hashtable>();
            object obj = null;
            if (count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("ID", row["ID"].ToString());
                    ht.Add("DeviceID", row["DeviceID"].ToString());
                    ht.Add("DeviceName", row["DeviceName"].ToString());
                    ht.Add("T_PARENTID", row["T_PARENTID"].ToString());
                    ht.Add("T_SWERK", row["T_PARENTID"].ToString());
                    ht.Add("T_IWERK", row["T_PARENTID"].ToString());
                    ht.Add("T_KOSTL", row["T_PARENTID"].ToString());

                    list.Add(ht);
                }
                obj = new
                {
                    total = count,
                    rows = list
                };
            }
            else
            {
                obj = new
                {
                    total = 0,
                    rows = list
                };
            }

            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 初始化点检项信息
        /// <summary>
        /// 初始化点检项信息
        /// </summary>
        /// <param name="id">父类ID</param>
        /// <param name="page">第几页数</param>
        /// <param name="rows">每页数据量</param>
        private void GetLoadItem(string id, int page, int rows)
        {
            BLLManDevice device = new BLLManDevice();
            count = device.GetItemCount(id);
            dt = device.GetItemDt(id, (page - 1) * rows + 1, page * rows);

            IList<Hashtable> list = new List<Hashtable>();
            object obj = null;
            if (count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("ID", row["ID"].ToString());
                    ht.Add("T_ITEMID", row["T_ITEMID"].ToString());
                    ht.Add("T_ITEMPOSITION", row["T_ITEMPOSITION"].ToString());
                    ht.Add("T_ITEMDESC", row["T_ITEMDESC"].ToString());
                    ht.Add("T_CONTENT", row["T_CONTENT"].ToString());
                    ht.Add("T_TYPE", row["T_TYPE"].ToString());
                    ht.Add("T_OBSERVE", row["T_OBSERVE"].ToString());
                    ht.Add("T_UNIT", row["T_UNIT"].ToString());
                    ht.Add("F_LOWER", row["F_LOWER"].ToString());
                    ht.Add("F_UPPER", row["F_UPPER"].ToString());
                    ht.Add("T_STARTTIME", row["T_STARTTIME"].ToString());
                    ht.Add("T_PERIODVALUE", row["T_PERIODVALUE"].ToString());
                    ht.Add("T_STATUS", row["T_STATUS"].ToString());
                    ht.Add("T_PERIODTYPE", row["T_STATUS"].ToString());
                    list.Add(ht);
                }
                obj = new
                {
                    total = count,
                    rows = list
                };
            }
            else
            {
                obj = new
                {
                    total = 0,
                    rows = list
                };
            }

            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 判断显示路线还是区域
        public void JudgeRouteOrArea(string id)
        {
            BLLManRoute route = new BLLManRoute();
            int routeCount = route.GetRountCount(id);
            BLLManArea area = new BLLManArea();
            int areaCount = area.GetAreaCount(id);
            string info = "";
            if (routeCount > 0)
            {
                info = "route";
            }
            else if (areaCount > 0)
            {
                info = "area";
            }
            else if (routeCount == 0 && areaCount == 0)
            {
                info = "route";
            }
            object obj = new
            {
                info = info
            };

            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 判断显示区域还是设备
        public void JudgeAreaOrDevice(string id)
        {
            BLL.BLLManDevice device = new BLL.BLLManDevice();
            int deviceCount = device.GetDeviceCount(id);
            BLLManArea area = new BLLManArea();
            int areaCount = area.GetAreaCount(id);
            string info = "";
            if (areaCount > 0)
            {
                info = "area";
            }
            else if (deviceCount > 0)
            {
                info = "device";
            }
            else if (deviceCount == 0 && areaCount == 0)
            {
                info = "area";
            }
            object obj = new
            {
                info = info
            };

            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 判断显示设备还是点检项
        public void JudgeDeviceOrItem(string id)
        {
            BLL.BLLManDevice device = new BLL.BLLManDevice();
            int deviceCount = device.GetDeviceCount(id);

            int areaCount = device.GetItemCount(id);
            string info = "";
            if (areaCount > 0)
            {
                info = "device";
            }
            else if (deviceCount > 0)
            {
                info = "item";
            }
            else if (deviceCount == 0 && areaCount == 0)
            {
                info = "device";
            }
            object obj = new
            {
                info = info
            };

            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 加载线路Tree
        //private void ShowRouteTree()
        //{
        //    BLLManRoute route = new BLLManRoute();
        //    StringBuilder st = new StringBuilder();
        //    DataTable dt = route.GetRouteTree();

        //    st.Append("[");
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        if (dt.Rows[i]["PARMENTID"].ToString() == "0")
        //        {
        //            st.Append("{id:'" + dt.Rows[i]["ID"] + "',ide:'" + dt.Rows[i]["IDE"] + "', pId:'" + dt.Rows[i]["PARMENTID"] + "',name:'" + dt.Rows[i]["NAME"] + "',t:'" + dt.Rows[i]["NAME"] + "', open:true},");
        //            id = dt.Rows[i]["ID"].ToString();
        //            name = dt.Rows[i]["NAME"].ToString();
        //        }
        //        else
        //            st.Append("{id:'" + dt.Rows[i]["ID"] + "',ide:'" + dt.Rows[i]["IDE"] + "',pId:'" + dt.Rows[i]["PARMENTID"] + "',name:'" + dt.Rows[i]["NAME"] + "',t:'" + dt.Rows[i]["NAME"] + "',doCheck:false},");
        //    }

        //    string resultMenu = st.ToString().Substring(0, st.ToString().Length - 1) + "]";
        //    object obj = new
        //    {
        //        count = 1,
        //        id = id,
        //        name = name,
        //        menu = resultMenu
        //    };
        //    string result = JsonConvert.SerializeObject(obj);
        //    Response.Write(result);
        //    Response.End();
        //}

        /// <summary>
        /// 初始化Tree结构
        /// </summary>
        private void GetListTree()
        {
            BLLManRoute bmr = new BLLManRoute();
            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            string errMsg = "";

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
                    id = "0",
                    name = zxlName,
                    menu = resultMenu
                };
                string result = JsonConvert.SerializeObject(obj);
                Response.Write(result);
                Response.End();
            }
        }
        #endregion
    }
}