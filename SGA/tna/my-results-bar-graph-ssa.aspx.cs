﻿using DataTier;
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
        

        protected bool isSgaResult = false;

        protected bool isTnaResult = false;

        protected bool isPmpResult = false;

        protected bool isDmpResult = false;

        protected bool isNpResult = false;

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
                this.spCMA.Attributes["class"] = (this.isCMAResult ? "" : "lock");
                this.spNegotiation.Attributes["class"] = (this.isNpResult ? "" : "lock");
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
