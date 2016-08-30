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
    public partial class ManagePlans : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                this.BindGrid(0, this.grdSSASuggestions, 1);
                this.BindGrid(0, this.grdCMASuggestions, 2);
            }
        }

        private void BindGrid(int flag, DataGrid grd, int testType)
        {
            grd.DataSource = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManagePlans", new SqlParameter[]
            {
                new SqlParameter("@flag", flag),
                new SqlParameter("@planType", testType)
            });
            grd.DataBind();
        }

        protected void grdSSASuggestions_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                this.pnlSSAEdit.Visible = true;
                this.pnlSSAList.Visible = false;
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManagePlans", new SqlParameter[]
                {
                    new SqlParameter("@planId", e.CommandArgument),
                    new SqlParameter("@flag", "2")
                });
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.txtSSAPlanName.Text = ds.Tables[0].Rows[0]["planName"].ToString();
                        this.txtSSAPlanDetails.Value = ds.Tables[0].Rows[0]["planDetails"].ToString();
                        this.txtSSAPlanCompletionTime.Text = ds.Tables[0].Rows[0]["timeWeek"].ToString();
                        this.imgSSAEdit.CommandArgument = ds.Tables[0].Rows[0]["planId"].ToString();
                    }
                }
            }
        }

        protected void imgSSAEdit_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlSSAEdit.Visible = false;
            this.pnlSSAList.Visible = true;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spManagePlans", new SqlParameter[]
            {
                new SqlParameter("@planId", this.imgSSAEdit.CommandArgument),
                new SqlParameter("@flag", "1"),
                new SqlParameter("@planName", this.txtSSAPlanName.Text),
                new SqlParameter("@planDetails", txtSSAPlanDetails.Value),
                new SqlParameter("@timeWeek", txtSSAPlanCompletionTime.Text)
            });
            this.BindGrid(0, this.grdSSASuggestions, 1);
        }

        protected void imgSSACancel_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlSSAEdit.Visible = false;
            this.pnlSSAList.Visible = true;
        }

        protected void grdCMASuggestions_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                this.pnlCMAEdit.Visible = true;
                this.pnlCMAList.Visible = false;
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManagePlans", new SqlParameter[]
                {
                    new SqlParameter("@planId", e.CommandArgument),
                    new SqlParameter("@flag", "2")
                });
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.txtCMAPlanName.Text = ds.Tables[0].Rows[0]["planName"].ToString();
                        this.txtCMAPlanDetails.Value = ds.Tables[0].Rows[0]["planDetails"].ToString();
                        this.txtCMAPlanCompletionTime.Text = ds.Tables[0].Rows[0]["timeWeek"].ToString();
                        this.imgCMAEdit.CommandArgument = ds.Tables[0].Rows[0]["planId"].ToString();
                    }
                }
            }
        }

        protected void imgCMAEdit_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlCMAEdit.Visible = false;
            this.pnlCMAList.Visible = true;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spManagePlans", new SqlParameter[]
            {
                new SqlParameter("@planId", this.imgCMAEdit.CommandArgument),
                new SqlParameter("@flag", "1"),
                new SqlParameter("@planName", this.txtCMAPlanName.Text),
                new SqlParameter("@planDetails", txtCMAPlanDetails.Value),
                new SqlParameter("@timeWeek",txtCMAPlanCompletionTime.Text)
            });
            this.BindGrid(0, this.grdCMASuggestions, 2);
        }

        protected void imgCMACancel_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlCMAEdit.Visible = false;
            this.pnlCMAList.Visible = true;
        }
    }
}