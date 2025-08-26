using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehousePjt.Core.Deliveries.Dtos;
using WarehousePjt.Core.Deliveries.Interfaces;

namespace WarehousePjt.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Livreur,Magasinier")]
    public class DeliveryController (IDeliveryRepository deliveryRepository, IDeliveryUseCase deliveryUseCase) : ControllerBase
    {
        //[HttpGet("planning")]
        //public async Task<IActionResult> GetPlanning() => Ok(await deliveryRepository.GetPlanningAsync());

        [HttpPost("{id}/delivered")]
        public async Task<IActionResult> MarquerCommeLivree(int id)
        {
            var delivery = await deliveryRepository.GetByIdAsync(id);
            var order = delivery.Order;
            var customer = order.Customer;
            await deliveryUseCase.ConfirmDelivery(new ConfirmDeliveryRequest(order, customer));
            return NoContent();
        }
    }

}
