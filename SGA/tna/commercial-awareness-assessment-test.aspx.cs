﻿using CookComputing.XmlRpc;
using DataTier;
using InfusionSoftDotNet;
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

namespace SGA.tna
{
    public partial class commercial_awareness_assessment_test : System.Web.UI.Page
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
            SGACommon.AddPageTitle(this.Page, "Commercial Awareness Assessment  ", "");
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
            SGACommon.IsTakeTest("viewCaaTest");
            this.Cronometro1.ScriptPath = "~/js/";
            this.Cronometro1.ImagesPath = "~/images/";
            this.Cronometro1.Ascendente = false;
            if (!base.IsPostBack)
            {
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spInitalizeCAATest", new SqlParameter[]
                {
                    new SqlParameter("@userId", SGACommon.LoginUserInfo.userId),
                    new SqlParameter("@testDate", System.DateTime.UtcNow.ToString()),
                    new SqlParameter("@startDate", System.DateTime.UtcNow.ToString()),
                    new SqlParameter("@endDate", System.DateTime.UtcNow.ToString()),
                    new SqlParameter("@sessionId", this.Session.SessionID),
                    new SqlParameter("@initYear", ConfigurationManager.AppSettings["initYear"].ToString())
                });
                this.testId = System.Convert.ToInt32(ds.Tables[0].Rows[0]["testId"].ToString());
                this.Cronometro1.Duracion = new System.TimeSpan(0, System.Convert.ToInt32(ds.Tables[0].Rows[0]["time"].ToString()), 0);
                this.LoadAllTopics();
                this.Percentage();
                this.SetClass();
                base.ClientScript.RegisterStartupScript(this.Page.GetType(), "abc", "$(document).ready(function(){\r\nStyleRadio();\r\n});", true);
            }
        }

        private void LoadAllTopics()
        {
            this.rptrTopics.DataSource = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetCAATopics");
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
                        this.Number = 4;
                        break;
                    case 2:
                        this.Number = 8;
                        break;
                    case 3:
                        this.Number = 12;
                        break;
                    case 4:
                        this.Number = 16;
                        break;
                        //case 5:
                        //    this.Number = 15;
                        //    break;
                        //case 6:
                        //    this.Number = 18;
                        //    break;
                        //case 7:
                        //    this.Number = 21;
                        //    break;
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
                    "SELECT isNull(optionId,'') from  tblCAAOptions where optionId = (select replyId from tblCAAQuestionReply where testId=",
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
            return SqlHelper.ExecuteDataset(CommandType.Text, "select * from tblCAAOptions where questionId = " + qid + " order by frontId ");
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
                    this.lblDescription.Text = SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetCAATopicDetail", param).ToString();
                }
                else
                {
                    lnkButton.Style.Add("color", "#FFFFFF");
                }
            }
        }

        private void BindTopicsQuestions(int topicId)
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetCAAQuestions", new SqlParameter[]
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
                    strPercentage = "You're 20% through, doing well!";
                    break;
                case 1:
                    strPercentage = "You're 40% through, doing well!";
                    break;
                case 2:
                    strPercentage = "You're 60% through, doing well!";
                    break;
                case 3:
                    strPercentage = "You're 80% through, doing well!";
                    break;
                case 4:
                    strPercentage = "You're 100% through, doing well!";
                    break;
                    //case 5:
                    //    strPercentage = "You're 75% through, doing well!";
                    //    break;
                    //case 6:
                    //    strPercentage = "You're 87.50% through, doing well!";
                    //    break;
                    //case 7:
                    //    strPercentage = "You're 100% through, congratulations!";
                    //    break;
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
                                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateCAAOptions", param);
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
                        this.Number = 4;
                        break;
                    case 2:
                        this.Number = 8;
                        break;
                    case 3:
                        this.Number = 12;
                        break;
                    case 4:
                        this.Number = 16;
                        break;
                        //case 5:
                        // this.Number = 20;
                        //  break;
                        //case 6:
                        //    this.Number = 18;
                        //    break;
                        //case 7:
                        //    this.Number = 21;
                        //    break;
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
            this.lnkNext.Visible = (this.PageNumber != 4);
            this.btnSubmit.Visible = (this.PageNumber == 4);
        }

        protected void btnFinal_Click(object sender, System.EventArgs e)
        {
            this.Session["caaTestId"] = this.testId;
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
                    SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateCAAOptions", param);
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


                if (resultFound[0]["Id"] != null && resultFound[0]["Id"].ToString() != String.Empty)
                {

                    SqlParameter[] paramPack = new SqlParameter[]
   {
                new SqlParameter("@userId", SqlDbType.Int),
                new SqlParameter("@initYear",SqlDbType.VarChar)
   };
                    paramPack[0].Value = SGACommon.LoginUserInfo.userId ;
                    paramPack[1].Value = ConfigurationManager.AppSettings["initYear"].ToString();
                    DataSet dsPacks = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetReportIdByUserId", paramPack);
                    if (dsPacks != null)
                    {
                        if (dsPacks.Tables.Count > 0 && dsPacks.Tables[0].Rows.Count > 0)
                        {
                            if (dsPacks.Tables[0].Rows[0]["packId"].ToString() == "6")
                            {
                                isdnAPI.addToGroup(userId, 2114);
                                string Url = "http://" + base.Request.UrlReferrer.Host + "/IndividualReport/ContractManagement.aspx?Id=" + dsPacks.Tables[0].Rows[0]["reportId"].ToString();
                                Contact.Add("_SAGovProcurement18UserReportLink", Url);
                                Contact.Add("ContactType", "Customer");
                                isdnAPI.dsUpdate("Contact", userId, Contact);
                            }
                            else
                            {
                                isdnAPI.addToGroup(userId, 2116);
                                string Url = "http://" + base.Request.UrlReferrer.Host + "/IndividualReport/Procurement.aspx?Id=" + dsPacks.Tables[0].Rows[0]["reportId"].ToString();
                                Contact.Add("_SAGovProcurementUserReportLink", Url);
                                Contact.Add("ContactType", "Customer");
                                isdnAPI.dsUpdate("Contact", userId, Contact);
                            }

                        }
                    }
                }


            }
            this.testId = 0;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spRestrictTest", new SqlParameter[]
            {
                new SqlParameter("@flag", "2"),
                new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
            });
            base.Response.Redirect("~/tna/my-results-bar-graph-caa.aspx", false);
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
                    SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateCAAOptions", param);
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

        protected void Cronometro1_TimeOut(object sender, System.EventArgs e)
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
                        SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spUpdateCAAOptions", param);
                    }
                }
            }
            this.testId = 0;
           // SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spRestrictTest", new SqlParameter[]
           //{
           //     new SqlParameter("@flag", "2"),
           //     new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
           //});
            base.ClientScript.RegisterStartupScript(this.Page.GetType(), "time", "timeOut();", true);
            base.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
        }
    }
}