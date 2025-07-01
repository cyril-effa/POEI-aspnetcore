using ex10bis.Core.Entities;

namespace ex10bis.Core.Interfaces
{
    public interface IDeliverySlotRepository
    {
        Task<List<DeliverySlot>> ListAsync();
        Task<DeliverySlot?> GetByIdAsync(int id);
        Task AddAsync(DeliverySlot deliverySlot);
        Task UpdateAsync(DeliverySlot deliverySlot);
        Task DeleteAsync(DeliverySlot deliverySlot);
    }
}
