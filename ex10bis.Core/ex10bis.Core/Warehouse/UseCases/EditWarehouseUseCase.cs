using ex10bis.Core.Warehouse.Dtos;
using ex10bis.Core.Warehouse.Interfaces;

namespace ex10bis.Core.Warehouse.UseCases
{
    public class EditWarehouseUseCase(IWarehouseRepository warehouseRepository) : IEditWarehouseUseCase
    {
        public Task<EditWarehouseResponse> Execute(EditWarehouseRequest request)
        {
            if (request == null || request.Id <= 0)
            {
                return Task.FromResult(new EditWarehouseResponse(false, "Invalid request", null));
            }
            var warehouse = warehouseRepository.GetByIdAsync(request.Id).Result;
            if (warehouse == null)
            {
                return Task.FromResult(new EditWarehouseResponse(false, "Warehouse not found", null));
            }
            warehouse.Name = request.Name;
            warehouse.Address = request.Address;
            warehouse.PostalCode = request.PostalCode;
            warehouse.Orders = request.Orders ?? new List<Entities.Order>();
            warehouseRepository.UpdateAsync(warehouse);
            return Task.FromResult(new EditWarehouseResponse(true, $"Warehouse updated successfully", warehouse));
        }
    }
}
