using ex07_WarehouseAPI.Models;

namespace ex07_WarehouseAPI.Interfaces
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
        List<Order> GetAllOrdersByCustomer(string customerName);

        // Method to get average order value
        double GetAverageOrderValue();

        // Method to get average number article by order
        double GetAverageArticlePerOrder();
    }
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T?> GetById(int id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}
