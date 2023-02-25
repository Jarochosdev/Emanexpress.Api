using System;
using System.Collections.Generic;
using System.Linq;

namespace Emanexpress.API.Business.Email.Common.HtmlStructure
{
    public class EmailRowTable : IEmailRowElement
    {
        List<EmailRowFieldTable> EmailRowFields { get; }

        public EmailRowTable()
        {
            EmailRowFields = new List<EmailRowFieldTable>();
        }

        internal void AddFields(EmailRowFieldTable[] rowFields)
        {
            foreach(var rowField in rowFields)
            {
                EmailRowFields.Add(rowField);
            }
        }

        public string GetHtml()
        {
            var html = "<table width= '100%;' style='padding-top:5; font-size: 14px'>" + 
			"<tr>";                       
            
            var distributedPercentage = GetDistributedPercentage();

            foreach(var rowfield in EmailRowFields)
            {     
                var widthPercentage = rowfield.WidthPercentage == null || rowfield.WidthPercentage == 0 ? distributedPercentage : rowfield.WidthPercentage;

                html += "<td width='" + widthPercentage + "'>" +
			            "<div style='border: 1px; border-style: solid; padding: 5px;'>" + 
			            "<div><strong>" + (string.IsNullOrWhiteSpace(rowfield.Label) ? "&nbsp;" : rowfield.Label)  + "</strong></div>" + 
			            "<div>"+ (string.IsNullOrWhiteSpace(rowfield.Value) ? "&nbsp;" : rowfield.Value) + "</div>" +
		                "</div>" +
			            "</td>";
            }

			html += "</tr>" +
			"</table>";

            return html;
        }

        private float GetDistributedPercentage()
        {
            var numberOfFieldsWithNoPercentage = EmailRowFields.Where(e => e.WidthPercentage == null || e.WidthPercentage == 0).Count();
            float usedPercentage = (float)EmailRowFields.Where(e => e.WidthPercentage != null && e.WidthPercentage > 0).Sum(e=>e.WidthPercentage);
            float distributedPercentage = (100 - usedPercentage) / numberOfFieldsWithNoPercentage;
            
            if(distributedPercentage <= 0)
            {
                return 10;
            }

            return distributedPercentage;
        }
    }
}
