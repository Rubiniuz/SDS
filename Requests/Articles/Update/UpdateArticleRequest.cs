using SDS.Mediator.Requests;
using SDS.Models;

namespace SDS.Requests.Articles.Update;

public class UpdateArticleRequest : IUpdateRequest<int>
{
    public int ArticleId { get; set; }
    public string Sku { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public UpdateArticleRequest(int articleId, string sku, string name, decimal price)
    {
        ArticleId = articleId;
        Sku = sku;
        Name = name;
        Price = price;
    }

    public UpdateArticleRequest(int id, Article article)
    {
        ArticleId = id;
        Sku = article.Sku ?? "";
        Name = article.Name ?? "";
        Price = article.Price ?? 0;
    }
}