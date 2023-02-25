using Emanexpress.API.Business.Email.Common.HtmlStructure;
using Emanexpress.API.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;

namespace Emanexpress.API.Business.Email
{
    public class DriverEmploymentEmailTableFactory
    {
        public IEnumerable<IDriverEmploymentEmailTableStrategy> DriverEmploymentEmailTableStrategies { get; }

        public DriverEmploymentEmailTableFactory(IEnumerable<IDriverEmploymentEmailTableStrategy> driverEmploymentEmailTableStrategies)
        {
            DriverEmploymentEmailTableStrategies = driverEmploymentEmailTableStrategies;
        }        

        public EmailTable GetEmailTable(DtoDriverEmploymentApplication driverEmploymentApplication, DriverEmploymentApplicationEmailTableType emailTableType)
        {
            var strategy = DriverEmploymentEmailTableStrategies.First(d=>d.emailTableType == emailTableType);
            if(strategy == null)
            {
                throw new System.Exception("Email table type " +  emailTableType + " is not supported.");
            }

            return strategy.GetEmailTable(driverEmploymentApplication);
        }
    }
}