using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.Statistic
{
    /// <summary>
    /// 指标信息。
    /// </summary>
    public class IndicatorInfo
    {
        //指标名称
        public string Name { set; get; }
        //基准值
        public double StandardValue { set; get; }
        //实际值
        public double RealValue { set; get; }
        //耗差值
        public double ConsumeValue { set; get; }
        /// <summary>
        /// 指标类型(0锅炉指标,1汽机指标)
        /// </summary>
        public string TargetType { set; get; }
        /// <summary>
        /// 耗差类型(0可控耗差，1不可控耗差)
        /// </summary>
        public string ConsumeType { set; get; }
        /// <summary>
        /// 指标单位
        /// </summary>
        public string Unit { set; get; }
    }
}