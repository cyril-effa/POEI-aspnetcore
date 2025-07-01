using ex10bis.Core.Customer.Dtos;

namespace ex10bis.Core.Customer.Interfaces
{
    public interface IEditCustomerUseCase
    {
        Task<EditCustomerResponse> Execute(EditCustomerRequest request);
    }
}