using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Entity.Concrete.DTO
{
    public class CompanyDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public double salary { get; set; }
    }
}
