using CookComputing.XmlRpc;
using DataTier;
using InfusionSoftDotNet;
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
                bool isPkeTest = false;
                bool isCmkTest = false;
                bool isCaaTest = false;

                bool isTnaComplete = false;
                bool isCMAComplete = false;
                bool isPkeComplete = false;
                bool isCmkComplete = false;
                bool isCaaComplete = false;

                if (dsPermission != null)
                {
                    if (dsPermission.Tables.Count > 0 && dsPermission.Tables[0].Rows.Count > 0)
                    {
                        isTnaTest = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewTnaTest"].ToString());
                        isPkeTest = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewPkeTest"].ToString());
                        isCMAtest = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["takeCMATest"].ToString());
                        isCmkTest = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewCmkTest"].ToString());
                        isCaaTest = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewCaaTest"].ToString());
                        isPkeComplete = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["isPkeComplete"].ToString());
                        isTnaComplete = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["isTnaComplete"].ToString());
                        isCMAComplete = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["isCmaComplete"].ToString());
                        isCmkComplete = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["isCmkComplete"].ToString());
                        isCaaComplete = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["isCaaComplete"].ToString());
                    }
                }
                SqlParameter[] param = new SqlParameter[]
           {
                new SqlParameter("@userId", SqlDbType.Int)
           };
                param[0].Value = SGACommon.LoginUserInfo.userId;
                int complete = System.Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spUserProfileComplete", param));

                if (complete != 1)
                {
                    spanBegin.Visible = false;
                    this.hylTna.CssClass = (isTnaTest ? "btn-go" : "locked");
                    this.hylSga.CssClass = (isPkeTest ? "btn-go" : "locked");
                    this.hylCMA.CssClass = (isCMAtest ? "btn-go" : "locked");
                    this.hylCMK.CssClass = (isCmkTest ? "btn-go" : "locked");
                    this.hylCAA.CssClass = (isCaaTest ? "btn-go" : "locked");

                    //this.hylTna.Text = (isTnaTest ? "GO" : "Currently locked.<br />Want more info?<span>Your access to this assessment is now locked as you have completed it. If you require access to the assessment again, please contact support@comprara.com.au If you are looking for your results, click 'My Results' at the top of the page.</span>");
                    //this.hylSga.Text = (isPkeTest ? "GO" : "Currently locked.<br />Want more info?<span>Hi! Your access to the Procurement Knowledge Evaluation is currently disabled. You have either completed the challenge or have not yet been registered by the system administrator. If you would like to sit the assessment or require additional information contact the system administrator at: info@skillsgapanalysis.com</span>");
                    //this.hylCMA.Text = (isCMAtest ? "GO" : "Currently locked.<br />Want more info?<span>Your access to this assessment is now locked as you have completed it. If you require access to the assessment again, please contact support@comprara.com.au If you are looking for your results, click 'My Results' at the top of the page.</span>");
                    //this.hylCMK.Text = (isCmkTest ? "GO" : "Currently locked.<br />Want more info?<span>Your access to this assessment is now locked as you have completed it. If you require access to the assessment again, please contact support@comprara.com.au If you are looking for your results, click 'My Results' at the top of the page.</span>");
                    //this.hylCAA.Text = (isCaaTest ? "GO" : "Currently locked.<br />Want more info?<span>Your access to this assessment is now locked as you have completed it. If you require access to the assessment again, please contact support@comprara.com.au If you are looking for your results, click 'My Results' at the top of the page.</span>");

                    this.hylTna.Text = (isTnaTest ? "GO" : "Assessment completed.<br />Want more info?<span>Your assessment is now locked. Should you want this to be re-opened please contact support@comprara.com.au detailing the reason for your request, e.g. perhaps you have changed Department, or changed role. </span>");
                    this.hylSga.Text = (isPkeTest ? "GO" : "Assessment completed.<br />Want more info?<span>Your assessment is now locked. Should you want this to be re-opened please contact support@comprara.com.au detailing the reason for your request, e.g. perhaps you have changed Department, or changed role. </span>");
                    this.hylCMA.Text = (isCMAtest ? "GO" : "Assessment completed.<br />Want more info?<span>Your assessment is now locked. Should you want this to be re-opened please contact support@comprara.com.au detailing the reason for your request, e.g. perhaps you have changed Department, or changed role. </span>");
                    this.hylCMK.Text = (isCmkTest ? "GO" : "Assessment completed.<br />Want more info?<span>Your assessment is now locked. Should you want this to be re-opened please contact support@comprara.com.au detailing the reason for your request, e.g. perhaps you have changed Department, or changed role. </span>");
                    this.hylCAA.Text = (isCaaTest ? "GO" : "Assessment completed.<br />Want more info?<span>Your assessment is now locked. Should you want this to be re-opened please contact support@comprara.com.au detailing the reason for your request, e.g. perhaps you have changed Department, or changed role. </span>");


                    this.hylTna.ToolTip = (isTnaTest ? "Go" : "Locked");
                    this.hylSga.ToolTip = (isPkeTest ? "Go" : "Locked");
                    this.hylCMA.ToolTip = (isCMAtest ? "Go" : "locked");
                    this.hylCMK.ToolTip = (isCmkTest ? "Go" : "locked");
                    this.hylCAA.ToolTip = (isCaaTest ? "Go" : "locked");

                    this.hylTna.NavigateUrl = (isTnaTest ? "~\\tna\\ssa-assessment-instructions.aspx" : "#");
                    this.hylSga.NavigateUrl = (isPkeTest ? "~\\tna\\pk-evaluation-instructions.aspx" : "#");
                    this.hylCMA.NavigateUrl = (isCMAtest ? "~\\tna\\cma-assessment-instructions.aspx" : "#");
                    this.hylCMK.NavigateUrl = (isCmkTest ? "~\\tna\\cmk-assessment-instructions.aspx" : "#");
                    this.hylCAA.NavigateUrl = (isCaaTest ? "~\\tna\\ca-awareness-instructions.aspx" : "#");

                }
                else
                {
                    this.hylTna.Visible = false;
                    this.hylSga.Visible = false;
                    this.hylCMA.Visible = false;
                    this.hylCMK.Visible = false;
                    this.hylCAA.Visible = false;

                }
                dsPermission = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetProfileDetails", sqlParamPermission);
                if (dsPermission != null)
                {
                    if (dsPermission.Tables.Count > 0 && dsPermission.Tables[0].Rows.Count > 0)
                    {

                        switch (System.Convert.ToInt32(dsPermission.Tables[0].Rows[0]["jobrole"].ToString()))
                        {
                            case 1:
                                this.pnlTNA.Visible = true;
                                this.pnlCAA.Visible = true;
                                if (!isTnaComplete)
                                {
                                    this.hylCAA.Text = "Currently locked.<br />Want more info?<span>Your access to this assessment is locked. If you require access to the assessment, please complete Procurement self assesment first.</span>";
                                    this.hylCAA.ToolTip = "Locked";
                                    this.hylCAA.CssClass = "lockedAsess";
                                    this.hylCAA.NavigateUrl = "#";
                                }
                                break;
                            case 2:
                                this.pnlTNA.Visible = true;
                                this.pnlCAA.Visible = true;
                                if (!isTnaComplete)
                                {
                                    this.hylCAA.Text = "Currently locked.<br />Want more info?<span>Your access to this assessment is locked. If you require access to the assessment, please complete Procurement self assesment first.</span>";
                                    this.hylCAA.ToolTip = "Locked";
                                    this.hylCAA.CssClass = "lockedAsess";
                                    this.hylCAA.NavigateUrl = "#";
                                }
                                break;
                            case 3:
                                this.pnlTNA.Visible = true;
                                this.pnlCAA.Visible = true;
                                if (!isTnaComplete)
                                {
                                    this.hylCAA.Text = "Currently locked.<br />Want more info?<span>Your access to this assessment is locked. If you require access to the assessment, please complete Procurement self assesment first.</span>";
                                    this.hylCAA.ToolTip = "Locked";
                                    this.hylCAA.CssClass = "lockedAsess";
                                    this.hylCAA.NavigateUrl = "#";
                                }
                                break;
                            case 4:
                                this.pnlTNA.Visible = true;
                                this.pnlCAA.Visible = true;
                                if (!isTnaComplete)
                                {
                                    this.hylCAA.Text = "Currently locked.<br />Want more info?<span>Your access to this assessment is locked. If you require access to the assessment, please complete Procurement self assesment first.</span>";
                                    this.hylCAA.ToolTip = "Locked";
                                    this.hylCAA.CssClass = "lockedAsess";
                                    this.hylCAA.NavigateUrl = "#";
                                }
                                break;
                            case 5:
                                this.pnlTNA.Visible = true;
                                this.pnlSga.Visible = true;
                                this.pnlCAA.Visible = true;
                                if (!isTnaComplete)
                                {
                                    this.hylSga.Text = "Currently locked.<br />Want more info?<span>Your access to this assessment is locked. If you require access to the assessment, please complete Procurement self assesment first.</span>";
                                    this.hylSga.ToolTip = "Locked";
                                    this.hylSga.CssClass = "lockedAsess";
                                    this.hylSga.NavigateUrl = "#";

                                }
                                if (!isPkeComplete)
                                {
                                    this.hylCAA.Text = "Currently locked.<br />Want more info?<span>Your access to this assessment is locked. If you require access to the assessment, please complete Procurement Knowledge Evaluation first.</span>";
                                    this.hylCAA.ToolTip = "Locked";
                                    this.hylCAA.CssClass = "lockedAsess";
                                    this.hylCAA.NavigateUrl = "#";
                                }
                                break;
                            case 6:

                                this.pnlCMA.Visible = true;
                                this.pnlCMK.Visible = true;
                                this.pnlCAA.Visible = true;
                                if (!isCMAComplete)
                                {
                                    this.hylCMK.Text = "Currently locked.<br />Want more info?<span>Your access to this assessment is locked. If you require access to the assessment, please complete Contract Management Self Assessment first.</span>";
                                    this.hylCMK.ToolTip = "Locked";
                                    this.hylCMK.CssClass = "lockedAsess";
                                    this.hylCMK.NavigateUrl = "#";
                                }
                                if (!isCmkComplete)
                                {
                                    this.hylCAA.Text = "Currently locked.<br />Want more info?<span>Your access to this assessment is locked. If you require access to the assessment, please complete Contract Management Knowledge Evaluation first.</span>";
                                    this.hylCAA.ToolTip = "Locked";
                                    this.hylCAA.CssClass = "lockedAsess";
                                    this.hylCAA.NavigateUrl = "#";
                                }
                                break;
                            case 7:
                                this.pnlTNA.Visible = true;
                                this.pnlSga.Visible = true;
                                this.pnlCAA.Visible = true;
                                if (!isTnaComplete)
                                {
                                    this.hylSga.Text = "Currently locked.<br />Want more info?<span>Your access to this assessment is locked. If you require access to the assessment, please complete Procurement self assesment first.</span>";
                                    this.hylSga.ToolTip = "Locked";
                                    this.hylSga.CssClass = "lockedAsess";
                                    this.hylSga.NavigateUrl = "#";
                                }
                                if (!isPkeComplete)
                                {
                                    this.hylCAA.Text = "Currently locked.<br />Want more info?<span>Your access to this assessment is locked. If you require access to the assessment, please complete Procurement Knowledge Evaluation first.</span>";
                                    this.hylCAA.ToolTip = "Locked";
                                    this.hylCAA.CssClass = "lockedAsess";
                                    this.hylCAA.NavigateUrl = "#";
                                }
                                break;
                            case 8:
                                this.pnlTNA.Visible = true;
                                this.pnlSga.Visible = true;
                                this.pnlCAA.Visible = true;
                                if (!isTnaComplete)
                                {
                                    this.hylSga.Text = "Currently locked.<br />Want more info?<span>Your access to this assessment is locked. If you require access to the assessment, please complete Procurement self assesment first.</span>";
                                    this.hylSga.ToolTip = "Locked";
                                    this.hylSga.CssClass = "lockedAsess";
                                    this.hylSga.NavigateUrl = "#";
                                }
                                if (!isPkeComplete)
                                {
                                    this.hylCAA.Text = "Currently locked.<br />Want more info?<span>Your access to this assessment is locked. If you require access to the assessment, please complete Procurement Knowledge Evaluation first.</span>";
                                    this.hylCAA.ToolTip = "Locked";
                                    this.hylCAA.CssClass = "lockedAsess";
                                    this.hylCAA.NavigateUrl = "#";
                                }
                                break;
                        }
                    }
                }               
               
            }
        }
    }
}
