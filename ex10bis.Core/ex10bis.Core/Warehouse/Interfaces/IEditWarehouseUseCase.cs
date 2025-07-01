using ex10bis.Core.Warehouse.Dtos;

namespace ex10bis.Core.Warehouse.Interfaces
{
    public interface IEditWarehouseUseCase
    {
        Task<EditWarehouseResponse> Execute(EditWarehouseRequest request);
    }
}