using ex10bis.Core.Entities;
using ex10bis.Core.Warehouse.Interfaces;
using ex10bis.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ex10bis.Infrastructure.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly ApplicationDbContext _context;
        public WarehouseRepository(ApplicationDbContext context) => _context = context;

        public async Task<List<Warehouse>> ListAsync() => await _context.Warehouse.Include(w => w.Orders)
                                                                                  .ThenInclude(o => o.Customer)
                                                                                  .ToListAsync();
        public async Task<Warehouse?> GetByIdAsync(int id) => await _context.Warehouse.Include(w => w.Orders)
                                                                                      .ThenInclude(o => o.Customer)
                                                                                      .Include(w => w.Orders)
                                                                                      .ThenInclude(o => o.OrderDetails)
                                                                                      .ThenInclude(od => od.Article)
                                                                                      .FirstOrDefaultAsync(w => w.Id == id);
        public async Task AddAsync(Warehouse warehouse)
        {
            _context.Warehouse.Add(warehouse);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Warehouse warehouse)
        {
            _context.Warehouse.Update(warehouse);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Warehouse warehouse)
        {
            _context.Warehouse.Remove(warehouse);
            await _context.SaveChangesAsync();
        }
    }
}