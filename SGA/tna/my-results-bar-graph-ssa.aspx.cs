using DataTier;
using SGA.App_Code;
using SGA.controls;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace SGA.tna
{
    public partial class my_results_bar_graph_ssa : Page
    {


        protected bool isPkeResult = false;

        protected bool isTnaResult = false;

        protected bool isCaaResult = false;

        protected bool isCmkResult = false;

        protected bool isCMAResult = false;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            SGACommon.IsViewResult("viewTNAResult");
            if (!base.IsPostBack)
            {
                DataSet dsPermission = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetPremission", new SqlParameter[]
				{
					new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
				});
                if (dsPermission != null)
                {
                    if (dsPermission.Tables.Count > 0 && dsPermission.Tables[0].Rows.Count > 0)
                    {
                        this.isPkeResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewPkeResult"].ToString());
                        this.isTnaResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewTnaResult"].ToString());
                        this.isCaaResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewCaaResult"].ToString());
                        this.isCmkResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewCmkResult"].ToString());
                        this.isCMAResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewCmaResult"].ToString());
                    }
                }
                this.spSkills.Attributes["class"] = (this.isTnaResult ? "" : "lock");
                this.spCMA.Attributes["class"] = (this.isCMAResult ? "" : "lock");
                this.spCMK.Attributes["class"] = (this.isCmkResult ? "" : "lock");
                this.spPKE.Attributes["class"] = (this.isPkeResult ? "" : "lock");

                base.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
                if (this.Session["ssaTestId"] != null)
                {
                    SqlParameter[] param = new SqlParameter[]
					{
						new SqlParameter("@userId", SGACommon.LoginUserInfo.userId),
						new SqlParameter("@testId", this.Session["ssaTestId"].ToString())
					};
                    this.graph1.testId = System.Convert.ToInt32(this.Session["ssaTestId"].ToString());
                }
                else
                {
                    base.Response.Redirect("my-results-reports-ssa.aspx", false);
                }
            }
        }
    }
}
