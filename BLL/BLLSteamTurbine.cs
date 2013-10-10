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
    public class BLLSteamTurbine
    {

        DAL.DALSteamTurbine DCA = new DALSteamTurbine();

        public DataSet GetSteamTurbineData(string unit_id)
        {
            return DCA.GetSteamTurbineData(unit_id);
        }

        public int InsertSteamTurbineData(string unit_id)
        {
            return DCA.InsertSteamTurbineData(unit_id);
        }
    }
}
