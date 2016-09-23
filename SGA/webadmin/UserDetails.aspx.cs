using AjaxControlToolkit;
using CookComputing.XmlRpc;
using DataTier;
using InfusionSoftDotNet;
using SGA.App_Code;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SGA.webadmin
{
    public partial class UserDetails : Page
    {
        private int userId = 0;

        

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (base.Request.QueryString["Id"] != null)
            {
                this.userId = System.Convert.ToInt32(base.Request.QueryString["Id"].ToString());
            }
            if (!base.IsPostBack)
            {
                if (this.userId > 0)
                {
                    this.LoadProfile();
                    this.BindPermission();
                    this.BindSSATest();
                    this.BindSGATest();
                    this.BindCMATest();
                    this.BindCMKTest();
                    this.BindCAATest();
                }
            }
        }

        

        private void BindPermission()
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetUserResult", new SqlParameter[]
			{
				new SqlParameter("@userIds", this.userId)
			});
            this.dtgPermission.DataSource = ds;
            this.dtgPermission.DataBind();
        }

        private void LoadProfile()
        {
            SqlParameter[] param = new SqlParameter[]
			{
				new SqlParameter("@userId", SqlDbType.Int)
			};
            param[0].Value = this.userId;
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetProfileDetailsAdmin", param);
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                   
                    this.txtEditFname.Text = ds.Tables[0].Rows[0]["firstname"].ToString();
                    this.txtEditLname.Text = ds.Tables[0].Rows[0]["lastname"].ToString();
                    this.txtEditPassword.Text = ds.Tables[0].Rows[0]["plainpassword"].ToString();
                    this.txtEditPhone.Text = ((ds.Tables[0].Rows[0]["Phone"].ToString().Length > 0) ? ds.Tables[0].Rows[0]["Phone"].ToString() : "");
                    this.txtEditExiryDate.Text = System.Convert.ToDateTime(ds.Tables[0].Rows[0]["expiryDate"]).ToShortDateString();
                   
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
                    SGACommon.SelectListItem(this.ddlExperience, ds.Tables[0].Rows[0]["experience"].ToString());
                    SGACommon.SelectListItem(this.ddlNature, ds.Tables[0].Rows[0]["nature"].ToString());
                    this.txtEditEmailAddress.Text = ds.Tables[0].Rows[0]["email"].ToString();
                    this.lblEditStatus.Text = (System.Convert.ToBoolean(ds.Tables[0].Rows[0]["expired"]) ? "Expired" : "Running");
                }
            }
        }

        public void UpdateProfile(string fname, string lname, int agencyId, string phone, int experience, int nature, string password, System.DateTime dtExpiryDate, bool isExpired, string email)
        {
            string passwordSalt = SGACommon.CreateSalt(5);
            string passwordHash = SGACommon.CreatePasswordHash(password, passwordSalt);
            SqlParameter[] param = new SqlParameter[13];
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
            param[5] = new SqlParameter("@agencyId", SqlDbType.Int);
            param[5].Value = agencyId;
            param[6] = new SqlParameter("@phone", SqlDbType.VarChar);
            param[6].Value = phone;
            param[7] = new SqlParameter("@nature", SqlDbType.VarChar);
            param[7].Value = nature;
            param[8] = new SqlParameter("@experience", SqlDbType.VarChar);
            param[8].Value = experience;
            param[9] = new SqlParameter("@userId", SqlDbType.Int);
            param[9].Value = this.userId;           
            param[10] = new SqlParameter("@expiryDate", SqlDbType.DateTime);
            param[10].Value = dtExpiryDate;
            param[11] = new SqlParameter("@isExpired", SqlDbType.VarChar);
            param[11].Value = isExpired;
            param[12] = new SqlParameter("@flag", SqlDbType.Int);
            param[12].Value = 2;
            int result = System.Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spUpdateProfileAdmin", param));
            string[] strField = new string[]
			{
				"Id"
			};
    //        XmlRpcStruct[] resultFound = isdnAPI.findByEmail(SGACommon.LoginUserInfo.name, strField);
    //        if (resultFound.Length > 0)
    //        {
    //            int Id = System.Convert.ToInt32(resultFound[0]["Id"]);
    //            isdnAPI.dsUpdate("Contact", Id, new XmlRpcStruct
				//{
				//	{
				//		"FirstName",
				//		fname
				//	},
				//	{
				//		"LastName",
				//		lname
				//	},
				//	{
				//		"OwnerID",
				//		"50036"
				//	},
				//	{
				//		"_StudentsManagersFirstName",
				//		managerFirstname
				//	},
				//	{
				//		"_StudentsManagersLastName",
				//		managerLastName
				//	},
				//	{
				//		"_StudentsManagersEmail",
				//		managerEmail
				//	},
				//	{
				//		"_CSBPassword",
				//		password
				//	},
				//	{
				//		"_YourOrganisation",
				//		Profile.GetOrganisation(agencyId)
				//	},
				//	{
				//		"_Role",
				//		Profile.GetJobRole(jobId)
				//	},
				//	{
				//		"_RoleLevel",
				//		Profile.GetJobLevel(jobLevel)
				//	},
				//	{
				//		"_Location",
				//		Profile.GetLocation(locationId)
				//	},
				//	{
				//		"_MegaCategory",
				//		Profile.GetGoodsLevel(goodsId)
				//	},
				//	{
				//		"_OrganisationDivision",
				//		division
				//	},
				//	{
				//		"JobTitle",
				//		position
				//	},
				//	{
				//		"_Phone1",
				//		phone
				//	}
				//});
    //        }
        }

        public void MarkExpireProfile(bool isExpired)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@isExpired", SqlDbType.VarChar);
            param[0].Value = isExpired;
            param[1] = new SqlParameter("@flag", SqlDbType.VarChar);
            param[1].Value = 1;
            param[2] = new SqlParameter("@userId", SqlDbType.Int);
            param[2].Value = this.userId;
            int result = System.Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spUpdateProfileAdmin", param));
            DataTable dt = this.GetUserDetailByUserId(this.userId);
            if (dt.Rows.Count > 0)
            {
                string subject = "";
                string body = "";
                SGACommon.GetEmailTemplate(9, ref subject, ref body);
                body = body.Replace("@v0", dt.Rows[0]["firstName"].ToString()).Replace("@v1", dt.Rows[0]["lastName"].ToString()).Replace("@v2", dt.Rows[0]["company"].ToString()).Replace("@v3", dt.Rows[0]["email"].ToString()).Replace("@v5", dt.Rows[0]["plainpassword"].ToString());
                MailSending.SendMail(ConfigurationManager.AppSettings["nameDisplay"].ToString(), ConfigurationManager.AppSettings["UserName"].ToString(), dt.Rows[0]["email"].ToString(), subject, body, "");
            }
        }

        protected void btnEditSaveProfile_Click(object sender, ImageClickEventArgs e)
        {
            //this.UpdateProfile(this.txtEditFname.Text.Trim(), this.txtEditLname.Text.Trim(), System.Convert.ToInt32(this.ddlEditJobRole.SelectedValue), System.Convert.ToInt32(this.ddlEditJobLevel.SelectedValue),System.Convert.ToInt32(this.ddlEditAgency.SelectedValue), this.txtEditPhone.Text.Trim(), this.txtDivision.Text.Trim(), System.Convert.ToInt32(this.ddlEditLocation.SelectedValue), this.txtEditPosition.Value.Trim(), System.Convert.ToInt32(this.ddlEditGoods.SelectedValue), this.txtEditPassword.Text.Trim(), System.Convert.ToDateTime(this.txtEditExiryDate.Text), false, this.txtEditEmailAddress.Text.Trim());
            this.UpdateProfile(this.txtEditFname.Text.Trim(), this.txtEditLname.Text.Trim(), System.Convert.ToInt32(this.ddlEditAgency.SelectedValue), txtEditPhone.Text, System.Convert.ToInt32(this.ddlExperience.SelectedValue), System.Convert.ToInt32(this.ddlNature.SelectedValue), txtEditPassword.Text.Trim(), Convert.ToDateTime(txtEditExiryDate.Text.Trim()), false, this.txtEditEmailAddress.Text.Trim());
            this.LoadProfile();
        }

        protected void btnEditProfileExpire_Click(object sender, ImageClickEventArgs e)
        {
            this.MarkExpireProfile(true);
            this.LoadProfile();
        }

        protected void ibtnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            foreach (DataGridItem item in this.dtgPermission.Items)
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

        public DataTable GetUserDetailByUserId(int userId)
        {
            return SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetProfileDetails", new SqlParameter[]
			{
				new SqlParameter("@userId", userId)
			}).Tables[0];
        }

        private void BindSSATest()
        {
            string strOrderBy = " testId desc  ";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageSSATest", new SqlParameter[]
			{
				new SqlParameter("@firstname", ""),
				new SqlParameter("@lastname", ""),
				new SqlParameter("@userIds", this.userId),
				new SqlParameter("@dateFrom", ""),
				new SqlParameter("@dateTo", ""),
				new SqlParameter("@orderBy", strOrderBy)
			});
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

        private void BindSGATest()
        {
            string strOrderBy = " testId desc  ";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageSGATest", new SqlParameter[]
            {
                new SqlParameter("@firstname", ""),
                new SqlParameter("@lastname", ""),
                new SqlParameter("@userIds", this.userId),
                new SqlParameter("@dateFrom", ""),
                new SqlParameter("@dateTo", ""),
                new SqlParameter("@orderBy", strOrderBy)
            });
            this.grdSga.DataSource = ds;
            this.grdSga.DataBind();
        }

        protected void grdSga_ItemDataBound(object sender, DataGridItemEventArgs e)
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

        protected void grdSga_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spDeleteSGATest", new SqlParameter[]
                {
                    new SqlParameter("@testId", e.CommandArgument)
                });
                this.BindSGATest();
            }
            else if (e.CommandName == "Graph")
            {
                base.Response.Redirect("TestChartSGA.aspx?id=" + e.CommandArgument, false);
            }
            else if (e.CommandName == "drilldown")
            {
                base.Response.Redirect("/SSAChart/" + e.CommandArgument, false);
            }
            else if (e.CommandName == "Edit")
            {
                base.Response.Redirect("EditSGAtest.aspx?id=" + e.CommandArgument, false);
            }
        }

        private void BindCMATest()
        {
            string strOrderBy = " testId desc  ";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageCMATest", new SqlParameter[]
			{
				new SqlParameter("@firstname", ""),
				new SqlParameter("@lastname", ""),
				new SqlParameter("@userIds", this.userId),
				new SqlParameter("@dateFrom", ""),
				new SqlParameter("@dateTo", ""),
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

        private void BindCMKTest()
        {
            string strOrderBy = " testId desc  ";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageCMKTest", new SqlParameter[]
            {
                new SqlParameter("@firstname", ""),
                new SqlParameter("@lastname", ""),
                new SqlParameter("@userIds", this.userId),
                new SqlParameter("@dateFrom", ""),
                new SqlParameter("@dateTo", ""),
                new SqlParameter("@orderBy", strOrderBy)
            });
            this.grdCMK.DataSource = ds;
            this.grdCMK.DataBind();
        }

        protected void grdCMK_ItemDataBound(object sender, DataGridItemEventArgs e)
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

        protected void grdCMK_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spDeleteCMKTest", new SqlParameter[]
                {
                    new SqlParameter("@testId", e.CommandArgument)
                });
                this.BindCMKTest();
            }
            else if (e.CommandName == "Graph")
            {
                base.Response.Redirect("TestChartCMK.aspx?id=" + e.CommandArgument, false);
            }
            else if (e.CommandName == "drilldown")
            {
                base.Response.Redirect("/CMKChart/" + e.CommandArgument, false);
            }
            else if (e.CommandName == "Edit")
            {
                base.Response.Redirect("EditCMKtest.aspx?id=" + e.CommandArgument, false);
            }
        }

        private void BindCAATest()
        {
            string strOrderBy = " testId desc  ";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageCAATest", new SqlParameter[]
            {
                new SqlParameter("@firstname", ""),
                new SqlParameter("@lastname", ""),
                new SqlParameter("@userIds", this.userId),
                new SqlParameter("@dateFrom", ""),
                new SqlParameter("@dateTo", ""),
                new SqlParameter("@orderBy", strOrderBy)
            });
            this.grdCAA.DataSource = ds;
            this.grdCAA.DataBind();
        }

        protected void grdCAA_ItemDataBound(object sender, DataGridItemEventArgs e)
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

        protected void grdCAA_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spDeleteCAATest", new SqlParameter[]
                {
                    new SqlParameter("@testId", e.CommandArgument)
                });
                this.BindCAATest();
            }
            else if (e.CommandName == "Graph")
            {
                base.Response.Redirect("TestChartCAA.aspx?id=" + e.CommandArgument, false);
            }
            else if (e.CommandName == "drilldown")
            {
                base.Response.Redirect("/CMKChart/" + e.CommandArgument, false);
            }
            else if (e.CommandName == "Edit")
            {
                base.Response.Redirect("EditCAAtest.aspx?id=" + e.CommandArgument, false);
            }
        }


    }
}
