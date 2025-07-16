using ex10bis.Core.Customer.Dtos;

namespace ex10bis.Core.Customer.Interfaces
{
    public interface ICustomerUseCase
    {
        Task<CreateCustomerResponse> Execute(CreateCustomerRequest request);
    }
}