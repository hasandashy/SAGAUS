using DataTier;
using SGA.App_Code;
using SGA.controls;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SGA.webadmin
{
    public partial class ManageCMC : Page
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
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetQuestionsSGA", new SqlParameter[]
			{
				new SqlParameter("@topicIds", this.hdSelectIds.Value)
			});
            this.grdQuestions.DataSource = ds;
            this.grdQuestions.DataBind();
        }

        private void BindOptions()
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetQuestionOptionsSGA", new SqlParameter[]
			{
				new SqlParameter("@questionIds", this.hdQuestionId.Value)
			});
            this.grdOptions.DataSource = ds;
            this.grdOptions.DataBind();
        }

        protected void imgSave_Click(object sender, ImageClickEventArgs e)
        {
            if (this.Page.IsValid)
            {
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateTopicDetailSGA", new SqlParameter[]
				{
					new SqlParameter("@topicId", this.imgSave.CommandArgument.ToString()),
					new SqlParameter("@topicTitle", this.txttopicTitle.Value.Trim())
				});
                this.pnlTopics.Visible = true;
                this.pnlTopicsEdit.Visible = false;
                this.BindTopics();
            }
        }

        protected void grdOptions_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblAnswer = (Label)e.Item.FindControl("lblAnswer");
                if (lblAnswer != null)
                {
                    lblAnswer.Text = SGACommon.YesNoConversion(DataBinder.Eval(e.Item.DataItem, "isAnswer"));
                }
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

        protected void iBtnSelectQuestion_Click(object sender, ImageClickEventArgs e)
        {
            this.hdQuestionId.Value = "";
            foreach (DataGridItem item in this.grdQuestions.Items)
            {
                HtmlInputCheckBox chkSelect = (HtmlInputCheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    if (chkSelect.Checked)
                    {
                        HiddenField expr_61 = this.hdQuestionId;
                        expr_61.Value = expr_61.Value + chkSelect.Value + ",";
                    }
                }
            }
            this.BindOptions();
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
					new SqlParameter("@optionText", this.txtOptionText.Text.Trim()),
					new SqlParameter("@optionMark", this.txtOptionMark.Value.Trim()),
					new SqlParameter("@isAnswer", this.chkAnswer.Checked),
					new SqlParameter("@questionId", this.hdEditQuestionId.Value.ToString())
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
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetTopicDetailSGA", new SqlParameter[]
				{
					new SqlParameter("@topicId", e.CommandArgument)
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.imgSave.CommandArgument = e.CommandArgument.ToString();
                        this.txttopicTitle.Value = ds.Tables[0].Rows[0]["topicName"].ToString();
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
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetQuestionDetailSGA", new SqlParameter[]
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
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetOptionDetailSGA", new SqlParameter[]
				{
					new SqlParameter("@optionId", e.CommandArgument)
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.ImageButton1.CommandArgument = e.CommandArgument.ToString();
                        this.txtOptionText.Text = ds.Tables[0].Rows[0]["optionText"].ToString();
                        this.txtOptionMark.Value = ds.Tables[0].Rows[0]["optionMark"].ToString();
                        this.chkAnswer.Checked = System.Convert.ToBoolean(ds.Tables[0].Rows[0]["isAnswer"].ToString());
                        this.hdEditQuestionId.Value = ds.Tables[0].Rows[0]["questionId"].ToString();
                        this.pnlOptions.Visible = false;
                        this.pnlOptionsEdit.Visible = true;
                    }
                }
            }
        }
    }
}
