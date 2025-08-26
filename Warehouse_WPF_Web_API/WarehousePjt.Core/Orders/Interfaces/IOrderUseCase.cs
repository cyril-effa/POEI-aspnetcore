using WarehousePjt.Core.Orders.Dtos;

namespace WarehousePjt.Core.Orders.Interfaces
{
    public interface IOrderUseCase
    {
        Task<CreateOrderResponse> Create(CreateOrderRequest request);
        Task<DeleteOrderResponse> Delete(DeleteOrderRequest request);
        Task<EditOrderResponse> Edit(EditOrderRequest request);
        Task<ReadOrderResponse> Read(ReadOrderRequest request);
        Task<ProcessOrderResponse> Process(ProcessOrderRequest request);
    }
}
