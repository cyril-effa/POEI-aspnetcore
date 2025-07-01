using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ex10_Final.Models;

namespace ex10_Final.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Order { get; set; } = default!;
        public DbSet<OrderDetail> OrderDetail { get; set; } = default!;
        public DbSet<Article> Article { get; set; } = default!;
        public DbSet<Warehouse> Warehouse { get; set; } = default!;
        public DbSet<Customer> Customer { get; set; } = default!;
        public DbSet<DeliverySlot> DeliverySlot { get; set; } = default!;
        public DbSet<DeliveryAssignment> DeliveryAssignment { get; set; } = default!;
        public DbSet<Facture> Facture { get; set; } = default!;
    }
}
