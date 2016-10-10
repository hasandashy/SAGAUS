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
                this.BindGrid(0, this.grdSSASuggestions, 1);
                this.BindGrid(0, this.grdCMASuggestions, 2);
                this.BindGrid(0, this.grdNPSuggestions, 3);
                this.BindGrid(0, this.grdProcSugg, 4);
                this.BindGrid(0, this.grdCMSugg, 5);
            }
        }

        private void BindGrid(int flag, DataGrid grd, int testType)
        {
            grd.DataSource = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageLevelRecommendations", new SqlParameter[]
			{
				new SqlParameter("@flag", flag),
				new SqlParameter("@reportType", testType)
			});
            grd.DataBind();
        }

        protected void grdSSASuggestions_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                this.pnlSSAEdit.Visible = true;
                this.pnlSSAList.Visible = false;
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageLevelRecommendations", new SqlParameter[]
				{
					new SqlParameter("@Id", e.CommandArgument),
					new SqlParameter("@flag", "2")
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.txtSSALevel.Text = ds.Tables[0].Rows[0]["level"].ToString();                      
                        this.txtSSARecommendation.Value = ds.Tables[0].Rows[0]["openingStatement"].ToString();
                        this.imgSSAEdit.CommandArgument = ds.Tables[0].Rows[0]["Id"].ToString();
                    }
                }
                DataSet dsDynamicRecommendation = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetProcureDynamicRecommendation", new SqlParameter[]
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
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageLevelRecommendations", new SqlParameter[]
				{
					new SqlParameter("@Id", e.CommandArgument),
					new SqlParameter("@flag", "2")
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.txtCMALevel.Text = ds.Tables[0].Rows[0]["level"].ToString();
                   
                        this.txtCMARecommendation.Value = ds.Tables[0].Rows[0]["openingStatement"].ToString();
                        this.imgCMAEdit.CommandArgument = ds.Tables[0].Rows[0]["Id"].ToString();
                    }
                }
                DataSet dsDynamicRecommendation = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetContractDynamicRecommendation", new SqlParameter[]
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
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spManageLevelRecommendations", new SqlParameter[]
			{
				new SqlParameter("@Id", this.imgSSAEdit.CommandArgument),
				new SqlParameter("@flag", "1"),
				new SqlParameter("@level", this.txtSSALevel.Text),			
				new SqlParameter("@openingStatement", this.txtSSARecommendation.Value)
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
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spManageLevelRecommendations", new SqlParameter[]
			{
				new SqlParameter("@Id", this.imgCMAEdit.CommandArgument),
				new SqlParameter("@flag", "1"),
				new SqlParameter("@level", this.txtCMALevel.Text),
				new SqlParameter("@openingStatement", this.txtCMARecommendation.Value)
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
            this.BindGrid(0, this.grdCMASuggestions, 2);
        }

        protected void imgCMACancel_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlCMAEdit.Visible = false;
            this.pnlCMAList.Visible = true;
        }

       

        protected void imgNPEdit_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlNPEdit.Visible = false;
            this.pnlNPList.Visible = true;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spManageLevelRecommendations", new SqlParameter[]
			{
				new SqlParameter("@Id", this.imgNPEdit.CommandArgument),
				new SqlParameter("@flag", "1"),
				new SqlParameter("@level", this.txtNPLevel.Text),
				new SqlParameter("@openingStatement", this.txtNPRecommendation.Value)
			});
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateDynamicRecommendation", new SqlParameter[]
			{
				new SqlParameter("@Id1", this.hdNPId1.Value),
				new SqlParameter("@Id2", this.hdNPId2.Value),
				new SqlParameter("@Id3", this.hdNPId3.Value),
				new SqlParameter("@Id4", this.hdNPId4.Value),
				new SqlParameter("@Id5", this.hdNPId5.Value),
				new SqlParameter("@dynamicRecommendation1", this.txtNPTopicText1.Value),
				new SqlParameter("@dynamicRecommendation2", this.txtNPTopicText2.Value),
				new SqlParameter("@dynamicRecommendation3", this.txtNPTopicText3.Value),
				new SqlParameter("@dynamicRecommendation4", this.txtNPTopicText4.Value),
				new SqlParameter("@dynamicRecommendation5", this.txtNPTopicText5.Value)
			});
            this.BindGrid(0, this.grdNPSuggestions, 3);
        }

        protected void grdNPSuggestions_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                this.pnlNPEdit.Visible = true;
                this.pnlNPList.Visible = false;
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageLevelRecommendations", new SqlParameter[]
				{
					new SqlParameter("@Id", e.CommandArgument),
					new SqlParameter("@flag", "2")
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.txtNPLevel.Text = ds.Tables[0].Rows[0]["level"].ToString();
                        this.txtNPRecommendation.Value = ds.Tables[0].Rows[0]["openingStatement"].ToString();
                        this.imgNPEdit.CommandArgument = ds.Tables[0].Rows[0]["Id"].ToString();
                    }
                }
                DataSet dsDynamicRecommendation = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetCAADynamicRecommendation", new SqlParameter[]
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
                        this.txtNPTopicText1.Value = dsDynamicRecommendation.Tables[0].Rows[0]["dynamicRecommendation"].ToString();
                        this.txtNPTopicText2.Value = dsDynamicRecommendation.Tables[0].Rows[1]["dynamicRecommendation"].ToString();
                        this.txtNPTopicText3.Value = dsDynamicRecommendation.Tables[0].Rows[2]["dynamicRecommendation"].ToString();
                        this.txtNPTopicText4.Value = dsDynamicRecommendation.Tables[0].Rows[3]["dynamicRecommendation"].ToString();
                        this.txtNPTopicText5.Value = dsDynamicRecommendation.Tables[0].Rows[4]["dynamicRecommendation"].ToString();
                        this.hdNPId1.Value = dsDynamicRecommendation.Tables[0].Rows[0]["id"].ToString();
                        this.hdNPId2.Value = dsDynamicRecommendation.Tables[0].Rows[1]["id"].ToString();
                        this.hdNPId3.Value = dsDynamicRecommendation.Tables[0].Rows[2]["id"].ToString();
                        this.hdNPId4.Value = dsDynamicRecommendation.Tables[0].Rows[3]["id"].ToString();
                        this.hdNPId5.Value = dsDynamicRecommendation.Tables[0].Rows[4]["id"].ToString();
                    }
                }
            }
        }

        protected void imgNPCancel_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlNPEdit.Visible = false;
            this.pnlNPList.Visible = true;
        }

        protected void ImgProcSgEdit_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlProcSuggEdit.Visible = false;
            this.pnlProcSuggList.Visible = true;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spManageLevelRecommendations", new SqlParameter[]
            {
                new SqlParameter("@Id", this.ImgProcSgEdit.CommandArgument),
                new SqlParameter("@flag", "1"),
                new SqlParameter("@level", this.txtProcSugglevel.Text),
                new SqlParameter("@openingStatement", this.txtProcSuggRecommendation.Value)
            });
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateDynamicRecommendation", new SqlParameter[]
            {
              new SqlParameter("@Id1", this.hdProcSuggId1.Value),
                new SqlParameter("@Id2", this.hdProcSuggId2.Value),
                new SqlParameter("@Id3", this.hdProcSuggId3.Value),
                new SqlParameter("@Id4", this.hdProcSuggId4.Value),
                new SqlParameter("@Id5", this.hdProcSuggId5.Value),
                new SqlParameter("@Id6", this.hdProcSuggId6.Value),
                new SqlParameter("@Id7", this.hdProcSuggId7.Value),
                new SqlParameter("@Id8", this.hdProcSuggId8.Value),
                new SqlParameter("@dynamicRecommendation1", this.txtProcSuggTxt1.Value),
                new SqlParameter("@dynamicRecommendation2", this.txtProcSuggTxt2.Value),
                new SqlParameter("@dynamicRecommendation3", this.txtProcSuggTxt3.Value),
                new SqlParameter("@dynamicRecommendation4", this.txtProcSuggTxt4.Value),
                new SqlParameter("@dynamicRecommendation5", this.txtProcSuggTxt5.Value),
                new SqlParameter("@dynamicRecommendation6", this.txtProcSuggTxt6.Value),
                new SqlParameter("@dynamicRecommendation7", this.txtProcSuggTxt7.Value),
                new SqlParameter("@dynamicRecommendation8", this.txtProcSuggTxt8.Value)
            });
            this.BindGrid(0, this.grdProcSugg, 4);
        }

        protected void grrdProcSugg_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                this.pnlProcSuggEdit.Visible = true;
                this.pnlProcSuggList.Visible = false;
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageLevelRecommendations", new SqlParameter[]
                {
                    new SqlParameter("@Id", e.CommandArgument),
                    new SqlParameter("@flag", "2")
                });
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.txtProcSugglevel.Text = ds.Tables[0].Rows[0]["level"].ToString();
                        this.txtProcSuggRecommendation.Value = ds.Tables[0].Rows[0]["openingStatement"].ToString();
                        this.ImgProcSgEdit.CommandArgument = ds.Tables[0].Rows[0]["Id"].ToString();
                    }
                }
                DataSet dsDynamicRecommendation = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetProcSGDynamicRecommendation", new SqlParameter[]
                {
                    new SqlParameter("@Id", e.CommandArgument)
                });
                if (dsDynamicRecommendation != null)
                {
                    if (dsDynamicRecommendation.Tables.Count > 0 && dsDynamicRecommendation.Tables[0].Rows.Count > 0)
                    {
                        this.lblProcSuggTopic1.Text = dsDynamicRecommendation.Tables[0].Rows[0]["topicName"].ToString();
                        this.lblProcSuggTopic2.Text = dsDynamicRecommendation.Tables[0].Rows[1]["topicName"].ToString();
                        this.lblProcSuggTopic3.Text = dsDynamicRecommendation.Tables[0].Rows[2]["topicName"].ToString();
                        this.lblProcSuggTopic4.Text = dsDynamicRecommendation.Tables[0].Rows[3]["topicName"].ToString();
                        this.lblProcSuggTopic5.Text = dsDynamicRecommendation.Tables[0].Rows[4]["topicName"].ToString();
                        this.lblProcSuggTopic6.Text = dsDynamicRecommendation.Tables[0].Rows[5]["topicName"].ToString();
                        this.lblProcSuggTopic7.Text = dsDynamicRecommendation.Tables[0].Rows[6]["topicName"].ToString();
                        this.lblProcSuggTopic8.Text = dsDynamicRecommendation.Tables[0].Rows[7]["topicName"].ToString();
                        this.txtProcSuggTxt1.Value = dsDynamicRecommendation.Tables[0].Rows[0]["dynamicRecommendation"].ToString();
                        this.txtProcSuggTxt2.Value = dsDynamicRecommendation.Tables[0].Rows[1]["dynamicRecommendation"].ToString();
                        this.txtProcSuggTxt3.Value = dsDynamicRecommendation.Tables[0].Rows[2]["dynamicRecommendation"].ToString();
                        this.txtProcSuggTxt4.Value = dsDynamicRecommendation.Tables[0].Rows[3]["dynamicRecommendation"].ToString();
                        this.txtProcSuggTxt5.Value = dsDynamicRecommendation.Tables[0].Rows[4]["dynamicRecommendation"].ToString();
                        this.txtProcSuggTxt6.Value = dsDynamicRecommendation.Tables[0].Rows[5]["dynamicRecommendation"].ToString();
                        this.txtProcSuggTxt7.Value = dsDynamicRecommendation.Tables[0].Rows[6]["dynamicRecommendation"].ToString();
                        this.txtProcSuggTxt8.Value = dsDynamicRecommendation.Tables[0].Rows[7]["dynamicRecommendation"].ToString();
                        this.hdProcSuggId1.Value = dsDynamicRecommendation.Tables[0].Rows[0]["id"].ToString();
                        this.hdProcSuggId2.Value = dsDynamicRecommendation.Tables[0].Rows[1]["id"].ToString();
                        this.hdProcSuggId3.Value = dsDynamicRecommendation.Tables[0].Rows[2]["id"].ToString();
                        this.hdProcSuggId4.Value = dsDynamicRecommendation.Tables[0].Rows[3]["id"].ToString();
                        this.hdProcSuggId5.Value = dsDynamicRecommendation.Tables[0].Rows[4]["id"].ToString();
                        this.hdProcSuggId6.Value = dsDynamicRecommendation.Tables[0].Rows[5]["id"].ToString();
                        this.hdProcSuggId7.Value = dsDynamicRecommendation.Tables[0].Rows[6]["id"].ToString();
                        this.hdProcSuggId8.Value = dsDynamicRecommendation.Tables[0].Rows[7]["id"].ToString();
                    }
                }
            }
        }

        protected void ImgProcSgCancel_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlProcSuggEdit.Visible = false;
            this.pnlProcSuggList.Visible = true;
        }

        protected void imgCMSgEdit_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlCMSuggEdit.Visible = false;
            this.pnlCMSuggList.Visible = true;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spManageLevelRecommendations", new SqlParameter[]
            {
                new SqlParameter("@Id", this.imgCMSgEdit.CommandArgument),
                new SqlParameter("@flag", "1"),
                new SqlParameter("@level", this.txtCMSuggLevel.Text),
                new SqlParameter("@openingStatement", this.txtCMSuggRecommendation.Value)
            });
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateDynamicRecommendation", new SqlParameter[]
            {
              new SqlParameter("@Id1", this.hdCMSuggId1.Value),
                new SqlParameter("@Id2", this.hdCMSuggId2.Value),
                new SqlParameter("@Id3", this.hdCMSuggId3.Value),
                new SqlParameter("@Id4", this.hdCMSuggId4.Value),
                new SqlParameter("@Id5", this.hdProcSuggId5.Value),
                new SqlParameter("@Id6", this.hdCMSuggId6.Value),
                new SqlParameter("@Id7", this.hdCMSuggId7.Value),
                new SqlParameter("@Id8", this.hdCMSuggId8.Value),
                new SqlParameter("@dynamicRecommendation1", this.txtCMSuggTxt1.Value),
                new SqlParameter("@dynamicRecommendation2", this.txtCMSuggTxt2.Value),
                new SqlParameter("@dynamicRecommendation3", this.txtCMSuggTxt3.Value),
                new SqlParameter("@dynamicRecommendation4", this.txtCMSuggTxt4.Value),
                new SqlParameter("@dynamicRecommendation5", this.txtCMSuggTxt5.Value),
                new SqlParameter("@dynamicRecommendation6", this.txtCMSuggTxt6.Value),
                new SqlParameter("@dynamicRecommendation7", this.txtCMSuggTxt7.Value),
                new SqlParameter("@dynamicRecommendation8", this.txtCMSuggTxt8.Value)
            });
            this.BindGrid(0, this.grdCMSugg, 4);
        }

        protected void grdCMSugg_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                this.pnlCMSuggEdit.Visible = true;
                this.pnlCMSuggList.Visible = false;
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageLevelRecommendations", new SqlParameter[]
                {
                    new SqlParameter("@Id", e.CommandArgument),
                    new SqlParameter("@flag", "2")
                });
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.txtCMSuggLevel.Text = ds.Tables[0].Rows[0]["level"].ToString();
                        this.txtCMSuggRecommendation.Value = ds.Tables[0].Rows[0]["openingStatement"].ToString();
                        this.imgCMSgEdit.CommandArgument = ds.Tables[0].Rows[0]["Id"].ToString();
                    }
                }
                DataSet dsDynamicRecommendation = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetCMSGDynamicRecommendation", new SqlParameter[]
                {
                    new SqlParameter("@Id", e.CommandArgument)
                });
                if (dsDynamicRecommendation != null)
                {
                    if (dsDynamicRecommendation.Tables.Count > 0 && dsDynamicRecommendation.Tables[0].Rows.Count > 0)
                    {
                        this.lblCMSuggTopic1.Text = dsDynamicRecommendation.Tables[0].Rows[0]["topicName"].ToString();
                        this.lblCMSuggTopic2.Text = dsDynamicRecommendation.Tables[0].Rows[1]["topicName"].ToString();
                        this.lblCMSuggTopic3.Text = dsDynamicRecommendation.Tables[0].Rows[2]["topicName"].ToString();
                        this.lblCMSuggTopic4.Text = dsDynamicRecommendation.Tables[0].Rows[3]["topicName"].ToString();
                        this.lblCMSuggTopic5.Text = dsDynamicRecommendation.Tables[0].Rows[4]["topicName"].ToString();
                        this.lblCMSuggTopic6.Text = dsDynamicRecommendation.Tables[0].Rows[5]["topicName"].ToString();
                        this.lblCMSuggTopic7.Text = dsDynamicRecommendation.Tables[0].Rows[6]["topicName"].ToString();
                        this.lblCMSuggTopic8.Text = dsDynamicRecommendation.Tables[0].Rows[7]["topicName"].ToString();
                        this.txtCMSuggTxt1.Value = dsDynamicRecommendation.Tables[0].Rows[0]["dynamicRecommendation"].ToString();
                        this.txtCMSuggTxt2.Value = dsDynamicRecommendation.Tables[0].Rows[1]["dynamicRecommendation"].ToString();
                        this.txtCMSuggTxt3.Value = dsDynamicRecommendation.Tables[0].Rows[2]["dynamicRecommendation"].ToString();
                        this.txtCMSuggTxt4.Value = dsDynamicRecommendation.Tables[0].Rows[3]["dynamicRecommendation"].ToString();
                        this.txtCMSuggTxt5.Value = dsDynamicRecommendation.Tables[0].Rows[4]["dynamicRecommendation"].ToString();
                        this.txtCMSuggTxt6.Value = dsDynamicRecommendation.Tables[0].Rows[5]["dynamicRecommendation"].ToString();
                        this.txtCMSuggTxt7.Value = dsDynamicRecommendation.Tables[0].Rows[6]["dynamicRecommendation"].ToString();
                        this.txtCMSuggTxt8.Value = dsDynamicRecommendation.Tables[0].Rows[7]["dynamicRecommendation"].ToString();
                        this.hdCMSuggId1.Value = dsDynamicRecommendation.Tables[0].Rows[0]["id"].ToString();
                        this.hdCMSuggId2.Value = dsDynamicRecommendation.Tables[0].Rows[1]["id"].ToString();
                        this.hdCMSuggId3.Value = dsDynamicRecommendation.Tables[0].Rows[2]["id"].ToString();
                        this.hdCMSuggId4.Value = dsDynamicRecommendation.Tables[0].Rows[3]["id"].ToString();
                        this.hdCMSuggId5.Value = dsDynamicRecommendation.Tables[0].Rows[4]["id"].ToString();
                        this.hdCMSuggId6.Value = dsDynamicRecommendation.Tables[0].Rows[5]["id"].ToString();
                        this.hdCMSuggId7.Value = dsDynamicRecommendation.Tables[0].Rows[6]["id"].ToString();
                        this.hdCMSuggId8.Value = dsDynamicRecommendation.Tables[0].Rows[7]["id"].ToString();
                    }
                }
            }
        }

        protected void imgCMSgCancel_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlCMSuggEdit.Visible = false;
            this.pnlCMSuggList.Visible = true;
        }
    }
}
