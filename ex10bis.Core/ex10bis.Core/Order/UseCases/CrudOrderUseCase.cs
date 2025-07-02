using ex10bis.Core.Order.Dtos;
using ex10bis.Core.Order.Interfaces;

namespace ex10bis.Core.Order.UseCases
{
    public class CrudOrderUseCase (IOrderRepository orderRepository) : ICrudOrderUseCase
    {
        public async Task<CreateOrderResponse> Create(CreateOrderRequest request)
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

        public Task<DeleteOrderResponse> Delete(DeleteOrderRequest request)
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

        public Task<EditOrderResponse> Edit(EditOrderRequest request)
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

        public Task<ReadOrderResponse> Read(ReadOrderRequest request)
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
