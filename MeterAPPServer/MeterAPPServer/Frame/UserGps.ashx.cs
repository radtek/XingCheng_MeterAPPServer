using Busines.DAL;
using Busines.IDAO;
using Common.DotNetJson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MeterAPPServer.Frame
{
    /// <summary>
    /// UserGps 的摘要说明
    /// </summary>
    public class UserGps : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1.0);
            context.Response.AddHeader("pragma", "no-cache");
            context.Response.AddHeader("cache-control", "");
            context.Response.CacheControl = "no-cache";
            string DepID = context.Request["Sector"];
            string username = context.Request["username"];
            string StartTime = context.Request["onLine"];
            RM_UserInfo_IDAO user_idao = new RM_UserInfo_Dal();


            if (DepID == null)
            {
                DataTable dtGPS = user_idao.GetUserGPS();
                if (dtGPS != null)
                    {
                        if (dtGPS.Rows.Count != 0)
                        {
                            string Con = JsonHelper.DataTableToJson(dtGPS);
                            context.Response.Write(Con);
                           context.Response.End();
                        }
                        else
                        {
                            context.Response.Write("");
                            context.Response.End();
                        }
                    }
                    else
                    {
                        context.Response.Write("");
                        context.Response.End();
                    }
            }

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