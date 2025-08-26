using WarehousePjt.Core.Customers.Dtos;

namespace WarehousePjt.Core.Customers.Interfaces
{
    public interface ICustomerUseCase
    {
        Task<CreateCustomerResponse> Execute(CreateCustomerRequest request);
    }
}