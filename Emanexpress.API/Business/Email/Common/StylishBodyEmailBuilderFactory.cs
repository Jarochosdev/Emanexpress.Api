using Emanexpress.API.Business.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emanexpress.API.Business.Email.Common
{
    public class StylishBodyEmailBuilderFactory
    {
        WebSiteConfiguration WebSiteConfiguration { get; }

        public StylishBodyEmailBuilderFactory(WebSiteConfiguration webSiteConfiguration)
        {
            WebSiteConfiguration = webSiteConfiguration;
        }

        public StylishBodyEmailBuilder GetBuilder()
        {
            return new StylishBodyEmailBuilder(WebSiteConfiguration);
        }
    }
}
