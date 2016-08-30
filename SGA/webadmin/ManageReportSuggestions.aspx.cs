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
    public partial class ManageReportSuggestions : Page
    {
       

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.selected_tab.Value = base.Request.Form[this.selected_tab.UniqueID];
            if (!base.IsPostBack)
            {
                this.BindGrid(2, this.grdSSASuggestions);
                this.BindGrid(3, this.grdBASuggestions);
                this.BindGrid(6, this.grdCMASuggestions);
            }
        }

        private void BindGrid(int flag, DataGrid grd)
        {
            grd.DataSource = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSuggestions", new SqlParameter[]
			{
				new SqlParameter("@flag", flag)
			});
            grd.DataBind();
        }

        protected void imgSSAEdit_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlSSAEdit.Visible = false;
            this.pnlSSAList.Visible = true;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spGetSuggestionById", new SqlParameter[]
			{
				new SqlParameter("@Id", this.imgSSAEdit.CommandArgument),
				new SqlParameter("@flag", "1"),
				new SqlParameter("@suggestionText", this.txtSSASuggestion.Value),
				new SqlParameter("@considerationText", this.txtSSARecommendation.Value)
			});
            this.BindGrid(2, this.grdSSASuggestions);
        }

        protected void imgCMAEdit_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlCMAEdit.Visible = false;
            this.pnlCMAList.Visible = true;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spGetSuggestionById", new SqlParameter[]
			{
				new SqlParameter("@Id", this.imgCMAEdit.CommandArgument),
				new SqlParameter("@flag", "1"),
				new SqlParameter("@suggestionText", this.txtCMASuggestion.Value),
				new SqlParameter("@considerationText", this.txtCMARecommendation.Value)
			});
            this.BindGrid(6, this.grdCMASuggestions);
        }

        protected void imgBAEdit_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlBAEdit.Visible = false;
            this.pnlBAList.Visible = true;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spGetSuggestionById", new SqlParameter[]
			{
				new SqlParameter("@Id", this.imgBAEdit.CommandArgument),
				new SqlParameter("@flag", "1"),
				new SqlParameter("@suggestionText", this.txtBADefination.Value),
				new SqlParameter("@considerationText", this.txtBAConsideration.Value)
			});
            this.BindGrid(3, this.grdBASuggestions);
        }

        protected void imgBACancel_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlBAEdit.Visible = false;
            this.pnlBAList.Visible = true;
        }

        protected void imgSSACancel_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlSSAEdit.Visible = false;
            this.pnlSSAList.Visible = true;
        }

        protected void imgCMACancel_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlCMAEdit.Visible = false;
            this.pnlCMAList.Visible = true;
        }

        protected void grdSSASuggestions_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                this.pnlSSAEdit.Visible = true;
                this.pnlSSAList.Visible = false;
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSuggestionById", new SqlParameter[]
				{
					new SqlParameter("@Id", e.CommandArgument),
					new SqlParameter("@flag", "0")
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.txtSSARecommendation.Value = ds.Tables[0].Rows[0]["ConsiderationText"].ToString();
                        this.txtSSASuggestion.Value = ds.Tables[0].Rows[0]["SuggestionText"].ToString();
                        this.imgSSAEdit.CommandArgument = ds.Tables[0].Rows[0]["Id"].ToString();
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
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSuggestionById", new SqlParameter[]
				{
					new SqlParameter("@Id", e.CommandArgument),
					new SqlParameter("@flag", "0")
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.txtCMARecommendation.Value = ds.Tables[0].Rows[0]["ConsiderationText"].ToString();
                        this.txtCMASuggestion.Value = ds.Tables[0].Rows[0]["SuggestionText"].ToString();
                        this.imgCMAEdit.CommandArgument = ds.Tables[0].Rows[0]["Id"].ToString();
                    }
                }
            }
        }

        protected void grdBASuggestions_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                this.pnlBAEdit.Visible = true;
                this.pnlBAList.Visible = false;
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSuggestionById", new SqlParameter[]
				{
					new SqlParameter("@Id", e.CommandArgument),
					new SqlParameter("@flag", "0")
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.txtBAConsideration.Value = ds.Tables[0].Rows[0]["ConsiderationText"].ToString();
                        this.txtBADefination.Value = ds.Tables[0].Rows[0]["SuggestionText"].ToString();
                        this.imgBAEdit.CommandArgument = ds.Tables[0].Rows[0]["Id"].ToString();
                    }
                }
            }
        }
    }
}
