using ex06_EntityFramework.Interfaces;
using ex06_EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex06_EntityFramework.Services
{
    internal class OrderService : IOrderService
    {
        public List<Order> Orders { get; set; } = new List<Order>();

        public Order Add(Order order)
        {
            Orders.Add(order);
            return order;
        }

        public void DeleteOrder(int orderId)
        {
            Orders.Remove(Orders.FirstOrDefault(o => o.Id == orderId));
        }

        public List<Order> GetAllOrdersByCustomer(int customerId)
        {
            return Orders.Where(o => o.CustomerId == customerId).ToList();
        }

        public double GetAverageOrderValue()
        {
            if (Orders.Count == 0)
            {
                return 0;
            }
            return Orders.Average(o => o.TotalAmount);
        }

        public double GetAverageArticlePerOrder()
        {
            if (Orders.Count == 0)
            {
                return 0;
            }
            return Orders.Average(o => o.OrderDetails.Count);
        }

    }
}
