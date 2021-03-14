using Emanexpress.API.DataTransferObjects;
using System;

namespace Emanexpress.API.Business.Email
{
    public class DriverEmploymentEmailTableStrategyLicenseHistory : IDriverEmploymentEmailTableStrategy
    {
        public DriverEmploymentApplicationEmailTableType emailTableType => DriverEmploymentApplicationEmailTableType.LicenseHistory;

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
                    var expiration = new EmailRowFieldTable("Expiration", GetDate(licenseHistory.Expiration));
                    licenseHistoryTable.AddRow(state, licenseNo, licenseClass, endorsement, expiration);                    
                }
            }

            licenseHistoryTable.TitleSeparator("");

            var deniedLicenses = new EmailRowFieldTable("Have you been denied a license, permit or privilege to operate a motor vehicle?", GetYesNo(driverEmploymentApplication.HaveYouBeenDeniedALicense));            
            licenseHistoryTable.AddRow(deniedLicenses);

            var suspendedLicense = new EmailRowFieldTable("Has any license, permit or privilege ever been suspended or revoked?", GetYesNo(driverEmploymentApplication.HasAnyLicenseBeenSuspended));            
            licenseHistoryTable.AddRow(suspendedLicense);

            var suspendedLicenseDetails = new EmailRowFieldTable("Details", driverEmploymentApplication.LicenseSuspendedDetail);            
            licenseHistoryTable.AddRow(suspendedLicenseDetails);
            
            return licenseHistoryTable;
        }

        private string GetYesNo(bool value)
        {
            if (value)
            {
                return "YES";
            }

            return "NO";
        }

        private string GetDate(DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToString("MM/dd/yyyy");
            }
            
            return "";            
        }
    }
}