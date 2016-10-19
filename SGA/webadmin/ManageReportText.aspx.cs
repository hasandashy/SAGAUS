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
    public partial class ManageReportText : Page
    {
       

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.selected_tab.Value = base.Request.Form[this.selected_tab.UniqueID];
            if (!base.IsPostBack)
            {
                this.BindSSAReportText();
                //this.BindBAReportText();
                this.BindCMAReportText();
                //this.BindNPReportText();
            }
        }

       

        private void BindSSAReportText()
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageProcurementText", new SqlParameter[]
			{
				new SqlParameter("@flag", 2)
			});
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    this.txtSSAheading.Text = ds.Tables[0].Rows[0]["page2Heading"].ToString();
                    this.txtSSA3Text.Value = ds.Tables[0].Rows[0]["page2Text"].ToString();
                    this.txtSSA4Text.Value = ds.Tables[0].Rows[0]["page2para2"].ToString();
                    this.iBtnSaveSSA.CommandArgument = ds.Tables[0].Rows[0]["Id"].ToString();
                }
            }
        }

        private void BindCMAReportText()
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageCMAText", new SqlParameter[]
            {
                new SqlParameter("@flag", 2)
            });
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    this.txtCMAheading.Text = ds.Tables[0].Rows[0]["page1Heading"].ToString();
                    this.txtCMA3Text.Value = ds.Tables[0].Rows[0]["page1Text"].ToString();                   
                    this.iBtnSaveCMA.CommandArgument = ds.Tables[0].Rows[0]["Id"].ToString();
                }
            }
        }



        protected void iBtnSaveSSA_Click(object sender, ImageClickEventArgs e)
        {
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spManageProcurementText", new SqlParameter[]
			{
				new SqlParameter("@flag", 1),
				new SqlParameter("@page2Heading", this.txtSSAheading.Text.Trim()),
				new SqlParameter("@page2Text", this.txtSSA3Text.Value.Trim()),
                new SqlParameter("@page2para2", this.txtSSA4Text.Value.Trim()),
                new SqlParameter("@Id", this.iBtnSaveSSA.CommandArgument)
            });
            this.BindSSAReportText();
        }

        protected void iBtnSaveCMA_Click(object sender, ImageClickEventArgs e)
        {
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spManageCMAText", new SqlParameter[]
            {
                new SqlParameter("@flag", 1),
                new SqlParameter("@page1Heading", this.txtCMAheading.Text.Trim()),
                new SqlParameter("@page1Text", this.txtCMA3Text.Value.Trim()),               
                new SqlParameter("@Id", this.iBtnSaveCMA.CommandArgument),
            });
            this.BindCMAReportText();
        }


    }
}
