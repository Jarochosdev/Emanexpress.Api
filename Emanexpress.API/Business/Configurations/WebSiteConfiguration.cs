using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emanexpress.API.Business.Configurations
{
    public class WebSiteConfiguration
    {
        public string LogoUrl { get; }
        public string ContactPhone { get; }
        public string ContactEmail { get;}
        public string CompanyName { get; }

        public WebSiteConfiguration(string logoUrl, string contactPhone, string contactEmail, string companyName)
        {
            LogoUrl = logoUrl;
            ContactPhone = contactPhone;
            ContactEmail = contactEmail;
            CompanyName = companyName;
        }
    }
}
