using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Text.Json;
using FluentValidation;
using MySqlConnector;
using SDS.Mediator.Response;

namespace SDS.Mediator.Middleware;

public class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            if (context.Response.HasStarted)
            {
                return;
            }

            int statusCode;
            string message;

            switch (exception)
            {
                case AuthenticationException authenticationException:
                    statusCode = 401;
                    message = "Unable to authenticate user. " + authenticationException.GetBaseException().Message;
                    break;
                case ForbiddenException forbiddenException:
                    statusCode = 403;
                    message = "Unable to authenticate user. " + forbiddenException.Message;
                    break;
                case MySqlException mySqlException:
                    statusCode = 500;
                    message = "An exception with the database has occured. " + mySqlException.GetBaseException().Message;
                    break;
                case ValidationException validationException:
                    statusCode = (int)Enumerable
                        .FirstOrDefault<object>(validationException.Errors
                            .Where(failure => failure.CustomState != null)
                            .Select(failure => failure.CustomState))!;//StatusCodes.Status400BadRequest
                    message = String.Join((string)"\r\n", (IEnumerable<string>)validationException.Errors.Select(failure => failure.ErrorMessage));
                    break;
                default:
                    statusCode = 500;
                    message = "An unexpected exception has occured. " + exception.GetBaseException().Message;
                    break;
            }

            var responseJson = JsonSerializer.Serialize(new ErrorResponse
            {
                Status = ((HttpStatusCode)statusCode).ToString(),
                Message = message
            });

            context.Response.ContentType = "application/json";//MediaTypeNames.Application.Json;
            context.Response.ContentLength = Encoding.UTF8.GetBytes((string)responseJson).Length;
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(responseJson, context.RequestAborted);

        }
    }
}
