using DemoTrip.Core.Interfaces;
using DemoTrip.Core.UseCases;
using DemoTrip.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DemoTrip.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ISearchTripUseCase searchTripUseCase;

        public TripsController(ISearchTripUseCase searchTripUseCase)
        {
            this.searchTripUseCase = searchTripUseCase;
        }

        // POST api/<TripsController>
        [HttpPost("search")]
        public IActionResult Post([FromBody] SearchTripRequestViewModel request)
        {
            if (ModelState.IsValid)
            {
                var response = searchTripUseCase.Execute(new SearchTripRequestDto()
                {
                    Origin = request.Origin!,
                    Destination = request.Destination!,
                    DateFrom = request.DateFrom!.Value,
                    DateTo = request.DateTo!.Value
                });

                /* OK VALUES ->
                {
                    "Origin" : "SFO",
                    "Destination" : "NYC",
                    "DateFrom" : "2024-10-25",
                    "DateTo" : "2024-11-05"
                }
                 */

                var responseVm = new SearchTripResponseViewModel()
                {
                    Trip = response.Trip,
                    NumberOfNights = response.NumberOfNights,
                    TotalCost = response.TotalCost
                };

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
