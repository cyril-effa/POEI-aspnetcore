using ex10bis.Core.Order.Dtos;

namespace ex10bis.Core.Order.Interfaces
{
    public interface IDeleteOrderUseCase
    {
        Task<DeleteOrderResponse> Execute(DeleteOrderRequest request);
    }
}