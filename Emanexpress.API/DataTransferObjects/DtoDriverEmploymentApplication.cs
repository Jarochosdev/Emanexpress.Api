using Emanexpress.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emanexpress.API.DataTransferObjects
{
    public class DtoDriverEmploymentApplication
    {        
        public string FirstName { get; set; }
        public string MiddleName { get;set; }
        public string LastName { get; set; }
        public string Phone { get;set;}
        public string DriverEmail { get; set; }
        public DateTime? DateOfBirth { get;set; }
        public string SocialSecurity { get;set; }
        public DateTime? DateAvailableToStart { get;set; }
        public string PositionAppliedfor { get;set;}
        public bool HaveLegalRightToWorkInUsa { get;set;}        
        public string WorkedBeforeForUsWhere { get;set; }
        public DateTime? WorkedBeforeForUsFrom { get;set; }
        public DateTime? WorkedBeforeForUsTo { get;set; }
        public string WorkedBeforeForUsRateOfPay { get;set; }
        public string WorkedBeforeForUsPosition { get;set; }
        public string WorkedBeforeForUsReasonOfLeaving { get;set; }
        public bool AreYouNowEmployed { get;set; }
        public string HowLongSinceLastEmployment { get;set; }
        public string WhoReferedYou { get;set; }
        public string RateOfPayExpected { get;set; }
        public bool HaveYouEverBeenBonded { get;set; }
        public string NameOfBondingCompany { get;set; }
        public bool HaveYouEverBeenConvictedOfAFelony { get;set; }
        public string FelonyDetails { get;set; }
        public bool UnableToPerformFunctionsJob { get;set; }
        public string UnableToPerformFunctionsJobDetails { get;set; }     
        public IEnumerable<DtoAdress> AddressHistory { get; set; }
        public IEnumerable<DtoEmployment> EmploymentHistory { get; set; }
        public IEnumerable<DtoAccident> AccidentRecords { get;set; }
        public IEnumerable<DtoTrafficConviction> TrafficConvictions { get;set; }
        public IEnumerable<DtoLicense> LicenseHistory { get;set; }
        public bool HaveYouBeenDeniedALicense { get;set; }
        public bool HasAnyLicenseBeenSuspended { get;set; }
        public string LicenseSuspendedDetail { get;set; }
        public IEnumerable<DtoDrivingExperience> DrivingExperience { get;set; }        
        public string StatesOperatedForLastYears { get;set; }
        public string SpecialCoursesOfTraining { get; set; }
        public string SafeDrivingAwards { get;set; }
        public string OtherExperience { get;set;}
        public string CoursesAndTraining { get;set; }
        public string SpecialEquipment { get;set; }
        public string HighestGradeCompleted { get;set; }
        public string HighSchool { get;set; }
        public string CollegeLevel { get;set; }
        public string LastAttendedSchool { get;set; }
    }
}
