using Busines.DAL;
using Busines.IDAO;
using Common.DotNetBean;
using Common.DotNetData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MeterAPPServer
{
    public partial class Main : System.Web.UI.Page
    {
        private RM_UserInfo_IDAO uidal = new RM_UserInfo_Dal();

        public string Tyear, TMonth, UTotal, UUserNum, UOver, UNever, U0, Urate, Fwater, FNum, FFee;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (RequestSession.GetSessionUser() == null)
                this.Response.Redirect("Login.aspx");

            if (!this.IsPostBack)
            {
                initdata();
            }
        }

        private void initdata()
        {
            DataTable dt = uidal.GetMonthTJ();
            if (DataTableHelper.IsExistRows(dt))
            {
                DataRow dr = dt.Rows[0];
                Tyear = dr["Tyear"].ToString();
                TMonth = dr["TMonth"].ToString();
                UTotal = dr["UTotal"].ToString();
                UUserNum = dr["UUserNum"].ToString();
                UOver = dr["UOver"].ToString();
               // UNever = dr["UNever"].ToString();
                int _Utotal = 0;
                int _UOver = 0;
                if (int.TryParse(dr["UTotal"].ToString(), out _Utotal) && int.TryParse(dr["UOver"].ToString(), out _UOver))
                {
                    UNever = (_Utotal - _UOver).ToString();
                    Urate = Math.Round(Convert.ToDecimal(_UOver) * 100 / Convert.ToDecimal(_Utotal), 1).ToString();
                }
                U0 = dr["U0"].ToString();
                Fwater = dr["Fwater"].ToString();
                FNum = dr["FNum"].ToString();
                FFee = dr["FFee"].ToString();
            }
        }
    }
}