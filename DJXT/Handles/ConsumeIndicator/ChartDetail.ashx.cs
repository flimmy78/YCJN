using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DJXT.Handles.ConsumeIndicator
{
    /// <summary>
    /// ChartDetail 的摘要说明
    /// </summary>
    public class ChartDetail : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}