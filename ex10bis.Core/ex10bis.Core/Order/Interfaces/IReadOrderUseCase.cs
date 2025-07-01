using ex10bis.Core.Order.Dtos;

namespace ex10bis.Core.Order.Interfaces
{
    public interface IReadOrderUseCase
    {
        Task<ReadOrderResponse> Execute(ReadOrderRequest request);
    }
}