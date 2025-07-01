using ex10bis.Core.Warehouse.Dtos;
using ex10bis.Core.Warehouse.Interfaces;

namespace ex10bis.Core.Warehouse.UseCases
{
    public class CreateWarehouseUseCase(IWarehouseRepository warehouseRepository) : ICreateWarehouseUseCase
    {
        public async Task<CreateWarehouseResponse> Execute(CreateWarehouseRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || 
                string.IsNullOrWhiteSpace(request.Address) || 
                request.PostalCode <= 0)
            {
                return new CreateWarehouseResponse(false, "Invalid input data", null);
            }
            var warehouse = new Entities.Warehouse
            {
                Name = request.Name,
                Address = request.Address,
                PostalCode = request.PostalCode
            };
            await warehouseRepository.AddAsync(warehouse);
            return new CreateWarehouseResponse(true, "Warehouse created successfully", warehouse);
        }
    }
}