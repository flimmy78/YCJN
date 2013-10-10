using System;
using SAC.DB2;
using SAC.Plink;
using SAC.Elink;
using SAC.Helper;
using System.Text;
using System.Data;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;

///创建者：刘海杰
///日期：2013-09-12

namespace DAL.DataManage
{
    public class DALUnitDataManage
    {
        string rtDBType = "";   //实时数据库
        string rlDBType = "";   //关系数据库
        Elink ek = new Elink();
        Plink pk = new Plink();

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <returns></returns>
        private void init()
        {
            rtDBType = IniHelper.ReadIniData("RTDB", "DBType", null);
            rlDBType = IniHelper.ReadIniData("RelationDBbase", "DBType", null);
        }
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string Fuzzy_Query(string id)
        {
            string str = "%";
            for (int i = 0; i < id.Trim(' ').Split(' ').Length; i++)
            {
                str += id.Trim(' ').Split(' ')[i] + "%";
            }
            return str;
        }


        public bool Re_Name(string id) {
            bool flag = false;
            string errMsg = "";
            string sql = "update T_BASE_DATUNM_DATA set FILE_DESC ='" + id.Split(',')[1] + "' where ID_KEY=" + id.Split(',')[0];
            if (rlDBType == "SQL")
            {

            }
            else
            {
                flag = DBdb2.RunNonQuery(sql, out errMsg);
            }

            return flag;
        }

        public bool De_lete(string id)
        {
            bool flag = false;
            string errMsg = "";
            string sql = "delete from T_BASE_DATUNM_DATA  where ID_KEY=" + id;
            if (rlDBType == "SQL")
            {

            }
            else
            {
                flag = DBdb2.RunNonQuery(sql, out errMsg);
            }

            return flag;
        }

        public IList<Hashtable> Get_All_data(string unit_id)
        {
            
            this.init();
            ArrayList list = new ArrayList();
            DataSet DS = new DataSet();
            string errMsg = "";
            IList<Hashtable> listdata = new List<Hashtable>();
            Hashtable ht = new Hashtable();
            string sql_str = "";
            sql_str = "SELECT T_BASE_DATUNM_DATA.ID_KEY,FILE_DESC,PARADESC,T_TIME FROM T_BASE_DATUNM_DATA left join T_BASE_DATUNM  on  T_BASE_DATUNM_DATA.FILE_TYPE=T_BASE_DATUNM.PARA_ID where UNIT_ID ='" + unit_id.Split(',')[0] + "' ";
            if (unit_id.Split(',')[1] != "-请选择-")
            {
                sql_str += " and FILE_TYPE='" + unit_id.Split(',')[1] + "' ";
            }

            if (unit_id.Split(',')[2] != "")
            {
                sql_str += " and FILE_DESC like '" + Fuzzy_Query(unit_id.Split(',')[2]) + "' ";
            }
            sql_str += " order by T_TIME asc";
            DS = DBdb2.RunDataSet(sql_str, out errMsg);
            if (DS.Tables[0].Rows.Count > 0)
            {
                int num = DS.Tables[0].Rows.Count;
                foreach (DataRow row in DS.Tables[0].Rows)
                {
                    ht = new Hashtable();
                    ht.Add("ID", num);
                    ht.Add("ID_KEY", row["ID_KEY"].ToString());
                    ht.Add("FILE_DESC", row["FILE_DESC"].ToString());
                    ht.Add("PARADESC", row["PARADESC"].ToString());
                    ht.Add("T_TIME", row["T_TIME"].ToString());
                    listdata.Add(ht);
                    num--;
                }
            }
            return listdata;
        } 


        /// <summary>
        /// 获取文件类别  T_BASE_DATUNM
        /// </summary>
        /// <returns></returns>
        public DataSet Get_Type()
        {
            this.init();
            string errMsg = "";
            DataSet DS = new DataSet();
            string sql = "select PARA_ID,PARADESC from T_BASE_DATUNM";
            if (rlDBType == "SQL")
            {

            }
            else
            {
                DS = DBdb2.RunDataSet(sql, out errMsg);
            }
            return DS;
        }

        /// <summary>
        /// 上传文档
        /// </summary>
        /// <param name="xmlID"></param>
        /// <param name="xmlName"></param>
        /// <param name="fileBytes"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool RetBoolUpFile(string unit_id, string Name,string type, byte[] fileBytes)
        {
            this.init();
           string errMsg = "";
            bool flag = false;

            if (fileBytes.Length > 0)
            {
                if (rlDBType == "DB2")
                {
                   

                }
                else
                {
                    string sql = "insert into  T_BASE_DATUNM_DATA(UNIT_ID,FILE_DESC,FILE_TYPE,FILE_DATA,T_TIME)values('" + unit_id + "','" + Name + "','"+type+"',? ,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "') ";
                    //string sql = "insert into  T_BASE_DATUNM_DATA(UNIT_ID,FILE_DESC,FILE_TYPE,T_TIME)values('" + unit_id + "','" + Name + "','" + type + "' ,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "') ";
                    OleDbConnection con = new OleDbConnection(DBdb2.SetConString());
                    try
                    {
                        con.Open();
                        OleDbCommand oledbcom = new OleDbCommand(sql, con);

                        oledbcom.Parameters.Add("?", fileBytes);

                        if (oledbcom.ExecuteNonQuery() > 0)
                            flag = true;

                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        errMsg = ex.Message;
                    }
                    finally { con.Close(); }
                }
                
            }

            return flag;
        }


        ///下载文档
        public bool  DownLoadFile(string id)
        {
            string errMsg = "", FileName = "";
            bool flag = true;
            try
            {
            this.init();
            
            
            DataSet DS = new DataSet();
            string sql = "select * from T_BASE_DATUNM_DATA where ID_KEY=" + id.Split('|')[0];
            if (rlDBType == "SQL")
            {

            }
            else
            {
                DS = DBdb2.RunDataSet(sql, out errMsg);
            }
            if (DS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in DS.Tables[0].Rows)
                {
                    FileName = row["FILE_DESC"].ToString();
                    byte[] bytes = (byte[])row["FILE_DATA"];
                    FileStream fs = new FileStream(id.Split('|')[1] + FileName, FileMode.CreateNew);
                    //FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Flush();
                    fs.Close();
                    //ht.Add("T_TIME", row["T_TIME"].ToString());
                }
            }
            }
            catch (Exception ce)
            {
                errMsg = ce.Message;
                flag = false;
            }
            return flag;
        }
    }
}
