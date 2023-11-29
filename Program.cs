using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SDS.Database;
using SDS.Mediator.Extensions;
using SDS.Mediator.Requests;
using SDS.Mediator.Validation;
using SDS.Requests.Articles;
using SDS.Requests.Articles.Create;
using SDS.Requests.Articles.Delete;
using SDS.Requests.Articles.Update;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

// Add the database context
builder.Services.AddDbContext<SDSBackendContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("SDS"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("SDS")));
});

// Fixed in later versions of .NET
builder.Services.AddMvc(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

// Add all the containers and generate the swagger files accordingly
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add validation
var mediatorAssemblies = new[]
{
    typeof(Program).Assembly, // This assembly
};

builder.Services.AddMediatR(options => options.RegisterServicesFromAssemblies(mediatorAssemblies));
builder.Services.AddValidatorsFromAssemblies(mediatorAssemblies);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
builder.Services.AddValidationExceptionHandling();

// Proceed to build the app
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SDS Test API");
    c.RoutePrefix = "";
});

app.UseRouting();
app.MapControllers();

app.Run();