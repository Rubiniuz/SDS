using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using SDS.Database;
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
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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