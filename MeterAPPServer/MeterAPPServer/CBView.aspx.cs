using Busines.DAL;
using Busines.IDAO;
using Common.DotNetBean;
using Common.DotNetData;
using Common.DotNetUI;
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
    public partial class CBView : System.Web.UI.Page
    {
        private RM_UserInfo_IDAO uidal = new RM_UserInfo_Dal();

        // public string Tyear, TMonth, UTotal, UUserNum, UOver, UNever, U0, Urate, Fwater, FNum, FFee;

        public string ItemList = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (RequestSession.GetSessionUser() == null)
                this.Response.Redirect("Login.aspx");


            if (!this.IsPostBack)
            {
                if (string.IsNullOrEmpty(this.DateBegin.Value))
                {
                    this.DateBegin.Value = DateTime.Now.ToString("MM/dd/yyyy");
                    this.DateEnd.Value = DateTime.Now.ToString("MM/dd/yyyy");

                }
            }
        }
        protected void Btn_Search_Click(object sender, EventArgs e)
        {
            SearchData();
        }
        private void SearchData()
        {
            string _dateBegin = this.DateBegin.Value;
            string _dateEnd = this.DateEnd.Value;

            if (string.IsNullOrEmpty(_dateBegin) || string.IsNullOrEmpty(_dateEnd))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "", "<script language=javascript>layer.msg('抄表时间不能为空！');</script>");
                return;
            }
            if (this.SearchKey.Value.Length < 1)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "", "<script language=javascript>layer.msg('请输入查询关键词！');</script>");
                return;
            }
            DateTime dtBegin = DateTime.ParseExact(_dateBegin, "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);
            DateTime dtEnd = DateTime.ParseExact(_dateEnd, "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentCulture);

            if (DateTime.Compare(dtBegin, dtEnd) > 0)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "", "<script language=javascript>layer.msg('查询起止时间错误！');</script>");
                return;
            }

            DataTable dt = uidal.GetRecordInfoSearch(_dateBegin, _dateEnd, this.SearchKey.Value);
            if (DataTableHelper.IsExistRows(dt))
            {
                StringBuilder sbItem = new StringBuilder();
                sbItem.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    sbItem.Append(GetUserInfos(dr));
                }
                ItemList = sbItem.ToString();
            }

        }

        private string GetUserInfos(DataRow dr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul class=\"rounded\">");
            sb.AppendFormat(" <li>用户名：{0}</li>", dr["waterUserName"].ToString());
            sb.AppendFormat("<li>用户号：{0}</li>", dr["waterUserNO"].ToString());
            sb.AppendFormat("<li>用水性质：{0} </li>", dr["waterUserTypeName"].ToString());
            sb.AppendFormat(" <li>抄表区域：{0}</li>", dr["areaName"].ToString());
            sb.AppendFormat(" <li>表本号：{0} </li>", dr["meterReadingNO"].ToString());
            sb.AppendFormat(" <li>电话：{0} </li>", dr["waterPhone"].ToString());
            sb.AppendFormat(" <li>地址：{0}</li>", dr["waterUserAddress"].ToString());

            string _dt = dr["readMeterRecordYearAndMonth"].ToString();
            DateTime dtMonth;
            if (DateTime.TryParse(_dt, out dtMonth))
            {
                _dt = dtMonth.ToString("yyyy年M月");
            }
            sb.AppendFormat(" <li>水费月份：{0}</li>", _dt);
            sb.AppendFormat("<li>上期读数：{0}</li>", dr["waterMeterLastNumber"].ToString());
            sb.AppendFormat("<li>本期读数：{0}</li>", dr["waterMeterEndNumber"].ToString());
            sb.AppendFormat(" <li>本期水量：{0}</li>", dr["totalNumber"].ToString());
            sb.AppendFormat(" <li>本期水费：{0}</li>", dr["totalCharge"].ToString());
            sb.AppendFormat(" <li>抄表状态：{0}</li>", GetChargeState(dr["chargeState"].ToString()));
            sb.AppendFormat("<li>抄表员：{0}</li>", dr["USERNAME"].ToString());
            sb.AppendFormat(" <li>抄表时间：{0}</li>", dr["checkDateTime"].ToString());
            sb.Append("</ul>");
            sb.Append(" <img src=\"images/shadow.jpg\" border=\"0\" class=\"shadow\" />");
            return sb.ToString();
        }

        private string GetChargeState(string chargestate)
        {
            string states = "";
            switch (chargestate)
            {
                case "0":
                    states = "未抄表";
                    break;
                case "1":
                    states = "已抄表";
                    break;
                case "2":
                    states = "尾欠";
                    break;
                case "3":
                    states = "已收费";
                    break;
                default:
                    states = "未抄表";
                    break;
            }
            return states;
        }


    }
}