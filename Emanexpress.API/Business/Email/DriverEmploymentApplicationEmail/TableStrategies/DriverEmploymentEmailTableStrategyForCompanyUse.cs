using Emanexpress.API.Business.Email.Common.HtmlStructure;
using Emanexpress.API.Converter;
using Emanexpress.API.DataTransferObjects;
using System;

namespace Emanexpress.API.Business.Email
{
    public class DriverEmploymentEmailTableStrategyForCompanyUse : IDriverEmploymentEmailTableStrategy
    {
        public DriverEmploymentApplicationEmailTableType emailTableType => DriverEmploymentApplicationEmailTableType.ForCompanyUse;

        public ConverterHelper ConverterHelper { get; }

        public DriverEmploymentEmailTableStrategyForCompanyUse(ConverterHelper converterHelper)
        {
            ConverterHelper = converterHelper;
        }

        public EmailTable GetEmailTable(DtoDriverEmploymentApplication driverEmploymentApplication)
        {            
            var forCompanyUseTable = new EmailTable("For Company Use");
            
            var applicantHired = new EmailRowFieldTable("Applicant Hired", "");
            var applicantRejected = new EmailRowFieldTable("Rejected", "");
            forCompanyUseTable.AddRow(applicantHired, applicantRejected);
            
            var dateEmployeed = new EmailRowFieldTable("Date Employed", "");
            var pointEmployeed = new EmailRowFieldTable("Point Employed", "");
            forCompanyUseTable.AddRow(dateEmployeed, pointEmployeed);

            var department = new EmailRowFieldTable("Department", "");
            var classification = new EmailRowFieldTable("Classification", "");
            forCompanyUseTable.AddRow(department, classification);

            var signatureOfInterviewingOfficer = new EmailRowFieldTable("Signature Of Interviewing Officer", "");
            forCompanyUseTable.AddRow(signatureOfInterviewingOfficer);
            
            return forCompanyUseTable;
        }       
    }
}