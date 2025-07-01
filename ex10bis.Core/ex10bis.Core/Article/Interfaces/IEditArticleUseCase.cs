using ex10bis.Core.Article.Dtos;

namespace ex10bis.Core.Article.Interfaces
{
    public interface IEditArticleUseCase
    {
        Task<EditArticleResponse> Execute(EditArticleRequest request);
    }
}