using DHLFakeApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DHLFakeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippingController : ControllerBase
    {
        [HttpPost]
        public ActionResult<ShippingResponse> Post([FromBody] ShippingRequest request)
        {
            // G�n�ration d'une r�ponse factice
            var response = new ShippingResponse
            {
                ShippingNumber = new Random().Next(10000, 99999),
                Cost = new Random().Next(5, 25),
                Status = "Planned"
            };
            return Ok(response);
        }
    }
}