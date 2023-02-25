using Emanexpress.API.Business.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emanexpress.API.Business.Email.Common
{
    public class StylishBodyEmailBuilder
    {
        public IEnumerable<string> AllLines => _allLines.AsReadOnly();

        private List<string> _allLines { get; }
        
        public WebSiteConfiguration WebSiteConfiguration { get;}

        public StylishBodyEmailBuilder(WebSiteConfiguration webSiteConfiguration)
        {
            _allLines = new List<string>();
            WebSiteConfiguration = webSiteConfiguration;
        }
        
        public StylishBodyEmailBuilder AddNewLine(string text, int fontSizeInPixels=14, int margingInPixels=10, int fontWeight = 700, string color = "black")
        {
            _allLines.Add($"<p style='font-size: {fontSizeInPixels}px; margin: {margingInPixels}px; font-weight: {fontWeight}; color: {color}'>{text}</p>");
            return this;
        }

        public StylishBodyEmailBuilder AddNewLine(string text, string style)
        {
            _allLines.Add($"<p style='{style}'>{text}</p>");
            return this;
        }

        public string GetBody()
        {
            var body = new StringBuilder();
            body.Append("<body style='background-color: #ECECEC; padding-top: 50px;'>");
            body.Append("<div style='top: 40px; width: 100%; min-height: 500px; height: auto; background-color: #ECECEC; position: relative; font: normal 14px/1.3 Helvetica,Arial,sans-serif;'>");
	        body.Append("<div style='width: 90%;  max-width: 800px; height: auto; margin: 10px auto; position: relative; background-color: #FFF;'>");
		    body.Append("<div style='background: #FFF; text-align: center; padding: 10px;'>");
			body.Append($"<img src='{WebSiteConfiguration.LogoUrl}'  width='150px'>");
		    body.Append("</div>");
		    body.Append("<div style='width: 80%; height: auto; margin: 0 auto; padding: 10px; text-align: center'>");
			
            foreach(var line in _allLines)
            {
                body.Append(line);
            }            
		    
            body.Append("</div>");		
		    body.Append("<div style='background: #FF2C1D; text-align: center; padding: 20px;'>");                        
			body.Append($"<p style='font-size: 14px; margin: 0px; font-weight: 700; color: #FFF !important; text-decoration: none !important;'>Phone: {WebSiteConfiguration.ContactPhone}</p>");
			body.Append($"<p style='font-size: 14px; margin: 0px; font-weight: 700; color: #FFF !important; text-decoration: none !important;'>Email: {WebSiteConfiguration.ContactEmail}</p>");
			body.Append("<p style='font-size: 14px; margin: 0px 0px 10px 0px; font-weight: 700; color: #FFF !important; text-decoration: none !important;'>We cover all the region of California.</p>");
			body.Append($"<p style='font-size: 12px; margin: 0px; font-weight: 700; color: #FFF !important; text-decoration: none !important;'>{WebSiteConfiguration.CompanyName} &#169; All Rights Reserved</p>");
            body.Append($"<p style='font-size: 10px; margin: 0px; font-weight: 700; color: #FFF !important; text-decoration: none !important;'>Make sure our messages get to your Inbox (and not your bulk or junk folders).</p>");            
		    body.Append("</div>");
            body.Append("</div>");
            body.Append("</div>");	
            body.Append("</body>");	
            return body.ToString();
        }
    }    
}
