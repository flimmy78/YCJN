using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Task;
using Entity.Home;
using System.Data;
using Entity.ConsumeIndicator;

namespace BLL.Task
{
    /// <summary>
    /// 首页数据导入业务逻辑
    /// </summary>
    public class BLLTask
    {
        string errMsg = string.Empty;
        DALTask dt = new DALTask();

         /// <summary>
        /// 将生产统计信息从EXCEL中插入到数据库中。
        /// </summary>
        /// <param name="statisticList"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool InsertHomeData(List<StatisticInfo> statisticList, out string errMsg)
        {
            return dt.InsertHomeData(statisticList, out errMsg);
        }

         /// <summary>
        /// 根据时间获取五大集团最优机组供电煤耗信息。
        /// </summary>
        /// <param name="times"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<BestUnitConsumeInfo> GetInfoByTime(string times, out string errMsg)
        {
            DataTable dts=dt.GetInfoByTime(times, out errMsg);
            List<BestUnitConsumeInfo> infoList = new List<BestUnitConsumeInfo>();
            //转化为list，筛选。
            if (dts.Rows.Count > 0)
            {
                for (int i = 0; i < dts.Rows.Count; i++)
                {
                    BestUnitConsumeInfo info = new BestUnitConsumeInfo();
                  
                    info.T_COMPANY = String.IsNullOrEmpty(dts.Rows[i]["T_COMPANY"].ToString()) ? String.Empty : dts.Rows[i]["T_COMPANY"].ToString();
                    info.T_900_SL = String.IsNullOrEmpty(dts.Rows[i]["T_900_SL"].ToString()) ? 0 : Convert.ToDouble(dts.Rows[i]["T_900_SL"].ToString());
                    info.T_600_HJ = String.IsNullOrEmpty(dts.Rows[i]["T_600_HJ"].ToString()) ? 0 : Convert.ToDouble(dts.Rows[i]["T_600_HJ"].ToString());
                    info.T_300_HJ = String.IsNullOrEmpty(dts.Rows[i]["T_300_HJ"].ToString()) ? 0 : Convert.ToDouble(dts.Rows[i]["T_300_HJ"].ToString());
                    info.T_200_HJ = String.IsNullOrEmpty(dts.Rows[i]["T_200_HJ"].ToString()) ? 0 : Convert.ToDouble(dts.Rows[i]["T_200_HJ"].ToString());
                    info.T_120_HJ = String.IsNullOrEmpty(dts.Rows[i]["T_120_HJ"].ToString()) ? 0 : Convert.ToDouble(dts.Rows[i]["T_120_HJ"].ToString());

                    infoList.Add(info);
                }
            }
            return infoList;
        }

         /// <summary>
        /// 根据时间获取四类信息。
        /// </summary>
        /// <param name="times"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<StatisticInfo> GetHomeByTime(string times, out string errMsg)
        {
            DataTable dts = dt.GetHomeByTime(times, out errMsg);
            List<StatisticInfo> infoList = new List<StatisticInfo>();
            //转化为list，筛选。
            if (dts.Rows.Count > 0)
            {
                for (int i = 0; i < dts.Rows.Count; i++)
                {
                    StatisticInfo info = new StatisticInfo();
                    info.T_INDICATORNAME = String.IsNullOrEmpty(dts.Rows[i]["T_INDICATORNAME"].ToString()) ? String.Empty : dts.Rows[i]["T_INDICATORNAME"].ToString();
                    info.T_UNITNAME = String.IsNullOrEmpty(dts.Rows[i]["T_UNITNAME"].ToString()) ? String.Empty : dts.Rows[i]["T_UNITNAME"].ToString();
                    info.T_TIME = String.IsNullOrEmpty(dts.Rows[i]["T_TIME"].ToString()) ? String.Empty : dts.Rows[i]["T_TIME"].ToString();
                    info.D_HNALL = String.IsNullOrEmpty(dts.Rows[i]["D_HNALL"].ToString()) ? 0 : Convert.ToDouble(dts.Rows[i]["D_HNALL"].ToString());
                    info.D_HNADD = String.IsNullOrEmpty(dts.Rows[i]["D_HNADD"].ToString()) ? 0 : Convert.ToDouble(dts.Rows[i]["D_HNADD"].ToString());
                    info.D_DTALL = String.IsNullOrEmpty(dts.Rows[i]["D_DTALL"].ToString()) ? 0 : Convert.ToDouble(dts.Rows[i]["D_DTALL"].ToString());
                    info.D_DTADD = String.IsNullOrEmpty(dts.Rows[i]["D_DTADD"].ToString()) ? 0 : Convert.ToDouble(dts.Rows[i]["D_DTADD"].ToString());
                    info.D_HDALL = String.IsNullOrEmpty(dts.Rows[i]["D_HDALL"].ToString()) ? 0 : Convert.ToDouble(dts.Rows[i]["D_HDALL"].ToString());
                    info.D_HDADD = String.IsNullOrEmpty(dts.Rows[i]["D_HDADD"].ToString()) ? 0 : Convert.ToDouble(dts.Rows[i]["D_HDADD"].ToString());
                    info.D_GDALL = String.IsNullOrEmpty(dts.Rows[i]["D_GDALL"].ToString()) ? 0 : Convert.ToDouble(dts.Rows[i]["D_GDALL"].ToString());
                    info.D_GDADD = String.IsNullOrEmpty(dts.Rows[i]["D_GDADD"].ToString()) ? 0 : Convert.ToDouble(dts.Rows[i]["D_GDADD"].ToString());
                    info.D_ZDTALL = String.IsNullOrEmpty(dts.Rows[i]["D_ZDTALL"].ToString()) ? 0 : Convert.ToDouble(dts.Rows[i]["D_ZDTALL"].ToString());
                    info.D_ZDTADD = String.IsNullOrEmpty(dts.Rows[i]["D_ZDTADD"].ToString()) ? 0 : Convert.ToDouble(dts.Rows[i]["D_ZDTADD"].ToString());

                    
                    infoList.Add(info);
                }
            }
            return infoList;
        }
        
    }
}
