using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BLL;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using SAC.DB2;

namespace DJXT.Device
{
    public partial class Device : System.Web.UI.Page
    {
        object obj = null;

        string errMsg = "";
        string resultMenu = "";
        public string strInfo="";

        DataTable dt = null;

        BLLManDevice bmd = new BLLManDevice();
        StringBuilder sb = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            string param = Request["param"];

            if (param == "")
            {
                GetListTree();
            }
            else if (param == "clickTree")
            {
                string id = Request.Form["id"];
                GetButton(id);
            }
            else if (param == "AddFile")
            {
                string sbID = Request.Form["SBID"];
                string sbName = HttpUtility.UrlDecode(Request.Form["SBID"]);
                string flPath = HttpUtility.UrlDecode(Request.Form["flPath"]);
                AddFile(sbID,flPath);
            }
            else if (param == "DownFile")
            {
                string sbID = Request.Form["SBID"];
                string sbName = HttpUtility.UrlDecode(Request.Form["SBName"]);
                
                DownFile(sbID, sbName);
            }
            else if (param == "query")
            {
                string sbID = Request.Form["SBID"];

                QueryData(sbID);
            }
            else if (param == "SaveStatus")
            {
                string sbID = Request.Form["SBnodeKey"];
                string sTime = Request.Form["StartTime"];
                string sType= Request.Form["type"];

                SaveStatus(sbID, sTime, sType);
            }
            else if (param == "queryItem")
            {
                string sbID = Request.Form["SBID"];
                QueryDataByItem(sbID);
            }
            else if (param == "SaveItem")
            {
                //SBnodeKey:$('#NodeCheckedID').val,itemID:$("#DJXID").val(),itemBw:escape($("#DJXBW").val()),itemDesc:escape($("#DJXDESC").val())
                //,itemContent:escape($("#DJXCONTENT").val()) ,itemObserve:escape($("#DJXObserve").val()),itemUnit:escape($("#DJXUnit").val()),itemType:$("#DJXType").val()
                //,itemStatus:$("#DJXSelectStatus").val() ,itemStatusQJ:$("#DJXT_SATAUS").val() ,itemUpper:$("#DJXUpper").val() ,itemLower:$("#DJXLower").val(),itemSpectrum:$("#DJXSpectrum").val()
                //,itemStartTime:$("#DJXSTARTTIME").val() ,itemPerValue:$("#DJXPERIODVALUE").val(),itemPerType:$("#DJXPERIODTYPE").val()

                //string itemID = Request.Form["itemID"];     //点检向标识 
                string nodeKey = Request.Form["SBnodeKey"]; //设备标识
                string itemBw = HttpUtility.UrlDecode(Request.Form["itemBw"]);      //点检项目部位 
                string itemDesc = HttpUtility.UrlDecode(Request.Form["itemDesc"]);  //点检项目描述 
                string itemContent = HttpUtility.UrlDecode(Request.Form["itemContent"]); //点检项内容
                string itemObserve = HttpUtility.UrlDecode(Request.Form["itemObserve"]); //观察名称
                string itemUnit = HttpUtility.UrlDecode(Request.Form["itemUnit"]);       //点检项单位
                string itemType = Request.Form["itemType"];         //点检项类型
                string itemStatus = Request.Form["itemStatus"];     //设备状态
                string itemStatusQJ = Request.Form["itemStatusQJ"];     //启动 停止
                string itemUpper = Request.Form["itemUpper"];     //上限
                string itemLower = Request.Form["itemLower"];     //下限
                string itemSpectrum = Request.Form["itemSpectrum"];     //是否频谱
                string itemStartTime = HttpUtility.UrlDecode(Request.Form["itemStartTime"]);   //开始时间
                string itemPerValue = Request.Form["itemPerValue"];     //周期数值
                string itemPerType = Request.Form["itemPerType"];       //周期类型

                AddItem(nodeKey, itemBw, itemDesc, itemContent, itemObserve, itemUnit, itemType, itemStatus, itemStatusQJ, itemUpper, itemLower, itemSpectrum, itemStartTime, itemPerValue, itemPerType);

            }
            else if (param == "load")
            {
                //页面Load获取设备信息
                string sbID = Request.Form["SBID"];
                LoadInfo(sbID);
            }
            else if (param == "status")
            {
                QueryStatus();
            }
            else if (param == "EditStatus")
            {
                string idkey=Request.Form["IDKEY"];
                string time = HttpUtility.UrlDecode(Request.Form["StartTime"]);
                string type = Request.Form["type"];

                EditStatus(idkey, time, type);
            }
            else if (param == "EditItem")
            {
                //SBnodeKey:$('#NodeCheckedID').val,itemID:$("#DJXID").val(),itemBw:escape($("#DJXBW").val()),itemDesc:escape($("#DJXDESC").val())
                //,itemContent:escape($("#DJXCONTENT").val()) ,itemObserve:escape($("#DJXObserve").val()),itemUnit:escape($("#DJXUnit").val()),itemType:$("#DJXType").val()
                //,itemStatus:$("#DJXSelectStatus").val() ,itemStatusQJ:$("#DJXT_SATAUS").val() ,itemUpper:$("#DJXUpper").val() ,itemLower:$("#DJXLower").val(),itemSpectrum:$("#DJXSpectrum").val()
                //,itemStartTime:$("#DJXSTARTTIME").val() ,itemPerValue:$("#DJXPERIODVALUE").val(),itemPerType:$("#DJXPERIODTYPE").val()

                //string itemID = Request.Form["itemID"];     //点检向标识 
                string idkey = Request.Form["idkey"]; //设备标识
                string itemBw = HttpUtility.UrlDecode(Request.Form["itemBw"]);      //点检项目部位 
                string itemDesc = HttpUtility.UrlDecode(Request.Form["itemDesc"]);  //点检项目描述 
                string itemContent = HttpUtility.UrlDecode(Request.Form["itemContent"]); //点检项内容
                string itemObserve = HttpUtility.UrlDecode(Request.Form["itemObserve"]); //观察名称
                string itemUnit = HttpUtility.UrlDecode(Request.Form["itemUnit"]);       //点检项单位
                string itemType = Request.Form["itemType"];         //点检项类型
                string itemStatus = Request.Form["itemStatus"];     //设备状态
                string itemStatusQJ = Request.Form["itemStatusQJ"];     //启动 停止
                string itemUpper = Request.Form["itemUpper"];     //上限
                string itemLower = Request.Form["itemLower"];     //下限
                string itemSpectrum = Request.Form["itemSpectrum"];     //是否频谱
                string itemStartTime = HttpUtility.UrlDecode(Request.Form["itemStartTime"]);   //开始时间
                string itemPerValue = Request.Form["itemPerValue"];     //周期数值
                string itemPerType = Request.Form["itemPerType"];       //周期类型

                EditItem(idkey, itemBw, itemDesc, itemContent, itemObserve, itemUnit, itemType, itemStatus, itemStatusQJ, itemUpper, itemLower, itemSpectrum, itemStartTime, itemPerValue, itemPerType);

            }
        }

        private void EditStatus(string idkey,string time,string statusid) 
        {
            int count = 0;
            string info="";
           
            bool flag = bmd.EditStatusByIDkey(idkey, time, statusid, out errMsg);

            if (flag == true)
            {
                info = "修改数据成功";
                count = 1;
            }
            else
                info = errMsg;

            obj = new
            {
                count=count,
                info = info
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }

        /// <summary>
        /// 附件下载
        /// </summary>
        /// <param name="sbID"></param>
        /// <param name="flPath"></param>
        private void DownFile(string sbID, string sbName)
        {
            byte[] file = null;

            file = bmd.RetBoolDownFile(sbID, sbName, out errMsg);

            string filename = sbName + ".doc";//这个就是要存放到服务器的文件名   
            
            if (file.Length > 0)
            {
                Response.Buffer = true;
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                //FileStream fs = new FileStream(Server.MapPath("~/LoadDown") + @"/" + filename, FileMode.OpenOrCreate);
                FileStream fs = new FileStream(Server.MapPath("~/LoadDown") + @"/" + filename, FileMode.OpenOrCreate);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(file, 0, file.Length);
                Response.OutputStream.Write(file, 0, file.Length);
                Response.OutputStream.Flush();
                Response.OutputStream.Close();
                Response.Flush();
                Response.End();
            }
        }

        /// <summary>
        /// 附件上传
        /// </summary>
        /// <param name="sbID"></param>
        /// <param name="flPath"></param>
        private void AddFile(string sbID,string flPath)
        { 
            errMsg="";
            int count = 0;
            string info = "";

            FileStream fs = new FileStream(flPath, FileMode.Open, FileAccess.Read); //将图片以文件流的形式进行保存
            BinaryReader br = new BinaryReader(fs);
           
            byte[] imgBytesIn = br.ReadBytes((int)fs.Length);  //将流读入到字节数组中

            bool flag = bmd.RetBoolUpFile(sbID, imgBytesIn, out errMsg);

            if (errMsg == "")
            {
                if (flag == true)
                {
                    info = "附件上传成功!";
                    count = 1;
                }
                else
                {
                    info = "附件上传失败!";
                }
            }
            else
                info = errMsg;

            obj = new
            {
                count=count,
                msg = info
            };

            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }

        /// <summary>
        /// 判断是否存在子节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns>1:设备,0:其他</returns>
        private void GetButton(string treeId)
        {
            int i = -1;

            string file = "";
            string status = "";
            string sbName = "";
           
            DataRow drSB = null;
            System.Collections.Generic.IList<Hashtable> list = null;

            i = bmd.RetCount(treeId);

            string str = bmd.GetDrDevByTopOne(treeId, out errMsg);

            if (str != "")
            {
                string[] res = str.Split(',');
               
                file = res[1];
                status = res[2];
            }

            if (i == 1)
            {
                drSB = bmd.RetdrBySBID(treeId);

                if (drSB != null)
                {
                    sbName = drSB["T_DEVICEDESC"].ToString();
                }
                list = bmd.RetTabStatus();
            }

            obj = new
            {
                count = i,
                sbName = sbName,
                list=list,
                file = file,
                status = status
            };

            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }

        /// <summary>
        /// 初始化Tree结构
        /// </summary>
        private void GetListTree()
        {
            int count = 1;

            dt = bmd.RetTableDevice();

            //设备树结构拼接
            if (dt != null && dt.Rows.Count > 0)
            {
                sb.Append("[");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["T_PARENTID"].ToString() == "0")
                        sb.Append("{id:'" + dt.Rows[i]["T_DEVICEID"] + "',pId:'" + dt.Rows[i]["T_PARENTID"] + "',name:'" + dt.Rows[i]["T_DEVICEDESC"] + "',t:'" + dt.Rows[i]["T_DEVICEDESC"] + "', open:true},");
                    else
                        sb.Append("{id:'" + dt.Rows[i]["T_DEVICEID"] + "',pId:'" + dt.Rows[i]["T_PARENTID"] + "',name:'" + dt.Rows[i]["T_DEVICEDESC"] + "',t:'" + dt.Rows[i]["T_DEVICEDESC"] + "'},");
                }

                resultMenu = sb.ToString().TrimEnd(',') + "]";
            }
            else
            { count = 0; }

            obj = new
            {
                count = count,
                menu = resultMenu
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }

        /// <summary>
        /// 下载按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnXia_Click(object sender, EventArgs e)
        {
            string sbID = "";
            string sbName = "";

            byte[] file = null;
          
            sbID = this.txtDeviceId.Value;
            sbName = HttpUtility.UrlDecode(this.txtDeviceName.Value);

            if (sbID != "" && sbName != "")
            {
                file = bmd.RetBoolDownFile(sbID, sbName, out errMsg);

                if (errMsg == "")
                {
                    string filename = sbName + ".doc";//这个就是要存放到服务器的文件名   

                    if (file.Length > 0)
                    {
                        Response.Buffer = true;
                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("Content-Disposition", "attachment;filename=" + Server.UrlPathEncode(filename));
                        //FileStream fs = new FileStream(Server.MapPath("~/LoadDown") + @"/" + filename, FileMode.OpenOrCreate);
                        FileStream fs = new FileStream(Server.MapPath("~/LoadDown") + @"/" + filename, FileMode.OpenOrCreate);
                        BinaryWriter bw = new BinaryWriter(fs);
                        bw.Write(file, 0, file.Length);
                        Response.OutputStream.Write(file, 0, file.Length);
                        Response.OutputStream.Flush();
                        Response.OutputStream.Close();
                        Response.Flush();
                        Response.End();
                    }
                }else
                {
                    Alert(errMsg);
                }
            }
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        private void QueryData(string sbID)
        {
            DataTable dt = null;
            //int page = Request.Form["page"] != "" ? Convert.ToInt32(Request.Form["page"]) : 0;
            //int size = Request.Form["rows"] != "" ? Convert.ToInt32(Request.Form["rows"]) : 0;
            //string sort = Request.Form["sort"] != "" ? Request.Form["sort"] : "";
            //string order = Request.Form["order"] != "" ? Request.Form["order"] : "";
            //if (page < 1) return;
            //string orderField = sort.Replace("JSON_", "");
            //string strWhere = GetWhere();

            //DataSet dsUser = DataHandler.GetList("TUser", "*", "ID", size, page, false, false, strWhere);
            //int count = bll.GetList(strWhere).Tables[0].Rows.Count;//获取总数
            //string strJSON = JsonHelper.CreateJsonParameters(dsUser.Tables[0], true, count);

            //Response.Write(strJSON);
            //Response.End();
            int page = Request.Form["page"] != "" ? Convert.ToInt32(Request.Form["page"]) : 0;
            int size = Request.Form["rows"] != "" ? Convert.ToInt32(Request.Form["rows"]) : 0;
            string sort = Request.Form["sort"] != "" ? Request.Form["sort"] : "";
            string order = Request.Form["order"] != "" ? Request.Form["order"] : "";
            if (page < 1) return;
            string orderField = sort.Replace("JSON_", "");
            string strWhere = "";// GetWhere();

            dt = bmd.RetTabDevByDevID(sbID, out errMsg);// DBdb2.RunDataSet("select * from T_INFO_DEVICE", out errMsg);// DataHandler.GetList("TUser", "*", "ID", size, page, false, false, strWhere);
            int count = dt.Rows.Count; //bll.GetList(strWhere).Tables[0].Rows.Count;//获取总数
            string strJSON = JsonHelper.CreateJsonParameters(dt, true, count);

            Response.Write(strJSON);
            Response.End();
        }

        /// <summary>
        /// 保存设备状态
        /// </summary>
        /// <param name="sbID"></param>
        /// <param name="sTime"></param>
        /// <param name="sType"></param>
        private void SaveStatus(string sbID,string sTime,string sType)
        {
            string info = "";

            bool flag = bmd.SaveStatus(sbID, sTime, sType,out errMsg);

            if (errMsg == "")
            {
                if (flag == true)
                { 
                   //flag = bmd.RetUpdateDveiceStatus(sbID,sTime, out errMsg);  //是否要传入更改设备的时间　

                   //if (flag == true)
                       info = "设备状态添加成功!";
                   //else
                   //    info = errMsg;
                }
                else
                { info = "设备状态添加失败!"; }
            }
            else { info = errMsg; }

            obj = new
            {
                msg = info
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }

        /// <summary>
        /// 查询点检项信息
        /// </summary>
        private void QueryDataByItem(string sbID)
        {
            DataTable dt = null;
          
            int page = Request.Form["page"] != "" ? Convert.ToInt32(Request.Form["page"]) : 0;
            int size = Request.Form["rows"] != "" ? Convert.ToInt32(Request.Form["rows"]) : 0;
            string sort = Request.Form["sort"] != "" ? Request.Form["sort"] : "";
            string order = Request.Form["order"] != "" ? Request.Form["order"] : "";
            if (page < 1) return;
            string orderField = sort.Replace("item_", "");
            string strWhere = "";// GetWhere();

            dt = bmd.RetTabItemsBySbID(sbID, out errMsg); //bmd.RetTabDevByDevID(sbID, out errMsg);// DBdb2.RunDataSet("select * from T_INFO_DEVICE", out errMsg);// DataHandler.GetList("TUser", "*", "ID", size, page, false, false, strWhere);
            int count = dt.Rows.Count; //bll.GetList(strWhere).Tables[0].Rows.Count;//获取总数
            string strJSON = JsonHelper.CreateJsonParameters(dt, true, count);

            Response.Write(strJSON);
            Response.End();
        }

        /// <summary>
        /// 新增点检项
        /// </summary>
        /// <param name="?"></param>
        private void AddItem(string nodeKey, string itemBw, string itemDesc, string itemContent, string itemObserve, string itemUnit, string itemType, string itemStatus, string itemStatusQJ, string itemUpper, string itemLower, string itemSpectrum, string itemStartTime, string itemPerValue, string itemPerType)
        {
            bool flag = false;

            string info = "";

            //string nodeKey = Request.Form["SBnodeKey"]; //设备标识
            //string itemID = Request.Form["itemID"];     //点检向标识 
            //string itemBw = HttpUtility.UrlDecode(Request.Form["itemBw"]);      //点检项目部位 
            //string itemDesc = HttpUtility.UrlDecode(Request.Form["itemDesc"]);  //点检项目描述 
            //string itemContent = HttpUtility.UrlDecode(Request.Form["itemContent"]); //点检项内容
            //string itemObserve = HttpUtility.UrlDecode(Request.Form["itemObserve"]); //观察名称
            //string itemUnit = HttpUtility.UrlDecode(Request.Form["itemUnit"]);       //点检项单位
            //string itemType = Request.Form["itemType"];         //点检项类型
            //string itemStatus = Request.Form["itemStatus"];     //设备状态
            //string itemStatusQJ = Request.Form["itemStatusQJ"];     //启动 停止
            //string itemUpper = Request.Form["itemUpper"];     //上限
            //string itemLower = Request.Form["itemLower"];     //下限
            //string itemSpectrum = Request.Form["itemSpectrum"];     //是否频谱
            //string itemStartTime = HttpUtility.UrlDecode(Request.Form["itemStartTime"]);   //开始时间
            //string itemPerValue = Request.Form["itemPerValue"];     //周期数值
            //string itemPerType = Request.Form["itemPerType"];       //周期类型

            #region  判断数字
            if (itemUpper != "")
            {
                try { int.Parse(itemUpper); }
                catch { info = "数据上限请输入数字."; }
            }

            if (itemLower != "")
            {
                try { int.Parse(itemLower); }
                catch { info = "数据下限请输入数字."; }
            }

            if (itemPerValue != "")
            {
                try { int.Parse(itemPerValue); }
                catch { info = "周期数值请输入数字."; }
            }
            #endregion

            if (info == "")
            {
                flag = bmd.AddItem(nodeKey, itemBw, itemDesc, itemContent, itemObserve, itemUnit, itemType, itemStatus, itemStatusQJ, itemUpper, itemLower, itemSpectrum, itemStartTime, itemPerValue, itemPerType, out errMsg);

                if (errMsg == "")
                {
                    if (flag == true)
                        info = "新增点检项成功!";
                    else
                        info = "新增点检项失败!";
                }
                else { info = errMsg; }
            }

            obj = new
            {
                msg = info
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }

        /// <summary>
        /// 页面Load
        /// </summary>
        /// <param name="id"></param>
        /// <returns>deviceDesc + ',' + file + ',' + status;</returns>
        private void LoadInfo(string treeId)
        {
            string sbName = "";
            string file="";
            string status="";

            string str = bmd.GetDrDevByTopOne(treeId,out errMsg);

            if (str != "")
            {
                string[] res = str.Split(',');

                sbName = res[0];
                file = res[1];
                status = res[2];
            }

            obj = new
            {
                count = 0,
                sbName = sbName,
                file=file,
                status=status
            };

            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        private void QueryStatus()
        {
            DataTable dt = bmd.RetTabStatus1();
            int count = dt.Rows.Count; 
            string strJSON = JsonHelper.CreateJsonParameters(dt, true, count);

            Response.Write(strJSON);
            Response.End();
        }

        /// <summary>
        /// 新增点检项
        /// </summary>
        /// <param name="?"></param>
        private void EditItem(string idkey, string itemBw, string itemDesc, string itemContent, string itemObserve, string itemUnit, string itemType, string itemStatus, string itemStatusQJ, string itemUpper, string itemLower, string itemSpectrum, string itemStartTime, string itemPerValue, string itemPerType)
        {
            bool flag = false;

            string info = "";

            #region  判断数字
            if (itemUpper != "")
            {
                try { int.Parse(itemUpper); }
                catch { info = "数据上限请输入数字."; }
            }

            if (itemLower != "")
            {
                try { int.Parse(itemLower); }
                catch { info = "数据下限请输入数字."; }
            }

            if (itemPerValue != "")
            {
                try { int.Parse(itemPerValue); }
                catch { info = "周期数值请输入数字."; }
            }
            #endregion

            if (info == "")
            {
                flag = bmd.EditItem(idkey, itemBw, itemDesc, itemContent, itemObserve, itemUnit, itemType, itemStatus, itemStatusQJ, itemUpper, itemLower, itemSpectrum, itemStartTime, itemPerValue, itemPerType, out errMsg);

                if (errMsg == "")
                {
                    if (flag == true)
                        info = "编辑点检项成功!";
                    else
                        info = "编辑点检项失败!";
                }
                else { info = errMsg; }
            }

            obj = new
            {
                msg = info
            };
            string result = JsonConvert.SerializeObject(obj);
            Response.Write(result);
            Response.End();
        }


        /// 弹出JavaScript小窗口
        /// </summary>
        /// <param name="js">窗口信息</param>
        public static void Alert(string message)
        {
            #region
            string js = @"<Script language='JavaScript'>
                    alert('" + message + "');</Script>";
            HttpContext.Current.Response.Write(js);

            #endregion
        }
    }
}
