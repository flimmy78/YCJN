using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.ProPara
{
    [Serializable]
    public class ProductionProPara
    {
        public ProductionProPara()
        { }

        /// <summary>
        /// 机组ID
        /// </summary>
        public string T_UNITID { get; set; }
        /// <summary>
        /// 工业分析收到基低位发热量
        /// </summary>
        public double D_QNET_AR_PROX { get; set; }
        /// <summary>
        /// 工业分析收到基水分
        /// </summary>
        public double D_M_AR_PROX { get; set; }
        /// <summary>
        /// 工业分析空干基水分
        /// </summary>
        public double D_M_AD { get; set; }
        /// <summary>
        /// 工业分析收到基灰分
        /// </summary>
        public double D_A_AR_PROX { get; set; }
        /// <summary>
        /// 工业分析空干基灰分
        /// </summary>
        public double D_A_AD { get; set; }
        /// <summary>
        /// 工业分析干燥无灰基挥发分
        /// </summary>
        public double D_V_DAF { get; set; }
        /// <summary>
        /// 工业分析空干基全硫
        /// </summary>
        public double D_ST_AD { get; set; }
        /// <summary>
        /// 工业分析收到基全硫
        /// </summary>
        public double D_ST_AR { get; set; }
        /// <summary>
        /// 工业分析飞灰可燃物含碳量
        /// </summary>
        public double D_CFH_C_PROX { get; set; }
        /// <summary>
        /// 工业分析炉渣可燃物含碳量 
        /// </summary>
        public double D_CLZ_C_PROX { get; set; }
        /// <summary>
        /// 录入时间 
        /// </summary>
        public DateTime T_TIME { get; set; }
    }
}