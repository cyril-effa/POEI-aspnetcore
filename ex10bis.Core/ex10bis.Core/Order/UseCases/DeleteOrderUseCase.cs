using ex10bis.Core.Order.Dtos;
using ex10bis.Core.Order.Interfaces;

namespace ex10bis.Core.Order.UseCases
{
    public class DeleteOrderUseCase(IOrderRepository orderRepository) : IDeleteOrderUseCase
    {
        public Task<DeleteOrderResponse> Execute(DeleteOrderRequest request)
        {
            if (request == null || request.Id <= 0)
            {
                return Task.FromResult(new DeleteOrderResponse(false, "Invalid request"));
            }
            var order = orderRepository.GetByIdAsync(request.Id).Result;
            if (order == null)
            {
                return Task.FromResult(new DeleteOrderResponse(false, "Order not found"));
            }
            orderRepository.DeleteAsync(order);
            return Task.FromResult(new DeleteOrderResponse(true, "Order deleted successfully"));
        }
    }
}
