using WarehousePjt.Core.Interfaces;
using WarehousePjt.Core.Entities;
using WarehousePjt.API.Data;
using Microsoft.EntityFrameworkCore;

namespace WarehousePjt.API.Repositories
{
    public class FactureRepository : IFactureRepository
    {
        private readonly ApplicationDbContext _context;
        public FactureRepository(ApplicationDbContext context) => _context = context;

        public async Task<List<Facture>> ListAsync() => await _context.Facture.ToListAsync();
        public async Task<Facture?> GetByIdAsync(int id) => await _context.Facture.FindAsync(id);
        public async Task AddAsync(Facture facture)
        {
            _context.Facture.Add(facture);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Facture facture)
        {
            _context.Facture.Update(facture);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Facture facture)
        {
            _context.Facture.Remove(facture);
            await _context.SaveChangesAsync();
        }
    }
}