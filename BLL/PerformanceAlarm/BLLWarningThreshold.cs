using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.Helper;
using System.Collections;
using System.Data;
using DAL;


///创建者：刘海杰
///日期：2013-09-06

namespace BLL.PerformanceAlarm
{
    public class BLLWarningThreshold
    {
        DAL.PerformanceAlarm.DALWarningThreshold DCA = new DAL.PerformanceAlarm.DALWarningThreshold();
        public DataSet Get_GRID_DATA(string unit_id)
        {
            return DCA.Get_GRID_DATA(unit_id);
        }

        /// <summary>
        /// 获取考核点ID
        /// </summary>
        /// <param name="unit_id"></param>
        /// <returns></returns>
        public DataSet GETKAOHRDIAN_DESC(string unit_id)
        {
            return DCA.GETKAOHRDIAN_DESC(unit_id);
        }

        public bool Save_data(string para)
        {
            return DCA.Save_data(para);
        }
        public bool Delete_data(string para)
        {
            return DCA.Delete_data(para);
        }
        

        public void Insert_data(string para)
        {
            DCA.Insert_data(para);
        }
    }
}
