using WarehousePjt.Core.Dtos;
using WarehousePjt.Core.Entities;

namespace WarehousePjt.Core.Orders.Dtos
{
    // CREATE
    public record CreateOrderRequest(
        int CustomerId,
        Customer? Customer,
        int WarehouseId,
        Warehouse? Warehouse,
        Delivery? Delivery,
        Facture? Facture,
        DateTime OrderDate,
        OrderStatus OrderStatus,
        List<OrderDetail> OrderDetails,
        double ShippingCost,
        int ShippingDuration);
    public record CreateOrderResponse(
        bool Success,
        string Response,
        Order? Order);

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
        Customer Customer,
        int WarehouseId,
        Warehouse Warehouse,
        Delivery? Delivery,
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

    // PROCESS
    public record ProcessOrderRequest(
        Entities.Order Order,
        ShippingResponse ShippingResponse);
    public record ProcessOrderResponse(
        bool Success,
        string Message);
}
