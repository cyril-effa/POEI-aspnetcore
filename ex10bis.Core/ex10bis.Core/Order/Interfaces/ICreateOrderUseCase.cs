using ex10bis.Core.Order.Dtos;

namespace ex10bis.Core.Order.Interfaces
{
    public interface ICreateOrderUseCase
    {
        Task<CreateOrderResponse> Execute(CreateOrderRequest request);
    }
}