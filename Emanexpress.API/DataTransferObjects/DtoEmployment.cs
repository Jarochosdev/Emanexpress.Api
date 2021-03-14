namespace Emanexpress.API.Models
{
    public class DtoEmployment
    {
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string FromMonthYear { get; set; }
        public string ToMonthYear { get;set; }
        public string Salary { get;set; }
        public string PositionHeld { get;set; }
        public string ReasonForLeaving { get;set; }
        public bool SubjectToMfcsrs { get;set; }
        public bool SafetySensitive { get;set; }
        public bool StillWorkingHere { get;set; }
    }
}
