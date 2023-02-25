using Emanexpress.API.Business.Email.Common.HtmlStructure;
using Emanexpress.API.Converter;
using Emanexpress.API.DataTransferObjects;
using System;

namespace Emanexpress.API.Business.Email
{
    public class DriverEmploymentEmailTableStrategyCertificationOfCompliance : IDriverEmploymentEmailTableStrategy
    {
        public DriverEmploymentApplicationEmailTableType emailTableType => DriverEmploymentApplicationEmailTableType.CertificationOfCompliance;

         public ConverterHelper ConverterHelper { get; }

        public DriverEmploymentEmailTableStrategyCertificationOfCompliance(ConverterHelper converterHelper)
        {
            ConverterHelper = converterHelper;
        }

        public EmailTable GetEmailTable(DtoDriverEmploymentApplication driverEmploymentApplication)
        {                        
            var completeApplicationTable = new EmailTable("Certification of compliance with driver license requirements");
            
            var toBeReadParagraph1 = "The requirements in Part 383 apply to every driver who operates in intrastate, interstate, or foreign commerce and operates a vehicle weighing 26001 pounds or more, can transport more than 15 people, or transport hazardous materials that require placarding.";
            var toBeReadParagraph2 = "The requirements in Part 391 apply to every driver who operates in interstate commerce and operates a vehicle weighing 10,001 pounds or more, can transport more than 15 people, or transport hazardous material that require placarding.";            
            var toBeReadParagraph3 = "DRIVER REQUIREMENTS: Parts 383 and 391 of the Federal Motor Carrier Safety Regulations contain some requirements that you as a driver must comply with. These requirements are in affect as of July 1, 1987. They are as follow:";
            var toBeReadParagraph4 = "1. POSSESS ONLY ONE LICENSE: You, as a commercial vehicle driver, may not posses more than one motor vehicle operator’s license.";
            var toBeReadParagraph5 = "If you have more than one license, keep the license from your state of residence and return the additional license to the states that issued them. DESTROYING a license does not close the record in the state that issued it; you must notify the state.";
            var toBeReadParagraph6 = "If a multiple license has been lost, stolen, or destroyed, close your record by notifying the state of issuance that you no longer want to be licensed by the state.";
            var toBeReadParagraph7 = "2. NOTIFICATION OD LICENSE SUSPENSION, REVOCATION OR CANCELLATION: Sections 392.42 and 383.33 of the Federal Motor Carrier Safety Regulations require that you notify your employer the NEXT BUSSINESS DAY of any revocation or suspension of your driver’s license.";
            var toBeReadParagraph8 = "In addition, Section 383.31 requires that any time you violate a state or local traffic law (other than parking), you must report it within 30 days to 1) your employing carrier, and 2) the state that issued your license (if the violation occurs in a state other than the one which issued your license). The notification to both the employer and state must be in writing.";

            completeApplicationTable.AddBoxMessage("TO BE READ AND SIGNED BY APPLICANT", 
                toBeReadParagraph1, 
                toBeReadParagraph2, 
                toBeReadParagraph3, 
                toBeReadParagraph4, 
                toBeReadParagraph5, 
                toBeReadParagraph6,
                toBeReadParagraph7,
                toBeReadParagraph8);

            completeApplicationTable.TitleSeparator("The following license is the only I will possess");
            var licenseNumber = new EmailRowFieldTable("License Number", driverEmploymentApplication.LicenseCertificationOfCompliance.LicenseNumber);
            var state = new EmailRowFieldTable("State", driverEmploymentApplication.LicenseCertificationOfCompliance.LicenseState);
            var expirationDate = new EmailRowFieldTable("Expiration Date", ConverterHelper.ToDateString(driverEmploymentApplication.LicenseCertificationOfCompliance.LicenseExpiration));
            completeApplicationTable.AddRow(licenseNumber, state, expirationDate);

            completeApplicationTable.TitleSeparator("Driver's Certification");
            var driverNamePrinted = new EmailRowFieldTable("Driver's Name (Printed)", driverEmploymentApplication.LicenseCertificationOfCompliance.DriverNamePrinted);
            var applicationDate = new EmailRowFieldTable("Application Date", ConverterHelper.ToDateString(driverEmploymentApplication.ApplicationDate));
            completeApplicationTable.AddRow(driverNamePrinted, applicationDate);
            completeApplicationTable.TitleSeparator("I certify that I have read and understand the above requirements.");
          
            return completeApplicationTable;
        }        
    }
}