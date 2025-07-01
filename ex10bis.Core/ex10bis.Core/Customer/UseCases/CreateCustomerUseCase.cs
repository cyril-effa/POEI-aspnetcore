using ex10bis.Core.Customer.Dtos;
using ex10bis.Core.Customer.Interfaces;

namespace ex10bis.Core.Customer.UseCases
{
    public class CreateCustomerUseCase(ICustomerRepository customerRepository) : ICreateCustomerUseCase
    {
        public async Task<CreateCustomerResponse> Execute(CreateCustomerRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Email))
            {
                return new CreateCustomerResponse(false, "Invalid request", null);
            }
            var customer = new Entities.Customer
            {
                UserId = request.UserId,
                Name = request.Name,
                Email = request.Email,
                Address = request.Address,
                City = request.City,
                Orders = request.Orders ?? new List<Entities.Order>()
            };
            await customerRepository.AddAsync(customer);
            return new CreateCustomerResponse(true, "Customer created successfully", customer);
        }
    }
}