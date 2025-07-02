using ex10bis.Core.Article.Dtos;

namespace ex10bis.Core.Article.Interfaces
{
    public interface ICrudArticleUseCase
    {
        Task<CreateArticleResponse> Create(CreateArticleRequest request);
        Task<DeleteArticleResponse> Delete(DeleteArticleRequest request);
        Task<EditArticleResponse> Edit(EditArticleRequest request);
        Task<ReadArticleResponse> Read(ReadArticleRequest request);
    }
}
