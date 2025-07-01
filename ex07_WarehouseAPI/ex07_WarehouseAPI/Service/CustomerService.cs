using ex07_WarehouseAPI.Data;
using ex07_WarehouseAPI.Interfaces;
using ex07_WarehouseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ex06_EntityFramework.Services
{
    internal class CustomerService : IRepository<Customer>
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Customer> _entities;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
            _entities = _context.Set<Customer>();
        }

        public async Task<List<Customer>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<Customer?> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task Add(Customer entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Customer entity)
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
