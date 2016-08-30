using DataTier;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using SGA.App_Code;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SGA.tna
{
    public partial class my_results_reports_ba : Page
    {
        private const int LeftAlign = 0;

        private const int CenterAlign = 1;

        private const int RightAlign = 2;

        
        private Document doc;

        private string imagepath;

        private Font arial24Bold = FontFactory.GetFont("Arial", 24f, 1, new BaseColor(0, 0, 0));

        private Font arial2 = FontFactory.GetFont("Arial", 24f, new BaseColor(234, 66, 31));

        private Font arial3 = FontFactory.GetFont("Arial", 14f, new BaseColor(0, 0, 0));

        private BaseColor hcolor = new BaseColor(234, 66, 31);

        private BaseColor bcolor = new BaseColor(0, 0, 0);

        private BaseColor wcolor = new BaseColor(255, 255, 255);

        private BaseColor tablebackcolor = new BaseColor(241, 238, 233);

        private BaseColor tableborcolor = new BaseColor(241, 238, 233);

        private BaseColor tablelitebackcolor = new BaseColor(221, 218, 213);

        private BaseColor tableliteborcolor = new BaseColor(255, 255, 255);

        protected bool isSgaResult = false;

        protected bool isTnaResult = false;

        protected bool isPmpResult = false;

        protected bool isDmpResult = false;

        protected bool isNpResult = false;

        protected bool isCMAResult = false;

        public int pgNum
        {
            get
            {
                int result;
                if (this.ViewState["PgNum"] != null)
                {
                    result = System.Convert.ToInt32(this.ViewState["PgNum"]);
                }
                else
                {
                    result = 0;
                }
                return result;
            }
            set
            {
                this.ViewState["PgNum"] = value;
            }
        }

        public int pgNumPd
        {
            get
            {
                int result;
                if (this.ViewState["pgNumPd"] != null)
                {
                    result = System.Convert.ToInt32(this.ViewState["pgNumPd"]);
                }
                else
                {
                    result = 0;
                }
                return result;
            }
            set
            {
                this.ViewState["pgNumPd"] = value;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            SGACommon.AddPageTitle(this.Page, "Leadership Assessment Result Page", "");
            SGACommon.IsViewResult("viewPMPResult");
            if (!base.IsPostBack)
            {
                DataSet dsPermission = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetPremission", new SqlParameter[]
				{
					new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
				});
                if (dsPermission != null)
                {
                    if (dsPermission.Tables.Count > 0 && dsPermission.Tables[0].Rows.Count > 0)
                    {
                        this.isSgaResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewSgaResult"].ToString());
                        this.isTnaResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewTnaResult"].ToString());
                        this.isPmpResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewPMPResult"].ToString());
                        this.isDmpResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewDMPResult"].ToString());
                        this.isNpResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewNPResult"].ToString());
                        this.isCMAResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewCMAResult"].ToString());
                    }
                }
                this.spSkills.Attributes["class"] = (this.isTnaResult ? "" : "lock");
                this.spBehaviour.Attributes["class"] = (this.isPmpResult ? "" : "lock");
                this.spCMA.Attributes["class"] = (this.isCMAResult ? "" : "lock");
                this.spNegotiation.Attributes["class"] = (this.isNpResult ? "" : "lock");
                this.BindResults();
                base.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
                base.ClientScript.RegisterStartupScript(this.Page.GetType(), "abc", "$(document).ready(function(){\r\nStyleRadio();\r\n});", true);
            }
        }

        private void BindResults()
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetBATests", new SqlParameter[]
			{
				new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
			});
            int cnt = ds.Tables[0].Rows.Count;
            PagedDataSource paged = new PagedDataSource();
            paged.DataSource = ds.Tables[0].DefaultView;
            paged.AllowPaging = true;
            paged.PageSize = 10;
            paged.CurrentPageIndex = this.pgNumPd;
            this.ViewState["pgNumPd"] = this.pgNumPd;
            int vcnt = cnt / paged.PageSize;
            this.btnprev.Visible = !paged.IsFirstPage;
            this.btnnext.Visible = !paged.IsLastPage;
            this.rptSgaTest.DataSource = paged;
            this.rptSgaTest.DataBind();
        }

        protected void btnprev_Click(object sender, System.EventArgs e)
        {
            this.pgNumPd--;
            this.BindResults();
            base.ClientScript.RegisterStartupScript(this.Page.GetType(), "abc", "$(document).ready(function(){\r\nStyleRadio();\r\n});", true);
        }

        protected void btnnext_Click(object sender, System.EventArgs e)
        {
            this.pgNumPd++;
            this.BindResults();
            base.ClientScript.RegisterStartupScript(this.Page.GetType(), "abc", "$(document).ready(function(){\r\nStyleRadio();\r\n});", true);
        }

        protected void rptSgaTest_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label lblConvertedDate = (Label)e.Item.FindControl("lblConvertedDate");
                System.DateTime dtTestdate = System.Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "testDate"));
                if (lblConvertedDate != null)
                {
                    lblConvertedDate.Text = SGACommon.ToAusTimeZone(dtTestdate).ToString("dd/MM/yyyy HH:mm tt");
                }
            }
        }

        protected void btnShare_Click(object sender, System.EventArgs e)
        {
        }

        protected void lnkResults_Click(object sender, System.EventArgs e)
        {
        }

        protected void rptSgaTest_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "bar")
            {
                this.Session["baTestId"] = e.CommandArgument;
                base.Response.Redirect("my-results-bar-graph-ba.aspx", false);
            }
        }

        [WebMethod]
        public static string EmailResultBack(string testIds)
        {
            string emailsubject = "";
            string body = "";
            SGACommon.GetEmailTemplate(13, ref emailsubject, ref body);
            testIds = SGACommon.RemoveLastCharacter(testIds);
            string[] strArray = testIds.Split(new char[]
			{
				','
			});
            if (strArray.Length > 0)
            {
                for (int i = 0; i < strArray.Length; i++)
                {
                    string strMail = "";
                    DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetBaGraph", new SqlParameter[]
					{
						new SqlParameter("@testId", strArray[i])
					});
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {
                                strMail = strMail + "<tr><td style='font-family: Arial; font-size: 12px;'>" + ds.Tables[0].Rows[j]["topicTitle"].ToString().Replace("<br />", " ") + "</td>";
                                object obj2 = strMail;
                                strMail = string.Concat(new object[]
								{
									obj2,
									"<td style='font-family: Arial; font-size: 12px;'>",
									ds.Tables[0].Rows[j]["topicMarks"],
									"</td>"
								});
                                obj2 = strMail;
                                strMail = string.Concat(new object[]
								{
									obj2,
									"<td style='font-family: Arial; font-size: 12px;'>",
									ds.Tables[0].Rows[j]["Level"],
									"</td></tr>"
								});
                            }
                        }
                    }
                    string content = body.Replace("@v0", SGACommon.GetName()).Replace("@v1", strMail);
                    my_results_reports_ba obj = new my_results_reports_ba();
                    string strFilePath = obj.CreateTestPdf(strArray[i].ToString());
                    MailSending.SendMailWithAttachment(ConfigurationManager.AppSettings["nameDisplay"].ToString(), SGACommon.LoginUserInfo.name, emailsubject, content, strFilePath);
                }
            }
            return "s";
        }

        public string CreateTestPdf(string testId)
        {
            this.imagepath = base.Server.MapPath("~/pdfBgImages/");
            string pdfPath = "~/pdfReports/";
            string newFile = base.Server.MapPath(string.Concat(new string[]
			{
				pdfPath,
				SGACommon.GetName(SGACommon.LoginUserInfo.userId),
				"_",
				testId,
				"_batest.pdf"
			}));
            DataSet dsPages = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageBAText", new SqlParameter[]
			{
				new SqlParameter("@flag", 2)
			});
            SqlParameter[] param = new SqlParameter[]
			{
				new SqlParameter("@userId", SqlDbType.Int)
			};
            param[0].Value = SGACommon.LoginUserInfo.userId;
            DataSet dsProfile = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetProfileDetails", param);
            System.DateTime dtEnd = System.Convert.ToDateTime(SqlHelper.ExecuteScalar(CommandType.Text, "select testDate from tbluserbaTest where testId=" + testId));
            param = new SqlParameter[2];
            param[0] = new SqlParameter("@userId", SqlDbType.Int);
            param[0].Value = SGACommon.LoginUserInfo.userId;
            param[1] = new SqlParameter("@testId", SqlDbType.Int);
            param[1].Value = testId;
            DataSet dspercentage = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spAvgValuesTests", param);
            this.doc = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            DataSet dsJob = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageJobRoleSuggestion", new SqlParameter[]
			{
				new SqlParameter("@id", System.Convert.ToInt32(System.Convert.ToInt32(dsProfile.Tables[0].Rows[0]["jobRole"].ToString()))),
				new SqlParameter("@jobSuggestion", ""),
				new SqlParameter("@flag", "4")
			});
            PdfWriter writer = PdfWriter.GetInstance(this.doc, new System.IO.FileStream(newFile, System.IO.FileMode.Create));
            HTMLWorker hw = new HTMLWorker(this.doc);
            try
            {
                this.doc.Open();
                iTextSharp.text.Image imgBG = iTextSharp.text.Image.GetInstance(this.imagepath + "/mainbg.jpg");
                imgBG.ScaleToFit(this.doc.PageSize.Width, this.doc.PageSize.Height);
                imgBG.Alignment = 8;
                this.doc.NewPage();
                this.doc.Add(imgBG);
                this.doc.SetMargins(0f, 0f, 0f, 0f);
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(this.imagepath + "/logo.png");
                img.ScaleToFit(200f, 78f);
                img.Alignment = 4;
                img.SetAbsolutePosition(30f, 715f);
                this.doc.Add(img);
                PdfContentByte cb = writer.DirectContent;
                BaseFont f_cn = BaseFont.CreateFont("Helvetica", "Cp1252", false);
                cb.BeginText();
                cb.SetFontAndSize(f_cn, 12f);
                cb.SetTextMatrix(80f, 380f);
                cb.SetRGBColorFill(0, 0, 0);
                cb.ShowText("Prepared for:");
                cb.EndText();
                if (dsProfile != null)
                {
                    if (dsProfile.Tables.Count > 0 && dsProfile.Tables[0].Rows.Count > 0)
                    {
                        cb.BeginText();
                        cb.SetFontAndSize(f_cn, 24f);
                        cb.SetTextMatrix(80f, 340f);
                        cb.SetRGBColorFill(234, 66, 31);
                        cb.ShowText(SGACommon.UppercaseFirst(dsProfile.Tables[0].Rows[0]["firstname"].ToString()) + " " + SGACommon.UppercaseFirst(dsProfile.Tables[0].Rows[0]["lastname"].ToString()));
                        cb.EndText();
                        cb.BeginText();
                        cb.SetFontAndSize(f_cn, 12f);
                        cb.SetTextMatrix(80f, 310f);
                        cb.SetRGBColorFill(0, 0, 0);
                        cb.ShowText("Leadership");
                        cb.EndText();
                        cb.BeginText();
                        cb.SetFontAndSize(f_cn, 12f);
                        cb.SetTextMatrix(80f, 290f);
                        cb.SetRGBColorFill(0, 0, 0);
                        cb.ShowText("Training Needs Analysis");
                        cb.EndText();
                        cb.BeginText();
                        cb.SetFontAndSize(f_cn, 12f);
                        cb.SetTextMatrix(80f, 200f);
                        cb.SetRGBColorFill(0, 0, 0);
                        cb.ShowText("Date completed: ");
                        cb.EndText();
                        cb.BeginText();
                        cb.SetFontAndSize(f_cn, 12f);
                        cb.SetTextMatrix(80f, 180f);
                        cb.SetRGBColorFill(0, 0, 0);
                        cb.ShowText(SGACommon.ToAusTimeZone(dtEnd).ToString("dddd, dd MMMM yyyy"));
                        cb.EndText();
                        cb.BeginText();
                        cb.SetFontAndSize(f_cn, 12f);
                        cb.SetTextMatrix(80f, 160f);
                        cb.SetRGBColorFill(0, 0, 0);
                        cb.ShowText("Email : skills2procure@hpw.qld.gov");
                        cb.EndText();
                        cb.BeginText();
                        cb.SetFontAndSize(f_cn, 16f);
                        cb.SetTextMatrix(80f, 130f);
                        cb.SetRGBColorFill(234, 66, 31);
                        cb.ShowText("www.criticalskillsboost.com");
                        cb.EndText();
                    }
                }
                PdfPTable table = this.GetTable(1, 560f);
                this.doc.SetMargins(55f, 55f, 25f, 25f);
                this.doc.NewPage();
                this.AddBlankParagraphLowHeight(1);
                if (dsPages != null)
                {
                    if (dsPages.Tables.Count > 0 && dsPages.Tables[0].Rows.Count > 0)
                    {
                        this.AddParagraph(dsPages.Tables[0].Rows[0]["page1Heading"].ToString(), 0, FontFactory.GetFont("Arial", 24f, 1, this.hcolor));
                        this.AddBlankParagraphLowHeight(1);
                        hw.Parse(new System.IO.StringReader(dsPages.Tables[0].Rows[0]["page1Text"].ToString()));
                    }
                }
                this.doc.SetMargins(55f, 55f, 55f, 55f);
                this.doc.NewPage();
                string str = "Your organisation and you";
                this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 24f, 1, new BaseColor(0, 0, 0)));
                this.AddBlankParagraphLowHeight(2);
                this.AddParagraph("During your registration process at criticalskillsboost.com you provided information to us. This information is displayed below for your reference. ", 0, FontFactory.GetFont("Arial", 10f, 0, new BaseColor(0, 0, 0)));
                this.AddBlankParagraph(2);
                str = "YOUR REGISTRATION INFORMATION";
                this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 10f, 1, this.hcolor));
                this.AddBlankParagraph(1);
                table = this.GetTable(2);
                if (dsProfile != null)
                {
                    if (dsProfile.Tables.Count > 0 && dsProfile.Tables[0].Rows.Count > 0)
                    {
                        this.Addrow(ref table, "First Name", SGACommon.UppercaseFirst(dsProfile.Tables[0].Rows[0]["firstname"].ToString()));
                        this.Addrow(ref table, "Last Name", SGACommon.UppercaseFirst(dsProfile.Tables[0].Rows[0]["lastname"].ToString()));
                        this.Addrow(ref table, "Email Address", SGACommon.UppercaseFirst(dsProfile.Tables[0].Rows[0]["email"].ToString()));
                        this.Addrow(ref table, "Manager's First Name", SGACommon.UppercaseFirst(dsProfile.Tables[0].Rows[0]["managerFirstname"].ToString()));
                        this.Addrow(ref table, "Manager's Last Name", SGACommon.UppercaseFirst(dsProfile.Tables[0].Rows[0]["managerLastName"].ToString()));
                        this.Addrow(ref table, "Manager's Email Address", SGACommon.UppercaseFirst(dsProfile.Tables[0].Rows[0]["managerEmail"].ToString()));
                        this.doc.Add(table);
                        table = this.GetTable(2);
                        this.AddBlankParagraph(2);
                        this.AddBoldParagraph("YOUR DETAILS");
                        this.AddBlankParagraph(2);
                        this.Addrow(ref table, "Organisation", Profile.GetOrganisation(System.Convert.ToInt32(dsProfile.Tables[0].Rows[0]["agencyId"].ToString())));
                        this.Addrow(ref table, "Division", SGACommon.UppercaseFirst(dsProfile.Tables[0].Rows[0]["Division"].ToString()));
                        this.Addrow(ref table, "Location", Profile.GetLocation(System.Convert.ToInt32(dsProfile.Tables[0].Rows[0]["locationId"].ToString())));
                        this.Addrow(ref table, "Role", Profile.GetJobRole(System.Convert.ToInt32(dsProfile.Tables[0].Rows[0]["jobRole"].ToString())));
                        this.Addrow(ref table, "Level", Profile.GetJobLevel(System.Convert.ToInt32(dsProfile.Tables[0].Rows[0]["jobLevel"].ToString())));
                        this.Addrow(ref table, "Position", SGACommon.UppercaseFirst(dsProfile.Tables[0].Rows[0]["Position"].ToString()));
                        this.Addrow(ref table, "The nature of the goods/services that you most commonly procure, or manage contracts for?", Profile.GetGoodsLevel(System.Convert.ToInt32(dsProfile.Tables[0].Rows[0]["goodsId"].ToString())));
                        this.Addrow(ref table, "Phone number", SGACommon.UppercaseFirst(dsProfile.Tables[0].Rows[0]["phone"].ToString()));
                        this.doc.Add(table);
                    }
                }
                this.AddBlankParagraph(1);
                float[] expr_BA4 = new float[]
				{
					50f,
					45f,
					5f
				};
                this.doc.SetMargins(55f, 55f, 55f, 55f);
                this.doc.NewPage();
                str = "Your assessment summary";
                this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 24f, 1, new BaseColor(0, 0, 0)));
                hw.Parse(new System.IO.StringReader(dsPages.Tables[0].Rows[0]["page5Text"].ToString().Replace("@v0", dspercentage.Tables[1].Rows[0]["totalValue"].ToString()).Replace("@v1", System.Convert.ToDecimal(dspercentage.Tables[1].Rows[0]["totalAvg"].ToString()).ToString("#.##"))));
                table = this.GetTable(3);
                float[] colwidth = new float[]
				{
					30f,
					20f,
					50f
				};
                table.SetWidths(colwidth);
                this.AddrowHeader(ref table, "Phase", "Average", " Level");
                DataSet dsSummary = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetBaGraph", new SqlParameter[]
				{
					new SqlParameter("@testId", testId)
				});
                if (dsSummary != null)
                {
                    if (dsSummary.Tables.Count > 0 && dsSummary.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsSummary.Tables[0].Rows.Count; i++)
                        {
                            this.Addrow(ref table, " " + dsSummary.Tables[0].Rows[i]["topicTitle"].ToString().Replace("<br />", " "), dsSummary.Tables[0].Rows[i]["marksAvg"].ToString(), "  " + dsSummary.Tables[0].Rows[i]["Level"].ToString(), false, false, true);
                        }
                    }
                }
                this.doc.Add(table);
                cb.BeginText();
                cb.SetFontAndSize(f_cn, 10f);
                cb.SetTextMatrix(55f, 320f);
                cb.SetRGBColorFill(0, 0, 0);
                cb.ShowText("Note");
                cb.EndText();
                cb.BeginText();
                cb.SetFontAndSize(f_cn, 10f);
                cb.SetTextMatrix(55f, 300f);
                cb.SetRGBColorFill(0, 0, 0);
                cb.ShowText("The level column describes the average rating received and aligns to the wording of the assessment scale below.");
                cb.EndText();
                img = iTextSharp.text.Image.GetInstance(this.imagepath + "/scoringguide.jpg");
                img.ScaleToFit(800f, 116f);
                img.SetAbsolutePosition(0f, 150f);
                this.doc.Add(img);
                this.AddBlankParagraph(1);
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSuggestions", new SqlParameter[]
				{
					new SqlParameter("@flag", "3")
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            this.doc.SetMargins(55f, 55f, 55f, 55f);
                            this.doc.NewPage();
                            string strTopicImage = "";
                            switch (j)
                            {
                                case 0:
                                    strTopicImage = "LTNA_1_StrategicLeader.jpg";
                                    break;
                                case 1:
                                    strTopicImage = "LTNA_2_BuildingTrust.jpg";
                                    break;
                                case 2:
                                    strTopicImage = "LTNA_3_CommunicateEngage.jpg";
                                    break;
                                case 3:
                                    strTopicImage = "LTNA_4_EmotionalIntelligence.jpg";
                                    break;
                                case 4:
                                    strTopicImage = "LTNA_5_EndorseSupportDevelop.jpg";
                                    break;
                                case 5:
                                    strTopicImage = "LTNA_6_CultureChangeLeader.jpg";
                                    break;
                                case 6:
                                    strTopicImage = "LTNA_7_RelationshipManagement.jpg";
                                    break;
                                case 7:
                                    strTopicImage = "LTNA_8_CoachMentor.jpg";
                                    break;
                            }
                            this.AddParagraph(j + 1 + ". " + ds.Tables[0].Rows[j]["topicName"].ToString().Replace("<br />", " "), 0, FontFactory.GetFont("Arial", 24f, 1, new BaseColor(0, 0, 0)));
                            img = iTextSharp.text.Image.GetInstance(this.imagepath + "/" + strTopicImage);
                            img.ScaleToFit(500f, 510f);
                            img.Alignment = 4;
                            img.SetAbsolutePosition(45f, 585f);
                            this.doc.Add(img);
                            this.AddBlankParagraph(15);
                            hw.Parse(new System.IO.StringReader(ds.Tables[0].Rows[j]["SuggestionText"].ToString().Replace("@level", dsSummary.Tables[0].Rows[j]["Level"].ToString()).Replace("@score", dsSummary.Tables[0].Rows[j]["Marksavg"].ToString())));
                            this.AddBlankParagraph(1);
                            this.AddParagraph("EXPECTED COMPETENCY LEVELS", 0, FontFactory.GetFont("Arial", 12f, 1, new BaseColor(234, 66, 31)));
                            this.AddBlankParagraphLowHeight(1);
                            hw.Parse(new System.IO.StringReader(dsSummary.Tables[0].Rows[j]["training"].ToString()));
                        }
                    }
                }
                this.doc.SetMargins(0f, 0f, 0f, 0f);
                this.doc.NewPage();
                imgBG = iTextSharp.text.Image.GetInstance(this.imagepath + "/sizzer_bg.jpg");
                imgBG.ScaleToFit(this.doc.PageSize.Width, this.doc.PageSize.Height);
                imgBG.Alignment = 8;
                this.doc.Add(imgBG);
                if (dsPages != null)
                {
                    if (dsPages.Tables.Count > 0 && dsPages.Tables[0].Rows.Count > 0)
                    {
                        if (dsProfile != null)
                        {
                            if (dsProfile.Tables.Count > 0 && dsProfile.Tables[0].Rows.Count > 0)
                            {
                                this.AddBlankParagraph(12);
                                hw.Parse(new System.IO.StringReader(dsPages.Tables[0].Rows[0]["page14Text"].ToString().Replace("@v0", SGACommon.UppercaseFirst(dsProfile.Tables[0].Rows[0]["firstname"].ToString())).Replace("@v1", SGACommon.UppercaseFirst(dsProfile.Tables[0].Rows[0]["lastname"].ToString()))));
                            }
                        }
                    }
                }
                img = iTextSharp.text.Image.GetInstance(this.imagepath + "/comprarawelcome.jpg");
                img.SetAbsolutePosition(0f, 0f);
                img.ScaleToFit(600f, 600f);
                img.Alignment = 4;
                img.SetAbsolutePosition(0f, 25f);
                this.doc.Add(img);
                DataRow[] foundRows = dsSummary.Tables[0].Select("1=1", "topicMarks asc");
                this.doc.SetMargins(55f, 55f, 55f, 55f);
                this.doc.NewPage();
                if (dsPages != null)
                {
                    if (dsPages.Tables.Count > 0 && dsPages.Tables[0].Rows.Count > 0)
                    {
                        this.AddParagraph(dsPages.Tables[0].Rows[0]["page16Heading"].ToString(), 0, FontFactory.GetFont("Arial", 24f, 1, new BaseColor(0, 0, 0)));
                        this.AddBlankParagraphLowHeight(1);
                        hw.Parse(new System.IO.StringReader(dsPages.Tables[0].Rows[0]["page16Text"].ToString()));
                    }
                }
                this.doc.SetMargins(55f, 55f, 55f, 55f);
                this.doc.NewPage();
                if (dsPages != null)
                {
                    if (dsPages.Tables.Count > 0 && dsPages.Tables[0].Rows.Count > 0)
                    {
                        this.AddParagraph(dsPages.Tables[0].Rows[0]["page17Heading"].ToString(), 0, FontFactory.GetFont("Arial", 24f, 1, new BaseColor(0, 0, 0)));
                        this.AddBlankParagraph(1);
                        hw.Parse(new System.IO.StringReader(dsPages.Tables[0].Rows[0]["page17Text"].ToString().Replace("@v0", Profile.GetJobRole(System.Convert.ToInt32(dsProfile.Tables[0].Rows[0]["jobRole"].ToString()))).Replace("@v1", dsJob.Tables[0].Rows[0]["page14Para1"].ToString()).Replace("@v2", dsJob.Tables[0].Rows[0]["page14Para2"].ToString())));
                    }
                }
                img = iTextSharp.text.Image.GetInstance(this.imagepath + "/proleadership.jpg");
                img.ScaleToFit(510f, 700f);
                img.Alignment = 4;
                img.SetAbsolutePosition(45f, 195f);
                this.doc.Add(img);
                this.AddBlankParagraph(33);
                hw.Parse(new System.IO.StringReader("<p style=\"padding-left:200px;\">&nbsp;<a href=\"#\" target=\"_blank\" style=\"font-family:Arial;font-size:10px;color:#FF7C00;\"><b>CLICK HERE TO LEARN MORE ABOUT THIS WORKSHOP ></b></a></p>"));
                this.doc.SetMargins(55f, 55f, 55f, 55f);
                this.doc.NewPage();
                imgBG = iTextSharp.text.Image.GetInstance(this.imagepath + "/page18bgleadership.jpg");
                imgBG.ScaleToFit(this.doc.PageSize.Width, this.doc.PageSize.Height);
                imgBG.Alignment = 8;
                imgBG.Annotation = new Annotation(255f, 255f, 255f, 255f, "http://events.criticalskillsboost.com/booking/");
                imgBG.SetAbsolutePosition(0f, 0f);
                this.doc.Add(imgBG);
                this.AddBlankParagraph(20);
                if (dsPages != null)
                {
                    if (dsPages.Tables.Count > 0 && dsPages.Tables[0].Rows.Count > 0)
                    {
                        this.AddBlankParagraph(2);
                        this.AddParagraph(dsPages.Tables[0].Rows[0]["page18Heading"].ToString(), 0, FontFactory.GetFont("Arial", 24f, 1, new BaseColor(0, 0, 0)));
                        hw.Parse(new System.IO.StringReader(dsPages.Tables[0].Rows[0]["page18SubPara1"].ToString()));
                        this.AddBlankParagraph(1);
                    }
                }
                this.doc.SetMargins(55f, 55f, 55f, 55f);
                this.doc.NewPage();
                if (dsPages != null)
                {
                    if (dsPages.Tables.Count > 0 && dsPages.Tables[0].Rows.Count > 0)
                    {
                        img = iTextSharp.text.Image.GetInstance(this.imagepath + "/img3_grey_bg.jpg");
                        img.ScaleToFit(this.doc.PageSize.Width, this.doc.PageSize.Height);
                        img.Alignment = 8;
                        img.SetAbsolutePosition(1f, 3f);
                        this.doc.Add(img);
                        img = iTextSharp.text.Image.GetInstance(this.imagepath + "/compraralogo.png");
                        img.SetAbsolutePosition(170f, 610f);
                        img.ScaleToFit(222f, 108f);
                        img.Alignment = 5;
                        this.doc.Add(img);
                        this.AddBlankParagraph(15);
                        hw.Parse(new System.IO.StringReader(dsPages.Tables[0].Rows[0]["page19Address"].ToString()));
                        this.AddBlankParagraph(15);
                        hw.Parse(new System.IO.StringReader(dsPages.Tables[0].Rows[0]["Disclaimer"].ToString()));
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (this.doc != null)
                {
                    this.doc.Close();
                }
            }
            return newFile;
        }

        protected PdfPTable GetTable(int columns)
        {
            return new PdfPTable(columns)
            {
                TotalWidth = 500f,
                LockedWidth = true,
                HorizontalAlignment = 0
            };
        }

        protected void AddBoldParagraph(string text)
        {
            Chunk c = new Chunk(text, FontFactory.GetFont("Arial", 10f, 1, this.bcolor));
            Phrase ph = new Phrase();
            ph.Add(c);
            Paragraph p = new Paragraph();
            p.Add(ph);
            p.Alignment = 0;
            p.SetLeading(0f, 1f);
            this.doc.Add(p);
        }

        protected PdfPTable GetTable(int columns, float width)
        {
            return new PdfPTable(columns)
            {
                TotalWidth = width,
                LockedWidth = true,
                HorizontalAlignment = 1
            };
        }

        protected void Addrow(ref PdfPTable tab, string lefttext, string righttext)
        {
            Chunk tc = new Chunk(lefttext, FontFactory.GetFont("Arial", 10f, 0, this.bcolor));
            PdfPCell cell = new PdfPCell(new Phrase
			{
				tc
			});
            cell.BackgroundColor = this.tablebackcolor;
            cell.BorderColor = this.tableborcolor;
            cell.PaddingBottom = 10f;
            cell.PaddingLeft = 5f;
            cell.PaddingTop = 4f;
            tab.AddCell(cell);
            tc = new Chunk(righttext, FontFactory.GetFont("Arial", 10f, 0, this.bcolor));
            cell = new PdfPCell(new Phrase
			{
				tc
			});
            cell.BorderColor = this.tableborcolor;
            tab.AddCell(cell);
        }

        protected void AddNormalrow(ref PdfPTable tab, string lefttext, string righttext)
        {
            Chunk tc = new Chunk(lefttext, FontFactory.GetFont("Arial", 10f, 0, this.bcolor));
            PdfPCell cell = new PdfPCell(new Phrase
			{
				tc
			});
            cell.BorderColor = this.wcolor;
            cell.PaddingBottom = 2f;
            cell.PaddingLeft = 5f;
            cell.PaddingTop = 2f;
            tab.AddCell(cell);
            tc = new Chunk(righttext, FontFactory.GetFont("Arial", 10f, 0, this.bcolor));
            cell = new PdfPCell(new Phrase
			{
				tc
			});
            cell.BorderColor = this.wcolor;
            tab.AddCell(cell);
        }

        protected void Addrow(ref PdfPTable tab, string lefttext, string middletext, string righttext, bool isBold = true, bool isBackground = true, bool isPadding = false)
        {
            Chunk tc = new Chunk(lefttext, FontFactory.GetFont("Arial", 10f, isBold ? 1 : 0, this.bcolor));
            PdfPCell cell = new PdfPCell(new Phrase
			{
				tc
			});
            cell.BorderColor = this.tableborcolor;
            if (isPadding)
            {
                cell.PaddingTop = 10f;
                cell.PaddingBottom = 10f;
            }
            if (isBackground)
            {
                cell.BackgroundColor = this.tablebackcolor;
            }
            tab.AddCell(cell);
            tc = new Chunk(middletext, FontFactory.GetFont("Arial", 10f, isBold ? 1 : 0, this.bcolor));
            cell = new PdfPCell(new Phrase
			{
				tc
			});
            cell.BorderColor = this.tableborcolor;
            if (isPadding)
            {
                cell.PaddingTop = 10f;
                cell.PaddingBottom = 10f;
            }
            if (isBackground)
            {
                cell.BackgroundColor = this.tablebackcolor;
            }
            cell.HorizontalAlignment = 1;
            tab.AddCell(cell);
            tc = new Chunk(righttext, FontFactory.GetFont("Arial", 10f, isBold ? 1 : 0, this.bcolor));
            cell = new PdfPCell(new Phrase
			{
				tc
			});
            cell.BorderColor = this.tableborcolor;
            if (isPadding)
            {
                cell.PaddingTop = 10f;
                cell.PaddingBottom = 10f;
            }
            if (isBackground)
            {
                cell.BackgroundColor = this.tablebackcolor;
            }
            tab.AddCell(cell);
        }

        protected void Addrow(ref PdfPTable tab, string lefttext1, string lefttext2, string middletext, string righttext1, string righttext2)
        {
            Chunk tc = new Chunk(lefttext1, FontFactory.GetFont("Arial", 10f, 1, this.bcolor));
            PdfPCell cell = new PdfPCell(new Phrase
			{
				tc
			});
            cell.BackgroundColor = this.tablebackcolor;
            cell.BorderColor = this.tableborcolor;
            tab.AddCell(cell);
            tc = new Chunk(lefttext2, FontFactory.GetFont("Arial", 10f, 1, this.bcolor));
            cell = new PdfPCell(new Phrase
			{
				tc
			});
            cell.BackgroundColor = this.tablebackcolor;
            cell.BorderColor = this.tableborcolor;
            tab.AddCell(cell);
            tc = new Chunk(middletext, FontFactory.GetFont("Arial", 10f, 1, this.bcolor));
            cell = new PdfPCell(new Phrase
			{
				tc
			});
            cell.BackgroundColor = this.tablebackcolor;
            cell.BorderColor = this.tableborcolor;
            tab.AddCell(cell);
            tc = new Chunk(righttext1, FontFactory.GetFont("Arial", 10f, 0, this.bcolor));
            cell = new PdfPCell(new Phrase
			{
				tc
			});
            cell.BackgroundColor = this.tablebackcolor;
            cell.BorderColor = this.tableborcolor;
            tab.AddCell(cell);
            tc = new Chunk(righttext2, FontFactory.GetFont("Arial", 10f, 0, this.bcolor));
            cell = new PdfPCell(new Phrase
			{
				tc
			});
            cell.BackgroundColor = this.tablebackcolor;
            cell.BorderColor = this.tableborcolor;
            tab.AddCell(cell);
        }

        protected void AddrowHeader(ref PdfPTable tab, string lefttext, string middletext, string righttext)
        {
            Chunk tc = new Chunk(lefttext, FontFactory.GetFont("Arial", 11f, 1, this.bcolor));
            PdfPCell cell = new PdfPCell(new Phrase
			{
				tc
			});
            cell.BackgroundColor = this.tablebackcolor;
            cell.BorderColor = this.tableborcolor;
            cell.PaddingBottom = 10f;
            cell.PaddingLeft = 5f;
            cell.PaddingTop = 4f;
            tab.AddCell(cell);
            tc = new Chunk(middletext, FontFactory.GetFont("Arial", 11f, 1, this.bcolor));
            cell = new PdfPCell(new Phrase
			{
				tc
			});
            cell.BackgroundColor = this.tablebackcolor;
            cell.BorderColor = this.tableborcolor;
            cell.PaddingBottom = 10f;
            cell.PaddingLeft = 5f;
            cell.PaddingTop = 4f;
            tab.AddCell(cell);
            tc = new Chunk(righttext, FontFactory.GetFont("Arial", 11f, 1, this.bcolor));
            cell = new PdfPCell(new Phrase
			{
				tc
			});
            cell.BackgroundColor = this.tablebackcolor;
            cell.BorderColor = this.tableborcolor;
            cell.PaddingBottom = 10f;
            cell.PaddingLeft = 5f;
            cell.PaddingTop = 4f;
            tab.AddCell(cell);
        }

        protected void AddParagraph(string text, int alignment, Font f)
        {
            Chunk c = new Chunk(text, f);
            Phrase ph = new Phrase();
            ph.Add(c);
            Paragraph p = new Paragraph();
            p.Add(ph);
            p.SetLeading(0f, 1f);
            p.Alignment = alignment;
            this.doc.Add(p);
        }

        protected void AddNormalParagraph(string text)
        {
            Chunk c = new Chunk(text, FontFactory.GetFont("Arial", 10f, 0, this.bcolor));
            Phrase ph = new Phrase();
            ph.Add(c);
            Paragraph p = new Paragraph();
            p.Add(ph);
            p.Alignment = 0;
            p.SetLeading(0f, 1f);
            this.doc.Add(p);
        }

        protected void AddBlankParagraphLowHeight(int rows)
        {
            for (int i = 0; i < rows; i++)
            {
                Paragraph p = new Paragraph("\n");
                p.SetLeading(0f, 0.5f);
                this.doc.Add(new Paragraph(p));
            }
        }

        protected void AddBlankParagraph(int rows)
        {
            for (int i = 0; i < rows; i++)
            {
                Paragraph p = new Paragraph("\n");
                p.SetLeading(0f, 1f);
                this.doc.Add(new Paragraph(p));
            }
        }

        protected void AddColorBoldParagraph(string text)
        {
            Chunk c = new Chunk(text, FontFactory.GetFont("Arial", 10f, 1, this.hcolor));
            Phrase ph = new Phrase();
            ph.Add(c);
            Paragraph p = new Paragraph();
            p.Add(ph);
            p.Alignment = 0;
            p.SetLeading(0f, 1f);
            this.doc.Add(p);
        }
    }
}
