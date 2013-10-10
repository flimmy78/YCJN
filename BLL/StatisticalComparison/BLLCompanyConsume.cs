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

namespace BLL.StatisticalComparison
{
    /// <summary>
    ///  集团耗差指标分析页面。
    /// </summary>
    public class BLLCompanyConsume
    {
        string errMsg = string.Empty;
        DALCompanyConsume dc = new DALCompanyConsume();
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
    }
}
