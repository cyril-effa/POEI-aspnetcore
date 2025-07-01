using ex07_WarehouseAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ex07_WarehouseAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ex06_EntityFramework;Trusted_Connection=True;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().Property(c => c.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Customer>().Property(c => c.Email).IsRequired();
            modelBuilder.Entity<Customer>().Property(c => c.Address).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Customer>().Property(c => c.City).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<Order>().Property(o => o.OrderDate).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.OrderStatus).IsRequired();

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(o => o.Order)
                .HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Article)
                .WithMany()
                .HasForeignKey(od => od.ArticleId);

            modelBuilder.Entity<Warehouse>()
                .Property(e => e.CodeAccesMD5)
                .HasConversion(v => string.Join(";", v),
                v => v.Split(new[] { ';' }, StringSplitOptions.None).ToList());

            modelBuilder.Entity<Warehouse>()
               .HasMany(w => w.Orders)
               .WithOne()
               .HasForeignKey(o => o.WarehouseId);

            modelBuilder.Entity<Customer>()
               .HasMany(c => c.Orders)
               .WithOne()
               .HasForeignKey(o => o.CustomerId);
        }
    }
}
