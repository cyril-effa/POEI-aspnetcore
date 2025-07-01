using CsvHelper;
using System.Globalization;
using ex06_EntityFramework.Models;

namespace ex06_EntityFramework.Services
{
    public class CsvParserService
    {
        public void ParseCsv(string filePath)
        {
            using (var context = new ApplicationDbContext())
            {
                using var reader = new StreamReader(filePath);
                string? line;
                string currentObject = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    switch (line)
                    {
                        case "# Articles":
                            currentObject = "Article";
                            break;
                        case "# Orders":
                            currentObject = "Order";
                            break;
                        case "# OrderDetails":
                            currentObject = "OrderDetail";
                            break;
                        case "":
                            currentObject = string.Empty;
                            break;
                        default:
                            var column = line.Split(',');
                            if (column[0] == "Id") continue;

                            switch (currentObject)
                            {
                                case "Article":
                                    //Id,Name,Description,Price,StockQuantity
                                    Article article = new Article
                                    {
                                        //Id = int.Parse(column[0]),
                                        Name = column[1],
                                        Description = column[2],
                                        Price = decimal.Parse(column[3], CultureInfo.InvariantCulture),
                                        StockQuantity = int.Parse(column[4])
                                    };
                                    context.Articles.Add(article);
                                    break;
                                case "Order":
                                    //Id,WarehouseId,CustomerId,Email,ShippingAddress,City,OrderDate,TotalAmount,OrderStatus
                                    Order order = new Order
                                    {
                                        //Id = int.Parse(column[0]),
                                        Warehouse = context.Warehouses.FirstOrDefault(w => w.Id == int.Parse(column[1])),
                                        Customer = context.Customers.FirstOrDefault(c => c.Id == int.Parse(column[2])) ?? new Customer
                                        {
                                            Email = column[3],
                                            ShippingAddress = new Address
                                            {
                                                Street = column[4],
                                                City = column[5]
                                            }
                                        },
                                        OrderDate = DateTime.Parse(column[6], CultureInfo.InvariantCulture),
                                        TotalAmount = double.Parse(column[7], CultureInfo.InvariantCulture),
                                        OrderStatus = column[8]
                                    };
                                    context.Orders.Add(order);
                                    break;
                                case "OrderDetail":
                                    //Id,OrderId,ArticleId,Quantity,UnitPrice
                                    OrderDetail orderDetail = new OrderDetail
                                    {
                                        //Id = int.Parse(column[0]),
                                        OrderId = int.Parse(column[1]),
                                        ArticleId = int.Parse(column[2]),
                                        Quantity = int.Parse(column[3]),
                                        UnitPrice = decimal.Parse(column[4], CultureInfo.InvariantCulture)
                                    };
                                    context.OrderDetails.Add(orderDetail);
                                    Order? updateOrder = context.Orders.FirstOrDefault(o => o.Id == orderDetail.OrderId);
                                    if (updateOrder != null)
                                    {
                                        updateOrder.OrderDetails.Add(orderDetail);
                                        context.Orders.Update(updateOrder);
                                    }
                                    break;
                            }
                            break;
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}