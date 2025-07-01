using ex10bis.Core.Warehouse.Dtos;

namespace ex10bis.Core.Warehouse.Interfaces
{
    public interface ICreateWarehouseUseCase
    {
        Task<CreateWarehouseResponse> Execute(CreateWarehouseRequest request);
    }
}