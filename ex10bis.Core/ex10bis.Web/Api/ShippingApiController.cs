using Microsoft.AspNetCore.Mvc;
using static ex10bis.Core.Dtos.ShippingDtos;

namespace ex10bis.Web.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippingApiController : ControllerBase
    {
        [HttpPost]
        public ActionResult<ShippingResponse> Post([FromBody] ShippingRequest request)
        {
            // Génération d'une réponse factice
            var response = new ShippingResponse(
                new Random().Next(10000, 99999),
                new Random().Next(5, 25),
                "Planned");
            return Ok(response);
        }
    }
}
