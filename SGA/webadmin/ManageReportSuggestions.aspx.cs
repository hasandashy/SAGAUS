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
                this.BindGrid(1, this.grdSSASuggestions);             
                this.BindGrid(2, this.grdContractManagerSuggestions);               
                this.BindGrid(3, this.grdCAASuggestions);
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
            this.BindGrid(1, this.grdSSASuggestions);
        }

      

     

        protected void imgCAAEdit_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlCAAEdit.Visible = false;
            this.pnlCAAList.Visible = true;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spGetSuggestionById", new SqlParameter[]
			{
				new SqlParameter("@Id", this.ImgCAAEdit.CommandArgument),
				new SqlParameter("@flag", "1"),
				new SqlParameter("@suggestionText", this.txtCAADefination.Value),
				new SqlParameter("@considerationText", this.txtCAAConsidration.Value)
			});
            this.BindGrid(3, this.grdCAASuggestions);
        }

    

        protected void imgSSACancel_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlSSAEdit.Visible = false;
            this.pnlSSAList.Visible = true;
        }

       
        protected void imgCAACancel_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlCAAEdit.Visible = false;
            this.pnlCAAList.Visible = true;
        }


        protected void ImgContractManagerEdit_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlContractManagerEdit.Visible = false;
            this.pnlContractManagerList.Visible = true;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spGetSuggestionById", new SqlParameter[]
            {
                new SqlParameter("@Id", this.ImgContractManagerEdit.CommandArgument),
                new SqlParameter("@flag", "1"),
                new SqlParameter("@suggestionText", this.txtContractManagerDefination.Value),
                new SqlParameter("@considerationText", this.txtContractManagerConsidration.Value)
            });
            this.BindGrid(2, this.grdContractManagerSuggestions);
        }

        protected void ImgContractManagerCancel_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlContractManagerEdit.Visible = false;
            this.pnlContractManagerList.Visible = true;
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

        protected void grdCAASuggestions_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                this.pnlCAAEdit.Visible = true;
                this.pnlCAAList.Visible = false;
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSuggestionById", new SqlParameter[]
				{
					new SqlParameter("@Id", e.CommandArgument),
					new SqlParameter("@flag", "0")
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.txtCAAConsidration.Value = ds.Tables[0].Rows[0]["ConsiderationText"].ToString();
                        this.txtCAADefination.Value = ds.Tables[0].Rows[0]["SuggestionText"].ToString();
                        this.ImgCAAEdit.CommandArgument = ds.Tables[0].Rows[0]["Id"].ToString();
                    }
                }
            }
        }


     

        protected void grdContractManagerSuggestions_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                this.pnlContractManagerList.Visible = false;
                this.pnlContractManagerEdit.Visible = true;
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSuggestionById", new SqlParameter[]
                {
                    new SqlParameter("@Id", e.CommandArgument),
                    new SqlParameter("@flag", "0")
                });
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.txtContractManagerConsidration.Value = ds.Tables[0].Rows[0]["ConsiderationText"].ToString();
                        this.txtContractManagerDefination.Value = ds.Tables[0].Rows[0]["SuggestionText"].ToString();
                        this.ImgContractManagerEdit.CommandArgument = ds.Tables[0].Rows[0]["Id"].ToString();
                    }
                }
            }
        }

     

     
    }
}
