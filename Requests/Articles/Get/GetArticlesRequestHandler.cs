using MediatR;
using Microsoft.EntityFrameworkCore;
using SDS.Database;
using SDS.Models;

namespace SDS.Requests.Articles.Get;

public class GetArticlesRequestHandler : IRequestHandler<GetArticlesRequest, List<Article>>
{
    private readonly SDSBackendContext _dbContext;

    public GetArticlesRequestHandler(SDSBackendContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Article>> Handle(GetArticlesRequest request, CancellationToken cancellationToken)
    {
        return await _dbContext.Articles.ToListAsync(cancellationToken);
    }
}