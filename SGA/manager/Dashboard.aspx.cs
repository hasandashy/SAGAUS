using DataTier;
using SGA.App_Code;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.manager
{
    public partial class Dashboard : Page
    {
        
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!base.IsPostBack)
            {
                HttpBrowserCapabilities browser = base.Request.Browser;
                SGACommon.SaveBrowserDetails(SGACommon.LoginUserInfo.userId, browser.Type, base.Request.UserAgent, this.Session.SessionID);
                this.lblName.Text = "Hi " + SGACommon.GetName() + "!";
                this.lblCompanyName.Text = SGACommon.GetCompanyName();
                this.BindTests();
            }
        }

        private void BindTests()
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetCompanyTests", new SqlParameter[]
			{
				new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
			});
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    this.lblCMCCount.Text = ds.Tables[0].Rows[0]["cmcTest"].ToString();
                    this.lblSSACount.Text = ds.Tables[0].Rows[0]["ssaTest"].ToString();
                    this.lblBACount.Text = ds.Tables[0].Rows[0]["baTest"].ToString();
                    this.lblNPCount.Text = ds.Tables[0].Rows[0]["npTest"].ToString();
                    this.lblMPCount.Text = ds.Tables[0].Rows[0]["mpTest"].ToString();
                    this.lblCMACount.Text = ds.Tables[0].Rows[0]["cmaTest"].ToString();
                }
            }
        }
    }
}
