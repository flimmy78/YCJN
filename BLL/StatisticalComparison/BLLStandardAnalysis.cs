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
    public class BLLStandardAnalysis
    {
        string errMsg = string.Empty;
        DateHelper dh = new SAC.Helper.DateHelper();
        DALStandardAnalysis ds = new DALStandardAnalysis();


        public List<ConsumeInfo> Get(string capacityLevel, string unitType, string BoilerId, string SteamId, string beginTime, string endTime, out string errMsg)
        {
            errMsg = string.Empty;
           
            List<ConsumeInfo> infoList = new List<ConsumeInfo>();
            infoList = ds.GetInfos(capacityLevel, unitType, BoilerId, SteamId, beginTime, endTime, out errMsg);
            return infoList;
        }
    }
}
