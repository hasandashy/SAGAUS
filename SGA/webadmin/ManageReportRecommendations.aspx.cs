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
    public partial class ManageReportRecommendations : Page
    {
        
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.selected_tab.Value = base.Request.Form[this.selected_tab.UniqueID];
            if (!base.IsPostBack)
            {
                this.BindGrid(0, this.grdSSASuggestions, 2);
                this.BindGrid(0, this.grdBASuggestions, 3);
                this.BindGrid(0, this.grdCMASuggestions, 6);
                this.BindGrid(0, this.grdNPSuggestions, 5);
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
                        this.txtSSARecommendation.Value = ds.Tables[0].Rows[0]["recommendation"].ToString();
                        this.imgSSAEdit.CommandArgument = ds.Tables[0].Rows[0]["Id"].ToString();
                    }
                }
                DataSet dsDynamicRecommendation = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSSADynamicRecommendation", new SqlParameter[]
				{
					new SqlParameter("@Id", e.CommandArgument)
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
                        this.txtSSATopicText1.Value = dsDynamicRecommendation.Tables[0].Rows[0]["dynamicRecommendation"].ToString();
                        this.txtSSATopicText2.Value = dsDynamicRecommendation.Tables[0].Rows[1]["dynamicRecommendation"].ToString();
                        this.txtSSATopicText3.Value = dsDynamicRecommendation.Tables[0].Rows[2]["dynamicRecommendation"].ToString();
                        this.txtSSATopicText4.Value = dsDynamicRecommendation.Tables[0].Rows[3]["dynamicRecommendation"].ToString();
                        this.txtSSATopicText5.Value = dsDynamicRecommendation.Tables[0].Rows[4]["dynamicRecommendation"].ToString();
                        this.txtSSATopicText6.Value = dsDynamicRecommendation.Tables[0].Rows[5]["dynamicRecommendation"].ToString();
                        this.txtSSATopicText7.Value = dsDynamicRecommendation.Tables[0].Rows[6]["dynamicRecommendation"].ToString();
                        this.txtSSATopicText8.Value = dsDynamicRecommendation.Tables[0].Rows[7]["dynamicRecommendation"].ToString();
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
                        this.txtCMARecommendation.Value = ds.Tables[0].Rows[0]["recommendation"].ToString();
                        this.imgCMAEdit.CommandArgument = ds.Tables[0].Rows[0]["Id"].ToString();
                    }
                }
                DataSet dsDynamicRecommendation = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetCMADynamicRecommendation", new SqlParameter[]
				{
					new SqlParameter("@Id", e.CommandArgument)
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
                        this.txtCMATopicText1.Value = dsDynamicRecommendation.Tables[0].Rows[0]["dynamicRecommendation"].ToString();
                        this.txtCMATopicText2.Value = dsDynamicRecommendation.Tables[0].Rows[1]["dynamicRecommendation"].ToString();
                        this.txtCMATopicText3.Value = dsDynamicRecommendation.Tables[0].Rows[2]["dynamicRecommendation"].ToString();
                        this.txtCMATopicText4.Value = dsDynamicRecommendation.Tables[0].Rows[3]["dynamicRecommendation"].ToString();
                        this.txtCMATopicText5.Value = dsDynamicRecommendation.Tables[0].Rows[4]["dynamicRecommendation"].ToString();
                        this.txtCMATopicText6.Value = dsDynamicRecommendation.Tables[0].Rows[5]["dynamicRecommendation"].ToString();
                        this.txtCMATopicText7.Value = dsDynamicRecommendation.Tables[0].Rows[6]["dynamicRecommendation"].ToString();
                        this.txtCMATopicText8.Value = dsDynamicRecommendation.Tables[0].Rows[7]["dynamicRecommendation"].ToString();
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
				new SqlParameter("@recommendation", this.txtSSARecommendation.Value)
			});
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateDynamicRecommendation", new SqlParameter[]
			{
				new SqlParameter("@Id1", this.hdSSAId1.Value),
				new SqlParameter("@Id2", this.hdSSAId2.Value),
				new SqlParameter("@Id3", this.hdSSAId3.Value),
				new SqlParameter("@Id4", this.hdSSAId4.Value),
				new SqlParameter("@Id5", this.hdSSAId5.Value),
				new SqlParameter("@Id6", this.hdSSAId6.Value),
				new SqlParameter("@Id7", this.hdSSAId7.Value),
				new SqlParameter("@Id8", this.hdSSAId8.Value),
				new SqlParameter("@dynamicRecommendation1", this.txtSSATopicText1.Value),
				new SqlParameter("@dynamicRecommendation2", this.txtSSATopicText2.Value),
				new SqlParameter("@dynamicRecommendation3", this.txtSSATopicText3.Value),
				new SqlParameter("@dynamicRecommendation4", this.txtSSATopicText4.Value),
				new SqlParameter("@dynamicRecommendation5", this.txtSSATopicText5.Value),
				new SqlParameter("@dynamicRecommendation6", this.txtSSATopicText6.Value),
				new SqlParameter("@dynamicRecommendation7", this.txtSSATopicText7.Value),
				new SqlParameter("@dynamicRecommendation8", this.txtSSATopicText8.Value)
			});
            this.BindGrid(0, this.grdSSASuggestions, 2);
        }

        protected void imgSSACancel_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlSSAEdit.Visible = false;
            this.pnlSSAList.Visible = true;
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
				new SqlParameter("@recommendation", this.txtCMARecommendation.Value)
			});
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateDynamicRecommendation", new SqlParameter[]
			{
				new SqlParameter("@Id1", this.hdCMAId1.Value),
				new SqlParameter("@Id2", this.hdCMAId2.Value),
				new SqlParameter("@Id3", this.hdCMAId3.Value),
				new SqlParameter("@Id4", this.hdCMAId4.Value),
				new SqlParameter("@Id5", this.hdCMAId5.Value),
				new SqlParameter("@Id6", this.hdCMAId6.Value),
				new SqlParameter("@Id7", this.hdCMAId7.Value),
				new SqlParameter("@Id8", this.hdCMAId8.Value),
				new SqlParameter("@dynamicRecommendation1", this.txtCMATopicText1.Value),
				new SqlParameter("@dynamicRecommendation2", this.txtCMATopicText2.Value),
				new SqlParameter("@dynamicRecommendation3", this.txtCMATopicText3.Value),
				new SqlParameter("@dynamicRecommendation4", this.txtCMATopicText4.Value),
				new SqlParameter("@dynamicRecommendation5", this.txtCMATopicText5.Value),
				new SqlParameter("@dynamicRecommendation6", this.txtCMATopicText6.Value),
				new SqlParameter("@dynamicRecommendation7", this.txtCMATopicText7.Value),
				new SqlParameter("@dynamicRecommendation8", this.txtCMATopicText8.Value)
			});
            this.BindGrid(0, this.grdCMASuggestions, 6);
        }

        protected void imgCMACancel_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlCMAEdit.Visible = false;
            this.pnlCMAList.Visible = true;
        }

        protected void imgBAEdit_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlBAEdit.Visible = false;
            this.pnlBAList.Visible = true;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spManageCMCRecommendations", new SqlParameter[]
			{
				new SqlParameter("@Id", this.imgBAEdit.CommandArgument),
				new SqlParameter("@flag", "1"),
				new SqlParameter("@level", this.txtBALevel.Text),
				new SqlParameter("@ratingScale", this.txtBAScale.Text),
				new SqlParameter("@score", this.txtBAScore.Text),
				new SqlParameter("@recommendation", this.txtBARecommendation.Value)
			});
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateDynamicRecommendation", new SqlParameter[]
			{
				new SqlParameter("@Id1", this.hdBAId1.Value),
				new SqlParameter("@Id2", this.hdBAId2.Value),
				new SqlParameter("@Id3", this.hdBAId3.Value),
				new SqlParameter("@Id4", this.hdBAId4.Value),
				new SqlParameter("@Id5", this.hdBAId5.Value),
				new SqlParameter("@Id6", this.hdBAId6.Value),
				new SqlParameter("@Id7", this.hdBAId7.Value),
				new SqlParameter("@Id8", this.hdBAId8.Value),
				new SqlParameter("@dynamicRecommendation1", this.txtBATopicText1.Value),
				new SqlParameter("@dynamicRecommendation2", this.txtBATopicText2.Value),
				new SqlParameter("@dynamicRecommendation3", this.txtBATopicText3.Value),
				new SqlParameter("@dynamicRecommendation4", this.txtBATopicText4.Value),
				new SqlParameter("@dynamicRecommendation5", this.txtBATopicText5.Value),
				new SqlParameter("@dynamicRecommendation6", this.txtBATopicText6.Value),
				new SqlParameter("@dynamicRecommendation7", this.txtBATopicText7.Value),
				new SqlParameter("@dynamicRecommendation8", this.txtBATopicText8.Value)
			});
            this.BindGrid(0, this.grdBASuggestions, 3);
        }

        protected void imgBACancel_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlBAEdit.Visible = false;
            this.pnlBAList.Visible = true;
        }

        protected void grdBASuggestions_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                this.pnlBAEdit.Visible = true;
                this.pnlBAList.Visible = false;
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageCMCRecommendations", new SqlParameter[]
				{
					new SqlParameter("@Id", e.CommandArgument),
					new SqlParameter("@flag", "2")
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.txtBALevel.Text = ds.Tables[0].Rows[0]["level"].ToString();
                        this.txtBAScale.Text = ds.Tables[0].Rows[0]["RatingScale"].ToString();
                        this.txtBAScore.Text = ds.Tables[0].Rows[0]["Score"].ToString();
                        this.txtBARecommendation.Value = ds.Tables[0].Rows[0]["recommendation"].ToString();
                        this.imgBAEdit.CommandArgument = ds.Tables[0].Rows[0]["Id"].ToString();
                    }
                }
                DataSet dsDynamicRecommendation = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetBADynamicRecommendation", new SqlParameter[]
				{
					new SqlParameter("@Id", e.CommandArgument)
				});
                if (dsDynamicRecommendation != null)
                {
                    if (dsDynamicRecommendation.Tables.Count > 0 && dsDynamicRecommendation.Tables[0].Rows.Count > 0)
                    {
                        this.lblBATopic1.Text = dsDynamicRecommendation.Tables[0].Rows[0]["topicName"].ToString();
                        this.lblBATopic2.Text = dsDynamicRecommendation.Tables[0].Rows[1]["topicName"].ToString();
                        this.lblBATopic3.Text = dsDynamicRecommendation.Tables[0].Rows[2]["topicName"].ToString();
                        this.lblBATopic4.Text = dsDynamicRecommendation.Tables[0].Rows[3]["topicName"].ToString();
                        this.lblBATopic5.Text = dsDynamicRecommendation.Tables[0].Rows[4]["topicName"].ToString();
                        this.lblBATopic6.Text = dsDynamicRecommendation.Tables[0].Rows[5]["topicName"].ToString();
                        this.lblBATopic7.Text = dsDynamicRecommendation.Tables[0].Rows[6]["topicName"].ToString();
                        this.lblBATopic8.Text = dsDynamicRecommendation.Tables[0].Rows[7]["topicName"].ToString();
                        this.txtBATopicText1.Value = dsDynamicRecommendation.Tables[0].Rows[0]["dynamicRecommendation"].ToString();
                        this.txtBATopicText2.Value = dsDynamicRecommendation.Tables[0].Rows[1]["dynamicRecommendation"].ToString();
                        this.txtBATopicText3.Value = dsDynamicRecommendation.Tables[0].Rows[2]["dynamicRecommendation"].ToString();
                        this.txtBATopicText4.Value = dsDynamicRecommendation.Tables[0].Rows[3]["dynamicRecommendation"].ToString();
                        this.txtBATopicText5.Value = dsDynamicRecommendation.Tables[0].Rows[4]["dynamicRecommendation"].ToString();
                        this.txtBATopicText6.Value = dsDynamicRecommendation.Tables[0].Rows[5]["dynamicRecommendation"].ToString();
                        this.txtBATopicText7.Value = dsDynamicRecommendation.Tables[0].Rows[6]["dynamicRecommendation"].ToString();
                        this.txtBATopicText8.Value = dsDynamicRecommendation.Tables[0].Rows[7]["dynamicRecommendation"].ToString();
                        this.hdBAId1.Value = dsDynamicRecommendation.Tables[0].Rows[0]["id"].ToString();
                        this.hdBAId2.Value = dsDynamicRecommendation.Tables[0].Rows[1]["id"].ToString();
                        this.hdBAId3.Value = dsDynamicRecommendation.Tables[0].Rows[2]["id"].ToString();
                        this.hdBAId4.Value = dsDynamicRecommendation.Tables[0].Rows[3]["id"].ToString();
                        this.hdBAId5.Value = dsDynamicRecommendation.Tables[0].Rows[4]["id"].ToString();
                        this.hdBAId6.Value = dsDynamicRecommendation.Tables[0].Rows[5]["id"].ToString();
                        this.hdBAId7.Value = dsDynamicRecommendation.Tables[0].Rows[6]["id"].ToString();
                        this.hdBAId8.Value = dsDynamicRecommendation.Tables[0].Rows[7]["id"].ToString();
                    }
                }
            }
        }

        protected void imgNPEdit_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlNPEdit.Visible = false;
            this.pnlNPList.Visible = true;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spManageCMCRecommendations", new SqlParameter[]
			{
				new SqlParameter("@Id", this.imgNPEdit.CommandArgument),
				new SqlParameter("@flag", "1"),
				new SqlParameter("@level", this.txtNPLevel.Text),
				new SqlParameter("@ratingScale", this.txtNPScale.Text),
				new SqlParameter("@score", this.txtNPScore.Text),
				new SqlParameter("@recommendation", this.txtNPRecommendation.Value)
			});
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateDynamicRecommendation", new SqlParameter[]
			{
				new SqlParameter("@Id1", this.hdNPId1.Value),
				new SqlParameter("@Id2", this.hdNPId2.Value),
				new SqlParameter("@Id3", this.hdNPId3.Value),
				new SqlParameter("@Id4", this.hdNPId4.Value),
				new SqlParameter("@Id5", this.hdNPId5.Value),
				new SqlParameter("@Id6", this.hdNPId6.Value),
				new SqlParameter("@Id7", this.hdNPId7.Value),
				new SqlParameter("@Id8", this.hdNPId8.Value),
				new SqlParameter("@dynamicRecommendation1", this.txtNPTopicText1.Value),
				new SqlParameter("@dynamicRecommendation2", this.txtNPTopicText2.Value),
				new SqlParameter("@dynamicRecommendation3", this.txtNPTopicText3.Value),
				new SqlParameter("@dynamicRecommendation4", this.txtNPTopicText4.Value),
				new SqlParameter("@dynamicRecommendation5", this.txtNPTopicText5.Value),
				new SqlParameter("@dynamicRecommendation6", this.txtNPTopicText6.Value),
				new SqlParameter("@dynamicRecommendation7", this.txtNPTopicText7.Value),
				new SqlParameter("@dynamicRecommendation8", this.txtNPTopicText8.Value)
			});
            this.BindGrid(0, this.grdNPSuggestions, 5);
        }

        protected void grdNPSuggestions_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                this.pnlNPEdit.Visible = true;
                this.pnlNPList.Visible = false;
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageCMCRecommendations", new SqlParameter[]
				{
					new SqlParameter("@Id", e.CommandArgument),
					new SqlParameter("@flag", "2")
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.txtNPLevel.Text = ds.Tables[0].Rows[0]["level"].ToString();
                        this.txtNPScale.Text = ds.Tables[0].Rows[0]["RatingScale"].ToString();
                        this.txtNPScore.Text = ds.Tables[0].Rows[0]["Score"].ToString();
                        this.txtNPRecommendation.Value = ds.Tables[0].Rows[0]["recommendation"].ToString();
                        this.imgNPEdit.CommandArgument = ds.Tables[0].Rows[0]["Id"].ToString();
                    }
                }
                DataSet dsDynamicRecommendation = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetNPDynamicRecommendation", new SqlParameter[]
				{
					new SqlParameter("@Id", e.CommandArgument)
				});
                if (dsDynamicRecommendation != null)
                {
                    if (dsDynamicRecommendation.Tables.Count > 0 && dsDynamicRecommendation.Tables[0].Rows.Count > 0)
                    {
                        this.lblNPTopic1.Text = dsDynamicRecommendation.Tables[0].Rows[0]["topicName"].ToString();
                        this.lblNPTopic2.Text = dsDynamicRecommendation.Tables[0].Rows[1]["topicName"].ToString();
                        this.lblNPTopic3.Text = dsDynamicRecommendation.Tables[0].Rows[2]["topicName"].ToString();
                        this.lblNPTopic4.Text = dsDynamicRecommendation.Tables[0].Rows[3]["topicName"].ToString();
                        this.lblNPTopic5.Text = dsDynamicRecommendation.Tables[0].Rows[4]["topicName"].ToString();
                        this.lblNPTopic6.Text = dsDynamicRecommendation.Tables[0].Rows[5]["topicName"].ToString();
                        this.lblNPTopic7.Text = dsDynamicRecommendation.Tables[0].Rows[6]["topicName"].ToString();
                        this.lblNPTopic8.Text = dsDynamicRecommendation.Tables[0].Rows[7]["topicName"].ToString();
                        this.txtNPTopicText1.Value = dsDynamicRecommendation.Tables[0].Rows[0]["dynamicRecommendation"].ToString();
                        this.txtNPTopicText2.Value = dsDynamicRecommendation.Tables[0].Rows[1]["dynamicRecommendation"].ToString();
                        this.txtNPTopicText3.Value = dsDynamicRecommendation.Tables[0].Rows[2]["dynamicRecommendation"].ToString();
                        this.txtNPTopicText4.Value = dsDynamicRecommendation.Tables[0].Rows[3]["dynamicRecommendation"].ToString();
                        this.txtNPTopicText5.Value = dsDynamicRecommendation.Tables[0].Rows[4]["dynamicRecommendation"].ToString();
                        this.txtNPTopicText6.Value = dsDynamicRecommendation.Tables[0].Rows[5]["dynamicRecommendation"].ToString();
                        this.txtNPTopicText7.Value = dsDynamicRecommendation.Tables[0].Rows[6]["dynamicRecommendation"].ToString();
                        this.txtNPTopicText8.Value = dsDynamicRecommendation.Tables[0].Rows[7]["dynamicRecommendation"].ToString();
                        this.hdNPId1.Value = dsDynamicRecommendation.Tables[0].Rows[0]["id"].ToString();
                        this.hdNPId2.Value = dsDynamicRecommendation.Tables[0].Rows[1]["id"].ToString();
                        this.hdNPId3.Value = dsDynamicRecommendation.Tables[0].Rows[2]["id"].ToString();
                        this.hdNPId4.Value = dsDynamicRecommendation.Tables[0].Rows[3]["id"].ToString();
                        this.hdNPId5.Value = dsDynamicRecommendation.Tables[0].Rows[4]["id"].ToString();
                        this.hdNPId6.Value = dsDynamicRecommendation.Tables[0].Rows[5]["id"].ToString();
                        this.hdNPId7.Value = dsDynamicRecommendation.Tables[0].Rows[6]["id"].ToString();
                        this.hdNPId8.Value = dsDynamicRecommendation.Tables[0].Rows[7]["id"].ToString();
                    }
                }
            }
        }

        protected void imgNPCancel_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlNPEdit.Visible = false;
            this.pnlNPList.Visible = true;
        }
    }
}
