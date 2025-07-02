using ex10bis.Core.Warehouse.Dtos;
namespace ex10bis.Core.Warehouse.Interfaces
{
    public interface ICrudWarehouseUseCase
    {
        Task<CreateWarehouseResponse> Create(CreateWarehouseRequest request);
        Task<DeleteWarehouseResponse> Delete(DeleteWarehouseRequest request);
        Task<EditWarehouseResponse> Edit(EditWarehouseRequest request);
        Task<ReadWarehouseResponse> Read(ReadWarehouseRequest request);
    }
}
