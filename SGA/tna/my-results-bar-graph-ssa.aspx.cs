﻿using DataTier;
using SGA.App_Code;
using SGA.controls;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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

        protected bool isCAAComplete = false; protected bool isCMAComplete = false; protected bool isPKEComplete = false; protected bool isTNAComplete = false; protected bool isCMKComplete = false;

        protected bool isResultLocked = true;protected bool isContractPack = true;

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
                        this.isCAAComplete = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["isCaaComplete"].ToString());
                        this.isResultLocked = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["isResultLocked"].ToString());
                        this.isCMAComplete = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["isCmaComplete"].ToString());
                        this.isCMKComplete = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["isCmkComplete"].ToString());
                        this.isTNAComplete = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["isTnaComplete"].ToString());
                        this.isPKEComplete = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["isPkeComplete"].ToString());

                    }
                }

                if (!isCMAComplete)
                {
                    this.acrdcma.Visible = false;
                }
                if (!isCMKComplete)
                {
                    this.acrdcmk.Visible = false;
                }
                if (!isTNAComplete)
                {
                    this.acrdtna.Visible = false;
                }
                if (!isPKEComplete)
                {
                    this.acrdpke.Visible = false;
                }
                if (!isCAAComplete)
                {
                    this.acrdcaa.Visible = false;
                }
                if (isResultLocked)
                {
                    reportDiv.Visible = false;
                }
                int jobRole = System.Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, "select jobRole from tblusers where Id=" + SGACommon.LoginUserInfo.userId));
                int[] arr = new int[] { 5, 6, 7, 8 };
                if (arr.Contains(jobRole))
                {
                   this.isTnaResult = false;
                   this.isContractPack = false;

                    spPKE.InnerHtml = "Procurement Technical Assessments";
                    spCMK.InnerHtml = "Contract Management Assessments";
                }
                this.spSkills.Attributes["class"] = (this.isTnaResult ? "" : "lock");
                this.spCMA.Attributes["class"] = (this.isCMAResult ? "" : "lock");
                this.spCMK.Attributes["class"] = (this.isCmkResult ? "" : "lock");
                this.spPKE.Attributes["class"] = (this.isPkeResult ? "" : "lock");
                this.spCaa.Attributes["class"] = (this.isCaaResult ? "" : "lock");

                base.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
                if (this.Session["ssaTestId"] != null)
                {
                    //int jobRole = System.Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, "select jobRole from tblusers where Id=" + SGACommon.LoginUserInfo.userId));
                    int[] arrRoles = new int[] { 5, 6, 7, 8 };
                    if (arrRoles.Contains(jobRole))
                    {
                        base.Response.Redirect("my-results-reports-ssa.aspx", false);
                    }
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

                //Report Link

       //         SqlParameter[] paramPack = new SqlParameter[]
       //{
       //         new SqlParameter("@userId", SqlDbType.Int)
       //};
       //         paramPack[0].Value = SGACommon.LoginUserInfo.userId;
       //         DataSet dsPacks = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetReportIdByUserId", paramPack);
       //         if (dsPacks != null)
       //         {
       //             if (dsPacks.Tables.Count > 0 && dsPacks.Tables[0].Rows.Count > 0)
       //             {
       //                 if (dsPacks.Tables[0].Rows[0]["packId"].ToString() == "6")
       //                 {
       //                     cmalink.HRef = "/IndividualReport/ContractManagement.aspx?id=" + dsPacks.Tables[0].Rows[0]["reportId"].ToString();
       //                 }
       //                 else
       //                 {
       //                     procurelink.HRef = "/IndividualReport/Procurement.aspx?id=" + dsPacks.Tables[0].Rows[0]["reportId"].ToString();
       //                 }
       //             }
       //         }
            }
        }
    }
}
