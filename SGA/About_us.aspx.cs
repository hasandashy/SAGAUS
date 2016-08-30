using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.App_Code;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DataTier;
using CookComputing.XmlRpc;
using InfusionSoftDotNet;
using System.Web.Services;
using System.Web.Security;

namespace SGA
{
    public partial class About_us : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
		{
			SGACommon.AddPageTitle(this.Page, "About Comprara and SkillsGap Analysis", "Procurement consultants, practitioners and trainers with experience across multiple industries.");
		}

		[WebMethod]
		public static string ForgotPassword(string email)
		{
			string retVal = "f";
			DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spForgotPassword", new SqlParameter[]
			{
				new SqlParameter("@email", email)
			});
			if (ds != null)
			{
				if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
				{
					string subject = "";
					string body = "";
					SGACommon.GetEmailTemplate(3, ref subject, ref body);
					body = body.Replace("@v0", ds.Tables[0].Rows[0]["firstName"].ToString()).Replace("@v1", ds.Tables[0].Rows[0]["lastName"].ToString()).Replace("@v3", ds.Tables[0].Rows[0]["email"].ToString()).Replace("@v5", ds.Tables[0].Rows[0]["plainpassword"].ToString());
					MailSending.SendMail(ConfigurationManager.AppSettings["nameDisplay"].ToString(), ConfigurationManager.AppSettings["UserName"].ToString(), email, subject, body, "");
					retVal = "s";
				}
			}
			return retVal;
		}

		[WebMethod]
		public static string SendMail(string Firstname, string Lastname, string Email, string CName, string Department, string Position, string interest, string comments)
		{
			SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spSaveContactInfo", new SqlParameter[]
			{
				new SqlParameter("@firstName", Firstname),
				new SqlParameter("@lastName", Lastname),
				new SqlParameter("@company", CName),
				new SqlParameter("@email", Email),
				new SqlParameter("@department", Department),
				new SqlParameter("@position", Position),
				new SqlParameter("@comments", comments),
				new SqlParameter("@interest", interest),
				new SqlParameter("@insDt", System.DateTime.UtcNow)
			});
			string[] strInterest = SGACommon.RemoveLastCharacter(interest).Split(new char[]
			{
				','
			});
			string[] strField = new string[]
			{
				"Id"
			};
			XmlRpcStruct[] resultFound = isdnAPI.findByEmail(Email, strField);
			XmlRpcStruct Contact = new XmlRpcStruct();
			if (resultFound.Length > 0)
			{
				int userId = System.Convert.ToInt32(resultFound[0]["Id"].ToString());
				if (strInterest.Length > 0)
				{
					for (int i = 0; i < strInterest.Length; i++)
					{
						string text = strInterest[i].ToString();
						if (text != null)
						{
							if (!(text == "Training Needs Analysis"))
							{
								if (!(text == "Contract Management - Training Workshops"))
								{
									if (!(text == "Category Management - Training Workshops"))
									{
										if (!(text == "Sourcing - Training Workshops"))
										{
											if (!(text == "Procurement Leadership - Training Workshops"))
											{
												if (text == "Access to e-Learning")
												{
													isdnAPI.addToGroup(userId, 632);
												}
											}
											else
											{
												isdnAPI.addToGroup(userId, 630);
											}
										}
										else
										{
											isdnAPI.addToGroup(userId, 628);
										}
									}
									else
									{
										isdnAPI.addToGroup(userId, 626);
									}
								}
								else
								{
									isdnAPI.addToGroup(userId, 624);
								}
							}
							else
							{
								isdnAPI.addToGroup(userId, 622);
							}
						}
					}
				}
				isdnAPI.optIn(Email, "Sending emails is allowed");
				Contact.Add("FirstName", Firstname);
				Contact.Add("LastName", Lastname);
				Contact.Add("Email", Email);
				Contact.Add("OwnerID", "50036");
				Contact.Add("Company", CName);
				Contact.Add("JobTitle", Position);
				Contact.Add("_OrganisationDivision", Department);
				Contact.Add("_ContactUsComments", comments);
				Contact.Add("ContactType", "Customer");
				isdnAPI.dsUpdate("Contact", userId, Contact);
			}
			else
			{
				Contact.Add("FirstName", Firstname);
				Contact.Add("LastName", Lastname);
				Contact.Add("Email", Email);
				Contact.Add("OwnerID", "50036");
				Contact.Add("Company", CName);
				Contact.Add("JobTitle", Position);
				Contact.Add("_OrganisationDivision", Department);
				Contact.Add("_ContactUsComments", comments);
				Contact.Add("ContactType", "Customer");
				int userId = isdnAPI.add(Contact);
				if (userId > 0)
				{
					if (strInterest.Length > 0)
					{
						for (int i = 0; i < strInterest.Length; i++)
						{
							string text = strInterest[i].ToString();
							if (text != null)
							{
								if (!(text == "Training Needs Analysis"))
								{
									if (!(text == "Contract Management - Training Workshops"))
									{
										if (!(text == "Category Management - Training Workshops"))
										{
											if (!(text == "Sourcing - Training Workshops"))
											{
												if (!(text == "Procurement Leadership - Training Workshops"))
												{
													if (text == "Access to e-Learning")
													{
														isdnAPI.addToGroup(userId, 632);
													}
												}
												else
												{
													isdnAPI.addToGroup(userId, 630);
												}
											}
											else
											{
												isdnAPI.addToGroup(userId, 628);
											}
										}
										else
										{
											isdnAPI.addToGroup(userId, 626);
										}
									}
									else
									{
										isdnAPI.addToGroup(userId, 624);
									}
								}
								else
								{
									isdnAPI.addToGroup(userId, 622);
								}
							}
						}
					}
					bool isAdded = isdnAPI.addToGroup(userId, 400);
					isdnAPI.optIn(Email, "Sending emails is allowed");
				}
			}
			return "success";
		}

		[WebMethod]
		public static string Login(string username, string password, bool rememberMe)
		{
			SqlParameter[] sql = new SqlParameter[]
			{
				new SqlParameter("@email", username)
			};
			int result = System.Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spCheckLoginEmail", sql));
			string result2;
			if (result <= 0)
			{
				result = System.Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spCheckUserExpired", sql));
				if (result <= 0)
				{
					result2 = "u";
				}
				else
				{
					result2 = "e";
				}
			}
			else
			{
				DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetPassword", sql);
				string passwordHash = SGACommon.CreatePasswordHash(password, ds.Tables[0].Rows[0]["passwordSalt"].ToString());
				bool passMatch = ds.Tables[0].Rows[0]["passwordHash"].ToString().Equals(passwordHash);
				if (passMatch)
				{
					result = System.Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetIdByEmail", sql));
					sql[0] = new SqlParameter("@userId", result);
					SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "addLoginUserHistory", sql);
					FormsAuthentication.SetAuthCookie(username + ":" + result, rememberMe);
					result2 = "s";
				}
				else
				{
					result2 = "i";
				}
			}
			return result2;
		}

		[WebMethod]
		public static string RegisterUser(string fname, string lname, string email, int jobId, int jobLevel, int agencyId)
		{
			SqlParameter[] param = new SqlParameter[]
			{
				new SqlParameter("@email", SqlDbType.VarChar)
			};
			param[0].Value = email;
			int status = System.Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spCheckUniqueEmail", param));
			string result2;
			if (status > 0)
			{
				result2 = "d";
			}
			else
			{
				try
				{
					string plainpassword = SGACommon.generatePassword(8);
					string passwordSalt = SGACommon.CreateSalt(5);
					string passwordHash = SGACommon.CreatePasswordHash(plainpassword, passwordSalt);
					param = new SqlParameter[12];
					param[0] = new SqlParameter("@action", SqlDbType.VarChar);
					param[0].Value = "Insert";
					param[1] = new SqlParameter("@password", SqlDbType.VarChar);
					param[1].Value = plainpassword;
					param[2] = new SqlParameter("@firstName", SqlDbType.VarChar);
					param[2].Value = fname;
					param[3] = new SqlParameter("@lastName", SqlDbType.VarChar);
					param[3].Value = lname;
					param[4] = new SqlParameter("@email", SqlDbType.VarChar);
					param[4].Value = email;
					param[5] = new SqlParameter("@isApproved", SqlDbType.Bit);
					param[5].Value = 1;
					param[6] = new SqlParameter("@passwordHash", SqlDbType.VarChar);
					param[6].Value = passwordHash;
					param[7] = new SqlParameter("@passwordSalt", SqlDbType.VarChar);
					param[7].Value = passwordSalt;
					param[8] = new SqlParameter("@jobRole", SqlDbType.Int);
					param[8].Value = jobId;
					param[9] = new SqlParameter("@isAdminAdded", SqlDbType.Bit);
					param[9].Value = 0;
					param[10] = new SqlParameter("@jobLevel", SqlDbType.Int);
					param[10].Value = jobLevel;				
					param[11] = new SqlParameter("@agencyId", SqlDbType.Int);
					param[11].Value = agencyId;
					int result = System.Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spUserMaster", param));
					if (result == 0)
					{
						result2 = "u";
					}
					else
					{
						string[] strField = new string[]
						{
							"Id"
						};
						XmlRpcStruct[] resultFound = isdnAPI.findByEmail(email, strField);
						if (resultFound.Length > 0)
						{
							int userId = System.Convert.ToInt32(resultFound[0]["Id"].ToString());
							bool isAdded = isdnAPI.addToGroup(userId, 400);
							isdnAPI.optIn(email, "Sending emails is allowed");
							isdnAPI.dsUpdate("Contact", userId, new XmlRpcStruct
							{
								{
									"FirstName",
									fname
								},
								{
									"LastName",
									lname
								},
								{
									"Email",
									email
								},
								{
									"OwnerID",
									"50036"
								},
								{
									"_CSBPassword",
									plainpassword
								},
								{
									"_YourOrganisation",
									Profile.GetOrganisation(agencyId)
								},
								{
									"_Role",
									Profile.GetJobRole(jobId)
								},
								{
									"_RoleLevel",
									Profile.GetJobLevel(jobLevel)
								},
								{
									"_CSBUsername",
									email
								}
							});
						}
						else
						{
							int userId = isdnAPI.add(new XmlRpcStruct
							{
								{
									"FirstName",
									fname
								},
								{
									"LastName",
									lname
								},
								{
									"Email",
									email
								},
								{
									"OwnerID",
									"50036"
								},								
								{
									"_CSBPassword",
									plainpassword
								},
								{
									"_YourOrganisation",
									Profile.GetOrganisation(agencyId)
								},
								{
									"_Role",
									Profile.GetJobRole(jobId)
								},
								{
									"_RoleLevel",
									Profile.GetJobLevel(jobLevel)
								},
								{
									"_CSBUsername",
									email
								},
								{
									"ContactType",
									"Customer"
								}
							});
							if (userId > 0)
							{
								bool isAdded = isdnAPI.addToGroup(userId, 400);
								isdnAPI.optIn(email, "Sending emails is allowed");
							}
						}
						result2 = "s";
					}
				}
				catch (System.Exception ex_571)
				{
					result2 = "e";
				}
			}
			return result2;
		}

		[WebMethod]
		public static string SendMailContactUs(string fullname, string jobTitle, string fromUser, string company, string phone, string city, string country, string interest, string needs, string comments)
		{
			SqlParameter[] param = new SqlParameter[11];
			param[0] = new SqlParameter("@name", SqlDbType.VarChar);
			param[0].Value = fullname;
			param[1] = new SqlParameter("@company", SqlDbType.VarChar);
			param[1].Value = company;
			param[2] = new SqlParameter("@jobTitle", SqlDbType.VarChar);
			param[2].Value = jobTitle;
			param[3] = new SqlParameter("@email", SqlDbType.VarChar);
			param[3].Value = fromUser;
			param[4] = new SqlParameter("@phone", SqlDbType.VarChar);
			param[4].Value = phone;
			param[5] = new SqlParameter("@city", SqlDbType.VarChar);
			param[5].Value = city;
			param[6] = new SqlParameter("@country", SqlDbType.VarChar);
			param[6].Value = country;
			param[8] = new SqlParameter("@interest", SqlDbType.VarChar);
			param[8].Value = interest;
			param[9] = new SqlParameter("@needs", SqlDbType.VarChar);
			param[9].Value = needs;
			param[10] = new SqlParameter("@comments", SqlDbType.VarChar);
			param[10].Value = comments;
			SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spContactUs", param);
			return "s";
		}

        [WebMethod]
        public static string Help(string firstName, string lastName, string email, string department, int primaryEnquirytype, string query, string comments)
        {
            string strRecommendation = "";
            int selectedValue = 0;
            string strEnquiry = "";
            switch (primaryEnquirytype) { 
                case 1:
                    strEnquiry = "Navigating or logging into the website";
                    selectedValue = Convert.ToInt32(query);
                    switch (selectedValue) { 
                        case 1:
                            strRecommendation = "Navigation and accessibility";
                            break;
                        case 2:
                            strRecommendation = "User registration";
                            break;
                    }
                    break;
                case 2:
                    strEnquiry = "Workshop event enquiries and bookings";
                    selectedValue = Convert.ToInt32(query);
                    switch (selectedValue)
                    {
                        case 1:
                            strRecommendation = "Event inquiries and booking";
                            break;
                        case 2:
                            strRecommendation = "Registration process";
                            break;
                        case 3:
                            strRecommendation = "Event Information";
                            break;
                        case 4:
                            strRecommendation = "Post event";
                            break;
                        case 5:
                            strRecommendation = "General";
                            break;
                    }
                    break;
                case 3:
                    strEnquiry = "Taking the TNA and accessing my report / results";
                    selectedValue = Convert.ToInt32(query);
                    switch (selectedValue)
                    {
                        case 1:
                            strRecommendation = "Registration, username and password";
                            break;
                        case 2:
                            strRecommendation = "Assessments";
                            break;
                        case 3:
                            strRecommendation = "Results";
                            break;
                        case 4:
                            strRecommendation = "Reporting";
                            break;
                        case 5:
                            strRecommendation = "General";
                            break;
                    }
                    break;
                case 4:
                    strEnquiry = "Accessing the e-Learning portal";
                    selectedValue = Convert.ToInt32(query);
                    switch (selectedValue)
                    {
                        case 1:
                            strRecommendation = "Registration, username and password";
                            break;
                        case 2:
                            strRecommendation = "Learning plan";
                            break;
                        case 3:
                            strRecommendation = "Reporting";
                            break;
                        case 4:
                            strRecommendation = "General";
                            break;
                    }
                    break;
                /*case 5:
                    strEnquiry = "Other -- addtional supporting comments";
                    strRecommendation = query;
                    break;*/
            }

            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@firstName", SqlDbType.VarChar);
            param[0].Value = firstName;

            param[1] = new SqlParameter("@lastName", SqlDbType.VarChar);
            param[1].Value = lastName;
            param[2] = new SqlParameter("@email", SqlDbType.VarChar);
            param[2].Value = email;
            param[3] = new SqlParameter("@department", SqlDbType.VarChar);
            param[3].Value = department;
            param[4] = new SqlParameter("@primaryEnquirytype", SqlDbType.VarChar);
            param[4].Value = strEnquiry;
            param[5] = new SqlParameter("@recommendations", SqlDbType.VarChar);
            param[5].Value = strRecommendation;
            param[6] = new SqlParameter("@comments", SqlDbType.VarChar);
            param[6].Value = comments;

            string subject = "";
            string body = "";
            SGACommon.GetEmailTemplate(12, ref subject, ref body);
            body = body.Replace("@v5", firstName).Replace("@v6", lastName).Replace("@v4", email).Replace("@v7", department).Replace("@v2", strEnquiry).Replace("@v3", strRecommendation).Replace("@comments",comments);
            MailSending.SendMail(ConfigurationManager.AppSettings["nameDisplay"].ToString(), email, "support@comprara.com.au", subject, body, "");

            SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spProcessHelp", param);
            return "s";
        }
	}
    
}