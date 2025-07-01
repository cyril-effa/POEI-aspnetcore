using ex10bis.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ex10bis.Infrastructure.Data
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
        public DbSet<Delivery> Delivery { get; set; } = default!;
        public DbSet<Facture> Facture { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Delivery)
                .WithOne(da => da.Order)
                .HasForeignKey<Delivery>(da => da.OrderId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Facture)
                .WithOne(f => f.Order)
                .HasForeignKey<Facture>(f => f.OrderId);
        }
    }
}
