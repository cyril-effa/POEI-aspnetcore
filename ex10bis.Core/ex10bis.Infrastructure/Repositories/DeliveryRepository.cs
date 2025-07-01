using ex10bis.Core.Entities;
using ex10bis.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ex10bis.Core.Delivery.Interfaces;

namespace ex10bis.Infrastructure.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly ApplicationDbContext _context;
        public DeliveryRepository(ApplicationDbContext context) => _context = context;

        public async Task<List<Delivery>> ListAsync() => await _context.Delivery.Include(d => d.DeliverySlots).ToListAsync();
        public async Task<Delivery?> GetByIdAsync(int id) => await _context.Delivery.Include(d => d.DeliverySlots).FirstOrDefaultAsync(d => d.Id == id);
        public async Task AddAsync(Delivery delivery)
        {
            _context.Delivery.Add(delivery);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Delivery delivery)
        {
            _context.Delivery.Update(delivery);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Delivery delivery)
        {
            _context.Delivery.Remove(delivery);
            await _context.SaveChangesAsync();
        }
    }
}