using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace Bussiness
{
    /// <summary>
    /// 试验报告类
    /// </summary>
    public class ThermalTestReport
    {
        /// <summary>
        /// 试验报告标识号
        /// </summary>
        public int ReportID { get; set; }
        /// <summary>
        /// 模板标识号
        /// </summary>
        public int TESTTemplateID { get; set; }
        /// <summary>
        /// 试验条件
        /// </summary>
        public string TESTCondition { get; set; }
        /// <summary>
        /// 机组唯一ID
        /// </summary>
        public string UNITID { get; set; }
        /// <summary>
        /// 热力试验报告名称
        /// </summary>
        public string ReportName { get; set; }
        /// <summary>
        /// 热力试验人
        /// </summary>
        public string Tester { get; set; }
        /// <summary>
        /// 试验起始时间
        /// </summary>
        public DateTime TestBegin { get; set; }
        /// <summary>
        /// 试验时长
        /// </summary>
        public int TestDuration { get; set; }
        /// <summary>
        /// 采样间隔
        /// </summary>
        public int SampleInterval { get; set; }
        /// <summary>
        /// 开始试验时间
        /// </summary>
        public DateTime TestCalBegin { get; set; }
        /// <summary>
        /// 试验结束时间
        /// </summary>
        public DateTime TestCalEnd { get; set; }
        /// <summary>
        /// 试验已经进行到的时间
        /// </summary>
        public DateTime CurTestTime { get; set; }
        /// <summary>
        /// 热力试验模块更新此状态
        /// </summary>
        public enum TestState
        {
            /// <summary>
            /// 初始化
            /// </summary>
            Init = 0,
            /// <summary>
            /// /// <summary>
            /// 完成
            /// </summary>
            Finish = 1,
            /// 采样
            /// </summary>
            Collection = 2,
            /// <summary>
            /// 试验
            /// </summary>
            Test = 3,            
            /// <summary>
            /// 终止
            /// </summary>
            End = 4
        }
        /// <summary>
        /// 用户状态
        /// </summary>
        public enum UserState
        {

            /// <summary>
            /// 完成
            /// </summary>
            Finish = 4,
            /// <summary>
            /// 中止
            /// </summary>
            End = 5,
            /// <summary>
            /// 新建
            /// </summary>
            New = 6,
            /// <summary>
            /// 重算
            /// </summary>
            ReCount = 7,
            /// <summary>
            /// 等待
            /// </summary>
            Wait = 8            
        }
        /// <summary>
        /// 获取最大报告号
        /// </summary>
        /// <returns></returns>
        public int GetMaxReportId()
        {
            int maxReportId=0;
            string sql = "SELECT MAX(REPORTID) FROM ADMINISTRATOR.ThermalTest";
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            try
            {
                object obj = link.GetFirstValue(sql);
                if (obj != null)
                    maxReportId = Convert.ToInt32(link.GetFirstValue(sql));
            }
            catch(Exception ex)
            {
                throw ex;
            }
             
            return maxReportId;
        }
        public DataTable GetTest(int reportId)
        {
            string sql = "SELECT * FROM ADMINISTRATOR.ThermalTest WHERE REPORTID=" + reportId;
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            DataTable dt = link.ExcuteRetureTable(sql);
            return dt;
        }

        public DataTable GetTest(string reportId)
        {
            string sql = "SELECT * FROM ADMINISTRATOR.ThermalTest WHERE REPORTID IN " + reportId;
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            DataTable dt = link.ExcuteRetureTable(sql);
            return dt;
        }

        /// <summary>
        /// 开始新实验
        /// </summary>
        public void InsertTest(UserState userState,ThermalTestReport report)
        {
            //string sql = "INSERT INTO ADMINISTRATOR.ThermalTestReport(TestTemplateID,ReportName,Tester,TestBegin,TestDuration,SampleInterval,TestCalBegin,TestCalEnd,CurTestTime,TestState,UserState) VALUES (";
            //sql += "" + TestTemplateID + ",'" + ReportName + "','" + Tester + "','" + TestBegin + "'," + TestDuration + "," + SampleInterval + ",'" + TestCalBegin + "','" + TestCalEnd + "'," + CurTestTime + ",0,0)";
            string sql = "INSERT INTO ADMINISTRATOR.ThermalTest(TESTTemplateID,TESTCONDITION,ReportName,UNITID,Tester,TestBegin,TestDuration,SampleInterval,TestCalBegin,TestState,UserState) VALUES (";
            if (userState == UserState.New)
                sql += "" + report.TESTTemplateID + ",'" + report.TESTCondition + "','" + report.ReportName + "','" + report.UNITID + "','" + report.Tester + "','" + report.TestBegin.ToString().Replace("/", "-") + "'," + report.TestDuration + "," + report.SampleInterval + ",'" + report.TestCalBegin.ToString().Replace("/", "-") + "',1,6)";
            else if (userState == UserState.Wait)
                sql += "" + report.TESTTemplateID + ",'" + report.TESTCondition + "','" + report.ReportName + "','" + report.UNITID + "','" + report.Tester + "','" + report.TestBegin.ToString().Replace("/", "-") + "'," + report.TestDuration + "," + report.SampleInterval + ",'" + report.TestCalBegin.ToString().Replace("/", "-") + "',1,8)";
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            link.Excute(sql);
        }
        /// <summary>
        /// 主动终止实验
        /// </summary>
        /// <param name="reportName">实验报告名</param>
        public void EndTest(int reportId)
        {
            string sql = "UPDATE ADMINISTRATOR.ThermalTest SET TestState=5,UserState=5 WHERE ReportID=" + reportId + "";
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            link.Excute(sql);
        }
        /// <summary>
        /// 被动终止实验
        /// </summary>
        /// <param name="reportName">实验报告名</param>
        public void BDEndTest(int reportId,int value)
        {
            string sql = "UPDATE ADMINISTRATOR.ThermalTest SET UserState="+value+" WHERE ReportID=" + reportId + "";
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            link.Excute(sql);
        }
        /// <summary>
        /// 重算实验
        /// </summary>
        public void ReCountTest()
        {
            string sql = "UPDATE ADMINISTRATOR.ThermalTest SET UserState=7 WHERE ReportID=" + ReportID + "";
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            link.Excute(sql);
        }

        /// <summary>
        /// 删除实验
        /// </summary>
        /// <param name="reportid"></param>
        public void DeleteTest(int reportid)
        {
            string sql = "DELETE FROM ADMINISTRATOR.ThermalTest WHERE ReportId=" + reportid;
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            link.Excute(sql);
        }
        public DataTable ReadConfig()
        {
            string sql = "SELECT * FROM ADMINISTRATOR.ThermalTest ";
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            DataTable dt = link.ExcuteRetureTable(sql);
            return dt;
        }

        public DateTime GetCurTestTime(int reportId)
        {
            DateTime dTime = new DateTime();
            string sql = "SELECT CurTestTime FROM ADMINISTRATOR.ThermalTest WHERE ReportID=" + reportId + "";
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            DataTable dt = link.ExcuteRetureTable(sql);
            if (dt != null && dt.Rows.Count > 0)
                if (dt.Rows[0][0].ToString() != "")
                    dTime = Convert.ToDateTime(dt.Rows[0][0]);
            return dTime;
        }

        /// <summary>
        /// 获取X值
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public string GetX(int reportId)
        {
            string value = string.Empty;
            string sql = "SELECT VALUE FROM ADMINISTRATOR.THERMALTESTREPORT WHERE REPORTID=" + reportId + " AND PARAID='Rleak_O'";
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            DataTable dt = link.ExcuteRetureTable(sql);
            if (dt != null && dt.Rows.Count > 0)
                if (dt.Rows[0][0].ToString() != "")
                    value = dt.Rows[0][0].ToString();
            return value;
        }

        /// <summary>
        /// 获取Y值
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="templateId"></param>
        /// <param name="testCondition"></param>
        /// <returns></returns>
        public string GetY(string unitId, string templateId, string testCondition)
        {
            string value = string.Empty;
            string sql = "SELECT LEAKTHRESHOLD FROM ADMINISTRATOR.THERMALTESTREQUIREMENT WHERE UNITID='" + unitId + "' and TEMPLATEID=" + templateId + " and TESTCONDITION='"+testCondition+"'";
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            DataTable dt = link.ExcuteRetureTable(sql);
            if (dt != null && dt.Rows.Count > 0)
                if (dt.Rows[0][0].ToString() != "")
                    value = dt.Rows[0][0].ToString();
            return value;
        }

        public double GetDiffTime(int reportId)
        {
            DateTime curTime = GetCurTestTime(reportId);
            
            DateTime startTime = Convert.ToDateTime(TestBegin);
            double ts = 0;
            if (curTime.ToString() != "0001-1-1 0:00:00")
                ts = curTime.Subtract(startTime).TotalMinutes;
            return ts;
        }

        public DataTable GetFinishExpriment(string startTime, string endTime, int id_key)
        {
            string sql = "SELECT ReportID,ReportName FROM ADMINISTRATOR.ThermalTest WHERE TestCalBegin BETWEEN '" + startTime + "' AND '" + endTime + "' AND TESTTemplateID=" + id_key + " AND USERSTATE=4 AND TestState=4";
            if (id_key == 0)
                sql = "SELECT ReportID,ReportName FROM ADMINISTRATOR.ThermalTest WHERE TestCalBegin BETWEEN '" + startTime + "' AND '" + endTime + "' AND USERSTATE=4 AND TestState=4";
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            DataTable dt = link.ExcuteRetureTable(sql);
            return dt;
        }

        public DataTable GetExpriment(string startTime, string endTime, int id_key)
        {
            DataTable dt = null;
            string sql="";
            if (startTime == "")
            {
                if (endTime == "")
                {
                    if (id_key == 0)
                        sql = "SELECT ReportID,ReportName FROM ADMINISTRATOR.ThermalTest WHERE USERSTATE=4 AND TestState=4 ";
                    else
                        sql = "SELECT ReportID,ReportName FROM ADMINISTRATOR.ThermalTest WHERE USERSTATE=4 AND TESTTemplateID=" + id_key + " AND TestState=4 ";
                }
                else
                {
                    if (id_key == 0)
                        sql = "SELECT ReportID,ReportName FROM ADMINISTRATOR.ThermalTest WHERE TestCalBegin <= '" + endTime + "' AND USERSTATE=4 AND TestState=4";
                    else
                        sql = "SELECT ReportID,ReportName FROM ADMINISTRATOR.ThermalTest WHERE TestCalBegin <= '" + endTime + "' AND TESTTemplateID=" + id_key + " AND USERSTATE=4 AND TestState=4";
                }
            }
            else
            {
                if (endTime == "")
                {
                    if (id_key == 0)
                        sql = "SELECT ReportID,ReportName FROM ADMINISTRATOR.ThermalTest WHERE TestCalBegin >= '" + startTime + "' AND USERSTATE=4 AND TestState=4";
                    else
                        sql = "SELECT ReportID,ReportName FROM ADMINISTRATOR.ThermalTest WHERE TestCalBegin >= '" + startTime + "' AND TESTTemplateID=" + id_key + " AND USERSTATE=4 AND TestState=4 ";

                }
                else
                {
                    if (id_key == 0)
                        sql = "SELECT ReportID,ReportName FROM ADMINISTRATOR.ThermalTest WHERE TestCalBegin BETWEEN '" + startTime + "' AND '" + endTime + "' AND USERSTATE=4 AND TestState=4";
                    else
                        sql = "SELECT ReportID,ReportName FROM ADMINISTRATOR.ThermalTest WHERE TestCalBegin BETWEEN '" + startTime + "' AND '" + endTime + "' AND TESTTemplateID=" + id_key + " AND USERSTATE=4 AND TestState=4";
                }
            }
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            dt = link.ExcuteRetureTable(sql);
            
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="id_key">模板</param>
        /// <param name="unitId">机组id</param>
        /// <returns></returns>
        public DataTable GetExpriment(string startTime, string endTime, string id_key,string unitId)
        {
            DataTable dt = null;
            string sql = "";
            if (startTime == "")
            {
                if (endTime == "")
                {
                    if (id_key == "0"||String.IsNullOrEmpty(id_key))
                        sql = "SELECT ReportID,ReportName FROM ADMINISTRATOR.ThermalTest WHERE USERSTATE=4 AND TestState=4 ";
                    else
                        sql = "SELECT ReportID,ReportName FROM ADMINISTRATOR.ThermalTest WHERE USERSTATE=4 AND TESTTemplateID IN (" + id_key + ") AND TestState=4 ";
                }
                else
                {
                    if (id_key == "0" || String.IsNullOrEmpty(id_key))
                        sql = "SELECT ReportID,ReportName FROM ADMINISTRATOR.ThermalTest WHERE TestCalBegin <= '" + endTime + "' AND USERSTATE=4 AND TestState=4";
                    else
                        sql = "SELECT ReportID,ReportName FROM ADMINISTRATOR.ThermalTest WHERE TestCalBegin <= '" + endTime + "' AND TESTTemplateID IN(" + id_key + ") AND USERSTATE=4 AND TestState=4";
                }
            }
            else
            {
                if (endTime == "")
                {
                    if (id_key == "0" || String.IsNullOrEmpty(id_key))
                        sql = "SELECT ReportID,ReportName FROM ADMINISTRATOR.ThermalTest WHERE TestCalBegin >= '" + startTime + "' AND USERSTATE=4 AND TestState=4";
                    else
                        sql = "SELECT ReportID,ReportName FROM ADMINISTRATOR.ThermalTest WHERE TestCalBegin >= '" + startTime + "' AND TESTTemplateID IN(" + id_key + ") AND USERSTATE=4 AND TestState=4 ";

                }
                else
                {
                    if (id_key == "0" || String.IsNullOrEmpty(id_key))
                        sql = "SELECT ReportID,ReportName FROM ADMINISTRATOR.ThermalTest WHERE TestCalBegin BETWEEN '" + startTime + "' AND '" + endTime + "' AND USERSTATE=4 AND TestState=4";
                    else
                        sql = "SELECT ReportID,ReportName FROM ADMINISTRATOR.ThermalTest WHERE TestCalBegin BETWEEN '" + startTime + "' AND '" + endTime + "' AND TESTTemplateID IN(" + id_key + ") AND USERSTATE=4 AND TestState=4";
                }
            }
            if (unitId != "0" || !String.IsNullOrEmpty(unitId))
            {
                sql += "  AND UNITID='"+unitId+"'";
            }
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            dt = link.ExcuteRetureTable(sql);

            return dt;
        }
        /// <summary>
        /// 获取所有完成实验
        /// </summary>
        /// <returns></returns>
        public DataTable GetFinishExpriment()
        {
            string sql = "SELECT ReportID,ReportName,TESTER,TESTCALBEGIN,TESTCALEND FROM ADMINISTRATOR.ThermalTest WHERE TESTSTATE=4 AND USERSTATE=4 ";
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            DataTable dt = link.ExcuteRetureTable(sql);
            return dt;
        }
        /// <summary>
        /// 判断实验是否正在进行
        /// </summary>
        /// <returns></returns>
        public bool IsDoingExpriment()
        {
            bool flag=false;
            string sql = "SELECT * FROM ADMINISTRATOR.ThermalTest WHERE teststate=1 or teststate=2 or teststate=3";
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            DataTable dt = link.ExcuteRetureTable(sql);
            if (dt != null && dt.Rows.Count > 0)
                flag = true;
            return flag;
        }
        /// <summary>
        /// 模糊查询实验报告
        /// </summary>
        /// <param name="reportName">实验名</param>
        /// <returns></returns>
        public DataTable GetReportByLikeName(string reportName)
        {
            if (reportName == "")
                return null;
            string sql = "SELECT ReportName FROM ADMINISTRATOR.ThermalTest WHERE ReportName LIKE '" + reportName + "'%";
            
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            DataTable dt = link.ExcuteRetureTable(sql);
            return dt;
        }
        /// <summary>
        /// 获取相同机组相同实验报告次数
        /// </summary>
        /// <param name="templateID">实验报告编号</param>
        /// <param name="dateTime">实验日期</param>
        /// <returns></returns>
        public int GetReportCount(int templateID,string dateTime)
        {
            string minTime = dateTime + " 00:00:00.0";
            string maxTime = dateTime + " 24:00:00.0";
            int reportCount=0;
            string sql = "SELECT COUNT(*) FROM ADMINISTRATOR.ThermalTest WHERE TESTTemplateID=" + TESTTemplateID + " AND TestCalBegin BETWEEN '" + minTime + "' AND '" + maxTime + "'";
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            object obj = link.GetFirstValue(sql);
            if (obj != null)
                reportCount = Convert.ToInt32(obj) + 1;
            else
                reportCount++;
            return reportCount;
        }

        /// <summary>
        /// 获取热力试验工况
        /// </summary>
       ///<param name="unitId">机组ID</param>
       ///<param name="templateId">模板ID</param>
        /// <returns></returns>
        public DataTable GetThermalTestRequirement(string unitId, string templateId)
        {
            string sql = "SELECT * FROM ADMINISTRATOR.THERMALTESTREQUIREMENT WHERE UNITID='" + unitId + "' and TEMPLATEID=" + templateId + "";
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            DataTable dt = link.ExcuteRetureTable(sql);
            return dt;
        }


    }
}
