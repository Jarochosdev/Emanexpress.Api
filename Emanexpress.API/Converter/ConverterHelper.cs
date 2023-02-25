using System;

namespace Emanexpress.API.Converter
{
    public class ConverterHelper
    {
        public string ToYesNo(bool value)
        {
            if (value)
            {
                return "YES";
            }

            return "NO";
        }

        public string ToDateString(DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToString("MM/dd/yyyy");
            }
            
            return "";            
        }
    }
}