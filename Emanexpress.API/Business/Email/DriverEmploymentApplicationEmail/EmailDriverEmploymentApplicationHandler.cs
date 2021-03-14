using Emanexpress.API.DataTransferObjects;
using Emanexpress.API.Models;
using System.Threading.Tasks;

namespace Emanexpress.API.Business.Email
{
    public class EmailDriverEmploymentApplicationHandler
    {
        public EmailSender EmailSender { get; }
        public DriverEmploymentEmailTableFactory DriverEmploymentEmailTableFactory { get; }
        public DriverApplicationEmailReceiverConfiguration DriverApplicationEmailReceiverConfiguration { get; }

        public EmailDriverEmploymentApplicationHandler(EmailSender emailSender, DriverEmploymentEmailTableFactory driverEmploymentEmailTableFactory, DriverApplicationEmailReceiverConfiguration driverApplicationEmailReceiverConfiguration)
        {
            EmailSender = emailSender;
            DriverEmploymentEmailTableFactory = driverEmploymentEmailTableFactory;
            DriverApplicationEmailReceiverConfiguration = driverApplicationEmailReceiverConfiguration;
        }

        public async Task SendToAdministratorAsync(DtoDriverEmploymentApplication driverEmploymentApplication)
        {            
            var driverEmailBuilder = new HtmlDriverEmailbuilder();
            
            var applicatInformationTable = DriverEmploymentEmailTableFactory.GetEmailTable(driverEmploymentApplication, DriverEmploymentApplicationEmailTableType.ApplicantInformation);
            var addressTable = DriverEmploymentEmailTableFactory.GetEmailTable(driverEmploymentApplication, DriverEmploymentApplicationEmailTableType.Address);
            var employmentHistoryTable = DriverEmploymentEmailTableFactory.GetEmailTable(driverEmploymentApplication, DriverEmploymentApplicationEmailTableType.EmploymentHistory);
            var accidentRecordsTable =DriverEmploymentEmailTableFactory.GetEmailTable(driverEmploymentApplication, DriverEmploymentApplicationEmailTableType.AccidentRecords);
            var trafficConvictionsTable = DriverEmploymentEmailTableFactory.GetEmailTable(driverEmploymentApplication, DriverEmploymentApplicationEmailTableType.TrafficConvictions);
            var licenseHistoryTable = DriverEmploymentEmailTableFactory.GetEmailTable(driverEmploymentApplication, DriverEmploymentApplicationEmailTableType.LicenseHistory);
            var experienceQualificationTable = DriverEmploymentEmailTableFactory.GetEmailTable(driverEmploymentApplication, DriverEmploymentApplicationEmailTableType.ExperienceQualifications);

            driverEmailBuilder.AddTable(applicatInformationTable);
            driverEmailBuilder.AddTable(addressTable);
            driverEmailBuilder.AddTable(employmentHistoryTable);
            driverEmailBuilder.AddTable(accidentRecordsTable);
            driverEmailBuilder.AddTable(trafficConvictionsTable);
            driverEmailBuilder.AddTable(licenseHistoryTable);
            driverEmailBuilder.AddTable(experienceQualificationTable);

            var driverApplicationemploymentBody =  driverEmailBuilder.Build();
            
            var driverName = driverEmploymentApplication.FirstName + 
                driverEmploymentApplication.MiddleName + 
                driverEmploymentApplication.LastName;
            
            await EmailSender.SendEmailAsync(DriverApplicationEmailReceiverConfiguration.Email,
                "You have a new Driver Employment Application [" + driverName + "] ", 
                driverApplicationemploymentBody, true);
        }
    }
}
