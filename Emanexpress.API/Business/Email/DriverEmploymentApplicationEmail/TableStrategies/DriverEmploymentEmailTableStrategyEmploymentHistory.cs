using Emanexpress.API.DataTransferObjects;
using System;

namespace Emanexpress.API.Business.Email
{
    public class DriverEmploymentEmailTableStrategyEmploymentHistory : IDriverEmploymentEmailTableStrategy
    {
        public DriverEmploymentApplicationEmailTableType emailTableType => DriverEmploymentApplicationEmailTableType.EmploymentHistory;

        public EmailTable GetEmailTable(DtoDriverEmploymentApplication driverEmploymentApplication)
        {
            var employmentHistoryTable = new EmailTable("Employment History");
            
            if(driverEmploymentApplication.EmploymentHistory == null)
            {
                return employmentHistoryTable;
            }

            foreach(var employment in driverEmploymentApplication.EmploymentHistory)
            {
                if(employment.StillWorkingHere)
                {
                    employmentHistoryTable.TitleSeparator("Current");
                }
                else
                {
                    employmentHistoryTable.TitleSeparator("Previous");
                }

                var name = new EmailRowFieldTable("Company Name", employment.CompanyName);
                var address = new EmailRowFieldTable("Address", employment.Address);
                var city = new EmailRowFieldTable("City", employment.City);
                employmentHistoryTable.AddRow(name, address, city);

                var state = new EmailRowFieldTable("State", employment.State);
                var zipCode = new EmailRowFieldTable("Zip Code", employment.Zipcode);
                var contactPerson = new EmailRowFieldTable("Contact Person", employment.ContactPerson);
                employmentHistoryTable.AddRow(state, zipCode, contactPerson);

                var phone = new EmailRowFieldTable("Phone Number", employment.PhoneNumber);
                var fromMonth = new EmailRowFieldTable("From Mo/Year", employment.FromMonthYear);
                var toMonth = new EmailRowFieldTable("To Mo/Year", employment.ToMonthYear);
                var salary = new EmailRowFieldTable("Salary", employment.Salary);
                employmentHistoryTable.AddRow(phone, fromMonth, toMonth, salary);

                var positionHeld = new EmailRowFieldTable("Position Held", employment.PositionHeld, 30);
                var reasonForLeaving = new EmailRowFieldTable("Reason for leaving", employment.ReasonForLeaving, 50);                
                employmentHistoryTable.AddRow(positionHeld, reasonForLeaving); 

                var subjectToFmcsr = new EmailRowFieldTable("Where you subject to the FMCSRs while employed?", GetYesNo(employment.SubjectToMfcsrs));
                employmentHistoryTable.AddRow(subjectToFmcsr); 

                var safeSensitiveTitle =  "Was you job designated as a safety-sensitive function " +
                    "in any dot-regulated mode subject to the drug and alcohol testing requirements of 49 CFR Part 40?";

                var safeSensitive = new EmailRowFieldTable(safeSensitiveTitle, GetYesNo(employment.SafetySensitive));
                employmentHistoryTable.AddRow(safeSensitive);
            }
                                  
            return employmentHistoryTable;
        }

        private string GetYesNo(bool value)
        {
            if (value)
            {
                return "YES";
            }

            return "NO";
        }
    }
}