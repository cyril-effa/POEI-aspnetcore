namespace ex10bis.Core.Order.Dtos
{
    public record DeleteOrderRequest(
        int Id);
    public record DeleteOrderResponse(
        bool Success,
        string Response);
}
