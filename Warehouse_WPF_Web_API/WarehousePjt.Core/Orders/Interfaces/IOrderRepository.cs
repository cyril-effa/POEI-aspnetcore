using WarehousePjt.Core.Entities;

namespace WarehousePjt.Core.Orders.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> ListAsync();
        Task<Order?> GetByIdAsync(int id);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Order order);
    }
}