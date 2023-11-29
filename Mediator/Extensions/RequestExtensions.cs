using MediatR;
using Microsoft.AspNetCore.Mvc;
using SDS.Mediator.Requests;
using SDS.Mediator.Response;

namespace SDS.Mediator.Extensions;

public static class RequestExtensions
{
    public static async Task Send(this ICreateRequest request, IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        await mediator.Send(request, cancellationToken);
    }

    public static async Task Send(this IUpdateRequest request, IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        await mediator.Send(request, cancellationToken);
    }

    public static async Task Send(this IDeleteRequest request, IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        await mediator.Send(request, cancellationToken);
    }

    public static async Task<TResult> Send<TResult>(this ICreateRequest<TResult> request, IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send(request, cancellationToken);
    }

    public static async Task<TResult> Send<TResult>(this IReadRequest<TResult> request, IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send<TResult>(request, cancellationToken);
    }

    public static async Task<TResult> Send<TResult>(this IUpdateRequest<TResult> request, IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send(request, cancellationToken);
    }

    public static async Task<ActionResult> SendAsActionResult(this IUpdateRequest request, IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        await request.Send(mediator, cancellationToken);
        return new NoContentResult();
    }

    public static async Task<ActionResult> SendAsActionResult(this IDeleteRequest request, IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        await request.Send(mediator, cancellationToken);
        return new NoContentResult();
    }

    public static async Task<ActionResult> SendAsActionResult(this ICreateRequest request, IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        await request.Send(mediator, cancellationToken);
        return new NoContentResult();
    }

    public static async Task<ActionResult<TResult>> SendAsActionResult<TResult>(this ICreateRequest<TResult> request,
        IMediator mediator, CancellationToken cancellationToken = default)
    {
        return new ObjectResult(await request.Send(mediator, cancellationToken)) { StatusCode = 201 };
    }

    public static async Task<ActionResult> SendAsCreatedResult<TResult>(this ICreateRequest<TResult> request,
        string controllerName, string actionName, IMediator mediator, CancellationToken cancellationToken = default)
    {
        var response = new CreatedResponse<TResult>(await request.Send(mediator, cancellationToken));
        var action = actionName.EndsWith("Async", StringComparison.OrdinalIgnoreCase)
            ? actionName.Replace("Async", "")//, StringComparison.OrdinalIgnoreCase)
            : actionName;
        return new CreatedAtActionResult(action, controllerName, response, response);
    }

    public static async Task<ActionResult> SendAsCreatedResult<TResult, TIdentifier>(
        this ICreateRequest<TResult> request,
        string controllerName, string actionName, TIdentifier identifier, IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        await request.Send(mediator, cancellationToken);
        var response = new CreatedResponse<TIdentifier>(identifier);
        var action = actionName.EndsWith("Async", StringComparison.OrdinalIgnoreCase)
            ? actionName.Replace("Async", "")//, StringComparison.OrdinalIgnoreCase)
            : actionName;
        return new CreatedAtActionResult(action, controllerName, response, response);
    }

    public static async Task<ActionResult> SendAsCreatedResult<TResult, TIdentifier>(
        this ICreateRequest<TResult> request,
        string controllerName, string actionName, Func<TResult, TIdentifier> identifier, IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        var result = await request.Send(mediator, cancellationToken);
        var response = new CreatedResponse<TIdentifier>(identifier(result));
        var action = actionName.EndsWith("Async", StringComparison.OrdinalIgnoreCase)
            ? actionName.Replace("Async", "")//, StringComparison.OrdinalIgnoreCase)
            : actionName;
        return new CreatedAtActionResult(action, controllerName, response, response);
    }

    public static async Task<ActionResult<TResult>> SendAsActionResult<TResult>(this IReadRequest<TResult> request,
        IMediator mediator, CancellationToken cancellationToken = default)
    {
        return new OkObjectResult(await request.Send(mediator, cancellationToken));
    }

    public static async Task<ActionResult<TResult>> SendAsActionResult<TResult>(this IUpdateRequest<TResult> request,
        IMediator mediator, CancellationToken cancellationToken = default)
    {
        return new OkObjectResult(await request.Send(mediator, cancellationToken));
    }
}
