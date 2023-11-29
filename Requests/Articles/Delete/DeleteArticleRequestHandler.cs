using MediatR;
using Microsoft.EntityFrameworkCore;
using SDS.Database;

namespace SDS.Requests.Articles.Delete;

public class DeleteArticleRequestHandler : IRequestHandler<DeleteArticleRequest>
{
    private readonly SDSBackendContext _dbContext;

    public DeleteArticleRequestHandler(SDSBackendContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeleteArticleRequest request, CancellationToken cancellationToken)
    {
        var article = await _dbContext.Articles.Where(x => x.ArticleId == request.ArticleId)
            .FirstOrDefaultAsync(cancellationToken);

        if (article == null)
            throw new Exception("Article not found");

        var filteredData = _dbContext.Articles
            .FirstOrDefault(x => x.ArticleId == article.ArticleId);

        if(filteredData == null)
            throw new Exception("Article not valid");

        _dbContext.Articles.Remove(filteredData);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}