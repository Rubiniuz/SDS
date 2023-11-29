using FluentValidation;
using MediatR;
using SDS.Requests.Articles.Validation;

namespace SDS.Requests.Articles.Update;

public class UpdateArticleRequestValidator : AbstractValidator<UpdateArticleRequest>
{
    public UpdateArticleRequestValidator(IMediator mediator)
    {
        RuleFor(request => request.ArticleId).ArticleMustExist(mediator);
    }
}