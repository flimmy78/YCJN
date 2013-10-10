using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.Helper;
using System.Collections;
using System.Data;
using DAL;


///创建者：刘海杰
///日期：2013-09-04

namespace BLL.PerformanceAlarm
{
    public class BLLUnitPerformance
    {

        DAL.PerformanceAlarm.DALUnitPerformance DCA = new DAL.PerformanceAlarm.DALUnitPerformance();

        /// <summary>
        /// 获取预警类别 T_BASE_FAULTCATEGORY
        /// </summary>
        /// <returns></returns>
        public DataSet GetFAULTCATEGORY()
        {

            return DCA.GetFAULTCATEGORY();
        }

        public DataSet Get_GRID_DATA(string unit_id)
        {
            return DCA.Get_GRID_DATA(unit_id);
        }

        /// <summary>
        /// 获取预警性质 T_BASE_FAULTPROPERTY
        /// </summary>
        /// <returns></returns>
        public DataSet GetFAULTPROPERTY()
        {

            return DCA.GetFAULTPROPERTY();
        }

        /// <summary>
        /// 获取预警专业分类 T_BASE_FAULTPROFESSIONAL
        /// </summary>
        /// <returns></returns>
        public DataSet GetFAULTPROFESSIONAL()
        {

            return DCA.GetFAULTPROFESSIONAL();
        }

        /// <summary>
        /// 获取预警原因分类 T_BASE_FAULTREASON
        /// </summary>
        /// <returns></returns>
        public DataSet GetFAULTREASON()
        {

            return DCA.GetFAULTREASON();
        }

        public DataSet Get_data(string para)
        {

            return DCA.Get_data(para);
        }

        public bool Edit_data(string para)
        {
            return DCA.Edit_data(para);
        }
    }
}
