using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
namespace BLL
{
    public class QuestcompleteBLL
    {
        bool result = false;
        QuestcompleteDAL dal = new QuestcompleteDAL();
        #region
        /// <summary>
        /// 点检项任务管理
        /// </summary>
        /// <param name="routeId">路线ID</param>
        /// <param name="area">区域ID</param>
        /// <param name="deviceId">设备ID</param>
        /// <param name="itemId">点检项ID</param>
        /// <param name="judge">操作类型：创建点检项关系 删除点检项关系 编辑点检项  编辑设备状态</param>
        /// <param name="sTime">周期开始时间</param>
        /// <param name="type">周期类型</param>
        /// <param name="type">周期数</param>
        /// <param name="deviceState">设备状态</param>
        /// <param name="state">检测状态</param>
        /// <returns></returns>
        public bool ManageQuestcomplete(string[] routeId, string[] area, string[] deviceId, string[] itemId, string[] judge, string[] sTimes, string[] type, string[] count, string[] deviceState, string[] state)
        {
            result = dal.ManageQuestcomplete(routeId, area, deviceId, itemId, judge, sTimes, type, count, deviceState, state);
            return result;
        }

        /// <summary>
        /// 上传检测结果
        /// </summary>
        /// <param name="routeId">线路ID</param>
        /// <param name="area">区域ID</param>
        /// <param name="deviceId">设备ID</param>
        /// <param name="itemId">点检项目ID</param>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <param name="cTime">检测时间</param>
        /// <param name="uTime">上传时间</param>
        /// <returns></returns>
        public bool ChangeState(string[] routeId, string[] area, string[] deviceId, string[] itemId, string[] sTime, string[] eTime, string[] cTime, string[] uTime)
        {
            result = dal.ChangeState(routeId, area, deviceId, itemId, sTime, eTime, cTime, uTime);
            return result;
        }
        #endregion
    }
}
