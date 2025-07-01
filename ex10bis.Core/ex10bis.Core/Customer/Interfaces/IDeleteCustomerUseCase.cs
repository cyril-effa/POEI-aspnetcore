using ex10bis.Core.Customer.Dtos;

namespace ex10bis.Core.Customer.Interfaces
{
    public interface IDeleteCustomerUseCase
    {
        Task<DeleteCustomerResponse> Execute(DeleteCustomerRequest request);
    }
}