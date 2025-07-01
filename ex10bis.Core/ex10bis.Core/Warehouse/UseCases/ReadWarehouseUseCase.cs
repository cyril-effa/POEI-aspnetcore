using ex10bis.Core.Warehouse.Dtos;
using ex10bis.Core.Warehouse.Interfaces;

namespace ex10bis.Core.Warehouse.UseCases
{
    public class ReadWarehouseUseCase(IWarehouseRepository warehouseRepository) : IReadWarehouseUseCase
    {
        public Task<ReadWarehouseResponse> Execute(ReadWarehouseRequest request)
        {
            if (request == null || request.Id <= 0)
            {
                return Task.FromResult(new ReadWarehouseResponse(false, "Invalid request", null));
            }
            var warehouse = warehouseRepository.GetByIdAsync(request.Id).Result;
            if (warehouse == null)
            {
                return Task.FromResult(new ReadWarehouseResponse(false, "Warehouse not found", null));
            }
            return Task.FromResult(new ReadWarehouseResponse(true, "Warehouse retrieved successfully", warehouse));
        }
    }
}
