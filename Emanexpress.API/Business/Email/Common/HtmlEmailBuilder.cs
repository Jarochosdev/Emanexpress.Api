using Emanexpress.API.Business.Email.Common.HtmlStructure;
using System.Collections.Generic;
using System.Linq;


namespace Emanexpress.API.Business.Email.Common
{
    public class HtmlBodyEmailBuilder
    {
        private List<EmailTable> EmailTables {get; }

        public HtmlBodyEmailBuilder()
        {
            EmailTables = new List<EmailTable>();
        }
        public HtmlBodyEmailBuilder AddTable(EmailTable emailTable)
        {           
            EmailTables.Add(emailTable);
            return this;
        }

        public string Build()
        {
            var htmlEmail = "<div style='width: 100%; margin: auto; padding-top:30px;'>";

            foreach(var table in EmailTables)
            {
                htmlEmail += GetTable(table);
            }

            htmlEmail+= "</div>";
            return htmlEmail;
        }

        private string GetTable(EmailTable table)
        {
            var htmlTable = "<table cellspacing='0' cellpadding='0' width='95%' " +
                            "style='margin: auto; padding: 10px; border: 1px solid #eee; color: #555;'>";

            htmlTable += "<tr style='background: #eee; border-bottom: 1px solid #ddd; font-weight: bold;'>" +
				         "<td style='text-align: center; font-size: 16px'>" + 
					     "<table width='100%;' style='padding:12px;'>" +
			             "<tr>" +
			             "<td width='100%' style='text-align: center;'>" + 
			             table.Header + 
			             "</td>" +
			             "</tr>";

            if (!string.IsNullOrWhiteSpace(table.HeaderMessage))
            {                
                htmlTable += "<tr>" +
			                 "<td style='text-align:justify; font-weight:lighter; font-size: 14px' width='100%'>" + 
			                 table.HeaderMessage + 
			                 "</td>" +
			                 "</tr>";
            }

	        htmlTable += "</table>" +
				         "</td>" +
			             "</tr>";

            if (!table.Elements.Any())
            {
                table.AddRow(new EmailRowFieldTable("No Records",""));
            }
            
            foreach(var emailElement in table.Elements)
            {
                htmlTable += "<tr><td> " + emailElement.GetHtml() + "</td></tr>";
            }

            if(table.FooterMessage.Any())
            {
                htmlTable += "<tr style='background: #eee; border-bottom: 1px solid #ddd;'>" +
				             "<td style='text-align: center; font-size: 16px'>" + 
					         "<table width='100%;' style='padding:12px;'>";
                
                foreach(var footerMessage in table.FooterMessage)
                {
                    
                    htmlTable += "<tr>" +
			                     "<td style='text-align:justify; font-weight:lighter; font-size: 14px' width='100%'>" + 
			                     footerMessage + 
			                     "</td>" +
			                     "</tr>";
                }
                
                htmlTable += "</table>" +
				             "</td>" +
			                 "</tr>";
            }
	        
            htmlTable += "</table>";

            return htmlTable;
        }
    }
}
