namespace SDS.Mediator.Response;

/// <summary>
/// Default response class for newly created resources.
/// </summary>
public class CreatedResponse : CreatedResponse<Guid>
{
    /// <summary>
    /// Constructs a new instance of the <see cref="MediatR.Response.CreatedResponse"/>.
    /// </summary>
    /// <param name="id"></param>
    public CreatedResponse(Guid id) : base(id) { }
}

/// <summary>
/// Default response class for newly created resources.
/// </summary>
/// <typeparam name="TId">The type of the identifier for the new resource.</typeparam>
public class CreatedResponse<TId>
{
    /// <summary>
    /// Constructs a new instance of the <see cref="MediatR.Response.CreatedResponse{TId}"/>.
    /// </summary>
    /// <param name="id">The identifier of the new resource.</param>
    public CreatedResponse(TId id) { Id = id; }

    /// <summary>
    /// The identifier of the new resource.
    /// </summary>
    public TId Id { get; }
}
