using BookingService.Business.Abstract;
using BookingService.Entity.Concrete.DTO;
using BookingService.Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookingService.DataAccess.Concrete.Helper.Exceptions;
using AutoMapper;

namespace BookingService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _manageBookings;
        private readonly IMapper _mapper;

        public BookingsController(IBookingService agentService, IBookingService manageBookings, IMapper mapper)
        {
            _manageBookings = agentService;
            _manageBookings = manageBookings;
            _mapper = mapper;
        }

        //-------------------------------------------------------Get Requests Starts------------------------------------------//
        /// <summary>
        /// This endpoint receives all bookings data via paging, bringing 10 data per page. (default)
        /// </summary>
        /// <param name="pagingParameters"></param>
        /// <returns>10 bookings data for a page (default)</returns>        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PagingParameters pagingParameters)
        {
            var bookings = await _manageBookings.GetElementsByPaging(pagingParameters);
            var bookingsDTO = _mapper.Map<List<BookingsDTO>>(bookings.ToList());
            return Ok(bookingsDTO); // 200 + retrieved data 
        }

        /// <summary>
        /// This endpoint gets booking data by id input.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>1 booking data which exist with id</returns>
        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var booking = await _manageBookings.GetElementById(id);
            var bookingDTO = _mapper.Map<BookingsDTO>(booking);
            return Ok(bookingDTO); // 200 + retrieved data   
        }

        /// <summary>
        /// This endpoint gets booked apartment name by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Apartment name which exist with id</returns>
        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetBookingAppartmentName(int id)
        {
            string aptName = await _manageBookings.GetAppartmentName(id);
            return Ok(aptName); // 200 + retrieved data   
        }

        /// <summary>
        /// This endpoint gets apartment address by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Apartment address which exist with id</returns>
        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetBookingAppartmentAddress(int id)
        {
            string aptAddress = await _manageBookings.GetAppartmentAddress(id);
            return Ok(aptAddress); // 200 + retrieved data   
        }

        /// <summary>
        /// This endpoint gets zip code of apartment address by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Zip code of apartment address which exist with id</returns>
        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetBookingAppartmentAddressZipCode(int id)
        {
            string aptAddressZipCode = await _manageBookings.GetAppartmentAddressZipCode(id);
            return Ok(aptAddressZipCode); // 200 + retrieved data   
        }

        /// <summary>
        /// This endpoint gets city of apartment by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>City of apartment address which exist with id</returns>
        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetBookingAppartmentCity(int id)
        {
            string aptCity = await _manageBookings.GetAppartmentCity(id);
            return Ok(aptCity); // 200 + retrieved data   
        }

        /// <summary>
        /// This endpoint gets country of apartment by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Country of apartment address which exist with id</returns>
        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetBookingAppartmentCountry(int id)
        {
            string aptCountry = await _manageBookings.GetAppartmentCountry(id);
            return Ok(aptCountry); // 200 + retrieved data   
        }

        /// <summary>
        /// This endpoint gets start date of booking by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Start date of booking which exist with id</returns>
        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetBookingStartDate(int id)
        {
            string startDate = await _manageBookings.GetBookingStartDate(id);
            return Ok(startDate); // 200 + retrieved data   
        }

        /// <summary>
        /// This endpoint gets end date of booking by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>End date of booking which exist with id</returns>
        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetBookingEndDate(int id)
        {
            string endDate = await _manageBookings.GetBookingEndDate(id);
            return Ok(endDate); // 200 + retrieved data   
        }

        /// <summary>
        /// This endpoint gets confirmation status of booking by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Confirmation status of booking which exist with id</returns>
        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetBookingConfirmationStatus(int id)
        {
            string confirmationStatus = await _manageBookings.GetBookingConfirmationStatus(id);
            return Ok(confirmationStatus); // 200 + retrieved data   
        }

        /// <summary>
        /// This endpoint returns the booking data it finds as a result of the filter made using multiple optional features.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>Booking data it finds as a result of the filter made using multiple optional features</returns>
        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public IActionResult GetByMultipleFilter([FromQuery] FilterDTO filter)
        {
            List<Bookings> filterResults = _manageBookings.GetByMultipleFilter(filter);
            return Ok(filterResults); // 200 + retrieved data   
        }

        /// <summary>
        /// This endpoint returns the user first name of booking data by given id.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>User first name of booking which exist with id</returns>
        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetBookedUserFirstName(int id)
        {
            string userFirstName = await _manageBookings.GetUserFirstName(id);
            return Ok(userFirstName); // 200 + retrieved data   
        }

        /// <summary>
        /// This endpoint returns the user last name of booking data by given id.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>User last name of booking which exist with id</returns>
        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetBookedUserLastName(int id)
        {
            string userLastName = await _manageBookings.GetUserLastName(id);
            return Ok(userLastName); // 200 + retrieved data   
        }

        /// <summary>
        /// This endpoint returns the user email of booking data by given id.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>User email of booking which exist with id</returns>
        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetBookedUserEmail(int id)
        {
            string userEmail = await _manageBookings.GetUserEmail(id);
            return Ok(userEmail); // 200 + retrieved data   
        }

        /// <summary>
        /// This endpoint returns the user phone number of booking data by given id.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>User phone number of booking which exist with id</returns>
        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetBookedUserPhoneNumber(int id)
        {
            string userPhone = await _manageBookings.GetUserPhoneNumber(id);
            return Ok(userPhone); // 200 + retrieved data   
        }

        /// <summary>
        /// This endpoint returns the normalized booking informations of booking record by given id.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>Normalized booking informations of booking which exist with id</returns>
        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetBookingInfoById(int id)
        {
            BookingInfoListDTO booking = await _manageBookings.GetBookingInfoById(id);
            return Ok(booking); // 200 + retrieved data   
        }
        //-------------------------------------------------------Get Requests Ends------------------------------------------//

        //-------------------------------------------------------Post Requests Starts------------------------------------------//
        /// <summary>
        /// This endpoint creates a new booking record using the values ​​it receives.
        /// </summary>
        /// <param name="book"></param>
        /// <returns>Created booking record</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Bookings book)
        {
            if (ModelState.IsValid)
            {
                var newBooknig = await _manageBookings.InsertElement(book);
                return CreatedAtAction("Get", new { bookingId = newBooknig.id }, newBooknig); // 201 + data + header info for data location
            }
            return BadRequest(ModelState); // 400 + validation errors
        }
        //-------------------------------------------------------Post Requests Ends------------------------------------------//

        //-------------------------------------------------------Put Requests Starts------------------------------------------//
        /// <summary>
        /// This endpoint updates an existing booking record using the values ​​it receives.
        /// </summary>
        /// <param name="oldBooking"></param>
        /// <returns>Updated booking record</returns>
        [HttpPut]
        [ExceptionFilter]

        public async Task<IActionResult> Put([FromBody] Bookings oldBooking)
        {
            if (await _manageBookings.GetElementById(oldBooking.id) != null)
            {
                return Ok(await _manageBookings.UpdateElement(oldBooking)); // 200 + data
            }
            return NotFound(); // 404 
        }
        //-------------------------------------------------------Put Requests Ends------------------------------------------//

        //-------------------------------------------------------Delete Requests Starts------------------------------------------//
        /// <summary>
        /// This endpoint deletes the existing booking record that matches the id value it received. In order for the deletion to be performed, the confirmation status must be 0 (not confirmed)!
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteBookingById(int id)
        {
            if (await _manageBookings.GetElementById(id) != null)
            {
                var status = await _manageBookings.DeleteItemWithCretdention(id);

                if (status)
                {
                    return Ok(); // 200
                }
                return BadRequest("Kiralama işlemi onaylanmış olduğu işin tablodaki bu kaydı silemezsiniz!");

            }
            return NotFound(); // 404 
        }
        //-------------------------------------------------------Delete Requests Ends------------------------------------------//

    }
}
