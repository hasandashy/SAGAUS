﻿using AjaxControlToolkit;
using CookComputing.XmlRpc;
using DataTier;
using InfusionSoftDotNet;
using OfficeOpenXml;
using SGA.App_Code;
using SGA.controls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SGA.webadmin
{
    public partial class ListUsers : Page
    {
        

        protected string _direct = "";

        protected string _indirect = "";

        public string SortExpression
        {
            get
            {
                return (this.ViewState["SortExpression"] == null) ? "" : this.ViewState["SortExpression"].ToString();
            }
            set
            {
                this.ViewState["SortExpression"] = value;
            }
        }

        public bool SortOrder
        {
            get
            {
                return this.ViewState["SortOrder"] != null && System.Convert.ToBoolean(this.ViewState["SortOrder"].ToString());
            }
            set
            {
                this.ViewState["SortOrder"] = value;
            }
        }

        public string SortExpressionCMC
        {
            get
            {
                return (this.ViewState["SortExpressionCMC"] == null) ? "" : this.ViewState["SortExpressionCMC"].ToString();
            }
            set
            {
                this.ViewState["SortExpressionCMC"] = value;
            }
        }

        public bool SortOrderCMC
        {
            get
            {
                return this.ViewState["SortOrderCMC"] != null && System.Convert.ToBoolean(this.ViewState["SortOrderCMC"].ToString());
            }
            set
            {
                this.ViewState["SortOrderCMC"] = value;
            }
        }

        public string SortExpressionCMA
        {
            get
            {
                return (this.ViewState["SortExpressionCMA"] == null) ? "" : this.ViewState["SortExpressionCMA"].ToString();
            }
            set
            {
                this.ViewState["SortExpressionCMA"] = value;
            }
        }

        public bool SortOrderCMA
        {
            get
            {
                return this.ViewState["SortOrderCMA"] != null && System.Convert.ToBoolean(this.ViewState["SortOrderCMA"].ToString());
            }
            set
            {
                this.ViewState["SortOrderCMA"] = value;
            }
        }

        public string SortExpressionSSA
        {
            get
            {
                return (this.ViewState["SortExpressionSSA"] == null) ? "" : this.ViewState["SortExpressionSSA"].ToString();
            }
            set
            {
                this.ViewState["SortExpressionSSA"] = value;
            }
        }

        public bool SortOrderSSA
        {
            get
            {
                return this.ViewState["SortOrderSSA"] != null && System.Convert.ToBoolean(this.ViewState["SortOrderSSA"].ToString());
            }
            set
            {
                this.ViewState["SortOrderSSA"] = value;
            }
        }

        public string SortExpressionBA
        {
            get
            {
                return (this.ViewState["SortExpressionBA"] == null) ? "" : this.ViewState["SortExpressionBA"].ToString();
            }
            set
            {
                this.ViewState["SortExpressionBA"] = value;
            }
        }

        public bool SortOrderBA
        {
            get
            {
                return this.ViewState["SortOrderBA"] != null && System.Convert.ToBoolean(this.ViewState["SortOrderBA"].ToString());
            }
            set
            {
                this.ViewState["SortOrderBA"] = value;
            }
        }

        public string SortExpressionNP
        {
            get
            {
                return (this.ViewState["SortExpressionNP"] == null) ? "" : this.ViewState["SortExpressionNP"].ToString();
            }
            set
            {
                this.ViewState["SortExpressionNP"] = value;
            }
        }

        public bool SortOrderNP
        {
            get
            {
                return this.ViewState["SortOrderNP"] != null && System.Convert.ToBoolean(this.ViewState["SortOrderNP"].ToString());
            }
            set
            {
                this.ViewState["SortOrderNP"] = value;
            }
        }

        public string SortExpressionMP
        {
            get
            {
                return (this.ViewState["SortExpressionMP"] == null) ? "" : this.ViewState["SortExpressionMP"].ToString();
            }
            set
            {
                this.ViewState["SortExpressionMP"] = value;
            }
        }

        public bool SortOrderMP
        {
            get
            {
                return this.ViewState["SortOrderMP"] != null && System.Convert.ToBoolean(this.ViewState["SortOrderMP"].ToString());
            }
            set
            {
                this.ViewState["SortOrderMP"] = value;
            }
        }

        public int PageNumber
        {
            get
            {
                int result;
                if (this.ViewState["PageNumber"] != null)
                {
                    result = System.Convert.ToInt32(this.ViewState["PageNumber"]);
                }
                else
                {
                    result = 0;
                }
                return result;
            }
            set
            {
                this.ViewState["PageNumber"] = value;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.selected_tab.Value = base.Request.Form[this.selected_tab.UniqueID];
            this.txtFrom.Attributes.Add("readonly", "readonly");
            this.txtTo.Attributes.Add("readonly", "readonly");
            this.txtSSAFrom.Attributes.Add("readonly", "readonly");
            this.txtSSATo.Attributes.Add("readonly", "readonly");
            this.txtBAFrom.Attributes.Add("readonly", "readonly");
            this.txtBATo.Attributes.Add("readonly", "readonly");
            this.txtCMAFrom.Attributes.Add("readonly", "readonly");
            this.txtCMATo.Attributes.Add("readonly", "readonly");
            this.txtNPFrom.Attributes.Add("readonly", "readonly");
            this.txtNPTo.Attributes.Add("readonly", "readonly");
            this.txtEditExiryDate.Attributes.Add("readonly", "readonly");
            if (!base.IsPostBack)
            {
                if (base.Request.QueryString["id"] != null)
                {
                    SGACommon.SelectListItem(this.ddlOrder, base.Request.QueryString["id"].ToString());
                }
                if (base.Request.QueryString["tabId"] != null)
                {
                    this.selected_tab.Value = base.Request.QueryString["tabId"].ToString();
                }
                this.BindGrid();
                this.BindPermission();
                this.BindUserTestInfo();
                this.BindSSATest();
                this.BindBATest();
                this.BindCMATest();
                this.BindNPTest();
            }
        }

        private void BindUserTestInfo()
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spUserTestInformation", new SqlParameter[]
			{
				new SqlParameter("@userIds", this.hdSelectIds.Value)
			});
            this.grdUserTest.DataSource = ds;
            this.grdUserTest.DataBind();
        }

        private void BindPermission()
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetUserResult", new SqlParameter[]
			{
				new SqlParameter("@userIds", this.hdSelectIds.Value)
			});
            this.dtgList.DataSource = ds;
            this.dtgList.DataBind();
        }

        private void BindGrid()
        {
            string strOrderBy = " u.isApproved asc, id desc  ";
            if (this.SortExpression.Length > 0)
            {
                strOrderBy = (this.SortOrder ? (this.SortExpression + " Asc") : (this.SortExpression + " Desc"));
            }
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageUsers", new SqlParameter[]
			{
				new SqlParameter("@firstname", this.txtFname.Value.Trim()),
				new SqlParameter("@lastname", this.txtLname.Value.Trim()),
				new SqlParameter("@email", this.txtEmail.Value.Trim()),
				new SqlParameter("@dateFrom", this.txtFrom.Text.Trim()),
				new SqlParameter("@dateTo", this.txtTo.Text.Trim()),
				new SqlParameter("@userCondition", this.ddlOrder.SelectedValue),
				new SqlParameter("@orderBy", strOrderBy),
				new SqlParameter("@roleId", 2)
			});
            this.grdUsers.DataSource = ds;
            this.grdUsers.DataBind();
        }

        protected void iBtnSelect_Click(object sender, ImageClickEventArgs e)
        {
            this.hdSelectIds.Value = "";
            foreach (DataGridItem item in this.grdUsers.Items)
            {
                HtmlInputCheckBox chkSelect = (HtmlInputCheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        HiddenField expr_61 = this.hdSelectIds;
                        expr_61.Value = expr_61.Value + chkSelect.Value + ",";
                    }
                }
            }
            this.BindGrid();
            this.dtgList.CurrentPageIndex = 0;
            this.BindPermission();
            this.grdUserTest.CurrentPageIndex = 0;
            this.BindUserTestInfo();
            this.grdSSA.CurrentPageIndex = 0;
            this.BindSSATest();
            this.grdBA.CurrentPageIndex = 0;
            this.BindBATest();
            this.grdCMA.CurrentPageIndex = 0;
            this.BindCMATest();
            this.grdNP.CurrentPageIndex = 0;
            this.BindNPTest();
            this.LoadProfile();
        }

        protected void iBtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            foreach (DataGridItem item in this.grdUsers.Items)
            {
                HtmlInputCheckBox chkSelect = (HtmlInputCheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spDeleteUser", new SqlParameter[]
						{
							new SqlParameter("@userId", chkSelect.Value)
						});
                    }
                }
            }
            this.BindGrid();
            this.BindPermission();
            this.BindUserTestInfo();
            this.BindSSATest();
            this.BindBATest();
            this.BindCMATest();
            this.BindNPTest();
        }

        protected void iBtnDisApproveAll_Click(object sender, ImageClickEventArgs e)
        {
            foreach (DataGridItem item in this.grdUsers.Items)
            {
                HtmlInputCheckBox chkSelect = (HtmlInputCheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUserStatus", new SqlParameter[]
						{
							new SqlParameter("@userId", chkSelect.Value)
						});
                        DataTable dt = this.GetUserDetailByUserId(chkSelect.Value);
                        if (dt.Rows.Count > 0)
                        {
                            string subject = "";
                            string body = "";
                            SGACommon.GetEmailTemplate(9, ref subject, ref body);
                            body = body.Replace("@v0", dt.Rows[0]["firstName"].ToString()).Replace("@v1", dt.Rows[0]["lastName"].ToString()).Replace("@v2", dt.Rows[0]["company"].ToString()).Replace("@v3", dt.Rows[0]["email"].ToString()).Replace("@v5", dt.Rows[0]["plainpassword"].ToString());
                            MailSending.SendMail(ConfigurationManager.AppSettings["nameDisplay"].ToString(), ConfigurationManager.AppSettings["UserName"].ToString(), dt.Rows[0]["email"].ToString(), subject, body, "");
                        }
                    }
                }
            }
            this.BindGrid();
            this.BindPermission();
            this.BindUserTestInfo();
            this.BindSSATest();
            this.BindBATest();
            this.BindCMATest();
            this.BindNPTest();
        }

        protected void iBtnApproveAll_Click(object sender, ImageClickEventArgs e)
        {
            foreach (DataGridItem item in this.grdUsers.Items)
            {
                HtmlInputCheckBox chkSelect = (HtmlInputCheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUserStatus", new SqlParameter[]
						{
							new SqlParameter("@userId", chkSelect.Value)
						});
                        DataTable dt = this.GetUserDetailByUserId(chkSelect.Value);
                        if (dt.Rows.Count > 0)
                        {
                            string subject = "";
                            string body = "";
                            SGACommon.GetEmailTemplate(7, ref subject, ref body);
                            body = body.Replace("@v0", dt.Rows[0]["firstName"].ToString()).Replace("@v1", dt.Rows[0]["lastName"].ToString()).Replace("@v2", dt.Rows[0]["company"].ToString()).Replace("@v3", dt.Rows[0]["email"].ToString()).Replace("@v5", dt.Rows[0]["plainpassword"].ToString()).Replace("@v4", Profile.GetJobRole(System.Convert.ToInt32(dt.Rows[0]["jobRole"].ToString())));
                            MailSending.SendMail(ConfigurationManager.AppSettings["nameDisplay"].ToString(), ConfigurationManager.AppSettings["UserName"].ToString(), dt.Rows[0]["email"].ToString(), subject, body, "");
                        }
                    }
                }
            }
            this.BindGrid();
            this.BindPermission();
            this.BindUserTestInfo();
            this.BindSSATest();
            this.BindBATest();
            this.BindCMATest();
            this.BindNPTest();
        }

        protected void iBtnLoginReminder_Click(object sender, ImageClickEventArgs e)
        {
            foreach (DataGridItem item in this.grdUsers.Items)
            {
                HtmlInputCheckBox chkSelect = (HtmlInputCheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        DataTable dt = this.GetUserDetailByUserId(chkSelect.Value);
                        if (dt.Rows.Count > 0)
                        {
                            string subject = "";
                            string body = "";
                            SGACommon.GetEmailTemplate(14, ref subject, ref body);
                            body = body.Replace("@v0", dt.Rows[0]["firstName"].ToString()).Replace("@v3", dt.Rows[0]["email"].ToString()).Replace("@v5", dt.Rows[0]["plainpassword"].ToString());
                            subject = subject.Replace("@v0", dt.Rows[0]["firstName"].ToString());
                            MailSending.SendMail(ConfigurationManager.AppSettings["nameDisplay"].ToString(), ConfigurationManager.AppSettings["UserName"].ToString(), dt.Rows[0]["email"].ToString(), subject, body, "");
                        }
                    }
                }
            }
        }

        protected void iBtnResendEmail_Click(object sender, ImageClickEventArgs e)
        {
            foreach (DataGridItem item in this.grdUsers.Items)
            {
                HtmlInputCheckBox chkSelect = (HtmlInputCheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        DataTable dt = this.GetUserDetailByUserId(chkSelect.Value);
                        if (dt.Rows.Count > 0)
                        {
                            string subject = "";
                            string body = "";
                            SGACommon.GetEmailTemplate(7, ref subject, ref body);
                            body = body.Replace("@v0", dt.Rows[0]["firstName"].ToString()).Replace("@v1", dt.Rows[0]["lastName"].ToString()).Replace("@v2", dt.Rows[0]["company"].ToString()).Replace("@v3", dt.Rows[0]["email"].ToString()).Replace("@v5", dt.Rows[0]["plainpassword"].ToString());
                            MailSending.SendMail(ConfigurationManager.AppSettings["nameDisplay"].ToString(), ConfigurationManager.AppSettings["UserName"].ToString(), dt.Rows[0]["email"].ToString(), subject, body, "");
                        }
                    }
                }
            }
        }

        protected void dtgList_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                bool isPke = System.Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "pkeResult"));
                bool isTna = System.Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "tnaResult"));
                bool isCmk = System.Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "cmkResult"));
                bool isCaa = System.Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "caaResult"));
                bool isCma = System.Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "cmaResult"));
                bool isTakePke = System.Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "takePKE"));
                bool isTakeTna = System.Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "takeTNA"));
                bool isTakeCmk = System.Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "takeCMK"));
                bool isTakeCaa = System.Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "takeCAA"));
                bool isTakeCMA = System.Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "takeCMA"));
                if (isPke && isTna && isCmk && isTna && isCaa && isCma && isTakePke && isTakeTna && isTakeCmk && isTakeCMA)
                {
                    e.Item.BackColor = Color.FromArgb(133, 195, 233);
                }
            }
        }

        protected void dtgList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            this.dtgList.CurrentPageIndex = e.NewPageIndex;
            this.BindPermission();
        }

        protected void btnUpdate_Click(object sender, System.EventArgs e)
        {
            foreach (DataGridItem item in this.dtgList.Items)
            {
                HtmlInputCheckBox chkPke = (HtmlInputCheckBox)item.FindControl("chkPke");
                HtmlInputCheckBox chkTna = (HtmlInputCheckBox)item.FindControl("chkTna");
                HtmlInputCheckBox chkCmk = (HtmlInputCheckBox)item.FindControl("chkCmk");
                HtmlInputCheckBox chkCaa = (HtmlInputCheckBox)item.FindControl("chkCaa");
                HtmlInputCheckBox chkCMA = (HtmlInputCheckBox)item.FindControl("chkCma");
                HtmlInputCheckBox chkPketest = (HtmlInputCheckBox)item.FindControl("chkPketest");
                HtmlInputCheckBox chkTnatest = (HtmlInputCheckBox)item.FindControl("chkTnatest");
                HtmlInputCheckBox chkCmktest = (HtmlInputCheckBox)item.FindControl("chkCmktest");
                HtmlInputCheckBox chkCaatest = (HtmlInputCheckBox)item.FindControl("chkCaatest");
                HtmlInputCheckBox chkCmatest = (HtmlInputCheckBox)item.FindControl("chkCmatest");
                if (chkPke != null && chkTna != null && chkCmk != null && chkTna != null && chkCaa != null && chkCMA != null && chkPketest != null && chkTnatest != null && chkCmktest != null && chkCaatest != null && chkCMA != null && chkCmatest != null)
                {
                    SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateResultPermission", new SqlParameter[]
					{
						new SqlParameter("@userId", chkPke.Value),
						new SqlParameter("@pkeResult", chkPke.Checked),
						new SqlParameter("@tnaResult", chkTna.Checked),
						new SqlParameter("@cmkResult", chkCmk.Checked),
                        new SqlParameter("@caaResult", chkCaa.Checked),
                        new SqlParameter("@cmaResult", chkCMA.Checked),
						new SqlParameter("@pkeTest", chkPketest.Checked),
						new SqlParameter("@tnaTest", chkTnatest.Checked),
						new SqlParameter("@cmkTest", chkCmktest.Checked),						
						new SqlParameter("@caaTest", chkCaatest.Checked),
						new SqlParameter("@cmaTest", chkCmatest.Checked)
					});
                }
            }
            this.BindPermission();
        }

        public DataTable GetUserDetailByUserId(string userId)
        {
            return SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetProfileDetails", new SqlParameter[]
			{
				new SqlParameter("@userId", userId)
			}).Tables[0];
        }

        protected void lnkSearch_Click(object sender, System.EventArgs e)
        {
            this.grdUsers.CurrentPageIndex = 0;
            this.BindGrid();
        }

        private void BindSSATest()
        {
            string strOrderBy = " testId desc  ";
            if (this.SortExpressionSSA.Length > 0)
            {
                strOrderBy = (this.SortOrderSSA ? (this.SortExpressionSSA + " Asc") : (this.SortExpressionSSA + " Desc"));
            }
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@firstname", this.txtSSAFname.Value.Trim());
            param[1] = new SqlParameter("@lastname", this.txtSSALname.Value.Trim());
            param[2] = new SqlParameter("@userIds", this.hdSelectIds.Value);
            param[3] = new SqlParameter("@dateFrom", this.txtSSAFrom.Text.Trim());
            param[4] = new SqlParameter("@dateTo", this.txtSSATo.Text.Trim());
            param[5] = new SqlParameter("@orderBy", strOrderBy);
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageSSATest", param);
            this.grdSSA.DataSource = ds;
            this.grdSSA.DataBind();
        }

        protected void grdSSA_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblAssesmentDate = (Label)e.Item.FindControl("lblAssesmentDate");
                if (lblAssesmentDate != null)
                {
                    lblAssesmentDate.Text = SGACommon.ToAusTimeZone(System.Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "testDate"))).ToString("dd MMM yyyy HH:mm tt");
                }
                int complete = System.Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "complete"));
                if (complete <= 0)
                {
                    e.Item.BackColor = Color.FromArgb(255, 156, 255);
                }
            }
        }

        protected void grdSSA_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            this.grdSSA.CurrentPageIndex = e.NewPageIndex;
            this.BindSSATest();
        }

        protected void grdSSA_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spDeleteSSATest", new SqlParameter[]
				{
					new SqlParameter("@testId", e.CommandArgument)
				});
                this.BindSSATest();
            }
            else if (e.CommandName == "Graph")
            {
                base.Response.Redirect("TestChartSSA.aspx?id=" + e.CommandArgument, false);
            }
            else if (e.CommandName == "drilldown")
            {
                base.Response.Redirect("/SSAChart/" + e.CommandArgument, false);
            }
            else if (e.CommandName == "Edit")
            {
                base.Response.Redirect("EditSSAtest.aspx?id=" + e.CommandArgument, false);
            }
        }

        protected void lnkSSASearch_Click(object sender, System.EventArgs e)
        {
            this.grdSSA.CurrentPageIndex = 0;
            this.BindSSATest();
        }

        private void BindBATest()
        {
            string strOrderBy = " testId desc  ";
            if (this.SortExpressionBA.Length > 0)
            {
                strOrderBy = (this.SortOrderBA ? (this.SortExpressionBA + " Asc") : (this.SortExpressionBA + " Desc"));
            }
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageBATest", new SqlParameter[]
			{
				new SqlParameter("@firstname", this.txtBAFname.Value.Trim()),
				new SqlParameter("@lastname", this.txtBALname.Value.Trim()),
				new SqlParameter("@userIds", this.hdSelectIds.Value),
				new SqlParameter("@dateFrom", this.txtBAFrom.Text.Trim()),
				new SqlParameter("@dateTo", this.txtBATo.Text.Trim()),
				new SqlParameter("@orderBy", strOrderBy)
			});
            this.grdBA.DataSource = ds;
            this.grdBA.DataBind();
        }

        protected void grdBA_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblAssesmentDate = (Label)e.Item.FindControl("lblAssesmentDate");
                if (lblAssesmentDate != null)
                {
                    lblAssesmentDate.Text = SGACommon.ToAusTimeZone(System.Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "testDate"))).ToString("dd MMM yyyy HH:mm tt");
                }
                int complete = System.Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "complete"));
                if (complete <= 0)
                {
                    e.Item.BackColor = Color.FromArgb(255, 156, 255);
                }
            }
        }

        protected void grdBA_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            this.grdBA.CurrentPageIndex = e.NewPageIndex;
            this.BindBATest();
        }

        protected void grdBA_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spDeleteBATest", new SqlParameter[]
				{
					new SqlParameter("@testId", e.CommandArgument)
				});
                this.BindBATest();
            }
            else if (e.CommandName == "Graph")
            {
                base.Response.Redirect("TestChartBA.aspx?id=" + e.CommandArgument, false);
            }
            else if (e.CommandName == "drilldown")
            {
                base.Response.Redirect("/BAChart/" + e.CommandArgument, false);
            }
            else if (e.CommandName == "Edit")
            {
                base.Response.Redirect("EditBAtest.aspx?id=" + e.CommandArgument, false);
            }
        }

        protected void lnkBASearch_Click(object sender, System.EventArgs e)
        {
            this.grdBA.CurrentPageIndex = 0;
            this.BindBATest();
        }

        private void BindCMATest()
        {
            string strOrderBy = " testId desc  ";
            if (this.SortExpressionCMA.Length > 0)
            {
                strOrderBy = (this.SortOrderCMA ? (this.SortExpressionCMA + " Asc") : (this.SortExpressionSSA + " Desc"));
            }
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageCMATest", new SqlParameter[]
			{
				new SqlParameter("@firstname", this.txtCMAFname.Value.Trim()),
				new SqlParameter("@lastname", this.txtCMALname.Value.Trim()),
				new SqlParameter("@userIds", this.hdSelectIds.Value),
				new SqlParameter("@dateFrom", this.txtCMAFrom.Text.Trim()),
				new SqlParameter("@dateTo", this.txtCMATo.Text.Trim()),
				new SqlParameter("@orderBy", strOrderBy)
			});
            this.grdCMA.DataSource = ds;
            this.grdCMA.DataBind();
        }

        protected void grdCMA_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblAssesmentDate = (Label)e.Item.FindControl("lblAssesmentDate");
                if (lblAssesmentDate != null)
                {
                    lblAssesmentDate.Text = SGACommon.ToAusTimeZone(System.Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "testDate"))).ToString("dd MMM yyyy HH:mm tt");
                }
                int complete = System.Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "complete"));
                if (complete <= 0)
                {
                    e.Item.BackColor = Color.FromArgb(255, 156, 255);
                }
            }
        }

        protected void grdCMA_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            this.grdSSA.CurrentPageIndex = e.NewPageIndex;
            this.BindCMATest();
        }

        protected void grdCMA_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spDeleteCMATest", new SqlParameter[]
				{
					new SqlParameter("@testId", e.CommandArgument)
				});
                this.BindCMATest();
            }
            else if (e.CommandName == "Graph")
            {
                base.Response.Redirect("TestChartCMA.aspx?id=" + e.CommandArgument, false);
            }
            else if (e.CommandName == "drilldown")
            {
                base.Response.Redirect("/CMAChart/" + e.CommandArgument, false);
            }
            else if (e.CommandName == "Edit")
            {
                base.Response.Redirect("EditCMAtest.aspx?id=" + e.CommandArgument, false);
            }
        }

        protected void lnkCMASearch_Click(object sender, System.EventArgs e)
        {
            this.grdCMA.CurrentPageIndex = 0;
            this.BindCMATest();
        }

        private void BindNPTest()
        {
            string strOrderBy = " testId desc  ";
            if (this.SortExpressionNP.Length > 0)
            {
                strOrderBy = (this.SortOrderNP ? (this.SortExpressionNP + " Asc") : (this.SortExpressionNP + " Desc"));
            }
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageNPTest", new SqlParameter[]
			{
				new SqlParameter("@firstname", this.txtNPFname.Value.Trim()),
				new SqlParameter("@lastname", this.txtNPLname.Value.Trim()),
				new SqlParameter("@dateFrom", this.txtNPFrom.Text.Trim()),
				new SqlParameter("@dateTo", this.txtNPTo.Text.Trim()),
                new SqlParameter("@userIds", this.hdSelectIds.Value),
				new SqlParameter("@orderBy", strOrderBy)
			});
            this.grdNP.DataSource = ds;
            this.grdNP.DataBind();
        }

        protected void grdNP_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblAssesmentDate = (Label)e.Item.FindControl("lblAssesmentDate");
                if (lblAssesmentDate != null)
                {
                    lblAssesmentDate.Text = SGACommon.ToAusTimeZone(System.Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "testDate"))).ToString("dd MMM yyyy HH:mm tt");
                }
                int complete = System.Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "complete"));
                if (complete <= 0)
                {
                    e.Item.BackColor = Color.FromArgb(255, 156, 255);
                }
            }
        }

        protected void grdNP_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            this.grdNP.CurrentPageIndex = e.NewPageIndex;
            this.BindNPTest();
        }

        protected void grdNP_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spDeleteNPTest", new SqlParameter[]
				{
					new SqlParameter("@testId", e.CommandArgument)
				});
                this.BindNPTest();
            }
            else if (e.CommandName == "Graph")
            {
                base.Response.Redirect("TestChartNP.aspx?id=" + e.CommandArgument, false);
            }
            else if (e.CommandName == "drilldown")
            {
                base.Response.Redirect("/NPChart/" + e.CommandArgument, false);
            }
            else if (e.CommandName == "Edit")
            {
                base.Response.Redirect("EditNPtest.aspx?id=" + e.CommandArgument, false);
            }
        }

        protected void lnkNPSearch_Click(object sender, System.EventArgs e)
        {
            this.grdNP.CurrentPageIndex = 0;
            this.BindNPTest();
        }

        protected void imgSave_Click(object sender, ImageClickEventArgs e)
        {
            SqlParameter[] param = new SqlParameter[]
			{
				new SqlParameter("@email", SqlDbType.VarChar)
			};
            param[0].Value = this.txtEmailAddress.Value.Trim();
            int status = System.Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spCheckUniqueEmail", param));
            if (status > 0)
            {
                this.lblError.Text = "The email address already exists!";
            }
            else
            {
                try
                {
                    string plainpassword = SGACommon.generatePassword(8);
                    string passwordSalt = SGACommon.CreateSalt(5);
                    string passwordHash = SGACommon.CreatePasswordHash(plainpassword, passwordSalt);
                    param = new SqlParameter[15];
                    param[0] = new SqlParameter("@action", SqlDbType.VarChar);
                    param[0].Value = "Insert";
                    param[1] = new SqlParameter("@password", SqlDbType.VarChar);
                    param[1].Value = plainpassword;
                    param[2] = new SqlParameter("@firstName", SqlDbType.VarChar);
                    param[2].Value = this.txtFirstname.Value.Trim();
                    param[3] = new SqlParameter("@lastName", SqlDbType.VarChar);
                    param[3].Value = this.txtLastname.Value.Trim();
                    param[4] = new SqlParameter("@email", SqlDbType.VarChar);
                    param[4].Value = this.txtEmailAddress.Value.Trim();
                    param[5] = new SqlParameter("@isApproved", SqlDbType.Bit);
                    param[5].Value = 1;
                    param[6] = new SqlParameter("@passwordHash", SqlDbType.VarChar);
                    param[6].Value = passwordHash;
                    param[7] = new SqlParameter("@passwordSalt", SqlDbType.VarChar);
                    param[7].Value = passwordSalt;
                    param[8] = new SqlParameter("@jobRole", SqlDbType.Int);
                    param[8].Value = System.Convert.ToInt32(this.ddlJobRole.SelectedValue);
                    param[9] = new SqlParameter("@isAdminAdded", SqlDbType.Bit);
                    param[9].Value = true;
                    param[10] = new SqlParameter("@jobLevel", SqlDbType.Int);
                    param[10].Value = System.Convert.ToInt32(this.ddlJobLevel.SelectedValue);
                    param[11] = new SqlParameter("@managerFirstname", SqlDbType.VarChar);
                    param[11].Value = this.MfirstName.Value.Trim();
                    param[12] = new SqlParameter("@managerLastName", SqlDbType.VarChar);
                    param[12].Value = this.MlastName.Value.Trim();
                    param[13] = new SqlParameter("@managerEmail", SqlDbType.VarChar);
                    param[13].Value = this.Memail.Value.Trim();
                    param[14] = new SqlParameter("@agencyId", SqlDbType.Int);
                    param[14].Value = System.Convert.ToInt32(this.ddlAgency.SelectedValue);
                    int result = System.Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spUserMaster", param));
                    if (result == 0)
                    {
                        this.lblError.Text = "There was some error with user saving, try again.";
                    }
                    else
                    {
                        string[] strField = new string[]
						{
							"Id"
						};
                        XmlRpcStruct[] resultFound = isdnAPI.findByEmail(this.Memail.Value.Trim(), strField);
                        XmlRpcStruct Contact = new XmlRpcStruct();
                        if (resultFound.Length > 0)
                        {
                            int userId = System.Convert.ToInt32(resultFound[0]["Id"].ToString());
                            bool isAdded = isdnAPI.addToGroup(userId, 756);
                        }
                        else
                        {
                            Contact.Add("FirstName", this.MfirstName.Value.Trim());
                            Contact.Add("LastName", this.MlastName.Value.Trim());
                            Contact.Add("Email", this.Memail.Value.Trim());
                            Contact.Add("OwnerID", "50036");
                            Contact.Add("ContactType", "Customer");
                            int userId = isdnAPI.add(Contact);
                            bool isAdded = isdnAPI.addToGroup(userId, 756);
                            isdnAPI.optIn(this.Memail.Value.Trim(), "Sending emails is allowed");
                        }
                        resultFound = isdnAPI.findByEmail(this.txtEmailAddress.Value.Trim(), strField);
                        if (resultFound.Length > 0)
                        {
                            int userId = System.Convert.ToInt32(resultFound[0]["Id"].ToString());
                            isdnAPI.optIn(this.txtEmailAddress.Value.Trim(), "Sending emails is allowed");
                            isdnAPI.dsUpdate("Contact", userId, new XmlRpcStruct
							{
								{
									"FirstName",
									this.txtFirstname.Value.Trim()
								},
								{
									"LastName",
									this.txtLastname.Value.Trim()
								},
								{
									"Email",
									this.txtEmailAddress.Value.Trim()
								},
								{
									"OwnerID",
									"50036"
								},
								{
									"_StudentsManagersFirstName",
									this.MfirstName.Value.Trim()
								},
								{
									"_StudentsManagersLastName",
									this.MlastName.Value.Trim()
								},
								{
									"_StudentsManagersEmail",
									this.Memail.Value.Trim()
								},
								{
									"_CSBPassword",
									plainpassword
								},
								{
									"_YourOrganisation",
									Profile.GetOrganisation(System.Convert.ToInt32(this.ddlAgency.SelectedValue))
								},
								{
									"_Role",
									Profile.GetJobRole(System.Convert.ToInt32(this.ddlJobRole.SelectedValue))
								},
								{
									"_RoleLevel",
									Profile.GetJobLevel(System.Convert.ToInt32(this.ddlJobLevel.SelectedValue))
								},
								{
									"_CSBUsername",
									this.txtEmailAddress.Value.Trim()
								}
							});
                        }
                        else
                        {
                            int userId = isdnAPI.add(new XmlRpcStruct
							{
								{
									"FirstName",
									this.txtFirstname.Value.Trim()
								},
								{
									"LastName",
									this.txtLastname.Value.Trim()
								},
								{
									"Email",
									this.txtEmailAddress.Value.Trim()
								},
								{
									"OwnerID",
									"50036"
								},
								{
									"_StudentsManagersFirstName",
									this.MfirstName.Value.Trim()
								},
								{
									"_StudentsManagersLastName",
									this.MlastName.Value.Trim()
								},
								{
									"_StudentsManagersEmail",
									this.Memail.Value.Trim()
								},
								{
									"_CSBPassword",
									plainpassword
								},
								{
									"_YourOrganisation",
									Profile.GetOrganisation(System.Convert.ToInt32(this.ddlAgency.SelectedValue))
								},
								{
									"_Role",
									Profile.GetJobRole(System.Convert.ToInt32(this.ddlJobRole.SelectedValue))
								},
								{
									"_RoleLevel",
									Profile.GetJobLevel(System.Convert.ToInt32(this.ddlJobLevel.SelectedValue))
								},
								{
									"_CSBUsername",
									this.txtEmailAddress.Value.Trim()
								},
								{
									"ContactType",
									"Customer"
								}
							});
                            if (userId > 0)
                            {
                                bool isAdded = isdnAPI.addToGroup(userId, 400);
                                isdnAPI.optIn(this.txtEmailAddress.Value.Trim(), "Sending emails is allowed");
                            }
                        }
                        this.lblError.Text = "User added successfully and email sent to him.";
                        this.txtFirstname.Value = "";
                        this.txtLastname.Value = "";
                        this.txtEmailAddress.Value = "";
                        this.ddlAgency.SelectedIndex = -1;
                        this.ddlJobRole.SelectedIndex = -1;
                        this.ddlJobLevel.SelectedIndex = -1;
                        this.MfirstName.Value = "";
                        this.MlastName.Value = "";
                        this.Memail.Value = "";
                        this.ddlJobRole.SelectedIndex = -1;
                        this.BindGrid();
                    }
                }
                catch (System.Exception ex)
                {
                    this.lblError.Text = "There was some error with user saving, try again.";
                }
            }
        }

        protected void imgUpload_Click(object sender, ImageClickEventArgs e)
        {
            System.Text.StringBuilder strmail = new System.Text.StringBuilder();
            strmail.Append("The following users can't be added as their email address already exists:<br />");
            HttpPostedFile file = this.FileInput.PostedFile;
            if (file != null && file.ContentLength > 0)
            {
                byte[] fileBytes = new byte[file.ContentLength];
                file.InputStream.Read(fileBytes, 0, file.ContentLength);
                string fileName = string.Format("SgaUsers_{0}_{1}.xlsx", System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"), SGACommon.generatePassword(4));
                string filePath = System.IO.Path.Combine(base.Request.PhysicalApplicationPath + "//webadmin/docs/", fileName);
                System.IO.File.WriteAllBytes(filePath, fileBytes);
                try
                {
                    System.IO.FileInfo newFile = new System.IO.FileInfo(filePath);
                    string[] properties = new string[]
					{
						"Email address",
						"First name",
						"Last name",
						"Your Organisation ID",
						"Your Job ROLE ID",
						"Your Job level ID",
						"Manager's First Name",
						"Manager's Last Name",
						"Manager's Email",
                        "Phone",
                        "Division",
                        "Location ID"
					};
                    int iRow = 2;
                    using (ExcelPackage xlPackage = new ExcelPackage(newFile))
                    {
                        ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                        if (worksheet == null)
                        {
                            throw new System.Exception("No worksheet found");
                        }
                        bool isValidData = true;
                        while (true)
                        {
                            bool allColumnsAreEmpty = true;
                            for (int i = 1; i < properties.Length; i++)
                            {
                                if (worksheet.Cells[iRow, i].Value != null && !string.IsNullOrEmpty(worksheet.Cells[iRow, i].Value.ToString()))
                                {
                                    allColumnsAreEmpty = false;
                                    break;
                                }
                            }
                            if (allColumnsAreEmpty)
                            {
                                break;
                            }
                            string email = worksheet.Cells[iRow, this.GetColumnIndex(properties, "Email address")].Value.ToString().Trim();
                            string fName = worksheet.Cells[iRow, this.GetColumnIndex(properties, "First name")].Value.ToString().Trim();
                            string lName = worksheet.Cells[iRow, this.GetColumnIndex(properties, "Last name")].Value.ToString().Trim();
                            int agencyId = System.Convert.ToInt32(worksheet.Cells[iRow, this.GetColumnIndex(properties, "Your Organisation ID")].Value.ToString().Trim());
                            int jobRoleId = System.Convert.ToInt32(worksheet.Cells[iRow, this.GetColumnIndex(properties, "Your Job ROLE ID")].Value.ToString().Trim());
                            int jobLevelId = System.Convert.ToInt32(worksheet.Cells[iRow, this.GetColumnIndex(properties, "Your Job level ID")].Value.ToString().Trim());
                            string mFirstName = worksheet.Cells[iRow, this.GetColumnIndex(properties, "Manager's First Name")].Value.ToString().Trim();
                            string mLastName = worksheet.Cells[iRow, this.GetColumnIndex(properties, "Manager's Last Name")].Value.ToString().Trim();
                            string mEmail = worksheet.Cells[iRow, this.GetColumnIndex(properties, "Manager's Email")].Value.ToString().Trim();
                            string phone = worksheet.Cells[iRow, this.GetColumnIndex(properties, "Phone")].Value.ToString().Trim();
                            string division = worksheet.Cells[iRow, this.GetColumnIndex(properties, "Division")].Value.ToString().Trim();
                            int locationId = System.Convert.ToInt32(worksheet.Cells[iRow, this.GetColumnIndex(properties, "Location ID")].Value.ToString().Trim());
                            if (email.Trim().Length <= 0)
                            {
                                goto Block_12;
                            }
                            iRow++;
                        }
                        goto IL_40D;
                    Block_12:
                        this.lblUploadError.Visible = true;
                        this.lblUploadError.Text = "Email address is required in the excel on row '" + iRow + "'.";
                        this.lblUploadError.ForeColor = Color.Red;
                        isValidData = false;
                    IL_40D:
                        iRow = 2;
                        if (isValidData)
                        {
                            bool userInsert = false;
                            bool isEmailError = false;
                            while (true)
                            {
                                bool allColumnsAreEmpty = true;
                                for (int i = 1; i < properties.Length; i++)
                                {
                                    if (worksheet.Cells[iRow, i].Value != null && !string.IsNullOrEmpty(worksheet.Cells[iRow, i].Value.ToString()))
                                    {
                                        allColumnsAreEmpty = false;
                                        break;
                                    }
                                }
                                if (allColumnsAreEmpty)
                                {
                                    break;
                                }
                                string email = worksheet.Cells[iRow, this.GetColumnIndex(properties, "Email address")].Value.ToString().Trim();
                                string fName = worksheet.Cells[iRow, this.GetColumnIndex(properties, "First name")].Value.ToString().Trim();
                                string lName = worksheet.Cells[iRow, this.GetColumnIndex(properties, "Last name")].Value.ToString().Trim();
                                int agencyId = System.Convert.ToInt32(worksheet.Cells[iRow, this.GetColumnIndex(properties, "Your Organisation ID")].Value.ToString().Trim());
                                int jobRoleId = System.Convert.ToInt32(worksheet.Cells[iRow, this.GetColumnIndex(properties, "Your Job ROLE ID")].Value.ToString().Trim());
                                int jobLevelId = System.Convert.ToInt32(worksheet.Cells[iRow, this.GetColumnIndex(properties, "Your Job level ID")].Value.ToString().Trim());
                                string mFirstName = worksheet.Cells[iRow, this.GetColumnIndex(properties, "Manager's First Name")].Value.ToString().Trim();
                                string mLastName = worksheet.Cells[iRow, this.GetColumnIndex(properties, "Manager's Last Name")].Value.ToString().Trim();
                                string mEmail = worksheet.Cells[iRow, this.GetColumnIndex(properties, "Manager's Email")].Value.ToString().Trim();
                                string phone = worksheet.Cells[iRow, this.GetColumnIndex(properties, "Phone")].Value.ToString().Trim();
                                string division = worksheet.Cells[iRow, this.GetColumnIndex(properties, "Division")].Value.ToString().Trim();
                                int locationId = System.Convert.ToInt32(worksheet.Cells[iRow, this.GetColumnIndex(properties, "Location ID")].Value.ToString().Trim());
                                if (!isEmailError)
                                {
                                    SqlParameter[] param = new SqlParameter[]
									{
										new SqlParameter("@email", SqlDbType.VarChar)
									};
                                    param[0].Value = email;
                                    int status = System.Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spCheckUniqueEmail", param));
                                    if (status > 0)
                                    {
                                        isEmailError = true;
                                        strmail.Append(email + "<br/>");
                                    }
                                    else
                                    {
                                        string plainpassword = SGACommon.generatePassword(8);
                                        string passwordSalt = SGACommon.CreateSalt(5);
                                        string passwordHash = SGACommon.CreatePasswordHash(plainpassword, passwordSalt);
                                        param = new SqlParameter[18];
                                        param[0] = new SqlParameter("@action", SqlDbType.VarChar);
                                        param[0].Value = "Insert";
                                        param[1] = new SqlParameter("@password", SqlDbType.VarChar);
                                        param[1].Value = plainpassword;
                                        param[2] = new SqlParameter("@firstName", SqlDbType.VarChar);
                                        param[2].Value = fName;
                                        param[3] = new SqlParameter("@lastName", SqlDbType.VarChar);
                                        param[3].Value = lName;
                                        param[4] = new SqlParameter("@email", SqlDbType.VarChar);
                                        param[4].Value = email;
                                        param[5] = new SqlParameter("@isApproved", SqlDbType.Bit);
                                        param[5].Value = 1;
                                        param[6] = new SqlParameter("@passwordHash", SqlDbType.VarChar);
                                        param[6].Value = passwordHash;
                                        param[7] = new SqlParameter("@passwordSalt", SqlDbType.VarChar);
                                        param[7].Value = passwordSalt;
                                        param[8] = new SqlParameter("@jobRole", SqlDbType.Int);
                                        param[8].Value = jobRoleId;
                                        param[9] = new SqlParameter("@isAdminAdded", SqlDbType.Bit);
                                        param[9].Value = 1;
                                        param[10] = new SqlParameter("@jobLevel", SqlDbType.Int);
                                        param[10].Value = jobLevelId;
                                        param[11] = new SqlParameter("@managerFirstname", SqlDbType.VarChar);
                                        param[11].Value = mFirstName;
                                        param[12] = new SqlParameter("@managerLastName", SqlDbType.VarChar);
                                        param[12].Value = mLastName;
                                        param[13] = new SqlParameter("@managerEmail", SqlDbType.VarChar);
                                        param[13].Value = mEmail;
                                        param[14] = new SqlParameter("@agencyId", SqlDbType.Int);
                                        param[14].Value = agencyId;
                                        param[15] = new SqlParameter("@phone", SqlDbType.VarChar);
                                        param[15].Value = phone;
                                        param[16] = new SqlParameter("@division", SqlDbType.VarChar);
                                        param[16].Value = division;
                                        param[17] = new SqlParameter("@locationId", SqlDbType.Int);
                                        param[17].Value = locationId;

                                        int result = System.Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spUserMaster", param));
                                        if (result > 0)
                                        {
                                            string[] strField = new string[]
											{
												"Id"
											};
                                            XmlRpcStruct[] resultFound = isdnAPI.findByEmail(mEmail, strField);
                                            XmlRpcStruct Contact = new XmlRpcStruct();
                                            if (resultFound.Length > 0)
                                            {
                                                int userId = System.Convert.ToInt32(resultFound[0]["Id"].ToString());
                                                bool isAdded = isdnAPI.addToGroup(userId, 756);
                                            }
                                            else
                                            {
                                                Contact.Add("FirstName", mFirstName);
                                                Contact.Add("LastName", mLastName);
                                                Contact.Add("Email", mEmail);
                                                Contact.Add("OwnerID", "50036");
                                                Contact.Add("ContactType", "Customer");
                                                int userId = isdnAPI.add(Contact);
                                                bool isAdded = isdnAPI.addToGroup(userId, 756);
                                                isdnAPI.optIn(mEmail, "Sending emails is allowed");
                                            }
                                            resultFound = isdnAPI.findByEmail(email, strField);
                                            if (resultFound.Length > 0)
                                            {
                                                int userId = System.Convert.ToInt32(resultFound[0]["Id"].ToString());
                                                bool isAdded = isdnAPI.addToGroup(userId, 400);
                                                isdnAPI.optIn(email, "Sending emails is allowed");
                                                isdnAPI.dsUpdate("Contact", userId, new XmlRpcStruct
												{
													{
														"FirstName",
														fName
													},
													{
														"LastName",
														lName
													},
													{
														"Email",
														email
													},
													{
														"OwnerID",
														"50036"
													},
													{
														"_StudentsManagersFirstName",
														mFirstName
													},
													{
														"_StudentsManagersLastName",
														mLastName
													},
													{
														"_StudentsManagersEmail",
														mEmail
													},
													{
														"_CSBPassword",
														plainpassword
													},
													{
														"_YourOrganisation",
														Profile.GetOrganisation(System.Convert.ToInt32(agencyId))
													},
													{
														"_Role",
														Profile.GetJobRole(System.Convert.ToInt32(jobRoleId))
													},
													{
														"_RoleLevel",
														Profile.GetJobLevel(System.Convert.ToInt32(jobLevelId))
													},
													{
														"_CSBUsername",
														email
													},
                                                    {"Phone1",phone},
													{
														"ContactType",
														"Customer"
													}
												});
                                            }
                                            else
                                            {
                                                int userId = isdnAPI.add(new XmlRpcStruct
												{
													{
														"FirstName",
														fName
													},
													{
														"LastName",
														lName
													},
													{
														"Email",
														email
													},
													{
														"OwnerID",
														"50036"
													},
													{
														"_StudentsManagersFirstName",
														mFirstName
													},
													{
														"_StudentsManagersLastName",
														mLastName
													},
													{
														"_StudentsManagersEmail",
														mEmail
													},
													{
														"_CSBPassword",
														plainpassword
													},
													{
														"_YourOrganisation",
														Profile.GetOrganisation(System.Convert.ToInt32(agencyId))
													},
													{
														"_Role",
														Profile.GetJobRole(System.Convert.ToInt32(jobRoleId))
													},
													{
														"_RoleLevel",
														Profile.GetJobLevel(System.Convert.ToInt32(jobLevelId))
													},
													{
														"_CSBUsername",
														email
													},
                                                    {"Phone1",phone},
													{
														"ContactType",
														"Customer"
													}
												});
                                                if (userId > 0)
                                                {
                                                    bool isAdded = isdnAPI.addToGroup(userId, 400);
                                                    isdnAPI.optIn(email, "Sending emails is allowed");
                                                }
                                            }
                                        }
                                        userInsert = true;
                                    }
                                }
                                iRow++;
                            }
                            if (isEmailError)
                            {
                                this.lblUploadError.Visible = true;
                                this.lblUploadError.Text = strmail.ToString();
                                this.lblUploadError.ForeColor = Color.Red;
                            }
                            if (userInsert)
                            {
                                this.BindGrid();
                                this.lblUploadError.Visible = true;
                                this.lblUploadError.Text = "Data inserted successfully.";
                                this.lblUploadError.ForeColor = Color.Green;
                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    this.lblUploadError.Visible = true;
                    this.lblUploadError.Text = ex.ToString();
                    this.lblUploadError.ForeColor = Color.Red;
                }
                finally
                {
                    System.IO.File.Delete(filePath);
                }
            }
        }

        protected virtual int GetColumnIndex(string[] properties, string columnName)
        {
            if (properties == null)
            {
                throw new System.ArgumentNullException("properties");
            }
            if (columnName == null)
            {
                throw new System.ArgumentNullException("columnName");
            }
            int result;
            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].Equals(columnName, System.StringComparison.InvariantCultureIgnoreCase))
                {
                    result = i + 1;
                    return result;
                }
            }
            result = 0;
            return result;
        }

        private void LoadProfile()
        {
            if (this.hdSelectIds.Value.Length > 0)
            {
                string[] strArr = this.hdSelectIds.Value.Split(new char[]
				{
					','
				});
                if (strArr.Length == 2)
                {
                    this.viewUser2.Visible = false;
                    this.viewUser1.Visible = true;
                    int id = System.Convert.ToInt32(strArr[0]);
                    SqlParameter[] param = new SqlParameter[]
					{
						new SqlParameter("@userId", SqlDbType.Int)
					};
                    param[0].Value = id;
                    DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetProfileDetailsAdmin", param);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            this.MEditfirstName.Text = ds.Tables[0].Rows[0]["managerFirstname"].ToString();
                            this.MEditlastName.Text = ds.Tables[0].Rows[0]["managerLastName"].ToString();
                            this.MEditemail.Text = ds.Tables[0].Rows[0]["managerEmail"].ToString();
                            this.txtEditFname.Text = ds.Tables[0].Rows[0]["firstname"].ToString();
                            this.txtEditLname.Text = ds.Tables[0].Rows[0]["lastname"].ToString();
                            this.txtEditPassword.Text = ds.Tables[0].Rows[0]["plainpassword"].ToString();
                            this.txtEditPhone.Text = ((ds.Tables[0].Rows[0]["Phone"].ToString().Length > 0) ? ds.Tables[0].Rows[0]["Phone"].ToString() : "");
                            this.txtEditExiryDate.Text = System.Convert.ToDateTime(ds.Tables[0].Rows[0]["expiryDate"]).ToShortDateString();
                            this.txtEditPosition.Value = ds.Tables[0].Rows[0]["position"].ToString();
                            this.txtDivision.Text = ds.Tables[0].Rows[0]["division"].ToString();
                            if (System.Convert.ToBoolean(ds.Tables[0].Rows[0]["expired"]))
                            {
                                this.btnEditProfileExpire.Visible = false;
                            }
                            else
                            {
                                this.btnEditProfileExpire.Visible = true;
                            }
                            SGACommon.SelectListItem(this.ddlEditAgency, ds.Tables[0].Rows[0]["agencyId"].ToString());
                            SGACommon.SelectListItem(this.ddlEditJobRole, ds.Tables[0].Rows[0]["jobRole"].ToString());
                            SGACommon.SelectListItem(this.ddlEditJobLevel, ds.Tables[0].Rows[0]["jobLevel"].ToString());
                            SGACommon.SelectListItem(this.ddlEditLocation, ds.Tables[0].Rows[0]["locationId"].ToString());
                            SGACommon.SelectListItem(this.ddlEditGoods, ds.Tables[0].Rows[0]["goodsId"].ToString());
                            this.txtEditEmailAddress.Text = ds.Tables[0].Rows[0]["email"].ToString();
                            this.lblEditStatus.Text = (System.Convert.ToBoolean(ds.Tables[0].Rows[0]["expired"]) ? "Expired" : "Running");
                        }
                    }
                }
                else
                {
                    this.viewUser2.Visible = true;
                    this.viewUser1.Visible = false;
                }
            }
        }

        public void UpdateProfile(string fname, string lname, int jobId, int jobLevel, string managerFirstname, string managerLastName, string managerEmail, int agencyId, string phone, string division, int locationId, string position, int goodsId, string password, System.DateTime dtExpiryDate, bool isExpired, string email)
        {
            string passwordSalt = SGACommon.CreateSalt(5);
            string passwordHash = SGACommon.CreatePasswordHash(password, passwordSalt);
            int id = System.Convert.ToInt32(this.hdSelectIds.Value.Split(new char[]
			{
				','
			})[0]);
            SqlParameter[] param = new SqlParameter[20];
            param[0] = new SqlParameter("@password", SqlDbType.VarChar);
            param[0].Value = password;
            param[1] = new SqlParameter("@firstName", SqlDbType.VarChar);
            param[1].Value = fname;
            param[2] = new SqlParameter("@lastName", SqlDbType.VarChar);
            param[2].Value = lname;
            param[3] = new SqlParameter("@passwordHash", SqlDbType.VarChar);
            param[3].Value = passwordHash;
            param[4] = new SqlParameter("@passwordSalt", SqlDbType.VarChar);
            param[4].Value = passwordSalt;
            param[5] = new SqlParameter("@jobRole", SqlDbType.Int);
            param[5].Value = jobId;
            param[6] = new SqlParameter("@jobLevel", SqlDbType.Int);
            param[6].Value = jobLevel;
            param[7] = new SqlParameter("@managerFirstname", SqlDbType.VarChar);
            param[7].Value = managerFirstname;
            param[8] = new SqlParameter("@managerLastName", SqlDbType.VarChar);
            param[8].Value = managerLastName;
            param[9] = new SqlParameter("@managerEmail", SqlDbType.VarChar);
            param[9].Value = managerEmail;
            param[10] = new SqlParameter("@agencyId", SqlDbType.Int);
            param[10].Value = agencyId;
            param[11] = new SqlParameter("@phone", SqlDbType.VarChar);
            param[11].Value = phone;
            param[12] = new SqlParameter("@division", SqlDbType.VarChar);
            param[12].Value = division;
            param[13] = new SqlParameter("@locationId", SqlDbType.Int);
            param[13].Value = locationId;
            param[14] = new SqlParameter("@position", SqlDbType.VarChar);
            param[14].Value = position;
            param[15] = new SqlParameter("@goodsId", SqlDbType.Int);
            param[15].Value = goodsId;
            param[16] = new SqlParameter("@userId", SqlDbType.Int);
            param[16].Value = id;
            param[17] = new SqlParameter("@expiryDate", SqlDbType.DateTime);
            param[17].Value = dtExpiryDate;
            param[18] = new SqlParameter("@isExpired", SqlDbType.VarChar);
            param[18].Value = isExpired;
            param[19] = new SqlParameter("@flag", SqlDbType.Int);
            param[19].Value = 2;
            int result = System.Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spUpdateProfileAdmin", param));
            string[] strField = new string[]
			{
				"Id"
			};
            XmlRpcStruct[] resultFound = isdnAPI.findByEmail(email, strField);
            if (resultFound.Length > 0)
            {
                int Id = System.Convert.ToInt32(resultFound[0]["Id"]);
                isdnAPI.dsUpdate("Contact", Id, new XmlRpcStruct
				{
					{
						"FirstName",
						fname
					},
					{
						"LastName",
						lname
					},
					{
						"OwnerID",
						"50036"
					},
					{
						"_StudentsManagersFirstName",
						managerFirstname
					},
					{
						"_StudentsManagersLastName",
						managerLastName
					},
					{
						"_StudentsManagersEmail",
						managerEmail
					},
					{
						"_CSBPassword",
						password
					},
					{
						"_YourOrganisation",
						Profile.GetOrganisation(agencyId)
					},
					{
						"_Role",
						Profile.GetJobRole(jobId)
					},
					{
						"_RoleLevel",
						Profile.GetJobLevel(jobLevel)
					},
					{
						"_Location",
						Profile.GetLocation(locationId)
					},
					{
						"_MegaCategory",
						Profile.GetGoodsLevel(goodsId)
					},
					{
						"_OrganisationDivision",
						division
					},
					{
						"JobTitle",
						position
					},
					{
						"_Phone1",
						phone
					}
				});
            }
        }

        public void MarkExpireProfile(bool isExpired)
        {
            SqlParameter[] param = new SqlParameter[3];
            int id = System.Convert.ToInt32(this.hdSelectIds.Value.Split(new char[]
			{
				','
			})[0]);
            param[0] = new SqlParameter("@isExpired", SqlDbType.VarChar);
            param[0].Value = isExpired;
            param[1] = new SqlParameter("@flag", SqlDbType.VarChar);
            param[1].Value = 1;
            param[2] = new SqlParameter("@userId", SqlDbType.Int);
            param[2].Value = id;
            int result = System.Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spUpdateProfileAdmin", param));
            DataTable dt = this.GetUserDetailByUserId(id.ToString());
            if (dt.Rows.Count > 0)
            {
                string subject = "";
                string body = "";
                SGACommon.GetEmailTemplate(9, ref subject, ref body);
                body = body.Replace("@v0", dt.Rows[0]["firstName"].ToString()).Replace("@v1", dt.Rows[0]["lastName"].ToString()).Replace("@v3", dt.Rows[0]["email"].ToString()).Replace("@v5", dt.Rows[0]["plainpassword"].ToString());
                MailSending.SendMail(ConfigurationManager.AppSettings["nameDisplay"].ToString(), ConfigurationManager.AppSettings["UserName"].ToString(), dt.Rows[0]["email"].ToString(), subject, body, "");
            }
        }

        protected void btnEditSaveProfile_Click(object sender, ImageClickEventArgs e)
        {
            this.UpdateProfile(this.txtEditFname.Text.Trim(), this.txtEditLname.Text.Trim(), System.Convert.ToInt32(this.ddlEditJobRole.SelectedValue), System.Convert.ToInt32(this.ddlEditJobLevel.SelectedValue), this.MEditfirstName.Text.Trim(), this.MEditlastName.Text.Trim(), this.MEditemail.Text.Trim(), System.Convert.ToInt32(this.ddlEditAgency.SelectedValue), this.txtEditPhone.Text.Trim(), this.txtDivision.Text.Trim(), System.Convert.ToInt32(this.ddlEditLocation.SelectedValue), this.txtEditPosition.Value.Trim(), System.Convert.ToInt32(this.ddlEditGoods.SelectedValue), this.txtEditPassword.Text.Trim(), System.Convert.ToDateTime(this.txtEditExiryDate.Text), false, this.txtEditEmailAddress.Text.Trim());
            this.LoadProfile();
            this.BindGrid();
        }

        protected void btnEditProfileExpire_Click(object sender, ImageClickEventArgs e)
        {
            this.MarkExpireProfile(true);
            this.LoadProfile();
        }

        protected void btnExport_Click(object sender, System.EventArgs e)
        {
            DataSet DS = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spExcelExportUsers", new SqlParameter[]
			{
				new SqlParameter("@firstname", this.txtFname.Value.Trim()),
				new SqlParameter("@lastname", this.txtLname.Value.Trim()),
				new SqlParameter("@email", this.txtEmail.Value.Trim()),
				new SqlParameter("@dateFrom", this.txtFrom.Text.Trim()),
				new SqlParameter("@dateTo", this.txtTo.Text.Trim()),
				new SqlParameter("@userCondition", this.ddlOrder.SelectedValue),
				new SqlParameter("@orderBy", " id desc ")
			});
            this.ExportDataSetToExcel(DS, "UsersList.xls");
        }

        public void ExportDataSetToExcel(DataSet ds, string filename)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.Charset = "";
            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    DataGrid dg = new DataGrid();
                    dg.DataSource = ds.Tables[0];
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString().Replace("`", "'"));
                    response.End();
                }
            }
        }

        protected void grdUsers_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            this.grdUsers.CurrentPageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        protected void grdUsers_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                System.DateTime dtRegisterdate = System.Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "dtInsertDate"));
                string strLoginDt = DataBinder.Eval(e.Item.DataItem, "lastLoginDt").ToString();
                Label lblLastlogin = (Label)e.Item.FindControl("lblLastlogin");
                Label lblRegisterDate = (Label)e.Item.FindControl("lblRegisterDate");
                Label lblStatus = (Label)e.Item.FindControl("lblStatus");
                if (lblLastlogin != null && strLoginDt.Length > 0)
                {
                    lblLastlogin.Text = SGACommon.ToAusTimeZone(System.Convert.ToDateTime(strLoginDt)).ToString("dd MMM yyyy HH:mm tt");
                }
                if (lblRegisterDate != null)
                {
                    lblRegisterDate.Text = SGACommon.ToAusTimeZone(dtRegisterdate).ToString("dd MMM yyyy");
                }
                ImageButton iBtnApprove = (ImageButton)e.Item.FindControl("iBtnApprove");
                ImageButton IbtnResend = (ImageButton)e.Item.FindControl("IbtnResend");
                ImageButton iBtnSelect = (ImageButton)e.Item.FindControl("iBtnSelect");
                if (System.Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "IsApproved")))
                {
                    if (System.Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "expired")))
                    {
                        iBtnApprove.Visible = false;
                        IbtnResend.Visible = false;
                        lblStatus.Text = "Expired";
                    }
                    else
                    {
                        iBtnApprove.CommandName = "disapprove";
                        iBtnApprove.ToolTip = "Disapprove user";
                        iBtnApprove.ImageUrl = this.Page.ResolveUrl("~/webadmin/images/disap.png");
                        lblStatus.Text = "Approved";
                    }
                }
                else
                {
                    e.Item.BackColor = Color.FromArgb(133, 195, 233);
                    iBtnApprove.CommandName = "approve";
                    iBtnApprove.ToolTip = "Approve user";
                    iBtnApprove.ImageUrl = this.Page.ResolveUrl("~/webadmin/images/approve.png");
                    lblStatus.Text = "Not Approved";
                }
                string[] strArr = this.hdSelectIds.Value.Split(new char[]
				{
					','
				});
                if (strArr.Length > 0)
                {
                    for (int i = 0; i < strArr.Length; i++)
                    {
                        if (strArr[i].ToString() == DataBinder.Eval(e.Item.DataItem, "Id").ToString())
                        {
                            e.Item.BackColor = Color.FromArgb(252, 136, 113);
                            iBtnSelect.ImageUrl = this.Page.ResolveUrl("~/webadmin/images/unselect-icon.png");
                            iBtnSelect.CommandName = "deselect";
                        }
                    }
                }
            }
        }

        protected void grdUsers_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "delete")
            {
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spDeleteUser", new SqlParameter[]
				{
					new SqlParameter("@userId", e.CommandArgument)
				});
                this.BindGrid();
            }
            if (e.CommandName.ToLower() == "reminder")
            {
                DataTable dt = this.GetUserDetailByUserId(e.CommandArgument.ToString());
                if (dt.Rows.Count > 0)
                {
                    string subject = "";
                    string body = "";
                    SGACommon.GetEmailTemplate(14, ref subject, ref body);
                    body = body.Replace("@v0", dt.Rows[0]["firstName"].ToString()).Replace("@v3", dt.Rows[0]["email"].ToString()).Replace("@v5", dt.Rows[0]["plainpassword"].ToString());
                    subject = subject.Replace("@v0", dt.Rows[0]["firstName"].ToString());
                    MailSending.SendMail(ConfigurationManager.AppSettings["nameDisplay"].ToString(), ConfigurationManager.AppSettings["UserName"].ToString(), dt.Rows[0]["email"].ToString(), subject, body, "");
                }
            }
            else if (e.CommandName == "select")
            {
                HiddenField expr_1C8 = this.hdSelectIds;
                expr_1C8.Value = expr_1C8.Value + e.CommandArgument + ",";
                this.BindGrid();
                this.dtgList.CurrentPageIndex = 0;
                this.BindPermission();
                this.grdUserTest.CurrentPageIndex = 0;
                this.BindUserTestInfo();
                this.grdSSA.CurrentPageIndex = 0;
                this.BindSSATest();
                this.grdBA.CurrentPageIndex = 0;
                this.BindBATest();
                this.grdCMA.CurrentPageIndex = 0;
                this.BindCMATest();
                this.grdNP.CurrentPageIndex = 0;
                this.BindNPTest();
                this.LoadProfile();
            }
            else if (e.CommandName == "deselect")
            {
                System.Collections.Generic.List<string> Items = (from i in this.hdSelectIds.Value.Split(new char[]
				{
					','
				})
                                                                 select i.Trim() into i
                                                                 where i != string.Empty
                                                                 select i).ToList<string>();
                Items.Remove(e.CommandArgument.ToString());
                this.hdSelectIds.Value = string.Join(",", Items.ToArray());
                this.BindGrid();
                this.BindPermission();
                this.BindUserTestInfo();
                this.BindSSATest();
                this.BindBATest();
                this.BindCMATest();
                this.BindNPTest();
            }
            else if (e.CommandName == "send")
            {
                DataTable dt = this.GetUserDetailByUserId(e.CommandArgument.ToString());
                if (dt.Rows.Count > 0)
                {
                    string subject = "";
                    string body = "";
                    SGACommon.GetEmailTemplate(7, ref subject, ref body);
                    body = body.Replace("@v0", dt.Rows[0]["firstName"].ToString()).Replace("@v1", dt.Rows[0]["lastName"].ToString()).Replace("@v3", dt.Rows[0]["email"].ToString()).Replace("@v5", dt.Rows[0]["plainpassword"].ToString());
                    MailSending.SendMail(ConfigurationManager.AppSettings["nameDisplay"].ToString(), ConfigurationManager.AppSettings["UserName"].ToString(), dt.Rows[0]["email"].ToString(), subject, body, "");
                }
            }
            else if (e.CommandName.ToLower() == "approve")
            {
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUserStatus", new SqlParameter[]
				{
					new SqlParameter("@userId", e.CommandArgument)
				});
                DataTable dt = this.GetUserDetailByUserId(e.CommandArgument.ToString());
                if (dt.Rows.Count > 0)
                {
                    string subject = "";
                    string body = "";
                    SGACommon.GetEmailTemplate(7, ref subject, ref body);
                    body = body.Replace("@v0", dt.Rows[0]["firstName"].ToString()).Replace("@v1", dt.Rows[0]["lastName"].ToString()).Replace("@v3", dt.Rows[0]["email"].ToString()).Replace("@v5", dt.Rows[0]["plainpassword"].ToString()).Replace("@v4", Profile.GetJobRole(System.Convert.ToInt32(dt.Rows[0]["jobRole"].ToString())));
                    MailSending.SendMail(ConfigurationManager.AppSettings["nameDisplay"].ToString(), ConfigurationManager.AppSettings["UserName"].ToString(), dt.Rows[0]["email"].ToString(), subject, body, "");
                }
                this.BindGrid();
            }
            else if (e.CommandName.ToLower() == "disapprove")
            {
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUserStatus", new SqlParameter[]
				{
					new SqlParameter("@userId", e.CommandArgument)
				});
                DataTable dt = this.GetUserDetailByUserId(e.CommandArgument.ToString());
                if (dt.Rows.Count > 0)
                {
                    string subject = "";
                    string body = "";
                    SGACommon.GetEmailTemplate(9, ref subject, ref body);
                    body = body.Replace("@v0", dt.Rows[0]["firstName"].ToString()).Replace("@v1", dt.Rows[0]["lastName"].ToString()).Replace("@v3", dt.Rows[0]["email"].ToString()).Replace("@v5", dt.Rows[0]["plainpassword"].ToString());
                    MailSending.SendMail(ConfigurationManager.AppSettings["nameDisplay"].ToString(), ConfigurationManager.AppSettings["UserName"].ToString(), dt.Rows[0]["email"].ToString(), subject, body, "");
                }
                this.BindGrid();
            }
        }

        protected void grdUsers_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            if (e.SortExpression.ToString() == this.SortExpression)
            {
                this.SortOrder = !this.SortOrder;
            }
            else
            {
                this.SortOrder = true;
            }
            this.SortExpression = e.SortExpression;
            int i = 0;
            string strOrder = this.SortOrder ? "ASC" : "DESC";
            foreach (DataGridColumn col in this.grdUsers.Columns)
            {
                if (col.SortExpression == e.SortExpression)
                {
                    this.grdUsers.Columns[i].HeaderStyle.CssClass = "gridHeaderSort" + strOrder;
                }
                else
                {
                    this.grdUsers.Columns[i].HeaderStyle.CssClass = "gridHeader";
                }
                i++;
            }
            this.BindGrid();
        }

        protected void grdSSA_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            if (e.SortExpression.ToString() == this.SortExpressionSSA)
            {
                this.SortOrderSSA = !this.SortOrderSSA;
            }
            else
            {
                this.SortOrderSSA = true;
            }
            this.SortExpressionSSA = e.SortExpression;
            int i = 0;
            string strOrder = this.SortOrderSSA ? "ASC" : "DESC";
            foreach (DataGridColumn col in this.grdSSA.Columns)
            {
                if (col.SortExpression == e.SortExpression)
                {
                    this.grdSSA.Columns[i].HeaderStyle.CssClass = "gridHeaderSort" + strOrder;
                }
                else
                {
                    this.grdSSA.Columns[i].HeaderStyle.CssClass = "gridHeader";
                }
                i++;
            }
            this.BindSSATest();
        }

        protected void grdBA_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            if (e.SortExpression.ToString() == this.SortExpressionBA)
            {
                this.SortOrderBA = !this.SortOrderBA;
            }
            else
            {
                this.SortOrderBA = true;
            }
            this.SortExpressionBA = e.SortExpression;
            int i = 0;
            string strOrder = this.SortOrderSSA ? "ASC" : "DESC";
            foreach (DataGridColumn col in this.grdBA.Columns)
            {
                if (col.SortExpression == e.SortExpression)
                {
                    this.grdBA.Columns[i].HeaderStyle.CssClass = "gridHeaderSort" + strOrder;
                }
                else
                {
                    this.grdBA.Columns[i].HeaderStyle.CssClass = "gridHeader";
                }
                i++;
            }
            this.BindBATest();
        }

        protected void grdCMA_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            if (e.SortExpression.ToString() == this.SortExpressionCMA)
            {
                this.SortOrderCMA = !this.SortOrderCMA;
            }
            else
            {
                this.SortOrderCMA = true;
            }
            this.SortExpressionCMA = e.SortExpression;
            int i = 0;
            string strOrder = this.SortOrderCMA ? "ASC" : "DESC";
            foreach (DataGridColumn col in this.grdCMA.Columns)
            {
                if (col.SortExpression == e.SortExpression)
                {
                    this.grdCMA.Columns[i].HeaderStyle.CssClass = "gridHeaderSort" + strOrder;
                }
                else
                {
                    this.grdCMA.Columns[i].HeaderStyle.CssClass = "gridHeader";
                }
                i++;
            }
            this.BindCMATest();
        }

        protected void grdNP_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            if (e.SortExpression.ToString() == this.SortExpressionNP)
            {
                this.SortOrderNP = !this.SortOrderNP;
            }
            else
            {
                this.SortOrderNP = true;
            }
            this.SortExpressionNP = e.SortExpression;
            int i = 0;
            string strOrder = this.SortOrderNP ? "ASC" : "DESC";
            foreach (DataGridColumn col in this.grdNP.Columns)
            {
                if (col.SortExpression == e.SortExpression)
                {
                    this.grdNP.Columns[i].HeaderStyle.CssClass = "gridHeaderSort" + strOrder;
                }
                else
                {
                    this.grdNP.Columns[i].HeaderStyle.CssClass = "gridHeader";
                }
                i++;
            }
            this.BindNPTest();
        }

        protected void lnkSSADownload_Click(object sender, System.EventArgs e)
        {
            base.Response.Redirect(string.Concat(new string[]
			{
				"DownloadReport.aspx?id=2&fname=",
				this.txtSSAFname.Value,
				"&lname=",
				this.txtSSALname.Value,
				"&dfrom=",
				this.txtSSAFrom.Text,
				"&dto=",
				this.txtSSATo.Text,
				"&userIds=",
				this.hdSelectIds.Value
			}));
        }

        protected void lnkBADownload_Click(object sender, System.EventArgs e)
        {
            base.Response.Redirect(string.Concat(new string[]
			{
				"DownloadReport.aspx?id=3&fname=",
				this.txtBAFname.Value,
				"&lname=",
				this.txtBALname.Value,
				"&dfrom=",
				this.txtBAFrom.Text,
				"&dto=",
				this.txtBATo.Text,
				"&userIds=",
				this.hdSelectIds.Value
			}));
        }

        protected void lnkNPDownload_Click(object sender, System.EventArgs e)
        {
            base.Response.Redirect(string.Concat(new string[]
			{
				"DownloadReport.aspx?id=5&fname=",
				this.txtNPFname.Value,
				"&lname=",
				this.txtNPLname.Value,
				"&dfrom=",
				this.txtNPFrom.Text,
				"&dto=",
				this.txtNPTo.Text,
				"&userIds=",
				this.hdSelectIds.Value
			}));
        }
    }
}