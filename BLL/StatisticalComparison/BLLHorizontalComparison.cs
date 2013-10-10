using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.Helper;
using System.Collections;
using System.Data;
using DAL;


///创建者：刘海杰
///日期：2013-08-30
namespace BLL.StatisticalComparison
{
    public class BLLHorizontalComparison
    {
        DAL.StatisticalComparison.DALHorizontalComparison DCA = new DAL.StatisticalComparison.DALHorizontalComparison();

        /// <summary>
        /// 获取所有机组容量等级 T_BASE_UNIT
        /// </summary>
        /// <returns></returns>
        public DataSet GetCAPABILITYLEVEL()
        {
            return DCA.GetCAPABILITYLEVEL();
        }

        /// <summary>
        /// 获取所有机组类型 T_BASE_UNIT
        /// </summary>
        /// <returns></returns>
        public DataSet GetPLANTTYPE()
        {
            return DCA.GetPLANTTYPE();
        }
        /// <summary>
        /// 获取所有机组类型 T_BASE_UNIT
        /// </summary>
        /// <returns></returns>
        public DataSet GetBOILERDESC()
        {
            return DCA.GetBOILERDESC();
        }

        /// <summary>
        /// 获取汽轮机厂家 T_BASE_STEAM
        /// </summary>
        /// <returns></returns>
        public DataSet GetSTEAMDESC()
        {
            return DCA.GetSTEAMDESC();
        }

        /// <summary>
        /// 获取HighCharts所需数据
        /// </summary>
        /// <param name="para_id"></param>
        /// <returns></returns>
        public IList<Hashtable> GetChartData(string para_id)
        {
            return DCA.GetChartData(para_id);
        }
    }
}
