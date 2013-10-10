/*
 *开发人员：胡进财
 *开发时间：2012-02027
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Collections;
using SAC.Helper;

namespace BLL
{
    public class MemberBLL
    {
        #region 胡进财
        MemberDAL dal = new MemberDAL();
        DateHelper pb = new DateHelper();
        DataTable dt = new DataTable();

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
            return dal.AddMember(id, name, pwd, classId, img);
        }

        /// <summary>
        /// 人员数据集
        /// </summary>
        /// <returns></returns>
        public IList<Hashtable> GetMembers()
        {
            return pb.DataTableToList(dal.GetMembers());
        }

        /// <summary>
        /// 分配职位
        /// </summary>
        /// <param name="parentId">职位ID</param>
        /// <param name="memberId">人员ID集合</param>
        /// <returns></returns>
        public bool AddMember(string parentId, string[] membersId)
        {
            return dal.AddMember(parentId, membersId);
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
            return dal.AddMember(parentId, membersId, state);
        }

        /// <summary>
        /// 获取人员岗位关系
        /// </summary>
        /// <returns></returns>
        public DataTable GetMembersAndParent()
        {
            return dal.GetMembersAndParent();
        }

        /// <summary>
        /// 获取人员岗位关系
        /// </summary>
        /// <returns></returns>
        public IList<Hashtable> GetParentAndMembers(string id)
        {
            return pb.DataTableToList(dal.GetParentAndMembers(id));
        }

        /// <summary>
        /// 判断是否存在该人员
        /// </summary>
        /// <param name="id">人员ID</param>
        /// <returns></returns>
        public bool JudgMember(string id)
        {
            return dal.JudgMember(id);
        }
        /// <summary>
        /// 判断是否存在该人员
        /// </summary>
        /// <param name="name">人员姓名</param>
        /// <returns></returns>
        public bool JudgMemberByName(string name)
        {
            return dal.JudgMemberByName(name);
        }
        /// <summary>
        /// 判断某个岗位下面是否存在人员
        /// </summary>
        /// <param name="name">人员ID</param>
        /// <returns></returns>
        public bool JudgMemberByClassId(string id)
        {
            return dal.JudgMemberByClassId(id);
        }

        /// <summary>
        /// 查询人员信息
        /// </summary>
        /// <param name="id">人员编号</param>
        /// <returns></returns>
        public IList<Hashtable> GetmemberInfo(string id, int i)
        {
            return pb.DataTableToList(dal.GetmemberInfo(id, i));
        }

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
            return dal.EditMemberInfo(oldId, trueName, id, pwd, classID, img);
        }

        /// <summary>
        /// 删除人员信息
        /// </summary>
        /// <param name="id">人员编码</param>
        /// <returns></returns>
        public bool RemoveMember(string id)
        {
            return dal.RemoveMember(id);
        }

        #region 岗位下人员分页
        public DataTable GetMember(string id, int sCount, int eCount)
        {
            return dal.GetMember(id, sCount, eCount);
        }
        public int GetMemberCount(string id)
        {
            return dal.GetMemberCount(id);
        }
        #endregion
        #endregion
    }
}
