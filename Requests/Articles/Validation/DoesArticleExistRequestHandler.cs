using MediatR;
using Microsoft.EntityFrameworkCore;
using SDS.Database;

namespace SDS.Requests.Articles.Validation;

public class DoesArticleExistRequestHandler : IRequestHandler<DoesArticleExistRequest, bool>
{
    private readonly SDSBackendContext _dbContext;

    public DoesArticleExistRequestHandler(SDSBackendContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DoesArticleExistRequest request, CancellationToken cancellationToken)
    {
        return await _dbContext.Articles
            .AsNoTracking()
            .AnyAsync(article => article.ArticleId == request.ArticleId, cancellationToken);
    }
}