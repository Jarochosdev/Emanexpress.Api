using System.Collections.Generic;

namespace Emanexpress.API.Business.Email.Common.HtmlStructure
{
    public class EmailBoxMessageTable : IEmailRowElement
    {
        List<string> Paragraphs { get; }
        string Title { get; }

        public EmailBoxMessageTable(string title)
        {
            Paragraphs = new List<string>();
            Title = title;
        }

        internal void AddParagraph(string paragraph)
        {
            Paragraphs.Add(paragraph);
        }

        public string GetHtml()
        {
            var html = "<table width= '100%;' style='padding-top:5; font-size: 14px'>" + 
			            "<tr>";                       
                                                   
            html += "<td width='100%'>" +
			        "<div style='border: 1px; border-style: solid; padding: 5px;'>" +
                    "<p>&nbsp;</p>" + 
			        "<div style='text-align: center;'><strong>" + (string.IsNullOrWhiteSpace(Title) ? "&nbsp;" : Title)  + "</strong></div>";
            
            foreach(var paragraph in Paragraphs)
            {
                html += "<p>" + paragraph + "</p>";
            }

		    html += "<p>&nbsp;</p>" +
                    "</div>" +
			        "</td>" +
			        "</tr>" +
			        "</table>";

            return html;
        }        
    }
}
