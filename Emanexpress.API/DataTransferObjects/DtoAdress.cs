using System;

namespace Emanexpress.API.Models
{
    public class DtoAdress
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public DateTime? LivingFrom { get; set; }
        public DateTime? LivingTo { get; set; }
        public bool StillLeavingHere { get;set; }
    }
}