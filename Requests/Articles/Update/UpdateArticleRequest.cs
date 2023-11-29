using SDS.Mediator.Requests;
using SDS.Models;

namespace SDS.Requests.Articles.Update;

public class UpdateArticleRequest : IUpdateRequest<int>
{
    public int ArticleId { get; set; }
    public string Sku { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public UpdateArticleRequest(Article article)
    {
        ArticleId = article.ArticleId;
        Sku = article.Sku ?? "";
        Name = article.Name ?? "";
        Price = article.Price ?? 0;
    }
}