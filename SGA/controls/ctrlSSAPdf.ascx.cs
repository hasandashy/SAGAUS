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
    public partial class ctrlSSAPdf : UserControl
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
                    SGACommon.GetName(userId),
                    "_",
                    packId,
                    "_procurementreport.pdf"
                }));
                DataSet dsPages = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageProcurementText", new SqlParameter[]
                {
                    new SqlParameter("@flag", 2)
                });
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@userId", SqlDbType.Int)
                };
                param[0].Value = userId;
                DataSet dsProfile = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetProfileDetails", param);
                System.DateTime dtEnd = System.Convert.ToDateTime(SqlHelper.ExecuteScalar(CommandType.Text, "select testDate from tbluserSsaTest where userId=" + userId));
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
                HTMLWorker hw = new HTMLWorker(this.doc);
                try
                {
                    this.doc.Open();
                    Image imgBG = Image.GetInstance(this.imagepath + "/mainbg.png");
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
                            cb.SetFontAndSize(f_cn, 12f);
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
                            cb.SetFontAndSize(f_cn, 12f);
                            cb.SetTextMatrix(80f, 180f);
                            cb.SetRGBColorFill(0, 0, 0);
                            cb.ShowText(SGACommon.ToAusTimeZone(dtEnd).ToString("dddd, dd MMMM yyyy"));
                            cb.EndText();
                            cb.BeginText();
                            cb.SetFontAndSize(f_cn, 12f);
                            cb.SetTextMatrix(80f, 160f);
                            cb.SetRGBColorFill(0, 0, 0);
                            cb.ShowText("Email : " + SGACommon.UppercaseFirst(dsProfile.Tables[0].Rows[0]["email"].ToString()));
                            cb.EndText();
                            cb.BeginText();
                            cb.SetFontAndSize(f_cn, 16f);
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
                            this.AddParagraph(dsPages.Tables[0].Rows[0]["page2Heading"].ToString(), 0, FontFactory.GetFont("Arial", 24f, 1, this.hcolor));
                            this.AddBlankParagraphLowHeight(1);
                            hw.Parse(new System.IO.StringReader(dsPages.Tables[0].Rows[0]["page2Text"].ToString()));
                        }
                    }
                    this.doc.SetMargins(55f, 55f, 55f, 55f);
                    this.doc.NewPage();
                    string str = "Your organisation and you";
                    this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 24f, 1, new BaseColor(0, 0, 0)));
                    this.AddBlankParagraphLowHeight(2);
                    this.AddParagraph("During your registration process at sagov.skillsgapanalysis.com you provided information to us. This information is displayed below for your reference. ", 0, FontFactory.GetFont("Arial", 10f, 0, new BaseColor(0, 0, 0)));
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
                    str = "2. Your assessment results";
                    this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 24f, 1, this.hcolor));
                    this.AddBlankParagraph(1);
                    str = "RESULTS TABLE 1 - PROCUREMENT";
                    this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 12f, 1, this.hcolor));
                    this.AddBlankParagraph(1);
                    str = "Your results for the Procurement assessment/s taken are displayed below by dimension and level.";
                    this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 10f, 1, new BaseColor(0, 0, 0)));
                    this.AddBlankParagraph(1);
                    table = this.GetTable(3);
                    float[] colwidth = new float[]
                    {
                        25f,
                        20f,
                        55f
                    };
                    table.SetWidths(colwidth);
                    this.AddrowHeader(ref table, "Dimension", "Result", " Level");
                    List<int> arr1 = new List<int>() { 1, 2, 3, 4 };
                    DataSet dsSummary = new DataSet();
                    if (arr1.Contains(packId))
                    {
                        testId = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetTestIdByUser", new SqlParameter[]
                        {
                        new SqlParameter("@userId", userId)
                        }));
                        dsSummary = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSsaGraph", new SqlParameter[]
                        {
                        new SqlParameter("@testId", testId)
                        });
                        if (dsSummary != null)
                        {
                            if (dsSummary.Tables.Count > 0 && dsSummary.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < dsSummary.Tables[0].Rows.Count; i++)
                                {
                                    string level = SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetLevelByPercentage", new SqlParameter[]
                        {
                        new SqlParameter("@percentage", dsSummary.Tables[0].Rows[i]["percentage"].ToString())
                        }).ToString();
                                    this.Addrow(ref table, " " + dsSummary.Tables[0].Rows[i]["topicTitle"].ToString().Replace("<br />", " "), dsSummary.Tables[0].Rows[i]["percentage"].ToString() + "%", "  " + level, false, false, true);
                                }
                            }
                        }
                    }
                    else
                    {
                        Dictionary<string, decimal> dict = new Dictionary<string, decimal>();
                                             
                        string procName = "spGetSsaGraph";
                        string topicTitle = string.Empty;
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
                                    if (dsTest.Tables[0].Rows[j]["testType"].ToString() == "2")
                                    { procName = "spGetSgaGraph"; }
                                    else
                                    {
                                        procName = "spGetSsaGraph";
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
                                                   if (procName == "spGetSsaGraph")
                                                {
                                                    topicTitle = dsSummary.Tables[0].Rows[i]["topicTitle"].ToString().Replace("<br />", " ");
                                                }
                                                   else
                                                {
                                                    topicTitle = dsSummary.Tables[0].Rows[i]["topicName"].ToString().Replace("<br />", " ");
                                                }
                                                    if (dict.ContainsKey(topicTitle))
                                                {
                                                    decimal Avgpercentage = (dict[topicTitle] + Convert.ToDecimal(dsSummary.Tables[0].Rows[i]["percentage"]))/2;
                                                    dict[topicTitle] = Avgpercentage;
                                                }
                                                else
                                                    dict.Add(topicTitle, Convert.ToDecimal(dsSummary.Tables[0].Rows[i]["percentage"]));


                                            }
                                           
                                        }
                                    }
                                }

                              
                             

                                foreach (KeyValuePair<String, decimal> kvp in dict)
                                {
                                    string level = SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetLevelByPercentage", new SqlParameter[]
                                {
                        new SqlParameter("@percentage", kvp.Value)
                                }).ToString();

                                    this.Addrow(ref table, " " + kvp.Key, kvp.Value.ToString("0.00") + "%", "  " + level, false, false, true);
                                }
                            }
                        }
                       
                    }
                    this.doc.Add(table);
                    //cb.BeginText();
                    //cb.SetFontAndSize(f_cn, 10f);
                    //cb.SetTextMatrix(55f, 320f);
                    //cb.SetRGBColorFill(0, 0, 0);
                    //cb.ShowText("Note");
                    //cb.EndText();
                    //cb.BeginText();
                    //cb.SetFontAndSize(f_cn, 10f);
                    //cb.SetTextMatrix(55f, 300f);
                    //cb.SetRGBColorFill(0, 0, 0);
                    //cb.ShowText("The level column describes the average rating received and aligns to the wording of the assessment scale");
                    //cb.EndText();
                    //cb.BeginText();
                    //cb.SetFontAndSize(f_cn, 10f);
                    //cb.SetTextMatrix(55f, 280f);
                    //cb.SetRGBColorFill(0, 0, 0);
                    //cb.ShowText("below");
                    //cb.EndText();
                    //img = Image.GetInstance(this.imagepath + "/scoringguide.jpg");
                    //img.ScaleToFit(800f, 116f);
                    //img.SetAbsolutePosition(0f, 150f);
                    //this.doc.Add(img);
                    this.AddBlankParagraph(1);
                    str = "RESULTS TABLE 2 - COMMERCIAL AWARENESS";
                    this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 12f, 1, this.hcolor));
                    this.AddBlankParagraph(1);
                    str = "Your results for the Commercial Awareness assessment, shown by dimension and level.";
                    this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 10f, 1, new BaseColor(0, 0, 0)));
                    this.AddBlankParagraph(1);
                    table = this.GetTable(3);
                    colwidth = new float[]
                    {
                        25f,
                        20f,
                        55f
                    };
                    table.SetWidths(colwidth);
                    this.AddrowHeader(ref table, "Dimension", "Result", " Level");
                    DataSet dsCAASummary = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetCaaGraph", new SqlParameter[]
                    {
                        new SqlParameter("@testId", 19)
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
                                this.Addrow(ref table, " " + dsCAASummary.Tables[0].Rows[i]["topicTitle"].ToString().Replace("<br />", " "), dsCAASummary.Tables[0].Rows[i]["percentage"].ToString() + "%", "  " +level, false, false, true);
                            }
                        }
                    }
                    this.doc.Add(table);
                    this.doc.SetMargins(55f, 55f, 55f, 55f);
                    this.doc.NewPage();
                    str = "3. Observations of your results";
                    this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 24f, 1, this.hcolor));
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
                                string level = SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetLevelByPercentage", new SqlParameter[]
                             {
                        new SqlParameter("@percentage", dsSummary.Tables[0].Rows[i - 1]["percentage"].ToString())
                             }).ToString();
                                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetReportRecommendation", new SqlParameter[]
                    {
                        new SqlParameter("@reportType", 1),
                        new SqlParameter("@level", level),
                        new SqlParameter("@topicId", i)

                    });
                                str = i.ToString() + ". " + dsSummary.Tables[0].Rows[i - 1]["topicTitle"].ToString().Replace("<br />", " ");
                                this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 12f, 1, this.hcolor));
                                this.AddBlankParagraph(1);
                                hw.Parse(new System.IO.StringReader(ds.Tables[0].Rows[0]["openingStatement"].ToString()));
                                this.AddBlankParagraph(1);
                                hw.Parse(new System.IO.StringReader(ds.Tables[0].Rows[0]["dynamicRecommendation"].ToString()));
                                this.AddBlankParagraph(3);
                            }
                        }
                    }
                    this.doc.SetMargins(0f, 0f, 0f, 0f);
                    this.doc.NewPage();
                    imgBG = Image.GetInstance(this.imagepath + "/sizzer_bg.jpg");
                    imgBG.ScaleToFit(this.doc.PageSize.Width, this.doc.PageSize.Height);
                    imgBG.Alignment = 8;
                    this.doc.Add(imgBG);
                    img = Image.GetInstance(this.imagepath + "/SaGovLogo.png");
                    img.ScaleToFit(110f, 110f);
                    img.Alignment = 4;
                    img.SetAbsolutePosition(230f, 400f);
                    this.doc.Add(img);
                    this.doc.SetMargins(55f, 55f, 55f, 55f);
                    this.doc.NewPage();
                    str = "Commercial Awareness";
                    this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 24f, 1, this.hcolor));
                    this.AddBlankParagraph(1);
                    str = "The Commercial Awareness Assessment is designed to diagnose commercial awareness across the Government of South Australia to deliver ‘value’ beyond traditional economic measures of value, such as price savings.";
                    this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 10f, 0, new BaseColor(0, 0, 0)));
                    this.AddBlankParagraph(1);
                    str = "The pursuit of value for money in procurement is hampered by the lack of a clear framework to define what is value? We may well be able to define what it is not, for example accepting the lowest priced offer, but we are not always able to define how we may recognise it if we achieved it?";
                    this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 10f, 0, new BaseColor(0, 0, 0)));
                    this.AddBlankParagraph(1);
                    str = "For organisation’s which do not aim to make a profit, or which have adopted a broader suite of objectives than simple economic measures of success, value needs to be defined in terms that are not simply measured in terms of dollars earned or in terms of customer satisfaction results.While most services are delivered in co - operation with other entities, such as suppliers, managers must be capable of engaging with  supply markets in ways that deliver value for the public.";
                    this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 10f, 0, new BaseColor(0, 0, 0)));
                    this.AddBlankParagraph(1);
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
                        new SqlParameter("@percentage", dsCAASummary.Tables[0].Rows[i - 1]["percentage"].ToString())
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
                                this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 12f, 1, this.hcolor));
                                this.AddBlankParagraph(1);
                                hw.Parse(new System.IO.StringReader(dsSuggestion.Tables[0].Rows[0]["SuggestionText"].ToString()));
                                this.AddBlankParagraph(1);
                                this.AddBoldParagraph("SUGGESTIONS:");
                                hw.Parse(new System.IO.StringReader(ds.Tables[0].Rows[0]["dynamicRecommendation"].ToString()));
                                this.AddBlankParagraph(3);
                            }
                        }
                    }

                    this.doc.SetMargins(55f, 55f, 55f, 55f);
                    this.doc.NewPage();
                    str = "Procurement based suggestions";
                    this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 24f, 1, this.hcolor));
                    this.AddBlankParagraph(1);
                    if (dsSummary != null)
                    {
                        if (dsSummary.Tables.Count > 0 && dsSummary.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 1; i <= dsSummary.Tables[0].Rows.Count; i++)
                            {
                                string level = SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetLevelByPercentage", new SqlParameter[]
                             {
                        new SqlParameter("@percentage", dsSummary.Tables[0].Rows[i - 1]["percentage"].ToString())
                             }).ToString();
                                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetReportRecommendation", new SqlParameter[]
                    {
                        new SqlParameter("@reportType", 4),
                        new SqlParameter("@level", level),
                        new SqlParameter("@topicId", i)

                    });
                                DataSet dsSuggestion = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSuggestionByTopic", new SqlParameter[]
                      {
                        new SqlParameter("@suggestionType", 1),
                        new SqlParameter("@topicId", i)

                      });
                                str = i.ToString() + ". " + dsSummary.Tables[0].Rows[i - 1]["topicTitle"].ToString().Replace("<br />", " ");
                                this.AddParagraph(str, 0, FontFactory.GetFont("Arial", 12f, 1, this.hcolor));
                                this.AddBlankParagraph(1);
                                hw.Parse(new System.IO.StringReader(dsSuggestion.Tables[0].Rows[0]["SuggestionText"].ToString()));
                                this.AddBlankParagraph(1);
                                this.AddBoldParagraph("SUGGESTIONS:");
                                this.AddBlankParagraph(1);
                                hw.Parse(new System.IO.StringReader(ds.Tables[0].Rows[0]["dynamicRecommendation"].ToString()));
                                this.AddBlankParagraph(3);
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
                            img = Image.GetInstance(this.imagepath + "/img3_grey_bg.jpg");
                            img.ScaleToFit(this.doc.PageSize.Width, this.doc.PageSize.Height);
                            img.Alignment = 8;
                            img.SetAbsolutePosition(1f, 3f);
                            this.doc.Add(img);
                            img = Image.GetInstance(this.imagepath + "/compraralogo.png");
                            img.SetAbsolutePosition(170f, 610f);
                            img.ScaleToFit(222f, 108f);
                            img.Alignment = 5;
                            this.doc.Add(img);
                            this.AddBlankParagraph(15);
                            //hw.Parse(new System.IO.StringReader(dsPages.Tables[0].Rows[0]["page19Address"].ToString()));
                            this.AddBlankParagraph(15);
                            // hw.Parse(new System.IO.StringReader(dsPages.Tables[0].Rows[0]["Disclaimer"].ToString()));
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
    }
}
