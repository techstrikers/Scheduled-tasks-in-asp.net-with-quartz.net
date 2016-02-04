using Quartz;
using System.IO;
using System.Net.Mail;
using System.Net;
using System;
using System.Configuration;
using System.Web;
using System.Web.UI;


namespace QuartZExample
{
    public class EmailJob : IJob
    {
        /// <summary>
        /// Automatically execute this function by Quartz.Net
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            this.SendHtmlFormattedEmail("youremail@domail.com", "SchedulerEmail", this.PopulateBody());
        }

        /// <summary>
        /// Sending Scheduler email to user using Quartz.Net
        /// </summary>
        /// <param name="recepientEmail"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["MailAddress"]);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(recepientEmail));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["Host"];
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
                NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                smtp.Send(mailMessage);
            }
        }
        /// <summary>
        /// Return HTML template body
        /// </summary>
        /// <returns></returns>
        private string PopulateBody()
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/TestMail.htm")))
            {
                body = reader.ReadToEnd();
            }

            return body;
        }
    }
}