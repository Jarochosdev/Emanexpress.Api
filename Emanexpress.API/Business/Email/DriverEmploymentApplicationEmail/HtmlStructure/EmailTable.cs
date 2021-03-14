using System;
using System.Collections.Generic;

namespace Emanexpress.API.Business.Email
{
    public class EmailTable
    {
        public string Header { get;set; }
        public List<IEmailRowElement> Elements {get;set; }

        public EmailTable(string header)
        {
            Header = header;
            Elements = new List<IEmailRowElement>();
        }

        internal void AddRow(params EmailRowFieldTable[] rowField)
        {
            var emailRowTable = new EmailRowTable();            
            emailRowTable.AddFields(rowField);
            Elements.Add(emailRowTable);
        }

        internal void TitleSeparator(string title)
        {
            var emailTitle = new EmailTitleSeparator(title);
            Elements.Add(emailTitle);            
        }
    }
}