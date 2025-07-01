namespace ex10bis.Core.Article.Interfaces
{
    public interface IArticleRepository
    {
        Task<List<Entities.Article>> ListAsync();
        Task<Entities.Article?> GetByIdAsync(int id);
        Task AddAsync(Entities.Article article);
        Task UpdateAsync(Entities.Article article);
        Task DeleteAsync(Entities.Article article);
    }
}