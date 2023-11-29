using MediatR;

namespace SDS.Mediator.Requests;

/// <summary>
/// Defines a request for data.
/// </summary>
/// <typeparam name="TResult">The type of data requested.</typeparam>
public interface IReadRequest<out TResult> : IRequest, IRequest<TResult> { }  


