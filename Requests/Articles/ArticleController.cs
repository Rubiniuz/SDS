using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SDS.Mediator.Extensions;
using SDS.Mediator.Response;
using SDS.Models;
using SDS.Requests.Articles.Create;
using SDS.Requests.Articles.Delete;
using SDS.Requests.Articles.Get;
using SDS.Requests.Articles.Update;

namespace SDS.Requests.Articles;

/// <summary>
///     API controller for managing Articles.
///     This is the style of controller I learned to use with mediatr.
/// </summary>
/// <response code="400">BadRequest - The operation could not be executed because the provided data is invalid.</response>
/// <response code="401">
///     Unauthorized - The request requires authentication. See message for a more specific error
///     description.
/// </response>
/// <response code="403">
///     Forbidden - Not authorized to perform the operation. See message for a more specific error
///     description.
/// </response>
/// <response code="404">NotFound - The instance is not available or does not exists in the current context.</response>
/// <response code="409">Conflict - The operation could not be executed because the instance already exists.</response>
/// <response code="412">
///     PreconditionFailed - The operation could not be executed because references have a dependency or
///     could not be resolved.
/// </response>
/// <response code="500">
///     Internal Server Error - We experienced an unexpected error. See message for a more specific error
///     description.
/// </response>
/// <response code="503">
///     Service Unavailable - One or more of the required services are unavailable. Please try again in a
///     few minutes. See message for a more specific error description.
/// </response>

// uses the controller name as route thus: api/Article 
[ApiController]
[Route("api/[controller]")] 

// defines the input and output of the request body as seen in postman
[Consumes(MediaTypeNames.Application.Json)] 
[Produces(MediaTypeNames.Application.Json)]

// status codes for validation
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)] 
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status412PreconditionFailed)]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
public class ArticleController : ControllerBase
{
    
    private readonly IMediator mediator;

    /// <summary>
    ///     Constructs a new instance of the <see cref="ArticleController" />.
    /// </summary>
    public ArticleController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    // all calls are defined below and can be found within the folder this controller is contained in.
    // for the sql that gets executed look at the request handlers.
    
    /// <summary>
    ///     Get all Articles.
    /// </summary>
    /// <param name="cancellationToken">A token which can be used to cancel the operation.</param>
    /// <returns>Return a collection of <see cref="Article" />.</returns>
    /// <remarks>Permission `EventTypeThresholds:Get` required.</remarks>
    /// <response code="200">Successful response - returns a set of event type thresholds.</response>
    [HttpGet("{Articles}")]
    [ProducesResponseType(typeof(List<Article>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Article>>> GetArticlesAsync(CancellationToken cancellationToken = default)
    {
        return await new GetArticlesRequest().SendAsActionResult(mediator, cancellationToken);
    }
    
    /// <summary>
    ///     Get specific Article.
    /// </summary>
    /// <param name="articleId">The Article Identifier the Article is associated with.</param>
    /// <param name="cancellationToken">A token which can be used to cancel the operation.</param>
    /// <returns>Return a collection of <see cref="Article" />.</returns>
    /// <remarks>Permission `EventTypeThresholds:Get` required.</remarks>
    /// <response code="200">Successful response - returns a set of event type thresholds.</response>
    [HttpGet("{ArticleId}")]
    [ProducesResponseType(typeof(Article), StatusCodes.Status200OK)]
    public async Task<ActionResult<Article>> GetArticleByIdAsync([Required] int articleId, CancellationToken cancellationToken = default)
    {
        return await new GetArticleByIdRequest(articleId).SendAsActionResult(mediator, cancellationToken);
    }

    /// <summary>
    ///     Create a Article
    /// </summary>
    /// <param name="article">The Article Model containing all the details for Article to be created.</param>
    /// <param name="cancellationToken">A token which can be used to cancel the operation.</param>
    /// <returns>Return a <see cref="Article" />.</returns>
    /// <remarks>Permission `EventTypeThresholds:Get` required.</remarks>
    /// <response code="201">Successful response - returns a set of event type thresholds.</response>
    [HttpPost("Article")]
    [ProducesResponseType(typeof(Article), StatusCodes.Status201Created)]
    public async Task<ActionResult<Article>> CreateArticleAsync([FromBody] Article article, CancellationToken cancellationToken = default)
    {
        return await new CreateArticleRequest(article).SendAsActionResult(mediator, cancellationToken);
    }

    /// <summary>
    ///     Get all Articles.
    /// </summary>
    /// <param name="articleId">The Article Id for the Article to update.</param>
    /// <param name="article">The Article Model containing all the details for Article to be created.</param>
    /// <param name="cancellationToken">A token which can be used to cancel the operation.</param>
    /// <returns>Return a collection of <see cref="Article" />.</returns>
    /// <remarks>Permission `EventTypeThresholds:Get` required.</remarks>
    /// <response code="204">Successful response - returns a set of event type thresholds.</response>
    [HttpPut("{ArticleId}")]
    [ProducesResponseType(typeof(Article), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<int>> UpdateArticleAsync([Required] int articleId, [FromBody] Article article, CancellationToken cancellationToken = default)
    {
        return await new UpdateArticleRequest(articleId, article).SendAsActionResult(mediator, cancellationToken);
    }

    /// <summary>
    ///     Get specific Article.
    /// </summary>
    /// <param name="articleId">The Article Id for the Article to update.</param>
    /// <param name="cancellationToken">A token which can be used to cancel the operation.</param>
    /// <returns>Return a collection of <see cref="Article" />.</returns>
    /// <remarks>Permission `EventTypeThresholds:Get` required.</remarks>
    /// <response code="204">Successful response - returns a set of event type thresholds.</response>
    [HttpDelete("{ArticleId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeleteArticleAsync([Required] int articleId, CancellationToken cancellationToken = default)
    {
        return await new DeleteArticleRequest(articleId).SendAsActionResult(mediator, cancellationToken);
    }
    
}