using DataTier;
using SGA.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CookComputing.XmlRpc;
using InfusionSoftDotNet;

namespace SGA.tna
{
    public partial class negotiation_profile_test : System.Web.UI.Page
    {
        protected int PageNumber
        {
            get
            {
                int result;
                if (this.ViewState["PageNumber"] != null)
                {
                    result = System.Convert.ToInt32(this.ViewState["PageNumber"]);
                }
                else
                {
                    result = 0;
                }
                return result;
            }
            set
            {
                this.ViewState["PageNumber"] = value;
            }
        }

        public int testId
        {
            get
            {
                return System.Convert.ToInt32(this.ViewState["testId"].ToString());
            }
            set
            {
                this.ViewState["testId"] = value;
            }
        }

        public int Number
        {
            get
            {
                int result;
                if (this.ViewState["number"] != null)
                {
                    result = System.Convert.ToInt32(this.ViewState["number"]);
                }
                else
                {
                    result = 0;
                }
                return result;
            }
            set
            {
                this.ViewState["number"] = value;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            SGACommon.AddPageTitle(this.Page, "Negotiation Profile  ", "");
            MasterPage mp = this.Page.Master;
            if (mp != null)
            {
                UserControl uc = (UserControl)mp.FindControl("header1");
                if (uc != null)
                {
                    Panel pnlTopMenu = (Panel)uc.FindControl("pnlTopMenu");
                    if (pnlTopMenu != null)
                    {
                        pnlTopMenu.Visible = false;
                    }
                }
            }
            SGACommon.IsTakeTest("viewNPTest");
            if (!base.IsPostBack)
            {
                this.testId = System.Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spInitalizeNPTest", new SqlParameter[]
				{
					new SqlParameter("@userId", SGACommon.LoginUserInfo.userId),
					new SqlParameter("@testDate", System.DateTime.UtcNow.ToString()),
					new SqlParameter("@sessionId", this.Session.SessionID)
				}));
                this.LoadAllTopics();
                this.Percentage();
                this.SetClass();
                base.ClientScript.RegisterStartupScript(this.Page.GetType(), "abc", "$(document).ready(function(){\r\nStyleRadio();\r\n});", true);
            }
        }

        private void LoadAllTopics()
        {
            this.rptrTopics.DataSource = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetNPTopics");
            this.rptrTopics.DataBind();
        }

        protected void rptrTopics_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                this.PageNumber = System.Convert.ToInt32(e.CommandArgument) - 1;
                switch (this.PageNumber)
                {
                    case 0:
                        this.Number = 0;
                        break;
                    case 1:
                        this.Number = 9;
                        break;
                    case 2:
                        this.Number = 18;
                        break;
                    case 3:
                        this.Number = 27;
                        break;
                    case 4:
                        this.Number = 36;
                        break;
                    case 5:
                        this.Number = 45;
                        break;
                    case 6:
                        this.Number = 54;
                        break;
                    case 7:
                        this.Number = 63;
                        break;
                }
                this.SetClass();
                this.setPrevButton();
                this.Percentage();
                base.ClientScript.RegisterStartupScript(this.Page.GetType(), "abc", "$(document).ready(function(){\r\nStyleRadio();\r\n});", true);
            }
        }

        protected void rptrTopics_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
        }

        protected void parentRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                this.Number++;
                Label lblNumber = (Label)e.Item.FindControl("lblNumber");
                if (lblNumber != null)
                {
                    if (this.Number <= 9)
                    {
                        lblNumber.Text = "0" + this.Number;
                    }
                    else
                    {
                        lblNumber.Text = this.Number.ToString();
                    }
                }
                int questionId = System.Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "questionid"));
                RadioButtonList rdb = (RadioButtonList)e.Item.FindControl("RadioButtonList1");
                string strQuery = string.Concat(new object[]
				{
					"SELECT isNull(optionId,'') from  tblNPOptions where optionId = (select replyId from tblNPQuestionReply where testId=",
					this.testId,
					" and questionId=",
					questionId,
					")"
				});
                object strOption = SqlHelper.ExecuteScalar(CommandType.Text, strQuery);
                if (strOption != null)
                {
                    rdb.Items.FindByValue(strOption.ToString()).Selected = true;
                }
            }
        }

        protected DataSet GetData(int qid)
        {
            return SqlHelper.ExecuteDataset(CommandType.Text, "select * from tblNPOptions where questionId=" + qid + "  order by frontOrderBy ");
        }

        private void SetClass()
        {
            for (int i = 0; i < this.rptrTopics.Items.Count; i++)
            {
                LinkButton lnkButton = (LinkButton)this.rptrTopics.Items[i].FindControl("lnkBtn");
                if (i == this.PageNumber)
                {
                    lnkButton.Style.Add("color", "#F79548");
                    this.lblTopic.Text = lnkButton.Text.Replace("<br />", " ");
                    this.BindTopicsQuestions(System.Convert.ToInt32(lnkButton.CommandArgument));
                    SqlParameter[] param = new SqlParameter[]
					{
						new SqlParameter("@topicId", System.Convert.ToInt32(lnkButton.CommandArgument))
					};
                    this.lblDescription.Text = SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetNPTopicDetail", param).ToString();
                }
                else
                {
                    lnkButton.Style.Add("color", "#FFFFFF");
                }
            }
        }

        private void BindTopicsQuestions(int topicId)
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetNPQuestions", new SqlParameter[]
			{
				new SqlParameter("@topicId", topicId)
			});
            this.parentRepeater.DataSource = ds;
            this.parentRepeater.DataBind();
            this.hdCount.Value = this.parentRepeater.Items.Count.ToString();
        }

        private void Percentage()
        {
            string strPercentage = "";
            switch (this.PageNumber)
            {
                case 0:
                    strPercentage = "You're 12.50% through, doing well!";
                    break;
                case 1:
                    strPercentage = "You're 25% through, doing well!";
                    break;
                case 2:
                    strPercentage = "You're 37.50% through, doing well!";
                    break;
                case 3:
                    strPercentage = "You're 50% through, doing well!";
                    break;
                case 4:
                    strPercentage = "You're 62.50% through, doing well!";
                    break;
                case 5:
                    strPercentage = "You're 75% through, doing well!";
                    break;
                case 6:
                    strPercentage = "You're 87.50% through, doing well!";
                    break;
                case 7:
                    strPercentage = "You're 100% through, congratulations!";
                    break;
            }
            this.lblPercentage.Text = strPercentage;
        }

        protected void lnkNext_Click(object sender, System.EventArgs e)
        {
        }

        protected void lnkPrev_Click(object sender, System.EventArgs e)
        {
            if (this.PageNumber != 0)
            {
                if (this.pgNumber.Value.Length > 0)
                {
                    this.PageNumber = System.Convert.ToInt32(this.pgNumber.Value);
                }
                else
                {
                    foreach (RepeaterItem itm in this.parentRepeater.Items)
                    {
                        RadioButtonList rdb = (RadioButtonList)itm.FindControl("RadioButtonList1");
                        if (rdb != null)
                        {
                            if (rdb.SelectedIndex != -1)
                            {
                                SqlParameter[] param = new SqlParameter[3];
                                string fff = rdb.SelectedValue.ToString();
                                Label qId = (Label)itm.FindControl("lblquestionId");
                                int questionId = System.Convert.ToInt32(qId.Text);
                                param[0] = new SqlParameter("@questionId", questionId);
                                param[1] = new SqlParameter("@OptionId", fff);
                                param[2] = new SqlParameter("@testId", this.testId);
                                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateNPOptions", param);
                            }
                        }
                    }
                    this.PageNumber--;
                }
                switch (this.PageNumber)
                {
                    case 0:
                        this.Number = 0;
                        break;
                    case 1:
                        this.Number = 9;
                        break;
                    case 2:
                        this.Number = 18;
                        break;
                    case 3:
                        this.Number = 27;
                        break;
                    case 4:
                        this.Number = 36;
                        break;
                    case 5:
                        this.Number = 45;
                        break;
                    case 6:
                        this.Number = 54;
                        break;
                    case 7:
                        this.Number = 63;
                        break;
                }
                this.SetClass();
                this.setPrevButton();
                this.Percentage();
                this.pgNumber.Value = "";
                base.ClientScript.RegisterStartupScript(this.Page.GetType(), "abc", "$(document).ready(function(){\r\nStyleRadio();\r\n});", true);
            }
        }

        protected void btnSubmit_Click(object sender, System.EventArgs e)
        {
        }

        public void setPrevButton()
        {
            this.lnkPrev.Visible = (this.PageNumber != 0);
            this.lnkPrev.Enabled = (this.PageNumber != 0);
            this.lnkNext.Visible = (this.PageNumber != 7);
            this.btnSubmit.Visible = (this.PageNumber == 7);
        }

        protected void btnFinal_Click(object sender, System.EventArgs e)
        {
            this.Session["npTestId"] = this.testId;
            foreach (RepeaterItem itm in this.parentRepeater.Items)
            {
                RadioButtonList rdb = (RadioButtonList)itm.FindControl("RadioButtonList1");
                if (rdb != null)
                {
                    if (rdb.SelectedIndex == -1)
                    {
                        return;
                    }
                    SqlParameter[] param = new SqlParameter[3];
                    string fff = rdb.SelectedValue.ToString();
                    Label qId = (Label)itm.FindControl("lblquestionId");
                    int questionId = System.Convert.ToInt32(qId.Text);
                    param[0] = new SqlParameter("@questionId", questionId);
                    param[1] = new SqlParameter("@OptionId", fff);
                    param[2] = new SqlParameter("@testId", this.testId);
                    SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateNPOptions", param);
                }
            }

            string[] strField = new string[]
						{
							"Id"
						};
            XmlRpcStruct[] resultFound = isdnAPI.findByEmail(SGACommon.LoginUserInfo.name, strField);
            int userId;
            XmlRpcStruct Contact = new XmlRpcStruct();
            if (resultFound.Length > 0)
            {
                userId = System.Convert.ToInt32(resultFound[0]["Id"].ToString());
                isdnAPI.addToGroup(userId, 1474);
                string Url = "http://" + base.Request.UrlReferrer.Host + "/Negotiation_Profile_Report.aspx?Id=" + DataTier.SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetSessionId", new SqlParameter[]{
                    new SqlParameter("@testId",this.testId),
                    new SqlParameter("@flag","5")
                }).ToString();
                Contact.Add("_NegotiationProfileTNALink", Url);
                Contact.Add("ContactType", "Customer");
                isdnAPI.dsUpdate("Contact", userId, Contact);
            }

            this.testId = 0;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spRestrictTest", new SqlParameter[]
			{
				new SqlParameter("@flag", "4"),
				new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
			});

            base.Response.Redirect("~/tna/my-results-bar-graph-np.aspx", false);
        }

        protected void btnSubmitNext_Click(object sender, System.EventArgs e)
        {
            foreach (RepeaterItem itm in this.parentRepeater.Items)
            {
                RadioButtonList rdb = (RadioButtonList)itm.FindControl("RadioButtonList1");
                if (rdb != null)
                {
                    if (rdb.SelectedIndex == -1)
                    {
                        return;
                    }
                    SqlParameter[] param = new SqlParameter[3];
                    string fff = rdb.SelectedValue.ToString();
                    Label qId = (Label)itm.FindControl("lblquestionId");
                    int questionId = System.Convert.ToInt32(qId.Text);
                    param[0] = new SqlParameter("@questionId", questionId);
                    param[1] = new SqlParameter("@OptionId", fff);
                    param[2] = new SqlParameter("@testId", this.testId);
                    SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateNPOptions", param);
                }
            }
            if (this.pgNumber.Value.Length > 0)
            {
                this.PageNumber = System.Convert.ToInt32(this.pgNumber.Value);
            }
            else
            {
                this.PageNumber++;
            }
            this.SetClass();
            this.setPrevButton();
            this.Percentage();
            this.pgNumber.Value = "";
            base.ClientScript.RegisterStartupScript(this.Page.GetType(), "abc", "$(document).ready(function(){\r\nStyleRadio();\r\n});", true);
        }
    }
}