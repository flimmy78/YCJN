using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;

namespace BLL
{
    public class PlanBLL
    {
        PlanDAL plan = new PlanDAL();

        /// <summary>
        /// 编辑点检任务关系
        /// </summary>
        /// <param name="routeID">线路编码</param>
        /// <param name="areaID">区域编码</param>
        /// <param name="deviceID">设备编码</param>
        /// <param name="items">点检项集合</param>
        /// <param name="judge">关系类型</param>
        /// <returns></returns>
        public bool EditRelation(string routeID, string areaID, string deviceID, string[] items, int[] judge)
        {
            return plan.EditRelation(routeID, areaID, deviceID, items, judge);
        }
        /// <summary>
        /// 新增设备状态
        /// </summary>
        /// <param name="deviceID">设备编号</param>
        /// <param name="time">增加时间</param>
        /// <param name="state">设备状态</param>
        /// <returns></returns>
        public bool addDeviceState(string deviceID, DateTime time, int state)
        {
            return plan.addDeviceState(deviceID, time, state);
        }

        /// <summary>
        /// 查询点检任务
        /// </summary>
        /// <param name="userID">人员编号</param>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns></returns>
        public DataTable GetPlans(string userID, DateTime sTime, DateTime eTime)
        {
            return plan.GetPlans(userID, sTime, eTime);
        }

        /// <summary>
        /// 查询点检任务
        /// </summary>
        /// <param name="userID">人员编号</param>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <param name="sCount">开始数量</param>
        /// <param name="eCount">结束数量</param>
        /// <returns></returns>
        public DataTable GetPlan(string userID, DateTime sTime, DateTime eTime, int sCount, int eCount)
        {
            return plan.GetPlan(userID, sTime, eTime, sCount, eCount);
        }

        /// <summary>
        /// 查询点检任务
        /// </summary>
        /// <param name="userID">人员编号</param>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns></returns>
        public int GetPlanCount(string userID, DateTime sTime, DateTime eTime)
        {
            return plan.GetPlanCount(userID, sTime, eTime);
        }

        /// <summary>
        /// 查询点检任务
        /// </summary>
        /// <param name="userID">用户编码</param>
        /// <param name="routeID">线路编号</param>
        /// <param name="areaID">区域编号</param>
        /// <param name="deviceID">设备编号</param>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <param name="sCount">开始范围</param>
        /// <param name="eCount">结束范围</param>
        /// <returns></returns>
        public DataTable GetPlan(string userID, string routeID, string areaID, string deviceID, DateTime sTime, DateTime eTime, int sCount, int eCount)
        {
            return plan.GetPlan(userID, routeID, areaID, deviceID, sTime, eTime, sCount, eCount);
        }

        /// <summary>
        /// 查询点检任务
        /// </summary>
        /// <param name="userID">用户编码</param>
        /// <param name="routeID">线路编号</param>
        /// <param name="areaID">区域编号</param>
        /// <param name="deviceID">设备编号</param>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns></returns>
        public int GetPlanCount(string userID, string routeID, string areaID, string deviceID, DateTime sTime, DateTime eTime)
        {
            return plan.GetPlanCount(userID, routeID, areaID, deviceID, sTime, eTime);
        }
        #region 修改点检结果
        public bool EditPlanResult(string routeId, string areaId, string deviceId, string itemId, int state, string value, string time)
        {
            return plan.EditPlanResult(routeId, areaId, deviceId, itemId, state, value, time);
        }
        #endregion
    }
}
