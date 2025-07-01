using ex10bis.Core.Customer.Dtos;
using ex10bis.Core.Customer.Interfaces;

namespace ex10bis.Core.Customer.UseCases
{
    public class EditCustomerUseCase(ICustomerRepository customerRepository) : IEditCustomerUseCase
    {
        public Task<EditCustomerResponse> Execute(EditCustomerRequest request)
        {
            if (request == null || request.Id <= 0)
            {
                return Task.FromResult(new EditCustomerResponse(false, "Invalid request", null));
            }
            var customer = customerRepository.GetByIdAsync(request.Id).Result;
            if (customer == null)
            {
                return Task.FromResult(new EditCustomerResponse(false, "Customer not found", null));
            }
            customer.UserId = request.UserId;
            customer.Name = request.Name;
            customer.Email = request.Email;
            customer.Address = request.Address;
            customer.City = request.City;
            customer.Orders = request.Orders ?? new List<Entities.Order>();
            customerRepository.UpdateAsync(customer);
            return Task.FromResult(new EditCustomerResponse(true, $"Customer updated successfully", customer));
        }
    }
}
