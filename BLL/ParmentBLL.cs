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
    public class ParmentBLL
    {
        #region 胡进财
        ParmentDAL dal = new ParmentDAL();
        DateHelper pd = new DateHelper();
        int count = 0;
        DataTable dt = new DataTable();
        StringBuilder sbl = new StringBuilder();
        /// <summary>
        /// 获取职位关系
        /// </summary>
        /// <returns></returns>
        public DataTable GetMenu()
        {
            dt = dal.GetMenu();
            return dt;
        }

        /// <summary>
        /// 获取组织岗位关系
        /// </summary>
        /// <param name="id">父类编码</param>
        /// <returns></returns>
        public DataTable Getmenu(string id, int sCount, int eCount)
        {
            return dal.Getmenu(id, sCount, eCount);
        }

        /// <summary>
        /// 获取组织岗位关系
        /// </summary>
        /// <param name="id">父类编码</param>
        /// <returns></returns>
        public int GetmenuCount(string id)
        {
            return dal.GetmenuCount(id);
        }

        /// <summary>
        /// 判断某个职位是否有子节点
        /// </summary>
        /// <param name="id">职位编号</param>
        /// <returns></returns>
        public int getSun(string id)
        {
            count = dal.getSun(id);
            return count;
        }

        /// <summary>
        /// 添加组织关系
        /// </summary>
        /// <param name="parentId">父类ID</param>
        /// <param name="id">组织编码</param>
        /// <param name="name">组织名称</param>
        /// <returns></returns>
        public bool AddOrganize(string parentId, string id, string name)
        {
            return dal.AddOrganize(parentId, id, name);
        }


        /// <summary>
        /// 编辑组织关系
        /// </summary>
        /// <param name="oid">组织原编码</param>
        /// <param name="id">组织新编码</param>
        /// <param name="name">组织名称</param>
        /// <returns></returns>
        public bool EditOrganize(string oid, string id, string name)
        {
            return dal.EditOrganize(oid, id, name);
        }

        /// <summary>
        /// 编辑组织关系
        /// </summary>
        /// <param name="id">组织编码</param>
        /// <returns></returns>
        public bool RemoveOrganize(string id)
        {
            return dal.RemoveOrganize(id);
        }


        /// <summary>
        /// 获取值别信息
        /// </summary>
        /// <returns></returns>
        public IList<Hashtable> GetClass()
        {
            return pd.DataTableToList(dal.GetClass());
        }

        /// <summary>
        /// 判断是否存在该部门
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public bool JudgeExitParent(string id)
        {
            return dal.JudgeExitParent(id);
        }

        /// <summary>
        /// 查询点检漏检率
        /// </summary>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns></returns>
        public string GetParmentCheck(DateTime sTime, DateTime eTime)
        {
            return dal.GetParmentCheck(sTime, eTime);
        }

        public IList<Hashtable> GetParmentCheckGrid(DateTime sTime, DateTime eTime, int sCount, int eCount)
        {
            return dal.GetParmentCheckGrid(sTime, eTime, sCount, eCount);
        }

        /// <summary>
        /// 查询巡检漏检率
        /// </summary>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns></returns>
        public string GetCheck(DateTime sTime, DateTime eTime)
        {
            return dal.GetCheck(sTime, eTime);
        }

        public IList<Hashtable> GetCheckGrid(DateTime sTime, DateTime eTime)
        {
            return dal.GetCheckGrid(sTime, eTime);
        }

        public DataTable GetOrganizeExistPerson(string id)
        {
            return dal.GetOrganizeExistPerson(id);
        }

        #endregion
    }
}
