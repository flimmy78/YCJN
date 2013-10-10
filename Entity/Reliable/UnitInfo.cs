using System;

namespace SAC.Entity
{
    /// <summary>
    /// 机组信息实体类。
    /// </summary>
    [Serializable]
    public class UnitInfo
    {
        public UnitInfo()
        { }
        public string T_CODE { set; get; }
        public string T_TIME { set; get; }
        public DateTime T_BEGINTIME { set; get; }
        public DateTime T_ENDTIME { set; get; }
        public double I_PH { set; get; }

        public string T_FCATEGORYID { set; get; }
        public string T_FPROPERTYID { set; get; }
        public string T_FPROFEESIOID { set; get; }
        public string T_FREASONID { set; get; }
        public string T_EVENTDESC { set; get; }
        public string T_REASONANALYSE { set; get; }
        public string T_DEALCONDITION { set; get; }

        public double I_GAAG { set; get; }
        public double I_PUMPPOWER { set; get; }
        public double I_UTH { set; get; }
        public double I_SH { set; get; }
        public double I_RH { set; get; }
        public double I_AH { set; get; }
        public double I_POH { set; get; }
        public double I_BPOH { set; get; }
        public double I_SPOH { set; get; }
        public double I_HPPOH { set; get; }
        public double I_IACTH { set; get; }
        public double I_FOH { set; get; }
        public double I_UOH { set; get; }
        public double I_ERFDH { set; get; }
        public double I_EFDH { set; get; }
        public double I_EUNDH { set; get; }
        public double I_RT { set; get; }
        public double I_SST { set; get; }
        public double I_UST { set; get; }
        public double I_POT { set; get; }
        public double I_BPOT { set; get; }
        public double I_SPOT { set; get; }
        public double I_HPOT { set; get; }
        public double I_IACTT { set; get; }
        public double I_FOT { set; get; }
        public double I_UOT { set; get; }
        public double I_FUOT { set; get; }
        public double I_LOSEPOWER { set; get; }

        public double D_SF { set; get; }
        public double D_AF { set; get; }
        public double D_UF { set; get; }
        public double D_EAF { set; get; }
        public double D_POF { set; get; }
        public double D_UOF { set; get; }
        public double D_FOF { set; get; }
        public double D_UDF { set; get; }
        public double D_OF { set; get; }
        public double D_GCF { set; get; }
        public double D_UTF { set; get; }
        public double D_FOR { set; get; }
        public double D_EFOR { set; get; }
        public double D_UOR { set; get; }
        public double D_SR { set; get; }
        public double D_EXR { set; get; }
        public double D_FOOR { set; get; }

        public string S_MTBS { set; get; }
        public string S_CAH { set; get; }
        public string S_MTBF { set; get; }
        public string S_MTTPO { set; get; }
        public string S_MTTUO { set; get; }
        public double D_MPOD { set; get; }
        public double D_MUOD { set; get; }
        public string S_BMTTPO { set; get; }
        public string S_SMTTPO { set; get; }
        public double D_BMPOD { set; get; }
        public double D_SMPOD { set; get; }
        public double D_KWZJ { set; get; }
        public double I_GEH { set; get; }
        public double I_PWH { set; get; }
        public double I_GMH { set; get; }
        public double I_PMH { set; get; }
        public double I_GET { set; get; }
        public double I_PWT { set; get; }
        public double I_GMT { set; get; }
        public double I_PMT { set; get; }
        public double I_MOE { set; get; }
        public double I_TOE { set; get; }
        public double I_FMOE { set; get; }
        public double I_CMOE { set; get; }
        public double I_MOET { set; get; }
        public double I_TOET { set; get; }
        public double I_FMOET { set; get; }
        public double I_CMOET { set; get; }
    }
}