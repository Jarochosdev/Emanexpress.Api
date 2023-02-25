using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emanexpress.API.DataTransferObjects
{
    public class DtoGetAQuote
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string TypeOfService { get; set; }
        public string LocationPickup { get; set; }
        public string LocationDelivery { get; set; }
        public DateTime AppointmentDatePickup { get; set; }
        public string AppointmentTimePickup { get; set; }
        public DateTime AppointmentDateDelivery { get; set; }
        public string AppointmentTimeDelivery { get; set; }
        public string Commodities { get; set; }
        public string Weight { get; set; }
        public string Notes { get; set; }
    }
}
