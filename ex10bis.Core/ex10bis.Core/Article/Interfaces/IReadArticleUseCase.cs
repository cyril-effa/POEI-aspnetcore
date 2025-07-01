using ex10bis.Core.Article.Dtos;

namespace ex10bis.Core.Article.Interfaces
{
    public interface IReadArticleUseCase
    {
        Task<ReadArticleResponse> Execute(ReadArticleRequest request);
    }
}