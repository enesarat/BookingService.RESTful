using AutoMapper;
using BookingService.Entity.Concrete;
using BookingService.Entity.Concrete.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Business.Concrete.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile() 
        {
            CreateMap<Appartments, AppartmentsDTO>().ReverseMap();
            CreateMap<Bookings, BookingsDTO>().ReverseMap();
            CreateMap<Company, CompanyDTO>().ReverseMap();
            CreateMap<Users, UsersDTO>().ReverseMap();
        }
    }
}
