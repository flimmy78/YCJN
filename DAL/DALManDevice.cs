using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.Helper;
using System.Collections;
using System.Data;
using SAC.DB2;
using System.Data.OleDb;

namespace DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class DALManDevice
    {
        string sql = "";
        string errMsg = "";

        DataTable dt = null;
        QuestcompleteDAL qc = new QuestcompleteDAL();

        /// <summary>
        /// 获取所有设备表
        /// </summary>
        /// <returns></returns>
        public DataTable RetTableDevice()
        {
            sql = "select * from T_BASE_DEVICE";

            return DBdb2.RunDataTable(sql, out errMsg);
        }

        /// <summary>
        /// 判断子节点是否存在
        /// </summary>
        /// <returns>2:最底层设备</returns>
        public int RetCount(string nodeID)
        {
            int count = 0;
            int countDev = 0;

            sql = "select * from T_BASE_DEVICE where T_DEVICEID='" + nodeID + "'";

            countDev = DBdb2.RunRowCount(sql, out errMsg);

            if (countDev > 0)
            {
                count = 1;
            }
            else
            { count = 2; }

            return count;
        }

        /// <summary>
        /// 根据设备标识获取设备信息
        /// </summary>
        /// <param name="treeID"></param>
        /// <returns></returns>
        public DataRow RetdrBySBID(string treeID)
        {
            sql = "select * from T_BASE_DEVICE where T_DEVICEID='" + treeID + "'";

            return DBdb2.RunDataRow(sql, out errMsg);
        }

        /// <summary>
        /// 附件上传
        /// </summary>
        /// <param name="SBID"></param>
        /// <param name="fileBytes"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool RetBoolUpFile(string SBID, byte[] fileBytes, out string errMsg)
        {
            errMsg = "";

            bool flag = false;

            if (fileBytes.Length > 0)
            {
                sql = "update T_BASE_DEVICE set B_ATTACHMENT=? where T_DEVICEID='" + SBID + "'";

                OleDbConnection con = new OleDbConnection(DBdb2.SetConString());
                try
                {
                    con.Open();
                    OleDbCommand oledbcom = new OleDbCommand(sql, con);

                    oledbcom.Parameters.Add("?", fileBytes);

                    if (oledbcom.ExecuteNonQuery() > 0)
                        flag = true;

                    con.Close();
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                }
                finally { con.Close(); }
            }

            return flag;
        }

        /// <summary>
        /// 附件下载 
        /// </summary>
        /// <param name="sbID"></param>
        /// <param name="sbName"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public byte[] RetBoolDownFile(string sbID, string sbName, out string errMsg)
        {
            byte[] File = null;

            DataRow dr = null;

            string fileName = sbName + ".doc";

            sql = "select B_ATTACHMENT from T_BASE_DEVICE where T_DEVICEID='" + sbID + "'";

            dr = DBdb2.RunDataRow(sql, out errMsg);

            try
            {
                if (dr != null)
                {
                    File = (byte[])dr[0];
                }
                else { errMsg = "没有查询到相关纪录!"; }
            }
            catch
            {
                errMsg = "此设备无附件可供下载！";
            }
            return File;
        }

        /// <summary>
        /// 返回Device INFO ADN BASE
        /// </summary>
        /// <param name="devID"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable RetTabDevByDevID(string devID, out string errMsg)
        {
            //sql = "SELECT i.ID_KEY,b.T_DEVICEID,b.T_DEVICEDESC,i.T_TIME,i.I_STATUS FROM T_INFO_DEVICE as i inner join T_BASE_DEVICE as b on i.T_DEVICEID=b.T_DEVICEID where i.T_DEVICEID='" + devID + "' order by i.T_TIME desc";
            sql = "select * from T_BASE_STATUS as s inner join (SELECT i.ID_KEY,b.T_DEVICEID,b.T_DEVICEDESC,i.T_TIME,i.I_STATUS FROM T_INFO_DEVICE as i inner join T_BASE_DEVICE as b on i.T_DEVICEID=b.T_DEVICEID where i.T_DEVICEID='" + devID + "' order by i.T_TIME desc) as t on s.I_STATUSID=t.I_STATUS";
            return DBdb2.RunDataTable(sql, out errMsg);
        }

        /// <summary>
        /// 设备状态分页
        /// </summary>
        /// <param name="sbID"></param>
        /// <param name="sCount"></param>
        /// <param name="eCount"></param>
        /// <returns></returns>
        public DataTable RetTabDevByDevID(string sbID, int sCount, int eCount)
        {
            sql = "select * from (select b.*,rownumber() over(order by b.T_TIME desc ) as rowid  from ( select * from T_BASE_STATUS as s inner join (SELECT i.ID_KEY,b.T_DEVICEID,b.T_DEVICEDESC,i.T_TIME,i.I_STATUS FROM T_INFO_DEVICE as i inner join T_BASE_DEVICE as b on i.T_DEVICEID=b.T_DEVICEID where i.T_DEVICEID='" + sbID + "' order by i.T_TIME desc) as t on s.I_STATUSID=t.I_STATUS)  as b )as a where a.rowid BETWEEN " + sCount + " AND " + eCount;

            dt = DBdb2.RunDataTable(sql, out errMsg);

            return dt;
        }
        public int RetTabDevByDevIDCount(string sbID)
        {
            sql = "select count(*) from T_BASE_STATUS as s inner join (SELECT i.ID_KEY,b.T_DEVICEID,b.T_DEVICEDESC,i.T_TIME,i.I_STATUS FROM T_INFO_DEVICE as i inner join T_BASE_DEVICE as b on i.T_DEVICEID=b.T_DEVICEID where i.T_DEVICEID='" + sbID + "' order by i.T_TIME desc) as t on s.I_STATUSID=t.I_STATUS";

            int count = DBdb2.RunRowCount(sql, out errMsg);

            return count;
        }

        /// <summary>
        /// 获取设备表中跟目录中第一条
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns>deviceDesc + ',' + file + ',' + status;</returns>
        public string GetDrDevByTopOne(string sbID, out string errMsg)
        {
            string status = "无";
            string deviceDesc = "";
            string file = "无";

            string whgc = "";
            string jhgc = "";
            string cbzx = "";
            string scbs = "";
            string sbyy = "";
            string djzt = "";
            string fjbm = "";
            string sql = "";

            if (sbID == "")
                sql = "select * from T_BASE_DEVICE where T_PARENTID not in (select T_DEVICEID from T_BASE_DEVICE)";// "select * from T_BASE_Device order by ID_KEY fetch first 1 rows only";
            else
                sql = "select * from T_BASE_Device where T_DEVICEID='" + sbID + "' order by ID_KEY fetch first 1 rows only ";


            DataRow dr = DBdb2.RunDataRow(sql, out errMsg);

            if (dr != null)
            {
                deviceDesc = dr["T_DEVICEDESC"].ToString();

                if (dr["B_ATTACHMENT"] != null && dr["B_ATTACHMENT"].ToString() != "")
                    file = "有";

                sql = "SELECT * FROM T_Info_DEVICE  as d inner join T_BASE_STATUS as s on d.I_STATUS=s.I_STATUSID where d.T_DEVICEID='" + dr["T_DEVICEID"].ToString() + "' order by d.T_Time desc fetch first 1 rows only";

                DataRow drS = DBdb2.RunDataRow(sql, out errMsg);

                if (drS != null)
                    status = drS["T_STATUSDESC"].ToString();

                if (dr["T_SWERK"] != null)
                    whgc = dr["T_SWERK"].ToString();
                else
                    whgc = "";

                if (dr["T_IWERK"] != null)
                    jhgc = dr["T_IWERK"].ToString();
                else
                    jhgc = "";

                if (dr["T_KOSTL"] != null)
                    cbzx = dr["T_KOSTL"].ToString();
                else
                    cbzx = "";

                if (dr["T_SCBS"] != null)
                    scbs = dr["T_SCBS"].ToString();
                else
                    scbs = "";

                if (dr["T_FHZT"] != null)
                {
                    if (dr["T_FHZT"].ToString() == "0")
                        djzt = "数据成功传到点检";
                    else if (dr["T_FHZT"].ToString() == "1")
                        djzt = "SAP与ESB连接失败";
                    else if (dr["T_FHZT"].ToString() == "2")
                        djzt = "ESB与中间件连接失败";
                    else if (dr["T_FHZT"].ToString() == "3")
                        djzt = "中间件与点检连接失败";
                    else if (dr["T_FHZT"].ToString() == "4")
                        djzt = "点检更新失败";
                    else if (dr["T_FHZT"].ToString() == "5")
                        djzt = "点检更新成功";
                }
                else
                    djzt = "";

                if (dr["T_SBYY"] != null)
                    sbyy = dr["T_SBYY"].ToString();
                else
                    sbyy = "";

                fjbm = dr["t_parentid"].ToString();
            }

            return deviceDesc + ',' + file + ',' + status + ',' + whgc + ',' + jhgc + ',' + cbzx + ',' + scbs + ',' + djzt + ',' + sbyy + ',' + fjbm;
        }

        /// <summary>
        /// 获取STATUS表
        /// </summary>
        /// <returns></returns>
        public DataTable RetTabStatus()
        {
            sql = "select i_statusid,T_statusDesc from T_BASE_STATUS";

            return DBdb2.RunDataTable(sql, out errMsg);
        }

        /// <summary>
        /// 新增设备状态
        /// </summary>
        /// <param name="devID"></param>
        /// <param name="time"></param>
        /// <param name="count"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool SaveStatus(string devID, string time, string count, out string errMsg)
        {
            errMsg = "";

            bool flag = false;

            sql = "insert into T_INFO_DEVICE (T_DEVICEID,T_TIME,I_STATUS) values ('" + devID + "','" + DateTime.Parse(time).ToString("yyyy-MM-dd 0:00:00") + "'," + count + ")";

            flag = DBdb2.RunNonQuery(sql, out errMsg);

            return flag;
        }

        /// <summary>
        /// 获取点检项
        /// </summary>
        /// <param name="sbID"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable RetTabItemsBySbID(string sbID, out string errMsg)
        {
            errMsg = "";
            DataTable dt = null;

            //sql = "select * from (select rank() over(partition by T_ITEMID order by T_STARTTIME desc) rk,b.* from T_BASE_ITEM as b where b.T_DeviceID ='"+sbID+"') as y where y.rk<=1;";

            try
            {
                if (int.Parse(sbID) > 0)
                    sql = "select * from (select rank() over(partition by T_ITEMID order by T_STARTTIME desc) rk,b.* from T_BASE_ITEM as b where b.T_DeviceID =(select T_NODEID from T_INFO_ROUTE where T_NODEKEY='" + sbID + "')) as y where y.rk<=1;";
            }
            catch
            {
                sql = "select * from (select rank() over(partition by T_ITEMID order by T_STARTTIME desc) rk,b.* from T_BASE_ITEM as b where b.T_DeviceID ='" + sbID + "') as y where y.rk<=1;";
            }

            dt = DBdb2.RunDataTable(sql, out errMsg);


            return dt;
        }

        /// <summary>
        /// 点检项分页
        /// </summary>
        /// <param name="sbID"></param>
        /// <param name="sCount"></param>
        /// <param name="eCount"></param>
        /// <returns></returns>
        public DataTable RetTabItemByDevID(string sbID, int sCount, int eCount)
        {
            //sql = "select * from (select b.*,rownumber() over(order by b.T_TIME desc ) as rowid  from ( select * from T_BASE_STATUS as s inner join (SELECT i.ID_KEY,b.T_DEVICEID,b.T_DEVICEDESC,i.T_TIME,i.I_STATUS FROM T_INFO_DEVICE as i inner join T_BASE_DEVICE as b on i.T_DEVICEID=b.T_DEVICEID where i.T_DEVICEID='" + sbID + "' order by i.T_TIME desc) as t on s.I_STATUSID=t.I_STATUS)  as b )as a where a.rowid BETWEEN " + sCount + " AND " + eCount;         

            try
            {
                //什么时候调用此方法分页 需要重新确定
                if (int.Parse(sbID) > 0)
                {     //sql ="select * from (select rank() over(partition by T_ITEMID order by T_STARTTIME desc) rk,b.* from T_BASE_ITEM as b where b.T_DeviceID =(select T_NODEID from T_INFO_ROUTE where T_NODEKEY='" + sbID + "')) as y where y.rk<=1;";
                    sql = "select * from (select z.* , rownumber() over (order by z.id_key asc ) as rowid from ";

                    sql += " (select * from (select rank() over(partition by T_ITEMID order by T_STARTTIME desc) rk,b.* from T_BASE_ITEM as b where b.T_DeviceID =(select T_NODEID from T_INFO_ROUTE where T_NODEKEY='" + sbID + "')) as y where y.rk<=1) as z)";

                    sql += " as f where f.rowid between " + sCount + " and " + eCount;
                }
            }
            catch
            {
                //sql = "select * from (select rank() over(partition by T_ITEMID order by T_STARTTIME desc) rk,b.* from T_BASE_ITEM as b where b.T_DeviceID ='" + sbID + "') as y where y.rk<=1;";
                sql = "select * from ( select c.*,rownumber() over(order by c.id_key asc ) as rowid from ";
                sql += "(select * from (select rank() over(partition by T_ITEMID order by T_STARTTIME desc) rk,b.* from T_BASE_ITEM as b where b.T_DeviceID ='" + sbID + "') as y where y.rk<=1) as c) as d ";
                sql += "where d.rowid BETWEEN " + sCount + " AND " + eCount;
            }

            dt = DBdb2.RunDataTable(sql, out errMsg);

            return dt;
        }
        public int RetTabItemByDevIDCount(string sbID)
        {
            try
            {
                if (int.Parse(sbID) > 0)
                    sql = "select * from (select rank() over(partition by T_ITEMID order by T_STARTTIME desc) rk,b.* from T_BASE_ITEM as b where b.T_DeviceID =(select T_NODEID from T_INFO_ROUTE where T_NODEKEY='10')) as y where y.rk<=1";
                //sql = "select * from (select rank() over(partition by T_ITEMID order by T_STARTTIME desc) rk,b.* from T_BASE_ITEM as b where b.T_DeviceID =(select T_NODEID from T_INFO_ROUTE where T_NODEKEY='" + sbID + "')) as y where y.rk<=1;";
            }
            catch
            {
                sql = "select * from (select rank() over(partition by T_ITEMID order by T_STARTTIME desc) rk,b.* from T_BASE_ITEM as b where b.T_DeviceID ='" + sbID + "') as y where y.rk<=1;";
            }

            int count = DBdb2.RunRowCount(sql, out errMsg);

            return count;
        }

        /// <summary>
        /// 点检项分页ALL
        /// </summary>
        /// <param name="sbID"></param>
        /// <param name="sCount"></param>
        /// <param name="eCount"></param>
        /// <returns></returns>
        public DataTable RetTabItemByDevIDAll(int sCount, int eCount)
        {
            sql = "select * from (select z.* ,rownumber () over ( order by id_key asc ) as rowid from  (select * from (select rank() over(partition by T_ITEMID order by T_STARTTIME desc) rk,b.* from T_BASE_ITEM as b ) as y where y.rk<=1) as z) as f";

            sql += " where f.rowid between " + sCount + " and " + eCount;

            dt = DBdb2.RunDataTable(sql, out errMsg);

            return dt;
        }
        public int RetTabItemByDevIDCountAll()
        {
            sql = "select * from (select rank() over(partition by T_ITEMID order by T_STARTTIME desc) rk,b.* from T_BASE_ITEM as b ) as y where y.rk<=1";

            int count = DBdb2.RunRowCount(sql, out errMsg);

            return count;
        }

        /// <summary>
        /// 获取ITEM详细信息
        /// </summary>
        /// <param name="ID_KEY">ID_KEY</param>
        /// <returns></returns>
        public DataTable RetTabItemsByIDKEY(string ID_KEY)
        {
            DataTable dt = null;
            DataRow dr = null;

            sql = "select T_ITEMID,T_DeviceID from T_BASE_ITEM where ID_KEY=" + ID_KEY;

            dr = DBdb2.RunDataRow(sql, out errMsg);

            if (dr != null)
            {
                sql = "select * from T_BASE_ITEM where T_ITEMID='" + dr["T_ITEMID"] + "' and T_DEVICEID='" + dr["T_DEVICEID"] + "' order by T_STARTTIME desc";

                dt = DBdb2.RunDataTable(sql, out errMsg);
            }

            //sql = "select * from T_BASE_ITEM where T_ITEMID in (select T_ITEMID from T_BASE_ITEM where ID_KEY="+ID_KEY+")  order by T_STARTTIME desc";

            return dt;
        }

        /// <summary>
        /// 历史详细信息分页
        /// 获取ITEM历史详细信息
        /// </summary>
        /// <param name="ID_KEY">ID_KEY</param>
        /// <returns></returns>
        public DataTable RetTabHisItemsByidKey(string ID_KEY, int sCount, int eCount)
        {
            DataTable dt = null;
            DataRow dr = null;

            sql = "select * from (select c.*,rownumber() over(order by c.t_starttime desc ) as rowid from  (select * from ADMINISTRATOR.T_BASE_ITEM where t_itemid= (SELECT t_itemid FROM ADMINISTRATOR.T_BASE_ITEM where id_key=" + ID_KEY + ")  ";

            sql += "and T_DEVICEID=(SELECT t_deviceid FROM ADMINISTRATOR.T_BASE_ITEM where id_key=" + ID_KEY + ") order by T_STARTTIME desc) as c) as d where d.rowid BETWEEN " + sCount + " AND " + eCount;

            dt = DBdb2.RunDataTable(sql, out errMsg);

            return dt;
        }

        /// <summary>
        /// 历史详细信息分页
        /// 获取ITEM历史详细信息
        /// </summary>
        /// <param name="ID_KEY">ID_KEY</param>
        /// <returns></returns>
        public int RetTabHisItemsByidKeyCount(string ID_KEY)
        {
            int count = 0;

            sql = "select * from ADMINISTRATOR.T_BASE_ITEM where t_itemid= (SELECT t_itemid FROM ADMINISTRATOR.T_BASE_ITEM where id_key=" + ID_KEY + ")  ";

            sql += "and T_DEVICEID=(SELECT t_deviceid FROM ADMINISTRATOR.T_BASE_ITEM where id_key=" + ID_KEY + ") order by T_STARTTIME desc";

            count = DBdb2.RunRowCount(sql, out errMsg);

            return count;
        }

        /// <summary>
        /// 更改设备状态调用接口
        /// </summary>
        /// <param name="sbID"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool RetUpdateDveiceStatus(string sbID, string time, out string errMsg)
        {
            bool flag = false;

            DataRow drRou = null;
            DataRow drDev = null;
            DataRow drArea = null;

            sql = "SELECT T_NODEID,T_PARAENTID FROM T_INFO_ROUTE where T_NODEID='" + sbID + "'";

            drDev = DBdb2.RunDataRow(sql, out errMsg);

            if (drDev != null)
            {
                sql = "SELECT T_NODEID,T_PARAENTID FROM T_INFO_ROUTE where T_NODEKEY='" + drDev["T_PARAENTID"] + "'";

                drArea = DBdb2.RunDataRow(sql, out errMsg);

                if (drArea != null)
                {
                    sql = "SELECT T_NODEID,T_PARAENTID FROM T_INFO_ROUTE where T_NODEKEY='" + drArea["T_PARAENTID"] + "'";
                    drRou = DBdb2.RunDataRow(sql, out errMsg);

                    if (drRou != null)
                    {
                        sql = "select * from (select rank() over(partition by T_ITEMID order by T_STARTTIME desc) rk,b.* from T_BASE_ITEM as b where b.T_DeviceID ='" + sbID + "') as y where y.rk<=1;";

                        DataTable dtItems = DBdb2.RunDataTable(sql, out errMsg);

                        //创建点检项关系 0 删除点检项关系 1 编辑点检项 2  编辑设备状态3
                        this.RetBoolDYJK(drRou["T_NODEID"].ToString(), drArea["T_NODEID"].ToString(), drDev["T_NODEID"].ToString(), "3", time, dtItems, out errMsg);
                    }
                    else
                    { errMsg = "没有找到线路ID"; }

                }
                else { errMsg = "没有找到区域ID"; }
            }

            return flag;
        }

        /// <summary>
        /// 调用接口
        /// </summary>
        /// <param name="lineID"></param>
        /// <param name="areaID"></param>
        /// <param name="sbID"></param>
        /// <param name="dtItems"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private bool RetBoolDYJK(string lineID, string areaID, string sbID, string jud, string sTime, DataTable dtItems, out string errMsg)
        {
            errMsg = "";

            bool flag = false;

            if (dtItems != null && dtItems.Rows.Count > 0)
            {
                string[] route = new string[dtItems.Rows.Count];
                string[] area = new string[dtItems.Rows.Count];
                string[] device = new string[dtItems.Rows.Count];
                string[] item = new string[dtItems.Rows.Count];
                string[] judge = new string[dtItems.Rows.Count]; //操作类型：创建点检项关系 删除点检项关系 编辑点检项  编辑设备状态
                string[] sTimes = new string[dtItems.Rows.Count];
                string[] type = new string[dtItems.Rows.Count];
                string[] count1 = new string[dtItems.Rows.Count];
                string[] devState = new string[dtItems.Rows.Count];
                string[] state = new string[dtItems.Rows.Count];

                for (int i = 0; i < dtItems.Rows.Count; i++)
                {
                    route[i] = lineID;
                    area[i] = areaID;
                    device[i] = sbID;
                    item[i] = dtItems.Rows[i]["T_ITEMID"].ToString();
                    judge[i] = jud; //创建点检项关系 0 删除点检项关系 1 编辑点检项 2  编辑设备状态3
                    sTimes[i] = sTime;// dtItems.Rows[i]["T_STARTTIME"].ToString();
                    type[i] = dtItems.Rows[i]["T_PERIODTYPE"].ToString();
                    count1[i] = dtItems.Rows[i]["T_PERIODVALUE"].ToString();
                    devState[i] = dtItems.Rows[i]["I_STATUS"].ToString();
                    state[i] = dtItems.Rows[i]["T_STATUS"].ToString();
                }

                flag = qc.ManageQuestcomplete(route, area, device, item, judge, sTimes, type, count1, devState, state);

            }
            else
            { errMsg = "此设备下没有点检项信息!"; }

            return flag;
        }

        /// <summary>
        /// 根据ID
        /// </summary>
        /// <param name="stID"></param>
        /// <returns></returns>
        public DataRow RetDrByStatusID(string stID)
        {
            sql = "select * from T_BASE_STATUS where I_STATUSID =" + stID;

            return DBdb2.RunDataRow(sql, out errMsg);
        }

        /// <summary>
        /// 添加点检项
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

            errMsg = "";

            sql = "select T_ITEMID from T_BASE_ITEM where T_DEVICEID='" + nodeKey + "' order by ID_KEY DESC fetch first 1 rows only ";

            obj = DBdb2.GetSingle(sql);

            if (obj != null && obj.ToString() != "")
                count = int.Parse(obj.ToString()) + 1;
            else
                count = 1;

            sql = "insert into T_BASE_ITEM ";
            sql += "(T_ITEMID,T_ITEMPOSITION,T_ITEMDESC,T_CONTENT,T_TYPE,I_STATUS,T_OBSERVE,T_UNIT,F_LOWER,F_UPPER,I_SPECTRUM,T_DEVICEID,T_STARTTIME,T_PERIODTYPE,T_PERIODVALUE,T_STATUS)";
            sql += " values ";
            sql += "('" + count + "','" + itemBw + "','" + itemDesc + "','" + itemContent + "','" + itemType + "'," + itemStatus + ",'" + itemObserve + "','" + itemUnit + "'," + double.Parse(itemLower) + "," + double.Parse(itemUpper) + "," + int.Parse(itemSpectrum) + ",'" + nodeKey + "','" + DateTime.Parse(itemStartTime).ToString("yyyy-MM-dd 0:00:00") + "'," + int.Parse(itemPerType) + "," + int.Parse(itemPerValue) + "," + int.Parse(itemStatusQJ) + ")";

            flag = DBdb2.RunNonQuery(sql, out errMsg);

            if (flag != true)
                errMsg = "新建点检项失败!";

            return flag;
        }

        /// <summary>
        /// 编辑点检项
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
        public bool EditItem(string idkey, string itemBw, string itemDesc, string itemContent, string itemObserve, string itemUnit, string itemType, string itemStatus, string itemStatusQJ, string itemUpper, string itemLower, string itemSpectrum, string itemStartTime, string itemPerValue, string itemPerType, out string errMsg)
        {
            object obj = null;
            bool flag = false;
            int count = 0;

            //sql = "insert into T_BASE_ITEM ";
            //sql += "(T_ITEMID,T_ITEMPOSITION,T_ITEMDESC,T_CONTENT,T_TYPE,I_STATUS,T_OBSERVE,T_UNIT,F_LOWER,F_UPPER,I_SPECTRUM,T_DEVICEID,T_STARTTIME,T_PERIODTYPE,T_PERIODVALUE,T_STATUS)";
            //sql += " values ";
            //sql += "('" + count + "','" + itemBw + "','" + itemDesc + "','" + itemContent + "','" + itemType + "'," + itemStatus + ",'" + itemObserve + "','" + itemUnit + "'," + double.Parse(itemLower) + "," + double.Parse(itemUpper) + "," + int.Parse(itemSpectrum) + ",'" + nodeKey + "','" + DateTime.Parse(itemStartTime).ToString("yyyy-MM-dd 0:00:00") + "'," + int.Parse(itemPerType) + "," + int.Parse(itemPerValue) + "," + int.Parse(itemStatusQJ) + ")";

            sql += "update T_BASE_ITEM set ";
            sql += "T_ITEMPOSITION='" + itemBw + "',T_ITEMDESC='" + itemDesc + "',T_CONTENT='" + itemContent + "',T_TYPE='" + itemType + "',I_STATUS=" + itemStatus + ",T_OBSERVE='" + itemObserve + "',";
            sql += "T_UNIT='" + itemUnit + "',F_LOWER=" + double.Parse(itemLower) + ",F_UPPER= " + double.Parse(itemUpper) + ",I_SPECTRUM=" + int.Parse(itemSpectrum) + ",T_STARTTIME='" + DateTime.Parse(itemStartTime).ToString("yyyy-MM-dd 0:00:00") + "',";
            sql += "T_PERIODTYPE=" + int.Parse(itemPerType) + ",T_PERIODVALUE=" + int.Parse(itemPerValue) + ",T_STATUS=" + int.Parse(itemStatusQJ) + " where id_key=" + idkey;

            flag = DBdb2.RunNonQuery(sql, out errMsg);

            if (flag != true)
                errMsg = "编辑点检项失败!";

            return flag;
        }


        /// <summary>
        /// 获取某个设备下点检项所有ID_KEY
        /// </summary>
        /// <param name="sbID"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public string RetStrItemsBySbID(string sbID, out string errMsg)
        {
            errMsg = "";
            string str = "";
            string strName = "";
            string strDevName = "";
            DataTable dt = null;

            //sql = "select * from (select rank() over(partition by T_ITEMID order by T_STARTTIME desc) rk,b.* from T_BASE_ITEM as b where b.T_DeviceID ='"+sbID+"') as y where y.rk<=1;";

            //try
            //{
            //    if (int.Parse(sbID) > 0)
            //        sql = "select * from (select rank() over(partition by T_ITEMID order by T_STARTTIME desc) rk,b.* from T_BASE_ITEM as b where b.T_DeviceID =(select T_NODEID from T_INFO_ROUTE where T_NODEKEY='" + sbID + "')) as y where y.rk<=1;";
            //}
            //catch
            //{
            //    sql = "select * from (select rank() over(partition by T_ITEMID order by T_STARTTIME desc) rk,b.* from T_BASE_ITEM as b where b.T_DeviceID ='" + sbID + "') as y where y.rk<=1;";
            //}

            sql = "select * from  (select rank() over( partition by T_ITEMID order by T_STARTTIME desc ) rk,b.* from (SELECT * FROM T_INFO_ROUTE as i INNER JOIN T_BASE_ITEM as b ON b.T_ITEMID =i.T_NODEID where i.T_DESC ='DJX' and i.T_PARAENTID='" + sbID + "' ) as b) as y where y.rk<=1;";

            dt = DBdb2.RunDataTable(sql, out errMsg);

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str += dt.Rows[i]["ID_KEY1"].ToString() + ',';
                    strName += dt.Rows[i]["T_ITEMDESC"].ToString() + ',';
                }
            }

            return str + ";" + strName;
        }

        /// <summary>
        /// 获取设备名称 
        /// </summary>
        /// <param name="nodeKey"></param>
        /// <returns></returns>
        public string RetSBNameBySbNodeKey(string nodeKey)
        {
            string str = "";

            sql = "select * from T_INFO_ROUTE as r inner join T_BASE_DEVICE as d on r.T_NODEID=d.T_DEVICEID where T_NODEKEY='" + nodeKey + "'";

            DataRow dr = DBdb2.RunDataRow(sql, out errMsg);
            if (dr != null)
                str = dr["T_DEVICEDESC"].ToString();


            return str;

        }

        /// <summary>
        /// 获取设备名称 
        /// </summary>
        /// <param name="nodeKey"></param>
        /// <returns></returns>
        public DataRow RetSBNameBySbNodeKey1(string nodeKey)
        {
            string str = "";

            sql = "select * from T_INFO_ROUTE as r inner join T_BASE_DEVICE as d on r.T_NODEID=d.T_DEVICEID where T_NODEKEY='" + nodeKey + "'";

            DataRow dr = DBdb2.RunDataRow(sql, out errMsg);


            return dr;

        }

        /// <summary>
        /// 获取点检项信息
        /// </summary>
        /// <param name="nodeKey"></param>
        /// <returns></returns>
        public DataRow RetItemInfoByNodeKey(string nodeKey)
        {
            sql = "select * from  (select rank() over( partition by T_ITEMID order by T_STARTTIME desc ) rk,b.* from ";

            sql += "(SELECT * FROM T_INFO_ROUTE as i INNER JOIN T_BASE_ITEM as b ON b.T_ITEMID =i.T_NODEID where i.T_DESC ='DJX' and i.T_NODEKEY='" + nodeKey + "' ) as b) as y where y.rk<=1";

            return DBdb2.RunDataRow(sql, out errMsg);
        }

        /// <summary>
        /// 编辑设备状态信息
        /// </summary>
        /// <param name="nodeKey"></param>
        /// <returns></returns>
        public bool EditStatusByIDkey(string idkey, string time, string statusid, out string errMsg)
        {
            bool flag = false;

            sql = "update T_INFO_DEVICE set T_time='" + DateTime.Parse(time).ToString("yyyy-MM-dd 00:00:00") + "', i_status=" + statusid + " where ID_KEY=" + idkey;

            flag = DBdb2.RunNonQuery(sql, out errMsg);

            if (flag == false)
                errMsg = "修改数据出错!";

            return flag;
        }
        #region 胡进财
        #region 设备数据分页
        /// <summary>
        /// 获取设备数量
        /// </summary>
        /// <param name="id">设备编号</param>
        /// <returns></returns>
        public int GetDeviceCount(string id)
        {
            sql = "select count(*) from (select r.ID,r.DeviceID,r.DeviceName,r.T_PARENTID,r.T_SWERK,r.T_IWERK,r.T_KOSTL,r.T_SCBS,rownumber() over(order by ID asc ) as rowid  from (select r.ID_KEY ID,r.T_DEVICEID DeviceID,r.T_DEVICEDESC DeviceName,r.T_PARENTID,r.T_SWERK,r.T_IWERK,r.T_KOSTL,r.T_SCBS from T_BASE_DEVICE r inner join (select T_NODEID from T_INFO_ROUTE where T_PARAENTID='" + id + "') rp on r.T_DEVICEID=rp.T_NODEID) r)as a;";
            return DBdb2.RunRowCount(sql, out errMsg);
        }

        /// <summary>
        /// 获取设备数据集
        /// </summary>
        /// <param name="id">设备编号</param>
        /// <param name="sCount">开始条数</param>
        /// <param name="eCount">结束条数</param>
        /// <returns></returns>
        public DataTable GetDeviceDt(string id, int sCount, int eCount)
        {
            sql = "select * from (select r.ID,r.DeviceID,r.DeviceName,r.T_PARENTID,r.T_SWERK,r.T_IWERK,r.T_KOSTL,r.T_SCBS,rownumber() over(order by ID asc ) as rowid  from (select r.ID_KEY ID,r.T_DEVICEID DeviceID,r.T_DEVICEDESC DeviceName,r.T_PARENTID,r.T_SWERK,r.T_IWERK,r.T_KOSTL,r.T_SCBS from T_BASE_DEVICE r inner join (select T_NODEID from T_INFO_ROUTE where T_PARAENTID='" + id + "') rp on r.T_DEVICEID=rp.T_NODEID) r)as a where a.rowid between " + sCount + " and " + eCount + ";";
            dt = DBdb2.RunDataTable(sql, out errMsg);
            return dt;
        }
        #endregion

        #region 点检项数据分页
        /// <summary>
        /// 获取点检项数量
        /// </summary>
        /// <param name="id">设备编号</param>
        /// <returns></returns>
        public int GetItemCount(string id)
        {
            sql = "select count(*) from (select r.ID,r.T_ITEMID,r.T_ITEMPOSITION,r.T_ITEMDESC,r.T_CONTENT,r.T_TYPE,r.T_OBSERVE,r.T_UNIT,r.F_LOWER,r.F_UPPER,r.T_STARTTIME,r.T_PERIODVALUE,r.T_PERIODVALUE,r.T_STATUS,r.T_PERIODTYPE,rownumber() over(order by ID asc ) as rowid  from (select r.ID_KEY ID,r.T_ITEMID,r.T_ITEMPOSITION,r.T_ITEMDESC,r.T_CONTENT,r.T_TYPE,r.T_OBSERVE,r.T_UNIT,r.F_LOWER,r.F_UPPER,r.T_STARTTIME,r.T_PERIODVALUE,case when r.T_STATUS=0 then '停机检测' when r.T_STATUS=1 then '起机检测' end T_STATUS,case when r.T_PERIODTYPE=1 then '日'  when r.T_PERIODTYPE=2 then '月'  when r.T_PERIODTYPE=3 then '年'  end T_PERIODTYPE from T_BASE_ITEM r inner join (select T_NODEID from T_INFO_ROUTE where T_PARAENTID='" + id + "') rp on r.T_ITEMID=rp.T_NODEID) r)as a;";
            return DBdb2.RunRowCount(sql, out errMsg);
        }

        /// <summary>
        /// 获取点检项数据集
        /// </summary>
        /// <param name="id">设备编号</param>
        /// <param name="sCount">开始条数</param>
        /// <param name="eCount">结束条数</param>
        /// <returns></returns>
        public DataTable GetItemDt(string id, int sCount, int eCount)
        {
            sql = "select * from (select r.ID,r.T_ITEMID,r.T_ITEMPOSITION,r.T_ITEMDESC,r.T_CONTENT,r.T_TYPE,r.T_OBSERVE,r.T_UNIT,r.F_LOWER,r.F_UPPER,r.T_STARTTIME,r.T_PERIODVALUE,r.T_PERIODVALUE,r.T_STATUS,r.T_PERIODTYPE,rownumber() over(order by ID asc ) as rowid  from (select r.ID_KEY ID,r.T_ITEMID,r.T_ITEMPOSITION,r.T_ITEMDESC,r.T_CONTENT,r.T_TYPE,r.T_OBSERVE,r.T_UNIT,r.F_LOWER,r.F_UPPER,r.T_STARTTIME,r.T_PERIODVALUE,case when r.T_STATUS=0 then '停机检测' when r.T_STATUS=1 then '起机检测' end T_STATUS,case when r.T_PERIODTYPE=1 then '日'  when r.T_PERIODTYPE=2 then '月'  when r.T_PERIODTYPE=3 then '年'  end T_PERIODTYPE from T_BASE_ITEM r inner join (select T_NODEID from T_INFO_ROUTE where T_PARAENTID='" + id + "') rp on r.T_ITEMID=rp.T_NODEID) r)as a where a.rowid between " + sCount + " and " + eCount + ";";
            dt = DBdb2.RunDataTable(sql, out errMsg);
            return dt;
        }
        #endregion
        #endregion
    }
}
