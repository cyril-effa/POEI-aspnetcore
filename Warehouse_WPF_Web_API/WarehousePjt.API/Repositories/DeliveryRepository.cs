using WarehousePjt.Core.Entities;
using WarehousePjt.Core.Deliveries.Interfaces;
using WarehousePjt.API.Data;
using Microsoft.EntityFrameworkCore;

namespace WarehousePjt.API.Repositories
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