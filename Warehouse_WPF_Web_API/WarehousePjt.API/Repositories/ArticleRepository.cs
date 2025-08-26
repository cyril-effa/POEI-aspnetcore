using WarehousePjt.Core.Articles.Interfaces;
using WarehousePjt.Core.Entities;
using WarehousePjt.API.Data;
using Microsoft.EntityFrameworkCore;

namespace WarehousePjt.API.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _context;
        public ArticleRepository(ApplicationDbContext context) => _context = context;

        public async Task<List<Article>> ListAsync() => await _context.Article.ToListAsync();
        public async Task<Article?> GetByIdAsync(int id) => await _context.Article.FindAsync(id);
        public async Task AddAsync(Article article)
        {
            _context.Article.Add(article);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Article article)
        {
            _context.Article.Update(article);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Article article)
        {
            _context.Article.Remove(article);
            await _context.SaveChangesAsync();
        }
    }
}