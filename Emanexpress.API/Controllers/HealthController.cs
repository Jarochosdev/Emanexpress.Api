using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emanexpress.API.Business.Email.ContactUs;
using Emanexpress.API.DataTransferObjects;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emanexpress.API.Controllers
{        
    [Route("[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        IWebHostEnvironment WebHostEnvironment { get;}        

        public HealthController(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;            
        }

        [HttpGet]
        public string Get()
        {                      
            return "I'm alive!!!" + Environment.NewLine + "Environment: " + WebHostEnvironment.EnvironmentName;
        }
    }
}
