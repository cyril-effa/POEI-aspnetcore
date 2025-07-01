using ex10bis.Core.Order.Dtos;

namespace ex10bis.Core.Order.Interfaces
{
    public interface IEditOrderUseCase
    {
        Task<EditOrderResponse> Execute(EditOrderRequest request);
    }
}