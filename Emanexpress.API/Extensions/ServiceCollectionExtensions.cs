using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Emanexpress.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSmtpService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<SmtpClient>((serviceProvider) =>
            {                  
                var config = serviceProvider.GetRequiredService<IConfiguration>();               
                var host = config.GetValue<string>("Email:Smtp:Host");
                var port = config.GetValue<int>("Email:Smtp:Port");
                var enableSsl = config.GetValue<bool>("Email:Smtp:EnableSsl");

                return new SmtpClient()
                {                      
                    Host = host,
                    Port = port,   
                    EnableSsl = enableSsl,
                };
            });
            
            return serviceCollection;
        }
    }
}
