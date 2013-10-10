using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.ComponentModel;

namespace Bussiness
{
    /// <summary>
    /// 全局配置类
    /// </summary>
    public class Config
    { 
        /// <summary>
        /// 热力试验唯一标识
        /// </summary>
        public int ID_KEY { get; set; }

        /// <summary>
        /// 热力试验取点间隔时间（秒）
        /// </summary>
        [DefaultValue(5)]
        public int DefaultInterval { get; set; }
        /// <summary>
        /// 热力试验最小间隔时间（秒）
        /// </summary>
        [DefaultValue(1)]
        public int MinInterval { get; set; }
        /// <summary>
        /// 最多采用工况数量
        /// </summary>
        public int MaxSampleCondition { get; set; }
        /// <summary>
        /// 热力试验默认时间（分钟）
        /// </summary>
        [DefaultValue(60)]
        public int DefaultTestDuration { get; set; }
        /// <summary>
        /// 热力试验最长时间（分钟）
        /// </summary>
        [DefaultValue(240)]
        public int MaxTestDuration { get; set; }
        /// <summary>
        /// 读取全局配置
        /// </summary>
        /// <returns></returns>
        public DataTable ReadConfig()
        {
            string sql="SELECT * FROM ADMINISTRATOR.THERMALTESTCFG";
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            DataTable dt = link.ExcuteRetureTable(sql);
            return dt;
        }
        /// <summary>
        /// 修改全局配置
        /// </summary>
        public void EditConfig(Config info)
        {
            string sql = "UPDATE ADMINISTRATOR.THERMALTESTCFG SET DefaultInterval=" + info.DefaultInterval + " ,MinInterval=" + info.MinInterval + ",MaxSampleCondition=" + info.MaxSampleCondition + ",DefaultTestDuration=" + info.DefaultTestDuration + ",MaxTestDuration=" + info.MaxTestDuration + " WHERE ID_KEY =" + info.ID_KEY + "";
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            link.Excute(sql);
        }
        /// <summary>
        /// 是否存在全局配置
        /// </summary>
        /// <returns></returns>
        public bool IsExitConfig()
        {
            bool flag=true;
            string sql = "SELECT COUNT(*) FROM ADMINISTRATOR.THERMALTESTCFG ";
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            DataTable dt = link.ExcuteRetureTable(sql);
            if (dt != null )
            {
                if (dt.Rows.Count == 0)
                    flag = false;
            }
            return flag;
        }
        /// <summary>
        /// 新增全局配置
        /// </summary>
        public void InsertConfig()
        {
            string sql = "INSERT INTO ADMINISTRAIR.THERMALTESTCFG(DefaultInterval,MinInterval,MaxSampleCondition,DefaultTestDuration,MaxTestDuration) VALUES ("+DefaultInterval+","+MinInterval+","+MaxSampleCondition+","+DefaultTestDuration+","+MaxTestDuration+")";
            DB2Provider.DataLink link = new DB2Provider.DataLink();
            link.Excute(sql);
        }
    }
}
