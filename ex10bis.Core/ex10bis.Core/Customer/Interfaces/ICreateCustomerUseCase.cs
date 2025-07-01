using ex10bis.Core.Customer.Dtos;

namespace ex10bis.Core.Customer.Interfaces
{
    public interface ICreateCustomerUseCase
    {
        Task<CreateCustomerResponse> Execute(CreateCustomerRequest request);
    }
}