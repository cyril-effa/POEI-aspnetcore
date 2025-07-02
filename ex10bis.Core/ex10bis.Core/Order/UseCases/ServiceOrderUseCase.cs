using ex10bis.Core.Dtos;
using ex10bis.Core.Entities;
using ex10bis.Core.Order.Dtos;
using ex10bis.Core.Order.Interfaces;

namespace ex10bis.Core.Order.UseCases
{
    public class ServiceOrderUseCase(IOrderRepository orderRepository) : IServiceOrderUseCase
    {
        public Task<ProcessOrderResponse> Process(ProcessOrderRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Request cannot be null");
            }
            if (request.Order == null)
            {
                throw new ArgumentNullException(nameof(request.Order), "Order cannot be null");
            }

            request.Order.ShippingCost = request.ShippingResponse.Cost;
            request.Order.OrderStatus = OrderStatus.Processing;
            orderRepository.UpdateAsync(request.Order);

            return Task.FromResult(new ProcessOrderResponse(true, "Order processed successfully."));
        }

        public Task<PlanDeliveryResponse> PlanDelivery(PlanDeliveryRequest request)
        {

        }
    }
}
