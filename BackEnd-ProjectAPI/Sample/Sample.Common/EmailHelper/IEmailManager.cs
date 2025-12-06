using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Common.EmailHelper
{
    public interface IEmailManager
    {
        EmailSender FromAddress { set; }
        string ToAddress { set; }
        string Subject { get; set; }
        string Body { get; set; }
        string EmailTemplate { get; set; }
        Dictionary<string, string> EmailData { set; }
        void AddToAddress(string email);
        void AddCcAddress(string email);
        void AddBccAddress(string email);
        void AddAttachment(string file);
        void AddAttachment(Attachment objAttachment);
        void SendMail();
        void SendMail(EmailSender FromAddress, string ToAddress, string Subject, string Body);
        void SendMail(EmailSender FromAddress, string ToAddress, string Subject, string EmailTemplate, Dictionary<string, string> EmailData);
    }
}
