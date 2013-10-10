using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.Helper;
using System.Collections;
using System.Data;
using SAC.DB2;

namespace DAL
{
    /// <summary>
    /// 线路设置
    /// </summary>
    public class DALManRoute
    {
        string sql = "";
        string errMsg = "";

        DataTable dt = null;

        QuestcompleteDAL qc = new QuestcompleteDAL();
        PlanDAL pd = new PlanDAL();
        /// <summary>
        /// 获取总线路名称和ID 
        /// </summary>
        /// <returns></returns>
        public string GetTotalLineNameAndID()
        {
            string name = IniHelper.ReadIniData("DJXT", "ZxlName", null);
            string parentId = IniHelper.ReadIniData("DJXT", "ZxlParentID", null);
            string nodeId = IniHelper.ReadIniData("DJXT", "ZxlNodeID", null);

            return parentId + ',' + nodeId + ',' + "东风电厂总线路";
        }

        /// <summary>
        /// 获得线路和关系配置表之间记录
        /// written by shan
        /// </summary>
        /// <returns></returns>
        public DataTable GetTableRoute(out string errMsg)
        {
            sql = "SELECT * FROM T_INFO_ROUTE as i INNER JOIN T_BASE_ROUTE as b ON b.T_ROUTEID =i.T_NODEID and T_DESC='XL'";

            return DBdb2.RunDataTable(sql, out errMsg);
        }

        /// <summary>
        /// 获取区域和关系关联记录
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetTableArea(out string errMsg)
        {
            sql = "SELECT * FROM T_INFO_ROUTE as i INNER JOIN T_BASE_AREA as b ON b.T_AREAID =i.T_NODEID and T_DESC='QY'";

            return DBdb2.RunDataTable(sql, out errMsg);
        }

        /// <summary>
        /// 判断子节点是否存在
        /// </summary>
        /// <returns>1:总线路,2:线路,3:区域,4:设备,5:点检项</returns>
        public int RetCount(string nodeID)
        {
            int count = 0;
            int countBase = 0;
            int countRoute = 0;
            int countArea = 0;
            int countDev = 0;
            int countItem = 0;

            //首先判断是否是

            sql = "select count(*) from T_INFO_ROUTE where T_NODEKEY='" + nodeID + "'";

            countRoute = DBdb2.RunRowCount(sql, out errMsg);

            sql = "select count(*) from T_BASE_ROUTE as b inner join T_INFO_ROUTE as r on r.T_NODEID=b.T_ROUTEID  where T_NODEKEY='" + nodeID + "' and r.T_DESC='XL'";

            countBase = DBdb2.RunRowCount(sql, out errMsg);

            sql = "select count(*) from T_BASE_AREA as b inner join T_INFO_ROUTE as i on b.T_AREAID=i.T_NODEID where T_NODEKEY='" + nodeID + "' and i.t_desc='QY'";

            countArea = DBdb2.RunRowCount(sql, out errMsg);

            sql = "select count(*) from T_BASE_DEVICE as b inner join T_INFO_ROUTE as i on b.T_DEVICEID=i.T_NODEID where T_NODEKEY='" + nodeID + "' and i.t_desc='SB'";

            countDev = DBdb2.RunRowCount(sql, out errMsg);

            sql = "select count(*) from T_BASE_ITEM as b inner join T_INFO_ROUTE as i on b.T_ITEMID=i.T_NODEID where T_NODEKEY='" + nodeID + "' and i.T_DESC='DJX'";

            countItem = DBdb2.RunRowCount(sql, out errMsg);

            if (countRoute != 0)
            {
                if (countBase > 0)
                    count = 2;
                else if (countArea > 0)
                    count = 3;
                else if (countDev > 0)
                    count = 4;
                else if (countItem > 0)
                    count = 5;
            }
            else
            { count = 1; }

            return count;
        }

        /// <summary>
        /// 新建线路
        /// </summary>
        /// <param name="lName">线路名称</param>
        /// <param name="lType">线路类型</param>
        /// <param name="lGw">线路岗位</param>
        /// <param name="lPID">线路父ID</param>
        public bool AddLineInfo(string lName, string lType, string lGw, string lPID, out string errMsg)
        {
            int count = 1;
            object obj = null;
            bool flag = false;

            sql = "select * from T_BASE_ROUTE as b inner join T_INFO_ROUTE as i on b.T_ROUTEID=i.T_NODEID where i.T_NODEKEY='" + lPID + "'";

            DataRow drIS = DBdb2.RunDataRow(sql, out errMsg);

            if (drIS == null)
            {
                sql = "select * from T_BASE_ROUTE where T_ROUTENANME='" + lName + "'";

                DataRow dr = DBdb2.RunDataRow(sql, out errMsg);

                if (dr == null)
                {
                    int countBase = 1;

                    sql = "select T_ROUTEID from T_BASE_ROUTE order by ID_KEY DESC fetch first 1 rows only ";// "select * from T_BASE_ROUTE where T_ROUTENAME='" + lName + "'";

                    obj = DBdb2.RunSingle(sql, out errMsg);

                    if (obj != null && obj.ToString() != "")
                        countBase = int.Parse(obj.ToString()) + 1;

                    sql = "select T_NODEKEY from T_INFO_ROUTE order by ID_KEY DESC fetch first 1 rows only ";

                    obj = DBdb2.GetSingle(sql);

                    if (obj != null && obj.ToString() != "")
                        count = int.Parse(obj.ToString()) + 1;

                    sql = "insert into T_BASE_ROUTE (T_ROUTEID,T_ROUTENAME,T_ORGID,I_TYPE) values ('" + countBase + "','" + lName + "','" + lGw + "'," + lType + ")";
                    string sql1 = "insert into T_INFO_ROUTE (T_NODEID,T_NODEKEY,T_PARAENTID,T_DESC) values ('" + countBase + "','" + count.ToString() + "','" + lPID + "','XL')";

                    flag = DBdb2.RunNonQuery(sql, out errMsg);
                    DBdb2.RunNonQuery(sql1, out errMsg);

                }
                else { errMsg = "此线路已被添加!"; }
            }
            else { errMsg = "线路下不允许在新建线路!"; }
            return flag;
        }

        /// <summary>
        /// 编辑线路
        /// </summary>
        /// <param name="lID"></param>
        /// <param name="lName"></param>
        /// <param name="lType"></param>
        /// <param name="lGw"></param>
        /// <param name="lPID"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool EditLineInfo(string lID, string lName, string lType, string lGw, string lPID, out string errMsg)
        {
            bool flag = false;

            sql = "update t_base_route set T_ROUTENAME='" + lName + "',T_ORGID='" + lGw + "',i_type=" + lType + " where T_ROUTEID='" + lID + "'";

            flag = DBdb2.RunNonQuery(sql, out errMsg);

            return flag;

        }

        /// <summary>
        /// 根据节点取得线路信息
        /// 只有count=2的时候才调用此方法
        /// count=2 代表点击的时线路
        /// </summary>
        /// <returns></returns>
        public DataRow RetDataRowByNodeID(string nodeID)
        {
            DataRow dr = null;

            sql = "select b.T_ORGID,b.T_ROUTEID from T_INFO_ROUTE as i inner join T_BASE_ROUTE as b on i.T_NODEID=b.T_ROUTEID where T_NODEKEY='" + nodeID + "'";

            dr = DBdb2.RunDataRow(sql, out errMsg);

            if (dr != null)
            {
                sql = "SELECT * FROM T_BASE_ROUTE as r inner join T_SYS_ORGANIZE as o on r.T_ORGID=o.T_ORGID where  r.T_ORGID='" + dr["T_ORGID"] + "' and r.T_ROUTEID='" + dr["T_ROUTEID"] + "'";

                dr = DBdb2.RunDataRow(sql, out errMsg);
            }

            return dr;
        }

        /// <summary>
        /// 获取线路和区域关系
        /// </summary>
        /// <param name="lineNodeKye">绑定Tree的Nodekey</param>
        /// <returns></returns>
        public DataTable RetTableLineAndAreaRelation(string lineNodeKye)
        {
            //sql = "select T_NODEID,T_NODEKEY,T_PARAENTID from T_INFO_ROUTE where T_PARAENTID ='" + lineNodeKye + "'";
            sql = "select b.id_key,i.T_NODEID,i.T_NODEKEY,i.T_PARAENTID,b.T_AREANAME from T_INFO_ROUTE as i inner join T_BASE_AREA as b on i.T_NODEID=b.T_AREAID where T_PARAENTID ='" + lineNodeKye + "'";
            return DBdb2.RunDataTable(sql, out errMsg);
        }

        /// <summary>
        /// 获取区域信息
        /// </summary>
        /// <returns></returns>
        public DataTable RetTableArea()
        {
            sql = "select T_AREAID,T_AREANAME from T_BASE_AREA order by ID_KEY asc";

            return DBdb2.RunDataTable(sql, out errMsg);
        }

        /// <summary>
        /// 添加线路区域关联信息
        /// </summary>
        /// <param name="nodeKey"></param>
        /// <param name="oArea">删除</param>
        /// <param name="iArea">添加</param>
        /// <returns></returns>
        public bool AddLineAndAreaRealtion(string nodeKey, string[] oArea, string[] iArea, string type, out string errMsg)
        {
            int oCou = 0;
            int iCou = 0;
            int count = 0;
            bool flag = false;
            object obj = null;

            errMsg = "";

            if (oArea != null)
            {
                for (int i = 0; i < oArea.Length; i++)
                {
                    sql = "delete from T_INFO_ROUTE where T_PARAENTID='" + nodeKey + "' and T_NODEID='" + oArea[i] + "' and T_DESC='" + type + "'"; //不需要再这里删除区域下的设备

                    int j = DBdb2.RunCommand(sql, out errMsg);

                    if (j > 0)
                        oCou = 1;
                }
            }
            else { oCou = 1; }

            if (iArea != null)
            {
                for (int i = 0; i < iArea.Length; i++)
                {
                    sql = "select T_NODEKEY from T_INFO_ROUTE order by ID_KEY DESC fetch first 1 rows only ";

                    obj = DBdb2.GetSingle(sql);

                    if (obj != null && obj.ToString() != "")
                        count = int.Parse(obj.ToString()) + 1;

                    sql = "insert into T_INFO_ROUTE (T_NODEID,T_NODEKEY,T_PARAENTID,T_DESC) values ('" + iArea[i] + "','" + count + "','" + nodeKey + "','" + type + "')";

                    int j = DBdb2.RunCommand(sql, out errMsg);

                    if (j > 0)
                        iCou = 1;
                }
            }
            else { iCou = 1; }

            if (iCou == 1 && oCou == 1)
                flag = true;
            else
                errMsg = "无论新增或者删除区域,有一项失败!";

            return flag;
        }

        /// <summary>
        /// 根据节点取得区域信息
        /// 只有count=3的时候才调用此方法
        /// count=3 代表点击的时区域
        /// </summary>
        /// <returns></returns>
        public DataRow RetDataRowAreaByNodeID(string AreaNodeKey)
        {
            //sql = "select b.T_AREAID,b.T_AREANAME,b.T_ROUTEID,i.T_NODEKEY,i.T_PARAENTID from T_BASE_AREA as b inner join T_INFO_ROUTE as i on b.T_AREAID=i.T_NODEID where T_NODEKEY='" + AreaNodeKey + "'";

            sql = "select b.T_AREAID,b.t_areacd,b.T_AREANAME,b.T_ROUTEID,i.T_NODEKEY,i.T_PARAENTID from T_BASE_AREA as b inner join T_INFO_ROUTE as i on b.T_AREAID=i.T_NODEID where T_NODEKEY='" + AreaNodeKey + "' and T_desc='QY'";

            return DBdb2.RunDataRow(sql, out errMsg);
        }

        public DataRow RetDataRowLineByAreaNodeKey(string AreaNodeKey)
        {
            //sql = "select b.T_ROUTEID,b.T_ROUTENAME from T_BASE_ROUTE as b inner join T_INFO_ROUTE as i on b.T_ROUTEID=i.T_NODEID where T_NODEKEY='" + AreaNodeKey + "'";
            sql = "select b.T_ROUTEID,b.T_ROUTENAME from T_BASE_ROUTE as b inner join T_INFO_ROUTE as i on b.T_ROUTEID=i.T_NODEID where T_NODEKEY=(select T_PARAENTID from T_INFO_ROUTE where T_NODEKEY='" + AreaNodeKey + "')";
            return DBdb2.RunDataRow(sql, out errMsg);
        }


        /// <summary>
        /// 根据区域ID获取此区域下的所有设备信息
        /// </summary>
        /// <param name="AreaNodeKey"></param>
        /// <returns></returns>
        public DataTable RetDataTableDeviceByNodeID(string AreaNodeKey)
        {
            string sql = "select * from T_INFO_ROUTE as i inner join T_BASE_DEVICE as b on i.T_NODEID=b.T_DEVICEID where T_PARAENTID='" + AreaNodeKey + "' and T_desc='SB'";

            return DBdb2.RunDataTable(sql, out errMsg);
        }

        /// <summary>
        /// 根据节点取得区域信息
        /// 只有count=4的时候才调用此方法
        /// count=4 代表点击的时区域
        /// </summary>
        /// <returns></returns>
        public DataRow RetDataRowDeviceByNodeID(string NodeKey)
        {
            sql = "select b.T_AREAID,b.T_AREANAME,b.T_ROUTEID,i.T_NODEKEY,i.T_PARAENTID from T_BASE_AREA as b inner join T_INFO_ROUTE as i on b.T_AREAID=i.T_NODEID where T_NODEKEY='" + NodeKey + "'";

            return DBdb2.RunDataRow(sql, out errMsg);
        }

        /// <summary>
        /// 获取设备表
        /// </summary>
        /// <returns></returns>
        public DataTable RetTableDevice()
        {
            sql = "select * from T_BASE_DEVICE";

            return DBdb2.RunDataTable(sql, out errMsg);
        }

        /// <summary>
        /// 设备表判断是否有字节点
        /// </summary>
        /// <param name="dID0"></param>
        /// <returns></returns>
        public bool RetGetNodeBy(string dID)
        {
            bool flag = false;

            sql = "select count(*) from T_BASE_DEVICE where T_PARENTID='" + dID + "'";

            object obj = DBdb2.RunSingle(sql, out errMsg);

            if (obj != null && obj.ToString() != "")
            {
                if (int.Parse(obj.ToString()) > 0)
                    flag = true;
            }

            return flag;
        }

        /// <summary>
        /// 获取设备和区域关联记录
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetTableAreaAndDevInfo(out string errMsg)
        {
            sql = "SELECT * FROM T_INFO_ROUTE as i INNER JOIN T_BASE_DEVICE as b ON b.T_DEVICEID =i.T_NODEID";

            return DBdb2.RunDataTable(sql, out errMsg);
        }

        /// <summary>
        /// 根据区域NodeKey获取该区域下的所有设备信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetTableDevByNodeKey(string nodeKey)
        {
            sql = "select * from T_INFO_ROUTE where T_PARAENTID='" + nodeKey + "' and T_DESC='SB'";

            return DBdb2.RunDataTable(sql, out errMsg);
        }

        /// <summary>
        /// 获取设备和区域关联记录
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetTableDevAndItemInfo(out string errMsg)
        {
            //sql = "SELECT * FROM T_INFO_ROUTE as i INNER JOIN T_BASE_ITEM as b ON b.T_ITEMID =i.T_NODEID where i.T_DESC ='DJX' ";

            sql = "select * from  (select rank() over( partition by T_ITEMID order by T_STARTTIME desc ) rk,b.* from (SELECT * FROM T_INFO_ROUTE as i INNER JOIN T_BASE_ITEM as b ON b.T_ITEMID =i.T_NODEID where i.T_DESC ='DJX') as b) as y where y.rk<=1;";

            return DBdb2.RunDataTable(sql, out errMsg);
        }

        /// <summary>
        /// 删除区域
        /// </summary>
        /// <param name="areaID"></param>
        /// <param name="nodeKey"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool DelArea(string areaID, string nodeKey, out string errMsg)
        {
            bool flag = false;

            //查询到此区域下的所有设备
            sql = "SELECT * FROM T_INFO_ROUTE as r1 inner join T_INFO_ROUTE as r2 on r1.T_NODEKEY =r2.T_PARAENTID where r1.T_NODEKEY='" + nodeKey + "'";

            DataTable dtDev = DBdb2.RunDataTable(sql, out errMsg);

            if (dtDev != null && dtDev.Rows.Count > 0)
            {
                for (int i = 0; i < dtDev.Rows.Count; i++)
                {
                    string DevNodeKey = dtDev.Rows[i]["T_NODEKEY1"].ToString();

                    //删除devNodekey 设备下所有点检项
                    sql = "delete from T_INFO_ROUTE where T_PARAENTID='" + DevNodeKey + "'";

                    flag = DBdb2.RunNonQuery(sql, out errMsg);
                }
            }
            else { flag = true; }

            //删除此区域下的所有设备
            if (flag == true)
            {
                sql = "delete from T_INFO_ROUTE where T_PARAENTID ='" + nodeKey + "'";

                flag = DBdb2.RunNonQuery(sql, out errMsg);
            }

            //删除此区域
            if (flag == true)
            {
                sql = "delete from T_INFO_ROUTE where T_NODEKEY='" + nodeKey + "'";

                flag = DBdb2.RunNonQuery(sql, out errMsg);
            }

            return flag;
        }

        /// <summary>
        /// 添加设备和点检项关系
        /// 添加点检项记录
        /// 调用任务生成接口
        /// </summary>
        /// <param name="nodeKey"></param>
        /// <param name="itemID"></param>
        /// <param name="itemBw"></param>
        /// <param name="itemDesc"></param>
        /// <param name="itemContent"></param>
        /// <param name="itemObserve"></param>
        /// <param name="itemUnit"></param>
        /// <param name="itemType"></param>
        /// <param name="itemStatus"></param>
        /// <param name="itemStatusQJ"></param>
        /// <param name="itemUpper"></param>
        /// <param name="itemLower"></param>
        /// <param name="itemSpectrum"></param>
        /// <param name="itemStartTime"></param>
        /// <param name="itemPerValue"></param>
        /// <param name="itemPerType"></param>
        /// <returns></returns>
        public bool AddItem(string nodeKey, string itemBw, string itemDesc, string itemContent, string itemObserve, string itemUnit, string itemType, string itemStatus, string itemStatusQJ, string itemUpper, string itemLower, string itemSpectrum, string itemStartTime, string itemPerValue, string itemPerType, out string errMsg)
        {
            object obj = null;
            bool flag = false;
            int count = 0;

            //添加 T_INFO_ROUTE 信息
            sql = "select T_NODEKEY from T_INFO_ROUTE order by ID_KEY DESC fetch first 1 rows only ";

            obj = DBdb2.GetSingle(sql);

            if (obj != null && obj.ToString() != "")
                count = int.Parse(obj.ToString()) + 1;

            sql = "insert into T_INFO_ROUTE (T_NODEID,T_NODEKEY,T_PARAENTID) values ('1','" + count + "','" + nodeKey + "')";

            flag = DBdb2.RunNonQuery(sql, out errMsg);

            if (flag == true)
            {
                sql = "select T_NODEID from T_INFO_ROUTE where T_NODEKEY='" + nodeKey + "'";

                obj = DBdb2.RunSingle(sql, out errMsg);

                if (obj != null && obj.ToString() != "")
                {
                    sql = "insert into T_BASE_ITEM ";
                    sql += "(T_ITEMPOSITION,T_ITEMDESC,T_CONTENT,T_TYPE,I_STATUS,T_OBSERVE,T_UNIT,F_LOWER,F_UPPER,I_SPECTRUM,T_DEVICEID,T_STARTTIME,T_PERIODTYPE,T_PERIODVALUE,T_STATUS)";
                    sql += " values ";
                    sql += "('" + itemBw + "','" + itemDesc + "','" + itemContent + "','" + itemType + "'," + itemStatus + ",'" + itemObserve + "','" + itemUnit + "'," + double.Parse(itemLower) + "," + double.Parse(itemUpper) + "," + int.Parse(itemSpectrum) + ",'" + obj.ToString() + "','" + DateTime.Parse(itemStartTime).ToString("yyyy-MM-dd 0:00:00") + "'," + int.Parse(itemPerType) + "," + int.Parse(itemPerValue) + "," + int.Parse(itemStatusQJ) + ")";

                    flag = DBdb2.RunNonQuery(sql, out errMsg);

                    if (flag == true)
                    {
                        //调用接口生成任务
                        //ManageQuestcomplete(string[] routeId, string[] area, string[] deviceId, string[] itemId, string[] judge, string[] sTimes, string[] type, string[] count, string[] deviceState, string[] state)
                        string[] route = new string[1];
                        string[] area = new string[1];
                        string[] device = new string[1];
                        string[] item = new string[1];
                        string[] judge = new string[1]; //操作类型：创建点检项关系 删除点检项关系 编辑点检项  编辑设备状态
                        string[] sTimes = new string[1];
                        string[] type = new string[1];
                        string[] count1 = new string[1];
                        string[] devState = new string[1];
                        string[] state = new string[1];

                        DataRow drRou = null;
                        DataRow drDev = null;
                        DataRow drArea = null;

                        sql = "SELECT T_NODEID,T_PARAENTID FROM T_INFO_ROUTE where T_NODEKEY='" + nodeKey + "'";

                        drDev = DBdb2.RunDataRow(sql, out errMsg);

                        if (drDev != null)
                        {
                            sql = "SELECT T_NODEID,T_PARAENTID FROM T_INFO_ROUTE where T_NODEKEY='" + drDev["T_PARAENTID"] + "'";

                            drArea = DBdb2.RunDataRow(sql, out errMsg);

                            if (drArea != null)
                            {
                                sql = "SELECT T_NODEID,T_PARAENTID FROM T_INFO_ROUTE where T_NODEKEY='" + drArea["T_PARAENTID"] + "' and T_DESC='XL'";

                                drRou = DBdb2.RunDataRow(sql, out errMsg);
                            }

                            route[0] = drRou["T_NODEID"].ToString();
                            area[0] = drArea["T_NODEID"].ToString();
                            device[0] = drDev["T_NODEID"].ToString();
                            item[0] = "1";//itemID;
                            judge[0] = "0"; //操作类型：创建点检项关系 0 删除点检项关系 1 编辑点检项 2  编辑设备状态3
                            sTimes[0] = itemStartTime; //T_STARTTIME
                            type[0] = itemPerType;     //
                            count1[0] = itemPerValue;
                            devState[0] = itemStatus;
                            state[0] = itemStatusQJ;
                        }

                        flag = qc.ManageQuestcomplete(route, area, device, item, judge, sTimes, type, count1, devState, state);
                    }
                    else { errMsg = "新建点检项失败!"; }
                }
            }
            else
            { errMsg = "添加T_INFO_ROUTE表关系错误!"; }

            return flag;
        }

        /// <summary>
        /// 获取点检项表纪录
        /// </summary>
        /// <param name="nodeKey"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable RetTableItemByNodeKey(string nodeKey, out string errMsg)
        {
            sql = "SELECT * FROM T_INFO_ROUTE as i inner join T_BASE_ITEM as b on i.T_NODEID=b.T_ITEMID where  i.T_NODEKEY='" + nodeKey + "'  order by  T_STARTTIME desc fetch first 1 rows only";

            return DBdb2.RunDataTable(sql, out errMsg);
        }

        /// <summary>
        /// 添加，编辑设备和点检项关联信息 (原来方法备用)
        /// </summary>
        /// <param name="nodeKey"></param>
        /// <param name="oArea">删除的纪录</param>
        /// <param name="iArea">添加的纪录</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool AddDeviceAndItemRealtionBY(string nodeKey, string[] oArea, string[] iArea, out string errMsg)
        {
            int oCou = 0;
            int iCou = 0;
            int count = 0;
            bool flag = false;
            object obj = null;

            DataRow drRou = null;
            DataRow drDev = null;
            DataRow drArea = null;

            errMsg = "";

            //查询线路区域设备ID

            sql = "SELECT T_NODEID,T_PARAENTID FROM T_INFO_ROUTE where T_NODEKEY='" + nodeKey + "'";

            drDev = DBdb2.RunDataRow(sql, out errMsg);

            if (drDev != null)
            {
                sql = "SELECT T_NODEID,T_PARAENTID FROM T_INFO_ROUTE where T_NODEKEY='" + drDev["T_PARAENTID"] + "'";

                drArea = DBdb2.RunDataRow(sql, out errMsg);

                if (drArea != null)
                {
                    sql = "SELECT T_NODEID,T_PARAENTID FROM T_INFO_ROUTE where T_NODEKEY='" + drArea["T_PARAENTID"] + "' and T_DESC='XL'";

                    drRou = DBdb2.RunDataRow(sql, out errMsg);

                    #region 删除点检项关系
                    if (oArea != null)
                    {
                        string[] route = new string[oArea.Length];
                        string[] area = new string[oArea.Length];
                        string[] device = new string[oArea.Length];
                        string[] item = new string[oArea.Length];
                        string[] judge = new string[oArea.Length]; //操作类型：创建点检项关系 删除点检项关系 编辑点检项  编辑设备状态
                        string[] sTimes = new string[oArea.Length];
                        string[] type = new string[oArea.Length];
                        string[] count1 = new string[oArea.Length];
                        string[] devState = new string[oArea.Length];
                        string[] state = new string[oArea.Length];

                        for (int i = 0; i < oArea.Length; i++)
                        {
                            route[i] = drRou["T_NODEID"].ToString();
                            area[i] = drArea["T_NODEID"].ToString();
                            device[i] = drDev["T_NODEID"].ToString();

                            sql = "select * from T_BASE_ITEM where ID_KEY=" + oArea[i];

                            DataRow drItem = DBdb2.RunDataRow(sql, out errMsg);

                            if (drItem != null)
                            {
                                item[i] = drItem["T_ITEMID"].ToString();
                                judge[i] = "1"; //操作类型：创建点检项关系 0 删除点检项关系 1 编辑点检项 2  编辑设备状态3
                                sTimes[i] = drItem["T_STARTTIME"].ToString();// itemStartTime; //T_STARTTIME
                                type[i] = drItem["T_PERIODTYPE"].ToString();//itemPerType;  //T_PERIODTYPE
                                count1[i] = drItem["T_PERIODVALUE"].ToString();// itemPerValue;//T_PERIODVALUE
                                devState[i] = drItem["I_STATUS"].ToString();// itemStatus;//I_STATUS
                                state[i] = drItem["T_STATUS"].ToString();// itemStatusQJ;//T_STATUS

                                sql = "delete from T_INFO_ROUTE where T_PARAENTID='" + nodeKey + "' and T_NODEID='" + drItem["T_ITEMID"].ToString() + "' and T_DESC='DJX'";

                                int j = DBdb2.RunCommand(sql, out errMsg);

                                if (j > 0)
                                    oCou = 1;
                            }
                        }

                        qc.ManageQuestcomplete(route, area, device, item, judge, sTimes, type, count1, devState, state);
                    }
                    else { oCou = 1; }
                    #endregion

                    #region 添加点检项关系
                    if (iArea != null)
                    {
                        string[] route = new string[iArea.Length];
                        string[] area = new string[iArea.Length];
                        string[] device = new string[iArea.Length];
                        string[] item = new string[iArea.Length];
                        string[] judge = new string[iArea.Length]; //操作类型：创建点检项关系 删除点检项关系 编辑点检项  编辑设备状态
                        string[] sTimes = new string[iArea.Length];
                        string[] type = new string[iArea.Length];
                        string[] count1 = new string[iArea.Length];
                        string[] devState = new string[iArea.Length];
                        string[] state = new string[iArea.Length];

                        for (int i = 0; i < iArea.Length; i++)
                        {
                            route[i] = drRou["T_NODEID"].ToString();
                            area[i] = drArea["T_NODEID"].ToString();
                            device[i] = drDev["T_NODEID"].ToString();

                            sql = "select * from T_BASE_ITEM where ID_KEY=" + iArea[i];

                            DataRow drItem = DBdb2.RunDataRow(sql, out errMsg);

                            if (drItem != null)
                            {
                                item[i] = drItem["T_ITEMID"].ToString();
                                judge[i] = "0"; //操作类型：创建点检项关系 0 删除点检项关系 1 编辑点检项 2  编辑设备状态3
                                sTimes[i] = drItem["T_STARTTIME"].ToString();// itemStartTime; //T_STARTTIME
                                type[i] = drItem["T_PERIODTYPE"].ToString();//itemPerType;  //T_PERIODTYPE
                                count1[i] = drItem["T_PERIODVALUE"].ToString();// itemPerValue;//T_PERIODVALUE
                                devState[i] = drItem["I_STATUS"].ToString();// itemStatus;//I_STATUS
                                state[i] = drItem["T_STATUS"].ToString();// itemStatusQJ;//T_STATUS

                                sql = "select T_NODEKEY from T_INFO_ROUTE order by ID_KEY DESC fetch first 1 rows only ";

                                obj = DBdb2.GetSingle(sql);

                                if (obj != null && obj.ToString() != "")
                                    count = int.Parse(obj.ToString()) + 1;

                                sql = "insert into T_INFO_ROUTE (T_NODEID,T_NODEKEY,T_PARAENTID,T_DESC) values ('" + drItem["T_ITEMID"].ToString() + "','" + count + "','" + nodeKey + "','DJX')";

                                int j = DBdb2.RunCommand(sql, out errMsg);

                                if (j > 0)
                                    iCou = 1;
                            }
                        }

                        flag = qc.ManageQuestcomplete(route, area, device, item, judge, sTimes, type, count1, devState, state);
                    }
                    else { iCou = 1; }

                    #endregion

                    if (iCou == 1 && oCou == 1)
                    { }
                    else
                        errMsg = "无论新增或删除设备和点检项关系,有一项失败!";
                }
            }
            return flag;
        }

        /// <summary>
        /// 添加，编辑设备和点检项关联信息
        /// </summary>
        /// <param name="nodeKey"></param>
        /// <param name="oArea">删除的纪录</param>
        /// <param name="iArea">添加的纪录</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool AddDeviceAndItemRealtion(string nodeKey, string[] oArea, string[] iArea, out string errMsg)
        {
            int oCou = 0;
            int iCou = 0;
            int count = 0;
            bool flag = false;
            object obj = null;

            DataRow drRou = null;
            DataRow drDev = null;
            DataRow drArea = null;

            errMsg = "";

            //查询线路区域设备ID
            PlanDAL pd = new PlanDAL();

            sql = "SELECT T_NODEID,T_PARAENTID FROM T_INFO_ROUTE where T_NODEKEY='" + nodeKey + "'";

            drDev = DBdb2.RunDataRow(sql, out errMsg);

            if (drDev != null)
            {
                sql = "SELECT T_NODEID,T_PARAENTID FROM T_INFO_ROUTE where T_NODEKEY='" + drDev["T_PARAENTID"] + "'";

                drArea = DBdb2.RunDataRow(sql, out errMsg);

                if (drArea != null)
                {
                    sql = "SELECT T_NODEID,T_PARAENTID FROM T_INFO_ROUTE where T_NODEKEY='" + drArea["T_PARAENTID"] + "' and T_DESC='XL'";

                    drRou = DBdb2.RunDataRow(sql, out errMsg);

                    #region 删除点检项关系
                    if (oArea != null)
                    {
                        string[] route = new string[oArea.Length];
                        string[] area = new string[oArea.Length];
                        string[] device = new string[oArea.Length];
                        string[] item = new string[oArea.Length];
                        int[] judge = new int[oArea.Length]; //操作类型：创建点检项关系 删除点检项关系 编辑点检项  编辑设备状态
                        string[] sTimes = new string[oArea.Length];
                        string[] type = new string[oArea.Length];
                        string[] count1 = new string[oArea.Length];
                        string[] devState = new string[oArea.Length];
                        string[] state = new string[oArea.Length];

                        for (int i = 0; i < oArea.Length; i++)
                        {
                            route[i] = drRou["T_NODEID"].ToString();
                            area[i] = drArea["T_NODEID"].ToString();
                            device[i] = drDev["T_NODEID"].ToString();

                            sql = "select * from T_BASE_ITEM where ID_KEY=" + oArea[i];

                            DataRow drItem = DBdb2.RunDataRow(sql, out errMsg);

                            if (drItem != null)
                            {
                                item[i] = drItem["T_ITEMID"].ToString();
                                judge[i] = 1; //操作类型：创建点检项关系 0 删除点检项关系 1 编辑点检项 2  编辑设备状态3
                                sTimes[i] = drItem["T_STARTTIME"].ToString();// itemStartTime; //T_STARTTIME
                                type[i] = drItem["T_PERIODTYPE"].ToString();//itemPerType;  //T_PERIODTYPE
                                count1[i] = drItem["T_PERIODVALUE"].ToString();// itemPerValue;//T_PERIODVALUE
                                devState[i] = drItem["I_STATUS"].ToString();// itemStatus;//I_STATUS
                                state[i] = drItem["T_STATUS"].ToString();// itemStatusQJ;//T_STATUS

                                sql = "delete from T_INFO_ROUTE where T_PARAENTID='" + nodeKey + "' and T_NODEID='" + drItem["T_ITEMID"].ToString() + "' and T_DESC='DJX'";

                                int j = DBdb2.RunCommand(sql, out errMsg);

                                if (j > 0)
                                    oCou = 1;
                            }
                        }

                        flag = pd.EditRelation(route[0], area[0], device[0], item, judge);// qc.ManageQuestcomplete(route, area, device, item, judge, sTimes, type, count1, devState, state);

                        //public bool EditRelation(string routeID, string areaID, string deviceID, string[] items, int[] judge)
                    }
                    else { oCou = 1; }
                    #endregion

                    #region 添加点检项关系
                    if (iArea != null)
                    {
                        string[] route = new string[iArea.Length];
                        string[] area = new string[iArea.Length];
                        string[] device = new string[iArea.Length];
                        string[] item = new string[iArea.Length];
                        int[] judge = new int[iArea.Length]; //操作类型：创建点检项关系 删除点检项关系 编辑点检项  编辑设备状态
                        string[] sTimes = new string[iArea.Length];
                        string[] type = new string[iArea.Length];
                        string[] count1 = new string[iArea.Length];
                        string[] devState = new string[iArea.Length];
                        string[] state = new string[iArea.Length];

                        for (int i = 0; i < iArea.Length; i++)
                        {
                            route[i] = drRou["T_NODEID"].ToString();
                            area[i] = drArea["T_NODEID"].ToString();
                            device[i] = drDev["T_NODEID"].ToString();

                            sql = "select * from T_BASE_ITEM where ID_KEY=" + iArea[i];

                            DataRow drItem = DBdb2.RunDataRow(sql, out errMsg);

                            if (drItem != null)
                            {
                                item[i] = drItem["T_ITEMID"].ToString();
                                judge[i] = 0; //操作类型：创建点检项关系 0 删除点检项关系 1 编辑点检项 2  编辑设备状态3
                                sTimes[i] = drItem["T_STARTTIME"].ToString();// itemStartTime; //T_STARTTIME
                                type[i] = drItem["T_PERIODTYPE"].ToString();//itemPerType;  //T_PERIODTYPE
                                count1[i] = drItem["T_PERIODVALUE"].ToString();// itemPerValue;//T_PERIODVALUE
                                devState[i] = drItem["I_STATUS"].ToString();// itemStatus;//I_STATUS
                                state[i] = drItem["T_STATUS"].ToString();// itemStatusQJ;//T_STATUS

                                sql = "select T_NODEKEY from T_INFO_ROUTE order by ID_KEY DESC fetch first 1 rows only ";

                                obj = DBdb2.GetSingle(sql);

                                if (obj != null && obj.ToString() != "")
                                    count = int.Parse(obj.ToString()) + 1;

                                sql = "insert into T_INFO_ROUTE (T_NODEID,T_NODEKEY,T_PARAENTID,T_DESC) values ('" + drItem["T_ITEMID"].ToString() + "','" + count + "','" + nodeKey + "','DJX')";

                                int j = DBdb2.RunCommand(sql, out errMsg);

                                if (j > 0)
                                    iCou = 1;
                            }
                        }

                        flag = pd.EditRelation(route[0], area[0], device[0], item, judge);
                        //flag = qc.ManageQuestcomplete(route, area, device, item, judge, sTimes, type, count1, devState, state);
                    }
                    else { iCou = 1; }

                    #endregion

                    if (iCou == 1 && oCou == 1)
                    { }
                    else
                        errMsg = "无论新增或删除设备和点检项关系,有一项失败!";
                }
            }
            return flag;
        }

        public string RetStatusBySID(string sid)
        {
            object obj = null;
            string res = "";

            if (sid != "")
            {
                sql = "select T_STATUSDESC from T_BASE_STATUS where I_STATUSID=" + sid;
                obj = DBdb2.RunSingle(sql, out errMsg);

                if (obj != null)
                    res = obj.ToString();

            }

            return res;
        }
        //*********************************//



        #region 根据nodekey获取设备
        /// <summary>
        /// 获取设备数量
        /// </summary>
        /// <param name="id">区域编号</param>
        /// <returns></returns>
        public int RetSBCount(string id)
        {
            sql = "select count(*)  from (select  i.id_key as ik,d.id_key as dk ,d.t_deviceid,d.t_devicedesc,d.t_parentid,d.b_attachment, i.t_nodekey,i.T_nodeid from t_info_route as i inner join t_base_device as d on i.t_nodeid=d.t_deviceid where t_desc='SB' and i.t_paraentid='" + id + "') as c";

            return DBdb2.RunRowCount(sql, out errMsg);
        }

        /// <summary>
        /// 获取设备数据集
        /// </summary>
        /// <param name="id">区域编号</param>
        /// <param name="sCount">开始条数</param>
        /// <param name="eCount">结束条数</param>
        /// <returns></returns>
        public DataTable RetTabSB(string id, int sCount, int eCount)
        {
            sql = "select * from (select c.*,rownumber () over (order by c.ik asc) as rowid  from (select  i.id_key as ik,d.id_key as dk ,d.t_deviceid,d.t_devicedesc,d.t_parentid,d.b_attachment, i.t_nodekey,i.T_nodeid from t_info_route as i inner join t_base_device as d on i.t_nodeid=d.t_deviceid where t_desc='SB' and i.t_paraentid='" + id + "') as c) as z";

            sql += " where z.rowid between " + sCount + " and " + eCount;

            dt = DBdb2.RunDataTable(sql, out errMsg);

            return dt;
        }
        #endregion

        #region 根据nodekey获取区域
        /// <summary>
        /// 获取区域数量
        /// </summary>
        /// <param name="id">区域编号</param>
        /// <returns></returns>
        public int RetAreaCount(string id)
        {
            sql = "select count(*) from T_INFO_ROUTE as i inner join T_BASE_AREA as b on i.T_NODEID=b.T_AREAID where T_PARAENTID ='" + id + "' and i.t_desc='QY'";

            return DBdb2.RunRowCount(sql, out errMsg);
        }

        /// <summary>
        /// 获取区域数据集
        /// </summary>
        /// <param name="id">区域编号</param>
        /// <param name="sCount">开始条数</param>
        /// <param name="eCount">结束条数</param>
        /// <returns></returns>
        public DataTable RetTabArea(string id, int sCount, int eCount)
        {
            sql = "select * from (select c.*, rownumber() over(order by c.bk asc )as rowid from ";
            sql += "(select b.id_key as bk,i.id_key as ik, i.T_NODEID,i.T_NODEKEY,i.T_PARAENTID,b.T_AREANAME,b.t_areaid,b.t_areacd from T_INFO_ROUTE as i inner join T_BASE_AREA as b on i.T_NODEID=b.T_AREAID where T_PARAENTID ='" + id + "' and i.t_desc='QY') as c) as z";
            sql += " where z.rowid between " + sCount + " and " + eCount + "";

            dt = DBdb2.RunDataTable(sql, out errMsg);

            return dt;
        }
        #endregion

        #region 根据nodekey获取设备
        /// <summary>
        /// 获取线路数量
        /// </summary>
        /// <param name="id">线路编号</param>
        /// <returns></returns>
        public int RetRouteCount(string id)
        {
            sql = "select count(*) from (select * from t_base_route as b inner join t_sys_organize as s on b.t_orgid=s.t_orgid) as c inner join t_info_route as i on c.t_routeid=i.t_nodeid where t_desc='XL' and i.t_paraentid='" + id + "'";

            return DBdb2.RunRowCount(sql, out errMsg);
        }

        /// <summary>
        /// 获取线路数据集
        /// </summary>
        /// <param name="id">线路编号</param>
        /// <param name="sCount">开始条数</param>
        /// <param name="eCount">结束条数</param>
        /// <returns></returns>
        public DataTable RetTabRoute(string id, int sCount, int eCount)
        {
            sql = "select * from (select n.* ,rownumber() over(order by n.t_nodekey asc ) as rowid from ( ";

            sql += " select * from (select * from t_base_route as b inner join t_sys_organize as s on b.t_orgid=s.t_orgid) as c inner join t_info_route as i on c.t_routeid=i.t_nodeid where t_desc='XL' and i.t_paraentid='" + id + "' ";

            sql += " ) as n) as m where m.rowid between " + sCount + " and " + eCount + "";

            dt = DBdb2.RunDataTable(sql, out errMsg);

            return dt;
        }
        #endregion

        #region 胡进财 2013/03/18
        /// <summary>
        /// 获取线路Tree
        /// </summary>
        /// <returns></returns>
        public DataTable GetRouteTree()
        {
            sql = "select r.T_NODEKEY ID,r.T_NODEID IDE,br.T_ROUTENAME NAME,r.T_PARAENTID PARMENTID from T_INFO_ROUTE r left join T_BASE_ROUTE br on r.T_NODEID=br.T_ROUTEID where br.T_ROUTENAME is not null union select r.T_NODEKEY ID,r.T_NODEID IDE,br.T_AREANAME NAME,r.T_PARAENTID PARMENTID from T_INFO_ROUTE r left join T_BASE_AREA br on r.T_NODEID=br.T_AREAID where br.T_AREANAME is not null union select r.T_NODEKEY ID,r.T_NODEID IDE,br.T_DEVICEDESC NAME,r.T_PARAENTID PARMENTID from T_INFO_ROUTE r left join T_BASE_DEVICE br on r.T_NODEID=br.T_DEVICEID where br.T_DEVICEDESC is not null union select r.T_NODEKEY ID,r.T_NODEID IDE,br.T_ITEMDESC NAME,r.T_PARAENTID PARMENTID from T_INFO_ROUTE r left join T_BASE_ITEM br on r.T_NODEID=br.T_ITEMID where br.T_ITEMDESC is not null;";
            try
            {
                dt = DBdb2.RunDataTable(sql, out errMsg);
            }
            catch (Exception ex) { }
            return dt;
        }

        /// <summary>
        /// 获取某个人员的所有线路
        /// </summary>
        /// <param name="userID">人员编号</param>
        /// <returns></returns>
        public IList<Hashtable> GetRouteTree(string userID)
        {
            DateHelper dh = new DateHelper();
            sql = "select T_ROUTEID,T_ROUTENAME from T_BASE_ROUTE where T_ORGID=( select T_ORGID from T_SYS_MEMBERRELATION where T_USERID='" + userID + "');";
            try
            {
                dt = DBdb2.RunDataTable(sql, out errMsg);
            }
            catch (Exception ex) { }
            return dh.DataTableToList(dt);
        }



        #region 线路数据分页
        /// <summary>
        /// 获取线路数量
        /// </summary>
        /// <param name="id">线路编号</param>
        /// <returns></returns>
        public int GetRountCount(string id)
        {
            sql = "select count(*) from (select r.ID,r.RouteID,r.RouteName,r.Rtype,rownumber() over(order by ID asc ) as rowid  from (select r.ID_KEY ID,r.T_ROUTEID RouteID,r.T_ROUTENAME RouteName, case when  r.I_TYPE=0 then '点检' when r.I_TYPE=1 then '巡检' end Rtype from T_BASE_ROUTE r inner join (select T_NODEID from T_INFO_ROUTE where T_DESC='XL' and T_PARAENTID='" + id + "') rp on r.T_ROUTEID=rp.T_NODEID) r)as a;";
            return DBdb2.RunRowCount(sql, out errMsg);
        }

        /// <summary>
        /// 获取线路数据集
        /// </summary>
        /// <param name="id">线路编号</param>
        /// <param name="sCount">开始条数</param>
        /// <param name="eCount">结束条数</param>
        /// <returns></returns>
        public DataTable GetRouteDt(string id, int sCount, int eCount)
        {
            sql = "select * from (select r.ID,r.RouteID,r.RouteName,r.Rtype,rownumber() over(order by ID asc ) as rowid  from (select r.ID_KEY ID,r.T_ROUTEID RouteID,r.T_ROUTENAME RouteName, case when  r.I_TYPE=0 then '点检' when r.I_TYPE=1 then '巡检' end Rtype from T_BASE_ROUTE r inner join (select T_NODEID from T_INFO_ROUTE where T_PARAENTID='" + id + "') rp on r.T_ROUTEID=rp.T_NODEID) r)as a where a.rowid between " + sCount + " and " + eCount + ";";
            dt = DBdb2.RunDataTable(sql, out errMsg);
            return dt;
        }
        #endregion
        #endregion

    }
}
