using MediatR;

namespace SDS.Mediator.Requests;

/// <summary>
/// Defines a update request.
/// </summary>
public interface IUpdateRequest : IRequest
{
    
}

/// <summary>
/// Defines a update request with a response.
/// </summary>
public interface IUpdateRequest<out TResponse> : IRequest<TResponse>
{
    
}
