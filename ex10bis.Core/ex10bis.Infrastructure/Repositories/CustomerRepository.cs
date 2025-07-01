using ex10bis.Core.Customer.Interfaces;
using ex10bis.Core.Entities;
using ex10bis.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ex10bis.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context) => _context = context;

        public async Task<List<Customer>> ListAsync() => await _context.Customer.ToListAsync();
        public async Task<Customer?> GetByIdAsync(int id) => await _context.Customer.FindAsync(id);
        public async Task AddAsync(Customer customer)
        {
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Customer customer)
        {
            _context.Customer.Update(customer);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Customer customer)
        {
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}