using WarehousePjt.Core.Orders.Interfaces;
using WarehousePjt.Core.Entities;
using WarehousePjt.API.Data;
using Microsoft.EntityFrameworkCore;

namespace WarehousePjt.API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context) => _context = context;

        public async Task<List<Order>> ListAsync() => await _context.Order.Include(o => o.Customer)
                                                                          .Include(o => o.Warehouse)
                                                                          .ToListAsync();
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
            _context.Order.Update(order);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Order order)
        {
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}