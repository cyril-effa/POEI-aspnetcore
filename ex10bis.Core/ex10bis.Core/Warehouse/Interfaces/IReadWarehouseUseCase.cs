using ex10bis.Core.Warehouse.Dtos;

namespace ex10bis.Core.Warehouse.Interfaces
{
    public interface IReadWarehouseUseCase
    {
        Task<ReadWarehouseResponse> Execute(ReadWarehouseRequest request);
    }
}