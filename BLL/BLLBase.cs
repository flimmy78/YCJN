using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using System.Data;

namespace BLL
{
    public class BLLBase
    {
        string errMsg = string.Empty;
        DALBase db = new DALBase();

          /// <summary>
        /// 获取公司信息。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetCompany(out string errMsg)
        {
            return db.GetCompany(out errMsg);
        }

         /// <summary>
        /// 根据公司Id获取电厂信息。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetPlant(string companyId, out string errMsg)
        {
            return db.GetPlant(companyId, out errMsg);
        }

          /// <summary>
        /// 获取电厂Id获取机组信息。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetUnit(string plantId, out string errMsg)
        {
            return db.GetUnit(plantId, out errMsg);
        }

         /// <summary>
        /// 获取锅炉信息。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetBoiler(out string errMsg)
        {
            return db.GetBoiler(out errMsg);
        }

         /// <summary>
        /// 获取汽机信息。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetSteam(out string errMsg)
        {
            return db.GetSteam(out errMsg);
        }
    }
}
