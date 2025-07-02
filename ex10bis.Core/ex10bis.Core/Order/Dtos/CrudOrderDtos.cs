using ex10bis.Core.Entities;

namespace ex10bis.Core.Order.Dtos
{
    // CREATE
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

    // DELETE
    public record DeleteOrderRequest(
        int Id);
    public record DeleteOrderResponse(
        bool Success,
        string Response);

    // EDIT
    public record EditOrderRequest(
        int Id,
        int CustomerId,
        Entities.Customer Customer,
        int WarehouseId,
        Entities.Warehouse Warehouse,
        Entities.Delivery? Delivery,
        Facture? Facture,
        DateTime OrderDate,
        OrderStatus OrderStatus,
        List<OrderDetail> OrderDetails,
        double ShippingCost,
        int ShippingDuration);
    public record EditOrderResponse(
        bool Success,
        string Response,
        Entities.Order? Order);

    // READ
    public record ReadOrderRequest(
        int Id);
    public record ReadOrderResponse(
        bool Success,
        string Response,
        Entities.Order? Order);
}
