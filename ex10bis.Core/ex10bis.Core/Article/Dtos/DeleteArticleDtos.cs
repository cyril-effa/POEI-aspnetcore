namespace ex10bis.Core.Article.Dtos
{
    public record DeleteArticleRequest(
        int Id);
    public record DeleteArticleResponse(
        bool Success,
        string Response);
}
