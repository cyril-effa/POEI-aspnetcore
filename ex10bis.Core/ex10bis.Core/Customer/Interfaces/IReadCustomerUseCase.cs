using ex10bis.Core.Customer.Dtos;

namespace ex10bis.Core.Customer.Interfaces
{
    public interface IReadCustomerUseCase
    {
        Task<ReadCustomerResponse> Execute(ReadCustomerRequest request);
    }
}