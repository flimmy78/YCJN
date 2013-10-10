using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.Helper;
using System.Collections;
using System.Data;
using DAL;

///创建者：刘海杰
///日期：2013-07-05
namespace BLL
{
    public class BLLCompareAnalyse
    {
        DAL.DALCompareAnalyse DCA = new DALCompareAnalyse();

        public IList<Hashtable> Get_Required_data(string unit_id, string[] para_id, string per, string stime, string etime, out string errMsg,out string max_data,out string min_data)
        {
            return DCA.Get_Required_data(unit_id, para_id, per,stime, etime, out errMsg,out max_data,out min_data);
        }
        public IList<Hashtable> Get_All_data(string unit_id, string[] para_id, string per, string stime, string etime, out string errMsg)
        {
            return DCA.Get_All_data(unit_id, para_id, per, stime, etime, out errMsg);
        }
    }
    
}
