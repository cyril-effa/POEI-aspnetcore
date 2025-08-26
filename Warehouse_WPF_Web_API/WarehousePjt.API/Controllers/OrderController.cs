using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehousePjt.Core.Orders.Dtos;
using WarehousePjt.Core.Orders.Interfaces;

namespace WarehousePjt.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController (IOrderRepository orderRepository, IOrderUseCase orderUseCase) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await orderRepository.ListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await orderRepository.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderRequest request)
        {
            var response = await orderUseCase.Create(request);
            return CreatedAtAction(nameof(Get), new { id = response.Order.Id }, response.Order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EditOrderRequest request)
        {
            var order = await orderRepository.GetByIdAsync(id);
            if (order == null) return NotFound();
            await orderUseCase.Edit(request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await orderUseCase.Delete(new DeleteOrderRequest(id));
            return NoContent();
        }
    }

}
