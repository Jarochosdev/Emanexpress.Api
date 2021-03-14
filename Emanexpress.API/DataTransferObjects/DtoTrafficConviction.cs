using System;

namespace Emanexpress.API.Models
{
    public class DtoTrafficConviction
    {
        public DateTime? Date { get;set; }
        public string Location { get;set; }
        public string Charge { get;set; }
        public string Penalty { get;set; }
    }
}