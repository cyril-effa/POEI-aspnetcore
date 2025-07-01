using ex10bis.Core.Article.Dtos;
using ex10bis.Core.Article.Interfaces;

namespace ex10bis.Core.Article.UseCases
{
    public class EditArticleUseCase (IArticleRepository articleRepository) : IEditArticleUseCase
    {
        public Task<EditArticleResponse> Execute(EditArticleRequest request)
        {
            if (request == null || request.Id <= 0)
            {
                return Task.FromResult(new EditArticleResponse(false, "Invalid request", null));
            }
            var article = articleRepository.GetByIdAsync(request.Id).Result;
            if (article == null)
            {
                return Task.FromResult(new EditArticleResponse(false, "Article not found", null));
            }
            article.Name = request.Name;
            article.Description = request.Description;
            article.Price = request.Price;
            article.StockQuantity = request.StockQuantity;
            articleRepository.UpdateAsync(article);
            return Task.FromResult(new EditArticleResponse(true, "Article updated successfully", article));
        }
    }
}
