namespace ex10bis.Core.Article.Dtos
{
    public record CreateArticleRequest(
        string Name,
        string Description,
        decimal Price,
        int StockQuantity);
    public record CreateArticleResponse(
            bool Success,
            string Response,
            Entities.Article? Article);
}
