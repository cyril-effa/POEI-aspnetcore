namespace ex10bis.Core.Delivery.Interfaces
{
    public interface IDeliveryRepository
    {
        Task<List<Entities.Delivery>> ListAsync();
        Task<Entities.Delivery?> GetByIdAsync(int id);
        Task AddAsync(Entities.Delivery delivery);
        Task UpdateAsync(Entities.Delivery delivery);
        Task DeleteAsync(Entities.Delivery delivery);
    }
}