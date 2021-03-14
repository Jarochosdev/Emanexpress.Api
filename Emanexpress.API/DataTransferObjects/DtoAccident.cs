using System;

namespace Emanexpress.API.DataTransferObjects
{
    public class DtoAccident
    {
        public DateTime? AccidentDate { get;set; }
        public string NatureOfAccident { get;set; }
        public string Fatalities { get;set; }
        public string Injuries { get;set; }
        public string HazardousMaterial { get;set; }
    }
}