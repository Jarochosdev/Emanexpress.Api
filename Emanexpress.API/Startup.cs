using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emanexpress.API.Business.Configurations;
using Emanexpress.API.Business.Email;
using Emanexpress.API.Business.Email.Common;
using Emanexpress.API.Business.Email.ContactUs;
using Emanexpress.API.Business.Email.GetAQuote;
using Emanexpress.API.Converter;
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
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }        

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    if (Env.IsProduction())
                    {
                        var allowedOrigins = Configuration.GetSection("AllowedOrigins").Get<string[]>();
                        builder.WithOrigins(allowedOrigins);
                    }
                    else
                    {
                        builder.AllowAnyOrigin();
                    }

                    builder
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            services.AddControllers();
            
            services.AddScoped<EmailValidator>();
            services.AddScoped<EmailSender>();
            services.AddScoped<DriverEmploymentEmailTableFactory>();
            services.AddScoped<StylishBodyEmailBuilderFactory>();
            
            services.AddScoped<EmailDriverEmploymentApplicationHandler>();      
            services.AddScoped<EmailGetAQuoteHandler>();
            services.AddScoped<EmailContactUsHandler>();
            services.AddScoped<ConverterHelper>();

              var driverApplicationEmailReceiver = Configuration.GetValue<string>("DriverApplicationEmailReceiver");            
            services.AddScoped( d => new DriverApplicationEmailReceiverConfiguration(driverApplicationEmailReceiver));
            
            var contactUsEmailReceiver = Configuration.GetValue<string>("ContactUsEmailReceiver");
            services.AddScoped( d => new ContactUsEmailReceiverConfiguration(contactUsEmailReceiver));

            var getQuoteEmailReceiver = Configuration.GetValue<string>("GetQuoteEmailReceiver");
            services.AddScoped( d => new GetAQuoteEmailReceiverConfiguration(getQuoteEmailReceiver));                                  

            services.AddScoped<IDriverEmploymentEmailTableStrategy,DriverEmploymentEmailTableStrategyApplicantInformation>();
            services.AddScoped<IDriverEmploymentEmailTableStrategy,DriverEmploymentEmailTableStrategyAddress>();  
            services.AddScoped<IDriverEmploymentEmailTableStrategy,DriverEmploymentEmailTableStrategyEmploymentHistory>();
            services.AddScoped<IDriverEmploymentEmailTableStrategy,DriverEmploymentEmailTableStrategyAccidentRecords>();
            services.AddScoped<IDriverEmploymentEmailTableStrategy,DriverEmploymentEmailTableStrategyTrafficConvictions>();
            services.AddScoped<IDriverEmploymentEmailTableStrategy,DriverEmploymentEmailTableStrategyLicenseHistory>();
            services.AddScoped<IDriverEmploymentEmailTableStrategy,DriverEmploymentEmailTableStrategyDrivingExperience>();
            services.AddScoped<IDriverEmploymentEmailTableStrategy,DriverEmploymentEmailTableStrategyExperienceQualifications>();
            services.AddScoped<IDriverEmploymentEmailTableStrategy,DriverEmploymentEmailTableStrategyCompleteApplication>();
            services.AddScoped<IDriverEmploymentEmailTableStrategy,DriverEmploymentEmailTableStrategyCertificationOfCompliance>();
            services.AddScoped<IDriverEmploymentEmailTableStrategy,DriverEmploymentEmailTableStrategyForCompanyUse>();
            services.AddScoped<IDriverEmploymentEmailTableStrategy,DriverEmploymentEmailTableStrategyTerminationOfEmployment>();
            
            services.AddSmtpService();
                                            
            var userName = Environment.GetEnvironmentVariable("EmanExpressEmailSenderUsername");
            var password = Environment.GetEnvironmentVariable("EmanExpressEmailSenderPassword");
            var emailBcc = Configuration.GetValue<string>("Email:EmailBcc");
            services.AddScoped( d => new EmailSenderConfiguration(userName, password, emailBcc));

            var logoUrl = Configuration.GetValue<string>("WebSiteConfiguration:LogoUrl");
            var contactPhone = Configuration.GetValue<string>("WebSiteConfiguration:ContactPhone");
            var contactEmail = Configuration.GetValue<string>("WebSiteConfiguration:ContactEmail");
            var companyName = Configuration.GetValue<string>("WebSiteConfiguration:CompanyName");

            services.AddScoped( d => new WebSiteConfiguration(logoUrl, contactPhone, contactEmail, companyName));

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors("CorsPolicy");
            });
        }
    }
}
