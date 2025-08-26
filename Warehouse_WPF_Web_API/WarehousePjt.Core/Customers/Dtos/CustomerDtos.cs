using WarehousePjt.Core.Entities;

namespace WarehousePjt.Core.Customers.Dtos
{
    public record CreateCustomerRequest(
        string UserId,
        string Name,
        string Email,
        string Address,
        string City,
        List<Order> Orders);
    public record CreateCustomerResponse(
        bool Success,
        string Response,
        Customer? Customer);
}
