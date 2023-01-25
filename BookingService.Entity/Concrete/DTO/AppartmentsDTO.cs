using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Entity.Concrete.DTO
{
    public class AppartmentsDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public int booked { get; set; }
    }
}
