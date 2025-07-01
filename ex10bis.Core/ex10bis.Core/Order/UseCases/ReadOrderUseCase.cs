using ex10bis.Core.Order.Dtos;
using ex10bis.Core.Order.Interfaces;

namespace ex10bis.Core.Order.UseCases
{
    public class ReadOrderUseCase (IOrderRepository orderRepository) : IReadOrderUseCase
    {
        public Task<ReadOrderResponse> Execute(ReadOrderRequest request)
        {
            if (request == null || request.Id <= 0)
            {
                return Task.FromResult(new ReadOrderResponse(false, "Invalid request", null));
            }
            var order = orderRepository.GetByIdAsync(request.Id).Result;
            if (order == null)
            {
                return Task.FromResult(new ReadOrderResponse(false, "Order not found", null));
            }
            return Task.FromResult(new ReadOrderResponse(true, "Order retrieved successfully", order));
        }
    }
}
