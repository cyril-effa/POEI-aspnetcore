namespace ex10bis.Core.Customer.Dtos
{
    public record ReadCustomerRequest(
        int Id);
    public record ReadCustomerResponse(
        bool Success,
        string Response,
        Entities.Customer? Customer);
}
