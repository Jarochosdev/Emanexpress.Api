using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Emanexpress.API.Business.Email
{
    public class EmailSender
    {        
        public SmtpClient SmtpClient { get; }
        public EmailSenderConfiguration EmailSenderConfiguration { get; }

        public EmailSender(SmtpClient smtpClient, EmailSenderConfiguration emailSenderConfiguration)
        {     
            SmtpClient = smtpClient;
            EmailSenderConfiguration = emailSenderConfiguration;
        }

        public async Task SendEmailAsync(string emailTo, string subject, string body, bool isBodyHtml = false)
        {            
            MailMessage message = new MailMessage(EmailSenderConfiguration.UserName, emailTo, subject, body);
            message.IsBodyHtml = isBodyHtml;
            if(!string.IsNullOrWhiteSpace(EmailSenderConfiguration.EmailBcc))
            {
                message.Bcc.Add(new MailAddress(EmailSenderConfiguration.EmailBcc));
            }
            

            SmtpClient.Credentials = new NetworkCredential(EmailSenderConfiguration.UserName, EmailSenderConfiguration.Password);

            await SmtpClient.SendMailAsync(message);
        }
    }
}
