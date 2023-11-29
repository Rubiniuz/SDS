using FluentValidation;
using FluentValidation.Results;

namespace SDS.Mediator.Requests;

public class RequestValidationException : ValidationException
{
    public RequestValidationException(string message, int statusCode) : base(message,
        new[] { new ValidationFailure("", message) { CustomState = statusCode } })
    {
    }
}