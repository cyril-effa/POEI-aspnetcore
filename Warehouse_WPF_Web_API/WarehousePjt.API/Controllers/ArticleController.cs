using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehousePjt.Core.Articles.Dtos;
using WarehousePjt.Core.Articles.Interfaces;

namespace WarehousePjt.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController (IArticleRepository articleRepository, IArticleUseCase articleUseCase) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await articleRepository.ListAsync());

        [HttpPost]
        public async Task<IActionResult> Create(CreateArticleRequest request)
        {
            var response = await articleUseCase.Create(request);
            return CreatedAtAction(nameof(GetAll), new { id = response.Article.Id}, response.Article);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EditArticleRequest request)
        {
            var article = await articleRepository.GetByIdAsync(id);
            if (article == null) return NotFound();
            await articleUseCase.Edit(request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await articleUseCase.Delete(new DeleteArticleRequest(id));
            return NoContent();
        }
    }
}
