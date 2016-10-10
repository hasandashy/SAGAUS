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
               // this.BindCMAReportText();
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
                    this.iBtnSaveSSA.CommandArgument = ds.Tables[0].Rows[0]["Id"].ToString();
                }
            }
        }

   //     private void BindCMAReportText()
   //     {
   //         DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageCMAText", new SqlParameter[]
			//{
			//	new SqlParameter("@flag", 2)
			//});
   //         if (ds != null)
   //         {
   //             if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
   //             {
   //                 this.txtCMAheading.Text = ds.Tables[0].Rows[0]["page1Heading"].ToString();
   //                 this.txtCMA3Text.Value = ds.Tables[0].Rows[0]["page1Text"].ToString();
   //                 this.txtCMA5Text.Value = ds.Tables[0].Rows[0]["page5Text"].ToString();
   //                 this.txtCMA14Heading.Text = ds.Tables[0].Rows[0]["page14Heading"].ToString();
   //                 this.txtCMA14Text.Value = ds.Tables[0].Rows[0]["page14Text"].ToString();
   //                 this.txtCMA17Heading.Text = ds.Tables[0].Rows[0]["page17Heading"].ToString();
   //                 this.txtCMA17Text.Value = ds.Tables[0].Rows[0]["page17text"].ToString();
   //                 this.txtCMA20Heading.Text = ds.Tables[0].Rows[0]["page20Heading"].ToString();
   //                 this.txtCMA21Heading.Text = ds.Tables[0].Rows[0]["page21Heading"].ToString();
   //                 this.txtCMA20Text.Value = ds.Tables[0].Rows[0]["page20text"].ToString();
   //                 this.txtCMA21Text.Value = ds.Tables[0].Rows[0]["page21text"].ToString();
   //                 this.txtCMA16Heading.Text = ds.Tables[0].Rows[0]["page16Heading"].ToString();
   //                 this.txtCMA16Text.Value = ds.Tables[0].Rows[0]["page16text"].ToString();
   //                 this.txtCMA18Heading.Text = ds.Tables[0].Rows[0]["page18Heading"].ToString();
   //                 this.txtCMA18Para1.Value = ds.Tables[0].Rows[0]["page18SubPara1"].ToString();
   //                 this.txtCMA18Para2.Value = ds.Tables[0].Rows[0]["page18SubPara2"].ToString();
   //                 this.txtCMAAddress.Value = ds.Tables[0].Rows[0]["page19Address"].ToString();
   //                 this.txtCMADisclaimer.Value = ds.Tables[0].Rows[0]["Disclaimer"].ToString();
   //                 this.txtIfpsmCMAHeading.Text = ds.Tables[0].Rows[0]["ifpsmHeading"].ToString();
   //                 this.txtIfpsmCMAText.Value = ds.Tables[0].Rows[0]["ifpsmText"].ToString();
   //                 this.iBtnSaveCMA.CommandArgument = ds.Tables[0].Rows[0]["Id"].ToString();
   //             }
   //         }
   //     }

      

     

        protected void iBtnSaveSSA_Click(object sender, ImageClickEventArgs e)
        {
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spManageProcurementText", new SqlParameter[]
			{
				new SqlParameter("@flag", 1),
				new SqlParameter("@page2Heading", this.txtSSAheading.Text.Trim()),
				new SqlParameter("@page2Text", this.txtSSA3Text.Value.Trim()),				
				new SqlParameter("@Id", this.iBtnSaveSSA.CommandArgument)
            });
            this.BindSSAReportText();
        }

   //     protected void iBtnSaveCMA_Click(object sender, ImageClickEventArgs e)
   //     {
   //         SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spManageCMAText", new SqlParameter[]
			//{
			//	new SqlParameter("@flag", 1),
			//	new SqlParameter("@page1Heading", this.txtCMAheading.Text.Trim()),
			//	new SqlParameter("@page1Text", this.txtCMA3Text.Value.Trim()),
			//	new SqlParameter("@page5Text", this.txtCMA5Text.Value.Trim()),
			//	new SqlParameter("@page14Heading", this.txtCMA14Heading.Text),
			//	new SqlParameter("@page14Text", this.txtCMA14Text.Value),
			//	new SqlParameter("@page17Heading", this.txtCMA17Heading.Text),
			//	new SqlParameter("@page17Text", this.txtCMA17Text.Value),
			//	new SqlParameter("@page18Heading", this.txtCMA18Heading.Text),
			//	new SqlParameter("@page18SubPara1", this.txtCMA18Para1.Value),
			//	new SqlParameter("@page18SubPara2", this.txtCMA18Para2.Value),
			//	new SqlParameter("@page19Address", this.txtCMAAddress.Value),
			//	new SqlParameter("@Disclaimer", this.txtCMADisclaimer.Value),
			//	new SqlParameter("@Id", this.iBtnSaveCMA.CommandArgument),
			//	new SqlParameter("@page20Heading", this.txtCMA20Heading.Text),
			//	new SqlParameter("@page20text", this.txtCMA20Text.Value),
			//	new SqlParameter("@page21Heading", this.txtCMA21Heading.Text),
			//	new SqlParameter("@page21text", this.txtCMA21Text.Value),
			//	new SqlParameter("@page16Heading", this.txtCMA16Heading.Text),
			//	new SqlParameter("@page16text", this.txtCMA16Text.Value),
   //             new SqlParameter("@ifpsmText", this.txtIfpsmCMAText.Value),
   //             new SqlParameter("@ifpsmHeading", this.txtIfpsmCMAHeading.Text),
   //         });
   //         this.BindSSAReportText();
   //     }

     
    }
}
