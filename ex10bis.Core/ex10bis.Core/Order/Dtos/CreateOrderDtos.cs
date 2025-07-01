using ex10bis.Core.Entities;

namespace ex10bis.Core.Order.Dtos
{
    public record CreateOrderRequest(
        int CustomerId,
        Entities.Customer? Customer,
        int WarehouseId,
        Entities.Warehouse? Warehouse,
        Entities.Delivery? Delivery,
        Facture? Facture,
        DateTime OrderDate,
        OrderStatus OrderStatus,
        List<OrderDetail> OrderDetails,
        double ShippingCost,
        int ShippingDuration);
    public record CreateOrderResponse(
        bool Success,
        string Response,
        Entities.Order? Order);
}
