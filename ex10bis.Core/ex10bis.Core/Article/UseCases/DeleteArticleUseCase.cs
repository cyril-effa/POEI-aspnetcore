using ex10bis.Core.Article.Dtos;
using ex10bis.Core.Article.Interfaces;

namespace ex10bis.Core.Article.UseCases
{
    public class DeleteArticleUseCase(IArticleRepository articleRepository) : IDeleteArticleUseCase
    {
        public Task<DeleteArticleResponse> Execute(DeleteArticleRequest request)
        {
            if (request == null || request.Id <= 0)
            {
                return Task.FromResult(new DeleteArticleResponse(false, "Invalid request"));
            }
            var article = articleRepository.GetByIdAsync(request.Id).Result;
            if (article == null)
            {
                return Task.FromResult(new DeleteArticleResponse(false, "Article not found"));
            }
            articleRepository.DeleteAsync(article);
            return Task.FromResult(new DeleteArticleResponse(true, "Article deleted successfully"));
        }
    }
}
