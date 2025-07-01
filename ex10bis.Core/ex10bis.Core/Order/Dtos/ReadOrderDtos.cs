namespace ex10bis.Core.Order.Dtos
{
    public record ReadOrderRequest(
        int Id);
    public record ReadOrderResponse(
        bool Success,
        string Response,
        Entities.Order? Order);
}
