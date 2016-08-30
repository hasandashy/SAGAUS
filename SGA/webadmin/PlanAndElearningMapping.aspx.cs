using DataTier;
using FredCK.FCKeditorV2;
using SGA.controls;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.App_Code;

namespace SGA.webadmin
{
    public partial class PlanAndElearningMapping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindGrid(0, this.grdSSASuggestions, 1);
                this.BindGrid(0, this.grdCMASuggestions, 2);
                this.BindPlans(0, this.ddlSSAUserPlan, 1);
                this.BindPlans(0, this.ddlSSANegPlan, 1);
                this.BindPlans(0, this.ddlSSASRMPlan, 1);
                this.BindPlans(0, this.ddlSSAOAPlan, 1);
                this.BindPlans(0, this.ddlSSASDPlan, 1);
                this.BindPlans(0, this.ddlSSARSPlan, 1);
                this.BindPlans(0, this.ddlCMAUserPlan, 2);
                this.BindPlans(0, this.ddlCMACAPlan, 2);
                this.BindPlans(0, this.ddlCMABRPlan, 2);
                this.BindPlans(0, this.ddlCMAPGPlan, 2);
            }
        }

        private void BindGrid(int flag, DataGrid grd, int testType)
        {
            grd.DataSource = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManagePlansMapping", new SqlParameter[]
            {
                new SqlParameter("@flag", flag),
                new SqlParameter("@tableType", testType)
            });
            grd.DataBind();
        }

        private void BindPlans(int flag, DropDownList ddl, int testType)
        {
            ddl.DataSource = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManagePlans", new SqlParameter[]
            {
                new SqlParameter("@flag", flag),
                new SqlParameter("@planType", testType)
            });
            ddl.DataTextField = "PlanName";
            ddl.DataValueField = "PlanId";
            ddl.DataBind();
        }

        protected void grdSSASuggestions_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                this.pnlSSAEdit.Visible = true;
                this.pnlSSAList.Visible = false;
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManagePlansMapping", new SqlParameter[]
                {
                    new SqlParameter("@Id", e.CommandArgument),
                    new SqlParameter("@flag", "2")
                });
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        lblSSAScore.Text = ds.Tables[0].Rows[0]["minPercentage"].ToString() + "% - " + ds.Tables[0].Rows[0]["maxPercentage"].ToString() + "%";
                        SGACommon.SelectListItem(ddlSSAUserPlan, ds.Tables[0].Rows[0]["allUserPlanId"].ToString());
                        SGACommon.SelectListItem(ddlSSANegPlan, ds.Tables[0].Rows[0]["negotiateTopicPlanId"].ToString());
                        SGACommon.SelectListItem(ddlSSASRMPlan, ds.Tables[0].Rows[0]["SrmTopicPlanId"].ToString());
                        SGACommon.SelectListItem(ddlSSAOAPlan, ds.Tables[0].Rows[0]["opportunityAnalysisTopicPlanId"].ToString());
                        SGACommon.SelectListItem(ddlSSASDPlan, ds.Tables[0].Rows[0]["sdTopicPlanId"].ToString());
                        SGACommon.SelectListItem(ddlSSARSPlan, ds.Tables[0].Rows[0]["rsTopicPlanId"].ToString());
                        this.imgSSAEdit.CommandArgument = ds.Tables[0].Rows[0]["Id"].ToString();
                    }
                }
            }
        }

        protected void imgSSAEdit_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlSSAEdit.Visible = false;
            this.pnlSSAList.Visible = true;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spManagePlansMapping", new SqlParameter[]
            {
                new SqlParameter("@planId", this.imgSSAEdit.CommandArgument),
                new SqlParameter("@flag", "1"),
                new SqlParameter("@allUserPlanId", Convert.ToInt32(ddlSSAUserPlan.SelectedValue)),
                new SqlParameter("@negotiateTopicPlanId", Convert.ToInt32(ddlSSANegPlan.SelectedValue)),
                new SqlParameter("@SrmTopicPlanId", Convert.ToInt32(ddlSSASRMPlan.SelectedValue)),
                new SqlParameter("@opportunityAnalysisTopicPlanId", Convert.ToInt32(ddlSSAOAPlan.SelectedValue)),
                new SqlParameter("@sdTopicPlanId", Convert.ToInt32(ddlSSASDPlan.SelectedValue)),
                new SqlParameter("@rsTopicPlanId", Convert.ToInt32(ddlSSARSPlan.SelectedValue))
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
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManagePlansMapping", new SqlParameter[]
                {
                    new SqlParameter("@Id", e.CommandArgument),
                    new SqlParameter("@flag", "2")
                });
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        lblCMAScore.Text = ds.Tables[0].Rows[0]["minPercentage"].ToString() + "% - "+ ds.Tables[0].Rows[0]["maxPercentage"].ToString() + "%";
                        SGACommon.SelectListItem(ddlCMAUserPlan, ds.Tables[0].Rows[0]["allUserPlanId"].ToString());
                        SGACommon.SelectListItem(ddlCMACAPlan, ds.Tables[0].Rows[0]["negotiateTopicPlanId"].ToString());
                        SGACommon.SelectListItem(ddlCMABRPlan, ds.Tables[0].Rows[0]["SrmTopicPlanId"].ToString());
                        SGACommon.SelectListItem(ddlCMAPGPlan, ds.Tables[0].Rows[0]["opportunityAnalysisTopicPlanId"].ToString());
                        this.imgCMAEdit.CommandArgument = ds.Tables[0].Rows[0]["Id"].ToString();
                    }
                }
            }
        }

        protected void imgCMAEdit_Click(object sender, ImageClickEventArgs e)
        {
            this.pnlCMAEdit.Visible = false;
            this.pnlCMAList.Visible = true;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spManagePlansMapping", new SqlParameter[]
            {
                new SqlParameter("@Id", this.imgCMAEdit.CommandArgument),
                new SqlParameter("@flag", "1"),
                new SqlParameter("@allUserPlanId", Convert.ToInt32(ddlCMAUserPlan.SelectedValue)),
                new SqlParameter("@negotiateTopicPlanId", Convert.ToInt32(ddlCMACAPlan.SelectedValue)),
                new SqlParameter("@SrmTopicPlanId", Convert.ToInt32(ddlCMABRPlan.SelectedValue)),
                new SqlParameter("@opportunityAnalysisTopicPlanId", Convert.ToInt32(ddlCMAPGPlan.SelectedValue))
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