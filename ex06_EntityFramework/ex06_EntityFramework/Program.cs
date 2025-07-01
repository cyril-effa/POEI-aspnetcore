using ex06_EntityFramework.Models;
using ex06_EntityFramework.Services;

namespace ex06_EntityFramework
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            InsertDefaultData();

            var parser = new CsvParserService();
            parser.ParseCsv("./ressources/export.csv");

            DisplayAllArticles();
        }


        private static void InsertDefaultData()
        {
            using (var context = new ApplicationDbContext())
            {
                var warehouse1 = new Warehouse
                {
                    Name = "Entrepot de Paris",
                    Address = "10 rue du csharp",
                    PostalCode = 75000,
                    CodeAccesMD5 = new List<string>
                        {
                             "840e998a22948adf5de39bd4f2b35da7" ,
                           "74b87337454200d4d33f80c4663dc5e5"
                        }
                };

                var warehouse2 = new Warehouse
                {
                    Name = "Entrepot de Nantes",
                    Address = "15 rue du code",
                    PostalCode = 44000,
                    CodeAccesMD5 = new List<string>
                        {
                             "840e998a22948adf5de39bd4f2b35da6" ,
                           "74b87337454200d4d33f80c4663dc5e4"
                        }
                };

                var articles = new List<Article>
                    {
                        new Article { Name = "Ordinateur Portable", Description = "Ordinateur portable haute performance", Price = 1200.00m, StockQuantity = 50 },
                        new Article { Name = "Smartphone", Description = "Smartphone avec écran AMOLED", Price = 800.00m, StockQuantity = 100 },
                        new Article { Name = "Tablette", Description = "Tablette 10 pouces avec stylet", Price = 600.00m, StockQuantity = 30 }
                    };

                var orders = new List<Order>
                    {
                        new Order
                        {
                            Warehouse = warehouse1,
                            Customer = new Customer
                            {
                                Email = "johndoe@example.com",
                                ShippingAddress = new Address
                                {
                                    Street = "123 Main Street",
                                    City = "Paris"
                                }
                            },
                            OrderDate = DateTime.Now,
                            TotalAmount = 2000.00d,
                            OrderStatus = "Processing",
                            OrderDetails = new List<OrderDetail>
                            {
                                new OrderDetail { Article = articles[1], Quantity = 1, UnitPrice = 1200.00m },
                                new OrderDetail { Article = articles[1], Quantity = 1, UnitPrice = 800.00m }
                            }
                        },
                        new Order
                        {
                            Warehouse = warehouse2,
                            Customer = new Customer
                            {
                                Email = "bill.gate@example.com",
                                ShippingAddress = new Address
                                {
                                    Street = "One Microsoft Way",
                                    City = "Nantes"
                                }
                            },
                            OrderDate = DateTime.Now,
                            TotalAmount = 2000.00d,
                            OrderStatus = "Processing",
                            OrderDetails = new List<OrderDetail>
                            {
                                new OrderDetail { Article = articles[2], Quantity = 1, UnitPrice = 800.00m }
                            }
                        }
                    };

                context.Warehouses.Add(warehouse1);
                context.Warehouses.Add(warehouse2);
                context.Articles.AddRange(articles);
                context.Orders.AddRange(orders);
                context.SaveChanges();
            }
        }

        static void DisplayAllArticles()
        {
            using (var context = new ApplicationDbContext())
            {
                var articles = context.Articles.ToList();
                foreach (var a in articles)
                {
                    //Id,Name,Description,Price,StockQuantity
                    Console.WriteLine($"ID: {a.Id}, Name: {a.Name}, Price: {a.Price}, NbStock: {a.StockQuantity}");
                }

                var orders = context.Orders.ToList();
                foreach (var o in orders)
                {
                    //Id,WarehouseId,CustomerId,Email,ShippingAddress,City,OrderDate,TotalAmount,OrderStatus
                    Console.WriteLine($"ID: {o.Id}, WarehouseId: {o.WarehouseId}, CustomerId: {o.CustomerId}, OrderDate: {o.OrderDate}, TotalAmount: {o.TotalAmount}, OrderStatus: {o.OrderStatus}");
                }

                var orderDetails = context.OrderDetails.ToList();
                foreach (var od in orderDetails)
                {
                    //Id,OrderId,ArticleId,Quantity,UnitPrice
                    Console.WriteLine($"ID: {od.Id}, OrderId: {od.OrderId}, ArticleId: {od.ArticleId}, Quantity: {od.Quantity}, UnitPrice: {od.UnitPrice}");
                }
            }
        }
    }
}