using ex07_WarehouseAPI.Data;
using ex07_WarehouseAPI.Interfaces;
using ex07_WarehouseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ex06_EntityFramework.Services
{
    internal class WarehouseService : IRepository<Warehouse>
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Warehouse> _entities;

        public WarehouseService(ApplicationDbContext context)
        {
            _context = context;
            _entities = _context.Set<Warehouse>();
        }

        public async Task<List<Warehouse>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<Warehouse?> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task Add(Warehouse entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Warehouse entity)
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
