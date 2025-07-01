namespace ex10bis.Core.Customer.Dtos
{
    public record EditCustomerRequest(
        int Id,
        string UserId,
        string Name,
        string Email,
        string Address,
        string City,
        List<Entities.Order> Orders);
    public record EditCustomerResponse(
        bool Success,
        string Response,
        Entities.Customer? Customer);
}
