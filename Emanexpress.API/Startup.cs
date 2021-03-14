using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emanexpress.API.Business.Email;
using Emanexpress.API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Emanexpress.API
{
    public class Startup
    {        
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<EmailDriverEmploymentApplicationHandler>();
            services.AddScoped<EmailSender>();
            services.AddScoped<DriverEmploymentEmailTableFactory>();
            services.AddScoped<IDriverEmploymentEmailTableStrategy,DriverEmploymentEmailTableStrategyApplicantInformation>();
            services.AddScoped<IDriverEmploymentEmailTableStrategy,DriverEmploymentEmailTableStrategyAddress>();            
            services.AddScoped<IDriverEmploymentEmailTableStrategy,DriverEmploymentEmailTableStrategyEmploymentHistory>();  
            services.AddScoped<IDriverEmploymentEmailTableStrategy,DriverEmploymentEmailTableStrategyAccidentRecords>();                      
            services.AddScoped<IDriverEmploymentEmailTableStrategy,DriverEmploymentEmailTableStrategyTrafficConvictions>();
            services.AddScoped<IDriverEmploymentEmailTableStrategy,DriverEmploymentEmailTableStrategyLicenseHistory>();
            services.AddScoped<IDriverEmploymentEmailTableStrategy,DriverEmploymentEmailTableStrategyDrivingExperience>();
            services.AddScoped<IDriverEmploymentEmailTableStrategy,DriverEmploymentEmailTableStrategyExperienceQualifications>();
            
            services.AddSmtpService();
            
            var userName = Configuration.GetValue<string>("Email:Smtp:Username");
            var password = Configuration.GetValue<string>("Email:Smtp:Password");
            var emailBcc = Configuration.GetValue<string>("Email:EmailBcc");
            services.AddScoped( d => new EmailSenderConfiguration(userName, password, emailBcc));
            
            var driverApplicationEmailReceiver = Configuration.GetValue<string>("DriverApplicationEmailReceiver");            
            services.AddScoped( d => new DriverApplicationEmailReceiverConfiguration(driverApplicationEmailReceiver));
            
            if(Env.IsDevelopment())
            {
              services.AddCors();
            }            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
