using FluentValidation;
using FluentValidation.Internal;
using SDS.Mediator.Middleware;

namespace SDS.Mediator.Extensions;

public static class ValidationExceptionExtensions
{
    public static IServiceCollection AddValidationExceptionHandling(
        this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }
        
        ValidatorOptions.Global.PropertyNameResolver = (type, member, expression) => {
            string? propertyName = null;
  
            if (expression != null) {
                var chain = PropertyChain.FromExpression(expression);
                if (chain.Count > 0) propertyName = chain.ToString()!;
            }
            else {
                propertyName = member?.Name!;
            }
  
            if(string.IsNullOrEmpty(propertyName)) {
                return "Property";
            }
  
            return propertyName;
        };


        return services.AddScoped<ExceptionHandlingMiddleware>();
    }

    public static IApplicationBuilder UseValidationExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
