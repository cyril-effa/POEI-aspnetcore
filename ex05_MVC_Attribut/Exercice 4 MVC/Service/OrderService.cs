using BO;

namespace Exercice_5_MVC.Service
{
    public class OrderService
    {

        private static ApplicationDbContext dbContext = new ApplicationDbContext();


        public List<Order> GetOrders()
        {
            return dbContext.Orders;
        }

        public void Add(Order newOrder)
        {
            dbContext.Orders.Add(newOrder);
        }

        public void Remove(Order newOrder)
        {
            dbContext.Orders.Remove(newOrder);
        }

        internal void Update(Order orderUpdated)
        {
            var orderBdd = dbContext.Orders.SingleOrDefault(w => w.Id == orderUpdated.Id);
            orderBdd = orderUpdated;
        }

    }
}
