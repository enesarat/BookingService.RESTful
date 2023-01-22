using BookingService.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Entity.Concrete
{
    public class Appartments : IEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string zip_code { get; set; }
        public string address { get; set; }
        public string address2 { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string direction { get; set; }
        public int booked { get; set; }
    }
}
