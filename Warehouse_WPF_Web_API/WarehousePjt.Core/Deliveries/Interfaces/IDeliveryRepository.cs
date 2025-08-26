using WarehousePjt.Core.Entities;

namespace WarehousePjt.Core.Deliveries.Interfaces
{
    public interface IDeliveryRepository
    {
        Task<List<Delivery>> ListAsync();
        Task<Delivery?> GetByIdAsync(int id);
        Task AddAsync(Delivery delivery);
        Task UpdateAsync(Delivery delivery);
        Task DeleteAsync(Delivery delivery);
    }
}