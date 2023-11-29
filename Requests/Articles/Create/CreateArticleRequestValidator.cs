using FluentValidation;
using MediatR;
using SDS.Requests.Articles.Validation;

namespace SDS.Requests.Articles.Create;

public class CreateArticleRequestValidator : AbstractValidator<CreateArticleRequest>
{
    public CreateArticleRequestValidator(IMediator mediator)
    {
        RuleFor(request => request.article.ArticleId).ArticleMustNotExist(mediator);
    }
}