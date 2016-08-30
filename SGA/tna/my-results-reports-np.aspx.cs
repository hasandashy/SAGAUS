using DataTier;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using SGA.App_Code;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SGA.tna
{
    public partial class my_results_reports_np : System.Web.UI.Page
    {
        protected bool isSgaResult = false;

        protected bool isTnaResult = false;

        protected bool isPmpResult = false;

        protected bool isDmpResult = false;

        protected bool isNpResult = false;

        protected bool isCMAResult = false;

        public int pgNum
        {
            get
            {
                int result;
                if (this.ViewState["PgNum"] != null)
                {
                    result = System.Convert.ToInt32(this.ViewState["PgNum"]);
                }
                else
                {
                    result = 0;
                }
                return result;
            }
            set
            {
                this.ViewState["PgNum"] = value;
            }
        }

        public int pgNumPd
        {
            get
            {
                int result;
                if (this.ViewState["pgNumPd"] != null)
                {
                    result = System.Convert.ToInt32(this.ViewState["pgNumPd"]);
                }
                else
                {
                    result = 0;
                }
                return result;
            }
            set
            {
                this.ViewState["pgNumPd"] = value;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            SGACommon.AddPageTitle(this.Page, "Negotiation Profile Result Page", "");
            SGACommon.IsViewResult("viewNPResult");
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
                        this.isSgaResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewSgaResult"].ToString());
                        this.isTnaResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewTnaResult"].ToString());
                        this.isPmpResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewPMPResult"].ToString());
                        this.isDmpResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewDMPResult"].ToString());
                        this.isNpResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewNPResult"].ToString());
                        this.isCMAResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewCMAResult"].ToString());
                    }
                }
                this.spSkills.Attributes["class"] = (this.isTnaResult ? "" : "lock");
                this.spBehaviour.Attributes["class"] = (this.isPmpResult ? "" : "lock");
                this.spNegotiation.Attributes["class"] = (this.isNpResult ? "" : "lock");
                this.spCMA.Attributes["class"] = (this.isCMAResult ? "" : "lock");

                this.BindResults();
                base.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
                base.ClientScript.RegisterStartupScript(this.Page.GetType(), "abc", "$(document).ready(function(){\r\nStyleRadio();\r\n});", true);
            }
        }

        private void BindResults()
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetNPTests", new SqlParameter[]
			{
				new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
			});
            int cnt = ds.Tables[0].Rows.Count;
            PagedDataSource paged = new PagedDataSource();
            paged.DataSource = ds.Tables[0].DefaultView;
            paged.AllowPaging = true;
            paged.PageSize = 10;
            paged.CurrentPageIndex = this.pgNumPd;
            this.ViewState["pgNumPd"] = this.pgNumPd;
            int vcnt = cnt / paged.PageSize;
            this.btnprev.Visible = !paged.IsFirstPage;
            this.btnnext.Visible = !paged.IsLastPage;
            this.rptSgaTest.DataSource = paged;
            this.rptSgaTest.DataBind();
        }

        protected void btnprev_Click(object sender, System.EventArgs e)
        {
            this.pgNumPd--;
            this.BindResults();
            base.ClientScript.RegisterStartupScript(this.Page.GetType(), "abc", "$(document).ready(function(){\r\nStyleRadio();\r\n});", true);
        }

        protected void btnnext_Click(object sender, System.EventArgs e)
        {
            this.pgNumPd++;
            this.BindResults();
            base.ClientScript.RegisterStartupScript(this.Page.GetType(), "abc", "$(document).ready(function(){\r\nStyleRadio();\r\n});", true);
        }

        protected void rptSgaTest_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label lblConvertedDate = (Label)e.Item.FindControl("lblConvertedDate");
                System.DateTime dtTestdate = System.Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "testDate"));
                if (lblConvertedDate != null)
                {
                    lblConvertedDate.Text = SGACommon.ToAusTimeZone(dtTestdate).ToString("dd/MM/yyyy HH:mm tt");
                }
            }
        }
        protected void rptSgaTest_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "bar")
            {
                this.Session["npTestId"] = e.CommandArgument;
                base.Response.Redirect("my-results-bar-graph-np.aspx", false);
            }
        }
    }
}