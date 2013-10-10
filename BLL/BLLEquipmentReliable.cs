using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using System.Data;
using SAC.Entity;

namespace BLL
{
    public class BLLEquipmentReliable
    {
        string errMsg = string.Empty;
        DALEquipmentReliable de = new DALEquipmentReliable();

        /// <summary>
        /// 根据条件获取数据。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetInitByCondition(string beginTime, string endTime, int sCount, int eCount,out int count, out string errMsg)
        {
            return de.GetInitByCondition(beginTime, endTime, sCount, eCount,out count, out errMsg);
        }

        /// <summary>
        /// 根据条件获取数据。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetInitByCondition(string companyId, string plantId, string unitId, string beginTime, string endTime, int sCount, int eCount, out int count, out string errMsg)
        {
            return de.GetInitByCondition(companyId, plantId, unitId, beginTime, endTime,sCount,eCount,out count, out errMsg);
        }

         /// <summary>
        /// 根据条件获取数据，可靠性分析。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetInitByCondition(string time, out string errMsg)
        {
            return de.GetInitByCondition(time, out errMsg);
        }

         /// <summary>
       /// 根据条件获取数据，可靠性分析半年。
       /// </summary>
       /// <param name="sTime">起始时间</param>
       /// <param name="eTime">结束时间</param>
       /// <param name="errMsg"></param>
       /// <returns></returns>
        public DataTable GetInitByCondition(string sTime, string eTime, out string errMsg)
        {
            return de.GetInitByCondition(sTime, eTime, out errMsg);
        }

        
        /// <summary>
        /// 根据条件获取数据，可靠性分析(强迫停运次数分析（按容量分类）)。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetInitByCapality(string time, out string errMsg)
        {
            return de.GetInitByCapality(time, out errMsg);
        }

         /// <summary>
        /// 根据条件获取数据，可靠性分析(强迫停运次数分析（按专业分类）)。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetInitByProfession(string time, out string errMsg)
        {
            return de.GetInitByProfession(time, out errMsg);
        }

         /// <summary>
        /// 根据条件获取数据，可靠性分析(强迫停运次数分析（按故障原因分类）)。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetInitByReason(string time, out string errMsg)
        {
            return de.GetInitByReason(time, out errMsg);
        }

        /// <summary>
        /// 获取故障类别信息。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetFaultCategory(out string errMsg)
        {
            return de.GetFaultCategory(out errMsg);
        }

        /// <summary>
        /// 获取故障性质信息。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetFaultProperty(out string errMsg)
        {
            return de.GetFaultProperty(out errMsg);
        }

        /// <summary>
        /// 获取故障专业分类信息。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetFaultProfessional(out string errMsg)
        {
            return de.GetFaultProfessional(out errMsg);
        }

        /// <summary>
        /// 获取故障故障原因分类信息。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetFaultReason(out string errMsg)
        {
            return de.GetFaultReason(out errMsg);
        }
        /// <summary>
        /// 根据唯一ID获取数据。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetUnitById(string Id, out string errMsg)
        {
            return de.GetUnitById(Id, out errMsg);
        }

         /// <summary>
        /// 根据机组Id更新机组部分信息。
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="professionalId"></param>
        /// <param name="reasonId"></param>
        /// <param name="eventDesc"></param>
        /// <param name="reasonAnalyse"></param>
        /// <param name="dealCondition"></param>
        /// <returns></returns>
        public bool UpdateUnit(string unitId, string professionalId, string reasonId, string eventDesc, string reasonAnalyse, string dealCondition, out string errMsg)
        {
            return de.UpdateUnit(unitId, professionalId, reasonId, eventDesc, reasonAnalyse, dealCondition, out errMsg);
        }

        /// <summary>
        /// 将机组信息从EXCEL中插入到数据库中。
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="professionalId"></param>
        /// <param name="reasonId"></param>
        /// <param name="eventDesc"></param>
        /// <param name="reasonAnalyse"></param>
        /// <param name="dealCondition"></param>
        /// <returns></returns>
        public bool InsertUnit(List<UnitInfo> unitList, out string errMsg)
        {
            return de.InsertUnit(unitList, out errMsg);
        }
    }
}