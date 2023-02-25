using System;
using System.Collections.Generic;

namespace Emanexpress.API.Business.Email.Common.HtmlStructure
{
    public class EmailTable
    {
        public string Header { get; }
        public List<string> FooterMessage { get; }
        public string HeaderMessage { get; }
        public List<IEmailRowElement> Elements {get;set; }

        public EmailTable(string header, string headerMessage = null)
        {
            Header = header;
            HeaderMessage = headerMessage;
            Elements = new List<IEmailRowElement>();
            FooterMessage = new List<string>();
        }

        internal void AddRow(params EmailRowFieldTable[] rowField)
        {
            var emailRowTable = new EmailRowTable();
            emailRowTable.AddFields(rowField);
            Elements.Add(emailRowTable);
        }

        internal void AddBoxMessage(string title, params string[] paragraphs)
        {
            var emailBoxMessageTable = new EmailBoxMessageTable(title);
            
            foreach(var paragraph in paragraphs)
            {
                emailBoxMessageTable.AddParagraph(paragraph);
            }
            
            Elements.Add(emailBoxMessageTable);
        }
       

        internal void TitleSeparator(string title)
        {
            var emailTitle = new EmailTitleSeparator(title);
            Elements.Add(emailTitle);            
        }

        internal void AddFooterMessage(string footerMessage)
        {
            FooterMessage.Add(footerMessage);
        }
    }
}