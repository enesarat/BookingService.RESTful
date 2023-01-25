using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Entity.Concrete.DTO
{
    public class BookingsDTO
    {
        public int id { get; set; }
        public string starts_at { get; set; }
        public int booked_for { get; set; }
        public int confirmed { get; set; }
    }
}
