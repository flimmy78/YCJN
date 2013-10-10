using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.Statistic
{
    /// <summary>
    /// 参数Id，以及对应的输出至关系库的数据表名。
    /// </summary>
    public class ParaTableInfo
    {
        /// <summary>
        /// 参数Id
        /// </summary>
        public string ParaId { set; get; }
        /// <summary>
        /// 机组Id
        /// </summary>
        public string UnitId { set; get; }
        public string UnitName { set; get; }
        /// <summary>
        /// 参数描述
        /// </summary>
        public string ParaDesc { set; get; }
        /// <summary>
        /// 输出至关系库的数据表名
        /// </summary>
        public string OutTableName { set; get; }
    }
}