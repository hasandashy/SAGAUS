﻿using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SGA.webadmin
{
    public partial class ManageSGA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.selected_tab.Value = base.Request.Form[this.selected_tab.UniqueID];
            if (!base.IsPostBack)
            {
                this.BindTopics();
                this.BindQuestions();
                this.BindOptions();
            }
        }

        private void BindTopics()
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSGATopicsAdmin");
            this.dtgList.DataSource = ds;
            this.dtgList.DataBind();
        }

        private void BindQuestions()
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSgaQuestionsAdmin", new SqlParameter[]
			{
				new SqlParameter("@topicIds", this.hdSelectIds.Value)
			});
            this.grdQuestions.DataSource = ds;
            this.grdQuestions.DataBind();
        }

        private void BindOptions()
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSGAOptionsAdmin");
            this.grdOptions.DataSource = ds;
            this.grdOptions.DataBind();
        }

        protected void imgSave_Click(object sender, ImageClickEventArgs e)
        {
            if (this.Page.IsValid)
            {
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateSgaTopicDetail", new SqlParameter[]
				{
					new SqlParameter("@topicId", this.imgSave.CommandArgument.ToString()),
					new SqlParameter("@topicName", this.txttopicTitle.Value.Trim())
				});
                this.pnlTopics.Visible = true;
                this.pnlTopicsEdit.Visible = false;
                this.BindTopics();
            }
        }

        protected void iBtnSelect_Click(object sender, ImageClickEventArgs e)
        {
            this.hdSelectIds.Value = "";
            foreach (DataGridItem item in this.dtgList.Items)
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
            this.BindQuestions();
        }

        protected void imgCancel_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlTopics.Visible = true;
            this.pnlTopicsEdit.Visible = false;
        }

        protected void imgUpdateQuestion_Click(object sender, ImageClickEventArgs e)
        {
            if (this.Page.IsValid)
            {
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateSGAQuestion", new SqlParameter[]
				{
					new SqlParameter("@questionId", this.imgUpdateQuestion.CommandArgument.ToString()),
					new SqlParameter("@questionText", this.txtQuestion.Text.Trim())

				});
                this.pnlQuestions.Visible = true;
                this.pnlQuestionsEdit.Visible = false;
                this.BindQuestions();
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (this.Page.IsValid)
            {
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateAdminSGAOptions", new SqlParameter[]
				{
					new SqlParameter("@optionId", this.ImageButton1.CommandArgument.ToString()),
					new SqlParameter("@optionText", this.txtOptionText.Value.Trim()),
					new SqlParameter("@optionMark", this.txtOptionValue.Value.Trim())
				});
                this.BindOptions();
                this.pnlOptions.Visible = true;
                this.pnlOptionsEdit.Visible = false;
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlOptions.Visible = true;
            this.pnlOptionsEdit.Visible = false;
        }

        protected void imgCancelQuestion_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlQuestions.Visible = true;
            this.pnlQuestionsEdit.Visible = false;
        }

        protected void dtgList_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSgaTopicDetail", new SqlParameter[]
				{
					new SqlParameter("@topicId", e.CommandArgument)
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.imgSave.CommandArgument = e.CommandArgument.ToString();
                        this.txttopicTitle.Value = ds.Tables[0].Rows[0]["topicName"].ToString();
                        //this.txtDescription.Value = ds.Tables[0].Rows[0]["topicDescription"].ToString();
                        this.pnlTopics.Visible = false;
                        this.pnlTopicsEdit.Visible = true;
                    }
                }
            }
        }

        protected void grdQuestions_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSgaQuestionDetail", new SqlParameter[]
				{
					new SqlParameter("@questionId", e.CommandArgument)
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.imgUpdateQuestion.CommandArgument = e.CommandArgument.ToString();
                        this.txtQuestion.Text = ds.Tables[0].Rows[0]["questionText"].ToString();
                        this.pnlQuestions.Visible = false;
                        this.pnlQuestionsEdit.Visible = true;
                    }
                }
            }
        }

        protected void grdOptions_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSgaOptionDetail", new SqlParameter[]
				{
					new SqlParameter("@optionId", e.CommandArgument)
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.ImageButton1.CommandArgument = e.CommandArgument.ToString();
                        this.txtOptionText.Value = ds.Tables[0].Rows[0]["optionText"].ToString();
                        this.txtOptionValue.Value = ds.Tables[0].Rows[0]["optionMark"].ToString();
                        this.pnlOptions.Visible = false;
                        this.pnlOptionsEdit.Visible = true;
                    }
                }
            }
        }
    }
}