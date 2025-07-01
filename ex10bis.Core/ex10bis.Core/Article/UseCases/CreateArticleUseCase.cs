using ex10bis.Core.Article.Dtos;
using ex10bis.Core.Article.Interfaces;

namespace ex10bis.Core.Article.UseCases
{
    public class CreateArticleUseCase (IArticleRepository articleRepository) : ICreateArticleUseCase
    {
        public async Task<CreateArticleResponse> Execute(CreateArticleRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Request cannot be null");
            }
            var article = new Entities.Article
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                StockQuantity = request.StockQuantity
            };
            await articleRepository.AddAsync(article);
            return new CreateArticleResponse(
                Success: true,
                Response: "Article created successfully",
                Article: article);
        }
    }
}