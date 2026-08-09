
using Basket.Api.Data;
using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Weasel.Storage;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter(new DependencyContextAssemblyCatalog([typeof(Program).Assembly]));
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBahavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddMarten(option =>
{
    option.Connection(builder.Configuration.GetConnectionString("database")!);
    option.Schema.For<ShoppingCart>().Identity(sc=>sc.UserName);
}).UseLightweightSessions();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});
// Add services to the container.
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!); ;
var app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(options =>
{

});
app.UseHealthChecks("/health", new HealthCheckOptions
    {
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
app.Run();

