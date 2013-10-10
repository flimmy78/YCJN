using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Entity.Statistic;
using Newtonsoft.Json;
using BLL.StatisticalComparison;

namespace DJXT.StatisticalComparison
{
    public partial class IndicatorSearch : System.Web.UI.Page
    {
        BLLBase bb = new BLLBase();
        BLLIndicatorSearch bi = new BLLIndicatorSearch();
        string errMsg = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string param = Request["param"];
            if (param != "")
            {
                if (param == "seachList")
                {
                    GetInfo();

                }
            }
            if (!IsPostBack)
            {
                BindCompany();
                //Bind();
            }
        }

         //根据不同类型的机组获取信息
        public void GetInfo()
        {
            string companyId = String.IsNullOrEmpty(Request["companyId"]) ? string.Empty : Request["companyId"].ToString();
            string plantId = String.IsNullOrEmpty(Request["plantId"]) ? string.Empty : Request["plantId"].ToString();
            string unit = String.IsNullOrEmpty(Request["unit"]) ? string.Empty : Request["unit"].ToString();
            string beginTime = String.IsNullOrEmpty(Request["BeginTime"]) ? string.Empty : Request["BeginTime"].ToString();
            string endTime = String.IsNullOrEmpty(Request["EndTime"]) ? string.Empty : Request["EndTime"].ToString();
            List<IndicatorInfo> infoList = new List<IndicatorInfo>();
            List<IndicatorInfo> saveList = new List<IndicatorInfo>();

            //获取汽机和锅炉的所有耗差类型。
            infoList = bi.GetInfo(beginTime, endTime,companyId,plantId, unit, 1, -1, out errMsg);
            //上线启用
            IndicatorInfo infos;
            for (int i = 0; i < infoList.Count; i++)
            {
                infos = new IndicatorInfo();
                if (!string.IsNullOrEmpty(infoList[i].Name))
                {
                    infos.Name = infoList[i].Name;
                    infos.StandardValue = Math.Round(infoList[i].StandardValue, 2);
                    infos.RealValue = Math.Round(infoList[i].RealValue, 2);
                    infos.ConsumeValue = Math.Round(infoList[i].ConsumeValue, 2);
                    infos.Unit = infoList[i].Unit;
                    saveList.Add(infos);
                }
            }

            int count = saveList.Count;
            object obj = new
            {
                total = count,
                rows = saveList
            };

            string result = JsonConvert.SerializeObject(obj);
            //Response.ContentType = "text/json;charset=utf-8;";
            Response.Write(result);
            Response.End();
        }
        protected void BindCompany()
        {
            ddlCompany.DataSource = bb.GetCompany(out errMsg);
            ddlCompany.DataTextField = "T_COMPANYDESC";
            ddlCompany.DataValueField = "T_COMPANYID";
            ddlCompany.DataBind();
            ddlCompany.Items.Add(new ListItem { Value = "0", Text = "--请选择省公司--", Selected = true });
        }

        protected void BindPlant()
        {
            ddlPlant.DataSource = bb.GetPlant(ddlCompany.SelectedValue, out errMsg);
            ddlPlant.DataTextField = "T_PLANTDESC";
            ddlPlant.DataValueField = "T_PLANTID";
            ddlPlant.DataBind();
            ddlPlant.Items.Add(new ListItem { Value = "0", Text = "--请选择电厂--", Selected = true });
        }

        protected void BindUnit()
        {
            ddlUnit.DataSource = bb.GetUnit(ddlPlant.SelectedValue, out errMsg);
            ddlUnit.DataTextField = "T_UNITDESC";
            ddlUnit.DataValueField = "T_UNITID";
            ddlUnit.DataBind();
            ddlUnit.Items.Add(new ListItem { Value = "0", Text = "--请选择机组--", Selected = true });
        }

        protected void ddlCompany_SelectedChanged(object sender, EventArgs e)
        {
            BindPlant();
        }

        protected void ddlPlant_SelectedChanged(object sender, EventArgs e)
        {
            BindUnit();
        }
        protected void Search_Click(object sender, EventArgs e)
        {
            //Bind();
        }
    }
}