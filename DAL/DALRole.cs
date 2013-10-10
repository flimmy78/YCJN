using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SAC.Helper;
using System.Collections;
using System.Data;
using SAC.DB2;

namespace DAL
{
    public class DALRole
    {
        string rlDBType = "";
        string rtDBType = "";

        string pGl1 = "WHSIS.U1APSH.U1A04013";
        string pGl2 = "WHSIS.U2APSH.U2A04013";

        /// <summary>
        /// 判断存储XML的表是否为空，如果为空则添加一条T_XMLID字段的值为Webmenu的记录
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool IsEmptyXmlMenu(string T_XMLID, out string errMsg)
        {
            errMsg = "";
            this.init();
            string sql1 = "select COUNT(*) from T_SYS_MENU where T_XMLID='" + T_XMLID + "'";
            string sql2 = "select COUNT(*) from T_SYS_MENU where T_XMLID='" + T_XMLID + "'";
            DataTable dt=null;
            if (rlDBType == "SQL")
            {
                //dt = DBsql.RunDataTable(sql1, out errMsg);
            }
            else
            {
                dt = DBdb2.RunDataTable(sql2, out errMsg);
            }
            if (dt.Rows[0]["1"].ToString() == "0")
            {
                sql1 = "insert into T_SYS_MENU (T_XMLID) values ('" + T_XMLID + "')";
                sql2 = "insert into T_SYS_MENU (T_XMLID) values ('" + T_XMLID + "')";
                if (rlDBType == "SQL")
                {
                    //DBsql.RunNonQuery(sql1, out errMsg);
                }
                else
                {
                    DBdb2.RunNonQuery(sql2, out errMsg);
                }
                if (errMsg == "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据所给的用户名查找其对应的密码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public string GetPwd(string username, out string errMsg)
        {
            errMsg = "";
            this.init();
            string sql1 = "select T_PASSWD from T_SYS_MEMBERINFO where T_USERID='" + username + "'";
            string sql2 = "select T_PASSWD from T_SYS_MEMBERINFO where T_USERID='" + username + "'";
            DataTable dt=null;
            if (rlDBType == "SQL")
            {
                //dt = DBsql.RunDataTable(sql1, out errMsg);
            }
            else
            {
                dt = DBdb2.RunDataTable(sql2, out errMsg);
            }
            string pwd;
            if ( dt.Rows.Count== 0 )
            {
                pwd = "";
            }
            else
            {

                pwd = dt.Rows[0]["T_PASSWD"].ToString();
            }
            return pwd;
        }

        /// <summary>
        ///根据所给的用户名查出表中共有多少条记录（一般为1或0个）
        /// </summary>
        /// <param name="username"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public string GetUnmN(string username, out string errMsg)
        {
            errMsg = "";
            this.init();
            string sql1 = "select count(*) from T_SYS_MEMBERINFO where T_USERID='" + username + "'";
            string sql2 = "select count(*) from T_SYS_MEMBERINFO where T_USERID='" + username + "'";
            DataTable dt=null;
            if (rlDBType == "SQL")
            {
                //dt = DBsql.RunDataTable(sql1, out errMsg);
            }
            else
            {
                dt = DBdb2.RunDataTable(sql2, out errMsg);
            }
            string UserNumber = dt.Rows[0]["1"].ToString();
            return UserNumber;
        }

        /// <summary>
        /// 根据所给的用户名查找其对应的ID_KEY
        /// </summary>
        /// <param name="username"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public string GetIdKeyByUN(string username, out string errMsg)
        {
            errMsg = "";
            this.init();
            string sql1 = "select ID_KEY from T_SYS_MEMBERINFO where T_USERID='" + username + "'";
            string sql2 = "select ID_KEY from T_SYS_MEMBERINFO where T_USERID='" + username + "'";
            DataTable dt=null;
            if (rlDBType == "SQL")
            {
                //dt = DBsql.RunDataTable(sql1, out errMsg);
            }
            else
            {
                dt = DBdb2.RunDataTable(sql2, out errMsg);
            }
            string idkey = dt.Rows[0]["ID_KEY"].ToString();
            return idkey;
        }

        /// <summary>
        /// 根据所给的ID_KEY查找其对应的用户名
        /// </summary>
        /// <param name="username"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public string GetUserNameById(string idkey, out string errMsg)
        {
            errMsg = "";
            int id = int.Parse(idkey);
            this.init();
            string sql1 = "select T_USERID from T_SYS_MEMBERINFO where ID_KEY=" + id + "";
            string sql2 = "select T_USERID from T_SYS_MEMBERINFO where ID_KEY=" + id + "";
            DataTable dt=null;
            if (rlDBType == "SQL")
            {
                //dt = DBsql.RunDataTable(sql1, out errMsg);
            }
            else
            {
                dt = DBdb2.RunDataTable(sql2, out errMsg);
            }
            string rolename = dt.Rows[0]["T_USERID"].ToString();
            return rolename;
        }
        /// <summary>
        /// 根据所给的用户名查找其对应的用户姓名
        /// </summary>
        /// <param name="username"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public string GetUserRealNameById(string userId, out string errMsg)
        {
            errMsg = "";
            int id = int.Parse(userId);
            this.init();
            string sql1 = "select T_USERNAME from T_SYS_MEMBERINFO where ID_KEY=" + id + "";
            string sql2 = "select T_USERNAME from T_SYS_MEMBERINFO where ID_KEY=" + id + "";
            DataTable dt = null;
            if (rlDBType == "SQL")
            {
                //dt = DBsql.RunDataTable(sql1, out errMsg);
            }
            else
            {
                dt = DBdb2.RunDataTable(sql2, out errMsg);
            }
            string rolename = dt.Rows[0]["T_USERNAME"].ToString();
            return rolename;
        }

        /// <summary>
        /// 根据所给的岗位ID查找其对应的岗位描述
        /// </summary>
        /// <param name="username"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public string GetRoleNameById(string roleId, out string errMsg)
        {
            errMsg = "";
            this.init();
            string sql1 = "select T_ORGDESC from T_SYS_ORGANIZE where T_ORGID='" + roleId + "'";
            string sql2 = "select T_ORGDESC from T_SYS_ORGANIZE where T_ORGID='" + roleId + "'";
            DataTable dt = null;
            if (rlDBType == "SQL")
            {
                //dt = DBsql.RunDataTable(sql1, out errMsg);
            }
            else
            {
                dt = DBdb2.RunDataTable(sql2, out errMsg);
            }
            string rolename = dt.Rows[0]["T_ORGDESC"].ToString();
            return rolename;
        }

        /// <summary>
        /// 根据所给的用户ID查找该用户所有的岗位
        /// </summary>
        /// <param name="username"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public ArrayList GetRolesByUserName(string UserName, out string errMsg)
        {
            ArrayList list = new ArrayList();
            
            errMsg = "";
            this.init();
            string sql1 = "select T_ORGID from T_SYS_MEMBERRELATION where T_USERID='" + UserName + "'";
            string sql2 = "select T_ORGID from T_SYS_MEMBERRELATION where T_USERID='" + UserName + "'";
            DataTable dt=null;
            if (rlDBType == "SQL")
            {
                //dt = DBsql.RunDataTable(sql1, out errMsg);
            }
            else
            {
                dt = DBdb2.RunDataTable(sql2, out errMsg);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                string[] val = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    val[i] = dt.Rows[i]["T_ORGID"].ToString();
                }
                list.Add(val);
            }
            
            return list;
        }

        /// <summary>
        /// 返回数据库的连接字符串（正式加入工程的时候要修改，改动DBdb2里的RetConString()方法）
        /// </summary>
        /// <param name="username"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public string GetConnstr(out string errMsg)
        {
            errMsg = "";
            this.init();
            string connstr="";
            if (rlDBType == "SQL")
            {

            }
            else
            {
                connstr = SAC.DB2.DBdb2.SetConString();
            }
            return connstr;
        }

        //获取所有班次的信息
        public DataTable GetAllBC(out string errMsg)
        {
            this.init();
            errMsg = "";

            //string sql1 = "select min(ID_KEY) as ID_KEY,UGROUP from WebUser group by UGROUP order by ID_KEY";
            string sql = "select ID_KEY,T_SHIFT,T_STARTTIME,T_ENDTIME from T_SYS_SHIFT order by ID_KEY";

            DataTable dt=null;

            if (rlDBType == "SQL")
            {
                // sql = "select ID_KEY,班次名,起始时间,结束时间 from 班次时间表 order by ID_KEY";
                //dt = DBsql.RunDataTable(sql, out errMsg);
            }
            else
            {
                //
                dt = DBdb2.RunDataTable(sql, out errMsg);
            }
            return dt;
        }

        //存储新添加的班次信息
        public bool SaveBC(string BcMs, string ST, string ET, out string errMsg)
        {
            errMsg = "";
            bool flag = false;

            this.init();

            string sql1 = "select * from T_SYS_SHIFT where T_SHIFT='" + BcMs + "'";

            DataTable dt=null;

            if (rlDBType == "SQL")
            {
                //dt = DBsql.RunDataTable(sql1, out errMsg);
            }
            else
            {
                dt = DBdb2.RunDataTable(sql1, out errMsg);
            }
            if (dt == null || dt.Rows.Count == 0)
            {
                string sql2 = "insert into T_SYS_SHIFT (T_SHIFT,T_STARTTIME,T_ENDTIME) values ('" + BcMs + "','" + ST + "','" + ET + "')";
                if (rlDBType == "SQL")
                {
                    //DBsql.RunNonQuery(sql2, out errMsg);
                }
                else
                {
                    DBdb2.RunNonQuery(sql2, out errMsg);
                }
                if (errMsg == "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //编辑原来的班次信息
        public bool UpadteBC(string OBcMs, string BcMs, string ST, string ET, out string errMsg)
        {
            errMsg = "";
            this.init();
            if (OBcMs == BcMs)
            {
                string sql = "update T_SYS_SHIFT set T_SHIFT='" + BcMs + "',T_STARTTIME='" + ST + "',T_ENDTIME='" + ET + "' where T_SHIFT=" + OBcMs + "";
                if (rlDBType == "SQL")
                {
                    //DBsql.RunNonQuery(sql, out errMsg);
                }
                else
                {
                    DBdb2.RunNonQuery(sql, out errMsg);
                }
                if (errMsg == "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                string sql1 = "select * from T_SYS_SHIFT where T_SHIFT='" + BcMs + "'";
                DataTable dt=null;

                if (rlDBType == "SQL")
                {
                    //dt = DBsql.RunDataTable(sql1, out errMsg);
                }
                else
                {
                    dt = DBdb2.RunDataTable(sql1, out errMsg);
                }
                if (dt == null || dt.Rows.Count == 0)
                {
                    string sql2 = "update T_SYS_SHIFT set T_SHIFT='" + BcMs + "',T_STARTTIME='" + ST + "',T_ENDTIME='" + ET + "' where T_SHIFT='" + OBcMs + "'";
                    if (rlDBType == "SQL")
                    {
                        //DBsql.RunNonQuery(sql2, out errMsg);
                    }
                    else
                    {
                        DBdb2.RunNonQuery(sql2, out errMsg);
                    }
                    if (errMsg == "")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        //删除原来的班次信息
        public bool DeleteBC(int BcId, out string errMsg)
        {
            errMsg = "";
            bool flag = true;
            this.init();

            string sql = "delete from T_SYS_SHIFT where ID_KEY=" + BcId + "";
            if (rlDBType == "SQL")
            {
                //DBsql.RunNonQuery(sql, out errMsg);
            }
            else
            {
                DBdb2.RunNonQuery(sql, out errMsg);
            }
            if (errMsg == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //返回所有职别（班组）信息
        public DataTable GetAllBZ(out string errMsg)
        {
            this.init();
            errMsg = "";

            //string sql1 = "select min(ID_KEY) as ID_KEY,UGROUP from WebUser group by UGROUP order by ID_KEY";
            string sql = "select T_CLASSID,T_CLASSNAME from T_SYS_CLASS order by T_CLASSID";

            DataTable dt=null;

            if (rlDBType == "SQL")
            {
                //dt = DBsql.RunDataTable(sql, out errMsg);
            }
            else
            {
                dt = DBdb2.RunDataTable(sql, out errMsg);
            }
            return dt;
        }

        //保存新的职别（班组）信息
        public bool InsertBZ(string BzId, string BzMs, out string errMsg)
        {
            errMsg = "";
            bool flag = false;

            this.init();

            string sql1 = "select * from T_SYS_CLASS where T_CLASSID='" + BzId + "'";

            DataTable dt=null;

            if (rlDBType == "SQL")
            {
                //dt = DBsql.RunDataTable(sql1, out errMsg);
            }
            else
            {
                dt = DBdb2.RunDataTable(sql1, out errMsg);
            }
            if (dt == null || dt.Rows.Count == 0)
            {
                string sql2 = "insert into T_SYS_CLASS (T_CLASSID,T_CLASSNAME) values ('" + BzId + "','" + BzMs + "')";
                if (rlDBType == "SQL")
                {
                    //DBsql.RunNonQuery(sql2, out errMsg);
                }
                else
                {
                    DBdb2.RunNonQuery(sql2, out errMsg);
                }
                if (errMsg == "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //编辑原有的职别（班组）信息
        public bool UpdateBZ(string OBzId, string BzId, string BzMs, out string errMsg)
        {
            errMsg = "";
            this.init();
            if (BzId==OBzId)
            {
                string sql = "update T_SYS_CLASS set T_CLASSNAME='" + BzMs + "' where T_CLASSID='" + OBzId + "'";
                if (rlDBType == "SQL")
                {
                    //DBsql.RunNonQuery(sql, out errMsg);
                }
                else
                {
                    DBdb2.RunNonQuery(sql, out errMsg);
                }
                if (errMsg == "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                string sql1 = "select * from T_SYS_CLASS where T_CLASSID='" + BzId + "'";

                DataTable dt=null;

                if (rlDBType == "SQL")
                {
                    //dt = DBsql.RunDataTable(sql1, out errMsg);
                }
                else
                {
                    dt = DBdb2.RunDataTable(sql1, out errMsg);
                }
                if (dt == null || dt.Rows.Count == 0)
                {
                    string sql2 = "update T_SYS_CLASS set T_CLASSID='" + BzId + "',T_CLASSNAME='" + BzMs + "' where T_CLASSID='" + OBzId + "'";
                    if (rlDBType == "SQL")
                    {
                        //DBsql.RunNonQuery(sql2, out errMsg);
                    }
                    else
                    {
                        DBdb2.RunNonQuery(sql2, out errMsg);
                    }
                    if (errMsg == "")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        //删除原有的职别（班组）信息
        public bool DeleteBZ(string BzId, out string errMsg)
        {
            errMsg = "";
            bool flag = true;
            this.init();

            string sql = "delete from T_SYS_CLASS where T_CLASSID='" + BzId + "'";
            if (rlDBType == "SQL")
            {
                //DBsql.RunNonQuery(sql, out errMsg);
            }
            else
            {
                DBdb2.RunNonQuery(sql, out errMsg);
            }
            if (errMsg == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //得到所有班次的信息列表
        public ArrayList BClist(out string errMsg)
        {
            ArrayList BClist = new ArrayList();
            int[] bc1 = new int[45];
            string[] bc2 = new string[45];
            string[] bc3 = new string[45];
            string[] bc4 = new string[45];
            this.init();
            errMsg = "";
            string sql = "select ID_KEY,T_SHIFT,T_STARTTIME,T_ENDTIME from T_SYS_SHIFT order by ID_KEY";
            DataTable dt=null;
            if (rlDBType == "SQL")
            {
                //dt = DBsql.RunDataTable(sql, out errMsg);
            }
            else
            {
                dt = DBdb2.RunDataTable(sql, out errMsg);
            }
            if (dt.Rows.Count > 0 && dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bc1[i] = int.Parse(dt.Rows[i]["ID_KEY"].ToString());
                    bc2[i] = dt.Rows[i]["T_SHIFT"].ToString();
                    bc3[i] = dt.Rows[i]["T_STARTTIME"].ToString();
                    bc4[i] = dt.Rows[i]["T_ENDTIME"].ToString();
                }
            }
            else
            {
                bc1 = null;
                bc2 = null;
                bc3 = null;
                bc4 = null;
            }
            BClist.Add(bc1);
            BClist.Add(bc2);
            BClist.Add(bc3);
            BClist.Add(bc4);

            return BClist;
        }

        //得到所有职别（班组）的信息列表
        public ArrayList BZlist(out string errMsg)
        {
            ArrayList BZlist = new ArrayList();
            string[] bz1 = new string[45];
            string[] bz2 = new string[45];
            this.init();
            errMsg = "";
            string sql = "select T_CLASSID,T_CLASSNAME from T_SYS_CLASS order by T_CLASSID";
            DataTable dt=null;
            if (rlDBType == "SQL")
            {
                //dt = DBsql.RunDataTable(sql, out errMsg);
            }
            else
            {
                dt = DBdb2.RunDataTable(sql, out errMsg);
            }
            if (dt.Rows.Count > 0 && dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bz1[i] = dt.Rows[i]["T_CLASSID"].ToString();
                    bz2[i] = dt.Rows[i]["T_CLASSNAME"].ToString();
                }
            }
            else
            {
                bz1 = null;
                bz2 = null;

            }
            BZlist.Add(bz1);
            BZlist.Add(bz2);

            return BZlist;
        }

        //根据职别（班组）描述得到职别（班组）的编号
        public string BZIDbyBZMS(string bzms, out string errMsg)
        {
            errMsg = "";
            this.init();
            string[] bzid = new string[45];
            string sql = "select T_CLASSID from T_SYS_CLASS where T_CLASSNAME='" + bzms + "'";
            DataTable dt=null;
            if (rlDBType == "SQL")
            {
                //dt = DBsql.RunDataTable(sql, out errMsg);
            }
            else
            {
                dt = DBdb2.RunDataTable(sql, out errMsg);
            }
            if (dt.Rows.Count > 0 && dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bzid[i] = dt.Rows[i]["T_CLASSID"].ToString();
                }
            }
            return bzid[0];
        }

        //清空T_SYS_DUTY表
        public bool EmptyPB(out string errMsg)
        {
            errMsg = "";
            bool flag = true;
            this.init();
            string sql = "delete from T_SYS_DUTY";
            if (rlDBType == "SQL")
            {
                //DBsql.RunNonQuery(sql, out errMsg);
            }
            else
            {
                DBdb2.RunNonQuery(sql, out errMsg);
            }
            if (errMsg == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //将排班信息添加到T_SYS_DUTY表中
        public bool InsertPB(string sqldb2, string sqlsql, out string errMsg)
        {
            errMsg = "";
            bool flag = false;
            this.init();

            if (rlDBType == "SQL")
            {
                //DBsql.RunNonQuery(sqlsql, out errMsg);
            }
            else
            {
                DBdb2.RunNonQuery(sqldb2, out errMsg);
            }
            if (errMsg == "")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //根据Grid每页显示多少条记录返回所有班次信息
        public DataTable GetBCmenu(int sCount, int eCount)
        {
            string sql = "select * from (select ID_KEY,T_SHIFT,T_STARTTIME,T_ENDTIME ,rownumber() over(order by ID_KEY asc ) as rowid  from T_SYS_SHIFT)as a where a.rowid between " + sCount + " and " + eCount + "";
            string errMsg;
            DataTable dt = null;
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

        //共有多少条班次记录
        public int GetBCCount()
        {
            string errMsg;
            DataTable dt = new DataTable();
            //sql = "select count(*) From (select ID,ParamentID,RouteID,RouteName,AreaID,AreaName,DeviceID,DeviceName,ItemID,sTime,eTime,state,T_ITEMPOSITION,T_ITEMDESC,T_CONTENT,Types,I_STATUS,T_OBSERVE,T_UNIT,F_LOWER,F_UPPER,I_SPECTRUM,T_STARTTIME,T_PERIODTYPE,T_PERIODVALUE,T_STATUS,T_VALUE,T_SCANTIME,T_UPLOADTIME,I_STATUSS,rownumber() over(order by RouteID asc ) as rowid  from (select pl.ID,pl.ParamentID,pl.RouteID,pl.RouteName,pl.AreaID,pl.AreaName,pl.DeviceID,pl.DeviceName,pl.ItemID,pl.sTime,pl.eTime,pl.state,pl.T_ITEMPOSITION,pl.T_ITEMDESC,pl.T_CONTENT,pl.Types,pl.I_STATUS,pl.T_OBSERVE,pl.T_UNIT,pl.F_LOWER,pl.F_UPPER,pl.I_SPECTRUM,pl.T_STARTTIME,pl.T_PERIODTYPE,pl.T_PERIODVALUE,pl.T_STATUS,rs.T_VALUE,rs.T_SCANTIME,rs.T_UPLOADTIME,case when rs.I_STATUS=1 then '已上报' when rs.I_STATUS=2 then '异常' when rs.I_STATUS=3 then '报缺陷' when rs.I_STATUS=4 then '已解决' else '未上报' end I_STATUSS from (select rout.ID,rout.ParamentID,rout.RouteID,rout.RouteName,rout.AreaID,rout.AreaName,rout.DeviceID,rout.DeviceName,rout.ItemID,rout.sTime,rout.eTime,rout.state,i.T_ITEMPOSITION,i.T_ITEMDESC,i.T_CONTENT,case when i.T_TYPE='0' then '点检' else '巡检' end Types,i.I_STATUS,i.T_OBSERVE,i.T_UNIT,i.F_LOWER,i.F_UPPER,case when i.I_SPECTRUM=0 then '不是频谱' else '是频谱' end I_SPECTRUM,i.T_STARTTIME,case when i.T_PERIODTYPE=0 then '日' when i.T_PERIODTYPE=1 then '周' when i.T_PERIODTYPE=2 then '月' else '年' end T_PERIODTYPE,i.T_PERIODVALUE,case when i.T_STATUS=0 then '停机检测' else '起机检测' end T_STATUS from (select rou.ID,rou.ParamentID,rou.RouteID,rou.RouteName,rou.AreaID,rou.AreaName,rou.DeviceID,d.T_DEVICEDESC as DeviceName,rou.ItemID,rou.sTime,rou.eTime,rou.state from (select ro.ID,ro.ParamentID,ro.RouteID,ro.RouteName,ro.AreaID,a.T_AREANAME as AreaName,ro.DeviceID,ro.ItemID,ro.sTime,ro.eTime,ro.state from (select p.ID_KEY ID,r.T_ORGID as ParamentID,r.T_ROUTEID as RouteID,r.T_ROUTENAME as RouteName,p.T_AREAID as AreaID,p.T_DEVICEID DeviceID,p.T_ITEMID ItemID,p.T_STARTTIME  as sTime,p.T_ENDTIME as eTime,case  when p. I_STATUS=0 then '未完成' else '未完成' end state from (select user.T_ORGID,r.T_ROUTEID,r.T_ROUTENAME from (select T_ORGID from T_SYS_MEMBERRELATION where T_USERID='" + userID + "') user  inner join T_BASE_ROUTE r on user.T_ORGID=r.T_ORGID)r inner join T_INFO_QUESTCOMPLETE p on r.T_ROUTEID=p.T_ROUTEID where p.I_FLAG=1 and p.T_STARTTIME >='" + sTime + "' and p.T_ENDTIME<='" + eTime + "') ro inner join T_BASE_AREA a on ro.AreaID=a.T_AREAID) rou inner join T_BASE_DEVICE d on rou.DeviceID=d.T_DEVICEID) rout inner join T_BASE_ITEM i on rout.ItemID=i.T_ITEMID) pl left join T_INFO_QUEST rs on pl.RouteID=rs.T_ROUTEID and pl.AreaID=rs.T_AREAID and pl.DeviceID=rs.T_DEVICEID and pl.ItemID=rs.T_ITEMID and rs.T_SCANTIME between '" + sTime + "' and '" + eTime + "' order by RouteID desc) sdf)  as a;";
            string sql = "select count(*) from T_SYS_SHIFT";
            int count = DBdb2.RunRowCount(sql, out errMsg);
            return count;
        }

        //根据Grid每页显示多少条记录返回所有职别（班组）信息
        public DataTable GetBZmenu(int sCount, int eCount)
        {
            string sql = "select * from (select ID_KEY,T_CLASSID,T_CLASSNAME ,rownumber() over(order by ID_KEY asc ) as rowid  from T_SYS_CLASS)as a where a.rowid between " + sCount + " and " + eCount + "";
            string errMsg;
            DataTable dt = null;
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

        //共有多少条职别（班组）记录
        public int GetBZCount()
        {
            string errMsg;
            DataTable dt = new DataTable();
            //sql = "select count(*) From (select ID,ParamentID,RouteID,RouteName,AreaID,AreaName,DeviceID,DeviceName,ItemID,sTime,eTime,state,T_ITEMPOSITION,T_ITEMDESC,T_CONTENT,Types,I_STATUS,T_OBSERVE,T_UNIT,F_LOWER,F_UPPER,I_SPECTRUM,T_STARTTIME,T_PERIODTYPE,T_PERIODVALUE,T_STATUS,T_VALUE,T_SCANTIME,T_UPLOADTIME,I_STATUSS,rownumber() over(order by RouteID asc ) as rowid  from (select pl.ID,pl.ParamentID,pl.RouteID,pl.RouteName,pl.AreaID,pl.AreaName,pl.DeviceID,pl.DeviceName,pl.ItemID,pl.sTime,pl.eTime,pl.state,pl.T_ITEMPOSITION,pl.T_ITEMDESC,pl.T_CONTENT,pl.Types,pl.I_STATUS,pl.T_OBSERVE,pl.T_UNIT,pl.F_LOWER,pl.F_UPPER,pl.I_SPECTRUM,pl.T_STARTTIME,pl.T_PERIODTYPE,pl.T_PERIODVALUE,pl.T_STATUS,rs.T_VALUE,rs.T_SCANTIME,rs.T_UPLOADTIME,case when rs.I_STATUS=1 then '已上报' when rs.I_STATUS=2 then '异常' when rs.I_STATUS=3 then '报缺陷' when rs.I_STATUS=4 then '已解决' else '未上报' end I_STATUSS from (select rout.ID,rout.ParamentID,rout.RouteID,rout.RouteName,rout.AreaID,rout.AreaName,rout.DeviceID,rout.DeviceName,rout.ItemID,rout.sTime,rout.eTime,rout.state,i.T_ITEMPOSITION,i.T_ITEMDESC,i.T_CONTENT,case when i.T_TYPE='0' then '点检' else '巡检' end Types,i.I_STATUS,i.T_OBSERVE,i.T_UNIT,i.F_LOWER,i.F_UPPER,case when i.I_SPECTRUM=0 then '不是频谱' else '是频谱' end I_SPECTRUM,i.T_STARTTIME,case when i.T_PERIODTYPE=0 then '日' when i.T_PERIODTYPE=1 then '周' when i.T_PERIODTYPE=2 then '月' else '年' end T_PERIODTYPE,i.T_PERIODVALUE,case when i.T_STATUS=0 then '停机检测' else '起机检测' end T_STATUS from (select rou.ID,rou.ParamentID,rou.RouteID,rou.RouteName,rou.AreaID,rou.AreaName,rou.DeviceID,d.T_DEVICEDESC as DeviceName,rou.ItemID,rou.sTime,rou.eTime,rou.state from (select ro.ID,ro.ParamentID,ro.RouteID,ro.RouteName,ro.AreaID,a.T_AREANAME as AreaName,ro.DeviceID,ro.ItemID,ro.sTime,ro.eTime,ro.state from (select p.ID_KEY ID,r.T_ORGID as ParamentID,r.T_ROUTEID as RouteID,r.T_ROUTENAME as RouteName,p.T_AREAID as AreaID,p.T_DEVICEID DeviceID,p.T_ITEMID ItemID,p.T_STARTTIME  as sTime,p.T_ENDTIME as eTime,case  when p. I_STATUS=0 then '未完成' else '未完成' end state from (select user.T_ORGID,r.T_ROUTEID,r.T_ROUTENAME from (select T_ORGID from T_SYS_MEMBERRELATION where T_USERID='" + userID + "') user  inner join T_BASE_ROUTE r on user.T_ORGID=r.T_ORGID)r inner join T_INFO_QUESTCOMPLETE p on r.T_ROUTEID=p.T_ROUTEID where p.I_FLAG=1 and p.T_STARTTIME >='" + sTime + "' and p.T_ENDTIME<='" + eTime + "') ro inner join T_BASE_AREA a on ro.AreaID=a.T_AREAID) rou inner join T_BASE_DEVICE d on rou.DeviceID=d.T_DEVICEID) rout inner join T_BASE_ITEM i on rout.ItemID=i.T_ITEMID) pl left join T_INFO_QUEST rs on pl.RouteID=rs.T_ROUTEID and pl.AreaID=rs.T_AREAID and pl.DeviceID=rs.T_DEVICEID and pl.ItemID=rs.T_ITEMID and rs.T_SCANTIME between '" + sTime + "' and '" + eTime + "' order by RouteID desc) sdf)  as a;";
            string sql = "select count(*) from T_SYS_CLASS";
            int count = DBdb2.RunRowCount(sql, out errMsg);
            return count;
        }

        //上传文档
        public bool RetBoolUpFile(byte[] fileBytes, out string errMsg)
        {
            errMsg = "";
            bool flag = false;

            IsEmptyXmlMenu("Webmenu", out errMsg);

            if (fileBytes.Length > 0)
            {
                string sql = "update T_SYS_MENU set B_XML=? where T_XMLID='Webmenu'";

                OleDbConnection con = new OleDbConnection(DBdb2.SetConString());
                try
                {
                    con.Open();
                    OleDbCommand oledbcom = new OleDbCommand(sql, con);

                    oledbcom.Parameters.Add("?", fileBytes);

                    if (oledbcom.ExecuteNonQuery() > 0)
                        flag = true;

                    con.Close();
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                }
                finally { con.Close(); }
            }

            return flag;
        }

        #region 根据每页显示多少条数据返回用户信息
        public DataTable GetUserMenu(string id, string treeID, int sCount, int eCount)
        {
            this.init();
            //string sql = "select * from (select ID_KEY,T_USERID,T_USERNAME,rownumber() over(order by ID_KEY asc ) as rowid  from T_SYS_GROUP)as a where a.rowid between " + sCount + " and " + eCount + "";
            string sql = "select * from ( select a.ID_KEY,a.T_USERID,a.T_USERNAME,rownumber() over(order by ID_KEY asc ) as rowid from (select T_SYS_MEMBERINFO.ID_KEY,T_SYS_MEMBERINFO.T_USERID,T_SYS_MEMBERINFO.T_USERNAME,T_SYS_MEMBERORG.T_ORGID,T_SYS_MEMBERORG.T_TREEID from T_SYS_MEMBERINFO left JOIN T_SYS_MEMBERORG ON T_SYS_MEMBERORG.T_USERID=T_SYS_MEMBERINFO.T_USERID ORDER BY T_SYS_MEMBERINFO.ID_KEY)as a where a.T_ORGID='" + id + "' and a.T_TREEID='" + treeID + "' ) as b where b.rowid between " + sCount + " and " + eCount + "";
            string sql2 = "select * from ( select ID_KEY,T_USERID,T_USERNAME,ROWNUM rn from (select T_SYS_MEMBERINFO.ID_KEY,T_SYS_MEMBERINFO.T_USERID,T_SYS_MEMBERINFO.T_USERNAME,T_SYS_MEMBERORG.T_ORGID,T_SYS_MEMBERORG.T_TREEID from T_SYS_MEMBERINFO left JOIN T_SYS_MEMBERORG ON T_SYS_MEMBERORG.T_USERID=T_SYS_MEMBERINFO.T_USERID ORDER BY T_SYS_MEMBERINFO.ID_KEY) where  T_ORGID='" + id + "' and  T_TREEID='" + treeID + "' and ROWNUM <= " + eCount + ")WHERE rn >= " + sCount + "";
            string errMsg;
            DataTable dt = null;
            if (rlDBType == "DB2")
            {
                try
                {
                    dt = DBdb2.RunDataTable(sql, out errMsg);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            else if (rlDBType == "Oracle")
            {
                try
                {
                   // dt = OracleHelper.GetDataTable(sql2);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            return dt;
        }
        #endregion

        #region 根据组织机构ID和组织机构数ID返回所有用户信息的条数
        public int GetUserCount(string id, string treeID)
        {
            this.init();
            string sql = "select count(*) from ( select a.ID_KEY,a.T_USERID,a.T_USERNAME from (select T_SYS_MEMBERINFO.ID_KEY,T_SYS_MEMBERINFO.T_USERID,T_SYS_MEMBERINFO.T_USERNAME,T_SYS_MEMBERORG.T_ORGID,T_SYS_MEMBERORG.T_TREEID from T_SYS_MEMBERINFO left JOIN T_SYS_MEMBERORG ON T_SYS_MEMBERORG.T_USERID=T_SYS_MEMBERINFO.T_USERID ORDER BY T_SYS_MEMBERINFO.ID_KEY)as a where a.T_ORGID='" + id + "' and a.T_TREEID='" + treeID + "')as b";
            string sql2 = "select count(*) from ( select ID_KEY,T_USERID,T_USERNAME from (select T_SYS_MEMBERINFO.ID_KEY,T_SYS_MEMBERINFO.T_USERID,T_SYS_MEMBERINFO.T_USERNAME,T_SYS_MEMBERORG.T_ORGID,T_SYS_MEMBERORG.T_TREEID from T_SYS_MEMBERINFO left JOIN T_SYS_MEMBERORG ON T_SYS_MEMBERORG.T_USERID=T_SYS_MEMBERINFO.T_USERID ORDER BY T_SYS_MEMBERINFO.ID_KEY) where T_ORGID='" + id + "' and T_TREEID='" + treeID + "')";
            string errMsg;
            int count = 0;
            if (rlDBType == "DB2")
            {
                try
                {
                    count = DBdb2.RunRowCount(sql, out errMsg);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            else if (rlDBType == "Oracle")
            {
                try
                {
                    //DataTable dt = OracleHelper.GetDataTable(sql2);
                   // count = int.Parse(dt.Rows[0]["COUNT(*)"].ToString());
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            return count;
        }
        #endregion

        /// <summary>
        /// 获取组织机构树节点
        /// </summary>
        /// <param name="usrID">用户ID</param>
        /// <param name="TreeName">组织机构树名称</param>
        /// <returns></returns>
        public DataSet GetTreeNodeID(string userID, string TreeName)
        {
            this.init();
            DataSet ds = new DataSet();
            string errMsg = "";
            if (rlDBType == "DB2")
            {
                try
                {
                    string sql = "select T_ORGID from T_SYS_MEMBERORG where T_USERID='" + userID + "' and T_TREEID='" + TreeName + "'";
                    //ds = OracleHelper.GetDataSet(sql);
                    //ds = OracleHelper.QueryDataSet(sql);
                    ds = DBdb2.RunDataSet(sql, out errMsg);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "方法名称：GetTreeNodeID/n/r 发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            else if (rlDBType == "Oracle")
            {
                try
                {
                    //string sql = "select T_ORGID from T_SYS_MEMBERORG where T_USERID='" + userID + "' and T_TREEID='" + TreeName + "'";
                    //ds = OracleHelper.GetDataSet(sql);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "方法名称：GetTreeNodeID/n/r 发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            return ds;
        }

        //返回所有组织机构ID和中文描述
        public DataTable GetTreeMenu(out string errMsg)
        {
            this.init();
            string sql = "select T_XMLNAME,T_XMLID from T_SYS_MENU where T_XMLID!='Webmenu'";
            errMsg = "";
            DataTable dt = null;
            if (rlDBType == "DB2")
            {
                try
                {
                    dt = DBdb2.RunDataTable(sql, out errMsg);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            else if (rlDBType == "Oracle")
            {
                try
                {
                    //dt = OracleHelper.GetDataTable(sql);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            return dt;
        }

        #region 判断是否存在该人员
        /// <summary>
        /// 判断是否存在该人员
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <returns></returns>
        public bool JudgMember(string id)
        {
            this.init();
            string errMsg;
            int count = 0;
            bool result = false;
            if (rlDBType == "DB2")
            {
                try
                {
                    string sql = "select count(*) from T_SYS_MEMBERINFO where T_USERID='" + id + "'";
                    count = DBdb2.RunRowCount(sql, out errMsg);
                    if (count > 0)
                        result = true;
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            else if (rlDBType == "Oracle")
            {
                try
                {
                    //string sql = "select count(*) from T_SYS_MEMBERINFO where T_USERID='" + id + "'";
                    //DataTable dt = OracleHelper.GetDataTable(sql);
                    //count = int.Parse(dt.Rows[0]["count(*)"].ToString());
                    //if (count > 0)
                    //    result = true;
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            return result;
        }
        #endregion

        #region 添加人员信息
        /// <summary>
        /// 添加人员信息
        /// </summary>
        /// <param name="id">用户编码</param>
        /// <param name="name">真实姓名</param>
        /// <param name="pwd">登陆密码</param>
        /// <param name="img">图片</param>
        /// <returns></returns>
        public bool AddMember(string id, string name, string pwd, byte[] img,string orgID,string treeID)
        {
            this.init();
            string errMsg;
            string sql1 = "";
            string sql2 = "";
            bool result = false;
            if (rlDBType == "DB2")
            {
                if (img != null && img.Length > 0)
                {
                    sql1 = "insert into T_SYS_MEMBERINFO(T_USERID,T_USERNAME,T_PASSWD,B_ATTACHMENT) values(?,?,?,?);";
                }
                else
                {
                    sql1 = "insert into T_SYS_MEMBERINFO(T_USERID,T_USERNAME,T_PASSWD) values(?,?,?);";
                }
                sql2 = "insert into T_SYS_MEMBERORG(T_USERID,T_ORGID,T_TREEID) values('" + id + "','" + orgID + "','" + treeID + "')";
                try
                {
                    OleDbConnection con = new OleDbConnection(DBdb2.SetConString());
                    con.Open();
                    OleDbCommand oledbcom = new OleDbCommand(sql1, con);
                    if (img != null && img.Length > 0)
                    {
                        oledbcom.Parameters.Add("?", id);
                        oledbcom.Parameters.Add("?", name);
                        oledbcom.Parameters.Add("?", pwd);
                        oledbcom.Parameters.Add("?", img);
                    }
                    else
                    {
                        oledbcom.Parameters.Add("?", id);
                        oledbcom.Parameters.Add("?", name);
                        oledbcom.Parameters.Add("?", pwd);
                    }
                    if (oledbcom.ExecuteNonQuery() > 0)
                        result = true;
                    con.Close();
                    DBdb2.RunNonQuery(sql2, out errMsg);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            else if (rlDBType == "Oracle")
            {
                //int idKeyMI = GetIDKEY("T_SYS_MEMBERINFO");
                //int idKeyMO = GetIDKEY("T_SYS_MEMBERORG");
                //if (img != null && img.Length > 0)
                //{
                //    sql1 = "insert into T_SYS_MEMBERINFO(ID_KEY，T_USERID,T_USERNAME,T_PASSWD,B_ATTACHMENT) values(:blobtodb,:blobtodb,:blobtodb,:blobtodb,:blobtodb);";
                //}
                //else
                //{
                //    sql1 = "insert into T_SYS_MEMBERINFO(ID_KEY，T_USERID,T_USERNAME,T_PASSWD) values(:blobtodb,:blobtodb,:blobtodb,:blobtodb);";
                //}
                //sql2 = "insert into T_SYS_MEMBERORG(ID_KEY，T_USERID,T_ORGID,T_TREEID) values(" + idKeyMO + ",'" + id + "','" + orgID + "','" + treeID + "')";
                //try
                //{
                //    //OracleConnection con = new OracleConnection(OracleHelper.retStr());
                //    //con.Open();
                //    //OracleCommand oledbcom = new OracleCommand(sql1, con);
                //    //if (img != null && img.Length > 0)
                //    //{
                //    //    oledbcom.Parameters.Add("blobtodb", idKeyMI);
                //    //    oledbcom.Parameters.Add("blobtodb", id);
                //    //    oledbcom.Parameters.Add("blobtodb", name);
                //    //    oledbcom.Parameters.Add("blobtodb", pwd);
                //    //    oledbcom.Parameters.Add("blobtodb", img);
                //    //}
                //    //else
                //    //{
                //    //    oledbcom.Parameters.Add("blobtodb", idKeyMI);
                //    //    oledbcom.Parameters.Add("blobtodb", id);
                //    //    oledbcom.Parameters.Add("blobtodb", name);
                //    //    oledbcom.Parameters.Add("blobtodb", pwd);
                //    //}
                //    //if (oledbcom.ExecuteNonQuery() > 0)
                //    //    result = true;
                //    //con.Close();
                //    //OracleHelper.RunNonQuery(sql2, out errMsg);
                //}
                //catch (Exception ex)
                //{
                //    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                //}
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
            this.init();
            string sql;
            string errMsg;
            DataTable dt = null;
            if (i == 1)
                sql = "select T_USERID,T_USERNAME,T_PASSWD from T_SYS_MEMBERINFO where T_USERID='" + id + "'";
            else
                sql = "select * from T_SYS_MEMBERINFO where T_USERID='" + id + "'";
            if (rlDBType == "DB2")
            {
                try
                {
                    dt = DBdb2.RunDataTable(sql, out errMsg);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            else if (rlDBType == "Oracle")
            {
                try
                {
                    //dt = OracleHelper.GetDataTable(sql);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            return dt;
        }
        #endregion

        #region 编辑人员信息
        public bool EditMemberInfo(string userIDO, string userID, string userName, string pwd, byte[] img, string treeNodeId, string treeAllId)
        {
            this.init();
            string sql="";
            string errMsg="";
            bool result = false;
            if (rlDBType == "DB2")
            {
                try
                {
                    if (img != null)
                    {
                        OleDbConnection con = new OleDbConnection(DBdb2.SetConString());
                        con.Open();
                        if (userIDO == userID)
                        {
                            sql += "update T_SYS_MEMBERINFO set T_USERNAME=?,T_PASSWD=?,B_ATTACHMENT=? where T_USERID='" + userIDO + "'";
                            OleDbCommand oledbcom = new OleDbCommand(sql, con);
                            oledbcom.Parameters.Add("?", userName);
                            oledbcom.Parameters.Add("?", pwd);
                            oledbcom.Parameters.Add("?", img);
                            if (oledbcom.ExecuteNonQuery() > 0)
                                result = true;
                        }
                        else
                        {
                            sql += "update T_SYS_MEMBERINFO set T_USERID=?,T_USERNAME=?,T_PASSWD=?,B_ATTACHMENT=? where T_USERID='" + userIDO + "'";
                            OleDbCommand oledbcom = new OleDbCommand(sql, con);
                            oledbcom.Parameters.Add("?", userID);
                            oledbcom.Parameters.Add("?", userName);
                            oledbcom.Parameters.Add("?", pwd);
                            oledbcom.Parameters.Add("?", img);
                            result = DBdb2.RunNonQuery("update T_SYS_MEMBERRELATION set T_USERID='" + userID + "' where T_USERID='" + userIDO + "';", out errMsg);
                            if (oledbcom.ExecuteNonQuery() > 0 && result == true)
                                result = true;
                        }
                        con.Close();
                    }
                    else
                    {
                        if (userIDO == userID)
                            sql += "update T_SYS_MEMBERINFO set T_USERNAME='" + userName + "',T_PASSWD='" + pwd + "' where T_USERID='" + userIDO + "';";
                        else
                            sql += "update T_SYS_MEMBERINFO set T_USERID='" + userID + "',T_USERNAME='" + userName + "',T_PASSWD='" + pwd + "' where T_USERID='" + userIDO + "';update T_SYS_MEMBERRELATION set T_USERID='" + userID + "' where T_USERID='" + userIDO + "';";
                        result = DBdb2.RunNonQuery(sql, out errMsg);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            else if (rlDBType == "Oracle")
            {
                try
                {
                    //if (img != null)
                    //{
                    //    OracleConnection con = new OracleConnection(OracleHelper.retStr());
                    //    con.Open();
                    //    if (userIDO == userID)
                    //    {
                    //        sql += "update T_SYS_MEMBERINFO set T_USERNAME=:blobtodb,T_PASSWD=:blobtodb,B_ATTACHMENT=:blobtodb where T_USERID='" + userIDO + "'";
                    //        OracleCommand orlcmd = new OracleCommand(sql, con);
                    //        orlcmd.Parameters.Add("blobtodb", userName);
                    //        orlcmd.Parameters.Add("blobtodb", pwd);
                    //        orlcmd.Parameters.Add("blobtodb", img);
                    //        if (orlcmd.ExecuteNonQuery() > 0)
                    //            result = true;
                    //    }
                    //    else
                    //    {
                    //        sql += "update T_SYS_MEMBERINFO set T_USERID=:blobtodb,T_USERNAME=:blobtodb,T_PASSWD=:blobtodb,B_ATTACHMENT=:blobtodb where T_USERID='" + userIDO + "'";
                    //        OracleCommand orlcmd = new OracleCommand(sql, con);
                    //        orlcmd.Parameters.Add("blobtodb", userID);
                    //        orlcmd.Parameters.Add("blobtodb", userName);
                    //        orlcmd.Parameters.Add("blobtodb", pwd);
                    //        orlcmd.Parameters.Add("blobtodb", img);
                    //        result = OracleHelper.RunNonQuery("update T_SYS_MEMBERGRP set T_USERID='" + userID + "' where T_USERID='" + userIDO + "';", out errMsg);
                    //        if (orlcmd.ExecuteNonQuery() > 0 && result == true)
                    //            result = true;
                    //    }
                    //    con.Close();
                    //}
                    //else
                    //{
                    //    if (userIDO == userID)
                    //        sql += "update T_SYS_MEMBERINFO set T_USERNAME='" + userName + "',T_PASSWD='" + pwd + "' where T_USERID='" + userIDO + "';";
                    //    else
                    //        sql += "update T_SYS_MEMBERINFO set T_USERID='" + userID + "',T_USERNAME='" + userName + "',T_PASSWD='" + pwd + "' where T_USERID='" + userIDO + "';update T_SYS_MEMBERGRP set T_USERID='" + userID + "' where T_USERID='" + userIDO + "';";
                    //    result = OracleHelper.RunNonQuery(sql,out errMsg);
                    //}
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
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
            this.init();
            string errMsg;
            bool result = false;
            string sql = "delete from T_SYS_MEMBERINFO where T_USERID in (" + id + ");delete from T_SYS_MEMBERORG where T_USERID in (" + id + ");";
            if (rlDBType == "DB2")
            {
                try
                {
                    result = DBdb2.RunNonQuery(sql, out errMsg);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            else if (rlDBType == "Oracle")
            {
                try
                {
                    //result = OracleHelper.RunNonQuery(sql, out errMsg);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            return result;
        }
        #endregion

        #region 角色人员管理

        #region 根据每页显示多少条数据返回用户信息
        public DataTable GetUserMenuByRole(string id, int sCount, int eCount)
        {
            this.init();
            //string sql = "select * from (select ID_KEY,T_USERID,T_USERNAME,rownumber() over(order by ID_KEY asc ) as rowid  from T_SYS_GROUP)as a where a.rowid between " + sCount + " and " + eCount + "";
            string sql = "select * from ( select a.ID_KEY,a.T_USERID,a.T_USERNAME,rownumber() over(order by ID_KEY asc ) as rowid from (select T_SYS_MEMBERINFO.ID_KEY,T_SYS_MEMBERINFO.T_USERID,T_SYS_MEMBERINFO.T_USERNAME,T_SYS_MEMBERGRP.T_GRPID from T_SYS_MEMBERINFO left JOIN T_SYS_MEMBERGRP ON T_SYS_MEMBERGRP.T_USERID=T_SYS_MEMBERINFO.T_USERID ORDER BY T_SYS_MEMBERINFO.ID_KEY)as a where a.T_GRPID='" + id + "') as b where b.rowid between " + sCount + " and " + eCount + "";
            string sql2 = "select * from(select ID_KEY,T_USERID,T_USERNAME,ROWNUM rn from(select T_SYS_MEMBERINFO.ID_KEY,T_SYS_MEMBERINFO.T_USERID,T_SYS_MEMBERINFO.T_USERNAME,T_SYS_MEMBERGRP.T_GRPID from T_SYS_MEMBERINFO left JOIN T_SYS_MEMBERGRP ON T_SYS_MEMBERGRP.T_USERID=T_SYS_MEMBERINFO.T_USERID ORDER BY T_SYS_MEMBERINFO.ID_KEY) where T_GRPID='" + id + "' and ROWNUM <= " + eCount + ")WHERE rn >= " + sCount + "";
            
            string errMsg;
            DataTable dt = null;
            if (rlDBType == "DB2")
            {
                try
                {
                    dt = DBdb2.RunDataTable(sql, out errMsg);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            else if (rlDBType == "Oracle")
            {
                try
                {
                    //dt = OracleHelper.GetDataTable(sql2);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            return dt;
        }
        #endregion

        #region 根据角色ID返回所有用户信息的条数
        public int GetUserCountByRole(string id)
        {
            this.init();
            string sql = "select count(*) from ( select a.ID_KEY,a.T_USERID,a.T_USERNAME from (select T_SYS_MEMBERINFO.ID_KEY,T_SYS_MEMBERINFO.T_USERID,T_SYS_MEMBERINFO.T_USERNAME,T_SYS_MEMBERGRP.T_GRPID from T_SYS_MEMBERINFO left JOIN T_SYS_MEMBERGRP ON T_SYS_MEMBERGRP.T_USERID=T_SYS_MEMBERINFO.T_USERID ORDER BY T_SYS_MEMBERINFO.ID_KEY)as a where a.T_GRPID='" + id + "')as b";
            string sql2 = "select count(*) from ( select ID_KEY,T_USERID,T_USERNAME from (select T_SYS_MEMBERINFO.ID_KEY,T_SYS_MEMBERINFO.T_USERID,T_SYS_MEMBERINFO.T_USERNAME,T_SYS_MEMBERGRP.T_GRPID from T_SYS_MEMBERINFO left JOIN T_SYS_MEMBERGRP ON T_SYS_MEMBERGRP.T_USERID=T_SYS_MEMBERINFO.T_USERID ORDER BY T_SYS_MEMBERINFO.ID_KEY) where T_GRPID='" + id + "')";
            string errMsg;
            int count = 0;
            if (rlDBType == "DB2")
            {
                try
                {
                    count = DBdb2.RunRowCount(sql, out errMsg);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            else if (rlDBType == "Oracle")
            {
                try
                {
                    //DataTable dt = OracleHelper.GetDataTable(sql);
                    //count = int.Parse(dt.Rows[0]["COUNT(*)"].ToString());
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            return count;
        }
        #endregion

        #endregion


        private string init()
        {
            rlDBType = IniHelper.ReadIniData("RelationDBbase", "DBType", null);
            rtDBType = IniHelper.ReadIniData("RTDB", "DBType", null);
            pGl1 = IniHelper.ReadIniData("Report", "FH1", null);
            pGl2 = IniHelper.ReadIniData("Report", "FH2", null);

            return rlDBType;
        }

        public string GetDBtype()
        {
            this.init();
            string DBtype = rlDBType;
            return DBtype;
        }

        // public int GetIDKEY(string tableName)//用于在Oracle数据库中自增长列
        //{
        //    //int idKeyM;
        //    //string sql = "select max(ID_KEY) from " + tableName + "";
        //    //DataTable dt = OracleHelper.GetDataTable(sql);
        //    //string idKey = dt.Rows[0]["max(ID_KEY)"].ToString();
        //    //if (idKey == "" || idKey == null)
        //    //{
        //    //    idKeyM = 0;
        //    //}
        //    //else
        //    //{
        //    //    idKeyM = int.Parse(idKey) + 1;
        //    //}
        //    //return idKeyM;
        //}

        //保存新的角色信息
        public bool SaveRole(string rId, string rName, out string errMsg)
        {
            errMsg = "";
            bool flag = false;

            this.init();

            string sql1 = "select * from T_SYS_GROUP where T_GRPID='" + rId + "'";

            DataTable dt = null;

            if (rlDBType == "SQL")
            {
                //dt = DBsql.RunDataTable(sql1, out errMsg);
            }
            else if (rlDBType == "DB2")
            {
                dt = DBdb2.RunDataTable(sql1, out errMsg);
            }
            else if (rlDBType == "Oracle")
            {
                //dt = OracleHelper.GetDataTable(sql1);
            }
            if (dt == null || dt.Rows.Count == 0)
            {
                //int idKey = GetIDKEY("T_SYS_GROUP");
                string sql2 = "insert into T_SYS_GROUP (T_GRPID,T_GRPDESC) values ('" + rId + "','" + rName + "')";
                //string sql3 = "insert into T_SYS_GROUP (ID_KEY,T_GRPID,T_GRPDESC) values (" + idKey + ",'" + rId + "','" + rName + "')";
                if (rlDBType == "SQL")
                {
                    //DBsql.RunNonQuery(sql2, out errMsg);
                }
                else if (rlDBType == "DB2")
                {
                    DBdb2.RunNonQuery(sql2, out errMsg);
                }
                else if (rlDBType == "Oracle")
                {
                    //OracleHelper.RunNonQuery(sql3, out errMsg);
                }
                if (errMsg == "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //编辑原有的职别（班组）信息
        public bool UpDateRole(string OrId, string rId, string rName, out string errMsg)
        {
            errMsg = "";
            this.init();
            if (rId == OrId)
            {
                string sql = "update T_SYS_GROUP set T_GRPDESC='" + rName + "' where T_GRPID='" + OrId + "'";
                if (rlDBType == "SQL")
                {
                    //DBsql.RunNonQuery(sql, out errMsg);
                }
                else if (rlDBType == "DB2")
                {
                    DBdb2.RunNonQuery(sql, out errMsg);
                }
                else if (rlDBType == "Oracle")
                {
                    //OracleHelper.RunNonQuery(sql, out errMsg);
                }
                if (errMsg == "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                string sql1 = "select * from T_SYS_GROUP where T_GRPID='" + rId + "'";

                DataTable dt = null;

                if (rlDBType == "SQL")
                {
                    //dt = DBsql.RunDataTable(sql1, out errMsg);
                }
                else
                {
                    dt = DBdb2.RunDataTable(sql1, out errMsg);
                }
                if (dt == null || dt.Rows.Count == 0)
                {
                    string sql2 = "update T_SYS_GROUP set T_GRPID='" + rId + "',T_GRPDESC='" + rName + "' where T_GRPID='" + OrId + "'";
                    if (rlDBType == "SQL")
                    {
                        //DBsql.RunNonQuery(sql2, out errMsg);
                    }
                    else
                    {
                        DBdb2.RunNonQuery(sql2, out errMsg);
                    }
                    if (errMsg == "")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        //删除原有的职别（班组）信息
        public bool DeleteRole(string rId, out string errMsg)
        {
            errMsg = "";
            bool flag = true;
            this.init();

            string sql = "delete from T_SYS_GROUP where T_GRPID='" + rId + "'";
            if (rlDBType == "SQL")
            {
                //DBsql.RunNonQuery(sql, out errMsg);
            }
            else if (rlDBType == "DB2")
            {
                DBdb2.RunNonQuery(sql, out errMsg);
            }
            else if (rlDBType == "Oracle")
            {
                //OracleHelper.RunNonQuery(sql, out errMsg);
            }
            if (errMsg == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region 角色管理
        //根据Grid每页显示多少条记录返回所有角色信息
        public DataTable GetAllRole(int sCount, int eCount)
        {
            this.init();
            string sql = "select * from (select ID_KEY,T_GRPID,T_GRPDESC ,rownumber() over(order by ID_KEY asc ) as rowid  from T_SYS_GROUP)as a where a.rowid between " + sCount + " and " + eCount + "";
            string sql2 = "select * from (select ID_KEY,T_GRPID,T_GRPDESC ,ROWNUM rn from T_SYS_GROUP WHERE ROWNUM <= " + eCount + ") WHERE rn >= " + sCount + "";
            string errMsg;
            DataTable dt = null;
            if (rlDBType == "DB2")
            {
                try
                {
                    dt = DBdb2.RunDataTable(sql, out errMsg);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            else if (rlDBType == "Oracle")
            {
                try
                {
                    //dt = OracleHelper.GetDataTable(sql2);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(LogHelper.EnLogType.Run, "发生时间：" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "/n错误信息：" + ex.Message);
                }
            }
            return dt;
        }
        //共有多少条角色记录
        public int GetRoleCount()
        {
            this.init();
            string errMsg;
            DataTable dt = new DataTable();
            int count = 0;
            //sql = "select count(*) From (select ID,ParamentID,RouteID,RouteName,AreaID,AreaName,DeviceID,DeviceName,ItemID,sTime,eTime,state,T_ITEMPOSITION,T_ITEMDESC,T_CONTENT,Types,I_STATUS,T_OBSERVE,T_UNIT,F_LOWER,F_UPPER,I_SPECTRUM,T_STARTTIME,T_PERIODTYPE,T_PERIODVALUE,T_STATUS,T_VALUE,T_SCANTIME,T_UPLOADTIME,I_STATUSS,rownumber() over(order by RouteID asc ) as rowid  from (select pl.ID,pl.ParamentID,pl.RouteID,pl.RouteName,pl.AreaID,pl.AreaName,pl.DeviceID,pl.DeviceName,pl.ItemID,pl.sTime,pl.eTime,pl.state,pl.T_ITEMPOSITION,pl.T_ITEMDESC,pl.T_CONTENT,pl.Types,pl.I_STATUS,pl.T_OBSERVE,pl.T_UNIT,pl.F_LOWER,pl.F_UPPER,pl.I_SPECTRUM,pl.T_STARTTIME,pl.T_PERIODTYPE,pl.T_PERIODVALUE,pl.T_STATUS,rs.T_VALUE,rs.T_SCANTIME,rs.T_UPLOADTIME,case when rs.I_STATUS=1 then '已上报' when rs.I_STATUS=2 then '异常' when rs.I_STATUS=3 then '报缺陷' when rs.I_STATUS=4 then '已解决' else '未上报' end I_STATUSS from (select rout.ID,rout.ParamentID,rout.RouteID,rout.RouteName,rout.AreaID,rout.AreaName,rout.DeviceID,rout.DeviceName,rout.ItemID,rout.sTime,rout.eTime,rout.state,i.T_ITEMPOSITION,i.T_ITEMDESC,i.T_CONTENT,case when i.T_TYPE='0' then '点检' else '巡检' end Types,i.I_STATUS,i.T_OBSERVE,i.T_UNIT,i.F_LOWER,i.F_UPPER,case when i.I_SPECTRUM=0 then '不是频谱' else '是频谱' end I_SPECTRUM,i.T_STARTTIME,case when i.T_PERIODTYPE=0 then '日' when i.T_PERIODTYPE=1 then '周' when i.T_PERIODTYPE=2 then '月' else '年' end T_PERIODTYPE,i.T_PERIODVALUE,case when i.T_STATUS=0 then '停机检测' else '起机检测' end T_STATUS from (select rou.ID,rou.ParamentID,rou.RouteID,rou.RouteName,rou.AreaID,rou.AreaName,rou.DeviceID,d.T_DEVICEDESC as DeviceName,rou.ItemID,rou.sTime,rou.eTime,rou.state from (select ro.ID,ro.ParamentID,ro.RouteID,ro.RouteName,ro.AreaID,a.T_AREANAME as AreaName,ro.DeviceID,ro.ItemID,ro.sTime,ro.eTime,ro.state from (select p.ID_KEY ID,r.T_ORGID as ParamentID,r.T_ROUTEID as RouteID,r.T_ROUTENAME as RouteName,p.T_AREAID as AreaID,p.T_DEVICEID DeviceID,p.T_ITEMID ItemID,p.T_STARTTIME  as sTime,p.T_ENDTIME as eTime,case  when p. I_STATUS=0 then '未完成' else '未完成' end state from (select user.T_ORGID,r.T_ROUTEID,r.T_ROUTENAME from (select T_ORGID from T_SYS_MEMBERRELATION where T_USERID='" + userID + "') user  inner join T_BASE_ROUTE r on user.T_ORGID=r.T_ORGID)r inner join T_INFO_QUESTCOMPLETE p on r.T_ROUTEID=p.T_ROUTEID where p.I_FLAG=1 and p.T_STARTTIME >='" + sTime + "' and p.T_ENDTIME<='" + eTime + "') ro inner join T_BASE_AREA a on ro.AreaID=a.T_AREAID) rou inner join T_BASE_DEVICE d on rou.DeviceID=d.T_DEVICEID) rout inner join T_BASE_ITEM i on rout.ItemID=i.T_ITEMID) pl left join T_INFO_QUEST rs on pl.RouteID=rs.T_ROUTEID and pl.AreaID=rs.T_AREAID and pl.DeviceID=rs.T_DEVICEID and pl.ItemID=rs.T_ITEMID and rs.T_SCANTIME between '" + sTime + "' and '" + eTime + "' order by RouteID desc) sdf)  as a;";
            string sql = "select count(*) from T_SYS_GROUP";
            string sql2 = "select count(*) from T_SYS_GROUP";
            if (rlDBType == "DB2")
            {
                count = DBdb2.RunRowCount(sql, out errMsg);
            }
            else if (rlDBType == "Oracle")
            {
                //dt = OracleHelper.GetDataTable(sql2);
                //count = int.Parse(dt.Rows[0]["count(*)"].ToString());
            }
            return count;
        }


        //上传文档
        public bool RetBoolUpFile(string xmlID, string xmlName, byte[] fileBytes, out string errMsg)
        {
            this.init();
            errMsg = "";
            bool flag = false;

            IsEmptyXmlMenu(xmlID, out errMsg);

            if (fileBytes.Length > 0)
            {
                if (rlDBType == "DB2")
                {
                    string sql = "update T_SYS_MENU set B_XML=? , T_XMLNAME='" + xmlName + "' where T_XMLID='" + xmlID + "'";

                    OleDbConnection con = new OleDbConnection(DBdb2.SetConString());
                    try
                    {
                        con.Open();
                        OleDbCommand oledbcom = new OleDbCommand(sql, con);

                        oledbcom.Parameters.Add("?", fileBytes);

                        if (oledbcom.ExecuteNonQuery() > 0)
                            flag = true;

                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        errMsg = ex.Message;
                    }
                    finally { con.Close(); }
                }
                else if (rlDBType == "Oracle")
                {
                    //string sql = "update T_SYS_MENU set B_XML=:blobtodb , T_XMLNAME='" + xmlName + "' where T_XMLID='" + xmlID + "'";
                    //string sqlcon = GetConnstr(out errMsg);
                    //OracleConnection con = new OracleConnection(sqlcon);
                    //try
                    //{
                    //    con.Open();
                    //    OracleCommand orlcmd = new OracleCommand(sql, con);
                    //    orlcmd.Parameters.Add("blobtodb", fileBytes);
                    //    if (orlcmd.ExecuteNonQuery() > 0)
                    //    {
                    //        flag = true;
                    //    }
                    //    con.Close();
                    //}
                    //catch (Exception ex)
                    //{
                    //    errMsg = ex.Message;
                    //}
                    //finally { con.Close(); }
                }
            }

            return flag;
        }

        #endregion
    }
}

