using DataTier;
using FredCK.FCKeditorV2;
using SGA.controls;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.webadmin
{
    public partial class ManageWorkshop : Page
    {
        

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.selected_tab.Value = base.Request.Form[this.selected_tab.UniqueID];
            if (!base.IsPostBack)
            {
                this.BindGrid(0, this.grdSSASuggestions, 2);
                this.BindGrid(0, this.grdCMASuggestions, 6);
            }
        }

        private void BindGrid(int flag, DataGrid grd, int testType)
        {
            grd.DataSource = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageCMCRecommendations", new SqlParameter[]
			{
				new SqlParameter("@flag", flag),
				new SqlParameter("@testType", testType)
			});
            grd.DataBind();
        }

        protected void grdSSASuggestions_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                this.pnlSSAEdit.Visible = true;
                this.pnlSSAList.Visible = false;
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageCMCRecommendations", new SqlParameter[]
				{
					new SqlParameter("@Id", e.CommandArgument),
					new SqlParameter("@flag", "2")
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.txtSSALevel.Text = ds.Tables[0].Rows[0]["level"].ToString();
                        this.txtSSAScale.Text = ds.Tables[0].Rows[0]["RatingScale"].ToString();
                        this.txtSSAScore.Text = ds.Tables[0].Rows[0]["Score"].ToString();
                        this.imgSSAEdit.CommandArgument = ds.Tables[0].Rows[0]["Id"].ToString();
                    }
                }
                DataSet dsDynamicRecommendation = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetTrainingCourses", new SqlParameter[]
				{
					new SqlParameter("@Id", e.CommandArgument),
					new SqlParameter("@testType", "2"),
					new SqlParameter("@flag", "0")
				});
                if (dsDynamicRecommendation != null)
                {
                    if (dsDynamicRecommendation.Tables.Count > 0 && dsDynamicRecommendation.Tables[0].Rows.Count > 0)
                    {
                        this.lblSSATopic1.Text = dsDynamicRecommendation.Tables[0].Rows[0]["topicName"].ToString();
                        this.lblSSATopic2.Text = dsDynamicRecommendation.Tables[0].Rows[1]["topicName"].ToString();
                        this.lblSSATopic3.Text = dsDynamicRecommendation.Tables[0].Rows[2]["topicName"].ToString();
                        this.lblSSATopic4.Text = dsDynamicRecommendation.Tables[0].Rows[3]["topicName"].ToString();
                        this.lblSSATopic5.Text = dsDynamicRecommendation.Tables[0].Rows[4]["topicName"].ToString();
                        this.lblSSATopic6.Text = dsDynamicRecommendation.Tables[0].Rows[5]["topicName"].ToString();
                        this.lblSSATopic7.Text = dsDynamicRecommendation.Tables[0].Rows[6]["topicName"].ToString();
                        this.lblSSATopic8.Text = dsDynamicRecommendation.Tables[0].Rows[7]["topicName"].ToString();
                        this.txtSSATopicText1.Value = dsDynamicRecommendation.Tables[0].Rows[0]["trainingRecommendation"].ToString();
                        this.txtSSATopicText2.Value = dsDynamicRecommendation.Tables[0].Rows[1]["trainingRecommendation"].ToString();
                        this.txtSSATopicText3.Value = dsDynamicRecommendation.Tables[0].Rows[2]["trainingRecommendation"].ToString();
                        this.txtSSATopicText4.Value = dsDynamicRecommendation.Tables[0].Rows[3]["trainingRecommendation"].ToString();
                        this.txtSSATopicText5.Value = dsDynamicRecommendation.Tables[0].Rows[4]["trainingRecommendation"].ToString();
                        this.txtSSATopicText6.Value = dsDynamicRecommendation.Tables[0].Rows[5]["trainingRecommendation"].ToString();
                        this.txtSSATopicText7.Value = dsDynamicRecommendation.Tables[0].Rows[6]["trainingRecommendation"].ToString();
                        this.txtSSATopicText8.Value = dsDynamicRecommendation.Tables[0].Rows[7]["trainingRecommendation"].ToString();
                        this.txtSSATopicHeading1.Text = dsDynamicRecommendation.Tables[0].Rows[0]["trainingHeading"].ToString();
                        this.txtSSATopicHeading2.Text = dsDynamicRecommendation.Tables[0].Rows[1]["trainingHeading"].ToString();
                        this.txtSSATopicHeading3.Text = dsDynamicRecommendation.Tables[0].Rows[2]["trainingHeading"].ToString();
                        this.txtSSATopicHeading4.Text = dsDynamicRecommendation.Tables[0].Rows[3]["trainingHeading"].ToString();
                        this.txtSSATopicHeading5.Text = dsDynamicRecommendation.Tables[0].Rows[4]["trainingHeading"].ToString();
                        this.txtSSATopicHeading6.Text = dsDynamicRecommendation.Tables[0].Rows[5]["trainingHeading"].ToString();
                        this.txtSSATopicHeading7.Text = dsDynamicRecommendation.Tables[0].Rows[6]["trainingHeading"].ToString();
                        this.txtSSATopicHeading8.Text = dsDynamicRecommendation.Tables[0].Rows[7]["trainingHeading"].ToString();
                        this.txtSSAMoreLink1.Text = dsDynamicRecommendation.Tables[0].Rows[0]["morelink"].ToString();
                        this.txtSSAMoreLink2.Text = dsDynamicRecommendation.Tables[0].Rows[1]["morelink"].ToString();
                        this.txtSSAMoreLink3.Text = dsDynamicRecommendation.Tables[0].Rows[2]["morelink"].ToString();
                        this.txtSSAMoreLink4.Text = dsDynamicRecommendation.Tables[0].Rows[3]["morelink"].ToString();
                        this.txtSSAMoreLink5.Text = dsDynamicRecommendation.Tables[0].Rows[4]["morelink"].ToString();
                        this.txtSSAMoreLink6.Text = dsDynamicRecommendation.Tables[0].Rows[5]["morelink"].ToString();
                        this.txtSSAMoreLink7.Text = dsDynamicRecommendation.Tables[0].Rows[6]["morelink"].ToString();
                        this.txtSSAMoreLink8.Text = dsDynamicRecommendation.Tables[0].Rows[7]["morelink"].ToString();
                        this.hdSSAId1.Value = dsDynamicRecommendation.Tables[0].Rows[0]["id"].ToString();
                        this.hdSSAId2.Value = dsDynamicRecommendation.Tables[0].Rows[1]["id"].ToString();
                        this.hdSSAId3.Value = dsDynamicRecommendation.Tables[0].Rows[2]["id"].ToString();
                        this.hdSSAId4.Value = dsDynamicRecommendation.Tables[0].Rows[3]["id"].ToString();
                        this.hdSSAId5.Value = dsDynamicRecommendation.Tables[0].Rows[4]["id"].ToString();
                        this.hdSSAId6.Value = dsDynamicRecommendation.Tables[0].Rows[5]["id"].ToString();
                        this.hdSSAId7.Value = dsDynamicRecommendation.Tables[0].Rows[6]["id"].ToString();
                        this.hdSSAId8.Value = dsDynamicRecommendation.Tables[0].Rows[7]["id"].ToString();
                    }
                }
            }
        }

        protected void grdCMASuggestions_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                this.pnlCMAEdit.Visible = true;
                this.pnlCMAList.Visible = false;
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageCMCRecommendations", new SqlParameter[]
				{
					new SqlParameter("@Id", e.CommandArgument),
					new SqlParameter("@flag", "2")
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.txtCMALevel.Text = ds.Tables[0].Rows[0]["level"].ToString();
                        this.txtCMAScale.Text = ds.Tables[0].Rows[0]["RatingScale"].ToString();
                        this.txtCMAScore.Text = ds.Tables[0].Rows[0]["Score"].ToString();
                        this.imgCMAEdit.CommandArgument = ds.Tables[0].Rows[0]["Id"].ToString();
                    }
                }
                DataSet dsDynamicRecommendation = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetTrainingCourses", new SqlParameter[]
				{
					new SqlParameter("@Id", e.CommandArgument),
					new SqlParameter("@testType", "6"),
					new SqlParameter("@flag", "1")
				});
                if (dsDynamicRecommendation != null)
                {
                    if (dsDynamicRecommendation.Tables.Count > 0 && dsDynamicRecommendation.Tables[0].Rows.Count > 0)
                    {
                        this.lblCMATopic1.Text = dsDynamicRecommendation.Tables[0].Rows[0]["topicName"].ToString();
                        this.lblCMATopic2.Text = dsDynamicRecommendation.Tables[0].Rows[1]["topicName"].ToString();
                        this.lblCMATopic3.Text = dsDynamicRecommendation.Tables[0].Rows[2]["topicName"].ToString();
                        this.lblCMATopic4.Text = dsDynamicRecommendation.Tables[0].Rows[3]["topicName"].ToString();
                        this.lblCMATopic5.Text = dsDynamicRecommendation.Tables[0].Rows[4]["topicName"].ToString();
                        this.lblCMATopic6.Text = dsDynamicRecommendation.Tables[0].Rows[5]["topicName"].ToString();
                        this.lblCMATopic7.Text = dsDynamicRecommendation.Tables[0].Rows[6]["topicName"].ToString();
                        this.lblCMATopic8.Text = dsDynamicRecommendation.Tables[0].Rows[7]["topicName"].ToString();
                        this.txtCMATopicText1.Value = dsDynamicRecommendation.Tables[0].Rows[0]["trainingRecommendation"].ToString();
                        this.txtCMATopicText2.Value = dsDynamicRecommendation.Tables[0].Rows[1]["trainingRecommendation"].ToString();
                        this.txtCMATopicText3.Value = dsDynamicRecommendation.Tables[0].Rows[2]["trainingRecommendation"].ToString();
                        this.txtCMATopicText4.Value = dsDynamicRecommendation.Tables[0].Rows[3]["trainingRecommendation"].ToString();
                        this.txtCMATopicText5.Value = dsDynamicRecommendation.Tables[0].Rows[4]["trainingRecommendation"].ToString();
                        this.txtCMATopicText6.Value = dsDynamicRecommendation.Tables[0].Rows[5]["trainingRecommendation"].ToString();
                        this.txtCMATopicText7.Value = dsDynamicRecommendation.Tables[0].Rows[6]["trainingRecommendation"].ToString();
                        this.txtCMATopicText8.Value = dsDynamicRecommendation.Tables[0].Rows[7]["trainingRecommendation"].ToString();
                        this.txtCMATopicHeading1.Text = dsDynamicRecommendation.Tables[0].Rows[0]["trainingHeading"].ToString();
                        this.txtCMATopicHeading2.Text = dsDynamicRecommendation.Tables[0].Rows[1]["trainingHeading"].ToString();
                        this.txtCMATopicHeading3.Text = dsDynamicRecommendation.Tables[0].Rows[2]["trainingHeading"].ToString();
                        this.txtCMATopicHeading4.Text = dsDynamicRecommendation.Tables[0].Rows[3]["trainingHeading"].ToString();
                        this.txtCMATopicHeading5.Text = dsDynamicRecommendation.Tables[0].Rows[4]["trainingHeading"].ToString();
                        this.txtCMATopicHeading6.Text = dsDynamicRecommendation.Tables[0].Rows[5]["trainingHeading"].ToString();
                        this.txtCMATopicHeading7.Text = dsDynamicRecommendation.Tables[0].Rows[6]["trainingHeading"].ToString();
                        this.txtCMATopicHeading8.Text = dsDynamicRecommendation.Tables[0].Rows[7]["trainingHeading"].ToString();
                        this.txtCMAMoreLink1.Text = dsDynamicRecommendation.Tables[0].Rows[0]["morelink"].ToString();
                        this.txtCMAMoreLink2.Text = dsDynamicRecommendation.Tables[0].Rows[1]["morelink"].ToString();
                        this.txtCMAMoreLink3.Text = dsDynamicRecommendation.Tables[0].Rows[2]["morelink"].ToString();
                        this.txtCMAMoreLink4.Text = dsDynamicRecommendation.Tables[0].Rows[3]["morelink"].ToString();
                        this.txtCMAMoreLink5.Text = dsDynamicRecommendation.Tables[0].Rows[4]["morelink"].ToString();
                        this.txtCMAMoreLink6.Text = dsDynamicRecommendation.Tables[0].Rows[5]["morelink"].ToString();
                        this.txtCMAMoreLink7.Text = dsDynamicRecommendation.Tables[0].Rows[6]["morelink"].ToString();
                        this.txtCMAMoreLink8.Text = dsDynamicRecommendation.Tables[0].Rows[7]["morelink"].ToString();
                        this.hdCMAId1.Value = dsDynamicRecommendation.Tables[0].Rows[0]["id"].ToString();
                        this.hdCMAId2.Value = dsDynamicRecommendation.Tables[0].Rows[1]["id"].ToString();
                        this.hdCMAId3.Value = dsDynamicRecommendation.Tables[0].Rows[2]["id"].ToString();
                        this.hdCMAId4.Value = dsDynamicRecommendation.Tables[0].Rows[3]["id"].ToString();
                        this.hdCMAId5.Value = dsDynamicRecommendation.Tables[0].Rows[4]["id"].ToString();
                        this.hdCMAId6.Value = dsDynamicRecommendation.Tables[0].Rows[5]["id"].ToString();
                        this.hdCMAId7.Value = dsDynamicRecommendation.Tables[0].Rows[6]["id"].ToString();
                        this.hdCMAId8.Value = dsDynamicRecommendation.Tables[0].Rows[7]["id"].ToString();
                    }
                }
            }
        }

        protected void imgSSAEdit_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlSSAEdit.Visible = false;
            this.pnlSSAList.Visible = true;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spManageCMCRecommendations", new SqlParameter[]
			{
				new SqlParameter("@Id", this.imgSSAEdit.CommandArgument),
				new SqlParameter("@flag", "1"),
				new SqlParameter("@level", this.txtSSALevel.Text),
				new SqlParameter("@ratingScale", this.txtSSAScale.Text),
				new SqlParameter("@score", this.txtSSAScore.Text),
				new SqlParameter("@recommendation", "")
			});
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateDynamicManagement", new SqlParameter[]
			{
				new SqlParameter("@Id1", this.hdSSAId1.Value),
				new SqlParameter("@Id2", this.hdSSAId2.Value),
				new SqlParameter("@Id3", this.hdSSAId3.Value),
				new SqlParameter("@Id4", this.hdSSAId4.Value),
				new SqlParameter("@Id5", this.hdSSAId5.Value),
				new SqlParameter("@Id6", this.hdSSAId6.Value),
				new SqlParameter("@Id7", this.hdSSAId7.Value),
				new SqlParameter("@Id8", this.hdSSAId8.Value),
				new SqlParameter("@heading1", this.txtSSATopicHeading1.Text),
				new SqlParameter("@heading2", this.txtSSATopicHeading2.Text),
				new SqlParameter("@heading3", this.txtSSATopicHeading3.Text),
				new SqlParameter("@heading4", this.txtSSATopicHeading4.Text),
				new SqlParameter("@heading5", this.txtSSATopicHeading5.Text),
				new SqlParameter("@heading6", this.txtSSATopicHeading6.Text),
				new SqlParameter("@heading7", this.txtSSATopicHeading7.Text),
				new SqlParameter("@heading8", this.txtSSATopicHeading8.Text),
				new SqlParameter("@dynamicRecommendation1", this.txtSSATopicText1.Value),
				new SqlParameter("@dynamicRecommendation2", this.txtSSATopicText2.Value),
				new SqlParameter("@dynamicRecommendation3", this.txtSSATopicText3.Value),
				new SqlParameter("@dynamicRecommendation4", this.txtSSATopicText4.Value),
				new SqlParameter("@dynamicRecommendation5", this.txtSSATopicText5.Value),
				new SqlParameter("@dynamicRecommendation6", this.txtSSATopicText6.Value),
				new SqlParameter("@dynamicRecommendation7", this.txtSSATopicText7.Value),
				new SqlParameter("@dynamicRecommendation8", this.txtSSATopicText8.Value),
				new SqlParameter("@morelink1", this.txtSSAMoreLink1.Text),
				new SqlParameter("@morelink2", this.txtSSAMoreLink2.Text),
				new SqlParameter("@morelink3", this.txtSSAMoreLink3.Text),
				new SqlParameter("@morelink4", this.txtSSAMoreLink4.Text),
				new SqlParameter("@morelink5", this.txtSSAMoreLink5.Text),
				new SqlParameter("@morelink6", this.txtSSAMoreLink6.Text),
				new SqlParameter("@morelink7", this.txtSSAMoreLink7.Text),
				new SqlParameter("@morelink8", this.txtSSAMoreLink7.Text)
			});
            this.BindGrid(0, this.grdSSASuggestions, 2);
        }

        protected void imgCMAEdit_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlCMAEdit.Visible = false;
            this.pnlCMAList.Visible = true;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spManageCMCRecommendations", new SqlParameter[]
			{
				new SqlParameter("@Id", this.imgCMAEdit.CommandArgument),
				new SqlParameter("@flag", "1"),
				new SqlParameter("@level", this.txtCMALevel.Text),
				new SqlParameter("@ratingScale", this.txtCMAScale.Text),
				new SqlParameter("@score", this.txtCMAScore.Text),
				new SqlParameter("@recommendation", "")
			});
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateDynamicManagement", new SqlParameter[]
			{
				new SqlParameter("@Id1", this.hdCMAId1.Value),
				new SqlParameter("@Id2", this.hdCMAId2.Value),
				new SqlParameter("@Id3", this.hdCMAId3.Value),
				new SqlParameter("@Id4", this.hdCMAId4.Value),
				new SqlParameter("@Id5", this.hdCMAId5.Value),
				new SqlParameter("@Id6", this.hdCMAId6.Value),
				new SqlParameter("@Id7", this.hdCMAId7.Value),
				new SqlParameter("@Id8", this.hdCMAId8.Value),
				new SqlParameter("@heading1", this.txtCMATopicHeading1.Text),
				new SqlParameter("@heading2", this.txtCMATopicHeading2.Text),
				new SqlParameter("@heading3", this.txtCMATopicHeading3.Text),
				new SqlParameter("@heading4", this.txtCMATopicHeading4.Text),
				new SqlParameter("@heading5", this.txtCMATopicHeading5.Text),
				new SqlParameter("@heading6", this.txtCMATopicHeading6.Text),
				new SqlParameter("@heading7", this.txtCMATopicHeading7.Text),
				new SqlParameter("@heading8", this.txtCMATopicHeading8.Text),
				new SqlParameter("@dynamicRecommendation1", this.txtCMATopicText1.Value),
				new SqlParameter("@dynamicRecommendation2", this.txtCMATopicText2.Value),
				new SqlParameter("@dynamicRecommendation3", this.txtCMATopicText3.Value),
				new SqlParameter("@dynamicRecommendation4", this.txtCMATopicText4.Value),
				new SqlParameter("@dynamicRecommendation5", this.txtCMATopicText5.Value),
				new SqlParameter("@dynamicRecommendation6", this.txtCMATopicText6.Value),
				new SqlParameter("@dynamicRecommendation7", this.txtCMATopicText7.Value),
				new SqlParameter("@dynamicRecommendation8", this.txtCMATopicText8.Value),
				new SqlParameter("@morelink1", this.txtCMAMoreLink1.Text),
				new SqlParameter("@morelink2", this.txtCMAMoreLink2.Text),
				new SqlParameter("@morelink3", this.txtCMAMoreLink3.Text),
				new SqlParameter("@morelink4", this.txtCMAMoreLink4.Text),
				new SqlParameter("@morelink5", this.txtCMAMoreLink5.Text),
				new SqlParameter("@morelink6", this.txtCMAMoreLink6.Text),
				new SqlParameter("@morelink7", this.txtCMAMoreLink7.Text),
				new SqlParameter("@morelink8", this.txtCMAMoreLink7.Text)
			});
            this.BindGrid(0, this.grdCMASuggestions, 6);
        }

        protected void imgCMACancel_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlCMAEdit.Visible = false;
            this.pnlCMAList.Visible = true;
        }

        protected void imgSSACancel_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlSSAEdit.Visible = false;
            this.pnlSSAList.Visible = true;
        }
    }
}
