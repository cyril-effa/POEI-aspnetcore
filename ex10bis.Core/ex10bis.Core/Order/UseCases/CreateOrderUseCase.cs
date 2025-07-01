using ex10bis.Core.Order.Dtos;
using ex10bis.Core.Order.Interfaces;

namespace ex10bis.Core.Order.UseCases
{
    public class CreateOrderUseCase (IOrderRepository orderRepository) : ICreateOrderUseCase
    {
        public async Task<CreateOrderResponse> Execute(CreateOrderRequest request)
        {
            if (request == null)
            {
                return new CreateOrderResponse(false, "Invalid request", null);
            }
            var order = new Entities.Order
            {
                CustomerId = request.CustomerId,
                Customer = request.Customer,
                WarehouseId = request.WarehouseId,
                Warehouse = request.Warehouse,
                Delivery = request.Delivery,
                Facture = request.Facture,
                OrderDate = request.OrderDate,
                OrderStatus = request.OrderStatus,
                OrderDetails = request.OrderDetails,
                ShippingCost = request.ShippingCost,
                ShippingDuration = request.ShippingDuration
            };
            await orderRepository.AddAsync(order);
            return new CreateOrderResponse(true, "Order created successfully", order);
        }
    }
}