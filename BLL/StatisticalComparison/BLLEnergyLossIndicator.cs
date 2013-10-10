using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.StatisticalComparison;
using System.Data;
using Entity.Statistic;

namespace BLL.StatisticalComparison
{
    public class BLLEnergyLossIndicator
    {
        string errMsg = string.Empty;
        DALEnergyLossIndicator el = new DALEnergyLossIndicator();

        /// <summary>
        ///根据unitId 从T_BASE_CALCPARA表（趋势分析表）中 获取不同(unitId和paraId)唯一的表名。数据存储在了不同的表中，耗差类型为0或1。
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetTableName(string unitId, out string errMsg)
        {
            return el.GetTableName(unitId, out errMsg);
        }

        /// <summary>
        ///根据时间段取得 参数描述（指标名称），平均基准值。取得的paraId留着使用。注：paraId+_el_B为实际值，paraId+_B为耗差值
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetBase(string tableName, string beginTime, string endTime, string unitId, string paraId , out string errMsg)
        {
            return el.GetBase(tableName, beginTime, endTime, unitId, paraId, out errMsg);
        }

        /// <summary>
        /// 得到绑定汽机或锅炉(TargetType:耗差类型:0可控耗差，1不可控耗差。ConsumeType:指标类型:0锅炉指标,1汽机指标）
        /// </summary>
        /// <returns></returns>
        public List<IndicatorInfo> GetInfo(string beginTime, string endTime, string unitId, int TargetType, int ConsumeType, out string errMsg)
        {
            List<IndicatorInfo> list = new List<IndicatorInfo>();

            List<TmpInfoE> infos = new List<TmpInfoE>();

            infos = GetFirstInfo(unitId, out errMsg);
            if (TargetType == 0 || TargetType == 1)
            {
                infos = infos.Where(info => info.TargetType == TargetType).ToList();
            }
            if (ConsumeType == 0 || ConsumeType == 1)
            {
                infos = infos.Where(info => info.ConsumeType == ConsumeType).ToList();
            }

            foreach (var i in infos)
            {
                IndicatorInfo sb = new IndicatorInfo();
                sb.Name = i.Desc;
                sb.TargetType =  i.TargetType.ToString();
                sb.ConsumeType = i.ConsumeType.ToString();
                      
                DataTable dt = new DataTable();
                dt = GetBase(i.TableName, beginTime, endTime, unitId, i.ParaId + "_B",  out errMsg);
             
                //得到基准值和指标类型和耗差类型
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                       sb.StandardValue = String.IsNullOrEmpty(dr["counts"].ToString()) ? 0 : Convert.ToDouble(dr["counts"].ToString());
                    }
                }

                //得到实际值
                dt = GetBase(i.TableName, beginTime, endTime, unitId, i.ParaId.Remove(i.ParaId.Length-3,3),   out errMsg);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        sb.RealValue = String.IsNullOrEmpty(dr["counts"].ToString()) ? 0 : Convert.ToDouble(dr["counts"].ToString());
                    }
                }

                //得到耗差值
                dt = GetBase(i.TableName, beginTime, endTime, unitId, i.ParaId,  out errMsg);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        sb.ConsumeValue = String.IsNullOrEmpty(dr["counts"].ToString()) ? 0 : Convert.ToDouble(dr["counts"].ToString());
                    }
                }




                list.Add(sb);
            }

            return list;
        }

        /// <summary>
        /// 得到绑定汽机和锅炉总的耗差值(TargetType:耗差类型:0可控耗差，1不可控耗差。ConsumeType:指标类型:0锅炉指标,1汽机指标）
        /// </summary>
        /// <returns></returns>
        public List<ConsumeInfo> GetAllInfo(string beginTime, string endTime, string unitId, out string errMsg)
        {
            List<ConsumeInfo> infoList = new List<ConsumeInfo>();
            List<TmpInfoE> infos = new List<TmpInfoE>();

            ConsumeInfo a = new ConsumeInfo();
            a.Count = 0;
            ConsumeInfo b = new ConsumeInfo();
            b.Count = 0;
            ConsumeInfo c = new ConsumeInfo();
            c.Count = 0;
            ConsumeInfo d = new ConsumeInfo();
            d.Count = 0;
            ConsumeInfo e = new ConsumeInfo();
            e.Count = 0;

            ////总的耗差值。
            infos = GetFirstInfo(unitId, out errMsg);

            foreach (var i in infos)
            {
                DataTable dt = new DataTable();

                ////分六类
                //总耗差。
                dt = GetBaseAll(i.TableName, beginTime, endTime, unitId, i.ParaId + "_B", -1, -1, out errMsg);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        a.Count += String.IsNullOrEmpty(dr["counts"].ToString()) ? 0 : Convert.ToDouble(dr["counts"].ToString());
                    }
                }
                //汽机可控
                dt = GetBaseAll(i.TableName, beginTime, endTime, unitId, i.ParaId + "_B", 0, 1, out errMsg);
                if (dt != null)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        b.Count += String.IsNullOrEmpty(dr["counts"].ToString()) ? 0 : Convert.ToDouble(dr["counts"].ToString());
                    }
                }
                //锅炉可控
                dt = GetBaseAll(i.TableName, beginTime, endTime, unitId, i.ParaId + "_B", 0, 0, out errMsg);

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        c.Count += String.IsNullOrEmpty(dr["counts"].ToString()) ? 0 : Convert.ToDouble(dr["counts"].ToString());
                    }
                }
                //汽机不可控
                dt = GetBaseAll(i.TableName, beginTime, endTime, unitId, i.ParaId + "_B", 1, 1, out errMsg);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        d.Count += String.IsNullOrEmpty(dr["counts"].ToString()) ? 0 : Convert.ToDouble(dr["counts"].ToString());
                    }
                }
                //锅炉不可控

                dt = GetBaseAll(i.TableName, beginTime, endTime, unitId, i.ParaId + "_B", 1, 0, out errMsg);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        e.Count += String.IsNullOrEmpty(dr["counts"].ToString()) ? 0 : Convert.ToDouble(dr["counts"].ToString());
                    }
                }
            }

            infoList.Add(a);
            infoList.Add(b);
            infoList.Add(c);
            infoList.Add(d);
            infoList.Add(e);

            return infoList;
        }
        /// <summary>
        ///根据时间段取得 参数描述（指标名称），总基准值。取得的paraId留着使用。注：paraId+_el_B为实际值，paraId+_B为耗差值
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetBaseAll(string tableName, string beginTime, string endTime, string unitId, string paraId, int TargetType, int ConsumeType, out string errMsg)
        {
            return el.GetBaseAll(tableName, beginTime, endTime, unitId, paraId, TargetType, ConsumeType, out errMsg);
        }

        /// <summary>
        /// 获取第一次全取出来的数据。再过滤。 在GetBase函数中 循环。。。 paraId+_el_B再循环得到实际值.....
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public List<TmpInfoE> GetFirstInfo(string unitId, out string errMsg)
        {
            DataTable dt = el.GetTableName(unitId, out errMsg);
            List<TmpInfoE> infoList = new List<TmpInfoE>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["T_OUTTABLE"].ToString()))
                    {
                        TmpInfoE tmp = new TmpInfoE();
                        tmp.TableName = dr["T_OUTTABLE"].ToString();
                        tmp.UnitId = dr["T_UNITID"].ToString();
                        tmp.ParaId = dr["T_PARAID"].ToString();
                        tmp.Desc = dr["T_DESC"].ToString();
                        tmp.Unit = dr["T_UNIT"].ToString();
                        tmp.TargetType = dr["I_TARGETTYPE"]==DBNull.Value? -1 : Convert.ToInt32(dr["I_TARGETTYPE"].ToString());
                        tmp.ConsumeType = dr["I_CONSUMETYPE"] == DBNull.Value ? -1 : Int32.Parse(dr["I_CONSUMETYPE"].ToString());
                        
                        tmp.Order = Convert.ToInt32(dr["I_ORDER"].ToString());
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
    }


    /// <summary>
    /// 第一次取得数据的类。
    /// </summary>
    public class TmpInfoE
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
        /// 参数描述
        /// </summary>
        public string Desc { set; get; }
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
