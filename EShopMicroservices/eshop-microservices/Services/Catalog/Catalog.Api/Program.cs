
using Catalog.Api.Data;
using HealthChecks.UI.Client;
using Marten;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter(new DependencyContextAssemblyCatalog([typeof(Program).Assembly]));
//builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBahavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddMarten(option =>
{
    option.Connection(builder.Configuration.GetConnectionString("database")!);
}).UseLightweightSessions();
if (builder.Environment.IsDevelopment()) builder.Services.InitializeMartenWith<CatalogInitialData>();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("database")!);
var app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(options =>
{

});
app.UseHealthChecks("/health", new HealthCheckOptions
    {
ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
//app.UseExceptionHandler(exceptionHandlerApp =>
//{
//    exceptionHandlerApp.Run(async context =>
//    {
//        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
//        if(exception == null)
//            {
//            return;
//            }
//        var problemDetails = new ProblemDetails
//            {
//            Title = exception.Message,
//            Status = StatusCodes.Status500InternalServerError,
//            Detail = exception.StackTrace,
//            };
//        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
//        logger.LogError(exception,exception.Message);
//        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//        context.Response.ContentType = "application/problem+json";
//        await context.Response.WriteAsJsonAsync(problemDetails);
//    });
//});
app.Run();
