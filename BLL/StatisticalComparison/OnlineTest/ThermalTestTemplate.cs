using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using DB2Provider;

namespace Bussiness
{
    /// <summary>
    /// 报告模板类
    /// </summary>
    public class ThermalTestTemplate
    {
        /// <summary>
        /// 热力试验模板标识号
        /// </summary>
        public int ID_KEY { get; set; }
        /// <summary>
        /// 模板标识号
        /// </summary>
        public string TemplateName { get; set; }
        /// <summary>
        /// 机组编号
        /// </summary>
        public string UnitID { get; set; }
        /// <summary>
        /// Para表ParaID
        /// </summary>
        public string ParaID { get; set; }
        /// <summary>
        /// 显示序列
        /// </summary>
        public int SN { get; set; }
        /// <summary>
        /// 版本
        /// </summary> 
        public int Version { get; set; }
        /// <summary>
        /// 模版编号
        /// </summary>
        public int TemplateID { get; set; }
        /// <summary>
        /// 插入实验模版 
        /// </summary>
        public void InsertTemplate()
        {
            string sql = "INSERT INTO ADMINISTRATOR.ThermalTestTemplate(TEMPLATENAME,UNITID,PARAID,SN,TEMPLATEID) VALUES('" + TemplateName + "','" + UnitID + "','" + ParaID  + "'," + SN + "," +TemplateID  + ")";
            DataLink link = new DataLink();
            link.Excute(sql);
        }
        /// <summary>
        /// 获取所有不同实验名
        /// </summary>
        /// <returns></returns>
        public DataTable GetReportName()
        {
            
            DataTable dt = null;
            try
            {
                string sql="";
                //if (UnitID == "0")
                //    //sql = "SELECT DISTINCT(TemplateName),TemplateID FROM ADMINISTRATOR.ThermalTestTemplate";
                //    sql = "SELECT DISTINCT(TemplateName) FROM ADMINISTRATOR.ThermalTestTemplate";
                //else
                sql = "SELECT DISTINCT(TemplateName),TEMPLATEID FROM ADMINISTRATOR.ThermalTestTemplate WHERE UNITID='" + UnitID + "'";
                DataLink link = new DataLink();
                dt = link.ExcuteRetureTable(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        /// <summary>
        /// 返回报告模版编号
        /// </summary>
        /// <param name="templateName">实验名</param>
        /// <param name="unitId">机组编号</param>
        /// <returns></returns>
        public int GetTemplateID(string templateName, string unitId)
        {
            DataTable dt = null;
            try
            {

                string sql = "SELECT TemplateID FROM ADMINISTRATOR.ThermalTestTemplate WHERE TemplateName='" + templateName + "' AND UNITID='" + unitId + "'";
                if (unitId == "0")
                {
                    if (templateName != "0")
                        sql = "SELECT TemplateID FROM ADMINISTRATOR.ThermalTestTemplate WHERE TemplateName='" + templateName + "'";
                    else
                        return 0;
                }
                //else
                //{
                //    if (templateName == "0")
                //        sql = "SELECT TemplateID FROM ADMINISTRATOR.ThermalTestTemplate WHERE TemplateName='" + templateName + "'";
                //}
                DataLink link = new DataLink();
                dt = link.ExcuteRetureTable(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Convert.ToInt32(dt.Rows[0][0]);
        }
        /// <summary>
        /// 返回报告模版编号集
        /// </summary>
        /// <param name="templateName"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public DataTable GetTemplateIDs(string templateName, string unitId)
        {
            DataTable dt = null;
            try
            {

                string sql = "SELECT TemplateID FROM ADMINISTRATOR.ThermalTestTemplate WHERE TemplateName='" + templateName + "' AND UNITID='" + unitId + "'";
                if (unitId == "0")
                {
                    if (templateName != "0")
                        sql = "SELECT TemplateID FROM ADMINISTRATOR.ThermalTestTemplate WHERE TemplateName='" + templateName + "'";
                }
                DataLink link = new DataLink();
                dt = link.ExcuteRetureTable(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        /// <summary>
        /// 获取实验报告模版
        /// </summary>
        /// <returns></returns>
        public DataTable GetReportByTempleteID()
        {
            DataTable dt = null;
            try
            {
                string sql = "SELECT * FROM ADMINISTRATOR.ThermalTestTemplate WHERE ID_KEY=" + ID_KEY + "";            
                DataLink link = new DataLink();
                dt = link.ExcuteRetureTable(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        /// <summary>
        /// 判断实验模版参数是否存在
        /// </summary>
        /// <param name="templateId">模版编号</param>
        /// <param name="paraId">参数编号</param>
        /// <returns></returns>
        public bool IsExitTemplatePara(int templateId,string paraId)
        {
            bool flag = false;
            string sql = "SELECT count(*) FROM ADMINISTRATOR.ThermalTestTemplate WHERE PARAID='" + paraId + "' AND TemplateID=" + templateId;
            DataLink link = new DataLink();
            DataTable dt = link.ExcuteRetureTable(sql);
            if (dt != null && dt.Rows.Count > 0)
                if (dt.Rows[0][0].ToString() != "0")
                    flag = true;

            return flag;
        }
        
    }
}
