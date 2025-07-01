namespace ex10bis.Core.Delivery.Dtos
{
    public record ReadDeliveryRequest(
        int Id);
    public record ReadDeliveryResponse(
        bool Success,
        string Response,
        Entities.Delivery? Delivery);
}
