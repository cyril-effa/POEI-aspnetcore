namespace ex10bis.Core.Customer.Dtos
{
    public record CreateCustomerRequest(
        string UserId,
        string Name,
        string Email,
        string Address,
        string City,
        List<Entities.Order> Orders);
    public record CreateCustomerResponse(
        bool Success,
        string Response,
        Entities.Customer? Customer);
}
