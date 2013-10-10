using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.Statistic
{
    public class ConsumeInfo
    {
        /// <summary>
        /// 耗差名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 耗差值
        /// </summary>
        public double Count { set; get; }

        public double StandardValue { set; get; }

        public double RealValue { set; get; }

        public double ConsumeValue { set; get; }
    }
}