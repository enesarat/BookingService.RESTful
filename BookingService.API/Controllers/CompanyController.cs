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
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _manageCompany;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _manageCompany = companyService;
            _mapper = mapper;
        }

        //-------------------------------------------------------Get Requests Starts------------------------------------------//
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PagingParameters pagingParameters)
        {
            var companies = await _manageCompany.GetElementsByPaging(pagingParameters);
            var compaiesDTO = _mapper.Map<List<CompanyDTO>>(companies.ToList());
            return Ok(compaiesDTO); // 200 + retrieved data 
        }
        //-------------------------------------------------------Get Requests Ends------------------------------------------//

        //-------------------------------------------------------Post Requests Starts------------------------------------------//
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Company company)
        {
            if (ModelState.IsValid)
            {
                var newCompany = await _manageCompany.InsertElement(company);
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
            if (await _manageCompany.GetElementById(oldCompany.id) != null)
            {
                return Ok(await _manageCompany.UpdateElement(oldCompany)); // 200 + data
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
            if (await _manageCompany.GetElementById(id) != null)
            {
                await _manageCompany.DeleteItem(id);
                return Ok(); // 200
            }
            return NotFound(); // 404 
        }
        //-------------------------------------------------------Delete Requests Ends------------------------------------------//

    }
}
