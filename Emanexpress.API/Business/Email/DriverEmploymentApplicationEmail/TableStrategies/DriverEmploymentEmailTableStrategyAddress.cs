using Emanexpress.API.DataTransferObjects;
using System;

namespace Emanexpress.API.Business.Email
{
    public class DriverEmploymentEmailTableStrategyAddress : IDriverEmploymentEmailTableStrategy
    {
        public DriverEmploymentApplicationEmailTableType emailTableType => DriverEmploymentApplicationEmailTableType.Address;

        public EmailTable GetEmailTable(DtoDriverEmploymentApplication driverEmploymentApplication)
        {
            var addressTable = new EmailTable("Address");

            if(driverEmploymentApplication.AddressHistory == null)
            {
                return addressTable;
            }
            
            foreach(var address in driverEmploymentApplication.AddressHistory)
            {
                if(address.StillLeavingHere)
                {
                    addressTable.TitleSeparator("Current");
                }
                else
                {
                    addressTable.TitleSeparator("Previous");
                }

                var street = new EmailRowFieldTable("Street", address.Street);
                var city = new EmailRowFieldTable("City", address.City);
                var state = new EmailRowFieldTable("State",address.State);
                addressTable.AddRow(street, city, state);

                var zipCode = new EmailRowFieldTable("Zip Code", address.ZipCode, 20);
                var numberOfYears = new EmailRowFieldTable("Number of years", address.NumberOfYears,30);
                addressTable.AddRow(zipCode, numberOfYears);
            }
                                  
            return addressTable;
        }        
    }
}