using DataTier;
using SGA.App_Code;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI;

namespace SGA.tna
{
    public partial class Help : Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
        }

        [WebMethod]
        public static string SaveHelpInfo(string subject, string description, int helpType)
        {
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spSaveHelp", new SqlParameter[]
			{
				new SqlParameter("@subject", subject),
				new SqlParameter("@description", description),
				new SqlParameter("@helpType", helpType),
				new SqlParameter("@insDt", System.DateTime.UtcNow.ToString()),
				new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
			});
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetProfileDetails", new SqlParameter[]
			{
				new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
			}).Tables[0];
            if (dt.Rows.Count > 0)
            {
                string emailsubject = "";
                string body = "";
                SGACommon.GetEmailTemplate(12, ref emailsubject, ref body);
                string help = "";
                switch (helpType)
                {
                    case 1:
                        help = "Technical support";
                        break;
                    case 2:
                        help = "My results";
                        break;
                    case 3:
                        help = "Assessment questions";
                        break;
                    case 4:
                        help = "Other";
                        break;
                }
                body = body.Replace("@v0", dt.Rows[0]["firstName"].ToString()).Replace("@v1", subject).Replace("@v2", help).Replace("@v3", description).Replace("@v4", dt.Rows[0]["email"].ToString());
                MailSending.SendMail(ConfigurationManager.AppSettings["nameDisplay"].ToString(), ConfigurationManager.AppSettings["UserName"].ToString(), ConfigurationManager.AppSettings["UserName"].ToString(), emailsubject, body, "");
            }
            return "s";
        }
    }
}
