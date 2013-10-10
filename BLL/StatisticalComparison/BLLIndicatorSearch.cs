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
    public class BLLIndicatorSearch
    {
        string errMsg = string.Empty;
        DALIndicatorSearch di = new DALIndicatorSearch();
        BLLEnergyLossIndicator be = new BLLEnergyLossIndicator();
        /// <summary>
        /// 获取第一次全取出来的数据。再过滤。 在GetBase函数中 循环。。。 paraId+_el_B再循环得到实际值.....
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<FirstInfo> GetFirstInfo(string companyId, string plantId, string unitId, int TargetType, int ConsumeType, out string errMsg)
        {
            DataTable dt = di.GetTableName(companyId,plantId,unitId,TargetType,ConsumeType, out errMsg);
            List<FirstInfo> infoList = new List<FirstInfo>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["T_OUTTABLE"].ToString()))
                    {
                        FirstInfo tmp = new FirstInfo();
                        tmp.TableName = dr["T_OUTTABLE"].ToString();
                        tmp.UnitId = dr["T_UNITID"].ToString();
                        tmp.ParaId = dr["T_PARAID"].ToString();
                        tmp.Unit = dr["T_UNIT"].ToString();
                        tmp.TargetType = dr["I_TARGETTYPE"] == DBNull.Value ? -1 : Convert.ToInt32(dr["I_TARGETTYPE"].ToString());
                        tmp.ConsumeType = dr["I_CONSUMETYPE"] == DBNull.Value ? -1 : Convert.ToInt32(dr["I_CONSUMETYPE"].ToString());
                        tmp.Order = dr["I_ORDER"] == DBNull.Value ? -1 : Convert.ToInt32(dr["I_ORDER"].ToString());
                        infoList.Add(tmp);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return infoList;
        }

        /// <summary>
        /// 得到绑定汽机或锅炉(TargetType:指标类型:0锅炉指标,1汽机指标。ConsumeType:耗差类型:0可控耗差，1不可控耗差）
        /// </summary>
        /// <returns></returns>
        public List<IndicatorInfo> GetInfo(string beginTime, string endTime, string companyId, string plantId, string unitId, int TargetType, int ConsumeType, out string errMsg)
        {
            List<IndicatorInfo> list = new List<IndicatorInfo>();

            List<FirstInfo> infos = new List<FirstInfo>();

            infos = GetFirstInfo(companyId,plantId, unitId,TargetType,ConsumeType, out errMsg);

            foreach (var i in infos)
            {
                IndicatorInfo sb = new IndicatorInfo();

                DataTable dt = new DataTable();
                //dt = be.GetBase(i.TableName, beginTime, endTime, unitId, i.ParaId + "_el_B", TargetType, ConsumeType, out errMsg);

                ////得到基准值和指标类型和耗差类型
                //if (dt != null)
                //{
                //    foreach (DataRow dr in dt.Rows)
                //    {
                //        sb.Name = String.IsNullOrEmpty(dr["T_DESC"].ToString()) ? String.Empty : dr["T_DESC"].ToString();
                //        sb.TargetType = String.IsNullOrEmpty(dr["I_TARGETTYPE"].ToString()) ? String.Empty : dr["I_TARGETTYPE"].ToString();
                //        sb.ConsumeType = String.IsNullOrEmpty(dr["I_CONSUMETYPE"].ToString()) ? String.Empty : dr["I_CONSUMETYPE"].ToString();
                //        sb.StandardValue = String.IsNullOrEmpty(dr["counts"].ToString()) ? 0 : Convert.ToDouble(dr["counts"].ToString());
                //    }
                //}

                //得到实际值
                dt = be.GetBase(i.TableName, beginTime, endTime, unitId, i.ParaId, out errMsg);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        sb.Name = String.IsNullOrEmpty(dr["T_DESC"].ToString()) ? String.Empty : dr["T_DESC"].ToString();
                        sb.TargetType = String.IsNullOrEmpty(dr["I_TARGETTYPE"].ToString()) ? String.Empty : dr["I_TARGETTYPE"].ToString();
                        sb.ConsumeType = String.IsNullOrEmpty(dr["I_CONSUMETYPE"].ToString()) ? String.Empty : dr["I_CONSUMETYPE"].ToString();
                        sb.RealValue = String.IsNullOrEmpty(dr["counts"].ToString()) ? 0 : Convert.ToDouble(dr["counts"].ToString());
                    }
                }

                ////得到耗差值
                //dt =be.GetBase(i.TableName, beginTime, endTime, unitId, i.ParaId + "_el", TargetType, ConsumeType, out errMsg);
                //if (dt != null)
                //{
                //    foreach (DataRow dr in dt.Rows)
                //    {
                //        sb.ConsumeValue = String.IsNullOrEmpty(dr["counts"].ToString()) ? 0 : Convert.ToDouble(dr["counts"].ToString());
                //    }
                //}


                //参数单位
                sb.Unit = i.Unit;
                list.Add(sb);
            }

            return list;
        }

    }


    /// <summary>
    /// 第一次取得数据的类。
    /// </summary>
    public class FirstInfo
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
