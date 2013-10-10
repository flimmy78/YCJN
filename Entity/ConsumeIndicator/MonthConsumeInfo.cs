using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.ConsumeIndicator
{
    /// <summary>
    /// 供电煤耗月线类
    /// </summary>
    public class MonthConsumeInfo
    {
        public MonthConsumeInfo()
        { }
        public int year { get; set; }
        public int month { get; set; }
        public double values { get; set; }

    }
}