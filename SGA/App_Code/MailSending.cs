using DataTier;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace SGA.App_Code
{
    public class MailSending
    {
        public static bool SendMail(string displayName, string FromAddress, string ToAddress, string Subject, string Message, string ccAddress)
        {
            bool result;
            try
            {
                MailMessage message = new MailMessage();
                if (ccAddress.Length > 0)
                {
                    message.Bcc.Add(ccAddress);
                }
                message.To.Add(ToAddress);
                message.From = new MailAddress(FromAddress, displayName);
                message.Subject = Subject;
                message.Body = Message;
                message.IsBodyHtml = true;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.ReplyTo = new MailAddress(ConfigurationManager.AppSettings["replyTo"].ToString());
                message.Priority = MailPriority.Normal;
                SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["smtpMailDomain"].ToString());
                client.EnableSsl = System.Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"].ToString());
                client.Port = System.Convert.ToInt32(ConfigurationManager.AppSettings["smtpPortNo"].ToString());
                client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["UserName"].ToString(), ConfigurationManager.AppSettings["Password"].ToString());
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spEmailTracking", new SqlParameter[]
				{
					new SqlParameter("@emailReceiver", ToAddress),
					new SqlParameter("@emailBody", Message),
					null,
					new SqlParameter("@subject", Subject),
					new SqlParameter("@sendDate", System.DateTime.UtcNow),
					new SqlParameter("@emailFrom", ConfigurationManager.AppSettings["UserName"].ToString()),
					new SqlParameter("@flag", "1")
				});
                client.Send(message);
                result = true;
            }
            catch (System.Exception ex)
            {
                result = false;
            }
            return result;
        }

        public static bool SendMailWithAttachment(string FromAddress, string ToAddress, string Subject, string Message, string filePath)
        {
            Attachment data = new Attachment(filePath);
            ContentDisposition disposition = data.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(filePath);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(filePath);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(filePath);
            bool result;
            try
            {
                MailMessage message = new MailMessage();
                message.To.Add(ToAddress);
                message.From = new MailAddress(FromAddress, ConfigurationManager.AppSettings["nameDisplay"].ToString());
                message.Subject = Subject;
                message.Body = Message;
                message.IsBodyHtml = true;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.ReplyTo = new MailAddress(ConfigurationManager.AppSettings["replyTo"].ToString());
                message.Attachments.Add(data);
                message.Priority = MailPriority.Normal;
                SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["smtpMailDomain"].ToString());
                client.Timeout = 100000;
                client.EnableSsl = System.Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"].ToString());
                client.Port = System.Convert.ToInt32(ConfigurationManager.AppSettings["smtpPortNo"].ToString());
                client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["UserName"].ToString(), ConfigurationManager.AppSettings["Password"].ToString());
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spEmailTracking", new SqlParameter[]
				{
					new SqlParameter("@emailReceiver", ToAddress),
					new SqlParameter("@emailBody", Message),
					null,
					new SqlParameter("@subject", Subject),
					new SqlParameter("@sendDate", System.DateTime.UtcNow),
					new SqlParameter("@emailFrom", ConfigurationManager.AppSettings["UserName"].ToString()),
					new SqlParameter("@flag", "1")
				});
                client.Send(message);
                data.Dispose();
                result = true;
            }
            catch (System.Exception ex)
            {
                data.Dispose();
                throw ex;
            }
            return result;
        }
    }
}
