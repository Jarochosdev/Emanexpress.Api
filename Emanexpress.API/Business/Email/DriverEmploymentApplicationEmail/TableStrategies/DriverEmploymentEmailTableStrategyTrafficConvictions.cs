using Emanexpress.API.DataTransferObjects;
using System;
using System.Linq;

namespace Emanexpress.API.Business.Email
{
    public class DriverEmploymentEmailTableStrategyTrafficConvictions : IDriverEmploymentEmailTableStrategy
    {
        public DriverEmploymentApplicationEmailTableType emailTableType => DriverEmploymentApplicationEmailTableType.TrafficConvictions;

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

                var date = new EmailRowFieldTable("Date", GetDate(trafficConviction.Date),30);
                var location = new EmailRowFieldTable("Location", trafficConviction.Location,40);
                var charge = new EmailRowFieldTable("Charge",trafficConviction.Charge,30);
                trafficConvictionsTable.AddRow(date, location, charge);

                var penalty = new EmailRowFieldTable("Penalty", trafficConviction.Penalty, 40);
                trafficConvictionsTable.AddRow(penalty);
                position++;
            }
                                  
            return trafficConvictionsTable;
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