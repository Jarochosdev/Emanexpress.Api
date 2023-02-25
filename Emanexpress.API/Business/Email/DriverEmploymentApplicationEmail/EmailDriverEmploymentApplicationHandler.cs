using Emanexpress.API.Business.Configurations;
using Emanexpress.API.Business.Email.Common;
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
        public StylishBodyEmailBuilderFactory StylishBodyEmailBuilderFactory { get; }
        public EmailValidator EmailValidator { get; }
        WebSiteConfiguration WebSiteConfiguration { get; }

        public EmailDriverEmploymentApplicationHandler(EmailSender emailSender, DriverEmploymentEmailTableFactory driverEmploymentEmailTableFactory, DriverApplicationEmailReceiverConfiguration driverApplicationEmailReceiverConfiguration, StylishBodyEmailBuilderFactory stylishBodyEmailBuilderFactory, EmailValidator emailValidator, WebSiteConfiguration webSiteConfiguration)
        {
            EmailSender = emailSender;
            DriverEmploymentEmailTableFactory = driverEmploymentEmailTableFactory;
            DriverApplicationEmailReceiverConfiguration = driverApplicationEmailReceiverConfiguration;
            StylishBodyEmailBuilderFactory = stylishBodyEmailBuilderFactory;
            EmailValidator = emailValidator;
            WebSiteConfiguration = webSiteConfiguration;
        }

        public async Task HandleAsync(DtoDriverEmploymentApplication driverEmploymentApplication)
        {            
            await HandleAdminEmail(driverEmploymentApplication);
            await HandleRequesterEmail(driverEmploymentApplication);           
        }

        private async Task HandleRequesterEmail(DtoDriverEmploymentApplication driverEmploymentApplication)
        {
            if(EmailValidator.IsValidEmail(driverEmploymentApplication.DriverEmail))
            { 
                var stylishBodyEmailBuilder = StylishBodyEmailBuilderFactory.GetBuilder();
                
                stylishBodyEmailBuilder.AddNewLine("Hello " + driverEmploymentApplication.FirstName + "!", fontSizeInPixels: 28, margingInPixels: 15, color: "#002054");
                stylishBodyEmailBuilder.AddNewLine($"Thanks for taking the time to apply for our driver position. We appreciate your interest in {WebSiteConfiguration.CompanyName}.", fontSizeInPixels: 18, margingInPixels: 10);
                stylishBodyEmailBuilder.AddNewLine("If you are selected to continue to the interview process, our human resources department will be in contact with you as soon as possible.", fontSizeInPixels: 18, margingInPixels: 20);

                await EmailSender.SendEmailAsync(
                    driverEmploymentApplication.DriverEmail,
                    "Thank you for your application",
                    stylishBodyEmailBuilder.GetBody(), true);
            }
        }

        private async Task HandleAdminEmail(DtoDriverEmploymentApplication driverEmploymentApplication)
        {
             var driverEmailBuilder = new HtmlBodyEmailBuilder();
            
            var forCompanyUseTable = DriverEmploymentEmailTableFactory.GetEmailTable(driverEmploymentApplication, DriverEmploymentApplicationEmailTableType.ForCompanyUse);
            var terminationOfEmploymentTable = DriverEmploymentEmailTableFactory.GetEmailTable(driverEmploymentApplication, DriverEmploymentApplicationEmailTableType.TerminationOfEmployment);
            var applicatInformationTable = DriverEmploymentEmailTableFactory.GetEmailTable(driverEmploymentApplication, DriverEmploymentApplicationEmailTableType.ApplicantInformation);
            var addressTable = DriverEmploymentEmailTableFactory.GetEmailTable(driverEmploymentApplication, DriverEmploymentApplicationEmailTableType.Address);
            var employmentHistoryTable = DriverEmploymentEmailTableFactory.GetEmailTable(driverEmploymentApplication, DriverEmploymentApplicationEmailTableType.EmploymentHistory);
            var accidentRecordsTable =DriverEmploymentEmailTableFactory.GetEmailTable(driverEmploymentApplication, DriverEmploymentApplicationEmailTableType.AccidentRecords);
            var trafficConvictionsTable = DriverEmploymentEmailTableFactory.GetEmailTable(driverEmploymentApplication, DriverEmploymentApplicationEmailTableType.TrafficConvictions);
            var licenseHistoryTable = DriverEmploymentEmailTableFactory.GetEmailTable(driverEmploymentApplication, DriverEmploymentApplicationEmailTableType.LicenseHistory);
            var drivingExperience = DriverEmploymentEmailTableFactory.GetEmailTable(driverEmploymentApplication, DriverEmploymentApplicationEmailTableType.DrivingExperience);
            var experienceQualificationTable = DriverEmploymentEmailTableFactory.GetEmailTable(driverEmploymentApplication, DriverEmploymentApplicationEmailTableType.ExperienceQualifications);            
            var completedApplicationTable = DriverEmploymentEmailTableFactory.GetEmailTable(driverEmploymentApplication, DriverEmploymentApplicationEmailTableType.CompleteApplication);            
            var certificationOfcompliance = DriverEmploymentEmailTableFactory.GetEmailTable(driverEmploymentApplication, DriverEmploymentApplicationEmailTableType.CertificationOfCompliance);            

            driverEmailBuilder.AddTable(forCompanyUseTable);
            driverEmailBuilder.AddTable(terminationOfEmploymentTable);
            driverEmailBuilder.AddTable(applicatInformationTable);
            driverEmailBuilder.AddTable(addressTable);
            driverEmailBuilder.AddTable(employmentHistoryTable);
            driverEmailBuilder.AddTable(accidentRecordsTable);
            driverEmailBuilder.AddTable(trafficConvictionsTable);
            driverEmailBuilder.AddTable(licenseHistoryTable);
            driverEmailBuilder.AddTable(drivingExperience);            
            driverEmailBuilder.AddTable(experienceQualificationTable);
            driverEmailBuilder.AddTable(completedApplicationTable);
            driverEmailBuilder.AddTable(certificationOfcompliance);                            

            var driverApplicationemploymentBody =  driverEmailBuilder.Build();
            
            var driverName = $"{driverEmploymentApplication.FirstName} " +
                $"{driverEmploymentApplication.MiddleName} " +
                $"{driverEmploymentApplication.LastName}";

            await EmailSender.SendEmailAsync(DriverApplicationEmailReceiverConfiguration.Email,
                "You have a new Driver Employment Application [" + driverName + "] ", 
                driverApplicationemploymentBody, true);
        }
    }
}
