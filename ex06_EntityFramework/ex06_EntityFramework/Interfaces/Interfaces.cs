using ex06_EntityFramework.Models;

namespace ex06_EntityFramework.Interfaces
{
    public interface IWarehouseService
    {
        // Method to fetch all warehouses
        List<Warehouse> GetAllWarehouses();
    }

    public interface IArticleService
    {
        // Method to add new article
        Article Add(Article article);

        // Method to update the stock quantity of an Article
        Article UpdateArticleStock(int itemId, int quantity);

        // Method to fetch all Articles that are below given stock
        List<Article> GetArticlesBelowStock(int stock);

        // Method to get total sales for each Article
        Dictionary<Article, int> GetTotalSalesPerArticle();
    }

    public interface IOrderService
    {
        // Method to add new Order
        Order Add(Order order);

        // Method to delete an order
        void DeleteOrder(int orderId);

        // Method to fetch all orders made by a specific customer
        List<Order> GetAllOrdersByCustomer(int customerId);

        // Method to get average order value
        double GetAverageOrderValue();

        // Method to get average number article by order
        double GetAverageArticlePerOrder();
    }
}
