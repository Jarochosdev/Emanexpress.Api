using Emanexpress.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emanexpress.API.DataTransferObjects
{
    public class DtoDriverEmploymentApplication : IDriverEmploymentApplication
    {        
        public string FirstName { get;set; }
        public string MiddleName { get;set; }
        public string LastName { get;set; }        
        public string DriverEmail { get; set; }
    }
}
