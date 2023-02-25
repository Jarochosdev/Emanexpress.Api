using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emanexpress.API.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Emanexpress.API.Business.Email.ContactUs;

namespace Emanexpress.API.Controllers.WebSite
{        
    [Route("[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {   
        public EmailContactUsHandler EmailContactUsHandler { get; }        

        public ContactUsController(EmailContactUsHandler emailContactUsHandler)
        {
            EmailContactUsHandler = emailContactUsHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]DtoContactUs contactUs)
        {            
            await EmailContactUsHandler.HandleAsync(contactUs);
            return Ok("OK");
        }
    }
}
