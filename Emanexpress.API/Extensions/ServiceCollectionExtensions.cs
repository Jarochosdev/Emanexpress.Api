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
                var userName = config.GetValue<string>("Email:Smtp:Username");                  
                var password = config.GetValue<string>("Email:Smtp:Password");                  
                var host = config.GetValue<string>("Email:Smtp:Host");                  
                var port = config.GetValue<int>("Email:Smtp:Port");
                                    
                return new SmtpClient()                 
                {                      
                    Host = host,                      
                    Port = port,                      
                    Credentials = new NetworkCredential(userName, password)                 
                };
            });                            
            
            return serviceCollection;
        }
    }
}
