using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.Helper;
using System.Collections;
using System.Data;
using DAL;

namespace BLL
{
    /// <summary>
    /// 区域表方法
    /// </summary>
    public class BLLManArea
    {
        DALManArea dmr = new DALManArea();

        /// <summary>
        /// 获取区域表信息
        /// </summary>
        /// <returns></returns>
        public DataTable RetTable()
        {
            return dmr.RetTable();
        }

        /// <summary>
        /// 区域数据分页查询
        /// </summary>
        /// <param name="sCount"></param>
        /// <param name="eCount"></param>
        /// <returns></returns>
        public DataTable RetTabAreas(int sCount, int eCount)
        {
            return dmr.RetTabAreas(sCount, eCount);
        }

        public int RetTabAreasCounts()
        {
            return dmr.RetTabAreasCounts();
        }


        #region 胡进财
        /// <summary>
        /// 获取某个线路下面的区域
        /// </summary>
        /// <param name="routeID">线路编号</param>
        /// <returns></returns>
        public IList<Hashtable> GetAreas(string routeID)
        {
            return dmr.GetAreas(routeID);
        }

        /// <summary>
        /// 获取设备信息  某个区域下面
        /// </summary>
        /// <param name="AreaID">区域编号</param>
        /// <returns></returns>
        public IList<Hashtable> GetDevices(string AreaID)
        {
            return dmr.GetDevices(AreaID);
        }

        /// <summary>
        /// 获取区域数量
        /// </summary>
        /// <param name="id">区域编号</param>
        /// <returns></returns>
        public int GetAreaCount(string id)
        {
            return dmr.GetAreaCount(id);
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
            return dmr.GetAreaDt(id, sCount, eCount);
        }
        #endregion
    }
}
