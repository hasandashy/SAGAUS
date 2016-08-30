using DataTier;
using SGA.App_Code;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.manager
{
    public partial class editResult : Page
    {
        protected string _user;

        

        public DataSet dsOptions
        {
            get
            {
                return (DataSet)this.ViewState["dsOptions"];
            }
            set
            {
                this.ViewState["dsOptions"] = value;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            base.ClientScript.RegisterStartupScript(this.Page.GetType(), "abc", "$(document).ready(function(){\r\nStyleRadio();\r\n});", true);
            if (!base.IsPostBack)
            {
                this.BindUsers();
            }
        }

        private void BindUsers()
        {
            SqlParameter[] param = new SqlParameter[]
			{
				new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
			};
            this.ddlUsers.DataSource = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetUsersByCompany", param);
            this.ddlUsers.DataBind();
        }

        private void BindGrid()
        {
            if (this.ddlAssessmentType.SelectedValue == "0")
            {
                this.lblError.Text = "Select assessment type";
            }
            else if (this.ddlTopics.SelectedValue == "0")
            {
                this.lblError.Text = "Select topics";
            }
            else if (base.Request["user"] == null)
            {
                this.lblError.Text = "Select at least a user to edit";
            }
            else
            {
                this._user = base.Request["user"].ToString();
                this.dsOptions = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetTestOptions", new SqlParameter[]
				{
					new SqlParameter("@type", System.Convert.ToInt32(this.ddlAssessmentType.SelectedValue))
				});
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetEditUserQuestions", new SqlParameter[]
				{
					new SqlParameter("@topicId", System.Convert.ToInt32(this.ddlTopics.SelectedValue)),
					new SqlParameter("@userIds", base.Request["user"].ToString()),
					new SqlParameter("@type", System.Convert.ToInt32(this.ddlAssessmentType.SelectedValue)),
					new SqlParameter("@jobroleId", System.Convert.ToInt32(this.ddlJobRole.SelectedValue))
				});
                this.grdQuestions.DataSource = ds;
                this.grdQuestions.DataBind();
                this.grdQuestions.Visible = (this.grdQuestions.Items.Count > 0);
                this.lblError.Text = ((this.grdQuestions.Items.Count > 0) ? "" : "No result found for search critria");
            }
        }

        protected void ddlAssessmentType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (base.Request["user"] != null)
            {
                this._user = base.Request["user"].ToString();
            }
            this.ddlTopics.Items.Clear();
            if (System.Convert.ToInt32(this.ddlAssessmentType.SelectedValue) > 0)
            {
                SqlParameter[] param = new SqlParameter[]
				{
					new SqlParameter("@assessmentType", System.Convert.ToInt32(this.ddlAssessmentType.SelectedValue))
				};
                this.ddlTopics.DataSource = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetTopicByTest", param);
                this.ddlTopics.DataTextField = "topicTitle";
                this.ddlTopics.DataValueField = "topicId";
                this.ddlTopics.DataBind();
            }
            SGACommon.InsertDefaultItem(this.ddlTopics, "Select Topics", "0");
        }

        protected void btnSubmit_Click(object sender, System.EventArgs e)
        {
            this.BindGrid();
        }

        protected void grdQuestions_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int optionValue = System.Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "optionValue"));
                int updatedMarks = System.Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "updatedMarks"));
                string allowValue = DataBinder.Eval(e.Item.DataItem, "optionimage").ToString();
                Image imgArrow = (Image)e.Item.FindControl("imgArrow");
                DropDownList ddlMarks = (DropDownList)e.Item.FindControl("ddlMarks");
                if (ddlMarks != null)
                {
                    ddlMarks.DataSource = this.dsOptions;
                    ddlMarks.DataTextField = "optionId";
                    ddlMarks.DataValueField = "optionId";
                    ddlMarks.DataBind();
                    SGACommon.SelectListItem(ddlMarks, optionValue);
                }
                string text = allowValue;
                if (text != null)
                {
                    if (text == "n")
                    {
                        imgArrow.Visible = false;
                        goto IL_18E;
                    }
                    if (text == "u")
                    {
                        SGACommon.SelectListItem(ddlMarks, updatedMarks);
                        imgArrow.ImageUrl = this.Page.ResolveUrl("~/innerimages/top_r.png");
                        goto IL_18E;
                    }
                    if (text == "d")
                    {
                        SGACommon.SelectListItem(ddlMarks, updatedMarks);
                        imgArrow.ImageUrl = this.Page.ResolveUrl("~/innerimages/down_r.png");
                        goto IL_18E;
                    }
                }
                imgArrow.Visible = false;
            IL_18E: ;
            }
        }

        protected void grdQuestions_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                DropDownList ddlMarks = (DropDownList)e.Item.FindControl("ddlMarks");
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateReviewOption", new SqlParameter[]
				{
					new SqlParameter("@replyId", System.Convert.ToInt32(e.CommandArgument)),
					new SqlParameter("@userId", SGACommon.LoginUserInfo.userId),
					new SqlParameter("@typeId", System.Convert.ToInt32(this.ddlAssessmentType.SelectedValue)),
					new SqlParameter("@marks", System.Convert.ToInt32(ddlMarks.SelectedValue))
				});
                this.BindGrid();
            }
        }

        protected void btnUpdateAllTop_Click(object sender, System.EventArgs e)
        {
            if (this.grdQuestions.Items.Count > 0)
            {
                foreach (DataGridItem item in this.grdQuestions.Items)
                {
                    DropDownList ddlMarks = (DropDownList)item.FindControl("ddlMarks");
                    Button btnEdit = (Button)item.FindControl("btnEdit");
                    if (ddlMarks != null && btnEdit != null)
                    {
                        SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateReviewOption", new SqlParameter[]
						{
							new SqlParameter("@replyId", System.Convert.ToInt32(btnEdit.CommandArgument)),
							new SqlParameter("@userId", SGACommon.LoginUserInfo.userId),
							new SqlParameter("@typeId", System.Convert.ToInt32(this.ddlAssessmentType.SelectedValue)),
							new SqlParameter("@marks", System.Convert.ToInt32(ddlMarks.SelectedValue))
						});
                    }
                }
                this.BindGrid();
            }
        }
    }
}
