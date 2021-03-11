using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emanexpress.API.Business.Email;
using Emanexpress.API.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Emanexpress.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverEmploymentApplicationsController : ControllerBase
    {   
        public EmailDriverEmploymentApplicationHandler EmailDriverEmploymentApplicationHandler { get; }        

        public DriverEmploymentApplicationsController(EmailDriverEmploymentApplicationHandler emailDriverEmploymentApplicationHandler)
        {
            EmailDriverEmploymentApplicationHandler = emailDriverEmploymentApplicationHandler;
        }        

        [HttpPost]
        public async Task Post([FromBody]DtoDriverEmploymentApplication dtoDriverEmploymentApplication)
        {            
            await EmailDriverEmploymentApplicationHandler.SendToDriverAsync(dtoDriverEmploymentApplication);
        }
    }
}
