using DemoTrip.Core.Interfaces;
using DemoTrip.Core.UseCases;
using DemoTrip.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoTrip.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly ISearchHotelUseCase searchHotelUseCase;

        public HotelsController(ISearchHotelUseCase searchHotelUseCase)
        {
            this.searchHotelUseCase = searchHotelUseCase;
        }

        // POST api/<HotelsController>
        [HttpPost("search")]
        public IActionResult Post([FromBody] SearchHotelRequestViewModel request)
        {
            if (ModelState.IsValid)
            {
                var response = searchHotelUseCase.Execute(new SearchHotelRequestDto()
                {
                    Location = request.Location!,
                    DateFrom = request.DateFrom!.Value,
                    DateTo = request.DateTo!.Value
                });

                var responseVm = new SearchHotelResponseViewModel() { Hotels = response.Hotels };

                if (response.Succes)
                {
                    return Ok(responseVm);
                }
                return NotFound(responseVm);
            }
            return BadRequest();
        }
    }
}
