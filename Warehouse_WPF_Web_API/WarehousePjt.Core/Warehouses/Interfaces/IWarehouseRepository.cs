using WarehousePjt.Core.Entities;

namespace WarehousePjt.Core.Warehouses.Interfaces
{
    public interface IWarehouseRepository
    {
        Task<List<Warehouse>> ListAsync();
        Task<Warehouse?> GetByIdAsync(int id);
        Task AddAsync(Warehouse warehouse);
        Task UpdateAsync(Warehouse warehouse);
        Task DeleteAsync(Warehouse warehouse);
    }
}