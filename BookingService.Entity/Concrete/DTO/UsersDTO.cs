using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Entity.Concrete.DTO
{
    public class UsersDTO
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string image { get; set; }
        public string country { get; set; }
        public string city { get; set; }
    }
}
