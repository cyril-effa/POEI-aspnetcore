using ex10bis.Core.Article.Dtos;
using ex10bis.Core.Article.Interfaces;

namespace ex10bis.Core.Article.UseCases
{
    public class ReadArticleUseCase(IArticleRepository articleRepository) : IReadArticleUseCase
    {
        public Task<ReadArticleResponse> Execute(ReadArticleRequest request)
        {
            if (request == null || request.Id <= 0)
            {
                return Task.FromResult(new ReadArticleResponse(false, "Invalid request", null));
            }
            var article = articleRepository.GetByIdAsync(request.Id).Result;
            if (article == null)
            {
                return Task.FromResult(new ReadArticleResponse(false, "Article not found", null));
            }
            return Task.FromResult(new ReadArticleResponse(true, "Article retrieved successfully", article));
        }
    }
}
