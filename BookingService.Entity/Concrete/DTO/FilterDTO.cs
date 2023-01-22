using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Entity.Concrete.DTO
{
    public class FilterDTO
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string starts_at { get; set; }
        public string end_at { get; set; }
        public string apt_name { get; set; }
        public string confirmed { get; set; }
    }
}
