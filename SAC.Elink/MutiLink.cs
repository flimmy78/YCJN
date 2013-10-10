using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using SAC.Helper;

namespace SAC.Elink
{
    public class MutiLink
    {
        Elink ek = new Elink();

        /// <summary>
        /// 根据测点和时间求平均值
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="type">1,2,3</param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string RetPointValueAvg(string tag, int type, string date)
        {
            int ret = -1;
            double value = 0;
            string mon = "";
            string res = "";

            ArrayList list = new ArrayList();

            if (type == 1)
            {
                list.Add(date + " 00:59:59");
                list.Add(date + " 01:59:59");
                list.Add(date + " 02:59:59");
                list.Add(date + " 03:59:59");
                list.Add(date + " 04:59:59");
                list.Add(date + " 05:59:59");
                list.Add(date + " 06:59:59");
                list.Add(date + " 07:59:59");
            }
            else if (type == 2)
            {
                list.Add(date + " 08:59:59");
                list.Add(date + " 09:59:59");
                list.Add(date + " 10:59:59");
                list.Add(date + " 11:59:59");
                list.Add(date + " 12:59:59");
                list.Add(date + " 13:59:59");
                list.Add(date + " 14:59:59");
                list.Add(date + " 15:59:59");
            }
            else if (type == 3)
            {
                list.Add(date + " 16:59:59");
                list.Add(date + " 17:59:59");
                list.Add(date + " 18:59:59");
                list.Add(date + " 19:59:59");
                list.Add(date + " 20:59:59");
                list.Add(date + " 21:59:59");
                list.Add(date + " 22:59:59");
                list.Add(date + " 23:59:59");
            }
            else
            { }

            if (list.Count > 0)
            {

                for (int i = 0; i < list.Count; i++)
                {
                    ek.GetHisValue(tag, list[i].ToString(), ref ret, ref value);

                    mon += "(" + value + ")+";
                }

                mon = "(" + mon.TrimEnd('+') + ")/" + list.Count.ToString();

                res = StrHelper.Cale(mon);
            }
            else { res = "0"; }

            return res;
        }

        /// <summary>
        /// 计算差值
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string RetPointDiffValue(string tag, string bt, string et)
        {
            int ret = -1;
            double value = 0;

            if (tag != "")
            {
                if (Convert.ToDateTime(et) > DateTime.Now)
                    value = 0;
                else
                    ek.GetHisDiffValue(tag, bt, et, ref ret, ref value);
            }
            
            return value.ToString();
        }
    }
}
