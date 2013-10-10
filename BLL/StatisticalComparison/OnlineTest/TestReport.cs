using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Bussiness
{
    public class TestReport
    {
        /// <summary>
        /// 获取实验报告值
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public DataTable GetReportResult(int reportId)
        {
            string sql = "SELECT * FROM ADMINISTRATOR.THERMALTESTREPORT AS R LEFT JOIN THERMALTESTTEMPLATE AS T ON R.PARAID=T.PARAID WHERE R.ReportID=" + reportId + " ORDER BY T.SN";
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            DataTable dt = link.ExcuteRetureTable(sql);
            return dt;
        }
        public DataTable GetReportResult(string colReportId)
        {
            string sql = "SELECT * FROM ADMINISTRATOR.THERMALTESTREPORT AS R LEFT JOIN THERMALTESTTEMPLATE AS T ON R.PARAID=T.PARAID WHERE R.ReportID IN " + colReportId + " ORDER BY T.SN";
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            DataTable dt = link.ExcuteRetureTable(sql);
            return dt;
        }
        /// <summary>
        /// 获取实验报告值
        /// </summary>
        /// <param name="reportid"></param>
        /// <param name="paraid"></param>
        /// <returns></returns>
        public string GetReportValue(string reportid, string paraid)
        {
            string value = "";
            string sql = "SELECT VALUE FROM ADMINISTRATOR.THERMALTESTREPORT WHERE PARAID='" + paraid + "' AND ReportId=" + reportid;
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            object obj = link.GetFirstValue(sql);
            if (obj != null)
                value = obj.ToString();
            return value;
        }
        /// <summary>
        /// 删除实验报告数据
        /// </summary>
        /// <param name="reportid"></param>
        public void DeleteReport(int reportid)
        {
            string sql = "DELETE FROM ADMINISTRATOR.THERMALTESTREPORT WHERE ReportId=" + reportid;
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            link.Excute(sql);
        }
    }
}
