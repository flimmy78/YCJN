using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.DB2;
using System.Data;

namespace DAL
{
    public class PlanDAL
    {
        DateTime sTime = new DateTime();
        DateTime eTime = new DateTime();
        DateTime endTime = new DateTime();
        int type = 0;
        int count = 0;
        int dState = 0;
        int iState = 0;

        DataTable sdt = new DataTable();
        DataTable bdt = new DataTable();
        DataTable dtState = new DataTable();

        string sql = "";
        string sqlStr = "";
        string errMsg = "";
        bool result = false;

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
            for (int i = 0; i < items.Length; i++)
            {
                //添加任务
                if (judge[i] == 0)
                {
                    //删除当前时间之后的所有已生成的任务
                    sqlStr = "delete from T_INFO_QUESTCOMPLETE where T_ROUTEID='" + routeID + "' and T_AREAID='" + areaID + "' and T_DEVICEID='" + deviceID + "' and T_ITEMID='" + items[i] + "' and T_STARTTIME>='" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "'";
                    DBdb2.RunNonQuery(sqlStr, out errMsg);

                    //获取到小于当前时间的最近的一个点检项目的周期   确定点检任务开始的时间
                    sqlStr = "select T_STARTTIME,T_PERIODTYPE,T_PERIODVALUE,T_STATUS from T_BASE_ITEM where T_ITEMID='" + items[i] + "' and T_STARTTIME<'" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' order by T_STARTTIME desc fetch first 1 rows only";
                    sdt = DBdb2.RunDataTable(sqlStr, out errMsg);
                    sqlStr = "select select T_STARTTIME,T_PERIODTYPE,T_PERIODVALUE,T_STATUS from T_BASE_ITEM where T_ITEMID='" + items[i] + "' and T_STARTTIME=>'" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "' order by T_STARTTIME asc";
                    bdt = DBdb2.RunDataTable(sqlStr, out errMsg);

                    if ((sdt != null && sdt.Rows.Count > 0) && (bdt != null && bdt.Rows.Count > 0))
                    {
                        //循环点检项任务周期
                        for (int k = 0; k < bdt.Rows.Count; k++)
                        {
                            #region 获取任务开始和结束时间
                            if (k == 0)
                            {
                                sTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
                                endTime = Convert.ToDateTime(bdt.Rows[0]["T_STARTTIME"].ToString());
                                type = Convert.ToInt32(sdt.Rows[0]["T_PERIODTYPE"].ToString());
                                count = Convert.ToInt32(sdt.Rows[0]["T_PERIODVALUE"].ToString());
                                iState = Convert.ToInt32(sdt.Rows[0]["T_STATUS"].ToString());
                            }
                            else
                            {
                                if (k != bdt.Rows.Count - 1)
                                {
                                    sTime = Convert.ToDateTime(bdt.Rows[k - 1]["T_TIME"].ToString());
                                    endTime = Convert.ToDateTime(bdt.Rows[k]["T_TIME"].ToString());
                                }
                                else
                                {
                                    sTime = Convert.ToDateTime(bdt.Rows[k]["T_STARTTIME"].ToString());
                                    endTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00")).AddYears(10);
                                }
                                type = Convert.ToInt32(bdt.Rows[0]["T_PERIODTYPE"].ToString());
                                count = Convert.ToInt32(bdt.Rows[0]["T_PERIODVALUE"].ToString());
                                iState = Convert.ToInt32(bdt.Rows[0]["T_STATUS"].ToString());
                            }

                            #endregion
                            #region 获取任务期间机器的状态
                            sqlStr = "SELECT T_DEVICEID,T_TIME,I_STATUS FROM ADMINISTRATOR.T_INFO_DEVICE where T_DEVICEID='" + deviceID + "' and T_TIME>='" + sTime + "' and T_TIME<'" + endTime + "' order by  T_TIME asc";
                            dtState = DBdb2.RunDataTable(sqlStr, out errMsg);

                            sqlStr = "SELECT T_DEVICEID,T_TIME,I_STATUS FROM ADMINISTRATOR.T_INFO_DEVICE where T_DEVICEID='" + deviceID + "' and T_TIME<'" + sTime + "' order by  T_TIME desc fetch first 1 rows only";
                            DataTable sdtState = DBdb2.RunDataTable(sqlStr, out errMsg);

                            #endregion
                            #region 生成点检任务

                            if (dtState != null && dtState.Rows.Count > 0)
                            {
                                for (int j = 0; j < dtState.Rows.Count; j++)
                                {
                                    if (j != dtState.Rows.Count - 1)
                                    {
                                        if (j == 0)
                                        {
                                            endTime = Convert.ToDateTime(dtState.Rows[j]["T_TIME"].ToString());
                                        }
                                        else
                                        {
                                            sTime = Convert.ToDateTime(dtState.Rows[j]["T_TIME"].ToString());
                                            endTime = Convert.ToDateTime(dtState.Rows[j + 1]["T_TIME"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        sTime = Convert.ToDateTime(dtState.Rows[j]["T_TIME"].ToString());
                                    }

                                    dState = Convert.ToInt32(dtState.Rows[j]["I_STATUS"].ToString());

                                    do
                                    {
                                        if (type == 1)
                                        {
                                            eTime = Convert.ToDateTime(sTime.AddDays(count).ToString("yyyy-MM-dd") + " 23:59:59");
                                        }
                                        else if (type == 2)
                                        {
                                            int month = 0;
                                            month = sTime.AddMonths(count).Month;
                                            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                                                eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "31 23:59:59");
                                            else if (month == 4 || month == 6 || month == 9 || month == 11)
                                                eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "30 23:59:59");
                                            else
                                                if (sTime.Year % 4 == 0)
                                                    eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "29 23:59:59");
                                                else
                                                    eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "28 23:59:59");
                                        }
                                        else
                                        {
                                            eTime = Convert.ToDateTime(sTime.AddYears(count).ToString("yyyy") + "-12-31 23:59:59");
                                        }

                                        //在用
                                        if (dState == 0)
                                            //停机
                                            if (iState == 0)
                                                sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,0);";
                                            else
                                                sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,1);";
                                        else
                                            if (iState == 0)
                                                sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,1);";
                                            else
                                                sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,0);";
                                        sTime = eTime.AddSeconds(1);
                                    } while (sTime <= endTime);
                                }
                            }
                            else
                            {
                                dState = Convert.ToInt32(sdtState.Rows[0]["I_STATUS"].ToString());

                                do
                                {
                                    if (type == 1)
                                    {
                                        eTime = Convert.ToDateTime(sTime.AddDays(count).ToString("yyyy-MM-dd") + " 23:59:59");
                                    }
                                    else if (type == 2)
                                    {
                                        int month = 0;
                                        month = sTime.AddMonths(count).Month;
                                        if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                                            eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "31 23:59:59");
                                        else if (month == 4 || month == 6 || month == 9 || month == 11)
                                            eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "30 23:59:59");
                                        else
                                            if (sTime.Year % 4 == 0)
                                                eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "29 23:59:59");
                                            else
                                                eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "28 23:59:59");
                                    }
                                    else
                                    {
                                        eTime = Convert.ToDateTime(sTime.AddYears(count).ToString("yyyy") + "-12-31 23:59:59");
                                    }

                                    //在用
                                    if (dState == 0)
                                        //停机
                                        if (iState == 0)
                                            sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,0);";
                                        else
                                            sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,1);";
                                    else
                                        if (iState == 0)
                                            sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,1);";
                                        else
                                            sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,0);";
                                    sTime = eTime.AddSeconds(1);
                                } while (sTime <= endTime);

                            }
                            #endregion
                        }
                    }
                    else if (sdt != null && sdt.Rows.Count > 0)
                    {//只存在一个点检项周期
                        sTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
                        type = Convert.ToInt32(sdt.Rows[0]["T_PERIODTYPE"].ToString());
                        count = Convert.ToInt32(sdt.Rows[0]["T_PERIODVALUE"].ToString());
                        iState = Convert.ToInt32(sdt.Rows[0]["T_STATUS"].ToString());

                        sqlStr = "SELECT T_DEVICEID,T_TIME,I_STATUS FROM ADMINISTRATOR.T_INFO_DEVICE where T_DEVICEID='" + deviceID + "' and T_TIME<'" + sTime + "' order by  T_TIME desc fetch first 1 rows only";
                        dtState = DBdb2.RunDataTable(sqlStr, out errMsg);
                        DataTable bdtState = new DataTable();
                        sqlStr = "SELECT T_DEVICEID,T_TIME,I_STATUS FROM ADMINISTRATOR.T_INFO_DEVICE where T_DEVICEID='" + deviceID + "' and T_TIME>='" + sTime + "' order by  T_TIME desc fetch first 1 rows only";
                        bdtState = DBdb2.RunDataTable(sqlStr, out errMsg);

                        if ((dtState != null && dtState.Rows.Count > 0) && (bdtState != null && bdtState.Rows.Count > 0))
                        {
                            for (int k = 0; k < bdtState.Rows.Count; k++)
                            {
                                if (k == 0)
                                {
                                    endTime = Convert.ToDateTime(bdtState.Rows[0]["T_TIME"].ToString());
                                    dState = Convert.ToInt32(dtState.Rows[0]["I_STATUS"].ToString());
                                }
                                else
                                {
                                    if (k != bdtState.Rows.Count - 1)
                                    {
                                        endTime = Convert.ToDateTime(bdtState.Rows[k]["T_TIME"].ToString());
                                        dState = Convert.ToInt32(bdtState.Rows[k - 1]["I_STATUS"].ToString());
                                    }
                                    else
                                    {
                                        endTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00")).AddYears(10);
                                        dState = Convert.ToInt32(bdtState.Rows[k]["I_STATUS"].ToString());
                                    }
                                }

                                do
                                {
                                    if (type == 1)
                                    {
                                        eTime = Convert.ToDateTime(sTime.AddDays(count).ToString("yyyy-MM-dd") + " 23:59:59");
                                    }
                                    else if (type == 2)
                                    {
                                        int month = 0;
                                        month = sTime.AddMonths(count).Month;
                                        if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                                            eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "31 23:59:59");
                                        else if (month == 4 || month == 6 || month == 9 || month == 11)
                                            eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "30 23:59:59");
                                        else
                                            if (sTime.Year % 4 == 0)
                                                eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "29 23:59:59");
                                            else
                                                eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "28 23:59:59");
                                    }
                                    else
                                    {
                                        eTime = Convert.ToDateTime(sTime.AddYears(count).ToString("yyyy") + "-12-31 23:59:59");
                                    }

                                    //在用
                                    if (dState == 0)
                                        //停机
                                        if (iState == 0)
                                            sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,0);";
                                        else
                                            sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,1);";
                                    else
                                        if (iState == 0)
                                            sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,1);";
                                        else
                                            sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,0);";
                                    sTime = eTime.AddSeconds(1);
                                } while (sTime <= endTime);
                            }
                        }
                        else if (dtState != null && dtState.Rows.Count > 0)
                        {
                            endTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00")).AddYears(10);
                            dState = Convert.ToInt32(dtState.Rows[0]["I_STATUS"].ToString());

                            do
                            {
                                if (type == 1)
                                {
                                    eTime = Convert.ToDateTime(sTime.AddDays(count).ToString("yyyy-MM-dd") + " 23:59:59");
                                }
                                else if (type == 2)
                                {
                                    int month = 0;
                                    month = sTime.AddMonths(count).Month;
                                    if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                                        eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "31 23:59:59");
                                    else if (month == 4 || month == 6 || month == 9 || month == 11)
                                        eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "30 23:59:59");
                                    else
                                        if (sTime.Year % 4 == 0)
                                            eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "29 23:59:59");
                                        else
                                            eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "28 23:59:59");
                                }
                                else
                                {
                                    eTime = Convert.ToDateTime(sTime.AddYears(count).ToString("yyyy") + "-12-31 23:59:59");
                                }

                                //在用
                                if (dState == 0)
                                    //停机
                                    if (iState == 0)
                                        sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,0);";
                                    else
                                        sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,1);";
                                else
                                    if (iState == 0)
                                        sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,1);";
                                    else
                                        sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,0);";
                                sTime = eTime.AddSeconds(1);
                            } while (sTime <= endTime);
                        }
                        else if (bdtState != null && bdtState.Rows.Count > 0)
                        {
                            for (int k = 0; k < bdtState.Rows.Count; k++)
                            {
                                if (k != bdtState.Rows.Count - 1)
                                {
                                    sTime = Convert.ToDateTime(bdtState.Rows[k]["T_TIME"].ToString());
                                    endTime = Convert.ToDateTime(bdtState.Rows[k + 1]["T_TIME"].ToString());
                                    dState = Convert.ToInt32(bdtState.Rows[k]["I_STATUS"].ToString());
                                }
                                else
                                {
                                    sTime = Convert.ToDateTime(bdtState.Rows[k]["T_TIME"].ToString());
                                    endTime = Convert.ToDateTime(bdtState.Rows[0]["T_TIME"].ToString()).AddYears(10);
                                    dState = Convert.ToInt32(bdtState.Rows[k]["I_STATUS"].ToString());
                                }
                                do
                                {
                                    if (type == 1)
                                    {
                                        eTime = Convert.ToDateTime(sTime.AddDays(count).ToString("yyyy-MM-dd") + " 23:59:59");
                                    }
                                    else if (type == 2)
                                    {
                                        int month = 0;
                                        month = sTime.AddMonths(count).Month;
                                        if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                                            eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "31 23:59:59");
                                        else if (month == 4 || month == 6 || month == 9 || month == 11)
                                            eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "30 23:59:59");
                                        else
                                            if (sTime.Year % 4 == 0)
                                                eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "29 23:59:59");
                                            else
                                                eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "28 23:59:59");
                                    }
                                    else
                                    {
                                        eTime = Convert.ToDateTime(sTime.AddYears(count).ToString("yyyy") + "-12-31 23:59:59");
                                    }

                                    //在用
                                    if (dState == 0)
                                        //停机
                                        if (iState == 0)
                                            sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,0);";
                                        else
                                            sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,1);";
                                    else
                                        if (iState == 0)
                                            sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,1);";
                                        else
                                            sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,0);";
                                    sTime = eTime.AddSeconds(1);
                                } while (sTime <= endTime);
                            }
                        }
                    }
                    else if (bdt != null && bdt.Rows.Count > 0)
                    {
                        //循环点检项任务周期
                        for (int k = 0; k < bdt.Rows.Count; k++)
                        {
                            #region 获取任务开始和结束时间
                            if (k != bdt.Rows.Count - 1)
                            {
                                sTime = Convert.ToDateTime(bdt.Rows[k]["T_STARTTIME"].ToString());
                                endTime = Convert.ToDateTime(bdt.Rows[k + 1]["T_STARTTIME"].ToString());
                                type = Convert.ToInt32(bdt.Rows[k]["T_PERIODTYPE"].ToString());
                                count = Convert.ToInt32(bdt.Rows[k]["T_PERIODVALUE"].ToString());
                                iState = Convert.ToInt32(bdt.Rows[k]["T_STATUS"].ToString());
                            }
                            else
                            {
                                sTime = Convert.ToDateTime(bdt.Rows[k]["T_STARTTIME"].ToString());
                                endTime = Convert.ToDateTime(bdt.Rows[0]["T_STARTTIME"].ToString()).AddYears(10);
                                type = Convert.ToInt32(bdt.Rows[k]["T_PERIODTYPE"].ToString());
                                count = Convert.ToInt32(bdt.Rows[k]["T_PERIODVALUE"].ToString());
                                iState = Convert.ToInt32(bdt.Rows[k]["T_STATUS"].ToString());
                            }

                            #endregion
                            #region 获取任务期间机器的状态
                            sqlStr = "SELECT T_DEVICEID,T_TIME,I_STATUS FROM ADMINISTRATOR.T_INFO_DEVICE where T_DEVICEID='" + deviceID + "' and T_TIME>='" + sTime + "' and T_TIME<'" + endTime + "' order by  T_TIME asc";
                            dtState = DBdb2.RunDataTable(sqlStr, out errMsg);

                            sqlStr = "SELECT T_DEVICEID,T_TIME,I_STATUS FROM ADMINISTRATOR.T_INFO_DEVICE where T_DEVICEID='" + deviceID + "' and T_TIME<'" + sTime + "' order by  T_TIME desc fetch first 1 rows only";
                            DataTable sdtState = DBdb2.RunDataTable(sqlStr, out errMsg);

                            #endregion
                            #region 生成点检任务

                            if (dtState != null && dtState.Rows.Count > 0)
                            {
                                for (int j = 0; j < dtState.Rows.Count; j++)
                                {
                                    if (j != dtState.Rows.Count - 1)
                                    {
                                        if (j == 0)
                                        {
                                            sTime = Convert.ToDateTime(sdtState.Rows[0]["T_TIME"].ToString());
                                            endTime = Convert.ToDateTime(dtState.Rows[j]["T_TIME"].ToString());

                                            do
                                            {
                                                if (type == 1)
                                                {
                                                    eTime = Convert.ToDateTime(sTime.AddDays(count).ToString("yyyy-MM-dd") + " 23:59:59");
                                                }
                                                else if (type == 2)
                                                {
                                                    int month = 0;
                                                    month = sTime.AddMonths(count).Month;
                                                    if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                                                        eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "31 23:59:59");
                                                    else if (month == 4 || month == 6 || month == 9 || month == 11)
                                                        eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "30 23:59:59");
                                                    else
                                                        if (sTime.Year % 4 == 0)
                                                            eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "29 23:59:59");
                                                        else
                                                            eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "28 23:59:59");
                                                }
                                                else
                                                {
                                                    eTime = Convert.ToDateTime(sTime.AddYears(count).ToString("yyyy") + "-12-31 23:59:59");
                                                }

                                                //在用
                                                if (dState == 0)
                                                    //停机
                                                    if (iState == 0)
                                                        sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,0);";
                                                    else
                                                        sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,1);";
                                                else
                                                    if (iState == 0)
                                                        sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,1);";
                                                    else
                                                        sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,0);";
                                                sTime = eTime.AddSeconds(1);
                                            } while (sTime <= endTime);

                                            sTime = Convert.ToDateTime(dtState.Rows[j]["T_TIME"].ToString());
                                            endTime = Convert.ToDateTime(dtState.Rows[j + 1]["T_TIME"].ToString());
                                            do
                                            {
                                                if (type == 1)
                                                {
                                                    eTime = Convert.ToDateTime(sTime.AddDays(count).ToString("yyyy-MM-dd") + " 23:59:59");
                                                }
                                                else if (type == 2)
                                                {
                                                    int month = 0;
                                                    month = sTime.AddMonths(count).Month;
                                                    if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                                                        eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "31 23:59:59");
                                                    else if (month == 4 || month == 6 || month == 9 || month == 11)
                                                        eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "30 23:59:59");
                                                    else
                                                        if (sTime.Year % 4 == 0)
                                                            eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "29 23:59:59");
                                                        else
                                                            eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "28 23:59:59");
                                                }
                                                else
                                                {
                                                    eTime = Convert.ToDateTime(sTime.AddYears(count).ToString("yyyy") + "-12-31 23:59:59");
                                                }

                                                //在用
                                                if (dState == 0)
                                                    //停机
                                                    if (iState == 0)
                                                        sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,0);";
                                                    else
                                                        sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,1);";
                                                else
                                                    if (iState == 0)
                                                        sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,1);";
                                                    else
                                                        sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,0);";
                                                sTime = eTime.AddSeconds(1);
                                            } while (sTime <= endTime);
                                        }
                                        else
                                        {
                                            sTime = Convert.ToDateTime(dtState.Rows[j]["T_TIME"].ToString());
                                            endTime = Convert.ToDateTime(dtState.Rows[j + 1]["T_TIME"].ToString());
                                            do
                                            {
                                                if (type == 1)
                                                {
                                                    eTime = Convert.ToDateTime(sTime.AddDays(count).ToString("yyyy-MM-dd") + " 23:59:59");
                                                }
                                                else if (type == 2)
                                                {
                                                    int month = 0;
                                                    month = sTime.AddMonths(count).Month;
                                                    if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                                                        eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "31 23:59:59");
                                                    else if (month == 4 || month == 6 || month == 9 || month == 11)
                                                        eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "30 23:59:59");
                                                    else
                                                        if (sTime.Year % 4 == 0)
                                                            eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "29 23:59:59");
                                                        else
                                                            eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "28 23:59:59");
                                                }
                                                else
                                                {
                                                    eTime = Convert.ToDateTime(sTime.AddYears(count).ToString("yyyy") + "-12-31 23:59:59");
                                                }

                                                //在用
                                                if (dState == 0)
                                                    //停机
                                                    if (iState == 0)
                                                        sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,0);";
                                                    else
                                                        sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,1);";
                                                else
                                                    if (iState == 0)
                                                        sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,1);";
                                                    else
                                                        sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,0);";
                                                sTime = eTime.AddSeconds(1);
                                            } while (sTime <= endTime);
                                        }
                                    }
                                }
                            }
                            else
                            {

                                dState = Convert.ToInt32(sdtState.Rows[0]["I_STATUS"].ToString());

                                do
                                {
                                    if (type == 1)
                                    {
                                        eTime = Convert.ToDateTime(sTime.AddDays(count).ToString("yyyy-MM-dd") + " 23:59:59");
                                    }
                                    else if (type == 2)
                                    {
                                        int month = 0;
                                        month = sTime.AddMonths(count).Month;
                                        if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                                            eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "31 23:59:59");
                                        else if (month == 4 || month == 6 || month == 9 || month == 11)
                                            eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "30 23:59:59");
                                        else
                                            if (sTime.Year % 4 == 0)
                                                eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "29 23:59:59");
                                            else
                                                eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "28 23:59:59");
                                    }
                                    else
                                    {
                                        eTime = Convert.ToDateTime(sTime.AddYears(count).ToString("yyyy") + "-12-31 23:59:59");
                                    }

                                    //在用
                                    if (dState == 0)
                                        //停机
                                        if (iState == 0)
                                            sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,0);";
                                        else
                                            sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,1);";
                                    else
                                        if (iState == 0)
                                            sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,1);";
                                        else
                                            sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + routeID + "','" + areaID + "','" + deviceID + "','" + items[i] + "','" + sTime + "','" + eTime + "',0,0);";
                                    sTime = eTime.AddSeconds(1);
                                } while (sTime <= endTime);

                            }
                            #endregion
                        }
                    }
                }
                else
                {//删除任务
                    sql += "delete from T_INFO_QUESTCOMPLETE where T_ROUTEID='" + routeID + "' and T_AREAID='" + areaID + "' and T_DEVICEID='" + deviceID + "' and T_ITEMID='" + items[i] + "' and T_STARTTIME>='" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00") + "';";
                }
            }
            result = DBdb2.RunNonQuery(sql, out errMsg);
            return result;
        }


        #region 编辑点检项
        /// <summary>
        /// 编辑点检项目
        /// </summary>
        /// <param name="deviceID">设备ID</param>
        /// <param name="itemID">点检项ID</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public bool EditItem(string deviceID, string itemID, DateTime time)
        {
            DataTable dt = new DataTable();
            //获取需要修改的数据的集合
            sqlStr = "select r.T_NODEID routeID,ar.areaID,ar.deviceID,ar.ItemID from T_INFO_ROUTE r inner join (select a.T_NODEID areaID,dv.deviceID,dv.ItemID,a.T_PARAENTID from T_INFO_ROUTE a inner join (select d.T_NODEID deviceID,d.T_PARAENTID,i.ItemID from T_INFO_ROUTE d inner join (select T_NODEID ItemID,T_PARAENTID from T_INFO_ROUTE where T_NODEID='" + itemID + "') i on d.T_NODEKEY=i.T_PARAENTID) dv on a.T_NODEKEY=dv.T_PARAENTID) ar on r.T_NODEKEY=ar.T_PARAENTID";
            dt = DBdb2.RunDataTable(sqlStr, out errMsg);

            //获取点检项周期
            sqlStr = "select T_STARTTIME,T_PERIODTYPE,T_PERIODVALUE,T_STATUS from T_BASE_ITEM where T_ITEMID='" + itemID + "' and T_STARTTIME=>'" + time + "' order by T_STARTTIME asc";
            bdt = DBdb2.RunDataTable(sqlStr, out errMsg);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //删除当前时间之后的所有已生成的任务
                sqlStr = "delete from T_INFO_QUESTCOMPLETE where T_ROUTEID='" + dt.Rows[i]["routeID"] + "' and T_AREAID='" + dt.Rows[i]["areaID"] + "' and T_DEVICEID='" + dt.Rows[i]["deviceID"] + "' and T_ITEMID='" + dt.Rows[i]["ItemID"] + "' and T_STARTTIME>='" + time + "'";
                DBdb2.RunNonQuery(sqlStr, out errMsg);
                //循环点检项周期
                for (int k = 0; k < bdt.Rows.Count; k++)
                {
                    if (bdt.Rows.Count == 1)
                    {
                        sTime = Convert.ToDateTime(bdt.Rows[k]["T_STARTTIME"].ToString());
                        endTime = Convert.ToDateTime(bdt.Rows[k]["T_STARTTIME"].ToString()).AddYears(10);
                        type = Convert.ToInt32(bdt.Rows[k]["T_PERIODTYPE"].ToString());
                        count = Convert.ToInt32(bdt.Rows[k]["T_PERIODVALUE"].ToString());
                        iState = Convert.ToInt32(bdt.Rows[k]["T_STATUS"].ToString());
                    }
                    else
                    {
                        if (k + 1 == bdt.Rows.Count)
                        {
                            sTime = Convert.ToDateTime(bdt.Rows[k + 1]["T_STARTTIME"].ToString());
                            endTime = Convert.ToDateTime(bdt.Rows[k + 1]["T_STARTTIME"].ToString()).AddYears(10);
                            type = Convert.ToInt32(bdt.Rows[k + 1]["T_PERIODTYPE"].ToString());
                            count = Convert.ToInt32(bdt.Rows[k + 1]["T_PERIODVALUE"].ToString());
                            iState = Convert.ToInt32(bdt.Rows[k + 1]["T_STATUS"].ToString());
                        }
                        else
                        {
                            sTime = Convert.ToDateTime(bdt.Rows[k]["T_STARTTIME"].ToString());
                            endTime = Convert.ToDateTime(bdt.Rows[k + 1]["T_STARTTIME"].ToString());
                            type = Convert.ToInt32(bdt.Rows[k]["T_PERIODTYPE"].ToString());
                            count = Convert.ToInt32(bdt.Rows[k]["T_PERIODVALUE"].ToString());
                            iState = Convert.ToInt32(bdt.Rows[k]["T_STATUS"].ToString());
                        }
                    }

                    //获取机器状态
                    sqlStr = "SELECT T_DEVICEID,T_TIME,I_STATUS FROM ADMINISTRATOR.T_INFO_DEVICE where T_DEVICEID='" + deviceID + "' and T_TIME>='" + sTime + "' and T_TIME<'" + endTime + "' order by  T_TIME asc";
                    dtState = DBdb2.RunDataTable(sqlStr, out errMsg);

                    sqlStr = "SELECT T_DEVICEID,T_TIME,I_STATUS FROM ADMINISTRATOR.T_INFO_DEVICE where T_DEVICEID='" + deviceID + "' and T_TIME<'" + sTime + "' order by  T_TIME desc fetch first 1 rows only";
                    DataTable sdtState = DBdb2.RunDataTable(sqlStr, out errMsg);

                    #region 生成点检任务

                    if (dtState != null && dtState.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtState.Rows.Count; j++)
                        {
                            if (j != dtState.Rows.Count - 1)
                            {
                                if (j == 0)
                                {
                                    endTime = Convert.ToDateTime(dtState.Rows[j]["T_TIME"].ToString());
                                    dState = Convert.ToInt32(sdtState.Rows[0]["I_STATUS"].ToString());
                                }
                                else
                                {
                                    if (j != dtState.Rows.Count - 1)
                                    {
                                        sTime = Convert.ToDateTime(dtState.Rows[j]["T_TIME"].ToString());
                                        endTime = Convert.ToDateTime(dtState.Rows[j + 1]["T_TIME"].ToString());
                                        dState = Convert.ToInt32(dtState.Rows[j]["I_STATUS"].ToString());
                                    }
                                    else
                                    {
                                        sTime = Convert.ToDateTime(dtState.Rows[j]["T_TIME"].ToString());
                                        dState = Convert.ToInt32(dtState.Rows[j]["I_STATUS"].ToString());
                                    }
                                }

                                do
                                {
                                    if (type == 1)
                                    {
                                        eTime = Convert.ToDateTime(sTime.AddDays(count).ToString("yyyy-MM-dd") + " 23:59:59");
                                    }
                                    else if (type == 2)
                                    {
                                        int month = 0;
                                        month = sTime.AddMonths(count).Month;
                                        if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                                            eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "31 23:59:59");
                                        else if (month == 4 || month == 6 || month == 9 || month == 11)
                                            eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "30 23:59:59");
                                        else
                                            if (sTime.Year % 4 == 0)
                                                eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "29 23:59:59");
                                            else
                                                eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "28 23:59:59");
                                    }
                                    else
                                    {
                                        eTime = Convert.ToDateTime(sTime.AddYears(count).ToString("yyyy") + "-12-31 23:59:59");
                                    }

                                    //在用
                                    if (dState == 0)
                                        //停机
                                        if (iState == 0)
                                            sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + dt.Rows[i]["routeID"] + "','" + dt.Rows[i]["areaID"] + "','" + deviceID + "','" + itemID + "','" + sTime + "','" + eTime + "',0,0);";
                                        else
                                            sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + dt.Rows[i]["routeID"] + "','" + dt.Rows[i]["areaID"] + "','" + deviceID + "','" + itemID + "','" + sTime + "','" + eTime + "',0,1);";
                                    else
                                        if (iState == 0)
                                            sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + dt.Rows[i]["routeID"] + "','" + dt.Rows[i]["areaID"] + "','" + deviceID + "','" + itemID + "','" + sTime + "','" + eTime + "',0,1);";
                                        else
                                            sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + dt.Rows[i]["routeID"] + "','" + dt.Rows[i]["areaID"] + "','" + deviceID + "','" + itemID + "','" + sTime + "','" + eTime + "',0,0);";
                                    sTime = eTime.AddSeconds(1);
                                } while (sTime <= endTime);
                            }
                        }
                    }
                    else
                    {
                        dState = Convert.ToInt32(sdtState.Rows[0]["I_STATUS"].ToString());

                        do
                        {
                            if (type == 1)
                            {
                                eTime = Convert.ToDateTime(sTime.AddDays(count).ToString("yyyy-MM-dd") + " 23:59:59");
                            }
                            else if (type == 2)
                            {
                                int month = 0;
                                month = sTime.AddMonths(count).Month;
                                if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                                    eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "31 23:59:59");
                                else if (month == 4 || month == 6 || month == 9 || month == 11)
                                    eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "30 23:59:59");
                                else
                                    if (sTime.Year % 4 == 0)
                                        eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "29 23:59:59");
                                    else
                                        eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "28 23:59:59");
                            }
                            else
                            {
                                eTime = Convert.ToDateTime(sTime.AddYears(count).ToString("yyyy") + "-12-31 23:59:59");
                            }

                            //在用
                            if (dState == 0)
                                //停机
                                if (iState == 0)
                                    sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + dt.Rows[i]["routeID"] + "','" + dt.Rows[i]["areaID"] + "','" + deviceID + "','" + itemID + "','" + sTime + "','" + eTime + "',0,0);";
                                else
                                    sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + dt.Rows[i]["routeID"] + "','" + dt.Rows[i]["areaID"] + "','" + deviceID + "','" + itemID + "','" + sTime + "','" + eTime + "',0,1);";
                            else
                                if (iState == 0)
                                    sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + dt.Rows[i]["routeID"] + "','" + dt.Rows[i]["areaID"] + "','" + deviceID + "','" + itemID + "','" + sTime + "','" + eTime + "',0,1);";
                                else
                                    sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + dt.Rows[i]["routeID"] + "','" + dt.Rows[i]["areaID"] + "','" + deviceID + "','" + itemID + "','" + sTime + "','" + eTime + "',0,0);";
                            sTime = eTime.AddSeconds(1);
                        } while (sTime <= endTime);

                    }
                    #endregion
                }
            }
            return result;
        }
        #endregion

        #region 编辑设备状态
        /// <summary>
        /// 更改设备状态
        /// </summary>
        /// <param name="deviceID">设备编码</param>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <param name="state">设备状态</param>
        /// <returns></returns>
        public bool EditDeviceState(string deviceID, DateTime startTime, DateTime closeTime, int state)
        {
            //获取某个设备下面的所有的点检项 及相关连信息   线路ID  区域ID  设备ID 点检项ID
            sqlStr = "select r.T_NODEID routeID,ar.areaID,ar.deviceID,ar.itemID from T_INFO_ROUTE r inner join (select a.T_NODEID areaID,a.T_PARAENTID,de.deviceID,de.itemID from T_INFO_ROUTE a inner join (select d.T_NODEID deviceID,d.T_PARAENTID,i.T_NODEID itemID from T_INFO_ROUTE d inner join (select T_NODEID,T_PARAENTID from T_INFO_ROUTE where T_PARAENTID in(select T_NODEKEY From T_INFO_ROUTE where T_NODEID='" + deviceID + "')) i on d.T_NODEKEY=i.T_PARAENTID) de on a.T_NODEKEY=de.T_PARAENTID) ar on r.T_NODEKEY=ar.T_PARAENTID;";

            DataTable dt = new DataTable();
            DataTable dtItem = new DataTable();
            dt = DBdb2.RunDataTable(sqlStr, out errMsg);

            DateTime time = startTime;
            DateTime objTime = closeTime;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //删除时间段之内的任务数据
                sqlStr = "delete from T_INFO_QUESTCOMPLETE where T_STARTTIME between '" + time + "' and '" + objTime + "'";
                DBdb2.RunNonQuery(sqlStr, out errMsg);

                //获取点检项周期
                sqlStr = "select T_STARTTIME,T_PERIODTYPE,T_PERIODVALUE,T_STATUS from T_BASE_ITEM where T_ITEMID='" + dt.Rows[i]["itemID"] + "' and T_STARTTIME between '" + time + "' and '" + objTime + "' order by T_STARTTIME asc ";
                dtItem = DBdb2.RunDataTable(sqlStr, out errMsg);

                if (dtItem != null && dtItem.Rows.Count > 0)
                {
                    for (int k = 0; k < dtItem.Rows.Count; k++)
                    {
                        if (k == 0)
                        {
                            sTime = time;

                            if (time != Convert.ToDateTime(dtItem.Rows[k]["T_PERIODTYPE"].ToString()))
                            {
                                //获取上一个周期 信息
                                sdt = DBdb2.RunDataTable("select T_STARTTIME,T_PERIODTYPE,T_PERIODVALUE,T_STATUS from T_BASE_ITEM where T_ITEMID='" + dt.Rows[i]["itemID"] + "' and T_STARTTIME < '" + time + "' order by T_STARTTIME desc fetch first 1 rows only;", out errMsg);
                                type = Convert.ToInt32(sdt.Rows[0]["T_PERIODTYPE"].ToString());
                                count = Convert.ToInt32(sdt.Rows[0]["T_PERIODVALUE"].ToString());
                                iState = Convert.ToInt32(sdt.Rows[0]["T_STATUS"].ToString());
                                endTime = Convert.ToDateTime(dtItem.Rows[k]["T_PERIODTYPE"].ToString());
                            }
                            else
                            {
                                type = Convert.ToInt32(dtItem.Rows[k]["T_PERIODTYPE"].ToString());
                                count = Convert.ToInt32(dtItem.Rows[k]["T_PERIODVALUE"].ToString());
                                iState = Convert.ToInt32(dtItem.Rows[k]["T_STATUS"].ToString());
                                endTime = Convert.ToDateTime(dtItem.Rows[k]["T_PERIODTYPE"].ToString());
                            }
                        }
                        else
                        {
                            if (k != dtItem.Rows.Count - 1)
                            {
                                sTime = Convert.ToDateTime(dtItem.Rows[k]["T_STARTTIME"].ToString());
                                type = Convert.ToInt32(dtItem.Rows[k]["T_PERIODTYPE"].ToString());
                                count = Convert.ToInt32(dtItem.Rows[k]["T_PERIODVALUE"].ToString());
                                iState = Convert.ToInt32(dtItem.Rows[k]["T_STATUS"].ToString());
                                endTime = Convert.ToDateTime(dtItem.Rows[k + 1]["T_STARTTIME"].ToString());
                            }
                            else
                            {
                                sTime = Convert.ToDateTime(dtItem.Rows[k]["T_STARTTIME"].ToString());
                                type = Convert.ToInt32(dtItem.Rows[k]["T_PERIODTYPE"].ToString());
                                count = Convert.ToInt32(dtItem.Rows[k]["T_PERIODVALUE"].ToString());
                                iState = Convert.ToInt32(dtItem.Rows[k]["T_STATUS"].ToString());
                                endTime = Convert.ToDateTime(objTime.ToString());
                            }
                        }

                        do
                        {
                            if (type == 1)
                            {
                                eTime = Convert.ToDateTime(sTime.AddDays(count).ToString("yyyy-MM-dd") + " 23:59:59");
                            }
                            else if (type == 2)
                            {
                                int month = 0;
                                month = sTime.AddMonths(count).Month;
                                if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                                    eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "31 23:59:59");
                                else if (month == 4 || month == 6 || month == 9 || month == 11)
                                    eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "30 23:59:59");
                                else
                                    if (sTime.Year % 4 == 0)
                                        eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "29 23:59:59");
                                    else
                                        eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "28 23:59:59");
                            }
                            else
                            {
                                eTime = Convert.ToDateTime(sTime.AddYears(count).ToString("yyyy") + "-12-31 23:59:59");
                            }

                            //判断该条任务是否已经完成(检测过)
                            int status = 0;
                            object obj = DBdb2.RunSingle("select I_STATUS from T_INFO_QUEST where T_SCANTIME between '" + sTime + "' and '" + eTime + "'", out errMsg);
                            if (obj != null)
                                status = 1;

                            //在用
                            if (state == 0)
                                //停机
                                if (iState == 0)
                                    sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + dt.Rows[i]["routeID"] + "','" + dt.Rows[i]["areaID"] + "','" + deviceID + "','" + dt.Rows[i]["itemID"] + "','" + sTime + "','" + eTime + "'," + status + ",0);";
                                else
                                    sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + dt.Rows[i]["routeID"] + "','" + dt.Rows[i]["areaID"] + "','" + deviceID + "','" + dt.Rows[i]["itemID"] + "','" + sTime + "','" + eTime + "'," + status + ",1);";
                            else
                                if (iState == 0)
                                    sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + dt.Rows[i]["routeID"] + "','" + dt.Rows[i]["areaID"] + "','" + deviceID + "','" + dt.Rows[i]["itemID"] + "','" + sTime + "','" + eTime + "'," + status + ",1);";
                                else
                                    sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + dt.Rows[i]["routeID"] + "','" + dt.Rows[i]["areaID"] + "','" + deviceID + "','" + dt.Rows[i]["itemID"] + "','" + sTime + "','" + eTime + "'," + status + ",0);";
                            sTime = eTime.AddSeconds(1);
                        } while (sTime <= endTime);
                    }
                }
                else
                {
                    //获取上一个周期 信息
                    sdt = DBdb2.RunDataTable("select T_STARTTIME,T_PERIODTYPE,T_PERIODVALUE,T_STATUS from T_BASE_ITEM where T_ITEMID='" + dt.Rows[i]["itemID"] + "' and T_STARTTIME < '" + time + "' order by T_STARTTIME desc fetch first 1 rows only;", out errMsg);
                    sTime = time;
                    type = Convert.ToInt32(sdt.Rows[0]["T_PERIODTYPE"].ToString());
                    count = Convert.ToInt32(sdt.Rows[0]["T_PERIODVALUE"].ToString());
                    iState = Convert.ToInt32(sdt.Rows[0]["T_STATUS"].ToString());
                    endTime = Convert.ToDateTime(objTime);

                    do
                    {
                        if (type == 1)
                        {
                            eTime = Convert.ToDateTime(sTime.AddDays(count).ToString("yyyy-MM-dd") + " 23:59:59");
                        }
                        else if (type == 2)
                        {
                            int month = 0;
                            month = sTime.AddMonths(count).Month;
                            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                                eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "31 23:59:59");
                            else if (month == 4 || month == 6 || month == 9 || month == 11)
                                eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "30 23:59:59");
                            else
                                if (sTime.Year % 4 == 0)
                                    eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "29 23:59:59");
                                else
                                    eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "28 23:59:59");
                        }
                        else
                        {
                            eTime = Convert.ToDateTime(sTime.AddYears(count).ToString("yyyy") + "-12-31 23:59:59");
                        }

                        //判断该条任务是否已经完成(检测过)
                        int status = 0;
                        object obj = DBdb2.RunSingle("select I_STATUS from T_INFO_QUEST where T_SCANTIME between '" + sTime + "' and '" + eTime + "'", out errMsg);
                        if (obj != null)
                            status = 1;

                        //在用
                        if (state == 0)
                            //停机
                            if (iState == 0)
                                sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + dt.Rows[i]["routeID"] + "','" + dt.Rows[i]["areaID"] + "','" + deviceID + "','" + dt.Rows[i]["itemID"] + "','" + sTime + "','" + eTime + "'," + status + ",0);";
                            else
                                sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + dt.Rows[i]["routeID"] + "','" + dt.Rows[i]["areaID"] + "','" + deviceID + "','" + dt.Rows[i]["itemID"] + "','" + sTime + "','" + eTime + "'," + status + ",1);";
                        else
                            if (iState == 0)
                                sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + dt.Rows[i]["routeID"] + "','" + dt.Rows[i]["areaID"] + "','" + deviceID + "','" + dt.Rows[i]["itemID"] + "','" + sTime + "','" + eTime + "'," + status + ",1);";
                            else
                                sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + dt.Rows[i]["routeID"] + "','" + dt.Rows[i]["areaID"] + "','" + deviceID + "','" + dt.Rows[i]["itemID"] + "','" + sTime + "','" + eTime + "'," + status + ",0);";
                        sTime = eTime.AddSeconds(1);
                    } while (sTime <= endTime);
                }
            }
            result = DBdb2.RunNonQuery(sql, out errMsg);
            return result;
        }
        #endregion

        #region 添加设备状态
        /// <summary>
        /// 添加设备状态
        /// </summary>
        /// <param name="deviceID">设备编号</param>
        /// <param name="time">添加时间</param>
        /// <returns></returns>
        public bool addDeviceState(string deviceID, DateTime time, int state)
        {
            //获取某个设备下面的所有的点检项 及相关连信息   线路ID  区域ID  设备ID 点检项ID
            sqlStr = "select r.T_NODEID routeID,ar.areaID,ar.deviceID,ar.itemID from T_INFO_ROUTE r inner join (select a.T_NODEID areaID,a.T_PARAENTID,de.deviceID,de.itemID from T_INFO_ROUTE a inner join (select d.T_NODEID deviceID,d.T_PARAENTID,i.T_NODEID itemID from T_INFO_ROUTE d inner join (select T_NODEID,T_PARAENTID from T_INFO_ROUTE where T_PARAENTID in(select T_NODEKEY From T_INFO_ROUTE where T_NODEID='" + deviceID + "')) i on d.T_NODEKEY=i.T_PARAENTID) de on a.T_NODEKEY=de.T_PARAENTID) ar on r.T_NODEKEY=ar.T_PARAENTID;";

            DataTable dt = new DataTable();
            DataTable dtItem = new DataTable();
            dt = DBdb2.RunDataTable(sqlStr, out errMsg);

            object objTime = new object();
            sqlStr = "select T_TIME from T_INFO_DEVICE where T_TIME>'" + time + "' and T_DEVICEID='" + deviceID + "' order by T_TIME asc  fetch first 1 rows only;";
            objTime = DBdb2.RunSingle(sqlStr, out errMsg);

            if (objTime == null)
            {
                objTime = time.AddYears(10);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //删除时间段之内的任务数据
                sqlStr = "delete from T_INFO_QUESTCOMPLETE where T_STARTTIME between '" + time + "' and '" + objTime + "'";
                DBdb2.RunNonQuery(sqlStr, out errMsg);

                //获取点检项周期
                sqlStr = "select T_STARTTIME,T_PERIODTYPE,T_PERIODVALUE,T_STATUS from T_BASE_ITEM where T_ITEMID='" + dt.Rows[i]["itemID"] + "' and T_STARTTIME between '" + time + "' and '" + objTime + "' order by T_STARTTIME asc ";
                dtItem = DBdb2.RunDataTable(sqlStr, out errMsg);

                if (dtItem != null && dtItem.Rows.Count > 0)
                {
                    for (int k = 0; k < dtItem.Rows.Count; k++)
                    {
                        if (k == 0)
                        {
                            sTime = time;

                            if (time != Convert.ToDateTime(dtItem.Rows[k]["T_PERIODTYPE"].ToString()))
                            {
                                //获取上一个周期 信息
                                sdt = DBdb2.RunDataTable("select T_STARTTIME,T_PERIODTYPE,T_PERIODVALUE,T_STATUS from T_BASE_ITEM where T_ITEMID='" + dt.Rows[i]["itemID"] + "' and T_STARTTIME < '" + time + "' order by T_STARTTIME desc fetch first 1 rows only;", out errMsg);
                                type = Convert.ToInt32(sdt.Rows[0]["T_PERIODTYPE"].ToString());
                                count = Convert.ToInt32(sdt.Rows[0]["T_PERIODVALUE"].ToString());
                                iState = Convert.ToInt32(sdt.Rows[0]["T_STATUS"].ToString());
                                endTime = Convert.ToDateTime(dtItem.Rows[k]["T_PERIODTYPE"].ToString());
                            }
                            else
                            {
                                type = Convert.ToInt32(dtItem.Rows[k]["T_PERIODTYPE"].ToString());
                                count = Convert.ToInt32(dtItem.Rows[k]["T_PERIODVALUE"].ToString());
                                iState = Convert.ToInt32(dtItem.Rows[k]["T_STATUS"].ToString());
                                endTime = Convert.ToDateTime(dtItem.Rows[k]["T_PERIODTYPE"].ToString());
                            }
                        }
                        else
                        {
                            if (k != dtItem.Rows.Count - 1)
                            {
                                sTime = Convert.ToDateTime(dtItem.Rows[k]["T_STARTTIME"].ToString());
                                type = Convert.ToInt32(dtItem.Rows[k]["T_PERIODTYPE"].ToString());
                                count = Convert.ToInt32(dtItem.Rows[k]["T_PERIODVALUE"].ToString());
                                iState = Convert.ToInt32(dtItem.Rows[k]["T_STATUS"].ToString());
                                endTime = Convert.ToDateTime(dtItem.Rows[k + 1]["T_STARTTIME"].ToString());
                            }
                            else
                            {
                                sTime = Convert.ToDateTime(dtItem.Rows[k]["T_STARTTIME"].ToString());
                                type = Convert.ToInt32(dtItem.Rows[k]["T_PERIODTYPE"].ToString());
                                count = Convert.ToInt32(dtItem.Rows[k]["T_PERIODVALUE"].ToString());
                                iState = Convert.ToInt32(dtItem.Rows[k]["T_STATUS"].ToString());
                                endTime = Convert.ToDateTime(objTime.ToString());
                            }
                        }

                        do
                        {
                            if (type == 1)
                            {
                                eTime = Convert.ToDateTime(sTime.AddDays(count).ToString("yyyy-MM-dd") + " 23:59:59");
                            }
                            else if (type == 2)
                            {
                                int month = 0;
                                month = sTime.AddMonths(count).Month;
                                if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                                    eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "31 23:59:59");
                                else if (month == 4 || month == 6 || month == 9 || month == 11)
                                    eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "30 23:59:59");
                                else
                                    if (sTime.Year % 4 == 0)
                                        eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "29 23:59:59");
                                    else
                                        eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "28 23:59:59");
                            }
                            else
                            {
                                eTime = Convert.ToDateTime(sTime.AddYears(count).ToString("yyyy") + "-12-31 23:59:59");
                            }

                            //判断该条任务是否已经完成(检测过)
                            int status = 0;
                            object obj = DBdb2.RunSingle("select I_STATUS from T_INFO_QUEST where T_SCANTIME between '" + sTime + "' and '" + eTime + "'", out errMsg);
                            if (obj != null)
                                status = 1;

                            //在用
                            if (state == 0)
                                //停机
                                if (iState == 0)
                                    sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + dt.Rows[i]["routeID"] + "','" + dt.Rows[i]["areaID"] + "','" + deviceID + "','" + dt.Rows[i]["itemID"] + "','" + sTime + "','" + eTime + "'," + status + ",0);";
                                else
                                    sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + dt.Rows[i]["routeID"] + "','" + dt.Rows[i]["areaID"] + "','" + deviceID + "','" + dt.Rows[i]["itemID"] + "','" + sTime + "','" + eTime + "'," + status + ",1);";
                            else
                                if (iState == 0)
                                    sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + dt.Rows[i]["routeID"] + "','" + dt.Rows[i]["areaID"] + "','" + deviceID + "','" + dt.Rows[i]["itemID"] + "','" + sTime + "','" + eTime + "'," + status + ",1);";
                                else
                                    sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + dt.Rows[i]["routeID"] + "','" + dt.Rows[i]["areaID"] + "','" + deviceID + "','" + dt.Rows[i]["itemID"] + "','" + sTime + "','" + eTime + "'," + status + ",0);";
                            sTime = eTime.AddSeconds(1);
                        } while (sTime <= endTime);
                    }
                }
                else
                {
                    //获取上一个周期 信息
                    sdt = DBdb2.RunDataTable("select T_STARTTIME,T_PERIODTYPE,T_PERIODVALUE,T_STATUS from T_BASE_ITEM where T_ITEMID='" + dt.Rows[i]["itemID"] + "' and T_STARTTIME < '" + time + "' order by T_STARTTIME desc fetch first 1 rows only;", out errMsg);
                    sTime = time;
                    type = Convert.ToInt32(sdt.Rows[0]["T_PERIODTYPE"].ToString());
                    count = Convert.ToInt32(sdt.Rows[0]["T_PERIODVALUE"].ToString());
                    iState = Convert.ToInt32(sdt.Rows[0]["T_STATUS"].ToString());
                    endTime = Convert.ToDateTime(objTime);

                    do
                    {
                        if (type == 1)
                        {
                            eTime = Convert.ToDateTime(sTime.AddDays(count).ToString("yyyy-MM-dd") + " 23:59:59");
                        }
                        else if (type == 2)
                        {
                            int month = 0;
                            month = sTime.AddMonths(count).Month;
                            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                                eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "31 23:59:59");
                            else if (month == 4 || month == 6 || month == 9 || month == 11)
                                eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "30 23:59:59");
                            else
                                if (sTime.Year % 4 == 0)
                                    eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "29 23:59:59");
                                else
                                    eTime = Convert.ToDateTime(sTime.AddMonths(count).ToString("yyyy-MM") + "28 23:59:59");
                        }
                        else
                        {
                            eTime = Convert.ToDateTime(sTime.AddYears(count).ToString("yyyy") + "-12-31 23:59:59");
                        }

                        //判断该条任务是否已经完成(检测过)
                        int status = 0;
                        object obj = DBdb2.RunSingle("select I_STATUS from T_INFO_QUEST where T_SCANTIME between '" + sTime + "' and '" + eTime + "'", out errMsg);
                        if (obj != null)
                            status = 1;

                        //在用
                        if (state == 0)
                            //停机
                            if (iState == 0)
                                sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + dt.Rows[i]["routeID"] + "','" + dt.Rows[i]["areaID"] + "','" + deviceID + "','" + dt.Rows[i]["itemID"] + "','" + sTime + "','" + eTime + "'," + status + ",0);";
                            else
                                sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + dt.Rows[i]["routeID"] + "','" + dt.Rows[i]["areaID"] + "','" + deviceID + "','" + dt.Rows[i]["itemID"] + "','" + sTime + "','" + eTime + "'," + status + ",1);";
                        else
                            if (iState == 0)
                                sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + dt.Rows[i]["routeID"] + "','" + dt.Rows[i]["areaID"] + "','" + deviceID + "','" + dt.Rows[i]["itemID"] + "','" + sTime + "','" + eTime + "'," + status + ",1);";
                            else
                                sql += "insert into T_INFO_QUESTCOMPLETE(T_ROUTEID,T_AREAID,T_DEVICEID,T_ITEMID,T_STARTTIME,T_ENDTIME,I_STATUS,I_FLAG) values('" + dt.Rows[i]["routeID"] + "','" + dt.Rows[i]["areaID"] + "','" + deviceID + "','" + dt.Rows[i]["itemID"] + "','" + sTime + "','" + eTime + "'," + status + ",0);";
                        sTime = eTime.AddSeconds(1);
                    } while (sTime <= endTime);
                }
            }
            result = DBdb2.RunNonQuery(sql, out errMsg);
            return result;
        }
        #endregion

        #region 查询点检任务
        /// <summary>
        /// 查询点检任务
        /// </summary>
        /// <param name="userID">人员编号</param>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns></returns>
        public DataTable GetPlans(string userID, DateTime sTime, DateTime eTime)
        {
            DataTable dt = new DataTable();
            //sql = "select pl.ParamentID,pl.RouteID,pl.RouteName,pl.AreaID,pl.AreaName,pl.DeviceID,pl.DeviceName,pl.ItemID,pl.sTime,pl.eTime,pl.state,pl.T_ITEMPOSITION,pl.T_ITEMDESC,pl.T_CONTENT,pl.Types,pl.I_STATUS,pl.T_OBSERVE,pl.T_UNIT,pl.F_LOWER,pl.F_UPPER,pl.I_SPECTRUM,pl.T_STARTTIME,pl.T_PERIODTYPE,pl.T_PERIODVALUE,pl.T_STATUS,rs.T_VALUE,rs.T_SCANTIME,rs.T_UPLOADTIME,case when rs.I_STATUS=1 then '已上报' when rs.I_STATUS=2 then '异常' when rs.I_STATUS=3 then '报缺陷' when rs.I_STATUS=4 then '已解决' else '未上报' end I_STATUS from (select rout.ParamentID,rout.RouteID,rout.RouteName,rout.AreaID,rout.AreaName,rout.DeviceID,rout.DeviceName,rout.ItemID,rout.sTime,rout.eTime,rout.state,i.T_ITEMPOSITION,i.T_ITEMDESC,i.T_CONTENT,case when i.T_TYPE='0' then '点检' else '巡检' end Types,i.I_STATUS,i.T_OBSERVE,i.T_UNIT,i.F_LOWER,i.F_UPPER,case when i.I_SPECTRUM=0 then '不是频谱' else '是频谱' end I_SPECTRUM,i.T_STARTTIME,case when i.T_PERIODTYPE=0 then '日' when i.T_PERIODTYPE=1 then '周' when i.T_PERIODTYPE=2 then '月' else '年' end T_PERIODTYPE,i.T_PERIODVALUE,case when i.T_STATUS=0 then '停机检测' else '起机检测' end T_STATUS from (select rou.ParamentID,rou.RouteID,rou.RouteName,rou.AreaID,rou.AreaName,rou.DeviceID,d.T_DEVICEDESC as DeviceName,rou.ItemID,rou.sTime,rou.eTime,rou.state from (select ro.ParamentID,ro.RouteID,ro.RouteName,ro.AreaID,a.T_AREANAME as AreaName,ro.DeviceID,ro.ItemID,ro.sTime,ro.eTime,ro.state from (select r.T_ORGID as ParamentID,r.T_ROUTEID as RouteID,r.T_ROUTENAME as RouteName,p.T_AREAID as AreaID,p.T_DEVICEID DeviceID,p.T_ITEMID ItemID,p.T_STARTTIME  as sTime,p.T_ENDTIME as eTime,case  when p. I_STATUS=0 then '未完成' else '未完成' end state from (select user.T_ORGID,r.T_ROUTEID,r.T_ROUTENAME from (select T_ORGID from T_SYS_MEMBERRELATION where T_USERID='" + userID + "') user  inner join T_BASE_ROUTE r on user.T_ORGID=r.T_ORGID) r inner join T_INFO_QUESTCOMPLETE p on r.T_ROUTEID=p.T_ROUTEID where p.I_FLAG=1 and p.T_STARTTIME >='" + sTime + "' and p.T_ENDTIME<='" + eTime + "') ro inner join T_BASE_AREA a on ro.AreaID=a.T_AREAID) rou inner join T_BASE_DEVICE d on rou.DeviceID=d.T_DEVICEID) rout inner join T_BASE_ITEM i on rout.ItemID=i.T_ITEMID) pl left join T_INFO_QUEST rs on pl.RouteID=rs.T_ROUTEID and pl.AreaID=rs.T_AREAID and pl.DeviceID=rs.T_DEVICEID and pl.ItemID=rs.T_ITEMID and rs.T_SCANTIME between '" + sTime + "' and '" + eTime + "';";
            sql = "select *from (select F_LOWER,F_UPPER,ID,routeID,routeName,areaID,areaName,deviceID,deviceName,itemID,T_ITEMPOSITION,T_ITEMDESC,T_CONTENT,type,sTime,eTIme,cTime,uTime,value,state,status ,rownumber() over(order by sTime asc ) as rowid from (select rq.F_LOWER,rq.F_UPPER,rq.ID,rq.routeID,rq.routeName,rq.areaID,rq.areaName,rq.deviceID,rq.deviceName,rq.itemID,rq.sTime,rq.eTIme,rq.state,rq.T_ITEMPOSITION,rq.T_ITEMDESC,rq.T_CONTENT,rq.type,q.T_VALUE value,q.T_SCANTIME cTime,q.T_UPLOADTIME uTime,case when q.I_STATUS=1 then '已上报' when q.I_STATUS=2 then '异常' when q.I_STATUS=3 then '报缺陷' when q.I_STATUS=4 then '已解决' end status from (select distinct ri.routeID,ri.routeName,ri.areaID,ri.areaName,ri.deviceID,ri.deviceName,ri.itemID,ri.sTime,ri.eTIme,ri.state,r.T_ITEMPOSITION,r.T_ITEMDESC,r.F_LOWER,r.F_UPPER,r.F_LOWER,r.F_UPPER,r.T_CONTENT,case when r.T_TYPE='0' then '点检' when r.T_TYPE='1' then '巡检' end type  from (select rd.ID,rd.routeID,rd.routeName,rd.areaID,rd.areaName,rd.deviceID,d.T_DEVICEDESC deviceName,rd.itemID,rd.sTime,rd.eTIme,rd.state from (select ra.ID,ra.routeID,ra.routeName, ra.areaID,a.T_AREANAME areaName,ra.deviceID,ra.itemID,ra.sTime,ra.eTIme,ra.state from (select p.ID_KEY ID,p.T_ROUTEID routeID,rt.T_ROUTENAME routeName ,p.T_AREAID areaID,p.T_DEVICEID deviceID,p.T_ITEMID itemID,p.T_STARTTIME sTime,p.T_ENDTIME eTIme,case when p.I_STATUS=0 then '未完成' when p.I_STATUS=1 then '已完成' end state From T_INFO_QUESTCOMPLETE  p inner join (select r.T_ROUTEID,r.T_ROUTENAME from (select T_ORGID from T_SYS_MEMBERRELATION where T_USERID='" + userID + "') user  inner join T_BASE_ROUTE r on user.T_ORGID=r.T_ORGID) rt on p.T_ROUTEID=rt.T_ROUTEID where p.T_STARTTIME between '" + sTime + "' and '" + eTime + "') ra inner join T_BASE_AREA a on ra.areaID=a.T_AREAID) rd inner join T_BASE_DEVICE d on rd.deviceID=d.T_DEVICEID) ri inner join T_BASE_ITEM r on ri.itemID=T_ITEMID ) rq left join T_INFO_QUEST q on rq.routeID=q.T_ROUTEID and rq.areaID=q.T_AREAID and rq.deviceID=q.T_DEVICEID and rq.itemID=q.T_ITEMID and (q.T_SCANTIME between rq.sTime and rq.eTIme)) as rou) plan;";
            dt = DBdb2.RunDataTable(sql, out errMsg);
            return dt;
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
            DataTable dt = new DataTable();
            //sql = "select *From (select ID,ParamentID,RouteID,RouteName,AreaID,AreaName,DeviceID,DeviceName,ItemID,sTime,eTime,state,T_ITEMPOSITION,T_ITEMDESC,T_CONTENT,Types,I_STATUS,T_OBSERVE,T_UNIT,F_LOWER,F_UPPER,I_SPECTRUM,T_STARTTIME,T_PERIODTYPE,T_PERIODVALUE,T_STATUS,T_VALUE,T_SCANTIME,T_UPLOADTIME,I_STATUSS,rownumber() over(order by RouteID asc ) as rowid  from (select pl.ID,pl.ParamentID,pl.RouteID,pl.RouteName,pl.AreaID,pl.AreaName,pl.DeviceID,pl.DeviceName,pl.ItemID,pl.sTime,pl.eTime,pl.state,pl.T_ITEMPOSITION,pl.T_ITEMDESC,pl.T_CONTENT,pl.Types,pl.I_STATUS,pl.T_OBSERVE,pl.T_UNIT,pl.F_LOWER,pl.F_UPPER,pl.I_SPECTRUM,pl.T_STARTTIME,pl.T_PERIODTYPE,pl.T_PERIODVALUE,pl.T_STATUS,rs.T_VALUE,rs.T_SCANTIME,rs.T_UPLOADTIME,case when rs.I_STATUS=1 then '已上报' when rs.I_STATUS=2 then '异常' when rs.I_STATUS=3 then '报缺陷' when rs.I_STATUS=4 then '已解决' else '未上报' end I_STATUSS from (select rout.ID,rout.ParamentID,rout.RouteID,rout.RouteName,rout.AreaID,rout.AreaName,rout.DeviceID,rout.DeviceName,rout.ItemID,rout.sTime,rout.eTime,rout.state,i.T_ITEMPOSITION,i.T_ITEMDESC,i.T_CONTENT,case when i.T_TYPE='0' then '点检' else '巡检' end Types,i.I_STATUS,i.T_OBSERVE,i.T_UNIT,i.F_LOWER,i.F_UPPER,case when i.I_SPECTRUM=0 then '不是频谱' else '是频谱' end I_SPECTRUM,i.T_STARTTIME,case when i.T_PERIODTYPE=0 then '日' when i.T_PERIODTYPE=1 then '周' when i.T_PERIODTYPE=2 then '月' else '年' end T_PERIODTYPE,i.T_PERIODVALUE,case when i.T_STATUS=0 then '停机检测' else '起机检测' end T_STATUS from (select rou.ID,rou.ParamentID,rou.RouteID,rou.RouteName,rou.AreaID,rou.AreaName,rou.DeviceID,d.T_DEVICEDESC as DeviceName,rou.ItemID,rou.sTime,rou.eTime,rou.state from (select ro.ID,ro.ParamentID,ro.RouteID,ro.RouteName,ro.AreaID,a.T_AREANAME as AreaName,ro.DeviceID,ro.ItemID,ro.sTime,ro.eTime,ro.state from (select p.ID_KEY ID,r.T_ORGID as ParamentID,r.T_ROUTEID as RouteID,r.T_ROUTENAME as RouteName,p.T_AREAID as AreaID,p.T_DEVICEID DeviceID,p.T_ITEMID ItemID,p.T_STARTTIME  as sTime,p.T_ENDTIME as eTime,case  when p. I_STATUS=0 then '未完成' else '未完成' end state from (select user.T_ORGID,r.T_ROUTEID,r.T_ROUTENAME from (select T_ORGID from T_SYS_MEMBERRELATION where T_USERID='" + userID + "') user  inner join T_BASE_ROUTE r on user.T_ORGID=r.T_ORGID)r inner join T_INFO_QUESTCOMPLETE p on r.T_ROUTEID=p.T_ROUTEID where p.I_FLAG=1 and p.T_STARTTIME >='" + sTime + "' and p.T_ENDTIME<='" + eTime + "') ro inner join T_BASE_AREA a on ro.AreaID=a.T_AREAID) rou inner join T_BASE_DEVICE d on rou.DeviceID=d.T_DEVICEID) rout inner join T_BASE_ITEM i on rout.ItemID=i.T_ITEMID) pl left join T_INFO_QUEST rs on pl.RouteID=rs.T_ROUTEID and pl.AreaID=rs.T_AREAID and pl.DeviceID=rs.T_DEVICEID and pl.ItemID=rs.T_ITEMID and rs.T_SCANTIME between '" + sTime + "' and '" + eTime + "' order by RouteID desc) sdf)  as a where a.rowid between " + sCount + " and " + eCount + ";";
            sql = "select *from (select F_LOWER,F_UPPER,ID,routeID,routeName,areaID,areaName,deviceID,deviceName,itemID,T_ITEMPOSITION,T_ITEMDESC,T_CONTENT,type,sTime,eTIme,cTime,uTime,value,state,status ,rownumber() over(order by sTime asc ) as rowid from (select rq.F_LOWER,rq.F_UPPER,rq.ID,rq.routeID,rq.routeName,rq.areaID,rq.areaName,rq.deviceID,rq.deviceName,rq.itemID,rq.sTime,rq.eTIme,rq.state,rq.T_ITEMPOSITION,rq.T_ITEMDESC,rq.T_CONTENT,rq.type,q.T_VALUE value,q.T_SCANTIME cTime,q.T_UPLOADTIME uTime,case when q.I_STATUS=1 then '已上报' when q.I_STATUS=2 then '异常' when q.I_STATUS=3 then '报缺陷' when q.I_STATUS=4 then '已解决' end status from (select distinct ri.ID,ri.routeID,ri.routeName,ri.areaID,ri.areaName,ri.deviceID,ri.deviceName,ri.itemID,ri.sTime,ri.eTIme,ri.state,r.T_ITEMPOSITION,r.T_ITEMDESC,r.F_LOWER,r.F_UPPER,r.T_CONTENT,case when r.T_TYPE='0' then '点检' when r.T_TYPE='1' then '巡检' end type  from (select rd.ID,rd.routeID,rd.routeName,rd.areaID,rd.areaName,rd.deviceID,d.T_DEVICEDESC deviceName,rd.itemID,rd.sTime,rd.eTIme,rd.state from (select ra.ID,ra.routeID,ra.routeName, ra.areaID,a.T_AREANAME areaName,ra.deviceID,ra.itemID,ra.sTime,ra.eTIme,ra.state from (select p.ID_KEY ID,p.T_ROUTEID routeID,rt.T_ROUTENAME routeName ,p.T_AREAID areaID,p.T_DEVICEID deviceID,p.T_ITEMID itemID,p.T_STARTTIME sTime,p.T_ENDTIME eTIme,case when p.I_STATUS=0 then '未完成' when p.I_STATUS=1 then '已完成' end state From T_INFO_QUESTCOMPLETE  p inner join (select r.T_ROUTEID,r.T_ROUTENAME from (select T_ORGID from T_SYS_MEMBERRELATION where T_USERID='" + userID + "') user  inner join T_BASE_ROUTE r on user.T_ORGID=r.T_ORGID) rt on p.T_ROUTEID=rt.T_ROUTEID where p.T_STARTTIME between '" + sTime + "' and '" + eTime + "') ra inner join T_BASE_AREA a on ra.areaID=a.T_AREAID) rd inner join T_BASE_DEVICE d on rd.deviceID=d.T_DEVICEID) ri inner join T_BASE_ITEM r on ri.itemID=T_ITEMID ) rq left join T_INFO_QUEST q on rq.routeID=q.T_ROUTEID and rq.areaID=q.T_AREAID and rq.deviceID=q.T_DEVICEID and rq.itemID=q.T_ITEMID and (q.T_SCANTIME between rq.sTime and rq.eTIme)) as rou) plan where plan.rowid between " + sCount + " and " + eCount + ";";
            dt = DBdb2.RunDataTable(sql, out errMsg);
            return dt;
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
            DataTable dt = new DataTable();
            //sql = "select count(*) From (select ID,ParamentID,RouteID,RouteName,AreaID,AreaName,DeviceID,DeviceName,ItemID,sTime,eTime,state,T_ITEMPOSITION,T_ITEMDESC,T_CONTENT,Types,I_STATUS,T_OBSERVE,T_UNIT,F_LOWER,F_UPPER,I_SPECTRUM,T_STARTTIME,T_PERIODTYPE,T_PERIODVALUE,T_STATUS,T_VALUE,T_SCANTIME,T_UPLOADTIME,I_STATUSS,rownumber() over(order by RouteID asc ) as rowid  from (select pl.ID,pl.ParamentID,pl.RouteID,pl.RouteName,pl.AreaID,pl.AreaName,pl.DeviceID,pl.DeviceName,pl.ItemID,pl.sTime,pl.eTime,pl.state,pl.T_ITEMPOSITION,pl.T_ITEMDESC,pl.T_CONTENT,pl.Types,pl.I_STATUS,pl.T_OBSERVE,pl.T_UNIT,pl.F_LOWER,pl.F_UPPER,pl.I_SPECTRUM,pl.T_STARTTIME,pl.T_PERIODTYPE,pl.T_PERIODVALUE,pl.T_STATUS,rs.T_VALUE,rs.T_SCANTIME,rs.T_UPLOADTIME,case when rs.I_STATUS=1 then '已上报' when rs.I_STATUS=2 then '异常' when rs.I_STATUS=3 then '报缺陷' when rs.I_STATUS=4 then '已解决' else '未上报' end I_STATUSS from (select rout.ID,rout.ParamentID,rout.RouteID,rout.RouteName,rout.AreaID,rout.AreaName,rout.DeviceID,rout.DeviceName,rout.ItemID,rout.sTime,rout.eTime,rout.state,i.T_ITEMPOSITION,i.T_ITEMDESC,i.T_CONTENT,case when i.T_TYPE='0' then '点检' else '巡检' end Types,i.I_STATUS,i.T_OBSERVE,i.T_UNIT,i.F_LOWER,i.F_UPPER,case when i.I_SPECTRUM=0 then '不是频谱' else '是频谱' end I_SPECTRUM,i.T_STARTTIME,case when i.T_PERIODTYPE=0 then '日' when i.T_PERIODTYPE=1 then '周' when i.T_PERIODTYPE=2 then '月' else '年' end T_PERIODTYPE,i.T_PERIODVALUE,case when i.T_STATUS=0 then '停机检测' else '起机检测' end T_STATUS from (select rou.ID,rou.ParamentID,rou.RouteID,rou.RouteName,rou.AreaID,rou.AreaName,rou.DeviceID,d.T_DEVICEDESC as DeviceName,rou.ItemID,rou.sTime,rou.eTime,rou.state from (select ro.ID,ro.ParamentID,ro.RouteID,ro.RouteName,ro.AreaID,a.T_AREANAME as AreaName,ro.DeviceID,ro.ItemID,ro.sTime,ro.eTime,ro.state from (select p.ID_KEY ID,r.T_ORGID as ParamentID,r.T_ROUTEID as RouteID,r.T_ROUTENAME as RouteName,p.T_AREAID as AreaID,p.T_DEVICEID DeviceID,p.T_ITEMID ItemID,p.T_STARTTIME  as sTime,p.T_ENDTIME as eTime,case  when p. I_STATUS=0 then '未完成' else '未完成' end state from (select user.T_ORGID,r.T_ROUTEID,r.T_ROUTENAME from (select T_ORGID from T_SYS_MEMBERRELATION where T_USERID='" + userID + "') user  inner join T_BASE_ROUTE r on user.T_ORGID=r.T_ORGID)r inner join T_INFO_QUESTCOMPLETE p on r.T_ROUTEID=p.T_ROUTEID where p.I_FLAG=1 and p.T_STARTTIME >='" + sTime + "' and p.T_ENDTIME<='" + eTime + "') ro inner join T_BASE_AREA a on ro.AreaID=a.T_AREAID) rou inner join T_BASE_DEVICE d on rou.DeviceID=d.T_DEVICEID) rout inner join T_BASE_ITEM i on rout.ItemID=i.T_ITEMID) pl left join T_INFO_QUEST rs on pl.RouteID=rs.T_ROUTEID and pl.AreaID=rs.T_AREAID and pl.DeviceID=rs.T_DEVICEID and pl.ItemID=rs.T_ITEMID and rs.T_SCANTIME between '" + sTime + "' and '" + eTime + "' order by RouteID desc) sdf)  as a;";
            sql = "select count(*) from (select F_LOWER,F_UPPER,ID,routeID,routeName,areaID,areaName,deviceID,deviceName,itemID,T_ITEMPOSITION,T_ITEMDESC,T_CONTENT,type,sTime,eTIme,cTime,uTime,value,state,status ,rownumber() over(order by sTime asc ) as rowid from (select rq.F_LOWER,rq.F_UPPER,rq.ID,rq.routeID,rq.routeName,rq.areaID,rq.areaName,rq.deviceID,rq.deviceName,rq.itemID,rq.sTime,rq.eTIme,rq.state,rq.T_ITEMPOSITION,rq.T_ITEMDESC,rq.T_CONTENT,rq.type,q.T_VALUE value,q.T_SCANTIME cTime,q.T_UPLOADTIME uTime,case when q.I_STATUS=1 then '已上报' when q.I_STATUS=2 then '异常' when q.I_STATUS=3 then '报缺陷' when q.I_STATUS=4 then '已解决' end status from (select distinct ri.ID,ri.routeID,ri.routeName,ri.areaID,ri.areaName,ri.deviceID,ri.deviceName,ri.itemID,ri.sTime,ri.eTIme,ri.state,r.T_ITEMPOSITION,r.T_ITEMDESC,r.F_LOWER,r.F_UPPER,r.T_CONTENT,case when r.T_TYPE='0' then '点检' when r.T_TYPE='1' then '巡检' end type  from (select rd.ID,rd.routeID,rd.routeName,rd.areaID,rd.areaName,rd.deviceID,d.T_DEVICEDESC deviceName,rd.itemID,rd.sTime,rd.eTIme,rd.state from (select ra.ID,ra.routeID,ra.routeName, ra.areaID,a.T_AREANAME areaName,ra.deviceID,ra.itemID,ra.sTime,ra.eTIme,ra.state from (select p.ID_KEY ID,p.T_ROUTEID routeID,rt.T_ROUTENAME routeName ,p.T_AREAID areaID,p.T_DEVICEID deviceID,p.T_ITEMID itemID,p.T_STARTTIME sTime,p.T_ENDTIME eTIme,case when p.I_STATUS=0 then '未完成' when p.I_STATUS=1 then '已完成' end state From T_INFO_QUESTCOMPLETE  p inner join (select r.T_ROUTEID,r.T_ROUTENAME from (select T_ORGID from T_SYS_MEMBERRELATION where T_USERID='" + userID + "') user  inner join T_BASE_ROUTE r on user.T_ORGID=r.T_ORGID) rt on p.T_ROUTEID=rt.T_ROUTEID where p.T_STARTTIME between '" + sTime + "' and '" + eTime + "') ra inner join T_BASE_AREA a on ra.areaID=a.T_AREAID) rd inner join T_BASE_DEVICE d on rd.deviceID=d.T_DEVICEID) ri inner join T_BASE_ITEM r on ri.itemID=T_ITEMID ) rq left join T_INFO_QUEST q on rq.routeID=q.T_ROUTEID and rq.areaID=q.T_AREAID and rq.deviceID=q.T_DEVICEID and rq.itemID=q.T_ITEMIDand and (q.T_SCANTIME between rq.sTime and rq.eTIme)) as rou) plan;";
            int count = DBdb2.RunRowCount(sql, out errMsg);
            return count;
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
            DataTable dt = new DataTable();
            string sqlWhe = "";
            if (routeID != "0")
                sqlWhe = " p.T_ROUTEID='" + routeID + "'";

            if (areaID != "0")
                if (sqlWhe != "")
                    sqlWhe += " and p.T_AREAID='" + areaID + "'";
                else
                    sqlWhe = " p.T_AREAID='" + areaID + "'";

            if (deviceID != "0")
                if (sqlWhe != "")
                    sqlWhe += "and p.T_DEVICEID='1010-24LBA50' ";
                else
                    sqlWhe = " p.T_DEVICEID='" + deviceID + "' ";
            if (sqlWhe != "")
                sql = "select *from (select F_LOWER,F_UPPER,ID,routeID,routeName,areaID,areaName,deviceID,deviceName,itemID,T_ITEMPOSITION,T_ITEMDESC,T_CONTENT,type,sTime,eTIme,cTime,uTime,value,state,status ,rownumber() over(order by sTime asc ) as rowid from (select rq.F_LOWER,rq.F_UPPER,rq.ID,rq.routeID,rq.routeName,rq.areaID,rq.areaName,rq.deviceID,rq.deviceName,rq.itemID,rq.sTime,rq.eTIme,rq.state,rq.T_ITEMPOSITION,rq.T_ITEMDESC,rq.T_CONTENT,rq.type,q.T_VALUE value,q.T_SCANTIME cTime,q.T_UPLOADTIME uTime,case when q.I_STATUS=1 then '已上报' when q.I_STATUS=2 then '异常' when q.I_STATUS=3 then '报缺陷' when q.I_STATUS=4 then '已解决' end status from (select distinct ri.ID,ri.routeID,ri.routeName,ri.areaID,ri.areaName,ri.deviceID,ri.deviceName,ri.itemID,ri.sTime,ri.eTIme,ri.state,r.T_ITEMPOSITION,r.T_ITEMDESC,r.F_LOWER,r.F_UPPER,r.T_CONTENT,case when r.T_TYPE='0' then '点检' when r.T_TYPE='1' then '巡检' end type  from (select rd.ID,rd.routeID,rd.routeName,rd.areaID,rd.areaName,rd.deviceID,d.T_DEVICEDESC deviceName,rd.itemID,rd.sTime,rd.eTIme,rd.state from (select ra.ID,ra.routeID,ra.routeName, ra.areaID,a.T_AREANAME areaName,ra.deviceID,ra.itemID,ra.sTime,ra.eTIme,ra.state from (select p.ID_KEY ID,p.T_ROUTEID routeID,rt.T_ROUTENAME routeName ,p.T_AREAID areaID,p.T_DEVICEID deviceID,p.T_ITEMID itemID,p.T_STARTTIME sTime,p.T_ENDTIME eTIme,case when p.I_STATUS=0 then '未完成' when p.I_STATUS=1 then '已完成' end state From T_INFO_QUESTCOMPLETE  p inner join (select r.T_ROUTEID,r.T_ROUTENAME from (select T_ORGID from T_SYS_MEMBERRELATION where T_USERID='" + userID + "') user  inner join T_BASE_ROUTE r on user.T_ORGID=r.T_ORGID) rt on p.T_ROUTEID=rt.T_ROUTEID where " + sqlWhe + " and p.T_STARTTIME between '" + sTime + "' and '" + eTime + "') ra inner join T_BASE_AREA a on ra.areaID=a.T_AREAID) rd inner join T_BASE_DEVICE d on rd.deviceID=d.T_DEVICEID) ri inner join T_BASE_ITEM r on ri.itemID=T_ITEMID ) rq left join T_INFO_QUEST q on rq.routeID=q.T_ROUTEID and rq.areaID=q.T_AREAID and rq.deviceID=q.T_DEVICEID and rq.itemID=q.T_ITEMID and (q.T_SCANTIME between rq.sTime and rq.eTIme)) as rou) plan where plan.rowid between " + sCount + " and " + eCount + ";";
            else
                sql = "select *from (select F_LOWER,F_UPPER,ID,routeID,routeName,areaID,areaName,deviceID,deviceName,itemID,T_ITEMPOSITION,T_ITEMDESC,T_CONTENT,type,sTime,eTIme,cTime,uTime,value,state,status ,rownumber() over(order by sTime asc ) as rowid from (select rq.F_LOWER,rq.F_UPPER,rq.ID,rq.routeID,rq.routeName,rq.areaID,rq.areaName,rq.deviceID,rq.deviceName,rq.itemID,rq.sTime,rq.eTIme,rq.state,rq.T_ITEMPOSITION,rq.T_ITEMDESC,rq.T_CONTENT,rq.type,q.T_VALUE value,q.T_SCANTIME cTime,q.T_UPLOADTIME uTime,case when q.I_STATUS=1 then '已上报' when q.I_STATUS=2 then '异常' when q.I_STATUS=3 then '报缺陷' when q.I_STATUS=4 then '已解决' end status from (select distinct ri.ID,ri.routeID,ri.routeName,ri.areaID,ri.areaName,ri.deviceID,ri.deviceName,ri.itemID,ri.sTime,ri.eTIme,ri.state,r.T_ITEMPOSITION,r.T_ITEMDESC,r.F_LOWER,r.F_UPPER,r.T_CONTENT,case when r.T_TYPE='0' then '点检' when r.T_TYPE='1' then '巡检' end type  from (select rd.ID,rd.routeID,rd.routeName,rd.areaID,rd.areaName,rd.deviceID,d.T_DEVICEDESC deviceName,rd.itemID,rd.sTime,rd.eTIme,rd.state from (select ra.ID,ra.routeID,ra.routeName, ra.areaID,a.T_AREANAME areaName,ra.deviceID,ra.itemID,ra.sTime,ra.eTIme,ra.state from (select p.ID_KEY ID,p.T_ROUTEID routeID,rt.T_ROUTENAME routeName ,p.T_AREAID areaID,p.T_DEVICEID deviceID,p.T_ITEMID itemID,p.T_STARTTIME sTime,p.T_ENDTIME eTIme,case when p.I_STATUS=0 then '未完成' when p.I_STATUS=1 then '已完成' end state From T_INFO_QUESTCOMPLETE  p inner join (select r.T_ROUTEID,r.T_ROUTENAME from (select T_ORGID from T_SYS_MEMBERRELATION where T_USERID='" + userID + "') user  inner join T_BASE_ROUTE r on user.T_ORGID=r.T_ORGID) rt on p.T_ROUTEID=rt.T_ROUTEID where p.T_STARTTIME between '" + sTime + "' and '" + eTime + "') ra inner join T_BASE_AREA a on ra.areaID=a.T_AREAID) rd inner join T_BASE_DEVICE d on rd.deviceID=d.T_DEVICEID) ri inner join T_BASE_ITEM r on ri.itemID=T_ITEMID ) rq left join T_INFO_QUEST q on rq.routeID=q.T_ROUTEID and rq.areaID=q.T_AREAID and rq.deviceID=q.T_DEVICEID and rq.itemID=q.T_ITEMID and (q.T_SCANTIME between rq.sTime and rq.eTIme)) as rou) plan where plan.rowid between " + sCount + " and " + eCount + ";";

            dt = DBdb2.RunDataTable(sql, out errMsg);
            return dt;
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
            DataTable dt = new DataTable();
            string sqlWhe = "";
            if (routeID != "0")
                sqlWhe = " p.T_ROUTEID='" + routeID + "'";

            if (areaID != "0")
                if (sqlWhe != "")
                    sqlWhe += " and p.T_AREAID='" + areaID + "'";
                else
                    sqlWhe = " p.T_AREAID='" + areaID + "'";

            if (deviceID != "0")
                if (sqlWhe != "")
                    sqlWhe += "and p.T_DEVICEID='1010-24LBA50' ";
                else
                    sqlWhe = " p.T_DEVICEID='" + deviceID + "' ";

            if (sqlWhe != "")
                sql = "select count(*) from (select F_LOWER,F_UPPER,ID,routeID,routeName,areaID,areaName,deviceID,deviceName,itemID,T_ITEMPOSITION,T_ITEMDESC,T_CONTENT,type,sTime,eTIme,cTime,uTime,value,state,status ,rownumber() over(order by sTime asc ) as rowid from (select rq.F_LOWER,rq.F_UPPER,rq.ID,rq.routeID,rq.routeName,rq.areaID,rq.areaName,rq.deviceID,rq.deviceName,rq.itemID,rq.sTime,rq.eTIme,rq.state,rq.T_ITEMPOSITION,rq.T_ITEMDESC,rq.T_CONTENT,rq.type,q.T_VALUE value,q.T_SCANTIME cTime,q.T_UPLOADTIME uTime,case when q.I_STATUS=1 then '已上报' when q.I_STATUS=2 then '异常' when q.I_STATUS=3 then '报缺陷' when q.I_STATUS=4 then '已解决' end status from (select distinct ri.ID,ri.routeID,ri.routeName,ri.areaID,ri.areaName,ri.deviceID,ri.deviceName,ri.itemID,ri.sTime,ri.eTIme,ri.state,r.T_ITEMPOSITION,r.T_ITEMDESC,r.F_LOWER,r.F_UPPER,r.T_CONTENT,case when r.T_TYPE='0' then '点检' when r.T_TYPE='1' then '巡检' end type  from (select rd.ID,rd.routeID,rd.routeName,rd.areaID,rd.areaName,rd.deviceID,d.T_DEVICEDESC deviceName,rd.itemID,rd.sTime,rd.eTIme,rd.state from (select ra.ID,ra.routeID,ra.routeName, ra.areaID,a.T_AREANAME areaName,ra.deviceID,ra.itemID,ra.sTime,ra.eTIme,ra.state from (select p.ID_KEY ID,p.T_ROUTEID routeID,rt.T_ROUTENAME routeName ,p.T_AREAID areaID,p.T_DEVICEID deviceID,p.T_ITEMID itemID,p.T_STARTTIME sTime,p.T_ENDTIME eTIme,case when p.I_STATUS=0 then '未完成' when p.I_STATUS=1 then '已完成' end state From T_INFO_QUESTCOMPLETE  p inner join (select r.T_ROUTEID,r.T_ROUTENAME from (select T_ORGID from T_SYS_MEMBERRELATION where T_USERID='" + userID + "') user  inner join T_BASE_ROUTE r on user.T_ORGID=r.T_ORGID) rt on p.T_ROUTEID=rt.T_ROUTEID where " + sqlWhe + " and p.T_STARTTIME between '" + sTime + "' and '" + eTime + "') ra inner join T_BASE_AREA a on ra.areaID=a.T_AREAID) rd inner join T_BASE_DEVICE d on rd.deviceID=d.T_DEVICEID) ri inner join T_BASE_ITEM r on ri.itemID=T_ITEMID ) rq left join T_INFO_QUEST q on rq.routeID=q.T_ROUTEID and rq.areaID=q.T_AREAID and rq.deviceID=q.T_DEVICEID and rq.itemID=q.T_ITEMID and (q.T_SCANTIME between rq.sTime and rq.eTIme)) as rou) plan;";
            else
                sql = "select count(*) from (select F_LOWER,F_UPPER,ID,routeID,routeName,areaID,areaName,deviceID,deviceName,itemID,T_ITEMPOSITION,T_ITEMDESC,T_CONTENT,type,sTime,eTIme,cTime,uTime,value,state,status ,rownumber() over(order by sTime asc ) as rowid from (select rq.F_LOWER,rq.F_UPPER,rq.ID,rq.routeID,rq.routeName,rq.areaID,rq.areaName,rq.deviceID,rq.deviceName,rq.itemID,rq.sTime,rq.eTIme,rq.state,rq.T_ITEMPOSITION,rq.T_ITEMDESC,rq.T_CONTENT,rq.type,q.T_VALUE value,q.T_SCANTIME cTime,q.T_UPLOADTIME uTime,case when q.I_STATUS=1 then '已上报' when q.I_STATUS=2 then '异常' when q.I_STATUS=3 then '报缺陷' when q.I_STATUS=4 then '已解决' end status from (select distinct ri.ID,ri.routeID,ri.routeName,ri.areaID,ri.areaName,ri.deviceID,ri.deviceName,ri.itemID,ri.sTime,ri.eTIme,ri.state,r.T_ITEMPOSITION,r.T_ITEMDESC,r.F_LOWER,r.F_UPPER,r.T_CONTENT,case when r.T_TYPE='0' then '点检' when r.T_TYPE='1' then '巡检' end type  from (select rd.ID,rd.routeID,rd.routeName,rd.areaID,rd.areaName,rd.deviceID,d.T_DEVICEDESC deviceName,rd.itemID,rd.sTime,rd.eTIme,rd.state from (select ra.ID,ra.routeID,ra.routeName, ra.areaID,a.T_AREANAME areaName,ra.deviceID,ra.itemID,ra.sTime,ra.eTIme,ra.state from (select p.ID_KEY ID,p.T_ROUTEID routeID,rt.T_ROUTENAME routeName ,p.T_AREAID areaID,p.T_DEVICEID deviceID,p.T_ITEMID itemID,p.T_STARTTIME sTime,p.T_ENDTIME eTIme,case when p.I_STATUS=0 then '未完成' when p.I_STATUS=1 then '已完成' end state From T_INFO_QUESTCOMPLETE  p inner join (select r.T_ROUTEID,r.T_ROUTENAME from (select T_ORGID from T_SYS_MEMBERRELATION where T_USERID='" + userID + "') user  inner join T_BASE_ROUTE r on user.T_ORGID=r.T_ORGID) rt on p.T_ROUTEID=rt.T_ROUTEID where p.T_STARTTIME between '" + sTime + "' and '" + eTime + "') ra inner join T_BASE_AREA a on ra.areaID=a.T_AREAID) rd inner join T_BASE_DEVICE d on rd.deviceID=d.T_DEVICEID) ri inner join T_BASE_ITEM r on ri.itemID=T_ITEMID ) rq left join T_INFO_QUEST q on rq.routeID=q.T_ROUTEID and rq.areaID=q.T_AREAID and rq.deviceID=q.T_DEVICEID and rq.itemID=q.T_ITEMID and (q.T_SCANTIME between rq.sTime and rq.eTIme)) as rou) plan;";

            int count = DBdb2.RunRowCount(sql, out errMsg);
            return count;
        }
        #endregion

        #region 修改点检结果
        public bool EditPlanResult(string routeId, string areaId, string deviceId, string itemId, int state, string value, string time)
        {
            sql = "update T_INFO_QUEST set I_STATUS=" + state + ",T_VALUE='" + value + "'  where T_ROUTEID='" + routeId + "' and T_AREAID='" + areaId + "' and T_DEVICEID='" + deviceId + "' and  T_ITEMID='" + itemId + "' and T_SCANTIME='" + time + "';";
            result = DBdb2.RunNonQuery(sql, out errMsg);
            return result;
        }
        #endregion

    }
}