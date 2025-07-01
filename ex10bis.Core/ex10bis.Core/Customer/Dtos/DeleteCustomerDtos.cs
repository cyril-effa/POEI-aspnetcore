namespace ex10bis.Core.Customer.Dtos
{
    public record DeleteCustomerRequest(
        int Id);
    public record DeleteCustomerResponse(
        bool Success,
        string Response);
}
