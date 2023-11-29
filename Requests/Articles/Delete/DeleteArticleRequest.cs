using SDS.Mediator.Requests;

namespace SDS.Requests.Articles.Delete;

public class DeleteArticleRequest : IDeleteRequest
{
    public int ArticleId { get; set; }

    public DeleteArticleRequest(int articleId)
    {
        ArticleId = articleId;
    }
}