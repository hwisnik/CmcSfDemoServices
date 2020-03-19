using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Notifier
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class AutomatedEmail
    {
        public int Id { get; set; }
        public string SendTime { get; set; }
        public string SendDate { get; set; }
        public string Description { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string EmailRecipients { get; set; }
        public string EmailCcList { get; set; }
        public string EmailBccList { get; set; }
        public MailPriority Priority { get; set; }

        public AutomatedEmail()
        {
            Priority = MailPriority.Normal;
        }

        public List<string> RecipientList()
        {
            List<string> recipientList = new List<string>();

            if (!string.IsNullOrEmpty(EmailRecipients))
            {
                recipientList = EmailRecipients.Split(new char[] { ';' }).ToList();
            }

            return recipientList;
        }

        public List<string> CcList()
        {
            List<string> ccList = new List<string>();

            if (!string.IsNullOrEmpty(EmailCcList))
            {
                ccList = EmailCcList.Split(new char[] { ';' }).ToList();
            }

            return ccList;
        }

        public List<string> BccList()
        {
            List<string> bccList = new List<string>();

            if (!string.IsNullOrEmpty(EmailBccList))
            {
                bccList = EmailBccList.Split(new char[] { ';' }).ToList();
            }

            return bccList;
        }

        public void SetPriority(string priority)
        {
            switch (priority.ToUpper())
            {
                case "HIGH":
                    Priority = MailPriority.High;
                    break;
                case "NORMAL":
                    Priority = MailPriority.Normal;
                    break;
                case "LOW":
                    Priority = MailPriority.Low;
                    break;
            }
        }
    }
}
