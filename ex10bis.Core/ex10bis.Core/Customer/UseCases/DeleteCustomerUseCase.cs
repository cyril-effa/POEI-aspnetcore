using ex10bis.Core.Customer.Dtos;
using ex10bis.Core.Customer.Interfaces;

namespace ex10bis.Core.Customer.UseCases
{
    public class DeleteCustomerUseCase(ICustomerRepository customerRepository) : IDeleteCustomerUseCase
    {
        public Task<DeleteCustomerResponse> Execute(DeleteCustomerRequest request)
        {
            if (request == null || request.Id <= 0)
            {
                return Task.FromResult(new DeleteCustomerResponse(false, "Invalid request"));
            }
            var customer = customerRepository.GetByIdAsync(request.Id).Result;
            if (customer == null)
            {
                return Task.FromResult(new DeleteCustomerResponse(false, "Customer not found"));
            }
            customerRepository.DeleteAsync(customer);
            return Task.FromResult(new DeleteCustomerResponse(true, "Customer deleted successfully"));
        }
    }
}
