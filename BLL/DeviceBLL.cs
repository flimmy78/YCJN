using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 基础设备类
    /// 作者：YZG
    /// </summary>
    public class DeviceBLL
    {
        string sql = "";
        string errMsg = "";
        DataTable dt = null;
        /// <summary>
        /// 自增长
        /// </summary>
        public int ID_KEY { get; set; }
        /// <summary>
        /// 点检项ID
        /// </summary>
        public string T_ITEMID { get; set; }
        /// <summary>
        /// 点检项部位
        /// </summary>
        public string T_ITEMPOSITION { get; set; }
        /// <summary>
        /// 点检项描述
        /// </summary>
        public string T_ITEMDESC { get; set; }
        /// <summary>
        /// 点检项检查内容
        /// </summary>
        public string T_CONTENT { get; set; }
        /// <summary>
        /// 点检类型
        /// </summary>
        public string T_TYPE { get; set; }
        /// <summary>
        /// 设备状态
        /// </summary>
        public string I_STATUS { get; set; }
        /// <summary>
        /// 观察名称
        /// </summary>
        public string T_OBSERVE { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string T_UNIT { get; set; }
        /// <summary>
        /// 测量下限
        /// </summary>
        public string F_LOWER { get; set; }
        /// <summary>
        /// 测量上限
        /// </summary>
        public string F_UPPER { get; set; }
        /// <summary>
        /// 是否频谱(0-否,1-是)
        /// </summary>
        public string I_SPECTRUM { get; set; }
        /// <summary>
        /// 所属设备ID
        /// </summary>
        public string T_DEVICEID { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string T_STARTTIME { get; set; }
        /// <summary>
        /// 周期类型
        /// </summary>
        public string T_PERIODTYPE { get; set; }
        /// <summary>
        /// 周期数值
        /// </summary>
        public string T_PERIODVALUE { get; set; }



        DeviceDAL Device = new DeviceDAL();
        DeviceDAL dd = new DeviceDAL();
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DeviceBLL()
        {

        }

        /// <summary>
        /// 获取设备树
        /// </summary>
        /// <returns></returns>
        public string GetTotalDevice()
        {
            System.Text.StringBuilder sb = new StringBuilder();
            DataTable dt = Device.GetRootDevice(); sb.Append("[");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("{id:'" + dt.Rows[i][1].ToString() + "',pId:'" + dt.Rows[i][3].ToString() + "',nodeID:'" + dt.Rows[i][1] + "',name:'" + dt.Rows[i][2].ToString() + "',t:'" + dt.Rows[i][1].ToString() + "', open:true},");
                }
            }


            dt = Device.GetNotRootDevice();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    sb.Append("{id:'" + dt.Rows[i][1].ToString() + "',pId:'" + dt.Rows[i][3].ToString() + "',nodeID:'" + dt.Rows[i][1] + "',name:'" + dt.Rows[i][2].ToString() + "',t:'" + dt.Rows[i][1].ToString() + "', open:true},");
                }
            }

            DataTable dtItem = null;
            dt = Device.GetItems();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dtItem = Device.GetBaseItem(dt.Rows[i]["T_NODEID"].ToString());
                    if (dtItem.Rows.Count > 0)

                        sb.Append("{id:'" + dtItem.Rows[0]["ID_KEY"].ToString() + "',pId:'" + dtItem.Rows[0]["T_DEVICEID"].ToString() + "',nodeID:'" + dtItem.Rows[0]["ID_KEY"] + "',name:'" + dtItem.Rows[0]["T_ITEMDESC"].ToString() + "',t:'" + dtItem.Rows[0]["ID_KEY"].ToString() + "', open:true},");
                }
            }

            return sb.ToString();
        }

        public int GetExistChildNode(string paraId)
        {
            return Device.RetCount(paraId);
        }

        public DataRow GetDeviceInfo(string deviceID)
        {
            return Device.ReadDeviceInfo(deviceID);
        }

        public string EditFile(string filePath, string deviceId)
        {
            return Device.EditFile(filePath, deviceId);
        }

        public DataRow GetItem(string id_key)
        {
            return Device.ReadItem(id_key);
        }

        public IList<Hashtable> GetItemByIdKey(string id_key)
        {
            SAC.Helper.DateHelper helper = new SAC.Helper.DateHelper();

            return helper.DataTableToList(Device.ReadItemByIdKey(id_key));

        }

        public IList<Hashtable> GetItems(string deviceId)
        {
            return Device.ReadItmes(deviceId);
        }


        /// <summary>
        /// 获取所有设备表
        /// </summary>
        /// <returns></returns>
        public DataTable RetTableDevice()
        {
            return dd.RetTableDevice();
        }

    }
}
