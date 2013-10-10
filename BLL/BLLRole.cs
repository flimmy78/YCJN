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
    public class BLLRole
    {
        string errMsg = "";
        DateHelper pb = new DateHelper();
        DALRole dr = new DALRole();


        //判断存储XML的表是否为空，如果为空则添加一条T_XMLID字段的值为Webmenu的记录
        public bool IsEmptyXmlMenu(string T_XMLID, out string errMsg)
        {
            return dr.IsEmptyXmlMenu(T_XMLID, out errMsg);
        }

        //根据所给的用户名查找其对应的密码
        public string GetPwd(string username, out string errMsg)
        {
            return dr.GetPwd(username, out errMsg);
        }

        //根据所给的用户名查出表中共有多少条记录（一般为1或0个）
        public string GetUnmN(string username, out string errMsg)
        {
            return dr.GetUnmN(username, out errMsg);
        }

        //根据所给的用户名查找其对应的ID_KEY
        public string GetIdKeyByUN(string username, out string errMsg)
        {
            return dr.GetIdKeyByUN(username, out errMsg);
        }

        //根据所给的ID_KEY查找其对应的用户名
        public string GetUserNameById(string idkey, out string errMsg)
        {
            return dr.GetUserNameById(idkey, out errMsg);
        }

        //根据所给的ID_KEY查找其对应的用户姓名
        public string GetUserRealNameById(string userId, out string errMsg)
        {
            return dr.GetUserRealNameById(userId, out errMsg);
        }

        /// 根据所给的岗位ID查找其对应的岗位描述
        public string GetRoleNameById(string roleId, out string errMsg)
        {
            return dr.GetRoleNameById(roleId, out errMsg);
        }

        //根据所给的用户ID查找该用户所有的岗位
        public ArrayList GetRolesByUserName(string username, out string errMsg)
        {
            return dr.GetRolesByUserName(username, out errMsg);
        }

        //返回数据库的连接字符串
        public string GetConnstr(out string errMsg)
        {
            return dr.GetConnstr(out errMsg);
        }

        //返回所有班次信息
        public DataTable RetAllBC(out string errMsg)
        {
            return dr.GetAllBC(out errMsg);
        }

        //保存新添加的班次信息
        public bool SaveBC(string BcMs, string ST, string ET, out string errMsg)
        {
            return dr.SaveBC(BcMs, ST, ET, out errMsg);
        }

        //编辑原来的班次信息
        public bool UpDateBC(string OBcMs, string BcMs, string ST, string ET, out string errMsg)
        {
            return dr.UpadteBC(OBcMs, BcMs, ST, ET, out errMsg);
        }

        //删除原来的班次信息
        public bool DeleteBC(int BcId, out string errMsg)
        {
            return dr.DeleteBC(BcId, out errMsg);
        }

        //返回所有职别（班组）信息
        public DataTable RetAllBZ(out string errMsg)
        {
            return dr.GetAllBZ(out errMsg);
        }

        //添加新的职别（班组）信息
        public bool SaveBZ(string BzId, string BzMs, out string errMsg)
        {
            return dr.InsertBZ(BzId, BzMs, out errMsg);
        }

        //编辑原有的职别（班组）信息
        public bool UpDateBZ(string OBzId, string BzId, string BzMs, out string errMsg)
        {
            return dr.UpdateBZ(OBzId, BzId, BzMs, out errMsg);
        }

        //删除原有的职别（班组）信息
        public bool DeleteBZ(string BzId, out string errMsg)
        {
            return dr.DeleteBZ(BzId, out errMsg);
        }

        //得到所有班次的信息列表
        public ArrayList BClist(out string errMsg)
        {
            return dr.BClist(out errMsg);
        }

        //得到所有职别（班组）的信息列表
        public ArrayList BZlist(out string errMsg)
        {
            return dr.BZlist(out errMsg);
        }

        //根据职别（班组）描述得到职别（班组）的编号
        public string BZIDbyBZMS(string bzms, out string errMsg)
        {
            return dr.BZIDbyBZMS(bzms, out errMsg);
        }

        //清空T_SYS_DUTY表
        public bool EmptyPB(out string errMsg)
        {
            return dr.EmptyPB(out errMsg);
        }

        //将排班信息添加到T_SYS_DUTY表中
        public bool InsertPB(string sqldb2, string sqlsql, out string errMsg)
        {
            return dr.InsertPB(sqldb2, sqlsql, out errMsg);
        }

        //根据每页显示多少条数据返回班次信息
        public DataTable GetBCmenu(int sCount, int eCount)
        {
            return dr.GetBCmenu(sCount, eCount);
        }

        //返回所有班次信息的条数
        public int GetBCCount()
        {
            return dr.GetBCCount();
        }

        //根据每页显示多少条数据返回职别（班组）信息
        public DataTable GetBZmenu(int sCount, int eCount)
        {
            return dr.GetBZmenu(sCount, eCount);
        }

        //返回所有职别（班组）信息的条数
        public int GetBZCount()
        {
            return dr.GetBZCount();
        }

        //上传最新XML
        public bool RetBoolUpFile(byte[] fileBytes, out string errMsg)
        {
            return dr.RetBoolUpFile(fileBytes, out errMsg);
        }

        public string GetDBtype()
        {
            return dr.GetDBtype();
        }

        #region 根据每页显示多少条数据，组织机构ID和组织机构数ID返回用户信息
        public DataTable GetUserMenu(string id, string treeID, int sCount, int eCount)
        {
            return dr.GetUserMenu(id, treeID, sCount, eCount);
        }
        #endregion

        #region 根据组织机构ID和组织机构数ID返回所有用户信息的条数
        public int GetUserCount(string id, string treeID)
        {
            return dr.GetUserCount(id, treeID);
        }
        #endregion

        /// <summary>
        /// 获取组织机构树节点
        /// </summary>
        /// <param name="usrID">用户ID</param>
        /// <param name="TreeName">组织机构树名称</param>
        /// <returns></returns>
        public DataSet GetTreeNodeID(string userID, string treeName)
        {
            return dr.GetTreeNodeID(userID, treeName);
        }

        //返回所有组织机构ID和中文描述
        public DataTable GetTreeMenu(out string errMsg)
        {
            return dr.GetTreeMenu(out errMsg);
        }

        #region 根据用户名判断是否存在该人员
        public bool JudgMember(string userId)
        {
            return dr.JudgMember(userId);
        }
        #endregion

         #region 添加人员
        public bool AddMember(string id, string name, string pwd, byte[] img, string orgID, string treeID)
        {
            return dr.AddMember(id, name, pwd, img, orgID, treeID);
        }
        #endregion
        #region 返回人员信息
        public IList<Hashtable> GetmemberInfo(string id, int i)
        {
            return pb.DataTableToList(dr.GetmemberInfo(id, i));
        }
        #endregion
        #region 编辑人员信息
        public bool EditMemberInfo(string userIDO, string userID, string userName, string pwd, byte[] img, string treeNodeId, string treeAllId)
        {
            return dr.EditMemberInfo(userIDO, userID, userName, pwd, img, treeNodeId, treeAllId);
        }
        #endregion
        #region 删除人员
        /// <summary>
        /// 删除人员信息
        /// </summary>
        /// <param name="id">人员编码</param>
        /// <returns></returns>
        public bool RemoveMember(string id)
        {
            return dr.RemoveMember(id);
        }
        #endregion

        #region 角色人员管理

        #region 根据每页显示多少条数据，角色ID返回用户信息
        public DataTable GetUserMenuByRole(string roleId, int sCount, int eCount)
        {
            return dr.GetUserMenuByRole(roleId, sCount, eCount);
        }
        #endregion

        #region 根据角色ID返回所有用户信息的条数
        public int GetUserCountByRole(string roleId)
        {
            return dr.GetUserCountByRole(roleId);
        }
        #endregion

        //添加新的角色信息
        public bool SaveRole(string rId, string rName, out string errMsg)
        {
            return dr.SaveRole(rId, rName, out errMsg);
        }

        //编辑原有的角色信息
        public bool UpDateRole(string OBzId, string BzId, string BzMs, out string errMsg)
        {
            return dr.UpDateRole(OBzId, BzId, BzMs, out errMsg);
        }

        //删除原有的角色信息
        public bool DeleteRole(string BzId, out string errMsg)
        {
            return dr.DeleteRole(BzId, out errMsg);
        }

        //返回所有角色信息
        public DataTable GetAllRole(int sCount, int eCount)
        {
            return dr.GetAllRole(sCount, eCount);
        }

        //返回所有角色信息的条数
        public int GetRoleCount()
        {
            return dr.GetRoleCount();
        }

        //上传最新XML
        public bool RetBoolUpFile(string xmlID, string xmlName, byte[] fileBytes, out string errMsg)
        {
            return dr.RetBoolUpFile(xmlID, xmlName, fileBytes, out errMsg);
        }
        #endregion
    }
}
