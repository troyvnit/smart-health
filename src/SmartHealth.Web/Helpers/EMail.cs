﻿using System;
using System.Web;
using System.Xml;
using System.Net.Mail;

namespace SmartHealth.Web.Helpers
{
    public class EMail
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string SMTPClient { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ReplyTo { get; set; }
        public string SMTPPort { get; set; }
        public Boolean isEnableSSL { get; set; }

        public void SendMail(string messageId, string[] param)
        {
            XmlDocument xdoc = new XmlDocument();
            string mailFormatxml = HttpContext.Current.Server.MapPath("\\") + "Mailformat.xml";
            string subject = "";
            string body = "";
            XmlNode mailNode = default(XmlNode);
            int n = 0;

            if ((System.IO.File.Exists(mailFormatxml)))
            {
                xdoc.Load(mailFormatxml);
                mailNode = xdoc.SelectSingleNode("MailFormats/MailFormat[@Id='" + messageId + "']");
                subject = mailNode.SelectSingleNode("Subject").InnerText;
                body = mailNode.SelectSingleNode("Body").InnerText;
                if ((param == null))
                {
                    throw new Exception("Mail format file not found.");
                }
                else
                {
                    for (n = 0; n <= param.Length - 1; n++)
                    {
                        body = body.Replace(n.ToString() + "?", param[n]);
                        subject = subject.Replace(n.ToString() + "?", param[n]);
                    }
                }
             
                dynamic MailMessage = new MailMessage();
                MailMessage.From = new MailAddress(FromAddress);
                MailMessage.To.Add(ToAddress);
                MailMessage.Subject = subject;
                MailMessage.IsBodyHtml = true;
                MailMessage.Body = body;
             
                MailMessage.ReplyTo = new MailAddress(FromAddress);

                SmtpClient SmtpClient = new SmtpClient();
                SmtpClient.Host = SMTPClient;
                SmtpClient.EnableSsl = isEnableSSL;
                SmtpClient.Port = Convert.ToInt32(SMTPPort);
                SmtpClient.Credentials = new System.Net.NetworkCredential(UserName, Password);
                try
                {
                    SmtpClient.Send(MailMessage);
                }
                catch (SmtpFailedRecipientsException ex)
                {
                    for (int i = 0; i <= ex.InnerExceptions.Length; i++)
                    {
                        SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                        if ((status == SmtpStatusCode.MailboxBusy) | (status == SmtpStatusCode.MailboxUnavailable))
                        {
                            System.Threading.Thread.Sleep(5000);
                            SmtpClient.Send(MailMessage);
                        }
                    }
                }
            }
        }
    }
}