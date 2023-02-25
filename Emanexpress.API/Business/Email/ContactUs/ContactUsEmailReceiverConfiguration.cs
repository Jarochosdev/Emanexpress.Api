using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emanexpress.API.Business.Email.ContactUs
{
    public class ContactUsEmailReceiverConfiguration
    {
        public string Email { get; }        

        public ContactUsEmailReceiverConfiguration(string email)
        {
            Email = email;            
        }
    }
}
