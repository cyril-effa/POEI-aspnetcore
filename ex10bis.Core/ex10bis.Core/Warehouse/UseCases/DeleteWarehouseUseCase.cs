using ex10bis.Core.Warehouse.Dtos;
using ex10bis.Core.Warehouse.Interfaces;

namespace ex10bis.Core.Warehouse.UseCases
{
    public class DeleteWarehouseUseCase(IWarehouseRepository warehouseRepository) : IDeleteWarehouseUseCase
    {
        public Task<DeleteWarehouseResponse> Execute(DeleteWarehouseRequest request)
        {
            if (request == null || request.Id <= 0)
            {
                return Task.FromResult(new DeleteWarehouseResponse(false, "Invalid request"));
            }
            var warehouse = warehouseRepository.GetByIdAsync(request.Id).Result;
            if (warehouse == null)
            {
                return Task.FromResult(new DeleteWarehouseResponse(false, "Warehouse not found"));
            }
            warehouseRepository.DeleteAsync(warehouse);
            return Task.FromResult(new DeleteWarehouseResponse(true, "Warehouse deleted successfully"));
        }
    }
}
