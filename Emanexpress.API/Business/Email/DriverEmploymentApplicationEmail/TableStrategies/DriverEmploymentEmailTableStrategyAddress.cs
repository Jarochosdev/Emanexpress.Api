using Emanexpress.API.Business.Email.Common.HtmlStructure;
using Emanexpress.API.Converter;
using Emanexpress.API.DataTransferObjects;
using System;

namespace Emanexpress.API.Business.Email
{
    public class DriverEmploymentEmailTableStrategyAddress : IDriverEmploymentEmailTableStrategy
    {
        public DriverEmploymentApplicationEmailTableType emailTableType => DriverEmploymentApplicationEmailTableType.Address;

        public ConverterHelper ConverterHelper { get; }

        public DriverEmploymentEmailTableStrategyAddress(ConverterHelper converterHelper)
        {
            ConverterHelper = converterHelper;
        }

        public EmailTable GetEmailTable(DtoDriverEmploymentApplication driverEmploymentApplication)
        {
            var titleMessage ="Addresses of residency for the past 3 years.";

            var addressTable = new EmailTable("Address", titleMessage);

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

                var zipCode = new EmailRowFieldTable("Zip Code", address.ZipCode);
                var livingFrom = new EmailRowFieldTable("Living From", ConverterHelper.ToDateString(address.LivingFrom));
                var livingTo = new EmailRowFieldTable("Living To", ConverterHelper.ToDateString(address.LivingTo));
                addressTable.AddRow(zipCode, livingFrom, livingTo);
            }
                                  
            return addressTable;
        }     
    }
}