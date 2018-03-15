using DataTier;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Linq;

namespace SGA.App_Code
{
    public static class SGACommon
    {
        public static User LoginUserInfo
        {
            get
            {
                User cu = null;
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    cu = new User();
                    string[] args = HttpContext.Current.User.Identity.Name.Split(new char[]
                    {
                        ':'
                    });
                    if (args.Length > 0)
                    {
                        cu.userId = System.Convert.ToInt32(args[1]);
                        cu.name = args[0];
                    }
                }
                return cu;
            }
        }

        public static void AddPageTitle(Page page, string title, string description)
        {
            page.Title = SGACommon.Left(title, 65);
            HtmlMeta keywords = new HtmlMeta();
            keywords.Name = "description";
            keywords.Content = SGACommon.Left(description, 156);
            page.Header.Controls.Add(keywords);
        }

        public static void SaveBrowserDetails(int userId, string browserName, string userAgent, string sessionId)
        {
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spProcessSessionDetails", new SqlParameter[]
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@browserName", browserName),
                new SqlParameter("@userAgent", userAgent),
                new SqlParameter("@sessionId", sessionId)
            });
        }

        public static string UppercaseFirst(string s)
        {
            string result;
            if (string.IsNullOrEmpty(s))
            {
                result = string.Empty;
            }
            else
            {
                result = char.ToUpper(s[0]) + s.Substring(1).ToLower();
            }
            return result;
        }

        public static string CmcRatingByPercentage(double percentage)
        {
            string strValue = "";
            if (percentage > 0.0 && percentage < 37.0)
            {
                strValue = "Novice";
            }
            else if (percentage > 37.01 && percentage < 46.5)
            {
                strValue = "Novice to Beginner";
            }
            else if (percentage > 46.51 && percentage < 56.0)
            {
                strValue = "Beginner";
            }
            else if (percentage > 56.01 && percentage < 65.5)
            {
                strValue = "Beginner to Intermediate";
            }
            else if (percentage > 65.51 && percentage < 75.0)
            {
                strValue = "Intermediate";
            }
            else if (percentage > 75.01 && percentage < 84.5)
            {
                strValue = "Specialist ";
            }
            else if (percentage > 84.51 && percentage < 94.0)
            {
                strValue = "Specialist to Mastery";
            }
            else if (percentage > 94.0)
            {
                strValue = "Mastery";
            }
            return strValue;
        }

        public static void IsViewResult(string colName)
        {
            bool isView = true;
            DataSet dsPermission = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetPremission", new SqlParameter[]
            {
                new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
            });
            if (dsPermission != null)
            {
                isView = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0][colName].ToString());
            }
            if (!isView)
            {
                HttpContext.Current.Response.Redirect("ResultDenied.aspx");
            }
        }

        public static void IsTakeTest(string colName)
        {
            bool isView = true;
            DataSet dsPermission = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetPremission", new SqlParameter[]
            {
                new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
            });
            if (dsPermission != null)
            {
                isView = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0][colName].ToString());
            }
            if (!isView)
            {
                HttpContext.Current.Response.Redirect("TestDenied.aspx");
            }
        }

        public static void DisplayErrorBox(string strMsg, Page myPage)
        {
            string script = "alert('" + strMsg + "');";
            ScriptManager.RegisterStartupScript(myPage, myPage.GetType(), "UserSecurity", script, true);
        }

        public static void GetEmailTemplate(int id, ref string subject, ref string body)
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageTemplate", new SqlParameter[]
            {
                new SqlParameter("@flag", "0"),
                new SqlParameter("@id", id)
            });
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    body = HttpContext.Current.Server.HtmlDecode(ds.Tables[0].Rows[0]["emailBody"].ToString());
                    subject = ds.Tables[0].Rows[0]["subject"].ToString();
                }
            }
        }

        public static System.DateTimeOffset ToAusTimeZone(System.DateTime sourceDt)
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");
            System.DateTimeOffset utcOffset = new System.DateTimeOffset(sourceDt, System.TimeSpan.Zero);
            return utcOffset.ToOffset(tz.GetUtcOffset(utcOffset));
        }

        public static string ToTitleCase(string text)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
        }

        public static void Redirect(string url, string target, string windowFeatures)
        {
            HttpContext context = HttpContext.Current;
            if ((string.IsNullOrEmpty(target) || target.Equals("_self", System.StringComparison.OrdinalIgnoreCase)) && string.IsNullOrEmpty(windowFeatures))
            {
                context.Response.Redirect(url);
            }
            else
            {
                Page page = (Page)context.Handler;
                if (page == null)
                {
                    throw new System.InvalidOperationException("Cannot redirect to new window outside Page context.");
                }
                url = page.ResolveClientUrl(url);
                string script;
                if (!string.IsNullOrEmpty(windowFeatures))
                {
                    script = "window.open(\"{0}\", \"{1}\", \"{2}\");";
                }
                else
                {
                    script = "window.open(\"{0}\", \"{1}\");";
                }
                script = string.Format(script, url, target, windowFeatures);
                ScriptManager.RegisterStartupScript(page, typeof(Page), "Redirect", script, true);
            }
        }

        public static SqlConnection dbConnect()
        {
            string sqlString = ConfigurationSettings.AppSettings["Conn"].ToString();
            SqlConnection cnQuestionaire = new SqlConnection(sqlString);
            cnQuestionaire.Open();
            return cnQuestionaire;
        }

        public static void SelectListItem(DropDownList list, object value)
        {
            if (list.Items.Count != 0)
            {
                ListItem selectedItem = list.SelectedItem;
                if (selectedItem != null)
                {
                    selectedItem.Selected = false;
                }
                if (value != null)
                {
                    selectedItem = list.Items.FindByValue(value.ToString());
                    if (selectedItem != null)
                    {
                        selectedItem.Selected = true;
                    }
                }
            }
        }

        public static void SelectListItemForListBox(ListBox list, object value)
        {
            if (list.Items.Count != 0)
            {
                ListItem selectedItem = list.SelectedItem;
                if (selectedItem != null)
                {
                    selectedItem.Selected = false;
                }
                if (value != null)
                {
                    selectedItem = list.Items.FindByValue(value.ToString());
                    if (selectedItem != null)
                    {
                        selectedItem.Selected = true;
                    }
                }
            }
        }

        public static void ShowMessageBox(string message, Page page)
        {
            if (message.Length > 0)
            {
                string strReturnValue = "jQuery(document).ready(function($) {\r\n";
                strReturnValue = strReturnValue + "jQuery.facebox(\"<b>" + message + "</b>\");\r\n";
                strReturnValue += "});";
                page.ClientScript.RegisterStartupScript(page.GetType(), "face", strReturnValue, true);
            }
        }

        public static void CloseWindow()
        {
            HttpContext.Current.Response.Write("<script>window.close();</script>");
        }

        public static string ContenType(string extention)
        {
            string strVal = "";
            switch (extention)
            {
                case "xls":
                    strVal = "application/vnd.ms-excel";
                    break;
                case "xlsx":
                    strVal = "application/vnd.ms-excel";
                    break;
                case "doc":
                    strVal = "application/msword";
                    break;
                case "docx":
                    strVal = "application/msword";
                    break;
                case "ppt":
                    strVal = "application/vnd.ms-powerpoint";
                    break;
                case "pptx":
                    strVal = "application/vnd.ms-powerpoint";
                    break;
                case "pdf":
                    strVal = "application/pdf";
                    break;
                case "jpg":
                    strVal = "image/jpeg";
                    break;
                case "jepg":
                    strVal = "image/jpeg";
                    break;
                case "gif":
                    strVal = "image/gif";
                    break;
                case "png":
                    strVal = "image/png";
                    break;
                case "bmp":
                    strVal = "image/bmp";
                    break;
                case "ico":
                    strVal = "image/vnd.microsoft.icon";
                    break;
                case "zip":
                    strVal = "application/zip";
                    break;
                case "txt":
                    strVal = "text/plain";
                    break;
                case "css":
                    strVal = "text/plain";
                    break;
                case "js":
                    strVal = "text/plain";
                    break;
                case "html":
                    strVal = "text/html";
                    break;
                case "rtf":
                    strVal = "text/richtext";
                    break;
            }
            return strVal;
        }

        public static bool ConfigGetBooleanValue(NameValueCollection config, string valueName, bool defaultValue)
        {
            string str = config[valueName];
            bool result2;
            if (str == null)
            {
                result2 = defaultValue;
            }
            else
            {
                bool result;
                if (!bool.TryParse(str, out result))
                {
                    throw new System.Exception(string.Format("Value must be boolean {0}", valueName));
                }
                result2 = result;
            }
            return result2;
        }

        public static int ConfigGetIntValue(NameValueCollection config, string valueName, int defaultValue, bool zeroAllowed, int maxValueAllowed)
        {
            string str = config[valueName];
            int result2;
            int result;
            if (str == null)
            {
                result2 = defaultValue;
            }
            else if (!int.TryParse(str, out result))
            {
                if (zeroAllowed)
                {
                    throw new System.Exception(string.Format("Value must be non negative integer {0}", valueName));
                }
                throw new System.Exception(string.Format("Value must be positive integer {0}", valueName));
            }
            else
            {
                if (zeroAllowed && result < 0)
                {
                    throw new System.Exception(string.Format("Value must be non negative integer {0}", valueName));
                }
                if (!zeroAllowed && result <= 0)
                {
                    throw new System.Exception(string.Format("Value must be positive integer {0}", valueName));
                }
                if (maxValueAllowed > 0 && result > maxValueAllowed)
                {
                    throw new System.Exception(string.Format("Value too big {0}", valueName));
                }
                result2 = result;
            }
            return result2;
        }

        public static string GetName()
        {
            string name = "";
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                name = SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetName", new SqlParameter[]
                {
                    new SqlParameter("@Id", SGACommon.LoginUserInfo.userId)
                }).ToString();
            }
            return SGACommon.ToTitleCase(name);
        }

        public static string GetName(int userId)
        {
            string name = SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetName", new SqlParameter[]
            {
                new SqlParameter("@Id", userId)
            }).ToString();
            return SGACommon.ToTitleCase(name);
        }

        public static string GetFullName()
        {
            string name = "";
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                name = SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetFullName", new SqlParameter[]
                {
                    new SqlParameter("@Id", SGACommon.LoginUserInfo.userId)
                }).ToString();
            }
            return SGACommon.ToTitleCase(name);
        }

        public static string GetFullName(int userId)
        {
            string name = "";
           
                name = SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetFullName", new SqlParameter[]
                {
                    new SqlParameter("@Id", userId)
                }).ToString();
           
            return SGACommon.ToTitleCase(name);
        }

        public static string GetCompanyName()
        {
            string name = "";
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                name = SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetCompanyByUser", new SqlParameter[]
                {
                    new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
                }).ToString();
            }
            return SGACommon.ToTitleCase(name);
        }

        public static string ShowMessageBoxWithJqeryInstance(string message, Page page)
        {
            string strReturnValue = "";
            if (message.Length > 0)
            {
                strReturnValue = "$(document).ready(function($) {\r\n";
                strReturnValue = strReturnValue + "$.facebox(\"<b>" + message + "</b>\");\r\n";
                strReturnValue += "});";
            }
            return strReturnValue;
        }

        public static string RemoveLastCharacter(string strInput)
        {
            if (strInput.Length > 0)
            {
                strInput = strInput.Remove(strInput.Length - 1, 1);
            }
            return strInput;
        }

        public static string GenerateFakeId(string prefix, int startNumber)
        {
            string strRetval;
            if (startNumber > 9)
            {
                strRetval = prefix + "00" + startNumber;
            }
            else
            {
                strRetval = prefix + "000" + startNumber;
            }
            return strRetval;
        }

        public static string Left(string text, int length)
        {
            string result;
            if (length <= 0 || text.Length == 0)
            {
                result = "";
            }
            else if (text.Length <= length)
            {
                result = text;
            }
            else
            {
                result = text.Substring(0, length);
            }
            return result;
        }

        public static bool isDecimal(string strInput)
        {
            bool retval;
            try
            {
                Regex numRange = new Regex("^\\d+(\\.\\d{1,2})?$");
                Match match = numRange.Match(strInput);
                retval = match.Success;
            }
            catch (System.Exception ex)
            {
                retval = false;
            }
            return retval;
        }

        public static void FillQuestionsPoints(DropDownList ddl)
        {
            for (int i = 0; i < 11; i++)
            {
                ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        public static string FormatMultilineText(string strInput)
        {
            if (strInput.Length > 0)
            {
                strInput = strInput.Replace("\r\n", "<br />");
            }
            return strInput;
        }

        internal static bool isCorrectDateformat(string p)
        {
            throw new System.NotImplementedException();
        }

        public static string YesNoConversion(object obj)
        {
            string strValue = "";
            try
            {
                if (obj is bool)
                {
                    strValue = (System.Convert.ToBoolean(obj) ? "Yes" : "No");
                }
            }
            catch (System.Exception ex)
            {
                strValue = "";
            }
            return strValue;
        }

        public static string YesNoConversionInt(object obj)
        {
            string strValue = "";
            try
            {
                if (obj is int)
                {
                    strValue = ((System.Convert.ToInt32(obj) == 0) ? "No" : "Yes");
                }
            }
            catch (System.Exception ex)
            {
                strValue = ex.ToString();
            }
            return strValue;
        }

        public static void InsertDefaultItem(DropDownList source, string defaultText, string defaultValue)
        {
            source.Items.Insert(0, new ListItem(defaultText, defaultValue));
        }

        public static bool isNumeric(string strInput)
        {
            bool retval;
            try
            {
                System.Convert.ToInt32(strInput.ToString());
                retval = true;
            }
            catch (System.Exception ex)
            {
                retval = false;
            }
            return retval;
        }

        public static string generatePassword(int length)
        {
            string guidResult = System.Guid.NewGuid().ToString();
            guidResult = guidResult.Replace("-", string.Empty);
            if (length <= 0 || length > guidResult.Length)
            {
                throw new System.ArgumentException("Length must be between 1 and " + guidResult.Length);
            }
            return guidResult.Substring(0, length);
        }

        public static string CreatePasswordHash(string password, string salt)
        {
            string passwordFormat = "SHA1";
            return FormsAuthentication.HashPasswordForStoringInConfigFile(password + salt, passwordFormat);
        }

        public static string CreateSalt(int size)
        {
            System.Security.Cryptography.RNGCryptoServiceProvider provider = new System.Security.Cryptography.RNGCryptoServiceProvider();
            byte[] data = new byte[size];
            provider.GetBytes(data);
            return System.Convert.ToBase64String(data);
        }
    }

    public static class Profile
    {
        public static string GetIndustry(int industryId)
        {
            string strValue;
            switch (industryId)
            {
                case 1:
                    strValue = "Advertising, Media & Communications";
                    break;
                case 2:
                    strValue = "Agribusiness";
                    break;
                case 3:
                    strValue = "Associations";
                    break;
                case 4:
                    strValue = "Automotive";
                    break;
                case 5:
                    strValue = "Business Services";
                    break;
                case 6:
                    strValue = "Consulting & Business Strategy";
                    break;
                case 7:
                    strValue = "Defence";
                    break;
                case 8:
                    strValue = "Diversified";
                    break;
                case 9:
                    strValue = "Education & Training";
                    break;
                case 10:
                    strValue = "Energy";
                    break;
                case 11:
                    strValue = "Environment";
                    break;
                case 12:
                    strValue = "Financial Services";
                    break;
                case 13:
                    strValue = "FMCG";
                    break;
                case 14:
                    strValue = "Government";
                    break;
                case 15:
                    strValue = "Healthcare & Medical";
                    break;
                case 16:
                    strValue = "Hospitality, Tourism & Entertainment";
                    break;
                case 17:
                    strValue = "HR & Recruitment";
                    break;
                case 18:
                    strValue = "Information Technology";
                    break;
                case 19:
                    strValue = "Infrastructure";
                    break;
                case 20:
                    strValue = "Legal";
                    break;
                case 21:
                    strValue = "Manufacturing";
                    break;
                case 22:
                    strValue = "Mining, Oil, Gas & Resources";
                    break;
                case 23:
                    strValue = "Not for Profit";
                    break;
                case 24:
                    strValue = "Pharmaceuticals";
                    break;
                case 25:
                    strValue = "Property, Construction & Engineering";
                    break;
                case 26:
                    strValue = "Retail";
                    break;
                case 27:
                    strValue = "Sports & Community";
                    break;
                case 28:
                    strValue = "Supply Chain, Logistics & Transport";
                    break;
                case 29:
                    strValue = "Telecommunications";
                    break;
                case 30:
                    strValue = "Trade & Services";
                    break;
                default:
                    strValue = "No response entered";
                    break;
            }
            return strValue;
        }

        public static string GetAnnualRevenue(int revenueId)
        {
            string strValue;
            switch (revenueId)
            {
                case 1:
                    strValue = "$1 billion or more";
                    break;
                case 2:
                    strValue = "$500 million to $999.9 million";
                    break;
                case 3:
                    strValue = "$100 million to $499.9 million";
                    break;
                case 4:
                    strValue = "$50 million to $99.9 million";
                    break;
                case 5:
                    strValue = "$20 million to $49.9 million";
                    break;
                case 6:
                    strValue = "$10 million to $19.9 million";
                    break;
                case 7:
                    strValue = "$5 million to $9.9 million";
                    break;
                case 8:
                    strValue = "$2.5 million to $4.9 million";
                    break;
                case 9:
                    strValue = "$1 million to $2.49 million";
                    break;
                case 10:
                    strValue = "$500,000 to $999,999";
                    break;
                case 11:
                    strValue = "Less than $500,000";
                    break;
                default:
                    strValue = "No response entered";
                    break;
            }
            return strValue;
        }

        public static string GetProcurementModel(int modelId)
        {
            string strValue;
            switch (modelId)
            {
                case 1:
                    strValue = "Centralised Procurement Function";
                    break;
                case 2:
                    strValue = "Decentralised Procurement Function";
                    break;
                case 3:
                    strValue = "Centre-Led Procurement Function";
                    break;
                case 4:
                    strValue = "Procurement strategy is centralised, but execution is de-centralised";
                    break;
                default:
                    strValue = "No response entered";
                    break;
            }
            return strValue;
        }

        public static string GetReportLineTo(int reportingId)
        {
            string strValue;
            switch (reportingId)
            {
                case 1:
                    strValue = "CEO";
                    break;
                case 2:
                    strValue = "CFO";
                    break;
                case 3:
                    strValue = "COO";
                    break;
                case 4:
                    strValue = "CIO";
                    break;
                case 5:
                    strValue = "Legal Council";
                    break;
                case 6:
                    strValue = "Head of Supply Chain";
                    break;
                case 7:
                    strValue = "Division or Business Unit Head";
                    break;
                case 8:
                    strValue = "Regional or Global Procurement";
                    break;
                default:
                    strValue = "No response entered";
                    break;
            }
            return strValue;
        }

        public static string GetNoOfEmployee(int noofEmployeeId)
        {
            string strValue;
            switch (noofEmployeeId)
            {
                case 1:
                    strValue = "100+";
                    break;
                case 2:
                    strValue = "75 to 99";
                    break;
                case 3:
                    strValue = "50 to 74";
                    break;
                case 4:
                    strValue = "30 to 49";
                    break;
                case 5:
                    strValue = "15 to 29";
                    break;
                case 6:
                    strValue = "10 to 14";
                    break;
                case 7:
                    strValue = "9";
                    break;
                case 8:
                    strValue = "8";
                    break;
                case 9:
                    strValue = "7";
                    break;
                case 10:
                    strValue = "6";
                    break;
                case 11:
                    strValue = "5";
                    break;
                case 12:
                    strValue = "4";
                    break;
                case 13:
                    strValue = "3";
                    break;
                case 14:
                    strValue = "2";
                    break;
                case 15:
                    strValue = "1";
                    break;
                default:
                    strValue = "No response entered";
                    break;
            }
            return strValue;
        }

        public static string GetGeoInfluence(int influenceId)
        {
            string strValue;
            switch (influenceId)
            {
                case 1:
                    strValue = "Local";
                    break;
                case 2:
                    strValue = "Regional";
                    break;
                case 3:
                    strValue = "Global";
                    break;
                default:
                    strValue = "No response entered";
                    break;
            }
            return strValue;
        }

        public static string GetJobRole(int roleId)
        {
            string strValue;
            switch (roleId)
            {
                case 1:
                    strValue = "Purchasing Officer";
                    break;
                case 2:
                    strValue = "Procurement/ Purchasing Support";
                    break;
                case 3:
                    strValue = "Procurement/ Purchasing Analyst";
                    break;
                case 4:
                    strValue = "Procurement Officer/ Advisor";
                    break;
                case 5:
                    strValue = "Procurement Specialist";
                    break;
                case 6:
                    strValue = "Contract Manager";
                    break;
                case 7:
                    strValue = "Category Manager";
                    break;
                case 8:
                    strValue = "Procurement Manager/ Director";
                    break;
                default:
                    strValue = "No response entered";
                    break;
            }
            return strValue;
        }

        public static string GetCategory(int categoryId)
        {
            string strValue;
            switch (categoryId)
            {
                case 1:
                    strValue = "Generalist Direct & Indirects";
                    break;
                case 2:
                    strValue = "Generalist Directs";
                    break;
                case 3:
                    strValue = "Generalist Indirects";
                    break;
                case 4:
                    strValue = "Chemicals";
                    break;
                case 5:
                    strValue = "Energy";
                    break;
                case 6:
                    strValue = "Facilities";
                    break;
                case 7:
                    strValue = "Fleet";
                    break;
                case 8:
                    strValue = "Heavy Machinery and Equipment";
                    break;
                case 9:
                    strValue = "HR Services";
                    break;
                case 10:
                    strValue = "ICT";
                    break;
                case 11:
                    strValue = "Ingredients";
                    break;
                case 12:
                    strValue = "Marketing";
                    break;
                case 13:
                    strValue = "Mining Equipment";
                    break;
                case 14:
                    strValue = "MRO and Capex";
                    break;
                case 15:
                    strValue = "Packaging";
                    break;
                case 16:
                    strValue = "Professional Services";
                    break;
                case 17:
                    strValue = "Raw Materials";
                    break;
                case 18:
                    strValue = "Travel";
                    break;
                case 19:
                    strValue = "Wardrobe & Workwear";
                    break;
                default:
                    strValue = "No response entered";
                    break;
            }
            return strValue;
        }

        public static string GetProcurementLevel(int proLevelId)
        {
            string strValue;
            switch (proLevelId)
            {
                case 1:
                    strValue = "Undergraduate degree in procurement / supply chain";
                    break;
                case 2:
                    strValue = "Postgraduate degree in procurement / supply chain";
                    break;
                case 3:
                    strValue = "CIPS: Member (MCIPS)";
                    break;
                case 4:
                    strValue = "CIPS: Fellow (FCIPS)";
                    break;
                case 5:
                    strValue = "AAPCM: Member";
                    break;
                case 6:
                    strValue = "AAPCM: Fellow";
                    break;
                case 7:
                    strValue = "Other";
                    break;
                case 8:
                    strValue = "Not applicable";
                    break;
                default:
                    strValue = "No response entered";
                    break;
            }
            return strValue;
        }

        public static string GetEducation(int educationId)
        {
            string strValue;
            switch (educationId)
            {
                case 1:
                    strValue = "Not applicable";
                    break;
                case 2:
                    strValue = "Certificate in Procurement / Contract Management";
                    break;
                case 3:
                    strValue = "Diploma in Procurement / Contract Management";
                    break;
                case 4:
                    strValue = "Undergraduate degree in Procurement / Contract Management";
                    break;
                case 5:
                    strValue = "Postgraduate diploma in Procurement / Contract Management";
                    break;
                case 6:
                    strValue = "Postgraduate degree in Procurement / Contract Management";
                    break;
                case 7:
                    strValue = "CIPS: Member (MCIPS)";
                    break;
                case 8:
                    strValue = "CIPS: Fellow (FCIPS)";
                    break;
                case 9:
                    strValue = "AAPCM: Member";
                    break;
                case 10:
                    strValue = "AAPCM: Fellow";
                    break;
                case 11:
                    strValue = "IACCM: Accreditation";
                    break;
                case 12:
                    strValue = "IFPSM: Certified International Procurement Professional";
                    break;
                case 13:
                    strValue = "IFPSM: Certified International Advanced Procurement Professional";
                    break;
                case 14:
                    strValue = "Other";
                    break;
                default:
                    strValue = "No response entered";
                    break;
            }
            return strValue;
        }

        public static string GetGoodsLevel(int goodsId)
        {
            string strValue = "";
            switch (goodsId)
            {
                case 1:
                    strValue = "Building Construction and Maintenance";
                    break;
                case 2:
                    strValue = "General Goods and Services";
                    break;
                case 3:
                    strValue = "ICT";
                    break;
                case 4:
                    strValue = "Medical";
                    break;
                case 5:
                    strValue = "Social Services";
                    break;
                case 6:
                    strValue = "Transport Infrastructure & Services";
                    break;
            }
            return strValue;
        }

        public static string GetJobLevel(int levelId)
        {
            string strValue = "";
            switch (levelId)
            {
                case 1:
                    strValue = "Graduate";
                    break;
                case 2:
                    strValue = "Officer";
                    break;
                case 3:
                    strValue = "Advisor";
                    break;
                case 4:
                    strValue = "Senior advisor";
                    break;
                case 5:
                    strValue = "Operational Leader";
                    break;
                case 6:
                    strValue = "Director";
                    break;
                case 7:
                    strValue = "Executive Level";
                    break;
            }
            return strValue;
        }

        public static string GetLocation(int locationId)
        {
            string strValue = "";
            switch (locationId)
            {
                case 1:
                    strValue = "Brisbane";
                    break;
                case 2:
                    strValue = "South West";
                    break;
                case 3:
                    strValue = "Sunshine Coast";
                    break;
                case 4:
                    strValue = "Gold Coast";
                    break;
                case 5:
                    strValue = "Fitzroy";
                    break;
                case 6:
                    strValue = "Mackay";
                    break;
                case 7:
                    strValue = "Northern";
                    break;
                case 8:
                    strValue = "North West";
                    break;
                case 9:
                    strValue = "Wide Bay-Burnett";
                    break;
                case 10:
                    strValue = "Far North Queensland";
                    break;
                case 11:
                    strValue = "Darling Downs";
                    break;
                case 12:
                    strValue = "Far North";
                    break;
                case 13:
                    strValue = "South West Queensland";
                    break;
                case 14:
                    strValue = "Northern Queensland";
                    break;
            }
            return strValue;
        }

        public static string GetOrganisation(int agencyId)
        {
            string strValue = "";
            switch (agencyId)
            {
                case 1:
                    strValue = "Attorney-Generals Department";
                    break;
                case 2:
                    strValue = "Courts Administration Authority";
                    break;
                case 3:
                    strValue = "Department for Communities and Social Inclusion";
                    break;
                case 4:
                    strValue = "Department for Correctional Services";
                    break;
                case 5:
                    strValue = "Department for Education and Child Development";
                    break;
                case 6:
                    strValue = "Department of Environment Water and Natural Resources";
                    break;
                case 7:
                    strValue = "Department of Planning Transport and Infrastructure";
                    break;
                case 8:
                    strValue = "Department of State Development";
                    break;
                case 9:
                    strValue = "Department of the Premier and Cabinet";
                    break;
                case 10:
                    strValue = "Department of Treasury and Finance";
                    break;
                case 11:
                    strValue = "Primary Industries and Regions SA";
                    break;
                case 12:
                    strValue = "SA Fire and Emergency Services Commission";
                    break;
                case 13:
                    strValue = "SA Health";
                    break;
                case 14:
                    strValue = "South Australia Police";
                    break;
                case 15:
                    strValue = "South Australian Tourism Commission";
                    break;
                case 16:
                    strValue = "TAFE SA";
                    break;
                case 17:
                    strValue = "Department for Child Protection";
                    break;
                case 18:
                    strValue = "SA Water";
                    break;
            }
            return strValue;
        }

        public static string IsCentralProcurement(int id)
        {
            string strValue = "";
            switch (id)
            {
                case 1:
                    strValue = "Yes";
                    break;
                case 2:
                    strValue = "No- Operational";
                    break;
                case 3:
                    strValue = "No- Regional";
                    break;
                case 4:
                    strValue = "No- Other";
                    break;
                default:
                    strValue = "No Value Selected";
                    break;
            }
            return strValue;
        }

        public static string GetJobClassification(int id)
        {
            string strValue = "";
            switch (id)
            {
                case 1:
                    strValue = "ASO2";
                    break;
                case 2:
                    strValue = "ASO3";
                    break;
                case 3:
                    strValue = "ASO4";
                    break;
                case 4:
                    strValue = "ASO5";
                    break;
                case 5:
                    strValue = "ASO6/ MAS1";
                    break;
                case 6:
                    strValue = "ASO7/ MAS2";
                    break;
                case 7:
                    strValue = "ASO8/ MAS3";
                    break;
                case 8:
                    strValue = "EX";
                    break;
                case 9:
                    strValue = "Other (e.g. technical grades)";
                    break;
                default:
                    strValue = "No Value Selected";
                    break;
            }
            return strValue;
        }

        public static string GetExperience(int id)
        {
            string strValue = "";
            switch (id)
            {
                case 1:
                    strValue = "Less than 1 year";
                    break;
                case 2:
                    strValue = "1 to 3 years";
                    break;
                case 3:
                    strValue = "3 to 5 years";
                    break;
                case 4:
                    strValue = "5 to 10 years";
                    break;
                case 5:
                    strValue = "10 to 15 years";
                    break;
                case 6:
                    strValue = "15 to 20 years";
                    break;
                case 7:
                    strValue = "More than 20 years";
                    break;
                default:
                    strValue = "No Value Selected";
                    break;
            }
            return strValue;
        }

        public static string GetWorkingHours(int id)
        {
            string strValue = "";
            switch (id)
            {
                case 1:
                    strValue = "Full-time";
                    break;
                case 2:
                    strValue = "Part-time less than 50%";
                    break;
                case 3:
                    strValue = "Part-time greater than 50%";
                    break;
                case 4:
                    strValue = "Part-time less than 25%";
                    break;
                default:
                    strValue = "No Value Selected";
                    break;
            }
            return strValue;
        }
        public static string GetNature(int id)
        {
            string strValue = "";
            switch (id)
            {
                case 1:
                    strValue = "General Goods and Services";
                    break;
                case 2:
                    strValue = "ICT";
                    break;
                case 3:
                    strValue = "Medical / Health";
                    break;
                case 4:
                    strValue = "Social Services";
                    break;
                case 5:
                    strValue = "Transport Infrastructure & Services";
                    break;
                case 6:
                    strValue = "Building Construction and Maintenance";
                    break;              
                default:
                    strValue = "No Value Selected";
                    break;
            }
            return strValue;
        }

        public static string GetSize(int id)
        {
            string strValue = "";
            switch (id)
            {
                case 1:
                    strValue = "$1 billion or more";
                    break;
                case 2:
                    strValue = "$500 million to $999.9 million";
                    break;
                case 3:
                    strValue = "$100 million to $499.9 million";
                    break;
                case 4:
                    strValue = "$50 million to $99.9 million";
                    break;
                case 5:
                    strValue = "$20 million to $49.9 million";
                    break;
                case 6:
                    strValue = "$10 million to $19.9 million";
                    break;
                case 7:
                    strValue = "$5 million to $9.9 million";
                    break;
                case 8:
                    strValue = "$2.5 million to $4.9 million";
                    break;
                case 9:
                    strValue = "$1 million to $2.49 million";
                    break;
                case 10:
                    strValue = "$500,000 to $999.999";
                    break;
                case 11:
                    strValue = "Less than $500,000";
                    break;
                case 12:
                    strValue = "Not Applicable";
                    break;
                default:
                    strValue = "No Value Selected";
                    break;
            }
            return strValue;
        }

        public static string GetNoOfContracts(int id)
        {
            string strValue = "";
            switch (id)
            {
                case 1:
                    strValue = "1 to 5";
                    break;
                case 2:
                    strValue = "5 to 10";
                    break;
                case 3:
                    strValue = "10 to 20";
                    break;
                case 4:
                    strValue = "20 plus";
                    break;
                case 5:
                    strValue = "Not relevant";
                    break;
                default:
                    strValue = "No Value Selected";
                    break;
            }
            return strValue;
        }

        public static string GetRange(int id)
        {
            string strValue = "";
            switch (id)
            {
                case 1:
                    strValue = "Operational tasks like routing non-matched invoices or other administrative tasks";
                    break;
                case 2:
                    strValue = "Managing contracts and dealing with operational problems that arise";
                    break;
                case 3:
                    strValue = "Operational procurement tasks like raising purchase orders for end users";
                    break;
                case 4:
                    strValue = "Tactical procurement activities, like issuing and evaluating quotes for simple acquisitions";
                    break;
                case 5:
                    strValue = "Strategic procurement activities, like managing procurement projects for complex and/or high value acquisitions";
                    break;
                case 6:
                    strValue = "Managing significant contracts and ensuring that outcomes are realised";
                    break;
                case 7:
                    strValue = "Policy, governance or other managerial tasks, such as supporting governance bodies and/or reporting";
                    break;
                case 8:
                    strValue = "Procurement leadership, setting goals and direction and leading a team of procurement practitioners";
                    break;
              
                default:
                    strValue = "Not relevant";
                    break;
            }
            return strValue;
        }
    }

    public class Topic
    {
        private int _topicId;

        private string _topicTitle;

        public int topicId
        {
            get
            {
                return this._topicId;
            }
            set
            {
                this._topicId = value;
            }
        }

        public string topicTitle
        {
            get
            {
                return this._topicTitle;
            }
            set
            {
                this._topicTitle = value;
            }
        }
    }

    public class User
    {
        private int _userId;

        private string _name;

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

        public string name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }
    }

    public static class Quartiles
    {
        public static double FirstQuartile(System.Collections.Generic.IEnumerable<double> list)
        {
            return Quartiles.GetQuartileExcel(list, 0.25);
        }

        public static double ThirdQuartile(System.Collections.Generic.IEnumerable<double> list)
        {
            return Quartiles.GetQuartileExcel(list, 0.75);
        }

        public static double MiddleQuartile(System.Collections.Generic.IEnumerable<double> list)
        {
            return Quartiles.GetQuartileExcel(list, 0.5);
        }

        private static double GetQuartileExcel(System.Collections.Generic.IEnumerable<double> list, double quartile)
        {
            int count = list.Count<double>();
            double index = (double)(count - 1) * quartile;
            double decimalPart = index % 1.0;
            int integerPart = (int)index;
            double result = (1.0 - decimalPart) * list.ElementAt(integerPart) + decimalPart * list.ElementAt(integerPart + 1);
            return System.Math.Round(result, 2);
        }
    }
}
