using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Common.EmailHelper
{
    public class EmailManager : IEmailManager
    {
        private MailMessage _message = null;
        private SmtpClient _smtpClient = null;
        private MailAddress _fromAddress;
        private List<MailAddress> _toAddress;
        private ListDictionary _emailData;

        public EmailSender FromAddress 
        {
            set { _fromAddress = new MailAddress(value.EmailAddress, value.Name); } 
        }
        
        public string ToAddress 
        { 
            set { AddListToAddress(value); } 
        }

        public string Subject { get; set; }
        public string Body { get; set; }
        public string EmailTemplate { get; set; }
        public Dictionary<string, string> EmailData 
        {
            set 
            {
                _emailData = new ListDictionary();
                foreach (var item in value)
                {
                    _emailData.Add(item.Key, string.IsNullOrEmpty(item.Value) ? "" : item.Value);
                }
            } 
        }

        public EmailManager()
        {
            _smtpClient = new SmtpClient();
            _message = new MailMessage();
        }

        //public EmailManager(bool IsConfigFromAppSettings) 
        //    : this()
        //{
        //    if (IsConfigFromAppSettings)
        //    {
        //        _smtpClient.Host = ConfigurationManager.AppSettings["SmtpHost"];  //Configure as your email provider
        //        _smtpClient.UseDefaultCredentials = false;
        //        _smtpClient.EnableSsl = true;//comment if you don't need SSL  
        //        _smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SmtpUserName"], ConfigurationManager.AppSettings["SmtpPassword"]);
        //        _smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SmptPort"]);
        //    }
        //}

        //public EmailManager(string host, int port, string userName, string password, bool ssl)
        //    : this()
        //{
        //    _smtpClient.Host = host;
        //    _smtpClient.Port = port;
        //    _smtpClient.EnableSsl = ssl;
        //    _smtpClient.Credentials = new NetworkCredential(userName, password);
        //}

        public void AddToAddress(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                email = email.Replace(",", ";");
                string[] emailList = email.Split(';');
                for (int i = 0; i < emailList.Length; i++)
                {
                    if (!string.IsNullOrEmpty(emailList[i]))
                        _message.To.Add(new MailAddress(emailList[i]));
                }
            }
        }

        private void AddListToAddress(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                _toAddress = new List<MailAddress>();
                email = email.Replace(",", ";");
                string[] emailList = email.Split(';');
                for (int i = 0; i < emailList.Length; i++)
                {
                    if (!string.IsNullOrEmpty(emailList[i]))
                        _toAddress.Add(new MailAddress(emailList[i]));
                }
            }
        }

        public void AddCcAddress(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                email = email.Replace(",", ";");
                string[] emailList = email.Split(';');
                for (int i = 0; i < emailList.Length; i++)
                {
                    if (!string.IsNullOrEmpty(emailList[i]))
                        _message.CC.Add(new MailAddress(emailList[i]));
                }
            }
        }

        public void AddBccAddress(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                email = email.Replace(",", ";");
                string[] emailList = email.Split(';');
                for (int i = 0; i < emailList.Length; i++)
                {
                    if (!string.IsNullOrEmpty(emailList[i]))
                        _message.Bcc.Add(new MailAddress(emailList[i]));
                }
            }
        }

        public void AddAttachment(string file)
        {
            Attachment attachment = new Attachment(file, GetFileMimeType(file));
            _message.Attachments.Add(attachment);
        }

        public void AddAttachment(Attachment objAttachment)
        {
            _message.Attachments.Add(objAttachment);
        }

        private string GetEmailTemplate()
        {
            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "bin", "EmailHelper", $"{EmailTemplate}.html");

            if (!File.Exists(templatePath))
                throw new FileNotFoundException($"Email template not found: {templatePath}");

            string body = File.ReadAllText(templatePath);

            // Replace tokens (e.g., {{Name}}) using the values from _emailData
            foreach (DictionaryEntry item in _emailData)
            {
                body = body.Replace($"{{{{{item.Key}}}}}", item.Value?.ToString() ?? string.Empty);
            }

            return body;
        }

        public void SendMail()
        {
            if (_fromAddress == null || (_fromAddress != null && string.IsNullOrEmpty(_fromAddress.Address)))
            {
                throw new Exception("Email Dispatcher : [From Address] is not defined");
            }
            else
            {
                if (string.IsNullOrEmpty(_fromAddress.DisplayName))
                    _fromAddress = new MailAddress(_fromAddress.Address, string.Empty);
                _message.From = _fromAddress;
            }

            if (_toAddress != null)
            {
                _toAddress.ForEach(x => _message.To.Add(x));
            }

            if (_message.To.Count <= 0)
                throw new Exception("Email Dispatcher : [To address] not defined");

            _message.Subject = Subject;
            _message.IsBodyHtml = true;

            if (!string.IsNullOrEmpty(EmailTemplate) && _emailData != null)
                _message.Body = GetEmailTemplate();
            else
                _message.Body = Body;

            try
            {
                _smtpClient.Send(_message);
            }
            catch (SmtpException ex)
            {
                throw ex;
            }
            finally
            {
                _message.Dispose();
            }
        }

        public void SendMail(EmailSender FromAddress, string ToAddress, string Subject, string Body)
        {
            this.FromAddress = FromAddress;
            this.ToAddress = ToAddress;
            this.Subject = Subject;
            this.Body = Body;
            SendMail();
        }

        public void SendMail(EmailSender FromAddress, string ToAddress, string Subject, string EmailTemplate, Dictionary<string, string> EmailData)
        {
            this.FromAddress = FromAddress;
            this.ToAddress = ToAddress;
            this.Subject = Subject;
            this.EmailTemplate = EmailTemplate;
            this.EmailData = EmailData;
            SendMail();
        }

        private static string GetFileMimeType(string fileName)
        {
            string fileExt = Path.GetExtension(fileName.ToLower());
            string mimeType = string.Empty;
            switch (fileExt)
            {
                case ".htm":
                case ".html":
                    mimeType = "text/html";
                    break;
                case ".xml":
                    mimeType = "text/xml";
                    break;
                case ".jpg":
                case ".jpeg":
                    mimeType = "image/jpeg";
                    break;
                case ".gif":
                    mimeType = "image/gif";
                    break;
                case ".png":
                    mimeType = "image/png";
                    break;
                case ".bmp":
                    mimeType = "image/bmp";
                    break;
                case ".pdf":
                    mimeType = "application/pdf";
                    break;
                case ".doc":
                    mimeType = "application/msword";
                    break;
                case ".docx":
                    mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                case ".xls":
                    mimeType = "application/x-msexcel";
                    break;
                case ".xlsx":
                    mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                case ".csv":
                    mimeType = "application/csv";
                    break;
                case ".ppt":
                    mimeType = "application/vnd.ms-powerpoint";
                    break;
                case ".pptx":
                    mimeType = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                    break;
                case ".rar":
                    mimeType = "application/x-rar-compressed";
                    break;
                case ".zip":
                    mimeType = "application/x-zip-compressed";
                    break;
                default:
                    mimeType = "text/plain";
                    break;
            }
            return mimeType;
        }
    }

    public class EmailSender
    {
        public string EmailAddress { get; set; }
        public string Name { get; set; }

        public EmailSender(string EmailAddress, string? Name = null)
        {
            this.EmailAddress = EmailAddress;
            this.Name = Name == null ? "" : Name;
        }
    }
}
