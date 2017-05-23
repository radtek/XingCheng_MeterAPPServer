using Busines.DAL;
using Busines.IDAO;
using Common.DotNetBean;
using Common.DotNetCode;
using Common.DotNetJson;
using Common.DotNetUI;
using System;
using System.Data;
using System.Web;
using System.Web.SessionState;

namespace Zhaopin.Frame
{
    /// <summary>
    /// Frame 的摘要说明
    /// </summary>
    public class Frame : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1.0);
            context.Response.AddHeader("pragma", "no-cache");
            context.Response.AddHeader("cache-control", "");
            context.Response.CacheControl = "no-cache";
            string Action = context.Request["action"];
            string username = context.Request["username"];
            string password = context.Request["password"];
            string code = context.Request["code"];
            RM_UserInfo_IDAO user_idao = new RM_UserInfo_Dal();
            string text = Action;
            if (text != null)
            {
                if (text == "login")
                {
                    DataTable dtlogin = user_idao.UserLogin(username.Trim(), password.Trim());
                    if (dtlogin != null)
                    {
                        if (dtlogin.Rows.Count != 0)
                        {
                            if (this.Islogin(context, username))
                            {
                                RequestSession.AddSessionUser(new SessionUser
                                {
                                    UserId = dtlogin.Rows[0]["loginId"].ToString(),
                                    UserAccount = dtlogin.Rows[0]["loginName"].ToString(),
                                    UserName = dtlogin.Rows[0]["userName"].ToString() + "(" + dtlogin.Rows[0]["loginName"].ToString() + ")",
                                    UserPwd = dtlogin.Rows[0]["loginPassword"].ToString(),
                                    AreaID="",
                                    OrganizationID = dtlogin.Rows[0]["departmentID"].ToString(),
                                });
                                context.Response.Write("3");
                                context.Response.End();
                            }
                            else
                            {
                                context.Response.Write("6");
                                context.Response.End();
                            }
                        }
                        else
                        {
                            context.Response.Write("4");
                            context.Response.End();
                        }
                    }
                    else
                    {
                        context.Response.Write("5");
                        context.Response.End();
                    }
                }
            }
        }

        public bool Islogin(HttpContext context, string User_Account)
        {
            return true;
        }
    }
}