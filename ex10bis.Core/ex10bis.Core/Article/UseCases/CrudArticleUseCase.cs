using ex10bis.Core.Article.Dtos;
using ex10bis.Core.Article.Interfaces;

namespace ex10bis.Core.Article.UseCases
{
    public class CrudArticleUseCase(IArticleRepository articleRepository) : ICrudArticleUseCase
    {
        public async Task<CreateArticleResponse> Create(CreateArticleRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Request cannot be null");
            }
            if (string.IsNullOrWhiteSpace(request.Name) || request.Price <= 0 || request.StockQuantity < 0)
            {
                return new CreateArticleResponse(false, "Invalid input data", null);
            }

            var article = new Entities.Article
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                StockQuantity = request.StockQuantity
            };

            try
            {
                await articleRepository.AddAsync(article);
                return new CreateArticleResponse(true, "Article created successfully", article);
            }
            catch (Exception ex)
            {
                return new CreateArticleResponse(false, $"Error creating article: {ex.Message}", null);
            }
        }

        public Task<DeleteArticleResponse> Delete(DeleteArticleRequest request)
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

            try
            {
                articleRepository.DeleteAsync(article);
                return Task.FromResult(new DeleteArticleResponse(true, "Article deleted successfully"));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new DeleteArticleResponse(false, $"Error deleting article: {ex.Message}"));
            }
        }

        public Task<EditArticleResponse> Edit(EditArticleRequest request)
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

            try
            {
                articleRepository.UpdateAsync(article);
                return Task.FromResult(new EditArticleResponse(true, "Article updated successfully", article));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new EditArticleResponse(false, $"Error updating article: {ex.Message}", null));
            }
        }

        public Task<ReadArticleResponse> Read(ReadArticleRequest request)
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
