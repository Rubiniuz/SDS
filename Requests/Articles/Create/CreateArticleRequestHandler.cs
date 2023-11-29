using MediatR;
using SDS.Database;
using SDS.Models;

namespace SDS.Requests.Articles.Create;

public class CreateArticleRequestHandler : IRequestHandler<CreateArticleRequest, Article>
{
    private readonly SDSBackendContext _dbContext;

    public CreateArticleRequestHandler(SDSBackendContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Article> Handle(CreateArticleRequest request, CancellationToken cancellationToken)
    {
        var result = _dbContext.Articles.Add(request.article);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }
}