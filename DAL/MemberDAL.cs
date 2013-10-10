/*
 *开发人员：胡进财
 *开发时间：2012-02027
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SAC.Helper;
using SAC.DB2;
using System.Data.OleDb;

namespace DAL
{
    /// <summary>
    /// 人员信息
    /// </summary>
    public class MemberDAL
    {
        #region 胡进财
        string sql = "";
        string errMsg = "";
        DataTable dt = new DataTable();

        int count = 0;
        bool result = false;

        #region 添加人员信息
        /// <summary>
        /// 添加人员信息
        /// </summary>
        /// <param name="id">用户编码</param>
        /// <param name="name">真实姓名</param>
        /// <param name="pwd">登陆密码</param>
        /// <param name="classId">值别ID</param>
        /// <param name="img">图片</param>
        /// <returns></returns>
        public bool AddMember(string id, string name, string pwd, string classId, byte[] img)
        {
            if (img.Length > 0)
            {
                sql = "insert into T_SYS_MEMBERINFO(T_USERID,T_USERNAME,T_PASSWD,T_CLASSID,B_ATTACHMENT) values(?,?,?,?,?);";
            }
            else
            {
                sql = "insert into T_SYS_MEMBERINFO(T_USERID,T_USERNAME,T_PASSWD,T_CLASSID) values(?,?,?,?);";
            }
            try
            {
                OleDbConnection con = new OleDbConnection(DBdb2.SetConString());
                con.Open();
                OleDbCommand oledbcom = new OleDbCommand(sql, con);
                if (img.Length > 0)
                {
                    oledbcom.Parameters.Add("?", id);
                    oledbcom.Parameters.Add("?", name);
                    oledbcom.Parameters.Add("?", pwd);
                    oledbcom.Parameters.Add("?", classId);
                    oledbcom.Parameters.Add("?", img);
                }
                else
                {
                    oledbcom.Parameters.Add("?", id);
                    oledbcom.Parameters.Add("?", name);
                    oledbcom.Parameters.Add("?", pwd);
                    oledbcom.Parameters.Add("?", classId);
                }
                if (oledbcom.ExecuteNonQuery() > 0)
                    result = true;
                con.Close();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
            }
            return result;
        }
        #endregion

        #region 人员数据集
        /// <summary>
        /// 人员数据集
        /// </summary>
        /// <returns></returns>
        public DataTable GetMembers()
        {
            sql = "select T_USERID,T_USERNAME from T_SYS_MEMBERINFO order by ID_KEY asc";
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

        #region 为人员分配职位
        /// <summary>
        /// 分配职位
        /// </summary>
        /// <param name="parentId">职位ID</param>
        /// <param name="memberId">人员ID集合</param>
        /// <returns></returns>
        public bool AddMember(string parentId, string[] membersId)
        {
            sql = "delete from T_SYS_MEMBERRELATION where T_ORGID='" + parentId.Trim() + "';";
            if (membersId != null)
                for (int i = 0; i < membersId.Length; i++)
                {
                    sql += "insert into T_SYS_MEMBERRELATION(T_USERID,T_ORGID,I_STATUS) values('" + membersId[i].Trim() + "','" + parentId.Trim() + "',1);";
                }
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

        /// <summary>
        /// 分配职位
        /// </summary>
        /// <param name="parentId">职位ID</param>
        /// <param name="membersId">人员ID</param>
        /// <param name="state">岗位类型</param>
        /// <returns></returns>
        public bool AddMember(string parentId, string membersId, int state)
        {
            sql = "insert into T_SYS_MEMBERRELATION(T_USERID,T_ORGID,I_STATUS) values('" + membersId + "','" + parentId.Trim() + "'," + state + ");";

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

        #region 获取人员岗位关系
        /// <summary>
        /// 获取人员岗位关系
        /// </summary>
        /// <returns></returns>
        public DataTable GetMembersAndParent()
        {
            sql = "select l.T_ORGID,i.T_USERID,i.T_USERNAME from T_SYS_MEMBERRELATION l inner join T_SYS_MEMBERINFO i on l.T_USERID=i.T_USERID";
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

        #region 获取岗位人员关系
        /// <summary>
        /// 获取人员岗位关系
        /// </summary>
        /// <returns></returns>
        public DataTable GetParentAndMembers(string id)
        {
            sql = "select l.T_ORGID,i.T_USERID,i.T_USERNAME from T_SYS_MEMBERRELATION l right join T_SYS_MEMBERINFO i on l.T_USERID=i.T_USERID where l.T_ORGID='" + id + "'";
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

        #region 岗位下人员分页
        public DataTable GetMember(string id, int sCount, int eCount)
        {
            sql = "select * from (select s.ID,s.parmentID,s.UserID,s.UserName,s.Pwd,s.Plan,rownumber() over(order by s.ID asc ) as rowid  from (select m.ID_KEY ID,m.T_ORGID ParmentID,m.T_USERID UserID,m.T_USERNAME UserName,m.T_PASSWD Pwd,c.T_CLASSNAME Plan from (select i.ID_KEY,l.T_ORGID,i.T_USERID,i.T_USERNAME,i.T_PASSWD,i.T_CLASSID from T_SYS_MEMBERRELATION l right join T_SYS_MEMBERINFO i on l.T_USERID=i.T_USERID) m left join T_SYS_CLASS c on m.T_CLASSID=c.T_CLASSID where m.T_ORGID='" + id + "') s )as a where a.rowid between " + sCount + " and " + eCount + ";";
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
        public int GetMemberCount(string id)
        {
            sql = "select count(*) from (select s.ID,s.parmentID,s.UserID,s.UserName,s.Pwd,s.Plan,rownumber() over(order by s.ID asc ) as rowid  from (select m.ID_KEY ID,m.T_ORGID ParmentID,m.T_USERID UserID,m.T_USERNAME UserName,m.T_PASSWD Pwd,c.T_CLASSNAME Plan from (select i.ID_KEY,l.T_ORGID,i.T_USERID,i.T_USERNAME,i.T_PASSWD,i.T_CLASSID from T_SYS_MEMBERRELATION l right join T_SYS_MEMBERINFO i on l.T_USERID=i.T_USERID) m left join T_SYS_CLASS c on m.T_CLASSID=c.T_CLASSID where m.T_ORGID='" + id + "') s )as a;";

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

        #region 判断是否存在该人员
        /// <summary>
        /// 判断是否存在该人员
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <returns></returns>
        public bool JudgMember(string id)
        {
            try
            {
                sql = "select count(*) from T_SYS_MEMBERINFO where T_USERID='" + id + "'";
                count = DBdb2.RunRowCount(sql, out errMsg);
                if (count > 0)
                    result = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 判断是否存在该人员
        /// </summary>
        /// <param name="name">人员姓名</param>
        /// <returns></returns>
        public bool JudgMemberByName(string name)
        {
            try
            {
                sql = "select count(*) from T_SYS_MEMBERINFO where T_USERNAME='" + name + "'";
                count = DBdb2.RunRowCount(sql, out errMsg);
                if (count > 0)
                    result = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 判断某个岗位下面是否存在人员
        /// </summary>
        /// <param name="name">人员ID</param>
        /// <returns></returns>
        public bool JudgMemberByClassId(string id)
        {
            try
            {
                sql = "select count(*) from T_SYS_MEMBERRELATION where T_ORGID='" + id + "'";
                count = DBdb2.RunRowCount(sql, out errMsg);
                if (count > 0)
                    result = true;
                else
                    result = false;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
            }
            return result;
        }
        #endregion

        #region 查询人员信息
        /// <summary>
        /// 查询人员信息
        /// </summary>
        /// <param name="id">人员编号</param>
        /// <returns></returns>
        public DataTable GetmemberInfo(string id, int i)
        {
            if (i == 1)
                sql = "select T_USERID,T_USERNAME,T_PASSWD,T_CLASSID from T_SYS_MEMBERINFO where T_USERID='" + id + "'";
            else
                sql = "select * from T_SYS_MEMBERINFO where T_USERID='" + id + "'";
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

        #region 编辑人员信息
        /// <summary>
        /// 编辑人员信息
        /// </summary>
        /// <param name="oldId">原用户名称</param>
        /// <param name="trueName">真实姓名</param>
        /// <param name="id">新用户名称</param>
        /// <param name="pwd">用户密码</param>
        /// <param name="classID">班值编码</param>
        /// <param name="path">图片文件(字节数组)</param>
        /// <returns></returns>
        public bool EditMemberInfo(string oldId, string trueName, string id, string pwd, string classID, byte[] img)
        {
            try
            {
                if (img != null)
                {
                    OleDbConnection con = new OleDbConnection(DBdb2.SetConString());
                    con.Open();
                    if (oldId == id)
                    {
                        sql += "update T_SYS_MEMBERINFO set T_USERNAME=?,T_PASSWD=?,T_CLASSID=?,B_ATTACHMENT=? where T_USERID='" + oldId + "'";
                        OleDbCommand oledbcom = new OleDbCommand(sql, con);
                        oledbcom.Parameters.Add("?", trueName);
                        oledbcom.Parameters.Add("?", pwd);
                        oledbcom.Parameters.Add("?", classID);
                        oledbcom.Parameters.Add("?", img);
                        if (oledbcom.ExecuteNonQuery() > 0)
                            result = true;
                    }
                    else
                    {
                        sql += "update T_SYS_MEMBERINFO set T_USERID=?,T_USERNAME=?,T_PASSWD=?,T_CLASSID=?,B_ATTACHMENT=? where T_USERID='" + oldId + "'";
                        OleDbCommand oledbcom = new OleDbCommand(sql, con);
                        oledbcom.Parameters.Add("?", id);
                        oledbcom.Parameters.Add("?", trueName);
                        oledbcom.Parameters.Add("?", pwd);
                        oledbcom.Parameters.Add("?", classID);
                        oledbcom.Parameters.Add("?", img);
                        result = DBdb2.RunNonQuery("update T_SYS_MEMBERRELATION set T_USERID='" + id + "' where T_USERID='" + oldId + "';", out errMsg);
                        if (oledbcom.ExecuteNonQuery() > 0 && result == true)
                            result = true;
                    }
                    con.Close();
                }
                else
                {
                    if (oldId == id)
                        sql += "update T_SYS_MEMBERINFO set T_USERNAME='" + trueName + "',T_PASSWD='" + pwd + "',T_CLASSID='" + classID + "' where T_USERID='" + oldId + "';";
                    else
                        sql += "update T_SYS_MEMBERINFO set T_USERID='" + id + "',T_USERNAME='" + trueName + "',T_PASSWD='" + pwd + "',T_CLASSID='" + classID + "' where T_USERID='" + oldId + "';update T_SYS_MEMBERRELATION set T_USERID='" + id + "' where T_USERID='" + oldId + "';";
                    result = DBdb2.RunNonQuery(sql, out errMsg);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
            }
            return result;
        }
        #endregion

        #region 删除人员信息
        /// <summary>
        /// 删除人员信息
        /// </summary>
        /// <param name="id">人员编码</param>
        /// <returns></returns>
        public bool RemoveMember(string id)
        {
            sql = "delete from T_SYS_MEMBERINFO where T_USERID in (" + id + ");delete from T_SYS_MEMBERRELATION where T_USERID in (" + id + ");";

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
        #endregion
    }
}