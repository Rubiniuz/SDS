using MediatR;
using Microsoft.EntityFrameworkCore;
using SDS.Database;
using SDS.Models;

namespace SDS.Requests.Articles.Get;

public class GetArticleByIdRequestHandler : IRequestHandler<GetArticleByIdRequest, Article>
{
    private readonly SDSBackendContext _dbContext;

    public GetArticleByIdRequestHandler(SDSBackendContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Article> Handle(GetArticleByIdRequest request, CancellationToken cancellationToken)
    {
        var value = await _dbContext.Articles
            .Where(x => x.ArticleId == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if(value == null)
        {
            throw new Exception("Article not found");
        }
        return value;
    }
}