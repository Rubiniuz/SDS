using FluentValidation;
using MediatR;
using SDS.Requests.Articles.Validation;

namespace SDS.Requests.Articles.Delete;

public class DeleteArticleRequestValidator : AbstractValidator<DeleteArticleRequest>
{
    public DeleteArticleRequestValidator(IMediator mediator)
    {
        RuleFor(request => request.ArticleId).ArticleMustExist(mediator);
    }
}