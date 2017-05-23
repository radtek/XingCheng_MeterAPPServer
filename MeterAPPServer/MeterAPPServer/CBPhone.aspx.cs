using Busines.DAL;
using Busines.IDAO;
using Common.DotNetBean;
using Common.DotNetData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MeterAPPServer
{
    public partial class CBPhone : System.Web.UI.Page
    {
        private RM_UserInfo_IDAO uidal = new RM_UserInfo_Dal();
        public StringBuilder sbList = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (RequestSession.GetSessionUser() == null)
                this.Response.Redirect("Login.aspx");

            initdata();
        }

        private void initdata()
        {
            DataTable dt = uidal.GetOrganizeList();
            if (DataTableHelper.IsExistRows(dt))
            {

                foreach (DataRow dr in dt.Rows)
                {
                    sbList.AppendFormat(" <h2>{0}</h2>", dr["departmentName"].ToString());
                    sbList.Append(" <div class=\"post\">");
                    sbList.Append(GetUserListByDepID(dr["departmentID"].ToString()));
                    sbList.Append(" <div class=\"clear\"></div>");
                    sbList.Append(" </div>");
                }
            }

        }

        private string GetUserListByDepID(string departmentID)
        {
            string searchkey = Request.Form["SearchKey"];
            StringBuilder sb = new StringBuilder();

            DataTable dd = uidal.GetUserListByDepID(departmentID, searchkey);
                
          
            if (DataTableHelper.IsExistRows(dd))
            {
                foreach (DataRow dr in dd.Rows)
                {
                    sb.AppendFormat("<div class=\"post_content\" id=\"U{0}\">", dr["loginId"].ToString());
                    sb.AppendFormat(" <h3><a href=\"tel:{0}\">{1}</a></h3>", dr["telePhoneNO"].ToString(), dr["userName"].ToString());
                    sb.AppendFormat(" <p><a href=\"tel:{0}\">　{0}</a> </p>", dr["telePhoneNO"].ToString());
                    sb.Append(" </div>");
                    sb.Append("  <div class=\"clear\"></div>");

                }
            }

            return sb.ToString();
        }
    }
}