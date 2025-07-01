using ex06_EntityFramework.Interfaces;
using ex06_EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex06_EntityFramework.Services
{
    internal class ArticleService : IArticleService
    {
        public List<Article> Articles { get; set; } = new List<Article>();

        public Article Add(Article article)
        {
            Articles.Add(article);
            return article;
        }

        public List<Article> GetArticlesBelowStock(int stock)
        {
            if (stock < 0)
            {
                throw new ArgumentException("Stock cannot be negative.", nameof(stock));
            }
            return Articles.Where(a => a.StockQuantity < stock).ToList();
        }

        public Dictionary<Article, int> GetTotalSalesPerArticle()
        {
            // This method is not implemented in the original code.
            // Assuming it should return a dictionary of articles and their total sales.
            // For now, returning an empty dictionary.
            return new Dictionary<Article, int>();
        }

        public Article UpdateArticleStock(int itemId, int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentException("Quantity cannot be negative.", nameof(quantity));
            }
            var article = Articles.FirstOrDefault(a => a.Id == itemId);
            if (article == null)
            {
                throw new KeyNotFoundException($"Article with ID {itemId} not found.");
            }
            article.StockQuantity += quantity;
            return article;
        }
    }
}
