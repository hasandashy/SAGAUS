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
using System.Linq;
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
                bool isExpert = false;
                Dictionary<int, decimal> _marks = new Dictionary<int, decimal>();
                DataSet dsTestDetails = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetTestDetailsById", new SqlParameter[]
                {
                    new SqlParameter("@Id", this.Id),
                    new SqlParameter("@flag", "0")
                });

                

                string initYear = SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetInitYear", new SqlParameter[]
                                {
                        new SqlParameter("@reportId", this.Id)
                                }).ToString();
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
                System.DateTime dtEnd = System.Convert.ToDateTime(SqlHelper.ExecuteScalar(CommandType.Text, "select testDate from tblusercaatest where userId=" + userId + " and initYear = '" + initYear +"'"));
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
                    str = "Your results for the Contract assessment/s taken are displayed below by dimension and level.";
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
                        new SqlParameter("@userId", userId),
                        new SqlParameter("@initYear",initYear)
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
                                _marks.Add(Convert.ToInt32(dsSummary.Tables[0].Rows[kvp.Key]["topicId"]), kvp.Value);
                                if (level.ToLower() == "expert")
                                {
                                    isExpert = true;
                                }
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
                    String caatestId = SqlHelper.ExecuteScalar(CommandType.Text, "select testid from tblusercaatest where userid=" + userId + " and initYear = '" + initYear + "'").ToString();
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
                                //if (level.ToLower() == "expert")
                                //{
                                //    isExpert = true;
                                //}
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
                        new SqlParameter("@suggestionType", 2),
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
                        new SqlParameter("@level", GetLevelForInsertRecoomendation(Convert.ToDecimal(percentage))),
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
                    //New Page
                    if (_marks.Count == 8)
                    {
                        var myList = _marks.ToList();

                        myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
                        myList.RemoveRange(3, 5);

                        this.doc.SetMargins(55f, 55f, 25f, 25f);
                        this.doc.NewPage();
                        this.AddBlankParagraphLowHeight(4);
                        this.AddParagraph("Your Priority Training Recommendations", 0, FontFactory.GetFont("Helvetica", 20f, 1, new BaseColor(0, 0, 0)));
                        this.AddBlankParagraphLowHeight(4);

                        int p = 1;
                        foreach (var item in myList)
                        {
                            DataSet ds1 = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetElearningRecommendation", new SqlParameter[]
                      {
                        new SqlParameter("@reportType", 5),
                        new SqlParameter("@level", GetLevelForInsertRecoomendation(item.Value)),
                        new SqlParameter("@topicId", item.Key)

                      });

                            this.AddParagraph("Priority " + p.ToString(), 0, FontFactory.GetFont("Helvetica", 14f, 1, this.hcolor));
                            this.AddBlankParagraphLowHeight(1);
                            hw.Parse(new System.IO.StringReader(ds1.Tables[0].Rows[0]["recommendation"].ToString()));
                            this.AddBlankParagraphLowHeight(2);
                            p++;
                        }
                    }
                    //
                    if (isExpert)
                    {
                        hw = new HTMLWorker(this.doc);
                        //new pages
                        this.doc.SetMargins(55f, 55f, 25f, 25f);
                        this.doc.NewPage();
                        this.AddBlankParagraphLowHeight(4);
                        this.AddParagraph("Leadership", 0, FontFactory.GetFont("Helvetica", 24f, 1, this.hcolor));
                        this.AddBlankParagraphLowHeight(4);
                        str = "As a Procurement Manager/Director or an identified Expert in one or more dimensions you are encouraged to undertake leadership training as part of your ongoing professional development. Here are some suggestions:";
                        this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 10f, 0, new BaseColor(0, 0, 0)));
                        this.AddBlankParagraphLowHeight(4);
                        this.AddParagraph("The Office of the Public Sector:", 0, FontFactory.GetFont("Helvetica", 14f, 1, this.hcolor));

                        str = "<p style='font-size:10px;font-face:Arial'><p><a style='color:#0000EE;text-decoration: underline;' href='https://publicsector.sa.gov.au/people/leadership-development/south-australian-leadership-academy/'>South Australian Leadership Academy</a>&nbsp;&ndash;&nbsp;for managers and executives - provides a range of leadership development programs. Visit the website to view current opportunities.</p><p><a style='color:#0000EE;text-decoration: underline;' href = 'https://publicsector.sa.gov.au/people/leadership-development/jawun-executive-secondments/'> Jawun Executive Secondments</a> &nbsp; &ndash; for executives and aspiring executives - a six week residential placement & nbsp; that benefits the indigenous led organisations, their communities, and the participants.</p><p><a style='color:#0000EE;text-decoration: underline;' href = 'https://publicsector.sa.gov.au/people/leadership-development/public-sector-management-program/'> Public Sector Management Program </a> &nbsp; &ndash; for current and aspiring public sector leaders.Participants who successfully complete the course gain a Graduate Certificate in Business (Public Sector Management).</p><p><a style='color:#0000EE;text-decoration: underline;' href = 'https://publicsector.sa.gov.au/people/leadership-development/south-australian-executive-service/'> South Australian Executive Service(SAES)</a>&nbsp;&ndash; for new and current executives.Provides a charter and competency framework and induction program for new executives.</p><p style='color: white'>linebreak</p><p>The Office of the Public Sector&nbsp;offer three competency frameworks to guide aspiring and current leaders in building their skills and knowledge:</p><p><a style='color:#0000EE;text-decoration: underline;' href = 'https://publicsector.sa.gov.au/wp-content/uploads/20080101-SAES-Competency-Framework.pdf'> SAES Competency Framework(PDF) 802KB&nbsp;</a></p><p><a style='color:#0000EE;text-decoration: underline;' href = 'https://publicsector.sa.gov.au/wp-content/uploads/20120412-First-Line-Manager-Competency-Framework.pdf'> First Line Manager Competency Framework(PDF) 407KB&nbsp;</a></p><p><a style='color:#0000EE;text-decoration: underline;' href = 'https://publicsector.sa.gov.au/wp-content/uploads/20120412-Middle-Manager-Competency-Framework.pdf'> Middle Manager Competency Framework(PDF) 327KB</a></p></p>";
                        hw.Parse(new System.IO.StringReader(str));
                        this.AddBlankParagraphLowHeight(4);
                        this.AddParagraph("Institute of Public Administration Australia (IPAA)", 0, FontFactory.GetFont("Helvetica", 14f, 1, this.hcolor));
                        str = "<p style='font-size:10px;font-face:Arial'><p><span style='text - decoration: underline; color: #0000ff;'><a style='color: #0000ff; text-decoration: underline;' href='http://www.sa.ipaa.org.au/PD/ExtendedPrograms.asp'>New and Emerging Managers Series</a>;</span> a 4 day program for people coming to grips with leading a team and managing others for the first time.</p><p><span style = 'text-decoration: underline; color: #0000ff;'><a style = 'color:#0000ff; text-decoration: underline;' href = 'http://www.sa.ipaa.org.au/PD/ExtendedPrograms.asp'> Management and Development Series - &nbsp; A Pathway to SAES</a>;</span> a 8 x half day program for public sector employees seeking to enhance and develop management and leadership skills.</p><p><span style = 'text-decoration: underline; color: #0000ff;'><a style='color: #0000ff; text-decoration: underline;' href='http://sa.ipaa.org.au/PD/21stCenturyManager.asp'>21st Century Manager - Core Skills for the new Millennium</a>;</span> a 6 x half day program to equip public sector managers with the requisite skills for survival in the 21st century.&nbsp;</p></p>";
                        hw.Parse(new System.IO.StringReader(str));
                        this.AddBlankParagraphLowHeight(4);
                        this.AddParagraph("Governor’s Leadership Foundation (GLF) Program", 0, FontFactory.GetFont("Helvetica", 14f, 1, this.hcolor));
                        str = "<p style='font-size:10px;font-face:Arial'><p>A prestigious annual leadership program run by the&nbsp;<span style='text-decoration: underline;'><span style='color: #0000ff; text-decoration: underline;'><a style='color: #0000ff; text-decoration: underline;' href='http://www.leadersinstitute.com.au/programs/governors-leadership-foundation/index.html'>Leaders Institute of South Australia</a>.</span></span> For executives and managers. An intensive 10-month leadership program that&nbsp;stretches participants intellectually and personally and develops practical wisdom. Visit the&nbsp;<span style='text-decoration: underline;'><span style='color: #0000ff;'><a style='color: #0000ff; text-decoration: underline;' href='https://publicsector.sa.gov.au/people/leadership-development/governors-leadership-foundation-program-2018/'>Office of the Public Sector</a></span></span>&nbsp;&nbsp;for GLF scholarship opportunities.</p></p>";
                        hw.Parse(new System.IO.StringReader(str));
                        this.AddBlankParagraphLowHeight(4);
                        this.AddParagraph("Leadership in Action Program", 0, FontFactory.GetFont("Helvetica", 14f, 1, this.hcolor));
                        this.AddBlankParagraphLowHeight(2);
                        this.AddParagraph("A key development program for our Procurement and Contract Management leaders.", 0, FontFactory.GetFont("Arial", 10f, 1, this.bcolor));
                        str = "<p style='font-size:10px;font-face:Arial'>This program will enhances procurement performance across SA Government in the way we lead and engage.&nbsp;These workshops are dedicated to opening pathways so we can perform at our peak. By joining together in these highly focused workshops we will leverage our collective strengths to create a landscape of positive change and deliver ongoing sustainable results to the business.</p>";
                        hw.Parse(new System.IO.StringReader(str));
                        this.AddBlankParagraphLowHeight(1);
                        str = "<p style='font-size:10px;font-face:Arial'><a href='https://academyofprocurement.com/'><u><strong>Read more</strong></u></a></p>";
                        hw.Parse(new System.IO.StringReader(str));
                        //end
                        //Start expert page
                        this.doc.SetMargins(55f, 55f, 25f, 25f);
                        this.doc.NewPage();
                        this.AddBlankParagraphLowHeight(4);
                        img = Image.GetInstance(this.imagepath + "/ExpertReport.jpg");
                        //img.SetAbsolutePosition(170f, 610f);
                        //img.ScaleToFit(222f, 108f);
                        //img.Alignment = 5;
                        this.doc.Add(img);
                        this.AddBlankParagraphLowHeight(4);
                        this.AddParagraph("You are an identified expert in one or more dimensions.", 1, FontFactory.GetFont("Helvetica", 14f, 1, this.hcolor));
                        this.AddBlankParagraphLowHeight(4);
                        this.AddParagraph("Are you contributing to your organisation by providing on-the-job coaching and sharing your knowledge so that colleagues have the opportunity to learn through experience?", 0, FontFactory.GetFont("Arial", 10f, 1, this.bcolor));
                        this.AddBlankParagraphLowHeight(2);
                        this.AddParagraph("Depending on your role, you could adopt one or more of the following approaches directly and/ or through your direct reports to improve learning throughout your procurement operation:", 0, FontFactory.GetFont("Arial", 10f, 0, this.bcolor));
                        this.AddBlankParagraphLowHeight(2);
                        str = "<p style='font-size:10px;font-face:Arial'><p>&bull; Encourage additional tasks, giving team members the opportunity to practice new skills and stretch beyond current competencies</p><p>&bull; Describe not only the task but expectations on how to perform it and 'success factors' to describe how one knows when the task is successfully completed</p><p>&bull; Accept that team members will make mistakes and view mistakes as learning opportunities</p><p>&bull; Provide timely and regular feedback</p><p>&bull; Provide space, time and opportunities for communities of skills sharing and to develop</p><p>&bull; Offer one-to-one coaching and mentoring</p><p>&bull; Help identify training for others given that the 10% training remains important since it solidifies theory behind the practice.</p></p>";
                        hw.Parse(new System.IO.StringReader(str));
                        this.AddBlankParagraphLowHeight(2);
                        str = "<p style='font-size:10px;font-face:Arial'>In the pages that follow you will find the same personal development plan structure that your peers receive. Your recommendations should be read through this lens - as an Expert you could be facilitating learning opportunities where you have specialist skills.&nbsp;</p>";
                        hw.Parse(new System.IO.StringReader(str));
                        //End
                    }
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

        private string GetLevelForInsertRecoomendation(decimal score)
        {
            string level = string.Empty;
            if (score <= 14.28M)
            {
                level = "Novice";
            }
            else if (score <= 28.57M)
            {
                level = "Awareness";
            }
            else if (score <= 57.14M)
            {
                level = "Understanding";
            }
            else if (score <= 92.66M)
            {
                level = "Practitioner";
            }
            else
            {
                level = "Expert";
            }
            return level;
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

