using System.Numerics;
using FluentValidation;
using MediatR;
using SDS.Mediator.Extensions;

namespace SDS.Requests.Articles.Validation;

public static class DoesArticleExistRequestValidatorExtensions
{
    public static IRuleBuilderOptions<T, int> ArticleMustExist<T>(this IRuleBuilder<T, int> ruleBuilder,
        IMediator mediator)
    {
        return ruleBuilder.MustAsync(async (articleId, cancellationToken) =>
                await new DoesArticleExistRequest(articleId).Send(mediator, cancellationToken))
            .WithState(_ => StatusCodes.Status404NotFound)
            .WithMessage("Article with id \"{PropertyValue}\" does not exist.");
    }

    public static IRuleBuilderOptions<T, int> ArticleMustNotExist<T>(this IRuleBuilder<T, int> ruleBuilder,
        IMediator mediator)
    {
        return ruleBuilder.MustAsync(async (articleId, cancellationToken) =>
                !await new DoesArticleExistRequest(articleId).Send(mediator, cancellationToken))
            .WithMessage("Article with id \"{PropertyValue}\" already exists.");
    }
}