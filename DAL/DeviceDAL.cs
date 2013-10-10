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
    public class DeviceDAL
    {
        string sql = "";
        string errMsg = "";

        DataTable dt = null;

        /// <summary>
        /// 获取根节点 
        /// </summary>
        /// <returns></returns>
        public DataTable GetRootDevice()
        {
            sql = "SELECT * FROM T_BASE_DEVICE WHERE T_PARENTID='0'";
            return DBdb2.RunDataTable(sql, out errMsg);
        }

        public DataTable GetNotRootDevice()
        {
            sql = "SELECT * FROM T_BASE_DEVICE WHERE T_PARENTID<>'0'";
            return DBdb2.RunDataTable(sql, out errMsg);
        }

        public DataTable GetItems()
        {
            sql = "SELECT * FROM T_INFO_ROUTE WHERE T_PARAENTID = (SELECT r.T_NODEKEY FROM T_BASE_DEVICE as d INNER JOIN T_INFO_ROUTE as r ON d.T_DEVICEID =r.T_NODEID)";

            return DBdb2.RunDataTable(sql, out errMsg);
        }

        public DataTable GetBaseItem(string itemId)
        {
            sql = "SELECT * FROM T_BASE_ITEM WHERE T_ITEMID='" + itemId + "' AND I_STATUS=0";
            return DBdb2.RunDataTable(sql, out errMsg);
        }

        public DataTable GetDeviceFile(string id)
        {
            sql = "SELECT * FROM T_BASE_DEVICE WHERE T_DEVICEID='" + id + "'";
            return DBdb2.RunDataTable(sql, out errMsg);
        }


        /// <summary>
        /// 获得线路和关系配置表之间记录
        /// written by shan
        /// </summary>
        /// <returns></returns>
        public DataTable GetTableRoute(out string errMsg)
        {
            sql = "SELECT * FROM T_BASE_DEVICE as i INNER JOIN T_BASE_ROUTE as b ON b.T_ROUTEID =i.T_NODEID";

            return DBdb2.RunDataTable(sql, out errMsg);
        }

        /// <summary>
        /// 判断子节点是否存在
        /// </summary>
        /// <returns>1:总线路,2:线路</returns>
        public int RetCount(string parId)
        {
            int count = 0;
            int countBase = 0;
            int countRoute = 0;

            sql = "select count(*) from T_BASE_DEVICE where T_DEVICEID='" + parId + "'";

            countRoute = DBdb2.RunRowCount(sql, out errMsg);

            //sql = "select * from T_BASE_ROUTE as b inner join T_BASE_DEVICE as r on r.T_NODEID=b.T_ROUTEID  where T_NODEKEY='" + nodeID + "'";

            //countBase = DBdb2.RunRowCount(sql, out errMsg);

            if (countRoute != 0)
            {
                if (countRoute > 0)
                    count = 2;
            }
            else
            { count = 1; }


            sql = "select count(*) from T_BASE_ITEM where ID_KEY=" + parId + "";

            countRoute = DBdb2.RunRowCount(sql, out errMsg);


            if (countRoute != 0)
            {
                if (countRoute > 0)
                    count = 3;
            }

            return count;
        }

        /// <summary>
        /// 新建线路
        /// </summary>
        /// <param name="lID">线路ID</param>
        /// <param name="lName">线路名称</param>
        /// <param name="lType">线路类型</param>
        /// <param name="lGw">线路岗位</param>
        /// <param name="lPID">线路父ID</param>
        public bool AddLineInfo(string lID, string lName, string lType, string lGw, string lPID, out string errMsg)
        {
            int count = 1;
            object obj = null;
            bool flag = false;

            //判断线路ID是否已被加入

            sql = "select * from T_BASE_ROUTE where T_ROUTEID='" + lID + "'";

            DataRow drBaseRoute = DBdb2.RunDataRow(sql, out errMsg);

            if (drBaseRoute != null)
            { errMsg = "此线路ID已被添加"; }
            else
            {

                sql = "select count(*) from T_BASE_DEVICE";

                obj = DBdb2.GetSingle(sql);

                if (obj != null && obj.ToString() != "")
                    count = int.Parse(obj.ToString()) + 1;

                sql = "insert into T_BASE_ROUTE (T_ROUTEID,T_ROUTENAME,T_ORGID,I_TYPE) values ('" + lID + "','" + lName + "','" + lGw + "'," + lType + ");";
                sql += "insert into T_BASE_DEVICE (T_NODEID,T_NODEKEY,T_PARAENTID) values ('" + lID + "','" + count.ToString() + "','" + lPID + "')";

                flag = DBdb2.RunNonQuery(sql, out errMsg);
            }

            return flag;
        }

        public string EditFile(string filePath, string deviceId)
        {
            sql = "UPDATE T_BASE_DEVICE SET B_ATTACHMENT='" + filePath + "' WHERE T_DEVICEID='" + deviceId + "'";
            DBdb2.RunNonQuery(sql, out errMsg);
            return errMsg;
        }

        /// <summary>
        /// 根据节点取得线路信息
        /// 只有count=2的时候才调用此方法
        /// count=2 代表点击的时线路
        /// </summary>
        /// <returns></returns>
        public DataRow ReadDeviceInfo(string deviceID)
        {
            DataRow dr = null;



            sql = "select * from T_BASE_DEVICE  where T_DEVICEID='" + deviceID + "'";

            dr = DBdb2.RunDataRow(sql, out errMsg);

            //if (dr != null)
            //{
            //    sql = "SELECT * FROM T_BASE_ROUTE as r inner join T_SYS_ORGANIZE as o on r.T_ORGID=o.T_ORGID where  r.T_ORGID='" + dr["T_ORGID"] + "' and r.T_ROUTEID='" + dr["T_ROUTEID"] + "'";

            //    dr = DBdb2.RunDataRow(sql, out errMsg);
            //}

            return dr;
        }

        public DataRow ReadItem(string id_key)
        {
            DataRow dr = null;



            sql = "select * from T_BASE_ITEM  where ID_KEY=" + id_key + "";

            dr = DBdb2.RunDataRow(sql, out errMsg);

            //if (dr != null)
            //{
            //    sql = "SELECT * FROM T_BASE_ROUTE as r inner join T_SYS_ORGANIZE as o on r.T_ORGID=o.T_ORGID where  r.T_ORGID='" + dr["T_ORGID"] + "' and r.T_ROUTEID='" + dr["T_ROUTEID"] + "'";

            //    dr = DBdb2.RunDataRow(sql, out errMsg);
            //}

            return dr;
        }

        public DataTable ReadItemByIdKey(string id_key)
        {

            DataTable dt = null;


            //IList<Hashtable> Ilist = new List<Hashtable>();
            sql = "select ID_KEY,T_ITEMID,T_ITEMPOSITION, T_ITEMDESC,T_CONTENT,T_TYPE,I_STATUS,T_OBSERVE,T_UNIT,F_LOWER,F_UPPER,F_UPPER,I_SPECTRUM,T_DEVICEID,to_char(T_STARTTIME,'yyyy-mm-dd hh24:mi:ss') as T_STARTTIME,T_PERIODTYPE,T_PERIODVALUE,T_STATUS from T_BASE_ITEM  where ID_KEY=" + id_key + "";
            dt = DBdb2.RunDataTable(sql, out errMsg);
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        Hashtable ht = new Hashtable();

            //        foreach (DataColumn col in dt.Columns)
            //        {
            //            ht.Add(col.ColumnName, dr[col.ColumnNameT_STATUS
            //            Ilist.Add(ht);
            //        }

            //    }
            //}

            return dt;
        }

        public IList<Hashtable> ReadItmes(string deviceId)
        {

            DataTable dt = null;


            IList<Hashtable> Ilist = new List<Hashtable>();
            sql = "select * from T_BASE_ITEM  where T_DEVICEID='" + deviceId + "'";
            dt = DBdb2.RunDataTable(sql, out errMsg);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Hashtable ht = new Hashtable();

                    foreach (DataColumn col in dt.Columns)
                    {
                        ht.Add(col.ColumnName, dr[col.ColumnName]);
                        Ilist.Add(ht);
                    }

                }
            }

            return Ilist;
        }

        //----------------设备维护--------------

        /// <summary>
        /// 获取所有设备表
        /// </summary>
        /// <returns></returns>
        public DataTable RetTableDevice() 
        {
            sql = "select * from T_BASE_DEVICE";

            return DBdb2.RunDataTable(sql, out errMsg);
        }

    }
}
