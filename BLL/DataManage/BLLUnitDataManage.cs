using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.Helper;
using System.Collections;
using System.Data;
using DAL;


///创建者：刘海杰
///日期：2013-09-12

namespace BLL.DataManage
{
    public class BLLUnitDataManage
    {

        DAL.DataManage.DALUnitDataManage DCA =new DAL.DataManage.DALUnitDataManage();

        /// <summary>
        /// 获取文件类别 T_BASE_DATUNM
        /// </summary>
        /// <returns></returns>
        public DataSet Get_Type()
        {
            return DCA.Get_Type();
        }

        public IList<Hashtable> Get_All_data(string unit_id)
        {
            return DCA.Get_All_data(unit_id);
        }

        public bool Re_Name(string id)
        {
            return DCA.Re_Name(id);
        }

        public bool De_lete(string id)
        {
            return DCA.De_lete(id);
        }

        public bool RetBoolUpFile(string unit_id, string Name,string type, byte[] fileBytes)
        {
            return DCA.RetBoolUpFile(unit_id, Name, type,fileBytes);
        }

        public bool DownLoadFile(string id)
        {
            return DCA.DownLoadFile(id);
        }
    }
}
