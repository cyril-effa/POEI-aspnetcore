using ex10bis.Core.Interfaces;
using ex10bis.Core.Entities;
using ex10bis.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ex10bis.Infrastructure.Repositories
{
    public class DeliverySlotRepository : IDeliverySlotRepository
    {
        private readonly ApplicationDbContext _context;
        public DeliverySlotRepository(ApplicationDbContext context) => _context = context;

        public async Task<List<DeliverySlot>> ListAsync() => await _context.DeliverySlot.ToListAsync();
        public async Task<DeliverySlot?> GetByIdAsync(int id) => await _context.DeliverySlot.FindAsync(id);
        public async Task AddAsync(DeliverySlot deliverySlot)
        {
            _context.DeliverySlot.Add(deliverySlot);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(DeliverySlot deliverySlot)
        {
            _context.DeliverySlot.Update(deliverySlot);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(DeliverySlot deliverySlot)
        {
            _context.DeliverySlot.Remove(deliverySlot);
            await _context.SaveChangesAsync();
        }
    }
}