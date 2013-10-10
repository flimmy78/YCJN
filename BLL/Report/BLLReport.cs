using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.Helper;
using System.Data;
using SAC.DB2;
using Entity.Statistic;
using SAC.Helper;
using DAL.StatisticalComparison;
using DAL.Report;
using Entity.Report;

namespace BLL.Report
{
    /// <summary>
    /// 报表管理。
    /// </summary>
    public class BLLReport
    {
        string errMsg = string.Empty;
        DALCompanyConsume dc = new DALCompanyConsume();
        DALReport dr = new DALReport();
        DateHelper dh = new SAC.Helper.DateHelper();
        /// <summary>
        /// 获取ParaId及对应的数据表名。（已过滤）
        /// </summary>
        /// <returns></returns>
        public List<ParaTableInfo> GetInfo(out string errMsg)
        {
            return dc.GetInfo(out errMsg);
        }

        /// <summary>
        /// 循环遍历ParaId,获取该ParaId的平均值（得到所有ParaId的值）
        /// </summary>
        /// <param name="AllParaId">所有ParaId</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<ConsumeInfo> GetInfos(string beginTime, string endTime, out string errMsg)
        {
            return dc.GetInfos(beginTime, endTime, out errMsg);
        }

        /// <summary>
        /// 返回的所有信息 (只处理时间)
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<ConsumeInfo> get(string beginTime, string endTime, out string errMsg)
        {
            errMsg = string.Empty;
            List<ConsumeInfo> infoList = new List<ConsumeInfo>();
            infoList = GetInfos(beginTime, endTime, out  errMsg);
            return infoList;
        }

        /// <summary>
        /// 循环遍历ParaId,获取该机组的ParaId的平均值（得到所有ParaId的值）
        /// </summary>
        /// <param name="AllParaId">所有ParaId</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<ReportInfo> GetByTime(string beginTime, string endTime, out string errMsg)
        {
            return dr.GetInfos(beginTime, endTime, out errMsg);
        }

        /// <summary>
        /// 循环遍历ParaId,获取集团的ParaId的平均值（得到集团的ParaId的平均值）
        /// </summary>
        /// <param name="AllParaId">所有ParaId</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<ReportInfo> GetCompanyInfo(string beginTime, string endTime, out string errMsg)
        {
            return dr.GetCompanyInfos(beginTime, endTime, out errMsg);
        }

        /// <summary>
        /// 循环遍历ParaId,获取机组的各个ParaId的平均值（得到集团的ParaId的平均值）
        /// </summary>
        /// <param name="AllParaId">所有ParaId</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<ReportInfo> GetUnitParaInfo(string beginTime, string endTime, out string errMsg)
        {
            return dr.GetUnitParaInfo(beginTime, endTime, out errMsg);
        }
    }
}
