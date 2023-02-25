using Emanexpress.API.Business.Configurations;
using Emanexpress.API.Business.Email.Common;
using Emanexpress.API.Business.Email.Common.HtmlStructure;
using Emanexpress.API.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Emanexpress.API.Business.Email.ContactUs
{
    public class EmailContactUsHandler
    {
        public EmailSender EmailSender { get; }        
        public ContactUsEmailReceiverConfiguration ContactUsEmailReceiverConfiguration { get; }
        public EmailValidator EmailValidator { get;set; }
        public StylishBodyEmailBuilderFactory StylishBodyEmailBuilderFactory { get; }
        public WebSiteConfiguration WebSiteConfiguration { get; }

        public EmailContactUsHandler(EmailSender emailSender, ContactUsEmailReceiverConfiguration contactUsEmailReceiverConfiguration, EmailValidator emailValidator, StylishBodyEmailBuilderFactory stylishBodyEmailBuilderFactory, WebSiteConfiguration webSiteConfiguration)
        {
            EmailSender = emailSender;            
            ContactUsEmailReceiverConfiguration = contactUsEmailReceiverConfiguration;
            EmailValidator = emailValidator;
            StylishBodyEmailBuilderFactory = stylishBodyEmailBuilderFactory;
            WebSiteConfiguration = webSiteConfiguration;
        }

        public async Task HandleAsync(DtoContactUs dtoContactUs)
        {
            await HandleAdminEmailAsync(dtoContactUs);
            await HandleRequesterEmailAsync(dtoContactUs);
        }

        private async Task HandleAdminEmailAsync(DtoContactUs dtoContactUs)
        {                                 
           
            var contactUsBody =
                "<p>Name: " + dtoContactUs.Name + "</p>" +
                "<p>Email: " + dtoContactUs.Email + "</p>" +
                "<p>Phone: " + dtoContactUs.Phone + "</p>" +
                "<p>Message: " + dtoContactUs.Message + "</p>";                
            
            await EmailSender.SendEmailAsync(
                ContactUsEmailReceiverConfiguration.Email,
                "You have a new Contact Us Message from " + dtoContactUs.Name, 
                contactUsBody, true, MailPriority.High);
        }

       
        private async Task HandleRequesterEmailAsync(DtoContactUs dtoContactUs)
        {
            if(EmailValidator.IsValidEmail(dtoContactUs.Email))
            { 
                var stylishBodyEmailBuilder =StylishBodyEmailBuilderFactory.GetBuilder();
                
                stylishBodyEmailBuilder.AddNewLine("Hello " + dtoContactUs.Name + "!", fontSizeInPixels: 28, margingInPixels: 15, color: "#002054");
                stylishBodyEmailBuilder.AddNewLine("Thank you for contacting us.", fontSizeInPixels: 18, margingInPixels: 10);
                stylishBodyEmailBuilder.AddNewLine($"One of our logistic associates from {WebSiteConfiguration.CompanyName} is processing your message and will contact with you as soon as possible.", fontSizeInPixels: 18, margingInPixels: 20);

                await EmailSender.SendEmailAsync(
                    dtoContactUs.Email,
                    "Thanks for contacting with us",
                    stylishBodyEmailBuilder.GetBody(), true);
            }
        }       
    }
}
