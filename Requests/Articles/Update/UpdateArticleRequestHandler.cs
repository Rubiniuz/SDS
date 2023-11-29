using MediatR;
using SDS.Database;
using SDS.Models;

namespace SDS.Requests.Articles.Update;

public class UpdateArticleRequestHandler : IRequestHandler<UpdateArticleRequest, int>
{
    private readonly SDSBackendContext _dbContext;

    public UpdateArticleRequestHandler(SDSBackendContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Handle(UpdateArticleRequest request, CancellationToken cancellationToken)
    {
        var article = new Article()
        {
            ArticleId = request.ArticleId,
            Sku = request.Sku,
            Name = request.Name,
            Price = request.Price,
        };

        _dbContext.Articles.Update(article);
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}