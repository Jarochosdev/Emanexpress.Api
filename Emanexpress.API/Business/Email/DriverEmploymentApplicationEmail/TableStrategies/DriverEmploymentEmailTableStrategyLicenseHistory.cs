using Emanexpress.API.Business.Email.Common.HtmlStructure;
using Emanexpress.API.Converter;
using Emanexpress.API.DataTransferObjects;
using System;

namespace Emanexpress.API.Business.Email
{
    public class DriverEmploymentEmailTableStrategyLicenseHistory : IDriverEmploymentEmailTableStrategy
    {
        public DriverEmploymentApplicationEmailTableType emailTableType => DriverEmploymentApplicationEmailTableType.LicenseHistory;

        public ConverterHelper ConverterHelper { get; }

        public DriverEmploymentEmailTableStrategyLicenseHistory(ConverterHelper converterHelper)
        {
            ConverterHelper = converterHelper;
        }

        public EmailTable GetEmailTable(DtoDriverEmploymentApplication driverEmploymentApplication)
        {
            var licenseHistoryTable = new EmailTable("License History");

            if(driverEmploymentApplication.LicenseHistory != null)
            {
                foreach(var licenseHistory in driverEmploymentApplication.LicenseHistory)
                {                  
                    var state = new EmailRowFieldTable("State", licenseHistory.State);
                    var licenseNo = new EmailRowFieldTable("License No.", licenseHistory.LicenseNumber);
                    var licenseClass = new EmailRowFieldTable("Class",licenseHistory.Class);
                    var endorsement = new EmailRowFieldTable("Endorsement",licenseHistory.Endorsement);
                    var expiration = new EmailRowFieldTable("Expiration", ConverterHelper.ToDateString(licenseHistory.Expiration));
                    licenseHistoryTable.AddRow(state, licenseNo, licenseClass, endorsement, expiration);                    
                }
            }

            licenseHistoryTable.TitleSeparator("");

            var deniedLicenses = new EmailRowFieldTable("Have you been denied a license, permit or privilege to operate a motor vehicle?", ConverterHelper.ToYesNo(driverEmploymentApplication.HaveYouBeenDeniedALicense));            
            licenseHistoryTable.AddRow(deniedLicenses);

            var suspendedLicense = new EmailRowFieldTable("Has any license, permit or privilege ever been suspended or revoked?", ConverterHelper.ToYesNo(driverEmploymentApplication.HasAnyLicenseBeenSuspended));            
            licenseHistoryTable.AddRow(suspendedLicense);

            var suspendedLicenseDetails = new EmailRowFieldTable("Details", driverEmploymentApplication.LicenseSuspendedDetail);            
            licenseHistoryTable.AddRow(suspendedLicenseDetails);
            
            return licenseHistoryTable;
        }       
    }
}