using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace Bussiness
{
    /// <summary>
    /// 配置类
    /// </summary>
    public class ThermaltestPara
    {
        /// <summary>
        /// 判断参数是否存在
        /// </summary>
        /// <param name="paraID"></param>
        /// <returns></returns>
        public bool IsExitPara(string paraID)
        {
            bool flag = true;
            string sql = "SELECT count(*) FROM ADMINISTRATOR.THERMALTESTPARA WHERE PARAID='" + paraID + "'";
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            DataTable dt = link.ExcuteRetureTable(sql);
            if (dt != null && dt.Rows.Count > 0)
                if (dt.Rows[0][0].ToString() == "0")
                    flag = false;

            return flag;

        }
        /// <summary>
        /// 获取配置描述
        /// </summary>
        /// <param name="paraID"></param>
        /// <returns></returns>
        public string GetParaDesc(string paraID)
        {
            string desc="";
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            string sql = "SELECT DESC FROM ADMINISTRATOR.THERMALTESTPARA WHERE PARAID='" + paraID + "'";
            object obj = link.GetFirstValue(sql);
            if (obj != null && obj.ToString() != "")
                desc = obj.ToString();
            return desc;
        }
        /// <summary>
        /// 获取描述与单位
        /// </summary>
        /// <param name="paraID"></param>
        /// <returns></returns>
        public string GetParaDescUnit(string paraID)
        {
            string desc = "";
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            //string sql = "SELECT DESC||'('||UNIT||')' FROM ADMINISTRATOR.THERMALTESTPARA WHERE PARAID='" + paraID + "'";
            string sql = "SELECT DESC,UNIT FROM ADMINISTRATOR.THERMALTESTPARA WHERE PARAID='" + paraID + "'";
            DataTable dt = link.ExcuteRetureTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0][1].ToString() != "")
                    desc = dt.Rows[0][0].ToString() + "(" + dt.Rows[0][1].ToString() + ")";
                else
                    desc = dt.Rows[0][0].ToString();
            }
            return desc;
        }
        /// <summary>
        /// 获取配置号
        /// </summary>
        /// <param name="desc"></param>
        /// <returns></returns>
        public string GetParaID(string desc)
        {
            string paraID = "";
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            string sql = "SELECT PARAID FROM ADMINISTRATOR.THERMALTESTPARA WHERE DESC='" + desc + "'";
            object obj = link.GetFirstValue(sql);
            if (obj != null && obj.ToString() != "")
                paraID = obj.ToString();
            return paraID;
        }

        public DataTable GetEditPara(string isEdit)
        {
            DataTable dt = null;
            string sql = "SELECT * FROM ADMINISTRATOR.THERMALTESTPARA WHERE ISEDIT='" + isEdit + "'";
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            dt = link.ExcuteRetureTable(sql);
            return dt;
        }
        /// <summary>
        /// 更新配置参数
        /// </summary>
        /// <param name="id_key"></param>
        /// <param name="formula"></param>
        public void Edit(int id_key,string formula)
        {
            string sql = "UPDATE ADMINISTRATOR.THERMALTESTPARA SET FORMULA='" + formula + "' WHERE ID_KEY=" + id_key;
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            link.Excute(sql);
        }
    }
}
