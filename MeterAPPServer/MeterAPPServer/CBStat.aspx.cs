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
    public partial class CBStat : System.Web.UI.Page
    {
        private RM_UserInfo_IDAO uidal = new RM_UserInfo_Dal();

        public string Tyear, TMonth, UTotal, UUserNum, UOver, UNever, U0, Urate, Fwater, FNum, FFee;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (RequestSession.GetSessionUser() == null)
                this.Response.Redirect("Login.aspx");


            if (!this.IsPostBack)
            {
                BindArea();
                 initdata("0000");
            }
        }

        private void BindArea()
        {
            DataTable dt = uidal.GetArea();
            this.Area.DataSource = dt;
            this.Area.DataTextField = "areaName";
            this.Area.DataValueField = "areaId";
            this.Area.DataBind();
        }
        protected void Area_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CBUser.Items.Clear();
            this.ReadingNO.Items.Clear();
            if (!Area.SelectedValue.Equals("0000"))
            {
                DataTable dt = uidal.GetCBUserByAreaID(Area.SelectedValue);
                this.CBUser.DataSource = dt;
                this.CBUser.DataValueField = "LOGINID";
                this.CBUser.DataTextField = "USERNAME";
                this.CBUser.DataBind();
            }
            this.CBUser.Items.Insert(0, new ListItem("-----全部-----", ""));
            this.ReadingNO.Items.Insert(0, new ListItem("-----全部-----", ""));
        }
        protected void CBUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ReadingNO.Items.Clear();
            if (!string.IsNullOrEmpty(this.CBUser.SelectedValue))
            {
                DataTable dt = uidal.GetReadingNOByUser(CBUser.SelectedValue);
                this.ReadingNO.DataSource = dt;
                this.ReadingNO.DataValueField = "meterReadingID";
                this.ReadingNO.DataTextField = "meterReadingNO";
                this.ReadingNO.DataBind();
            }
            this.ReadingNO.Items.Insert(0, new ListItem("-----全部-----", ""));
        }
        protected void Btn_Search_Click(object sender, EventArgs e)
        {
            string _Areaid = this.Area.SelectedValue;
            if (!string.IsNullOrEmpty(_Areaid))
            {
                initdata(_Areaid);
            }
        }
        private void initdata(string areaId)
        {
            areaId = areaId.PadLeft(4, '0');
            string sw = string.Empty;

            sw = areaId.Equals("0000") ? "" : string.IsNullOrEmpty(CBUser.SelectedValue) ? string.Format(" And areaId='{0}'", areaId) : string.IsNullOrEmpty(ReadingNO.SelectedValue) ? string.Format(" And areaId='{0}' And loginId='{1}'", areaId, this.CBUser.SelectedValue) : string.Format(" And areaId='{0}' And loginId='{1}' And meterReadingID='{2}'", areaId, this.CBUser.SelectedValue, this.ReadingNO.SelectedValue);


          //  string _searchKey = string.IsNullOrEmpty(SearchKey.Value.Trim()) ? null : SearchKey.Value.Trim();

            DataTable dt = uidal.GetMeterStat(sw);
            if (DataTableHelper.IsExistRows(dt))
            {
                DataRow dr = dt.Rows[0];
                Tyear = dr["Tyear"].ToString();
                TMonth = dr["TMonth"].ToString();
                UTotal = string.IsNullOrEmpty(dr["UTotal"].ToString()) ? "0" : dr["UTotal"].ToString();
                UUserNum = string.IsNullOrEmpty(dr["UUserNum"].ToString()) ? "0" : dr["UUserNum"].ToString();
                UOver = string.IsNullOrEmpty(dr["UOver"].ToString()) ? "0" : dr["UOver"].ToString();
                // UNever = dr["UNever"].ToString();
                UNever = (Convert.ToInt32(UTotal) - Convert.ToInt32(UOver)).ToString();
                U0 = dr["U0"].ToString();
                Urate = UTotal == "0" ? "0" : Math.Round(Convert.ToDecimal(UOver) * 100 / Convert.ToDecimal(UTotal), 1).ToString();
                Fwater = dr["Fwater"].ToString();
                FNum = dr["FNum"].ToString();
                FFee = dr["FFee"].ToString();
            }
        }


    }
}