using System.Collections.Generic;
using System.Linq;

namespace Emanexpress.API.Business.Email
{
    public class HtmlDriverEmailbuilder
    {
        private List<EmailTable> EmailTables {get; }

        public HtmlDriverEmailbuilder()
        {
            EmailTables = new List<EmailTable>();
        }
        public HtmlDriverEmailbuilder AddTable(EmailTable emailTable)
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
			             "</tr>" +
			             "</table>" +
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

            htmlTable+= "</table>";

            return htmlTable;
        }
    }
}
