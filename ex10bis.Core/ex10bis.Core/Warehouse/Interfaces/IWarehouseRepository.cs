namespace ex10bis.Core.Warehouse.Interfaces
{
    public interface IWarehouseRepository
    {
        Task<List<Entities.Warehouse>> ListAsync();
        Task<Entities.Warehouse?> GetByIdAsync(int id);
        Task AddAsync(Entities.Warehouse warehouse);
        Task UpdateAsync(Entities.Warehouse warehouse);
        Task DeleteAsync(Entities.Warehouse warehouse);
    }
}