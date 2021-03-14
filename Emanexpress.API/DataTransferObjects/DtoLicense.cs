using System;

namespace Emanexpress.API.Models
{
    public class DtoLicense
    {
        public string State { get; set; }
        public string LicenseNumber { get; set; }
        public string Class { get; set; }
        public string Endorsement { get;set; }
        public DateTime?  Expiration { get;set; }
    }
}