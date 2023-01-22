using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Entity.Concrete.DTO
{
    public class BookingInfoListDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ApartmentName { get; set; }
        public string ApartmentAddress { get; set; }
        public string ApartmentAddressZipCode { get; set; }
        public string ApartmentAddressCity { get; set; }
        public string ApartmentAddressCountry { get; set; }
        public string BookingStartDate { get; set; }
        public string BookingEndDate { get; set; }
        public string ConfirmationStatus { get; set; }
    }
}
