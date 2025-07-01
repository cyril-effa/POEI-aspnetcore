namespace ex10bis.Core.Customer.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Entities.Customer>> ListAsync();
        Task<Entities.Customer?> GetByIdAsync(int id);
        Task AddAsync(Entities.Customer customer);
        Task UpdateAsync(Entities.Customer customer);
        Task DeleteAsync(Entities.Customer customer);
    }
}