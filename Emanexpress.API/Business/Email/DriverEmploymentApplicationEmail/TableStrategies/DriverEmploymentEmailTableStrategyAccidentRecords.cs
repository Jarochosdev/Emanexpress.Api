using Emanexpress.API.Business.Email.Common.HtmlStructure;
using Emanexpress.API.Converter;
using Emanexpress.API.DataTransferObjects;
using System;
using System.Linq;

namespace Emanexpress.API.Business.Email
{
    public class DriverEmploymentEmailTableStrategyAccidentRecords : IDriverEmploymentEmailTableStrategy
    {
        public DriverEmploymentApplicationEmailTableType emailTableType => DriverEmploymentApplicationEmailTableType.AccidentRecords;

        public ConverterHelper ConverterHelper { get; }

        public DriverEmploymentEmailTableStrategyAccidentRecords(ConverterHelper converterHelper)
        {
            ConverterHelper = converterHelper;
        }

        public EmailTable GetEmailTable(DtoDriverEmploymentApplication driverEmploymentApplication)
        {
             var accidentRecordsTable = new EmailTable("Accident Records");

            if(driverEmploymentApplication.AccidentRecords == null)
            {
                return accidentRecordsTable;
            }
            int position = 0;
            foreach(var accidentRecords in driverEmploymentApplication.AccidentRecords.OrderBy(o => o.AccidentDate))
            {
                if(position == 0)
                {
                    accidentRecordsTable.TitleSeparator("Last");
                }
                else
                {
                    accidentRecordsTable.TitleSeparator("Previous");
                }

                var date = new EmailRowFieldTable("Date", ConverterHelper.ToDateString(accidentRecords.AccidentDate));
                var natureOfAccident = new EmailRowFieldTable("Nature of accident", accidentRecords.NatureOfAccident);
                var fatalities = new EmailRowFieldTable("Fatalities",accidentRecords.Fatalities);
                accidentRecordsTable.AddRow(date, natureOfAccident, fatalities);

                var injuries = new EmailRowFieldTable("Injuries", accidentRecords.Injuries);
                var hazardousMaterial = new EmailRowFieldTable("Hazardous material spill", accidentRecords.HazardousMaterial);
                accidentRecordsTable.AddRow(injuries, hazardousMaterial);
                position++;
            }
            return accidentRecordsTable;
        }
    }
}