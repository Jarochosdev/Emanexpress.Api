using Emanexpress.API.DataTransferObjects;
using System;

namespace Emanexpress.API.Business.Email
{
    public class DriverEmploymentEmailTableStrategyAddress : IDriverEmploymentEmailTableStrategy
    {
        public DriverEmploymentApplicationEmailTableType emailTableType => DriverEmploymentApplicationEmailTableType.Address;

        public EmailTable GetEmailTable(DtoDriverEmploymentApplication driverEmploymentApplication)
        {
            var applicatInformationTable = new EmailTable("Aplicant Information");
            
            var firstName = new EmailRowFieldTable("First Name", driverEmploymentApplication.FirstName);
            var middleName = new EmailRowFieldTable("Middle Name", driverEmploymentApplication.MiddleName);
            var lastName = new EmailRowFieldTable("Last Name",driverEmploymentApplication.LastName);
            applicatInformationTable.AddRow(firstName, middleName, lastName);            
            
            var phoneNumber = new EmailRowFieldTable("Phone Number", driverEmploymentApplication.Phone);
            var driverEmail = new EmailRowFieldTable("Middle Name", driverEmploymentApplication.DriverEmail);
            applicatInformationTable.AddRow(phoneNumber, driverEmail);
            applicatInformationTable.TitleSeparator("Have you ever worked for this company before?");
            //applicatInformationTable.AddRow(where, dateFrom, dateTo, rateOfPay);

            return applicatInformationTable;
        }
    }
}