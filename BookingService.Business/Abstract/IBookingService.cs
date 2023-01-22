using BookingService.Entity.Concrete;
using BookingService.Entity.Concrete.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Business.Abstract
{
    public interface IBookingService : IGenericService<Bookings>
    {
        public Task<BookingInfoListDTO> GetBookingInfoById(int id);
        public Task<string> GetUserFirstName(int id);
        public Task<string> GetUserLastName(int id);
        public Task<string> GetUserEmail(int id);
        public Task<string> GetUserPhoneNumber(int id);
        public Task<string> GetAppartmentName(Bookings item);
        public Task<string> GetAppartmentAddress(Bookings item);
        public Task<string> GetAppartmentAddressZipCode(Bookings item);
        public Task<string> GetAppartmentCity(Bookings item);
        public Task<string> GetAppartmentCountry(Bookings item);
        public Task<string> GetBookingStartDate(int id);
        public Task<string> GetBookingEndDate(int id);
        public Task<string> GetBookingConfirmationStatus(int id);
        public Task<bool> DeleteItemWithCretdention(int id);
        public List<Bookings> GetByMultipleFilter(FilterDTO filter);
    }
}
