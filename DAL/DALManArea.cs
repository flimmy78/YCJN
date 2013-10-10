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
    /// 区域表方法
    /// </summary>
    public class DALManArea
    {
        string sql = "";
        string errMsg = "";

        DataTable dt = null;
        IList<Hashtable> list = null;
        DateHelper dh = new DateHelper();

        /// <summary>
        /// 获取区域表信息
        /// </summary>
        /// <returns></returns>
        public DataTable RetTable()
        {
            sql = "SELECT a.T_AREAID,a.T_AREANAME,r.T_ROUTENAME FROM T_BASE_AREA as a left join T_BASE_ROUTE as r on a.T_ROUTEID =r.T_ROUTEID order by  a.T_ROUTEID";

            dt = DBdb2.RunDataTable(sql, out errMsg);

            return dt;
        }

        /// <summary>
        /// 区域数据分页查询
        /// </summary>
        /// <param name="sCount"></param>
        /// <param name="eCount"></param>
        /// <returns></returns>
        public DataTable RetTabAreas(int sCount, int eCount)
        {
            sql = "select * from (select b.* ,rownumber() over(order by b.id_key asc ) as rowid from (SELECT * FROM ADMINISTRATOR.T_BASE_AREA) as b) as c where c.rowid between " + sCount + " and " + eCount;

            dt = DBdb2.RunDataTable(sql, out errMsg);

            return dt;
        }

        public int RetTabAreasCounts()
        {
            int count = 0;

            sql = "SELECT count(*) FROM ADMINISTRATOR.T_BASE_AREA";

            count = DBdb2.RunRowCount(sql, out errMsg);

            return count;
        }

        #region 胡进财
        /// <summary>
        /// 获取某个线路下面的区域
        /// </summary>
        /// <param name="routeID">线路编号</param>
        /// <returns></returns>
        public IList<Hashtable> GetAreas(string routeID)
        {
            sql = "select T_AREAID,T_AREANAME from T_BASE_AREA where T_AREAID in( select T_NODEID from T_INFO_ROUTE where T_PARAENTID in(select T_NODEKEY from T_INFO_ROUTE where T_NODEID='" + routeID + "'))";
            dt = DBdb2.RunDataTable(sql, out errMsg);
            list = dh.DataTableToList(dt);
            return list;
        }

        /// <summary>
        /// 获取设备信息  某个区域下面
        /// </summary>
        /// <param name="AreaID">区域编号</param>
        /// <returns></returns>
        public IList<Hashtable> GetDevices(string AreaID)
        {
            sql = "select T_DEVICEID,T_DEVICEDESC from T_BASE_DEVICE where T_DEVICEID in( select T_NODEID from T_INFO_ROUTE where T_PARAENTID in(select T_NODEKEY from T_INFO_ROUTE where T_NODEID='" + AreaID + "'))";
            dt = DBdb2.RunDataTable(sql, out errMsg);
            list = dh.DataTableToList(dt);
            return list;
        }

        #region 区域数据分页
        /// <summary>
        /// 获取区域数量
        /// </summary>
        /// <param name="id">区域编号</param>
        /// <returns></returns>
        public int GetAreaCount(string id)
        {
            sql = "select count(*) from (select r.ID,r.AreaID,r.AreaName,r.AreaCD,rownumber() over(order by ID asc ) as rowid  from (select r.ID_KEY ID,r.T_AREAID AreaID,r.T_AREANAME AreaName,r.T_AREACD AreaCD from T_BASE_AREA r inner join (select T_NODEID from T_INFO_ROUTE where T_PARAENTID='" + id + "') rp on r.T_AREAID=rp.T_NODEID) r)as a;";
            return DBdb2.RunRowCount(sql, out errMsg);
        }

        /// <summary>
        /// 获取区域数据集
        /// </summary>
        /// <param name="id">区域编号</param>
        /// <param name="sCount">开始条数</param>
        /// <param name="eCount">结束条数</param>
        /// <returns></returns>
        public DataTable GetAreaDt(string id, int sCount, int eCount)
        {
            sql = "select * from (select r.ID,r.AreaID,r.AreaName,r.AreaCD,rownumber() over(order by ID asc ) as rowid  from (select r.ID_KEY ID,r.T_AREAID AreaID,r.T_AREANAME AreaName,r.T_AREACD AreaCD from T_BASE_AREA r inner join (select T_NODEID from T_INFO_ROUTE where T_PARAENTID='" + id + "') rp on r.T_AREAID=rp.T_NODEID) r)as a where a.rowid between " + sCount + " and " + eCount + ";";
            dt = DBdb2.RunDataTable(sql, out errMsg);
            return dt;
        }
        #endregion
        #endregion

    }
}
