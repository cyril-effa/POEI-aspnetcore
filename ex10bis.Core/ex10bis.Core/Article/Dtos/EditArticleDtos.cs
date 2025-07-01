namespace ex10bis.Core.Article.Dtos
{
    public record EditArticleRequest(
        int Id,
        string Name,
        string Description,
        decimal Price,
        int StockQuantity);
    public record EditArticleResponse(
        bool Success,
        string Response,
        Entities.Article? Article);
}
