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
        protected bool isTnaResult = false;

        protected bool isPkeResult = false;

        protected bool isCaaResult = false;

        protected bool isCmkResult = false;

        protected bool isCMAResult = false;

        protected bool isCAAComplete = false; protected bool isCMAComplete = false; protected bool isPKEComplete = false; protected bool isTNAComplete = false; protected bool isCMKComplete = false;

        protected bool isResultLocked = true;protected bool isContractPack = true;

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
                    this.isCMAResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewCmaResult"].ToString());
                    this.isCmkResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewCmkResult"].ToString());
                    this.isCaaResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewCaaResult"].ToString());
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
                    //this.lblPercentage.Text = System.Convert.ToDecimal(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetPrecentage", param)).ToString("#.##") + " %";
                    this.graph1.testId = System.Convert.ToInt32(this.Session["sgaTestId"].ToString());
                    this.graph1.userId = SGACommon.LoginUserInfo.userId;
                }
                else
                {
                    base.Response.Redirect("my-results-reports.aspx", false);
                }
            }

            //Report Link

   //         SqlParameter[] paramPack = new SqlParameter[]
   //{
   //             new SqlParameter("@userId", SqlDbType.Int)
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

        protected void lnkLower_Click(object sender, System.EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            this.graph1.CompareResult(System.Convert.ToInt32(lnk.CommandArgument));

            if (lnk != null)
            {
                lnk.Attributes["class"] = "active";
            }
        }
    }
}