using DataTier;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using SGA.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;

namespace SGA.controls
{
    public partial class ctrlCMAPdf : UserControl
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

        private string _Id;

        public string Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this._Id = value;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!base.IsPostBack)
            {
                int userId = 0;
                int testId = 0;
                int packId = 0;
                DataSet dsTestDetails = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetTestDetailsById", new SqlParameter[]
                {
                    new SqlParameter("@Id", this.Id),
                    new SqlParameter("@flag", "0")
                });
                if (dsTestDetails != null)
                {
                    if (dsTestDetails.Tables.Count > 0 && dsTestDetails.Tables[0].Rows.Count > 0)
                    {
                        userId = System.Convert.ToInt32(dsTestDetails.Tables[0].Rows[0]["userId"].ToString());
                        packId = System.Convert.ToInt32(dsTestDetails.Tables[0].Rows[0]["packId"].ToString());
                    }
                }
                if (userId <= 0 || packId <= 0)
                {
                    base.Response.Redirect("~/tna/default.aspx");
                }
                this.imagepath = base.Server.MapPath("~/pdfBgImages/");
                string pdfPath = "~/pdfReports/";
                string newFile = base.Server.MapPath(string.Concat(new object[]
                {
                    pdfPath,
                    "SkillsForProcurement_IndividualReport-CM-",
                    packId + "_",
                    SGACommon.GetFullName(userId).Replace(" ",""),
                    ".pdf"
                }));
                DataSet dsPages = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageProcurementText", new SqlParameter[]
                {
                    new SqlParameter("@flag", 2)
                });
                DataSet dsCAAPage = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageCMAText", new SqlParameter[]
               {
                    new SqlParameter("@flag", 2)
               });
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@userId", SqlDbType.Int)
                };
                param[0].Value = userId;
                DataSet dsProfile = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetProfileDetails", param);
                System.DateTime dtEnd = System.Convert.ToDateTime(SqlHelper.ExecuteScalar(CommandType.Text, "select testDate from tbluserCmaTest where userId=" + userId));
                param = new SqlParameter[2];
                param[0] = new SqlParameter("@userId", SqlDbType.Int);
                param[0].Value = userId;
                param[1] = new SqlParameter("@testId", SqlDbType.Int);
                param[1].Value = testId;
                DataSet dspercentage = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spAvgValuesTests", param);
                this.doc = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
                //            DataSet dsJob = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageJobRoleSuggestion", new SqlParameter[]
                //{
                //	new SqlParameter("@id", System.Convert.ToInt32(System.Convert.ToInt32(dsProfile.Tables[0].Rows[0]["jobRole"].ToString()))),
                //	new SqlParameter("@jobSuggestion", ""),
                //	new SqlParameter("@flag", "4")
                //});
                PdfWriter writer = PdfWriter.GetInstance(this.doc, new System.IO.FileStream(newFile, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                HTMLWorker hw = new HTMLWorker(this.doc);
                try
                {
                    this.doc.Open();
                    Image imgBG = Image.GetInstance(this.imagepath + "/reportcoverfront.png");
                    imgBG.ScaleToFit(this.doc.PageSize.Width, this.doc.PageSize.Height);
                    imgBG.Alignment = 8;
                    this.doc.NewPage();
                    this.doc.Add(imgBG);
                    this.doc.SetMargins(0f, 0f, 0f, 0f);
                    Image img = Image.GetInstance(this.imagepath + "/logo.gif");
                    /*img.ScaleToFit(200f, 78f);
                    img.Alignment = 4;
                    img.SetAbsolutePosition(30f, 715f);
                    this.doc.Add(img);*/
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
                            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA_BOLD).BaseFont, 12f);
                            cb.SetTextMatrix(80f, 310f);
                            cb.SetRGBColorFill(0, 0, 0);
                            cb.ShowText(Profile.GetJobRole(System.Convert.ToInt32(dsProfile.Tables[0].Rows[0]["jobRole"].ToString())));
                            cb.EndText();
                            cb.BeginText();
                            cb.SetFontAndSize(f_cn, 12f);
                            cb.SetTextMatrix(80f, 290f);
                            cb.SetRGBColorFill(0, 0, 0);
                            cb.ShowText(Profile.GetOrganisation(System.Convert.ToInt32(dsProfile.Tables[0].Rows[0]["agencyId"].ToString())));
                            cb.EndText();
                            cb.BeginText();
                            cb.SetFontAndSize(f_cn, 12f);
                            cb.SetTextMatrix(80f, 200f);
                            cb.SetRGBColorFill(0, 0, 0);
                            cb.ShowText("Date completed: ");
                            cb.EndText();
                            cb.BeginText();
                            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA_BOLD).BaseFont, 12f);
                            cb.SetTextMatrix(80f, 180f);
                            cb.SetRGBColorFill(0, 0, 0);
                            cb.ShowText(SGACommon.ToAusTimeZone(dtEnd).ToString("dddd, dd MMMM yyyy"));
                            cb.EndText();
                            cb.BeginText();
                            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA_BOLD).BaseFont, 12f);
                            cb.SetTextMatrix(80f, 160f);
                            cb.SetRGBColorFill(0, 0, 0);
                            cb.ShowText("Email : " + SGACommon.UppercaseFirst(dsProfile.Tables[0].Rows[0]["email"].ToString()));
                            cb.EndText();
                            cb.BeginText();
                            cb.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA_BOLD).BaseFont, 16f);
                            cb.SetTextMatrix(80f, 130f);
                            cb.SetRGBColorFill(234, 66, 31);
                            cb.ShowText("www.sagov.skillsgapanalysis.com");
                            cb.EndText();
                        }
                    }
                    PdfPTable table = this.GetTable(1, 560f);
                    this.doc.SetMargins(55f, 55f, 25f, 25f);
                    this.doc.NewPage();
                    this.AddBlankParagraphLowHeight(4);
                    if (dsPages != null)
                    {
                        if (dsPages.Tables.Count > 0 && dsPages.Tables[0].Rows.Count > 0)
                        {
                            this.AddParagraph(dsPages.Tables[0].Rows[0]["page2Heading"].ToString(), 0, FontFactory.GetFont("Helvetica", 24f, 1, this.hcolor));
                            this.AddBlankParagraphLowHeight(1);
                            hw.Parse(new System.IO.StringReader(dsPages.Tables[0].Rows[0]["page2Text"].ToString()));
                            this.AddBlankParagraphLowHeight(1);
                            img = Image.GetInstance(this.imagepath + "/PastedGraphic-3.png");
                            //img.SetAbsolutePosition(170f, 610f);
                            //img.ScaleToFit(222f, 108f);
                            //img.Alignment = 5;
                            this.doc.Add(img);
                            this.AddBlankParagraphLowHeight(1);
                            hw.Parse(new System.IO.StringReader(dsPages.Tables[0].Rows[0]["page2para2"].ToString()));
                        }
                    }
                    this.doc.SetMargins(55f, 55f, 55f, 55f);
                    this.doc.NewPage();
                    string str = "Your organisation and you";
                    this.AddParagraph(str, 0, FontFactory.GetFont("Helvetica", 20f, 1, new BaseColor(0, 0, 0)));
                    this.AddBlankParagraphLowHeight(2);
                    this.AddParagraph("During your registration process at sagov.skillsgapanalysis.com you provided information to us.", 0, FontFactory.GetFont("Arial", 10f, 0, new BaseColor(0, 0, 0)));
                    this.AddParagraph("This information is displayed below for your reference. ", 0, FontFactory.GetFont("Arial", 10f, 0, new BaseColor(0, 0, 0)));
                    this.AddBlankParagraph(2);
                    str = "YOUR REGISTRATION INFORMATION";
                    this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 10f, 1, this.hcolor));
                    this.AddBlankParagraph(2);
                    table = this.GetTable(2);
                    if (dsProfile != null)
                    {
                        if (dsProfile.Tables.Count > 0 && dsProfile.Tables[0].Rows.Count > 0)
                        {
                            this.Addrow(ref table, "First Name", SGACommon.UppercaseFirst(dsProfile.Tables[0].Rows[0]["firstname"].ToString()));
                            this.Addrow(ref table, "Last Name", SGACommon.UppercaseFirst(dsProfile.Tables[0].Rows[0]["lastname"].ToString()));
                            this.Addrow(ref table, "Email Address", SGACommon.UppercaseFirst(dsProfile.Tables[0].Rows[0]["email"].ToString()));
                            this.doc.Add(table);
                            table = this.GetTable(2);
                            this.AddBlankParagraph(2);
                            str = "YOUR DETAILS";
                            this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 10f, 1, this.hcolor));
                            this.AddBlankParagraph(2);
                            this.Addrow(ref table, "Your Department", Profile.GetOrganisation(System.Convert.ToInt32(dsProfile.Tables[0].Rows[0]["agencyId"].ToString())));
                            this.Addrow(ref table, "Your branch", dsProfile.Tables[0].Rows[0]["branch"].ToString());
                            this.Addrow(ref table, "Your actual job title (position)", dsProfile.Tables[0].Rows[0]["jobTitle"].ToString());
                            this.Addrow(ref table, "Are you part of the Central Procurement function?", Profile.IsCentralProcurement(System.Convert.ToInt32(dsProfile.Tables[0].Rows[0]["isCentralProcurement"].ToString())));
                            this.Addrow(ref table, "Your current job classification", Profile.GetJobClassification(System.Convert.ToInt32(dsProfile.Tables[0].Rows[0]["jobClassification"].ToString())));
                            this.Addrow(ref table, "Years of procurement/ contract management experience", Profile.GetExperience(System.Convert.ToInt32(dsProfile.Tables[0].Rows[0]["experience"].ToString())));
                            this.Addrow(ref table, "Your procurement / contract management qualifications", Profile.GetEducation(System.Convert.ToInt32(dsProfile.Tables[0].Rows[0]["qualification"].ToString())));
                            this.Addrow(ref table, "How much of your time is allocated to Procurement and / or Contract Management activities", Profile.GetWorkingHours(System.Convert.ToInt32(dsProfile.Tables[0].Rows[0]["time"].ToString())));
                            this.Addrow(ref table, "What is the nature of the goods/ services that you most commonly procure, or manage contracts for?", Profile.GetNature(System.Convert.ToInt32(dsProfile.Tables[0].Rows[0]["nature"].ToString())));
                            this.Addrow(ref table, "What is the size of spend under your influence?", Profile.GetSize(System.Convert.ToInt32(dsProfile.Tables[0].Rows[0]["size"].ToString())));
                            this.Addrow(ref table, "How many procurements/ contracts have you managed in the past 12 months? ", Profile.GetNoOfContracts(System.Convert.ToInt32(dsProfile.Tables[0].Rows[0]["noOfContracts"].ToString())));
                            this.Addrow(ref table, "Select one of the range of activities listed which most closely reflects the nature of your role", Profile.GetRange(System.Convert.ToInt32(dsProfile.Tables[0].Rows[0]["activities"].ToString())));
                            this.doc.Add(table);
                        }
                    }
                    this.AddBlankParagraph(1);
                    float[] expr_CAF = new float[]
                    {
                        50f,
                        45f,
                        5f
                    };
                    this.doc.SetMargins(55f, 55f, 55f, 55f);
                    this.doc.NewPage();
                    str = "Your assessment results";
                    this.AddParagraph(str, 0, FontFactory.GetFont("Helvetica", 24f, 1, this.hcolor));
                    this.AddBlankParagraph(1);
                    str = "RESULTS TABLE 1 - CONTRACT MANAGEMENT";
                    this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 10f, 1, this.hcolor));
                    str = "Your results for the Procurement assessment/s taken are displayed below by dimension and level.";
                    this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 10f, 1, new BaseColor(0, 0, 0)));
                    this.AddBlankParagraph(1);
                    table = this.GetTable(3);
                    float[] colwidth = new float[]
                    {
                        45f,
                        20f,
                        35f
                    };
                    table.SetWidths(colwidth);
                    this.AddrowHeader(ref table, "Dimension", "Result", " Level");
                  
                    DataSet dsSummary = new DataSet();
                 
                        Dictionary<int, decimal> dict = new Dictionary<int, decimal>();

                        string procName = "spGetCMAGraph";
                        string topicTitle = string.Empty;
                    decimal scaledMarks = 0.00M;
                    DataSet dsTest = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetTestIdByUser", new SqlParameter[]
                        {
                        new SqlParameter("@userId", userId)
                        });
                        if (dsTest != null)
                        {
                            if (dsTest.Tables.Count > 0 && dsTest.Tables[0].Rows.Count > 0)
                            {
                                for (int j = 0; j < dsTest.Tables[0].Rows.Count; j++)
                                {
                                    if (dsTest.Tables[0].Rows[j]["testType"].ToString() == "4")
                                    { procName = "spGetCMKGraph"; }
                                    else
                                    {
                                        procName = "spGetCMAGraph";
                                    }
                                    dsSummary = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, procName, new SqlParameter[]
                         {
                        new SqlParameter("@testId", dsTest.Tables[0].Rows[j]["testId"].ToString())
                         });
                                    if (dsSummary != null)
                                    {
                                        if (dsSummary.Tables.Count > 0 && dsSummary.Tables[0].Rows.Count > 0)
                                        {
                                            for (int i = 0; i < dsSummary.Tables[0].Rows.Count; i++)
                                            {


                                            if (procName == "spGetCMKGraph")
                                            {
                                                scaledMarks = GetPercentage(Convert.ToDecimal(dsSummary.Tables[0].Rows[i]["percentage"]), 2);
                                            }
                                            else
                                            {
                                                scaledMarks = Convert.ToDecimal(dsSummary.Tables[0].Rows[i]["percentage"]);
                                            }
                                            if (dict.ContainsKey(i))
                                            {
                                                //decimal Avgpercentage = (dict[i] + Convert.ToDecimal(dsSummary.Tables[0].Rows[i]["percentage"])) / 2;
                                                decimal Avgpercentage = (dict[i] + scaledMarks) / 2;
                                                dict[i] = Avgpercentage;
                                            }
                                            else
                                            {
                                                //dict.Add(i, Convert.ToDecimal(dsSummary.Tables[0].Rows[i]["percentage"]));
                                                dict.Add(i, scaledMarks);
                                            }


                                        }

                                        }
                                    }
                                }




                                foreach (KeyValuePair<int, decimal> kvp in dict)
                                {
                                    string level = SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetLevelByPercentage", new SqlParameter[]
                                {
                        new SqlParameter("@percentage", kvp.Value)
                                }).ToString();

                                    this.Addrow(ref table, " " + dsSummary.Tables[0].Rows[kvp.Key]["topicTitle"].ToString().Replace("<br />", " "), kvp.Value.ToString("0.00") + "%", "  " + level, false, false, true);
                                }
                            }
                        }

                    
                    this.doc.Add(table);
                 
                    this.AddBlankParagraph(1);
                    str = "RESULTS TABLE 2 - COMMERCIAL AWARENESS";
                    this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 10f, 1, this.hcolor));
                    str = "Your results for the Commercial Awareness assessment, shown by dimension and level.";
                    this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 10f, 1, new BaseColor(0, 0, 0)));
                    this.AddBlankParagraph(1);
                    table = this.GetTable(3);
                    colwidth = new float[]
                    {
                        45f,
                        20f,
                        35f
                    };
                    table.SetWidths(colwidth);
                    this.AddrowHeader(ref table, "Dimension", "Result", " Level");
                    String caatestId = SqlHelper.ExecuteScalar(CommandType.Text, "select testid from tblusercaatest where userid=" + userId).ToString();
                    DataSet dsCAASummary = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetCaaGraph", new SqlParameter[]
                    {
                        new SqlParameter("@testId", caatestId)
                    });
                    if (dsCAASummary != null)
                    {
                        if (dsCAASummary.Tables.Count > 0 && dsCAASummary.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsCAASummary.Tables[0].Rows.Count; i++)
                            {
                                string level = SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetLevelByPercentage", new SqlParameter[]
                                {
                        new SqlParameter("@percentage", dsCAASummary.Tables[0].Rows[i]["percentage"].ToString())
                                }).ToString();
                                this.Addrow(ref table, " " + dsCAASummary.Tables[0].Rows[i]["topicTitle"].ToString().Replace("<br />", " "), dsCAASummary.Tables[0].Rows[i]["percentage"].ToString() + "%", "  " + level, false, false, true);
                            }
                        }
                    }
                    this.doc.Add(table);
                    this.AddBlankParagraphLowHeight(1);
                    this.AddBoldParagraph("Scoring Notes:");
                    str = "The diagnoses for each functional activity are calibrated for that activity. So the self-assessment for each activity (contract management and procurement) is calibrated to align with the knowledge assessment for that activity.This allows the combination of the result of the self - assessment with the result for the underpinning knowledge for each activity. Each assessment has merit individually, and also when combined into a consolidated overall result. Your self - assessment of your own capability is enhanced by an objective calibration of the underpinning knowledge that you bring to the performance of your duties.The results are specific to each functional activity. The self - assessment and knowledge evaluations for the same functional activity can be compared and consolidated, but cannot be compared with another functional activity.The community of practitioners for contract management and for procurement share some common characteristics, but the differences between the practitioners are such that a particular result for a contract management diagnostic is not directly comparable to the same result for a procurement diagnostic.Diagnoses are only part of the assessment of your capability, and this feedback report should serve as the basis for a conversation between you and your supervisor.";
                    this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 9f, 0, new BaseColor(0, 0, 0)));
                    this.doc.SetMargins(55f, 55f, 55f, 55f);
                    this.doc.NewPage();
                    str = "Observations of your results";
                    this.AddParagraph(str, 0, FontFactory.GetFont("Helvetica", 20f, 1, this.hcolor));
                    this.AddBlankParagraph(1);
                    str = "Ideas for development can be found in the next section.";
                    this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 12f, 1, new BaseColor(0, 0, 0)));
                    this.AddBlankParagraph(1);
                    if (dsSummary != null)
                    {
                        if (dsSummary.Tables.Count > 0 && dsSummary.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 1; i <= dsSummary.Tables[0].Rows.Count; i++)
                            {
                                string level = String.Empty;
                                string percentage = string.Empty;
                                if (dict.ContainsKey(i - 1))
                                {
                                    percentage = dict[i - 1].ToString("0.00");
                                    level = SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetLevelByPercentage", new SqlParameter[]
                                 {
                        new SqlParameter("@percentage", dict[i - 1])
                                 }).ToString();
                                }
                                else
                                {
                                    percentage = dsSummary.Tables[0].Rows[i - 1]["percentage"].ToString();
                                    level = SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetLevelByPercentage", new SqlParameter[]
                                 {
                        new SqlParameter("@percentage", dsSummary.Tables[0].Rows[i - 1]["percentage"].ToString())
                                 }).ToString();
                                }
                                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetReportRecommendation", new SqlParameter[]
                    {
                        new SqlParameter("@reportType", 2),
                        new SqlParameter("@level", GetLevelForInsert(Convert.ToDecimal(percentage))),
                        new SqlParameter("@topicId", i)

                    });
                                DataSet dsSuggestion = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSuggestionByTopic", new SqlParameter[]
                      {
                        new SqlParameter("@suggestionType", 1),
                        new SqlParameter("@topicId", i)

                      });
                                str = i.ToString() + ". " + dsSummary.Tables[0].Rows[i - 1]["topicTitle"].ToString().Replace("<br />", " ");
                                this.AddParagraph(str, 0, FontFactory.GetFont("Helvetica", 14f, 1, this.hcolor));

                                hw.Parse(new System.IO.StringReader(dsSuggestion.Tables[0].Rows[0]["SuggestionText"].ToString().Replace("@level", level).Replace("@score", percentage)));
                                this.AddBlankParagraph(1);
                                this.AddBoldParagraph("OBSERVATIONS:");
                                //this.AddBlankParagraph(1);
                                hw.Parse(new System.IO.StringReader(ds.Tables[0].Rows[0]["openingStatement"].ToString()));
                                this.AddBlankParagraph(1);
                                hw.Parse(new System.IO.StringReader(ds.Tables[0].Rows[0]["dynamicRecommendation"].ToString()));
                                //this.AddBlankParagraph(3);
                                this.doc.SetMargins(55f, 55f, 55f, 55f);
                                this.doc.NewPage();
                            }
                        }
                    }
                    this.doc.SetMargins(0f, 0f, 0f, 0f);
                    this.doc.NewPage();
                    imgBG = Image.GetInstance(this.imagepath + "/reportmid.png");
                    imgBG.ScaleToFit(this.doc.PageSize.Width, this.doc.PageSize.Height);
                    imgBG.Alignment = 8;
                    this.doc.Add(imgBG);
                    this.AddBlankParagraph(10);
                    Paragraph title = new Paragraph();
                    title.Alignment = Element.ALIGN_CENTER;
                    title.Font = FontFactory.GetFont("Arial", 24f, 1, this.hcolor);
                    title.Add(SGACommon.UppercaseFirst(dsProfile.Tables[0].Rows[0]["firstname"].ToString()) + " " + SGACommon.UppercaseFirst(dsProfile.Tables[0].Rows[0]["lastname"].ToString()));
                    this.doc.Add(title);
                    this.AddBlankParagraph(3);
                    title = new Paragraph();
                    title.Alignment = Element.ALIGN_CENTER;
                    title.Font = FontFactory.GetFont("Arial", 24f, 1, new BaseColor(0, 0, 0));
                    title.Add("Personal Development Plan");
                    this.doc.Add(title);
                    this.AddBlankParagraph(3);
                    title = new Paragraph();
                    title.Alignment = Element.ALIGN_CENTER;
                    title.Font = FontFactory.GetFont("Arial", 12f, 1, new BaseColor(0, 0, 0));
                    title.Add(SGACommon.UppercaseFirst("In the pages that follow are suggestions for you to"));
                    this.doc.Add(title);
                    title = new Paragraph();
                    title.Alignment = Element.ALIGN_CENTER;
                    title.Font = FontFactory.GetFont("Arial", 12f, 1, new BaseColor(0, 0, 0));
                    title.Add(SGACommon.UppercaseFirst("implement in your work environment."));
                    this.doc.Add(title);
                    //img = Image.GetInstance(this.imagepath + "/SaGovLogo.png");
                    //img.ScaleToFit(110f, 110f);
                    //img.Alignment = 4;
                    //img.SetAbsolutePosition(230f, 400f);
                    //this.doc.Add(img);
                    this.doc.SetMargins(55f, 55f, 55f, 55f);
                    this.doc.NewPage();
                    if (dsCAAPage != null)
                    {
                        if (dsCAAPage.Tables.Count > 0 && dsCAAPage.Tables[0].Rows.Count > 0)
                        {
                            this.AddParagraph(dsCAAPage.Tables[0].Rows[0]["page1Heading"].ToString(), 0, FontFactory.GetFont("Arial", 24f, 1, this.hcolor));
                            this.AddBlankParagraphLowHeight(1);
                            hw.Parse(new System.IO.StringReader(dsCAAPage.Tables[0].Rows[0]["page1Text"].ToString()));
                            this.AddBlankParagraphLowHeight(1);
                        }
                    }
                    this.doc.SetMargins(55f, 55f, 55f, 55f);
                    this.doc.NewPage();
                    this.AddBoldParagraph("SUGGESTIONS FOR YOU TO APPLY IN YOUR WORK ENVIRONMENT");
                    this.AddBlankParagraph(1);
                    if (dsCAASummary != null)
                    {
                        if (dsCAASummary.Tables.Count > 0 && dsCAASummary.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 1; i <= dsCAASummary.Tables[0].Rows.Count; i++)
                            {
                                string level = SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetLevelByPercentage", new SqlParameter[]
                                {
                        new SqlParameter("@percentage", dsCAASummary.Tables[0].Rows[i-1]["percentage"].ToString())
                                }).ToString();
                                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetReportRecommendation", new SqlParameter[]
                    {
                        new SqlParameter("@reportType", 3),
                        new SqlParameter("@level", level),
                        new SqlParameter("@topicId", i)

                    });
                                DataSet dsSuggestion = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSuggestionByTopic", new SqlParameter[]
                      {
                        new SqlParameter("@suggestionType", 3),
                        new SqlParameter("@topicId", i)

                      });
                                str = i.ToString() + ". " + dsCAASummary.Tables[0].Rows[i - 1]["topicTitle"].ToString().Replace("<br />", " ");
                                this.AddParagraph(str, 0, FontFactory.GetFont("Helvetica", 14f, 1, this.hcolor));
                                hw.Parse(new System.IO.StringReader(dsSuggestion.Tables[0].Rows[0]["SuggestionText"].ToString().Replace("@level", level).Replace("@score", dsCAASummary.Tables[0].Rows[i - 1]["percentage"].ToString())));
                                this.AddBlankParagraph(1);
                                this.AddBoldParagraph("SUGGESTIONS:");
                                hw.Parse(new System.IO.StringReader(ds.Tables[0].Rows[0]["dynamicRecommendation"].ToString()));
                                this.doc.SetMargins(55f, 55f, 55f, 55f);
                                this.doc.NewPage();
                            }
                        }
                    }

                    this.doc.SetMargins(55f, 55f, 55f, 55f);
                    this.doc.NewPage();
                    str = "Contract Management based suggestions";
                    this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 24f, 1, this.hcolor));
                    this.AddBlankParagraph(1);
                    if (dsSummary != null)
                    {
                        if (dsSummary.Tables.Count > 0 && dsSummary.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 1; i <= dsSummary.Tables[0].Rows.Count; i++)
                            {
                                string level = string.Empty;
                                string percentage = string.Empty;
                                if (dict.ContainsKey(i - 1))
                                {
                                    percentage = dict[i - 1].ToString("0.00");
                                    level = SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetLevelByPercentage", new SqlParameter[]
                                 {
                        new SqlParameter("@percentage", dict[i-1])
                                 }).ToString();
                                }
                                else
                                {
                                    percentage = dsSummary.Tables[0].Rows[i - 1]["percentage"].ToString();
                                    level = SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetLevelByPercentage", new SqlParameter[]
                                 {
                        new SqlParameter("@percentage", dsSummary.Tables[0].Rows[i - 1]["percentage"].ToString())
                                 }).ToString();
                                }
                                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetReportRecommendation", new SqlParameter[]
                    {
                        new SqlParameter("@reportType", 5),
                        new SqlParameter("@level", GetLevelForInsert(Convert.ToDecimal(percentage))),
                        new SqlParameter("@topicId", i)

                    });
                                DataSet dsSuggestion = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSuggestionByTopic", new SqlParameter[]
                      {
                        new SqlParameter("@suggestionType", 2),
                        new SqlParameter("@topicId", i)

                      });
                                str = i.ToString() + ". " + dsSummary.Tables[0].Rows[i - 1]["topicTitle"].ToString().Replace("<br />", " ");
                                this.AddParagraph(str, 0, FontFactory.GetFont("Helvetica", 14f, 1, this.hcolor));
                               
                                hw.Parse(new System.IO.StringReader(dsSuggestion.Tables[0].Rows[0]["SuggestionText"].ToString().Replace("@level", level).Replace("@score", percentage)));
                                this.AddBlankParagraph(1);
                                this.AddBoldParagraph("SUGGESTIONS:");
                                this.AddBlankParagraph(1);
                                hw.Parse(new System.IO.StringReader(ds.Tables[0].Rows[0]["dynamicRecommendation"].ToString()));
                                //this.AddBlankParagraph(3);
                                this.doc.SetMargins(55f, 55f, 55f, 55f);
                                this.doc.NewPage();
                            }
                        }
                    }
                    //               DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSuggestions", new SqlParameter[]
                    //{
                    //	new SqlParameter("@flag", "1")
                    //});
                    //               if (ds != null)
                    //               {
                    //                   if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    //                   {
                    //                       for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    //                       {
                    //                           this.doc.SetMargins(55f, 55f, 55f, 55f);
                    //                           this.doc.NewPage();
                    //                           this.AddParagraph(j + 1 + ". " + ds.Tables[0].Rows[j]["topicName"].ToString().Replace("<br />", " "), 0, FontFactory.GetFont("Arial", 24f, 1, new BaseColor(0, 0, 0)));
                    //                           this.AddBlankParagraph(1);
                    //                           hw.Parse(new System.IO.StringReader(ds.Tables[0].Rows[j]["SuggestionText"].ToString().Replace("@level", dsSummary.Tables[0].Rows[j]["Level"].ToString()).Replace("@score", dsSummary.Tables[0].Rows[j]["percentage"].ToString() + "%")));
                    //                           this.AddBlankParagraphLowHeight(1);
                    //                           this.AddParagraph("RECOMMENDATIONS", 0, FontFactory.GetFont("Arial", 12f, 1, new BaseColor(234, 66, 31)));
                    //                           this.AddBlankParagraphLowHeight(1);
                    //                           hw.Parse(new System.IO.StringReader(dsSummary.Tables[0].Rows[j]["training"].ToString()));
                    //                       }
                    //                   }
                    //               }
                    //this.doc.SetMargins(0f, 0f, 0f, 0f);
                    //this.doc.NewPage();
                    //imgBG = Image.GetInstance(this.imagepath + "/sizzer_bg.jpg");
                    //imgBG.ScaleToFit(this.doc.PageSize.Width, this.doc.PageSize.Height);
                    //imgBG.Alignment = 8;
                    //this.doc.Add(imgBG);
                    //if (dsPages != null)
                    //{
                    //    if (dsPages.Tables.Count > 0 && dsPages.Tables[0].Rows.Count > 0)
                    //    {
                    //        if (dsProfile != null)
                    //        {
                    //            if (dsProfile.Tables.Count > 0 && dsProfile.Tables[0].Rows.Count > 0)
                    //            {
                    //                this.AddBlankParagraph(12);
                    //                hw.Parse(new System.IO.StringReader(dsPages.Tables[0].Rows[0]["page14Text"].ToString().Replace("@v0", SGACommon.UppercaseFirst(dsProfile.Tables[0].Rows[0]["firstname"].ToString())).Replace("@v1", SGACommon.UppercaseFirst(dsProfile.Tables[0].Rows[0]["lastname"].ToString()))));
                    //            }
                    //        }
                    //    }
                    //}
                    //img = Image.GetInstance(this.imagepath + "/emailwelcome.jpg");
                    //img.SetAbsolutePosition(0f, 0f);
                    //img.ScaleToFit(600f, 600f);
                    //img.Alignment = 4;
                    //img.SetAbsolutePosition(0f, 0f);
                    //this.doc.Add(img);

                    //img = Image.GetInstance(this.imagepath + "/QldGov-Logo.png");
                    //img.ScaleToFit(110f, 110f);
                    //img.Alignment = 4;
                    //img.SetAbsolutePosition(230f, 400f);
                    //this.doc.Add(img);

                    //DataRow[] foundRows = dsSummary.Tables[0].Select("1=1", "topicMarks asc");
                    //int k = 0;
                    //this.doc.SetMargins(55f, 55f, 55f, 55f);
                    //this.doc.NewPage();
                    //if (dsPages != null)
                    //{
                    //    if (dsPages.Tables.Count > 0 && dsPages.Tables[0].Rows.Count > 0)
                    //    {
                    //        this.AddParagraph(dsPages.Tables[0].Rows[0]["page17Heading"].ToString(), 0, FontFactory.GetFont("Arial", 24f, 1, new BaseColor(0, 0, 0)));
                    //        this.AddBlankParagraph(1);
                    //        //hw.Parse(new System.IO.StringReader(dsPages.Tables[0].Rows[0]["page17Text"].ToString().Replace("@v0", Profile.GetJobRole(System.Convert.ToInt32(dsProfile.Tables[0].Rows[0]["jobRole"].ToString()))).Replace("@v1", dsJob.Tables[0].Rows[0]["page14Para1"].ToString()).Replace("@v2", dsJob.Tables[0].Rows[0]["page14Para2"].ToString())));
                    //    }
                    //}
                    //img = Image.GetInstance(this.imagepath + "/elearning.jpg");
                    //img.ScaleToFit(600f, 600f);
                    //img.Alignment = 4;
                    //img.SetAbsolutePosition(0f, 25f);
                    //this.doc.Add(img);



                    //DataSet dsPlans = DataTier.SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetPlansByPercentageForSSA", new SqlParameter[] {
                    //    new SqlParameter("@tableType","1"),
                    //    new SqlParameter("@testId",testId),
                    //});

                    //if (dsPlans != null) {
                    //    if (dsPlans.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
                    //        string strPageCode = dsPlans.Tables[0].Rows[0]["planDetails"].ToString();
                    //        this.doc.SetMargins(30f, 30f, 30f, 10f);
                    //        this.doc.NewPage();
                    //        string[] strArr = dsPlans.Tables[0].Rows[0]["timeweek"].ToString().Split(':');
                    //        DateTime dt = Convert.ToDateTime(dsPlans.Tables[0].Rows[0]["testDate"].ToString());
                    //        if (strArr.Length > 0) {
                    //            for (int i = 0; i < strArr.Length; i++) {
                    //                int hours = Convert.ToInt32(strArr[i].ToString());
                    //                double months = (((1.0 / 30.0) * (hours * 60.0)) / 4.0 + 1);
                    //                dt = dt.AddMonths(Convert.ToInt32(months));
                    //                strPageCode = strPageCode.Replace("@v" + i.ToString(), dt.ToString("dd MMM yyyy")).Replace("@per", Convert.ToDecimal(dsPlans.Tables[0].Rows[0]["percentage"].ToString()).ToString("#.##") + "%");
                    //                //strPageCode = strPageCode.Replace("@v" + i.ToString(), Convert.ToDateTime(dsPlans.Tables[0].Rows[0]["testDate"].ToString()).AddMonths(months).ToString("dd/MM/yyyy"));

                    //            }
                    //        }
                    //        hw.Parse(new System.IO.StringReader(strPageCode.ToString()));
                    //    }
                    //}

                    ////hw.Parse(new System.IO.StringReader(dsJob.Tables[0].Rows[0]["jobSuggestion"].ToString()));


                    //this.doc.SetMargins(55f, 55f, 55f, 55f);
                    //this.doc.NewPage();
                    //if (dsPages != null)
                    //{
                    //    if (dsPages.Tables.Count > 0 && dsPages.Tables[0].Rows.Count > 0)
                    //    {
                    //        this.AddParagraph(dsPages.Tables[0].Rows[0]["page16Heading"].ToString(), 0, FontFactory.GetFont("Arial", 24f, 1, new BaseColor(0, 0, 0)));
                    //        this.AddBlankParagraph(1);
                    //        hw.Parse(new System.IO.StringReader(dsPages.Tables[0].Rows[0]["page16Text"].ToString()));
                    //        this.AddBlankParagraph(2);
                    //        DataRow[] array = foundRows;
                    //        for (int l = 0; l < array.Length; l++)
                    //        {
                    //            DataRow dr = array[l];
                    //            if (k < 3)
                    //            {
                    //                if (k == 1)
                    //                {
                    //                    this.doc.SetMargins(55f, 55f, 55f, 55f);
                    //                    this.doc.NewPage();
                    //                }
                    //                this.AddParagraph(dr["mgtHeading"].ToString().Replace("<br />", " ").ToUpper(), 0, FontFactory.GetFont("Arial", 12f, 1, new BaseColor(234, 66, 31)));
                    //                this.AddBlankParagraphLowHeight(1);
                    //                hw.Parse(new System.IO.StringReader(dr["mgtTraining"].ToString()));
                    //                this.AddBlankParagraphLowHeight(2);
                    //                hw.Parse(new System.IO.StringReader("<p style=\"padding-left:140px;\">&nbsp;<a href=\"" + dr["mgtmorelink"] + "\" target=\"_blank\" style=\"font-family:Arial;font-size:12px;color:#FF7C00;\">CLICK HERE TO LEARN MORE ABOUT THIS WORKSHOP ></a></p>"));
                    //                this.AddBlankParagraphLowHeight(6);
                    //                k++;
                    //            }
                    //        }
                    //    }
                    //}
                    //this.doc.SetMargins(55f, 55f, 55f, 55f);
                    //this.doc.NewPage();
                    //img = Image.GetInstance(this.imagepath + "/page20.jpg");
                    //img.ScaleToFit(530f, 510f);
                    //img.Alignment = 4;
                    //img.SetAbsolutePosition(30f, 550f);
                    //img.Annotation = new Annotation(0f, 0f, 0f, 0f, "http://events.criticalskillsboost.com/booking/");
                    //this.doc.Add(img);
                    //if (dsPages != null)
                    //{
                    //    if (dsPages.Tables.Count > 0 && dsPages.Tables[0].Rows.Count > 0)
                    //    {
                    //        this.AddBlankParagraph(23);
                    //        this.AddParagraph(dsPages.Tables[0].Rows[0]["page20heading"].ToString(), 0, FontFactory.GetFont("Arial", 20f, 1, new BaseColor(0, 0, 0)));
                    //        hw.Parse(new System.IO.StringReader(dsPages.Tables[0].Rows[0]["page20text"].ToString()));
                    //        this.AddBlankParagraph(1);
                    //    }
                    //}
                    //this.doc.SetMargins(55f, 55f, 55f, 55f);
                    //this.doc.NewPage();
                    //img = Image.GetInstance(this.imagepath + "/page21.jpg");
                    //img.ScaleToFit(500f, 510f);
                    //img.Alignment = 4;
                    //img.SetAbsolutePosition(45f, 350f);
                    //this.doc.Add(img);
                    //if (dsPages != null)
                    //{
                    //    if (dsPages.Tables.Count > 0 && dsPages.Tables[0].Rows.Count > 0)
                    //    {
                    //        this.AddBlankParagraph(2);
                    //        this.AddParagraph(dsPages.Tables[0].Rows[0]["page21heading"].ToString(), 0, FontFactory.GetFont("Arial", 20f, 1, new BaseColor(234, 67, 32)));
                    //        hw.Parse(new System.IO.StringReader(dsPages.Tables[0].Rows[0]["page21text"].ToString()));
                    //        this.AddBlankParagraph(1);
                    //    }
                    //}

                    //this.doc.SetMargins(55f, 55f, 0f, 0f);
                    //this.doc.NewPage();
                    //img = iTextSharp.text.Image.GetInstance(this.imagepath + "/page19banner.png");
                    //img.ScaleToFit(595f, 710f);
                    //img.Alignment = 4;
                    //img.SetAbsolutePosition(0f, 601f);
                    //this.doc.Add(img);

                    //this.AddBlankParagraph(22);
                    //this.AddParagraph(dsPages.Tables[0].Rows[0]["ifpsmHeading"].ToString(), 0, FontFactory.GetFont("Arial", 25f, 1, new BaseColor(0, 0, 0)));
                    //this.AddBlankParagraph(1);
                    //hw.Parse(new System.IO.StringReader(dsPages.Tables[0].Rows[0]["ifpsmText"].ToString()));


                    //this.doc.SetMargins(55f, 55f, 55f, 55f);
                    //this.doc.NewPage();
                    ///*imgBG = Image.GetInstance(this.imagepath + "/page18bg.jpg");
                    //imgBG.ScaleToFit(this.doc.PageSize.Width, this.doc.PageSize.Height);
                    //imgBG.Alignment = 8;
                    //imgBG.SetAbsolutePosition(0f, 0f);
                    //this.doc.Add(imgBG);*/
                    //img = Image.GetInstance(this.imagepath + "/compraralogo.png");
                    //img.SetAbsolutePosition(50f, 750f);
                    //img.ScaleToFit(172f, 58f);
                    //img.Alignment = 5;
                    //this.doc.Add(img);

                    //img = Image.GetInstance(this.imagepath + "/QldGov-Logo.png");
                    //img.SetAbsolutePosition(450f, 750f);
                    //img.ScaleToFit(110f, 110f);
                    //img.Alignment = 5;
                    //this.doc.Add(img);

                    //img = Image.GetInstance(this.imagepath + "/Updatedtagline.jpg");
                    //img.SetAbsolutePosition(50f, 680f);
                    //img.ScaleToFit(500f, 520f);
                    //img.Alignment = 0;
                    //this.doc.Add(img);

                    //if (dsPages != null)
                    //{
                    //    if (dsPages.Tables.Count > 0 && dsPages.Tables[0].Rows.Count > 0)
                    //    {
                    //        this.AddBlankParagraph(12);
                    //        //this.AddParagraph(dsPages.Tables[0].Rows[0]["page18Heading"].ToString(), 0, FontFactory.GetFont("Arial", 24f, 1, new BaseColor(0, 0, 0)));
                    //        this.AddParagraph("About Comprara", 0, FontFactory.GetFont("Arial", 24f, 1, new BaseColor(255, 124, 0)));
                    //        hw.Parse(new System.IO.StringReader(dsPages.Tables[0].Rows[0]["page18SubPara1"].ToString()));
                    //        this.AddBlankParagraph(1);
                    //    }
                    //}
                    this.doc.SetMargins(55f, 55f, 55f, 55f);
                    this.doc.NewPage();
                    if (dsPages != null)
                    {
                        if (dsPages.Tables.Count > 0 && dsPages.Tables[0].Rows.Count > 0)
                        {
                            img = Image.GetInstance(this.imagepath + "/reportcoverback.png");
                            img.ScaleToFit(this.doc.PageSize.Width, this.doc.PageSize.Height);
                            img.Alignment = 8;
                            img.SetAbsolutePosition(1f, 3f);
                            this.doc.Add(img);
                            //img = Image.GetInstance(this.imagepath + "/compraralogo.png");
                            //img.SetAbsolutePosition(170f, 610f);
                            //img.ScaleToFit(222f, 108f);
                            //img.Alignment = 5;
                            //this.doc.Add(img);
                            this.AddBlankParagraph(10);
                            str = "Comprara means ‘to purchase’ and we help organisations to gain more ground.Comprara works with ASX 200 listed companies and Government organisations giving clients insights into how well they are performing against others and in the context of their own unique strategies.With insights gained and roadmaps developed your capacity will grow to do ‘more with less’ or ‘more with the same’.";
                            this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 10f, 0, new BaseColor(0, 0, 0)));
                            this.AddBlankParagraph(10);
                            this.AddParagraph("www.comprara.com.au", 0, FontFactory.GetFont("Arial", 10f, 0, new BaseColor(0, 0, 0)));
                            this.AddParagraph("www.p-i.com.au", 0, FontFactory.GetFont("Arial", 10f, 0, new BaseColor(0, 0, 0)));
                            this.AddParagraph("www.academyofprocurement.com", 0, FontFactory.GetFont("Arial", 10f, 0, new BaseColor(0, 0, 0)));
                            this.AddParagraph("www.skillsgapanalysis.com", 0, FontFactory.GetFont("Arial", 10f, 0, new BaseColor(0, 0, 0)));
                            this.AddParagraph("www.criticalskillsboost.com", 0, FontFactory.GetFont("Arial", 10f, 0, new BaseColor(0, 0, 0)));
                            this.AddBlankParagraph(15);
                            title = new Paragraph();
                            title.Alignment = Element.ALIGN_CENTER;
                            title.Font = FontFactory.GetFont("Arial", 12f, 1, new BaseColor(0, 0, 0));
                            title.Add(SGACommon.UppercaseFirst("Disclaimer"));
                            this.doc.Add(title);
                            str = "This document is intended to provide the recipient (“Recipient”) with preliminary background information, of a general nature. Each Recipient should conduct and rely on their own investigation and analysis and should seek their own professional advice.To the maximum extent permitted by law, Comprara and its shareholders, advisers, directors, employees and consultants expressly disclaim any and all liability for any loss suffered or incurred by any person in connection with: (a)The provision or use of information in this document to or by the Recipient; (b)any errors or omission from this document; and(c) Any other written or oral communications transmitted to the Recipient in the course of the Recipient’s evaluation of this document. Except for statutory liability, which cannot be disclaimed, the Recipient waives its right to make any claim that it may have against Comprara in relation to this document.";
                            this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 10f, 0, new BaseColor(0, 0, 0)));
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
                this.ShowPdf(newFile);
            }
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

        private void ShowPdf(string s)
        {
            base.Response.ClearContent();
            base.Response.ClearHeaders();
            base.Response.AddHeader("Content-Disposition", "inline;filename=" + s);
            base.Response.ContentType = "application/pdf";
            base.Response.WriteFile(s);
            base.Response.Flush();
            base.Response.Clear();
        }

        private decimal GetPercentage(decimal score, int flag)
        {
            score = Convert.ToDecimal(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "getScaledDownPercentage", new SqlParameter[]
                                 {
                        new SqlParameter("@score", score),
                        new SqlParameter("@flag",flag)
                                 }));
            return score;
        }
        private string GetLevelForInsert(decimal score)
        {
            string level = string.Empty;
            if (score <= 21.23M)
            {
                level = "Novice";
            }
            else if (score <= 35.52M)
            {
                level = "Awareness";
            }
            else if (score <= 49.80M)
            {
                level = "Understanding";
            }
            else if (score <= 78.37M)
            {
                level = "Practitioner";
            }
            else
            {
                level = "Expert";
            }
            return level;
        }
    }
}
//class PDFFooter : PdfPageEventHelper
//{
//    PdfContentByte moCB;
//    BaseFont moBF;
//    PdfTemplate moTemplate;

//    public override void OnCloseDocument(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
//    {

//        moTemplate.BeginText();
//        moTemplate.SetFontAndSize(moBF, 8);
//        moTemplate.ShowText((writer.PageNumber - 2).ToString());
//        moTemplate.EndText();
//    }

//    public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
//    {
//        if (writer.PageNumber != 1)
//            setFooter(writer, document);
//    }

//    public override void OnOpenDocument(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
//    {
//        try
//        {
//            moBF = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
//            moCB = writer.DirectContent;
//            moTemplate = moCB.CreateTemplate(50, 50);


//        }
//        catch (DocumentException de)
//        {

//        }
//        catch (IOException ioe)
//        {
//        }
//    }

//    public void setFooter(iTextSharp.text.pdf.PdfWriter oWriter, iTextSharp.text.Document oDocument)
//    {

//        //three columns at bottom of page
//        string sText = null;
//        float fLen = 0;

//        //---Column 1: Disclaimer
//        sText = "Skills for Procurement - Assess and Build";
//        fLen = moBF.GetWidthPoint(sText, 8);

//        moCB.BeginText();
//        moCB.SetFontAndSize(moBF, 8);
//        moCB.SetTextMatrix(30, 30);
//        moCB.ShowText(sText);
//        moCB.EndText();

//        //---Column 2: 
//        sText = "Individual Report";
//        fLen = moBF.GetWidthPoint(sText, 8);

//        moCB.BeginText();
//        moCB.SetFontAndSize(moBF, 8);
//        moCB.SetTextMatrix(oDocument.PageSize.Width / 2 - fLen / 2, 30);
//        moCB.ShowText(sText);
//        moCB.EndText();


//        //---Column 3: Page Number
//        int iPageNumber = oWriter.PageNumber - 1;
//        sText = "Page " + iPageNumber + " of ";
//        fLen = moBF.GetWidthPoint(sText, 8);

//        moCB.BeginText();
//        moCB.SetFontAndSize(moBF, 8);
//        moCB.SetTextMatrix(oDocument.PageSize.Width - 90, 30);
//        moCB.ShowText(sText);
//        moCB.EndText();

//        moCB.AddTemplate(moTemplate, oDocument.PageSize.Width - 90 + fLen, 30);
//        moCB.BeginText();
//        moCB.SetFontAndSize(moBF, 8);
//        moCB.SetTextMatrix(280, 820);
//        moCB.EndText();

//    }
//}

