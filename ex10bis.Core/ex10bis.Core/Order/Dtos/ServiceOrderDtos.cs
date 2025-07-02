using ex10bis.Core.Dtos;

namespace ex10bis.Core.Order.Dtos
{
    public record ProcessOrderRequest(
        Entities.Order Order,
        ShippingResponse ShippingResponse);
    public record ProcessOrderResponse(
        bool Success,
        string Message);

    public record PlanDeliveryRequest(
        Entities.Order Order,
        DateTime DeliveryDate,
        string DeliveryAddress);
    public record PlanDeliveryResponse(
        bool Success,
        string Message,
        DateTime ScheduledDate);
}
