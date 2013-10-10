using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.ConsumeIndicator
{
    /// <summary>
    /// 五大集团最优机组供电煤耗信息
    /// </summary>
    public class BestUnitConsumeInfo
    {
        public BestUnitConsumeInfo()
        { }
        public string T_COMPANY { get; set; }
        public double T_900_SL { get; set; }
        public double T_600_HJ { get; set; }
        public double T_300_HJ { get; set; }
        public double T_200_HJ { get; set; }
        public double T_120_HJ { get; set; }
       
    }
}