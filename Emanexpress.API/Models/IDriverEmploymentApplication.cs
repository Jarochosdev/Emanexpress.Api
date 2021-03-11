using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emanexpress.API.Models
{
    public interface IDriverEmploymentApplication
    {        
        string FirstName { get; set; }
        public string MiddleName { get;set; }
        string LastName { get; set; }
        string DriverEmail { get; set; }
    }
}
