/*
 *开发人员：胡进财
 *开发时间：2012-0304
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.DB2;
using SAC.Helper;

namespace DAL
{
    /// <summary>
    /// 点检任务情况
    /// </summary>
    public class QuestcompleteDAL
    {
        bool result = false;
        string sql = "";
        string errMsg = "";
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
            string startTime = "";
            string endTime = "";
            try
            {
                LogHelper.WriteLog(LogHelper.EnLogType.Run, "数据录入开始发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：");
                //循环点检项目
                for (int i = 0; i < itemId.Length; i++)
                {
                    DateTime time = Convert.ToDateTime(Convert.ToDateTime(sTimes[i]).ToString("yyyy-MM-dd") + " 00:00:00");
                    if (judge[i] == "0")
                    {
                        startTime = Convert.ToDateTime(sTimes[i]).ToString("yyyy-MM-dd") + " 00:00:00";
                        DateTime sTime = Convert.ToDateTime(startTime);
                        //创建点检项目关系
                        do
                        {
                            if (type[i] == "日")
                                endTime = sTime.AddDays(Convert.ToInt32(count[i]) - 1).ToString("yyyy-MM-dd") + " 23:59:59";
                            else if (type[i] == "月")
                            {
                                int month = sTime.Month;
                                if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                                    endTime = sTime.AddMonths(Convert.ToInt32(count[i]) - 1).ToString("yyyy-MM") + "31 23:59:59";
                                else if (month == 4 || month == 6 || month == 9 || month == 11)
                                    endTime = sTime.AddMonths(Convert.ToInt32(count[i]) - 1).ToString("yyyy-MM") + "30 23:59:59";
                                else
                                    if (sTime.Year % 4 == 0)
                                        endTime = sTime.AddMonths(Convert.ToInt32(count[i]) - 1).ToString("yyyy-MM") + "29 23:59:59";
                                    else
                                        endTime = sTime.AddMonths(Convert.ToInt32(count[i]) - 1).ToString("yyyy-MM") + "28 23:59:59";
                            }
                            else
                                endTime = sTime.AddYears(Convert.ToInt32(count[i]) - 1).ToString("yyyy") + "-12-31 23:59:59";

                            if (Convert.ToInt32(state[i]) == 0)
                                if (deviceState[i] == "0")
                                    sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeId[i] + "','" + area[i] + "','" + deviceId[i] + "','" + itemId[i] + "','" + sTime + "','" + endTime + "',0,1);";
                                else
                                    sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeId[i] + "','" + area[i] + "','" + deviceId[i] + "','" + itemId[i] + "','" + sTime + "','" + endTime + "',0,0);";
                            else
                                if (deviceState[i] == "0")
                                    sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeId[i] + "','" + area[i] + "','" + deviceId[i] + "','" + itemId[i] + "','" + sTime + "','" + endTime + "',0,0);";
                                else
                                    sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeId[i] + "','" + area[i] + "','" + deviceId[i] + "','" + itemId[i] + "','" + sTime + "','" + endTime + "',0,1);";

                            sTime = Convert.ToDateTime(endTime).AddSeconds(1);
                        } while (Convert.ToDateTime(endTime) <= time.AddYears(10));

                    }
                    else if (judge[i] == "1")
                    {
                        //删除点检项目关系
                        sql += "delete from T_INFO_QUESTCOMPLETE where T_ROUTEID='" + routeId[i] + "' and T_AREAID='" + area[i] + "' and T_DEVICEID='" + deviceId[i] + "' and T_ITEMID='" + itemId[i] + "'";
                    }
                    else if (judge[i] == "2")
                    {
                        startTime = Convert.ToDateTime(sTimes[i]).ToString("yyyy-MM-dd") + " 00:00:00";
                        DateTime sTime = Convert.ToDateTime(startTime);
                        //编辑点检项目
                        string strSql = "select T_STARTTIME from T_INFO_QUESTCOMPLETE where T_ROUTEID='" + routeId[i] + "' and T_AREAID='" + area[i] + "' and T_DEVICEID='" + deviceId[i] + "' and T_ITEMID='" + itemId[i] + "' and T_STARTTIME <='" + sTime + "' order by T_STARTTIME desc;";
                        object obj = SAC.DB2.DBdb2.RunSingle(strSql, out errMsg);
                        //判断该点检项是否有点检任务
                        if (obj != null)
                        {
                            startTime = obj.ToString();
                            //修改点检项结束时间
                            strSql = "update T_INFO_QUESTCOMPLETE set T_ENDTIME='" + sTimes[i] + "' where T_ROUTEID='" + routeId[i] + "' and T_AREAID='" + area[i] + "' and T_DEVICEID='" + deviceId[i] + "' and T_ITEMID='" + itemId[i] + "' and T_STARTTIME ='" + startTime + "';";
                            //删除时间节点以后的任务
                            strSql = "delete from T_INFO_QUESTCOMPLETE where T_ROUTEID='" + Convert.ToDateTime(routeId[i]).AddSeconds(-1) + "' and T_AREAID='" + area[i] + "' and T_DEVICEID='" + deviceId[i] + "' and T_ITEMID='" + itemId[i] + "' and T_STARTTIME >'" + startTime + "';";
                            sql += strSql;
                            //重新生成修改后的点检任务
                            do
                            {
                                if (type[i] == "日")
                                    endTime = sTime.AddDays(Convert.ToInt32(count[i]) - 1).ToString("yyyy-MM-dd") + " 23:59:59";
                                else if (type[i] == "月")
                                {
                                    int month = sTime.Month;
                                    if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                                        endTime = sTime.AddMonths(Convert.ToInt32(count[i]) - 1).ToString("yyyy-MM") + "31 23:59:59";
                                    else if (month == 4 || month == 6 || month == 9 || month == 11)
                                        endTime = sTime.AddMonths(Convert.ToInt32(count[i]) - 1).ToString("yyyy-MM") + "30 23:59:59";
                                    else
                                        if (sTime.Year % 4 == 0)
                                            endTime = sTime.AddMonths(Convert.ToInt32(count[i]) - 1).ToString("yyyy-MM") + "29 23:59:59";
                                        else
                                            endTime = sTime.AddMonths(Convert.ToInt32(count[i]) - 1).ToString("yyyy-MM") + "28 23:59:59";
                                }
                                else
                                    endTime = sTime.AddYears(Convert.ToInt32(count[i]) - 1).ToString("yyyy") + "-12-31 23:59:59";

                                if (Convert.ToInt32(state[i]) == 0)
                                    if (deviceState[i] == "0")
                                        sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeId[i] + "','" + area[i] + "','" + deviceId[i] + "','" + itemId[i] + "','" + sTime + "','" + endTime + "',0,1);";
                                    else
                                        sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeId[i] + "','" + area[i] + "','" + deviceId[i] + "','" + itemId[i] + "','" + sTime + "','" + endTime + "',0,0);";
                                else
                                    if (deviceState[i] == "0")
                                        sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeId[i] + "','" + area[i] + "','" + deviceId[i] + "','" + itemId[i] + "','" + sTime + "','" + endTime + "',0,0);";
                                    else
                                        sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeId[i] + "','" + area[i] + "','" + deviceId[i] + "','" + itemId[i] + "','" + sTime + "','" + endTime + "',0,1);";

                                sTime = Convert.ToDateTime(endTime).AddSeconds(1);
                            } while (Convert.ToDateTime(endTime) <= time.AddYears(10));
                        }
                    }
                    else if (judge[i] == "3")
                    {
                        //编辑设备状态
                        if (Convert.ToInt32(state[i]) == 0)
                            if (deviceState[i] == "0")
                                sql += "update T_INFO_QUESTCOMPLETE set I_FLAG=1 where T_ROUTEID='" + routeId[i] + "' and T_AREAID='" + area[i] + "' and T_DEVICEID='" + deviceId[i] + "' and T_ITEMID='" + itemId[i] + "';";
                            else
                                sql += "update T_INFO_QUESTCOMPLETE set I_FLAG=0 where T_ROUTEID='" + routeId[i] + "' and T_AREAID='" + area[i] + "' and T_DEVICEID='" + deviceId[i] + "' and T_ITEMID='" + itemId[i] + "';";
                        else
                            if (deviceState[i] == "0")
                                sql += "update T_INFO_QUESTCOMPLETE set I_FLAG=0 where T_ROUTEID='" + routeId[i] + "' and T_AREAID='" + area[i] + "' and T_DEVICEID='" + deviceId[i] + "' and T_ITEMID='" + itemId[i] + "';";
                            else
                                sql += "update T_INFO_QUESTCOMPLETE set I_FLAG=1 where T_ROUTEID='" + routeId[i] + "' and T_AREAID='" + area[i] + "' and T_DEVICEID='" + deviceId[i] + "' and T_ITEMID='" + itemId[i] + "';";
                        //if (Convert.ToInt32(state[i]) == 0)
                        //    if (deviceState[i] == "0")
                        //        sql += "update T_INFO_QUESTCOMPLETE set I_FLAG=1 where T_DEVICEID='" + deviceId[i] + "' and T_ITEMID='" + itemId[i] + "';";
                        //    else
                        //        sql += "update T_INFO_QUESTCOMPLETE set I_FLAG=0 where T_DEVICEID='" + deviceId[i] + "' and T_ITEMID='" + itemId[i] + "';";
                        //else
                        //    if (deviceState[i] == "0")
                        //        sql += "update T_INFO_QUESTCOMPLETE set I_FLAG=0 where T_DEVICEID='" + deviceId[i] + "' and T_ITEMID='" + itemId[i] + "';";
                        //    else
                        //        sql += "update T_INFO_QUESTCOMPLETE set I_FLAG=1 where T_DEVICEID='" + deviceId[i] + "' and T_ITEMID='" + itemId[i] + "';";

                    }
                }

                if (DBdb2.RunNonQuery(sql, out errMsg))
                {
                    result = true; LogHelper.WriteLog(LogHelper.EnLogType.Run, "数据录入发生结束时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：");
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
            }
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
            for (int i = 0; i < itemId.Length; i++)
            {
                if (Convert.ToDateTime(cTime[i]) >= Convert.ToDateTime(sTime[i]) && Convert.ToDateTime(cTime[i]) <= Convert.ToDateTime(eTime[i]))
                {
                    sql += "update T_INFO_QUESTCOMPLETE set I_STATUS=1 where T_ROUTEID='" + routeId[i] + "' and T_AREAID='" + area[i] + "' and T_DEVICEID='" + deviceId[i] + "' and T_ITEMID='" + itemId[i] + "' and T_STARTTIME ='" + sTime[i] + "';";
                }
            }
            try
            {
                if (DBdb2.RunNonQuery(sql, out errMsg))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
            }
            return result;
        }
        #endregion
    }
}
