﻿using DataTier;
using SGA.App_Code;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace SGA.tna
{
    public partial class share_the_challenge : System.Web.UI.Page
    {
        

        protected string _name = "";

        protected bool isSgaResult = false;

        protected bool isTnaResult = false;

        protected bool isPmpResult = false;

        protected bool isDmpResult = false;

        protected bool isNpResult = false;

        protected bool isCMAResult = false;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!base.IsPostBack)
            {
                DataSet dsPermission = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetPremission", new SqlParameter[]
				{
					new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
				});
                if (dsPermission != null)
                {
                    if (dsPermission.Tables.Count > 0 && dsPermission.Tables[0].Rows.Count > 0)
                    {
                        this.isSgaResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewSgaResult"].ToString());
                        this.isTnaResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewTnaResult"].ToString());
                        this.isPmpResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewPMPResult"].ToString());
                        this.isDmpResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewDMPResult"].ToString());
                        this.isNpResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewNPResult"].ToString());
                        this.isCMAResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewCMAResult"].ToString());
                    }
                }
                this.spSkills.Attributes["class"] = (this.isTnaResult ? "" : "lock");
                this.spBehaviour.Attributes["class"] = (this.isPmpResult ? "" : "lock");
                this._name = SGACommon.GetName();
                base.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
                base.ClientScript.RegisterStartupScript(this.Page.GetType(), "abc", "$(document).ready(function(){\r\nStyleRadio();\r\n});", true);
            }
        }

        [WebMethod]
        public static string SaveShareInfo(string fname, string lname, string email, string company, int personType, string message)
        {
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spSaveShare", new SqlParameter[]
			{
				new SqlParameter("@fname", fname),
				new SqlParameter("@lname", lname),
				new SqlParameter("@email", email),
				new SqlParameter("@company", company),
				new SqlParameter("@personType", personType),
				new SqlParameter("@message", message),
				new SqlParameter("@insDt", System.DateTime.UtcNow.ToString()),
				new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
			});
            string emailsubject = "";
            string body = "";
            SGACommon.GetEmailTemplate(4, ref emailsubject, ref body);
            emailsubject = emailsubject.Replace("@v0", fname).Replace("@v1", lname).Replace("@v2", SGACommon.GetFullName());
            body = body.Replace("@v0", SGACommon.GetFullName()).Replace("@v2", message.Replace("\r\n", "<br/>"));
            MailSending.SendMail(ConfigurationManager.AppSettings["nameDisplay"].ToString(), ConfigurationManager.AppSettings["UserName"].ToString(), email, emailsubject, body, "");
            return "s";
        }
    }
}