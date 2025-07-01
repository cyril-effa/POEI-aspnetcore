using ex07_WarehouseAPI.Data;
using ex07_WarehouseAPI.Interfaces;
using ex07_WarehouseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ex06_EntityFramework.Services
{
    internal class OrderService : IRepository<Order>
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Order> _entities;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
            _entities = _context.Set<Order>();
        }

        public async Task<List<Order>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<Order?> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task Add(Order entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Order entity)
        {
            _entities.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _entities.FindAsync(id);
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
