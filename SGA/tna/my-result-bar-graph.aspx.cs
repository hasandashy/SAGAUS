﻿using DataTier;
using SGA.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.tna
{
    public partial class my_result_bar_graph : System.Web.UI.Page
    {
        protected bool isPkeResult = false;

        protected bool isTnaResult = false;

        protected bool isCaaResult = false;

        protected bool isCmkResult = false;     

        protected bool isCMAResult = false;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            SGACommon.IsViewResult("viewPKEResult");
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
                    this.isCmkResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewCmkPResult"].ToString());                   
                    this.isCMAResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewCmaResult"].ToString());
                }
            }
            this.spCategory.Attributes["class"] = (this.isPkeResult ? "" : "lock");
            this.spSkills.Attributes["class"] = (this.isTnaResult ? "" : "lock");
            this.spBehaviour.Attributes["class"] = (this.isCmkResult ? "" : "lock");
            this.spNegotiation.Attributes["class"] = (this.isCaaResult ? "" : "lock");
            this.spCMA.Attributes["class"] = (this.isCMAResult ? "" : "lock");
         

            if (!base.IsPostBack)
            {
                base.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
                if (this.Session["sgaTestId"] != null)
                {
                    SqlParameter[] param = new SqlParameter[]
					{
						new SqlParameter("@userId", SGACommon.LoginUserInfo.userId),
						new SqlParameter("@testId", this.Session["sgaTestId"].ToString())
					};
                    this.lblPercentage.Text = System.Convert.ToDecimal(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetPrecentage", param)).ToString("#.##") + " %";
                    this.graph1.testId = System.Convert.ToInt32(this.Session["sgaTestId"].ToString());
                }
                else
                {
                    base.Response.Redirect("my-results-reports.aspx", false);
                }
            }
        }

        protected void lnkLower_Click(object sender, System.EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            this.graph1.CompareResult(System.Convert.ToInt32(lnk.CommandArgument));
            this.lnkLower.Attributes["class"] = "";
            this.lnkMiddle.Attributes["class"] = "";
            this.lnkUpper.Attributes["class"] = "";
            if (lnk != null)
            {
                lnk.Attributes["class"] = "active";
            }
        }
    }
}