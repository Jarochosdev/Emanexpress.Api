using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emanexpress.API.Business.Email.GetAQuote
{
    public class GetAQuoteEmailReceiverConfiguration
    {
        public string Email { get; }        

        public GetAQuoteEmailReceiverConfiguration(string email)
        {
            Email = email;            
        }
    }
}
