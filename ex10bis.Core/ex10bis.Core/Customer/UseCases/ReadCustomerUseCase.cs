using ex10bis.Core.Customer.Dtos;
using ex10bis.Core.Customer.Interfaces;

namespace ex10bis.Core.Customer.UseCases
{
    public class ReadCustomerUseCase(ICustomerRepository customerRepository) : IReadCustomerUseCase
    {
        public Task<ReadCustomerResponse> Execute(ReadCustomerRequest request)
        {
            if (request == null || request.Id <= 0)
            {
                return Task.FromResult(new ReadCustomerResponse(false, "Invalid request", null));
            }
            var customer = customerRepository.GetByIdAsync(request.Id).Result;
            if (customer == null)
            {
                return Task.FromResult(new ReadCustomerResponse(false, "Customer not found", null));
            }
            return Task.FromResult(new ReadCustomerResponse(true, "Customer retrieved successfully", customer));
        }
    }
}
