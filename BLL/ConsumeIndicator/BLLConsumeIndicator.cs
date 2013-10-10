using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.ConsumeIndicator;
using Entity.ConsumeIndicator;
using System.Data;
using SAC.Helper;

namespace BLL.ConsumeIndicator
{
    
  public class BLLConsumeIndicator
    {
        string errMsg = string.Empty;
        DALConsumeIndicator dc = new DALConsumeIndicator();

        /// <summary>
        /// 将数据对标信息从EXCEL中插入到数据库中。
        /// </summary>
        /// <param name="statisticList"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
      public bool InsertData(List<DataInfo> dataList, out string errMsg)
      {
          return dc.InsertData(dataList, out errMsg);
      }

      /// <summary>
        /// 获取数据对标信息
        /// </summary>
        /// <returns></returns>
      public List<DataInfo> GetInfo(string time, out string errMsg)
      {
          return dc.GetInfo(time,out errMsg);
      }
       /// <summary>
        /// 将各个类型机组能耗信息从EXCEL中插入到数据库中。
        /// </summary>
        /// <param name="statisticList"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
      public bool InsertChartDetailData(List<UnitConsumeInfo> dataList, out string errMsg)
      {
          return dc.InsertChartDetailData(dataList, out errMsg);
      }
       /// <summary>
        /// 根据时间获取各个类型机组能耗信息
        /// </summary>
        /// <returns></returns>
      public List<UnitConsumeInfo> GetUnitConsumeList(string time, out string errMsg)
      {
          List<UnitConsumeInfo> tmp = new List<UnitConsumeInfo>();
          tmp = dc.GetUnitConsumeList(time, out errMsg);
          if (tmp.Count > 0)
          {
              return tmp.Distinct(new EqualCompare<UnitConsumeInfo>((x, y) => (x != null && y != null) && (x.T_UNITCODE == y.T_UNITCODE))).ToList();

          }
          else
          {
              return tmp;
          }
      }
        /// <summary>
        /// 获取所有供电能耗月线信息。
        /// </summary>
        /// <returns></returns>
      public List<MonthConsumeInfo> GetMonthConsume(out string errMsg)
      {
          DataTable dts = dc.GetMonthConsume(out errMsg);
          List<MonthConsumeInfo> infoList = new List<MonthConsumeInfo>();
          //转化为list，筛选。
          if (dts.Rows.Count > 0)
          {
              for (int i = 0; i < dts.Rows.Count; i++)
              {
                  MonthConsumeInfo info = new MonthConsumeInfo();

                  info.year = String.IsNullOrEmpty(dts.Rows[i]["T_YEAR"].ToString()) ? 0 : Convert.ToInt32(dts.Rows[i]["T_YEAR"].ToString());
                  info.month = String.IsNullOrEmpty(dts.Rows[i]["T_MONTH"].ToString()) ? 0 : Convert.ToInt32(dts.Rows[i]["T_MONTH"].ToString());
                  info.values = String.IsNullOrEmpty(dts.Rows[i]["T_VALUE"].ToString()) ? 0 : Convert.ToDouble(dts.Rows[i]["T_VALUE"].ToString());
                 
                  infoList.Add(info);
              }
          }
          return infoList;
      }

        /// <summary>
        /// 更新指定时间供电能耗月线信息。
        /// </summary>
        /// <returns></returns>
      public bool UpdateMonthConsumeByTime(MonthConsumeInfo info, out string errMsg)
      {
          return dc.UpdateMonthConsumeByTime(info, out errMsg);
      }
        /// <summary>
        /// 获取指定时间供电能耗月线信息。
        /// </summary>
        /// <returns></returns>
      public DataTable GetMonthConsumeByTime(string year, string month, out string errMsg)
      {
          return dc.GetMonthConsumeByTime(year, month, out errMsg);
      }
    }
}
