using SDS.Mediator.Requests;
using SDS.Models;

namespace SDS.Requests.Articles.Get;

public class GetArticleByIdRequest : IReadRequest<Article>
{
    public GetArticleByIdRequest(int id)
    {
        Id = id;
    }
    public int Id { get; set; }
}