using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehousePjt.Core.Warehouses.Dtos;
using WarehousePjt.Core.Warehouses.Interfaces;

namespace WarehousePjt.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WarehouseController(IWarehouseRepository warehouseRepository, IWarehouseUseCase warehouseUseCase) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await warehouseRepository.ListAsync());

        [HttpPost]
        public async Task<IActionResult> Create(CreateWarehouseRequest request)
        {
            var response = await warehouseUseCase.Create(request);
            return CreatedAtAction(nameof(GetAll), new { id = response.Warehouse.Id }, response.Warehouse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EditWarehouseRequest request)
        {
            var warehouse = await warehouseRepository.GetByIdAsync(id);
            if (warehouse == null) return NotFound();
            await warehouseUseCase.Edit(request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await warehouseUseCase.Delete(new DeleteWarehouseRequest(id));
            return NoContent();
        }
    }
}
