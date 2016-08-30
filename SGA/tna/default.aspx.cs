using DataTier;
using SGA.App_Code;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SGA.tna
{
    public partial class _default : Page
    {
        

        protected void Page_Load(object sender, System.EventArgs e)
        {
            SGACommon.AddPageTitle(this.Page, "Multiple Assessment Landing page", "");
            if (!base.IsPostBack)
            {
                HttpBrowserCapabilities browser = base.Request.Browser;
                SGACommon.SaveBrowserDetails(SGACommon.LoginUserInfo.userId, browser.Type, base.Request.UserAgent, this.Session.SessionID);
                base.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
                this.lblName.Text = "Hi " + SGACommon.GetName() + "!";
                SqlParameter[] sqlParamPermission = new SqlParameter[]
				{
					new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
				};
                DataSet dsPermission = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetPremission", sqlParamPermission);
                bool isTnaTest = false;               
                bool isCMAtest = false;
              
                if (dsPermission != null)
                {
                    if (dsPermission.Tables.Count > 0 && dsPermission.Tables[0].Rows.Count > 0)
                    {                       
                        isTnaTest = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewTnaTest"].ToString());                   
                    
                        isCMAtest = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["takeCMATest"].ToString());
                    }
                }
                dsPermission = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetProfileDetails", sqlParamPermission);
                if (dsPermission != null)
                {
                    if (dsPermission.Tables.Count > 0 && dsPermission.Tables[0].Rows.Count > 0)
                    {
                        int jobLevel = System.Convert.ToInt32(dsPermission.Tables[0].Rows[0]["jobLevel"].ToString());
                        switch (System.Convert.ToInt32(dsPermission.Tables[0].Rows[0]["jobrole"].ToString()))
                        {
                            case 1:
                                this.pnlTNA.Visible = true;
                                this.pnlCMA.Visible = true;
                                break;
                            case 2:
                                this.pnlTNA.Visible = true;
                                this.pnlCMA.Visible = true;
                                break;
                            case 3:
                                this.pnlTNA.Visible = true;
                                this.pnlCMA.Visible = true;                        
                                break;
                        }
                    }
                }
                this.hylTna.CssClass = (isTnaTest ? "btn-go" : "locked");
               
                this.hylCMA.CssClass = (isCMAtest ? "btn-go" : "locked");
                this.hylTna.Text = (isTnaTest ? "GO" : "Currently locked.<br />Want more info?<span>Your access to this assessment is now locked as you have completed it. If you require access to the assessment again, please contact support@comprara.com.au If you are looking for your results, click 'My Results' at the top of the page.</span>");
                
                this.hylCMA.Text = (isCMAtest ? "GO" : "Currently locked.<br />Want more info?<span>Your access to this assessment is now locked as you have completed it. If you require access to the assessment again, please contact support@comprara.com.au If you are looking for your results, click 'My Results' at the top of the page.</span>");
                this.hylTna.ToolTip = (isTnaTest ? "Go" : "Locked");
                
                this.hylCMA.ToolTip = (isCMAtest ? "Go" : "locked");
                this.hylTna.NavigateUrl = (isTnaTest ? "~\\tna\\ssa-assessment-instructions.aspx" : "#");
               
                this.hylCMA.NavigateUrl = (isCMAtest ? "~\\tna\\cma-assessment-instructions.aspx" : "#");

              

            }
        }
    }
}
