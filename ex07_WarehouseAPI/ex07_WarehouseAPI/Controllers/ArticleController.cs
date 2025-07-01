using ex07_WarehouseAPI.Data;
using ex07_WarehouseAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ex07_WarehouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ArticleController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        /// <summary>
        /// Ajoute un nouvel article à la liste des articles.
        /// </summary>
        /// <returns></returns>
        [HttpPost("CreateArticle")]
        public IActionResult CreateArticle(Article article)
        {
            if (article == null)
            {
                return BadRequest();
            }

            context.Articles.Add(article);
            context.SaveChanges();

            return CreatedAtAction(nameof(GetArticleById), new { id = article.Id }, article);
        }

        [HttpPatch("UpdateArticleStock")]
        public IActionResult UpdateArticleStock(int articleId, int stockQuantity)
        {
            var article = context.Articles.Find(articleId);

            if (article == null)
            {
                return NotFound();
            }

            article.StockQuantity = stockQuantity;
            context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Récupère un article par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant de l'article</param>
        /// <returns>Retourne l'Article</returns>
        [HttpGet("GetArticle/{id}")]
        public IActionResult GetArticleById(int id)
        {
            var article = context.Articles.Find(id);

            if (article == null)
                return NotFound();

            return Ok(article);
        }

        /// <summary>
        /// Récupère la liste de tous les articles.
        /// </summary>
        /// <returns>Retourne les articles</returns>
        [HttpGet("/api/articles")]
        public IActionResult GetAllArticles()
        {
            var articles = context.Articles.ToList();
            return Ok(articles);
        }
    }
}
