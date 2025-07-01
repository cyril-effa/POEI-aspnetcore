namespace ex10bis.Core.Delivery.Dtos
{
    public record DeleteDeliveryRequest(
        int Id);
    public record DeleteDeliveryResponse(
        bool Success,
        string Response);
}
