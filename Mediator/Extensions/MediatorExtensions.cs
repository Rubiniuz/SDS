using System.Runtime.CompilerServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SDS.Mediator.Requests;

namespace SDS.Mediator.Extensions;

public static class MediatorExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<TResult> ReadAsync<TResult>(this IMediator mediator, IReadRequest<TResult> query, CancellationToken cancellationToken = default)
    {
        return await mediator.Send<TResult>(query, cancellationToken);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<ActionResult<TResult>> ReadWithResultAsync<TResult>(this IMediator mediator, IReadRequest<TResult> query, CancellationToken cancellationToken = default)
    {
        return new OkObjectResult((await ReadAsync<TResult>(mediator, query, cancellationToken)));
    }
}
