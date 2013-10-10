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
    public class BLLManRoute
    {
        string errMsg = "";

        DateHelper pb = new DateHelper();
        DALManRoute dmr = new DALManRoute();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetTableRoute(out string errMsg)
        {
            return dmr.GetTableRoute(out errMsg);
        }

        /// <summary>
        /// 获取区域和关系关联记录
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetTableArea(out string errMsg)
        {
            return dmr.GetTableArea(out errMsg);
        }

        /// <summary>
        /// 获取总线路名称和ID 
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetTotalLineNameAndID()
        {
            return dmr.GetTotalLineNameAndID();
        }

        /// <summary>
        /// 判断子节点
        /// </summary>
        /// <param name="nodeID"></param>
        /// <returns>1:总线路,2:线路,3:区域,4:设备,5:点检项</returns>
        public int RetCount(string nodeID)
        {
            return dmr.RetCount(nodeID);
        }

        /// <summary>
        /// 新建线路
        /// </summary>     
        /// <param name="lName">线路名称</param>
        /// <param name="lType">线路类型</param>
        /// <param name="lGw">线路岗位</param>
        /// <param name="lPID">线路父ID</param>
        public bool AddLineInfo(string lName, string lType, string lGw, string lPID, out string errMsg)
        { return dmr.AddLineInfo(lName, lType, lGw, lPID, out errMsg); }

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
        { return dmr.EditLineInfo(lID, lName, lType, lGw, lPID, out errMsg); }

        /// <summary>
        /// 根据节点取得线路信息
        /// 只有count=2的时候才调用此方法
        /// count=2 代表点击的时线路
        /// </summary>
        /// <returns></returns>
        public DataRow RetDataRowByNodeID(string nodeID)
        {
            return dmr.RetDataRowByNodeID(nodeID);
        }

        /// <summary>
        /// 获取区域和线路关系
        /// </summary>
        /// <param name="lineNodeKey"></param>
        /// <returns></returns>
        public IList<Hashtable> RetListLineAndAreaRealtion(string lineNodeKey)
        {
            return pb.DataTableToList(dmr.RetTableLineAndAreaRelation(lineNodeKey));
        }

        /// <summary>
        /// 获取区域信息
        /// </summary>
        /// <returns></returns>
        public IList<Hashtable> RetListAraeInfo()
        {
            return pb.DataTableToList(dmr.RetTableArea());
        }

        /// <summary>
        /// 添加线路区域关联信息
        /// </summary>
        /// <param name="nodeKey"></param>
        /// <param name="oArea">删除</param>
        /// <param name="iArea">添加</param>
        /// <returns></returns>
        public bool AddLineAndAreaRealtion(string nodeKey, string[] oArea, string[] iArea,string type, out string errMsg)
        {
            return dmr.AddLineAndAreaRealtion(nodeKey, oArea, iArea,type, out errMsg);
        }

        /// <summary>
        /// 根据区域ID获取此区域下的所有设备信息
        /// </summary>
        /// <param name="AreaNodeKey"></param>
        /// <returns></returns>
        public DataTable RetDataTableDeviceByNodeID(string AreaNodeKey)
        {
            return dmr.RetDataTableDeviceByNodeID(AreaNodeKey);
        }

        /// <summary>
        /// 根据节点取得区域信息
        /// 只有count=3的时候才调用此方法
        /// count=3 代表点击的时区域
        /// </summary>
        /// <returns></returns>
        public DataRow RetDataRowAreaByNodeID(string AreaNodeKey)
        {
            return dmr.RetDataRowAreaByNodeID(AreaNodeKey);
        }

        public DataRow RetDataRowLineByAreaNodeKey(string AreaNodeKey)
        {
            return dmr.RetDataRowLineByAreaNodeKey(AreaNodeKey);
        }

        /// <summary>
        /// 获取设备表
        /// </summary>
        /// <returns></returns>
        public DataTable RetTableDevice()
        {
            return dmr.RetTableDevice();
        }

        /// <summary>
        /// 设备表判断是否有字节点
        /// </summary>
        /// <param name="dID0"></param>
        /// <returns></returns>
        public bool RetGetNodeBy(string dID)
        {
            return dmr.RetGetNodeBy(dID);
        }

        /// <summary>
        /// 获取设备和区域关联记录
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetTableAreaAndDevInfo(out string errMsg)
        {

            return dmr.GetTableAreaAndDevInfo(out errMsg);
        }

        /// <summary>
        /// 根据区域NodeKey获取该区域下的所有设备信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetTableDevByNodeKey(string nodeKey)
        {
            return dmr.GetTableDevByNodeKey(nodeKey);
        }

        /// <summary>
        /// 获取设备和区域关联记录
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetTableDevAndItemInfo(out string errMsg)
        {
            return dmr.GetTableDevAndItemInfo(out errMsg);
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
            return dmr.DelArea(areaID, nodeKey, out errMsg);
        }

        /// <summary>
        /// 新建点检项目
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
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool AddItem(string nodeKey, string itemBw, string itemDesc, string itemContent, string itemObserve, string itemUnit, string itemType, string itemStatus, string itemStatusQJ, string itemUpper, string itemLower, string itemSpectrum, string itemStartTime, string itemPerValue, string itemPerType, out string errMsg)
        {
            return dmr.AddItem(nodeKey, itemBw, itemDesc, itemContent, itemObserve, itemUnit, itemType, itemStatus, itemStatusQJ, itemUpper, itemLower, itemSpectrum, itemStartTime, itemPerValue, itemPerType, out  errMsg);
        }

        /// <summary>
        /// 获取点检项信息
        /// </summary>
        /// <returns></returns>
        public IList<Hashtable> RetListItemInfo(string sbID, out string errMsg)
        {
            return pb.DataTableToList(dmr.RetTableItemByNodeKey(sbID, out errMsg));
        }

        /// <summary>
        /// 添加，编辑设备和点检项关联信息
        /// </summary>
        /// <param name="nodeKey"></param>
        /// <param name="oArea"></param>
        /// <param name="iArea"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool AddDeviceAndItemRealtion(string nodeKey, string[] oArea, string[] iArea, out string errMsg)
        {
            return dmr.AddDeviceAndItemRealtion(nodeKey, oArea, iArea, out errMsg);
        }

        public string RetStatusBySID(string sid)
        {
            return dmr.RetStatusBySID(sid);
        }


        ///// <summary>
        ///// 获取某个人员的所有线路
        ///// </summary>
        ///// <param name="userID">人员编号</param>
        ///// <returns></returns>
        //public IList<Hashtable> GetRouteTree(string userID)
        //{
        //    return dmr.GetRouteTree(userID);
        //}

        ///// <summary>
        ///// 获取线路Tree
        ///// </summary>
        ///// <returns></returns>
        //public DataTable GetRouteTree()
        //{
        //    return dmr.GetRouteTree();
        
        //}
        //*************************//

        /// <summary>
        /// 获取设备数量
        /// </summary>
        /// <param name="id">区域编号</param>
        /// <returns></returns>
        public int RetSBCount(string id)
        {
            return dmr.RetSBCount(id);
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
            return dmr.RetTabSB(id, sCount, eCount);
        }

        /// <summary>
        /// 获取线路数量
        /// </summary>
        /// <param name="id">线路编号</param>
        /// <returns></returns>
        public int RetRouteCount(string id)
        {
            return dmr.RetRouteCount(id);
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
            return dmr.RetTabRoute(id, sCount, eCount);
        }

        /// <summary>
        /// 获取区域数量
        /// </summary>
        /// <param name="id">区域编号</param>
        /// <returns></returns>
        public int RetAreaCount(string id)
        {
            return dmr.RetAreaCount(id);
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
            return dmr.RetTabArea(id, sCount, eCount);
        }
        
        #region 胡进财

        /// <summary>
        /// 获取某个人员的所有线路
        /// </summary>
        /// <param name="userID">人员编号</param>
        /// <returns></returns>
        public IList<Hashtable> GetRouteTree(string userID)
        {
            return dmr.GetRouteTree(userID);
        }

        /// <summary>
        /// 获取线路Tree
        /// </summary>
        /// <returns></returns>
        public DataTable GetRouteTree()
        {
            return dmr.GetRouteTree();
        }

        #region 线路数据分页
        /// <summary>
        /// 获取线路数量
        /// </summary>
        /// <param name="id">线路编号</param>
        /// <returns></returns>
        public int GetRountCount(string id)
        {
            return dmr.GetRountCount(id);
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
            return dmr.GetRouteDt(id, sCount, eCount);
        }
        #endregion
        #endregion

    }
}
