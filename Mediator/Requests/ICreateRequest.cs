using MediatR;

namespace SDS.Mediator.Requests;

/// <summary>
/// Defines a create request.
/// </summary>
public interface ICreateRequest : IRequest
{

}

/// <summary>
/// Defines a create request with a response.
/// </summary>
public interface ICreateRequest<out TResponse> : IRequest<TResponse>
{

}
