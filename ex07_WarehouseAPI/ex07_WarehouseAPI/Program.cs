
using ex07_WarehouseAPI.Data;
using ex07_WarehouseAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Runtime.CompilerServices;

namespace ex07_WarehouseAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Warehouse API",
                        Description = "API de gestion des entrepots",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Cyril RASTEL",
                            Email = "@"
                        }
                    });

                string xmlPath = Path.Combine(AppContext.BaseDirectory, "Swagger.xml");
                c.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddDbContext<Data.ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            InsertDefaultData();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Warehouse API");
                    c.RoutePrefix = string.Empty; // Serve Swagger at the app's root
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void InsertDefaultData()
        {
            using (var context = new ApplicationDbContext())
            {
                var warehouses = new List<Warehouse>
                {
                    new Warehouse
                    {
                        //Id = 1,
                        Name = "Entrepot de Paris",
                        Address = "10 rue du csharp",
                        PostalCode = 75000,
                        CodeAccesMD5 = new List<string>
                        {
                            "840e998a22948adf5de39bd4f2b35da7",
                            "74b87337454200d4d33f80c4663dc5e5"
                        }
                    },
                    new Warehouse
                    {
                        //Id = 2,
                        Name = "Entrepot de Nantes",
                        Address = "15 rue du code",
                        PostalCode = 44000,
                        CodeAccesMD5 = new List<string>
                        {
                            "840e998a22948adf5de39bd4f2b35da6",
                            "74b87337454200d4d33f80c4663dc5e4"
                        }
                    }
                };

                context.Warehouses.AddRange(warehouses);
                context.SaveChanges();

                var customers = new List<Customer>
                {
                    new Customer
                    {
                        //Id = 1,
                        Name = "John Doe",
                        Email = "johndoe@example.com",
                        Address = "123 Main Street",
                        City = "Paris",
                    },
                    new Customer
                    {
                        //Id = 2,
                        Name = "Bill Gates",
                        Email = "bill.gate@example.com",
                        Address = "One Microsoft Way",
                        City = "Nantes",
                    }
                };

                context.Customers.AddRange(customers);
                context.SaveChanges();

                var articles = new List<Article>
                    {
                        new Article { Name = "Ordinateur Portable", Description = "Ordinateur portable haute performance", Price = 1200.00m, StockQuantity = 50 },
                        new Article { Name = "Smartphone", Description = "Smartphone avec écran AMOLED", Price = 800.00m, StockQuantity = 100 },
                        new Article { Name = "Tablette", Description = "Tablette 10 pouces avec stylet", Price = 600.00m, StockQuantity = 30 }
                    };

                context.Articles.AddRange(articles);
                context.SaveChanges();

                var orders = new List<Order>
                    {
                        new Order
                        {
                            //Id = 1,
                            WarehouseId = warehouses[0].Id,
                            CustomerId = customers[0].Id,
                            OrderDate = DateTime.Now,
                            OrderStatus = "Processing",
                            OrderDetails = new List<OrderDetail>
                            {
                                new OrderDetail { Article = articles[0], Quantity = 1 },
                                new OrderDetail { Article = articles[1], Quantity = 1 }
                            }
                        },
                        new Order
                        {
                            //Id = 2,
                            WarehouseId = warehouses[1].Id,
                            CustomerId = customers[1].Id,
                            OrderDate = DateTime.Now,
                            OrderStatus = "Processing",
                            OrderDetails = new List<OrderDetail>
                            {
                                new OrderDetail { Article = articles[2], Quantity = 1 }
                            }
                        }
                    };

                context.Orders.AddRange(orders);
                context.SaveChanges();
            }
        }
    }
}
