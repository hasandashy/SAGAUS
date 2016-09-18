using DataTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.controls
{
    public partial class ctrlCAAGraph : System.Web.UI.UserControl
    {
        protected decimal topic1mark = 0m;

        protected decimal topic2mark = 0m;

        protected decimal topic3mark = 0m;

        protected decimal topic4mark = 0m;

        protected decimal topic5mark = 0m;

     

        protected string topic1name = "";

        protected string topic2name = "";

        protected string topic3name = "";

        protected string topic4name = "";

        protected string topic5name = "";

      
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

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!base.IsPostBack)
            {
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetCAAGraph", new SqlParameter[]
				{
					new SqlParameter("@testId", this.testId)
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    this.topic1mark = System.Convert.ToDecimal(ds.Tables[0].Rows[i]["percentage"].ToString());
                                    this.topic1name = ds.Tables[0].Rows[i]["topicTitle"].ToString().Replace("<br />", " ");
                                    break;
                                case 1:
                                    this.topic2mark = System.Convert.ToDecimal(ds.Tables[0].Rows[i]["percentage"].ToString());
                                    this.topic2name = ds.Tables[0].Rows[i]["topicTitle"].ToString().Replace("<br />", " ");
                                    break;
                                case 2:
                                    this.topic3mark = System.Convert.ToDecimal(ds.Tables[0].Rows[i]["percentage"].ToString());
                                    this.topic3name = ds.Tables[0].Rows[i]["topicTitle"].ToString().Replace("<br />", " ");
                                    break;
                                case 3:
                                    this.topic4mark = System.Convert.ToDecimal(ds.Tables[0].Rows[i]["percentage"].ToString());
                                    this.topic4name = ds.Tables[0].Rows[i]["topicTitle"].ToString().Replace("<br />", " ");
                                    break;
                                case 4:
                                    this.topic5mark = System.Convert.ToDecimal(ds.Tables[0].Rows[i]["percentage"].ToString());
                                    this.topic5name = ds.Tables[0].Rows[i]["topicTitle"].ToString().Replace("<br />", " ");
                                    break;
                              
                            }
                        }
                    }
                }
            }
        }
    }
}