using ex06_EntityFramework.Services;
using ex07_WarehouseAPI.Data;
using ex07_WarehouseAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ex07_WarehouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public StatisticsController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        /// <summary>
        /// Page de statistiques globales
        /// </summary>
        /// <returns></returns>
        [HttpGet("GlobalStats")]
        public IActionResult GetGlobalStatistics()
        {
            var articlesUnder5 = context.Articles.Where(a => a.StockQuantity < 5).ToList();
            var averageOrderValue = context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Article)
                .AsEnumerable()
                .Select(o => o.TotalAmount)
                .Average();
            var averageArticlePerOrder = context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Article)
                .AsEnumerable()
                .Select(o => o.OrderDetails.Count)
                .Average();
            var mostSoldArticle = context.OrderDetails
                .Include(o => o.Article)
                .GroupBy(od => od.ArticleId)
                .Select(g => new
                {
                    ArticleId = g.Key,
                    TotalQuantity = g.Sum(od => od.Quantity)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .FirstOrDefault();

            return Ok(new
            {
                ArticlesUnder5 = articlesUnder5.Select(a => new
                {
                    a.Id,
                    a.Name,
                    a.StockQuantity
                }),
                AverageOrderValue = averageOrderValue,
                AverageArticlePerOrder = averageArticlePerOrder,
                mostSoldArticle = mostSoldArticle != null ? new
                {
                    ArticleId = mostSoldArticle.ArticleId,
                    TotalQuantity = mostSoldArticle.TotalQuantity,
                    ArticleName = context.Articles.FirstOrDefault(a => a.Id == mostSoldArticle.ArticleId)?.Name
                } : null
            });
        }
    }
}
