namespace ex10bis.Core.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> ListAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T element);
        Task UpdateAsync(T element);
        Task DeleteAsync(T element);
    }
}
