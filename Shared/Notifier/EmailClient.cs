using System;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using log4net;
using Shared.Logger;

namespace Shared.Notifier
{
    public class EmailClient
    {
        /// <summary>
        /// Sends a notification email out about actions taken by the service.
        /// </summary>
        /// <param name="automatedEmail"></param>
        /// <param name="loggingInstance"></param>
        /// <returns></returns>
        public bool SendNotification(AutomatedEmail automatedEmail, ILog loggingInstance)
        {
            try
            {
                var senderAddress = ConfigurationManager.AppSettings["EmailSenderAddress"];

                var receiverAddressList = automatedEmail.RecipientList();

                var smtpHost = ConfigurationManager.AppSettings["EmailSmtpHost"];
                var password = Encoding.UTF8.GetString(Convert.FromBase64String(ConfigurationManager.AppSettings["EmailPassword"]));

                var mail = new MailMessage
                {
                    From = new MailAddress(senderAddress)
                };
                foreach (var receiverAddress in receiverAddressList)
                {
                    mail.To.Add(receiverAddress);
                }
                foreach (var ccAddress in automatedEmail.CcList())
                {
                    mail.CC.Add(ccAddress);
                }
                foreach (var bccAddress in automatedEmail.BccList())
                {
                    mail.Bcc.Add(bccAddress);
                }
                var client = new SmtpClient(smtpHost, 587);
                mail.IsBodyHtml = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(senderAddress, password);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                mail.Subject = automatedEmail.EmailSubject;
                mail.Body = automatedEmail.EmailBody;
                mail.Priority = automatedEmail.Priority;

                client.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                AppLogger.LogException(loggingInstance, ex.Message, ex);
                return false;
            }
        }
    }
}

