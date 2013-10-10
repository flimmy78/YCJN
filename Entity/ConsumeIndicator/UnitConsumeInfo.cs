using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.ConsumeIndicator
{
     /// <summary>
    /// 各类型机组能耗类
    /// </summary>
    [Serializable]
    public class UnitConsumeInfo
    {
        public UnitConsumeInfo()
        { }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string T_DWNAME { get; set; }
        /// <summary>
        /// 电厂名称
        /// </summary>
        public string T_PLANTNAME { get; set; }
        /// <summary>
        /// 机组编号
        /// </summary>
        public string T_UNITCODE { get; set; }
        /// <summary>
        /// 机组类型
        /// </summary>
        public string T_TYPE { get; set; }
        /// <summary>
        /// 统计的时间
        /// </summary>
        public DateTime T_TIME { get; set; }
        /// <summary>
        /// 台数
        /// </summary>
        public string T_COUNT { get; set; }
        /// <summary>
        /// 利用小时
        /// </summary>
        public string T_USEHOUR { get; set; }
        /// <summary>
        /// 出力系数% 
        /// </summary>
        public string T_OF { get; set; }
        /// <summary>
        /// 热电比% 
        /// </summary>
        public string T_RDB { get; set; }
        /// <summary>
        /// 厂用电率（%）
        /// </summary>
        public string T_CYDL { get; set; }
        /// <summary>
        /// 供电煤耗 
        /// </summary>
        public string T_GDMH { get; set; }
        /// <summary>
        /// 对标煤耗 
        /// </summary>
        public string T_DBMH { get; set; }
        /// <summary>
        /// 供电量 
        /// </summary>
        public string T_GDL { get; set; }
        /// <summary>
        /// 与集团平均比
        /// </summary>
        public string T_JTPJB { get; set; }

    }
}