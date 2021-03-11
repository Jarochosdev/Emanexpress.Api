using Emanexpress.API.Models;
using System.Threading.Tasks;

namespace Emanexpress.API.Business.Email
{
    public class EmailDriverEmploymentApplicationHandler
    {
        public EmailSender EmailSender { get; }

        public EmailDriverEmploymentApplicationHandler(EmailSender emailSender)
        {
            EmailSender = emailSender;
        }        

        public async Task SendToDriverAsync(IDriverEmploymentApplication driverEmploymentApplication)
        {
            var driverEmail = driverEmploymentApplication.DriverEmail;            
            await EmailSender.SendEmailAsync(driverEmail,"Thanks for your application", GetDriverEmail(driverEmploymentApplication));
        }

        private string GetDriverEmail(IDriverEmploymentApplication driverEmploymentApplication)
        {
            var driverName = driverEmploymentApplication.FirstName + " " + driverEmploymentApplication.LastName;
            return "Hi " + driverName + " we will check your application and answer very soon!!! Thanks.";
        }
    }
}
