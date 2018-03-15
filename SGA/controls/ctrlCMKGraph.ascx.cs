using DataTier;
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
    public partial class ctrlCMKGraph : System.Web.UI.UserControl
    {
        protected decimal topic1mark = 0m;

        protected decimal topic2mark = 0m;

        protected decimal topic3mark = 0m;

        protected decimal topic4mark = 0m;

        protected decimal topic5mark = 0m;

        protected decimal topic6mark = 0m;

        protected decimal topic7mark = 0m;

        protected decimal topic8mark = 0m;

        protected string topic1name = "";

        protected string topic2name = "";

        protected string topic3name = "";

        protected string topic4name = "";

        protected string topic5name = "";

        protected string topic6name = "";

        protected string topic7name = "";

        protected string topic8name = "";

        private int _testId;

        public int testId
        {
            get
            {
                return this._testId;
            }
            set
            {
                this._testId = value;
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

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!base.IsPostBack)
            {
                //-------------------------------------------------------------------------------------
                DataSet dsSummary = new DataSet();

                Dictionary<int, decimal> dict = new Dictionary<int, decimal>();

                string procName = "spGetCMAGraph";
                string topicTitle = string.Empty;
                decimal scaledMarks = 0.00M;
                DataSet dsTest = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetTestIdByUser", new SqlParameter[]
                    {
                        new SqlParameter("@userId", userId),
                        new SqlParameter("@initYear",ConfigurationManager.AppSettings["initYear"].ToString())
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
                                            scaledMarks = GetPercentage(Convert.ToDecimal(dsSummary.Tables[0].Rows[i]["percentage"]));
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




                      
                    }
                }
                //-------------------------------------------------------------------------------------
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetCMKGraph", new SqlParameter[]
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
                                    this.topic1name = ds.Tables[0].Rows[i]["topicTitle"].ToString().Replace("<br />", " ");
                                    break;
                                case 1:
                                    this.topic2mark = Math.Truncate(Convert.ToDecimal(dict.Where(k => k.Key == 1).Select(m => m.Value).First()) * 100) / 100;
                                    this.topic2name = ds.Tables[0].Rows[i]["topicTitle"].ToString().Replace("<br />", " ");
                                    break;
                                case 2:
                                    this.topic3mark = Math.Truncate(Convert.ToDecimal(dict.Where(k => k.Key == 2).Select(m => m.Value).First()) * 100) / 100;
                                    this.topic3name = ds.Tables[0].Rows[i]["topicTitle"].ToString().Replace("<br />", " ");
                                    break;
                                case 3:
                                    this.topic4mark = Math.Truncate(Convert.ToDecimal(dict.Where(k => k.Key == 3).Select(m => m.Value).First()) * 100) / 100;
                                    this.topic4name = ds.Tables[0].Rows[i]["topicTitle"].ToString().Replace("<br />", " ");
                                    break;
                                case 4:
                                    this.topic5mark = Math.Truncate(Convert.ToDecimal(dict.Where(k => k.Key == 4).Select(m => m.Value).First()) * 100) / 100;
                                    this.topic5name = ds.Tables[0].Rows[i]["topicTitle"].ToString().Replace("<br />", " ");
                                    break;
                                case 5:
                                    this.topic6mark = Math.Truncate(Convert.ToDecimal(dict.Where(k => k.Key == 5).Select(m => m.Value).First()) * 100) / 100;
                                    this.topic6name = ds.Tables[0].Rows[i]["topicTitle"].ToString().Replace("<br />", " ");
                                    break;
                                case 6:
                                    this.topic7mark = Math.Truncate(Convert.ToDecimal(dict.Where(k => k.Key == 6).Select(m => m.Value).First()) * 100) / 100;
                                    this.topic7name = ds.Tables[0].Rows[i]["topicTitle"].ToString().Replace("<br />", " ");
                                    break;
                                case 7:
                                    this.topic8mark = Math.Truncate(Convert.ToDecimal(dict.Where(k => k.Key == 7).Select(m => m.Value).First()) * 100) / 100;
                                    this.topic8name = ds.Tables[0].Rows[i]["topicTitle"].ToString().Replace("<br />", " ");
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private decimal GetPercentage(decimal score)
        {
            score = Convert.ToDecimal(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "getScaledDownPercentage", new SqlParameter[]
                                 {
                        new SqlParameter("@score", score),
                        new SqlParameter("@flag",2)
                                 }));
            return score;
        }
    }
}