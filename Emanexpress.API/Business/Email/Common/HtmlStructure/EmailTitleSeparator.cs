namespace Emanexpress.API.Business.Email.Common.HtmlStructure
{
    public class EmailTitleSeparator : IEmailRowElement
    {
       public string Title { get; }

        public EmailTitleSeparator(string title)
        {
            Title = title;
        }

        public string GetHtml()
        {
            var html = "<table width='100%;' style='padding:5;'>" +
			"<tr>" +
			"<td width='100%' style='text-align: center; font-size: 16px'>" + 
			(string.IsNullOrEmpty(Title) ? "&nbsp;" : Title) + 
			"</td>" +
			"</tr>" +
			"</table>";

            return html;
        }
    }
}