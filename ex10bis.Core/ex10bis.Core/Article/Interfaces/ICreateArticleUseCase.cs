using ex10bis.Core.Article.Dtos;

namespace ex10bis.Core.Article.Interfaces
{
    public interface ICreateArticleUseCase
    {
        Task<CreateArticleResponse> Execute(CreateArticleRequest request);
    }
}