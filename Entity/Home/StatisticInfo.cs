using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.Home
{
    /// <summary>
    /// 首页信息类
    /// </summary>
    [Serializable]
    public class StatisticInfo
    {
        public StatisticInfo()
        { 
        }
        /// <summary>
        /// 
        /// </summary>
        public int ID_KEY { get; set; }
        /// <summary>
        /// 指标名称
        /// </summary>
        public string T_INDICATORNAME { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string T_UNITNAME { get; set; }
        /// <summary>
        /// 统计时间
        /// </summary>
        public string T_TIME{get;set;}
        /// <summary>
        /// 华能累计
        /// </summary>
        public double D_HNALL{get;set;}
        /// <summary>
        /// 华能增长
        /// </summary>
        public  double D_HNADD{get;set;}
        /// <summary>
        /// 大唐累计
        /// </summary>
        public  double D_DTALL{get;set;}
        /// <summary>
        /// 大唐增长
        /// </summary>
        public double D_DTADD{get;set;}
        /// <summary>
        /// 华电累计
        /// </summary>
        public double D_HDALL{get;set;}
        /// <summary>
        /// 华电增长
        /// </summary>
        public double D_HDADD{get;set;}
        /// <summary>
        /// 国电累计
        /// </summary>
        public double D_GDALL{get;set;}
        /// <summary>
        /// 国电增长
        /// </summary>
        public double D_GDADD{get;set;}
        /// <summary>
        /// 中电投累计
        /// </summary>
        public double D_ZDTALL{get;set;}
        /// <summary>
        /// 中电投增长
        /// </summary>
        public double D_ZDTADD { get; set; }

    }
}