using Emanexpress.API.Business.Email.Common.HtmlStructure;
using Emanexpress.API.Converter;
using Emanexpress.API.DataTransferObjects;
using System;

namespace Emanexpress.API.Business.Email
{
    public class DriverEmploymentEmailTableStrategyCompleteApplication : IDriverEmploymentEmailTableStrategy
    {
        public DriverEmploymentApplicationEmailTableType emailTableType => DriverEmploymentApplicationEmailTableType.CompleteApplication;

         public ConverterHelper ConverterHelper { get; }

        public DriverEmploymentEmailTableStrategyCompleteApplication(ConverterHelper converterHelper)
        {
            ConverterHelper = converterHelper;
        }

        public EmailTable GetEmailTable(DtoDriverEmploymentApplication driverEmploymentApplication)
        {                        
            var completeApplicationTable = new EmailTable("Complete Application");
            
            var toBeReadParagraph1 = "I authorize you to make investigations (including contacting current and prior employers) into my personal, employment, financial, medical history, and other related matters as may be necessary in arriving at an employment decision. I hereby release employers, schools, health care providers, and other persons from all liability in responding to inquiries and releasing information in connection with my application.";
            var toBeReadParagraph2 = "In the event of employment, I understand that false or misleading information given in my application or interview(s) may result in discharge. I also understand that I am required to abide by all rules and regulations of the Company.";
            var toBeReadParagraph3 = "I understand that the information I provide regarding my current and/or prior employers may be used, and those employer(s) will be contacted for the purpose of investigating my safety performance history as required by 49 CFR 391.23. I understand that I have the right to:";
            var toBeReadParagraph4 = "Review information provided by current/previous employers;";
            var toBeReadParagraph5 = "Have errors in the information corrected by previous employers, and for those previous employers to resend the corrected information to the prospective employer; and";
            var toBeReadParagraph6 = "Have a rebuttal statement attached to the alleged erroneous information, if the previous employer(s) and I cannot agree on the accuracy of the information.";

            completeApplicationTable.AddBoxMessage("TO BE READ AND SIGNED BY APPLICANT", toBeReadParagraph1, toBeReadParagraph2, toBeReadParagraph3, toBeReadParagraph4, toBeReadParagraph5, toBeReadParagraph6);

            var driverSignature = new EmailRowFieldTable("Driver Name Signature", driverEmploymentApplication.DriverNameSignature);
            var applicationDate = new EmailRowFieldTable("Application Date", ConverterHelper.ToDateString(driverEmploymentApplication.ApplicationDate));
            completeApplicationTable.AddRow(driverSignature, applicationDate);
                                    
            var thisCertifyFooterMessage = "This certifies that I completed this application, and that all entries on it and information in it are true and complete to the best of my knowledge.";
            completeApplicationTable.AddFooterMessage(thisCertifyFooterMessage);
            
            return completeApplicationTable;
        }        
    }
}