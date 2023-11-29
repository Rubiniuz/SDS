using MediatR;

namespace SDS.Mediator.Requests;

/// <summary>
/// Defines a delete request.
/// </summary>
public interface IDeleteRequest : IRequest
{
    
}

/// <summary>
/// Defines a delete request with a response.
/// </summary>
public interface IDeleteRequest<out TResponse> : IRequest<TResponse>
{
    
}
