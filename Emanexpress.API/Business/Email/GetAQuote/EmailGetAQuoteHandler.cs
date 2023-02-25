using Emanexpress.API.Business.Configurations;
using Emanexpress.API.Business.Email.Common;
using Emanexpress.API.Business.Email.Common.HtmlStructure;
using Emanexpress.API.Converter;
using Emanexpress.API.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Emanexpress.API.Business.Email.GetAQuote
{
    public class EmailGetAQuoteHandler
    {
        public EmailSender EmailSender { get; }        
        public GetAQuoteEmailReceiverConfiguration GetAQuoteEmailReceiverConfiguration { get; }
        public EmailValidator EmailValidator { get;set; }
        public WebSiteConfiguration WebSiteConfiguration { get; }
        public StylishBodyEmailBuilderFactory StylishBodyEmailBuilderFactory { get;}
        public ConverterHelper ConverterHelper { get; }

        public EmailGetAQuoteHandler(EmailSender emailSender, GetAQuoteEmailReceiverConfiguration getAQuoteEmailReceiverConfiguration, EmailValidator emailValidator, WebSiteConfiguration webSiteConfiguration, StylishBodyEmailBuilderFactory stylishBodyEmailBuilderFactory, ConverterHelper converterHelper)
        {
            EmailSender = emailSender;            
            GetAQuoteEmailReceiverConfiguration = getAQuoteEmailReceiverConfiguration;
            EmailValidator = emailValidator;
            WebSiteConfiguration = webSiteConfiguration;
            StylishBodyEmailBuilderFactory = stylishBodyEmailBuilderFactory;
            ConverterHelper = converterHelper;
        }

        public async Task HandleAsync(DtoGetAQuote dtoGetAQuote)
        {
            await HandleAdminEmailAsync(dtoGetAQuote);
            await HandleRequesterEmailAsync(dtoGetAQuote);            
        }

        private async Task HandleAdminEmailAsync(DtoGetAQuote dtoGetAQuote)
        {                                            
            var getAQuoteBody =
                "<p>Name: " + dtoGetAQuote.Name + "</p>" +
                "<p>Company: " + dtoGetAQuote.Company + "</p>" +
                "<p>Email: " + dtoGetAQuote.Email + "</p>" +
                "<p>Phone: " + dtoGetAQuote.Phone + "</p>" +               
                "<p>Type Of Service: " + dtoGetAQuote.TypeOfService + "</p>" +
                "<p>Location Pick-Up: " + dtoGetAQuote.LocationPickup + "</p>" +
                "<p>Location Delivery: " + dtoGetAQuote.LocationDelivery + "</p>" + 
                "<p>Pick-Up Date: " + ConverterHelper.ToDateString(dtoGetAQuote.AppointmentDatePickup) + "</p>" + 
                "<p>Pick-Up Time: " + dtoGetAQuote.AppointmentTimePickup + "</p>" + 
                "<p>Delivery Date: " + ConverterHelper.ToDateString(dtoGetAQuote.AppointmentDateDelivery) + "</p>" + 
                "<p>Delivery Time: " + dtoGetAQuote.AppointmentTimeDelivery + "</p>" + 
                "<p>Commodities: " + dtoGetAQuote.Commodities + "</p>" + 
                "<p>Weight: " + dtoGetAQuote.Weight + "</p>" + 
                "<p>Notes: " + dtoGetAQuote.Notes + "</p>";            
            
            await EmailSender.SendEmailAsync(
                GetAQuoteEmailReceiverConfiguration.Email,
                "You have a new quote request from " + dtoGetAQuote.Name, 
                getAQuoteBody, true, MailPriority.High);
        }

       
        private async Task HandleRequesterEmailAsync(DtoGetAQuote dtoGetAQuote)
        {
             if(EmailValidator.IsValidEmail(dtoGetAQuote.Email))
            { 
                var stylishBodyEmailBuilder = StylishBodyEmailBuilderFactory.GetBuilder();
                
                stylishBodyEmailBuilder.AddNewLine("Hello " + dtoGetAQuote.Name + "!", fontSizeInPixels: 28, margingInPixels: 15, color: "#002054");
                stylishBodyEmailBuilder.AddNewLine($"Thank you for choosing us among a wide range of logistic companies.", fontSizeInPixels: 18, margingInPixels: 10);
                stylishBodyEmailBuilder.AddNewLine("One of our logistic associates from " +
                                                    WebSiteConfiguration.CompanyName + " is processing your quote " +
                                                    $"from {dtoGetAQuote.LocationPickup} to {dtoGetAQuote.LocationDelivery} and will contact with " +
                                                    "you as soon as possible.",
                                                    fontSizeInPixels: 18, margingInPixels: 20);
                await EmailSender.SendEmailAsync(
                    dtoGetAQuote.Email,
                    "Thank you for choosing us",
                    stylishBodyEmailBuilder.GetBody(), true);
            }
        }       
    }
}
