using BookingService.Business.Abstract;
using BookingService.DataAccess.Abstract;
using BookingService.DataAccess.Concrete.Helper.Calculation;
using BookingService.DataAccess.Concrete.Helper.Enums;
using BookingService.DataAccess;
using BookingService.Entity.Concrete;
using BookingService.Entity.Concrete.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Business.Concrete
{
    public class BookingManager : IBookingService
    {
        IBookingsDAL bookingsAccess;
        IAppartmentsDAL appartmentAccess;
        IUsersDAL userAccess;
        public BookingManager(IBookingsDAL bookingsAccess, IAppartmentsDAL appartmentAccess, IUsersDAL userAccess)
        {
            this.bookingsAccess = bookingsAccess;
            this.appartmentAccess = appartmentAccess;
            this.userAccess = userAccess;
        }

        public async Task DeleteItem(int id)
        {
            var deleteItem = bookingsAccess.GetItemById(id);
            var status = deleteItem.Result.confirmed;
            if (status != 1)
            {
                await bookingsAccess.DeleteItem(id);
            }

        }
        public async Task<bool> DeleteItemWithCretdention(int id)
        {
            var deleteItem = bookingsAccess.GetItemById(id);
            var status = deleteItem.Result.confirmed;
            if (status != 1)
            {
                await bookingsAccess.DeleteItem(id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Bookings>> GetAllElement()
        {
            var bookingsList = await bookingsAccess.GetAllItems();
            return bookingsList;
        }

        public async Task<List<Bookings>> GetElementsByPaging(PagingParameters pagingParameters)
        {
            var bookingsList = await bookingsAccess.GetElementsByPaging(pagingParameters);
            return bookingsList;
        }


        public List<Bookings> GetAllItemsByFilter(Expression<Func<Bookings, bool>> filter)
        {
            return bookingsAccess.GetAllItemsByFilter(filter);
        }

        public List<Bookings> GetByMultipleFilter(FilterDTO filter)
        {
            int userId = -1;
            int aptID = -1;
            List<Bookings> queryResults;

            if (filter.first_name != null || filter.last_name != null)
            {
                List<Users> users;
                if (filter.first_name != null)
                {
                    users = userAccess.GetAllItemsByFilter(x => x.first_name == filter.first_name);
                }
                else
                {
                    users = userAccess.GetAllItemsByFilter(x => x.last_name == filter.last_name);
                }

                if (users.Count == 1)
                {
                    userId = users[0].id;
                }
            }
            if (filter.apt_name != null)
            {
                List<Appartments> appartments;
                appartments = appartmentAccess.GetAllItemsByFilter(x => x.name == filter.apt_name);
                if (appartments.Count == 1)
                {
                    aptID = appartments[0].id;
                }
            }
            using (BookingServiceDbContext dbContext = new BookingServiceDbContext())
            {
                IEnumerable<Bookings> query = dbContext.bookings;
                if (userId != -1)
                {
                    query = bookingsAccess.GetAllItemsByFilter(x => x.user_id == userId).ToList();
                }
                if (filter.starts_at != null)
                {
                    query = query.Where(x => x.starts_at.Contains(filter.starts_at)).ToList();
                }
                if (filter.end_at != null)
                {
                    var timeCalc1 = new TimeCalculator(filter.end_at);
                    int end_year = timeCalc1.year;
                    int end_month = timeCalc1.month;
                    int end_day = timeCalc1.day;

                    var timeCalc2 = new TimeCalculator(filter.starts_at);
                    int start_year = timeCalc2.year;
                    int start_month = timeCalc2.month;
                    int start_day = timeCalc2.day;

                    if (end_day == start_day)
                    {
                        if (end_month == start_month)
                        {
                            if (end_year == start_year)
                            {
                                query = query.Where(x => x.booked_for == 0).ToList();
                            }
                            else if (end_year - start_year > 0)
                            {
                                query = query.Where(x => x.booked_for == 365 * (end_year - start_year)).ToList();

                            }
                        }
                        else if (end_month - start_month > 0)
                        {
                            query = query.Where(x => x.booked_for == 30 * (end_month - start_month)).ToList();

                        }
                    }
                    else if (end_day > start_day)
                    {
                        var day_count = end_day - start_day;
                        if (end_month > start_month)
                        {
                            day_count += (end_month - start_month) * 30;
                        }
                        else if (end_month < start_month)
                        {
                            if (end_year > start_year)
                            {
                                day_count += (end_year - start_year) * 365;
                            }
                        }
                        else if (end_year > start_year)
                        {
                            day_count += (end_year - start_year) * 365;
                        }
                        query = query.Where(x => x.booked_for == day_count).ToList();

                    }
                    else
                    {
                        var day_count = (30 - start_day) + end_day;
                        if (end_month > start_month)
                        {
                            day_count += (end_month - start_month - 1) * 30;
                        }
                        else if (end_month < start_month)
                        {
                            if (end_year > start_year)
                            {
                                day_count += (end_year - start_year) * 365;
                            }

                        }
                        else if (end_year > start_year)
                        {

                            day_count += (end_year - start_year) * 365;
                        }
                        query = query.Where(x => x.booked_for == day_count).ToList();

                    }
                }
                if (aptID != -1)
                {
                    query = query.Where(x => x.apartment_id == aptID).ToList();

                }
                if (filter.confirmed != null)
                {
                    if (Convert.ToInt32(filter.confirmed) == 1)
                    {
                        query = query.Where(x => x.confirmed == Convert.ToInt32(filter.confirmed)).ToList();
                    }
                    else
                    {
                        query = query.Where(x => x.confirmed == Convert.ToInt32(filter.confirmed)).ToList();

                    }
                }
                queryResults = query.ToList();
            }


            return queryResults;
        }

        public async Task<string> GetAppartmentAddress(int id)
        {
            Appartments appartment = await appartmentAccess.GetItemById(id);
            string aptAddress = appartment.address;
            return aptAddress;
        }

        public async Task<string> GetAppartmentAddressZipCode(int id)
        {
            Appartments appartment = await appartmentAccess.GetItemById(id);
            string aptAddressZipCode = appartment.zip_code;
            return aptAddressZipCode;
        }

        public async Task<string> GetAppartmentCity(int id)
        {
            Appartments appartment = await appartmentAccess.GetItemById(id);
            string aptCity = appartment.city;
            return aptCity;
        }

        public async Task<string> GetAppartmentCountry(int id)
        {
            Appartments appartment = await appartmentAccess.GetItemById(id);
            string aptCountry = appartment.country;
            return aptCountry;
        }

        public async Task<string> GetAppartmentName(int id)
        {
            Appartments appartment = await appartmentAccess.GetItemById(id);
            string aptName = appartment.name;
            return aptName;
        }

        public async Task<string> GetBookingConfirmationStatus(int id)
        {
            var booking = await bookingsAccess.GetItemById(id);
            int confirmationStatus = booking.confirmed;
            string statusAsString;
            if (confirmationStatus == 1)
            {
                statusAsString = Enum.GetName(typeof(ConfirmationStatus), ConfirmationStatus.onaylı);
            }
            else
            {
                statusAsString = Enum.GetName(typeof(ConfirmationStatus), ConfirmationStatus.onaylanmamış);
            }
            return statusAsString;
        }

        public async Task<string> GetBookingEndDate(int id)
        {
            string bookingEndDate;
            int currYear, currMonth, finalDay;
            var booking = await bookingsAccess.GetItemById(id);
            string bookingStartDate = booking.starts_at;
            var timeCalc = new TimeCalculator(bookingStartDate);
            var dayCount = Convert.ToInt32(booking.booked_for);

            var startDay = timeCalc.day;
            finalDay = startDay + dayCount;
            if (finalDay > 30)
            {
                finalDay = finalDay % 30;
                currMonth = timeCalc.month;
                currMonth += 1;
                if (currMonth > 12)
                {
                    currMonth = currMonth % 12;
                    currYear = timeCalc.year;
                    currYear += 1;
                }
                else
                {
                    currYear = timeCalc.year;
                }


                if (finalDay >= 10)
                {
                    bookingEndDate = String.Format("{0}-{1}-{2}{3}", currYear, currMonth, finalDay, timeCalc.hourInfo);
                }
                else
                {
                    bookingEndDate = String.Format("{0}-{1}-0{2}{3}", currYear, currMonth, finalDay, timeCalc.hourInfo);
                }
            }
            else
            {
                currMonth = timeCalc.month;
                currYear = timeCalc.year;
                if (finalDay >= 10)
                {
                    bookingEndDate = String.Format("{0}-{1}-{2}{3}", currYear, currMonth, finalDay, timeCalc.hourInfo);
                }
                else
                {
                    bookingEndDate = String.Format("{0}-{1}-0{2}{3}", currYear, currMonth, finalDay, timeCalc.hourInfo);
                }
            }

            return bookingEndDate;
        }

        public async Task<string> GetBookingStartDate(int id)
        {
            var booking = await bookingsAccess.GetItemById(id);
            string bookingStartDate = booking.starts_at;
            return bookingStartDate;
        }

        public async Task<Bookings> GetElementById(int id)
        {
            var booking = await bookingsAccess.GetItemById(id);
            return booking;
        }

        /* ------------------------------------------- INFO ABOUT INSERT ------------------------------------------- *
         * In the externally used pgsql database, the ID column is not set as "primary key" in the "bookings" table, 
         * and it does not seem possible for you to apply this change to the database due to the schema change constraint. 
         * Since the auto-increment feature could not be set, the "increment" operation in this method had to be done manually.
         *//*/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\\*/
        public async Task<Bookings> InsertElement(Bookings item)
        {
            List<Bookings> bookingList = await bookingsAccess.GetAllItems();
            var count = bookingList.Count;
            if (count > 0)
            {
                var lastBooking = bookingList[count - 1];
                item.id = lastBooking.id + 1;
            }
            else
            {
                item.id = 0;
            }
            await bookingsAccess.InsertItem(item);
            return item;
        }

        public async Task<Bookings> UpdateElement(Bookings item)
        {
            await bookingsAccess.UpdateItem(item);
            return item;
        }

        public async Task<string> GetUserFirstName(int id)
        {
            var booking = await bookingsAccess.GetItemById(id);
            int bookingUserId = booking.user_id;
            var user = userAccess.GetItemById(bookingUserId);
            string userFirstName = user.Result.first_name;
            return userFirstName;
        }

        public async Task<string> GetUserLastName(int id)
        {
            var booking = await bookingsAccess.GetItemById(id);
            int bookingUserId = booking.user_id;
            var user = userAccess.GetItemById(bookingUserId);
            string userLastName = user.Result.last_name;
            return userLastName;
        }

        public async Task<string> GetUserEmail(int id)
        {
            var booking = await bookingsAccess.GetItemById(id);
            int bookingUserId = booking.user_id;
            var user = userAccess.GetItemById(bookingUserId);
            string userEmail = user.Result.email;
            return userEmail;
        }

        public async Task<string> GetUserPhoneNumber(int id)
        {
            var booking = await bookingsAccess.GetItemById(id);
            int bookingUserId = booking.user_id;
            var user = userAccess.GetItemById(bookingUserId);
            string userPhone = user.Result.phone;
            return userPhone;
        }

        public async Task<BookingInfoListDTO> GetBookingInfoById(int id)
        {
            BookingInfoListDTO bookingInfo = new BookingInfoListDTO();
            var booking = await bookingsAccess.GetItemById(id);
            int bookingUserId = booking.user_id;
            var user = userAccess.GetItemById(bookingUserId);
            bookingInfo.FirstName = user.Result.first_name;
            bookingInfo.LastName = user.Result.last_name;
            bookingInfo.Email = user.Result.email;
            bookingInfo.Phone = user.Result.phone;
            var apartment = appartmentAccess.GetItemById(booking.apartment_id);
            bookingInfo.ApartmentName = apartment.Result.name;
            bookingInfo.ApartmentAddress = apartment.Result.address;
            bookingInfo.ApartmentAddressZipCode = apartment.Result.zip_code;
            bookingInfo.ApartmentAddressCity = apartment.Result.city;
            bookingInfo.ApartmentAddressCountry = apartment.Result.country;
            bookingInfo.BookingStartDate = booking.starts_at;
            bookingInfo.BookingEndDate = await GetBookingEndDate(booking.id);
            bookingInfo.ConfirmationStatus = await GetBookingConfirmationStatus(booking.id);
            return bookingInfo;
        }
    }
}
