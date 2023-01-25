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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _manageUsers;
        private readonly IMapper _mapper;
        public UsersController(IUserService usersService, IMapper mapper)
        {
            _manageUsers = usersService;
            _mapper = mapper;
        }

        //-------------------------------------------------------Get Requests Starts------------------------------------------//
        /// <summary>
        /// This endpoint receives all user data via paging, bringing 10 data per page. (default)
        /// </summary>
        /// <param name="pagingParameters"></param>
        /// <returns>10 user data for a page (default)</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PagingParameters pagingParameters)
        {
            var users = await _manageUsers.GetElementsByPaging(pagingParameters);
            var usersDTO = _mapper.Map<List<UsersDTO>>(users.ToList());

            return Ok(usersDTO); // 200 + retrieved data 
        }

        /// <summary>
        /// This endpoint gets first name of user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>First name of user which exist with id</returns>
        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetFistName(int id)
        {
            string fistname = await _manageUsers.GetUserFirstName(id);
            return Ok(fistname); // 200 + retrieved data 
        }

        /// <summary>
        /// This endpoint gets last name of user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Last name of user which exist with id</returns>
        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetLastName(int id)
        {
            var lastname = await _manageUsers.GetUserLastName(id);
            return Ok(lastname); // 200 + retrieved data 
        }

        /// <summary>
        /// This endpoint gets email of user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Email of user which exist with id</returns>
        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetEmail(int id)
        {
            var email = await _manageUsers.GetUserEmail(id);
            return Ok(email); // 200 + retrieved data 
        }

        /// <summary>
        /// This endpoint gets phone number of user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Phone number of user which exist with id</returns>
        [HttpGet]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetPhoneNo(int id)
        {
            var phoneno = await _manageUsers.GetUserPhoneNo(id);
            return Ok(phoneno); // 200 + retrieved data 
        }
        //-------------------------------------------------------Get Requests Ends------------------------------------------//

        //-------------------------------------------------------Post Requests Starts------------------------------------------//
        /// <summary>
        /// This endpoint creates a new user record using the values ​​it receives.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Created user record</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Users user)
        {
            if (ModelState.IsValid)
            {
                var newUser = await _manageUsers.InsertElement(user);
                return CreatedAtAction("Get", new { userId = newUser.id }, newUser); // 201 + data + header info for data location
            }
            return BadRequest(ModelState); // 400 + validation errors
        }
        //-------------------------------------------------------Post Requests Ends------------------------------------------//

        //-------------------------------------------------------Put Requests Starts------------------------------------------//
        /// <summary>
        /// This endpoint updates an existing user record using the values ​​it receives.
        /// </summary>
        /// <param name="oldUser"></param>
        /// <returns>Updated user record</returns>
        [HttpPut]
        [ExceptionFilter]

        public async Task<IActionResult> Put([FromBody] Users oldUser)
        {
            if (await _manageUsers.GetElementById(oldUser.id) != null)
            {
                return Ok(await _manageUsers.UpdateElement(oldUser)); // 200 + data
            }
            return NotFound(); // 404 
        }
        //-------------------------------------------------------Put Requests Ends------------------------------------------//

        //-------------------------------------------------------Delete Requests Starts------------------------------------------//
        /// <summary>
        /// This endpoint deletes the existing user record that matches the id value it received.In order for the deletion process to be carried out successfully, the user must not be included in any booking record!
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            if (await _manageUsers.GetElementById(id) != null)
            {
                var status = await _manageUsers.DeleteItemWithRecordCheck(id);

                if (status)
                {
                    return Ok(); // 200
                }
                return BadRequest("Silmek istediğiniz kullanıcı, herhangi bir kiralama kaydında yer aldığı için silme işlemi gerçekleştirilememiştir.");

            }
            return NotFound(); // 404 
        }
        //-------------------------------------------------------Delete Requests Ends------------------------------------------//

    }
}
