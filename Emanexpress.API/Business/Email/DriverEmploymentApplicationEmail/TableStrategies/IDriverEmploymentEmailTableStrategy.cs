using Emanexpress.API.Business.Email.Common.HtmlStructure;
using Emanexpress.API.DataTransferObjects;

namespace Emanexpress.API.Business.Email
{
    public interface IDriverEmploymentEmailTableStrategy
    {
        DriverEmploymentApplicationEmailTableType emailTableType {get; }
        EmailTable GetEmailTable(DtoDriverEmploymentApplication driverEmploymentApplication);
    }
}