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
                this.BindBAReportText();
                this.BindCMAReportText();
                this.BindNPReportText();
            }
        }

        private void BindBAReportText()
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageBAText", new SqlParameter[]
			{
				new SqlParameter("@flag", 2)
			});
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    this.txtBA3heading.Text = ds.Tables[0].Rows[0]["page1Heading"].ToString();
                    this.txtBA3text.Value = ds.Tables[0].Rows[0]["page1Text"].ToString();
                    this.txtBA5text.Value = ds.Tables[0].Rows[0]["page5Text"].ToString();
                    this.txtBA14Heading.Text = ds.Tables[0].Rows[0]["page14Heading"].ToString();
                    this.txtBA14text.Value = ds.Tables[0].Rows[0]["page14Text"].ToString();
                    this.txtBA17Heading.Text = ds.Tables[0].Rows[0]["page17Heading"].ToString();
                    this.txtBA17text.Value = ds.Tables[0].Rows[0]["page17text"].ToString();
                    this.txtBA20Heading.Text = ds.Tables[0].Rows[0]["page20Heading"].ToString();
                    this.txtBA21Heading.Text = ds.Tables[0].Rows[0]["page21Heading"].ToString();
                    this.txtBA20text.Value = ds.Tables[0].Rows[0]["page20text"].ToString();
                    this.txtBA21text.Value = ds.Tables[0].Rows[0]["page21text"].ToString();
                    this.txtBA16Heading.Text = ds.Tables[0].Rows[0]["page16Heading"].ToString();
                    this.txtBA16text.Value = ds.Tables[0].Rows[0]["page16text"].ToString();
                    this.txtBA18Heading.Text = ds.Tables[0].Rows[0]["page18Heading"].ToString();
                    this.txtBA18subPara1.Value = ds.Tables[0].Rows[0]["page18SubPara1"].ToString();
                    this.txtBA18subPara2.Value = ds.Tables[0].Rows[0]["page18SubPara2"].ToString();
                    this.txtBA16Address.Value = ds.Tables[0].Rows[0]["page19Address"].ToString();
                    this.txtBADisclaimer.Value = ds.Tables[0].Rows[0]["Disclaimer"].ToString();
                    this.txtIFPSMBAHeading.Text = ds.Tables[0].Rows[0]["ifpsmHeading"].ToString();
                    this.txtIFPSMBAText.Value = ds.Tables[0].Rows[0]["ifpsmText"].ToString();
                    this.iBtnBASave.CommandArgument = ds.Tables[0].Rows[0]["Id"].ToString();
                }
            }
        }

        private void BindSSAReportText()
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageSSAText", new SqlParameter[]
			{
				new SqlParameter("@flag", 2)
			});
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    this.txtSSAheading.Text = ds.Tables[0].Rows[0]["page1Heading"].ToString();
                    this.txtSSA3Text.Value = ds.Tables[0].Rows[0]["page1Text"].ToString();
                    this.txtSSA5Text.Value = ds.Tables[0].Rows[0]["page5Text"].ToString();
                    this.txtSSA14Heading.Text = ds.Tables[0].Rows[0]["page14Heading"].ToString();
                    this.txtSSA14Text.Value = ds.Tables[0].Rows[0]["page14Text"].ToString();
                    this.txtSSA17Heading.Text = ds.Tables[0].Rows[0]["page17Heading"].ToString();
                    this.txtSSA17text.Value = ds.Tables[0].Rows[0]["page17text"].ToString();
                    this.txtSSA20Heading.Text = ds.Tables[0].Rows[0]["page20Heading"].ToString();
                    this.txtSSA21Heading.Text = ds.Tables[0].Rows[0]["page21Heading"].ToString();
                    this.txtSSA20text.Value = ds.Tables[0].Rows[0]["page20text"].ToString();
                    this.txtSSA21text.Value = ds.Tables[0].Rows[0]["page21text"].ToString();
                    this.txtSSA16Heading.Text = ds.Tables[0].Rows[0]["page16Heading"].ToString();
                    this.txtSSA16text.Value = ds.Tables[0].Rows[0]["page16text"].ToString();
                    this.txtSSA18Heading.Text = ds.Tables[0].Rows[0]["page18Heading"].ToString();
                    this.txtSSA18SubPara1.Value = ds.Tables[0].Rows[0]["page18SubPara1"].ToString();
                    this.txtSSA18SubPara2.Value = ds.Tables[0].Rows[0]["page18SubPara2"].ToString();
                    this.txtSSA19Address.Value = ds.Tables[0].Rows[0]["page19Address"].ToString();
                    this.txtSSADisclaimer.Value = ds.Tables[0].Rows[0]["Disclaimer"].ToString();
                    this.txtSSAIfpsmHeading.Text = ds.Tables[0].Rows[0]["ifpsmHeading"].ToString();
                    this.txtSSAIfpsmText.Value = ds.Tables[0].Rows[0]["ifpsmText"].ToString();
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
                    this.txtCMA5Text.Value = ds.Tables[0].Rows[0]["page5Text"].ToString();
                    this.txtCMA14Heading.Text = ds.Tables[0].Rows[0]["page14Heading"].ToString();
                    this.txtCMA14Text.Value = ds.Tables[0].Rows[0]["page14Text"].ToString();
                    this.txtCMA17Heading.Text = ds.Tables[0].Rows[0]["page17Heading"].ToString();
                    this.txtCMA17Text.Value = ds.Tables[0].Rows[0]["page17text"].ToString();
                    this.txtCMA20Heading.Text = ds.Tables[0].Rows[0]["page20Heading"].ToString();
                    this.txtCMA21Heading.Text = ds.Tables[0].Rows[0]["page21Heading"].ToString();
                    this.txtCMA20Text.Value = ds.Tables[0].Rows[0]["page20text"].ToString();
                    this.txtCMA21Text.Value = ds.Tables[0].Rows[0]["page21text"].ToString();
                    this.txtCMA16Heading.Text = ds.Tables[0].Rows[0]["page16Heading"].ToString();
                    this.txtCMA16Text.Value = ds.Tables[0].Rows[0]["page16text"].ToString();
                    this.txtCMA18Heading.Text = ds.Tables[0].Rows[0]["page18Heading"].ToString();
                    this.txtCMA18Para1.Value = ds.Tables[0].Rows[0]["page18SubPara1"].ToString();
                    this.txtCMA18Para2.Value = ds.Tables[0].Rows[0]["page18SubPara2"].ToString();
                    this.txtCMAAddress.Value = ds.Tables[0].Rows[0]["page19Address"].ToString();
                    this.txtCMADisclaimer.Value = ds.Tables[0].Rows[0]["Disclaimer"].ToString();
                    this.txtIfpsmCMAHeading.Text = ds.Tables[0].Rows[0]["ifpsmHeading"].ToString();
                    this.txtIfpsmCMAText.Value = ds.Tables[0].Rows[0]["ifpsmText"].ToString();
                    this.iBtnSaveCMA.CommandArgument = ds.Tables[0].Rows[0]["Id"].ToString();
                }
            }
        }

        private void BindNPReportText()
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageNPText", new SqlParameter[]
			{
				new SqlParameter("@flag", 2)
			});
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    this.txtNP3heading.Text = ds.Tables[0].Rows[0]["page1Heading"].ToString();
                    this.txtNP3text.Value = ds.Tables[0].Rows[0]["page1Text"].ToString();
                    this.txtNP5text.Value = ds.Tables[0].Rows[0]["page5Text"].ToString();
                    this.txtNP15heading.Text = ds.Tables[0].Rows[0]["page15Heading"].ToString();
                    this.txtNP15SubPara1.Value = ds.Tables[0].Rows[0]["page15SubPara1"].ToString();
                    this.txtNP15SubPara2.Value = ds.Tables[0].Rows[0]["page15SubPara2"].ToString();
                    this.txtNP16Address.Value = ds.Tables[0].Rows[0]["page16Address"].ToString();
                    this.txtNPDisclaimer.Value = ds.Tables[0].Rows[0]["Disclaimer"].ToString();
                    this.txtIfpsmNPHeading.Text = ds.Tables[0].Rows[0]["ifpsmHeading"].ToString();
                    this.txtIfpsmNPText.Value = ds.Tables[0].Rows[0]["ifpsmText"].ToString();
                    this.ImageButton1.CommandArgument = ds.Tables[0].Rows[0]["Id"].ToString();
                }
            }
        }

        protected void iBtnBASave_Click(object sender, ImageClickEventArgs e)
        {
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spManageBAText", new SqlParameter[]
			{
				new SqlParameter("@flag", 1),
				new SqlParameter("@page1Heading", this.txtBA3heading.Text.Trim()),
				new SqlParameter("@page1Text", this.txtBA3text.Value.Trim()),
				new SqlParameter("@page5Text", this.txtBA5text.Value.Trim()),
				new SqlParameter("@page14Heading", this.txtBA14Heading.Text),
				new SqlParameter("@page14Text", this.txtBA14text.Value),
				new SqlParameter("@page17Heading", this.txtBA17Heading.Text),
				new SqlParameter("@page17Text", this.txtBA17text.Value),
				new SqlParameter("@page18Heading", this.txtBA18Heading.Text),
				new SqlParameter("@page18SubPara1", this.txtBA18subPara1.Value),
				new SqlParameter("@page18SubPara2", this.txtBA18subPara2.Value),
				new SqlParameter("@page19Address", this.txtBA16Address.Value),
				new SqlParameter("@Disclaimer", this.txtBADisclaimer.Value),
				new SqlParameter("@Id", this.iBtnBASave.CommandArgument),
				new SqlParameter("@page20Heading", this.txtBA20Heading.Text),
				new SqlParameter("@page20text", this.txtBA20text.Value),
				new SqlParameter("@page21Heading", this.txtBA21Heading.Text),
				new SqlParameter("@page21text", this.txtBA21text.Value),
				new SqlParameter("@page16Heading", this.txtBA16Heading.Text),
				new SqlParameter("@page16text", this.txtBA16text.Value),
                new SqlParameter("@ifpsmText", this.txtIFPSMBAText.Value),
                new SqlParameter("@ifpsmHeading", this.txtIFPSMBAHeading.Text),
            });
            this.BindBAReportText();
        }

        protected void iBtnSaveSSA_Click(object sender, ImageClickEventArgs e)
        {
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spManageSSAText", new SqlParameter[]
			{
				new SqlParameter("@flag", 1),
				new SqlParameter("@page1Heading", this.txtSSAheading.Text.Trim()),
				new SqlParameter("@page1Text", this.txtSSA3Text.Value.Trim()),
				new SqlParameter("@page5Text", this.txtSSA5Text.Value.Trim()),
				new SqlParameter("@page14Heading", this.txtSSA14Heading.Text),
				new SqlParameter("@page14Text", this.txtSSA14Text.Value),
				new SqlParameter("@page17Heading", this.txtSSA17Heading.Text),
				new SqlParameter("@page17Text", this.txtSSA17text.Value),
				new SqlParameter("@page18Heading", this.txtSSA18Heading.Text),
				new SqlParameter("@page18SubPara1", this.txtSSA18SubPara1.Value),
				new SqlParameter("@page18SubPara2", this.txtSSA18SubPara2.Value),
				new SqlParameter("@page19Address", this.txtSSA19Address.Value),
				new SqlParameter("@Disclaimer", this.txtSSADisclaimer.Value),
				new SqlParameter("@Id", this.iBtnSaveSSA.CommandArgument),
				new SqlParameter("@page20Heading", this.txtSSA20Heading.Text),
				new SqlParameter("@page20text", this.txtSSA20text.Value),
				new SqlParameter("@page21Heading", this.txtSSA21Heading.Text),
				new SqlParameter("@page21text", this.txtSSA21text.Value),
				new SqlParameter("@page16Heading", this.txtSSA16Heading.Text),
				new SqlParameter("@page16text", this.txtSSA16text.Value),
                new SqlParameter("@ifpsmText", this.txtSSAIfpsmText.Value),
                new SqlParameter("@ifpsmHeading", this.txtSSAIfpsmHeading.Text),
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
				new SqlParameter("@page5Text", this.txtCMA5Text.Value.Trim()),
				new SqlParameter("@page14Heading", this.txtCMA14Heading.Text),
				new SqlParameter("@page14Text", this.txtCMA14Text.Value),
				new SqlParameter("@page17Heading", this.txtCMA17Heading.Text),
				new SqlParameter("@page17Text", this.txtCMA17Text.Value),
				new SqlParameter("@page18Heading", this.txtCMA18Heading.Text),
				new SqlParameter("@page18SubPara1", this.txtCMA18Para1.Value),
				new SqlParameter("@page18SubPara2", this.txtCMA18Para2.Value),
				new SqlParameter("@page19Address", this.txtCMAAddress.Value),
				new SqlParameter("@Disclaimer", this.txtCMADisclaimer.Value),
				new SqlParameter("@Id", this.iBtnSaveCMA.CommandArgument),
				new SqlParameter("@page20Heading", this.txtCMA20Heading.Text),
				new SqlParameter("@page20text", this.txtCMA20Text.Value),
				new SqlParameter("@page21Heading", this.txtCMA21Heading.Text),
				new SqlParameter("@page21text", this.txtCMA21Text.Value),
				new SqlParameter("@page16Heading", this.txtCMA16Heading.Text),
				new SqlParameter("@page16text", this.txtCMA16Text.Value),
                new SqlParameter("@ifpsmText", this.txtIfpsmCMAText.Value),
                new SqlParameter("@ifpsmHeading", this.txtIfpsmCMAHeading.Text),
            });
            this.BindSSAReportText();
        }

        protected void iBtnNPSave_Click(object sender, ImageClickEventArgs e)
        {
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spManageNPText", new SqlParameter[]
			{
				new SqlParameter("@flag", 1),
				new SqlParameter("@page1Heading", this.txtNP3heading.Text.Trim()),
				new SqlParameter("@page1Text", this.txtNP3text.Value.Trim()),
				new SqlParameter("@page5Text", this.txtNP5text.Value.Trim()),
				new SqlParameter("@page15Heading", this.txtNP15heading.Text),
				new SqlParameter("@page15SubPara1", this.txtNP15SubPara1.Value),
				new SqlParameter("@page15SubPara2", this.txtNP15SubPara2.Value),
				new SqlParameter("@page16Address", this.txtNP16Address.Value),
				new SqlParameter("@Disclaimer", this.txtNPDisclaimer.Value),
				new SqlParameter("@Id", this.ImageButton1.CommandArgument),
                new SqlParameter("@ifpsmText", this.txtIfpsmNPText.Value),
                new SqlParameter("@ifpsmHeading", this.txtIfpsmNPHeading.Text),
            });
            this.BindNPReportText();
        }
    }
}
