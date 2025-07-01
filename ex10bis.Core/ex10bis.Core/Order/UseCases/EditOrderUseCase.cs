using ex10bis.Core.Order.Dtos;
using ex10bis.Core.Order.Interfaces;

namespace ex10bis.Core.Warehouse.UseCases
{
    public class EditOrderUseCase(IOrderRepository orderRepository) : IEditOrderUseCase
    {
        public Task<EditOrderResponse> Execute(EditOrderRequest request)
        {
            if (request == null || request.Id <= 0)
            {
                return Task.FromResult(new EditOrderResponse(false, "Invalid request", null));
            }
            var order = orderRepository.GetByIdAsync(request.Id).Result;
            if (order == null)
            {
                return Task.FromResult(new EditOrderResponse(false, "Order not found", null));
            }
            order.CustomerId = request.CustomerId;
            order.Customer = request.Customer;
            order.WarehouseId = request.WarehouseId;
            order.Warehouse = request.Warehouse;
            order.Delivery = request.Delivery;
            order.Facture = request.Facture;
            order.OrderDate = request.OrderDate;
            order.OrderStatus = request.OrderStatus;
            order.OrderDetails = request.OrderDetails ?? new List<Entities.OrderDetail>();
            order.ShippingCost = request.ShippingCost;
            order.ShippingDuration = request.ShippingDuration;
            orderRepository.UpdateAsync(order);
            return Task.FromResult(new EditOrderResponse(true, $"Order updated successfully", order));
        }
    }
}
