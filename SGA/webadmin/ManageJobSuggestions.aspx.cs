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
    public partial class ManageJobSuggestions : Page
    {
        

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.BindJobSuggestions();
            }
        }

        private void BindJobSuggestions()
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageJobRoleSuggestion", new SqlParameter[]
			{
				new SqlParameter("@id", "0"),
				new SqlParameter("@jobSuggestion", ""),
				new SqlParameter("@flag", "0"),
				new SqlParameter("@page14Para1", ""),
				new SqlParameter("@page14Para2", "")
			});
            this.rptUsers.DataSource = ds;
            this.rptUsers.DataBind();
        }

        protected void rptUsers_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                this.pnlSSAEdit.Visible = true;
                this.pnlList.Visible = false;
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageJobRoleSuggestion", new SqlParameter[]
				{
					new SqlParameter("@id", System.Convert.ToInt32(e.CommandArgument)),
					new SqlParameter("@jobSuggestion", ""),
					new SqlParameter("@flag", "1"),
					new SqlParameter("@page14Para1", ""),
					new SqlParameter("@page14Para2", "")
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.lblJobRole.Text = ds.Tables[0].Rows[0]["jobRole"].ToString();
                        this.txtJobRoleSuggestion.Value = ds.Tables[0].Rows[0]["jobSuggestion"].ToString();
                        this.imgSSAEdit.CommandArgument = e.CommandArgument.ToString();
                        this.txtPage14Para1.Value = ds.Tables[0].Rows[0]["page14Para1"].ToString();
                        this.txtPage14Para2.Value = ds.Tables[0].Rows[0]["page14Para2"].ToString();
                    }
                }
            }
        }

        protected void imgSSAEdit_Click(object sender, ImageClickEventArgs e)
        {
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spManageJobRoleSuggestion", new SqlParameter[]
			{
				new SqlParameter("@id", this.imgSSAEdit.CommandArgument),
				new SqlParameter("@jobSuggestion", this.txtJobRoleSuggestion.Value),
				new SqlParameter("@flag", "2"),
				new SqlParameter("@page14Para1", this.txtPage14Para1.Value),
				new SqlParameter("@page14Para2", this.txtPage14Para2.Value)
			});
            this.pnlSSAEdit.Visible = false;
            this.pnlList.Visible = true;
            this.BindJobSuggestions();
        }

        protected void imgSSACancel_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlSSAEdit.Visible = false;
            this.pnlList.Visible = true;
        }
    }
}
