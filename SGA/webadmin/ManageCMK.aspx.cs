using DataTier;
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
    public partial class ManageCMK : System.Web.UI.Page
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
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetCMATopicsAdmin");
            this.dtgList.DataSource = ds;
            this.dtgList.DataBind();
        }

        private void BindQuestions()
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetCMAAdminQuestions", new SqlParameter[]
			{
				new SqlParameter("@topicIds", this.hdSelectIds.Value)
			});
            this.grdQuestions.DataSource = ds;
            this.grdQuestions.DataBind();
        }

        private void BindOptions()
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetOptionsAdminCMA");
            this.grdOptions.DataSource = ds;
            this.grdOptions.DataBind();
        }

        protected void imgSave_Click(object sender, ImageClickEventArgs e)
        {
            if (this.Page.IsValid)
            {
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateTopicDetailCMA", new SqlParameter[]
				{
					new SqlParameter("@topicId", this.imgSave.CommandArgument.ToString()),
					new SqlParameter("@topicTitle", this.txttopicTitle.Value.Trim()),
					new SqlParameter("@topicDesc", this.txtDescription.Value.Trim())
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
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateCMAQuestion", new SqlParameter[]
				{
					new SqlParameter("@questionId", this.imgUpdateQuestion.CommandArgument.ToString()),
					new SqlParameter("@questionText", this.txtQuestion.Text.Trim()),
					new SqlParameter("@hintText", this.txtHintText.Value.Trim()),
					new SqlParameter("@hintWord", this.txtHintWord.Value.Trim())
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
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateAdminCMAOptions", new SqlParameter[]
				{
					new SqlParameter("@optionId", this.ImageButton1.CommandArgument.ToString()),
					new SqlParameter("@optionText", this.txtOptionText.Value.Trim()),
					new SqlParameter("@optionValue", this.txtOptionValue.Value.Trim()),
					new SqlParameter("@optionLevel", this.txtOptionLevel.Value.Trim())
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
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetTopicDetailCMA", new SqlParameter[]
				{
					new SqlParameter("@topicId", e.CommandArgument)
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.imgSave.CommandArgument = e.CommandArgument.ToString();
                        this.txttopicTitle.Value = ds.Tables[0].Rows[0]["topicTitle"].ToString();
                        this.txtDescription.Value = ds.Tables[0].Rows[0]["topicDescription"].ToString();
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
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetQuestionDetailCMA", new SqlParameter[]
				{
					new SqlParameter("@questionId", e.CommandArgument)
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.imgUpdateQuestion.CommandArgument = e.CommandArgument.ToString();
                        this.txtQuestion.Text = ds.Tables[0].Rows[0]["questionText"].ToString();
                        this.txtHintText.Value = ds.Tables[0].Rows[0]["hintText"].ToString();
                        this.txtHintWord.Value = ds.Tables[0].Rows[0]["hintWord"].ToString();
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
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetOptionDetailCMA", new SqlParameter[]
				{
					new SqlParameter("@optionId", e.CommandArgument)
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.ImageButton1.CommandArgument = e.CommandArgument.ToString();
                        this.txtOptionText.Value = ds.Tables[0].Rows[0]["optionText"].ToString();
                        this.txtOptionValue.Value = ds.Tables[0].Rows[0]["optionValue"].ToString();
                        this.txtOptionLevel.Value = ds.Tables[0].Rows[0]["optionLevel"].ToString();
                        this.pnlOptions.Visible = false;
                        this.pnlOptionsEdit.Visible = true;
                    }
                }
            }
        }
    }
}