using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.Report
{
    /// <summary>
    /// 参数Id，以及对应的输出至关系库的数据表名。
    /// </summary>
    public class ReportInfo
    {
        /// <summary>
        /// 参数Id
        /// </summary>
        public string ParaId { set; get; }
        /// <summary>
        /// 参数描述
        /// </summary>
        public string ParaDesc { set; get; }
        /// <summary>
        /// 参数的值
        /// </summary>
        public double ParaValue { set; get; }
        /// <summary>
        /// 机组Id
        /// </summary>
        public string UnitId { set; get; }
        /// <summary>
        /// 机组名称
        /// </summary>
        private string unitName=string.Empty;
        public string UnitName { 
            
            set{unitName=value;}
            get { return unitName; }
        }
        
    }
}