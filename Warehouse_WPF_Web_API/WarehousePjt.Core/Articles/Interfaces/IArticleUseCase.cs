using WarehousePjt.Core.Articles.Dtos;

namespace WarehousePjt.Core.Articles.Interfaces
{
    public interface IArticleUseCase
    {
        Task<CreateArticleResponse> Create(CreateArticleRequest request);
        Task<DeleteArticleResponse> Delete(DeleteArticleRequest request);
        Task<EditArticleResponse> Edit(EditArticleRequest request);
        Task<ReadArticleResponse> Read(ReadArticleRequest request);
    }
}
