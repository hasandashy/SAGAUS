using CookComputing.XmlRpc;
using DataTier;
using InfusionSoftDotNet;
using SGA.App_Code;
using SGA.controls;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SGA.webadmin
{
    public partial class CompanyManagers : Page
    {
        

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

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.BindCompany();
                this.BindGrid();
            }
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
				new SqlParameter("@firstname", ""),
				new SqlParameter("@lastname", ""),
				new SqlParameter("@email", ""),
				new SqlParameter("@company", ""),
				new SqlParameter("@dateFrom", ""),
				new SqlParameter("@dateTo", ""),
				new SqlParameter("@userCondition", SqlDbType.BigInt),
				new SqlParameter("@orderBy", strOrderBy),
				new SqlParameter("@roleId", 3)
			});
            this.grdUsers.DataSource = ds;
            this.grdUsers.DataBind();
        }

        private void BindCompany()
        {
            this.ddlCompany.DataSource = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetCompany");
            this.ddlCompany.DataTextField = "company";
            this.ddlCompany.DataValueField = "company";
            this.ddlCompany.DataBind();
            SGACommon.InsertDefaultItem(this.ddlCompany, "Select company", "0");
        }

        protected void btnAddnew_Click(object sender, System.EventArgs e)
        {
            this.pnlAdd.Visible = true;
            this.pnlList.Visible = false;
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
                    string passwordSalt = SGACommon.CreateSalt(5);
                    string passwordHash = SGACommon.CreatePasswordHash(this.txtPassword.Value.Trim(), passwordSalt);
                    param = new SqlParameter[11];
                    param[0] = new SqlParameter("@action", SqlDbType.VarChar);
                    param[0].Value = "Insert";
                    param[1] = new SqlParameter("@password", SqlDbType.VarChar);
                    param[1].Value = this.txtPassword.Value.Trim();
                    param[2] = new SqlParameter("@company", SqlDbType.VarChar);
                    param[2].Value = this.ddlCompany.SelectedValue;
                    param[3] = new SqlParameter("@firstName", SqlDbType.VarChar);
                    param[3].Value = this.txtFirstname.Value.Trim();
                    param[4] = new SqlParameter("@lastName", SqlDbType.VarChar);
                    param[4].Value = this.txtLastname.Value.Trim();
                    param[5] = new SqlParameter("@email", SqlDbType.VarChar);
                    param[5].Value = this.txtEmailAddress.Value.Trim();
                    param[6] = new SqlParameter("@isApproved", SqlDbType.Bit);
                    param[6].Value = 1;
                    param[7] = new SqlParameter("@passwordHash", SqlDbType.VarChar);
                    param[7].Value = passwordHash;
                    param[8] = new SqlParameter("@passwordSalt", SqlDbType.VarChar);
                    param[8].Value = passwordSalt;
                    param[9] = new SqlParameter("@jobRole", SqlDbType.Int);
                    param[9].Value = 0;
                    param[10] = new SqlParameter("@isAdminAdded", SqlDbType.Bit);
                    param[10].Value = true;
                    int result = System.Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spManagerMaster", param));
                    if (result == 0)
                    {
                        this.lblError.Text = "There was some error with user saving, try again.";
                    }
                    else
                    {
                        this.BindGrid();
                        this.pnlAdd.Visible = false;
                        this.pnlList.Visible = true;
                        string[] strField = new string[]
						{
							"Id"
						};
                        XmlRpcStruct[] resultFound = isdnAPI.findByEmail(this.txtEmailAddress.Value.Trim(), strField);
                        int userId;
                        if (resultFound.Length > 0)
                        {
                            userId = System.Convert.ToInt32(resultFound[0]["Id"].ToString());
                            isdnAPI.addToGroup(userId, 104);
                        }
                        else
                        {
                            userId = isdnAPI.add(new XmlRpcStruct
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
									"JobTitle",
									""
								},
								{
									"Email",
									this.txtEmailAddress.Value.Trim()
								},
								{
									"Company",
									this.ddlCompany.SelectedValue
								},
								{
									"_Yourlevel",
									""
								},
								{
									"LeadSourceId",
									"22"
								},
								{
									"OwnerID",
									"6"
								},
								{
									"ContactType",
									"Customer"
								}
							});
                            if (userId > 0)
                            {
                                bool isAdded = isdnAPI.addToGroup(userId, 104);
                            }
                        }
                        XmlRpcStruct MailContent = new XmlRpcStruct();
                        MailContent = isdnAPI.getEmailTemplate(3396);
                        string subject = MailContent["subject"].ToString();
                        string fromAddress = MailContent["fromAddress"].ToString();
                        string htmlBody = MailContent["htmlBody"].ToString();
                        isdnAPI.dsAdd("Lead", new XmlRpcStruct
						{
							{
								"OpportunityTitle",
								string.Concat(new string[]
								{
									this.ddlCompany.SelectedValue,
									" - ",
									this.txtFirstname.Value.Trim(),
									" ",
									this.txtLastname.Value.Trim(),
									" - Register SkillsGapAnlaysis"
								})
							},
							{
								"ContactID",
								userId
							},
							{
								"StageID",
								22
							},
							{
								"UserID",
								6
							},
							{
								"OpportunityNotes",
								"Please follow up contact"
							},
							{
								"NextActionDate",
								System.DateTime.Now.AddDays(7.0).ToString()
							}
						});
                        string htmlSignature = "<div><div><p></p><p><font face=\"arial,helvetica,sans-serif\" size=\"2\"><b>Ben Shute, FCIPS<br></b><b>Chief Executive Officer</b></font></p><p></p><p><u>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</u></p><p></p><p>Comprara Pty Ltd<br><b>T:</b>&nbsp;+ 61 (03) 8606 0379<br><b>E:</b>&nbsp;<a href=\"mailto:info@comprara.com.au\" target=\"_blank\">info@comprara.com.au<br></a>Melbourne 3000,&nbsp;VIC, Australia</p><p><span style=\"font-family: arial,helvetica,sans-serif; font-size: 12px;\"><img height=\"58\" src=\"https://d1yoaun8syyxxt.cloudfront.net/gn231-c1bd9d0a-bec3-4ce0-ad19-b3d8195e6914-v2\" width=\"221\"></span></p></div></div>";
                        string address = "<div><div><p class=\"MsoNormal\"><span style=\"font-size:7.5pt;font-family:'Verdana,sans-serif';color:black\">Comprara L11, 356 Collins Street Melbourne, Victoria 3000 Australia +61 3 9505 3276 </span></p></div></div>";
                        MailSending.SendMail(ConfigurationManager.AppSettings["nameDisplay"].ToString(), fromAddress, this.txtEmailAddress.Value.Trim(), subject, htmlBody.Replace("~Contact.FirstName~", this.txtFirstname.Value.Trim()).Replace("~User_1.HTMLSignature~", htmlSignature).Replace("~Company.HTMLCanSpamAddressBlock~", address), "");
                        subject = "New user registration email";
                        System.IO.StreamReader objStreamReader = System.IO.File.OpenText(HttpContext.Current.Server.MapPath("~/css/register.htm"));
                        string content = objStreamReader.ReadToEnd();
                        objStreamReader.Close();
                        objStreamReader.Dispose();
                        content = content.Replace("@firstname", this.txtFirstname.Value.Trim()).Replace("@lastname", this.txtLastname.Value.Trim()).Replace("@email", this.txtEmailAddress.Value.Trim()).Replace("@company", this.ddlCompany.SelectedValue).Replace("@jobrole", "").Replace("@ip", HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
                        MailSending.SendMail(ConfigurationManager.AppSettings["nameDisplay"].ToString(), fromAddress, ConfigurationManager.AppSettings["infusionTo"].ToString(), subject, content, ConfigurationManager.AppSettings["infusionCC"].ToString());
                        this.lblError.Text = "Company Manager added successfully and email sent to him.";
                        this.txtFirstname.Value = "";
                        this.txtLastname.Value = "";
                        this.txtEmailAddress.Value = "";
                        this.txtPassword.Value = "";
                        this.ddlCompany.SelectedIndex = -1;
                    }
                }
                catch (System.Exception ex_6FE)
                {
                    this.lblError.Text = "There was some error with user saving, try again.";
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
                if (System.Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "IsApproved")))
                {
                    if (System.Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "expired")))
                    {
                        lblStatus.Text = "Expired";
                    }
                    else
                    {
                        lblStatus.Text = "Approved";
                    }
                }
                else
                {
                    e.Item.BackColor = Color.FromArgb(133, 195, 233);
                    lblStatus.Text = "Not Approved";
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
    }
}
