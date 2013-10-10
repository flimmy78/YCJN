/*
 *开发人员：胡进财
 *开发时间：2012-02027
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.Helper;
using System.Data;
using SAC.DB2;
using System.Collections;

namespace DAL
{
    /// <summary>
    /// 组织关系
    /// </summary>
    public class ParmentDAL
    {
        #region 胡进财
        string sql = "";
        string errMsg = "";
        DataTable dt = new DataTable();
        StringBuilder sbl = new StringBuilder();

        int count = 0;
        bool result = false;

        #region 获取职位关系  1 获取所有  2 by T_PARENTID
        /// <summary>
        /// 获取职位关系
        /// </summary>
        /// <returns></returns>
        public DataTable GetMenu()
        {
            sql = "select *from T_SYS_ORGANIZE order by ID_KEY asc";
            try
            {
                dt = DBdb2.RunDataTable(sql, out errMsg);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
            }
            return dt;
        }


        /// <summary>
        /// 获取组织岗位关系
        /// </summary>
        /// <param name="id">父类编码</param>
        /// <returns></returns>
        public DataTable Getmenu(string id, int sCount, int eCount)
        {
            sql = "select * from (select ID_KEY,T_ORGID,T_ORGDESC,T_PARENTID ,rownumber() over(order by ID_KEY asc ) as rowid  from T_SYS_ORGANIZE where T_PARENTID='" + id + "' )as a where a.rowid between " + sCount + " and " + eCount + "";
            try
            {
                dt = DBdb2.RunDataTable(sql, out errMsg);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// 获取组织岗位关系
        /// </summary>
        /// <param name="id">父类编码</param>
        /// <returns></returns>
        public int GetmenuCount(string id)
        {
            sql = "select count(*) from T_SYS_ORGANIZE where T_PARENTID='" + id + "'";
            try
            {
                count = DBdb2.RunRowCount(sql, out errMsg);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
            }
            return count;
        }
        #endregion

        #region 判断某个职位是否有子节点
        /// <summary>
        /// 判断某个职位是否有子节点
        /// </summary>
        /// <param name="id">职位编号</param>
        /// <returns></returns>
        public int getSun(string id)
        {
            sql = "select count(*) from T_SYS_ORGANIZE where T_PARENTID='" + id + "'";

            try
            {
                if (DBdb2.RunRowCount(sql, out errMsg) > 0)
                    count = 1;
                else
                {
                    sql = "select count(*) from T_SYS_MEMBERRELATION where T_ORGID='" + id + "'";
                    if (DBdb2.RunRowCount(sql, out errMsg) > 0)
                        count = 1;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
            }

            return count;
        }
        #endregion

        #region 添加组织关系
        /// <summary>
        /// 添加组织关系
        /// </summary>
        /// <param name="parentId">父类ID</param>
        /// <param name="id">组织编码</param>
        /// <param name="name">组织名称</param>
        /// <returns></returns>
        public bool AddOrganize(string parentId, string id, string name)
        {
            sql = "insert into T_SYS_ORGANIZE(T_ORGID,T_ORGDESC,T_PARENTID) values('" + id + "','" + name + "','" + parentId + "');";

            try
            {
                result = DBdb2.RunNonQuery(sql, out errMsg);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
            }
            return result;
        }
        #endregion

        #region 编辑组织关系
        /// <summary>
        /// 编辑组织关系
        /// </summary>
        /// <param name="oid">组织原编码</param>
        /// <param name="id">组织新编码</param>
        /// <param name="name">组织名称</param>
        /// <returns></returns>
        public bool EditOrganize(string oid, string id, string name)
        {
            sql = "update T_SYS_ORGANIZE set T_ORGID='" + id + "' , T_ORGDESC='" + name + "' where T_ORGID='" + oid + "'";

            try
            {
                result = DBdb2.RunNonQuery(sql, out errMsg);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
            }
            return result;
        }
        #endregion

        #region 删除组织关系
        /// <summary>
        /// 删除组织关系
        /// </summary>
        /// <param name="id">组织编码</param>
        /// <returns></returns>
        public bool RemoveOrganize(string id)
        {
            sql = "delete from T_SYS_ORGANIZE where T_ORGID in (" + id + ")";

            try
            {
                result = DBdb2.RunNonQuery(sql, out errMsg);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
            }
            return result;
        }
        #endregion

        #region 获取值别信息
        /// <summary>
        /// 获取值别信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetClass()
        {
            sql = "select *From T_SYS_CLASS";
            try
            {
                dt = DBdb2.RunDataTable(sql, out errMsg);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
            }
            return dt;
        }
        #endregion

        #region 判断是否存在该部门
        /// <summary>
        /// 判断是否存在该部门
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public bool JudgeExitParent(string id)
        {
            sql = "select *From T_SYS_ORGANIZE where T_ORGID='" + id + "'";
            try
            {
                if (DBdb2.RunRowCount(sql, out errMsg) > 0)
                    result = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
            }
            return result;
        }
        #endregion

        #region 获取岗位信息 有人员存在的岗位
        public DataTable GetOrganizeExistPerson(string id)
        {
            sql = "select distinct b.T_ORGID,s.T_ORGDESC from T_SYS_MEMBERRELATION b inner join T_SYS_ORGANIZE s on b.T_ORGID=s.T_ORGID";
            try
            {
                dt = DBdb2.RunDataTable(sql, out errMsg);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
            }
            return dt;
        }
        public DataTable GetOrganizeExistPerson(int sCount, int eCount)
        {
            sql = "select * from (select a.ID,a.ORGID,a.NAME,rownumber() over(order by ID asc ) as rowid  from (select distinct s.ID_KEY ID, b.T_ORGID ORGID,s.T_ORGDESC NAME from T_SYS_MEMBERRELATION b inner join T_SYS_ORGANIZE s on b.T_ORGID=s.T_ORGID order by s.ID_KEY asc) a )  p where p.rowid between " + sCount + " and " + eCount + ";";
            try
            {
                dt = DBdb2.RunDataTable(sql, out errMsg);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
            }
            return dt;
        }
        #endregion

        #region 获取查询数量
        /// <summary>
        /// SQL 语句
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int GetCount(string sqlWhere)
        {
            try
            {
                count = DBdb2.RunRowCount(sqlWhere, out errMsg);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
            }

            return count;
        }
        #endregion

        #region 查询点检漏检率
        /// <summary>
        /// 查询点检漏检率
        /// </summary>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns></returns>
        public string GetParmentCheck(DateTime sTime, DateTime eTime)
        {
            dt = GetOrganizeExistPerson("");
            sbl.Append("");
            if (dt != null && dt.Rows.Count > 0)
            {
                double[] task = new double[dt.Rows.Count];
                double[] Maketask = new double[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    task[i] = Convert.ToInt32(GetCount("select count(*) from T_INFO_QUESTCOMPLETE where T_ROUTEID in(select T_ROUTEID from T_BASE_ROUTE where T_ORGID ='" + dt.Rows[i]["T_ORGID"] + "') and I_FLAG=1 and T_STARTTIME>='" + sTime + "' and T_ENDTIME<='" + eTime + "'"));
                    Maketask[i] = Convert.ToInt32(GetCount("select count(*) from T_INFO_QUESTCOMPLETE where T_ROUTEID in(select T_ROUTEID from T_BASE_ROUTE where T_ORGID ='" + dt.Rows[i]["T_ORGID"] + "') and I_FLAG=1 and I_STATUS=1 and T_STARTTIME>='" + sTime + "' and T_ENDTIME<='" + eTime + "'"));
                }

                sbl.Append("<table class='admintable' width='100%'>");
                sbl.Append("<tr><th class='adminth' colspan='6'>各班组点检完成情况分析</th></tr>");
                sbl.Append("<tr><td class='admincls0' colspan='6' align='center'>查询时间&nbsp;&nbsp;&nbsp;&nbsp;" + sTime + "&nbsp;&nbsp;至&nbsp;&nbsp;" + eTime + "</th></tr>");
                sbl.Append("<tr><td class='admincls1' align='center'>序号</td><td class='admincls1' align='center'>点检部门</td><td class='admincls1' align='center'>任务数量</td><td class='admincls1' align='center'>完成数量</td><td class='admincls1' align='center'>漏检数量</td><td class='admincls1' align='center'>任务完成率</td></tr>");
                for (int i = 0; i < task.Length; i++)
                {
                    double res = 0;
                    if (task[i] != 0)
                        res = getDouble(Maketask[i] / task[i], 3) * 100;
                    else
                        res = 100;
                    if (i % 2 == 0)
                        sbl.Append("<tr><td class='admincls0' align='center'>" + (i + 1) + "</td><td class='admincls0' align='center'>" + dt.Rows[i]["T_ORGDESC"] + "</td><td class='admincls0' align='center'>" + task[i] + "</td><td class='admincls0' align='center'>" + Maketask[i] + "</td><td class='admincls0' align='center'>" + (task[i] - Maketask[i]) + "</td><td class='admincls0' align='center'>" + res + "%</td></tr>");
                    else
                        sbl.Append("<tr><td class='admincls1' align='center'>" + (i + 1) + "</td><td class='admincls1' align='center'>" + dt.Rows[i]["T_ORGDESC"] + "</td><td class='admincls1' align='center'>" + task[i] + "</td><td class='admincls1' align='center'>" + Maketask[i] + "</td><td class='admincls1' align='center'>" + (task[i] - Maketask[i]) + "</td><td class='admincls1' align='center'>" + res + "%</td></tr>");
                }
                sbl.Append("</table>");
            }
            return sbl.ToString();
        }
        public IList<Hashtable> GetParmentCheckGrid(DateTime sTime, DateTime eTime, int sCount, int eCount)
        {
            dt = GetOrganizeExistPerson(sCount, eCount);
            IList<Hashtable> list = new List<Hashtable>();
            if (dt != null && dt.Rows.Count > 0)
            {
                double[] task = new double[dt.Rows.Count];
                double[] Maketask = new double[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    task[i] = Convert.ToInt32(GetCount("select count(*) from T_INFO_QUESTCOMPLETE where T_ROUTEID in(select T_ROUTEID from T_BASE_ROUTE where T_ORGID ='" + dt.Rows[i]["ORGID"] + "') and I_FLAG=1 and T_STARTTIME>='" + sTime + "' and T_ENDTIME<='" + eTime + "'"));
                    Maketask[i] = Convert.ToInt32(GetCount("select count(*) from T_INFO_QUESTCOMPLETE where T_ROUTEID in(select T_ROUTEID from T_BASE_ROUTE where T_ORGID ='" + dt.Rows[i]["ORGID"] + "') and I_FLAG=1 and I_STATUS=1 and T_STARTTIME>='" + sTime + "' and T_ENDTIME<='" + eTime + "'"));
                }

                int j = 0;
                foreach (DataRow row in dt.Rows)
                {
                    j++;
                    Hashtable ht = new Hashtable();
                    ht.Add("id", row["ID"].ToString());
                    ht.Add("parment", row["NAME"].ToString());
                    ht.Add("task", task[j - 1]);
                    ht.Add("Maketask", Maketask[j - 1]);
                    ht.Add("value", task[j - 1] - Maketask[j - 1]);
                    if (task[j - 1] != 0)
                        ht.Add("values", (getDouble(Maketask[j - 1] / task[j - 1], 3) * 100).ToString() + "%");
                    else
                        ht.Add("values", "100%");
                    list.Add(ht);
                }
            }
            return list;
        }

        #endregion

        #region 查询巡检漏检率
        /// <summary>
        /// 查询巡检漏检率
        /// </summary>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns></returns>
        public string GetCheck(DateTime sTime, DateTime eTime)
        {
            dt = GetClass();
            sbl.Append("");
            if (dt != null && dt.Rows.Count > 0)
            {
                double[] task = new double[dt.Rows.Count];
                double[] Maketask = new double[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    task[i] = Convert.ToInt32(GetCount("select count(*) from T_SYS_DUTY where T_CLASSID='" + dt.Rows[i]["T_CLASSID"] + "' and T_STARTTIME>='" + sTime + "' and T_ENDTIME<='" + eTime + "'"));
                    Maketask[i] = Convert.ToInt32(GetCount("select count(*) from T_INFO_QUEST where T_CLASSID='" + dt.Rows[i]["T_CLASSID"] + "' and T_SCANTIME between '" + sTime + "' and '" + eTime + "'"));
                }

                sbl.Append("<table class='admintable' width='100%'>");
                sbl.Append("<tr><th class='adminth' colspan='6'>各班组巡检完成情况分析</th></tr>");
                sbl.Append("<tr><td class='admincls0' colspan='6' align='center'>查询时间&nbsp;&nbsp;&nbsp;&nbsp;" + sTime + "&nbsp;&nbsp;至&nbsp;&nbsp;" + eTime + "</th></tr>");
                sbl.Append("<tr><td class='admincls1' align='center'>序号</td><td class='admincls1' align='center'>班值名称</td><td class='admincls1' align='center'>任务数量</td><td class='admincls1' align='center'>完成数量</td><td class='admincls1' align='center'>漏检数量</td><td class='admincls1' align='center'>任务完成率</td></tr>");
                for (int i = 0; i < task.Length; i++)
                {
                    double res = 0;
                    if (task[i] != 0)
                        res = getDouble(Maketask[i] / task[i], 3) * 100;
                    else
                        res = 100;
                    if (i % 2 == 0)
                        sbl.Append("<tr><td class='admincls0' align='center'>" + (i + 1) + "</td><td class='admincls0' align='center'>" + dt.Rows[i]["T_CLASSNAME"] + "</td><td class='admincls0' align='center'>" + task[i] + "</td><td class='admincls0' align='center'>" + Maketask[i] + "</td><td class='admincls0' align='center'>" + (task[i] - Maketask[i]) + "</td><td class='admincls0' align='center'>" + res + "%</td></tr>");
                    else
                        sbl.Append("<tr><td class='admincls1' align='center'>" + (i + 1) + "</td><td class='admincls1' align='center'>" + dt.Rows[i]["T_CLASSNAME"] + "</td><td class='admincls1' align='center'>" + task[i] + "</td><td class='admincls1' align='center'>" + Maketask[i] + "</td><td class='admincls1' align='center'>" + (task[i] - Maketask[i]) + "</td><td class='admincls1' align='center'>" + res + "%</td></tr>");
                }
                sbl.Append("</table>");
            }
            return sbl.ToString();
        }

        public IList<Hashtable> GetCheckGrid(DateTime sTime, DateTime eTime)
        {
            dt = GetClass();
            IList<Hashtable> list = new List<Hashtable>();
            if (dt != null && dt.Rows.Count > 0)
            {
                double[] task = new double[dt.Rows.Count];
                double[] Maketask = new double[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    task[i] = Convert.ToInt32(GetCount("select count(*) from T_SYS_DUTY where T_CLASSID='" + dt.Rows[i]["T_CLASSID"] + "' and T_STARTTIME>='" + sTime + "' and T_ENDTIME<='" + eTime + "'"));
                    Maketask[i] = Convert.ToInt32(GetCount("select count(*) from T_INFO_QUEST where T_CLASSID='" + dt.Rows[i]["T_CLASSID"] + "' and T_SCANTIME between '" + sTime + "' and '" + eTime + "'"));
                }


                int j = 0;
                foreach (DataRow row in dt.Rows)
                {
                    j++;
                    Hashtable ht = new Hashtable();
                    ht.Add("id", row["T_CLASSNAME"].ToString());
                    ht.Add("task", task[j - 1]);
                    ht.Add("Maketask", Maketask[j - 1]);
                    ht.Add("value", task[j - 1] - Maketask[j - 1]);
                    if (task[j - 1] != 0)
                        ht.Add("values", (getDouble(Maketask[j - 1] / task[j - 1], 3) * 100).ToString() + "%");
                    else
                        ht.Add("values", "100%");
                    list.Add(ht);
                }

            }
            return list;
        }

        #endregion
        /// <summary>
        /// 获取值别信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetClassInfo()
        {
            sql = "SELECT T_CLASSID,T_CLASSNAME FROM T_SYS_CLASS;";
            try
            {
                dt = DBdb2.RunDataTable(sql, out errMsg);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
            }
            return dt;
        }
        #region 四舍五入
        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="result">要转换的数值</param>
        /// <param name="num">保留位数</param>
        /// <returns></returns>
        public double getDouble(double result, int num)
        {
            string res = result.ToString();
            string results = "";
            int index = res.IndexOf('.');

            if (res.Length - index == num + 1)
                return Convert.ToDouble(res);
            else
            {
                if (index > 0)
                {
                    index += num;
                    res = res + "000000000000000000";
                    res = res.Remove(0, index + 1);
                    results = result + "000000000000000000";
                    results = results.ToString().Substring(0, index + 1);
                    res = res.Substring(0, 1);

                    string point = "0.";

                    for (int count = 0; count < num - 1; count++)
                    {
                        point += "0";
                    }
                    point += "1";


                    if (Convert.ToInt32(res) > 4)
                    {
                        results = (Convert.ToDouble(results) + Convert.ToDouble(point)).ToString();
                        res = results;
                    }
                    else
                    {
                        res = results;
                    }
                }
                else
                {
                    res += ".";
                    for (int i = 0; i < num; i++)
                    {
                        res += "0";
                    }
                }
                return Convert.ToDouble(res);
            }
        }
        #endregion
        #endregion
    }
}