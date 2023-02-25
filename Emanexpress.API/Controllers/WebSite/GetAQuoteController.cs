using System.Threading.Tasks;
using Emanexpress.API.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Emanexpress.API.Business.Email.GetAQuote;

namespace Emanexpress.API.Controllers.WebSite
{        
    [Route("[controller]")]
    [ApiController]
    public class GetAQuoteController : ControllerBase
    {   
        public EmailGetAQuoteHandler EmailContactUsHandler { get; }        

        public GetAQuoteController(EmailGetAQuoteHandler emailContactUsHandler)
        {
            EmailContactUsHandler = emailContactUsHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]DtoGetAQuote getAQuote)
        {            
            await EmailContactUsHandler.HandleAsync(getAQuote);
            return Ok("OK");
        }
    }
}
