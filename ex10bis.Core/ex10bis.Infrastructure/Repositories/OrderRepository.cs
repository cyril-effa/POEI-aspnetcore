using ex10bis.Core.Order.Interfaces;
using ex10bis.Core.Entities;
using ex10bis.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ex10bis.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context) => _context = context;

        public async Task<List<Order>> ListAsync() => await _context.Order.ToListAsync();
        public async Task<Order?> GetByIdAsync(int id) => await _context.Order.Include(o => o.Customer)
                                                                              .Include(o => o.Warehouse)
                                                                              .Include(o => o.Facture)
                                                                              .Include(o => o.Delivery)
                                                                              .ThenInclude(d => d.DeliverySlots)
                                                                              .Include(o => o.OrderDetails)
                                                                              .ThenInclude(od => od.Article)
                                                                              .FirstOrDefaultAsync(o => o.Id == id);
        public async Task AddAsync(Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Order order)
        {
            var existingOrder = await _context.Order
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.Id == order.Id);

            if (existingOrder is null)
                return;

            // Met à jour les propriétés scalaires
            _context.Entry(existingOrder).CurrentValues.SetValues(order);
            existingOrder.OrderDetails = order.OrderDetails;

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Order order)
        {
            var existingOrder = await _context.Order.FindAsync(order.Id);
            if (existingOrder is null)
                return;

            _context.Order.Remove(existingOrder);
            await _context.SaveChangesAsync();
        }
    }
}