using WarehousePjt.Core.Customers.Dtos;
using WarehousePjt.Core.Customers.Interfaces;
using WarehousePjt.Core.Entities;

namespace WarehousePjt.Core.Customers.UseCases
{
    public class CustomerUseCase(ICustomerRepository customerRepository) : ICustomerUseCase
    {
        public async Task<CreateCustomerResponse> Execute(CreateCustomerRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Email))
            {
                return new CreateCustomerResponse(false, "Invalid request", null);
            }
            var customer = new Customer
            {
                UserId = request.UserId,
                Name = request.Name,
                Email = request.Email,
                Address = request.Address,
                City = request.City,
                Orders = request.Orders ?? new List<Order>()
            };

            try
            {
                await customerRepository.AddAsync(customer);
                return new CreateCustomerResponse(true, "Customer created successfully", customer);
            }
            catch (Exception ex)
            {
                return new CreateCustomerResponse(false, $"Error creating customer: {ex.Message}", null);
            }
        }
    }
}