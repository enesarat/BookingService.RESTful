using BookingService.Business.Abstract;
using BookingService.Entity.Concrete.DTO;
using BookingService.Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookingService.DataAccess.Concrete.Helper.Exceptions;

namespace BookingService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        ICompanyService manageCompany;
        public CompanyController(ICompanyService companyService)
        {
            manageCompany = companyService;
        }

        //-------------------------------------------------------Get Requests Starts------------------------------------------//
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PagingParameters pagingParameters)
        {
            return Ok(await manageCompany.GetElementsByPaging(pagingParameters)); // 200 + retrieved data 
        }
        //-------------------------------------------------------Get Requests Ends------------------------------------------//

        //-------------------------------------------------------Post Requests Starts------------------------------------------//
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Company company)
        {
            if (ModelState.IsValid)
            {
                var newCompany = await manageCompany.InsertElement(company);
                return CreatedAtAction("Get", new { companyId = newCompany.id }, newCompany); // 201 + data + header info for data location
            }
            return BadRequest(ModelState); // 400 + validation errors
        }
        //-------------------------------------------------------Post Requests Ends------------------------------------------//

        //-------------------------------------------------------Put Requests Starts------------------------------------------//
        [HttpPut]
        [ExceptionFilter]

        public async Task<IActionResult> Put([FromBody] Company oldCompany)
        {
            if (await manageCompany.GetElementById(oldCompany.id) != null)
            {
                return Ok(await manageCompany.UpdateElement(oldCompany)); // 200 + data
            }
            return NotFound(); // 404 
        }
        //-------------------------------------------------------Put Requests Ends------------------------------------------//

        //-------------------------------------------------------Delete Requests Starts------------------------------------------//
        [HttpDelete]
        [ExceptionFilter]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteCompanyById(int id)
        {
            if (await manageCompany.GetElementById(id) != null)
            {
                await manageCompany.DeleteItem(id);
                return Ok(); // 200
            }
            return NotFound(); // 404 
        }
        //-------------------------------------------------------Delete Requests Ends------------------------------------------//

    }
}
