using WarehousePjt.Core.Entities;

namespace WarehousePjt.Core.Customers.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> ListAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(Customer customer);
    }
}