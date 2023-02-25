using Emanexpress.API.Business.Email.Common.HtmlStructure;
using Emanexpress.API.Converter;
using Emanexpress.API.DataTransferObjects;
using System;
using System.Linq;

namespace Emanexpress.API.Business.Email
{
    public class DriverEmploymentEmailTableStrategyTrafficConvictions : IDriverEmploymentEmailTableStrategy
    {
        public DriverEmploymentApplicationEmailTableType emailTableType => DriverEmploymentApplicationEmailTableType.TrafficConvictions;

        public ConverterHelper ConverterHelper { get; }

        public DriverEmploymentEmailTableStrategyTrafficConvictions(ConverterHelper converterHelper)
        {
            ConverterHelper = converterHelper;
        }

        public EmailTable GetEmailTable(DtoDriverEmploymentApplication driverEmploymentApplication)
        {
            var trafficConvictionsTable = new EmailTable("Traffic Convictions");

            if(driverEmploymentApplication.TrafficConvictions == null)
            {
                return trafficConvictionsTable;
            }
            int position = 0;

            foreach(var trafficConviction in driverEmploymentApplication.TrafficConvictions.OrderBy(o => o.Date))
            {
                if(position == 0)
                {
                    trafficConvictionsTable.TitleSeparator("Last");
                }
                else
                {
                    trafficConvictionsTable.TitleSeparator("Previous");
                }

                var date = new EmailRowFieldTable("Date", ConverterHelper.ToDateString(trafficConviction.Date),30);
                var location = new EmailRowFieldTable("Location", trafficConviction.Location,40);
                var charge = new EmailRowFieldTable("Charge",trafficConviction.Charge,30);
                trafficConvictionsTable.AddRow(date, location, charge);

                var penalty = new EmailRowFieldTable("Penalty", trafficConviction.Penalty, 40);
                trafficConvictionsTable.AddRow(penalty);
                position++;
            }
                                  
            return trafficConvictionsTable;
        }       
    }
}