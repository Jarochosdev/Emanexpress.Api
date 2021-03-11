using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Emanexpress.API.Business.Email
{
    public class EmailSender
    {        
        public SmtpClient SmtpClient { get; }

        public EmailSender(SmtpClient smtpClient)
        {     
            SmtpClient = smtpClient;
        }        

        public async Task SendEmailAsync(string emailTo, string subject, string body)
        {            
            MailMessage message = new MailMessage("adriantostega@jarochos.dev", emailTo, subject, body);
            await SmtpClient.SendMailAsync(message);
        }
    }
}
