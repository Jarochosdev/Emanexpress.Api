using Emanexpress.API.Business.Email.Common.HtmlStructure;
using Emanexpress.API.Converter;
using Emanexpress.API.DataTransferObjects;
using System;

namespace Emanexpress.API.Business.Email
{
    public class DriverEmploymentEmailTableStrategyEmploymentHistory : IDriverEmploymentEmailTableStrategy
    {
        public DriverEmploymentApplicationEmailTableType emailTableType => DriverEmploymentApplicationEmailTableType.EmploymentHistory;

         public ConverterHelper ConverterHelper { get; }

        public DriverEmploymentEmailTableStrategyEmploymentHistory(ConverterHelper converterHelper)
        {
            ConverterHelper = converterHelper;
        }

        public EmailTable GetEmailTable(DtoDriverEmploymentApplication driverEmploymentApplication)
        {
            var titleMessage ="All driver applicants to drive in interstate commerce must provide the following information on " +
                "all employers during the preceeding 2 years. List complete mailing address, street number, city, state, and zip code. " +
                "Applicants to drive a commercial motor vehicle* in intrastate or interstate commerce shall also " +
                "provide an additional 7 years' information on those employers for whom the applicant operated such vehicle." +
                " (NOTE: List employers in reverse order starting with the most recent.)";
            
            var employmentHistoryTable = new EmailTable("Employment History", titleMessage);
            
            var footerMessageP1 = "* Includes vehicles having a GVWR of 26,001 lbs. or more, vehicles designed to transport 16 " +
                "or more passengers (including the driver), or any size vehicle used to transport hazardous materials in a " +
                "quantity requiring placarding.";

            var footerMessageP2 = "† The Federal Motor Carrier Safety Regulations (FMCSRs) apply to anyone operating a " +
                "motor vehicle on a highway in interstate commerce to transport passengers or property when the vehicle: " +
                "(1) weighs or has a GVWR of 10,001 pounds or more, (2) is designed or used to transport 8 or more passengers " +
                "(including the driver), OR (3) is of any size and is used to transport hazardous materials in a quantity " +
                "requiring placarding.";

            employmentHistoryTable.AddFooterMessage(footerMessageP1);
            employmentHistoryTable.AddFooterMessage(footerMessageP2);

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
                var fromMonth = new EmailRowFieldTable("From Mo/Year", ConverterHelper.ToDateString(employment.From));
                var toMonth = new EmailRowFieldTable("To Mo/Year", ConverterHelper.ToDateString(employment.To));
                var salary = new EmailRowFieldTable("Salary", employment.Salary);
                employmentHistoryTable.AddRow(phone, fromMonth, toMonth, salary);

                var positionHeld = new EmailRowFieldTable("Position Held", employment.PositionHeld, 30);
                var reasonForLeaving = new EmailRowFieldTable("Reason for leaving", employment.ReasonForLeaving, 50);                
                employmentHistoryTable.AddRow(positionHeld, reasonForLeaving); 

                var subjectToFmcsr = new EmailRowFieldTable("Where you subject to the FMCSRs while employed?", ConverterHelper.ToYesNo(employment.SubjectToMfcsrs));
                employmentHistoryTable.AddRow(subjectToFmcsr); 

                var safeSensitiveTitle =  "Was you job designated as a safety-sensitive function " +
                    "in any dot-regulated mode subject to the drug and alcohol testing requirements of 49 CFR Part 40?";

                var safeSensitive = new EmailRowFieldTable(safeSensitiveTitle, ConverterHelper.ToYesNo(employment.SafetySensitive));
                employmentHistoryTable.AddRow(safeSensitive);
            }
                                  
            return employmentHistoryTable;
        }        
    }
}