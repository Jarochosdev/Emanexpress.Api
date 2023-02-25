using System;

namespace Emanexpress.API.DataTransferObjects
{
    public class DtoLicenseCertificationOfCompliance
    {
        public string LicenseNumber { get;set; }
        public string LicenseState { get;set;}
        public DateTime LicenseExpiration { get;set;}
        public string DriverNamePrinted { get;set; }
        public bool CertificationAgree { get;set; }
    }
}
