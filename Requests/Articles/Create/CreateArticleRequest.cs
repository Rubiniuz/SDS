using SDS.Mediator.Requests;
using SDS.Models;

namespace SDS.Requests.Articles.Create;

public class CreateArticleRequest : ICreateRequest<Article>
{
    public Article article;

    public CreateArticleRequest(Article _article)
    {
        article = _article;
    }
}