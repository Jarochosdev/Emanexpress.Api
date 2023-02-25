using Emanexpress.API.Business.Email.Common.HtmlStructure;
using Emanexpress.API.Converter;
using Emanexpress.API.DataTransferObjects;
using System;

namespace Emanexpress.API.Business.Email
{
    public class DriverEmploymentEmailTableStrategyTerminationOfEmployment : IDriverEmploymentEmailTableStrategy
    {
        public DriverEmploymentApplicationEmailTableType emailTableType => DriverEmploymentApplicationEmailTableType.TerminationOfEmployment;

        public ConverterHelper ConverterHelper { get; }

        public DriverEmploymentEmailTableStrategyTerminationOfEmployment(ConverterHelper converterHelper)
        {
            ConverterHelper = converterHelper;
        }

        public EmailTable GetEmailTable(DtoDriverEmploymentApplication driverEmploymentApplication)
        {            
            var terminationOfEmployment = new EmailTable("Termination Of Employment");
            
            var dateTerminated = new EmailRowFieldTable("Date Terminated", "");
            var departmentReleasedFrom = new EmailRowFieldTable("Department Released From", "");
            terminationOfEmployment.AddRow(dateTerminated, departmentReleasedFrom);
            
            var dismissed = new EmailRowFieldTable("Dismissed", "");
            var voluntarilyQuit = new EmailRowFieldTable("Voluntarily Quit", "");
            var other = new EmailRowFieldTable("Other", "");
            terminationOfEmployment.AddRow(dismissed, voluntarilyQuit, other);

            var terminationReportPlaced = new EmailRowFieldTable("Termination Report Placed In File", "");
            var supervisor = new EmailRowFieldTable("Supervisor", "");
            terminationOfEmployment.AddRow(terminationReportPlaced, supervisor);
            
            return terminationOfEmployment;
        }       
    }
}