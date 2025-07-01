namespace ex10bis.Core.Order.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Entities.Order>> ListAsync();
        Task<Entities.Order?> GetByIdAsync(int id);
        Task AddAsync(Entities.Order order);
        Task UpdateAsync(Entities.Order order);
        Task DeleteAsync(Entities.Order order);
    }
}