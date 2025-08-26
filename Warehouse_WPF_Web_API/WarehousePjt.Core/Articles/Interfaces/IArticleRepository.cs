using WarehousePjt.Core.Entities;

namespace WarehousePjt.Core.Articles.Interfaces
{
    public interface IArticleRepository
    {
        Task<List<Article>> ListAsync();
        Task<Article?> GetByIdAsync(int id);
        Task AddAsync(Article article);
        Task UpdateAsync(Article article);
        Task DeleteAsync(Article article);
    }
}