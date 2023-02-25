using System.Threading.Tasks;
using Emanexpress.API.Business.Email;
using Emanexpress.API.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Emanexpress.API.Controllers.WebSite
{     
    [Route("[controller]")]
    [ApiController]
    public class DriverEmploymentApplicationsController : ControllerBase
    {   
        public EmailDriverEmploymentApplicationHandler EmailDriverEmploymentApplicationHandler { get; }        

        public DriverEmploymentApplicationsController(EmailDriverEmploymentApplicationHandler emailDriverEmploymentApplicationHandler)
        {
            EmailDriverEmploymentApplicationHandler = emailDriverEmploymentApplicationHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]DtoDriverEmploymentApplication dtoDriverEmploymentApplication)
        {
            await EmailDriverEmploymentApplicationHandler.HandleAsync(dtoDriverEmploymentApplication);
            return Ok("OK");
        }
    }
}
