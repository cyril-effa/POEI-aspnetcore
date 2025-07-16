namespace ex10bis.Core.Article.Dtos
{
    // CREATE
    public record CreateArticleRequest(
        string Name,
        string Description,
        decimal Price,
        int StockQuantity);
    public record CreateArticleResponse(
            bool Success,
            string Response,
            Entities.Article? Article);

    // DELETE
    public record DeleteArticleRequest(
        int Id);
    public record DeleteArticleResponse(
        bool Success,
        string Response);

    // EDIT
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

    // READ
    public record ReadArticleRequest(
        int Id);
    public record ReadArticleResponse(
        bool Success,
        string Response,
        Entities.Article? Article);
}
