using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.ConsumeIndicator
{
    /// <summary>
    /// 数据对标类
    /// </summary>
    [Serializable]
    public class DataInfo
    {
        public DataInfo()
        { }
        public string T_COMPANY { get; set; }
        public string T_DATATYPE { get; set; }
        public string T_TIME { get; set; }
        public string T_900_SL { get; set; }
        public string T_900_KL { get; set; }
        public string T_600_HJ { get; set; }
        public string T_600_CCL { get; set; }
        public string T_600_CLS { get; set; }
        public string T_600_CLK { get; set; }
        public string T_600_YLS { get; set; }
        public string T_600_YLK { get; set; }
        public string T_600_J { get; set; }
        public string T_350_HJ { get; set; }
        public string T_350_CNHJ { get; set; }
        public string T_350_CLJ { get; set; }
        public string T_350_YLJ { get; set; }
        public string T_350_GRHJ { get; set; }
        public string T_350_RCLJ { get; set; }
        public string T_350_RYLJ { get; set; }
        public string T_300_HJ { get; set; }
        public string T_300_CGSL { get; set; }
        public string T_300_CGKL { get; set; }
        public string T_300_LHC { get; set; }
        public string T_300_SL { get; set; }
        public string T_300_KL { get; set; }
        public string T_200_HJ { get; set; }
        public string T_200_SL { get; set; }
        public string T_200_KL { get; set; }
        public string T_200_LHC { get; set; }
        public string T_200_J { get; set; }
        public string T_120_HJ { get; set; }
        public string T_120_J { get; set; }
        public string T_120_LHC { get; set; }
        public string T_120_KL { get; set; }
        public string T_120_JC { get; set; }
        public string T_135_LHC { get; set; }
        public string T_100_HJ { get; set; }
        public string T_100_CG { get; set; }
        public string T_100_LHC { get; set; }
        public string T_100_J { get; set; }
        public string T_90_HJ { get; set; }
        public string T_90_NJ { get; set; }
        public string T_90_LHC { get; set; }
        public string T_90_RJ { get; set; }
    }
}