using ex10bis.Core.Warehouse.Dtos;

namespace ex10bis.Core.Warehouse.Interfaces
{
    public interface IDeleteWarehouseUseCase
    {
        Task<DeleteWarehouseResponse> Execute(DeleteWarehouseRequest request);
    }
}