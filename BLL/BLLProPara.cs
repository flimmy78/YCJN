using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.Helper;
using System.Collections;
using System.Data;
using DAL;

///创建者：刘海杰
///日期：2013-08-20

namespace BLL
{
    public class BLLProPara
    {
        DAL.DALProPara DCA = new DALProPara();

        public DataSet Get_Required_data(string unit_id,string num)
        {
            return DCA.GetProductionData(unit_id, num);
        }

        public DataSet GetProductionPreData(string stime, string etime, string unit_id)
        {
            return DCA.GetProductionPreData(stime,etime,unit_id);
        }

        /// <summary>
        /// 元素
        /// </summary>
        /// <param name="unit_id"></param>
        /// <returns></returns>
        public int InsertProductionElementData(string unit_id)
        {
            return DCA.InsertProductionElementData(unit_id);
        }

        /// <summary>
        /// 工业
        /// </summary>
        /// <param name="unit_id"></param>
        /// <returns></returns>
        public int InsertProductionIndustryData(string unit_id)
        {
            return DCA.InsertProductionIndustryData(unit_id);
        }

        /// <summary>
        /// 导入excel
        /// </summary>
        /// <param name="dataList"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool InsertExcelData(List<Entity.ProPara.ProductionProPara> dataList, out string errMsg)
        {
            return DCA.InsertExcelData(dataList, out errMsg);
        }
    }
}
