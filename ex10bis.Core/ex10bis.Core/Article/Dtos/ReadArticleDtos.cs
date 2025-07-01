namespace ex10bis.Core.Article.Dtos
{
    public record ReadArticleRequest(
        int Id);
    public record ReadArticleResponse(
        bool Success,
        string Response,
        Entities.Article? Article);
}
