using ex07_WarehouseAPI.Data;
using ex07_WarehouseAPI.Interfaces;
using ex07_WarehouseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ex06_EntityFramework.Services
{
    internal class ArticleService : IRepository<Article>
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Article> _entities;

        public ArticleService(ApplicationDbContext context)
        {
            _context = context;
            _entities = _context.Set<Article>();
        }

        public async Task<List<Article>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<Article?> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task Add(Article entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Article entity)
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
