using Microsoft.EntityFrameworkCore;

namespace ex06_EntityFramework.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }

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

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(c => c.Customer)
                .HasForeignKey(c => c.CustomerId);

            modelBuilder.Entity<Customer>()
                .HasOne(a => a.ShippingAddress)
                .WithMany();
        }
    }

}
