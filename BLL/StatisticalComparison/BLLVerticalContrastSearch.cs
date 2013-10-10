using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.StatisticalComparison;
using System.Data;
using Entity.Statistic;
using BLL.StatisticalComparison;

namespace BLL.StatisticalComparison
{
    public class BLLVerticalContrastSearch
    {
        string errMsg = string.Empty;
        DALVerticalContrastSearch dv = new DALVerticalContrastSearch();
        BLLEnergyLossIndicator be = new BLLEnergyLossIndicator();

        /// <summary>
        /// 获取机组的参数列表
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetParaName(string unitId, out string errMsg)
        {
            return dv.GetParaName(unitId, out errMsg);
        }
        /// <summary>
        ///根据unitId 从T_BASE_CALCPARA表（趋势分析表）中 获取不同(unitId和paraId)唯一的表名。数据存储在了不同的表中，耗差类型为0或1。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetTableName(string unitId, string paraId, out string errMsg)
        {
            return dv.GetTableName(unitId, paraId, out errMsg);
        }

        /// <summary>
        ///根据时间段取得 参数描述（指标名称），平均基准值。取得的paraId留着使用。注：paraId+_el_B为实际值，paraId+_B为耗差值
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetValueByPara(string tableName, string beginTime, string endTime, string unitId, string paraId, out string errMsg)
        {
            return dv.GetValueByPara(tableName, beginTime, endTime, unitId, paraId, out errMsg);
        }

        /// <summary>
        /// 得到绑定汽机或锅炉(TargetType:耗差类型:0可控耗差，1不可控耗差。ConsumeType:指标类型:0锅炉指标,1汽机指标）
        /// </summary>
        /// <returns></returns>
        public List<IndicatorInfo> GetInfo(string beginTime, string endTime, string unitId, string paraId, out string errMsg)
        {
            List<IndicatorInfo> list = new List<IndicatorInfo>();

            List<VerticalInfo> infos = new List<VerticalInfo>();

            IndicatorInfo sb = new IndicatorInfo();

            DataTable dt = new DataTable();
            DataTable dtTable = new DataTable();

            dtTable = GetTableName(unitId, paraId, out errMsg);
            string tableName =dtTable.Rows.Count==0 ? string.Empty : dtTable.Rows[0]["T_OUTTABLE"].ToString();
            string unit = dtTable.Rows.Count == 0 ? string.Empty : dtTable.Rows[0]["T_UNIT"].ToString();
            //得到实际值
            if (!string.IsNullOrEmpty(tableName))
            {
                dt = be.GetBase(tableName, beginTime, endTime, unitId, paraId, out errMsg);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        sb.Name = String.IsNullOrEmpty(dr["T_DESC"].ToString()) ? String.Empty : dr["T_DESC"].ToString();
                        sb.RealValue = String.IsNullOrEmpty(dr["counts"].ToString()) ? 0 : Convert.ToDouble(dr["counts"].ToString());
                        sb.Unit = unit;
                        list.Add(sb);
                    }
                }
            }

            return list;
        }

    }


    /// <summary>
    /// 第一次取得数据的类。
    /// </summary>
    public class VerticalInfo
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { set; get; }
        /// <summary>
        /// 机组Id
        /// </summary>
        public string UnitId { set; get; }
        /// <summary>
        /// 参数Id
        /// </summary>
        public string ParaId { set; get; }
        /// <summary>
        /// 参数单位
        /// </summary>
        public string Unit { set; get; }
        /// <summary>
        /// 指标类型（0，锅炉指标1，汽机指标）
        /// </summary>
        public int TargetType { set; get; }
        /// <summary>
        /// 耗差类型0，可控耗差1，不可控耗差
        /// </summary>
        public int ConsumeType { set; get; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Order { set; get; }
    }

}

