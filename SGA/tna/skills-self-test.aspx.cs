using CookComputing.XmlRpc;
using DataTier;
using InfusionSoftDotNet;
using SGA.App_Code;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.tna
{
    public partial class skills_self_test : Page
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

        public DataSet dsOptions
        {
            get
            {
                return (DataSet)this.ViewState["dsOptions"];
            }
            set
            {
                this.ViewState["dsOptions"] = value;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            SGACommon.AddPageTitle(this.Page, "Procurement Skills Self Assessment ", "");
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
            SGACommon.IsTakeTest("viewTnaTest");
            if (!base.IsPostBack)
            {
                this.testId = System.Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spInitalizeSsaTest", new SqlParameter[]
				{
					new SqlParameter("@userId", SGACommon.LoginUserInfo.userId),
					new SqlParameter("@testDate", System.DateTime.UtcNow.ToString()),
					new SqlParameter("@sessionId", this.Session.SessionID),
                    new SqlParameter("@initYear", ConfigurationManager.AppSettings["initYear"].ToString())

                }));
                this.dsOptions = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSSAOptions");
                this.LoadAllTopics();
                this.SetClass();
                this.setPrevButton();
                base.ClientScript.RegisterStartupScript(this.Page.GetType(), "abc", "$(document).ready(function(){\r\nStyleRadio();\r\n});", true);
            }
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
                    SqlParameter[] param = new SqlParameter[]
					{
						new SqlParameter("@topicId", System.Convert.ToInt32(lnkButton.CommandArgument))
					};
                    this.lblDescription.Text = SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetSSATopicDetail", param).ToString();
                    this.BindTopicsQuestions(System.Convert.ToInt32(lnkButton.CommandArgument));
                }
                else
                {
                    lnkButton.Style.Add("color", "#FFFFFF");
                }
            }
        }

        private void BindTopicsQuestions(int topicId)
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSsaQuestions", new SqlParameter[]
			{
				new SqlParameter("@topicId", topicId)
			});
            this.parentRepeater.DataSource = ds;
            this.parentRepeater.DataBind();
            this.hdCount.Value = this.parentRepeater.Items.Count.ToString();
        }

        private void LoadAllTopics()
        {
            this.rptrTopics.DataSource = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSSATopics");
            this.rptrTopics.DataBind();
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
                string hintText = DataBinder.Eval(e.Item.DataItem, "hintText").ToString();
                string hintWord = DataBinder.Eval(e.Item.DataItem, "hintWord").ToString();
                Label lblQuestionText = (Label)e.Item.FindControl("lblQuestionText");
                if (lblQuestionText != null)
                {
                    if (hintWord.Length > 0)
                    {
                        int pos = lblQuestionText.Text.IndexOf(hintWord);
                        Regex regex = new Regex(hintWord);
                        lblQuestionText.Text = regex.Replace(lblQuestionText.Text, " ", 1);
                        lblQuestionText.Text = lblQuestionText.Text.Insert(pos, string.Concat(new string[]
						{
							"<a href=\"javascript:void('0')\" class=\"tip-info\">",
							hintWord,
							"<span>",
							hintText,
							"</span></a>"
						}));
                    }
                }
                int questionId = System.Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "questionid"));
                string strQuery = string.Concat(new object[]
				{
					"SELECT isNull(optionId,'') from  tblSsaOptions where optionId = (select optionId from tblSsaQuestionReply where testId=",
					this.testId,
					" and questionId=",
					questionId,
					")"
				});
                RadioButtonList rbtnlst = (RadioButtonList)e.Item.FindControl("rbtnlst");
                if (rbtnlst != null)
                {
                    rbtnlst.DataSource = this.dsOptions;
                    rbtnlst.DataTextField = "optionText";
                    rbtnlst.DataValueField = "optionId";
                    rbtnlst.DataBind();
                    object strOption = SqlHelper.ExecuteScalar(CommandType.Text, strQuery);
                    if (strOption != null)
                    {
                        rbtnlst.Items.FindByValue(strOption.ToString()).Selected = true;
                    }
                }
            }
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
                base.ClientScript.RegisterStartupScript(this.Page.GetType(), "abc", "$(document).ready(function(){\r\nStyleRadio();\r\n});", true);
            }
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
                this.pgNumber.Value = "";
                base.ClientScript.RegisterStartupScript(this.Page.GetType(), "abc", "$(document).ready(function(){\r\nStyleRadio();\r\n});", true);
            }
        }

        public void setPrevButton()
        {
            this.lnkPrev.Visible = (this.PageNumber != 0);
            this.lnkPrev.Enabled = (this.PageNumber != 0);
            this.lnkNext.Visible = (this.PageNumber != 7);
            this.btnSubmit.Visible = (this.PageNumber == 7);
        }

        protected void btnSubmitNext_Click(object sender, System.EventArgs e)
        {
            foreach (RepeaterItem itm in this.parentRepeater.Items)
            {
                RadioButtonList rdb = (RadioButtonList)itm.FindControl("rbtnlst");
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
                    SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateSSaOptions", param);
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
            this.pgNumber.Value = "";
            base.ClientScript.RegisterStartupScript(this.Page.GetType(), "abc", "$(document).ready(function(){\r\nStyleRadio();\r\n});", true);
        }

        protected void btnFinal_Click(object sender, System.EventArgs e)
        {
            this.Session["ssaTestId"] = this.testId;
            string strLink = "";
            foreach (RepeaterItem itm in this.parentRepeater.Items)
            {
                RadioButtonList rdb = (RadioButtonList)itm.FindControl("rbtnlst");
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
                    SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateSSaOptions", param);
                    strLink = SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetTestEmailLink", new SqlParameter[]
					{
						new SqlParameter("@testId", this.testId),
						new SqlParameter("@flag", "0")
					}).ToString();
                }
            }
            string[] strField = new string[]
			{
				"Id"
			};
    //        XmlRpcStruct[] resultFound = isdnAPI.findByEmail(SGACommon.LoginUserInfo.name, strField);
    //        if (resultFound.Length > 0)
    //        {
    //            int Id = System.Convert.ToInt32(resultFound[0]["Id"]);
    //            bool isAdded = isdnAPI.addToGroup(Id, 402);
    //            isdnAPI.dsUpdate("Contact", Id, new XmlRpcStruct
				//{
				//	{
				//		"_ProcurementTNALink",
				//		"http://" + base.Request.UrlReferrer.Host + "/ProcurementReport.aspx?Id=" + strLink
				//	}
				//});
    //        }
            this.testId = 0;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spRestrictTest", new SqlParameter[]
			{
				new SqlParameter("@flag", "1"),
				new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
			});
            base.Response.Redirect("~/tna/my-results-bar-graph-ssa.aspx", false);
        }
    }
}
