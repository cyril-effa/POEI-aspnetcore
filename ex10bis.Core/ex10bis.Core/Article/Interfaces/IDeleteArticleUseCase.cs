using ex10bis.Core.Article.Dtos;

namespace ex10bis.Core.Article.Interfaces
{
    public interface IDeleteArticleUseCase
    {
        Task<DeleteArticleResponse> Execute(DeleteArticleRequest request);
    }
}