using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Collections;


namespace DJXT.DataFile
{
    public partial class GetBenchmarkReference : System.Web.UI.Page
    {
        public string errMsg = "", Benchmark = "", Benchmark_update="";
        StringBuilder sb = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            Benchmark = Request["Benchmark"];Benchmark_update = Request["Benchmark_update"];
            if ((Benchmark != "") && (Benchmark != null))
            {
                BLL.BLLBenchmarkReference BB = new BLL.BLLBenchmarkReference();

                DataSet DS = BB.GetBoilerData(Benchmark);

                if (DS.Tables[0].Rows.Count > 0)
                {
                    string[] str = new string[23] { "T0_t_el_B", "P0_t_el_B", "Trh_el_B", "PLrh_el_B", "Pdp_el_B", "Dgrjw_el_B", "Dzrjw_el_B", "Dpw_el_B", "Del_BtaT_gl_el_B", "Dtur_el_B", "O2_el_B", "Tpy_el_B", "Alpha_bs_el_B", "Tfw_el_B", "Eta_H_el_B", "Eta_M_el_B", "Theta_1_el_B", "Theta_2_el_B", "Theta_3_el_B", "Theta_5_el_B", "Theta_6_el_B", "Theta_7_el_B", "Theta_8_el_B" };
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        if (str[i] == DS.Tables[0].Rows[i][0].ToString())
                        {
                            str[i] = DS.Tables[0].Rows[i][1].ToString();
                            //sb.Append(DS.Tables[0].Rows[i][0].ToString() + ",");
                        }
                    }
                    for (int j = 0; j < DS.Tables[0].Rows.Count; j++)
                    {
                        sb.Append(str[j] + ",");
                    }
                        sb.ToString().TrimEnd(',');
                }
            }
            else if ((Benchmark_update != "") && (Benchmark_update != null))
            {
                BLL.BLLBenchmarkReference BB = new BLL.BLLBenchmarkReference();
                bool flag = BB.updateBoiler(Benchmark_update);

            }
            Response.Clear();
            Response.Write(sb.ToString());
            Response.End();  
        }
    }
}