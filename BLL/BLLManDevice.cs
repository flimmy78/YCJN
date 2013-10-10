using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Collections;
using SAC.Helper;

namespace BLL
{
    /// <summary>
    /// 
    /// </summary>
    public class BLLManDevice
    {
        string sql = "";
        string errMsg = "";
        DataTable dt = null;
        DateHelper pb = new DateHelper();

        DALManDevice dmd = new DALManDevice();
        /// <summary>
        /// 获取所有设备表
        /// </summary>
        /// <returns></returns>
        public DataTable RetTableDevice()
        {
            return dmd.RetTableDevice();
        }

        /// <summary>
        /// 判断子节点是否存在
        /// </summary>
        /// <returns>2:最底层设备</returns>
        public int RetCount(string nodeID)
        {
            return dmd.RetCount(nodeID);
        }

        /// <summary>
        /// 根据设备标识获取设备信息
        /// </summary>
        /// <param name="treeID"></param>
        /// <returns></returns>
        public DataRow RetdrBySBID(string treeID)
        {
            return dmd.RetdrBySBID(treeID);
        }

        /// <summary>
        /// 附件上传
        /// </summary>
        /// <param name="SBID"></param>
        /// <param name="fileBytes"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool RetBoolUpFile(string SBID, byte[] fileBytes, out string errMsg)
        {
            return dmd.RetBoolUpFile(SBID, fileBytes, out errMsg);
        }

        /// <summary>
        /// 附件下在
        /// </summary>
        /// <param name="sbID"></param>
        /// <param name="sbName"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public byte[] RetBoolDownFile(string sbID, string sbName, out string errMsg)
        {
            return dmd.RetBoolDownFile(sbID, sbName, out errMsg);
        }

        /// <summary>
        /// 返回Device INFO ADN BASE
        /// </summary>
        /// <param name="devID"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable RetTabDevByDevID(string devID, out string errMsg)
        {
            return dmd.RetTabDevByDevID(devID, out errMsg);
        }

        /// <summary>
        /// 获取STATUS表
        /// </summary>
        /// <param name="lineNodeKey"></param>
        /// <returns></returns>
        public IList<Hashtable> RetTabStatus()
        {
            return pb.DataTableToList(dmd.RetTabStatus());
        }

        /// <summary>
        /// 获取STATUS表
        /// </summary>
        /// <param name="lineNodeKey"></param>
        /// <returns></returns>
        public DataTable RetTabStatus1()
        {
            return dmd.RetTabStatus();
        }

        /// <summary>
        /// 获取设备表中跟目录中第一条
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns>deviceDesc + ',' + file + ',' + status;</returns>
        public string GetDrDevByTopOne(string sbID, out string errMsg)
        {
            return dmd.GetDrDevByTopOne(sbID, out errMsg);
        }

        /// <summary>
        /// 新增设备状态
        /// </summary>
        /// <param name="devID"></param>
        /// <param name="time"></param>
        /// <param name="count"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool SaveStatus(string devID, string time, string count, out string errMsg)
        {
            return dmd.SaveStatus(devID, time, count, out errMsg);
        }

        /// <summary>
        /// 获取点检项
        /// </summary>
        /// <param name="sbID"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable RetTabItemsBySbID(string sbID, out string errMsg)
        {
            return dmd.RetTabItemsBySbID(sbID, out errMsg);
        }

        public IList<Hashtable> RetTabItemsBySbID(string sbID)
        {
            return pb.DataTableToList(dmd.RetTabItemsBySbID(sbID, out errMsg));
        }

        /// <summary>
        /// 更改设备状态调用接口
        /// </summary>
        /// <param name="sbID"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool RetUpdateDveiceStatus(string sbID, string time, out string errMsg)
        {
            return dmd.RetUpdateDveiceStatus(sbID, time, out errMsg);
        }

        /// <summary>
        /// 获取ITEM详细信息
        /// </summary>
        /// <param name="ID_KEY">ID_KEY</param>
        /// <returns></returns>
        public DataTable RetTabItemsByIDKEY(string ID_KEY)
        {
            return dmd.RetTabItemsByIDKEY(ID_KEY);
        }

        /// <summary>
        /// 根据ID获取设备状态信息
        /// </summary>
        /// <param name="stID"></param>
        /// <returns></returns>
        public DataRow RetDrByStatusID(string stID)
        {
            return dmd.RetDrByStatusID(stID);
        }

        /// <summary>
        /// 新建点检项目
        /// </summary>
        /// <param name="nodeKey"></param>
        /// <param name="itemID"></param>
        /// <param name="itemBw"></param>
        /// <param name="itemDesc"></param>
        /// <param name="itemContent"></param>
        /// <param name="itemObserve"></param>
        /// <param name="itemUnit"></param>
        /// <param name="itemType"></param>
        /// <param name="itemStatus"></param>
        /// <param name="itemStatusQJ"></param>
        /// <param name="itemUpper"></param>
        /// <param name="itemLower"></param>
        /// <param name="itemSpectrum"></param>
        /// <param name="itemStartTime"></param>
        /// <param name="itemPerValue"></param>
        /// <param name="itemPerType"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool AddItem(string nodeKey, string itemBw, string itemDesc, string itemContent, string itemObserve, string itemUnit, string itemType, string itemStatus, string itemStatusQJ, string itemUpper, string itemLower, string itemSpectrum, string itemStartTime, string itemPerValue, string itemPerType, out string errMsg)
        {
            return dmd.AddItem(nodeKey, itemBw, itemDesc, itemContent, itemObserve, itemUnit, itemType, itemStatus, itemStatusQJ, itemUpper, itemLower, itemSpectrum, itemStartTime, itemPerValue, itemPerType, out  errMsg);
        }

        public bool EditItem(string idkey, string itemBw, string itemDesc, string itemContent, string itemObserve, string itemUnit, string itemType, string itemStatus, string itemStatusQJ, string itemUpper, string itemLower, string itemSpectrum, string itemStartTime, string itemPerValue, string itemPerType, out string errMsg)
        {
            return dmd.EditItem(idkey, itemBw, itemDesc, itemContent, itemObserve, itemUnit, itemType, itemStatus, itemStatusQJ, itemUpper, itemLower, itemSpectrum, itemStartTime, itemPerValue, itemPerType, out  errMsg);
        }

        /// <summary>
        /// 获取某个设备下点检项所有ID_KEY
        /// </summary>
        /// <param name="sbID"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public string RetStrItemsBySbID(string sbID, out string errMsg)
        {
            return dmd.RetStrItemsBySbID(sbID, out errMsg);
        }

        /// <summary>
        /// 获取设备名称 
        /// </summary>
        /// <param name="nodeKey"></param>
        /// <returns></returns>
        public string RetSBNameBySbNodeKey(string nodeKey)
        {
            return dmd.RetSBNameBySbNodeKey(nodeKey);

        }

        /// <summary>
        /// 获取设备名称 
        /// </summary>
        /// <param name="nodeKey"></param>
        /// <returns></returns>
        public DataRow RetSBNameBySbNodeKey1(string nodeKey)
        {
            return dmd.RetSBNameBySbNodeKey1(nodeKey);

        }

        /// <summary>
        /// 获取点检项信息
        /// </summary>
        /// <param name="nodeKey"></param>
        /// <returns></returns>
        public DataRow RetItemInfoByNodeKey(string nodeKey)
        {
            return dmd.RetItemInfoByNodeKey(nodeKey);
        }

        /// <summary>
        /// 编辑设备状态信息
        /// </summary>
        /// <param name="nodeKey"></param>
        /// <returns></returns>
        public bool EditStatusByIDkey(string idkey, string time, string statusid, out string errMsg)
        {
            return dmd.EditStatusByIDkey(idkey, time, statusid, out errMsg);
        }

        public DataTable RetTabDevByDevID(string sbID, int sCount, int eCount)
        {
            return dmd.RetTabDevByDevID(sbID, sCount, eCount);
        }
        public int RetTabDevByDevIDCount(string sbID)
        {
            return dmd.RetTabDevByDevIDCount(sbID);
        }

        /// <summary>
        /// 点检项分页
        /// </summary>
        /// <param name="sbID"></param>
        /// <param name="sCount"></param>
        /// <param name="eCount"></param>
        /// <returns></returns>
        public DataTable RetTabItemByDevID(string sbID, int sCount, int eCount)
        {
            return dmd.RetTabItemByDevID(sbID, sCount, eCount);
        }
        public int RetTabItemByDevIDCount(string sbID)
        {
            return dmd.RetTabItemByDevIDCount(sbID);
        }

        /// <summary>
        /// 点检项分页ALL
        /// </summary>
        /// <param name="sbID"></param>
        /// <param name="sCount"></param>
        /// <param name="eCount"></param>
        /// <returns></returns>
        public DataTable RetTabItemByDevIDAll(int sCount, int eCount)
        {
            return dmd.RetTabItemByDevIDAll(sCount, eCount);


        }
        public int RetTabItemByDevIDCountAll()
        {
            return dmd.RetTabItemByDevIDCountAll();
        }


        /// <summary>
        /// 历史详细信息分页
        /// 获取ITEM历史详细信息
        /// </summary>
        /// <param name="ID_KEY">ID_KEY</param>
        /// <returns></returns>
        public DataTable RetTabHisItemsByidKey(string ID_KEY, int sCount, int eCount)
        {
            return dmd.RetTabHisItemsByidKey(ID_KEY, sCount, eCount);
        }

        /// <summary>
        /// 历史详细信息分页
        /// 获取ITEM历史详细信息
        /// </summary>
        /// <param name="ID_KEY">ID_KEY</param>
        /// <returns></returns>
        public int RetTabHisItemsByidKeyCount(string ID_KEY)
        {
            return dmd.RetTabHisItemsByidKeyCount(ID_KEY);
        }

        #region 胡进财
        #region 设备数据分页
        /// <summary>
        /// 获取设备数量
        /// </summary>
        /// <param name="id">设备编号</param>
        /// <returns></returns>
        public int GetDeviceCount(string id)
        {
            DALManDevice dal = new DALManDevice();
            return dal.GetDeviceCount(id);
        }

        /// <summary>
        /// 获取区域数据集
        /// </summary>
        /// <param name="id">设备编号</param>
        /// <param name="sCount">开始条数</param>
        /// <param name="eCount">结束条数</param>
        /// <returns></returns>
        public DataTable GetDeviceDt(string id, int sCount, int eCount)
        {
            DALManDevice dal = new DALManDevice();
            return dal.GetDeviceDt(id, sCount, eCount);
        }
        #endregion
        #region 点检项数据分页
        /// <summary>
        /// 获取点检项数量
        /// </summary>
        /// <param name="id">设备编号</param>
        /// <returns></returns>
        public int GetItemCount(string id)
        {
            return dmd.GetItemCount(id);
        }

        /// <summary>
        /// 获取点检项数据集
        /// </summary>
        /// <param name="id">设备编号</param>
        /// <param name="sCount">开始条数</param>
        /// <param name="eCount">结束条数</param>
        /// <returns></returns>
        public DataTable GetItemDt(string id, int sCount, int eCount)
        {
            return dmd.GetItemDt(id, sCount, eCount);
        }
        #endregion
        #endregion
    }

    public class JsonHelper
    {
        /// <summary>
        /// 生成表单编辑赋值 JSON格式
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="displayCount"></param>
        /// <returns></returns>
        public static string CreateJsonOne(DataTable dt, bool displayCount)
        {
            StringBuilder JsonString = new StringBuilder();
            //Exception Handling        
            if (dt != null && dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{ ");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            JsonString.Append("ipt_" + dt.Columns[j].ColumnName.ToString().ToLower() + ":" + "\"" + dt.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            JsonString.Append("ipt_" + dt.Columns[j].ColumnName.ToString().ToLower() + ":" + "\"" + dt.Rows[i][j].ToString() + "\"");
                        }
                    }

                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("} ");
                    }
                    else
                    {
                        JsonString.Append("}, ");
                    }
                }

                return JsonString.ToString();
            }
            else
            {
                return null;
            }

        }


        /// <summary>
        /// 将DataTable中的数据转换成JSON格式
        /// </summary>
        /// <param name="dt">数据源DataTable</param>
        /// <param name="displayCount">是否输出数据总条数</param>
        /// <returns></returns>
        public static string CreateJsonParameters(DataTable dt, bool displayCount)
        {
            return CreateJsonParameters(dt, displayCount, dt.Rows.Count);
        }
        /// <summary>
        /// 将DataTable中的数据转换成JSON格式
        /// </summary>
        /// <param name="dt">数据源DataTable</param>
        /// <returns></returns>
        public static string CreateJsonParameters(DataTable dt)
        {
            return CreateJsonParameters(dt, true);
        }
        /// <summary>
        /// 将DataTable中的数据转换成JSON格式
        /// </summary>
        /// <param name="dt">数据源DataTable</param>
        /// <param name="displayCount">是否输出数据总条数</param>
        /// <param name="totalcount">JSON中显示的数据总条数</param>
        /// <returns></returns>
        public static string CreateJsonParameters(DataTable dt, bool displayCount, int totalcount)
        {
            StringBuilder JsonString = new StringBuilder();
            //Exception Handling        

            if (dt != null)
            {
                JsonString.Append("{ ");
                JsonString.Append("\"rows\":[ ");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{ ");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            //if (dt.Rows[i][j] == DBNull.Value) continue;
                            if (dt.Columns[j].DataType == typeof(bool))
                            {
                                JsonString.Append("\"JSON_" + dt.Columns[j].ColumnName.ToLower() + "\":" +
                                                  dt.Rows[i][j].ToString().ToLower() + ",");
                            }
                            else if (dt.Columns[j].DataType == typeof(string))
                            {
                                JsonString.Append("\"JSON_" + dt.Columns[j].ColumnName.ToLower() + "\":" + "\"" +
                                                  dt.Rows[i][j].ToString().Replace("\"", "\\\"") + "\",");
                            }
                            else
                            {
                                JsonString.Append("\"JSON_" + dt.Columns[j].ColumnName.ToLower() + "\":" + "\"" + dt.Rows[i][j] + "\",");
                            }
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            //if (dt.Rows[i][j] == DBNull.Value) continue;
                            if (dt.Columns[j].DataType == typeof(bool))
                            {
                                JsonString.Append("\"JSON_" + dt.Columns[j].ColumnName.ToLower() + "\":" +
                                                  dt.Rows[i][j].ToString().ToLower());
                            }
                            else if (dt.Columns[j].DataType == typeof(string))
                            {
                                JsonString.Append("\"JSON_" + dt.Columns[j].ColumnName.ToLower() + "\":" + "\"" +
                                                  dt.Rows[i][j].ToString().Replace("\"", "\\\"") + "\"");
                            }
                            else
                            {
                                JsonString.Append("\"JSON_" + dt.Columns[j].ColumnName.ToLower() + "\":" + "\"" + dt.Rows[i][j] + "\"");
                            }
                        }
                    }
                    /*end Of String*/
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("} ");
                    }
                    else
                    {
                        JsonString.Append("}, ");
                    }
                }
                JsonString.Append("]");

                if (displayCount)
                {
                    JsonString.Append(",");

                    JsonString.Append("\"total\":");
                    JsonString.Append(totalcount);
                }

                JsonString.Append("}");
                return JsonString.ToString().Replace("\n", "");
            }
            else
            {
                return null;
            }
        }

        #region object 2 json

        //private static void WriteDataRow(StringBuilder sb, DataRow row)
        //{
        //    sb.Append("{");
        //    foreach (DataColumn column in row.Table.Columns)
        //    {
        //        sb.AppendFormat("\"{0}\":", column.ColumnName);
        //        WriteValue(sb, row[column]);
        //        sb.Append(",");
        //    }
        //    // Remove the trailing comma.
        //    if (row.Table.Columns.Count > 0)
        //    {
        //        --sb.Length;
        //    }
        //    sb.Append("}");
        //}

        //private static void WriteDataSet(StringBuilder sb, DataSet ds)
        //{
        //    sb.Append("{\"Tables\":{");
        //    foreach (DataTable table in ds.Tables)
        //    {
        //        sb.AppendFormat("\"{0}\":", table.TableName);
        //        WriteDataTable(sb, table);
        //        sb.Append(",");
        //    }
        //    // Remove the trailing comma.
        //    if (ds.Tables.Count > 0)
        //    {
        //        --sb.Length;
        //    }
        //    sb.Append("}}");
        //}

        //private static void WriteDataTable(StringBuilder sb, DataTable table)
        //{
        //    sb.Append("{\"Rows\":[");
        //    foreach (DataRow row in table.Rows)
        //    {
        //        WriteDataRow(sb, row);
        //        sb.Append(",");
        //    }
        //    // Remove the trailing comma.
        //    if (table.Rows.Count > 0)
        //    {
        //        --sb.Length;
        //    }
        //    sb.Append("]}");
        //}

        //private static void WriteEnumerable(StringBuilder sb, IEnumerable e)
        //{
        //    bool hasItems = false;
        //    sb.Append("[");
        //    foreach (object val in e)
        //    {
        //        WriteValue(sb, val);
        //        sb.Append(",");
        //        hasItems = true;
        //    }
        //    // Remove the trailing comma.
        //    if (hasItems)
        //    {
        //        --sb.Length;
        //    }
        //    sb.Append("]");
        //}

        //private static void WriteHashtable(StringBuilder sb, IDictionary e)
        //{
        //    bool hasItems = false;
        //    sb.Append("{");
        //    foreach (string key in e.Keys)
        //    {
        //        sb.AppendFormat("\"{0}\":", key.ToLower());
        //        WriteValue(sb, e[key]);
        //        sb.Append(",");
        //        hasItems = true;
        //    }
        //    // Remove the trailing comma.
        //    if (hasItems)
        //    {
        //        --sb.Length;
        //    }
        //    sb.Append("}");
        //}

        //private static void WriteObject(StringBuilder sb, object o)
        //{
        //    MemberInfo[] members = o.GetType().GetMembers(BindingFlags.Instance | BindingFlags.Public);
        //    sb.Append("{");
        //    bool hasMembers = false;
        //    foreach (MemberInfo member in members)
        //    {
        //        bool hasValue = false;
        //        object val = null;
        //        if ((member.MemberType & MemberTypes.Field) == MemberTypes.Field)
        //        {
        //            FieldInfo field = (FieldInfo)member;
        //            val = field.GetValue(o);
        //            hasValue = true;
        //        }
        //        else if ((member.MemberType & MemberTypes.Property) == MemberTypes.Property)
        //        {
        //            PropertyInfo property = (PropertyInfo)member;
        //            if (property.CanRead && property.GetIndexParameters().Length == 0)
        //            {
        //                val = property.GetValue(o, null);
        //                hasValue = true;
        //            }
        //        }
        //        if (hasValue)
        //        {
        //            sb.Append("\"");
        //            sb.Append(member.Name);
        //            sb.Append("\":");
        //            WriteValue(sb, val);
        //            sb.Append(",");
        //            hasMembers = true;
        //        }
        //    }
        //    if (hasMembers)
        //    {
        //        --sb.Length;
        //    }
        //    sb.Append("}");
        //}

        //private static void WriteString(StringBuilder sb, IEnumerable s)
        //{
        //    sb.Append("\"");
        //    foreach (char c in s)
        //    {
        //        switch (c)
        //        {
        //            case '\"':
        //                sb.Append("\\\"");
        //                break;
        //            case '\\':
        //                sb.Append("\\\\");
        //                break;
        //            case '\b':
        //                sb.Append("\\b");
        //                break;
        //            case '\f':
        //                sb.Append("\\f");
        //                break;
        //            case '\n':
        //                sb.Append("\\n");
        //                break;
        //            case '\r':
        //                sb.Append("\\r");
        //                break;
        //            case '\t':
        //                sb.Append("\\t");
        //                break;
        //            default:
        //                int i = c;
        //                if (i < 32 || i > 127)
        //                {
        //                    sb.AppendFormat("\\u{0:X04}", i);
        //                }
        //                else
        //                {
        //                    sb.Append(c);
        //                }
        //                break;
        //        }
        //    }
        //    sb.Append("\"");
        //}

        //public static void WriteValue(StringBuilder sb, object val)
        //{
        //    if (val == null || val == DBNull.Value)
        //    {
        //        sb.Append("null");
        //    }
        //    else if (val is string || val is Guid)
        //    {
        //        WriteString(sb, val.ToString());
        //    }
        //    else if (val is bool)
        //    {
        //        sb.Append(val.ToString().ToLower());
        //    }
        //    else if (val is double ||
        //             val is float ||
        //             val is long ||
        //             val is int ||
        //             val is short ||
        //             val is byte ||
        //             val is decimal)
        //    {
        //        sb.AppendFormat(CultureInfo.InvariantCulture.NumberFormat, "{0}", val);
        //    }
        //    else if (val.GetType().IsEnum)
        //    {
        //        sb.Append((int)val);
        //    }
        //    else if (val is DateTime)
        //    {
        //        sb.Append("new Date(\"");
        //        sb.Append(((DateTime)val).ToString("MMMM, d yyyy HH:mm:ss",
        //                                            new CultureInfo("en-US", false).DateTimeFormat));
        //        sb.Append("\")");
        //    }
        //    else if (val is DataSet)
        //    {
        //        WriteDataSet(sb, val as DataSet);
        //    }
        //    else if (val is DataTable)
        //    {
        //        WriteDataTable(sb, val as DataTable);
        //    }
        //    else if (val is DataRow)
        //    {
        //        WriteDataRow(sb, val as DataRow);
        //    }
        //    else if (val is Hashtable)
        //    {
        //        WriteHashtable(sb, val as Hashtable);
        //    }
        //    else if (val is IEnumerable)
        //    {
        //        WriteEnumerable(sb, val as IEnumerable);
        //    }
        //    else
        //    {
        //        WriteObject(sb, val);
        //    }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="o"></param>
        ///// <returns></returns>
        //public static string Convert2Json(object o)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    WriteValue(sb, o);
        //    return sb.ToString();
        //}

        #endregion
    }
}
