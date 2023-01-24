using BookingService.Business.Abstract;
using BookingService.DataAccess.Concrete.Helper.Exceptions;
using BookingService.Entity.Concrete;
using BookingService.Entity.Concrete.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppartmentsController : ControllerBase
    {
        IAppartmentService manageAppartments;
        public AppartmentsController(IAppartmentService appartmentService)
        {
            manageAppartments = appartmentService;
        }

        //-------------------------------------------------------Get Requests Starts------------------------------------------//
        /// <summary>
        /// This endpoint receives all appartment data via paging, bringing 10 data per page. (default)
        /// </summary>
        /// <param name="pagingParameters"></param>
        /// <returns>10 appartment data for a page </returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PagingParameters pagingParameters)
        {
            return Ok(await manageAppartments.GetElementsByPaging(pagingParameters)); // 200 + retrieved data 
        }
        //-------------------------------------------------------Get Requests Ends------------------------------------------//

        //-------------------------------------------------------Post Requests Starts------------------------------------------//
        /// <summary>
        /// This endpoint creates a new appartment record using the values ​​it receives.
        /// </summary>
        /// <param name="appartment"></param>
        /// <returns>Created appartment record</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Appartments appartment)
        {
            if (ModelState.IsValid)
            {
                var newAppartment = await manageAppartments.InsertElement(appartment);
                return CreatedAtAction("Get", new { appartmentId = newAppartment.id }, newAppartment); // 201 + data + header info for data location
            }
            return BadRequest(ModelState); // 400 + validation errors
        }
        //-------------------------------------------------------Post Requests Ends------------------------------------------//

        //-------------------------------------------------------Put Requests Starts------------------------------------------//
        /// <summary>
        /// This endpoint updates an existing appartment record using the values ​​it receives.
        /// </summary>
        /// <param name="oldAppartment"></param>
        /// <returns>Updated appartment record</returns>
        [HttpPut]
        [ExceptionFilter]

        public async Task<IActionResult> Put([FromBody] Appartments oldAppartment)
        {
            if (await manageAppartments.GetElementById(oldAppartment.id) != null)
            {
                return Ok(await manageAppartments.UpdateElement(oldAppartment)); // 200 + data
            }
            return NotFound(); // 404 
        }
        //-------------------------------------------------------Put Requests Ends------------------------------------------//

        //-------------------------------------------------------Delete Requests Starts------------------------------------------//
        /// <summary>
        /// This endpoint deletes the existing appartment record that matches the id value it received.In order for the deletion process to be carried out successfully, the apartment must not be included in any booking record!
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteAppartmentById(int id)
        {
            if (await manageAppartments.GetElementById(id) != null)
            {
                var status = await manageAppartments.DeleteItemWithRecordCheck(id);

                if (status)
                {
                    return Ok(); // 200
                }
                return BadRequest("Silmek istediğiniz apartman, herhangi bir kiralama kaydında yer aldığı için silme işlemi gerçekleştirilememiştir.");

            }
            return NotFound(); // 404 
        }
        //-------------------------------------------------------Delete Requests Ends------------------------------------------//

    }
}
