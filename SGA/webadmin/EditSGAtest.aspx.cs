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

namespace SGA.webadmin
{
    public partial class EditSGAtest : System.Web.UI.Page
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
          
            if (!base.IsPostBack)
            {
                this.testId = System.Convert.ToInt32(base.Request.QueryString["Id"].ToString());
                this.LoadAllTopics();
                this.Percentage();
                this.SetClass();
                base.ClientScript.RegisterStartupScript(this.Page.GetType(), "abc", "$(document).ready(function(){\r\nStyleRadio();\r\n});", true);
            }
        }

        private void LoadAllTopics()
        {
            this.rptrTopics.DataSource = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSgaTopics");
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
                    "SELECT isNull(optionId,'') from  tblSgaOptions where optionId = (select optionId from tblSgaQuestionReply where testId=",
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
            return SqlHelper.ExecuteDataset(CommandType.Text, "select * from tblSgaOptions where questionId=" + qid + " ");
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
                }
                else
                {
                    lnkButton.Style.Add("color", "#FFFFFF");
                }
            }
        }

        private void BindTopicsQuestions(int topicId)
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSgaQuestions", new SqlParameter[]
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
                                SqlParameter[] param = new SqlParameter[4];
                                string fff = rdb.SelectedValue.ToString();
                                Label qId = (Label)itm.FindControl("lblquestionId");
                                int questionId = System.Convert.ToInt32(qId.Text);
                                param[0] = new SqlParameter("@questionId", questionId);
                                param[1] = new SqlParameter("@OptionId", fff);
                                param[2] = new SqlParameter("@testId", this.testId);
                                param[3] = new SqlParameter("@endDate", System.DateTime.UtcNow.ToString());
                                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateOptions", param);
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
            this.Session["sgaTestId"] = this.testId;
            foreach (RepeaterItem itm in this.parentRepeater.Items)
            {
                RadioButtonList rdb = (RadioButtonList)itm.FindControl("RadioButtonList1");
                if (rdb != null)
                {
                    if (rdb.SelectedIndex == -1)
                    {
                        return;
                    }
                    SqlParameter[] param = new SqlParameter[4];
                    string fff = rdb.SelectedValue.ToString();
                    Label qId = (Label)itm.FindControl("lblquestionId");
                    int questionId = System.Convert.ToInt32(qId.Text);
                    param[0] = new SqlParameter("@questionId", questionId);
                    param[1] = new SqlParameter("@OptionId", fff);
                    param[2] = new SqlParameter("@testId", this.testId);
                    param[3] = new SqlParameter("@endDate", System.DateTime.UtcNow.ToString());
                    SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateOptions", param);
                }
            }
            this.testId = 0;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spRestrictTest", new SqlParameter[]
            {
                new SqlParameter("@flag", "0"),
                new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
            });
            base.Response.Redirect("~/webadmin/ListUser.aspx", false);
        }

       
        public void setPrevButton()
        {
            this.lnkPrev.Visible = (this.PageNumber != 0);
            this.lnkPrev.Enabled = (this.PageNumber != 0);
            this.lnkNext.Visible = (this.PageNumber != 7);
            this.btnNextNoForward.Visible = (this.PageNumber == 7);
        }

        protected void btnFinal_Click(object sender, System.EventArgs e)
        {
            this.Session["sgaTestId"] = this.testId;
            foreach (RepeaterItem itm in this.parentRepeater.Items)
            {
                RadioButtonList rdb = (RadioButtonList)itm.FindControl("RadioButtonList1");
                if (rdb != null)
                {
                    if (rdb.SelectedIndex == -1)
                    {
                        return;
                    }
                    SqlParameter[] param = new SqlParameter[4];
                    string fff = rdb.SelectedValue.ToString();
                    Label qId = (Label)itm.FindControl("lblquestionId");
                    int questionId = System.Convert.ToInt32(qId.Text);
                    param[0] = new SqlParameter("@questionId", questionId);
                    param[1] = new SqlParameter("@OptionId", fff);
                    param[2] = new SqlParameter("@testId", this.testId);
                    param[3] = new SqlParameter("@endDate", System.DateTime.UtcNow.ToString());
                    SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateOptions", param);
                }
            }
            string[] strField = new string[]
                        {
                            "Id"
                        };
            //XmlRpcStruct[] resultFound = isdnAPI.findByEmail(SGACommon.LoginUserInfo.name, strField);
            //int userId;
            //XmlRpcStruct Contact = new XmlRpcStruct();
            //if (resultFound.Length > 0)
            //{
            //    userId = System.Convert.ToInt32(resultFound[0]["Id"].ToString());
            //    isdnAPI.addToGroup(userId, 1466);
            //    string Url = "http://" + base.Request.UrlReferrer.Host + "/Category_Management_Challenge_Report.aspx?Id=" + DataTier.SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetSessionId", new SqlParameter[]{
            //        new SqlParameter("@testId",this.testId),
            //        new SqlParameter("@flag","1")
            //    }).ToString();
            //    Contact.Add("_CMCReportURL", Url);
            //    Contact.Add("ContactType", "Customer");
            //    isdnAPI.dsUpdate("Contact", userId, Contact);
            //}

            this.testId = 0;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spRestrictTest", new SqlParameter[]
            {
                new SqlParameter("@flag", "0"),
                new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
            });
            base.Response.Redirect("~/webadmin/ListUser.aspx", false);
        }

        protected void btnNextNoForward_Click(object sender, System.EventArgs e)
        {
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
                    SqlParameter[] param = new SqlParameter[4];
                    string fff = rdb.SelectedValue.ToString();
                    Label qId = (Label)itm.FindControl("lblquestionId");
                    int questionId = System.Convert.ToInt32(qId.Text);
                    param[0] = new SqlParameter("@questionId", questionId);
                    param[1] = new SqlParameter("@OptionId", fff);
                    param[2] = new SqlParameter("@testId", this.testId);
                    param[3] = new SqlParameter("@endDate", System.DateTime.UtcNow.ToString());
                    SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateOptions", param);
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