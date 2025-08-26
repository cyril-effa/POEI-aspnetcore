using WarehousePjt.Core.Warehouses.Dtos;

namespace WarehousePjt.Core.Warehouses.Interfaces
{
    public interface IWarehouseUseCase
    {
        Task<CreateWarehouseResponse> Create(CreateWarehouseRequest request);
        Task<DeleteWarehouseResponse> Delete(DeleteWarehouseRequest request);
        Task<EditWarehouseResponse> Edit(EditWarehouseRequest request);
        Task<ReadWarehouseResponse> Read(ReadWarehouseRequest request);
    }
}
