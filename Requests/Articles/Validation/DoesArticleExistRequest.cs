using SDS.Mediator.Requests;

namespace SDS.Requests.Articles.Validation;

public class DoesArticleExistRequest : IReadRequest<bool>
{
    public int ArticleId { get; }
    public DoesArticleExistRequest(int articleId)
    {
        ArticleId = articleId;
    }
}