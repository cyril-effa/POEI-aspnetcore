namespace ex10bis.Core.Delivery.Dtos
{
    public record ConfirmDeliveryRequest(
        Entities.Order Order,
        Entities.Customer Customer);
    public record ConfirmDeliveryResponse(
        bool Success,
        string Message);

    public record CancelDeliveryRequest(
        Entities.Order Order);
    public record CancelDeliveryResponse(
        bool Success,
        string Message);
}
