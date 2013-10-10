using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SAC.JScript;
using SAC.Helper;
using Entity.ConsumeIndicator;
using Entity.ProPara;
using System.Collections;
using Newtonsoft.Json;
using System.IO;
using System.Data.OleDb;
using SAC.DB2;

namespace DJXT.DataManage
{
    public partial class UnitDataManage : System.Web.UI.Page
    {
        string errMsg = "", param = "", para_rename = "", para_delete = "", para_udate = "", para_down="";
        private IList<Hashtable> list = new List<Hashtable>();
        protected void Page_Load(object sender, EventArgs e)
        {
            param = Request["param"]; para_rename = Request["para_rename"];
            para_delete = Request["para_delete"]; para_udate = Request["para_udate"];
            para_down = Request["para_down"];
            if ((param != "") && (param != null))
            {
                GET_DATA(param);
            }
            if ((para_rename != "") && (para_rename != null))
            {
                rename(para_rename);
            }
            if ((para_delete != "") && (para_delete != null))
            {
                delete(para_delete);
            }
            if ((para_udate != "") && (para_udate != null))
            {
                update(para_udate);
            }
            if ((para_down != "") && (para_down != null))
            {
                download(para_down);
            }
            if (!IsPostBack)
            {
                Sec_data_bind();
            }
        }


        private void download(string msg)
        {
            BLL.DataManage.BLLUnitDataManage BCA = new BLL.DataManage.BLLUnitDataManage();

            BCA.DownLoadFile(msg);
        }

        private void update(string ms)
        {
            BLL.DataManage.BLLUnitDataManage BCA = new BLL.DataManage.BLLUnitDataManage();
            errMsg = "";
            string info = "";
            string flPath = ms.Split('|')[0];
            //if(ms.Split('|')[0].Split('\\').Length)
            string filename = ms.Split('|')[0].Split('\\')[ms.Split('|')[0].Split('\\').Length - 1];

            FileStream fs = new FileStream(flPath.Replace("\\", "/"), FileMode.Open, FileAccess.Read); //将图片以文件流的形式进行保存
            //BinaryReader br = new BinaryReader(fs);

            //byte[] imgBytesIn = br.ReadBytes((int)fs.Length);  //将流读入到字节数组中

            byte[] imgBytesIn = new byte[fs.Length];
            fs.Read(imgBytesIn, 0, Convert.ToInt32(fs.Length));
            fs.Flush();
            fs.Close();
            //FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            //byte[] bufferPhoto = new byte[stream.Length];
            //stream.Read(bufferPhoto, 0, Convert.ToInt32(stream.Length));
            //stream.Flush();
            //stream.Close();
            



            string data_type = "";
            if (ms.Split('|')[2] != "-请选择-")
            {
                data_type = ms.Split('|')[2];
            }
            //ms.Split('|')[0].Split('\\')[ms.Split('|')[0].Split('\\').Length - 1]
            bool flag = BCA.RetBoolUpFile(ms.Split('|')[1], filename, data_type, imgBytesIn);

            if (errMsg == "")
            {
                if (flag == true)
                {
                    info = "文件导入成功!";
                }
                else
                {
                    info = "文件导入失败!";
                }
            }
            else
                info = errMsg;

        }
        private void delete(string msg)
        {
            BLL.DataManage.BLLUnitDataManage BCA = new BLL.DataManage.BLLUnitDataManage();
            bool flag = BCA.De_lete(msg);
            Response.Clear();
            Response.Write(flag);
            Response.End();
        }

        private void rename(string msg)
        {
            BLL.DataManage.BLLUnitDataManage BCA = new BLL.DataManage.BLLUnitDataManage();
            bool flag = BCA.Re_Name(msg);
            Response.Clear();
            Response.Write(flag);
            Response.End();
        }

        private void GET_DATA(string msg)
        {
            BLL.DataManage.BLLUnitDataManage BCA = new BLL.DataManage.BLLUnitDataManage();
            list = BCA.Get_All_data(msg);

            object obj = new
            {
                rows = list
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }

        private void Sec_data_bind()
        {
            BLL.BLLRealQuery BLQ = new BLL.BLLRealQuery();
            DataSet DS = BLQ.Get_Company_Info(out errMsg);
            this.sec_company.DataSource = DS.Tables[0].DefaultView;
            this.sec_company.DataTextField = "T_COMPANYDESC";
            this.sec_company.DataValueField = "T_COMPANYID";
            this.sec_company.DataBind();
            this.sec_company.Items.Insert(0, "-请选择-");


            BLL.DataManage.BLLUnitDataManage BDM = new BLL.DataManage.BLLUnitDataManage();
            this.sec_type.DataSource = BDM.Get_Type().Tables[0].DefaultView;
            this.sec_type.DataTextField = "PARADESC";
            this.sec_type.DataValueField = "PARA_ID";
            this.sec_type.DataBind();
            this.sec_type.Items.Insert(0, "-请选择-");
            this.sec_data_type.DataSource = BDM.Get_Type().Tables[0].DefaultView;
            this.sec_data_type.DataTextField = "PARADESC";
            this.sec_data_type.DataValueField = "PARA_ID";
            this.sec_data_type.DataBind();
            
        }

        
    }
}