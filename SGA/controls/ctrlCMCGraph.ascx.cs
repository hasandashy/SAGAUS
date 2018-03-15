using DataTier;
using SGA.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.controls
{
    public partial class ctrlCMCGraph : System.Web.UI.UserControl
    {
        

        private int _showCompare;

        protected decimal topic1mark = 0m;

        protected decimal topic2mark = 0m;

        protected decimal topic3mark = 0m;

        protected decimal topic4mark = 0m;

        protected decimal topic5mark = 0m;

        protected decimal topic6mark = 0m;

        protected decimal topic7mark = 0m;

        protected decimal topic8mark = 0m;

        protected double medain1 = 0.0;

        protected double medain2 = 0.0;

        protected double medain3 = 0.0;

        protected double medain4 = 0.0;

        protected double medain5 = 0.0;

        protected double medain6 = 0.0;

        protected double medain7 = 0.0;

        protected double medain8 = 0.0;

        protected string topic1name = "";

        protected string topic2name = "";

        protected string topic3name = "";

        protected string topic4name = "";

        protected string topic5name = "";

        protected string topic6name = "";

        protected string topic7name = "";

        protected string topic8name = "";

        protected string median = "";

        public int testId
        {
            get
            {
                return (this.ViewState["testId"] == null) ? 0 : System.Convert.ToInt32(this.ViewState["testId"].ToString());
            }
            set
            {
                this.ViewState["testId"] = value;
            }
        }

        private int _userId;

        public int userId
        {
            get
            {
                return this._userId;
            }
            set
            {
                this._userId = value;
            }
        }

        public int showCompare
        {
            get
            {
                return this._showCompare;
            }
            set
            {
                this._showCompare = value;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!base.IsPostBack)
            {
                tblCompare.Visible = (this.showCompare == 1);
                BindGraph();
                Page.ClientScript.RegisterStartupScript(base.GetType(), "CallMyFunction", "drawChart()", true);
            }
        }

        private void BindGraph()
        {
            Dictionary<int, decimal> dict = new Dictionary<int, decimal>();
            string procName = "spGetSsaGraph";
            string topicTitle = string.Empty;
            decimal scaledMarks = 0.00M;
            DataSet dsTest = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetTestIdByUser", new SqlParameter[]
            {
                        new SqlParameter("@userId", SGACommon.LoginUserInfo.userId),
                        new SqlParameter("@initYear",ConfigurationManager.AppSettings["initYear"].ToString())
            });
            if (dsTest != null)
            {
                if (dsTest.Tables.Count > 0 && dsTest.Tables[0].Rows.Count > 0)
                {
                    for (int j = 0; j < dsTest.Tables[0].Rows.Count; j++)
                    {
                        if (dsTest.Tables[0].Rows[j]["testType"].ToString() == "2")
                        {
                            procName = "spGetSgaGraph";
                        }
                        else
                        {
                            procName = "spGetSsaGraph";
                        }
                        DataSet dsSummary = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, procName, new SqlParameter[]
               {
                        new SqlParameter("@testId", dsTest.Tables[0].Rows[j]["testId"].ToString())
               });
                        if (dsSummary != null)
                        {
                            if (dsSummary.Tables.Count > 0 && dsSummary.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < dsSummary.Tables[0].Rows.Count; i++)
                                {
                                    if (procName == "spGetSgaGraph")
                                    {
                                        scaledMarks = GetPercentage(Convert.ToDecimal(dsSummary.Tables[0].Rows[i]["percentage"]));
                                    }
                                    else
                                    {
                                        scaledMarks = Convert.ToDecimal(dsSummary.Tables[0].Rows[i]["percentage"]);
                                    }
                                    if (dict.ContainsKey(i))
                                    {
                                        //decimal Avgpercentage = (dict[i] + Convert.ToDecimal(dsSummary.Tables[0].Rows[i]["percentage"])) / 2;
                                        decimal Avgpercentage =  (dict[i] + scaledMarks) / 2;
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
                }
            }
                    //--------------------------------------------------
                    DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSgaGraph", new SqlParameter[]
			{
				new SqlParameter("@testId", this.testId)
			});
            if (ds != null && dict.Count > 0)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                this.topic1mark = Math.Truncate(Convert.ToDecimal(dict.Where(k => k.Key == 0).Select(m => m.Value).First()) * 100) / 100;
                                this.topic1name = ds.Tables[0].Rows[i]["topicName"].ToString().Replace("<br />", " ");
                                break;
                            case 1:
                                this.topic2mark = Math.Truncate(Convert.ToDecimal(dict.Where(k => k.Key == 1).Select(m => m.Value).First()) * 100) / 100;
                                this.topic2name = ds.Tables[0].Rows[i]["topicName"].ToString().Replace("<br />", " ");
                                break;
                            case 2:
                                this.topic3mark = Math.Truncate(Convert.ToDecimal(dict.Where(k => k.Key == 2).Select(m => m.Value).First()) * 100) / 100;
                                this.topic3name = ds.Tables[0].Rows[i]["topicName"].ToString().Replace("<br />", " ");
                                break;
                            case 3:
                                this.topic4mark = Math.Truncate(Convert.ToDecimal(dict.Where(k => k.Key == 3).Select(m => m.Value).First()) * 100) / 100;
                                this.topic4name = ds.Tables[0].Rows[i]["topicName"].ToString().Replace("<br />", " ");
                                break;
                            case 4:
                                this.topic5mark = Math.Truncate(Convert.ToDecimal(dict.Where(k => k.Key == 4).Select(m => m.Value).First()) * 100) / 100;
                                this.topic5name = ds.Tables[0].Rows[i]["topicName"].ToString().Replace("<br />", " ");
                                break;
                            case 5:
                                this.topic6mark = Math.Truncate(Convert.ToDecimal(dict.Where(k => k.Key == 5).Select(m => m.Value).First()) * 100) / 100;
                                this.topic6name = ds.Tables[0].Rows[i]["topicName"].ToString().Replace("<br />", " ");
                                break;
                            case 6:
                                this.topic7mark = Math.Truncate(Convert.ToDecimal(dict.Where(k => k.Key == 6).Select(m => m.Value).First()) * 100) / 100;
                                this.topic7name = ds.Tables[0].Rows[i]["topicName"].ToString().Replace("<br />", " ");
                                break;
                            case 7:
                                this.topic8mark = Math.Truncate(Convert.ToDecimal(dict.Where(k => k.Key == 7).Select(m => m.Value).First()) * 100) / 100;
                                this.topic8name = ds.Tables[0].Rows[i]["topicName"].ToString().Replace("<br />", " ");
                                break;
                        }
                    }
                }
            }
        }

        protected void lnkCompare_Click(object sender, System.EventArgs e)
        {
            int selectedValue = System.Convert.ToInt32(rdlQuartile.SelectedValue);
            this.CompareResult(selectedValue);
        }

        public void CompareResult(int selectedValue)
        {
            if (selectedValue == 1)
            {
                this.median = "Lower Quartile";
            }
            else if (selectedValue == 2)
            {
                this.median = "Median";
            }
            else if (selectedValue == 3)
            {
                this.median = "Upper Quartile";
            }
            else if (selectedValue == 4)
            {
                this.median = "Average";
            }
            this.BindGraph();
            string[] selectedColumns = new string[]
			{
				"marks"
			};
            for (int i = 0; i < 8; i++)
            {
                DataSet dsMarks = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetPercentageByTopics", new SqlParameter[]
				{
					new SqlParameter("@topicId", i + 1)
				});
                if (dsMarks != null)
                {
                    if (dsMarks.Tables.Count > 0 && dsMarks.Tables[0].Rows.Count > 0)
                    {
                        EnumerableRowCollection<DataRow> result = from r in dsMarks.Tables[0].AsEnumerable()
                                                                  select r;
                        DataTable dtResult = result.CopyToDataTable<DataRow>();
                        DataTable dt = new DataView(dtResult).ToTable(false, selectedColumns);
                        System.Collections.Generic.IEnumerable<double> list = ctrlCMCGraph.ConvertToDecimals(dt);
                        switch (i)
                        {
                            case 0:
                                switch (selectedValue)
                                {
                                    case 1:
                                        this.medain1 = Quartiles.FirstQuartile(from m in list
                                                                               orderby m
                                                                               select m);
                                        break;
                                    case 2:
                                        this.medain1 = Quartiles.MiddleQuartile(from m in list
                                                                                orderby m
                                                                                select m);
                                        break;
                                    case 3:
                                        this.medain1 = Quartiles.ThirdQuartile(from m in list
                                                                               orderby m
                                                                               select m);
                                        break;
                                    case 4:
                                        this.medain1 = System.Math.Round(list.Average(), 2);
                                        break;
                                }
                                break;
                            case 1:
                                switch (selectedValue)
                                {
                                    case 1:
                                        this.medain2 = Quartiles.FirstQuartile(from m in list
                                                                               orderby m
                                                                               select m);
                                        break;
                                    case 2:
                                        this.medain2 = Quartiles.MiddleQuartile(from m in list
                                                                                orderby m
                                                                                select m);
                                        break;
                                    case 3:
                                        this.medain2 = Quartiles.ThirdQuartile(from m in list
                                                                               orderby m
                                                                               select m);
                                        break;
                                    case 4:
                                        this.medain2 = System.Math.Round(list.Average(), 2);
                                        break;
                                }
                                break;
                            case 2:
                                switch (selectedValue)
                                {
                                    case 1:
                                        this.medain3 = Quartiles.FirstQuartile(from m in list
                                                                               orderby m
                                                                               select m);
                                        break;
                                    case 2:
                                        this.medain3 = Quartiles.MiddleQuartile(from m in list
                                                                                orderby m
                                                                                select m);
                                        break;
                                    case 3:
                                        this.medain3 = Quartiles.ThirdQuartile(from m in list
                                                                               orderby m
                                                                               select m);
                                        break;
                                    case 4:
                                        this.medain3 = System.Math.Round(list.Average(), 2);
                                        break;
                                }
                                break;
                            case 3:
                                switch (selectedValue)
                                {
                                    case 1:
                                        this.medain4 = Quartiles.FirstQuartile(from m in list
                                                                               orderby m
                                                                               select m);
                                        break;
                                    case 2:
                                        this.medain4 = Quartiles.MiddleQuartile(from m in list
                                                                                orderby m
                                                                                select m);
                                        break;
                                    case 3:
                                        this.medain4 = Quartiles.ThirdQuartile(from m in list
                                                                               orderby m
                                                                               select m);
                                        break;
                                    case 4:
                                        this.medain4 = System.Math.Round(list.Average(), 2);
                                        break;
                                }
                                break;
                            case 4:
                                switch (selectedValue)
                                {
                                    case 1:
                                        this.medain5 = Quartiles.FirstQuartile(from m in list
                                                                               orderby m
                                                                               select m);
                                        break;
                                    case 2:
                                        this.medain5 = Quartiles.MiddleQuartile(from m in list
                                                                                orderby m
                                                                                select m);
                                        break;
                                    case 3:
                                        this.medain5 = Quartiles.ThirdQuartile(from m in list
                                                                               orderby m
                                                                               select m);
                                        break;
                                    case 4:
                                        this.medain5 = System.Math.Round(list.Average(), 2);
                                        break;
                                }
                                break;
                            case 5:
                                switch (selectedValue)
                                {
                                    case 1:
                                        this.medain6 = Quartiles.FirstQuartile(from m in list
                                                                               orderby m
                                                                               select m);
                                        break;
                                    case 2:
                                        this.medain6 = Quartiles.MiddleQuartile(from m in list
                                                                                orderby m
                                                                                select m);
                                        break;
                                    case 3:
                                        this.medain6 = Quartiles.ThirdQuartile(from m in list
                                                                               orderby m
                                                                               select m);
                                        break;
                                    case 4:
                                        this.medain6 = System.Math.Round(list.Average(), 2);
                                        break;
                                }
                                break;
                            case 6:
                                switch (selectedValue)
                                {
                                    case 1:
                                        this.medain7 = Quartiles.FirstQuartile(from m in list
                                                                               orderby m
                                                                               select m);
                                        break;
                                    case 2:
                                        this.medain7 = Quartiles.MiddleQuartile(from m in list
                                                                                orderby m
                                                                                select m);
                                        break;
                                    case 3:
                                        this.medain7 = Quartiles.ThirdQuartile(from m in list
                                                                               orderby m
                                                                               select m);
                                        break;
                                    case 4:
                                        this.medain7 = System.Math.Round(list.Average(), 2);
                                        break;
                                }
                                break;
                            case 7:
                                switch (selectedValue)
                                {
                                    case 1:
                                        this.medain8 = Quartiles.FirstQuartile(from m in list
                                                                               orderby m
                                                                               select m);
                                        break;
                                    case 2:
                                        this.medain8 = Quartiles.MiddleQuartile(from m in list
                                                                                orderby m
                                                                                select m);
                                        break;
                                    case 3:
                                        this.medain8 = Quartiles.ThirdQuartile(from m in list
                                                                               orderby m
                                                                               select m);
                                        break;
                                    case 4:
                                        this.medain8 = System.Math.Round(list.Average(), 2);
                                        break;
                                }
                                break;
                        }
                    }
                }
            }
            this.Page.ClientScript.RegisterStartupScript(base.GetType(), "CallFunction", "drawChartAverage()", true);
        }

        private static System.Collections.Generic.IEnumerable<double> ConvertToDecimals(DataTable dataTable)
        {
            return from row in dataTable.AsEnumerable()
                   select System.Convert.ToDouble(row["Marks"]);
        }

        private decimal GetPercentage(decimal score)
        {
            score = Convert.ToDecimal(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "getScaledDownPercentage", new SqlParameter[]
                                 {
                        new SqlParameter("@score", score),
                        new SqlParameter("@flag",1)
                                 }));
            return score;
        }
    }
}