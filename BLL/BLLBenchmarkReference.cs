using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.Helper;
using System.Collections;
using System.Data;
using DAL;

///创建者：刘海杰
///日期：2013-09-26
namespace BLL
{
    public class BLLBenchmarkReference
    {

        DAL.DALBenchmarkReference DCA = new DALBenchmarkReference();

        /// <summary>
        /// 获取队标基准值公式
        /// </summary>
        /// <param name="unit">机组编号</param>
        /// <returns></returns>
        public DataSet GetBoilerData(string unit)
        {
            return DCA.GetBoilerData(unit);
        }

        public bool updateBoiler(string unit)
        {
            return DCA.updateBoiler(unit);
        }
    }
}
