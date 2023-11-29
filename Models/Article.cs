namespace SDS.Models;

public partial class Article
{
    public int ArticleId { get; set; }

    public string? Sku { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }
}
