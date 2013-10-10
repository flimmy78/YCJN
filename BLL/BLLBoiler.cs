using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAC.Helper;
using System.Collections;
using System.Data;
using DAL;
namespace BLL
{
    public class BLLBoiler
    {

        DAL.DALBoiler DCA = new DALBoiler();

        public DataSet GetBoilerData(string unit_id)
        {
            return DCA.GetBoilerData(unit_id);
        }

        public int InsertBoilerData(string unit_id)
        {
            return DCA.InsertBoilerData(unit_id);
        }
    }
}
