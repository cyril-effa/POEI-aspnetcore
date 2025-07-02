using ex10bis.Core.Order.Dtos;

namespace ex10bis.Core.Order.Interfaces
{
    public interface ICrudOrderUseCase
    {
        Task<CreateOrderResponse> Create(CreateOrderRequest request);
        Task<DeleteOrderResponse> Delete(DeleteOrderRequest request);
        Task<EditOrderResponse> Edit(EditOrderRequest request);
        Task<ReadOrderResponse> Read(ReadOrderRequest request);
    }
}
