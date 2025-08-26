using WarehousePjt.Core.Entities;

namespace WarehousePjt.Core.Interfaces
{
    public interface IFactureRepository
    {
        Task<List<Facture>> ListAsync();
        Task<Facture?> GetByIdAsync(int id);
        Task AddAsync(Facture facture);
        Task UpdateAsync(Facture facture);
        Task DeleteAsync(Facture facture);
    }
}
